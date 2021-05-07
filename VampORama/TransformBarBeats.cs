using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LORUtils;
using xUtilities;

namespace VampORama
{
	public class BarBeats : ITransform
	{
		private const int PLUGINqmBarAndBeat = 0;
		private const int PLUGINqmTempo = 1;
		private const int PLUGINbeatRoot = 2;
		private const int PLUGINporto = 3;
		private const int PLUGINaubio = 4;
		public xTimings xBars = new xTimings("Bars" + " (Whole notes, (4 Quarter notes))");
		public xTimings xBeatsFull = new xTimings("Beats-Full (Quarter notes)");
		public xTimings xBeatsHalf = new xTimings("Beats-Half (Eighth notes)");
		public xTimings xBeatsThird = new xTimings("Beats-Third (Twelth notes)");
		public xTimings xBeatsQuarter = new xTimings("Beats-Quarter (Sixteenth notes)");
		public xTimings xFrames = new xTimings("Frames");
		public int BeatsPerBar = 4;
		public int FirstBeat = 1;
		public bool ReuseResults = false;
		public int totalCentiseconds = 0;
		public int totalMilliseconds = 0;

		private Annotator myAnnotator = null;

		public readonly string[] availablePluginNames = {	"Queen Mary Bar and Beat Tracker",
																											"Queen Mary Tempo and Beat Tracker",
																											"BeatRoot Beat Tracker",
																											"Porto Beat Tracker",
																											"Aubio Beat Tracker"};

		private readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-barbeattracker:beats",
																											"vamp:qm-vamp-plugins:qm-tempotracker:beats",
																											"vamp:beatroot-vamp:beatroot:beats",
																											"vamp:mvamp-ibt:marsyas_ibt:beat_times",
																											"vamp:vamp-aubio:aubiotempo:beats" };

		private readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-barbeattracker_beats.n3",
																											"vamp_qm-vamp-plugins_qm-tempotracker_output_beats.n3",
																											"vamp_beatroot_beats.n3",
																											"mvamp-ibt_marsyas_ibt_beat_times.n3",
																											"vamp-aubio_aubiotempo_beats.n3" };

		private string fileConfigBase = "vamp_qm-vamp-plugins_qm-barbeattracker_beats";

	private SequenceFunctions seqFunct = new SequenceFunctions();

		public BarBeats()
		{
			// Constructor

		}

		public BarBeats(Annotator annot)
		{
			// Alternate Constructor
			myAnnotator = annot;
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
		public const int METHODdomain = 0;
		public const int METHODspectral = 1;
		public const int METHODphase = 2;
		public const int METHODenergy = 3;

		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public readonly string[] DetectionMethodNames = {"'Complex Domain' (Strings/Mixed: Piano, Guitar)",
																				"'Spectral Difference' (Percussion: Drums, Chimes)",
																				"'Phase Deviation' (Wind: Flute, Sax, Trumpet)",
																				"'Broadband Energy Rise' (Percussion mixed with other)"};

		private DetectionMethods detectionMethod = BarBeats.DetectionMethods.ComplexDomain;

		public DetectionMethods DetectionMethod
		{
			set
			{
				detectionMethod = value;
			}
			get
			{
				return detectionMethod;
			}
		}


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


		public Annotator Annotator
		{
			set
			{
				myAnnotator = Annotator;
			}
			get
			{
				return myAnnotator;
			}
		}

		public xTimings Timings
		{
			get
			{
				return xBars;
			}
		}
		public int TransformationType
		{
			get
			{
				return 1;
			}
		}

		public string TransformationName
		{
			get
			{
				return "Bars and Beats";
			}
		}

		public string AnnotateSong(string songFile, int pluginIndex)
		{
			return AnnotateSong(songFile, pluginIndex, 4, 512);
		}

		public string AnnotateSong(string fileSong, int pluginIndex, int beatsPerBar, int stepSize, 
								int detectionMethod = METHODdomain, bool reuse = false, bool whiten = true)
		{
			// Song file should have already been copied to the temp folder and named song.mp3
			// Annotator will use the same folder the song is in for it's files
			// Returns the name of the results file, which will also be in the same temp folder

			string results = "";
			int err = 0;
			string fileConfig = filesAvailableConfigs[pluginIndex];
			string vampParams = availablePluginCodes[pluginIndex];
			string pathConfigs = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
	
			int lineCount = 0;
			string lineIn = "";
			BeatsPerBar = beatsPerBar;
			ReuseResults = reuse;
			StreamReader reader;
			StreamWriter writer;

			string fileConfigFrom = pathConfigs + fileConfig;
			string fileConfigTo = pathWork + fileConfig;

			try
			{
				switch (pluginIndex)
				{
					case PLUGINqmBarAndBeat:
						// Queen Mary Bar and Beat Tracker
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
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
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 7)
							{
								lineIn = lineIn.Replace("557", stepSize.ToString());
							}
							if (lineCount == 20)
							{
								//string m = (cboDetectBarBeats.SelectedIndex + 1).ToString();
								int dt = detectionMethod + 1;
								string m = dt.ToString();
								lineIn = lineIn.Replace("3", m);
							}
							if (lineCount == 32)
							{
								if (whiten)
								{
									lineIn = lineIn.Replace('0', '1');
								}
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;
					case PLUGINbeatRoot:
						// BeatRoot Beat Tracker
						err = utils.SafeCopy(fileConfigFrom, fileConfigTo);
						break;
					case PLUGINporto:
						// Porto Beat Tracker
						err = utils.SafeCopy(fileConfigFrom, fileConfigTo);
						break;
					case PLUGINaubio:
						// Aubio Beat Tracker
						err = utils.SafeCopy(fileConfigFrom, fileConfigTo);
						break;
				}
			} // End try
			catch (Exception e)
			{
				err = e.HResult;
				if (utils.IsWizard)
				{
					string msg = e.Message;
					string ermsg = "Error: " + msg;
					ermsg += " copying " + fileConfigFrom;
					ermsg += " to " + fileConfigTo;
					DialogResult dr = MessageBox.Show(ermsg, "Config File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					System.Diagnostics.Debugger.Break();
				}
			}


			if (err == 0)
			{
				results = Annotator.AnnotateSong(fileSong, vampParams, fileConfigTo, reuse);
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
			int err = 0;
			if ((xBars == null) || (xBars.effects.Count < 2) || (!ReuseResults))
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

				if (alignmentType == vamps.AlignmentType.FPS20)
				{
					fps = 20;
					msPF = 50;
				}
				else
				{
					if (alignmentType == vamps.AlignmentType.FPS40)
					{
						fps = 40;
						msPF = 25;
					}
					else
					{
						// if (alignmentType == vamps.AlignmentType.None)
						// or if alignmentType == anything else (which would be invalid!)
						//{
							fps = 1000;
							msPF = 1;
						//}

					}
				}








				// Pass 1, count lines
				StreamReader reader = new StreamReader(resultsFile);
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					countLines++;
				}
				reader.Close();

				xBars = new xTimings("Bars" + " (Whole notes, (" + BeatsPerBar.ToString() + " Quarter notes))");
				xBeatsFull = new xTimings("Beats-Full (Quarter notes)");
				xBeatsHalf = new xTimings("Beats-Half (Eighth notes)");
				xBeatsThird = new xTimings("Beats-Third (Twelth notes)");
				xBeatsQuarter = new xTimings("Beats-Quarter (Sixteenth notes)");

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
						xBeatsFull.Add(countBeats.ToString(), lastBeat, millisecs);
						countBeats++;

						// BARS
						if (countBeats > maxBeats)
						{
							countBeats = 1;
							xBars.Add(countBars.ToString(), lastBar, millisecs);
							countBars++;
							lastBar = millisecs;
						}

						// HALF BEATS - EIGHTH NOTES
						subBeat = lastBeat + (beatLength / 2);
						subBeat = xUtils.RoundTimeTo(subBeat, msPF);
						xBeatsHalf.Add(countHalves.ToString(), lastBeat, subBeat);
						countHalves++;

						xBeatsHalf.Add(countHalves.ToString(), subBeat, millisecs);
						countHalves++;
						if (countHalves > maxHalves) countHalves = 1;

						// THIRD BEATS - TWELTH NOTES
						subBeat = lastBeat + (beatLength / 3);
						subBeat = xUtils.RoundTimeTo(subBeat, msPF);
						xBeatsThird.Add(countThirds.ToString(), lastBeat, subBeat);
						countThirds++;

						subSubBeat = lastBeat + (beatLength * 2 / 3);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, msPF);
						xBeatsThird.Add(countThirds.ToString(), subBeat, subSubBeat);
						countThirds++;

						xBeatsThird.Add(countThirds.ToString(), subSubBeat, millisecs);
						countThirds++;
						if (countThirds > maxThirds) countThirds = 1;

						// QUARTER BEATS - SIXTEENTH NOTES
						subBeat = lastBeat + (beatLength / 4);
						subBeat = xUtils.RoundTimeTo(subBeat, msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), lastBeat, subBeat);
						countQuarters++;

						subSubBeat = lastBeat + (beatLength / 2);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), subBeat, subSubBeat);
						countQuarters++;

						subSubSubBeat = lastBeat + (beatLength * 3 / 4);
						subSubSubBeat = xUtils.RoundTimeTo(subSubSubBeat, msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), subSubBeat, subSubSubBeat);
						countQuarters++;

						xBeatsQuarter.Add(countQuarters.ToString(), subSubSubBeat, millisecs);
						countQuarters++;
						if (countQuarters > maxQuarters) countQuarters = 1;



						lastBeat = millisecs;
						onsetCount++;
					} // end line contains a period
				} // end while loop more lines remaining

				reader.Close();
				totalMilliseconds = lastBeat;
				totalCentiseconds = lastBeat / 10;
				//int t = xBars.effects[0].starttime;
				int t = 0;
				int f = 1;
				if (alignmentType == vamps.AlignmentType.FPS10)
				{
					xFrames.timingName = "Frames 10FPS, 10cs";
					while (t <= totalMilliseconds)
					{
						if (t>0)
						{
							xFrames.Add(f.ToString(), t, t+100);
						}
						t += 100;
						f++;

					}
				}
				if (alignmentType == vamps.AlignmentType.FPS20)
				{
					xFrames.timingName = "Frames 20FPS, 5cs";
					while (t <= totalMilliseconds)
					{
						if (t > 0)
						{
							xFrames.Add(f.ToString(), t, t + 50);
						}
						t += 50;
						f++;
					}
				}
				if (alignmentType == vamps.AlignmentType.FPS30)
				{
					xFrames.timingName = "Frames 30FPS, 3.33cs";
					while (t <= totalMilliseconds)
					{
						if (t > 0)
						{
							xFrames.Add(f.ToString(), t, t + 33);
							f++;
							xFrames.Add(f.ToString(), t + 33, t + 67);
							f++;
							xFrames.Add(f.ToString(), t + 67, t + 100);
						}
						t += 100;
						f++;
					}
				}
				if (alignmentType == vamps.AlignmentType.FPS40)
				{
					xFrames.timingName = "Frames 40FPS, 2.5cs";
					while (t < totalMilliseconds)
					{
						if (t > 0)
						{
							xFrames.Add(f.ToString(), t, t + 25);
						}
						t += 25;
						f++;
					}
				}
				if (alignmentType == vamps.AlignmentType.FPS60)
				{
					xFrames.timingName = "Frames 60FPS, 1.667cs";
					while (t <= totalMilliseconds)
					{
						if (t > 0)
						{
							xFrames.Add(f.ToString(), t, t + 17);
							f++;
							xFrames.Add(f.ToString(), t + 17, t + 33);
							f++;
							xFrames.Add(f.ToString(), t + 33, t + 50);
							f++;
							xFrames.Add(f.ToString(), t + 50, t + 67);
							f++;
							xFrames.Add(f.ToString(), t + 67, t + 83);
							f++;
							xFrames.Add(f.ToString(), t + 83, t + 100);
						}
						t += 100;
						f++;
					}
				}


			}
			return err;
		} // end Beats


		public int xTimingsToLORtimings(xTimings timings, Sequence4 sequence)
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)
			
			seqFunct.Sequence = sequence;
			int errs = 0;

			if (xBars != null)
			{
				if (xBars.effects.Count > 0)
				{
					TimingGrid barGrid = seqFunct.GetGrid("Bars", true);
					seqFunct.ImportTimingGrid(barGrid, xBars);
				}
			}
			//if (chkBeatsFull.Checked)
			if (true)
			{
				if (xBeatsFull != null)
				{
					if (xBeatsFull.effects.Count > 0)
					{
						TimingGrid barGrid = seqFunct.GetGrid("Beats-Full", true);
						seqFunct.ImportTimingGrid(barGrid, xBeatsFull);
					}
				}
			}
			//if (chkBeatsHalf.Checked)
			if (true)
			{
				if (xBeatsHalf != null)
				{
					if (xBeatsHalf.effects.Count > 0)
					{
						TimingGrid barGrid = seqFunct.GetGrid("Beats-Half", true);
						seqFunct.ImportTimingGrid(barGrid, xBeatsHalf);
					}
				}
			}
			//if (chkBeatsThird.Checked)
			if (true)
			{
				if (xBeatsThird != null)
				{
					if (xBeatsThird.effects.Count > 0)
					{
						TimingGrid barGrid = seqFunct.GetGrid("Beats-Third", true);
						seqFunct.ImportTimingGrid(barGrid, xBeatsThird);
					}
				}
			}
			//if (chkBeatsQuarter.Checked)
			if (true)
			{
				if (xBeatsQuarter != null)
				{
					if (xBeatsQuarter.effects.Count > 0)
					{
						TimingGrid barGrid = seqFunct.GetGrid("Beats-Quarter", true);
						seqFunct.ImportTimingGrid(barGrid, xBeatsQuarter);
					}
				}
			}
			if (xFrames != null)
			{
				if (xFrames.effects.Count > 0)
				{
					TimingGrid barFrame = seqFunct.GetGrid(xFrames.timingName, true);
					seqFunct.ImportTimingGrid(barFrame, xFrames);
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

			Track vampTrack = seqFunct.GetTrack("Vamp-O-Rama", true);
			ChannelGroup beatGroup = seqFunct.GetGroup("Bars and Beats", vampTrack);
			if (xBars != null)
			{
				if (xBars.effects.Count > 0)
				{
					//if (ramps)
					//{
					Channel barCh = seqFunct.GetChannel("Bars", beatGroup.Members);
					//seqFunct.ImportBeatChannel(barCh, xBars, 1, firstBeat, ramps);
					//seqFunct.ImportBeatChannel(barCh, xBars, 1, firstBeat, true);
					seqFunct.ImportBeatChannel(barCh, xBars, 1, firstBeat, ramps);
					//////}
					//}
				}
			}
			//if (chkBeatsFull.Checked)
			if (true)
			{
				if (xBeatsFull != null)
				{
					if (xBeatsFull.effects.Count > 0)
					{
						Channel beatCh = seqFunct.GetChannel("Beats-Full", beatGroup.Members);
						seqFunct.ImportBeatChannel(beatCh, xBeatsFull, BeatsPerBar, firstBeat, ramps);
					}
				}
			}
			//if (chkBeatsHalf.Checked)
			if (true)
			{
				if (xBeatsHalf != null)
				{
					if (xBeatsHalf.effects.Count > 0)
					{
						Channel beatCh = seqFunct.GetChannel("Beats-Half", beatGroup.Members);
						seqFunct.ImportBeatChannel(beatCh, xBeatsHalf, BeatsPerBar * 2, firstBeat, ramps);
					}
				}
			}
			//if (chkBeatsThird.Checked)
			if (true)
			{
				if (xBeatsThird != null)
				{
					if (xBeatsThird.effects.Count > 0)
					{
						Channel beatCh = seqFunct.GetChannel("Beats-Third", beatGroup.Members);
						seqFunct.ImportBeatChannel(beatCh, xBeatsThird, BeatsPerBar * 3, firstBeat, ramps);
					}
				}
			}
			//if (chkBeatsQuarter.Checked)
			if (true)
			{
				if (xBeatsQuarter != null)
				{
					if (xBeatsQuarter.effects.Count > 0)
					{
						Channel beatCh = seqFunct.GetChannel("Beats-Quarter", beatGroup.Members);
						seqFunct.ImportBeatChannel(beatCh, xBeatsQuarter, BeatsPerBar * 4, firstBeat, ramps);
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
