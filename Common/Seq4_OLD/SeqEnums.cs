using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOR4Utils
{
	public enum LOR4DeviceType
	{ None = lutils.UNDEFINED, LOR = 1, DMX = 7, Digital = 3, LOR4Cosmic = 2, Dasher = 4 };

	//public enum LOR4EffectType { None=lutils.UNDEFINED, Intensity=1, Shimmer, Twinkle, DMX }
	public enum LOR4EffectType { None = lutils.UNDEFINED, Intensity = 1, Shimmer, Twinkle, DMX, Constant, FadeUp, FadeDown }

	//internal enum timingGridType
	public enum LOR4TimingGridType
	{ None = lutils.UNDEFINED, Freeform = 1, FixedGrid };

	//internal enum LOR4MemberType
	//	public enum LOR4MemberType
	//	{ None=lutils.UNDEFINED, LOR4Channel=1, LOR4RGBChannel=2, LOR4ChannelGroup=4, LORTrack4=8, LORTimings4=16, Items=7, FullTrack=15; RGBonly=14, RegularOnly=13, All=31, Sequence=32 }
	public enum LOR4RGBChild
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
		RegularOnly = LOR4SeqEnums.MEMBER_Channel & LOR4SeqEnums.MEMBER_ChannelGroup & LOR4SeqEnums.MEMBER_Track,
		RGBonly = LOR4SeqEnums.MEMBER_RGBchannel & LOR4SeqEnums.MEMBER_ChannelGroup & LOR4SeqEnums.MEMBER_Track,
		GroupsOnly = LOR4SeqEnums.MEMBER_ChannelGroup & LOR4SeqEnums.MEMBER_Track,
		Items = LOR4SeqEnums.MEMBER_Channel & LOR4SeqEnums.MEMBER_RGBchannel & LOR4SeqEnums.MEMBER_ChannelGroup,
		FullTrack = LOR4SeqEnums.MEMBER_Channel & LOR4SeqEnums.MEMBER_RGBchannel & LOR4SeqEnums.MEMBER_ChannelGroup & LOR4SeqEnums.MEMBER_Track,
		Visualization = LOR4SeqEnums.MEMBER_Vizualization,
		VizChannel4 = LOR4SeqEnums.MEMBER_VizChannel,
		VizObject = LOR4SeqEnums.MEMBER_VizDrawObject

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


		public static LOR4DeviceType EnumDevice(string deviceName)
		{
			LOR4DeviceType valueOut = LOR4DeviceType.None;

			if (deviceName == DEVICElor)
			{
				valueOut = LOR4DeviceType.LOR;
			}
			else if (deviceName == "1") // Visualizer
			{
				valueOut = LOR4DeviceType.LOR;
			}
			else if (deviceName == DEVICEdmx)
			{
				valueOut = LOR4DeviceType.DMX;
			}
			else if (deviceName == "7") // Visualizer
			{
				valueOut = LOR4DeviceType.DMX;
			}
			else if (deviceName == DEVICEdigital)
			{
				valueOut = LOR4DeviceType.Digital;
			}
			else if (deviceName == DEVICEdasher)
			{
				valueOut = LOR4DeviceType.Dasher;
			}
			else if (deviceName == DEVICEcosmic)
			{
				valueOut = LOR4DeviceType.LOR4Cosmic;
			}
			else if (deviceName == "")
			{
				valueOut = LOR4DeviceType.None;
			}
			else
			{
				// TODO: throw exception here!
				valueOut = LOR4DeviceType.None;
				string sMsg = "Unrecognized Device Type: ";
				sMsg += deviceName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
				System.Diagnostics.Debugger.Break();
			}

			return valueOut;
		}

		public static LOR4EffectType EnumEffectType(string effectName)
		{
			LOR4EffectType valueOut = LOR4EffectType.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = LOR4EffectType.Intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = LOR4EffectType.Shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = LOR4EffectType.Twinkle;
			}
			else if (effectName == EFFECTdmx)
			{
				valueOut = LOR4EffectType.DMX;
			}
			else
			{
				// TODO: throw exception here
				valueOut = LOR4EffectType.None;
				string sMsg = "Unrecognized Effect Name: ";
				sMsg += effectName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static LOR4TimingGridType EnumGridType(string typeName)
		{
			LOR4TimingGridType valueOut = LOR4TimingGridType.None;

			if (typeName == GRIDfreeform)
			{
				valueOut = LOR4TimingGridType.Freeform;
			}
			else if (typeName == "fixed")
			{
				valueOut = LOR4TimingGridType.FixedGrid;
			}
			else
			{
				// TODO: throw exception here
				valueOut = LOR4TimingGridType.None;
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
			return valueOut;
		}


		public static string DeviceName(LOR4DeviceType devType)
		{
			string valueOut = "";
			switch (devType)
			{
				case LOR4DeviceType.LOR:
					valueOut = DEVICElor;
					break;

				case LOR4DeviceType.DMX:
					valueOut = DEVICEdmx;
					break;

				case LOR4DeviceType.None:
					valueOut = DEVICEnone;
					break;

				case LOR4DeviceType.Digital:
					valueOut = DEVICEdigital;
					break;

				case LOR4DeviceType.LOR4Cosmic:
					valueOut = DEVICEcosmic;
					break;

				case LOR4DeviceType.Dasher:
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

		public static string EffectTypeName(LOR4EffectType effType)
		{
			string valueOut = "";
			switch (effType)
			{
				case LOR4EffectType.Intensity:
					valueOut = EFFECTintensity;
					break;

				case LOR4EffectType.Shimmer:
					valueOut = EFFECTshimmer;
					break;

				case LOR4EffectType.Twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case LOR4EffectType.DMX:
					valueOut = EFFECTdmx;
					break;

				case LOR4EffectType.Constant:
					valueOut = EFFECTconstant;
					break;

				case LOR4EffectType.FadeUp:
					valueOut = EFFECTfadeUp;
					break;

				case LOR4EffectType.FadeDown:
					valueOut = EFFECTfadeDown;
					break;
			}
			return valueOut;
		}

		public static string TimingName(LOR4TimingGridType grdType)
		{
			string valueOut = "";
			switch (grdType)
			{
				case LOR4TimingGridType.Freeform:
					valueOut = GRIDfreeform;
					break;

				case LOR4TimingGridType.FixedGrid:
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
