using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using LOR4Utils;
using FileHelper;
using xUtilities;

namespace UtilORama4
{
	//! PITCH AND KEY
	static class VampPitchKey //: ITransform
	{
		public const string transformName = "Pitch and Key Transcription";
		public const int PLUGINqueenMary = 0;
		public const int PLUGINcepstral = 1;
		public const int PLUGINchordino = 2;
		public const int PLUGINaubio = 3;

		public static int lastPluginUsed = PLUGINqueenMary;

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
		public static string PitchKeyNamePrefix = "";
		public static string PitchKeyGroupNamePrefix = "";
		public static string ResultsName = "PitchKey.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.Bars;
		public static vamps.LabelType LabelType = vamps.LabelType.KeyNamesUnicode;


		public static xTimings xPitchKey = new xTimings(transformName);
		public static xTimings alignTimes = null;
		//public static labelType labelType = labelType.none;

		public static readonly string[] availablePluginNames = {  "Queen Mary Key Detector",
																															"Cepstral Pitch Tracker",
																															"Chordino Chord Notes",
																															"Aubio Pitch Frequency",
		//																											"#Alicante Note Onset Detector",
																												"#Alicante Polyphonic Transcription" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-keydetector:key",
																														"vamp:cepstral-pitchtracker:cepstral-pitchtracker:notes",
																														"vamp:nnls-chroma:chordino:chordnotes",
																														"vamp:vamp-aubio:aubiopitch:frequency" };

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter,
																																					vamps.AlignmentType.Bars };

		public static readonly string[] filesAvailableConfigs = { "vamp_qm-vamp-plugins_qm-keydetector_key.n3",
																															"vamp_cepstral-pitchtracker_cepstral-pitchtracker_notes.n3",
																															"vamp_nnls-chroma_chordino_chordnotes.n3",
																															"vamp_vamp-aubio_aubiopitch_frequency.n3" };


		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None,
																																	vamps.LabelType.KeyNamesUnicode,
																																	vamps.LabelType.KeyNamesASCII,
																																	vamps.LabelType.KeyNumbers,
																																	vamps.LabelType.Frequency };

		//public static readonly string[] availableLabels = { vamps.LABELNAMEnone,
		//																										vamps.LABELNAMEkeyNamesASCII,
		//																										vamps.LABELNAMEkeyNamesUnicode,
		//																										vamps.LABELNAMEmidiNoteNumbers,
		//																										vamps.LABELNAMEfrequency };
		//public static readonly string[] availableLabels = {"None",
		//																						"Note Names ASCII",
		//																						"Note Names Unicode",
		//																						"Midi Note Numbers" };

		//public const int LABELNone = 0;
		//public const int LABELNoteNameASCII = 1;
		//public const int LABELNoteNameUnicode = 2;
		//public const int LABELMidiNoteNumber = 3;

		private static LOR4ChannelGroup pitchKeyGroup = null;
		public static LOR4Channel[] pitchKeyChannels = null;

		private static int idx = 0;

		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static xTimings Timings
		{
			get
			{
				return xPitchKey;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.PitchAndKey;
			}
		}

		public static string TransformationName
		{
			get
			{
				return transformName;
			}
		}


		public static int PrepareToVamp(string fileSong, int pluginIndex, int detectionMethod = METHOD1domain)
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
					case PLUGINqueenMary:
						// Queen Mary Key Detector
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

					case PLUGINcepstral:
						// Cepstral Pitch Tracker
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

					case PLUGINaubio:
						// Aubio Pitch Frequency
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINchordino:
						// Chordino Chord Notes
						//string msg = "Alicante Polyphonic Transcription is not currently available.  Using Queen Mary Transcription instead.";
						//DialogResult dr = MessageBox.Show(msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						//err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
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


		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelType labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int pcount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int keyID = 0;
			int eStart = 0;
			int eEnd = 0;

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);

			string msg = "\r\n\r\n### PROCESSING PITCH and KEY ####################################";
			Debug.WriteLine(msg);

			pcount = 0; // reset for re-use
			xPitchKey.effects.Clear();

			StreamReader reader = new StreamReader(resultsFile);
			// First, lets get the first line of the file which has the start time of the first
			// Key or Pitch and it's Key or Pitch number
			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					// Try parsing fields 0 start time (in decimal seconds)
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs);
					// Get MIDI note (pitch-key) number
					int.TryParse(parts[1], out keyID);
					if (keyID < 0) // Data integrity check
					{
						// In theory, it's possible to have a key with MIDI value of 0, but really really really unlikely
						Fyle.BUG("Invalid Key!");
					}
				}
			}

			// Now get the rest of the lines from the file, with the start times and key numbers of each subsequent
			// change in Pitch or Key
			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					// Try parsing fields 0 start time (in decimal seconds)
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eEnd = Annotator.AlignTime(millisecs);
					if (eEnd <= eStart) // Data integrity check
					{
						//Fyle.BUG("Zero length or Backwards Key?!?!");
					}
					else
					{
						// After Aligning to Bars (or other long time) we may end up with a key of Zero length
						// if it was shorter than a Bar to begin with
						if (eEnd > eStart)
						{
							// Default name is the Key Number
							string keyName = keyID.ToString();
							if (labelType == vamps.LabelType.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[keyID];
							if (labelType == vamps.LabelType.KeyNamesASCII) keyName = MusicalNotation.keyNamesASCII[keyID];
							// Create new xEffect with these values, add to timings list
							xEffect xef = new xEffect(keyName, eStart, eEnd, keyID);
							xPitchKey.effects.Add(xef);

							// End of this one is the start of the next one
							eStart = eEnd;
							// Get MIDI key number for the NEXT section
							Int32.TryParse(parts[1], out keyID);
							if (keyID < 0) // Data integrity check
							{
								// it's impossible to have a key with MIDI value of 0
								Fyle.BUG("Invalid Key!");
							}

							pcount++;
						}
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Finally, if not at the end-- add one last effect containing the last Pitch or Key
			if (eStart < Annotator.songTimeMS)
			{
				eEnd = Annotator.songTimeMS;
				// Default name is the Key Number
				string keyName = keyID.ToString();
				if (labelType == vamps.LabelType.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[keyID];
				if (labelType == vamps.LabelType.KeyNamesASCII) keyName = MusicalNotation.keyNamesASCII[keyID];
				// Create new xEffect with these values, add to timings list
				xEffect xef = new xEffect(MusicalNotation.noteNamesUnicode[keyID], eStart, eEnd, keyID);
				xPitchKey.effects.Add(xef);
			}
			//Fyle.BUG("Check output window.  Did the alignments work as expected?");

			return pcount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xPitchKey != null)
			{
				if (xPitchKey.effects.Count > 0)
				{
					string gridName = "6 " + transformName;
					LORTimings4 polyGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(polyGrid, xPitchKey);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			int errs = 0;
			int grpNum = -1;
			int keyCount = 25; // 24 standard Keys numbered 1-24.  0 is included but invalid.
			int lastStart = -1;
			if (LabelType == vamps.LabelType.NoteNamesASCII) keyCount = MusicalNotation.keyNamesASCII.Length;
			if (LabelType == vamps.LabelType.NoteNamesUnicode) keyCount = MusicalNotation.keyNamesUnicode.Length;

			// Part 1
			// Get the Vamp Track, and the PitchKey Group,
			// and create the Key Channels
			pitchKeyGroup = Annotator.VampTrack.Members.FindChannelGroup(transformName, true);
			Array.Resize(ref pitchKeyChannels, keyCount);
			for (int n = 1; n < keyCount; n++)
			{
				// Default channel name is the key's number...
				string keyName = n.ToString();
				// ... But use the ASCII or Unicode Key representation if specified
				if (LabelType == vamps.LabelType.KeyNamesASCII) keyName = MusicalNotation.keyNamesASCII[n];
				if (LabelType == vamps.LabelType.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[n];

				LOR4Channel chs = pitchKeyGroup.Members.FindChannel(PitchKeyNamePrefix + keyName, true, true);
				chs.color = SequenceFunctions.ChannelColor(n - 1);
				chs.effects.Clear();
				pitchKeyChannels[n] = chs;
			}

			// Part 2
			// Create an effect in the appropriate Key or Pitch Channel for each timing
			if (xPitchKey != null)
			{
				if (xPitchKey.effects.Count > 0)
				{
					for (int f = 0; f < xPitchKey.effects.Count; f++)
					{
						xEffect timing = xPitchKey.effects[f];
						int keyID = timing.Number;
						if (keyID < 1) // Data integrity check
						{
							// In theory, its possible to have a note with a MIDI number of 0, but really really really unlikely
							Fyle.BUG("Invalid Key number!");
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
									LOR4Effect eft = null;
									if (Annotator.UseRamps)
									{
										eft = new LOR4Effect(LOR4EffectType.FadeDown, csStart, csEnd, 100, 0);
									}
									else
									{
										eft = new LOR4Effect(LOR4EffectType.Intensity, csStart, csEnd);
									}
									pitchKeyChannels[keyID].effects.Add(eft);
								}
								lastStart = csStart;
							}
						}
					}
				}
			}

			// Part 3
			// Get rid of the empty ones
			for (int n1 = 0; n1 < pitchKeyGroup.Members.Count; n1++)
			{
				if (pitchKeyGroup.Members[n1].MemberType == LOR4MemberType.Channel)
				{
					LOR4Channel ch = (LOR4Channel)pitchKeyGroup.Members[n1];
					if (ch.effects.Count < 1)
					{
						pitchKeyGroup.Members.Items.RemoveAt(n1);
						n1--;
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
