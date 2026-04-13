using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced

namespace UtilORama4
{
	public class DeviceTypes : IComparable<DeviceTypes>
	{
		protected int myID = -1;
		protected string myName = "";
		public string Description = "";
		public int DisplayOrder;
		public TreeNodeAdv TreeNode = null;

		public DeviceTypes()
		{ 
		}

		public DeviceTypes(string theName, int theID, int theOrder)
		{
			myName = theName.Trim();
			myID = theID;
			DisplayOrder = theOrder;
		}

		public int ID { get {return myID;} set{myID = value; }}
		public string Name { get { return myName;} set { myName = value; } }

		public int CompareTo(DeviceTypes otherDevice)
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

	}
}
