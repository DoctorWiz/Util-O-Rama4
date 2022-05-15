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
	public class LOR4Membership : IEnumerator, IEnumerable  //  IEnumerator<iLOR4Member>
	{
		public List<iLOR4Member> Items = new List<iLOR4Member>();   // The Main List
		public List<iLOR4Member> bySavedIndex = new List<iLOR4Member>();
		public List<iLOR4Member> byAltSavedIndex = new List<iLOR4Member>();
		public List<LORTimings4> bySaveID = new List<LORTimings4>();
		public List<LORTimings4> byAltSaveID = new List<LORTimings4>();
		public List<LORTrack4> byTrackIndex = new List<LORTrack4>();
		public List<LORTrack4> byAltTrackIndex = new List<LORTrack4>();
		public SortedDictionary<string, iLOR4Member> byName = new SortedDictionary<string, iLOR4Member>();
		//public SortedList<string, iLOR4Member> byName = new SortedList<string, iLOR4Member>();

		private int highestSavedIndex = lutils.UNDEFINED;
		public int altHighestSavedIndex = lutils.UNDEFINED;
		private int highestSaveID = lutils.UNDEFINED;
		public int altHighestSaveID = lutils.UNDEFINED;
		//public iLOR4Member Parent = null;  // Parent SEQUENCE
		protected iLOR4Member myOwner = null;  // Parent GROUP or TRACK

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

		//iLOR4Member IEnumerator<iLOR4Member>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();

		public LOR4Membership(iLOR4Member theOwner)
		{
			if (theOwner == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				myOwner = theOwner;
			}
		}

		public iLOR4Member Owner
		{
			get
			{
				return myOwner;
			}
		}

		public void ChangeOwner(iLOR4Member newOwner)
		{
			//! WHY?!?!
			if (Fyle.isWiz) System.Diagnostics.Debugger.Break();
			myOwner = newOwner;
		}

		// LOR4Membership.Add(Member)
		public int Add(iLOR4Member newMember)
		{
			int memberSI = lutils.UNDEFINED;
			//#if DEBUG
			//	string msg = "LOR4Membership.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//	Debug.WriteLine(msg);
			//#endif
			//#if DEBUG
			//	if ((newMember.SavedIndex == 6) || (newMember.MemberType == LOR4MemberType.Timings))
			//	{
			//		string msg = "LOR4Membership.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
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
					if (Fyle.isWiz)
					{
						System.Diagnostics.Debugger.Break();
					}
				}
				else
				{
					//this.Parent = (LOR4Sequence)newMember.Parent;
				}
			}
			else
			{
				newMember.SetParent(this.Parent);
			}


			if (Parent != null) Parent.MakeDirty(true);
			if ((newMember.MemberType == LOR4MemberType.Channel) ||
					(newMember.MemberType == LOR4MemberType.RGBChannel) ||
					(newMember.MemberType == LOR4MemberType.ChannelGroup) ||
					(newMember.MemberType == LOR4MemberType.Cosmic))
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

				if (newMember.MemberType == LOR4MemberType.Channel)
				{
					//byAlphaChannelNames.Add(newMember);
					channelCount++;
				}
				if (newMember.MemberType == LOR4MemberType.RGBChannel)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					rgbChannelCount++;
				}
				if (newMember.MemberType == LOR4MemberType.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					channelGroupCount++;
				}
				if (newMember.MemberType == LOR4MemberType.Cosmic)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					cosmicDeviceCount++;
				}

				while ((bySavedIndex.Count - 1) < memberSI)
				{
					bySavedIndex.Add(null);
					byAltSavedIndex.Add(null);
				}
				bySavedIndex[memberSI] = newMember;
				byAltSavedIndex[memberSI] = newMember;
				Items.Add(newMember);
			}

			if (newMember.MemberType == LOR4MemberType.Track)
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
				while ((byTrackIndex.Count - 1) < trackIdx)
				{
					byTrackIndex.Add(null);
					byAltTrackIndex.Add(null);
				}
				byTrackIndex[trackIdx] = tr;
				byAltTrackIndex[trackIdx] = tr;
				trackCount++;
			}

			if (newMember.MemberType == LOR4MemberType.Timings)
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

			if (myOwner.MemberType == LOR4MemberType.Sequence)
			{
				LOR4Sequence mySeq = (LOR4Sequence)myOwner.Parent;
				bool need2add = false; // Reset
															 //if (newMember.SavedIndex < 0)
															 //{
															 //	need2add = true;
															 //}
				if ((newMember.MemberType == LOR4MemberType.Channel) ||
						(newMember.MemberType == LOR4MemberType.RGBChannel) ||
						(newMember.MemberType == LOR4MemberType.ChannelGroup) ||
						(newMember.MemberType == LOR4MemberType.Cosmic))
				{
					memberSI = newMember.SavedIndex;
					//LOR4Sequence myParentSeq = (LOR4Sequence)Parent;
					if (memberSI > mySeq.Members.highestSavedIndex)
					{
						need2add = true;
					}
				}
				// Else new member is LORTrack4 or Timing Grid
				if (newMember.MemberType == LOR4MemberType.Track)
				{
					LORTrack4 newTrack = (LORTrack4)newMember;
					int trkIdx = newTrack.Index;
					if (trkIdx > mySeq.Tracks.Count)
					{
						need2add = true;
					}
				}
				if (newMember.MemberType == LOR4MemberType.Timings)
				{
					LORTimings4 newGrid = (LORTimings4)newMember;
					int gridSaveID = newGrid.SaveID;
					if (gridSaveID > mySeq.Members.highestSaveID)
					{
						need2add = true;
					}
				}


				if (need2add)
				{
					if (newMember.MemberType == LOR4MemberType.Channel)
					{
						mySeq.Channels.Add((LOR4Channel)newMember);
					}
					if (newMember.MemberType == LOR4MemberType.RGBChannel)
					{
						mySeq.RGBchannels.Add((LOR4RGBChannel)newMember);
					}
					if (newMember.MemberType == LOR4MemberType.ChannelGroup)
					{
						mySeq.ChannelGroups.Add((LOR4ChannelGroup)newMember);
					}
					if (newMember.MemberType == LOR4MemberType.Cosmic)
					{
						mySeq.CosmicDevices.Add((LOR4Cosmic)newMember);
					}
					if (newMember.MemberType == LOR4MemberType.Track)
					{
						mySeq.Tracks.Add((LORTrack4)newMember);
					}
					if (newMember.MemberType == LOR4MemberType.Timings)
					{
						mySeq.TimingGrids.Add((LORTimings4)newMember);
					}

					Items.Add(newMember);
					allCount++;
				} // end if need2add
			} // end if owner is the sequenced

			return memberSI;
		}

		// For iEnumerable
		public iLOR4Member this[int index]
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
			for (int m = 0; m < Items.Count; m++)
			{
				iLOR4Member member = Items[m];
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
				iLOR4Member member = Items[m];
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
					foreach (iLOR4Member m in Items)
					{
						if (m.MemberType == LOR4MemberType.Channel)
						{
							if (m.Selected) count++;
						}
						if (m.MemberType == LOR4MemberType.RGBChannel)
						{
							if (m.Selected)
							{
								int subCount = 0;
								LOR4RGBChannel r = (LOR4RGBChannel)m;
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
						if (m.MemberType == LOR4MemberType.ChannelGroup)
						{
							if (m.Selected)
							{
								LOR4ChannelGroup g = (LOR4ChannelGroup)m;
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
						if (m.MemberType == LOR4MemberType.Cosmic)
						{
							if (m.Selected)
							{
								LOR4Cosmic d = (LOR4Cosmic)m;
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

			byName = new SortedDictionary<string, iLOR4Member>();
			byAltSavedIndex = new List<iLOR4Member>();
			bySaveID = new List<LORTimings4>();
			byAltSaveID = new List<LORTimings4>();

			for (int i = 0; i < Items.Count; i++)
			{
				iLOR4Member member = Items[i];

				int n = 2;
				string itemName = member.Name;
				// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
				if (itemName == "")
				{
					// Make up a name based on type and index
					itemName = LOR4SeqEnums.MemberName(member.MemberType) + " " + member.Index.ToString();
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

				if (member.MemberType == LOR4MemberType.Channel)
				{
					//byAlphaChannelNames.Add(member);
					//channelNames[channelCount] = member;
					byAltSavedIndex.Add(member);
					channelCount++;
				}
				else
				{
					if (member.MemberType == LOR4MemberType.RGBChannel)
					{
						//byAlphaRGBchannelNames.Add(member);
						//rgbChannelNames[rgbChannelCount] = member;
						byAltSavedIndex.Add(member);
						rgbChannelCount++;
					}
					else
					{
						if (member.MemberType == LOR4MemberType.ChannelGroup)
						{
							//byAlphaChannelGroupNames.Add(member);
							//channelGroupNames[channelGroupCount] = member;
							byAltSavedIndex.Add(member);
							channelGroupCount++;
						}
						else
						{
							if (member.MemberType == LOR4MemberType.Cosmic)
							{
								//byAlphaChannelGroupNames.Add(member);
								//channelGroupNames[channelGroupCount] = member;
								byAltSavedIndex.Add(member);
								cosmicDeviceCount++;
							}
							else
							{
								if (member.MemberType == LOR4MemberType.Track)
								{
									//byAlphaTrackNames.Add(member);
									//trackNames[trackCount] = member;
									byAltSavedIndex.Add(member);
									trackCount++;
								}
								else
								{
									if (member.MemberType == LOR4MemberType.Timings)
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

			LOR4Sequence mySeq = (LOR4Sequence)Parent;
			if (mySeq.TimingGrids.Count > 0)
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
		public LOR4Channel FindChannel(string theName, bool createIfNotFound = false, bool whatIsThisFor = false)
		{ return (LOR4Channel)Find(theName, LOR4MemberType.Channel, createIfNotFound); }

		public LOR4Channel FindChannel(string theName, bool createIfNotFound = false)
		{ return (LOR4Channel)Find(theName, LOR4MemberType.Channel, createIfNotFound); }

		public LOR4RGBChannel FindRGBChannel(string theName, bool createIfNotFound = false, bool whatIsThisFor = false)
		{ return (LOR4RGBChannel)Find(theName, LOR4MemberType.RGBChannel, createIfNotFound); }

		public LOR4RGBChannel FindRGBChannel(string theName, bool createIfNotFound = false)
		{ return (LOR4RGBChannel)Find(theName, LOR4MemberType.RGBChannel, createIfNotFound); }

		public LOR4ChannelGroup FindChannelGroup(string theName, bool createIfNotFound = false)
		{ return (LOR4ChannelGroup)Find(theName, LOR4MemberType.ChannelGroup, createIfNotFound); }

		// LOR4Membership.find(name, type, create)
		public iLOR4Member Find(string theName, LOR4MemberType theType, bool createIfNotFound)
		{
#if DEBUG
			string msg = "LOR4Membership.find(" + theName + ", ";
			msg += theType.ToString() + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			LOR4Sequence mySeq = (LOR4Sequence)Parent;
			iLOR4Member ret = null;
			if (ret == null)
			{
				if (mySeq == null)
				{
					mySeq = (LOR4Sequence)Parent;
					if (Parent != null)
					{
						if (theType == LOR4MemberType.Channel)
						{
							if (channelCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateChannel(theName);
								Add(ret);
							}
						}
						if (theType == LOR4MemberType.RGBChannel)
						{
							if (rgbChannelCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateRGBchannel(theName);
								Add(ret);
							}
						}
						if (theType == LOR4MemberType.ChannelGroup)
						{
							if (channelGroupCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateChannelGroup(theName);
								Add(ret);
							}
						}
						if (theType == LOR4MemberType.Cosmic)
						{
							if (cosmicDeviceCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateCosmicDevice(theName);
								Add(ret);
							}
						}
						if (theType == LOR4MemberType.Timings)
						{
							if (timingGridCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateTimingGrid(theName);
								Add(ret);
							}
						}
						if (theType == LOR4MemberType.Track)
						{
							if (trackCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = mySeq.CreateTrack(theName);
								Add(ret);
							}
						}
					}
				}
			}

			return ret;
		}



		public iLOR4Member FindBySavedIndex(int theSavedIndex)
		{
			iLOR4Member ret = bySavedIndex[theSavedIndex];
			return ret;
		}

		private iLOR4Member FindByName(string theName, LOR4MemberType PartType)
		{
			//iLOR4Member ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			if (byName.TryGetValue(theName, out iLOR4Member ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != PartType)
				{
					ret = null;
				}
			}
			return ret;
		}

		private static iLOR4Member FindByName(string theName, List<iLOR4Member> Members)
		{
			iLOR4Member ret = null;
			int idx = BinarySearch(theName, Members);
			if (idx > lutils.UNDEFINED)
			{
				ret = Members[idx];
			}
			return ret;
		}

		private static int BinarySearch(string theName, List<iLOR4Member> Members)
		{
			return TreeSearch(theName, Members, 0, Members.Count - 1);
		}

		private static int TreeSearch(string theName, List<iLOR4Member> Members, int start, int end)
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
			foreach (iLOR4Member member in bySavedIndex)
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
				LOR4MemberType t = Items[l].MemberType;
				if (t == LOR4MemberType.Channel)
				{
					if (countPlain)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LOR4MemberType.RGBChannel)
				{
					if (countRGBparents)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LOR4RGBChannel rgbCh = (LOR4RGBChannel)Items[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LOR4MemberType.ChannelGroup)
				{
					LOR4ChannelGroup grp = (LOR4ChannelGroup)Items[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LOR4MemberType.Cosmic)
				{
					LOR4Cosmic dev = (LOR4Cosmic)Items[l];
					// Recurse!!
					c += dev.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
			}


			return c;
		}

		public iLOR4Member Parent
		{
			get
			{
				iLOR4Member myParent = null;
				if (myOwner != null)
				{
					myParent = myOwner.Parent;
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
		// ~LOR4Membership() {
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


	} // end LOR4Membership Class (Collection)




}
