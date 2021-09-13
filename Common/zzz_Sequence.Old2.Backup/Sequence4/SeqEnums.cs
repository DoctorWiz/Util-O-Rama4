using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LORUtils4
{
	public enum LORDeviceType4
	{ None=lutils.UNDEFINED, LOR=1, DMX, Digital };

	public enum LOREffectType4 { None=lutils.UNDEFINED, intensity=1, shimmer, twinkle, DMX }

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
		LORChannel4 =				LORSeqEnums4.MEMBER_Channel,
		LORRGBChannel4 =		LORSeqEnums4.MEMBER_RGBchannel,
		LORChannelGroup4 =	LORSeqEnums4.MEMBER_ChannelGroup,
		LORTrack4 =					LORSeqEnums4.MEMBER_Track,
		LORTimings4 =		LORSeqEnums4.MEMBER_TimingGrid,
		Sequence =			LORSeqEnums4.MEMBER_Sequence,
		RegularOnly =		LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		RGBonly =				LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		GroupsOnly =		LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track,
		Items =					LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup,
		FullTrack =			LORSeqEnums4.MEMBER_Channel & LORSeqEnums4.MEMBER_RGBchannel & LORSeqEnums4.MEMBER_ChannelGroup & LORSeqEnums4.MEMBER_Track
	}

	public static class LORSeqEnums4
	{
		// * MEMBER TYPES *
		public const int MEMBER_Channel = 1;
		public const int MEMBER_RGBchannel = 2;
		public const int MEMBER_ChannelGroup = 4;
		public const int MEMBER_Track = 8;
		public const int MEMBER_TimingGrid = 16;
		public const int MEMBER_Sequence = 32;

		public const string DEVICElor = "LOR";
		public const string DEVICEdmx = "DMX Universe";
		public const string DEVICEdigital = "Digital IO";
		public const string EFFECTintensity = "intensity";
		public const string EFFECTshimmer = "shimmer";
		public const string EFFECTtwinkle = "twinkle";
		public const string EFFECTdmx = "DMX intensity";
		public const string GRIDfreeform = "freeform";
		public const string GRIDfixed = "fixed";
		public const string OBJnone = "None";
		public const string OBJchannel = "LORChannel4";
		public const string OBJrgbChannel = "LORRGBChannel4";
		public const string OBJchannelGroup = "LORChannelGroup4";
		public const string OBJtrack = "LORTrack4";
		public const string OBJtimingGrid = "LORTimings4";
		public const string OBJsequence = "Sequence";
		public const string OBJall = "All";
		public const string OBJitems = "Items";


		public static LORDeviceType4 LOREnumDevice4(string deviceName)
		{
			LORDeviceType4 valueOut = LORDeviceType4.None;

			if (deviceName == DEVICElor)
			{
				valueOut = LORDeviceType4.LOR;
			}
			else if (deviceName == DEVICEdmx)
			{
				valueOut = LORDeviceType4.DMX;
			}
			else if (deviceName == DEVICEdigital)
			{
				valueOut = LORDeviceType4.Digital;
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
			}

			return valueOut;
		}

		public static LOREffectType4 enumEffect(string effectName)
		{
			LOREffectType4 valueOut = LOREffectType4.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = LOREffectType4.intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = LOREffectType4.shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = LOREffectType4.twinkle;
			}
			else if (effectName == EFFECTdmx)
			{
				valueOut = LOREffectType4.DMX;
			}
			else
			{
				// TODO: throw exception here
				valueOut = LOREffectType4.None;
				string sMsg = "Unrecognized LOREffect4 Name: ";
				sMsg += effectName;
				//DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static LORTimingGridType4 LOREnumGridType4(string typeName)
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

		public static LORMemberType4 enumTableType(string typeName)
		{
			// Only supported the 5 needed for Identity owners
			LORMemberType4 valueOut = LORMemberType4.None;
			if (typeName == OBJchannel)
			{
				valueOut = LORMemberType4.LORChannel4;
			}
			else if (typeName == OBJrgbChannel)
			{
				valueOut = LORMemberType4.LORRGBChannel4;
			}
			else if (typeName == OBJchannelGroup)
			{
				valueOut = LORMemberType4.LORChannelGroup4;
			}
			else if (typeName == OBJtrack)
			{
				valueOut = LORMemberType4.LORTrack4;
			}
			else if (typeName == OBJtimingGrid)
			{
				valueOut = LORMemberType4.LORTimings4;
			}
			return valueOut;
		}


		public static string LORDeviceName4(LORDeviceType4 devType)
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

				case LORDeviceType4.Digital:
					valueOut = DEVICEdigital;
					break;

					//TODO: Other device types, such as cosmic color ribbon and ...
			}
			return valueOut;
		}

		public static string EffectName(LOREffectType4 effType)
		{
			string valueOut = "";
			switch (effType)
			{
				case LOREffectType4.intensity:
					valueOut = EFFECTintensity;
					break;

				case LOREffectType4.shimmer:
					valueOut = EFFECTshimmer;
					break;

				case LOREffectType4.twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case LOREffectType4.DMX:
					valueOut = EFFECTdmx;
					break;
			}
			return valueOut;
		}

		public static string LORTimingName4(LORTimingGridType4 grdType)
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

		public static string LORMemberName4(LORMemberType4 memberType)
		{
			// Only doing the 5 needed for Members
			string valueOut = "";
			switch (memberType)
			{
				case LORMemberType4.LORChannel4:
					valueOut = OBJchannel;
					break;
				case LORMemberType4.LORRGBChannel4:
					valueOut = OBJrgbChannel;
					break;
				case LORMemberType4.LORChannelGroup4:
					valueOut = OBJchannelGroup;
					break;
				case LORMemberType4.LORTrack4:
					valueOut = OBJtrack;
					break;
				case LORMemberType4.LORTimings4:
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
