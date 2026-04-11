using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using LOR4;
using xLights22;
using FileHelper;
using System.Threading.Channels;

namespace UtilORama4
{
	//public enum ChannelType { LEDstring, SingleLight, Projector, Laser, Beacon, Strobe, Motor, Tree, CandyCane, Box, RGBstrip,
	//													RGBFlood, LEDstrip, Ropelight, FlickerBulb, UFO, Inflatable, Blowmold};


	public class DMXChannel : IDMXThingy, IComparable<DMXChannel>, IEquatable<DMXChannel>
	{
		//NOTE: myID is not permanant, it is assigned and used at runtime
		protected int myID = -1;
		//private string myName = "";
		protected string myName = "";
		protected string myComment = "";
		protected string myLocation = "";
		protected bool isActive = true;
		public Color Color = Color.White;
		public TreeNodeAdv myNode = null;


		//public long ColorLOR = 0x00FFFFFF;
		public DMXController DMXController = null;
		//private int myOutput = 1;
		protected int myOutput = 1;
		//public ChannelType ChannelType = ChannelType.SingleLight;
		//public static List<DMXDeviceType> DeviceTypes = new List<DMXDeviceType>();
		public DMXDeviceType DeviceType = new DMXDeviceType("Unclassified", 0, 999);
		protected bool isEditing = false;
		protected bool isDirty = false;
		protected bool nameIsBad = true;
		//public bool BadOutput = true;
		protected object myTag = null;
		public iLOR4Member TagLOR = null;
		public ixMember TagX = null;
		public iLOR4Member TagViz = null;
		public bool ExactLOR = false;
		public bool ExactX = false;
		public bool ExactViz = false;
		public static bool SortByName = false;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
																						//public TreeNodeAdv TreeNode = null;
		public string ColorName = "";


		public DMXChannel()
		{

		}
		public DMXChannel(DMXController controller, string theName = "")
		{
			DMXController = controller;
			DMXController.DMXChannels.Add(this);
			if (theName.Length > 0)
			{ Name = theName; }
		}

		public DMXChannel(string theName)
		{
			Name = theName;
		}

		public DMXChannel(DMXController controller)
		{
			DMXController = controller;
			DMXController.DMXChannels.Add(this);
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
			//DMXController.DMXChannels.Add(this);
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
				string ret = myOutput.ToString("00") + " " + myName;
				return ret;
			}
		}



		public int OutputNum
		{
			get
			{
				return myOutput;
			}
			set
			{
				myOutput = Math.Abs(value);
			}
		}

		public int Number
		// Wrapper for OutputNum, for compatibility with the IDMXThingy interface, and to make it more intuitive when we are talking about DMX channels, which are usually identified by their output number.
		{
			get
			{
				return myOutput;
			}
			set
			{
				myOutput = Math.Abs(value);
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
		public bool Active
		{
			get
			{
				return isActive;
			}
			set
			{
				isActive = value;
			}
		}

		public bool Editing { get { return isEditing; } set { isEditing = value; } }
		public bool Dirty { get { return isDirty; } set { isDirty = value; } }
		public bool BadName
		{
			get
			{
				bool ret = false;
				string lowname = myName.ToLower();
				int myOriginalID = Math.Abs(myID);
				string dbg = "This channel " + this.ID.ToString() + "-" + this.Name + " Name conflicts with";
				for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
				{
					DMXChannel chn = DMXUniverse.AllChannels[c];
					if (chn.ID != myOriginalID)
					{
						if (chn.Name.ToLower().CompareTo(lowname) == 0)
						{
							ret = true;
							dbg += "\r\n  " + chn.ID.ToString() + "-" + chn.Name;
							// Match found, so name is bad
							// But DON'T exit loop, as we may need to flag others as bad too
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
				string dbg = "This channel " + this.ID.ToString() + "-" + this.Name + "  Output " + this.OutputNum + " conflicts with";
				if (isActive)
				{
					if (this.OutputNum < 1)
					{
						ret = true;
					}
					else if (this.OutputNum > this.DMXController.OutputCount)
					{
						ret = true;
					}
					else
					{
						//for (int c = 0; c < DMXUniverse.AllChannels.Count; c++)
						for (int c = 0; c < this.DMXController.DMXChannels.Count; c++)
						{
							//DMXChannel chn = DMXUniverse.AllChannels[c];
							DMXChannel chn = this.DMXController.DMXChannels[c];
							//if (chn.myOutput == myOutput)
							if (chn.DMXAddress == DMXAddress)
							{
								//if (chn.UniverseNumber == UniverseNumber)
								{
									if (chn.myID != myOriginalID)
									{
										if (myID > 0)
										{
											ret = true;
											dbg += "\r\n  " + chn.ID.ToString() + "-" + chn.Name + " Output " + chn.OutputNum;
											// Match found, so output # is bad
											// But DON'T exit loop, as we may need to flag others as bad too
										}
									}
								}
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

		public bool BadOutput
		// Wrapper
		{
			get
			{
				return BadNumber;
			}
		}

		public object Tag { get { return myTag; } set { myTag = value; } }
		public bool ExactMatch { get { return matchesExactly; } set { matchesExactly = value; } }

		public DMXObjectType ObjectType { get { return DMXObjectType.DMXChannel; } }

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

		/*
		public string ColorName
		{
			get
			{
				return Color.Name;
			}
		}
		*/

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
				ret = UniverseNumber.CompareTo(otherChannel.UniverseNumber);
				if (ret == 0)
				{
					{
						ret = DMXAddress.CompareTo(otherChannel.DMXAddress);
					}
				}
			}
			return ret;
		}

		public DMXChannel Copy()
		{
			DMXChannel newChan = new DMXChannel();
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
			// Store the original ID as a negative number.
			newChan.myID = -Math.Abs(myID);


			return newChan;
		}

		public DMXChannel Clone()
		{
			// Clones everything, including ID
			DMXChannel newChan = Copy();
			newChan.myID = myID;
			return newChan;
		}

		public void ApplyChanges(DMXChannel otherChannel)
		{
			//myID = otherChannel.myID;
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

		public void Clone(DMXChannel otherChannel)
		{
			myID = otherChannel.myID;
			ApplyChanges(otherChannel);
		}

		public bool Equals(DMXChannel otherChannel)
		{
			bool eq = true;
			//if (myID != otherChannel.myID) eq = false;
			if (myName.CompareTo(otherChannel.myName) != 0) eq = false;
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
