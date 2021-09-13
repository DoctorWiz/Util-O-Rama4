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
		public List<iLORMember4> Items = new List<iLORMember4>();   // The Main List
		public List<iLORMember4> bySavedIndex = new List<iLORMember4>();
		public List<iLORMember4> byAltSavedIndex = new List<iLORMember4>();
		public List<LORTimings4> bySaveID = new List<LORTimings4>();
		public List<LORTimings4> byAltSaveID = new List<LORTimings4>();
		public List<LORTrack4> byTrackIndex = new List<LORTrack4>();
		public List<LORTrack4> byAltTrackIndex = new List<LORTrack4>();
		public SortedDictionary<string, iLORMember4> byName = new SortedDictionary<string, iLORMember4>();
		//public SortedList<string, iLORMember4> byName = new SortedList<string, iLORMember4>();

		private int highestSavedIndex = lutils.UNDEFINED;
		public int altHighestSavedIndex = lutils.UNDEFINED;
		private int highestSaveID = lutils.UNDEFINED;
		public int altHighestSaveID = lutils.UNDEFINED;
		//public iLORMember4 Parent = null;  // Parent SEQUENCE
		protected iLORMember4 myOwner = null;  // Parent GROUP or TRACK
		private LORSequence4 myParentSeq = null;


		public const int SORTbySavedIndex = 1;
		public const int SORTbyAltSavedIndex = 2;
		public const int SORTbyName = 3;
		public const int SORTbyOutput = 4;
		public static int sortMode = SORTbySavedIndex;

		public int channelCount = 0;
		public int rgbChannelCount = 0;
		public int channelGroupCount = 0;
		public int cosmicDeviceCount = 0;
		public int trackCount = 0;
		public int timingGridCount = 0;
		public int allCount = 0;
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
					highestSavedIndex++;
					memberSI = highestSavedIndex;
					newMember.SetSavedIndex(memberSI);
				}
				if (memberSI > highestSavedIndex)
				{
					highestSavedIndex = memberSI;
				}
				
				if (newMember.MemberType == LORMemberType4.Channel)
				{
					//byAlphaChannelNames.Add(newMember);
					channelCount++;
				}
				if (newMember.MemberType == LORMemberType4.RGBChannel)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					rgbChannelCount++;
				}
				if (newMember.MemberType == LORMemberType4.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					channelGroupCount++;
				}
				if (newMember.MemberType == LORMemberType4.Cosmic)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					cosmicDeviceCount++;
				}

				while ((bySavedIndex.Count-1) < memberSI)
				{
					bySavedIndex.Add(null);
					byAltSavedIndex.Add(null);
				}
				bySavedIndex[memberSI] = newMember;
				byAltSavedIndex[memberSI] = newMember;
				Items.Add(newMember);
			}

			if (newMember.MemberType == LORMemberType4.Track)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newMember);
				//bySavedIndex.Add(newMember);
				//byAltSavedIndex.Add(newMember);
				LORTrack4 tr = (LORTrack4)newMember;
				int trackIdx = tr.Index;
				if (trackIdx < 0) // Sanity Check
				{
					System.Diagnostics.Debugger.Break();
				}
				while ((byTrackIndex.Count -1) < trackIdx)
				{
					byTrackIndex.Add(null);
					byAltTrackIndex.Add(null);
				}
				byTrackIndex[trackIdx] = tr;
				byAltTrackIndex[trackIdx] = tr;
				trackCount++;
			}

			if (newMember.MemberType == LORMemberType4.Timings)
			{
				LORTimings4 tg = (LORTimings4)newMember;
				int gridSID = tg.SaveID;
				if (gridSID < 0)
				{
					highestSaveID++;
					gridSID = highestSaveID;
					//newMember.SetSavedIndex(memberSI);
					tg.SaveID = gridSID;
				}
				if (gridSID > highestSaveID)
				{
					highestSaveID = gridSID;
				}
				while ((bySaveID.Count - 1) < gridSID)
				{
					bySaveID.Add(null);
					byAltSaveID.Add(null);
				}
				//! Exception Here!  memberSI not set!  (=-1 Undefined)
				//bySaveID[memberSI] = tg;
				bySaveID[tg.SaveID] = tg;
				//byAltSaveID[memberSI] = tg;
				byAltSaveID[tg.SaveID] = tg;
				timingGridCount++;
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
					//if (memberSI > mySeq.Members.highestSavedIndex)
					if (memberSI > myParentSeq.Members.highestSavedIndex)
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
					if (gridSaveID > myParentSeq.Members.highestSaveID)
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

					Items.Add(newMember);
					allCount++;
				} // end if need2add
			} // end if owner is the sequenced

			return memberSI;
		}

		// For iEnumerable
		public iLORMember4 this[int index]
		{
			get { return Items[index]; }
			set { Items.Insert(index, value); }
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}

		public bool Includes(string memberName)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m=0; m< Items.Count; m++)
			{
				iLORMember4 member = Items[m];
				if (member.Name == memberName)
				{
					found = true;
					m = Items.Count; // Exit loop
				}
			}
			return found;
		}

		public bool Includes(int savedIndex)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m = 0; m < Items.Count; m++)
			{
				iLORMember4 member = Items[m];
				if (member.SavedIndex == savedIndex)
				{
					found = true;
					m = Items.Count; // Exit loop
				}
			}
			return found;
		}


		public bool MoveNext()
		{
			position++;
			return (position < Items.Count);
		}

		public void Reset()
		{ position = -1; }

		public object Current
		{
			get { return Items[position]; }
		}


		public int Count
		{
			get
			{
				return Items.Count;
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
					foreach (iLORMember4 m in Items)
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

		public int HighestSavedIndex
		{
			get
			{
				return highestSavedIndex;
			}
		}

		public int HighestSaveID
		{
			get
			{
				return highestSaveID;
			}
			set
			{
				highestSaveID = value;
			}
		}

		public void ReIndex()
		{
			// Clear previous run

			channelCount = 0;
			rgbChannelCount = 0;
			channelGroupCount = 0;
			trackCount = 0;
			timingGridCount = 0;
			allCount = 0;

			sortMode = SORTbySavedIndex;

			byName = new SortedDictionary<string, iLORMember4>();
			byAltSavedIndex = new List<iLORMember4>();
			bySaveID = new List<LORTimings4>();
			byAltSaveID = new List<LORTimings4>();

			for (int i = 0; i < Items.Count; i++)
			{
				iLORMember4 member = Items[i];

				int n = 2;
				string itemName = member.Name;
				// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
				if (itemName == "")
				{
					// Make up a name based on type and index
					itemName = LORSeqEnums4.MemberName(member.MemberType) + " " + member.Index.ToString();
				}
				// Check for duplicate names
				while (byName.ContainsKey(itemName))
				{
					// Append a number
					itemName = member.Name + " ‹" + n.ToString() + "›";
					n++;
				}
				byName.Add(itemName, member);
				allCount++;

				if (member.MemberType == LORMemberType4.Channel)
				{
					//byAlphaChannelNames.Add(member);
					//channelNames[channelCount] = member;
					byAltSavedIndex.Add(member);
					channelCount++;
				}
				else
				{
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						//byAlphaRGBchannelNames.Add(member);
						//rgbChannelNames[rgbChannelCount] = member;
						byAltSavedIndex.Add(member);
						rgbChannelCount++;
					}
					else
					{
						if (member.MemberType == LORMemberType4.ChannelGroup)
						{
							//byAlphaChannelGroupNames.Add(member);
							//channelGroupNames[channelGroupCount] = member;
							byAltSavedIndex.Add(member);
							channelGroupCount++;
						}
						else
						{
							if (member.MemberType == LORMemberType4.Cosmic)
							{
								//byAlphaChannelGroupNames.Add(member);
								//channelGroupNames[channelGroupCount] = member;
								byAltSavedIndex.Add(member);
								cosmicDeviceCount++;
							}
							else
							{
								if (member.MemberType == LORMemberType4.Track)
								{
									//byAlphaTrackNames.Add(member);
									//trackNames[trackCount] = member;
									byAltSavedIndex.Add(member);
									trackCount++;
								}
								else
								{
									if (member.MemberType == LORMemberType4.Timings)
									{
										//byAlphaTimingGridNames.Add(member);
										//timingGridNames[timingGridCount] = member;
										byAltSavedIndex.Add(member);
										LORTimings4 tg = (LORTimings4)member;
										bySaveID.Add(tg);
										byAltSaveID.Add(tg);
										timingGridCount++;
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
			bySavedIndex.Sort();
			bySaveID.Sort();

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
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i] == null) System.Diagnostics.Debugger.Break();
				if (Items[i].Parent == null) System.Diagnostics.Debugger.Break();
				int v = Items[i].CompareTo(Items[0]);
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
							if (channelCount > 0)
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
							if (rgbChannelCount > 0)
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
							if (channelGroupCount > 0)
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
							if (cosmicDeviceCount > 0)
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
							if (timingGridCount > 0)
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
							if (trackCount > 0)
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
			iLORMember4 ret = bySavedIndex[theSavedIndex];
			return ret;
		}

		private iLORMember4 FindByName(string theName, LORMemberType4 PartType)
		{
			//iLORMember4 ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			if (byName.TryGetValue(theName, out iLORMember4 ret))
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
			foreach(iLORMember4 member in bySavedIndex)
			{
				member.AltSavedIndex = lutils.UNDEFINED;
				//member.Written = false;
			}
			altHighestSavedIndex = lutils.UNDEFINED;
			altHighestSaveID = lutils.UNDEFINED;
		}


		public int DescendantCount(bool selectedOnly, bool countPlain, bool countRGBparents, bool countRGBchildren)
		{
			// Depending on situation/usaage, you will probably want to count
			// The RGBchannels, OR count their 3 children.
			//    Unlikely you will need to count neither or both, but you can if you want
			// ChannelGroups themselves are not counted, but all their descendants are (except descendant groups).
			// Tracks are not counted.

			int c = 0;
			for (int l = 0; l < Items.Count; l++)
			{
				LORMemberType4 t = Items[l].MemberType;
				if (t == LORMemberType4.Channel)
				{
					if (countPlain)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.RGBChannel)
				{
					if (countRGBparents)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LORRGBChannel4 rgbCh = (LORRGBChannel4)Items[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)Items[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LORMemberType4.Cosmic)
				{
					LORCosmic4 dev = (LORCosmic4)Items[l];
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
