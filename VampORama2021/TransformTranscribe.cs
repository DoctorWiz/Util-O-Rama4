using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LORUtils4; using FileHelper;
using xUtilities;

namespace UtilORama4
{
	//! POLYPHONIC TRANSCRIPTION
	class VampPolyphonic : ITransform
	{
		public const int PLUGINqmTranscribe = 0;
		public const int PLUGINSilvet = 1;
		public const int PLUGINalicante = 2;
		public const int PLUGINaubioTracker = 3;

		public int lastPluginUsed = PLUGINqmTranscribe;

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

		public xTimings XPolyphonic = new xTimings("Polyphonic Transcription");
		public int BeatsPerBar = 4;
		public int FirstBeat = 1;
		public bool ReuseResults = false;
		public xTimings alignTimes = null;

		public readonly string[] availablePluginNames = { "Queen Mary Polyphonic Transcription",
																											"Silvet Note Transcription",
																											"Aubio Note Tracker",
		//																											"#Alicante Note Onset Detector",
																												"#Alicante Polyphonic Transcription" };

		public readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

	public readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																										"vamp_silvet_silvet_onsets.n3",
																										"vamp_vamp-aubio_aubionotes_notes.n3" };

	public static readonly string[] availableLabels = {"None",
																								"Note Names ASCII",
																								"Note Names Unicode",
																								"Midi Note Numbers" };

		private LORChannelGroup4 polyGroup = null;
		public static LORChannel4[] polyChannels = null;


		private SequenceFunctions seqFunct = new SequenceFunctions();
		private static int idx = 0;

		public VampPolyphonic()
		{
			// Constructor

			//polyChannels = seqFunct.CreatePolyChannels();
		}

		public string[] AvailablePluginNames
		{
			get
			{
				return availablePluginNames;
			}
		}


		public int UsePlugin
		{
			set
			{
				lastPluginUsed = value;
				if (lastPluginUsed < 0) lastPluginUsed = 0;
				if (lastPluginUsed >= availablePluginNames.Length) lastPluginUsed = availablePluginNames.Length - 1;
			}
			get
			{
				return lastPluginUsed;
			}
		}


		//public string[] PluginFiles
		//{
		//	get
		//	{
		//		return pluginFiles;
		//	}
		//}
		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		private readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None, vamps.AlignmentType.FPS20,
										vamps.AlignmentType.FPS30,  vamps.AlignmentType.FPS40 , vamps.AlignmentType.FPS60, vamps.AlignmentType.NoteOnsets,
										vamps.AlignmentType.BeatsQuarter, vamps.AlignmentType.BeatsHalf };
		public vamps.AlignmentType[] AllowableAlignments
		{
			get
			{
				return allowableAlignments;
			}
		}

		private vamps.AlignmentType alignmentType = vamps.AlignmentType.None;

		private vamps.AlignmentType AlignmentType
		{
			set
			{
				bool valid = false;
				for (int i = 0; i < allowableAlignments.Length; i++)
				{
					if (value == allowableAlignments[i])
					{
						alignmentType = value;
						valid = true;
						i = allowableAlignments.Length; // Force exit of loop
					}
				}
			}
			get
			{
				return alignmentType;
			}
		}


		private readonly vamps.LabelTypes[] allowableLabels = { vamps.LabelTypes.None };

		public vamps.LabelTypes[] AllowableLabels
		{
			get
			{
				return allowableLabels;
			}
		}

		public xTimings Timings
		{
			get
			{
				return XPolyphonic;
			}
		}
		public int TransformationType
		{
			get
			{
				return 2;
			}
		}

		public string TransformationName
		{
			get
			{
				return "Note Onsets";
			}
		}


		public int PrepareToVamp(string fileSong, int pluginIndex, int beatsPerBar, int stepSize,
								int detectionMethod = METHOD1domain, bool reuse = false, bool whiten = true)
		{
			// Song file should have already been copied to the temp folder and named song.mp3
			// Annotator will use the same folder the song is in for it's files
			// Returns the name of the results file, which will also be in the same temp folder

			int err = 0;
			string fileConfig = filesAvailableConfigs[pluginIndex];
			//string vampParams = availablePluginCodes[pluginIndex];
			string pathConfigs = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
			lastPluginUsed = pluginIndex;

			int lineCount = 0;
			string lineIn = "";
			//string fileConfigFrom = ""; // vampConfigs;
			//string fileConfigTo = ""; // tempPath;
			string vampFile = "";
			//string rezults = "";
			BeatsPerBar = beatsPerBar;
			ReuseResults = reuse;
			StreamReader reader;
			StreamWriter writer;

			string fileConfigFrom = pathConfigs + fileConfig;
			string fileConfigTo = pathWork + fileConfig;
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
					case PLUGINqmTranscribe:
						// Queen Mary Polyphonic Transcription
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("441", stepSize.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;

					case PLUGINSilvet:
						// Silvet
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("1024", stepSize.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;

					case PLUGINaubioTracker:
						// Aubio Note Tracker
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINalicante:
						// *Alicante Polyphonic Transcription
						string msg = "Alicante Polyphonic Transcription is not currently available.  Using Queen Mary Transcription instead.";
						DialogResult dr = MessageBox.Show(msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					default:
						Fyle.BUG("Unrecognized Plugin Index, using default!");
						//if (Fyle.isWiz) System.Diagnostics.Debugger.Break();
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;
				}
			} // End try
			catch (Exception e)
			{
				err = e.HResult;
				string m = e.Message;
				if (Fyle.isWiz)
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

		private int fps = 40;
		private int msPF = 25;
		public int FramesPerSecond
		{
			set
			{
				fps = value;
				if (fps < 10) fps = 10;
				if (fps > 100) fps = 100;
				msPF = 1000 / fps;
			}
			get
			{
				return fps;
			}
		}

		public int MillisecondsPerFrame
		{
			set
			{
				msPF = value;
				if (msPF < 10) msPF = 10;
				if (msPF > 100) msPF = 100;
				fps = 1000 / msPF;
			}
		}

		private int ReadTranscriptionData(string transcriptionFile, vamps.AlignmentType alignType)
		{
			int onsetCount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			//int align = SetAlignmentHost(cboAlignTranscribe.Text);
			string noteLabel = "";
			int duration = 0;
			int note = 0;
			int lastNote = -1;

			StreamReader reader = new StreamReader(transcriptionFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			XPolyphonic = new xTimings("Polyphonic Transcription");

			reader = new StreamReader(transcriptionFile);



			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = xUtils.ParseMilliseconds(parts[0]);
					if (alignType > vamps.AlignmentType.None)
					{
						if ((alignType == vamps.AlignmentType.FPS40) || (alignType == vamps.AlignmentType.FPS20))
						{
							millisecs = Annotator.AlignTimeTo(millisecs, alignType); // AlignStartTo(millisecs, align);
						}
						else
						{
							if ((alignType == vamps.AlignmentType.Bars) ||
								(alignType == vamps.AlignmentType.BeatsFull) ||
								(alignType == vamps.AlignmentType.BeatsHalf) ||
								(alignType == vamps.AlignmentType.BeatsThird) ||
								(alignType == vamps.AlignmentType.BeatsQuarter) ||
								(alignType == vamps.AlignmentType.NoteOnsets))
							{
								millisecs = Annotator.AlignTimeTo(millisecs, alignType);
							}
						}
					}

					// Avoid closely spaced duplicates
					//if ((alignType < vamps.AlignmentType.None) || (millisecs > lastNote))
					if (millisecs > lastNote)
						{
							// Get label, if available
							if (parts.Length == 3)
						{
							duration = xUtils.ParseMilliseconds(parts[1]);
							note = Int16.Parse(parts[2]);

							//if (cboLabelsSpectrum.SelectedIndex == 1)
							//{
							//TODO Impliment Label Type	
							noteLabel = MusicalNotation.noteNamesUnicode[note];
							//}
							//if (cboLabelsOnsets.SelectedIndex == 2)
							//{
							//	noteLabel = note.ToString();
							//}
						}

						XPolyphonic.Add(noteLabel, millisecs, millisecs + duration);
						lastNote = millisecs;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Note Onsets










		// Required by ITransform inteface, wrapper to true ResultsToxTimings procedure requiring more parameters
		public int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType)
		{
			return ResultsToxTimings(resultsFile, alignmentType, labelType, DetectionMethods.ComplexDomain);
		}


		// The true ResultsToxTimings procedure requiring more parameters, (not compliant with ITransform inteface)

		public int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;


			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				pcount++;
			} // end while loop more lines remaining

			reader.Close();
			//Array.Resize(ref polyNotes, pcount);
			//Array.Resize(ref polyMSstart, pcount);
			//Array.Resize(ref polyMSlen, pcount);
			pcount = 0; // reset for re-use
			XPolyphonic.effects.Clear();

			reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					// declare doubles to hold start and length
					double dstart = 0;
					double dlen = 0;
					// Try parsing fields 0 and 1 for start time and length (in decimal seconds)
					double.TryParse(parts[0], out dstart);
					double.TryParse(parts[1], out dlen);
					// End time is start + length
					double dend = dstart + dlen;
					// Convert double:seconds to int:milliseconds
					int msstart = (int)Math.Round(dstart * 1000);
					int msend = (int)Math.Round(dend * 1000);
					// Alighn
					int startms = Annotator.AlignTimeTo(millisecs, alignmentType);
					int endms = Annotator.AlignTimeTo(millisecs, alignmentType);
					// Get MIDI note number
					note = Int16.Parse(parts[2]);
					// Create new xEffect with these values, add to timings list
					xEffect xef = new xEffect(MusicalNotation.noteNamesUnicode[note], startms, endms, note);
					XPolyphonic.effects.Add(xef);
					pcount++;
				}
			} // end while loop more lines remaining

			reader.Close();

			return pcount;
		} // end Beats


		public int xTimingsToLORtimings(xTimings timings, LORSequence4 sequence)
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)
			
			seqFunct.Sequence = sequence;
			int errs = 0;

			if (XPolyphonic != null)
			{
				if (XPolyphonic.effects.Count > 0)
				{
					LORTimings4 barGrid = seqFunct.GetGrid("Note Transcription", true);
					seqFunct.ImportTimingGrid(barGrid, XPolyphonic);
				}
			}

			return errs;
		}

		public int xTimingsToLORChannels(xTimings timings, LORSequence4 sequence)
		{
			return xTimingsToLORChannels(timings, sequence, 1, false);
		}

		public int xTimingsToLORChannels(xTimings timings, LORSequence4 sequence, int firstBeat, bool ramps)
		{
			int errs = 0;
			int grpNum = -1;

			// Part 1
			// Get Track, Polyphonic Group, Octave Groups, and Poly Channels
			LORTrack4 vampTrack = seqFunct.GetTrack("Vamp-O-Rama");
			LORChannelGroup4 polyGroup = seqFunct.GetGroup("Note Transcription", vampTrack.Members);
			LORChannelGroup4 octoGroup = null;
			for (int n = 0; n < MusicalNotation.noteNamesUnicode.Length; n++)
			{
				int g2 = (int)n / 12;
				if (g2 != grpNum)
				{
					octoGroup = seqFunct.GetGroup(PolyGroupNamePrefix + MusicalNotation.octaveNamesA[g2], polyGroup.Members);
					grpNum = g2;
				}
				LORChannel4 chs = seqFunct.GetChannel(PolyNoteNamePrefix + MusicalNotation.noteNamesUnicode[n], octoGroup.Members);
				polyChannels[n] = chs;
				chs.color = SequenceFunctions.NoteColor(n);
				chs.effects.Clear();
			}

			// Part 2
			// Create an effect in the appropriate Note Poly Channel for each timing
			if (timings != null)
			{
				if (timings.effects.Count > 0)
				{
					for (int f = 0; f < timings.effects.Count; f++)
					{
						xEffect timing = timings.effects[f];
						int note = timing.Midi;
						int csStart = (int)Math.Round(timing.starttime / 10D);
						int csEnd = (int)Math.Round(timing.endtime / 10D);
						LOREffect4 eft = null;
						if (ramps)
						{
							eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
						}
						else
						{
							eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd);
						}
						polyChannels[note].effects.Add(eft);
					}
				}
			}

			// Part 3
			// Get rid of the empty ones
			for (int g1 = 0; g1< polyGroup.Members.Count; g1++)
			{
				if (polyGroup.Members[g1].MemberType == LORMemberType4.ChannelGroup)
				{
					octoGroup = (LORChannelGroup4)polyGroup.Members[g1];
					for (int n1 = 0; n1< octoGroup.Members.Count; n1++)
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
						polyGroup.Members.Items.RemoveAt(g1);
						g1--;
					}
				}
			}

			return errs;
		}


		private int ReadPolyData(string PolyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;


			StreamReader reader = new StreamReader(PolyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				pcount++;
			} // end while loop more lines remaining

			reader.Close();
			//Array.Resize(ref polyNotes, pcount);
			//Array.Resize(ref polyMSstart, pcount);
			//Array.Resize(ref polyMSlen, pcount);
			pcount = 0; // reset for re-use

			reader = new StreamReader(PolyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					//TODO (maybe) Align with note onsets and/or beats (?)
					ontime = xUtils.ParseMilliseconds(parts[1]);
					note = Int16.Parse(parts[2]);
					//polyNotes[pcount] = note;
					//polyMSstart[pcount] = millisecs;
					//polyMSlen[pcount] = ontime;
					pcount++;
				}
			} // end while loop more lines remaining

			reader.Close();

			return pcount;
		} // end Polyphonic Transcription

		private int AddPolyTimings(vamps.AlignmentType alignTo = vamps.AlignmentType.None)
		{
			int pcount = 0;
			xTimings times = new xTimings("Polyphonic Transcription");
			//timingsList[LISTpoly] = times;Fsequence

			//for (int i=0; i< polyNotes.Length; i++)
			//{
			//times.Add(polyNotes[i].ToString(), polyMSstart[i], polyMSstart[i] + polyMSlen[i]);
			pcount++;
			//}

			return pcount;
		} // end Polyphonic Transcription

		//! Other What?
		/*
		private void AddPolyTimings_Other(string prefix)
		{
			string dmsg = "";
			//xTimings times;
			int octave = 0;
			int lastOctave = 0;

			Array.Resize(ref noteTimings, MusicalNotation.noteNamesUnicode.Length);
			for (int n = 0; n < MusicalNotation.noteNamesUnicode.Length; n++)
			{
				xTimings times = new xTimings(prefix + MusicalNotation.noteNamesUnicode[n]);
				noteTimings[n] = times;

				//dmsg = "Adding xTimings '" + times.Name + "' SI:" + times.SavedIndex;
				//dmsg += " Note #" + n.ToString();
				//dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				//Debug.WriteLine(dmsg);

			}
		} // end Polyphonic Transcription - Other
		*/
		

		public int xTimingsToxLights(xTimings timings, string baseFileName)
		{
			int errs = 0;


			return errs;
		}

		// Moved into Annotator
		/*
		public static int AlignTo(int timeIn, vamps.AlignmentType alignType, xTimings timings)
		{
			double timeOut = timeIn;
			int t1 = 0;
			int t2 = 0;
			//static int idx = 0;
			switch (alignType)
			{
				case vamps.AlignmentType.None:
					timeOut = timeIn;
					break;
				case vamps.AlignmentType.FPS10:
					// 100 ms
					timeOut = Math.Round(timeIn / 100D) * 100D;
					break;
				case vamps.AlignmentType.FPS20:
					// 50 ms
					timeOut = Math.Round(timeIn / 50D) * 50D;
					break;
				case vamps.AlignmentType.FPS30:
					// 33.333333 ms
					timeOut = Math.Round(timeIn / 33.3333333D) * 33.3333333D;
					break;
				case vamps.AlignmentType.FPS40:
					// 25 ms
					timeOut = Math.Round(timeIn / 25D) * 25D;
					break;
				case vamps.AlignmentType.FPS60:
					// 16.666667 ms
					timeOut = Math.Round(timeIn / 16.6666667D) * 16.6666667D;
					break;
				default:
					// Assume one of the following:
					// vamps.AlignmentType.Bars
					// vamps.AlignmentType.BeatsFull;
					// vamps.AlignmentType.BeatsHalf
					// vamps.AlignmentType.BeatsThird
					// vamps.AlignmentType.BeatsQuarter
					// vamps.AlignmentType.NoteOnsets
					// And assume that the timings passed to this routine are the correct ones


					//TODO Finish this later, have more important things to finish first
					//TODO Usable as is, for now
					Annotator.AlignTimeTo(timeIn, alignType);
					/*
					while ((timeIn < timings.effects[idx].starttime) && (idx > 0))
					{
						// In theory, should never need to go backwards, so why?
						if (Fyle.isWiz) System.Diagnostics.Debugger.Break();
						idx--;
					}
					if (timeIn < timings.effects[idx].starttime)
					{
						if (idx == 0)
						{
							t1 = 0;
							t2 = timings.effects[idx].starttime;
						}
						else
						{
							// Sanity check: Should not get here
							if (Fyle.isWiz) System.Diagnostics.Debugger.Break();
						}
					}
					bool keepGoing = true;
					while (keepGoing)
					{
						if (idx >= (timings.effects.Count -1))
						{
							keepGoing = false;
							t1 = timings.effects[idx].starttime;
							t2 = timings.effects[idx].endtime;
						}


					}


					// Which is it closer to?
					int d1 = timeIn - t1;
					int d2 = t2 = timeIn;
					if (d1 > d2) timeOut = t2; else timeOut = t1;

					 

					break;
			}

			return (int)timeOut;
		}
		*/
		// Moved into Annotator




	}
}
