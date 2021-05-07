using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LORUtils
{
	public enum DeviceType
	{ None=utils.UNDEFINED, LOR=1, DMX, Digital };

	public enum EffectType { None=utils.UNDEFINED, intensity=1, shimmer, twinkle, DMX }

	//internal enum timingGridType
	public enum TimingGridType
	{ None=utils.UNDEFINED, Freeform=1, FixedGrid };

	//internal enum TableType
	public enum TableType
	{ None=utils.UNDEFINED, Channel=1, RGBchannel=2, ChannelGroup=4, Track=8, TimingGrid=16, Items=7, All=31, Sequence=32 }

	public enum RGBchild
	{ None=utils.UNDEFINED, Red=1, Green, Blue }

	public enum SequenceType
	{ Undefined=utils.UNDEFINED, Animated=1, Musical, Clipboard, ChannelConfig, Visualizer }

	public enum MatchType
	{ Unmatched=0, Source=-1, Destination=1}

	public static class SeqEnums
	{
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
		public const string OBJchannel = "Channel";
		public const string OBJrgbChannel = "RGBchannel";
		public const string OBJchannelGroup = "ChannelGroup";
		public const string OBJtrack = "Track";
		public const string OBJtimingGrid = "TimingGrid";
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

		public static EffectType enumEffect(string effectName)
		{
			EffectType valueOut = EffectType.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = EffectType.intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = EffectType.shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = EffectType.twinkle;
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

		public static TableType enumTableType(string typeName)
		{
			// Only supported the 5 needed for Identity owners
			TableType valueOut = TableType.None;
			if (typeName == OBJchannel)
			{
				valueOut = TableType.Channel;
			}
			else if (typeName == OBJrgbChannel)
			{
				valueOut = TableType.RGBchannel;
			}
			else if (typeName == OBJchannelGroup)
			{
				valueOut = TableType.ChannelGroup;
			}
			else if (typeName == OBJtrack)
			{
				valueOut = TableType.Track;
			}
			else if (typeName == OBJtimingGrid)
			{
				valueOut = TableType.TimingGrid;
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

		public static string EffectName(EffectType effType)
		{
			string valueOut = "";
			switch (effType)
			{
				case EffectType.intensity:
					valueOut = EFFECTintensity;
					break;

				case EffectType.shimmer:
					valueOut = EFFECTshimmer;
					break;

				case EffectType.twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case EffectType.DMX:
					valueOut = EFFECTdmx;
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

		public static string TableTypeName(TableType PartType)
		{
			// Only doing the 5 needed for Children
			string valueOut = "";
			switch (PartType)
			{
				case TableType.Channel:
					valueOut = OBJchannel;
					break;
				case TableType.RGBchannel:
					valueOut = OBJrgbChannel;
					break;
				case TableType.ChannelGroup:
					valueOut = OBJchannelGroup;
					break;
				case TableType.Track:
					valueOut = OBJtrack;
					break;
				case TableType.TimingGrid:
					valueOut = OBJtimingGrid;
					break;
			}
			return valueOut;
		}





	} // end clas SeqEnums



} // end namespace LORUtils
