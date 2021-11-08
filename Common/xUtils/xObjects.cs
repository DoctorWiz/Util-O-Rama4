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
		void ChangeName(string newName);

  		int CompareTo(xMember otherObj);

		string ToString();

		xMemberType xMemberType
		{	get; }

		bool Selected
		{	get; set; }

		bool ExactMatch
		{ get; set; }

		object Tag
		{ get; set; }

		int xLightsAddress
		{ get; set; }

		string StartChannel
		{ get; set; }

		Color Color
		{ get; set; }

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
		private int xaddress = -1;
		private string startChannel = "";
		

		public xModel(string theName)
		{
			myName = theName;
		}

		public string Name
		{
			get	{	return myName;}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
		}

		public int CompareTo(xMember otherObj)
		{
			return Name.CompareTo(otherObj.Name);
		}

		public Color Color
		{
			get	{	return myColor;	}
			set	{	myColor = value;}
		}

		public string ToString()
		{
			return myName;
		}

		public xMemberType xMemberType
		{
			get	{ return xMemberType.Model;}
		}

		public bool Selected
		{
			get	{	return isSelected;}
			set	{	isSelected = value;	}
		}

		public bool ExactMatch
		{
			get	{	return matchExact;}
			set	{	matchExact = value;	}
		}

		public object Tag
		{
			get	{	return tagItem;	}
			set	{	tagItem = value;}
		}

		public int xLightsAddress
		{
			get	{	return xaddress;}
			set	{	xaddress = value;	}
		}

		public string StartChannel
		{
			get { return startChannel; }
			set { startChannel = value; }
		}

	}


	/// <summary>
	/// Model: StringType=3 Channel RGB
	/// </summary>
	public class xRGBmodel : xMember, IComparable<xMember>
	{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		private int xaddress = -1;
		private string startChannel = "";

		public xRGBmodel(string theName)
		{
			myName = theName;
		}

		public string Name
		{
			get	{	return myName;}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
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
			get	{	return xMemberType.RGBmodel;}
		}

		public bool Selected
		{
			get { return isSelected; }
			set { isSelected = value; }
		}

		public Color Color
		{
			get { return LORUtils4.lutils.Color_LORtoNet( LORUtils4.lutils.LORCOLOR_RGB); }
			set { Color ignore = value; }
		}
		public bool ExactMatch
		{
			get { return matchExact; }
			set { matchExact = value; }
		}

		public object Tag
		{
			get { return tagItem; }
			set { tagItem = value; }
		}

		public int xLightsAddress
		{
			get { return xaddress; }
			set { xaddress = value; }
		}

		public string StartChannel
		{
			get { return startChannel; }
			set { startChannel = value; }
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
		private int xaddress = -1;
		private string startChannel = "";

		public xPixels(string theName)
	{
		myName = theName;
	}

	public string Name
	{
		get	{	return myName;}
	}

	public void ChangeName(string newName)
	{
		myName = newName;
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
		get	{	return xMemberType.Pixels;}
	}

		public bool Selected
		{
			get { return isSelected; }
			set { isSelected = value; }
		}

		public bool ExactMatch
		{
			get { return matchExact; }
			set { matchExact = value; }
		}

		public object Tag
		{
			get { return tagItem; }
			set { tagItem = value; }
		}

		public int xLightsAddress
		{
			get { return xaddress; }
			set { xaddress = value; }
		}

		public string StartChannel
		{
			get { return startChannel; }
			set { startChannel = value; }
		}

		public Color Color
		{
			get { return LORUtils4.lutils.Color_LORtoNet(LORUtils4.lutils.LORCOLOR_RGB); }
			set { Color ignore = value; }
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

		public xModelGroup(string theName)
		{
			myName = theName;
		}

		public string Name
		{
			get	{	return myName;}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
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
			get	{	return xMemberType.ModelGroup;}
		}

		public bool Selected
		{
			get { return isSelected; }
			set { isSelected = value; }
		}

		public bool ExactMatch
		{
			get { return matchExact; }
			set { matchExact = value; }
		}

		public object Tag
		{
			get { return tagItem; }
			set { tagItem = value; }
		}

		public int xLightsAddress
		{
			get
			{
				int xadddress = -1;
				if (xMembers != null)
				{
					if (xMembers.Count > 0)
					{
						xadddress = xMembers[0].xLightsAddress;
					}
				}
				return xadddress;
			}
			set
			{
				// Throw away
			}
		}

		public string StartChannel
		{
			get
			{
				string startCh = "";
				if (xMembers != null)
				{
					if (xMembers.Count > 0)
					{
						startCh = xMembers[0].StartChannel;
					}
				}
				return startCh;
			}
			set
			{
				// Throw away
			}
		}

		public Color Color
		{
			get
			{
				Color clr = Color.Black;
				if (xMembers != null)
				{
					if (xMembers.Count > 0)
					{
						clr = xMembers[0].Color;
					}
				}
				return clr;
			}
			set
			{
				// Throw away
			}
		}

	}

} // End namespace xUtils
		