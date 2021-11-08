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



	public class LORCosmic4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		// Cosmic Color Devices are basically the same thing as ChannelGroups
		// Channel Groups and Cosmic Devices are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Cosmic Devices can (?!?!) contain regular Channels, RGB Channels, and other groups.
		// Cosmic Devices can (?!?!) be nested many levels deep (limit?).
		// See additional notes in the LORChannelGroup4 class

		public const string TABLEcosmicDeviceDevice = "colorCosmicDevice";
		private const string STARTcosmicDevice = lutils.STFLD + TABLEcosmicDeviceDevice + lutils.SPC;
		public LORMembership4 Members; // = new LORMembership4(this);

		//! CONSTRUCTORS
		public LORCosmic4(string theName, int theSavedIndex)
		{
			myName = theName;
			myID = theSavedIndex;
			Members = new LORMembership4(this);
		}

		public LORCosmic4(iLORMember4 theParent, string lineIn)
		{
			myParent = theParent;
			Members = new LORMembership4(this);
			Parse(lineIn);
		}


		//! PROPERTIES, METHODS, ETC.
		public override int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value != myCentiseconds)
				{
					myCentiseconds = value;
					for (int idx = 0; idx < Members.Count; idx++)
					{
						Members.Items[idx].Centiseconds = value;
					}
					if (myParent != null) myParent.MakeDirty(true);

					//if (myCentiseconds > Parent.Centiseconds)
					//{
					//	Parent.Centiseconds = value;
					//}
				}
			}
		}


		public override LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Cosmic;
			}
		}


		public override string LineOut()
		{
			return LineOut(false);
		}

		public override void Parse(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLEcosmicDevice + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
				myID = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
				//Members = new LORMembership4(this);
				myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			}
			else
			{
				if (lineIn.Length > 1)
				{
					myName = lineIn;
				}
			}
			//if (myParent != null) myParent.MakeDirty(true);
		}

		public override iLORMember4 Clone()
		{
			LORCosmic4 cosm = (LORCosmic4)Clone();
			return cosm;
		}

		public override iLORMember4 Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			LORCosmic4 cosm = (LORCosmic4)this.Clone();
			ChangeName(newName);
			return cosm;
		}

		

		public string LineOut(bool selectedOnly)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEcosmicDevice);

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

			ret.Append(lutils.CRLF); ;
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
			ret.Append(LORSequence4.TABLEcosmicDevice);
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
				if (myParent != null) myParent.MakeDirty(true);
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

		//TODO: add RemoveItem procedure
	}
}
