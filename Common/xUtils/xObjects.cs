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


	} // End Interface xMember



/// <summary>
/// Model: StringType = Single Color
/// </summary>
	public class Model : xMember, IComparable<xMember>
	{
		private string myName = "";
		private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		

		public Model(string theName)
		{
			myName = theName;
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

	}

	/// <summary>
	/// Model: StringType=3 Channel RGB
	/// </summary>
	public class RGBmodel : xMember, IComparable<xMember>
	{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;

		public RGBmodel(string theName)
		{
			myName = theName;
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

	}

	/// <summary>
	/// Model: StringType = RGB Nodes
	/// </summary>
	public class Pixels : xMember, IComparable<xMember>
	{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;


		public Pixels(string theName)
	{
		myName = theName;
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

	}

	/// <summary>
	/// Model Group
	/// </summary>
	public class ModelGroup : xMember, IComparable<xMember>
{
		private string myName = "";
		//private Color myColor = Color.Black;
		private bool isSelected = false;
		private bool matchExact = false;
		private object tagItem = null;
		public List<xMember> xMembers = new List<xMember>();

		public ModelGroup(string theName)
		{
			myName = theName;
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

	}

} // End namespace xUtils
		