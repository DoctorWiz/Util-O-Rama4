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
		protected object myTag = null; // General Purpose Object
		protected object myNodes = null;
		
		// Note: Mapped to is used by Map-O-Rama and possibly by other utils in the future
		// Only holds a single member so only works for master-to-source mapping
		// source-to-master mapping may include multiple members, and is stored in a List<iLORmember4> stored in the Tag property
		protected iLORMember4 mappedTo = null;

		protected int myUniverseNumber = lutils.UNDEFINED;
		protected int myDMXAddress = lutils.UNDEFINED;
		protected string myComment = ""; // Not really a comment somuch as a general purpose temporary string.
		protected int miscNumber = 0; // General purpose temporary integer.  Use varies according to utility and function.

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
		{	get	{	return myName; } }
		// Note: Name property does not have a 'set'.  Uses ChangeName() instead-- because this property is
		// usually only set once and not usually changed thereafter.
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
		{	get	{	return myIndex;	} }
		// Note: Index property does not have a 'set'.  Uses SetIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
			//MakeDirty(true);
		}

		public int SavedIndex
		{	get	{	return mySavedIndex; } }
		// Note: SavedIndex property does not have a 'set'.  Uses SetSavedIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void SetSavedIndex(int theSavedIndex)
		{
			mySavedIndex = theSavedIndex;
			MakeDirty(true);
		}

		public int AltSavedIndex
		{	get	{	return myAltSavedIndex;	}	set	{	myAltSavedIndex = value; } }

		public iLORMember4 Parent
		{	get	{	return myParent; } }
		// Note: Parent property does not have a 'set'.  Uses SetParent() instead-- because this property is
		// usually only set once and not usually changed thereafter
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
		{	get	{	return imSelected; } set { imSelected = value; } }

		public bool Dirty
		{	get	{	return isDirty;	}	}
		// Note: Dirty flag is read-only.  Uses MakeDirty() instead-- to set it
		// and optionally to clear it.
		public void MakeDirty(bool dirtyState = true)
		{
			isDirty = dirtyState;
			if (dirtyState)
			{
				if (myParent != null)
				{
					if (!myParent.Dirty)
					{
						myParent.MakeDirty(true);
					}
				}
			}
		}

		// This property is included here to be part of the base interface
		// But every subclass should override it and return their own value
		public LORMemberType4 MemberType
		{	get	{	return LORMemberType4.None;	}	}

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

		// This function is included here to be part of the base interface
		// But every subclass should override it and return their own value
		public string LineOut()
		{
			return "";
		}

		// The 'Name' property is the default return value for ToString()
		// But subclasses may override it, for sorting, or other reasons
		public override string ToString()
		{
			return myName;
		}

		// This function is included here to be a skeleton for the base interface
		// But every subclass should override it and return their own value
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
				// Sneaky trick: Uses AltSavedIndex to tell if it has been renumbered and thus written
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
			mbr.myNodes = myNodes;
			mbr.mappedTo = mappedTo;
			mbr.isDirty = isDirty;
			mbr.myParent = myParent;
			mbr.isExactMatch = isExactMatch;
			mbr.myUniverseNumber = myUniverseNumber;
			mbr.myDMXAddress = myDMXAddress;
			mbr.myComment = myComment;
			return mbr;
		}

		public object Tag
		{ get { return myTag; } set { myTag = value; } }

		// Intended to normally hold a List<TreeNodeAdv> (List of SyncFusion TreeView Nodes)
		// But not specifically typed here so as not to require SyncFusion if not used
		public object Nodes
		{ get { return myTag; } set { myTag = value; } }

		public iLORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				// Hmmmmmmm, do I really want to enforce this??
				if (value.MemberType == this.MemberType)
				{
					mappedTo = value;
				}
				else
				{
					string msg = "Why are you trying to map a " + LORSeqEnums4.MemberName(value.MemberType);
					msg += " a " + LORSeqEnums4.MemberName(this.MemberType) + " ?!?";
					//Fyle.BUG(msg);
					Debug.WriteLine(msg);
					Fyle.MakeNoise(Fyle.Noises.Pop);
					// Now that I've been warned, go ahead and do it anyway.
					// (Unless I tell the debugger to step over this next line...)
					//mappedTo = value;
				}
			}
		}

		public bool ExactMatch
		{ get { return isExactMatch; } set { isExactMatch = value; } }
		
		// Properties are read-only and overridden by subclasses to pull the correct values from the appropriate
		// locations.  Included in base class only as a placeholder.  (No way to set them in base class)
		public int UniverseNumber
		{	get	{	return myUniverseNumber; } }
		public int DMXAddress
		{	get	{	return myDMXAddress; } }

		// Not supported by ShowTime, and not saved along with the sequence file.  Included only for temporary use in Util-O-Rama
		public string Comment
		{ get { return myComment; } set { myComment = value; } }
		public int RuleID
		{ get { return miscNumber; } set { miscNumber = value; } }

	}// End class LORMemberBase4
}
