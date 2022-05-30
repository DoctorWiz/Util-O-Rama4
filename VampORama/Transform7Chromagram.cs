using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using LOR4;
using FileHelper;
using xLights22;

namespace UtilORama4
{
	//! CHROMAGRAM
	static class VampChromagram //: ITransform
	{
		public const string transformName = "Chromagram";
		public const int PLUGINQueenMary = 0;
		public const int PLUGINConstantQ = 1;
		public const int PLUGINNNLS = 2;
		public const int PLUGINSilvet = 3;
		public const int PLUGINTipic = 4;
		public const int PLUGINSmoothTipic = 5;

		public static int lastPluginUsed = PLUGINQueenMary;

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
		public static string ResultsName = "Chromagram.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;

		public static xTimings xChromagram = new xTimings(transformName);
		public static xTimings alignTimes = null;

		public static vamps.LabelType LabelType = vamps.LabelType.NoteNamesUnicode;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.BeatsQuarter;

		public static readonly string[] availablePluginNames = { "Queen Mary Chromagram",
																														"Constant-Q Chromagram",
																														"NNLS Chroma",
																														"Silvet Chroma",
																														"Tipic Chromagram",
																														"Tipic Smoothed Chromagram" };

		public static readonly string[] availablePluginCodes = {"vamp:qm-vamp-plugins:qm-chromagram:chromagram",
																														"vamp:cqvamp:cqchromavamp:chromagram",
																														"vamp:nnls-chroma:nnls-chroma:chroma",
																														"vamp:silvet:silvet:chroma",
																														"vamp:tipic:tipic:chroma",
																														"vamp:tipic:tipic:chroma-smoothed" };

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.NoteOnsets,
																																					vamps.AlignmentType.BeatsQuarter };

		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None,
																																vamps.LabelType.NoteNamesUnicode,
																																vamps.LabelType.NoteNamesASCII,
																																vamps.LabelType.MIDINoteNumbers,
																																vamps.LabelType.Frequency };

		public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-chromagram_chromagram.n3",
																														"vamp_cqvamp_cqchromavamp_chromagram.n3",
																														"vamp_nnls-chroma_nnls-chroma_chroma.n3",
																														"vamp_silvet_silvet_chroma",
																														"vamp_tipic_tipic_chroma",
																														"vamp_tipic_tipic_chroma-smoothed" };


		private static LOR4ChannelGroup chromaGroup = null;
		public static LOR4Channel[] chromaChannels = null;

		private static int idx = 0;
		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };

		public static xTimings Timings
		{
			get
			{
				return xChromagram;
			}
		}
		public static Annotator.TransformTypes TransformationType
		{
			get
			{
				return Annotator.TransformTypes.Chromagram;
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
					case PLUGINQueenMary:
						// Queen Mary Chromagram
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

					case PLUGINConstantQ:
						// Constant-Q Chromagram (think this is also from Queen Mary)
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINNNLS:
						// NNLS (What does that stand for? ) by Matthias Mauch
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINTipic:
						// Tipic Chromagram
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					case PLUGINSmoothTipic:
						// Smoothed Tipic Chromagram
						err = Fyle.SafeCopy(fileConfigFrom, fileConfigTo);
						break;

					default:
						Fyle.BUG("Unrecognized Plugin Index, using default!");
						//string msg = "Alicante Chromagram Transcription is not currently available.  Using Queen Mary Transcription instead.";
						//DialogResult dr = MessageBox.Show(msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

			string msg = "\r\n\r\n### PROCESSING Chromagram TRANSCRIPTION ####################################";
			Debug.WriteLine(msg);

			xChromagram.Markers.Clear();

			StreamReader reader = new StreamReader(resultsFile);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				string[] parts = lineIn.Split(',');
				if (parts.Length > 2) ;
				{
					millisecs = xAdmin.ParseMilliseconds(parts[0]);
					eStart = Annotator.AlignTime(millisecs);
					duration = xAdmin.ParseMilliseconds(parts[1]);
					int ee = eStart + duration;
					eEnd = Annotator.AlignTime(ee);
					// Note: Unlike other annotator transforms, we will NOT be checking to see
					// if this note starts AFTER the last one, or if the end is after it
					// since for Chromagram Transcription this is perfectly legitimate
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
						xChromagram.Add(noteLabel, eStart, eEnd, note);
						lastStart = eStart;
					}
				} // end line contains a period
					//TODO: Raise event for progress bar
			} // end while loop more lines remaining

			reader.Close();
			//TODO Output status message with count
			return noteCount;
		} // end Beats


		public static int xTimingsToLORtimings()
		{
			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xChromagram != null)
			{
				if (xChromagram.Markers.Count > 0)
				{
					// Note: Number '5' is the same as None Onsets because this returns, effectively, the same results
					string gridName = "5 " + transformName;
					LOR4Timings polyGrid = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(polyGrid, xChromagram);
				}
			}

			return errs;
		}

		public static int xTimingsToLORChannels()
		{
			return ResultsToLORChannels();
		}

		private static void CreateChromaChannels()
		{
			string dmsg = "";
			int chroCount = MusicalNotation.chromaNamesASCII.Length;
			string prefix = "Chroma ";
			//Channel chan;
			//int octave = 0;
			//int lastOctave = 0;
			chromaGroup = Annotator.VampTrack.Members.FindChannelGroup(transformName, true);
			Array.Resize(ref chromaChannels, chroCount);
			for (int n = 0; n < chroCount; n++)
			{
				LOR4Channel chan = chromaGroup.Members.FindChannel(prefix + MusicalNotation.chromaNamesASCII[n], true, true);
				chan.color = SequenceFunctions.ChannelColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				chromaChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + chromaGroup.Name + "' SI:" + chromaGroup.SavedIndex;
				Debug.WriteLine(dmsg);

			}
			//seq.Members.ReIndex();
		} // end CreateChromaChannels


		public static int ResultsToLORChannels()
		{
			int pcount = 0;

			string resultsFile = Annotator.TempPath + ResultsName;
			if (Fyle.Exists(resultsFile))
			{

				string lineIn = "";
				int ppos = 0;
				int centisecs = 0;
				string[] parts;
				int ontime = 0;
				//int note = 0;
				LOR4Channel ch;
				LOR4Effect ef;

				Annotator.VampTrack.Selected = true;

				LOR4Timings tg = Annotator.GridOnsets;
				Annotator.VampTrack.timingGrid = tg;
				tg.Selected = true;
				CreateChromaChannels(); // grp, "Chroma ", doGroups);
				chromaGroup.Selected = true;
				if (tg == null)
				{
					Annotator.VampTrack.timingGrid = tg;
					tg.Selected = true;
				}
				else
				{
					Annotator.VampTrack.timingGrid = tg;
					for (int tgs = 0; tgs < Annotator.Sequence.TimingGrids.Count; tgs++)
					{
						if (Annotator.Sequence.TimingGrids[tgs].SaveID == tg.SaveID)
						{
							Annotator.VampTrack.timingGrid = Annotator.Sequence.TimingGrids[tgs];
							Annotator.Sequence.TimingGrids[tgs].Selected = true;
							tgs = Annotator.Sequence.TimingGrids.Count; // break loop
						}
					}
				}

				// Pass 1, Get max values
				double[] dVals = new double[12];
				StreamReader reader = new StreamReader(resultsFile);

				while ((lineIn = reader.ReadLine()) != null)
				{
					//TODO Raise event here to update progress bar
					parts = lineIn.Split(',');
					if (parts.Length == 13)
					{
						pcount++;
						//centisecs = ParseCentiseconds(parts[0]);
						//Debug.Write(centisecs);
						//Debug.Write(":");
						for (int nt = 0; nt < 12; nt++)
						{
							double d = Double.Parse(parts[nt + 1]);
							if (d > dVals[nt]) dVals[nt] = d;
						}
					}
				} // end while loop more lines remaining
				reader.Close();

				// Pass 2, Convert those maxvals to a scale factor
				for (int n = 0; n < 12; n++)
				{
					dVals[n] = 160 / dVals[n];
				}

				// Pass 3, convert to percents
				int[] lastcs = new int[12];
				double lastdVal = 0;
				int[] lastiVal = new int[12];

				reader = new StreamReader(resultsFile);

				while ((lineIn = reader.ReadLine()) != null)
				{
					//TODO Raise event here to update progress bar
					parts = lineIn.Split(',');
					if (parts.Length == 13)
					{
						pcount++;
						int ms = xAdmin.ParseMilliseconds(parts[0]);
						centisecs = (int)Math.Round(ms / 10D);
						//Debug.Write(centisecs);
						//Debug.Write(":");
						for (int note = 0; note < 12; note++)
						{
							double dt = 0;
							double d = Double.Parse(parts[note + 1]);
							d *= dVals[note];
							dt += d;
							int iVal = (int)dt;
							if (iVal < 31)
							{
								iVal = 0;
							}
							else
							{
								if (iVal > 130)
								{
									iVal = 100;
								}
								else
								{
									iVal -= 30;
								}
							}

							if (iVal != lastiVal[note])
							{
								//ch = seq.Channels[firstCobjIdx + note];
								ch = chromaChannels[note];
								//Identity id = seq.Members.BySavedIndex[noteChannels[note]];
								//if (id.PartType == MemberType.Channel)
								//{
								//ch = (Channel)id.owner;
								ef = new LOR4Effect();
								ef.EffectType = LOR4EffectType.Intensity;
								ef.startCentisecond = lastcs[note];
								ef.endCentisecond = centisecs;
								ef.startIntensity = lastiVal[note];
								ef.endIntensity = iVal;
								ch.effects.Add(ef);
								lastcs[note] = centisecs;
								lastiVal[note] = iVal;
								//}
								//else
								//{
								//	string emsg = "Crash! Burn! Explode!";
								//}
							}


						}
					}
				} // end while loop more lines remaining
				reader.Close();
				//TODO Raise event to update status & provide count
			}
			return pcount;
		}


	}
}
