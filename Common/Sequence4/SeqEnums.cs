//!////////////////////////////////////////////////////////////////////////////////////////////////////
//!                                                                                                ///
//?  SeqEnums: LOR4Sequence related Enumerators                                                   ///
//?  Including: LORDeviceType4 - Controllers such as LOR, DMX, Cosmic, etc.                      ///
//?					LOREffectType4 - Intensity, Shimmer, Twinkle, Fade-Up, etc.                        ///
//?					LORTimingGridType4 - Freeform or Fixed.                                           ///
//?					LORRGBChild4 - Red, Green, Blue.                                                 ///
//?					LOR4SequenceType - Musical, Animation, Visualization                            ///
//?					LOR4MemberType - Channel, RGB Channel, Channel Group, Track, Timing Grid, etc. ///
//!                                                                                        ///
//!//////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOR4Utils
{
	public enum LORDeviceType4
	{ None = lutils.UNDEFINED, LOR = 1, DMX = 7, Digital = 3, Cosmic = 2, Dasher = 4 };

	//public enum LOREffectType4 { None=lutils.UNDEFINED, Intensity=1, Shimmer, Twinkle, DMX }
	public enum LOREffectType4 { None = lutils.UNDEFINED, Intensity = 1, Shimmer, Twinkle, DMX, Constant, FadeUp, FadeDown }

	//internal enum timingGridType
	public enum LORTimingGridType4
	{ None = lutils.UNDEFINED, Freeform = 1, FixedGrid };

	//internal enum LOR4MemberType
	//	public enum LOR4MemberType
	//	{ None=lutils.UNDEFINED, LOR4Channel=1, LOR4RGBChannel=2, LOR4ChannelGroup=4, LORTrack4=8, LORTimings4=16, Items=7, FullTrack=15; RGBonly=14, RegularOnly=13, All=31, Sequence=32 }
	public enum LORRGBChild4
	{ None = lutils.UNDEFINED, Red = 1, Green, Blue }

	public enum LOR4SequenceType
	{ Undefined = lutils.UNDEFINED, Animated = 1, Musical, Clipboard, ChannelConfig, Visualizer }

	public enum MatchType
	{ Unmatched = 0, Source = -1, Destination = 1 }

	public enum LOR4MemberType
	{
		// * MEMBER TYPES *
		None = 0,
		Channel = LOR4SeqEnums.MEMBER_Channel,
		RGBChannel = LOR4SeqEnums.MEMBER_RGBchannel,
		ChannelGroup = LOR4SeqEnums.MEMBER_ChannelGroup,
		Cosmic = LOR4SeqEnums.MEMBER_CosmicDevice,
		Track = LOR4SeqEnums.MEMBER_Track,
		Timings = LOR4SeqEnums.MEMBER_TimingGrid,
		Sequence = LOR4SeqEnums.MEMBER_Sequence,
		RegularOnly = LOR4SeqEnums.MEMBER_Channel |
										LOR4SeqEnums.MEMBER_ChannelGroup |
										LOR4SeqEnums.MEMBER_Track,
		RGBonly = LOR4SeqEnums.MEMBER_RGBchannel |
										LOR4SeqEnums.MEMBER_ChannelGroup |
										LOR4SeqEnums.MEMBER_Track,
		GroupsOnly = LOR4SeqEnums.MEMBER_ChannelGroup |
										LOR4SeqEnums.MEMBER_Track,
		Items = LOR4SeqEnums.MEMBER_Channel |
										LOR4SeqEnums.MEMBER_RGBchannel |
										LOR4SeqEnums.MEMBER_ChannelGroup |
										LOR4SeqEnums.MEMBER_CosmicDevice,
		FullTrack = LOR4SeqEnums.MEMBER_Channel |
										LOR4SeqEnums.MEMBER_RGBchannel |
										LOR4SeqEnums.MEMBER_ChannelGroup |
										LOR4SeqEnums.MEMBER_CosmicDevice |
										LOR4SeqEnums.MEMBER_Track,
		Visualization = LOR4SeqEnums.MEMBER_Vizualization,
		VizChannel = LOR4SeqEnums.MEMBER_VizChannel,
		VizDrawObject = LOR4SeqEnums.MEMBER_VizDrawObject,
		VizItemGroup = LOR4SeqEnums.MEMBER_VizItemGroup

		// What is RegularOnly s'posed to be for?
		// Should I include CosmicDevice in groups?
		// What is RGBonly for, and why does it contain groups and tracks?
	}

	public static class LOR4SeqEnums
	{
		// * MEMBER TYPES *
		public const int MEMBER_Channel = 1;
		public const int MEMBER_RGBchannel = 2;
		public const int MEMBER_ChannelGroup = 4;
		public const int MEMBER_CosmicDevice = 8;
		public const int MEMBER_Track = 16;
		public const int MEMBER_TimingGrid = 32;
		public const int MEMBER_Sequence = 64;
		public const int MEMBER_Vizualization = 256;
		public const int MEMBER_VizChannel = 512;
		public const int MEMBER_VizDrawObject = 1024;
		public const int MEMBER_VizItemGroup = 2048;

		public const string DEVICElor = "LOR";
		public const string DEVICEdmx = "DMX Universe";
		public const string DEVICEdigital = "Digital IO";
		public const string DEVICEcosmic = "Cosmic Color Device";
		public const string DEVICEdasher = "Dasher";
		public const string DEVICEnone = "None";

		public const string EFFECTintensity = "intensity";
		public const string EFFECTshimmer = "shimmer";
		public const string EFFECTtwinkle = "twinkle";
		public const string EFFECTdmx = "DMX intensity";
		public const string EFFECTconstant = "Constant";
		public const string EFFECTfadeUp = "Fade Up";
		public const string EFFECTfadeDown = "Fade Down";

		public const string GRIDfreeform = "freeform";
		public const string GRIDfixed = "fixed";

		public const string OBJnone = "None";
		public const string OBJchannel = "Channel";
		public const string OBJrgbChannel = "RGBChannel";
		public const string OBJchannelGroup = "ChannelGroup";
		public const string OBJcosmicDevice = "CosmicDevice";
		public const string OBJtrack = "Track";
		public const string OBJtimingGrid = "Timings";
		public const string OBJsequence = "Sequence";
		public const string OBJall = "All";
		public const string OBJitems = "Items";


		public static LORDeviceType4 EnumDevice(string deviceName)
		{
			LORDeviceType4 valueOut = LORDeviceType4.None;

			if (deviceName == DEVICElor)
			{
				valueOut = LORDeviceType4.LOR;
			}
			else if (deviceName == "1") // Visualizer
			{
				valueOut = LORDeviceType4.LOR;
			}
			else if (deviceName == DEVICEdmx)
			{
				valueOut = LORDeviceType4.DMX;
			}
			else if (deviceName == "7") // Visualizer
			{
				valueOut = LORDeviceType4.DMX;
			}
			else if (deviceName == DEVICEdigital)
			{
				valueOut = LORDeviceType4.Digital;
			}
			else if (deviceName == DEVICEdasher)
			{
				valueOut = LORDeviceType4.Dasher;
			}
			else if (deviceName == DEVICEcosmic)
			{
				valueOut = LORDeviceType4.Cosmic;
			}
			else if (deviceName == "")
			{
				valueOut = LORDeviceType4.None;
			}
			else
			{
				// TODO: throw exception here!
				valueOut = LORDeviceType4.None;
				string sMsg = "Unrecognized Device Type: ";
				sMsg += deviceName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Diagnostics.Debugger.Break();
			}

			return valueOut;
		}

		public static LOREffectType4 EnumEffectType(string effectName)
		{
			LOREffectType4 valueOut = LOREffectType4.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = LOREffectType4.Intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = LOREffectType4.Shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = LOREffectType4.Twinkle;
			}
			else if (effectName == EFFECTdmx)
			{
				valueOut = LOREffectType4.DMX;
			}
			else
			{
				// TODO: throw exception here
				valueOut = LOREffectType4.None;
				string sMsg = "Unrecognized Effect Name: ";
				sMsg += effectName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static LORTimingGridType4 EnumGridType(string typeName)
		{
			LORTimingGridType4 valueOut = LORTimingGridType4.None;

			if (typeName == GRIDfreeform)
			{
				valueOut = LORTimingGridType4.Freeform;
			}
			else if (typeName == "fixed")
			{
				valueOut = LORTimingGridType4.FixedGrid;
			}
			else
			{
				// TODO: throw exception here
				valueOut = LORTimingGridType4.None;
				string sMsg = "Unrecognized Timing Grid Type: ";
				sMsg += typeName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static LOR4MemberType EnumMemberType(string typeName)
		{
			// Only supported the 5 needed for Identity owners
			LOR4MemberType valueOut = LOR4MemberType.None;
			if (typeName == OBJchannel)
			{
				valueOut = LOR4MemberType.Channel;
			}
			else if (typeName == OBJrgbChannel)
			{
				valueOut = LOR4MemberType.RGBChannel;
			}
			else if (typeName == OBJchannelGroup)
			{
				valueOut = LOR4MemberType.ChannelGroup;
			}
			else if (typeName == OBJtrack)
			{
				valueOut = LOR4MemberType.Track;
			}
			else if (typeName == OBJtimingGrid)
			{
				valueOut = LOR4MemberType.Timings;
			}
			else if (typeName == OBJsequence)
			{
				valueOut = LOR4MemberType.Sequence;
			}
			else if (typeName == OBJcosmicDevice)
			{
				valueOut = LOR4MemberType.Cosmic;
			}
			return valueOut;
		}


		public static string DeviceName(LORDeviceType4 devType)
		{
			string valueOut = "";
			switch (devType)
			{
				case LORDeviceType4.LOR:
					valueOut = DEVICElor;
					break;

				case LORDeviceType4.DMX:
					valueOut = DEVICEdmx;
					break;

				case LORDeviceType4.None:
					valueOut = DEVICEnone;
					break;

				case LORDeviceType4.Digital:
					valueOut = DEVICEdigital;
					break;

				case LORDeviceType4.Cosmic:
					valueOut = DEVICEcosmic;
					break;

				case LORDeviceType4.Dasher:
					valueOut = DEVICEdasher;
					break;

				default:
					// TODO: throw exception here
					valueOut = DEVICEnone;
					string sMsg = "Unrecognized Device Type: ";
					sMsg += devType.ToString();
					//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
					System.Diagnostics.Debugger.Break();
					break;

					//TODO: Other device types, such as cosmic color ribbon and ...
			}
			return valueOut;
		}

		public static string EffectTypeName(LOREffectType4 effType)
		{
			// Note: This is the human friendly version
			// For the versio used for outputing to sequenc files, see 'EffectName' below
			string valueOut = "";
			switch (effType)
			{
				case LOREffectType4.Intensity:
					valueOut = EFFECTintensity;
					break;

				case LOREffectType4.Shimmer:
					valueOut = EFFECTshimmer;
					break;

				case LOREffectType4.Twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case LOREffectType4.DMX:
					valueOut = EFFECTdmx;
					break;

				case LOREffectType4.Constant:
					valueOut = EFFECTconstant;
					break;

				case LOREffectType4.FadeUp:
					valueOut = EFFECTfadeUp;
					break;

				case LOREffectType4.FadeDown:
					valueOut = EFFECTfadeDown;
					break;
			}
			return valueOut;
		}

		public static string EffectName(LOREffectType4 effType)
		{
			// Note: This is the versio used for outputing to sequence files.
			// For the human friendly version, see 'EffectTypeName' above

			// Use this as default unless overridden below
			string valueOut = EFFECTintensity;
			switch (effType)
			{
				case LOREffectType4.Shimmer:
					valueOut = EFFECTshimmer;
					break;

				case LOREffectType4.Twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case LOREffectType4.DMX:
					valueOut = EFFECTdmx;
					break;
			}
			return valueOut;
		}



		public static string TimingName(LORTimingGridType4 grdType)
		{
			string valueOut = "";
			switch (grdType)
			{
				case LORTimingGridType4.Freeform:
					valueOut = GRIDfreeform;
					break;

				case LORTimingGridType4.FixedGrid:
					valueOut = GRIDfixed;
					break;
			}
			return valueOut;
		}

		public static string MemberName(LOR4MemberType memberType)
		{
			// Only doing the 5 needed for Members
			string valueOut = "";
			switch (memberType)
			{
				case LOR4MemberType.Channel:
					valueOut = OBJchannel;
					break;
				case LOR4MemberType.RGBChannel:
					valueOut = OBJrgbChannel;
					break;
				case LOR4MemberType.ChannelGroup:
					valueOut = OBJchannelGroup;
					break;
				case LOR4MemberType.Cosmic:
					valueOut = OBJcosmicDevice;
					break;
				case LOR4MemberType.Track:
					valueOut = OBJtrack;
					break;
				case LOR4MemberType.Timings:
					valueOut = OBJtimingGrid;
					break;
				case LOR4MemberType.Sequence:
					valueOut = OBJsequence;
					break;
			}
			return valueOut;
		}





	} // end clas LOR4SeqEnums



} // end namespace LOR4Utils
