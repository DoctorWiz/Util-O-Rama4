using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using FileHelper;

namespace LORUtils4
{
	public class LORMembership4 : IEnumerator, IEnumerable  //  IEnumerator<iLORMember4>
	{
		#region Class-Level Global-Scope variables and objects
		private List<iLORMember4>					myByDisplayOrderList =							new List<iLORMember4>();   // The Main List
		private List<iLORMember4>					myByIDList =		new List<iLORMember4>();
		private List<iLORMember4>					myByAltIDList = new List<iLORMember4>();
		//private List<LORTimings4>					myBySaveIDList =				new List<LORTimings4>();
		//private List<LORTimings4>					myByAltSaveIDList =			new List<LORTimings4>();
		//private List<LORTrack4>						myByTrackIndexList =		new List<LORTrack4>();
		//private List<LORTrack4>						myByAltTrackIndexList = new List<LORTrack4>();
		//private List<LORVizDrawObject4>		myByObjectIDList =			new List<LORVizDrawObject4>();
		//private List<LORVizDrawObject4>		myByAltObjectIDList =		new List<LORVizDrawObject4>();
		//private List<LORVizItemGroup4>		myByItemIDList =				new List<LORVizItemGroup4>();
		//private List<LORVizItemGroup4>		myByAltItemIDList =			new List<LORVizItemGroup4>();
		private SortedDictionary<string, iLORMember4> myByNameDictionary = new SortedDictionary<string, iLORMember4>();

		private int myHighestID = lutils.UNDEFINED;
		private int myHighestAltID = lutils.UNDEFINED;

		// B-cuz item numbers in Visualizations are 1-based (instead of normal 0-based)
		//private int myHighestItemID = 0;
		//public int AltHighestItemID = lutils.UNDEFINED;
		// B-cuz drawobjects in Visualizations do the same thing
		//private int myHighestObjectID = 0;
		//public int AltHighestObjectID = lutils.UNDEFINED;
		//iLORMember4 Parent = null;  // Parent SEQUENCE
		protected iLORMember4 myOwner = null;  // Parent GROUP or TRACK or Sequence or Visualization
		private iLORMember4 myParent = null;


		public const int SORTbyID = 1;
		//public const int SORTbySavedIndex = 1;
		public const int SORTbyAltID = 2;
		//public const int SORTbyAltSavedIndex = 2;
		public const int SORTbyName = 3;
		public const int SORTbyOutput = 4;
		public static int sortMode = SORTbyID;

		private	int myChannelCount = 0;
		private	int myRGBChannelCount = 0;
		private	int myChannelGroupCount = 0;
		private	int myCosmicDeviceCount = 0;
		private	int myTrackCount = 0;
		private	int myTimingGridCount = 0;
		private int myVizChannelCount = 0;
		private int myVizItemGroupCount = 0;
		private int myVizDrawObjectCount = 0;
		//private	int myEverythingCount = 0;
		//private int duplNameFix = 2;

		// For Enumeration
		private int position = 0;

		//iLORMember4 IEnumerator<iLORMember4>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();
		#endregion

		//! CONSTRUCTOR
		public LORMembership4(iLORMember4 theOwner)
		{
			if (theOwner == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				// Reminder: Owner = Member (Track, Group, or Cosmic) that owns this Membership
				//           Parent = Base Sequence or Visualization
				myOwner = theOwner;
				if (myOwner.Parent != null)
				{
					myParent = myOwner.Parent;
				}
			}

			if (theOwner.MemberType == LORMemberType4.Visualization)
			{
				// SavedIndices and SaveIDs in Sequences start at 0. Cool! Great! No Prob!
				// But Channels, Groups, and DrawObjects in Visualizations start at 1 (Grrrrr)
				// So add a dummy object at the [0] start of the lists
				/*
				LORVizChannel4 lvc = new LORVizChannel4(this.Parent, "\0\0DUMMY VIZCHANNEL AT INDEX [0] - DO NOT USE!");
				lvc.SetIndex(0);
				lvc.SetSavedIndex(0);
				lvc.SetParent(myParent);
				myHighestSavedIndex = 0;
				myBySavedIndexList.Add(lvc);
				LORVizDrawObject4 lvdo = new LORVizDrawObject4(this.Parent, "\0\0DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
				lvdo.SetIndex(0);
				lvdo.SetSavedIndex(0);
				lvdo.SetParent(myParent);
				if (myParent != null)
				{
					LORVisualization4 pv = (LORVisualization4)myParent;
					if (pv.VizChannels.Count > 0)
					{
						lvdo.redChannel = pv.VizChannels[0];
					}
				}
				myHighestObjectID = 0;
				myByObjectIDList.Add(lvdo);
				LORVizItemGroup4 lvig = new LORVizItemGroup4(this.Parent, "\0\0DUMMY VIZITEMGROUP AT INDEX [0] - DO NOT USE!");
				lvig.SetIndex(0);
				lvig.SetSavedIndex(0);
				lvig.SetParent(myParent);
				myHighestSavedIndex = 0;
				myBySavedIndexList.Add(lvig);
				*/
			}
			if (theOwner.MemberType == LORMemberType4.VizItemGroup)
			{
				//LORVizDrawObject4 lvdo = new LORVizDrawObject4(this.Parent, "\0\0DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
				//lvdo.SetIndex(0);
				//lvdo.SetSavedIndex(0);
				//lvdo.SetParent(myParent);
				//myHighestItemID = 0;
				//if (myByObjectIDList.Count == 0) myByObjectIDList.Add(null);
				//myByObjectIDList[0] = lvdo;
			}


		}
		public void Clear()
		{
			myByDisplayOrderList = new List<iLORMember4>();   // The Main List
			myByIDList = new List<iLORMember4>();
			myByAltIDList = new List<iLORMember4>();
			//myBySaveIDList = new List<LORTimings4>();
			//myByAltSaveIDList = new List<LORTimings4>();
			//myByTrackIndexList = new List<LORTrack4>();
			//myByAltTrackIndexList = new List<LORTrack4>();
			//myByObjectIDList = new List<LORVizDrawObject4>();
			//myByAltObjectIDList = new List<LORVizDrawObject4>();
			//myByItemIDList = new List<LORVizItemGroup4>();
			//myByAltItemIDList = new List<LORVizItemGroup4>();

			myByNameDictionary = new SortedDictionary<string, iLORMember4>();
			myHighestID = lutils.UNDEFINED;
			myHighestAltID = lutils.UNDEFINED;
			//myHighestSaveID = lutils.UNDEFINED;
			//AltHighestSaveID = lutils.UNDEFINED;
			//myHighestItemID = lutils.UNDEFINED;
			//AltHighestItemID = lutils.UNDEFINED;
			//myHighestObjectID = lutils.UNDEFINED;
			//AltHighestObjectID = lutils.UNDEFINED;
			//myOwner = null;  // Parent GROUP or TRACK
			//myParentSeq = null;
			sortMode = SORTbyID;
			myChannelCount = 0;
			myRGBChannelCount = 0;
			myChannelGroupCount = 0;
			myCosmicDeviceCount = 0;
			myTrackCount = 0;
			myTimingGridCount = 0;
			//myEverythingCount = 0;
			position = 0;
		}
		public List<iLORMember4> Items
		{ get { return myByDisplayOrderList; } }
		public List<iLORMember4> Members
		{ get { return myByDisplayOrderList; } }
		//private List<iLORMember4> Members = null;
		public List<iLORMember4> BySavedIndex
		{ get 
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by SavedIndex?";
						Fyle.BUG(msg);
					}
				}
				return myByIDList;
			} 
		}
		public List<iLORMember4> ByDisplayOrder
		{	get	{	return myByDisplayOrderList;	}	}
		
		public List<iLORMember4> ByAltSavedIndex
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by AltSavedIndex?";
						Fyle.BUG(msg);
					}
				}
				return myByAltIDList;
			}
		}
		public List<iLORMember4> ByItemID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Visualization)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by ItemID?";
						Fyle.BUG(msg);
					}
				}
				return myByIDList;
			}
		}
		public List<iLORMember4> ByAltItemID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Visualization)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by AltItemID?";
						Fyle.BUG(msg);
					}
				}
				return myByAltIDList;
			}
		}
		public List<iLORMember4> BySaveID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by SaveID?";
						Fyle.BUG(msg);
					}
				}
				return myByIDList;
			}
		}
		public List<iLORMember4> ByAltSaveID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by AltSaveID?";
						Fyle.BUG(msg);
					}
				}
				return myByAltIDList;
			}
		}
		public List<iLORMember4> ByObjectID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.VizItemGroup)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by ObjectID?";
						Fyle.BUG(msg);
					}
				}
				return myByIDList;
			}
		}
		public List<iLORMember4> ByAltObjectID
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.VizItemGroup)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by AltObjectID?";
						Fyle.BUG(msg);
					}
				}
				return myByAltIDList;
			}
		}
		public List<iLORMember4> ByTrackIndex
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by TrackIndex?";
						Fyle.BUG(msg);
					}
				}
				return myByIDList;
			}
		}
		public List<iLORMember4> ByAltTrackIndex
		{
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access members by AltTrackIndex?";
						Fyle.BUG(msg);
					}
				}
				return myByAltIDList;
			}
		}
		public SortedDictionary<string, iLORMember4> ByName
		{ get { return myByNameDictionary; } }
		public int HighestSavedIndex
		{ 
			// Used by Sequence Channels, RGB Channels, Channel Groups, and Cosmic Devices
			// Used by Visualization VizChannels
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access HighestSavedIndex?";
						Fyle.BUG(msg);
					}
				}
				return myHighestID;
			}
		}
		public int HighestAltSavedIndex
		{
			// Used by Sequence Channels, RGB Channels, Channel Groups, and Cosmic Devices
			// Used by Visualization VizChannels
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Sequence)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access HighestSavedIndex?";
						Fyle.BUG(msg);
					}
				}
				return myHighestAltID;
			}
			set
			{ myHighestAltID = value; }
		}
		public int HighestItemID
		{
			// Used by Sequence Channels, RGB Channels, Channel Groups, and Cosmic Devices
			// Used by Visualization VizChannels
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.Visualization)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access HighestItemID?";
						Fyle.BUG(msg);
					}
				}
				return myHighestAltID;
			}
		}
		public int HighestDrawObjectID
		{
			// Used by Sequence Channels, RGB Channels, Channel Groups, and Cosmic Devices
			// Used by Visualization VizChannels
			get
			{
				if (Fyle.isWiz)
				{
					if (myOwner.MemberType != LORMemberType4.VizItemGroup)
					{
						string msg = "Why is a " + LORSeqEnums4.MemberName(myOwner.MemberType);
						msg += "Trying to access HighestDrawObjectID?";
						Fyle.BUG(msg);
					}
				}
				return myHighestAltID;
			}
		}
		public int ChannelCount
		{ get { return myChannelCount; } }
		public int RGBChannelCount
		{ get { return myRGBChannelCount; } }
		public int ChannelGroupCount
		{ get { return myChannelGroupCount; } }
		public int CosmicDeviceCount
		{ get { return myCosmicDeviceCount; } }
		public int TrackCount
		{ get { return myTrackCount; } }
		public int TimingGridCount
		{ get { return myTimingGridCount; } }
		public int VizChannelCount
		{ get { return myVizChannelCount; } }
		public int VizItemGroupCount
		{ get { return myVizItemGroupCount; } }
		public int VizDrawObjectCount
		{ get { return myVizDrawObjectCount; } }
		public int AllCount
		{ get { return myByDisplayOrderList.Count; } }
		public iLORMember4 Owner
		{
			get
			{
				return myOwner;
			}
		}
		public void ChangeOwner(iLORMember4 newOwner)
		{
			//! WHY?!?!
			if (Fyle.DebugMode) System.Diagnostics.Debugger.Break();
			myOwner = newOwner;
			myParent = myOwner.Parent;
		}
		// LORMembership4.Add(Member)
		public int Add(iLORMember4 newMember)
		{
			int psi = lutils.UNDEFINED;
			int tc = lutils.UNDEFINED;
			int psv = lutils.UNDEFINED;
			LORMemberType4 parentType = myParent.MemberType;
			LORMemberType4 ownerType = myOwner.MemberType;
			LORMemberType4 newMemberType = newMember.MemberType;
			int newMemberID = newMember.ID;

			if (newMember.ID < 0)
			{
				Fyle.BUG("New Member has no ID yet!");
			}

			// Add to the mYMembers list, no matter who the owner of this membership is
			myByDisplayOrderList.Add(newMember);
			
			// By Name Dictionary
			string itemName = newMember.Name;
			// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
			if (itemName == "")
			{
				// Make up a name based on type and index
				itemName = LORSeqEnums4.MemberName(newMember.MemberType) + " " + newMember.Index.ToString("0000");
			}
			// Check for duplicate names
			while (myByNameDictionary.ContainsKey(itemName))
			{
				// Append a number
				itemName = newMember.Name + " ‹" + myByNameDictionary.Count.ToString() + "›";
			}
			myByNameDictionary.Add(itemName, newMember);
			
			myParent.MakeDirty(true);
			// Is this the new Guiness World Record Highest ID?
			if (newMemberID > myHighestID) myHighestID = newMemberID;

			
			//! ** SEQUENCES **
			// Is the base parent a sequence, and will this contain sequence-type objects?
			if (parentType == LORMemberType4.Sequence)
			{
				// Is new member a 'regular' type that has a SaveIndex?
				if ((newMemberType == LORMemberType4.Channel) ||
						(newMemberType == LORMemberType4.RGBChannel) ||
						(newMemberType == LORMemberType4.ChannelGroup) ||
						(newMemberType == LORMemberType4.Cosmic))
				{
					//myByIDList.Add(newMember);
					//myByAltIDList.Add(newMember);
					if (newMemberType == LORMemberType4.Channel)
					{
						myChannelCount++;
					}
					if (newMemberType == LORMemberType4.RGBChannel)
					{
						myRGBChannelCount++;
					}
					if (newMemberType == LORMemberType4.ChannelGroup)
					{
						myChannelGroupCount++;
					}
					if (newMember.MemberType == LORMemberType4.Cosmic)
					{
						myCosmicDeviceCount++;
					}
				} // End if 'regular' Sequence-Type Member: Channel, RGB, Group, or Cosmic

				// Not a 'regular' member...
				// Is it a track?
				if (newMemberType == LORMemberType4.Track)
				{
					myTrackCount++;
				} // End if a track

				// OK, not a 'regular' member or a track...
				// Is it a timing grid?
				if (newMemberType == LORMemberType4.Timings)
				{
					myTimingGridCount++;
				} // End timing Grid
			} // End base parent is a sequence and newmember is a sequenc-y type thing



			//! ** VISUALIZATIONS **
			// Is the base parent a Visualization?
			if (parentType == LORMemberType4.Visualization)
			{
				// Is it a 'regular' visual thing; a channel, drawobject, or itemgroup?
				if ((newMemberType == LORMemberType4.VizChannel) ||
						(newMemberType == LORMemberType4.VizDrawObject) ||
						(newMemberType == LORMemberType4.VizItemGroup))
				{
					if (newMemberType == LORMemberType4.VizChannel)
					{
						myVizChannelCount++;
					}
					if (newMember.MemberType == LORMemberType4.VizDrawObject)
					{
						myVizDrawObjectCount++;
					}
					if (newMember.MemberType == LORMemberType4.VizItemGroup)
					{
						myVizItemGroupCount++;
					} // End if ItemGroup
				} // End if a 'regular' visualizer channel, drawobject, or itemgroup
			} // end base parent is a visualization

			// Owner may be a sequence, and Members will be just the tracks,
			//                          and AllMembers will be regular items with saved indices
			// For these, we want to be able to fetch the by SavedIndex, AltSavedIndex, or TrackIndex
			// NOTE: do NOT do this if the owner is a track, channelgroup, or vizitemgroup
			if ((ownerType == LORMemberType4.Sequence) ||
				  (ownerType == LORMemberType4.Visualization))
			{
				while ((myByIDList.Count - 1) < newMember.ID)
				{
					myByIDList.Add(null);
					//myByAltIDList.Add(null);
				}
				myByIDList[newMemberID] = newMember;
				while ((myByAltIDList.Count - 1) < newMember.ID)
				{
					//myByIDList.Add(null);
					myByAltIDList.Add(null);
				}
				myByAltIDList[newMemberID] = newMember;
			}





			return newMemberID;
		}
		// For iEnumerable
		public iLORMember4 this[int index]
		{
			get
			{
				if (index < myByDisplayOrderList.Count)
				{
					return myByDisplayOrderList[index];
				}
				else
				{
					return null;
				}
			}
			set
			{
				while (myByDisplayOrderList.Count <= index)
				{
					myByDisplayOrderList.Add(null);
				}
				myByDisplayOrderList[index] = value;
			}
		}
		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}
		public bool Includes(string memberName)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m=0; m< myByDisplayOrderList.Count; m++)
			{
				iLORMember4 member = myByDisplayOrderList[m];
				if (member.Name == memberName)
				{
					found = true;
					m = myByDisplayOrderList.Count; // Exit loop
				}
			}
			return found;
		}
		public bool Includes(int savedIndex)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m = 0; m < myByDisplayOrderList.Count; m++)
			{
				iLORMember4 member = myByDisplayOrderList[m];
				if (member.ID == savedIndex)
				{
					found = true;
					m = myByDisplayOrderList.Count; // Exit loop
				}
			}
			return found;
		}
		public bool MoveNext()
		{
			position++;
			return (position < myByDisplayOrderList.Count);
		}
		public void Reset()
		{ position = -1; }
		public object Current
		{
			get { return myByDisplayOrderList[position]; }
		}
		public int Count
		{
			get
			{
				return myByDisplayOrderList.Count;
			}
		}
		public CheckState CheckState
		{
			get
			{
				// Default is indeterminate
				CheckState cs = CheckState.Indeterminate;
				// How many SELECTED descendants are there?
				//    first parameter is selected=true
				int s = DescendantCount(true, true, false, true);
				// Are NONE of them selected, and my owner unselected also?
				if ((s == 0) &&  !myOwner.Selected)
				{
					// Then we are completely unchecked!
					cs = CheckState.Unchecked;
				}
				else // at least one or more descented is selected
				{
					// How many TOTAL descendants are there (including unselected)
					int d = DescendantCount(false, true, false, true);
					// Does selected = total, and is my owner selected also?
					if ((s==d) && myOwner.Selected)
					{
						// Then we are fully and completely checked!
						cs = CheckState.Checked;
					}
				}
				return cs;
			}
		}
		public int SelectedDescendantCount
		{
			// Besides getting the number of selected members and submembers
			// it also 'cleans up' the selection states

			get
			{
				int count = 0;
				if (myOwner.Selected)
				{
					foreach (iLORMember4 m in myByDisplayOrderList)
					{
						if (m.MemberType == LORMemberType4.Channel)
						{
							if (m.Selected) count++;
						}
						if (m.MemberType == LORMemberType4.RGBChannel)
						{
							if (m.Selected)
							{
								int subCount = 0;
								LORRGBChannel4 r = (LORRGBChannel4)m;
								if (r.redChannel.Selected) subCount++;
								if (r.grnChannel.Selected) subCount++;
								if (r.bluChannel.Selected) subCount++;
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									//subCount++;
									count += subCount;
								}
							}
						}
						if (m.MemberType == LORMemberType4.ChannelGroup)
						{
							if (m.Selected)
							{
								LORChannelGroup4 g = (LORChannelGroup4)m;
								int subCount = g.Members.SelectedDescendantCount;  // Recurse!
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									count += subCount;
								}
							}
						}
						if (m.MemberType == LORMemberType4.Cosmic)
						{
							if (m.Selected)
							{
								LORCosmic4 d = (LORCosmic4)m;
								int subCount = d.Members.SelectedDescendantCount;  // Recurse!
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									count += subCount;
								}
							}
						}
					}
					if (count == 0)
					{
						if (myOwner != null)
						{
							myOwner.Selected = false;
						}
						else
						{
							//this.Owner.Selected = true;
						}
					}
				}
				return count;
			}
		}
		public int HighestObjectID
		{
			// For Visualization DrawObjects only
			get
			{
				return myHighestID;
			}
			set
			{
				myHighestID = value;
			}
		}
		public void ReIndex()
		{
			// Clear previous run

			myChannelCount = 0;
			myRGBChannelCount = 0;
			myChannelGroupCount = 0;
			myTrackCount = 0;
			myTimingGridCount = 0;
			myVizChannelCount = 0;
			myVizDrawObjectCount = 0;
			myVizItemGroupCount = 0;
			//myEverythingCount = 0;

			sortMode = SORTbyID;

			myByNameDictionary = new SortedDictionary<string, iLORMember4>();
			myByIDList = new List<iLORMember4>();
			myByAltIDList = new List<iLORMember4>();

			try
			{
				for (int i = 0; i < myByDisplayOrderList.Count; i++)
				{
					iLORMember4 member = myByDisplayOrderList[i];

					string itemName = member.Name;
					// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
					if (itemName == "")
					{
						// Make up a name based on type and index
						itemName = LORSeqEnums4.MemberName(member.MemberType) + " " + member.Index.ToString("0000");
					}
					// Check for duplicate names
					while (myByNameDictionary.ContainsKey(itemName))
					{
						// Append a number
						itemName = member.Name + " ‹" + myByNameDictionary.Count.ToString() + "›";
					}
					myByNameDictionary.Add(itemName, member);

					switch(member.MemberType)
					{
						case LORMemberType4.Channel:
							myChannelCount++;
							break;
						case LORMemberType4.RGBChannel:
							myRGBChannelCount++;
							break;
						case LORMemberType4.ChannelGroup:
							myChannelGroupCount++;
							break;
						case LORMemberType4.Cosmic:
							myCosmicDeviceCount++;
							break;
						case LORMemberType4.Track:
							myTrackCount++;
							break;
						case LORMemberType4.Timings:
							myTimingGridCount++;
							break;
						case LORMemberType4.VizChannel:
							myVizChannelCount++;
							break;
						case LORMemberType4.VizDrawObject:
							myVizDrawObjectCount++;
							break;
						case LORMemberType4.VizItemGroup:
							myVizItemGroupCount++;
							break;
					}
					// NOTE: do NOT do this if the owner is a track, channelgroup, or vizitemgroup
					if ((myOwner.MemberType == LORMemberType4.Sequence) ||
							(myOwner.MemberType == LORMemberType4.Visualization))
					{
						while ((myByIDList.Count - 1) < member.ID)
						{
							myByIDList.Add(null);
						}
						myByIDList[member.ID] = member;
						if (member.AltID >=0)
						{
							while ((myByAltIDList.Count - 1) < member.AltID)
							{
								myByAltIDList.Add(null);
							}
							myByAltIDList[member.AltID] = member;
						} // End member has valid AltID
					} // End if Owner is Sequence or Visualization
				} // End myMembers Loop
			} // End Try
			catch(Exception ex)
			{
				string msg = "Error Reindexing Membership\r\n\r\n";
				msg += ex.Message;
				Fyle.BUG(msg);
			}

			// Sort 'em all!
			sortMode = SORTbyID;
			//System.Diagnostics.Debugger.Break();
			// Sort is failing, supposedly because array elements are not set (null/empty)
			//  -- but a quick check of 'Locals' doesn't show any empties
			NULLITEMCHECK();
			myByIDList.Sort();
			myByAltIDList.Sort();

			//sortMode = SORTbyName;

			//LORSequence4 mySeq = (LORSequence4)Parent;
			//if (myParentSeq.TimingGrids.Count > 0)
			//{
				//AlphaSortSavedIndexes(byTimingGridName, 0, Parent.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			//}

		} // end ReIndex
		private void NULLITEMCHECK()
		{
			for (int i = 0; i < myByDisplayOrderList.Count; i++)
			{
				if (myByDisplayOrderList[i] == null)
				{
					string newName = "Member " + i.ToString("0000");
					LORMemberBase4 mbr = new LORMemberBase4(myParent, newName);
					mbr.SetID(i);
				}	
				//System.Diagnostics.Debugger.Break();
				if (myByDisplayOrderList[i].Parent == null) System.Diagnostics.Debugger.Break();
				int v = myByDisplayOrderList[i].CompareTo(myByDisplayOrderList[0]);
			}

		}
		// LORMembership4.find(name, type, create)
		public iLORMember4 FindByName(string theName, LORMemberType4 theType, bool createIfNotFound = false)
		{
			//iLORMember4 ret = null;
			#if DEBUG
			string msg = "LORMembership4.find(" + theName + ", ";
			msg += theType.ToString() + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
			#endif


			if (myByNameDictionary.TryGetValue(theName, out iLORMember4 ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != theType)
				{
					ret = null;
				}
			}
			if ((ret == null) && createIfNotFound)
			{
				if (myParent != null)
				{
					if (myParent.MemberType == LORMemberType4.Sequence)
					{
						LORSequence4 parentSeq = (LORSequence4)Parent;
						if (theType == LORMemberType4.Channel)
						{
							ret = parentSeq.CreateNewChannel(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.RGBChannel)
						{
							LORRGBChannel4 grp = parentSeq.CreateNewRGBChannel(theName);

							LORChannel4 ch = parentSeq.CreateNewChannel(theName + " (R)");
							ch.color = lutils.LORCOLOR_RED;
							ch.rgbChild = LORRGBChild4.Red;
							grp.redChannel = ch;
							ch = parentSeq.CreateNewChannel(theName + " (G)");
							ch.color = lutils.LORCOLOR_GRN;
							ch.rgbChild = LORRGBChild4.Green;
							grp.grnChannel = ch;
							ch = parentSeq.CreateNewChannel(theName + " (B)");
							ch.color = lutils.LORCOLOR_BLU;
							ch.rgbChild = LORRGBChild4.Blue;
							grp.bluChannel = ch;


							Add(grp);
							ret = grp;
						}
						if (theType == LORMemberType4.ChannelGroup)
						{
							ret = parentSeq.CreateNewChannelGroup(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Cosmic)
						{
							ret = parentSeq.CreateNewCosmicDevice(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Timings)
						{
							ret = parentSeq.CreateNewTimingGrid(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Track)
						{
							ret = parentSeq.CreateNewTrack(theName);
							Add(ret);
						}
					} // End if parent is sequence

					if (myParent.MemberType == LORMemberType4.Visualization)
					{
						LORVisualization4 parentViz = (LORVisualization4)Parent;
						if (theType == LORMemberType4.VizChannel)
						{
							//ret = parentViz.CreateVizChannel(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.VizDrawObject)
						{
							//ret = parentViz.CreateDrawObject(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.VizItemGroup)
						{
							//ret = parentViz.CreateItemGroup(theName);
							Add(ret);
						} // End if VizItemGroup
					} // End if parent is Visualization
				} // End if parent isn't null
			} // End if find by name returned null and CreateIfNotFound is true
			return ret;
		}
		public LORChannel4 FindChannel(string channelName, bool createIfNotFound = false, bool clearEffects = false)
		{
			LORChannel4 ret = null;
			iLORMember4 member = FindByName(channelName, LORMemberType4.Channel, createIfNotFound);
			if (member != null)
			{
				ret = (LORChannel4)member;
				if (clearEffects)
				{
					ret.effects.Clear();
				}
			}
			else
			{
				LORSequence4 sq = (LORSequence4)myParent;
				ret = sq.CreateNewChannel(channelName);
				this.Add(ret);
			}
			return ret;
		}
		public LORRGBChannel4 FindRGBChannel(string rgbChannelName, bool createIfNotFound = false, bool clearEffects = false)
		{
			LORRGBChannel4 ret = null;
			iLORMember4 member = FindByName(rgbChannelName, LORMemberType4.RGBChannel, createIfNotFound);
			if (member != null)
			{
				if (member.MemberType == LORMemberType4.RGBChannel)
				{
					ret = (LORRGBChannel4)member;
					if (clearEffects)
					{
						ret.redChannel.effects.Clear();
						ret.grnChannel.effects.Clear();
						ret.redChannel.effects.Clear();
					}
				}
			}
			else
			{
				System.Diagnostics.Debugger.Break();
			}

			return ret;
		}
		public LORChannelGroup4 FindChannelGroup(string channelGroupName, bool createIfNotFound = false)
		{
			LORChannelGroup4 ret = null;
			iLORMember4 member = FindByName(channelGroupName, LORMemberType4.ChannelGroup, createIfNotFound);
			if (member != null)
			{
				ret = (LORChannelGroup4)member;
			}
			else
			{
				LORSequence4 seq = (LORSequence4)myParent;
				ret = seq.CreateNewChannelGroup(channelGroupName);
			}
			return ret;
		}
		public LORTrack4 FindTrack(string trackName, bool createIfNotFound = false)
		{
			LORTrack4 ret = null;
			iLORMember4 member = FindByName(trackName, LORMemberType4.Track, createIfNotFound);
			if (member != null)
			{
				ret = (LORTrack4)member;
			}
			return ret;
		}
		public LORVizChannel4 FindVizChannel(string channelName, bool createIfNotFound = false)
		{
			LORVizChannel4 ret = null;
			iLORMember4 member = FindByName(channelName, LORMemberType4.Channel, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizChannel4)member;
			}
			return ret;
		}
		public LORVizDrawObject4 FindVizDrawObject(string drawObjectName, bool createIfNotFound = false)
		{
			LORVizDrawObject4 ret = null;
			iLORMember4 member = FindByName(drawObjectName, LORMemberType4.RGBChannel, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizDrawObject4)member;
			}
			return ret;
		}
		public LORVizItemGroup4 FindVizItemGroup(string itemGroupName, bool createIfNotFound = false)
		{
			LORVizItemGroup4 ret = null;
			iLORMember4 member = FindByName(itemGroupName, LORMemberType4.ChannelGroup, createIfNotFound);
			if (member != null)
			{
				ret = (LORVizItemGroup4)member;
			}
			return ret;
		}
		public iLORMember4 FindBySavedIndex(int theSavedIndex)
		{
			return FindByID(theSavedIndex);
		}
		public iLORMember4 FindByID(int theID)
		{
			iLORMember4 ret = null;
			if ((theID >= 0) && (theID < myByIDList.Count))
			{
				ret = myByIDList[theID];
			}
			return ret;
		}
		/*
		private static iLORMember4 FindByName(string theName, List<iLORMember4> Members)
		{
			iLORMember4 ret = null;
			int idx = BinarySearch(theName, Members);
			if (idx > lutils.UNDEFINED)
			{
				ret= Members[idx];
			}
			return ret;
		}

		private static int BinarySearch(string theName, List<iLORMember4> Members)
		{
			return TreeSearch(theName, Members, 0, Members.Count - 1);
		}

		private static int TreeSearch(string theName, List<iLORMember4> Members, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			string sname = Members[start].Name;
			string ename = Members[end].Name;
			string mname = Members[mid].Name;
			//if ((theName.CompareTo(Members[start].Name) > 0) && (theName.CompareTo(Members[end].Name) < 0))
			if ((theName.CompareTo(sname) >= 0) && (theName.CompareTo(ename) <= 0))
			{
				int cmp = theName.CompareTo(mname);
				if (cmp < 0)
					index = TreeSearch(theName, Members, start, mid);
				else if (cmp > 0)
					index = TreeSearch(theName, Members, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
				//	Console.WriteLine("got it at " + index);
			}
			return index;
		}
		*/
		public void ResetWritten()
		{
			foreach(iLORMember4 member in myByIDList)
			{
				member.AltID = lutils.UNDEFINED;
				//member.Written = false;
			}
			myHighestAltID = lutils.UNDEFINED;
		}
		public int DescendantCount(bool selectedOnly = false, bool countPlain = true, bool countRGBparents = false, bool countRGBchildren = true)
		{
			// Depending on situation/usaage, you will probably want to count
			// The RGBchannels, OR count their 3 children.
			//    Unlikely you will need to count neither or both, but you can if you want
			// ChannelGroups themselves are not counted, but all their descendants are (except descendant groups).
			// Tracks are not counted.

			int c = 0;
			for (int l = 0; l < myByDisplayOrderList.Count; l++)
			{
				LORMemberType4 t = myByDisplayOrderList[l].MemberType;
				if (t == LORMemberType4.Channel)
				{
					if (countPlain)
					{
						if (myByDisplayOrderList[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.RGBChannel)
				{
					if (countRGBparents)
					{
						if (myByDisplayOrderList[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LORRGBChannel4 rgbCh = (LORRGBChannel4)myByDisplayOrderList[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)myByDisplayOrderList[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LORMemberType4.Cosmic)
				{
					LORCosmic4 dev = (LORCosmic4)myByDisplayOrderList[l];
					// Recurse!!
					c += dev.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
			}


			return c;
		}
		public iLORMember4 Parent
		{
			get
			{
				if (myParent == null)
				{
					if (myOwner != null)
					{
						myParent = myOwner.Parent;
					}
				}
				return myParent;
			}
		}
		//bool IEnumerator.MoveNext()
		//{
		//	throw new NotImplementedException();
		//}

		//void IEnumerator.Reset()
		//{
		//	throw new NotImplementedException();
		//}
		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~LORMembership4() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		//void IDisposable.Dispose()
		//{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			//Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		//}
		#endregion


	} // end LORMembership4 Class (Collection)




}
