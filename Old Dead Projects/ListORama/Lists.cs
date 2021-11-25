using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LORUtils4; using FileHelper;
using System.Drawing;

namespace ListORama
{
	class ChannelList
	{
		public int count = 0;
		public int[] positions = null;
		public string[] groups = null;
	}

	class SeqChannelItem : IComparable<SeqChannelItem>
	{
		public int foundOrder = lutils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public LORDeviceType4 type = LORDeviceType4.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		//public Int32 lorColor = 0;
		//public Color netColor = Color.Black;
		//public string colorName = "";
		public string sortString = "";
		public LORChannel4 theChannel;
		public bool isKeywd = false;


		public int CompareTo(SeqChannelItem otherItem)
		{
			return this.sortString.CompareTo(otherItem.sortString);
		}

		public static string Header(string keyword)
		{
				string lineOut = "Name,";
				lineOut += keyword;
				lineOut += "Order,";
				lineOut += "SavedIndex,";
				lineOut += "Type,";
				lineOut += "Controller,";
				lineOut += "LORChannel4,";
				lineOut += "0xColor,";
				lineOut += "ColorName";
				return lineOut;
		}
		public override string ToString()
		{
			string ret = lutils.XMLifyName(theChannel.Name);
			int p = theChannel.Name.IndexOf(',');
			if (p>= 0) ret = "\"" + ret + "\"";
				
			ret	+= ",";
			if (isKeywd)
			{
				ret += "Yes,";
			}
			else
			{
				ret += "No,";
			}
			ret += foundOrder.ToString();
			ret += theChannel.SavedIndex.ToString() + ",";
			LORDeviceType4 devType = theChannel.output.deviceType;
			ret += LORSeqEnums4.DeviceName(devType) + ",";
			if (devType == LORDeviceType4.LOR)
			{
				ret += theChannel.output.unit.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == LORDeviceType4.DMX)
			{
				ret += theChannel.output.network.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == LORDeviceType4.Digital)
			{
				ret += theChannel.output.network.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == LORDeviceType4.None)
			{
				ret += ",,";
			}
			ret += lutils.Color_LORtoHTML(theChannel.color) + ",";
			ret += NearestColor.FindNearestColorName(theChannel.Color); // + ",";
			return ret;
		}

		public string Name
		{
			get
			{
				return theChannel.Name;
			}
		}
		public int savedIndex
		{
			get
			{
				return theChannel.SavedIndex;
			}
		}
		public LORDeviceType4 deviceType
		{
			get
			{
				return theChannel.output.deviceType;
			}
		}
		public int network
		{
			get
			{
				return theChannel.output.network;
			}
		}
		public int universe
		{
			get
			{
				return theChannel.output.universe;
			}
		}
		public int unit
		{
			get
			{
				return theChannel.output.unit;
			}
		}
		public int channel
		{
			get
			{
				return theChannel.output.channel;
			}
		}
		public Int32 color // LOR
		{
			get
			{
				return theChannel.color; // LOR
			}
		}
		public Color Color // Net
		{
			get
			{
				return theChannel.Color; // Net
			}
		}
		public string ColorName
		{
			get
			{
				return NearestColor.FindNearestColorName(theChannel.Color);
			}
		}
	} // End class SeqChannelItem

	class SeqRGBItem : IComparable<SeqRGBItem>
	{
		public int foundOrder = lutils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public LORDeviceType4 type = LORDeviceType4.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		public string sortString = "";
		public LORRGBChannel4 theRGBchannel;
		public bool isKeywd = false;

		public int CompareTo(SeqRGBItem otherItem)
		{
			return this.sortString.CompareTo(otherItem.sortString);
		}

		public static string Header(string keyword)
		{
			string lineOut = "Name,";
			lineOut += keyword;
			lineOut += "Order,";
			lineOut += "SavedIndex,";
			lineOut += "Type,";
			lineOut += "Controller,";
			lineOut += "LORChannel4";
			return lineOut;
		}
		public override string ToString()
		{
			string ret = lutils.XMLifyName(theRGBchannel.Name);
			int p = theRGBchannel.Name.IndexOf(',');
			if (p >= 0) ret = "\"" + ret + "\"";

			ret += ",";
			if (isKeywd)
			{
				ret += "Yes,";
			}
			else
			{
				ret += "No,";
			}
			ret += foundOrder.ToString();
			ret += theRGBchannel.SavedIndex.ToString() + ",";
			LORDeviceType4 devType = theRGBchannel.redChannel.output.deviceType;
			ret += LORSeqEnums4.DeviceName(devType) + ",";
			if (devType == LORDeviceType4.LOR)
			{
				ret += theRGBchannel.redChannel.output.unit.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == LORDeviceType4.DMX)
			{
				ret += theRGBchannel.redChannel.output.network.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == LORDeviceType4.Digital)
			{
				ret += theRGBchannel.redChannel.output.network.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == LORDeviceType4.None)
			{
				ret += ",";
			}
			return ret;
		}

		public string Name
		{
			get
			{
				return theRGBchannel.Name;
			}
		}
		public int savedIndex
		{
			get
			{
				return theRGBchannel.SavedIndex;
			}
		}
		public LORDeviceType4 deviceType
		{
			get
			{
				return theRGBchannel.redChannel.output.deviceType;
			}
		}
		public int network
		{
			get
			{
				return theRGBchannel.redChannel.output.network;
			}
		}
		public int universe
		{
			get
			{
				return theRGBchannel.redChannel.output.universe;
			}
		}
		public int unit
		{
			get
			{
				return theRGBchannel.redChannel.output.unit;
			}
		}
		public int channel
		{
			get
			{
				return theRGBchannel.redChannel.output.channel;
			}
		}
	} // End class SeqChannelItem

	class SeqGroupItem : IComparable<SeqGroupItem>
	{
		public int foundOrder = lutils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public LORDeviceType4 type = LORDeviceType4.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		public string sortString = "";
		public LORChannelGroup4 theGroup;
		public bool isKeywd = false;

		public int CompareTo(SeqGroupItem otherItem)
		{
			return this.sortString.CompareTo(otherItem.sortString);
		}

		public static string Header(string keyword)
		{
			string lineOut = "Name,";
			lineOut += keyword;
			lineOut += "Order,";
			lineOut += "SavedIndex,";
			lineOut += "Count";
			return lineOut;
		}
		public override string ToString()
		{
			string ret = lutils.XMLifyName(theGroup.Name);
			int p = theGroup.Name.IndexOf(',');
			if (p >= 0) ret = "\"" + ret + "\"";

			ret += ",";
			if (isKeywd)
			{
				ret += "Yes,";
			}
			else
			{
				ret += "No,";
			}
			ret += foundOrder.ToString();
			ret += theGroup.SavedIndex.ToString() + ",";
			ret += theGroup.Members.Count.ToString();
			return ret;
		}

		public string Name
		{
			get
			{
				return theGroup.Name;
			}
		}
		public int savedIndex
		{
			get
			{
				return theGroup.SavedIndex;
			}
		}
		public int Count
		{
			get
			{
				return theGroup.Members.Count;
			}
		}
	} // End class SeqChannelItem

	class VizChannelItem : IComparable<VizChannelItem>
	{
		public int foundOrder = lutils.UNDEFINED;
		public string sortString = "";
		public LORVizChannel4 theChannel;
		public bool isKeywd = false;
		private static string COMMA = ",";

		public int CompareTo(VizChannelItem otherItem)
		{
			return this.sortString.CompareTo(otherItem.sortString);
		}

		public static string Header(string keyword)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Type,");
			sb.Append("SavedIndex,");
			sb.Append("Name,");
			sb.Append("Centiseconds,");
			sb.Append("Device,");
			sb.Append("Unit,");
			sb.Append("Network,");
			sb.Append("Circuit,");
			sb.Append("Universe,");
			sb.Append("LORChannel4,");
			sb.Append("LOR Color,");
			sb.Append("Web Color,");
			sb.Append("Color Name");
			string lineOut = sb.ToString();
			return lineOut;
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("LORChannel4,");
			sb.Append(theChannel.SavedIndex.ToString() + COMMA);
			sb.Append(lutils.XMLifyName(theChannel.Name) + COMMA);
			sb.Append(COMMA); // Centiseconds
			sb.Append(LORSeqEnums4.DeviceName(theChannel.output.deviceType) + COMMA);
			if (theChannel.output.deviceType == LORDeviceType4.LOR)
			{
				sb.Append(theChannel.output.unit.ToString() + COMMA);
				sb.Append(theChannel.output.networkName + COMMA);
				sb.Append(theChannel.output.circuit.ToString() + COMMA);
				sb.Append(COMMA + COMMA);
			}
			if (theChannel.output.deviceType == LORDeviceType4.DMX)
			{
				sb.Append(COMMA + COMMA + COMMA);
				sb.Append(theChannel.output.UniverseNumber.ToString() + COMMA);
				sb.Append(theChannel.output.DMXAddress.ToString() + COMMA);
			}
			sb.Append(theChannel.color.ToString() + COMMA);
			sb.Append("#" + lutils.Color_LORtoHTML(theChannel.color) + COMMA);
			sb.Append(NearestColor.FindNearestColorName(theChannel.color));

			string ret = sb.ToString();
			return ret;
		}

		public string Name
		{
			get
			{
				return theChannel.Name;
			}
		}
		public int savedIndex
		{
			get
			{
				return theChannel.SavedIndex;
			}
		}
		public LORDeviceType4 deviceType
		{
			get
			{
				return theChannel.output.deviceType;
			}
		}
		public int network
		{
			get
			{
				return theChannel.output.network;
			}
		}
		public int universe
		{
			get
			{
				return theChannel.output.universe;
			}
		}
		public int unit
		{
			get
			{
				return theChannel.output.unit;
			}
		}
		public int channel
		{
			get
			{
				return theChannel.output.channel;
			}
		}
		public Int32 color // LOR
		{
			get
			{
				return theChannel.color; // LOR
			}
		}
		public Color Color // Net
		{
			get
			{
				return theChannel.Color; // Net
			}
		}
		public string ColorName
		{
			get
			{
				return NearestColor.FindNearestColorName(theChannel.Color);
			}
		}
	} // End class SeqChannelItem



}
