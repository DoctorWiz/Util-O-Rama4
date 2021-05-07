using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LORUtils;

namespace VampORama
{
	public class SequenceFunctions
	{
		public readonly static string[] noteNames = {"C0","C♯0-D♭0","D0","D♯0-E♭0","E0","F0","F♯0-G♭0","G0","G♯0-A♭0","A0","A♯0-B♭0","B0",
													"C1","C♯1-D♭1","D1","D♯1-E♭1","E1","F1","F♯1-G♭1","G1","G♯1-A♭1","A1","A♯1-B♭1","B1",
													"C2","C♯2-D♭2","D2","D♯2-E♭2","E2","F2","F♯2-G♭2","Low_G","Low_G♯-A♭","Low_A","Low_A♯-B♭","Low_B",
													"Low_C","Low_C♯-D♭","Low_D","Low_D♯-E♭","Low_E","Low_F","Low_F♯-G♭","Bass_G","Bass_G♯-A♭","Bass_A","Bass_A♯-B♭","Bass_B",
													"Bass_C","Bass_C♯-D♭","Bass_D","Bass_D♯-E♭","Bass_E","Bass_F","Bass_F♯-G♭","Middle_G","Middle_G♯-A♭","Middle_A","Middle_A♯-B♭","Middle_B",
													"Middle_C","Middle_C♯-D♭","Middle_D","Middle_D♯-Eb","Middle_E","Middle_F","Treble_F♯-G♭","Treble_G","Treble_G♯-A♭","Treble_A","Treble_A♯-B♭","Treble_B",
													"Treble_C","Treble_C♯-D♭","Treble_D","Treble_D♯-E♭","Treble_E","Treble_F","High_F♯-G♭","High_G","High_G♯-A♭","High_A","High_A♯-B♭","High_B",
													"High_C","High_C♯-D♭","High_D","High_D♯-E♭","High_E","High_F","F♯7-G♭7","G7","G♯7-A♭7","A7","A♯7-B♭7","B7",
													"C8","C♯8-D♭8","D8","D♯8-E♭8","E8","F8","F♯8-G♭8","G8","G♯8-A♭8","A8","A♯8-B♭8","B8",
													"C9","C♯9-D♭9","D9","D♯9-E♭9","E9","F9","F♯9-G♭9","G9","G♯9-A♭9","A9","A♯9-B♭9","B9",
													"C10","C♯10-D♭10","D10","D♯10-E♭10","E10","F10","F♯10-G♭10","G10"};

		//													C		 C♯-D♭        D    D♯-E♭        E        F    F♯-G♭        G    G♯-A♭        A    A♯-B♭        B
		public readonly static string[] noteFreqs = { "16.35", "17.32", "18.35", "19.45", "20.60", "21.83", "23.12", "24.50", "25.96", "27.50", "29.14", "30.87",
													 "32.70", "34.65", "36.71", "38.89", "41.20", "43.65", "46.25", "49.00", "51.91", "55.00", "58.27", "61.74",
													 "65.41", "69.30", "73.42", "77.78", "82.41", "87.31", "92.50", "98.00","103.83","110.00","116.54","123.47",
													"130.81","138.59","146.83","155.56","164.81","174.61","185.00","196.00","207.65","220.00","233.08","246.94",
													"261.63","277.18","293.66","311.13","329.63","349.23","369.99","392.00","415.30","440.00","466.16","493.88",
													"523.25","554.37","587.33","622.25","659.25","698.46","739.99","783.99","830.61","880,00","932.33","987.77",
													"1046.5","1108.7","1174.7","1244.5","1318.5","1396.9","1480.0","1568.0","1661.2","1760.0","1864.7","1975.3",
													"2093.0","2217.5","2349.3","2489.0","2637.0","2793.8","2960.0","3136.0","3322.4","3520.0","3729.3","3951.1",
													"4186.0","4434.2","4698.6","4978.0","5274.0","5587.6","5919.9","6271.9","6644.9","7040.0","7458.6","7902.1",
														"8372",  "8870",  "9397",  "9956", "10548", "11175", "11840", "12544", "13290", "14080", "14917", "15804",
													 "16744", "17740", "18795", "19912", "21096", "22351", "23680", "25088", "26579", "28160", "29834", "31608" };

		public readonly static string[] octaveFreqs = { "0-31", "31-63", "63-127", "127-253", "253-507", "507-1015", "1015-2034", "2035-4068", "4068-8137", "8137-16274", "16274-32000" };

		public readonly static string[] octaveNamesA = { "CCCCCC 128'", "CCCCC 64'", "CCCC 32'", "CCC 16'", "CC 8'", "C4'", "c1 2'", "c2 1'", "c3 1/2'", "c4 1/4'", "c5 1/8'", "c6 1/16'", "Err", "Err" };
		public readonly static string[] octaveNamesB = { "Sub-Sub-Sub Contra", "Sub-Sub Contra", "Sub-Contra", "Contra", "Great", "Small", "1-Line", "2-Line", "3-Line", "4-Line", "5-Line", "6-Line", "Err", "Err" };

		public readonly static string[] chromaNames = { "C", "C♯-D♭", "D", "D♯-E♭", "E", "F", "F♯-G♭", "G", "G♯-A♭", "A", "A♯-B♭", "B" };
		public readonly static string[] keyNames = { "", "C major", "C♯ major", "D major", "D♯ major", "E major", "F major", "F♯ major", "G major", "G♯ major", "A major", "A♯ major", "B major",
															"C minor", "C♯ minor", "D minor", "D♯ minor", "E minor", "F minor", "F♯ minor", "G minor", "G♯ minor", "A minor", "A♯ minor", "B minor" };


		public Sequence4 Sequence = null;

		private Channel[] noteChannels = null;
		private ChannelGroup octaveGroups = null;
		private int firstCobjIdx = utils.UNDEFINED;
		private int firstCsavedIndex = utils.UNDEFINED;



		public Track GetTrack(string trackName, bool createIfNotFound = false)
		{
			// Gets existing track specified by Name if it already exists
			// Creates it if it does not
			Track ret = Sequence.FindTrack(trackName, createIfNotFound);
			if (ret == null)
			{
				if (createIfNotFound)
				{
					ret = Sequence.CreateTrack(trackName);
					ret.Centiseconds = Sequence.Centiseconds;
				}
				//Sequence.AddTrack(ret);
			}
			return ret;
		}

		public TimingGrid GetGrid(string gridName, bool createIfNotFound = true)
		{
			// Gets existing track specified by Name if it already exists
			// Creates it if it does not
			TimingGrid ret = Sequence.FindTimingGrid(gridName, createIfNotFound);
			//TimingGrid ret = Sequence.TimingGrids.Find(gridName, MemberType.TimingGrid, true);
			if (ret == null)
			{
				if (createIfNotFound)
				{
					// ERROR! Should not ever get here
					System.Diagnostics.Debugger.Break();
					//ret = Sequence.CreateTimingGrid(gridName);
					//ret.Centiseconds = centiseconds;
					//Sequence.AddTimingGrid(ret);
				}
			}
			else
			{
				// Clear any existing timings from a previous run
				if (ret.timings.Count > 0)
				{
					ret.timings = new List<int>();
				}
			}
			return ret;
		}


		public ChannelGroup GetGroup(string groupName, IMember parent)
		{
			// Gets existing group specified by Name if it already exists in the track or group
			// Creates it if it does not
			// Can't use 'Find' functions because we only want to look in this one particular track or group

			// Make dummy item list
			Membership Children = new Membership(Sequence);
			// Get the parent
			MemberType parentType = parent.MemberType;
			// if parent is a group
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				// Get it's items saved index list
				Children = ((ChannelGroup)parent).Members;
			}
			else // not a group
			{
				// if parent is a track
				if (parent.MemberType == MemberType.Track)
				{
					Children = ((Track)parent).Members;
				}
				else // not a track either
				{
					string emsg = "WTF? Parent is not group or track, but should be!";
				} // end if track, or not
			} // end if group, or not

			// Create blank/null return object
			ChannelGroup ret = null;
			int gidx = 0; // loop counter
										// loop while we still have no group, and we haven't reached to end of the list
			while ((ret == null) && (gidx < Children.Count))
			{
				// Get each item's ID
				//int SI = Children.Items[gidx].SavedIndex;
				IMember part = Children.Items[gidx];
				if (part.MemberType == MemberType.ChannelGroup)
				{
					ChannelGroup group = (ChannelGroup)part;
					if (part.Name == groupName)
					{
						ret = group;
						gidx = Children.Count;
					}
				}
				gidx++;
			}

			if (ret == null)
			{
				//int si = Sequence.Members.HighestSavedIndex + 1;
				ret = Sequence.CreateChannelGroup(groupName);
				ret.Centiseconds = Sequence.Centiseconds;
				//Sequence.AddChannelGroup(ret);
				//ID = Sequence.Members.bySavedIndex[parentSI];
				if (parent.MemberType == MemberType.Track)
				{
					((Track)parent).Members.Add(ret);
				}
				if (parent.MemberType == MemberType.ChannelGroup)
				{
					((ChannelGroup)parent).Members.Add(ret);
				}
			}

			return ret;
		}

		public Channel GetChannel(string channelName, Membership parentSubItems)
		{
			// Gets existing channel specified by Name if it already exists in the group
			// Creates it if it does not
			Channel ret = null;
			IMember part = null;
			int gidx = 0;
			while ((ret == null) && (gidx < parentSubItems.Count))
			{
				part = parentSubItems.Items[gidx];
				if (part.MemberType == MemberType.Channel)
				{
					if (part.Name == channelName)
					{
						ret = (Channel)part;
						// Clear any existing effects from a previous run
						if (ret.effects.Count > 0)
						{
							ret.effects = new List<Effect>();
						}
					}
				}
				gidx++;
			}

			if (ret == null)
			{
				//int si = Sequence.Members.HighestSavedIndex + 1;
				ret = Sequence.CreateChannel(channelName);
				ret.Centiseconds = Sequence.Centiseconds;
				parentSubItems.Add(ret);
			}

			return ret;
		}

		public void CreatePolyChannels(IMember parent, string prefix, bool useGroups)
		{
			string dmsg = "";
			//Channel chan;
			int octave = 0;
			int lastOctave = 0;
			Membership parentSubs = new Membership(Sequence);
			ChannelGroup grp = new ChannelGroup("null");
			if (useGroups)
			{
				grp = GetGroup(prefix + octaveNamesA[octave], parent);
				parentSubs = grp.Members;
				//grp.identity.Centiseconds = Sequence.totalCentiseconds;
			}
			else
			{
				if (parent.MemberType == MemberType.Track)
				{
					parentSubs = ((Track)parent).Members;
				}
				else
				{
					// useGroups is false, so the parent should be a track, but it's not!
					System.Diagnostics.Debugger.Break();
				}
			}
			Array.Resize(ref noteChannels, noteNames.Length);
			for (int n = 0; n < noteNames.Length; n++)
			{
				if (useGroups)
				{
					octave = n / 12;
					if (octave != lastOctave)
					{
						// add group from last octave
						//AddChildToParent(grp, parent);
						// then create new octave group
						grp = GetGroup(prefix + octaveNamesA[octave], parent);
						//grp.identity.Centiseconds = Sequence.totalCentiseconds;
						lastOctave = octave;
						parentSubs = grp.Members;
						dmsg = "Adding Group '" + grp.Name + "' SI:" + grp.SavedIndex;
						dmsg += " Octave #" + octave.ToString();
						dmsg += " to Parent '" + parent.Name + "' SI:" + parent.SavedIndex;
						Debug.WriteLine(dmsg);
					}
				}
				Channel chan = GetChannel(prefix + noteNames[n], parentSubs);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = Sequence.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + parentSubs.owner.Name + "' SI:" + parentSubs.owner.SavedIndex;
				//Debug.WriteLine(dmsg);
				Debug.WriteLine(dmsg);


				if (n == 0)
				{
					firstCobjIdx = Sequence.Channels.Count - 1;
					firstCsavedIndex = chan.SavedIndex;
				}
			}
			if (useGroups)
			{
				//AddChildToParent(grp, parent);
			}
			Sequence.Members.ReIndex();



		}

		public int ImportTimingGrid(TimingGrid beatGrid, xTimings xEffects)
		{
			int errs = 0;
			int lastStart = -1;

			// If grid already has timings (from a previous run) clear them, start over fresh
			if (beatGrid.timings.Count > 0)
			{
				beatGrid.timings.Clear();
			}
			beatGrid.TimingGridType = TimingGridType.Freeform;
			for (int q = 0; q < xEffects.effects.Count; q++)
			{
				xEffect xef = xEffects.effects[q];
				int t = ms2cs(xef.starttime);
				if (t > lastStart)
				{
					if (t < Sequence.Centiseconds)
					{
						beatGrid.AddTiming(t);
					}
				}
				lastStart = t;
			}
			return errs;
		}

		public int ImportBeatChannel(Channel beatCh, xTimings xEffects, int barDivs, int firstBeat, bool ramps)
		{
			int errs = 0;
			// Detect if we are doing 'Bars' channel for special handling
			bool bars = false;
			int b = beatCh.Name.IndexOf("Bars");
			if (b >= 0) bars = true;
			int halfBar = 10;

			// If channel already has effects (from a previous run) clear them, start over fresh
			if (beatCh.effects.Count > 0)
			{
				beatCh.effects.Clear();
			}
			//int lb = barDivs - 1;
			for (int q = 0; q < xEffects.effects.Count; q++)
			{
				if (ramps)
				{
					xEffect xef = xEffects.effects[q];
					Effect lef = new Effect();
					lef.EffectType = EffectType.Intensity;
					lef.startIntensity = 100;
					lef.endIntensity = 0;
					lef.startCentisecond = ms2cs(xef.starttime);
					// Note: No special handling required for bars if using ramps
					lef.endCentisecond = ms2cs(xef.endtime);
					//if (q < (xEffects.effects.Count - 1))
					//{
						// Alternative
					//	lef.endCentisecond = ms2cs(xEffects.effects[q + 1].starttime);
					//}
					beatCh.AddEffect(lef);
				}
				else // On-Off, NOT ramps
				{
					int n = ((q + 1) % barDivs);
					if ((n == firstBeat) || bars)
					{
						xEffect xef = xEffects.effects[q];
						Effect lef = new Effect();
						lef.EffectType = EffectType.Intensity;
						lef.Intensity = 100;
						lef.startCentisecond = ms2cs(xef.starttime);
						// If processing 'Bars' channel without Ramps, special handling is required
						if (bars)
						{
							// Calculate a half bar for the end time
							halfBar = ms2cs(xef.endtime);
							halfBar -= lef.startCentisecond;
							halfBar /= 2;
							//halfBar = ms2cs(halfBar);
							lef.endCentisecond = (lef.startCentisecond + halfBar);
						}
						else
						{
							lef.endCentisecond = ms2cs(xef.endtime);
						}
						//if (q < (xEffects.effects.Count - 1))
						//{
						// Alternative
						//	lef.endCentisecond = ms2cs(xEffects.effects[q].starttime);
						//}
						beatCh.AddEffect(lef);
					}
				}
			} // end for loop
			return errs;
		}

		public static int ms2cs(int ms)
		{
			double c = (ms / 10);
			int cs = (int)Math.Round(c);
			return cs;
		}

		public static int ms2cs(double ms)
		{
			double c = (ms / 10);
			int cs = (int)Math.Round(c);
			return cs;
		}

		public static int ms2cs(TimeSpan ms)
		{
			double c = (ms.TotalMilliseconds / 10);
			int cs = (int)Math.Round(c);
			return cs;
		}

		public static void AddChildToParent(IMember child, IMember parent)
		{
			// Tests for, and works with either a track or a channel group as the parent
			if (parent.MemberType == MemberType.Track)
			{
				Track trk = (Track)parent;
				trk.Members.Add(child);
			}
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				ChannelGroup grp = (ChannelGroup)parent;
				grp.Members.Add(child);
			}


		}

		public static Int32 NoteColor(int note)
		{
			// Returned value is LOR color, NOT Web or .Net color!
			int hexClr = 0;
			int q = note % 12;
			switch (q)
			{
				case 0:
					hexClr = 255; // 0xFF0000; // Red
					break;
				case 1:
					hexClr = 32767; // 0xFF7F00;
					break;
				case 2:
					hexClr = 65535; // 0xFFFF00; // Yellow
					break;
				case 3:
					hexClr = 65407; // 0x7FFF00;
					break;
				case 4:
					hexClr = 65280; // 0x00FF00; // Green
					break;
				case 5:
					hexClr = 8388573; // 0x00FF7F;
					break;
				case 6:
					hexClr = 16776960; // 0x00FFFF; // Cyan
					break;
				case 7:
					hexClr = 16744192; // 0x007FFF;
					break;
				case 8:
					hexClr = 16711680; // 0x0000FF; // Blue
					break;
				case 9:
					hexClr = 16711807; // 0x7F00FF;
					break;
				case 10:
					hexClr = 16711935; // 0xFF00FF; // Magenta
					break;
				case 11:
					hexClr = 8323327; // 0xFF007F;
					break;
			}

			//int lorClr = RGBtoLOR(hexClr);
			//Debug.Write(note);
			//Debug.Write(" ");
			//Debug.Write(hexClr.ToString("X6"));
			//Debug.Write(" ");
			//Debug.WriteLine(lorClr.ToString());


			//return lorClr;
			return hexClr;
		}



	}
}
