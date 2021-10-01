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
	//////////////////
	//! T E M P O  //
	////////////////
	static class VampSegments //: ITransform
	{
		public const string transformName = "Segments";
		public const int PLUGINqueenMary = 0;
		public const int PLUGINbbc = 1;
		public const int PLUGINsegmentino = 2;

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
		public static string SegmentsNamePrefix = "";
		public static string SegmentsGroupNamePrefix = "";
		public static string ResultsName = "Segments.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;
		
		//TODO Implement these in all the other Vamp Transform classes
		//TODO Implement these in frmVamp.FillCombos
		public const vamps.AlignmentType DefaultAlignment = vamps.AlignmentType.Bars;
		public const vamps.LabelType DefaultLabels = vamps.LabelType.Numbers;
		
		public static xTimings xSegments = new xTimings(transformName);
		public static xTimings alignTimes = null;
		//public static labelType labelType = labelType.none;
		public static vamps.LabelType LabelType = vamps.LabelType.Numbers;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.Bars;

		public static readonly string[] availablePluginNames = {  "Queen Mary Segmenter",
																															"BBC Segmenter"                                                       ,
																															"Segmentino" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-segmenter:segmentation",
																														"vamp:bbc-vamp-plugins:bbc-speechmusic-segmenter:segmentation",
																														"vamp:segmentino:segmentino:segmentation" };

		public static readonly string[] filesAvailableConfigs = { "vamp_qm-vamp-plugins_qm-segmenter_segmentation.n3",
																															"vamp:bbc-vamp-plugins:bbc-speechmusic-segmenter:segmentation",
																															"vamp_segmentino_segmentino_segmentation" };
		
		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter,
																																					vamps.AlignmentType.Bars };



		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None,
																																	vamps.LabelType.Numbers,
																																	vamps.LabelType.Letters };

		//public static readonly string[] availableLabels = { vamps.LABELNAMEnone,
		//																										vamps.LABELNAMEnumbers };

		//public const int LABELNone = 0;
		//public const int LABELNoteNameASCII = 1;
		//public const int LABELNoteNameUnicode = 2;
		//public const int LABELMidiNoteNumber = 3;

		private static LORTrack4 vampTrack = Annotator.VampTrack;
		private static LORSequence4 sequence = Annotator.Sequence;


		public static LORChannelGroup4 segmentGroup = null;
		// Be careful not to confuse these---
		// SegmentChannel is a single channel containing ramp/fade effects for each segment
		// segmentChannels is a set of channels each containing on-off effects for a different segment
		public static LORChannel4 SegmentChannel = null;
		public static LORChannel4[] segmentChannels = null;

		private static int idx = 0;
		public static int segmentCount = 0;
		public static int MinChangePercent = 3;

		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static xTimings Timings
		{
			get
			{
				return xSegments;
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
						// Queen Mary Segments Tracker
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

					case PLUGINbbc:
						// BBC Segments Tracker
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINsegmentino:
						// Aubio Segments Tracker
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
					//System.Diagnostics.Debugger.Break();
					string msg = "PrepareToVamp Crashed!\r\n";
					msg += m;
					Fyle.BUG(msg);
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
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int eStart = 0;
			int eEnd = 0;
			//int align = SetAlignmentHost(cboAlignTranscribe.Text);
			string segLabel = "";
			int duration = 0;
			int segNo = 0;
			int lastStart = -1;

			// Column 1 part[0] = Start Time
			// Column 2 part[1] = Length
			// NOTE: There appears to be NO gaps (at least not in my samples)
			// Column 3 part[2] = Segment Number 1-x
			// Column 4 part[3] = Segment Name (Letter) A-x

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);

			string msg = "\r\n\r\n### PROCESSING SEGMENTS ####################################";
			Debug.WriteLine(msg);

			StreamReader reader = new StreamReader(resultsFile);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs); // AlignStartTo(millisecs, align);

					if (eStart > lastStart)
					{
						// Get label, if available
						if (parts.Length > 2)
						{
							duration = xUtils.ParseMilliseconds(parts[1]);
							int ee = eStart + duration;
							eEnd = Annotator.AlignTime(ee);
							// After Aligning to Bars (or other long time) we may end up with a key of Zero length
							// if it was shorter than a Bar to begin with
							if (eEnd > eStart)
							{
								int.TryParse(parts[2], out segNo);
								// Keep track of how many different segments,
								//  which has the highest number so far
								if (segNo > segmentCount) segmentCount = segNo;
								segLabel = parts[3];
								segLabel = segLabel.Replace("\"", "");
							}
							xSegments.Add(segLabel, eStart, eEnd, segNo);
							lastStart = eStart;
						}
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();
			//Fyle.BUG("Check output window.  Did the alignments work as expected?");

			return segmentCount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xSegments != null)
			{
				if (xSegments.effects.Count > 0)
				{
					string gridName = "8 " + transformName;
					LORTimings4 segmentGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(segmentGrid, xSegments);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			int errs = 0;
			int grpNum = -1;
			int tempoLow = 10000;
			int tempoHigh = 0;
			int tempoSpread = 50; // just bullshit starter value
														// Minimum tempo will show up as 25% intensity
														// Maximum tempo will show up as 100% intensity
														// tempoRatio is how many bpm per percent
			double tempoRatio = 2D; // bullshit starter value
			int lastStart = -1;

			Array.Resize(ref segmentChannels, segmentCount); // Segment are numbered 1-x (no zero)

			// Part 1
			// Get the Vamp Track and create the Segments Channels
			segmentGroup = Annotator.Sequence.FindChannelGroup(transformName,Annotator.VampTrack.Members, true);
			//? segmentGroup = sequence.FindChannelGroup(transformName, vampTrack.Members, true); //? Try shortcuts...
			// Be careful not to confuse these---
			// SegmentChannel is a single channel containing ramp/fade effects for each segment
			// segmentChannels is a set of channels each containing on-off effects for a different segment
			SegmentChannel = Annotator.Sequence.FindChannel("Segments", segmentGroup.Members, true, true);
			//? SegmentChannel = sequence.FindChannel("Segments", segmentGroup.Members, true, true); //? Try shortcut...
			for (int c = 0; c < segmentCount; c++)
			{
				//	string chName = "Segment " + (c + 1).ToString(); 
				string chName = "Segment " + (char)(c + 65); // I think I like this better
				segmentChannels[c] = Annotator.Sequence.FindChannel(chName, segmentGroup.Members, true, true);
				//? segmentChannels[c] = sequence.FindChannel(chName, segmentGroup.Members, true, true); //? Try shortcut...
				segmentChannels[c].color = SequenceFunctions.ChannelColor(c);
			}

			// Part 2
			// Create an effect in the appropriate segment channel
			if (xSegments != null)
			{
				if (xSegments.effects.Count > 0)
				{
					for (int f = 0; f < xSegments.effects.Count; f++)
					{
						xEffect timing = xSegments.effects[f];
						int csStart = (int)Math.Round(timing.starttime / 10D);
						int csEnd = (int)Math.Round(timing.endtime / 10D);
						if (csEnd <= csStart) // Data integrity check
						{
							Fyle.BUG("Backwards Segment?!?!");
						}
						else
						{
							if (csStart < lastStart) // Data integrity check
							{
								Fyle.BUG("Out of order segments!");
							}
							else
							{
								LOREffect4 eftR = new LOREffect4(LOREffectType4.FadeDown, csStart, csEnd, 100,0);
								LOREffect4 eftO = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, 100);
								int segNo = timing.Number;
								if ((segNo < 1) || (segNo > segmentCount))
								{
									Fyle.BUG("Invalid Segment Number!");
								}
								else
								{
									// Segments are numbered 1-x (no zero) so subtract 1
									segmentChannels[segNo - 1].effects.Add(eftO);
									SegmentChannel.effects.Add(eftR);
								}
							}
							lastStart = csStart;
						}
					}
				}
			}

			// Necessary??
			//Annotator.Sequence.CentiFix();

			return errs;
		}

	}
}
