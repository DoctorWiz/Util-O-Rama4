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

	//////////////////////////////////////////////
	// 
	//		RGB CHANNEL
	//
	////////////////////////////////////////////////

	public class LORRGBChannel4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		public LORChannel4 redChannel = null;
		public LORChannel4 grnChannel = null;
		public LORChannel4 bluChannel = null;

		//! CONSTRUCTOR(s)
		public LORRGBChannel4(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLErgbChannel + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}

		public LORRGBChannel4(string lineIn, int theSavedIndex)
		{
			string seek = lutils.STFLD + LORSequence4.TABLErgbChannel + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
			SetSavedIndex(theSavedIndex);
		}


		//! OTHER PROPERTIES, METHODS, ETC.
		public new int Centiseconds
		{
			get
			{
				int cs = 0;
				if (redChannel != null)
				{
					cs = redChannel.Centiseconds;
				}
				if (grnChannel != null)
				{
					if (grnChannel.Centiseconds > cs) cs = grnChannel.Centiseconds;
				}
				if (bluChannel != null)
				{
					if (bluChannel.Centiseconds > cs) cs = bluChannel.Centiseconds;
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
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							if (redChannel != null)
							{
								if (redChannel.Centiseconds > value) redChannel.Centiseconds = value;
							}
							if (grnChannel != null)
							{
								if (grnChannel.Centiseconds > value) grnChannel.Centiseconds = value;
							}
							if (bluChannel != null)
							{
								if (bluChannel.Centiseconds > value) bluChannel.Centiseconds = value;
							}

							//if (parentSequence != null) parentSequence.MakeDirty(true);

							//if (myCentiseconds > ParentSequence.Centiseconds)
							//{
							//	ParentSequence.Centiseconds = value;
							//}
						}
					}
				}
			}
		}

		public new LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.RGBChannel;
			}
		}

		public new string LineOut()
		{
			return LineOut(false, false, LORMemberType4.FullTrack);
		}

		public new void Parse(string lineIn)
		{
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			//if (parentSequence != null) parentSequence.MakeDirty(true);
		}


		public new iLORMember4 Clone()
		{
			LORRGBChannel4 rgb = (LORRGBChannel4)this.Clone();
			rgb.redChannel = (LORChannel4)redChannel.Clone();
			rgb.grnChannel = (LORChannel4)grnChannel.Clone();
			rgb.bluChannel = (LORChannel4)bluChannel.Clone();
			return rgb;
		}

		public new iLORMember4 Clone(string newName)
		{
			LORRGBChannel4 rgb = (LORRGBChannel4)this.Clone();  //   new LORRGBChannel4(newName, lutils.UNDEFINED);
			rgb.ChangeName(newName);
			return rgb;
		}

		public string LineOut(bool selectedOnly, bool noEffects, LORMemberType4 itemTypes)
		{
			StringBuilder ret = new StringBuilder();

			//int redSavedIndex = lutils.UNDEFINED;
			//int grnSavedIndex = lutils.UNDEFINED;
			//int bluSavedIndex = lutils.UNDEFINED;

			int AltSavedIndex = lutils.UNDEFINED;

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Channel))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent LORRGBChannel4 is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.RGBChannel))
			{
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.STFLD);
				ret.Append(LORSequence4.TABLErgbChannel);
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

				// Start SubChannels
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.PLURAL);
				ret.Append(lutils.FINFLD);

				// RED subchannel
				AltSavedIndex = redChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// GREEN subchannel
				AltSavedIndex = grnChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// BLUE subchannel
				AltSavedIndex = bluChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// End SubChannels
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.FINTBL);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.PLURAL);
				ret.Append(lutils.FINFLD);

				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.FINTBL);
				ret.Append(LORSequence4.TABLErgbChannel);
				ret.Append(lutils.FINFLD);
			} // end LORMemberType4 Channel or LORRGBChannel4

			return ret.ToString();
		} // end LineOut

		public new int UniverseNumber
		{
			get
			{
				int ret = 0;
				if (redChannel != null)
				{
					ret = redChannel.output.network;
				}
				return ret;
			}
		}
		public new int DMXAddress
		{
			get
			{
				int ret = 0;
				if (redChannel != null)
				{
					ret = redChannel.output.channel;
				}
				return ret;
			}
		}

	} // end LORRGBChannel4 Class
}
