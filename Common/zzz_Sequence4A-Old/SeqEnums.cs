using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LORUtils4
{
	public enum LORDeviceType4
	{ None=lutils.UNDEFINED, LOR=1, DMX=7, Digital=3, LORCosmic=2, Dasher=4 };

	//public enum LOREffectType4 { None=lutils.UNDEFINED, Intensity=1, Shimmer, Twinkle, DMX }
	public enum LOREffectType4 { None = lutils.UNDEFINED, Intensity = 1, Shimmer, Twinkle, DMX, Constant, FadeUp, FadeDown }

	//internal enum timingGridType
	public enum LORTimingGridType4
	{ None=lutils.UNDEFINED, Freeform=1, FixedGrid };

	//internal enum LORMemberType4
//	public enum LORMemberType4
//	{ None=lutils.UNDEFINED, LORChannel4=1, LORRGBChannel4=2, LORChannelGroup4=4, LORTrack4=8, LORTimings4=16, Items=7, FullTrack=15; RGBonly=14, RegularOnly=13, All=31, Sequence=32 }
	public enum LORRGBChild4
	{ None=lutils.UNDEFINED, Red=1, Green, Blue }

	public enum LORSequenceType4
	{ Undefined=lutils.UNDEFINED, Animated=1, Musical, Clipboard, ChannelConfig, Visualizer }

	public enum MatchType
	{ Unmatched=0, Source=-1, Destination=1}

	public enum LORMemberType4
	{
		// * MEMBER TYPES *
		None =					0,
		Channel =				LORSeqEnums4.MEMBER_Channel,
		RGBChannel =		LORSeqEnums4.MEMBER_RGBchannel,
		ChannelGroup =	LORSeqEnums4.MEMBER_ChannelGroup,
		Cosmic =				LORSeqEnums4.MEMBER_CosmicDevice,
		Track =					LORSeqEnums4.MEMBER_Track,
		Timings =				LORSeqEnums4.MEMBER_TimingGrid,
		Sequence =			LORSeqEnums4.MEMBER_Sequence,
		RegularOnly =		LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		RGBonly =				LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		GroupsOnly =		LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		Items =					LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup,
		FullTrack =			LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		Visualization	= LORSeqEnums4.MEMBER_Vizualization,
		VizChannel4 =		LORSeqEnums4.MEMBER_VizChannel,
		VizObject =			LORSeqEnums4.MEMBER_VizDrawObject

	}

	public static class LORSeqEnums4
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
				valueOut = LORDeviceType4.LORCosmic;
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

		public static LORMemberType4 EnumMemberType(string typeName)
		{
			// Only supported the 5 needed for Identity owners
			LORMemberType4 valueOut = LORMemberType4.None;
			if (typeName == OBJchannel)
			{
				valueOut = LORMemberType4.Channel;
			}
			else if (typeName == OBJrgbChannel)
			{
				valueOut = LORMemberType4.RGBChannel;
			}
			else if (typeName == OBJchannelGroup)
			{
				valueOut = LORMemberType4.ChannelGroup;
			}
			else if (typeName == OBJtrack)
			{
				valueOut = LORMemberType4.Track;
			}
			else if (typeName == OBJtimingGrid)
			{
				valueOut = LORMemberType4.Timings;
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

				case LORDeviceType4.LORCosmic:
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

		public static string MemberName(LORMemberType4 memberType)
		{
			// Only doing the 5 needed for Members
			string valueOut = "";
			switch (memberType)
			{
				case LORMemberType4.Channel:
					valueOut = OBJchannel;
					break;
				case LORMemberType4.RGBChannel:
					valueOut = OBJrgbChannel;
					break;
				case LORMemberType4.ChannelGroup:
					valueOut = OBJchannelGroup;
					break;
				case LORMemberType4.Track:
					valueOut = OBJtrack;
					break;
				case LORMemberType4.Timings:
					valueOut = OBJtimingGrid;
					break;
				case LORMemberType4.Sequence:
					valueOut = OBJsequence;
					break;
			}
			return valueOut;
		}





	} // end clas LORSeqEnums4



} // end namespace LORUtils4
