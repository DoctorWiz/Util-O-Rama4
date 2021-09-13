using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LORUtils4; using FileHelper;
using xUtilities;

namespace UtilORama4
{
	static class VampNoteOnsets //: ITransform
	{
		public const string transformName = "Note Onsets";
		public const int PLUGINqmOnset = 0;
		public const int PLUGINqmTranscribe = 1;
		public const int PLUGINOnsetDS = 2;
		public const int PLUGINSilvet = 3;
		public const int PLUGINaubioOnset = 4;
		public const int PLUGINaubioTracker = 5;
		public static int lastPluginUsed = PLUGINqmOnset;

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

		public static xTimings xOnsets = Annotator.xOnsets;

		public static readonly string[] availablePluginNames = {	"Queen Mary Note Onset Detector",
																											"Queen Mary Polyphonic Transcription",
																											"OnsetDS Onset Detector",
																											"Silvet Note Transcription",
																											"Aubio Onset Detector",
																											"Aubio Note Tracker" };
		//																											"#Alicante Note Onset Detector",
		//																										"#Alicante Polyphonic Transcription" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-onsetdetector:onsets",
																											"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:vamp-onsetsds:onsetsds:onsets",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubioonset:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

		public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-onsetdetector_onsets.n3",
																											"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																											"vamp_vamp-onsetsds_onsetsds_onsets.n3",
																											"vamp_silvet_silvet_onsets.n3",
																											"vamp_vamp-aubio_aubioonset_onsets.n3",
																											"vamp_vamp-aubio_aubionotes_notes.n3" };

		public static readonly string[] availableLabels = {"None",
																								"Note Names",
																								"Midi Note Numbers" };

	//private SequenceFunctions SequenceFunctions = new SequenceFunctions();
		//

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

		private static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None, vamps.AlignmentType.FPS20, vamps.AlignmentType.FPS40 };


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


		private static readonly vamps.LabelTypes[] allowableLabels = { vamps.LabelTypes.None };


		public static xTimings Timings
		{
			get
			{
				return xOnsets;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.NoteOnsets;
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

			//string tempPath = Path.GetDirectoryName(fileSong);
			//string vampParams = availablePluginNames[pluginIndex];
			//vampFile = vampParams.Replace(':', '_') + ".n3";
			//fileConfigFrom += vampFile;
			//fileConfigTo = tempPath + vampFile;

			try
			{
				switch (pluginIndex)
				{
					case PLUGINqmOnset:
						// Queen Mary Note Onsets
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("512", Annotator.StepSize.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;

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
					case PLUGINOnsetDS:
						// Onset DS
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
					case PLUGINaubioOnset:
						// Aubio Onsets
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("256", Annotator.StepSize.ToString());
							}
							if (lineCount == 16)
							{
								//string m = (cboDetectBarBeats.SelectedIndex + 1).ToString();
								int dt = detectionMethod + 1;
								string m = dt.ToString();
								lineIn = lineIn.Replace("3", m);
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;
					case PLUGINaubioTracker:
						// Aubio Tracker
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("256", Annotator.StepSize.ToString());
							}
							if (lineCount == 28)
							{
								//string m = (cboDetectBarBeats.SelectedIndex + 1).ToString();
								int dt = detectionMethod + 1;
								string m = dt.ToString();
								lineIn = lineIn.Replace("3", m);
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
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


		// Required by ITransform inteface, wrapper to true ResultsToxTimings procedure requiring more parameters
		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType)
		{
			return ResultsToxTimings(resultsFile, alignmentType, labelType);
		}


		// The true ResultsToxTimings procedure requiring more parameters, (not compliant with ITransform inteface)

		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int err = 0;
			bool redo = true;

			if (xOnsets == null)
			{
				xOnsets = new xTimings(transformName);
			}
			if ((xOnsets.effects.Count > 0) && (!Annotator.ReuseResults))
			{
				xOnsets.effects.Clear();
			}


			//TODO Fix this so it works correctly
			//if ((xOnsets == null) || (!ReuseResults))
			//if (xOnsets == null)
			{
				int onsetCount = 0;
				string lineIn = "";
				int lastBeat = 0;
				int lastBar = -1;
				int beatLength = 0;
				int ppos = 0;
				int millisecs = 0;
				int midiNum = -1;
				string label = "1";
				int milliLen = 0;
				string noteNum = "0";
				//string[] parts;

				int countLines = 0;
				int countBars = 1;
				int countBeats = Annotator.FirstBeat;

				//int align = SequenceFunctions.GetAlignment(cboAlignBarsBeats.Text);

				switch (alignmentType)
				{
					case vamps.AlignmentType.FPS10:
						Annotator.FPS = 10;
						break;
					case vamps.AlignmentType.FPS20:
						Annotator.FPS = 20;
						break;
					case vamps.AlignmentType.FPS30:
						Annotator.FPS = 30;
						break;
					case vamps.AlignmentType.FPS40:
						Annotator.FPS = 40;
						break;
					case vamps.AlignmentType.FPS60:
						Annotator.FPS = 60;
						break;
					case vamps.AlignmentType.BeatsFull:
						Annotator.xAlignTo = Annotator.xBeatsFull;
						break;
					case vamps.AlignmentType.BeatsHalf:
						Annotator.xAlignTo = Annotator.xBeatsHalf;
						break;
					case vamps.AlignmentType.BeatsThird:
						Annotator.xAlignTo = Annotator.xBeatsThird;
						break;
					case vamps.AlignmentType.BeatsQuarter:
						Annotator.xAlignTo = Annotator.xBeatsQuarter;
						break;
				}
				Annotator.alignIdx = 0; // Reset


				// Pass 1, count lines
				StreamReader reader = new StreamReader(resultsFile);
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					countLines++;
				}
				reader.Close();


				// Pass 2, read data into arrays
				reader = new StreamReader(resultsFile);

				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();

					ppos = lineIn.IndexOf('.');
					if (ppos > xUtils.UNDEFINED)
					{

						string[] parts = lineIn.Split(',');

						millisecs = xUtils.ParseMilliseconds(parts[0]);
						millisecs = Annotator.AlignTimeTo(millisecs, alignmentType);
						lastBeat = millisecs;
						lastBar = millisecs;

						if (parts.Length > 1)
						{
							milliLen = xUtils.ParseMilliseconds(parts[1]);
						}
						if (parts.Length > 2)
						{
							noteNum = parts[2];
							if (noteNum.Length > 0)
							{
								midiNum = Int32.Parse(noteNum);
								if (midiNum > 0)
								{
									//label = SequenceFunctions.noteNamesUnicode[midiNum];
								}
							}
						}
					} // end line contains a period
				} // end while loop more lines remaining
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					ppos = lineIn.IndexOf('.');
					if (ppos > xUtils.UNDEFINED)
					{

						string[] parts = lineIn.Split(',');

						millisecs = xUtils.ParseMilliseconds(parts[0]);
						label = countBeats.ToString();
						// FULL BEATS - QUARTER NOTES
						//millisecs = xUtils.RoundTimeTo(millisecs, msPF);
						// Moved down to below > lastbeat check
						//millisecs = Annotator.AlignTimeTo(millisecs, alignmentType);
						beatLength = millisecs - lastBeat;
						if (parts.Length > 1)
						{
							milliLen = xUtils.ParseMilliseconds(parts[1]);
						}

						// Has it advanced since the last one?
						if (millisecs > lastBeat)
						{
							// OK, now align it
							millisecs = Annotator.AlignTimeTo(millisecs, alignmentType);
							// Is it still past the lastone after alignment?
							if (millisecs > lastBeat)
							{
								// Save it, add it to list
								xOnsets.Add(label, lastBeat, millisecs, midiNum);
								// update count
								countBeats++;
								// Remember this for next round (in order to skip ones which haven't advanced)
								lastBeat = millisecs;
							}
						}

						// Get length and midi number for next entry
						if (parts.Length > 2)
						{
							noteNum = parts[2];
							if (noteNum.Length > 0)
							{
								midiNum = Int32.Parse(noteNum);
								if (midiNum > 0)
								{
									//label = SequenceFunctions.noteNamesUnicode[midiNum];
								}
							}
						}
						onsetCount++;
					} // end line contains a period
				} // end while loop more lines remaining
				xOnsets.Add(label, lastBeat, millisecs, midiNum);

				reader.Close();
			}
			return err;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)
			
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xOnsets != null)
			{
				if (xOnsets.effects.Count > 0)
				{
					LORTimings4 gridOnsets = Annotator.Sequence.FindTimingGrid(transformName, true);
					SequenceFunctions.ImportTimingGrid(gridOnsets, xOnsets);
					// Save a reference to the Note Onsets timing grid
					Annotator.GridOnsets = gridOnsets;
					// If the Vamp Track's timing grid is currently set to Full Beats or is null
					//   switch it to Note Onsets.  (if anything else, leave it unchanged)
					if (Annotator.VampTrack.timingGrid == null)
					{ Annotator.VampTrack.timingGrid = gridOnsets; }
					else
					{
						string tname = Annotator.VampTrack.timingGrid.Name;
						if (tname == VampBarBeats.beatsFullName)
						{ Annotator.VampTrack.timingGrid = gridOnsets; }
					}
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			int errs = 0;

			//LORTrack4 vampTrack = SequenceFunctions.GetTrack("Vamp-O-Rama");
			LORChannelGroup4 beatGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
			if (xOnsets != null)
			{
				if (xOnsets.effects.Count > 0)
				{
					if (Annotator.UseRamps)
					{
						LORChannel4 chan = Annotator.Sequence.FindChannel(transformName, beatGroup.Members, true, true);
						SequenceFunctions.ImportNoteChannel(chan, xOnsets);
					}
				}
			}

			return errs;
		}

		public static int xTimingsToxLights(string baseFileName)
		{
			int errs = 0;


			return errs;
		}

	}
}
