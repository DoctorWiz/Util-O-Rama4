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

	//////////////////////////////////////////////
	// 
	//		RGB CHANNEL
	//
	////////////////////////////////////////////////

	public class LOR4RGBChannel : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
	{
		public LOR4Channel redChannel = null;
		public LOR4Channel grnChannel = null;
		public LOR4Channel bluChannel = null;

		//! CONSTRUCTOR(s)
		public LOR4RGBChannel(string lineIn)
		{
			string seek = lutils.STFLD + LOR4Sequence.TABLErgbChannel + lutils.FIELDtotalCentiseconds;
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

		public LOR4RGBChannel(string lineIn, int theSavedIndex)
		{
			string seek = lutils.STFLD + LOR4Sequence.TABLErgbChannel + lutils.FIELDtotalCentiseconds;
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
						if (Fyle.isWiz)
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
							if (Fyle.isWiz)
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

		public new LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.RGBChannel;
			}
		}

		public new string LineOut()
		{
			return LineOut(false, false, LOR4MemberType.FullTrack);
		}

		public new void Parse(string lineIn)
		{
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			//if (parentSequence != null) parentSequence.MakeDirty(true);
		}


		public new iLOR4Member Clone()
		{
			LOR4RGBChannel rgb = (LOR4RGBChannel)this.Clone();
			rgb.redChannel = (LOR4Channel)redChannel.Clone();
			rgb.grnChannel = (LOR4Channel)grnChannel.Clone();
			rgb.bluChannel = (LOR4Channel)bluChannel.Clone();
			return rgb;
		}

		public new iLOR4Member Clone(string newName)
		{
			LOR4RGBChannel rgb = (LOR4RGBChannel)this.Clone();  //   new LOR4RGBChannel(newName, lutils.UNDEFINED);
			rgb.ChangeName(newName);
			return rgb;
		}

		public string LineOut(bool selectedOnly, bool noEffects, LOR4MemberType itemTypes)
		{
			StringBuilder ret = new StringBuilder();

			//int redSavedIndex = lutils.UNDEFINED;
			//int grnSavedIndex = lutils.UNDEFINED;
			//int bluSavedIndex = lutils.UNDEFINED;

			int AltSavedIndex = lutils.UNDEFINED;

			if ((itemTypes == LOR4MemberType.Items) || (itemTypes == LOR4MemberType.Channel))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent LOR4RGBChannel is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == LOR4MemberType.Items) || (itemTypes == LOR4MemberType.RGBChannel))
			{
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.STFLD);
				ret.Append(LOR4Sequence.TABLErgbChannel);
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
				ret.Append(LOR4Sequence.TABLErgbChannel);
				ret.Append(lutils.FINFLD);
			} // end LOR4MemberType Channel or LOR4RGBChannel

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

	} // end LOR4RGBChannel Class
}
