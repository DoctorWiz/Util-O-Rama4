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
		private List<iLORMember4> myMembers = new List<iLORMember4>();   // The Main List
		//private List<iLORMember4> Members = null;
		private List<iLORMember4> myBySavedIndexList = new List<iLORMember4>();
		private List<iLORMember4> myByAltSavedIndexList = new List<iLORMember4>();
		private List<LORTimings4> myBySaveIDList = new List<LORTimings4>();
		private List<LORTimings4> myByAltSaveIDList = new List<LORTimings4>();

		private List<LORVizDrawObject4> myByObjectIDList = new List<LORVizDrawObject4>();
		private List<LORVizDrawObject4> myByAltObjectIDList = new List<LORVizDrawObject4>();
		private List<LORVizItemGroup4> myByItemIDList = new List<LORVizItemGroup4>();
		private List<LORVizItemGroup4> myByAltItemIDList = new List<LORVizItemGroup4>();





		private List<LORTrack4> myByTrackIndexList = new List<LORTrack4>();
		private List<LORTrack4> myByAltTrackIndexList = new List<LORTrack4>();
		private SortedDictionary<string, iLORMember4> myByNameDictionary = new SortedDictionary<string, iLORMember4>();
		//SortedList<string, iLORMember4> myByNameDictionary = new SortedList<string, iLORMember4>();

		private int myHighestSavedIndex = lutils.UNDEFINED;
		public int AltHighestSavedIndex = lutils.UNDEFINED;
		private int myHighestSaveID = lutils.UNDEFINED;
		public int AltHighestSaveID = lutils.UNDEFINED;
		// B-cuz item numbers in Visualizations are 1-based (instead of normal 0-based)
		private int myHighestItemID = 0;
		public int AltHighestItemID = lutils.UNDEFINED;
		// B-cuz drawobjects in Visualizations do the same thing
		private int myHighestObjectID = 0;
		public int AltHighestObjectID = lutils.UNDEFINED;
		//iLORMember4 Parent = null;  // Parent SEQUENCE
		protected iLORMember4 myOwner = null;  // Parent GROUP or TRACK or Sequence or Visualization
		private iLORMember4 myParent = null;


		public const int SORTbySavedIndex = 1;
		public const int SORTbyAltSavedIndex = 2;
		public const int SORTbyName = 3;
		public const int SORTbyOutput = 4;
		public static int sortMode = SORTbySavedIndex;

		private	int myChannelCount = 0;
		private	int myRGBChannelCount = 0;
		private	int myChannelGroupCount = 0;
		private	int myCosmicDeviceCount = 0;
		private	int myTrackCount = 0;
		private	int myTimingGridCount = 0;
		private int myVizChannelCount = 0;
		private int myVizItemGroupCount = 0;
		private int myVizDrawObjectCount = 0;
		private	int myEverythingCount = 0;
		
		// For Enumeration
		private int position = 0;

		//iLORMember4 IEnumerator<iLORMember4>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();

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
				if (Owner.Parent != null)
				{
					myParent = Owner.Parent;
				}
			}

			if (theOwner.MemberType == LORMemberType4.Visualization)
			{
				// SavedIndices and SaveIDs in Sequences start at 0. Cool! Great! No Prob!
				// But Channels, Groups, and DrawObjects in Visualizations start at 1 (Grrrrr)
				// So add a dummy object at the [0] start of the lists
				LORVizChannel4 lvc = new LORVizChannel4("\0\0DUMMY VIZCHANNEL AT INDEX [0] - DO NOT USE!");
				lvc.SetIndex(0);
				lvc.SetSavedIndex(0);
				lvc.SetParent(myParent);
				myHighestSavedIndex = 0;
				myBySavedIndexList.Add(lvc);
				LORVizDrawObject4 lvdo = new LORVizDrawObject4("\0\0DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
				lvdo.SetIndex(0);
				lvdo.SetSavedIndex(0);
				lvdo.SetParent(myParent);
				myHighestObjectID = 0;
				myByObjectIDList.Add(lvdo);
				LORVizItemGroup4 lvig = new LORVizItemGroup4("\0\0DUMMY VIZITEMGROUP AT INDEX [0] - DO NOT USE!");
				lvig.SetIndex(0);
				lvig.SetSavedIndex(0);
				lvig.SetParent(myParent);
				myHighestSavedIndex = 0;
				myBySavedIndexList.Add(lvig);
			}
			if (theOwner.MemberType == LORMemberType4.VizItemGroup)
			{
				LORVizDrawObject4 lvdo = new LORVizDrawObject4("\0\0DUMMY VIZDRAWOBJECT AT INDEX [0] - DO NOT USE!");
				lvdo.SetIndex(0);
				lvdo.SetSavedIndex(0);
				lvdo.SetParent(myParent);
				myHighestItemID = 0;
				myByObjectIDList.Add(lvdo);
			}


		}

		public void Clear()
		{
			myMembers = new List<iLORMember4>();   // The Main List
			myBySavedIndexList = new List<iLORMember4>();
			myByAltSavedIndexList = new List<iLORMember4>();
			myBySaveIDList = new List<LORTimings4>();
			myByAltSaveIDList = new List<LORTimings4>();
			myByTrackIndexList = new List<LORTrack4>();
			myByAltTrackIndexList = new List<LORTrack4>();
			myByObjectIDList = new List<LORVizDrawObject4>();
			myByAltObjectIDList = new List<LORVizDrawObject4>();
			myByItemIDList = new List<LORVizItemGroup4>();
			myByAltItemIDList = new List<LORVizItemGroup4>();

			myByNameDictionary = new SortedDictionary<string, iLORMember4>();
			myHighestSavedIndex = lutils.UNDEFINED;
			AltHighestSavedIndex = lutils.UNDEFINED;
			myHighestSaveID = lutils.UNDEFINED;
			AltHighestSaveID = lutils.UNDEFINED;
			myHighestItemID = lutils.UNDEFINED;
			AltHighestItemID = lutils.UNDEFINED;
			myHighestObjectID = lutils.UNDEFINED;
			AltHighestObjectID = lutils.UNDEFINED;
			myOwner = null;  // Parent GROUP or TRACK
			//myParentSeq = null;
			sortMode = SORTbySavedIndex;
			myChannelCount = 0;
			myRGBChannelCount = 0;
			myChannelGroupCount = 0;
			myCosmicDeviceCount = 0;
			myTrackCount = 0;
			myTimingGridCount = 0;
			myEverythingCount = 0;
			position = 0;
		}

		// READ-ONLY values and object properties
		public List<iLORMember4> Items
		{ get { return myMembers; } }
		public List<iLORMember4> Members
		{ get { return myMembers; } }
		//private List<iLORMember4> Members = null;
		public List<iLORMember4> BySavedIndex
		{ get { return myBySavedIndexList; } }
		public List<iLORMember4> ByAltSavedIndex
		{ get { return myByAltSavedIndexList; } }
		public List<LORVizItemGroup4> ByItemID
		{ get { return myByItemIDList; } }
		public List<LORVizItemGroup4> ByAltItemID
		{ get { return myByItemIDList; } }
		public List<LORTimings4> BySaveID
		{ get { return myBySaveIDList; } }
		public List<LORTimings4> ByAltSaveID
		{ get { return myByAltSaveIDList; } }
		public List<LORVizDrawObject4> ByObjectID
		{ get { return myByObjectIDList; } }
		public List<LORVizDrawObject4> ByAltObjectID
		{ get { return myByAltObjectIDList; } }
		public List<LORTrack4> ByTrackIndex
		{ get { return myByTrackIndexList; } }
		public List<LORTrack4> ByAltTrackIndex
		{ get { return myByAltTrackIndexList; } }
		public SortedDictionary<string, iLORMember4> ByName
		{ get { return myByNameDictionary; } }
		public int HighestSavedIndex
		{ 
			// Used by Sequence Channels, RGB Channels, Channel Groups, and Cosmic Devices
			// Used by Visualization VizChannels
			get
			{
				return myHighestSavedIndex;
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
		{ get { return myEverythingCount; } }








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
			int memberSI = lutils.UNDEFINED;
			int psi = lutils.UNDEFINED;
			int tc = lutils.UNDEFINED;
			int psv = lutils.UNDEFINED;
			bool need2add = false;
			LORMemberType4 parentType = LORMemberType4.None;
			//#if DEBUG
			//	string msg = "LORMembership4.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//	Debug.WriteLine(msg);
			//#endif
			//#if DEBUG
			//	if ((newMember.SavedIndex == 6) || (newMember.MemberType == LORMemberType4.Timings))
			//	{
			//		string msg = "LORMembership4.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//		msg += " to owner " + this.Owner.Name;
			//		Debug.WriteLine(msg);
			// Check to make sure timing grid is not being added to a channel group (while initial reading of file)
			// If so, trace stack, find out why
			//System.Diagnostics.Debugger.Break();
			//	}
			//#endif


			// If for some reason this didn't get set yet
			if (myParent == null)
			{
				if (myOwner.Parent != null)
				{
					myParent = myOwner.Parent;
				}
				else
				{
					string warn = myOwner.Name + " has no parent and so neither does its Membership!";
					//Fyle.BUG(warn);
				}
			}
			// Did that fix it, or still null?
			if (myParent == null)
			{
				if (newMember.Parent != null)
				{
					myParent = newMember.Parent;
				}
				else
				{
					string fatal = "Parentage of " + myOwner.Name + "'s Membership is undetermined!";
					Fyle.BUG(fatal);
				}
			}
			else
			{
				newMember.SetParent(myParent);
			}

			if (myParent == null)
			{
				Fyle.BUG("Membership has no Parent Sequence!");
			}



			if (myParent != null) Parent.MakeDirty(true);
			if ((newMember.MemberType == LORMemberType4.Channel) ||
				  (newMember.MemberType == LORMemberType4.RGBChannel) ||
				  (newMember.MemberType == LORMemberType4.ChannelGroup) ||
				  (newMember.MemberType == LORMemberType4.Cosmic))
			{
				// Reminder, Must be a member which really has a SavedIndex to hit this point
				parentType = myParent.MemberType;
				memberSI = newMember.SavedIndex;
				if (memberSI < 0)
				{
					myHighestSavedIndex++;
					memberSI = myHighestSavedIndex;
					newMember.SetSavedIndex(memberSI);
				}
				if (memberSI > myHighestSavedIndex)
				{
					myHighestSavedIndex = memberSI;
				}
				
				if (newMember.MemberType == LORMemberType4.Channel)
				{
					//byAlphaChannelNames.Add(newMember);
					myChannelCount++;
				}
				if (newMember.MemberType == LORMemberType4.RGBChannel)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					myRGBChannelCount++;
				}
				if (newMember.MemberType == LORMemberType4.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					myChannelGroupCount++;
				}
				if (newMember.MemberType == LORMemberType4.Cosmic)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					myCosmicDeviceCount++;
				}

				while ((myBySavedIndexList.Count-1) < memberSI)
				{
					myBySavedIndexList.Add(null);
					myByAltSavedIndexList.Add(null);
				}
				myBySavedIndexList[memberSI] = newMember;
				myByAltSavedIndexList[memberSI] = newMember;
				myMembers.Add(newMember);
			}

			if (newMember.MemberType == LORMemberType4.Track)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newMember);
				//myBySavedIndexList.Add(newMember);
				//myByAltSavedIndexList.Add(newMember);
				LORTrack4 tr = (LORTrack4)newMember;
				int trackIdx = tr.Index;
				if (trackIdx < 0) // Sanity Check
				{
					System.Diagnostics.Debugger.Break();
				}
				while ((myByTrackIndexList.Count -1) < trackIdx)
				{
					myByTrackIndexList.Add(null);
					myByAltTrackIndexList.Add(null);
				}
				myByTrackIndexList[trackIdx] = tr;
				myByAltTrackIndexList[trackIdx] = tr;
				myTrackCount++;
			}

			if (newMember.MemberType == LORMemberType4.Timings)
			{
				LORTimings4 tg = (LORTimings4)newMember;
				int gridSID = tg.SaveID;
				if (gridSID < 0)
				{
					myHighestSaveID++;
					gridSID = myHighestSaveID;
					//newMember.SetSavedIndex(memberSI);
					tg.SaveID = gridSID;
				}
				if (gridSID > myHighestSaveID)
				{
					myHighestSaveID = gridSID;
				}
				while ((myBySaveIDList.Count - 1) < gridSID)
				{
					myBySaveIDList.Add(null);
					myByAltSaveIDList.Add(null);
				}
				//! Exception Here!  memberSI not set!  (=-1 Undefined)
				//myBySaveIDList[memberSI] = tg;
				myBySaveIDList[tg.SaveID] = tg;
				//myByAltSaveIDList[memberSI] = tg;
				myByAltSaveIDList[tg.SaveID] = tg;
				myTimingGridCount++;
			}


			if ((newMember.MemberType == LORMemberType4.VizChannel) ||
					(newMember.MemberType == LORMemberType4.VizDrawObject) ||
					(newMember.MemberType == LORMemberType4.VizItemGroup))
			{
				// Reminder, Must be a Visualizer member to hit this point
				parentType = myParent.MemberType;
				// Note: SavedIndex same as ItemID
				int memberIID = newMember.SavedIndex;

				if (newMember.MemberType == LORMemberType4.VizChannel)
				{
					if (memberIID < 0)
					{
						myHighestSavedIndex++;
						memberIID = myHighestSavedIndex;
						newMember.SetSavedIndex(memberIID);
					}
					if (memberIID > myHighestSavedIndex)
					{
						myHighestSavedIndex = memberIID;
					}
					while (myBySavedIndexList.Count <= memberIID)
					{
						myBySavedIndexList.Add(null);
					}
					myBySavedIndexList[memberIID] = newMember;
					myVizChannelCount++;
				}
				if (newMember.MemberType == LORMemberType4.VizDrawObject)
				{
					if (memberIID < 0)
					{
						myHighestObjectID++;
						memberIID = myHighestObjectID;
						newMember.SetSavedIndex(memberIID);
					}
					if (memberIID > myHighestObjectID)
					{
						myHighestObjectID = memberIID;
					}
					while(myByObjectIDList.Count <= memberIID)
					{
						myByObjectIDList.Add(null);
					}
					myByObjectIDList[memberIID] = (LORVizDrawObject4)newMember;
					myVizDrawObjectCount++;
				}
				if (newMember.MemberType == LORMemberType4.VizItemGroup)
				{
					if (memberIID < 0)
					{
						myHighestItemID++;
						memberIID = myHighestItemID;
						newMember.SetSavedIndex(memberIID);
					}
					if (memberIID > myHighestItemID)
					{
						myHighestItemID = memberIID;
					}
					while (myByItemIDList.Count <= memberIID)
					{
						myByItemIDList.Add(null);
					}
					myByItemIDList[memberIID] = (LORVizItemGroup4)newMember;
					myVizItemGroupCount++;
				}

				myMembers.Add(newMember);
			}





			if (myOwner.MemberType == LORMemberType4.Sequence)
			{
				//LORSequence4 mySeq = (LORSequence4)myOwner.Parent;
				need2add = false; // Reset
															 //if (newMember.SavedIndex < 0)
															 //{
															 //	need2add = true;
															 //}
				if ((newMember.MemberType == LORMemberType4.Channel) ||
						(newMember.MemberType == LORMemberType4.RGBChannel) ||
						(newMember.MemberType == LORMemberType4.ChannelGroup) ||
						(newMember.MemberType == LORMemberType4.Cosmic))
				{
					memberSI = newMember.SavedIndex;
					//LORSequence4 myParentSeq = (LORSequence4)Parent;
					//if (memberSI > mySeq.Members.myHighestSavedIndex)

						LORSequence4 ps = (LORSequence4)myParent;
						psi = ps.AllMembers.myHighestSavedIndex;
						tc = ps.Tracks.Count;
						psv = ps.AllMembers.HighestSaveID;

					if (memberSI > psi)
					{
						need2add = true;
					}
				}
				// Else new member is LORTrack4 or Timing Grid
				if (newMember.MemberType == LORMemberType4.Track)
				{
					LORTrack4 newTrack = (LORTrack4)newMember;
					int trkIdx = newTrack.Index;
					if (trkIdx > tc)
					{
						need2add = true;
					}
				}
				if (newMember.MemberType == LORMemberType4.Timings)
				{
					LORTimings4 newGrid = (LORTimings4)newMember;
					int gridSaveID = newGrid.SaveID;
					if (gridSaveID > psv)
					{
						need2add = true;
					}
				}
			}

			if (myOwner.MemberType == LORMemberType4.Visualization)
			{
				//LORSequence4 mySeq = (LORSequence4)myOwner.Parent;
				need2add = false; // Reset
															 //if (newMember.SavedIndex < 0)
															 //{
															 //	need2add = true;
															 //}
				if ((newMember.MemberType == LORMemberType4.VizChannel) ||
						(newMember.MemberType == LORMemberType4.VizDrawObject) ||
						(newMember.MemberType == LORMemberType4.VizItemGroup))
				{
					int memberIID = newMember.SavedIndex;
					//LORSequence4 myParentSeq = (LORSequence4)Parent;
					//if (memberSI > mySeq.Members.myHighestSavedIndex)

					LORVisualization4 pv = (LORVisualization4)myParent;
					psi = pv.AllMembers.myHighestItemID;

					if (memberIID > psi)
					{
						need2add = true;
					}
				}
			}


			if (need2add)
			{
				if (parentType == LORMemberType4.Sequence)
				{
					LORSequence4 psq = (LORSequence4)myParent;
					if (newMember.MemberType == LORMemberType4.Channel)
					{
						psq.Channels.Add((LORChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.RGBChannel)
					{
						psq.RGBchannels.Add((LORRGBChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.ChannelGroup)
					{
						psq.ChannelGroups.Add((LORChannelGroup4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Cosmic)
					{
						psq.CosmicDevices.Add((LORCosmic4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Track)
					{
						psq.Tracks.Add((LORTrack4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Timings)
					{
						psq.TimingGrids.Add((LORTimings4)newMember);
					}
				}
				if (parentType == LORMemberType4.Visualization)
				{
					LORVisualization4 pvz = (LORVisualization4)myParent;
					if (newMember.MemberType == LORMemberType4.VizChannel)
					{
						pvz.VizChannels.Add((LORVizChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.VizItemGroup)
					{
						pvz.VizItemGroups.Add((LORVizItemGroup4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.VizDrawObject)
					{
						pvz.VizDrawObjects.Add((LORVizDrawObject4)newMember);
					}
				}

					myMembers.Add(newMember);
				myEverythingCount++;
			} // end if need2add

			return memberSI;
		}

		// For iEnumerable
		public iLORMember4 this[int index]
		{
			get { return myMembers[index]; }
			set { myMembers.Insert(index, value); }
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}

		public bool Includes(string memberName)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m=0; m< myMembers.Count; m++)
			{
				iLORMember4 member = myMembers[m];
				if (member.Name == memberName)
				{
					found = true;
					m = myMembers.Count; // Exit loop
				}
			}
			return found;
		}

		public bool Includes(int savedIndex)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m = 0; m < myMembers.Count; m++)
			{
				iLORMember4 member = myMembers[m];
				if (member.SavedIndex == savedIndex)
				{
					found = true;
					m = myMembers.Count; // Exit loop
				}
			}
			return found;
		}


		public bool MoveNext()
		{
			position++;
			return (position < myMembers.Count);
		}

		public void Reset()
		{ position = -1; }

		public object Current
		{
			get { return myMembers[position]; }
		}


		public int Count
		{
			get
			{
				return myMembers.Count;
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
				if ((s == 0) &&  !Owner.Selected)
				{
					// Then we are completely unchecked!
					cs = CheckState.Unchecked;
				}
				else // at least one or more descented is selected
				{
					// How many TOTAL descendants are there (including unselected)
					int d = DescendantCount(false, true, false, true);
					// Does selected = total, and is my owner selected also?
					if ((s==d) && Owner.Selected)
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
					foreach (iLORMember4 m in myMembers)
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

		public int HighestSaveID
		{
			// For Sequence Timing Grids only
			get
			{
				return myHighestSaveID;
			}
			set
			{
				myHighestSaveID = value;
			}
		}

		public int HighestItemID
		{
			// For Visualization ItemGroups only
			get
			{
				return myHighestItemID;
			}
			set
			{
				myHighestItemID = value;
			}
		}

		public int HighestObjectID
		{
			// For Visualization DrawObjects only
			get
			{
				return myHighestObjectID;
			}
			set
			{
				myHighestObjectID = value;
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
			myEverythingCount = 0;

			sortMode = SORTbySavedIndex;

			myByNameDictionary = new SortedDictionary<string, iLORMember4>();
			myByAltSavedIndexList = new List<iLORMember4>();
			myBySaveIDList = new List<LORTimings4>();
			myByAltSaveIDList = new List<LORTimings4>();

			try
			{
				for (int i = 0; i < myMembers.Count; i++)
				{
					iLORMember4 member = myMembers[i];

					int n = 2;
					string itemName = member.Name;
					// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
					if (itemName == "")
					{
						// Make up a name based on type and index
						itemName = LORSeqEnums4.MemberName(member.MemberType) + " " + member.Index.ToString();
					}
					// Check for duplicate names
					while (myByNameDictionary.ContainsKey(itemName))
					{
						// Append a number
						itemName = member.Name + " ‹" + n.ToString() + "›";
						n++;
					}
					myByNameDictionary.Add(itemName, member);
					myEverythingCount++;

					if (member.MemberType == LORMemberType4.Channel)
					{
						//byAlphaChannelNames.Add(member);
						//channelNames[myChannelCount] = member;
						myByAltSavedIndexList.Add(member);
						myChannelCount++;
					}
					else
					{
						if (member.MemberType == LORMemberType4.RGBChannel)
						{
							//byAlphaRGBchannelNames.Add(member);
							//rgbChannelNames[myRGBChannelCount] = member;
							myByAltSavedIndexList.Add(member);
							myRGBChannelCount++;
						}
						else
						{
							if (member.MemberType == LORMemberType4.ChannelGroup)
							{
								//byAlphaChannelGroupNames.Add(member);
								//channelGroupNames[myChannelGroupCount] = member;
								myByAltSavedIndexList.Add(member);
								myChannelGroupCount++;
							}
							else
							{
								if (member.MemberType == LORMemberType4.Cosmic)
								{
									//byAlphaChannelGroupNames.Add(member);
									//channelGroupNames[myChannelGroupCount] = member;
									myByAltSavedIndexList.Add(member);
									myCosmicDeviceCount++;
								}
								else
								{
									if (member.MemberType == LORMemberType4.Track)
									{
										//byAlphaTrackNames.Add(member);
										//trackNames[myTrackCount] = member;
										myByAltSavedIndexList.Add(member);
										myTrackCount++;
									}
									else
									{
										if (member.MemberType == LORMemberType4.Timings)
										{
											//byAlphaTimingGridNames.Add(member);
											//timingGridNames[myTimingGridCount] = member;
											myByAltSavedIndexList.Add(member);
											LORTimings4 tg = (LORTimings4)member;
											myBySaveIDList.Add(tg);
											myByAltSaveIDList.Add(tg);
											myTimingGridCount++;
										}
									}
								}
							}
						}
					}
				} // end foreach
			}
			catch(Exception ex)
			{
				string msg = "Error Reindexing Membership\r\n\r\n";
				msg += ex.Message;
				Fyle.BUG(msg);
			}

			// Sort 'em all!
			sortMode = SORTbySavedIndex;
			//System.Diagnostics.Debugger.Break();
			// Sort is failing, supposedly because array elements are not set (null/empty)
			//  -- but a quick check of 'Locals' doesn't show any empties
			NULLITEMCHECK();
			myBySavedIndexList.Sort();
			myBySaveIDList.Sort();

			sortMode = SORTbyName;

			//LORSequence4 mySeq = (LORSequence4)Parent;
			//if (myParentSeq.TimingGrids.Count > 0)
			//{
				//AlphaSortSavedIndexes(byTimingGridName, 0, Parent.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			//}

		} // end ReIndex

		private void NULLITEMCHECK()
		{
			for (int i = 0; i < myMembers.Count; i++)
			{
				if (myMembers[i] == null) System.Diagnostics.Debugger.Break();
				if (myMembers[i].Parent == null) System.Diagnostics.Debugger.Break();
				int v = myMembers[i].CompareTo(myMembers[0]);
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
				if (myParent == null)
				{
					if (myParent.MemberType == LORMemberType4.Sequence)
					{
						LORSequence4 parentSeq = (LORSequence4)Parent;
						if (theType == LORMemberType4.Channel)
						{
							ret = parentSeq.CreateChannel(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.RGBChannel)
						{
							ret = parentSeq.CreateRGBchannel(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.ChannelGroup)
						{
							ret = parentSeq.CreateChannelGroup(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Cosmic)
						{
							ret = parentSeq.CreateCosmicDevice(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Timings)
						{
							ret = parentSeq.CreateTimingGrid(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.Track)
						{
							ret = parentSeq.CreateTrack(theName);
							Add(ret);
						}
					} // End if parent is sequence

					if (myParent.MemberType == LORMemberType4.Visualization)
					{
						LORVisualization4 parentViz = (LORVisualization4)Parent;
						if (theType == LORMemberType4.VizChannel)
						{
							ret = parentViz.CreateVizChannel(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.VizDrawObject)
						{
							ret = parentViz.CreateDrawObject(theName);
							Add(ret);
						}
						if (theType == LORMemberType4.VizItemGroup)
						{
							ret = parentViz.CreateItemGroup(theName);
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
			iLORMember4 ret = myBySavedIndexList[theSavedIndex];
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
			foreach(iLORMember4 member in myBySavedIndexList)
			{
				member.AltSavedIndex = lutils.UNDEFINED;
				//member.Written = false;
			}
			AltHighestSavedIndex = lutils.UNDEFINED;
			AltHighestSaveID = lutils.UNDEFINED;
		}


		public int DescendantCount(bool selectedOnly = false, bool countPlain = true, bool countRGBparents = false, bool countRGBchildren = true)
		{
			// Depending on situation/usaage, you will probably want to count
			// The RGBchannels, OR count their 3 children.
			//    Unlikely you will need to count neither or both, but you can if you want
			// ChannelGroups themselves are not counted, but all their descendants are (except descendant groups).
			// Tracks are not counted.

			int c = 0;
			for (int l = 0; l < myMembers.Count; l++)
			{
				LORMemberType4 t = myMembers[l].MemberType;
				if (t == LORMemberType4.Channel)
				{
					if (countPlain)
					{
						if (myMembers[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.RGBChannel)
				{
					if (countRGBparents)
					{
						if (myMembers[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LORRGBChannel4 rgbCh = (LORRGBChannel4)myMembers[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)myMembers[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LORMemberType4.Cosmic)
				{
					LORCosmic4 dev = (LORCosmic4)myMembers[l];
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
