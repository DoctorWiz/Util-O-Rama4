using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LORUtils4
{
	public class LORMemberBase4 : iLORMember4, IComparable<iLORMember4>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected iLORMember4 myParent = null;
		protected bool imSelected = false;
		protected bool isDirty = false;
		protected bool isExactMatch = false;
		protected object myTag = null;
		protected iLORMember4 mappedTo = null;
		protected int myUniverseNumber = lutils.UNDEFINED;
		protected int myDMXAddress = lutils.UNDEFINED;

		public LORMemberBase4()
		{ }

		public LORMemberBase4(string theName)
		{
			myName = theName;
		}

		public LORMemberBase4(string theName, int savedIndex)
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
						if (Fyle.DebugMode)
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
							if (Fyle.DebugMode)
							{
								//System.Diagnostics.Debugger.Break();
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

		public iLORMember4 Parent
		{
			get
			{
				return myParent;
			}
		}

		public void SetParent(iLORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					myParent = (LORSequence4)newParent;
				}
				else
				{
					if (t.Equals(typeof(LORVisualization4)))
					{
						myParent = (LORVisualization4)newParent;
					}
					else
					{
						if (Fyle.DebugMode)
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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Channel;
			}
		}

		public int CompareTo(iLORMember4 other)
		{
			int result = 0;
			//if (parentSequence.Members.sortMode == LORMembership4.SORTbySavedIndex)
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
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
			//LORSequence4 Parent = ID.Parent;
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

		public iLORMember4 Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LORChannel4 chan = (LORChannel4)this.Clone(myName);
			return chan;
		}

		public iLORMember4 Clone(string newName)
		{
			LORMemberBase4 mbr = new LORMemberBase4(newName, lutils.UNDEFINED);
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

		public iLORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.Channel)
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

	} // End class LORMemberBase4
}
