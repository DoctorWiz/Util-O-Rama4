using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LORUtils4; using FileHelper;
using xUtilities;

namespace UtilORama4
{
	public static class VampBarBeats //: ITransform
	{
		public const string transformName = "Bars and Beats";
		public const string barsName = "Bars (Whole notes, (4 Quarter notes))";
		public const string beatsFullName = "Beats-Full (Quarter notes)";
		public const string beatsHalfName = "Beats-Half (Eighth notes)";
		public const string beatsThirdName = "Beats-Third (Twelth notes)";
		public const string beatsQuarterName = "Beats-Quarter (Sixteenth notes)";
		public const string framesName = "Frames";



		public const int PLUGINqmBarAndBeat = 0;
		public const int PLUGINqmTempo = 1;
		public const int PLUGINbeatRoot = 2;
		//public const int PLUGINporto = 3;
		public const int PLUGINaubio = 3;
		public static xTimings xBars = Annotator.xBars; // new xTimings(barsName);
		public static xTimings xBeatsFull = Annotator.xBeatsFull; // new xTimings(beatsFullName);
		public static xTimings xBeatsHalf = Annotator.xBeatsHalf; // new xTimings(beatsHalfName);
		public static xTimings xBeatsThird = Annotator.xBeatsThird; // new xTimings(beatsThirdName);
		public static xTimings xBeatsQuarter = Annotator.xBeatsQuarter; // new xTimings(beatsQuarterName);
		public static xTimings xFrames = Annotator.xFrames; // new xTimings(framesName);
																												//public static int BeatsPerBar = 4;
																												//public static int FirstBeat = 1;
																												//public bool ReuseResults = false;
																												//public int totalCentiseconds = 0;
																												//public int totalMilliseconds = 0;
		public static string ResultsFile = "song_vamp_qm-vamp-plugins_qm-barbeattracker_beats.csv";

		//private Annotator myAnnotator = null;

		public static readonly string[] availablePluginNames = {	"Queen Mary Bar and Beat Tracker",
																											"Queen Mary Tempo and Beat Tracker",
																											"BeatRoot Beat Tracker",
																											// Does not work with latest Sonic Annotator, and 32-bit only (for older Annotator)
																											//"Porto Beat Tracker",
																											"Aubio Beat Tracker"};

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-barbeattracker:beats",
																											"vamp:qm-vamp-plugins:qm-tempotracker:beats",
																											"vamp:beatroot-vamp:beatroot:beats",
																											//"vamp:mvamp-ibt:marsyas_ibt:beat_times",
																											"vamp:vamp-aubio:aubiotempo:beats" };

		public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-barbeattracker_beats.n3",
																											"vamp_qm-vamp-plugins_qm-tempotracker_output_beats.n3",
																											"vamp_beatroot-vamp_beatroot_beats.n3",
																											//"mvamp-ibt_marsyas_ibt_beat_times.n3",
																											"vamp_vamp-aubio_aubiotempo_beats.n3" };

		public static readonly string[] availableLabels = { vamps.LABELNAMEnone, vamps.LABELNAMEnumbers };

		private static string fileConfigBase = "vamp_qm-vamp-plugins_qm-barbeattracker_beats";
		private static int pluginIndex = 0;

		public static int UsePlugin
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

		public static readonly string[] DetectionMethodNames = {"'Complex Domain' (Strings/Mixed: Piano, Guitar)",
																				"'Spectral Difference' (Percussion: Drums, Chimes)",
																				"'Phase Deviation' (Wind: Flute, Sax, Trumpet)",
																				"'Broadband Energy Rise' (Percussion mixed with other)"};

		private static DetectionMethods detectionMethod = VampBarBeats.DetectionMethods.ComplexDomain;

		public static DetectionMethods DetectionMethod
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


		private static readonly vamps.LabelTypes[] allowableLabels = { vamps.LabelTypes.None, vamps.LabelTypes.Numbers };

		public static xTimings Timings
		{
			get
			{
				return xBars;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.BarsAndBeats;
			}
		}

		public static string TransformationName
		{
			get
			{
				return transformName;
			}
		}

		//public static int PrepareToVamp(string fileSong, int pluginIndex, int detectionMethod = METHODdomain)
		public static int PrepareVampConfig(int pluginIndex, int detectionMethod = METHODdomain)
		{
			// Song file should have already been copied to the temp folder and named song.mp3
			// Annotator will use the same folder the song is in for it's files
			// Returns the name of the results file, which will also be in the same temp folder

			int err = 0;
			string fileConfig = filesAvailableConfigs[pluginIndex];
			//string vampParams = availablePluginCodes[pluginIndex];
			string pathConfigs = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			//string pathWork = Path.GetDirectoryName(fileSong) + "\\";
	
			int lineCount = 0;
			string lineIn = "";
			StreamReader reader;
			StreamWriter writer;

			string fileConfigFrom = pathConfigs + fileConfig;
			string fileConfigTo = Annotator.WorkPath + fileConfig;

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
								lineIn = lineIn.Replace("557", Annotator.StepSize.ToString());
							}
							if (lineCount == 16)
							{
								if (Annotator.BeatsPerBar == 3)
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
								lineIn = lineIn.Replace("557", Annotator.StepSize.ToString());
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
								if (Annotator.Whiten)
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
					//case PLUGINporto:
					// Porto Beat Tracker
					//err = lutils.SafeCopy(fileConfigFrom, fileConfigTo);
					//break;
					case PLUGINaubio:
						// Aubio Beat Tracker
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
							if (lineCount == 12)
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
				if (Fyle.DebugMode)
				{
					string msg = e.Message;
					string ermsg = "Error: " + msg;
					ermsg += " copying " + fileConfigFrom;
					ermsg += " to " + fileConfigTo;
					Fyle.MakeNoise(Fyle.Noises.WrongButton);
					DialogResult dr = MessageBox.Show(ermsg, "Config File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					System.Diagnostics.Debugger.Break();
				}
			}


			if (err == 0)
			{
				//results = Annotator.AnnotateSong(fileSong, vampParams, fileConfigTo, reuse);
			}

			return err;
		}


		// Required by ITransform inteface, wrapper to true ResultsToxTimings procedure requiring more parameters
		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelTypes labelType)
		{
			int err = 0;
			if ((xBars == null) || (xBars.effects.Count < 2) || (!Annotator.ReuseResults))
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
				int countBeats = Annotator.FirstBeat;
				int countHalves = 1;
				int countThirds = 1;
				int countQuarters = 1;
				int maxBeats = Annotator.BeatsPerBar;
				int maxHalves = Annotator.BeatsPerBar * 2;
				int maxThirds = Annotator.BeatsPerBar * 3;
				int maxQuarters = Annotator.BeatsPerBar * 4;

				//int align = SequenceFunctions.GetAlignment(cboAlignBarsBeats.Text);

				if (alignmentType == vamps.AlignmentType.FPS20)
				{
					Annotator.FPS = 20;
					//Annotator.msPF = 50;
				}
				else
				{
					if (alignmentType == vamps.AlignmentType.FPS40)
					{
						Annotator.FPS = 40;
						//Annotator.msPF = 25;
					}
					else
					{
						// if (alignmentType == vamps.AlignmentType.None)
						// or if alignmentType == anything else (which would be invalid!)
						//{
							Annotator.FPS = 1000;
							//msPF = 1;
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

				xBars					= new xTimings("Bars" + " (Whole notes, (" + Annotator.BeatsPerBar.ToString() + " Quarter notes))");
				xBeatsFull		= new xTimings(beatsFullName);
				xBeatsHalf		= new xTimings(beatsHalfName);
				xBeatsThird		= new xTimings(beatsThirdName);
				xBeatsQuarter = new xTimings(beatsQuarterName);
				xFrames				= new xTimings("Frames");

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
						millisecs = xUtils.RoundTimeTo(millisecs, Annotator.msPF);
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
						millisecs = xUtils.RoundTimeTo(millisecs, Annotator.msPF);
						beatLength = millisecs - lastBeat;
						xBeatsFull.Add(countBeats.ToString(), lastBeat, millisecs,countBeats);
						countBeats++;

						// BARS
						if (countBeats > maxBeats)
						{
							countBeats = 1;
							xBars.Add(countBars.ToString(), lastBar, millisecs,countBars);
							countBars++;
							lastBar = millisecs;
						}

						// HALF BEATS - EIGHTH NOTES
						subBeat = lastBeat + (beatLength / 2);
						subBeat = xUtils.RoundTimeTo(subBeat, Annotator.msPF);
						xBeatsHalf.Add(countHalves.ToString(), lastBeat, subBeat,countHalves);
						countHalves++;

						xBeatsHalf.Add(countHalves.ToString(), subBeat, millisecs, countHalves);
						countHalves++;
						if (countHalves > maxHalves) countHalves = 1;

						// THIRD BEATS - TWELTH NOTES
						subBeat = lastBeat + (beatLength / 3);
						subBeat = xUtils.RoundTimeTo(subBeat, Annotator.msPF);
						xBeatsThird.Add(countThirds.ToString(), lastBeat, subBeat, countThirds);
						countThirds++;

						subSubBeat = lastBeat + (beatLength * 2 / 3);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, Annotator.msPF);
						xBeatsThird.Add(countThirds.ToString(), subBeat, subSubBeat, countThirds);
						countThirds++;

						xBeatsThird.Add(countThirds.ToString(), subSubBeat, millisecs, countThirds);
						countThirds++;
						if (countThirds > maxThirds) countThirds = 1;

						// QUARTER BEATS - SIXTEENTH NOTES
						subBeat = lastBeat + (beatLength / 4);
						subBeat = xUtils.RoundTimeTo(subBeat, Annotator.msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), lastBeat, subBeat, countQuarters);
						countQuarters++;

						subSubBeat = lastBeat + (beatLength / 2);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, Annotator.msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), subBeat, subSubBeat, countQuarters);
						countQuarters++;

						subSubSubBeat = lastBeat + (beatLength * 3 / 4);
						subSubSubBeat = xUtils.RoundTimeTo(subSubSubBeat, Annotator.msPF);
						xBeatsQuarter.Add(countQuarters.ToString(), subSubBeat, subSubSubBeat, countQuarters);
						countQuarters++;

						xBeatsQuarter.Add(countQuarters.ToString(), subSubSubBeat, millisecs, countQuarters);
						countQuarters++;
						if (countQuarters > maxQuarters) countQuarters = 1;



						lastBeat = millisecs;
						onsetCount++;
					} // end line contains a period
				} // end while loop more lines remaining

				reader.Close();
				Annotator.TotalMilliseconds = lastBeat;
				Annotator.TotalCentiseconds = lastBeat / 10;
				//int t = xBars.effects[0].starttime;
				int t = 0;
				int f = 1;
				if (alignmentType == vamps.AlignmentType.FPS10)
				{
					xFrames.Name = "Frames 10FPS, 10cs";
					while (t <= Annotator.TotalMilliseconds)
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
					xFrames.Name = "Frames 20FPS, 5cs";
					while (t <= Annotator.TotalMilliseconds)
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
					xFrames.Name = "Frames 30FPS, 3.33cs";
					while (t <= Annotator.TotalMilliseconds)
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
					xFrames.Name = "Frames 40FPS, 2.5cs";
					while (t < Annotator.TotalMilliseconds)
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
					xFrames.Name = "Frames 60FPS, 1.667cs";
					while (t <= Annotator.TotalMilliseconds)
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
			// Temp, for debugging while stepping thru code
			int bc = xBars.effects.Count;
			int bf = xBeatsFull.effects.Count;


			Annotator.xBars = xBars;
			Annotator.xBeatsFull = xBeatsFull;
			Annotator.xBeatsHalf = xBeatsHalf;
			Annotator.xBeatsThird = xBeatsThird;
			Annotator.xBeatsQuarter = xBeatsQuarter;
			Annotator.xFrames = xFrames;
			return err;
		} // end Beats

		public static int xTimingsToLORtimings()
		{
			int errs = 0;
			xTimingToLORtimings(xBars);
			Annotator.GridBeats = xTimingToLORtimings(xBeatsFull);
			if (Annotator.VampTrack.timingGrid == null) Annotator.VampTrack.timingGrid = Annotator.GridBeats;
			xTimingToLORtimings(xBeatsHalf);
			xTimingToLORtimings(xBeatsThird);
			xTimingToLORtimings(xBeatsQuarter);
			//errs += xTimingToLORtimings(xFrames);
			Annotator.Sequence.CentiFix();
			return errs;
		}

		public static LORTimings4 xTimingToLORtimings(xTimings timings)
		{
			LORTimings4 beatGrid = null;
			int errs = 0;
			string theName = timings.Name;
			if (timings != null)
			{
				if (timings.effects.Count > 0)
				{
					int tec = timings.effects.Count;
					beatGrid = Annotator.Sequence.FindTimingGrid(theName, true);
					errs = SequenceFunctions.ImportTimingGrid(beatGrid, timings);
					if (tec != beatGrid.timings.Count)
					{
						string msg = "Warning:\r\nxTimings '" + theName + "' has ";
						msg += timings.effects.Count.ToString() + " effects, but\r\n";
						msg += "Timing Grid '" + theName + "' has ";
						msg += beatGrid.timings.Count.ToString() + " effects.\r\n";
						msg += "   (This may be because of tightly close timings)";
						Fyle.BUG(msg);
					}
				}
			}
			return beatGrid;
		}




		private static int OLD_xTimingsToLORtimings(xTimings timings, LORSequence4 sequence)
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)
			
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xBars != null)
			{
				if (xBars.effects.Count > 0)
				{
					LORTimings4 gridBars = sequence.FindTimingGrid(xBars.Name, true);
					SequenceFunctions.ImportTimingGrid(gridBars, xBars);
				}
			}
			//if (chkBeatsFull.Checked)
			if (true)
			{
				if (xBeatsFull != null)
				{
					if (xBeatsFull.effects.Count > 0)
					{
						LORTimings4 gridBeats = sequence.FindTimingGrid(beatsFullName, true);
						SequenceFunctions.ImportTimingGrid(gridBeats, xBeatsFull);
						Annotator.GridBeats = gridBeats;
						if (Annotator.VampTrack.timingGrid == null)
						{
							Annotator.VampTrack.timingGrid = gridBeats;
						}
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
						LORTimings4 gridHalf = sequence.FindTimingGrid(beatsHalfName, true);
						SequenceFunctions.ImportTimingGrid(gridHalf, xBeatsHalf);
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
						LORTimings4 gridThird = sequence.FindTimingGrid(beatsThirdName, true);
						SequenceFunctions.ImportTimingGrid(gridThird, xBeatsThird);
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
						LORTimings4 gridQuarter = sequence.FindTimingGrid(beatsQuarterName, true);
						SequenceFunctions.ImportTimingGrid(gridQuarter, xBeatsQuarter);
					}
				}
			}
			if (xFrames != null)
			{
				if (xFrames.effects.Count > 0)
				{
					LORTimings4 barFrame = sequence.FindTimingGrid(xFrames.Name, true);
					SequenceFunctions.ImportTimingGrid(barFrame, xFrames);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			// NOTE: Exports all 5 (bars, full beats, half beats, thirds, quarters)
			int errs = 0;
			if (Annotator.UseRamps)
			{
				errs += xTimingToLORChannels(xBars);
				errs += xTimingToLORChannels(xBeatsFull);
				errs += xTimingToLORChannels(xBeatsHalf);
				errs += xTimingToLORChannels(xBeatsThird);
				errs += xTimingToLORChannels(xBeatsQuarter);
			}
			else
			{
				// Actually Bars
				errs += xTimingToLORChannels(xBeatsQuarter, xBars.Name, Annotator.BeatsPerBar * 4);
				// Actually Full Beats
				errs += xTimingToLORChannels(xBeatsQuarter, xBeatsFull.Name, Annotator.BeatsPerBar);
				// Actually Half Beats
				errs += xTimingToLORChannels(xBeatsQuarter, xBeatsHalf.Name, 2);
				// Third Beats
				errs += xTimingToLORChannels(xBeatsThird);
				// Finally, Quarter Beats
				errs += xTimingToLORChannels(xBeatsQuarter);
			}
			
			return errs;
		}


		public static int xTimingToLORChannels(xTimings timings, string beatName = "", int divider = 1) // Note lack of an s
		{
			// NOTE: Exports a single timing
			int errs = 0;
			if (timings != null)
			{
				if (timings.effects.Count > 0)
				{
					if (beatName == "") beatName = timings.Name;
					int tec = timings.effects.Count;
					//int bpb = Annotator.BeatsPerBar;
					string tname = timings.Name.Substring(6, 4);
					LORChannelGroup4 beatGroup = Annotator.Sequence.FindChannelGroup("Bars and Beats", Annotator.VampTrack.Members, true);
					LORChannel4 chan = Annotator.Sequence.FindChannel(beatName, beatGroup.Members, true, true);
					errs = SequenceFunctions.ImportBeatChannel(chan, timings, divider);
				}
			}

			return errs;
		}







		/*
				public xTimings xBars = new xTimings("Bars" + " (Whole notes, (4 Quarter notes))");
public xTimings xBeatsFull = new xTimings("Beats-Full (Quarter notes)");
public xTimings xBeatsHalf = new xTimings("Beats-Half (Eighth notes)");
public xTimings xBeatsThird = new xTimings("Beats-Third (Twelth notes)");
public xTimings xBeatsQuarter = new xTimings("Beats-Quarter (Sixteenth notes)");
public xTimings xFrames = new xTimings("Frames");



		//LORTrack4 vampTrack = sequence.FindTrack("Vamp-O-Rama", true);
		LORChannelGroup4 beatGroup = sequence.FindChannelGroup("Bars and Beats", Annotator.VampTrack.Members, true);
		if (xBars != null)
		{
			if (xBars.effects.Count > 0)
			{
				//if (ramps)
				//{
				LORChannel4 barCh = sequence.FindChannel("Bars", beatGroup.Members, true, true);
				//SequenceFunctions.ImportBeatChannel(barCh, xBars, 1, firstBeat, ramps);
				//SequenceFunctions.ImportBeatChannel(barCh, xBars, 1, firstBeat, true);
				SequenceFunctions.ImportBeatChannel(barCh, xBars, 1, firstBeat, ramps);
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
					LORChannel4 beatCh = sequence.FindChannel("Beats-Full", beatGroup.Members, true, true);
					SequenceFunctions.ImportBeatChannel(beatCh, xBeatsFull, BeatsPerBar, firstBeat, ramps);
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
					LORChannel4 beatCh = sequence.FindChannel("Beats-Half", beatGroup.Members, true, true);
					SequenceFunctions.ImportBeatChannel(beatCh, xBeatsHalf, BeatsPerBar * 2, firstBeat, ramps);
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
					LORChannel4 beatCh = sequence.FindChannel("Beats-Third", beatGroup.Members, true, true);
					SequenceFunctions.ImportBeatChannel(beatCh, xBeatsThird, BeatsPerBar * 3, firstBeat, ramps);
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
					LORChannel4 beatCh = sequence.FindChannel("Beats-Quarter", beatGroup.Members, true, true);
					SequenceFunctions.ImportBeatChannel(beatCh, xBeatsQuarter, BeatsPerBar * 4, firstBeat, ramps);
				}
			}
		}

		return errs;
	}
		*/

	}
}
