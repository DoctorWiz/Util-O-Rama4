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

		bool ExactMatch
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

	public class xSequence
	{
		public List<xModel> AllModels = xModel.AllModels;
		public List<xPixels> AllPixels = xPixels.AllPixels;
		public List<xModelGroup> AllModelGroups = xModelGroup.AllModelGroups;
		public List<xRGBModel> AllRGBModels = xRGBModel.AllRGBModels;
		public string FileName = "";
	}

/// <summary>
/// Model: StringType = Single Color
/// </summary>
	public class xModel : xMember, IComparable<xMember>
	{
		protected string myName = "";
		protected Color myColor = Color.Black;
		protected bool isSelected = false;
		protected bool matchExact = false;
		protected object tagItem = null;
		protected int universeNumber = 0;
		protected int myDMXAddress = 0;
		public string StartChannel = "";
		protected int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;
		public static List<xModel> AllModels = new List<xModel>();

		public xModel(string theName)
		{
			myName = theName;
		}

		public xModel(string theName, xModelTypes theType, int theAddress)
		{
			myName = theName;
			myType = theType;
			xLights_Address = theAddress;
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

		public bool ExactMatch
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
				return myDMXAddress;
			}
			set
			{
				myDMXAddress = value;
			}
		}
		public int xLightsAddress
		{
			get
			{
				if (xLights_Address < 1)
				{
					if (StartChannel.Length > 0)
					{
						int a = xutils.GetAddress(StartChannel, AllModels);
						if (a != xLights_Address)
						{
							xLights_Address = a;
						}
					}
				}
				
				
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
		protected string myName = "";
		//protected Color myColor = Color.Black;
		protected bool isSelected = false;
		protected bool matchExact = false;
		protected object tagItem = null;
		protected int universeNumber = 0;
		protected int myDMXAddress = 0;
		protected int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;
		public static List<xRGBModel> AllRGBModels = new List<xRGBModel>();

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

		public bool ExactMatch
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
				return myDMXAddress;
			}
			set
			{
				myDMXAddress = value;
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
		protected string myName = "";
		//protected Color myColor = Color.Black;
		protected bool isSelected = false;
		protected bool matchExact = false;
		protected object tagItem = null;
		protected int universeNumber = 0;
		protected int myDMXAddress = 0;
		protected int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;
		public static List<xPixels> AllPixels = new List<xPixels>();


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

		public bool ExactMatch
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
				return myDMXAddress;
			}
			set
			{
				myDMXAddress = value;
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
		protected string myName = "";
		//protected Color myColor = Color.Black;
		protected bool isSelected = false;
		protected bool matchExact = false;
		protected object tagItem = null;
		public List<xMember> xMembers = new List<xMember>();
		protected int universeNumber = 0;
		protected int myDMXAddress = 0;
		protected int xLights_Address = 0;
		private xModelTypes myType = xModelTypes.Unknown;
		public static List<xModelGroup> AllModelGroups = new List<xModelGroup>();

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

		public bool ExactMatch
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
				return myDMXAddress;
			}
			set
			{
				myDMXAddress = value;
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
		