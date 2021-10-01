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
	public class LORMembership4 : IEnumerator, IEnumerable  //  IEnumerator<iLORMember4>
	{
		private List<iLORMember4> myItemsList = new List<iLORMember4>();   // The Main List
		//private List<iLORMember4> Members = null;
		private List<iLORMember4> myBySavedIndexList = new List<iLORMember4>();
		private List<iLORMember4> myByAltSavedIndexList = new List<iLORMember4>();
		private List<LORTimings4> myBySaveIDList = new List<LORTimings4>();
		private List<LORTimings4> myByAltSaveIDList = new List<LORTimings4>();
		private List<LORTrack4> myByTrackIndexList = new List<LORTrack4>();
		private List<LORTrack4> myByAltTrackIndexList = new List<LORTrack4>();
		private SortedDictionary<string, iLORMember4> myByNameDictionary = new SortedDictionary<string, iLORMember4>();
		//SortedList<string, iLORMember4> myByNameDictionary = new SortedList<string, iLORMember4>();

		private int myHighestSavedIndex = lutils.UNDEFINED;
		public int AltHighestSavedIndex = lutils.UNDEFINED;
		private int myHighestSaveID = lutils.UNDEFINED;
		public int AltHighestSaveID = lutils.UNDEFINED;
		//iLORMember4 Parent = null;  // Parent SEQUENCE
		protected iLORMember4 myOwner = null;  // Parent GROUP or TRACK
		private LORSequence4 myParentSeq = null;


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
				myOwner = theOwner;
				myParentSeq = (LORSequence4)Owner.Parent;
			}
		}

		public void Clear()
		{
			myItemsList = new List<iLORMember4>();   // The Main List
			myBySavedIndexList = new List<iLORMember4>();
			myByAltSavedIndexList = new List<iLORMember4>();
			myBySaveIDList = new List<LORTimings4>();
			myByAltSaveIDList = new List<LORTimings4>();
			myByTrackIndexList = new List<LORTrack4>();
			myByAltTrackIndexList = new List<LORTrack4>();
			myByNameDictionary = new SortedDictionary<string, iLORMember4>();
			myHighestSavedIndex = lutils.UNDEFINED;
			AltHighestSavedIndex = lutils.UNDEFINED;
			myHighestSaveID = lutils.UNDEFINED;
			AltHighestSaveID = lutils.UNDEFINED;
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
		{ get { return myItemsList; } }
		public List<iLORMember4> Members
		{ get { return myItemsList; } }
		//private List<iLORMember4> Members = null;
		public List<iLORMember4> BySavedIndex
		{ get { return myBySavedIndexList; } }
		public List<iLORMember4> ByAltSavedIndex
		{ get { return myByAltSavedIndexList; } }
		public List<LORTimings4> BySaveID
		{ get { return myBySaveIDList; } }
		public List<LORTimings4> ByAltSaveID
		{ get { return myByAltSaveIDList; } }
		public List<LORTrack4> ByTrackIndex
		{ get { return myByTrackIndexList; } }
		public List<LORTrack4> ByAltTrackIndex
		{ get { return myByAltTrackIndexList; } }
		public SortedDictionary<string, iLORMember4> ByName
		{ get { return myByNameDictionary; } }
		public int HighestSavedIndex
		{ get { return myHighestSavedIndex; } }
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
			myParentSeq = (LORSequence4)myOwner.Parent;
		}

		// LORMembership4.Add(Member)
		public int Add(iLORMember4 newMember)
		{
			int memberSI = lutils.UNDEFINED;
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
			if (this.Parent == null)
			{
				if (newMember.Parent == null)
				{
					if (Fyle.DebugMode)
					{
						System.Diagnostics.Debugger.Break();
					}
				}
				else
				{
					//this.Parent = (LORSequence4)newMember.Parent;
				}
			}
			else
			{
				newMember.SetParent(this.Parent);
			}

			if (myParentSeq == null)
			{
				Fyle.BUG("Membership has no Parent Sequence!");
			}



			if (Parent != null) Parent.MakeDirty(true);
			if ((newMember.MemberType == LORMemberType4.Channel) ||
				  (newMember.MemberType == LORMemberType4.RGBChannel) ||
				  (newMember.MemberType == LORMemberType4.ChannelGroup) ||
				  (newMember.MemberType == LORMemberType4.Cosmic))
			{
				// Reminder, Must be a member which really has a SavedIndex to hit this point
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
				myItemsList.Add(newMember);
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

			if (myOwner.MemberType == LORMemberType4.Sequence)
			{
				//LORSequence4 mySeq = (LORSequence4)myOwner.Parent;
				bool need2add = false; // Reset
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
					if (memberSI > myParentSeq.Members.myHighestSavedIndex)
					{
							need2add = true;
					}
				}
				// Else new member is LORTrack4 or Timing Grid
				if (newMember.MemberType == LORMemberType4.Track)
				{
					LORTrack4 newTrack = (LORTrack4)newMember;
					int trkIdx = newTrack.Index;
					if (trkIdx > myParentSeq.Tracks.Count)
					{
						need2add = true;
					}
				}
				if (newMember.MemberType == LORMemberType4.Timings)
				{
					LORTimings4 newGrid = (LORTimings4)newMember;
					int gridSaveID = newGrid.SaveID;
					if (gridSaveID > myParentSeq.Members.myHighestSaveID)
					{ 
						need2add = true;
					}
				}


				if (need2add)
				{
					if (newMember.MemberType == LORMemberType4.Channel)
					{
						myParentSeq.Channels.Add((LORChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.RGBChannel)
					{
						myParentSeq.RGBchannels.Add((LORRGBChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.ChannelGroup)
					{
						myParentSeq.ChannelGroups.Add((LORChannelGroup4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Cosmic)
					{
						myParentSeq.CosmicDevices.Add((LORCosmic4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Track)
					{
						myParentSeq.Tracks.Add((LORTrack4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Timings)
					{
						myParentSeq.TimingGrids.Add((LORTimings4)newMember);
					}

					myItemsList.Add(newMember);
					myEverythingCount++;
				} // end if need2add
			} // end if owner is the sequenced

			return memberSI;
		}

		// For iEnumerable
		public iLORMember4 this[int index]
		{
			get { return myItemsList[index]; }
			set { myItemsList.Insert(index, value); }
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}

		public bool Includes(string memberName)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m=0; m< myItemsList.Count; m++)
			{
				iLORMember4 member = myItemsList[m];
				if (member.Name == memberName)
				{
					found = true;
					m = myItemsList.Count; // Exit loop
				}
			}
			return found;
		}

		public bool Includes(int savedIndex)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m = 0; m < myItemsList.Count; m++)
			{
				iLORMember4 member = myItemsList[m];
				if (member.SavedIndex == savedIndex)
				{
					found = true;
					m = myItemsList.Count; // Exit loop
				}
			}
			return found;
		}


		public bool MoveNext()
		{
			position++;
			return (position < myItemsList.Count);
		}

		public void Reset()
		{ position = -1; }

		public object Current
		{
			get { return myItemsList[position]; }
		}


		public int Count
		{
			get
			{
				return myItemsList.Count;
			}
		}

		public int SelectedMemberCount
		{
			// Besides getting the number of selected members and submembers
			// it also 'cleans up' the selection states

			get
			{
				int count = 0;
				if (myOwner.Selected)
				{
					foreach (iLORMember4 m in myItemsList)
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
								int subCount = g.Members.SelectedMemberCount;  // Recurse!
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
								int subCount = d.Members.SelectedMemberCount;  // Recurse!
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
			get
			{
				return myHighestSaveID;
			}
			set
			{
				myHighestSaveID = value;
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

			for (int i = 0; i < myItemsList.Count; i++)
			{
				iLORMember4 member = myItemsList[i];

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
			if (myParentSeq.TimingGrids.Count > 0)
			{
				//AlphaSortSavedIndexes(byTimingGridName, 0, Parent.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			}

		} // end ReIndex

		private void NULLITEMCHECK()
		{
			for (int i = 0; i < myItemsList.Count; i++)
			{
				if (myItemsList[i] == null) System.Diagnostics.Debugger.Break();
				if (myItemsList[i].Parent == null) System.Diagnostics.Debugger.Break();
				int v = myItemsList[i].CompareTo(myItemsList[0]);
			}

		}


		// LORMembership4.find(name, type, create)
		public iLORMember4 Find(string theName, LORMemberType4 theType, bool createIfNotFound)
		{
#if DEBUG
			string msg = "LORMembership4.find(" + theName + ", ";
			msg += theType.ToString() + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			//LORSequence4 mySeq = (LORSequence4)Parent;
			iLORMember4 ret = null;
			if (ret==null)
			{
				if (myParentSeq == null)
				{
					myParentSeq = (LORSequence4)Parent;
					if (Parent != null)
					{
						if (theType == LORMemberType4.Channel)
						{
							if (myChannelCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateChannel(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.RGBChannel)
						{
							if (myRGBChannelCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateRGBchannel(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.ChannelGroup)
						{
							if (myChannelGroupCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateChannelGroup(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.Cosmic)
						{
							if (myCosmicDeviceCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateCosmicDevice(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.Timings)
						{
							if (myTimingGridCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateTimingGrid(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.Track)
						{
							if (myTrackCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = myParentSeq.CreateTrack(theName);
								Add(ret);
							}
						}
					}
				}
			}

			return ret;
		}



		public iLORMember4 FindBySavedIndex(int theSavedIndex)
		{
			iLORMember4 ret = myBySavedIndexList[theSavedIndex];
			return ret;
		}

		private iLORMember4 FindByName(string theName, LORMemberType4 PartType)
		{
			//iLORMember4 ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			if (myByNameDictionary.TryGetValue(theName, out iLORMember4 ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != PartType)
				{
					ret = null;
				}
			}
			return ret;
		}

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


		public int DescendantCount(bool selectedOnly, bool countPlain, bool countRGBparents, bool countRGBchildren)
		{
			// Depending on situation/usaage, you will probably want to count
			// The RGBchannels, OR count their 3 children.
			//    Unlikely you will need to count neither or both, but you can if you want
			// ChannelGroups themselves are not counted, but all their descendants are (except descendant groups).
			// Tracks are not counted.

			int c = 0;
			for (int l = 0; l < myItemsList.Count; l++)
			{
				LORMemberType4 t = myItemsList[l].MemberType;
				if (t == LORMemberType4.Channel)
				{
					if (countPlain)
					{
						if (myItemsList[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.RGBChannel)
				{
					if (countRGBparents)
					{
						if (myItemsList[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LORRGBChannel4 rgbCh = (LORRGBChannel4)myItemsList[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)myItemsList[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LORMemberType4.Cosmic)
				{
					LORCosmic4 dev = (LORCosmic4)myItemsList[l];
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
				if (myParentSeq == null)
				{
					if (myOwner != null)
					{
						myParentSeq = (LORSequence4)myOwner.Parent;
					}
				}
				return myParentSeq;
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
