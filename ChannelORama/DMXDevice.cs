using DocumentFormat.OpenXml.Wordprocessing;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class DeviceType : IComparable<DeviceType>
	{
		protected int myID = -1;
		protected string myName = "";
		public string Comment = "";
		public int DisplayOrder;
		public TreeNodeAdv TreeNode = null;

		public DeviceType()
		{ 
		}

		public DeviceType(string theName, int theID, int theOrder)
		{
			myName = theName.Trim();
			myID = theID;
			DisplayOrder = theOrder;
		}

		public int ID { get {return myID;} set{myID = value; }}
		public string Name { get { return myName;} set { myName = value; } }

		public int CompareTo(DeviceType otherDevice)
		{
			int ret = 0;
			if (DisplayOrder > otherDevice.DisplayOrder) ret = 1;
			if (DisplayOrder < otherDevice.DisplayOrder) ret = -1;
			return ret;
		}

		public override string ToString()
		{
			return myName;
		}

		public int UsedByCount
		{
			get
			{
				int ret = 0;
				foreach (Channel chan in Universe.AllChannels)
				{
					if (chan.DeviceType.ID == myID)
					{
						ret++;
					}
				}
				return ret;
			}
		}
		
		public string UsedByChannels
		{
			get
			{
				string ret = "";
				foreach (Channel chan in Universe.AllChannels)
				{
					if (chan.DeviceType.ID == myID)
					{
						// add this channel's name + a CR/LF
						ret += chan.Address.ToString("000") + " ";
						ret += chan.Name + "\r\n";
					}
				}
				if (ret.Length > 4)
				{
					// Remove the very last CR/LF
					ret = ret.Substring(0, ret.Length - 2);
				}
				return ret;
			}
		} // End (read-only) Property UsedByChannels

		public DeviceType Copy()
		{
			// Returns a new DeviceType with all the same values as this DeviceTyupe
			// EXCEPT for ID, which is not copied, so this DeviceType gets a new ID, and is not quite an exact clone
			{
				DeviceType newDevice = new DeviceType();
				newDevice.Name = myName;
				newDevice.Comment = Comment;
				newDevice.DisplayOrder = DisplayOrder;
				newDevice.TreeNode = TreeNode;
				newDevice.myID = -Math.Abs(myID);  // assign a new ID, negative of the original to indicate it is a copy, not an original.

				return newDevice;
			}
		}

		public DeviceType Clone()
		// Returns a new DeviceType with all the same values as this DeviceType
		// INCLUDING ID, so it is an exact clone, not a new DeviceType with a new ID
		{
			DeviceType newDevice = Copy();
			newDevice.myID = myID;
			return newDevice;
		}

		public void Clone(DeviceType otherDevice)
		// Copies all the values from otherDevice into this Controller
		{
			myID = otherDevice.myID;
			ApplyChanges(otherDevice);
		}

		public bool Equals(DeviceType otherDevice)
		{
			bool eq = true;
			//if (myID != otherDevice.myID) eq = false;
			if (myName.CompareTo(otherDevice.myName) != 0) eq = false;
			else if (Comment.CompareTo(otherDevice.Comment) != 0) eq = false;
			else if (DisplayOrder.CompareTo(otherDevice.DisplayOrder) != 0) eq = false;
			return eq;
		}

		public void ApplyChanges(DeviceType otherDevice)
		// Copies all the values from otherDevice into this Controller
		// Except for ID, which is not copied, so this Controller keeps its original ID
		{
			//myID = otherDevice.myID;
			Name = otherDevice.myName;
			Comment = otherDevice.Comment;
			DisplayOrder = otherDevice.DisplayOrder;
		}



	} // End class DeviceTypes
}  // End namespace
