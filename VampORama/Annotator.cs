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

		// Note this is a temporary (bad pun) temp location
		// Should get overwritten by the REAL temp path
		// C:\\Users\\Username\\AppData\\Temp\\UtilORama\\VampORama\\
		//public static string WorkPath = "C:\\Windows\\Temp\\";
		public static string TempPath = Fyle.GetTempPath("Vamps");
		


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
			//string pathWork = Path.GetDirectoryName(fileSong) + "\\";
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
						resultsFile = TempPath;
						resultsFile += "song_";
						resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
						resultsFile += ".csv";

						//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
						if ((!reuse) || (!Fyle.Exists(resultsFile)))
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
						procInfo.WorkingDirectory = TempPath;
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

					if (Fyle.Exists(resultsFile))
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
			//string pathWork = Path.GetDirectoryName(fileSong) + "\\";
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
					resultsFile = TempPath;
					resultsFile += "song_";
					resultsFile += Path.GetFileNameWithoutExtension(fileConfig);
					resultsFile += ".csv";

					//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
					if ((!reuse) || (!Fyle.Exists(resultsFile)))
					{
						string runthis = annotatorProgram + " " + annotatorArguments;
						//runthis += " 2>output.txt";
						string vampCommandLast = runthis;
						if (Fyle.DebugMode) Clipboard.SetText(runthis);

						Process cmdProc = new Process();
						ProcessStartInfo procInfo = new ProcessStartInfo();
						procInfo.FileName = "cmd.exe";
						procInfo.Arguments = "/k " + runthis;
						procInfo.WorkingDirectory = TempPath;
						cmdProc.StartInfo = procInfo;
						cmdProc.Start();
					}

					if (Fyle.Exists(resultsFile))
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

	}
}
