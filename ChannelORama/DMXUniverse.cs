using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class ListItem
	{
		public ListItem()
		{ }
		public ListItem(string name, int id)
		{
			Name = name;
			ID = id;
		}
		public string Name { get; set; }
		public int ID { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	interface IDMXThingy
	{
		int ID { get; set; }
		string Name { get; set; }
		string Comment { get; set; }
		string Location { get; set; }
		bool Active { get; set; }
		int UniverseNumber { get; }
		bool BadName { get; }
		bool Editing { get; set; }
		bool Dirty { get; set; }
		object Tag { get; set; }
		int xLightsAddress { get; }
		bool ExactMatch { get; set; }

	}


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
		public int SizeLimit = 510;
		protected bool nameIsBad = false;
		public bool BadNumber = false;
		protected bool isEditing = false;
		protected bool isDirty = false;
		protected object myTag = null;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		public List<DMXController> DMXControllers = new List<DMXController>();
		public static List<DMXChannel> AllChannels = new List<DMXChannel>();

		public DMXChannel DMXChannelAt(int DMXAddress)
		{
			DMXChannel ret = null;
			for (int ctl=0; ctl< DMXControllers.Count; ctl++)
			{
				for (int chn=0; chn < DMXControllers[ctl].DMXChannels.Count; chn++)
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
				nameIsBad = false;  // reset, optomistic, for now
				if (myName.Length < 4)
				{
					nameIsBad = true;
				}
				else
				{
					string lowName = myName.ToLower();
					//for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
					//{
					//! if ID != ID
					//	if (DMXUniverse.AllChannels[c].Name.ToLower().CompareTo(lowName) == 0)
					//	{
					//		nameIsBad = true;
					//		DMXUniverse.AllChannels[c].nameIsBad = true;
					//	}
					//}
				}
				return myName;
			}
			set
			{
				string newName = value.Trim();
				nameIsBad = false;  // reset, optomistic, for now
				if (newName.Length < 4)
				{
					nameIsBad = true;
				}
				else
				{
					string lowName = newName.ToLower();
					//for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
					//{
					//! if ID != ID
					//	if (DMXUniverse.AllChannels[c].Name.ToLower().CompareTo(lowName) == 0)
					//	{
					//		nameIsBad = true;
					//		DMXUniverse.AllChannels[c].nameIsBad = true;
					//	}
					//}
				}
				// accept it, even if bad
				myName = newName;
			}
		}

		public int ID { get { return myID; } set { myID = value; } }
		public string Comment { get { return myComment; } set { myComment = value; } }
		public string Location { get { return myLocation; } set { myLocation = value; } }
		public bool Active { get { return isActive; } set { isActive = value; } }
		public bool Editing { get { return isEditing; } set { isEditing = value; } }
		public bool Dirty { get { return isDirty; } set { isDirty = value; } }
		public bool BadName { get { return nameIsBad; } }
		public object Tag { get { return myTag; } set { myTag = value; } }
		public int xLightsAddress { get { return myxLightsAddress; } set { myxLightsAddress = value; } }
		public bool ExactMatch { get { return matchesExactly; } set { matchesExactly = value; } }

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
				myUniverseNumber = value;
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
			if (myxLightsAddress > otherUniverse.myxLightsAddress) ret = 1;
			if (myxLightsAddress < otherUniverse.myxLightsAddress) ret = -1;
			if (ret == 0)
			{
				if (UniverseNumber > otherUniverse.UniverseNumber) ret = 1;
				if (UniverseNumber < otherUniverse.UniverseNumber) ret = -1;
			}
			if (ret==0)
			{
				ret = Name.CompareTo(otherUniverse.Name);
			}
			return ret;
		}

		public DMXUniverse Clone()
		{
			DMXUniverse newUni = new DMXUniverse();
			newUni.myID										= myID;
			newUni.Name									= myName;
			newUni.myLocation							= myLocation;
			newUni.myComment							= myComment;
			newUni.isActive								= isActive;
			newUni.myUniverseNumber				= myUniverseNumber;
			newUni.myxLightsAddress	= myxLightsAddress;
			newUni.Connection						= Connection;
			newUni.SizeLimit						= SizeLimit;
			newUni.DMXControllers				= DMXControllers;
			newUni.isEditing							= isEditing;
			//newUni.nameIsBad							= nameIsBad;
			newUni.BadNumber						= BadNumber;
			//newUni.isDirty = isDirty;
			newUni.myTag = myTag;
			newUni.matchesExactly = matchesExactly;
			return newUni;
		}

		public void Clone(DMXUniverse otherUniverse)
		{
			myID									= otherUniverse.myID;
			Name								= otherUniverse.myName;
			myLocation						= otherUniverse.myLocation;
			myComment							= otherUniverse.myComment;
			isActive							= otherUniverse.isActive;
			myUniverseNumber			= otherUniverse.myUniverseNumber;
			myxLightsAddress = otherUniverse.myxLightsAddress;
			Connection					= otherUniverse.Connection;
			SizeLimit						= otherUniverse.SizeLimit;
			DMXControllers			= otherUniverse.DMXControllers;
			isEditing							= otherUniverse.isEditing;
			//nameIsBad							= otherUniverse.nameIsBad;
			BadNumber						= otherUniverse.BadNumber;
			//isDirty = otherUniverse.isDirty;
			myTag = otherUniverse.myTag;
			matchesExactly = otherUniverse.matchesExactly;
		}

		public bool Equals(DMXUniverse otherUniverse)
		{
			bool eq = true;
			if (myID != otherUniverse.myID) eq = false;
			else if (myName.CompareTo(otherUniverse.myName) != 0) eq = false;
			else if (myLocation.CompareTo(otherUniverse.myLocation) != 0) eq = false;
			else if (myComment.CompareTo(otherUniverse.myComment) != 0) eq = false;
			else if (isActive != otherUniverse.isActive) eq = false;
			else if (myUniverseNumber != otherUniverse.myUniverseNumber) eq = false;
			else if (myxLightsAddress != otherUniverse.myxLightsAddress) eq = false;
			else if (Connection.CompareTo(otherUniverse.Connection) != 0) eq = false;
			else if (SizeLimit != otherUniverse.SizeLimit) eq = false;
			//else if (DMXControllers != otherUniverse.DMXControllers) eq = false;
			else if (isEditing != otherUniverse.isEditing) eq = false;
			//else if (nameIsBad							!= otherUniverse.nameIsBad)							eq = false;
			//else if (BadNumber != otherUniverse.BadNumber) eq = false;
			//else if (isDirty != otherUniverse.isDirty) eq = false;
			//else if (myTag != otherUniverse.myTag) eq = false;
			return eq;
		}


	}
}
