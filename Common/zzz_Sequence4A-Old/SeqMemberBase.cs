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
	public class LORMemberBase4 : LORMember4, IComparable<LORMember4>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		protected bool isDirty = false;
		protected bool isExactMatch = false;
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected int myUniverseNumber = lutils.UNDEFINED;
		protected int myDMXAddress = lutils.UNDEFINED;
		
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Channel;
			}
		}

		public int CompareTo(LORMember4 other)
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
							LORChannel4 ch = (LORChannel4)other;
							output.ToString().CompareTo(ch.output.ToString());
						}
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false);
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
			color = lutils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			output.Parse(lineIn);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LORChannel4 chan = (LORChannel4)this.Clone(myName);
			return chan;
		}

		public LORMember4 Clone(string newName)
		{
			LORChannel4 chan = new LORChannel4(newName, lutils.UNDEFINED);
			chan.myCentiseconds = myCentiseconds;
			chan.myIndex = myIndex;
			chan.mySavedIndex = mySavedIndex;
			chan.myAltSavedIndex = myAltSavedIndex;
			chan.imSelected = imSelected;
			chan.color = color;
			chan.output = output.Clone();
			chan.rgbChild = this.rgbChild;
			chan.rgbParent = rgbParent; // should be changed/overridden
			chan.effects = CloneEffects();
			return chan;
		}

		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public LORMember4 MapTo
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
		{ get { return matchExact; } set { matchExact = value; } }
		public int UniverseNumber
		{
			get
			{
				return output.network;
			}
		}
		public int DMXAddress
		{
			get
			{
				return output.channel;
			}
		}

	} // End class LORMemberBase4
