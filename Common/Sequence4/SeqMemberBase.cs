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
	public class LORMemberBase4 : iLOR4Member, IComparable<iLOR4Member>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int myID = lutils.UNDEFINED;
		protected int myAltID = lutils.UNDEFINED;
		protected int mycolor = 0; // Note: LOR color, 12-bit int in BGR order
															 // Do not confuse with .Net or HTML color, 16 bits in ARGB order
		protected iLOR4Member myParent = null;
		protected bool imSelected = false;
		protected bool isDirty = false;
		protected bool isExactMatch = false;
		protected object myTag = null; // General Purpose Object
																	 // Note: Mapped to is used by Map-O-Rama and possibly by other utils in the future
																	 // Only holds a single member so only works for destination to source mapping
																	 // source-to-destination mapping may include multiple members, and is stored in a List<iLOR4Member> stored in the Tag property
		protected iLOR4Member mappedTo = null;

		// Holds a List<TreeNodeAdv> but is not defined that way, so that this base member is NOT dependant on
		// SyncFusion's TreeViewAdv in projects that don't use it.
		protected object myNodes = null;
		protected int myUniverseNumber = lutils.UNDEFINED;
		protected int myDMXAddress = lutils.UNDEFINED;
		protected string myComment = ""; // Not really a comment somuch as a general purpose temporary string.
		protected int miscNumber = 0; // General purpose temporary integer.  Use varies according to utility and function.

		public LORMemberBase4()
		// Necessary to be the base member of other members
		// Should never be called directly!
		{ }

		public LORMemberBase4(iLOR4Member theParent, string lineIn)
		{
			myParent = theParent;
			Parse(lineIn);
		}

		public string Name
		{ get { return myName; } }
		// Note: Name property does not have a 'set'.  Uses ChangeName() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void ChangeName(string newName)
		{
			myName = newName;
			MakeDirty(true);
		}

		public virtual int Centiseconds
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
		{ get { return myIndex; } }
		// Note: Index property does not have a 'set'.  Uses SetIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
			//MakeDirty(true);
		}

		public int ID
		{ get { return myID; } }
		// Note: SavedIndex property does not have a 'set'.  Uses SetSavedIndex() instead-- because this property is
		// usually only set once and not usually changed thereafter.
		public void SetID(int newID)
		{
			myID = newID;
			//MakeDirty(true);
		}

		public int AltID
		{ get { return myAltID; } set { myAltID = value; } }

		public int SavedIndex
		{ get { return myID; } }
		public void SetSavedIndex(int newSavedIndex)
		{ myID = newSavedIndex; }

		public int AltSavedIndex
		{ get { return myAltID; } set { myAltID = value; } }

		// Important difference-- color with lower case c is LOR color, 12-bit int in BGR order
		public virtual int color
		{ get { return mycolor; } set { mycolor = value; } }
		// Whereas Color property with capital C returns the .Net or HTML color in ARGB order
		public virtual Color Color
		{ get { return lutils.Color_LORtoNet(mycolor); } set { mycolor = lutils.Color_NettoLOR(value); } }


		public virtual iLOR4Member Parent
		{ get { return myParent; } }
		// Note: Parent property does not have a 'set'.  Uses SetParent() instead-- because this property is
		// usually only set once and not usually changed thereafter
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
		{ get { return imSelected; } set { imSelected = value; } }

		public bool Dirty
		{ get { return isDirty; } }
		// Note: Dirty flag is read-only.  Uses MakeDirty() instead-- to set it
		// and optionally to clear it.
		public virtual void MakeDirty(bool dirtyState = true)
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
		public virtual LOR4MemberType MemberType
		{ get { return LOR4MemberType.None; } }

		public virtual int CompareTo(iLOR4Member other)
		{
			int result = 0;
			//if (parentSequence.Members.sortMode == LOR4Membership.SORTbySavedIndex)
			if (other == null)
			{
				result = 1;
				//string msg = "Why are we comparing " + this.Name + " to null?";
				//msg+= "\r\nClick Cancel, step thru code, check call stack!";
				//Fyle.BUG(msg);

				//! TODO: Find out why we are getting null members in visualizations
				//! This is an ugly kludgy fix in the meantime
				LORMemberBase4 bass = new LORMemberBase4(this, "WTF");
				other = bass;
			}
			else
			{
				if (LOR4Membership.sortMode == LOR4Membership.SORTbyID)
				{
					result = myID.CompareTo(other.ID);
				}
				else
				{
					if (LOR4Membership.sortMode == LOR4Membership.SORTbyName)
					{
						result = myName.CompareTo(other.Name);
					}
					else
					{
						if (LOR4Membership.sortMode == LOR4Membership.SORTbyAltID)
						{
							result = myID.CompareTo(other.AltID);
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
			}
			return result;
		}

		// This function is included here to be part of the base interface
		// But every subclass should override it and return their own value
		public virtual string LineOut()
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
		public virtual void Parse(string lineIn)
		{
			int nu = lineIn.IndexOf(" Name=\"");
			int nl = lineIn.IndexOf(" name=\"");
			if ((nu + nl) > 0)
			{
				//LOR4Sequence Parent = ID.Parent;
				myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
				if (myName.Length == 0) myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, " Name"));
				myID = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
				myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			}
			else
			{
				myName = lineIn;
			}
			//if (myParent != null) myParent.MakeDirty(true);
		}

		public bool Written
		{
			get
			{
				// Sneaky trick: Uses AltSavedIndex to tell if it has been renumbered and thus written
				bool r = false;
				if (myAltID > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public virtual iLOR4Member Clone()
		{
			return this.Clone(myName);
		}

		public virtual iLOR4Member Clone(string newName)
		{
			LORMemberBase4 mbr = new LORMemberBase4(myParent, newName);
			mbr.myCentiseconds = myCentiseconds;
			mbr.myIndex = myIndex;
			mbr.myID = myID;
			mbr.myAltID = myAltID;
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
		{ get { return myNodes; } set { myNodes = value; } }

		public iLOR4Member MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value != null)
				{
					// Hmmmmmmm, do I really want to enforce this??
					if (value.MemberType == this.MemberType)
					{
						mappedTo = value;
					}
					else
					{
						string msg = "Why are you trying to map a " + LOR4SeqEnums.MemberName(value.MemberType);
						msg += " a " + LOR4SeqEnums.MemberName(this.MemberType) + " ?!?";
						//Fyle.BUG(msg);
						Debug.WriteLine(msg);
						Fyle.MakeNoise(Fyle.Noises.Pop);
						// Now that I've been warned, go ahead and do it anyway.
						// (Unless I tell the debugger to step over this next line...)
						//mappedTo = value;
					} // Type Match
				} // Not Null
			}
		}

		public bool ExactMatch
		{ get { return isExactMatch; } set { isExactMatch = value; } }

		// Properties are read-only and overridden by subclasses to pull the correct values from the appropriate
		// locations.  Included in base class only as a placeholder.  (No way to set them in base class)
		public virtual int UniverseNumber
		{ get { return myUniverseNumber; } }
		public virtual int DMXAddress
		{ get { return myDMXAddress; } }

		// Not supported by ShowTime, and not saved along with the sequence file.  Included only for temporary use in Util-O-Rama
		public string Comment
		{ get { return myComment; } set { myComment = value; } }
		public int ZCount
		{ get { return miscNumber; } set { miscNumber = value; } }

	}// End class LORMemberBase4
}
