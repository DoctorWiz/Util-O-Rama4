using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LOR4Utils
{
	public class LOR4MemberBase : iLOR4Member, IComparable<iLOR4Member>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected iLOR4Member myParent = null;
		protected bool imSelected = false;
		protected bool isDirty = false;
		protected bool isExactMatch = false;
		protected object myTag = null;
		protected iLOR4Member mappedTo = null;
		protected int myUniverseNumber = lutils.UNDEFINED;
		protected int myDMXAddress = lutils.UNDEFINED;

		public LOR4MemberBase()
		{ }

		public LOR4MemberBase(string theName)
		{
			myName = theName;
		}

		public LOR4MemberBase(string theName, int savedIndex)
		{
			myName = theName;
			mySavedIndex = savedIndex;
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
			MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > 360000)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < 950)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.isWiz)
							{
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							if (myParent != null)
							{
								if (myParent.Centiseconds < value)
								{
									myParent.Centiseconds = value;
								}
							}
							MakeDirty(true);
						}
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
			//MakeDirty(true);
		}

		public int SavedIndex
		{
			get
			{
				return mySavedIndex;
			}
		}

		public void SetSavedIndex(int theSavedIndex)
		{
			mySavedIndex = theSavedIndex;
			MakeDirty(true);
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public iLOR4Member Parent
		{
			get
			{
				return myParent;
			}
		}

		public void SetParent(iLOR4Member newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LOR4Sequence)))
				{
					myParent = (LOR4Sequence)newParent;
				}
				else
				{
					if (t.Equals(typeof(LOR4Visualization)))
					{
						myParent = (LOR4Visualization)newParent;
					}
					else
					{
						if (Fyle.isWiz)
						{
							// Why are we trying to assign something other than a sequence?!?!
							System.Diagnostics.Debugger.Break();
						}
					}
				}
			}
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public bool Dirty
		{
			get
			{
				return isDirty;
			}
		}

		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (myParent != null)
				{
					myParent.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}

		public LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.Channel;
			}
		}

		public int CompareTo(iLOR4Member other)
		{
			int result = 0;
			//if (parentSequence.Members.sortMode == LOR4Membership.SORTbySavedIndex)
			if (LOR4Membership.sortMode == LOR4Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LOR4Membership.sortMode == LOR4Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LOR4Membership.sortMode == LOR4Membership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LOR4Membership.sortMode == LOR4Membership.SORTbyOutput)
						{
							result = UniverseNumber.CompareTo(other.UniverseNumber);
							if (result == 0)
							{
								result = DMXAddress.CompareTo(other.DMXAddress);
							}
						}
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return "";
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			//LOR4Sequence Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			//if (myParent != null) myParent.MakeDirty(true);
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public iLOR4Member Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LOR4Channel chan = (LOR4Channel)this.Clone(myName);
			return chan;
		}

		public iLOR4Member Clone(string newName)
		{
			LOR4MemberBase mbr = new LOR4MemberBase(newName, lutils.UNDEFINED);
			mbr.myCentiseconds = myCentiseconds;
			mbr.myIndex = myIndex;
			mbr.mySavedIndex = mySavedIndex;
			mbr.myAltSavedIndex = myAltSavedIndex;
			mbr.imSelected = imSelected;
			mbr.Tag = myTag;
			mbr.mappedTo = mappedTo;
			mbr.isDirty = isDirty;
			mbr.myParent = myParent;
			mbr.isExactMatch = isExactMatch;
			mbr.myUniverseNumber = myUniverseNumber;
			mbr.myDMXAddress = myDMXAddress;
			return mbr;
		}

		public object Tag
		{
			get
			{
				return myTag;
			}
			set
			{
				myTag = value;
			}
		}

		public iLOR4Member MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LOR4MemberType.Channel)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return isExactMatch; } set { isExactMatch = value; } }
		public int UniverseNumber
		{
			get
			{
				return myUniverseNumber;
			}
		}
		public int DMXAddress
		{
			get
			{
				return myDMXAddress;
			}
		}

	} // End class LOR4MemberBase
}
