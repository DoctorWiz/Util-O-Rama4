using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LORUtils4;
using FileHelper;
using xUtilities;

namespace UtilORama4
{
	//! POLYPHONIC TRANSCRIPTION
	static class VampPolyphonic //: ITransform
	{
		public const string transformName = "Polyphonic Transcription";
		public const int PLUGINqmTranscribe = 0;
		public const int PLUGINSilvet = 1;
		public const int PLUGINalicante = 2;
		public const int PLUGINaubioTracker = 3;

		public static int lastPluginUsed = PLUGINqmTranscribe;

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

		public static xTimings xPolyphonic = new xTimings(transformName);
		public static xTimings alignTimes = null;
		//public static labelType labelType = labelType.none;
		public static vamps.LabelTypes LabelType = vamps.LabelTypes.NoteNamesUnicode;

		public static readonly string[] availablePluginNames = { "Queen Mary Polyphonic Transcription",
																											"Silvet Note Transcription",
																											"Aubio Note Tracker",
		//																											"#Alicante Note Onset Detector",
																												"#Alicante Polyphonic Transcription" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

	public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																										"vamp_silvet_silvet_onsets.n3",
																										"vamp_vamp-aubio_aubionotes_notes.n3" };


		public static readonly string[] availableLabels = {	vamps.LABELNAMEnone,
																												vamps.LABELNAMEnoteNamesAscii,
																												vamps.LABELNAMEnoteNamesUnicode,
																												vamps.LABELNAMEmidiNoteNumbers };
		//public static readonly string[] availableLabels = {"None",
		//																						"Note Names ASCII",
		//																						"Note Names Unicode",
		//																						"Midi Note Numbers" };

		//public const int LABELNone = 0;
		//public const int LABELNoteNameASCII = 1;
		//public const int LABELNoteNameUnicode = 2;
		//public const int LABELMidiNoteNumber = 3;

		private static LORChannelGroup4 polyGroup = null;
		public static LORChannel4[] polyChannels = null;

		private static int idx = 0;


		public static int UsePlugin
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

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None, vamps.AlignmentType.FPS20,
										vamps.AlignmentType.FPS30,  vamps.AlignmentType.FPS40 , vamps.AlignmentType.FPS60, vamps.AlignmentType.NoteOnsets,
										vamps.AlignmentType.BeatsQuarter, vamps.AlignmentType.BeatsHalf };

		private static vamps.AlignmentType alignmentType = vamps.AlignmentType.None;

		private static vamps.AlignmentType AlignmentType
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


		private static readonly vamps.LabelTypes[] allowableLabels = { vamps.LabelTypes.None, vamps.LabelTypes.NoteNamesUnicode, vamps.LabelTypes.NoteNamesAscii,
												vamps.LabelTypes.MIDINoteNumbers, vamps.LabelTypes.Frequency };


		public static xTimings Timings
		{
			get
			{
				return xPolyphonic;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.PolyphonicTranscription;
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
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
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
								lineIn = lineIn.Replace("441", Annotator.StepSize.ToString());
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
								lineIn = lineIn.Replace("1024", Annotator.StepSize.ToString());
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


		private static int ResultsToxTimings1(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType)
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

			LabelType = labelType;
			StreamReader reader = new StreamReader(resultsFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			xPolyphonic = new xTimings(transformName);

			reader = new StreamReader(resultsFile);



			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = xUtils.ParseMilliseconds(parts[0]);
					if (alignmentType > vamps.AlignmentType.None)
					{
						if ((alignmentType == vamps.AlignmentType.FPS40) || (alignmentType == vamps.AlignmentType.FPS20))
						{
							millisecs = Annotator.AlignTimeTo(millisecs, alignmentType); // AlignStartTo(millisecs, align);
						}
						else
						{
							if ((alignmentType == vamps.AlignmentType.Bars) ||
								(alignmentType == vamps.AlignmentType.BeatsFull) ||
								(alignmentType == vamps.AlignmentType.BeatsHalf) ||
								(alignmentType == vamps.AlignmentType.BeatsThird) ||
								(alignmentType == vamps.AlignmentType.BeatsQuarter) ||
								(alignmentType == vamps.AlignmentType.NoteOnsets))
							{
								millisecs = Annotator.AlignTimeTo(millisecs, alignmentType);
							}
						}
					}

					// Avoid closely spaced duplicates
					//if ((alignmentType < vamps.AlignmentType.None) || (millisecs > lastNote))
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

						xPolyphonic.Add(noteLabel, millisecs, millisecs + duration);
						lastNote = millisecs;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Note Onsets



		// The true ResultsToxTimings procedure requiring more parameters, (not compliant with ITransform inteface)

		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int pcount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			double dstart = 0D;
			double dlen = 0D;
			// End time is start + length
			double dend = 0D;
			// Convert double:seconds to int:milliseconds
			int msstart = 0;
			int msend = 0;
			// Align
			int startms = 0;
			int endms = 0;






			LabelType = labelType;
			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
			} // end while loop more lines remaining

			reader.Close();
			//Array.Resize(ref polyNotes, pcount);
			//Array.Resize(ref polyMSstart, pcount);
			//Array.Resize(ref polyMSlen, pcount);
			pcount = 0; // reset for re-use
			xPolyphonic.effects.Clear();

			reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					// declare doubles to hold start and length
					dstart = 0;
					dlen = 0;
					// Try parsing fields 0 and 1 for start time and length (in decimal seconds)
					double.TryParse(parts[0], out dstart);
					double.TryParse(parts[1], out dlen);
					// End time is start + length
					dend = dstart + dlen;
					// Convert double:seconds to int:milliseconds
					msstart = (int)Math.Round(dstart * 1000);
					msend = (int)Math.Round(dend * 1000);
					if (msstart < 1) // Data integrity check
					{
						Fyle.BUG("Invalid start time!");
					}
					else
					{
						if (msend < msstart) // Data integrity check
						{
							Fyle.BUG("Backwards Note?!?!");
						}
						else
						{
							// Align
							startms = Annotator.AlignTimeTo(msstart, alignmentType);
							endms = Annotator.AlignTimeTo(msend, alignmentType);
							// Get MIDI note number
							note = Int16.Parse(parts[2]);
							if (note < 0) // Data integrity check
							{
								// In theory, it's possible to have a note with MIDI value of 0, but really really really unlikely
								Fyle.BUG("Invalid note!");
							}
							else
							{
								// Create new xEffect with these values, add to timings list
								xEffect xef = new xEffect(MusicalNotation.noteNamesUnicode[note], startms, endms, note);
								xPolyphonic.effects.Add(xef);
								pcount++;
							}
						}
					}
				}
			} // end while loop more lines remaining
			
			// Need to sort because vamp plugin outputs in the order of the ENDtime
			//  (Understandable, since it can't properly detect it until it's done)
			//   (Sometimes, if a long note starts after a short one, they may be out of order
			//    because the short one ended first.)
			//        (And that will crash LOR!)
			// But we need them sorted in order of STARTtime (for LOR's sake)
			xPolyphonic.effects.Sort();

			reader.Close();

			return pcount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xPolyphonic != null)
			{
				if (xPolyphonic.effects.Count > 0)
				{
					LORTimings4 polyGrid = Annotator.Sequence.FindTimingGrid(transformName, true);
					SequenceFunctions.ImportTimingGrid(polyGrid, xPolyphonic);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			int errs = 0;
			int grpNum = -1;
			int polyCount = 128;
			int lastStart = -1;
			if (LabelType == vamps.LabelTypes.NoteNamesAscii)		polyCount = MusicalNotation.noteNamesASCII.Length;
			if (LabelType == vamps.LabelTypes.NoteNamesUnicode) polyCount = MusicalNotation.noteNamesUnicode.Length;

			// Part 1
			// Get Track, Polyphonic Group, Octave Groups, and Poly Channels
			//LORTrack4 vampTrack = SequenceFunctions.GetTrack("Vamp-O-Rama");
			LORChannelGroup4 polyGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
			//polyChannels = SequenceFunctions.GetPolyChannels();
			Array.Resize(ref polyChannels, polyCount);
			LORChannelGroup4 octoGroup = null;
			for (int n = 0; n < polyCount; n++)
			{
				int g2 = (int)n / 12;
				if (g2 != grpNum)
				{
					octoGroup = Annotator.Sequence.FindChannelGroup(PolyGroupNamePrefix + MusicalNotation.octaveNamesA[g2], polyGroup.Members, true); ;
					grpNum = g2;
				}
				string noteName = n.ToString();
				if (LabelType == vamps.LabelTypes.NoteNamesAscii)		noteName = MusicalNotation.noteNamesASCII[n];
				if (LabelType == vamps.LabelTypes.NoteNamesUnicode) noteName = MusicalNotation.noteNamesUnicode[n];

				LORChannel4 chs = Annotator.Sequence.FindChannel(PolyNoteNamePrefix + noteName, octoGroup.Members, true, true);
				chs.color = SequenceFunctions.NoteColors[g2];
				chs.effects.Clear();
				polyChannels[n] = chs;
			}

			// Part 2
			// Create an effect in the appropriate Note Poly Channel for each timing
			if (xPolyphonic != null)
			{
				if (xPolyphonic.effects.Count > 0)
				{
					for (int f = 0; f < xPolyphonic.effects.Count; f++)
					{
						xEffect timing = xPolyphonic.effects[f];
						int note = timing.Midi;
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
									polyChannels[note].effects.Add(eft);
								}
								lastStart = csStart;
							}
						}
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

			Annotator.Sequence.CentiFix();

			return errs;
		}


		public static int xTimingsToxLights(string baseFileName)
		{
			int errs = 0;


			return errs;
		}


	}
}
