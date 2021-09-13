using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using FileHelper;

namespace LORUtils4
{
	public class LORVizItemGroup4 : LORMember4, IComparable<LORMember4>
	// An "Item" in a Visualization file is basically a group!
	{
		protected string myName = "";
		//private int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int VizID = lutils.UNDEFINED;
		protected int myAltVizID = lutils.UNDEFINED;
		protected LORVisualization4 parentVisualization = null;
		protected bool imSelected = false;
		protected bool isDirty = false;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		public bool Locked = false;
		public string Comment = "";
		public int[] AssignedObjectsNumbers = null;
		public List<LORMember4> AssignedObjects = new List<LORMember4>();

		private static readonly string FIELDLocked = " Locked";
		private static readonly string FIELDComment = " Comment";
		private static readonly string FIELDObjectID = " Object";
		
		// SuperStarStuff
		// Since (for now) I don't have SuperStar, and
		// Since (for now) We are not writing out Visualization files
		//   Ignore and throw away this stuff
		public int SSWU = 0;
		public string SSFF = "";
		public bool SSReverseOrder = false;
		public bool SSForceRowColumn = false;
		public int SSRow = 0;
		public int SSColumn = 0;
		public bool SSUseMyOrder = false;
		public bool SSStar = false;
		public int SSMatrixInd = 0;
		public int SSPropColorTemp = 0;
		public static readonly string FIELD_SSWU = " SSWU";
		public static readonly string FIELD_SSFF = " SSFF";
		public static readonly string FIELD_SSReverseOrder = " SSReverseOrder";
		public static readonly string FIELD_SSForceRowColumn = " SSForceRowColumn";
		public static readonly string FIELD_SSRow = " SSRow";
		public static readonly string FIELD_SSColumn = " SSColumn";
		public static readonly string FIELD_SSUseMyOrder = " SSUseMyOrder";
		public static readonly string FIELD_SSStar = " SSStar";
		public static readonly string FIELD_SSMatrixInd = " SSMatrixInd";
		public static readonly string FIELD_SSPropColorTemp = " SSPropColorTemp";
		// So when I get ready to start writing out Visualization Files, I probably won't yet be supporting
		// SuperStar since it is very expensive!  So I can just write out all these defaults in one go.
		public static readonly string FIELDS_SS_DEFAULTS = " SSWU=\"0\" SSFF=\"\" SSReverseOrder=\"False\" SSForceRowColumn=\"False\" SSRow=\"0\" SSColumn=\"0\" SSUseMyOrder=\"False\" SSStar=\"False\" SSMatrixInd=\"0\" SSPropColorTemp=\"0\"";

		public LORVizItemGroup4()
		{ }

		public LORVizItemGroup4(string lineIn)
		{
			Parse(lineIn);
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
			if (parentVisualization != null) parentVisualization.MakeDirty(true);

		}

		public int Centiseconds
		{
			get
			{
				int cs = 0;
				if (parentVisualization != null)
				{ cs = parentVisualization.Centiseconds; }
				return cs;
			}
			set
			{
				// Do nothing, throw value away, don't need it
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
				return VizID;
			}
		}

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentVisualization != null)
				{
					parentVisualization.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public void SetSavedIndex(int theSavedIndex)
		{
			// Depreciated, at least for now
			System.Diagnostics.Debugger.Break();
			//myVizID = theSavedIndex;
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltVizID;
			}
			set
			{
				myAltVizID = value;
			}
		}

		public object Tag
		{ get { return myTag; } set { myTag = value; } }
		public bool ExactMatch
		{ get { return matchesExactly; } set { matchesExactly = value; }	}
		public LORMember4 MapTo
		{ get { return mappedTo; } set { mappedTo = value; } }
		public int UniverseNumber
		{
			get
			{
				int un = lutils.UNDEFINED;
				if (AssignedObjects.Count > 0)
				{
					un = AssignedObjects[0].UniverseNumber;
				}
				return un;
			}
		}
		public int DMXAddress
		{
			get
			{
				int da = lutils.UNDEFINED;
				if (AssignedObjects.Count > 0)
				{
					da = AssignedObjects[0].DMXAddress;
				}
				return da;
			}
		}
		public LORMember4 Parent
		{
			get
			{
				return parentVisualization;
			}
		}
		public bool Written
		{
			get
			{
				bool wr = false;
				if (AltSavedIndex >= 0) wr = true;
				return wr;
			}
		}
		public void SetParent(LORMember4 newParent)
		{
			// Do Nothing
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
				return LORMemberType4.VizChannel4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = VizID.CompareTo(other.SavedIndex);
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
						result = myAltVizID.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
						{
							if (AssignedObjects != null)
							{
								result = AssignedObjects[0].UniverseNumber.CompareTo(other.UniverseNumber);
								if (result == 0)
								{
									result = AssignedObjects[0].DMXAddress.CompareTo(other.DMXAddress);
								}
							}
						}
					}
				}
			}
			
			return result;
		}
		
		public LORMember4 Clone()
		{
			LORVizItemGroup4 newGrp = new LORVizItemGroup4();
			newGrp.myName = myName;
			newGrp.myIndex = myIndex;
			newGrp.VizID = VizID;
			newGrp.myAltVizID = myAltVizID;
			newGrp.parentVisualization = parentVisualization;
			newGrp.imSelected = imSelected;
			newGrp.isDirty = isDirty;
			newGrp.matchesExactly = matchesExactly;
			newGrp.myTag = myTag;
			newGrp.mappedTo = mappedTo;
			newGrp.Locked = Locked;
			newGrp.Comment = newGrp.Comment;
			newGrp.AssignedObjectsNumbers = AssignedObjectsNumbers;
			//newGrp.AssignedObjects = AssignedObjects;

			// Use/Keep Defaults for SuperStarStuff
			return newGrp;
		}

		public LORMember4 Clone(string newName)
		{
			LORMember4 newGrp = Clone();
			newGrp.ChangeName(newName);
			return newGrp;
		}

		public string LineOut()
		{
			//TODO Add support for writing Visualization files
			return "";
		}

		public void Parse(string lineIn)
		{
			VizID = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizID);
			myName = lutils.getKeyWord(lineIn, lutils.FIELDname);
			Locked = lutils.getKeyState(lineIn, FIELDLocked);
			Comment = lutils.getKeyWord(lineIn, FIELDComment);
		}

		public void ParseAssignedObjectNumbers(StreamReader reader)
		{
			bool keepGoing = true;
			int aoCount = 0;
			while (keepGoing)
			{
				if (reader.EndOfStream) keepGoing = false;
				if (keepGoing)
				{
					string lineIn = reader.ReadLine();
					int iEnd = lineIn.IndexOf("</Item>");
					if (iEnd > 0) keepGoing = false;
					if (keepGoing)
					{
						int o = lutils.getKeyValue(lineIn, FIELDObjectID);
						aoCount++;
						Array.Resize(ref AssignedObjectsNumbers, aoCount);
						AssignedObjectsNumbers[aoCount - 1] = o;
					} // End second KeepGoing test-- not end of <Item>
				} // End first KeepGoing test-- not EndOfStream
			} // End While KeepGoing
		} // End ParseAssignedObjectNumbers



	} // End Class LORVizItemGroup4
} // End Namespace

