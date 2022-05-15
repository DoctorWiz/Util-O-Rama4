using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LOR4Utils
{



	public class LOR4Cosmic : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
	{
		// Cosmic Color Devices are basically the same thing as ChannelGroups
		// Channel Groups and Cosmic Devices are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Cosmic Devices can (?!?!) contain regular Channels, RGB Channels, and other groups.
		// Cosmic Devices can (?!?!) be nested many levels deep (limit?).
		// See additional notes in the LOR4ChannelGroup class

		public const string TABLEcosmicDeviceDevice = "colorCosmicDevice";
		private const string STARTcosmicDevice = lutils.STFLD + TABLEcosmicDeviceDevice + lutils.SPC;
		public LOR4Membership Members; // = new LOR4Membership(this);

		//! CONSTRUCTORS
		public LOR4Cosmic(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new LOR4Membership(this);
		}

		public LOR4Cosmic(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new LOR4Membership(this);
			string seek = lutils.STFLD + LOR4Sequence.TABLEcosmicDevice + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				if (lineIn.Length > 1)
				{
					myName = lineIn;
				}
			}
		}


		//! PROPERTIES, METHODS, ETC.
		public new int Centiseconds
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


		public new LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.Cosmic;
			}
		}


		public new string LineOut()
		{
			return LineOut(false);
		}

		public new void Parse(string lineIn)
		{
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			Members = new LOR4Membership(this);
			//Members = new LOR4Membership(this);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (myParent != null) myParent.MakeDirty(true);
		}

		public new iLOR4Member Clone()
		{
			LOR4Cosmic cosm = (LOR4Cosmic)Clone();
			return cosm;
		}

		public new iLOR4Member Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			LOR4Cosmic cosm = (LOR4Cosmic)this.Clone();
			ChangeName(newName);
			return cosm;
		}



		public string LineOut(bool selectedOnly)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LOR4Sequence.TABLEcosmicDevice);

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
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF); ;
			ret.Append(lutils.STFLD);
			ret.Append(LOR4Sequence.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			foreach (iLOR4Member member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > lutils.UNDEFINED)
				{
					ret.Append(lutils.CRLF);
					ret.Append(lutils.LEVEL4);
					ret.Append(lutils.STFLD);
					ret.Append(LOR4Sequence.TABLEchannelGroup);

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
			ret.Append(LOR4Sequence.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(LOR4Sequence.TABLEcosmicDevice);
			ret.Append(lutils.FINFLD);

			return ret.ToString();
		}

		public int AddItem(iLOR4Member newPart)
		{
			int retSI = lutils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this Channel Group '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "LOR4Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
						//TODO: Make this just a warning, put "add" code below into an else block
						//TODO: Do the same with Tracks
						alreadyAdded = true;
					retSI = newPart.SavedIndex;
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
				LOR4Sequence mySeq = (LOR4Sequence)myParent;
				iLOR4Member newPart = mySeq.Members.bySavedIndex[itemSavedIndex];
				ret = AddItem(newPart);
			}
			return ret;
		}

		public new int UniverseNumber
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
		public new int DMXAddress
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
		//TODO: add RemoveItem procedure
	}
}
