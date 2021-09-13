using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LORUtils4;
using xUtils;

namespace UtilORama4
{
	public enum ReportSort { NameManager, NameLOR, NamexLights, MatchLOR, MatchxLights,
		AddressDMXManager, AddressDMXLOR, AddressManagerxLights, AddressxLightsxLights }
	
	
	public class Reporter: IComparable<Reporter>
	{
		public DMXChannel ChannelManager = null;
		public iLORMember4 ChannelLOR = null;
		public xMember ChannelxLights = null;

		// Whether the name was an EXACT match or not.
		// true if match was exact
		// false if match was fuzzy
		public bool MatchExactLOR = false;
		public bool MatchExactxLights = false;
		
		public ReportSort SortMode = ReportSort.NameManager;

		// This CompareTo() is used by IEnumerable Interface
		public int CompareTo(Reporter otherChannel)
		{
			int ret = 0;
			switch (SortMode)
			{
				case ReportSort.NameManager:
					ret = ChannelManager.Name.ToLower().CompareTo(otherChannel.ChannelManager.Name.ToLower());
					break;
				case ReportSort.NameLOR:
					ret = ChannelLOR.Name.ToLower().CompareTo(otherChannel.ChannelLOR.Name.ToLower());
					break;
				case ReportSort.NamexLights:
					ret = ChannelxLights.Name.ToLower().CompareTo(otherChannel.ChannelxLights.Name.ToLower());
					break;
				case ReportSort.MatchLOR:
					ret = MatchExactLOR.CompareTo(otherChannel.MatchExactLOR);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(otherChannel.ChannelManager.Name.ToLower());
					}
					break;
				case ReportSort.MatchxLights:
					ret = MatchExactxLights.CompareTo(otherChannel.MatchExactxLights);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(otherChannel.ChannelManager.Name.ToLower());
					}
					break;
				case ReportSort.AddressDMXManager:
					ret = ChannelManager.UniverseNumber.CompareTo(otherChannel.ChannelManager.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(otherChannel.ChannelManager.DMXAddress);
					}
					break;
				case ReportSort.AddressDMXLOR:
					ret = ChannelLOR.UniverseNumber.CompareTo(otherChannel.ChannelLOR.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelLOR.DMXAddress.CompareTo(otherChannel.ChannelLOR.DMXAddress);
					}
					break;
				case ReportSort.AddressManagerxLights:
					ret = ChannelManager.UniverseNumber.CompareTo(otherChannel.ChannelManager.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(otherChannel.ChannelManager.DMXAddress);
					}
					break;
				case ReportSort.AddressxLightsxLights:
					ret = ChannelxLights.UniverseNumber.CompareTo(otherChannel.ChannelxLights.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelxLights.DMXAddress.CompareTo(otherChannel.ChannelxLights.DMXAddress);
					}
					break;
			}
			return ret;
		}

		public int CompareTo(iLORMember4 ChannelLOR)
		{
			int ret = 0;
			switch (SortMode)
			{
				case ReportSort.NameManager:
					ret = ChannelManager.Name.ToLower().CompareTo(ChannelLOR.Name.ToLower());
					break;
				case ReportSort.NameLOR:
					ret = ChannelLOR.Name.ToLower().CompareTo(ChannelLOR.Name.ToLower());
					break;
				case ReportSort.NamexLights:
					ret = ChannelLOR.Name.ToLower().CompareTo(ChannelLOR.Name.ToLower());
					break;
				case ReportSort.MatchLOR:
					ret = MatchExactLOR.CompareTo(ChannelLOR.ExactMatch);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(ChannelLOR.Name.ToLower());
					}
					break;
				case ReportSort.MatchxLights:
					ret = MatchExactxLights.CompareTo(ChannelLOR.ExactMatch);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(ChannelLOR.Name.ToLower());
					}
					break;
				case ReportSort.AddressDMXManager:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelLOR.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelLOR.DMXAddress);
					}
					break;
				case ReportSort.AddressDMXLOR:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelLOR.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelLOR.DMXAddress);
					}
					break;
				case ReportSort.AddressManagerxLights:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelLOR.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelLOR.DMXAddress);
					}
					break;
				case ReportSort.AddressxLightsxLights:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelLOR.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelLOR.DMXAddress);
					}
					break;
			}
			return ret;
		}

		public int CompareTo(xMember ChannelxLights)
		{
			int ret = 0;
			switch (SortMode)
			{
				case ReportSort.NameManager:
					ret = ChannelManager.Name.ToLower().CompareTo(ChannelxLights.Name.ToLower());
					break;
				case ReportSort.NameLOR:
					ret = ChannelxLights.Name.ToLower().CompareTo(ChannelxLights.Name.ToLower());
					break;
				case ReportSort.NamexLights:
					ret = ChannelxLights.Name.ToLower().CompareTo(ChannelxLights.Name.ToLower());
					break;
				case ReportSort.MatchLOR:
					ret = MatchExactLOR.CompareTo(ChannelxLights.ExactMatch);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(ChannelxLights.Name.ToLower());
					}
					break;
				case ReportSort.MatchxLights:
					ret = MatchExactxLights.CompareTo(ChannelxLights.ExactMatch);
					if (ret == 0)
					{
						ret = ChannelManager.Name.ToLower().CompareTo(ChannelxLights.Name.ToLower());
					}
					break;
				case ReportSort.AddressDMXManager:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelxLights.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelxLights.DMXAddress);
					}
					break;
				case ReportSort.AddressDMXLOR:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelxLights.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelxLights.DMXAddress);
					}
					break;
				case ReportSort.AddressManagerxLights:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelxLights.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelxLights.DMXAddress);
					}
					break;
				case ReportSort.AddressxLightsxLights:
					ret = ChannelManager.UniverseNumber.CompareTo(ChannelxLights.UniverseNumber);
					if (ret == 0)
					{
						ret = ChannelManager.DMXAddress.CompareTo(ChannelxLights.DMXAddress);
					}
					break;
			}
			return ret;
		}




	}
}
