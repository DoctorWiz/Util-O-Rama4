using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LORUtils
{
	public enum DeviceType
	{ None=utils.UNDEFINED, LOR=1, DMX=7, Digital };

	//public enum EffectType { None=utils.UNDEFINED, Intensity=1, Shimmer, Twinkle, DMX }
	public enum EffectType { None = utils.UNDEFINED, Intensity = 1, Shimmer, Twinkle, DMX, Constant, FadeUp, FadeDown }

	//internal enum timingGridType
	public enum TimingGridType
	{ None=utils.UNDEFINED, Freeform=1, FixedGrid };

	//internal enum MemberType
//	public enum MemberType
//	{ None=utils.UNDEFINED, Channel=1, RGBchannel=2, ChannelGroup=4, Track=8, TimingGrid=16, Items=7, FullTrack=15; RGBonly=14, RegularOnly=13, All=31, Sequence=32 }
	public enum RGBchild
	{ None=utils.UNDEFINED, Red=1, Green, Blue }

	public enum SequenceType
	{ Undefined=utils.UNDEFINED, Animated=1, Musical, Clipboard, ChannelConfig, Visualizer }

	public enum MatchType
	{ Unmatched=0, Source=-1, Destination=1}

	public enum MemberType
	{
		// * MEMBER TYPES *
		None =					0,
		Channel =				SeqEnums.MEMBER_Channel,
		RGBchannel =		SeqEnums.MEMBER_RGBchannel,
		ChannelGroup =	SeqEnums.MEMBER_ChannelGroup,
		Track =					SeqEnums.MEMBER_Track,
		TimingGrid =		SeqEnums.MEMBER_TimingGrid,
		Sequence =			SeqEnums.MEMBER_Sequence,
		RegularOnly =		SeqEnums.MEMBER_Channel & SeqEnums.MEMBER_ChannelGroup & SeqEnums.MEMBER_Track,
		RGBonly =				SeqEnums.MEMBER_RGBchannel & SeqEnums.MEMBER_ChannelGroup & SeqEnums.MEMBER_Track,
		GroupsOnly =		SeqEnums.MEMBER_ChannelGroup & SeqEnums.MEMBER_Track,
		Items =					SeqEnums.MEMBER_Channel & SeqEnums.MEMBER_RGBchannel & SeqEnums.MEMBER_ChannelGroup,
		FullTrack =			SeqEnums.MEMBER_Channel & SeqEnums.MEMBER_RGBchannel & SeqEnums.MEMBER_ChannelGroup & SeqEnums.MEMBER_Track,
		VizChannel =		SeqEnums.MEMBER_VizChannel,
		VizObject =			SeqEnums.MEMBER_VizObject

	}

	public static class SeqEnums
	{
		// * MEMBER TYPES *
		public const int MEMBER_Channel = 1;
		public const int MEMBER_RGBchannel = 2;
		public const int MEMBER_ChannelGroup = 4;
		public const int MEMBER_Track = 8;
		public const int MEMBER_TimingGrid = 16;
		public const int MEMBER_Sequence = 32;
		public const int MEMBER_VizChannel = 256;
		public const int MEMBER_VizObject = 512;

		public const string DEVICElor = "LOR";
		public const string DEVICEdmx = "DMX Universe";
		public const string DEVICEdigital = "Digital IO";
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
		public const string OBJrgbChannel = "RGBchannel";
		public const string OBJchannelGroup = "ChannelGroup";
		public const string OBJtrack = "Track";
		public const string OBJtimingGrid = "TimingGrid";
		public const string OBJsequence = "Sequence";
		public const string OBJall = "All";
		public const string OBJitems = "Items";


		public static DeviceType enumDevice(string deviceName)
		{
			DeviceType valueOut = DeviceType.None;

			if (deviceName == DEVICElor)
			{
				valueOut = DeviceType.LOR;
			}
			else if (deviceName == DEVICEdmx)
			{
				valueOut = DeviceType.DMX;
			}
			else if (deviceName == DEVICEdigital)
			{
				valueOut = DeviceType.Digital;
			}
			else if (deviceName == "")
			{
				valueOut = DeviceType.None;
			}
			else
			{
				// TODO: throw exception here!
				valueOut = DeviceType.None;
				string sMsg = "Unrecognized Device Type: ";
				sMsg += deviceName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static EffectType enumEffectType(string effectName)
		{
			EffectType valueOut = EffectType.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = EffectType.Intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = EffectType.Shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = EffectType.Twinkle;
			}
			else if (effectName == EFFECTdmx)
			{
				valueOut = EffectType.DMX;
			}
			else
			{
				// TODO: throw exception here
				valueOut = EffectType.None;
				string sMsg = "Unrecognized Effect Name: ";
				sMsg += effectName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static TimingGridType enumGridType(string typeName)
		{
			TimingGridType valueOut = TimingGridType.None;

			if (typeName == GRIDfreeform)
			{
				valueOut = TimingGridType.Freeform;
			}
			else if (typeName == "fixed")
			{
				valueOut = TimingGridType.FixedGrid;
			}
			else
			{
				// TODO: throw exception here
				valueOut = TimingGridType.None;
				string sMsg = "Unrecognized Timing Grid Type: ";
				sMsg += typeName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static MemberType enumTableType(string typeName)
		{
			// Only supported the 5 needed for Identity owners
			MemberType valueOut = MemberType.None;
			if (typeName == OBJchannel)
			{
				valueOut = MemberType.Channel;
			}
			else if (typeName == OBJrgbChannel)
			{
				valueOut = MemberType.RGBchannel;
			}
			else if (typeName == OBJchannelGroup)
			{
				valueOut = MemberType.ChannelGroup;
			}
			else if (typeName == OBJtrack)
			{
				valueOut = MemberType.Track;
			}
			else if (typeName == OBJtimingGrid)
			{
				valueOut = MemberType.TimingGrid;
			}
			return valueOut;
		}


		public static string DeviceName(DeviceType devType)
		{
			string valueOut = "";
			switch (devType)
			{
				case DeviceType.LOR:
					valueOut = DEVICElor;
					break;

				case DeviceType.DMX:
					valueOut = DEVICEdmx;
					break;

				case DeviceType.Digital:
					valueOut = DEVICEdigital;
					break;

					//TODO: Other device types, such as cosmic color ribbon and ...
			}
			return valueOut;
		}

		public static string EffectTypeName(EffectType effType)
		{
			string valueOut = "";
			switch (effType)
			{
				case EffectType.Intensity:
					valueOut = EFFECTintensity;
					break;

				case EffectType.Shimmer:
					valueOut = EFFECTshimmer;
					break;

				case EffectType.Twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case EffectType.DMX:
					valueOut = EFFECTdmx;
					break;

				case EffectType.Constant:
					valueOut = EFFECTconstant;
					break;

				case EffectType.FadeUp:
					valueOut = EFFECTfadeUp;
					break;

				case EffectType.FadeDown:
					valueOut = EFFECTfadeDown;
					break;
			}
			return valueOut;
		}

		public static string TimingName(TimingGridType grdType)
		{
			string valueOut = "";
			switch (grdType)
			{
				case TimingGridType.Freeform:
					valueOut = GRIDfreeform;
					break;

				case TimingGridType.FixedGrid:
					valueOut = GRIDfixed;
					break;
			}
			return valueOut;
		}

		public static string MemberName(MemberType memberType)
		{
			// Only doing the 5 needed for Members
			string valueOut = "";
			switch (memberType)
			{
				case MemberType.Channel:
					valueOut = OBJchannel;
					break;
				case MemberType.RGBchannel:
					valueOut = OBJrgbChannel;
					break;
				case MemberType.ChannelGroup:
					valueOut = OBJchannelGroup;
					break;
				case MemberType.Track:
					valueOut = OBJtrack;
					break;
				case MemberType.TimingGrid:
					valueOut = OBJtimingGrid;
					break;
				case MemberType.Sequence:
					valueOut = OBJsequence;
					break;
			}
			return valueOut;
		}





	} // end clas SeqEnums



} // end namespace LORUtils
