using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LORUtils; using FileHelper;
using xUtilities;

namespace UtilORama4
{
	class NoteOnsets : ITransform
	{
		public const int PLUGINqmOnset = 0;
		public const int PLUGINqmTranscribe = 1;
		public const int PLUGINOnsetDS = 2;
		public const int PLUGINSilvet = 3;
		public const int PLUGINaubioOnset = 4;
		public const int PLUGINaubioTracker = 5;
		public int lastPluginUsed = PLUGINqmOnset;

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

		public xTimings xOnsets = new xTimings("Note Onsets");
		public int BeatsPerBar = 4;
		public int FirstBeat = 1;
		public bool ReuseResults = false;
		public xTimings alignTimes = null;

		public readonly string[] availablePluginNames = {	"Queen Mary Note Onset Detector",
																											"Queen Mary Polyphonic Transcription",
																											"OnsetDS Onset Detector",
																											"Silvet Note Transcription",
																											"Aubio Onset Detector",
																											"Aubio Note Tracker" };
		//																											"#Alicante Note Onset Detector",
		//																										"#Alicante Polyphonic Transcription" };

		public readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-onsetdetector:onsets",
																											"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:vamp-onsetsds:onsetsds:onsets",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubioonset:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

		public readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-onsetdetector_onsets.n3",
																											"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																											"vamp_vamp-onsetsds_onsetsds_onsets.n3",
																											"vamp_silvet_silvet_onsets.n3",
																											"vamp_vamp-aubio_aubioonset_onsets.n3",
																											"vamp_vamp-aubio_aubionotes_notes.n3" };

		public static readonly string[] availableLabels = {"None",
																								"Note Names",
																								"Midi Note Numbers" };

	private SequenceFunctions seqFunct = new SequenceFunctions();

		public NoteOnsets()
		{
			// Constructor

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

		private readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None, vamps.AlignmentType.FPS20, vamps.AlignmentType.FPS40 };

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
				return xOnsets;
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
								lineIn = lineIn.Replace("512", stepSize.ToString());
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
								lineIn = lineIn.Replace("441", stepSize.ToString());
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
								lineIn = lineIn.Replace("1024", stepSize.ToString());
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
								lineIn = lineIn.Replace("256", stepSize.ToString());
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
								lineIn = lineIn.Replace("256", stepSize.ToString());
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

		// Required by ITransform inteface, wrapper to true ResultsToxTimings procedure requiring more parameters
		public int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType)
		{
			return ResultsToxTimings(resultsFile, alignmentType, labelType);
		}


		// The true ResultsToxTimings procedure requiring more parameters, (not compliant with ITransform inteface)

		public int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int err = 0;
			bool redo = true;

			if (xOnsets == null)
			{
				xOnsets = new xTimings("Note Onsets");
			}
			if ((xOnsets.effects.Count > 0) && (!ReuseResults))
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
				int subBeat = 0;
				int subSubBeat = 0;
				int subSubSubBeat = 0;
				int theTime = 0;
				int milliLen = 0;
				string noteNum = "";
				int midiNum = -1;
				string label = "1";

				int countLines = 0;
				int countBars = 1;
				int countBeats = FirstBeat;
				int countHalves = 1;
				int countThirds = 1;
				int countQuarters = 1;
				int maxBeats = BeatsPerBar;
				int maxHalves = BeatsPerBar * 2;
				int maxThirds = BeatsPerBar * 3;
				int maxQuarters = BeatsPerBar * 4;

				//int align = seqFunct.GetAlignment(cboAlignBarsBeats.Text);

				fps = 1000;
				msPF = 1;
				switch (alignmentType)
				{
					case vamps.AlignmentType.FPS10:
						fps = 10;
						msPF = 100;
						break;
					case vamps.AlignmentType.FPS20:
						fps = 20;
						msPF = 50;
						break;
					case vamps.AlignmentType.FPS30:
						fps = 30;
						msPF = 33;
						break;
					case vamps.AlignmentType.FPS40:
						fps = 40;
						msPF = 25;
						break;
					case vamps.AlignmentType.FPS60:
						fps = 60;
						msPF = 17;
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
									//label = SequenceFunctions.noteNames[midiNum];
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
									//label = SequenceFunctions.noteNames[midiNum];
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


		public int xTimingsToLORtimings(xTimings timings, Sequence4 sequence)
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)
			
			seqFunct.Sequence = sequence;
			int errs = 0;

			if (xOnsets != null)
			{
				if (xOnsets.effects.Count > 0)
				{
					TimingGrid barGrid = seqFunct.GetGrid("Note Onsets", true);
					seqFunct.ImportTimingGrid(barGrid, xOnsets);
				}
			}

			return errs;
		}

		public int xTimingsToLORChannels(xTimings timings, Sequence4 sequence)
		{
			return xTimingsToLORChannels(timings, sequence, 1, false);
		}

		public int xTimingsToLORChannels(xTimings timings, Sequence4 sequence, int firstBeat, bool ramps)
		{
			int errs = 0;

			Track vampTrack = seqFunct.GetTrack("Vamp-O-Rama");
			ChannelGroup beatGroup = seqFunct.GetGroup("Bars and Beats", vampTrack);
			if (xOnsets != null)
			{
				if (xOnsets.effects.Count > 0)
				{
					if (ramps)
					{
						Channel barCh = seqFunct.GetChannel("Bars", beatGroup.Members);
						seqFunct.ImportBeatChannel(barCh, xOnsets, 1, firstBeat, ramps);
					}
				}
			}

			return errs;
		}

		public int xTimingsToxLights(xTimings timings, string baseFileName)
		{
			int errs = 0;


			return errs;
		}

	}
}
