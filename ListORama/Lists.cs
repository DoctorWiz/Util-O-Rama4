using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LORUtils;
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
		public int foundOrder = utils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public DeviceType type = DeviceType.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		//public Int32 lorColor = 0;
		//public Color netColor = Color.Black;
		//public string colorName = "";
		public string sortString = "";
		public Channel theChannel;
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
				lineOut += "Channel,";
				lineOut += "0xColor,";
				lineOut += "ColorName";
				return lineOut;
		}
		public override string ToString()
		{
			string ret = utils.XMLifyName(theChannel.Name);
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
			DeviceType devType = theChannel.output.deviceType;
			ret += SeqEnums.DeviceName(devType) + ",";
			if (devType == DeviceType.LOR)
			{
				ret += theChannel.output.unit.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == DeviceType.DMX)
			{
				ret += theChannel.output.network.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == DeviceType.Digital)
			{
				ret += theChannel.output.network.ToString() + "," + theChannel.output.channel.ToString() + ",";
			}
			if (devType == DeviceType.None)
			{
				ret += ",,";
			}
			ret += utils.Color_LORtoHTML(theChannel.color) + ",";
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
		public DeviceType deviceType
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
		public int foundOrder = utils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public DeviceType type = DeviceType.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		public string sortString = "";
		public RGBchannel theRGBchannel;
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
			lineOut += "Channel";
			return lineOut;
		}
		public override string ToString()
		{
			string ret = utils.XMLifyName(theRGBchannel.Name);
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
			DeviceType devType = theRGBchannel.redChannel.output.deviceType;
			ret += SeqEnums.DeviceName(devType) + ",";
			if (devType == DeviceType.LOR)
			{
				ret += theRGBchannel.redChannel.output.unit.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == DeviceType.DMX)
			{
				ret += theRGBchannel.redChannel.output.network.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == DeviceType.Digital)
			{
				ret += theRGBchannel.redChannel.output.network.ToString() + "," + theRGBchannel.redChannel.output.channel.ToString();
			}
			if (devType == DeviceType.None)
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
		public DeviceType deviceType
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
		public int foundOrder = utils.UNDEFINED;
		//public string name = "";
		//public int savedIndex = -1;
		//public DeviceType type = DeviceType.None;
		//public int network = 0;
		//public int unit = 0;
		//public int channel = 0;
		public string sortString = "";
		public ChannelGroup theGroup;
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
			string ret = utils.XMLifyName(theGroup.Name);
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
		public int foundOrder = utils.UNDEFINED;
		public string sortString = "";
		public VizChannel theChannel;
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
			sb.Append("Channel,");
			sb.Append("LOR Color,");
			sb.Append("Web Color,");
			sb.Append("Color Name");
			string lineOut = sb.ToString();
			return lineOut;
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Channel,");
			sb.Append(theChannel.SavedIndex.ToString() + COMMA);
			sb.Append(utils.XMLifyName(theChannel.Name) + COMMA);
			sb.Append(COMMA); // Centiseconds
			sb.Append(SeqEnums.DeviceName(theChannel.output.deviceType) + COMMA);
			if (theChannel.output.deviceType == DeviceType.LOR)
			{
				sb.Append(theChannel.output.unit.ToString() + COMMA);
				sb.Append(theChannel.output.networkName + COMMA);
				sb.Append(theChannel.output.circuit.ToString() + COMMA);
				sb.Append(COMMA + COMMA);
			}
			if (theChannel.output.deviceType == DeviceType.DMX)
			{
				sb.Append(COMMA + COMMA + COMMA);
				sb.Append(theChannel.output.universe.ToString() + COMMA);
				sb.Append(theChannel.output.channel.ToString() + COMMA);
			}
			sb.Append(theChannel.color.ToString() + COMMA);
			sb.Append("#" + utils.Color_LORtoHTML(theChannel.color) + COMMA);
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
		public DeviceType deviceType
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
