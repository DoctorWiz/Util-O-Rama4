using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class DMXController : IDMXThingy, IComparable<DMXController>,  IEquatable<DMXController>
	{
		//NOTE: myID is not permenant, it is assigned and used only at runtime
		protected int myID = -1;
		//private string myName = "";
		protected string myName = "";
		protected string myComment = "";
		protected string myLocation = "";
		protected bool isActive = true;
		//private string myLetter = "";  // Must be unique!!
		protected string myLetter = ""; // Must be Unique!!
		public int OutputCount = 16;
		public int DMXStartAddress = 1;
		public string ControllerBrand = "";
		public string ControllerModel = "";
		public int Voltage = 120;
		protected bool isEditing = false;
		protected bool isDirty = false;
		protected bool nameIsBad = false;
		public bool BadLetter = false;
		protected object myTag = null;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches

		public List<DMXChannel> DMXChannels = new List<DMXChannel>();
		public DMXUniverse DMXUniverse = new DMXUniverse();

		public int xLightsAddress
		{
			get
			{
				int ret = 0;
				if (DMXUniverse != null)
				{
					ret = DMXUniverse.xLightsAddress + DMXStartAddress - 1;
				}
				return ret;
			}
		}
		
		
		public DMXChannel DMXChannelAt(int DMXAddress)
		{
			DMXChannel ret = null;



			return ret;
		}

		public DMXChannel xLightsChannelAt(int xLightsChannelID)
		{
			DMXChannel ret = null;



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
					for (int c = 0; c < DMXUniverse.DMXControllers.Count; c++)
					{
						if (DMXUniverse.DMXControllers[c].myID != myID)
						{
							//if (DMXUniverse.DMXControllers[c].Name.ToLower().CompareTo(lowName) == 0)
							if (DMXUniverse.DMXControllers[c].myName.ToLower().CompareTo(lowName) == 0)
							{
								nameIsBad = true;
								DMXUniverse.DMXControllers[c].nameIsBad = true;
							}
						}
					}
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
					for (int c = 0; c < DMXUniverse.DMXControllers.Count; c++)
					{
						if (DMXUniverse.DMXControllers[c].myID != myID)
						{
							//if (DMXUniverse.DMXControllers[c].Name.ToLower().CompareTo(lowName) == 0)
							if (DMXUniverse.DMXControllers[c].myName.ToLower().CompareTo(lowName) == 0)
							{
								nameIsBad = true;
								DMXUniverse.DMXControllers[c].nameIsBad = true;
							}
						}
					}
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
		public bool ExactMatch { get { return matchesExactly; } set { matchesExactly = value; } }

		public string LetterID
		{
			get
			{
				BadLetter = false;  // reset, optomistic, for now
				if (myLetter.Length != 1)
				{
					BadLetter = true;
				}
				else
				{
					for (int c = 0; c < DMXUniverse.DMXControllers.Count; c++)
					{
						if (DMXUniverse.DMXControllers[c].myID != myID)
						{
							//if (DMXUniverse.DMXControllers[c].LetterID.CompareTo(myLetter) == 0)
							if (DMXUniverse.DMXControllers[c].myLetter.CompareTo(myLetter) == 0)
							{
								BadLetter = true;
								DMXUniverse.DMXControllers[c].BadLetter = true;
							}
						}
					}
				}
				return myLetter;
			}
			set
			{
				string newLetter = value.Trim().ToUpper();
				BadLetter = false;  // reset, optomistic, for now
				if (newLetter.Length != 1)
				{
					BadLetter = true;
				}
				else
				{
					for (int c = 0; c < DMXUniverse.DMXControllers.Count; c++)
					{
						if (DMXUniverse.DMXControllers[c].myID != myID)
						{
							//if (DMXUniverse.DMXControllers[c].LetterID.CompareTo(newLetter) == 0)
							if (DMXUniverse.DMXControllers[c].myLetter.CompareTo(newLetter) == 0)
							{
								BadLetter = true;
								DMXUniverse.DMXControllers[c].BadLetter = true;
							}
						}
					}
				}
				// accept it, even if bad
				myLetter = newLetter;
			}
		}

		public int UniverseNumber
		{
			get
			{
				return DMXUniverse.UniverseNumber;
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
			string ret = DMXStartAddress.ToString("000") + "-";
			int l = DMXStartAddress + OutputCount - 1;
			ret += l.ToString("000") + ": ";
			ret += LetterID + ": " + Name;
			return ret;
		}

		public int CompareTo(DMXController otherController)
		{
			int ret = 0;
			if (xLightsAddress > otherController.xLightsAddress) ret = 1;
			if (xLightsAddress < otherController.xLightsAddress) ret = -1;
			if (ret == 0)
			{
				ret = LetterID.CompareTo(otherController.LetterID);
			}
			if (ret == 0)
			{
				ret = Name.CompareTo(otherController.Name);
			}
			return ret;
		}

		public DMXController Clone()
		{
			DMXController newCtl = new DMXController();
			newCtl.myID								= myID;
			newCtl.Name							= myName;
			newCtl.myLocation					= myLocation;
			newCtl.myComment					= myComment;
			newCtl.isActive						= isActive;
			newCtl.LetterID					= myLetter;
			newCtl.OutputCount			= OutputCount;
			newCtl.DMXStartAddress	= DMXStartAddress;
			newCtl.ControllerBrand	= ControllerBrand;
			newCtl.ControllerModel	= ControllerModel;
			newCtl.Voltage					= Voltage;
			newCtl.DMXChannels			= DMXChannels;
			newCtl.DMXUniverse			= DMXUniverse;
			newCtl.isEditing					= isEditing;
			//newCtl.BadLetter				= BadLetter;
			//newCtl.nameIsBad					= nameIsBad;
			//newCtl.isDirty = isDirty;
			newCtl.myTag = myTag;
			newCtl.matchesExactly = matchesExactly;

			return newCtl;
		}

		public void Clone(DMXController otherController)
		{
			myID							= otherController.myID;
			Name						= otherController.myName;
			myLocation				= otherController.myLocation;
			myComment					= otherController.myComment;
			isActive					= otherController.isActive;
			LetterID				= otherController.myLetter;
			OutputCount			= otherController.OutputCount;
			DMXStartAddress = otherController.DMXStartAddress;
			ControllerBrand = otherController.ControllerBrand;
			ControllerModel = otherController.ControllerModel;
			Voltage					= otherController.Voltage;
			DMXChannels			= otherController.DMXChannels;
			DMXUniverse			= otherController.DMXUniverse;
			isEditing					= otherController.isEditing;
			//BadLetter				= otherController.BadLetter;
			//nameIsBad					= otherController.nameIsBad;
			//isDirty = otherController.isDirty;
			myTag = otherController.myTag;
			matchesExactly = otherController.matchesExactly;
		}

		public bool Equals(DMXController otherController)
		{
			bool eq = true;
			if (myID != otherController.myID) eq = false;
			else if (myName.CompareTo(otherController.myName) != 0) eq = false;
			else if (myLocation.CompareTo(otherController.myLocation) != 0) eq = false;
			else if (myComment.CompareTo(otherController.myComment) != 0) eq = false;
			else if (isActive != otherController.isActive) eq = false;
			else if (myLetter != otherController.myLetter) eq = false;
			else if (OutputCount != otherController.OutputCount) eq = false;
			else if (DMXStartAddress != otherController.DMXStartAddress) eq = false;
			else if (ControllerBrand.CompareTo(otherController.ControllerBrand) != 0) eq = false;
			else if (ControllerModel.CompareTo(otherController.ControllerModel) != 0) eq = false;
			else if (Voltage != otherController.Voltage) eq = false;
			//DMXChannels = otherController.DMXChannels;
			//DMXUniverse = otherController.DMXUniverse;
			else if (isEditing != otherController.isEditing) eq = false;
			//else if (BadLetter				!= otherController.BadLetter)										eq = false;
			//else if (nameIsBad					!= otherController.nameIsBad)											eq = false;
			//else if (isDirty != otherController.isDirty) eq = false;
			//else if (myTag != otherController.myTag) eq = false;
			return eq;
		}

	}
}
