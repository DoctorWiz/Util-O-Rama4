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
		public static string ResultsName = "Polyphonic.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;


		public static xTimings xPolyphonic = new xTimings(transformName);
		public static xTimings alignTimes = null;

		public static vamps.LabelType LabelType = vamps.LabelType.NoteNamesUnicode;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.BeatsQuarter;

		public static readonly string[] availablePluginNames = { "Queen Mary Polyphonic Transcription",
																											"Silvet Note Transcription",
																											"Aubio Note Tracker",
		//																											"#Alicante Note Onset Detector",
																												"#Alicante Polyphonic Transcription" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter };

		public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																										"vamp_silvet_silvet_onsets.n3",
																										"vamp_vamp-aubio_aubionotes_notes.n3" };


		//public static readonly string[] availableLabels = {	vamps.LABELNAMEnone,
		//																										vamps.LABELNAMEnoteNamesASCII,
		//																										vamps.LABELNAMEnoteNamesUnicode,
		//																										vamps.LABELNAMEmidiNoteNumbers };
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
		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None, vamps.LabelType.NoteNamesUnicode, vamps.LabelType.NoteNamesASCII,
												vamps.LabelType.MIDINoteNumbers, vamps.LabelType.Frequency };

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


		private static int ResultsToxTimings1(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelType labelType)
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
			int noteCount = 0;

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);

			xPolyphonic.effects.Clear();

			StreamReader reader = new StreamReader(resultsFile);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				string[] parts = lineIn.Split(',');
				if (parts.Length > 2) ;
				{
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs);
					duration = xUtils.ParseMilliseconds(parts[1]);
					int ee = eStart + duration;
					eEnd = Annotator.AlignTime(ee);
					// Note: Unlike other annotator transforms, we will NOT be checking to see
					// if this note starts AFTER the last one, or if the end is after it
					// since for Polyphonic Transcription this is perfectly legitimate
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

						noteCount++;
						xPolyphonic.Add(noteLabel, eStart, eEnd, note);
						lastStart = eStart;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return noteCount;
		} // end Note Onsets



		// The true ResultsToxTimings procedure requiring more parameters, (not compliant with ITransform inteface)

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
			int noteCount = 0;

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);
			
			string msg = "\r\n\r\n### PROCESSING POLYPHONIC TRANSCRIPTION ####################################";
			Debug.WriteLine(msg);

			xPolyphonic.effects.Clear();

			StreamReader reader = new StreamReader(resultsFile);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				string[] parts = lineIn.Split(',');
				if (parts.Length > 2) ;
				{
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs);
					duration = xUtils.ParseMilliseconds(parts[1]);
					int ee = eStart + duration;
					eEnd = Annotator.AlignTime(ee);
					// Note: Unlike other annotator transforms, we will NOT be checking to see
					// if this note starts AFTER the last one, or if the end is after it
					// since for Polyphonic Transcription this is perfectly legitimate
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

						noteCount++;
						xPolyphonic.Add(noteLabel, eStart, eEnd, note);
						lastStart = eStart;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return noteCount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xPolyphonic != null)
			{
				if (xPolyphonic.effects.Count > 0)
				{
					// Note: Number '5' is the same as None Onsets because this returns, effectively, the same results
					string gridName = "5 " + transformName;
					LORTimings4 polyGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
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
			if (LabelType == vamps.LabelType.NoteNamesASCII)		polyCount = MusicalNotation.noteNamesASCII.Length;
			if (LabelType == vamps.LabelType.NoteNamesUnicode) polyCount = MusicalNotation.noteNamesUnicode.Length;

			// Part 1
			// Get Track, Polyphonic Group, Octave Groups, and Poly Channels
			polyGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
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
				if (LabelType == vamps.LabelType.NoteNamesASCII)		noteName = MusicalNotation.noteNamesASCII[n];
				if (LabelType == vamps.LabelType.NoteNamesUnicode) noteName = MusicalNotation.noteNamesUnicode[n];

				LORChannel4 chs = Annotator.Sequence.FindChannel(PolyNoteNamePrefix + noteName, octoGroup.Members, true, true);
				chs.color = SequenceFunctions.ChannelColor(n);
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
