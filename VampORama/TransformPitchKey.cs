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


		public static xTimings xPitchKey = new xTimings(transformName);
		public static xTimings alignTimes = null;
		//public static labelType labelType = labelType.none;
		public static vamps.LabelTypes LabelType = vamps.LabelTypes.KeyNamesUnicode;

		public static readonly string[] availablePluginNames = {	"Queen Mary Key Detector",
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

		public static readonly string[] filesAvailableConfigs = {	"vamp_qm-vamp-plugins_qm-keydetector_key.n3",
																															"vamp_cepstral-pitchtracker_cepstral-pitchtracker_notes.n3",
																															"vamp_nnls-chroma_chordino_chordnotes.n3",
																															"vamp_vamp-aubio_aubiopitch_frequency.n3" };


		private static readonly vamps.LabelTypes[] allowableLabels = { vamps.LabelTypes.None, vamps.LabelTypes.KeyNamesUnicode,
																																		vamps.LabelTypes.KeyNamesASCII,
												vamps.LabelTypes.KeyNumbers, vamps.LabelTypes.Frequency };

		public static readonly string[] availableLabels = { vamps.LABELNAMEnone,
																												vamps.LABELNAMEkeyNamesASCII,
																												vamps.LABELNAMEkeyNamesUnicode,
																												vamps.LABELNAMEmidiNoteNumbers,
																												vamps.LABELNAMEfrequency };
		//public static readonly string[] availableLabels = {"None",
		//																						"Note Names ASCII",
		//																						"Note Names Unicode",
		//																						"Midi Note Numbers" };

		//public const int LABELNone = 0;
		//public const int LABELNoteNameASCII = 1;
		//public const int LABELNoteNameUnicode = 2;
		//public const int LABELMidiNoteNumber = 3;

		private static LORChannelGroup4 pitchKeyGroup = null;
		public static LORChannel4[] pitchKeyChannels = null;

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
			xPitchKey = new xTimings(transformName);

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
							noteLabel = MusicalNotation.keyNamesUnicode[note];
							//}
							//if (cboLabelsOnsets.SelectedIndex == 2)
							//{
							//	noteLabel = note.ToString();
							//}
						}

						xPitchKey.Add(noteLabel, millisecs, millisecs + duration);
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
			int keyID = 0;
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
			xPitchKey.effects.Clear();

			reader = new StreamReader(resultsFile);

			// First, lets get the first line of the file which has the start time of the first
			// Key or Pitch and it's Key or Pitch number
			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					// Try parsing fields 0 start time (in decimal seconds)
					double.TryParse(parts[0], out dstart);
					// Convert double:seconds to int:milliseconds
					msstart = (int)Math.Round(dstart * 1000);
					startms = Annotator.AlignTimeTo(msstart, alignmentType);
					// Get MIDI note number
					Int32.TryParse(parts[1], out keyID);
					if (keyID < 0) // Data integrity check
					{
						// In theory, it's possible to have a note with MIDI value of 0, but really really really unlikely
						Fyle.BUG("Invalid note!");
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
					double.TryParse(parts[0], out dend);
					// Convert double:seconds to int:milliseconds
					msend = (int)Math.Round(dend * 1000);
					if (msend <= msstart) // Data integrity check
					{
						Fyle.BUG("Backwards Note?!?!");
					}
					else
					{
						// Align
						endms = Annotator.AlignTimeTo(msend, alignmentType);
						// Default name is the Key Number
						string keyName = keyID.ToString();
						if (labelType == vamps.LabelTypes.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[keyID];
						if (labelType == vamps.LabelTypes.KeyNamesASCII) keyName = MusicalNotation.keyNamesASCII[keyID];
						// Create new xEffect with these values, add to timings list
						xEffect xef = new xEffect(keyName, startms, endms, keyID);
						xPitchKey.effects.Add(xef);
							
						// End of this one is the start of the next one
						startms = endms;
						// Get MIDI note number
						Int32.TryParse(parts[1], out keyID);
						if (keyID < 0) // Data integrity check
						{
							// In theory, it's possible to have a note with MIDI value of 0, but really really really unlikely
							Fyle.BUG("Invalid note!");
						}

						pcount++;
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Finally, if not at the end-- add one last effect containing the last Pitch or Key
			if (startms < Annotator.songTimeMS)
			{
				endms = Annotator.songTimeMS;
				// Default name is the Key Number
				string keyName = keyID.ToString();
				if (labelType == vamps.LabelTypes.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[keyID];
				if (labelType == vamps.LabelTypes.KeyNamesASCII) keyName = MusicalNotation.keyNamesASCII[keyID];
				// Create new xEffect with these values, add to timings list
				xEffect xef = new xEffect(MusicalNotation.noteNamesUnicode[keyID], startms, endms, keyID);
				xPitchKey.effects.Add(xef);
			}
			
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
			if (LabelType == vamps.LabelTypes.NoteNamesAscii)		keyCount = MusicalNotation.keyNamesASCII.Length;
			if (LabelType == vamps.LabelTypes.NoteNamesUnicode) keyCount = MusicalNotation.keyNamesUnicode.Length;

			// Part 1
			// Get the Vamp Track, and the PitchKey Group,
			// and create the Key Channels
			pitchKeyGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
			Array.Resize(ref pitchKeyChannels, keyCount);
			for (int n = 1; n < keyCount; n++)
			{
				// Default channel name is the key's number...
				string keyName = n.ToString();
				// ... But use the ASCII or Unicode Key representation if specified
				if (LabelType == vamps.LabelTypes.KeyNamesASCII)	 keyName = MusicalNotation.keyNamesASCII[n];
				if (LabelType == vamps.LabelTypes.KeyNamesUnicode) keyName = MusicalNotation.keyNamesUnicode[n];

				LORChannel4 chs = Annotator.Sequence.FindChannel(PitchKeyNamePrefix + keyName, pitchKeyGroup.Members, true, true);
				chs.color = SequenceFunctions.GetKeyColor(n);
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
						int keyID = timing.Midi;
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
									LOREffect4 eft = null;
									if (Annotator.UseRamps)
									{
										eft = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100, 0);
									}
									else
									{
										eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd);
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
			for (int n1 = 0; n1< pitchKeyGroup.Members.Count; n1++)
			{
				if (pitchKeyGroup.Members[n1].MemberType == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)pitchKeyGroup.Members[n1];
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
