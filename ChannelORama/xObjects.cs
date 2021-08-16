using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using xUtils;

namespace xUtils
{
	public enum xMemberType { Model, RGBmodel, Pixels, ModelGroup }


	public interface xMember : IComparable<xMember>
	{
		string Name
		{
			get;
		}

		xModelTypes xModelType
		{
			get;
			set;
		}
		void ChangeName(string newName);

  		int CompareTo(xMember otherObj);

		string ToString();

		xMemberType xMemberType
		{
			get;
		}

		bool Selected
		{
			get;
			set;
		}

		bool MatchExact
		{
			get;
			set;
		}

		object Tag
		{
			get;
			set;
		}

		int UniverseNumber
		{
			get;
			set;
		}

		int DMXAddress
		{
			get;
			set;
		}

		int xLightsAddress
		{
			get;
			set;
		}


	} // End Interface xMember



/// <summary>
/// Model: StringType = Single Color
/// </summary>
	public class xModel : xMember, IComparable<xMember>
	{
		private string myName = "";
		private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		private int universeNumber = 0;
		private int dmxAddress = 0;
		private int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;
		

		public xModel(string theName)
		{
			myName = theName;
		}

		public xModel(string theName, xModelTypes theType, int theAddress)
		{
			myName = theName;
			myType = theType;
			xLightsAddress = theAddress;
		}
		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
		}

		public xModelTypes xModelType
		{
			get
			{
				return myType;
			}
			set
			{
				myType = value;
			}
		}

		public int CompareTo(xMember otherObj)
		{
			return Name.CompareTo(otherObj.Name);
		}

		public Color Color
		{
			get
			{
				return myColor;
			}
			set
			{
				myColor = value;
			}
		}

		public string ToString()
		{
			return myName;
		}

		public xMemberType xMemberType
		{
			get
			{
				return xMemberType.Model;
			}
		}

		public bool Selected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
			}
		}

		public bool MatchExact
		{
			get
			{
				return matchExact;
			}
			set
			{
				matchExact = value;
			}
		}

		public object Tag
		{
			get
			{
				return tagItem;
			}
			set
			{
				tagItem = value;
			}
		}

		public int UniverseNumber
		{
			get
			{
				return universeNumber;
			}
			set
			{
				universeNumber = value;
			}
		}
		public int DMXAddress
		{
			get
			{
				return dmxAddress;
			}
			set
			{
				dmxAddress = value;
			}
		}
		public int xLightsAddress
		{
			get
			{
				return xLights_Address;
			}
			set
			{
				xLights_Address = value;
			}
		}

	}

	/// <summary>
	/// Model: StringType=3 Channel RGB
	/// </summary>
	public class xRGBModel : xMember, IComparable<xMember>
	{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		private int universeNumber = 0;
		private int dmxAddress = 0;
		private int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;

		public xRGBModel(string theName)
		{
			myName = theName;
		}

		public xRGBModel(string theName, xModelTypes theType, int theAddress)
		{
			myName = theName;
			myType = theType;
			xLightsAddress = theAddress;
		}


		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
		}

		public xModelTypes xModelType
		{
			get
			{
				return myType;
			}
			set
			{
				myType = value;
			}
		}

		public int CompareTo(xMember otherObj)
		{
			return Name.CompareTo(otherObj.Name);
		}

		public string ToString()
		{
			return myName;
		}

		public xMemberType xMemberType
		{
			get
			{
				return xMemberType.RGBmodel;
			}
		}

		public bool Selected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
			}
		}

		public bool MatchExact
		{
			get
			{
				return matchExact;
			}
			set
			{
				matchExact = value;
			}
		}

		public object Tag
		{
			get
			{
				return tagItem;
			}
			set
			{
				tagItem = value;
			}
		}
		public int UniverseNumber
		{
			get
			{
				return universeNumber;
			}
			set
			{
				universeNumber = value;
			}
		}
		public int DMXAddress
		{
			get
			{
				return dmxAddress;
			}
			set
			{
				dmxAddress = value;
			}
		}
		public int xLightsAddress
		{
			get
			{
				return xLights_Address;
			}
			set
			{
				xLights_Address = value;
			}
		}


	}

	/// <summary>
	/// Model: StringType = RGB Nodes
	/// </summary>
	public class xPixels : xMember, IComparable<xMember>
	{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		private int universeNumber = 0;
		private int dmxAddress = 0;
		private int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;


		public xPixels(string theName)
	{
		myName = theName;
	}

		public xPixels(string theName, xModelTypes theType, int theAddress)
		{
			myName = theName;
			myType = theType;
			xLightsAddress = theAddress;
		}

		public string Name
	{
		get
		{
			return myName;
		}
	}

	public void ChangeName(string newName)
	{
		myName = newName;
	}

		public xModelTypes xModelType
		{
			get
			{
				return myType;
			}
			set
			{
				myType = value;
			}
		}

		public int CompareTo(xMember otherObj)
		{
		return Name.CompareTo(otherObj.Name);
	}

	public string ToString()
	{
		return myName;
	}

	public xMemberType xMemberType
	{
		get
		{
				return xMemberType.Pixels;
		}
	}

		public bool Selected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
			}
		}

		public bool MatchExact
		{
			get
			{
				return matchExact;
			}
			set
			{
				matchExact = value;
			}
		}

		public object Tag
		{
			get
			{
				return tagItem;
			}
			set
			{
				tagItem = value;
			}
		}

		public int UniverseNumber
		{
			get
			{
				return universeNumber;
			}
			set
			{
				universeNumber = value;
			}
		}
		public int DMXAddress
		{
			get
			{
				return dmxAddress;
			}
			set
			{
				dmxAddress = value;
			}
		}
		public int xLightsAddress
		{
			get
			{
				return xLights_Address;
			}
			set
			{
				xLights_Address = value;
			}
		}

	}

	/// <summary>
	/// Model Group
	/// </summary>
	public class xModelGroup : xMember, IComparable<xMember>
{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		public List<xMember> xMembers = new List<xMember>();
		private int universeNumber = 0;
		private int dmxAddress = 0;
		private int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;

		public xModelGroup(string theName)
		{
			myName = theName;
		}

		public xModelGroup(string theName, xModelTypes theType, int theAddress)
		{
			myName = theName;
			myType = theType;
			xLightsAddress = theAddress;
		}

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
		}

		public xModelTypes xModelType
		{
			get
			{
				return myType;
			}
			set
			{
				myType = value;
			}
		}

		public int CompareTo(xMember otherObj)
		{
			return Name.CompareTo(otherObj.Name);
		}

		public string ToString()
		{
			return myName;
		}

		public xMemberType xMemberType
		{
			get
			{
				return xMemberType.ModelGroup;
			}
		}

		public bool Selected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
			}
		}

		public bool MatchExact
		{
			get
			{
				return matchExact;
			}
			set
			{
				matchExact = value;
			}
		}

		public object Tag
		{
			get
			{
				return tagItem;
			}
			set
			{
				tagItem = value;
			}
		}

		public int UniverseNumber
		{
			get
			{
				return universeNumber;
			}
			set
			{
				universeNumber = value;
			}
		}
		public int DMXAddress
		{
			get
			{
				return dmxAddress;
			}
			set
			{
				dmxAddress = value;
			}
		}
		public int xLightsAddress
		{
			get
			{
				int xla = 9999999;
				for (int m=0; m<xMembers.Count; m++)
				{
					if (xMembers[m].xLightsAddress < xla)
					{
						xla = xMembers[m].xLightsAddress;
					}
				}
				return xla;
			}
			set
			{
				xLights_Address = value;
			}
		}

	}

} // End namespace xUtils
		