﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LOR4;
using xLights22;
using FileHelper;

namespace UtilORama4
{
	public static class SequenceFunctions
	{
		//private static LOR4ChannelGroup octaveGroups = null;
		private static int firstCobjIdx = LOR4Admin.UNDEFINED;
		private static int firstCsavedIndex = LOR4Admin.UNDEFINED;

		// Note: these are LOR colors which are 24-bit int in BGR order
		//       (NOT .Net colors in RGB order)
		public static int[] ChannelColors = {255, 32767, 65535, 65407, 65280, 8388573, 16776960, 16744192,
																			16711680, 16711807, 16711935, 8323327 };
		// Colors are 0 = 0xFF0000; Red, 1 = 0xFF7F00; Orange, 2 = 0xFFFF00; Yellow, 3 = 0x7FFF00; Yellow-Green, 4 = 0x00FF00; Green,
		// 5 = 0x00FF7F; Green-Cyan, 6 = 0x00FFFF; Cyan, 7 = 0x007FFF; Cyan-Blue, 8 = 0x0000FF; Blue, 9 = 0x7F00FF; Blu-Magenta,
		// 10 = 0xFF00FF; Magenta, and 11 = 0xFF007F; Magenta-Red



		static SequenceFunctions()
		{

		}

		//public static int ImportTimingGrid(LOR4Timings beatGrid, xTimings markfects)
		//{
		//string grdName = beatGrid.Name;
		//return ImportTimingGrid(beatGrid, grdName, markfects);
		//}

		public static int ImportTimingGrid(LOR4Timings beatGrid, xTimings xTimes)
		{
			int errs = 0;
			int lastStart = -1;
			//string gName = beatGrid.Name;
			string xName = xTimes.Name;

			// If grid already has timings (from a previous run) clear them, start over fresh
			if (beatGrid.timings.Count > 0)
			{
				beatGrid.timings.Clear();
			}
			int tc = xTimes.Markers.Count;
			beatGrid.TimingGridType = LOR4TimingGridType.Freeform;
			for (int q = 0; q < xTimes.Markers.Count; q++)
			{
				xMarker mark = xTimes.Markers[q];
				int t = ms2cs(mark.starttime);

				if (t > Annotator.Sequence.Centiseconds) Annotator.Sequence.Centiseconds = t;
				if (t > Annotator.VampTrack.Centiseconds) Annotator.VampTrack.Centiseconds = t;

				if (t > lastStart)
				{
					if (t <= Annotator.Sequence.Centiseconds)
					{
						beatGrid.AddTiming(t);
					}
				}
				//TODO Raise event for progress bar
				lastStart = t;
			}
			if (tc != beatGrid.timings.Count)
			{
				string msg = "Warning:\r\nxTimings '" + xName + "' has ";
				msg += tc.ToString() + " effects, but\r\n";
				msg += "Timing Grid '" + beatGrid.Name + "' has ";
				msg += beatGrid.timings.Count.ToString() + " effects.\r\n";
				msg += "   (This may be because of tightly close timings)";
				Fyle.BUG(msg);
			}
			//TODO ? Output status message with count?
			return errs;
		}

		public static int LessEfficient_ImportBeatChannel(LOR4Channel beatCh, xTimings xTimes, int divider)
		{
			int errs = 0;
			// Detect if we are doing 'Bars' channel for special handling
			xMarker mark = null;
			LOR4Effect lef = null;
			bool bars = false;
			int st = 0;
			int et = 0;
			int eh = 0;
			int len = 0;
			int b = beatCh.Name.IndexOf("Bars");
			if (b >= 0) bars = true;
			int halfBar = 10;
			bool cancel = false;

			// If channel already has effects (from a previous run) clear them, start over fresh
			if (beatCh.effects.Count > 0)
			{
				beatCh.effects.Clear();
			}
			if (Annotator.UseRamps)
			{
				// Ramps are very simple, just add a fade-down effect for each effect
				for (int q = 0; q < xTimes.Markers.Count; q++)
				{
					mark = xTimes.Markers[q];
					st = ms2cs(mark.starttime);
					et = ms2cs(mark.endtime);
					lef = new LOR4Effect(LOR4EffectType.FadeDown, st, et, 100, 0);
					beatCh.AddEffect(lef);
				}
			}
			else // On-Off, NOT ramps
			{
				int q = 0;
				int efc = xTimes.Markers.Count;
				while (q < efc)
				{
					cancel = false; // reset
					mark = xTimes.Markers[q];
					st = ms2cs(mark.starttime);
					if (divider == 1)
					{
						// Quarter and Third Beats (Sixteenth and twelth notes)
						eh = ms2cs(mark.endtime);
						len = eh - st;
						et = st + (len / 2);
					} // End divider = 1;
					else
					{
						if (divider == 2)
						{
							// Half Beats (Eighth notes)

							// If not starting on the first beat
							if ((Annotator.FirstBeat == 2) || (Annotator.FirstBeat == 4))
							{
								// Start from the NEXT beat, if it exists
								if ((q + 1) < efc)
								{
									mark = xTimes.Markers[q + 1];
									st = ms2cs(mark.starttime);
									// End is the START of the NEXT beat, if it exists
									if ((q + 2) < efc)
									{
										mark = xTimes.Markers[q + 2];
										et = ms2cs(mark.starttime);
									}
									else
									{
										// If no more beats, end time is the end of the sequence
										et = Annotator.Sequence.Centiseconds;
									}
								}
								else
								{ cancel = true; }
							}
							else // Starting on the first beat (as normal)
							{
								// End time is the START of the next beat, if it exists
								if ((q + 1) < efc)
								{
									mark = xTimes.Markers[q + 1];
									et = ms2cs(mark.starttime);
								}
								else
								{
									// If no more beats, end time is the end of the sequence
									et = Annotator.Sequence.Centiseconds;
								}
							}
						}
						else // divider != 2
						{
							if (divider == 4)
							{
								// Full Beats (Quarter notes)

								// If not starting from the first beat
								int offset = Annotator.FirstBeat - 1;
								// Start from the first beat, if it exists
								if ((q + offset) < efc)
								{
									mark = xTimes.Markers[q + 1];
									st = ms2cs(mark.starttime);
									// End is the START of the 2 beats later, if it exists
									if ((q + offset + 2) < efc)
									{
										mark = xTimes.Markers[q + 2];
										et = ms2cs(mark.starttime);
									}
									else
									{
										// If note enough additional beats, end time is the end of the sequence
										et = Annotator.Sequence.Centiseconds;
									}
								}
								else
								{ cancel = true; }
							}
							else // Divider != 1, 2, or 4 - must be bars
							{
								// Bars (Beats-Per-Bar number of Quarter notes (could be 3 or 4)

								// If not starting from the first beat
								int offset = ((Annotator.FirstBeat - 1) * Annotator.BeatsPerBar);
								// Start from the first beat, if it exists
								if ((q + offset) < efc)
								{
									mark = xTimes.Markers[q + 1];
									st = ms2cs(mark.starttime);
									// End is the START of 6 (3/4 time) or 8 (4/4 time) later
									if ((q + offset + 2) < efc)
									{
										mark = xTimes.Markers[Annotator.BeatsPerBar * 2];
										et = ms2cs(mark.starttime);
									}
									else
									{
										// If note enough additional beats, end time is the end of the sequence
										et = Annotator.Sequence.Centiseconds;
									}
								}
								else
								{ cancel = true; }
							} // End divider = 4 (or not)
						} // End divider = 2 (or not)
					} // End divider = 1 (or not)
					if (!cancel)
					{
						lef = new LOR4Effect(LOR4EffectType.Intensity, st, et, 100);
						beatCh.effects.Add(lef);
					} // End if not cancelled
					q += divider;
				} // End while q < effects count
			} // end ramps or on-off
				//return errs;
			return beatCh.effects.Count;
		}

		public static int ImportBeatChannel(LOR4Channel beatCh, xTimings xTimes, int divider)
		{
			//int errs = 0;
			// Detect if we are doing 'Bars' channel for special handling
			xMarker mark = null;
			LOR4Effect lorEffect = null;
			//bool bars = false;
			int effectStart = 0;
			int effectEnd = 0;
			//int eh = 0;
			//int len = 0;
			//int b = beatCh.Name.IndexOf("Bars");
			//if (b >= 0) bars = true;
			//int halfBar = 10;
			//bool cancel = false;

			// If channel already has effects (from a previous run) clear them, start over fresh
			if (beatCh.effects.Count > 0)
			{
				beatCh.effects.Clear();
			}
			if (Annotator.UseRamps)
			{
				// Ramps are very simple, just add a fade-down effect for each effect
				for (int q = 0; q < xTimes.Markers.Count; q++)
				{
					mark = xTimes.Markers[q];
					effectStart = ms2cs(mark.starttime);
					effectEnd = ms2cs(mark.endtime);
					lorEffect = new LOR4Effect(LOR4EffectType.FadeDown, effectStart, effectEnd, 100, 0);
					beatCh.AddEffect(lorEffect);
					//TODO Raise event for progress bar
				}
			}
			else // On-Off, NOT ramps
			{
				int div = divider - 1;
				int ofs = Annotator.FirstBeat - 1;
				int j = ofs * divider;
				// Index of effect to get start time
				int effectIndexStart = j * div;
				int effectCount = xTimes.Markers.Count;
				while (effectIndexStart < effectCount)
				{
					mark = xTimes.Markers[effectIndexStart];
					effectStart = ms2cs(mark.starttime);
					if (divider < 2)
					{
						int xEnd = ms2cs(mark.endtime);
						int len = xEnd - effectStart;
						effectEnd = effectStart + (len / 2);
					}
					else
					{
						int effectIndexEnd = effectIndexStart + (divider / 2);
						if ((effectIndexEnd) < effectCount)
						{
							mark = xTimes.Markers[effectIndexEnd];
							effectEnd = ms2cs(mark.starttime);
						}
						else
						{
							// If note enough additional beats, end time is the end of the sequence
							effectEnd = Annotator.Sequence.Centiseconds;
						}
					}
					lorEffect = new LOR4Effect(LOR4EffectType.Intensity, effectStart, effectEnd, 100);
					beatCh.effects.Add(lorEffect);
					effectIndexStart += divider;
					//TODO Raise event for progress bar
				} // End while effectIndexStart < effect count
			} // End while q < effects count
				//TODO Output status message
			return beatCh.effects.Count;
		}



		public static int ImportNoteChannel(LOR4Channel noteCh, xTimings xTimes)
		{
			int errs = 0;
			// Detect if we are doing 'Bars' channel for special handling
			xMarker mark = null;
			LOR4Effect lef = null;
			bool bars = false;
			int st = 0;
			int et = 0;

			// If channel already has effects (from a previous run) clear them, start over fresh
			if (noteCh.effects.Count > 0)
			{
				noteCh.effects.Clear();
			}
			for (int q = 0; q < xTimes.Markers.Count; q++)
			{
				mark = xTimes.Markers[q];
				st = ms2cs(mark.starttime);
				et = ms2cs(mark.endtime);
				if (Annotator.UseRamps)
				{
					// Ramps are very simple, just add a fade-down effect for each effect
					lef = new LOR4Effect(LOR4EffectType.FadeDown, st, et, 100, 0);
				}
				else
				{
					lef = new LOR4Effect(LOR4EffectType.Intensity, st, et, 100);
				}
				noteCh.AddEffect(lef);
				//TODO Raise event for progress bar
			}
			//TODO Output status message with count
			return noteCh.effects.Count;
		}




		public static int OLD2_ImportBeatChannel(LOR4Channel beatCh, xTimings xTimes, int barDivs, int firstBeat, bool ramps)
		{
			int errs = 0;
			// Detect if we are doing 'Bars' channel for special handling
			xMarker mark = null;
			LOR4Effect lef = null;
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
			for (int q = 0; q < xTimes.Markers.Count; q++)
			{
				mark = xTimes.Markers[q];
				int st = ms2cs(mark.starttime);
				int et = ms2cs(mark.endtime);
				if (ramps)
				{
					lef = new LOR4Effect(LOR4EffectType.FadeDown, st, et, 100, 0);
					beatCh.AddEffect(lef);
				}
				else // On-Off, NOT ramps
				{
					int n = ((q + 1) % barDivs);
					if ((n == firstBeat) || bars)
					{
						mark = xTimes.Markers[q];
						st = ms2cs(mark.starttime);
						et = ms2cs(mark.endtime);
						if (et > Annotator.Sequence.Centiseconds) Annotator.Sequence.Centiseconds = et;
						if (et > Annotator.VampTrack.Centiseconds) Annotator.VampTrack.Centiseconds = et;
						if (et > beatCh.Centiseconds) beatCh.Centiseconds = et;

						lef = new LOR4Effect();
						lef.EffectType = LOR4EffectType.Intensity;
						lef.Intensity = 100;
						lef.startCentisecond = st;
						// If processing 'Bars' channel without Ramps, special handling is required
						if (bars)
						{
							// Calculate a half bar for the end time
							halfBar = et;
							halfBar -= st;
							halfBar /= 2;
							//halfBar = ms2cs(halfBar);
							lef.endCentisecond = (st + halfBar);
						}
						else
						{
							lef.endCentisecond = et;
						}
						//if (q < (xTimes.Markers.Count - 1))
						//{
						// Alternative
						//	lef.endCentisecond = ms2cs(xTimes.Markers[q].starttime);
						//}
						beatCh.AddEffect(lef);
					}

				}
			} // end for loop
			return errs;
		}

		public static int ms2cs(int ms)
		{
			//double c = (ms / 10);
			//int cs = (int)Math.Round(c);
			return (int)Math.Round(ms / 10D);
		}

		public static int ms2cs(double ms)
		{
			//double c = (ms / 10);
			//int cs = (int)Math.Round(c);
			return (int)Math.Round(ms / 10D);
		}

		public static int ms2cs(TimeSpan ms)
		{
			//double c = (ms.TotalMilliseconds / 10);
			//int cs = (int)Math.Round(c);
			return (int)Math.Round(ms.TotalMilliseconds / 10D);
		}

		public static void AddChildToParent(iLOR4Member child, iLOR4Member parent)
		{
			// Tests for, and works with either a track or a channel group as the parent
			if (parent.MemberType == LOR4MemberType.Track)
			{
				LOR4Track trk = (LOR4Track)parent;
				trk.Members.Add(child);
			}
			if (parent.MemberType == LOR4MemberType.ChannelGroup)
			{
				LOR4ChannelGroup grp = (LOR4ChannelGroup)parent;
				grp.Members.Add(child);
			}


		}


		public static int ChannelColor(int index)
		{
			// Make sure index is > 0 and get modulus of 12 ('cuz there's 12 colors!)
			int n = (index + 1200) % 12;
			return ChannelColors[n];

		}

		/*
		public static int GetKeyColor(int keyNumber)
		{
			int idx = keyNumber - 1;
			idx %= 12; ;
			return NoteColors[idx];
		}

		public static int GetNoteColor(int noteNum)
		{
			int nn = noteNum % 12;
			return NoteColors[noteNum];
		}
		*/

		public static int NoteColor_BAD(int noteNum)
		{
			// Returned value is LOR color, NOT Web or .Net color!
			int hexClr = 0;
			int q = noteNum % 12;
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
			return hexClr;
		}


		/*
				public static LOR4Channel[] CreatePolyChannels()  // iLOR4Member parent, string prefix, bool useGroups)
		//public LOR4ChannelGroup CreatePolyChannels(iLOR4Member parent, string prefix, bool useGroups)
		{
			// Returns an array of Channels of (empty) channels for Polyphonic Transcription

			string prefix = "Note ";
			string dmsg = "";
			//LOR4Channel chan;
			int octave = 0;
			int lastOctave = 0;
			//LOR4Membership parentSubs = new LOR4Membership(Sequence);
			//LOR4ChannelGroup grp = new LOR4ChannelGroup("null");
			LOR4Channel[] polyChannels = null;
			Array.Resize(ref polyChannels, MusicalNotation.noteNamesUnicode.Length);
			//if (useGroups)
			//{
			//	grp = GetGroup(prefix + MusicalNotation.octaveNamesA[octave], parent);
			//	parentSubs = grp.Members;
				//grp.identity.Centiseconds = Sequence.totalCentiseconds;
			//}
			//else
			//{
			//	if (parent.MemberType == LOR4MemberType.Track)
			//	{
			//		parentSubs = ((LOR4Track)parent).Members;
			//	}
			//	else
			//	{
			//		// useGroups is false, so the parent should be a track, but it's not!
			//		System.Diagnostics.Debugger.Break();
			//	}
			//}
			//Array.Resize(ref noteChannels, MusicalNotation.noteNamesUnicode.Length);
			for (int n = 0; n < MusicalNotation.noteNamesUnicode.Length; n++)
			{
				//if (useGroups)
				//{
				//	octave = n / 12;
				//	if (octave != lastOctave)
				//	{
				//		// add group from last octave
				//		//AddChildToParent(grp, parent);
				//		// then create new octave group
				//		grp = GetGroup(prefix + MusicalNotation.octaveNamesA[octave], parent);
				//		//grp.identity.Centiseconds = Sequence.totalCentiseconds;
				//		lastOctave = octave;
				//		parentSubs = grp.Members;
				//		dmsg = "Adding Group '" + grp.Name + "' SI:" + grp.SavedIndex;
				//		dmsg += " Octave #" + octave.ToString();
				//		dmsg += " to Parent '" + parent.Name + "' SI:" + parent.SavedIndex;
				//		Debug.WriteLine(dmsg);
				//	}
				//}
				//LOR4Channel chan = GetChannel(prefix + MusicalNotation.noteNamesUnicode[n], parentSubs);
				LOR4Channel chan = new LOR4Channel(prefix + MusicalNotation.noteNamesUnicode[n]);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = Sequence.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				//dmsg += " to Parent '" + parentSubs.Owner.Name + "' SI:" + parentSubs.Owner.SavedIndex;
				//Debug.WriteLine(dmsg);
				Debug.WriteLine(dmsg);


				if (n == 0)
				{
					firstCobjIdx = Sequence.Channels.Count - 1;
					firstCsavedIndex = chan.SavedIndex;
				}
			}
			//if (useGroups)
			//{
			//AddChildToParent(grp, parent);
			//}
			//Sequence.Members.ReIndex();

			return polyChannels;

		}

		public static LOR4ChannelGroup[] CreatePolyOctaveGroups()
		{
			string prefix = "Octave ";
			LOR4ChannelGroup[] polyGroups = null;
			Array.Resize(ref polyGroups, MusicalNotation.octaveNamesA.Length);
			for (int n = 0; n < MusicalNotation.noteNamesUnicode.Length; n++)
			{
				LOR4ChannelGroup grp = new LOR4ChannelGroup(prefix + MusicalNotation.octaveNamesA[n]);
			}
			return polyGroups;
		}

		public static LOR4ChannelGroup PutNonEmptyPolyChannelsIntoPolyOctaveGroups(LOR4Channel[] polyChannels, LOR4ChannelGroup[] polyGroups)
		{
			// Returns the parent group holding non-empty subgroups holding non-empty channels ...
			//  ... if there is no errors!
			//    Still returns the parent group if there are errors, but it will be empty
			//     furthermore, the group name will indicate the error
			int err = 0;
			LOR4ChannelGroup parentGroup = new LOR4ChannelGroup("Polyphonic Transcription");
			int keepCount = 0;
			if (polyChannels.Length != 128)
			{
				err = 1;
				parentGroup.ChangeName("Err 1: Not 128 Note Channels");
			}
			else
			{
				if (polyGroups.Length < 12)
				{
					err = 2;
					parentGroup.ChangeName("Err 2: Not enough Octave Groups");
				}
				else
				{
					for (int n=0; n<128; n++)
					{
						int g = n / 12;
						if (polyChannels[n].effects.Count > 0)
						{
							polyGroups[g].Members.Add(polyChannels[n]);
							keepCount++;
						}
					}
					if (keepCount == 0)
					{
						err = 3;
						parentGroup.ChangeName("Err 3: Note Channels are all empty");
					}
					else
					{
						for (int g = 0; g < 12; g++)
						{
							if (polyGroups[g].Members.Count > 0)
							{
								parentGroup.Members.Add(polyGroups[g]);
							}								
						}
					}
				}
			}
			return parentGroup;
		}


 
		*/

	}
}
