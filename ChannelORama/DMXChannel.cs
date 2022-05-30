using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LOR4;
using xLights22;
using FileHelper;

namespace UtilORama4
{
	//public enum ChannelType { LEDstring, SingleLight, Projector, Laser, Beacon, Strobe, Motor, Tree, CandyCane, Box, RGBstrip,
	//													RGBFlood, LEDstrip, Ropelight, FlickerBulb, UFO, Inflatable, Blowmold};


	public class DMXChannel : IDMXThingy, IComparable<DMXChannel>, IEquatable<DMXChannel>
	{
		//NOTE: myID is not permenant, it is assigned and used at runtime
		protected int myID = -1;
		//private string myName = "";
		protected string myName = "";
		protected string myComment = "";
		protected string myLocation = "";
		protected bool isActive = true;
		public Color Color = Color.White;

		//public long ColorLOR = 0x00FFFFFF;
		public DMXController DMXController = null;
		//private int myOutput = 1;
		protected int myOutput = 1;
		//public ChannelType ChannelType = ChannelType.SingleLight;
		public static List<DMXDeviceType> DeviceTypes = new List<DMXDeviceType>();
		public DMXDeviceType DeviceType = new DMXDeviceType("Unclassified", 0, 999);
		protected bool isEditing = false;
		protected bool isDirty = false;
		protected bool nameIsBad = true;
		public bool BadOutput = true;
		protected object myTag = null;
		public iLOR4Member TagLOR = null;
		public ixMember TagX = null;
		public iLOR4Member TagViz = null;
		public bool ExactLOR = false;
		public bool ExactX = false;
		public bool ExactViz = false;
		public static bool SortByName = false;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches


		public DMXChannel()
		{

		}
		public DMXChannel(DMXController controller)
		{

		}

		public DMXChannel(DMXChannel otherChannel)
		{
			// Clone Constructor
			myID = otherChannel.myID;
			Name = otherChannel.myName;
			myLocation = otherChannel.myLocation;
			myComment = otherChannel.myComment;
			isActive = otherChannel.isActive;
			Color = otherChannel.Color;
			DMXController = otherChannel.DMXController;
			OutputNum = otherChannel.myOutput;
			DeviceType = otherChannel.DeviceType;
			isEditing = otherChannel.isEditing;
			//nameIsBad				= otherChannel.nameIsBad;
			//isDirty = otherChannel.isDirty;
			//BadOutput			= otherChannel.BadOutput;
			myTag = otherChannel.myTag;
			matchesExactly = otherChannel.matchesExactly;
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
					for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
					{
						DMXChannel chn = DMXUniverse.AllChannels[c];
						if (chn.myID != myID)
						{
							//if (DMXUniverse.AllChannels[c].Name.ToLower().CompareTo(lowName) == 0)
							if (chn.myName.ToLower().CompareTo(lowName) == 0)
							{
								nameIsBad = true;
								chn.nameIsBad = true;
							}
						}
					}
				}
				return myName;
			}
			set
			{
				string newName = value.TrimStart();
				nameIsBad = false;  // reset, optomistic, for now
				if (newName.Length < 4)
				{
					nameIsBad = true;
				}
				else
				{
					string lowName = newName.ToLower();
					for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
					{
						DMXChannel chn = DMXUniverse.AllChannels[c];
						if (chn.myID != myID)
						{
							//if (DMXUniverse.AllChannels[c].Name.ToLower().CompareTo(lowName) == 0)
							if (chn.myName.ToLower().CompareTo(lowName) == 0)
							{
								nameIsBad = true;
								chn.nameIsBad = true;
							}
						}
					}
				}
				// accept it, even if bad
				myName = newName;
			}
		}

		public int OutputNum
		{
			get
			{
				BadOutput = false;
				for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
				{
					DMXChannel chn = DMXUniverse.AllChannels[c];
					//if (chn.myOutput == myOutput)
					if (chn.DMXAddress == DMXAddress)
					{
						if (chn.UniverseNumber == UniverseNumber)
						{
							if (chn.myID != myID)
							{
								// Flag this channel as bad
								BadOutput = true;
								// And flag the other channel as bad also
								DMXUniverse.AllChannels[c].BadOutput = true;
								// Match found, so output # is bad
								// But DON'T exit loop, as we may need to flag others as bad too
							}
						}
					}
				}
				return myOutput;
			}
			set
			{
				// Did it even change?
				if (myOutput != value)
				{
					BadOutput = false;
					for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
					{
						DMXChannel chn = DMXUniverse.AllChannels[c];
						//if (chn.myOutput == value)
						if (chn.DMXAddress == DMXAddress)
						{
							if (chn.UniverseNumber == UniverseNumber)
							{
								if (chn.myID != myID)
								{
									BadOutput = true;
									chn.BadOutput = true;
									// Match found, so output # is bad
									// But DON'T exit loop, as we may need to flag others as bad too
								}
							}
						}
					}
					myOutput = value;
				}
			}
		}

		public DMXUniverse DMXUniverse
		{
			get
			{
				DMXUniverse univ = null;
				if (DMXController != null)
				{
					univ = DMXController.DMXUniverse;
				}
				return univ;
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

		public int UniverseNumber
		{
			get
			{
				int ret = 0;
				if (DMXController != null)
				{
					ret = DMXController.DMXUniverse.UniverseNumber;
				}
				return ret;
			}
		}

		public int ColorLOR
		{
			get
			{
				return LOR4Admin.Color_NettoLOR(Color);
			}
			set
			{
				Color = LOR4Admin.Color_LORtoNet(value);
			}
		}

		public string ColorName
		{
			get
			{
				return Color.Name;
			}
		}

		public string ColorHTML
		{
			get
			{
				return ColorTranslator.ToHtml(Color);
			}
			set
			{
				Color = ColorTranslator.FromHtml(value);
			}
		}

		public int DMXAddress
		{
			get
			{
				int ret = 0;
				if (DMXController != null)
				{
					ret = DMXController.DMXStartAddress + myOutput - 1;
				}
				return ret;

			}
		}

		public int xLightsAddress
		{
			get
			{
				int ret = 0;
				if (DMXController != null)
				{
					ret = DMXController.xLightsAddress + OutputNum - 1;
				}
				return ret;

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
			string ret = OutputNum.ToString("00") + ": " + Name;
			return ret;
		}

		public int CompareTo(DMXChannel otherChannel)
		{
			int ret = 0;
			if (SortByName)
			{
				ret = myName.ToLower().CompareTo(otherChannel.myName.ToLower());
			}
			else
			{
				if (xLightsAddress > otherChannel.xLightsAddress) ret = 1;
				if (xLightsAddress < otherChannel.xLightsAddress) ret = -1;
				if (ret == 0)
				{
					ret = myName.ToLower().CompareTo(otherChannel.myName.ToLower());
				}
			}
			return ret;
		}

		public DMXChannel Clone()
		{
			DMXChannel newChan = new DMXChannel();
			newChan.myID = myID;
			newChan.Name = myName;
			newChan.myLocation = myLocation;
			newChan.myComment = myComment;
			newChan.isActive = isActive;
			newChan.Color = Color;
			newChan.DMXController = DMXController;
			newChan.OutputNum = myOutput;
			newChan.DeviceType = DeviceType;
			newChan.isEditing = isEditing;
			//newChan.nameIsBad				= nameIsBad;
			//newChan.BadOutput			= BadOutput;
			//newChan.isDirty = isDirty;
			newChan.myTag = myTag;
			newChan.matchesExactly = matchesExactly;

			return newChan;
		}

		public void Clone(DMXChannel otherChannel)
		{
			myID = otherChannel.myID;
			Name = otherChannel.myName;
			myLocation = otherChannel.myLocation;
			myComment = otherChannel.myComment;
			isActive = otherChannel.isActive;
			Color = otherChannel.Color;
			DMXController = otherChannel.DMXController;
			OutputNum = otherChannel.myOutput;
			DeviceType = otherChannel.DeviceType;
			isEditing = otherChannel.isEditing;
			//nameIsBad				= otherChannel.nameIsBad;
			//isDirty = otherChannel.isDirty;
			//BadOutput			= otherChannel.BadOutput;
			myTag = otherChannel.myTag;
			matchesExactly = otherChannel.matchesExactly;
		}

		public bool Equals(DMXChannel otherChannel)
		{
			bool eq = true;
			if (myID != otherChannel.myID) eq = false;
			else if (myName.CompareTo(otherChannel.myName) != 0) eq = false;
			else if (myLocation.CompareTo(otherChannel.myLocation) != 0) eq = false;
			else if (myComment.CompareTo(otherChannel.myComment) != 0) eq = false;
			else if (isActive != otherChannel.isActive) eq = false;
			else if (Color.ToArgb() != otherChannel.Color.ToArgb()) eq = false;
			//DMXController = otherChannel.DMXController;
			else if (myOutput != otherChannel.myOutput) eq = false;
			else if (DeviceType.ID != otherChannel.DeviceType.ID) eq = false;
			else if (isEditing != otherChannel.isEditing) eq = false;
			//else if (nameIsBad				!= otherChannel.nameIsBad)					eq = false;
			//else if (BadOutput			!= otherChannel.BadOutput)				eq = false;
			//else if (isDirty != otherChannel.isDirty) eq = false;
			//else if (myTag != otherChannel.myTag) eq = false;
			return eq;
		}

	}
}
