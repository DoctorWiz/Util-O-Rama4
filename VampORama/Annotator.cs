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
using LORUtils4; using FileHelper;

namespace UtilORama4
{
	public static class Annotator
	{
		private	const string WRITEformat = " -f -w csv --csv-force ";

		//public ITransform transformer = new BarBeats();
		//public Transpose transposer = new Transpose();
		public static string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";

		//public static VampBarBeats BarBeatsTransformer = null; // Initialized in constructor

		public static xTimings xBars = new xTimings(VampBarBeats.barsName);
		public static xTimings xBeatsFull = new xTimings(VampBarBeats.beatsFullName);
		public static xTimings xBeatsHalf = new xTimings(VampBarBeats.beatsHalfName);
		public static xTimings xBeatsThird = new xTimings(VampBarBeats.beatsThirdName);
		public static xTimings xBeatsQuarter = new xTimings(VampBarBeats.beatsQuarterName);
		public static xTimings xFrames = new xTimings("Frames");
		public static xTimings xOnsets = new xTimings(VampNoteOnsets.transformName);
		public static xTimings xAlignTo = null;
		public static int songTimeMS = 0;
		public static int BeatsPerBar = 4;
		public static int FirstBeat = 1;
		public static bool UseRamps = false;
		public static bool ReuseResults = false;
		public static bool Whiten = false;
		public static int StepSize = 512;
		private static int totalMilliseconds = 0;
		private static int highestTime = 0;
		//public static int TotalCentiseconds = 0;
		private static int fps = 40;
		//private static int mspf = 25;

		public static int alignIdx = 0;
		//private static bool Fyle.DebugMode = Fyle.IsWizard || Fyle.IsAWizard;
		//public static LORTrack4 VampTrack = null;

		public static LORSequence4 Sequence = null;
		public const string VAMPTRACKname = "Vamp-O-Rama";
		public static LORTrack4 VampTrack = null;
		public static LORTimings4 GridBeats = null;
		public static LORTimings4 GridOnsets = null;
		private static LORChannel4[] noteChannels = null;
		public static string WorkPath = "C:\\Windows\\Temp\\";

		public enum TransformTypes { BarsAndBeats, NoteOnsets, PolyphonicTranscription, PitchAndKey, Tempo, Chromagram, Spectrogram, Segments};

		static Annotator()
		{
			int x = 5;
		}

		static void Clear()
		{
			xBars = new xTimings(VampBarBeats.barsName);
			xBeatsFull = new xTimings(VampBarBeats.beatsFullName);
			xBeatsHalf = new xTimings(VampBarBeats.beatsHalfName);
			xBeatsThird = new xTimings(VampBarBeats.beatsThirdName);
			xBeatsQuarter = new xTimings(VampBarBeats.beatsQuarterName);
			xFrames = new xTimings("Frames");
			xOnsets = new xTimings(VampNoteOnsets.transformName);
			xAlignTo = null;
			songTimeMS = 0;
			BeatsPerBar = 4;
			FirstBeat = 1;
			UseRamps = false;
			ReuseResults = false;
			Whiten = false;
			StepSize = 512;
			totalMilliseconds = 0;
			highestTime = 0;
			fps = 40;
			alignIdx = 0;
			LORSequence4 Sequence = null;
			VampTrack = null;
			noteChannels = null;
		}


		public static void Init(LORSequence4 sequence)
		{
			Clear();
			Sequence = sequence;
			VampTrack = Sequence.FindTrack(VAMPTRACKname,true);
		}

		public static string OLDAnnotateSong(string fileSong, string vampParams, string fileConfig, bool reuse = false)
		{
			string resultsFile = "";
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
			string ex = Path.GetExtension(fileSong);
			//! string annotatorArguments = "-t " + vampParams;
			string annotatorArguments = "-t " + fileConfig;
			annotatorArguments += " " + fileSong; // + ex;
			annotatorArguments += WRITEformat;
			//annotatorArguments += " 2>output.txt";
			//string outputFile = tempPath + "output.log";
			string fileOutput = vampParams.Replace(':', '_') + ".n3";

			try
			{
					string emsg = annotatorProgram + " " + annotatorArguments;
					Console.WriteLine(emsg);
					//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
					DialogResult dr = DialogResult.Yes;

					if (dr == DialogResult.Yes)
					{
						if (Fyle.DebugMode) Clipboard.SetText(emsg);
					}
					if (dr != DialogResult.Cancel)
					{
						resultsFile = pathWork;
						resultsFile += "song_";
						resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
						resultsFile += ".csv";

						//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
						if ((!reuse) || (!System.IO.File.Exists(resultsFile)))
						{
							string runthis = annotatorProgram + " " + annotatorArguments;
						runthis = "/c " + runthis; // + " 2>output.txt";

						string vampCommandLast = runthis;
							if (Fyle.DebugMode) Clipboard.SetText(runthis);

							Process cmdProc = new Process();
							ProcessStartInfo procInfo = new ProcessStartInfo();
						//x procInfo.FileName = annotatorProgram;
						procInfo.FileName = "cmd.exe";
						procInfo.RedirectStandardOutput = true;
						procInfo.RedirectStandardError = true;
						procInfo.CreateNoWindow = true;
						procInfo.WindowStyle = ProcessWindowStyle.Hidden;
						procInfo.UseShellExecute = false;
						//x procInfo.Arguments = annotatorArguments;
						procInfo.Arguments = runthis;
						procInfo.WorkingDirectory = pathWork;
							cmdProc.StartInfo = procInfo;
						cmdProc.EnableRaisingEvents = true;
						//cmdProc.ErrorDataReceived += ProcessVampError;
						//cmdProc.OutputDataReceived += ProcessVampError;
						//cmdProc.Exited += VampProcessEnded;
						cmdProc.Start();
						cmdProc.BeginErrorReadLine();
						cmdProc.BeginOutputReadLine();

						//cmdProc.WaitForExit();
						while (cmdProc != null)
						{
							//while (!cmdProc.HasExited)
							//{
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
						if (Fyle.DebugMode)
						{
							System.Diagnostics.Debugger.Break();
						}
						}
					}
			}
			catch (Exception e)
			{
				if (Fyle.DebugMode)
				{
					string msg = e.Message;
					System.Diagnostics.Debugger.Break();
				}
				resultsFile = "";
			}

			return resultsFile;
		}

		public static string TestAnnotateSong(string fileSong, string vampParams, string fileConfig, bool reuse = false)
		{
			// Does pretty much the same thing as the regular 'AnnotateSong' procedure
			// Except leaves the command prompt open so the user can review it for errors
			// Necessary because SonicAnnotator (apparently) does not use stdout
			string resultsFile = "";
			string pathWork = Path.GetDirectoryName(fileSong) + "\\";
			string ex = Path.GetExtension(fileSong);
			string annotatorArguments = "-t " + fileConfig;
			annotatorArguments += " " + fileSong; // + ex;
			annotatorArguments += WRITEformat;
			string fileOutput = vampParams.Replace(':', '_') + ".n3";

			try
			{
				string emsg = annotatorProgram + " " + annotatorArguments;
				Console.WriteLine(emsg);
				DialogResult dr = DialogResult.Yes;

				if (dr == DialogResult.Yes)
				{
					if (Fyle.DebugMode) Clipboard.SetText(emsg);
				}
				if (dr != DialogResult.Cancel)
				{
					resultsFile = pathWork;
					resultsFile += "song_";
					resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
					resultsFile += ".csv";

					//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
					if ((!reuse) || (!System.IO.File.Exists(resultsFile)))
					{
						string runthis = annotatorProgram + " " + annotatorArguments;
						//runthis += " 2>output.txt";
						string vampCommandLast = runthis;
						if (Fyle.DebugMode) Clipboard.SetText(runthis);

						Process cmdProc = new Process();
						ProcessStartInfo procInfo = new ProcessStartInfo();
						procInfo.FileName = "cmd.exe";
						procInfo.Arguments = "/k " + runthis;
						procInfo.WorkingDirectory = pathWork;
						cmdProc.StartInfo = procInfo;
						cmdProc.Start();
					}

					if (System.IO.File.Exists(resultsFile))
					{
						// return resultsFile;
						// errCount = 99999;
					}
					else
					{
						// NO RESULTS FILE!	
						if (Fyle.DebugMode)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
				}
			}
			catch (Exception e)
			{
				if (Fyle.DebugMode)
				{
					string msg = e.Message;
					System.Diagnostics.Debugger.Break();
				}
				resultsFile = "";
			}

			return resultsFile;
		}

		public static int AlignTimeTo(int startTime, vamps.AlignmentType alignTo)
		{
			int msPerFrame = 0;
			int ret = startTime;
			if (alignTo == vamps.AlignmentType.FPS10) msPerFrame = 100;
			if (alignTo == vamps.AlignmentType.FPS20) msPerFrame = 50;
			if (alignTo == vamps.AlignmentType.FPS30) msPerFrame = 33;
			if (alignTo == vamps.AlignmentType.FPS40) msPerFrame = 25;
			if (alignTo == vamps.AlignmentType.FPS60) msPerFrame = 17;
			//if (alignTo == vamps.AlignmentType.FPScustom)
			if (msPerFrame > 1 && msPerFrame < 1000)
			{ 
				int half = msPerFrame / 2;
				int newStart = startTime / msPerFrame * msPerFrame;
				int diff = startTime - newStart;
				if (diff > half) newStart += msPerFrame;
				ret = newStart;
			}
			else
			{
				if ((alignTo == vamps.AlignmentType.Bars) ||
						(alignTo == vamps.AlignmentType.BeatsFull) ||
						(alignTo == vamps.AlignmentType.BeatsHalf) ||
						(alignTo == vamps.AlignmentType.BeatsThird) ||
						(alignTo == vamps.AlignmentType.BeatsQuarter) ||
						(alignTo == vamps.AlignmentType.NoteOnsets))
				{
				if (xAlignTo != null)
					{
						if (xAlignTo.effects.Count > 0)
						{
							int ns = AlignToNearestTiming(startTime);
							ret = ns;
						}
					}
				}
			}
			return ret;
		}

		private static int AlignToNearestTiming(int startTime)
		{
			//! Important: Reset lastFoundIdx to xUtils.UNDEFINED before starting new timings and/or different alignTimes.
			//! alignTimes are assumed to be in ascending numerical order.

			int retIdx = -1;
			int diffLo = 0;
			int diffHi = 0;
			bool keepTrying = true;
			int matchTime = -1;
			int newStart = startTime;

			while (keepTrying)
			{
				if (alignIdx >= 0)
				{
					//! Crashing Here!  xAlignTo is null-not assigned
					diffLo = startTime - xAlignTo.effects[alignIdx].starttime;
					if (diffLo < 0)
					{
						alignIdx--;
					}
					else
					{
						if (alignIdx < xAlignTo.effects.Count - 1)
						{
							diffHi = xAlignTo.effects[alignIdx + 1].starttime - startTime;
							if (diffHi < 0)
							{
								alignIdx++;
							}
							else
							{
								if (diffLo < diffHi)
								{
									retIdx = alignIdx;
									matchTime = xAlignTo.effects[alignIdx].starttime;
									keepTrying = false;
								}
								else
								{
									alignIdx++;
									retIdx = alignIdx;
									matchTime = xAlignTo.effects[alignIdx].starttime;
									keepTrying = false;
								}
							}
						}
						else
						{
							diffLo = startTime - xAlignTo.effects[xAlignTo.effects.Count - 1].starttime;
							if (diffLo < 0)
							{
								alignIdx--;
							}
							else
							{
								diffHi = songTimeMS - startTime;
								if (diffLo <= diffHi)
								{
									retIdx = alignIdx;
									matchTime = xAlignTo.effects[alignIdx].starttime;
									keepTrying = false;
								}
								else
								{
									alignIdx++;
									retIdx = alignIdx;
									matchTime = xAlignTo.effects[alignIdx].starttime;
									keepTrying = false;
								}
							}
						}
					}
				}
				else
				{
					diffHi = xAlignTo.effects[0].starttime - startTime;
					if (diffHi < 0)
					{
						alignIdx++;
					}
					else
					{
						diffLo = startTime;
						if (diffLo < diffHi)
						{
							retIdx = alignIdx;
							matchTime = 0;
							keepTrying = false;
						}
						else
						{
							alignIdx++;
							retIdx = alignIdx;
							matchTime = xAlignTo.effects[alignIdx].starttime;
							keepTrying = false;
						}
					}
				}
			}
			if (Fyle.DebugMode)
			{
				string msg = "Start time " + startTime.ToString() + " aligned to " + matchTime.ToString();
				Console.WriteLine(msg);
			}

			//if (retIdx >= 0)
			//{
			//	newStart = xAlignTo.effects[retIdx].starttime;
			//}

			//return newStart;
			return matchTime;
		}

		public static int FPS
		{
			// Frames-Per-Second
			set
			{
				fps = value;
				if (fps < 10) fps = 10;
				if (fps > 100) fps = 100;
				//mspf = 1000 / fps;
			}
			get
			{
				return fps;
			}
		}

		public static int msPF
		{
			// Milliseconds-Per-Frame
			set
			{
				int mspf = value;
				if (mspf < 10) mspf = 10;
				if (mspf > 100) mspf = 100;
				fps = 1000 / mspf;
			}
			get
			{
				return (int)Math.Round(1000D / fps);
			}
		}

		public static int csPF
		{
			// Milliseconds-Per-Frame
			set
			{
				int mspf = value;
				if (mspf < 10) mspf = 10;
				if (mspf > 100) mspf = 100;
				fps = 100 / mspf;
			}
			get
			{
				return (int)Math.Round(100D / fps);
			}
		}



		public static int TotalMilliseconds
		{
			get { return TotalCentiseconds * 10; }
			set { TotalCentiseconds = (int)Math.Round(value / 10D); }
		}

		public static int TotalCentiseconds
		{
			get
			{
				if (Sequence == null)
				{
					return (int)Math.Round(totalMilliseconds / 10D);
				}
				else
				{
					return Sequence.Centiseconds;
				}
			}
			set
			{
				totalMilliseconds = value * 10;
				if (Sequence != null)
				{
					if ((value > lutils.MINCentiseconds) && (value < lutils.MAXCentiseconds))
					{
						Sequence.CentiFix(value);
					}
				}
			}
		}

		public static int HighTime
		{
			get { return highestTime; }
			set
			{
				highestTime = Math.Max(value, highestTime);
				totalMilliseconds = Math.Max(highestTime, totalMilliseconds);
				if (Sequence != null)
				{
					int cs = (int)Math.Round(totalMilliseconds / 10D);
					Sequence.Centiseconds = Math.Max(Sequence.Centiseconds, cs);
				}
			}
		}

		/*

		public int AnalyzeSong(string audioFileName)
		{
			//ImBusy(true);

			//this.Cursor = Cursors.WaitCursor;
			grpAnalyze.Enabled = false;
			grpAudio.Enabled = false;
			//grpOptions.Enabled = false;
			grpTimings.Enabled = false;
			grpSavex.Enabled = false;
			//Array.Resize(ref timingsList, LISTcount);
			//Array.Resize(ref doTimes, LISTcount);

			//pnlVamping.Visible = true;

			bool needsFix = false;
			
			//Reset these from the last run
			errLevel = 0;
			doBarsBeats = false;
			doNoteOnsets = false;
			doTranscribe = false;
			doSpectrum = false;
			doPitchKey = false;
			doSegments = false;
			keepGoing = true;
			//string resultsFile = "";
			if (xLightsInstalled && !lightORamaInstalled)
			{
				string MASTERTRACK = "Beats + Song Information [Vamperizer]";
			}


				if (!chkReuse.Checked)
			{
				ClearLastRun();
			}

			//txtFileAudio.Text = Path.GetFileName(audioFileName);
			preppedAudio = PrepAudioFile(audioFileName);
			songTimeMS = GetDuration(audioData.Duration);
			theSong = audioData.Title;
			songTitle = audioData.Title;
			if (audioData.Artist.Length > 1)
			{
				theSong += BY + audioData.Artist;
				songArtist = audioData.Artist;
			}
			else
			{
				if (audioData.AlbumArtist.Length > 1)
				{
					theSong += BY + audioData.AlbumArtist;
					songArtist = audioData.AlbumArtist;
				}
			}
			this.Text = applicationName + " - " + theSong;
			ShowVamping(theSong);

			int alignOnsets = GetAlignment(cboAlignOnsets.Text);
			int alignTrans = GetAlignment(cboAlignTranscribe.Text);
			int alignSpectro = GetAlignment(cboAlignSpectrum.Text);
			int alignTempo = GetAlignment(cboAlignTempo.Text);
			int alignPitch = GetAlignment(cboAlignPitch.Text);
			int alignSegment = GetAlignment(cboAlignSegments.Text);
			int alignVoice = GetAlignment(cboAlignVocals.Text);


			///////////////////////
			///  SANITY CHECK  ///
			/////////////////////
			if ((chkBars.Checked == true) ||
					(alignOnsets == ALIGNbar) ||
					(alignOnsets == ALIGNbeatFull) ||
					(alignOnsets == ALIGNbeatHalf) ||
					(alignOnsets == ALIGNbeatQuarter) ||
					(alignOnsets == ALIGNbeatThird) ||
					(alignTrans == ALIGNbar) ||
					(alignTrans == ALIGNbeatFull) ||
					(alignTrans == ALIGNbeatHalf) ||
					(alignTrans == ALIGNbeatQuarter) ||
					(alignTrans == ALIGNbeatThird) ||
					(alignPitch == ALIGNbar) ||
					(alignPitch == ALIGNbeatFull) ||
					(alignPitch == ALIGNbeatHalf) ||
					(alignPitch == ALIGNbeatQuarter) ||
					(alignPitch == ALIGNbeatThird) ||
					(alignSegment == ALIGNbar) ||
					(alignSegment == ALIGNbeatFull) ||
					(alignSegment == ALIGNbeatHalf) ||
					(alignSegment == ALIGNbeatQuarter) ||
					(alignSegment == ALIGNbeatThird))
			{
				doBarsBeats = true;
			}
			if (chkBars.Checked ||
				chkBeatsFull.Checked ||
				chkBeatsHalf.Checked ||
				chkBeatsThird.Checked ||
				chkBeatsQuarter.Checked ||
				((alignOnsets >= ALIGNbar) && (alignOnsets <= ALIGNbeatQuarter)) ||
				((alignTrans >= ALIGNbar) && (alignTrans <= ALIGNbeatQuarter)) ||
				((alignSpectro >= ALIGNbar) && (alignSpectro <= ALIGNbeatQuarter)) ||
				((alignTempo >= ALIGNbar) && (alignTempo <= ALIGNbeatQuarter)) ||
				((alignPitch >= ALIGNbar) && (alignPitch <= ALIGNbeatQuarter)) ||
				((alignSegment >= ALIGNbar) && (alignSegment <= ALIGNbeatQuarter)) ||
				((alignVoice >= ALIGNbar) && (alignVoice <= ALIGNbeatQuarter)))
			{
				doBarsBeats = true;
			}


			if ((chkNoteOnsets.Checked == true) ||
					(alignTrans == ALIGNOnset) ||
					(alignPitch == ALIGNOnset) ||
					(alignSegment == ALIGNOnset))
			{
				doNoteOnsets = true;
			}
			if ((chkNoteOnsets.Checked) ||
				(alignTrans == ALIGNOnset) ||
				(alignSpectro == ALIGNOnset) ||
				(alignTempo == ALIGNOnset) ||
				(alignPitch == ALIGNOnset) ||
				(alignSegment == ALIGNOnset) ||
				(alignVoice == ALIGNOnset))
			{
				doNoteOnsets = true;
			}


			if (chkTranscribe.Checked)
			{
				doTranscribe = true;
			}
			if (chkPitchKey.Checked)
			{
				doPitchKey = true;
			}
			if (chkSegments.Checked)
			{
				doSegments = true;
			}



			if (doBarsBeats)
			{
				errLevel = RunBarBeats();
			}
			if (doNoteOnsets && (errLevel==0))
			{
				errLevel = RunNoteOnsets();
			}
			if (doTranscribe && (errLevel == 0))
			{
				errLevel = RunTranscribe();
			}
			if (doPitchKey && (errLevel == 0))
			{
				errLevel = RunPitchKey();
			}
			if (doSegments && (errLevel == 0))
			{
				errLevel = RunSegments();
			}



			//! TEMP
			/*



			//if (chkBars.Checked) doTimes[LISTbars] = true;
			//if (chkBeatsFull.Checked) doTimes[LISTbeatsFull] = true;
			//if (chkBeatsHalf.Checked) doTimes[LISTbeatsHalf] = true;
			//if (chkBeatsThird.Checked) doTimes[LISTbeatsThird] = true;
			//if (chkBeatsQuarter.Checked) doTimes[LISTbeatsQuarter] = true;

			// Run these plug-ins every time, no matter what data has been requested
			//   They are cross-referenced and used at least indirectly for most timings
			// Bars and Beats Data
			//TODO choice of algorithm
			//TODO if 3/4 time signature, change line 16 of qm-barbeattracker.n3
			//TODO    from value "4" to value "3"
			// Polyphonic Transcription Data
			resultsPoly = VampIt(VAMPqmPoly, FILEpoly);
			if (resultsPoly.Length > 4)
			{
				ReadPolyData(resultsPoly);
			}
			// Constant Q Spectrogram Data
			resultsConstQ = VampIt(VAMPconstq, FILEconstq);
			if (resultsConstQ.Length > 4)
			{
				//ReadConstQData(resultsConstQ);
			}

			lblAnalyzing.Text = "Processing Vamp Data";
			pnlVamping.Refresh();
			// Now analyze data collected above, and generate any additional data as needed
			// Bars and Beats Timings
			if (chkBars.Checked || chkBeatsFull.Checked || chkBeatsHalf.Checked || chkBeatsThird.Checked || chkBeatsQuarter.Checked)
			{
				if (resultsBarBeats.Length > 4)
				{
					AddBarBeatTimings(timeSignature, startBeat, ALIGNnone);
				}
			}

			// Note Onsets Timings
			if (chkNoteOnsets.Checked)
			{
				if (resultsNoteOnsets.Length > 4)
				{
					AddNoteOnsetTimings(ALIGNnone);
				}
			}

			// Key, Pitch, Melody
			if (chkPitchKey.Checked)
			{
				resultsKey = VampIt(VAMPkey, FILEkey);
				if (resultsKey.Length > 4)
				{
					ReadKeyData(resultsKey);
					AddKeyTimings(ALIGNnone);
				}
			}

			// Song Segments
			if (chkSegments.Checked)
			{
				//TODO: does not provide meaningful results.  Can the Vamp Plugin be tweaked?
				resultsSegments = VampIt(VAMPsegments, FILEsegments);
				if (resultsSegments.Length > 4)
				{
					//AddSegmentxTimingss(resultsSegments);
				}
			}

			// Chromagram
			if (chkChromagram.Checked)
			{
				resultsChroma = VampIt(VAMPchroma, FILEchroma);
				if (resultsChroma.Length > 4)
				{
					//AddChromaTimings(resultsChroma);
				}
			}

			// Tempo Changes
			if (chkTempo.Checked)
			{
				resultsTempo = VampIt(VAMPtempo, FILEtempo);
				if (resultsTempo.Length > 4)
				{
					//AddTempoTimings(resultsTempo);
				}
			}

			// Polyphonic Transcription
			if (chkTranscribe.Checked)
			{
				AddPolyTimings(ALIGNnone);
			}

			// Constant Q Spectrogram
			if (chkSpectrum.Checked)
			{
					//AddConstQTimings(resultsConstQ);
			}

			// Speech / Singing
			//TODO: does not provide meaningful results.  Can the Vamp Plugin Parameters be tweaked?
			if (chkVocals.Checked)
			{
				resultsVocals = VampIt(VAMPvocals, FILEvocals);
				if (resultsVocals.Length > 4)
				{
					//AddSpeechxTimingss(resultsSpeech);
				}
			}


			//ClearLastRun(); // Not yet, keep 'em for diagnostics


			//MessageBox.Show(Annotator.Sequence.summary());

			fileAudioLast = audioFileName;
			/*
			if (doAutoSave)
			{
				int f = 2;
				string autoSeqPath = "";
				string autoSeqName = "";
				string tryFile = "";
				string ext = Path.GetExtension(fileCurrent).ToLower();
				if (ext == ".lms")
				{
					autoSeqPath = Path.GetDirectoryName(fileCurrent);
					autoSeqName = Path.GetFileNameWithoutExtension(fileCurrent);
					tryFile = autoSeqPath + autoSeqName + ext;
					while (System.IO.File.Exists(tryFile))
					{
						tryFile = autoSeqPath + autoSeqName + " (" + f.ToString() + ")" + ext;
						f++;
					}
				}
				if (ext == ".lcc")
				{
					autoSeqPath = xUtils.DefaultSequencesPath;
					autoSeqName = Path.GetFileNameWithoutExtension(fileAudioOriginal);
					ext = ".lms";
					tryFile = autoSeqPath + autoSeqName + ext;
					while (System.IO.File.Exists(tryFile))
					{
						tryFile = autoSeqPath + autoSeqName + " (" + f.ToString() + ")" + ext;
						f++;
					}
				}
				SaveSequence(tryFile);
				fileSeqSave = tryFile;

				if (doAutoLaunch)
				{
					System.Diagnostics.Process.Start(fileSeqSave);
				}
			} // end AutoSave
			

			if (tuneTrack != null)
			{
				if (beatsGrid != null)
				{
					tuneTrack.timingGrid = beatsGrid;
				}
			}

			*/
		/*
			if (errLevel == 0)
			{
				btnSavexL.Enabled = true;
				fileAudioLast = audioFileName;
				grpAnalyze.Enabled = true;
				grpAudio.Enabled = true;
				//grpOptions.Enabled = true;
				grpTimings.Enabled = true;
				grpSavex.Enabled = true;

				btnSaveSeq.Enabled = true;
				grpSaveLOR.Enabled = true;

				//ImBusy(false);
				ShowVamping("");
				this.Cursor = Cursors.Default;
			}

			return errLevel;

		} // End Analyze Song
		*/
	}
}
