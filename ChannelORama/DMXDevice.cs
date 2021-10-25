using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class DMXDeviceType : IComparable<DMXDeviceType>
	{
		protected int myID = -1;
		protected string myName = "";
		public int DisplayOrder;

		public DMXDeviceType()
		{ 
		}

		public DMXDeviceType(string theName, int theID, int theOrder)
		{
			myName = theName.Trim();
			myID = theID;
			DisplayOrder = theOrder;
		}

		public int ID { get {return myID;} set{myID = value; }}
		public string Name { get { return myName;} set { myName = value; } }

		public int CompareTo(DMXDeviceType otherDevice)
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
