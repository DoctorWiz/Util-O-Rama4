using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Windows.ApplicationModel.Calls.Background;   // SyncFusion TreeView Advanced

namespace UtilORama4
{

	public class DMXUniverse : IDMXThingy, IComparable<DMXUniverse>, IEquatable<DMXUniverse>
	{
		// Note: ID is not permenant, it is assigned and used only at run time
		protected int myID = -1;
		//private string myName = "";
		protected string myName = "";
		protected string myComment = "";
		protected string myLocation = "";
		protected bool isActive = true;
		//private int myUniverseNumber = 1;
		protected int myUniverseNumber = 1;
		protected int myxLightsAddress = 1;
		public string Connection = "";
		//public int SizeLimit = 510;
		protected int MaxChannels = 510;
		//protected bool nameIsBad = false;
		//public bool BadNumber = false;
		protected bool isEditing = false;
		protected bool isDirty = false;
		protected object myTag = null;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
																						//public TreeNodeAdv TreeNode = null;
		public bool SortByName = false;
		// Controllers under this universe
		public List<DMXController> DMXControllers = new List<DMXController>();
		// ALL Universes
		public static List<DMXUniverse> AllUniverses = new List<DMXUniverse>();
		// All Controllers under ALL Universes
		public static List<DMXController> AllControllers = new List<DMXController>();
		// All DMX Channels across all Universes and all Controllers
		public static List<DMXChannel> AllChannels = new List<DMXChannel>();

		public DMXChannel DMXChannelAt(int DMXAddress)
		{
			DMXChannel ret = null;
			for (int ctl = 0; ctl < DMXControllers.Count; ctl++)
			{
				for (int chn = 0; chn < DMXControllers[ctl].DMXChannels.Count; chn++)
				{
					if (DMXAddress == DMXControllers[ctl].DMXChannels[chn].DMXAddress)
					{
						ret = DMXControllers[ctl].DMXChannels[chn];
						chn = DMXControllers[ctl].DMXChannels.Count;
						ctl = DMXControllers.Count; // Exit loops
					}
				}
			}
			return ret;
		}

		public DMXChannel xLightsChannelAt(int xLightsChannelID)
		{
			DMXChannel ret = null;
			for (int ctl = 0; ctl < DMXControllers.Count; ctl++)
			{
				for (int chn = 0; chn < DMXControllers[ctl].DMXChannels.Count; chn++)
				{
					if (xLightsChannelID == DMXControllers[ctl].DMXChannels[chn].xLightsAddress)
					{
						ret = DMXControllers[ctl].DMXChannels[chn];
						chn = DMXControllers[ctl].DMXChannels.Count;
						ctl = DMXControllers.Count; // Exit loops
					}
				}
			}
			return ret;
		}

		public string Name
		{
			get
			{
				return myName;
			}
			set
			{
				// accept it, even if bad
				myName = value.Trim();
			}
		}

		public string FullName
		{
			get
			{
				string ret = UniverseNumber.ToString(myName) + ": " + myName;
				return ret;
			}
		}

		public int ID { get { return myID; } set { myID = value; } }
		public string Comment { get { return myComment; } set { myComment = value; } }
		public string Location { get { return myLocation; } set { myLocation = value; } }
		public bool Active { get { return isActive; } set { isActive = value; } }
		public bool Editing { get { return isEditing; } set { isEditing = value; } }
		public bool Dirty { get { return isDirty; } set { isDirty = value; } }
		public bool BadName
		{ 
			get
			{
				bool ret = false;
				string lowname = myName.ToLower();
				int myOriginalID = Math.Abs(myID);
				string dbg = "This universe " + this.ID.ToString() + "-" + this.Name + " Name conflicts with";
				for (int ch = 0; ch < DMXUniverse.AllUniverses.Count; ch++)
				{
					DMXUniverse uni = DMXUniverse.AllUniverses[ch];
					if (uni.myID != myOriginalID)
					{
						if (uni.Name.CompareTo(myName) == 0)
						{
							ret = true;
							dbg += "\r\n  " + uni.ID.ToString() + "-" + uni.Name;
							//nameIsBad = true;
							//DMXUniverse.AllControllers[ch].nameIsBad = true;
						}
					}
				}
				if (ret)
				{
					dbg += "\r\n\r\n";
					System.Diagnostics.Debug.WriteLine(dbg);
				}
				return ret;
			}
		}

		public bool BadNumber
		{
			get
			{
				bool ret = false;
				int myOriginalID = Math.Abs(myID);
				string dbg = "This universe " + this.ID.ToString() + "-" + this.Name + " Number " + this.UniverseNumber + " conflicts with";
				for (int ch = 0; ch < AllUniverses.Count; ch++)
				{
					DMXUniverse uni = AllUniverses[ch];
					if (uni.myID != myOriginalID)
					{
						if (myUniverseNumber == uni.myUniverseNumber)
						{
							ret = true;
							dbg += "\r\n  " + uni.ID.ToString() + "-" + uni.Name + " Number " + uni.UniverseNumber;
							//addressIsBad = true;
							//DMXUniverse.AllControllers[ch].addressIsBad = true;
						}
					}
				}
				if (ret)
				{
					dbg += "\r\n\r\n";
					System.Diagnostics.Debug.WriteLine(dbg);
				}
				return ret;
			}
		}





		public object Tag { get { return myTag; } set { myTag = value; } }
		public int xLightsAddress { get { return myxLightsAddress; } set { myxLightsAddress = value; } }
		public bool ExactMatch { get { return matchesExactly; } set { matchesExactly = value; } }

		public DMXObjectType ObjectType	{	get	{	return DMXObjectType.DMXUniverse;	}	}

		public int UniverseNumber
		{
			get
			{
				// Put validation here...
				return myUniverseNumber;
			}
			set
			{
				// ... and here
				myUniverseNumber = Math.Abs(value);
			}
		}

		public int Number
		// Wrapper for UniverseNumber, to satisfy the IDMXThingy interface, which is used for generic handling of both universes and controllers in some places.
		{
			get
			{
				return UniverseNumber;
			}
			set
			{
				UniverseNumber = value;
			}
		}


		public int Save(string fileName)
		{
			int errs = 0;



			return errs;
		}

		public int Load(string fileName)
		{
			int errs = 0;

			return errs;
		}

		public override string ToString()
		{
			string ret = UniverseNumber.ToString("00") + ": ";
			ret += Name;
			return ret;
		}

		public int CompareTo(DMXUniverse otherUniverse)
		{
			int ret = 0;
			if (SortByName)
			{
				ret = Name.CompareTo(otherUniverse.Name);
				if (ret == 0)
				{
					ret = UniverseNumber.CompareTo(otherUniverse.UniverseNumber);
				}
				}
			return ret;
		}
		public DMXUniverse Copy()
		{
			DMXUniverse newUni = new DMXUniverse();
			//newUni.myID = myID;
			newUni.Name = myName;
			newUni.myLocation = myLocation;
			newUni.myComment = myComment;
			newUni.isActive = isActive;
			newUni.myUniverseNumber = myUniverseNumber;
			newUni.myxLightsAddress = myxLightsAddress;
			newUni.Connection = Connection;
			newUni.MaxChannelsAllowed = MaxChannelsAllowed;
			newUni.DMXControllers = DMXControllers;
			newUni.isEditing = isEditing;
			//newUni.nameIsBad							= nameIsBad;
			//newUni.isDirty = isDirty;
			newUni.myTag = myTag;
			newUni.matchesExactly = matchesExactly;
			newUni.myID = -Math.Abs(myID);  // Set the new ID to a negative of the original value to indicate it is a copy and not an original
			return newUni;
		}

		public DMXUniverse Clone()
		{
			DMXUniverse newUni = Copy();
			newUni.myID = myID;
			return newUni;
		}

		public void ApplyChanges(DMXUniverse otherUniverse)
		{
			//myID = otherUniverse.myID;
			Name = otherUniverse.myName;
			myLocation = otherUniverse.myLocation;
			myComment = otherUniverse.myComment;
			isActive = otherUniverse.isActive;
			myUniverseNumber = otherUniverse.myUniverseNumber;
			myxLightsAddress = otherUniverse.myxLightsAddress;
			Connection = otherUniverse.Connection;
			MaxChannelsAllowed = otherUniverse.MaxChannelsAllowed;
			DMXControllers = otherUniverse.DMXControllers;
			isEditing = otherUniverse.isEditing;
			//nameIsBad							= otherUniverse.nameIsBad;
			//isDirty = otherUniverse.isDirty;
			myTag = otherUniverse.myTag;
			matchesExactly = otherUniverse.matchesExactly;
		}

		public void Clone(DMXUniverse otherUniverse)
		{
			myID = otherUniverse.myID;
			ApplyChanges(otherUniverse);
		}

		public bool Equals(DMXUniverse otherUniverse)
		{
			bool eq = true;
			//if (myID != otherUniverse.myID) eq = false;
			if (myName.CompareTo(otherUniverse.myName) != 0) eq = false;
			else if (myLocation.CompareTo(otherUniverse.myLocation) != 0) eq = false;
			else if (myComment.CompareTo(otherUniverse.myComment) != 0) eq = false;
			else if (isActive != otherUniverse.isActive) eq = false;
			else if (myUniverseNumber != otherUniverse.myUniverseNumber) eq = false;
			else if (myxLightsAddress != otherUniverse.myxLightsAddress) eq = false;
			else if (Connection.CompareTo(otherUniverse.Connection) != 0) eq = false;
			else if (MaxChannels != otherUniverse.MaxChannels) eq = false;
			//else if (DMXControllers != otherUniverse.DMXControllers) eq = false;
			else if (isEditing != otherUniverse.isEditing) eq = false;
			//else if (nameIsBad							!= otherUniverse.nameIsBad)							eq = false;
			//else if (BadNumber != otherUniverse.BadNumber) eq = false;
			//else if (isDirty != otherUniverse.isDirty) eq = false;
			//else if (myTag != otherUniverse.myTag) eq = false;
			return eq;
		}

		public int HighestChannel
		{
			get
			{
				// What is the highest DMX Channel number used in this universe?
				int ret = 0;
				// Look through all controllers and channels to find the highest DMX Address used
				for (int ctl = 0; ctl < DMXControllers.Count; ctl++)
				{
					for (int chn = 0; chn < DMXControllers[ctl].DMXChannels.Count; chn++)
					{
						if (DMXControllers[ctl].DMXChannels[chn].DMXAddress > ret)
						{
							ret = DMXControllers[ctl].DMXChannels[chn].DMXAddress;
						}
					}
				}
				return ret;
			}
		}
		public int MaxChannelsAllowed
		{
			get
			{
				int ret = HighestChannel;
				if (ret > MaxChannels) ret = MaxChannels;
				return ret;
			}
			set
			{
				int v = value;
				if (v < HighestChannel) v = HighestChannel;
				if (v > 510) v = 510;
				// If the new max is less than the current highest channel, we need to adjust the max down to the current highest channel
				MaxChannels = v;
			}
		}
	}
}
