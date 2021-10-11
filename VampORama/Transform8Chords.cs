using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using LORUtils4;
using FileHelper;
using xUtilities;

namespace UtilORama4
{
	//! Chords
	static class VampChords //: ITransform
	{
		public const string transformName = "Chords";
		public const int PLUGINNotes = 0;
		public const int PLUGINSimple = 1;

		public static int lastPluginUsed = PLUGINNotes;

		public const int METHOD1frequency = 0;
		public const int METHOD1spectral = 1;
		public const int METHOD1phase = 2;
		public const int METHOD1domain = 3;
		public const int METHOD1energy = 4;

		public const int METHOD2energy = 0;
		public const int METHOD2spectral = 1;
		public const int METHOD2frequency = 2;
		public const int METHOD2domain = 3;
		public const int METHOD2phase = 4;
		public const int METHOD2kullback = 5;
		public const int METHOD2modKulback = 6;
		public const int METHOD2flux = 7;
		public static string PolyNoteNamePrefix = "";
		public static string PolyGroupNamePrefix = "";
		public static string ResultsName = "Chords.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;

		public static xTimings xChords = new xTimings(transformName);
		public static xTimings alignTimes = null;

		public static vamps.LabelType LabelType = vamps.LabelType.NoteNamesUnicode;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.BeatsQuarter;

		public static readonly string[] availablePluginNames = { "Chordino Chord Notes",
																														"Chordino Simple Chord" };

		public static readonly string[] availablePluginCodes = {"vamp:nnls-chroma:chordino:chordnotes",
																														"vamp:nnls-chroma:chordino:simplechord" };

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter,
																																					vamps.AlignmentType.BeatsFull,
																																					vamps.AlignmentType.Bars };
		
		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None,
																																vamps.LabelType.NoteNamesUnicode,
																																vamps.LabelType.NoteNamesASCII,
																																vamps.LabelType.MIDINoteNumbers,
																																vamps.LabelType.Frequency };

		public static readonly string[] filesAvailableConfigs = {"vamp_nnls-chroma_chordino_chordnotes.n3",
																														"vamp_nnls-chroma_chordino_simplechord.n3" };


		private static LORChannelGroup4 chordGroup = null;
		public static LORChannel4[] chordChannels = null;

		private static int idx = 0;
		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static xTimings Timings
		{
			get
			{
				return xChords;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.Chords;
			}
		}

		public static string TransformationName
		{
			get
			{
				return transformName;
			}
		}

		public static int PrepareToVamp(string fileSong, int pluginIndex,	int detectionMethod = METHOD1domain)
		{
			// Song file should have already been copied to the temp folder and named song.mp3
			// Annotator will use the same folder the song is in for it's files
			// Returns the name of the results file, which will also be in the same temp folder

			int err = 0;
			string fileConfig = filesAvailableConfigs[pluginIndex];
			//string vampParams = availablePluginCodes[pluginIndex];
			string pathConfigs = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			//string pathWork = Path.GetDirectoryName(fileSong) + "\\";
			lastPluginUsed = pluginIndex;

			int lineCount = 0;
			string lineIn = "";
			//string fileConfigFrom = ""; // vampConfigs;
			//string fileConfigTo = ""; // tempPath;
			string vampFile = "";
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;

			string fileConfigFrom = pathConfigs + fileConfig;
			string fileConfigTo = Annotator.TempPath + fileConfig;
			idx = 0; // Reset for later

			//string tempPath = Path.GetDirectoryName(fileSong);
			//string vampParams = availablePluginNames[pluginIndex];
			//vampFile = vampParams.Replace(':', '_') + ".n3";
			//fileConfigFrom += vampFile;
			//fileConfigTo = tempPath + vampFile;

			try
			{
				switch (pluginIndex)
				{
					/*
					case PLUGINQueenMary:
						// Queen Mary Chords
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("441", Annotator.StepSize.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;
					*/

					case PLUGINNotes:
						// Constant-Q Chords (think this is also from Queen Mary)
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINSimple:
						// NNLS (What does that stand for? ) by Matthias Mauch
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					default:
						Fyle.BUG("Unrecognized Plugin Index, using default!");
						//string msg = "Alicante Chords Transcription is not currently available.  Using Queen Mary Transcription instead.";
						//DialogResult dr = MessageBox.Show(msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						//if (Fyle.DebugMode) System.Diagnostics.Debugger.Break();
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;
				}
			} // End try
			catch (Exception e)
			{
				err = e.HResult;
				string m = e.Message;
				if (Fyle.DebugMode)
				{
					System.Diagnostics.Debugger.Break();
				}
			}


			if (err == 0)
			{
				//results = AnnotateSong("XX", songFile, fileConfigTo, reuse);
			}

			return err;
		}

		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelType labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string noteLabel = "";
			int duration = 0;
			int note = 0;
			int lastNote = -1;
			int eStart = 0;
			int eEnd = 0;
			int lastStart = -1;
			int lastEnd = -1;
			string lastLabel = ""; 
			string[] parts = null;
			string cName = "";
			int pcount = 1;

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);
			
			string msg = "\r\n\r\n### PROCESSING CHORDS TRANSCRIPTION ####################################";
			Debug.WriteLine(msg);

			xChords.effects.Clear();

			//? CHORD NOTES PLUGIN
			if (lastPluginUsed == PLUGINNotes)
			{
				StreamReader reader = new StreamReader(resultsFile);
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					parts = lineIn.Split(',');
					if (parts.Length > 2)
					{
						millisecs = xUtils.ParseMilliseconds(parts[0]);
						eStart = Annotator.AlignTime(millisecs);
						duration = xUtils.ParseMilliseconds(parts[1]);
						int ee = eStart + duration;
						eEnd = Annotator.AlignTime(ee);
						// Note: Unlike other annotator transforms, we will NOT be checking to see
						// if this note starts AFTER the last one, or if the end is after it
						// since for Chords there are multiple simultaneous notes which is perfectly legitimate
						int.TryParse(parts[2], out note);
						if ((note > 0) && (note < 127))
						{
							noteLabel = MusicalNotation.noteNamesUnicode[note]; // Default
							if (LabelType == vamps.LabelType.NoteNamesASCII)
							{ noteLabel = MusicalNotation.noteNamesASCII[note]; }
							if (LabelType == vamps.LabelType.Frequency)
							{ noteLabel = MusicalNotation.noteFreqs[note]; }
							if (LabelType == vamps.LabelType.MIDINoteNumbers)
							{ noteLabel = note.ToString(); }

							if ((eStart == lastStart) & (eEnd == lastEnd))
							{
								string lbl = xChords.effects[xChords.effects.Count - 1].Label;
								noteLabel = lbl + "; " + noteLabel;
								xChords.effects[xChords.effects.Count - 1].Label = noteLabel;
							}
							else
							{
								pcount++;
								xChords.Add(noteLabel, eStart, eEnd, note);
								lastStart = eStart;
								lastEnd = eEnd;
							}
							lastLabel = noteLabel;
						}
					} // end line contains a period
				} // End ChordNotes
				reader.Close();
			}
			
			//? SIMPLE CHORDS PLUGIN
			if (lastPluginUsed == PLUGINSimple)
			{
				StreamReader reader = new StreamReader(resultsFile);
				// First, lets get the first line of the file which has the start time of the first
				// tempo and it's BPM
				if (!reader.EndOfStream)
				{
					// Field 1 Part 0 is the start time
					// Field 2 Part 1 is the chord name
					lineIn = reader.ReadLine();
					parts = lineIn.Split(',');
					if (parts.Length > 1)
					{
						// Try parsing fields 0 start time (in decimal seconds)
						millisecs = xUtils.ParseMilliseconds(parts[0]);
						eStart = Annotator.AlignTime(millisecs);
						// Get Name of chord
						cName = parts[1];
						cName = cName.Replace("\"", "");
					}
				}

				// Now get the rest of the lines from the file, with the start times and BPM of each subsequent
				// change in tempo
				while ((lineIn = reader.ReadLine()) != null)
				{
					parts = lineIn.Split(',');
					if (parts.Length > 1)
					{
						// Try parsing fields 0 start time (in decimal seconds)
						millisecs = xUtils.ParseMilliseconds(parts[0]);
						eEnd = Annotator.AlignTime(millisecs);
						if (eEnd < eStart) // Data integrity check
						{
							Fyle.BUG("Backwards Chord?!?!");
						}
						else
						{
							// After Aligning to Bars (or other long time) we may end up with a tempo change of Zero length
							// if it was shorter than a Bar to begin with
							if (eEnd > eStart)
							{
								// Default name is the BPM Number
								if (cName != "N") // No chord, Nothing, Null
								{
									// Create new xEffect with these values, add to timings list
									xEffect xef = new xEffect(cName, eStart, eEnd, pcount);
									xChords.effects.Add(xef);
								}

								// End of this one is the start of the next one
								eStart = eEnd;
								// Get Name for the NEXT one
								cName = parts[1];
								cName = cName.Replace("\"", "");
								pcount++;
							} // End chord length
						} // End chord forward
					} // End enough parts
				} // end while loop more lines remaining
				reader.Close();

				// Finally, if not at the end-- add one last effect containing the last Pitch or Key
				if (eEnd < Annotator.songTimeMS)
				{
					eEnd = Annotator.songTimeMS;
					if (cName != "N")
					{
						xEffect xef = new xEffect(cName, eStart, eEnd, pcount);
						xChords.effects.Add(xef);
					}
				}


			}
			//TODO: Raise event for progress bar
			return pcount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xChords != null)
			{
				if (xChords.effects.Count > 0)
				{
					// Note: Number '5' is the same as None Onsets because this returns, effectively, the same results
					string gridName = "5 " + transformName;
					LORTimings4 polyGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(polyGrid, xChords);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			//! DO NOT USE THIS FUNCTION, USE ResultsToLORChannels BELOW

			int errs = 0;
			int grpNum = -1;
			int chordCount = 128;
			int lastStart = -1;
			if (LabelType == vamps.LabelType.NoteNamesASCII) chordCount = MusicalNotation.noteNamesASCII.Length;
			if (LabelType == vamps.LabelType.NoteNamesUnicode) chordCount = MusicalNotation.noteNamesUnicode.Length;

			// Part 1
			// Get Track, Polyphonic Group, Octave Groups, and Poly Channels
			chordGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
			Array.Resize(ref chordChannels, chordCount);
			LORChannelGroup4 octoGroup = null;
			for (int n = 0; n < chordCount; n++)
			{
				int g2 = (int)n / 12;
				if (g2 != grpNum)
				{
					octoGroup = Annotator.Sequence.FindChannelGroup(PolyGroupNamePrefix + MusicalNotation.octaveNamesA[g2], chordGroup.Members, true); ;
					grpNum = g2;
				}
				string noteName = n.ToString();
				if (LabelType == vamps.LabelType.NoteNamesASCII) noteName = MusicalNotation.noteNamesASCII[n];
				if (LabelType == vamps.LabelType.NoteNamesUnicode) noteName = MusicalNotation.noteNamesUnicode[n];

				LORChannel4 chs = Annotator.Sequence.FindChannel(PolyNoteNamePrefix + noteName, octoGroup.Members, true, true);
				chs.color = SequenceFunctions.ChannelColor(n);
				chs.effects.Clear();
				chordChannels[n] = chs;
			}

			// Part 2
			// Create an effect in the appropriate Note Poly Channel for each timing
			if (xChords != null)
			{
				if (xChords.effects.Count > 0)
				{
					for (int f = 0; f < xChords.effects.Count; f++)
					{
						xEffect timing = xChords.effects[f];
						int note = timing.Number;
						if (note < 1) // Data integrity check
						{
							// In theory, its possible to have a note with a MIDI number of 0, but really really really unlikely
							Fyle.BUG("Invalid Note MIDI number!");
						}
						else
						{
							int csStart = (int)Math.Round(timing.starttime / 10D);
							int csEnd = (int)Math.Round(timing.endtime / 10D);
							if (csEnd <= csStart) // Data integrity check
							{
								Fyle.BUG("Backwards Note?!?!");
							}
							else
							{
								if (csStart < lastStart) // Data integrity check
								{
									Fyle.BUG("Out of order notes!");
								}
								else
								{
									LOREffect4 eft = null;
									if (Annotator.UseRamps)
									{
										eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
									}
									else
									{
										eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd);
									}
									chordChannels[note].effects.Add(eft);
								}
								lastStart = csStart;
							}
						}
						//TODO Raise event for progress bar
					}
				}
			}

			// Part 3
			// Get rid of the empty ones
			for (int g1 = 0; g1 < chordGroup.Members.Count; g1++)
			{
				if (chordGroup.Members[g1].MemberType == LORMemberType4.ChannelGroup)
				{
					octoGroup = (LORChannelGroup4)chordGroup.Members[g1];
					for (int n1 = 0; n1 < octoGroup.Members.Count; n1++)
					{
						if (octoGroup.Members[n1].MemberType == LORMemberType4.Channel)
						{
							LORChannel4 ch = (LORChannel4)octoGroup.Members[n1];
							if (ch.effects.Count < 1)
							{
								octoGroup.Members.Items.RemoveAt(n1);
								n1--;
							}
						}
					}
					if (octoGroup.Members.Count < 1)
					{
						chordGroup.Members.Items.RemoveAt(g1);
						g1--;
					}
				}
			}

			Annotator.Sequence.CentiFix();
			//TODO Output status message with count(s)
			return errs;
		}

		public static int ResultsToLORChannels()
		{
			int pcount = 0;
			int millisecs = 0;
			string noteLabel = "";
			int duration = 0;
			int note = 0;
			int lastNote = -1;
			int msStart = 0;
			int msEnd = 0;
			int csStart = 0;
			int csEnd = 0;
			int lastStart = -1;
			int lastEnd = -1;
			string lastLabel = "";
			int noteCount = 0;

			string resultsFile = Annotator.TempPath + ResultsName;
			if (Fyle.Exists(resultsFile))
			{

				string lineIn = "";
				int ppos = 0;
				int centisecs = 0;
				string[] parts;
				int ontime = 0;
				//int note = 0;
				LORChannel4 ch;
				LOREffect4 ef;
				int grpNum = -1;
				
				Annotator.SetAlignment(AlignmentType);

				if (lastPluginUsed == PLUGINNotes)
				{
					// Part 1
					// Get Track, Chord Group, Octave Groups, and Note Channels
					int chordCount = 128;
					if (LabelType == vamps.LabelType.NoteNamesASCII) chordCount = MusicalNotation.noteNamesASCII.Length;
					if (LabelType == vamps.LabelType.NoteNamesUnicode) chordCount = MusicalNotation.noteNamesUnicode.Length;
					chordGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
					Array.Resize(ref chordChannels, chordCount);
					LORChannelGroup4 octoGroup = null;
					for (int n = 0; n < chordCount; n++)
					{
						int g2 = (int)n / 12;
						if (g2 != grpNum)
						{
							octoGroup = Annotator.Sequence.FindChannelGroup(PolyGroupNamePrefix + MusicalNotation.octaveNamesA[g2], chordGroup.Members, true); ;
							grpNum = g2;
						}
						string noteName = n.ToString();
						if (LabelType == vamps.LabelType.NoteNamesASCII) noteName = MusicalNotation.noteNamesASCII[n];
						if (LabelType == vamps.LabelType.NoteNamesUnicode) noteName = MusicalNotation.noteNamesUnicode[n];

						LORChannel4 chs = Annotator.Sequence.FindChannel(PolyNoteNamePrefix + noteName, octoGroup.Members, true, true);
						chs.color = SequenceFunctions.ChannelColor(n);
						chs.effects.Clear();
						chordChannels[n] = chs;
					}

					// Part 2, Read in the file and add effects to note channels
					StreamReader reader = new StreamReader(resultsFile);

					while ((lineIn = reader.ReadLine()) != null)
					{
						//TODO Raise event here to update progress bar
						parts = lineIn.Split(',');
						if (parts.Length > 2)
						{
							millisecs = xUtils.ParseMilliseconds(parts[0]);
							msStart = Annotator.AlignTime(millisecs);
							duration = xUtils.ParseMilliseconds(parts[1]);
							int ee = msStart + duration;
							msEnd = Annotator.AlignTime(ee);
							// Convert Milliseconds to Centiseconds
							csStart = (int)Math.Round(msStart / 10D);
							csEnd = (int)Math.Round(msEnd / 10D);
							// Note: Unlike other annotator transforms, we will NOT be checking to see
							// if this chord starts AFTER the last one, or if the end is after it
							// since for Chords there are multiple simultaneous notes which is perfectly legitimate
							int.TryParse(parts[2], out note);
							if ((note > 0) && (note < 127))
							{
								noteLabel = MusicalNotation.noteNamesUnicode[note]; // Default
								if (LabelType == vamps.LabelType.NoteNamesASCII)
								{ noteLabel = MusicalNotation.noteNamesASCII[note]; }
								if (LabelType == vamps.LabelType.Frequency)
								{ noteLabel = MusicalNotation.noteFreqs[note]; }
								if (LabelType == vamps.LabelType.MIDINoteNumbers)
								{ noteLabel = note.ToString(); }

								LOREffect4 eft = null;
								//if (Annotator.UseRamps)
								//{
								//	eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
								//}
								//else
								//{
									eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd);
								//}
								chordChannels[note].effects.Add(eft);
								noteCount++;
								lastStart = msStart;
								lastEnd = msEnd;
							}
						} // end line contains a period
					} // end while loop more lines remaining
					reader.Close();

					// Part 3
					// Get rid of the empty ones
					for (int g1 = 0; g1 < chordGroup.Members.Count; g1++)
					{
						if (chordGroup.Members[g1].MemberType == LORMemberType4.ChannelGroup)
						{
							octoGroup = (LORChannelGroup4)chordGroup.Members[g1];
							for (int n1 = 0; n1 < octoGroup.Members.Count; n1++)
							{
								if (octoGroup.Members[n1].MemberType == LORMemberType4.Channel)
								{
									ch = (LORChannel4)octoGroup.Members[n1];
									if (ch.effects.Count < 1)
									{
										octoGroup.Members.Items.RemoveAt(n1);
										n1--;
									}
								}
							}
							if (octoGroup.Members.Count < 1)
							{
								chordGroup.Members.Items.RemoveAt(g1);
								g1--;
							}
						}
					}
				}


				if (lastPluginUsed == PLUGINSimple)
				{
					string cName = "";
					// Part 1
					// Get Track and Chord Group
					chordGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);

					StreamReader reader = new StreamReader(resultsFile);
					// First, lets get the first line of the file which has the start time of the first chord
					if (!reader.EndOfStream)
					{
						// Field 1 Part 0 is the start time
						// Field 2 Part 1 is the chord name
						lineIn = reader.ReadLine();
						parts = lineIn.Split(',');
						if (parts.Length > 1)
						{
							// Try parsing fields 0 start time (in decimal seconds)
							millisecs = xUtils.ParseMilliseconds(parts[0]);
							msStart = Annotator.AlignTime(millisecs);
							// Get Name of chord
							cName = parts[1];
							cName = cName.Replace("\"", "");
						}
					}

					// Now get the rest of the lines from the file, with the start times and chord
					while ((lineIn = reader.ReadLine()) != null)
					{
						parts = lineIn.Split(',');
						if (parts.Length > 1)
						{
							// Try parsing fields 0 start time (in decimal seconds)
							millisecs = xUtils.ParseMilliseconds(parts[0]);
							msEnd = Annotator.AlignTime(millisecs);
							if (msEnd < msStart) // Data integrity check
							{
								Fyle.BUG("Backwards Chord?!?!");
							}
							else
							{
								// After Aligning to Bars (or other long time) we may end up with a chord of Zero length
								// if it was shorter than a Bar to begin with
								if (cName != "N") // No chord, Nothing, Null
								{
									csStart = (int)Math.Round(msStart / 10D);
									csEnd = (int)Math.Round(msEnd / 10D);

									LORChannel4 chordChan = Annotator.Sequence.FindChannel(cName, chordGroup.Members, true, true);
									chordChan.color = ChordColor(cName);
									// Create new LOR Effect with these values, add to timings list
									LOREffect4 eft = null;
									//if (Annotator.UseRamps)
									//{
									//	eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
									//}
									//else
									//{
										eft = new LOREffect4(LOREffectType4.Intensity,csStart, csEnd);
									//}
									chordChan.effects.Add(eft);
								}

								// End of this one is the start of the next one
								msStart = msEnd;
								// Get Name for the NEXT one
								cName = parts[1];
								cName = cName.Replace("\"", "");
								pcount++;
							} // End chord forward
						} // End enough parts
					} // end while loop more lines remaining
					reader.Close();

					// Finally, if not at the end-- add one last effect containing the last Chord
					if (msEnd < Annotator.songTimeMS)
					{
						msEnd = Annotator.songTimeMS;
						if (cName != "N")
						{
							csStart = (int)Math.Round(msStart / 10D);
							csEnd = (int)Math.Round(msEnd / 10D);

							LORChannel4 chordChan = Annotator.Sequence.FindChannel(cName, chordGroup.Members, true, true);
							chordChan.color = ChordColor(cName);
							// Create new LOR Effect with these values, add to timings list
							LOREffect4 eft = null;
							//if (Annotator.UseRamps)
							//{
							//	eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
							//}
							//else
							//{
								eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd);
							//}
							chordChan.effects.Add(eft);
						}
					}

				}
				Annotator.Sequence.CentiFix();
				//TODO Raise event to update status & provide count
			}
			return pcount;
		}

		public static int ChordColor(string chordName)
		{
			int ret = lutils.LORCOLOR_BLK;
			int i = -2;
			if (chordName.Length > 1)
			{
				string n = chordName.Substring(0, 2);
				if ((n == "C#") | (n == "Db"))
				{
					i = 1;
				}
				else
				{
					if ((n == "D#") | (n == "Eb"))
					{
						i = 3;
					}
					else
					{
						if ((n == "F#") | (n == "gb"))
						{
							i = 6;
						}
						else
						{
							if ((n == "G#") | (n == "Ab"))
							{
								i = 8;
							}
							else
							{
								if ((n == "A#") | (n == "Bb"))
								{
									i = 1;
								}
							}
						}
					}
				}
				if (i < 0)
				{
					n = chordName.Substring(0, 1);
					if (n == "C")
					{
						i = 0;
					}
					else
					{
						if (n == "D")
						{
							i = 2;
						}
						else
						{
							if (n == "E")
							{
								i = 4;
							}
							else
							{
								if (n == "F")
								{
									i = 5;
								}
								else
								{
									if (n == "G")
									{
										i = 7;
									}
									else
									{
										if (n == "A")
										{
											i = 9;
										}
										else
										{
											if (n == "B")
											{
												i = 11;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (i>= 0)
			{
				ret = SequenceFunctions.ChannelColor(i);
			}

			return ret;
		}



	}
}
