using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using LOR4Utils;
using FileHelper;
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

		public static xTimings xNoteOnsets = Annotator.xNoteOnsets;
		public static string ResultsName = "NoteOnsets.csv";
		public static string FileResults = Annotator.TempPath + ResultsName;

		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.BeatsQuarter; // Default
		public static vamps.LabelType LabelType = vamps.LabelType.NoteNamesUnicode; // Default


		public static readonly string[] availablePluginNames = {  "Queen Mary Note Onset Detector",
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

		public static readonly vamps.AlignmentType[] allowableAlignments = { vamps.AlignmentType.None,
																																					vamps.AlignmentType.FPS20,
																																					vamps.AlignmentType.FPS30,
																																					vamps.AlignmentType.FPS40,
																																					vamps.AlignmentType.FPS60,
																																					vamps.AlignmentType.BeatsQuarter };


		public static readonly string[] filesAvailableConfigs = {"vamp_qm-vamp-plugins_qm-onsetdetector_onsets.n3",
																											"vamp_qm-vamp-plugins_qm-transcription_transcription.n3",
																											"vamp_vamp-onsetsds_onsetsds_onsets.n3",
																											"vamp_silvet_silvet_onsets.n3",
																											"vamp_vamp-aubio_aubioonset_onsets.n3",
																											"vamp_vamp-aubio_aubionotes_notes.n3" };

		public static readonly vamps.LabelType[] allowableLabels = { vamps.LabelType.None, vamps.LabelType.Numbers,
																																	 vamps.LabelType.NoteNamesUnicode, vamps.LabelType.NoteNamesASCII,
																																	vamps.LabelType.MIDINoteNumbers };

		//public static readonly string[] availableLabels = {vamps.LABELNAMEnone, vamps.LABELNAMEnumbers,
		//																											vamps.LABELNAMEnoteNamesUnicode, vamps.LABELNAMEnoteNamesASCII,
		//																											vamps.LABELNAMEmidiNoteNumbers };

		public enum DetectionMethods { ComplexDomain = 0, SpectralDifference = 1, PhaseDeviation = 2, BroadbandEnergyRise = 3 };


		public static xTimings Timings
		{
			get
			{
				return xNoteOnsets;
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

		public static int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelType labelType, DetectionMethods detectMethod = DetectionMethods.ComplexDomain)
		{
			int err = 0;
			int pcount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int eStart = 0;
			int eEnd = 0;
			int lastStart = -1;
			string msg = "";

			// Store These
			LabelType = labelType;
			AlignmentType = alignmentType;
			Annotator.SetAlignment(alignmentType);

			msg = "\r\n\r\n### PROCESSING NOTE ONSETS ####################################";
			Debug.WriteLine(msg);

			pcount = 0;
			xNoteOnsets.effects.Clear();

			int onsetCount = 0;
			int noteNum = -1;
			string label = "1";
			int milliLen = 0;
			string noteInfo = "0";
			int countBeats = Annotator.FirstBeat;

			// Pass 2, read data into arrays
			StreamReader reader = new StreamReader(resultsFile);
			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				parts = lineIn.Split(',');
				millisecs = xUtils.ParseMilliseconds(parts[0]);
				eStart = Annotator.AlignTime(millisecs);
				// Has it advanced since the last one?
				if (eStart > lastStart)
				{
					// Depending on which plugin was used, the output file may have only 1, or maybe more, fields
					// In the event of just one column default endTime to startTime
					eEnd = eStart;
					// But if there is another column- for duration- lets use it
					if (parts.Length > 1)
					{
						milliLen = xUtils.ParseMilliseconds(parts[1]);
						int ee = eStart + milliLen;
						eEnd = Annotator.AlignTime(ee);
					}
					// If annotation file has no duration or end time, or if
					// After Aligning to Beats (or other long time) we may end up with a note of Zero length
					// if it was shorter than a Bar to begin with

					if (eEnd <= eStart)
					{
						// if not, lets ATTEMPT a fix
						if ((alignmentType == vamps.AlignmentType.BeatsQuarter) ||
								(alignmentType == vamps.AlignmentType.NoteOnsets))
						{
							// If aligning to quarter beats or note onsets, add 3/4 of an average quarter beat then align
							int ee = eStart + (int)Math.Round((Annotator.AverageBeatQuarterMS * .75D));
							eEnd = Annotator.AlignTime(ee);
						}
						if (Annotator.AlignFPS > 0)
						{
							// If aligning to frames, add one frame's worth
							eEnd = eStart + (int)Math.Round(1000D / Annotator.AlignFPS);
						}
						if (alignmentType == vamps.AlignmentType.None)
						{
							// If no alignment, add 50ms which is 20 FPS
							eEnd = eStart + 50;
						}
					}
					onsetCount++;
					label = onsetCount.ToString(); // Default
					noteNum = onsetCount; // Default
																// Again, depending on which plugin was used, there may or may not be note info
					if (parts.Length > 2)
					{
						noteInfo = parts[2];
						int.TryParse(noteInfo, out noteNum);

						int effNum = onsetCount;
						if (labelType == vamps.LabelType.MIDINoteNumbers) label = noteNum.ToString();
						if ((noteNum > 0) && (noteNum < 128))
						{
							if (labelType == vamps.LabelType.MIDINoteNumbers)
							{
								label = noteNum.ToString();
								effNum = noteNum;
							}
							if (labelType == vamps.LabelType.NoteNamesASCII)
							{
								label = MusicalNotation.noteNamesASCII[noteNum];
								effNum = noteNum;
							}
							if (labelType == vamps.LabelType.NoteNamesASCII)
							{
								label = MusicalNotation.noteNamesASCII[noteNum];
								effNum = noteNum;
							}
						}
					}
					else
					{
						// Do nothing?
						// (may need to mess with label, which is why this 'else' is here
					}
					xNoteOnsets.Add(label, eStart, eEnd, noteNum);
					// Remember this for next round (in order to skip ones which haven't advanced)
					lastStart = eStart;
				} // end enough parts
					//TODO Raise event for progress bar
			} // end while loop more lines remaining
			reader.Close();
			//Fyle.BUG("Check output window.  Did the alignments work as expected?");
			//TODO Output status message with count
			return err;
		} // end Beats

		public static int xTimingsToLORtimings()
		{
			// Ignore the timings passed in, and use the ones already cached for Bars and Beats
			// (Other transforms will use the one passed in)

			//SequenceFunctions.Sequence = sequence;
			int errs = 0;

			if (xNoteOnsets != null)
			{
				if (xNoteOnsets.effects.Count > 0)
				{
					string gridName = "5 " + transformName;
					LORTimings4 gridOnsets = Annotator.Sequence.FindTimingGrid(gridName, true);
					SequenceFunctions.ImportTimingGrid(gridOnsets, xNoteOnsets);
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
				//TODO Raise event for progess bar
			}

			return errs;
		}

		public static int xTimingsToLORChannel()
		{
			int errs = 0;

			//LORTrack4 vampTrack = SequenceFunctions.GetTrack("Vamp-O-Rama");
			//LOR4ChannelGroup onsetGroup = Annotator.Sequence.FindChannelGroup(transformName, Annotator.VampTrack.Members, true);
			if (xNoteOnsets != null)
			{
				if (xNoteOnsets.effects.Count > 0)
				{
					if (Annotator.UseRamps)
					{
						LOR4Channel chan = Annotator.VampTrack.Members.FindChannel(transformName, true, true);
						chan.color = lutils.Color_NettoLOR(System.Drawing.Color.DarkViolet);
						SequenceFunctions.ImportNoteChannel(chan, xNoteOnsets);
					}
				}
			}

			return errs;
		}

	}
}
