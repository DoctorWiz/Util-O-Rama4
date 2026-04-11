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
	static class VampTempo //: ITransform
	{
		public const string transformName = "Tempo Changes";
		public const int PLUGINqueenMary = 0;
		public const int PLUGINbbc = 1;
		public const int PLUGINtempogram = 3;
		public const int PLUGINaubio = 2;

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
		public static string TempoNamePrefix = "";
		public static string TempoGroupNamePrefix = "";
		public static string ResultsName = "Tempo.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;

		//TODO Implement these in all the other Vamp Transform classes
		//TODO Implement these in frmVamp.FillCombos
		public const vamps.AlignmentType DefaultAlignment = vamps.AlignmentType.Bars;
		public const vamps.LabelType DefaultLabels = vamps.LabelType.BPM;

		public static xTimings xTempo = new xTimings(transformName);
		public static vamps.LabelType LabelType = vamps.LabelType.BPM;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.Bars; // Defaults

		public static readonly string[] availablePluginNames = {  "Queen Mary Tempo Tracker",
																															"BBC Tempo Tracker",
																															"Aubio Tempo Tracker",
																															"Cyclic Tempogram" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-tempotracker:tempo",
																														"vamp:bbc-vamp-plugins:bbc-rhythm:tempo",
																														"vamp:vamp-aubio:aubiotempo:tempo",
																														"vamp:tempogram:tempogram:cyclicTempogram"  };

		public static readonly string[] filesAvailableConfigs = { "vamp_qm-vamp-plugins_qm-tempotracker_tempo.n3",
																															"vamp_bbc-vamp-plugins_bbc-rhythm_tempo.n3",
																															"vamp_nnls-chroma_chordino_chordnotes.n3",
																															"vamp_tempogram_tempogram_cyclicTempogram.n3" };

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter,
																																					vamps.AlignmentType.Bars };



		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None,
																																	vamps.LabelType.BPM,
																																	vamps.LabelType.TempoName };

		//public static readonly string[] availableLabels = { vamps.LABELNAMEnone,
		//																										vamps.LABELNAMEbpm,
		//																										vamps.LABELNAMEtempoName };

		//public const int LABELNone = 0;
		//public const int LABELNoteNameASCII = 1;
		//public const int LABELNoteNameUnicode = 2;
		//public const int LABELMidiNoteNumber = 3;

		public static LORChannelGroup4 tempoGroup = null;
		public static LORChannel4 tempoChannel = null;
		public static LORRGBChannel4 tempoRGBChannel = null;

		private static int idx = 0;
		public static int MinChangePercent = 3;

		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static xTimings Timings
		{
			get
			{
				return xTempo;
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
						// Queen Mary Tempo Tracker
						reader = new StreamReader(fileConfigFrom);
						writer = new StreamWriter(fileConfigTo);
						while (!reader.EndOfStream)
						{
							lineCount++;
							lineIn = reader.ReadLine();
							if (lineCount == 14)
							{
								lineIn = lineIn.Replace("512", Annotator.StepSize.ToString());
							}
							if (lineCount == 15)
							{
								int ss2 = Annotator.StepSize * 2;
								lineIn = lineIn.Replace("1024", ss2.ToString());
							}
							writer.WriteLine(lineIn);
						}
						reader.Close();
						writer.Close();
						break;

					case PLUGINbbc:
						// BBC Tempo Tracker
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINaubio:
						// Aubio Tempo Tracker
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINtempogram:
						// Cyclic Tempogram
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
			int pcount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int keyID = 0;
			int eStart = 0;
			int eEnd = 0;
			double rawbpm = 80D;
			int bpm = 80;

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);

			string msg = "\r\n\r\n### PROCESSING TEMPO CHANGES ####################################";
			Debug.WriteLine(msg);

			pcount = 0; // reset for re-use
			xTempo.effects.Clear();

			StreamReader reader = new StreamReader(resultsFile);
			// First, lets get the first line of the file which has the start time of the first
			// tempo and it's BPM
			if (!reader.EndOfStream)
			{
				// Field 1 Part 0 is the start time
				// Field 2 Part 1 is the BPM as a number with 3 decimal places
				// Field 3 Part 2 is the BPM as a string
				lineIn = reader.ReadLine();
				parts = lineIn.Split(',');
				if (parts.Length > 1)
				{
					// Try parsing fields 0 start time (in decimal seconds)
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs);
					// Get BPM
					string strbpm = parts[1];
					Double.TryParse(strbpm, out rawbpm);
					bpm = (int)Math.Round(rawbpm);
					if ((bpm < 10) || (bpm > 250)) // Data integrity check
					{
						// In theory, it's possible to have a verrry high or low BPM, but really really really unlikely
						Fyle.BUG("Invalid BPM!");
					}
				}
			}

			// Now get the rest of the lines from the file, with the start times and BPM of each subsequent
			// change in tempo
			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length > 1)
				{
					// Try parsing fields 0 start time (in decimal seconds)
					millisecs = xUtils.ParseMilliseconds(parts[0]);
					eEnd = Annotator.AlignTime(millisecs);
					if (eEnd < eStart) // Data integrity check
					{
						Fyle.BUG("Backwards Tempo?!?!");
					}
					else
					{
						// After Aligning to Bars (or other long time) we may end up with a tempo change of Zero length
						// if it was shorter than a Bar to begin with
						if (eEnd > eStart)
						{
							// Default name is the BPM Number
							string eName = bpm.ToString();
							if (labelType == vamps.LabelType.TempoName)
							{ eName += " " + MusicalNotation.FindTempoName(bpm); }
							// Create new xEffect with these values, add to timings list
							xEffect xef = new xEffect(eName, eStart, eEnd, bpm);
							xTempo.effects.Add(xef);

							// End of this one is the start of the next one
							eStart = eEnd;
							// Get BPM for the NEXT one
							string strbpm = parts[1];
							Double.TryParse(strbpm, out rawbpm);
							bpm = (int)Math.Round(rawbpm);
							if ((bpm < 10) || (bpm > 250)) // Data integrity check
							{
								// In theory, it's possible to have a verrry high or low BPM, but really really really unlikely
								Fyle.BUG("Invalid BPM!");
							}
							pcount++;
						} // End tempo length
					} // End temp forward
				} // End enough parts
			} // end while loop more lines remaining
			reader.Close();

			// Finally, if not at the end-- add one last effect containing the last Pitch or Key
			if (eEnd < Annotator.songTimeMS)
			{
				eEnd = Annotator.songTimeMS;
				// Default name is the BPM
				string eName = bpm.ToString();
				if (labelType == vamps.LabelType.TempoName)
				{ eName += " " + MusicalNotation.FindTempoName(bpm); }
				xEffect xef = new xEffect(eName, eStart, eEnd, bpm);
				xTempo.effects.Add(xef);
			}
			//Fyle.BUG("Check output window.  Did the alignments work as expected?");

			return pcount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xTempo != null)
			{
				if (xTempo.effects.Count > 0)
				{
					string gridName = "7 " + transformName;
					LORTimings4 polyGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(polyGrid, xTempo);
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

			// Part 1
			// Get the Vamp Track and create the Tempo Channel
			tempoGroup = Annotator.VampTrack.Members.FindChannelGroup(transformName, true);
			tempoRGBChannel = tempoGroup.Members.FindRGBChannel(transformName, true, true);
			tempoChannel = FindTempoChannel(tempoGroup.Members, true, true);

			tempoRGBChannel.redChannel.effects.Clear();
			tempoRGBChannel.grnChannel.effects.Clear();
			tempoRGBChannel.bluChannel.effects.Clear();

			// Part 2
			// Find the slowest and fastest tempo
			for (int f = 0; f < xTempo.effects.Count; f++)
			{
				xEffect timing = xTempo.effects[f];
				if (timing.Number > 12)
				{
					if (timing.Number < tempoLow) tempoLow = timing.Number;
				}
				if (timing.Number < 250)
				{
					if (timing.Number > tempoHigh) tempoHigh = timing.Number;
				}
			}
			tempoSpread = tempoHigh - tempoLow;
			tempoRatio = (100D / tempoSpread);
			string tName = "Tempo " + tempoLow.ToString() + "-" + tempoHigh.ToString() + " BPM";
			tempoChannel.ChangeName(tName);

			// Part 3
			// Create an effect in the appropriate Key or Pitch Channel for each timing
			if (xTempo != null)
			{
				if (xTempo.effects.Count > 0)
				{
					for (int f = 0; f < xTempo.effects.Count; f++)
					{
						xEffect timing = xTempo.effects[f];
						int bpm = timing.Number;
						if ((bpm < 10) || (bpm > 250)) // Data integrity check
						{
							// In theory, its possible to have a note with a MIDI number of 0, but really really really unlikely
							Fyle.BUG("Invalid BPM");
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
									int intenPct = (int)Math.Round((bpm - tempoLow) * tempoRatio);
									int r = 0;
									int g = 0;
									int b = 0;
									PercentageColor(intenPct, ref r, ref g, ref b);
									if (r > 0)
									{
										eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, r);
										tempoRGBChannel.redChannel.effects.Add(eft);
									}
									if (g > 0)
									{
										eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, g);
										tempoRGBChannel.grnChannel.effects.Add(eft);
									}
									if (b > 0)
									{
										eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, b);
										tempoRGBChannel.bluChannel.effects.Add(eft);
									}
									eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, intenPct);
									tempoChannel.effects.Add(eft);
								}
								lastStart = csStart;
							}
						}
					}
				}
			}

			// Necessary??
			//Annotator.Sequence.CentiFix();

			return errs;
		}

		private static LORChannel4 FindTempoChannel(LORMembership4 members, bool createIfNotFound = true, bool clearEffects = false)
		{
			LORChannel4 tempoCh = null;
			if (members == null)
			{
				Fyle.BUG("Membership is null");
			}
			else
			{
				for (int m = 0; m < members.Count; m++)
				{
					if (members[m].MemberType == LORMemberType4.Channel)
					{
						string mName = members.Items[m].Name;
						if (mName.Length > 8)
						{
							if (mName.Substring(0, 6) == "Tempo ")
							{
								if (mName.Substring(mName.Length - 3, 3) == " BPM")
								{
									// found it!
									tempoCh = (LORChannel4)members[m];
									m = members.Count; // force exit of loop
								}
							}
						}
					}
				}
				if (createIfNotFound)
				{
					tempoCh = Annotator.Sequence.CreateNewChannel("TempoChannel"); // Temporary name
					tempoCh.color = 0xA00050; // Dark purple
					members.Add(tempoCh);
				}
				if (clearEffects)
				{
					tempoCh.effects.Clear();
				}

			}
			return tempoCh;
		}

		/*
		public static int OLD_xTimingsToLORChannel()
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

			// Part 1
			// Get the Vamp Track and create the Tempo Channel
			tempoChannel = Annotator.Sequence.FindChannel(TempoNamePrefix + "Tempo", Annotator.VampTrack.Members, true, true);
			tempoChannel.color = lutils.Color_NettoLOR(System.Drawing.Color.Black);
			tempoChannel.effects.Clear();

			// Part 2
			// Find the slowest and fastest tempo
			for (int f = 0; f < xTempo.effects.Count; f++)
			{
				xEffect timing = xTempo.effects[f];
				if (timing.Number > 12)
				{
					if (timing.Number < tempoLow) tempoLow = timing.Number;
				}
				if (timing.Number < 250)
				{ 
					if (timing.Number > tempoHigh) tempoHigh = timing.Number;
				}
			}
			tempoSpread = tempoHigh - tempoLow;
			tempoRatio = (75D / tempoSpread);


				// Part 3
				// Create an effect in the appropriate Key or Pitch Channel for each timing
			if (xTempo != null)
			{
				if (xTempo.effects.Count > 0)
				{
					for (int f = 0; f < xTempo.effects.Count; f++)
					{
						xEffect timing = xTempo.effects[f];
						int bpm = timing.Number;
						if ((bpm < 10) || (bpm > 250)) // Data integrity check
						{
							// In theory, its possible to have a note with a MIDI number of 0, but really really really unlikely
							Fyle.BUG("Invalid BPM");
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
									int intensity = (int)Math.Round((bpm - tempoLow) * tempoRatio) + 25;
									eft = new LOREffect4(LOREffectType4.Intensity, csStart, csEnd, intensity);
									tempoChannel.effects.Add(eft);
								}
								lastStart = csStart;
							}
						}
					}
				}
			}

			// Necessary??
			//Annotator.Sequence.CentiFix();

			return errs;
		}
		*/
		public static void PercentageColor(int percentage, ref int r, ref int g, ref int b)
		{
			// Lowest = Blue
			// Mid-Low = Cyan
			// Middle = Green
			// Mid-High = Yellow
			// Highest = Red
			// Sortofa cold-to-hot look
			int px = percentage * 5;
			if (percentage < 50)
			{
				r = 0;
				g = px;
				b = 250 - px;
			}
			else
			{
				r = px - 250;
				g = 500 - px;
				b = 0;
			}
			int foooooo = 0;
		}

		public static int OLD_PercentageColor(int percentage, ref int r, ref int g, ref int b)
		{
			// Returns a LOR color
			int ret = 0;
			int px = percentage * 7;
			if (percentage < 34)
			{
				r = px;
				b = 231 - px;
			}
			if ((percentage > 33) && (percentage < 67))
			{
				r = 231 - px;
				g = px;
			}
			if (percentage > 66)
			{
				g = 231 - px;
				b = px;
			}
			ret = (b << 16) + (g << 8) + r;
			return ret;
		}

		public static void Old2_PercentageColor(int percentage, ref int r, ref int g, ref int b)
		{
			int px = percentage * 7;
			if (percentage < 34)
			{
				r = px;
				b = 231 - px;
			}
			if ((percentage > 33) && (percentage < 67))
			{
				r = 231 - px;
				g = px;
			}
			if (percentage > 66)
			{
				g = 231 - px;
				b = px;
			}
		}

		public static int xTimingsToxLights(string baseFileName)
		{
			int errs = 0;


			return errs;
		}


	}
}
