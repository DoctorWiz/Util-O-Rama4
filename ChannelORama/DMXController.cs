using FileHelper;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using FileHelper;

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
		protected string myIdentifier = "";
		protected int myUnitID = 1; // LOR Unit ID, affects DMX address on some LOR controller models
		protected int myOutputCount = 16;
		protected int myStartAddress = 1;
		public string ControllerBrand = "";
		public string ControllerModel = "";
		public int Voltage = 120;
		protected bool isEditing = false;
		protected bool isDirty = false;
		// These are not stored values, they are calculated on the fly by checking for duplicates in the DMXUniverse.AllControllers list, and other rules, so they are not stored values, but calculated values, so they are properties, not fields
		//protected bool nameIsBad = false;
		//public bool BadIdentity = false;
		//public bool BadAddress = false;
		//public bool identityIsBad = false;
		//public bool addressIsBad = false;
		protected object myTag = null;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		public bool SortByName = false;
		public bool SortByIdentifier = false;

		public List<DMXChannel> DMXChannels = new List<DMXChannel>();
		public DMXUniverse DMXUniverse = new DMXUniverse();
		public TreeNodeAdv TreeNode = null;

		public int xLightsAddress
		{
			get
			{
				int ret = 0;
				if (DMXUniverse != null)
				{
					ret = DMXUniverse.xLightsAddress + myStartAddress - 1;
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
				/*
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
				*/
				return myName;
			}
			set
			{
				string newName = value.Trim();
				/*
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
				*/
				myName = newName;
			}
		}

		public string FullName
		{
			get
			{
				string ret = "";
				if (myUnitID > 0)
				{
					ret = "Unit:" + myUnitID.ToString("00") + ": ";
				}
				ret += myStartAddress.ToString("000") + "-";
				int l = myStartAddress + myOutputCount - 1;
				ret += l.ToString("000") + ": ";
				if (myIdentifier.Length > 0)
				{
					ret += myIdentifier + ": ";
				}
				ret += myName;
				return ret;

			}
		}
		public int ID { get { return myID; } set { myID = value; } }
		public string Comment { get { return myComment; } set { myComment = value; } }
		public string Location { get { return myLocation; } set { myLocation = value; } }
		public bool Active { get { return isActive; } set { isActive = value; } }
		public bool Editing { get { return isEditing; } set { isEditing = value; } }
		public bool Dirty { get { return isDirty; } set { isDirty = value; } }
		public object Tag { get { return myTag; } set { myTag = value; } }
		public DMXObjectType ObjectType { get { return DMXObjectType.DMXController; } }

		public bool ExactMatch { get { return matchesExactly; } set { matchesExactly = value; } }
		// Wrapper
		public DMXUniverse Universe { get { return DMXUniverse; } set { DMXUniverse = value; } }

		public int DMXStartAddress
		{
			get
			{ 
				return myStartAddress;
			}
			set
			{
				int sa = Math.Abs(value);
				if ((0 < sa) && (sa < 513))
					myStartAddress = Math.Abs(value);
			} 
		}

		public int Number
		// Wrapper for DMXStartAddress, to allow for compatibility with DMXThingy interface, which requires a Number property, but we want to call it DMXStartAddress in DMXController for clarity, but we also want to be able to use Number as a wrapper for DMXStartAddress for compatibility with DMXThingy interface and other code that uses DMXThingy interface and expects a Number property.
		{
			get
			{
				return myStartAddress;
			}
			set
			{
				int sa = Math.Abs(value);
				if ((0 < sa) && (sa < 513))
					myStartAddress = Math.Abs(value);
			}
		}


		public bool BadName
		{ 
			get
			{
				bool ret = false;
				string lowname = myName.ToLower();
				int myOriginalID = Math.Abs(myID);
				string dbg = "This controller " + this.ID.ToString() + "-" + this.Name + " Name conflicts with";
				for (int ch = 0; ch < DMXUniverse.AllControllers.Count; ch++)
					{
					DMXController ctl = DMXUniverse.AllControllers[ch];
					if (ctl.myID != myOriginalID)
					{
						if (ctl.Name.CompareTo(myName) == 0)
						{
							ret = true;
							dbg += "\r\n  " + ctl.ID.ToString() + "-" + ctl.Name;
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

		public bool BadIdentity
		{
			get {
				bool ret = false;
				string lowident = myIdentifier.ToLower();
				int myOriginalID = Math.Abs(myID);
				string dbg = "This controller " + this.ID.ToString() + "-" + this.Name + " Identifier " + this.Identifier + " conflicts with";
				if (myIdentifier.Length > 0)
				{
					for (int ch=0; ch < DMXUniverse.AllControllers.Count; ch++)
					{
						DMXController ctl = DMXUniverse.AllControllers[ch];
						if (ctl.myID != myOriginalID)
						{
							if (ctl.Identifier.CompareTo(myIdentifier) == 0)
							{
								ret = true;
								dbg += "\r\n  " + ctl.ID.ToString() + "-" + ctl.Name + " Identifier " + ctl.Identifier;
								//identityIsBad = true;
								//DMXUniverse.AllControllers[ch].identityIsBad = true;
							}
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
				string dbg = "This controller " + this.ID.ToString() + "-" + this.Name + " Address " + this.myStartAddress + " conflicts with";
				for (int ch = 0; ch < DMXUniverse.DMXControllers.Count; ch++)
				{
					DMXController ctl = DMXUniverse.DMXControllers[ch];
					if (ctl.myID != myOriginalID)
					{
						int start= (ctl.myStartAddress);
						int end = ctl.myStartAddress + ctl.myOutputCount - 1;
						if (myStartAddress >= start && myStartAddress <= end)
						{
							ret = true;
							dbg += "\r\n  " + ctl.ID.ToString() + "-" + ctl.Name + " Address " + ctl.myStartAddress;
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
		
		public bool BadAddress
			// Wrapper
		{
			get
			{
				return BadNumber;
			}
		}

		public string Identifier
		{
			// NO LONGER REQUIRED TO BE UNIQUE, just a letter for display purposes, but still should be unique if possible
			get
			{
				return myIdentifier;
			}
			set
			{
				string newIdent = value.Trim().ToUpper();
				if (newIdent.Length > 5)
				{
					newIdent= newIdent.Substring(0, 5);
				}
				// accept it, even if bad
				myIdentifier = newIdent;
			}
		}

		public int UniverseNumber
		{
			get
			{
				return DMXUniverse.UniverseNumber;
			}
		}

		public int UnitID
		{
			get
			{
				if (myUnitID == 16)
				{
					if (Fyle.isWiz)
					{
						//Debugger.Break(); ;
					}
				}
				return myUnitID;
			}
			set
			{
				int ui = Math.Abs(value);
				if (ui == 16)
				{
					if (Fyle.isWiz)
					{
						//Debugger.Break(); ;
					}
				}
				if ((ui > 0) && (ui<33))
				{ 
						myUnitID = value;
						// Set Start Address based on Unit Number
						myStartAddress = (ui - 1) * 16 + 1;
				}
				else if (ui == 0)
				{
					// Zero is allowable, means NO Unit ID,
					// Leave StartAddress unchanged.
					myUnitID = ui;
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(UnitID), value, "Unit Number must be between 0 and 32.");
				}
			}
		}

		public int OutputCount
		{
			get
			{
				return myOutputCount;
			}
			set
			{
				int oc=Math.Abs(value);
				if ((oc > 0) && (oc < 65))
				{
					myOutputCount = oc;
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(OutputCount), value, "Output Count must be between 1 and 64.");
				}

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
			string ret = myStartAddress.ToString("000") + "-";
			int l = myStartAddress + myOutputCount - 1;
			ret += l.ToString("000") + ": ";
			ret += Identifier + ": " + Name;
			return ret;
		}

		public int CompareTo(DMXController otherController)
		{
			int ret = 0;
			if (SortByName)
			{
				ret = Name.CompareTo(otherController.Name);
			}
			else if (SortByIdentifier)
			{
				ret = Identifier.CompareTo(otherController.Identifier);
			}
			else
			{
				ret = UniverseNumber.CompareTo(otherController.UniverseNumber);
				if (ret == 0)
				{
					ret = myStartAddress.CompareTo(otherController.myStartAddress);
				}
			}
			return ret;
		}

		public DMXController Copy()
		// Returns a new DMXController with all the same values as this DMXController
		// EXCEPT for ID, which is not copied, so this DMXController gets a new ID, and is not quite an exact clone
		{
			DMXController newCtl = new DMXController();
			newCtl.Name = myName;
			newCtl.myLocation = myLocation;
			newCtl.myComment = myComment;
			newCtl.isActive = isActive;
			newCtl.Identifier = myIdentifier;
			newCtl.myUnitID = myUnitID;
			newCtl.myOutputCount = myOutputCount;
			newCtl.myStartAddress = myStartAddress;
			newCtl.ControllerBrand = ControllerBrand;
			newCtl.ControllerModel = ControllerModel;
			newCtl.Voltage = Voltage;
			newCtl.DMXChannels = DMXChannels;
			newCtl.DMXUniverse = DMXUniverse;
			newCtl.isEditing = isEditing;
			//newCtl.BadIdentity				= BadIdentity;
			//newCtl.nameIsBad					= nameIsBad;
			//newCtl.isDirty = isDirty;
			newCtl.myTag = myTag;
			newCtl.matchesExactly = matchesExactly;
			 
			newCtl.myID = -Math.Abs(myID);  // assign a new ID, negative of the original to indicate it is a copy, not an original.


			return newCtl;
		}

		public DMXController Clone()
		// Returns a new DMXController with all the same values as this DMXController
		// INCLUDING ID, so it is an exact clone, not a new DMXController with a new ID
		{
			DMXController newCtl = Copy();
			newCtl.myID								= myID;
			return newCtl;
		}

		public void ApplyChanges(DMXController otherController)
		// Copies all the values from otherController into this DMXController
		// Except for ID, which is not copied, so this DMXController keeps its original ID
		{
			//myID = otherController.myID;
			Name = otherController.myName;
			myLocation = otherController.myLocation;
			myComment = otherController.myComment;
			isActive = otherController.isActive;
			Identifier = otherController.myIdentifier;
			myUnitID = otherController.myUnitID;
			myOutputCount = otherController.myOutputCount;
			myStartAddress = otherController.myStartAddress;
			ControllerBrand = otherController.ControllerBrand;
			ControllerModel = otherController.ControllerModel;
			Voltage = otherController.Voltage;
			DMXChannels = otherController.DMXChannels;
			DMXUniverse = otherController.DMXUniverse;
			isEditing = otherController.isEditing;
			//BadIdentity				= otherController.BadIdentity;
			//nameIsBad					= otherController.nameIsBad;
			//isDirty = otherController.isDirty;
			myTag = otherController.myTag;
			matchesExactly = otherController.matchesExactly;
		}

		public void Clone(DMXController otherController)
		// Copies all the values from otherController into this DMXController
		{
			myID							= otherController.myID;
			ApplyChanges(otherController);
		}

		public bool Equals(DMXController otherController)
		{
			bool eq = true;
			//if (myID != otherController.myID) eq = false;
			if (myName.CompareTo(otherController.myName) != 0) eq = false;
			else if (myLocation.CompareTo(otherController.myLocation) != 0) eq = false;
			else if (myComment.CompareTo(otherController.myComment) != 0) eq = false;
			else if (isActive != otherController.isActive) eq = false;
			else if (myUnitID != otherController.myUnitID) eq = false;
			else if (myIdentifier != otherController.myIdentifier) eq = false;
			else if (myOutputCount != otherController.myOutputCount) eq = false;
			else if (myStartAddress != otherController.myStartAddress) eq = false;
			else if (ControllerBrand.CompareTo(otherController.ControllerBrand) != 0) eq = false;
			else if (ControllerModel.CompareTo(otherController.ControllerModel) != 0) eq = false;
			else if (Voltage != otherController.Voltage) eq = false;
			//DMXChannels = otherController.DMXChannels;
			//DMXUniverse = otherController.DMXUniverse;
			else if (isEditing != otherController.isEditing) eq = false;
			//else if (BadIdentity				!= otherController.BadIdentity)										eq = false;
			//else if (nameIsBad					!= otherController.nameIsBad)											eq = false;
			//else if (isDirty != otherController.isDirty) eq = false;
			//else if (myTag != otherController.myTag) eq = false;
			return eq;
		}

	}
}
