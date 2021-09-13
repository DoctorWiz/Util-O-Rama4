using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Configuration;
using System.Threading;
using Microsoft.Win32;
using xUtilities;
using LORUtils;
using Musik;
//using Ini;
using TagLib;
using TagLib.Mpeg;
using TagLib.Ogg;
using TagLib.Flac;
//using TagLib.identity3v1;
//using TagLib.identity3v2;
using TagLib.Aac;
using TagLib.Aiff;
using TagLib.Asf;
using TagLib.MusePack;
using TagLib.NonContainer;
using System.Diagnostics.Eventing.Reader;


namespace VampORama
{
	public partial class frmVamp : Form
	{
		private string annotatorProgram = "";


		//private static Annotator anno = null;
		private BarBeats transBarBeats = null;
		private NoteOnsets transOnsets = null;

		private string resultsBarBeats = "";
		private string resultsqmBeats = "";
		private string resultsBeatRoot = "";
		private string resultsPortoBeat = "";
		private string resultsAubioBeat = "";

		private string resultsNoteOnsets = "";
		private string resultsOnsetDS = "";
		private string resultsSilvetOnset = "";
		private string resultsAubioOnset = "";
		private string resultsAubioPoly = "";

		private string resultsTranscribe = "";
		private string resultsConstQ = "";
		private string resultsChroma = "";
		private string resultsSegments = "";
		private string resultsSpectro = "";

		private string resultsKey = "";
		private string resultsMelody = "";
		private string resultsVocals = "";
		private string resultsTempo = "";

		private Musik.AudioInfo audioData;
		string fileSongTemp = "song.mp3";
		int beatsPerBar = 4;
		int firstBeat = 1;
		int stepSize = 512;
		bool reuse = false;
		bool whiten = false;
		
		private object syncGate = new object();
		private Process cmdProc;
		private StringBuilder outLog = new StringBuilder();
		private StreamWriter writer = null;
		private bool outputChanged;

		private int PrepareToAnnotate()
		{
			int errs = 0;

			beatsPerBar = 4; // reset to default
			if (swTrackBeat.Checked) beatsPerBar = 3;
			firstBeat = 1; // reset to default
			Int32.TryParse(txtStartBeat.Text, out firstBeat);
			if (firstBeat < 1) firstBeat = 1;
			if (firstBeat > beatsPerBar) firstBeat = beatsPerBar;

			//fileSongTemp = pathWork + "song.mp3";
			//errs = utils.SafeCopy(fileAudioLast, fileSongTemp);
			//audioData.Filename = fileSongTemp;
			//audioData = ReadAudioFile(fileSongTemp);
			fileSongTemp = PrepAudioFile(fileAudioLast);


			reuse = chkReuse.Checked;
			Int32.TryParse(cboStepSize.Text, out stepSize);
			if ((stepSize < 200) || (stepSize > 800)) stepSize = 512;
			//anno = new Annotator(annotatorProgram);
			//anno.songTimeMS

			return errs;
		}
		
		private int AnnotateSelectedVamps()
		{
			int completed = 0;
			int err = 0;
			
			// BARS AND BEATS
			if (chkBarsBeats.Checked)
			{
				transBarBeats = new BarBeats();
				int pluginIndex = cboMethodBarsBeats.SelectedIndex;
				int detectionMethod = cboDetectBarBeats.SelectedIndex;
				whiten = chkWhiteBarBeats.Checked;

				err = transBarBeats.PrepareToVamp (fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				if (err == 0)
				{
					string fileConfig = transBarBeats.filesAvailableConfigs[pluginIndex];
					string vampParams = transBarBeats.availablePluginCodes[pluginIndex];
					resultsBarBeats = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsBarBeats.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignBarsBeats.Text);
						transBarBeats.ResultsToxTimings(resultsBarBeats, algn, vamps.LabelTypes.Numbers);
						completed++;
					}
				}
			}

			// NOTE ONSETS
			if (chkNoteOnsets.Checked)
			{
				transOnsets = new NoteOnsets();
				int pluginIndex = cboOnsetsPlugin.SelectedIndex;
				int detectionMethod = cboOnsetsDetect.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				if (err == 0)
				{
					string fileConfig = transOnsets.filesAvailableConfigs[pluginIndex];
					string vampParams = transOnsets.availablePluginCodes[pluginIndex];
					resultsNoteOnsets = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsNoteOnsets.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignOnsets.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transOnsets.ResultsToxTimings(resultsNoteOnsets, algn, vamps.LabelTypes.NoteNames, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}

			dirtyTimes = true;

			return completed;
		}

		private void ExportSelectedxTimings(string fileName)
		{
			// Get Temp Directory
			int writeCount = 0;
			bool allInOne = optMultiPer.Checked;
			string lineOut = "<timings>";
			string fileBaseName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName);
			string exten = ".xtiming";

			// Save Filename for next time (really only need the path, but...)
			fileTimingsLast = fileName;
			txtSaveNamexL.Text = ShortenPath(fileName, 100);
			heartOfTheSun.fileTimingsLast = fileTimingsLast;

			if (optMultiPer.Checked) heartOfTheSun.saveFormat = 2;
			else heartOfTheSun.saveFormat = 1;
			mruTimings.AddNew(fileName);
			mruTimings.SaveToConfig();
			// Get path and name for export files

			if (allInOne)
			{
				writer = BeginTimingsXFile(fileName);
				writer.WriteLine(lineOut);
				writeCount++;
			}

			// BARS AND BEATS
			//if (chkBars.Checked)
			//if (doBarsBeats)
			if (chkBarsBeats.Checked)
			{
				if (transBarBeats != null)
				{
					if (transBarBeats.xBars != null)
					{
						if (transBarBeats.xBars.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transBarBeats.xBars.timingName + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(transBarBeats.xBars, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsFull.Checked)
				{
					if (transBarBeats.xBeatsFull != null)
					{
						if (transBarBeats.xBeatsFull.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transBarBeats.xBeatsFull.timingName + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(transBarBeats.xBeatsFull, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsHalf.Checked)
				{
					if (transBarBeats.xBeatsHalf != null)
					{
						if (transBarBeats.xBeatsHalf.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transBarBeats.xBeatsHalf.timingName + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(transBarBeats.xBeatsHalf, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsThird.Checked)
				{
					if (transBarBeats.xBeatsThird != null)
					{
						if (transBarBeats.xBeatsThird.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transBarBeats.xBeatsThird.timingName + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(transBarBeats.xBeatsThird, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsQuarter.Checked)
				{
					if (transBarBeats.xBeatsQuarter != null)
					{
						if (transBarBeats.xBeatsQuarter.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transBarBeats.xBeatsQuarter.timingName + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(transBarBeats.xBeatsQuarter, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
			}

			// NOTE ONSETS
			if (transOnsets != null)
			{
				if (chkNoteOnsets.Checked)
				{
					if (transOnsets.xOnsets != null)
					{
						if (transOnsets.xOnsets.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + transOnsets.xOnsets.timingName + exten);
								writeCount++;
							}
							xTimings.LabelTypes lt = xTimings.LabelType(cboOnsetsLabels.Text);
							lineOut = xTimingsOutX(transOnsets.xOnsets, lt, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
			}

			/*
			if (chkTranscribe.Checked)
			{
				if (transOnsets.xTranscription != null)
				{
					if (xTimes.xTranscription.effects.Count > 0)
					{
						WriteTimingFileX(xTimes.xTranscription, fileName);
						WriteTimingFile4(xTimes.xTranscription, fileName);
						WriteTimingFile5(xTimes.xTranscription, fileName);
						writeCount+=3;
					}
				}
			}
			if (chkPitchKey.Checked)
			{
				if (xTimes.xKey != null)
				{
					if (xTimes.xKey.effects.Count > 0)
					{
						WriteTimingFileX(xTimes.xKey, fileName);
						WriteTimingFile4(xTimes.xKey, fileName);
						WriteTimingFile5(xTimes.xKey, fileName);
						writeCount += 3;
					}
				}
			}
			if (chkSegments.Checked)
			{
				if (xTimes.xSegments != null)
				{
					if (xTimes.xSegments.effects.Count > 0)
					{
						WriteTimingFileX(xTimes.xSegments, fileName);
						WriteTimingFile4(xTimes.xSegments, fileName);
						WriteTimingFile5(xTimes.xSegments, fileName);
						writeCount += 3;
					}
				}
			}


			*/

			//}

			if (allInOne)
			{
				lineOut = "</timings>";
				writer.WriteLine(lineOut);
			}
			writer.Close();



			pnlStatus.Text = writeCount.ToString() + " files writen.";


		}

		private int ExportSelectedVamps()
		{
			int completed = 0;

			// BARS AND BEATS
			if (chkBarsBeats.Checked)
			{
				if (lightORamaInstalled && chkLOR.Checked)
				{
					bool ramps = swRamps.Checked;

					transBarBeats.xTimingsToLORtimings(transBarBeats.xFrames, seq);
					//transBarBeats.xTimingsToLORtimings(transBarBeats.xBars, seq);
					//transBarBeats.xTimingsToLORtimings(transBarBeats.xBeatsFull, seq);
					//transBarBeats.xTimingsToLORtimings(transBarBeats.xBeatsHalf, seq);
					//transBarBeats.xTimingsToLORtimings(transBarBeats.xBeatsThird, seq);
					//transBarBeats.xTimingsToLORtimings(transBarBeats.xBeatsQuarter, seq);

					transBarBeats.xTimingsToLORChannels(transBarBeats.xBars, seq, firstBeat, ramps);
					//transBarBeats.xTimingsToLORChannels(transBarBeats.xBeatsFull, seq, firstBeat, ramps);
					//transBarBeats.xTimingsToLORChannels(transBarBeats.xBeatsHalf, seq, firstBeat, ramps);
					//transBarBeats.xTimingsToLORChannels(transBarBeats.xBeatsThird, seq, firstBeat, ramps);
					//transBarBeats.xTimingsToLORChannels(transBarBeats.xBeatsQuarter, seq, firstBeat, ramps);

					if (resultsBarBeats.Length > 4) completed++;
				}
			}

			// NOTE ONSETS
			if (chkNoteOnsets.Checked)
			{
				//int pluginIndex = cboMethodOnsets.SelectedIndex;
				//int detectionMethod = cboDetectOnsets.SelectedIndex;
				//whiten = chkWhiteOnsets.Checked;

				//resultsBarBeats = transBarBeats.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//if (resultsBarBeats.Length > 4) completed++;
			}



			return completed;
		}


		private int GetDuration(string songLength)
		{
			int ret = 0;
			string rest = "";
			string hrs = "00";
			string min = "00";
			string sec = "00";
			string subsec = "0000000";
			int cp = songLength.IndexOf(':');
			if (cp > 0)
			{
				hrs = songLength.Substring(0, cp);
				rest = songLength.Substring(cp + 1);
				cp = rest.IndexOf(':');
				if (cp > 0)
				{
					min = rest.Substring(0, cp);
					rest = rest.Substring(cp + 1);
				}
				else
				{
					min = hrs;
					hrs = "00";
				}
				cp = rest.IndexOf('.');
				if (cp > 0)
				{
					sec = rest.Substring(0, cp);
					rest = rest.Substring(cp + 1);
					subsec = rest;
				}
				else
				{
					sec = rest;
				}
			}

			int hr = 0;
			int.TryParse(hrs, out hr);
			int mn = 0;
			int.TryParse(min, out mn);
			int sc = 0;
			int.TryParse(sec, out sc);
			int ss = 0;
			int.TryParse(subsec, out ss);
			int ms = 0;
			if (ss > 0)
			{
				double div = Math.Pow(10, subsec.Length - 3);
				double dss = Math.Round(ss / div);
				ms = (int)dss;
			}

			ret = ms;
			ret += sc * 1000;
			ret += mn * 60000;
			ret += hr * 360000;

			return ret;
		}

		private int GetDuration(System.TimeSpan songLength)
		{
			double rms = Math.Round(songLength.TotalMilliseconds);
			int ims = (int)rms;
			return ims;
		}

		private string PrepAudioFile(string originalAudioFile)
		{
			// Input: A fully qualified path and filename to a audio file
			// Example: D:\xLights\MyShow\Audio\Wizards in Winter by TSO.mp3
			// Output: The Name of the COPY of the audio file that is to be annotated, in annotator's required format
			// Example: c:/users/johndoe/appdata/utilorama/Vamperizer/wizardsinwinter.mp3
			int err = 0;


			originalPath = Path.GetDirectoryName(originalAudioFile);
			originalFileName = Path.GetFileNameWithoutExtension(originalAudioFile);
			originalExt = Path.GetExtension(originalAudioFile);

			//string preppedFileName = PrepName(originalFileName);
			//newFile = pathWork + "song" + originalExt;
			newFile = pathWork + originalFileName + originalExt;
			//fileAudioWork = newFile;
			//fileResults = tempPath + preppedFileName;

			if (overwriteExistingVamps || !System.IO.File.Exists(newFile))
			{
				err = utils.SafeCopy(originalAudioFile, newFile, true);
			}

			audioData = ReadAudioFile(newFile);
			TimeSpan audioTime = audioData.Duration;
			int ms = audioTime.Minutes * 60000;
			ms += audioTime.Seconds * 1000;
			ms += audioTime.Milliseconds;
			//milliseconds = ms;
			Annotator.songTimeMS = ms;
			
			// Fill in blank or missing tag info
			if (audioData.Artist == null)
			{
				audioData.Artist = "[Unknown Artist]";
			}
			if (audioData.Artist.Length < 2)
			{
				audioData.Artist = "[Unknown Artist]";
			}
			if (audioData.Title == null)
			{
				audioData.Title = "[Unknown Title]";
			}
			if (audioData.Title.Length < 2)
			{
				audioData.Title = "[Unknown Title]";
			}
			if (audioData.Album == null)
			{
				audioData.Artist = "[Unknown Album]";
			}
			if (audioData.Album.Length < 2)
			{
				audioData.Artist = "[Unknown Album]";
			}






			//string preppedPath = PrepPath(tempPath);
			//string preppedExt = originalExt.ToLower();

			//string prepFile = preppedPath + preppedFileName + preppedExt;
			fileAudioLast = originalAudioFile;
			fileAudioWork = newFile;
			return newFile;

		}

		public StreamWriter BeginTimingsXFile(string fileName)
		{
			if (writer != null)
			{
				writer.Close();
			}
			writer = new StreamWriter(fileName);
			writer.WriteLine(xTimings.XMLinfo);
			return writer;
		}


		public string xTimingsOutX(xTimings timings, xTimings.LabelTypes labelType, bool indent = false)
		{
			string label = "";
			string level0 = "";
			string level1 = "  ";
			string level2 = "    ";
			if (indent)
			{
				level0 = "  ";
				level1 = "    ";
				level2 = "      ";
			}
			xEffect effect = null;

			StringBuilder ret = new StringBuilder();
			//  <timing
			ret.Append(level0);
			ret.Append(xTimings.RECORD_start);
			ret.Append(xTimings.TABLE_timing);
			ret.Append(xTimings.SPC);
			//  name="the Name"
			ret.Append(xTimings.FIELD_name);
			ret.Append(xTimings.VALUE_start);
			ret.Append(timings.timingName);
			ret.Append(xTimings.VALUE_end);
			ret.Append(xTimings.SPC);
			//  SourceVersion="2019.21">
			ret.Append(xTimings.FIELD_source);
			ret.Append(xTimings.VALUE_start);
			ret.Append(timings.sourceVersion);
			ret.Append(xTimings.VALUE_end);
			ret.Append(xTimings.RECORD_end);
			ret.Append(xTimings.CRLF);
			//    <EffectLayer>
			ret.Append(level1);
			ret.Append(xTimings.RECORD_start);
			ret.Append(xTimings.TABLE_layers);
			ret.Append(xTimings.RECORD_end);
			ret.Append(xTimings.CRLF);

			for (int i = 0; i < timings.effects.Count; i++)
			{
				effect = timings.effects[i];
				ret.Append(level2);
				ret.Append(xTimings.RECORD_start);
				ret.Append(xEffect.TABLE_Effect);
				ret.Append(xTimings.SPC);
				//  label="foo" 
				ret.Append(xEffect.FIELD_label);
				ret.Append(xTimings.VALUE_start);
				switch(labelType)
				{
					case xTimings.LabelTypes.None:
						// Append Nothing!
						break;
					case xTimings.LabelTypes.Numbers:
						ret.Append(effect.xlabel);
						break;
					case xTimings.LabelTypes.NoteNames:
						label = "";
						if (effect.Midi >= 0 && effect.Midi <= 127)
						{
							//label = SequenceFunctions.noteNames[timings.effects[i].Midi];
							label = xUtils.noteNames[effect.Midi];
						}
						ret.Append(label);
						break;
					case xTimings.LabelTypes.MidiNumbers:
						ret.Append(effect.Midi.ToString());
						break;
					case xTimings.LabelTypes.KeyNumbers:
						ret.Append(effect.Midi.ToString());
						break;
					case xTimings.LabelTypes.KeyNames:
						label = "";
						if (effect.Midi >= 0 && effect.Midi <= 24)
						{
							label = SequenceFunctions.keyNames[effect.Midi];
						}
						ret.Append(label);
						break;
					case xTimings.LabelTypes.Letters:
						ret.Append(effect.xlabel);
						break;
					case xTimings.LabelTypes.Frequency:
						label = "";
						if (effect.Midi >= 0 && effect.Midi <= 127)
						{
							label = SequenceFunctions.noteFreqs[effect.Midi];
						}
						ret.Append(label);
						break;
				}
				ret.Append(xTimings.VALUE_end);
				ret.Append(xTimings.SPC);
				//  starttime="50" 
				ret.Append(xEffect.FIELD_start);
				ret.Append(xTimings.VALUE_start);
				ret.Append(timings.effects[i].starttime.ToString());
				ret.Append(xTimings.VALUE_end);
				ret.Append(xTimings.SPC);
				//  endtime="350" />
				ret.Append(xEffect.FIELD_end);
				ret.Append(xTimings.VALUE_start);
				ret.Append(timings.effects[i].endtime.ToString());
				ret.Append(xTimings.VALUE_end);
				ret.Append(xTimings.SPC);

				ret.Append(xEffect.RECORD_end);
				ret.Append(xTimings.CRLF);
			}

			//     </EffectLayer>
			ret.Append(level1);
			ret.Append(xTimings.RECORDS_done);
			ret.Append(xTimings.TABLE_layers);
			ret.Append(xTimings.RECORD_end);
			ret.Append(xTimings.CRLF);
			//  </timing>
			ret.Append(level0);
			ret.Append(xTimings.RECORDS_done);
			ret.Append(xTimings.TABLE_timing);
			ret.Append(xTimings.RECORD_end);

			return ret.ToString();
		}


		public string VampThatSong(string fileSong, string vampParams, string fileConfig, bool reuse = false)
		{
			string resultsFile = "";
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
			string ex = Path.GetExtension(fileSong);
			//! string annotatorArguments = "-t " + vampParams;
			string annotatorArguments = "-t " + fileConfig;
			string WRITEformat = " -f -w csv --csv-force ";

			annotatorArguments += " \"" + fileSong + "\""; // + ex;
			annotatorArguments += WRITEformat;
			//annotatorArguments += " 2>output.txt";
			//string outputFile = tempPath + "output.log";
			string fileOutput = vampParams.Replace(':', '_') + ".n3";
			//string fileOutput = fileConfig;

			try
			{
				string emsg = annotatorProgram + " " + annotatorArguments;
				Console.WriteLine(emsg);
				//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				DialogResult dr = DialogResult.Yes;

				if (dr == DialogResult.Yes)
				{
					if (utils.IsWizard) Clipboard.SetText(emsg);
				}
				if (dr != DialogResult.Cancel)
				{
					resultsFile = pathWork;
					//resultsFile += "song_";
					resultsFile += Path.GetFileNameWithoutExtension(fileSong);
					//resultsFile += "_" + Path.GetFileNameWithoutExtension(fileConfig);
					resultsFile += "_" + Path.GetFileNameWithoutExtension(fileOutput);
					resultsFile += ".csv";

					//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
					//if ((!reuse) || (!System.IO.File.Exists(resultsFile)))
					if (true) 
					{
						lock (syncGate)
						{
							if (cmdProc != null) return "";
						}

						string runthis = annotatorProgram + " " + annotatorArguments;
						runthis = "/c " + runthis; // + " 2>output.txt";

						string vampCommandLast = runthis;
						if (utils.IsWizard) Clipboard.SetText(runthis);

						cmdProc = new Process();
						ProcessStartInfo procInfo = new ProcessStartInfo();
						procInfo.FileName = "cmd.exe";
						procInfo.RedirectStandardOutput = true;
						procInfo.RedirectStandardError = true;
						procInfo.CreateNoWindow = true;
						//procInfo.WindowStyle = ProcessWindowStyle.Hidden;
						procInfo.UseShellExecute = false;
						procInfo.Arguments = runthis;
						procInfo.WorkingDirectory = pathWork;

						cmdProc.StartInfo = procInfo;
						cmdProc.EnableRaisingEvents = true;
						cmdProc.ErrorDataReceived += ProcessVampError;
						cmdProc.OutputDataReceived += ProcessVampError;
						cmdProc.Exited += VampProcessEnded;
						cmdProc.Start();
						cmdProc.BeginErrorReadLine();
						cmdProc.BeginOutputReadLine();

						//cmdProc.WaitForExit();
						while (cmdProc != null)
						{
							//while (!cmdProc.HasExited)
							//{
							if (cmdProc.HasExited)
							{
								cmdProc = null;
							}
							Application.DoEvents(); // This keeps your form responsive by processing events
																			//}
						}

					}

					if (System.IO.File.Exists(resultsFile))
					{
						// return resultsFile;
						// errCount = 99999;
					}
					else
					{
						// NO RESULTS FILE!	
						if (utils.IsWizard)
						{
							utils.MakeNoise(utils.Noises.SamCurseC);
							System.Diagnostics.Debugger.Break();
						}
					}
				}
			}
			catch (Exception e)
			{
				if (utils.IsWizard)
				{
					string msg = e.Message;
					utils.MakeNoise(utils.Noises.Crash);
					System.Diagnostics.Debugger.Break();
				}
				resultsFile = "";
			}

			return resultsFile;
		}



		private void ProcessVampError(object sender, DataReceivedEventArgs drea)
		{
			lock (syncGate)
			{
				if (sender != cmdProc) return;
				outLog.AppendLine(drea.Data);
				if (outputChanged) return;
				outputChanged = true;
				BeginInvoke(new Action(OnOutputChanged));
			}
		}

		private void OnOutputChanged()
		{
			lock (syncGate)
			{
				logWindow.LogText = outLog.ToString();
				outputChanged = false;
			}
		}

		private void VampProcessEnded(object sender, EventArgs e)
		{
			lock (syncGate)
			{
				if (sender != cmdProc) return;
				cmdProc.Dispose();
				cmdProc = null;
			}
		}

	}
}
