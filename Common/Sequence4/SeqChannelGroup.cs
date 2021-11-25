using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LORUtils4
{
	public class LORChannelGroup4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		// Channel Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All Channel Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// Channel groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the LORTrack4 class)

		public const string TABLEchannelGroupList = "channelGroupList";
		private const string STARTchannelGroup = lutils.STFLD + TABLEchannelGroupList + lutils.SPC;

		public LORMembership4 Members; // = new LORMembership4(this);

		//! CONSTRUCTORS

		public LORChannelGroup4(iLORMember4 theParent, string lineIn)
		{
			myParent = theParent;
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new LORMembership4(this);
			Parse(lineIn);
		}



		//! PROPERTIES, METHODS, ETC.
		public override int Centiseconds
		{
			get
			{
				int cs = 0;
				for (int idx = 0; idx < Members.Count; idx++)
				{
					iLORMember4 mbr = Members[idx];
					if (Members.Items[idx].Centiseconds > cs)
					{
						cs = Members.Items[idx].Centiseconds;
					}
				}
				myCentiseconds = cs;
				return cs;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.DebugMode)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < lutils.MINCentiseconds)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.DebugMode)
							{
								//System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							for (int idx = 0; idx < Members.Count; idx++)
							{
								iLORMember4 mbr = Members[idx];
								if (Members.Items[idx].Centiseconds > value)
								{
									Members.Items[idx].Centiseconds = value;
								}
							}
							if (myParent != null)
							{
								if (myParent.Centiseconds < value)
								{
									myParent.Centiseconds = value;
								}
							}

							//if (myParent != null) myParent.MakeDirty(true);

							//if (myCentiseconds > Parent.Centiseconds)
							//{
							//	Parent.Centiseconds = value;
							//}
						}
					}
				}
			}
		}

		public override LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.ChannelGroup;
			}
		}

		public override string LineOut()
		{
			return LineOut(false);
		}

		public override void Parse(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLEchannelGroupList + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
				if (myName.IndexOf("inese 27") > 0)
				{
					//System.Diagnostics.Debugger.Break();
				}
				myID = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
				Members = new LORMembership4(this);
				//Members = new LORMembership4(this);
				myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
				//if (myParent != null) myParent.MakeDirty(true);
				this.MakeDirty(true);
			}
			else
			{
				if (lineIn.Length > 1)
				{
					myName = lineIn;
				}
			}
		}

		public int Count
		{ get { return Members.Count; } }


		public override iLORMember4 Clone()
		{
			LORChannelGroup4 grp = (LORChannelGroup4)Clone();
			grp.Members = Members;
			return grp;
		}

		public override iLORMember4 Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			LORChannelGroup4 grp = (LORChannelGroup4)Clone();
			ChangeName(newName);
			return grp;
		}


		public string LineOut(bool selectedOnly)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEchannelGroupList);

			ret.Append(lutils.FIELDtotalCentiseconds);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDname);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(myName));
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDsavedIndex);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myAltID.ToString());
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			foreach (iLORMember4 member in Members.Items)
			{
				int osi = member.ID;
				int asi = member.AltID;
				if (asi > lutils.UNDEFINED)
				{
					ret.Append(lutils.CRLF);
					ret.Append(lutils.LEVEL4);
					ret.Append(lutils.STFLD);
					ret.Append(LORSequence4.TABLEchannelGroup);

					ret.Append(lutils.FIELDsavedIndex);
					ret.Append(lutils.FIELDEQ);
					ret.Append(asi.ToString());
					ret.Append(lutils.ENDQT);
					ret.Append(lutils.ENDFLD);
				}
			}
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEchannelGroupList);
			ret.Append(lutils.FINFLD);

			return ret.ToString();
		}

		public int AddItem(iLORMember4 newPart)
		{
			int retSI = lutils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.ID == Members.Items[i].ID)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this Channel Group '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "LORChannel4 Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
						//TODO: Make this just a warning, put "add" code below into an else block
						//TODO: Do the same with Tracks
						alreadyAdded = true;
					retSI = newPart.ID;
					i = Members.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				retSI = Members.Add(newPart);
				//if (myParent != null) myParent.MakeDirty(true);
				this.MakeDirty(true);
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			// Adds an EXISTING item to this Group's membership
			int ret = lutils.UNDEFINED;
			if (myParent != null)
			{
				LORSequence4 mySeq = (LORSequence4)myParent;
				iLORMember4 newPart = mySeq.AllMembers.BySavedIndex[itemSavedIndex];
				this.MakeDirty(true);
				ret = AddItem(newPart);
			}
			return ret;
		}

		public override int UniverseNumber
		{
			get
			{
				if (Members.Count > 0)
				{
					return Members[0].UniverseNumber;
				}
				else
				{
					return 0;
				}
			}
		}
		public override int DMXAddress
		{
			get
			{
				if (Members.Count > 0)
				{
					return Members[0].DMXAddress;
				}
				else
				{
					return 0;
				}
			}
		}

		//public int SavedIndex
		//{ get { return myID; } }
		//public void SetSavedIndex(int newSavedIndex)
		//{ myID = newSavedIndex; }
		//public int AltSavedIndex
		//{ get { return myAltID; } set { myAltID = value; } }

		public override int color
		{
			get
			{
				if (Members.Count > 0)
				{
					return Members[0].color;
				}
				else
				{
					return 0;
				}
			}
			set
			{
				int ignore = value;
			}
		}

		public override Color Color
		{
			get { return lutils.Color_LORtoNet(this.color); }
			set { Color ignore = value; }
		}

		//TODO: add RemoveItem procedure
	}
}
