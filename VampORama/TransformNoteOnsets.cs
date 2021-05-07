using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LORUtils;
using xUtilities;

namespace VampORama
{
	class NoteOnsets : ITransform
	{
		private const int PLUGINqmBarAndBeat = 0;
		private const int PLUGINqmTempo = 1;
		private const int PLUGINbeatRoot = 2;
		private const int PLUGINporto = 3;
		private const int PLUGINaubio = 4;
		public xTimings xOnsets = new xTimings("Note Onsets");
		public int BeatsPerBar = 4;
		public int FirstBeat = 1;
		public bool ReuseResults = false;

		public readonly string[] availablePluginNames = {	"Queen Mary Note Onset Detector",
																											"Queen Mary Polyphonic Transcription",
																											"OnsetDS Onset Detector",
																											"Silvet Note Transcription",
																											"Aubio Onset Detector",
																											"Aubio Note Tracker" };
		//																											"#Alicante Note Onset Detector",
		//																										"#Alicante Polyphonic Transcription" };

		private readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-onsetdetector:onsets",
																											"vamp:qm-vamp-plugins:qm-transcription:transcription",
																											"vamp:vamp-onsetsds:onsetsds:onsets",
																											"vamp:silvet:silvet:onsets",
																											"vamp:vamp-aubio:aubioonset:onsets",
																											"vamp:vamp-aubio:aubionotes:notes" };

		private readonly string[] availablePluginFiles = {"qm-onsetdetector.n3",
																											"qm-transcription.n3",
																											"onsetsds_onsetsds_onsets.n3",
																											"silvet_silvet_onsets.n3",
																											"aubio_aubioonset_onsets.n3",
																											"aubio_aubionotes_notes.n3" };

	private SequenceFunctions seqFunct = new SequenceFunctions();

		public NoteOnsets()
		{
			// Constructor

		}

		public NoteOnsets(Annotator annot)
		{
			// Alternate Constructor
			annotator = annot;
		}

		public string[] AvailablePluginNames
		{
			get
			{
				return availablePluginNames;
			}
		}

		private int pluginIndex = 0;

		public int UsePlugin
		{
			set
			{
				pluginIndex = value;
				if (pluginIndex < 0) pluginIndex = 0;
				if (pluginIndex >= availablePluginNames.Length) pluginIndex = availablePluginNames.Length - 1;
			}
			get
			{
				return pluginIndex;
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

		private Annotator annotator;

		public Annotator Annotator
		{
			set
			{
				annotator = Annotator;
			}
			get
			{
				return Annotator;
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

		public string AnnotateSong(string songFile, int pluginIndex)
		{
			return AnnotateSong(songFile, pluginIndex, 4, 512);
		}

		public string AnnotateSong(string songFile, int pluginIndex, int beatsPerBar, int stepSize, 
								bool reuse = false)
		{
			// Song file should have already been copied to the temp folder and named song.mp3
			// Annotator will use the same folder the song is in for it's files
			// Returns the name of the results file, which will also be in the same temp folder

			string results = "";
			int errs = 0;
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			int lineCount = 0;
			string lineIn = "";
			string fromVamp = ""; // vampConfigs;
			string toVamp = ""; // tempPath;
			string vampFile = "";
			int err = 0;
			//string rezults = "";
			BeatsPerBar = beatsPerBar;
			ReuseResults = reuse;
			StreamReader reader;
			StreamWriter writer;

			string tempPath = Path.GetDirectoryName(songFile);
			vampParams = availablePluginNames[pluginIndex];
			vampFile = vampParams.Replace(':', '_') + ".n3";
			fromVamp += vampFile;
			toVamp = tempPath + vampFile;

			try
			{
				switch (pluginIndex)
				{
					case PLUGINqmBarAndBeat:
						// Queen Mary Bar and Beat Tracker
						reader = new StreamReader(fromVamp);
						writer = new StreamWriter(toVamp);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("557", stepSize.ToString());
							}
							if (lineCount == 16)
							{
								if (beatsPerBar == 3)
								{
									lineIn = lineIn.Replace('4', '3');
								}
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;

					case PLUGINqmTempo:
						// Queen Mary Tempo and Beat Tracker
						reader = new StreamReader(fromVamp);
						writer = new StreamWriter(toVamp);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("557", stepSize.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;
					case 2:
						// BeatRoot Beat Tracker
						err = utils.SafeCopy(fromVamp, toVamp);
						break;
					case 3:
						// Porto Beat Tracker
						err = utils.SafeCopy(fromVamp, toVamp);
						break;
					case 4:
						// Aubio Beat Tracker
						err = utils.SafeCopy(fromVamp, toVamp);
						break;
				}
			} // End try
			catch (Exception e)
			{
				err = e.HResult;
				if (utils.IsWizard)
				{
					System.Diagnostics.Debugger.Break();
				}
			}


			if (err == 0)
			{
				results = Annotator.AnnotateSong("XX", songFile, toVamp, reuse);
			}

			return results;
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
			if ((xOnsets == null) || (!ReuseResults))
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

				if (alignmentType == vamps.AlignmentType.None)
				{
					fps = 1000;
					msPF = 1;
				}
				if (alignmentType == vamps.AlignmentType.FPS20)
				{
					fps = 20;
					msPF = 50;
				}
				if (alignmentType == vamps.AlignmentType.FPS40)
				{
					fps = 40;
					msPF = 25;
				}

				// Pass 1, count lines
				StreamReader reader = new StreamReader(resultsFile);
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					countLines++;
				}
				reader.Close();

				xOnsets = new xTimings("Note Onsets");

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
						millisecs = xUtils.RoundTimeTo(millisecs, msPF);
						lastBeat = millisecs;
						lastBar = millisecs;
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
						// FULL BEATS - QUARTER NOTES
						millisecs = xUtils.RoundTimeTo(millisecs, msPF);
						beatLength = millisecs - lastBeat;
						xOnsets.Add(countBeats.ToString(), lastBeat, millisecs);
						countBeats++;


						lastBeat = millisecs;
						onsetCount++;
					} // end line contains a period
				} // end while loop more lines remaining

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
