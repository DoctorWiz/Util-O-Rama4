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
using xLights22;
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
using LOR4;
using FileHelper;

namespace UtilORama4
{
	public static class Annotator
	{
		#region class variables and constants
		private const string WRITEformat = " -f -w csv --csv-force ";

		//public ITransform transformer = new BarBeats();
		//public Transpose transposer = new Transpose();
		public static string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";

		//public static VampBarBeats BarBeatsTransformer = null; // Initialized in constructor

		public static xTimings xBars = new xTimings(VampBarBeats.barsName);
		public static xTimings xBeatsFull = new xTimings(VampBarBeats.beatsFullName);
		public static xTimings xBeatsHalf = new xTimings(VampBarBeats.beatsHalfName);
		public static xTimings xBeatsThird = new xTimings(VampBarBeats.beatsThirdName);
		public static xTimings xBeatsQuarter = new xTimings(VampBarBeats.beatsQuarterName);
		//public static xTimings xFrames = new xTimings("Frames");
		public static xTimings xNoteOnsets = new xTimings(VampNoteOnsets.transformName);
		public static xTimings xAlignTo = null;
		public static vamps.AlignmentType AlignmentType = vamps.AlignmentType.None;

		public static int AverageBarMS = 1;
		public static int AverageBeatFullMS = 1;
		public static int AverageBeatQuarterMS = 1;

		// Default MS per Frame alignment is Zero
		// Zero indicates alignment to Bars, Beats, Onsets, or None
		// Will range from 100-17 for 10-60 FPS
		private static double msPerFrame = 0;
		private static int alignFPS = 0;


		public static int songTimeMS = 0;
		public static int BeatsPerBar = 4;
		public static int FirstBeat = 1;
		public static bool UseRamps = false;
		public static bool ReuseResults = false;
		public static bool Whiten = false;
		public static int StepSize = 512;
		public static int NeedStepSize = 0;
		public static bool StepErrFlag = false;
		private static int totalMilliseconds = 0;
		private static int highestTime = 0;
		//public static int TotalCentiseconds = 0;
		//private static int fps = 40;
		//private static int mspf = 25;

		private static int alignIndex = 0;
		//private static bool Fyle.DebugMode = Fyle.IsWizard || Fyle.IsAWizard;
		//public static LOR4Track VampTrack = null;

		public static LOR4Sequence Sequence = null;
		public const string VAMPTRACKname = "Vamp-O-Rama";
		public static LOR4Track VampTrack = null;
		public static LOR4Timings GridBeats = null;
		public static LOR4Timings GridOnsets = null;
		private static LOR4Channel[] noteChannels = null;

		// Note this is a temporary (bad pun) temp location
		// Should get overwritten by the REAL temp path
		// C:\\Users\\Username\\AppData\\Temp\\UtilORama\\VampORama\\
		//public static string WorkPath = "C:\\Windows\\Temp\\";
		public static string TempPath = Fyle.GetTempPath("Vamps");



		public enum TransformTypes { BarsAndBeats, NoteOnsets, PolyphonicTranscription, PitchAndKey, Tempo, Chromagram, Spectrogram, Segments, Chords };


		#endregion


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
			//xFrames = new xTimings("Frames");
			xNoteOnsets = new xTimings(VampNoteOnsets.transformName);
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
			msPerFrame = 0;
			alignIndex = 0;
			LOR4Sequence Sequence = null;
			VampTrack = null;
			noteChannels = null;
		}


		public static void Init(LOR4Sequence sequence)
		{
			Clear();
			Sequence = sequence;
			VampTrack = Sequence.FindTrack(VAMPTRACKname, true);
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

		public static void SetAlignment(vamps.AlignmentType alignTo)
		{
			AlignmentType = alignTo;
			if (alignTo == vamps.AlignmentType.Bars) xAlignTo = xBars;
			if (alignTo == vamps.AlignmentType.BeatsFull) xAlignTo = xBeatsFull;
			if (alignTo == vamps.AlignmentType.BeatsHalf) xAlignTo = xBeatsHalf;
			if (alignTo == vamps.AlignmentType.BeatsThird) xAlignTo = xBeatsThird;
			if (alignTo == vamps.AlignmentType.BeatsQuarter) xAlignTo = xBeatsQuarter;
			if (alignTo == vamps.AlignmentType.NoteOnsets) xAlignTo = xNoteOnsets;

			if ((alignTo == vamps.AlignmentType.Bars) ||
					(alignTo == vamps.AlignmentType.BeatsFull) ||
					(alignTo == vamps.AlignmentType.BeatsHalf) ||
					(alignTo == vamps.AlignmentType.BeatsThird) ||
					(alignTo == vamps.AlignmentType.BeatsQuarter) ||
					(alignTo == vamps.AlignmentType.NoteOnsets))
			{ alignFPS = 0; }

			if (alignTo == vamps.AlignmentType.FPS10) alignFPS = 10;
			if (alignTo == vamps.AlignmentType.FPS20) alignFPS = 20;
			if (alignTo == vamps.AlignmentType.FPS30) alignFPS = 30;
			if (alignTo == vamps.AlignmentType.FPS40) alignFPS = 40;
			if (alignTo == vamps.AlignmentType.FPS60) alignFPS = 60;

			if (alignTo == vamps.AlignmentType.None) alignFPS = 0;

			msPerFrame = (1000D / alignFPS);

			alignIndex = 0; // Reset any time alignment is changed or starting processing new annotations

		}

		public static int AlignTime(int theTime)
		{
			if (AlignmentType == vamps.AlignmentType.None)
			{
				return theTime;
			}
			else
			{
				int ret = theTime;

				if (alignFPS > 10)
				{
					// Divide then multiply by ms per frame to get earliest possible time
					// (Note: as an divide as integer so remainder gets discarded)
					int p1 = (int)(theTime / msPerFrame);
					int newStart = (int)(p1 * msPerFrame);
					// How much is half a frame?
					double half = (msPerFrame / 2D);
					// How far is it from the halfway point?
					double diff = (newStart + half) - theTime;
					// If less than zero, it is closer to the next frame
					// so add a frame's worth of time to the start calculated above
					if (diff < 0) newStart += (int)Math.Round(msPerFrame);
					ret = newStart;
				}
				else
				{
					if ((AlignmentType == vamps.AlignmentType.Bars) ||
							(AlignmentType == vamps.AlignmentType.BeatsFull) ||
							(AlignmentType == vamps.AlignmentType.BeatsHalf) ||
							(AlignmentType == vamps.AlignmentType.BeatsThird) ||
							(AlignmentType == vamps.AlignmentType.BeatsQuarter) ||
							(AlignmentType == vamps.AlignmentType.NoteOnsets))
					{
						if (xAlignTo != null)
						{
							if (xAlignTo.Markers.Count > 0)
							{
								int ns = AlignToNearestTiming(theTime);
								ret = ns;
							}
						}
						else
						{
							Fyle.BUG("Why is xAlignTo not set?");
						}
					}
				}
				return ret;
			}
		}

		private static int AlignToNearestTiming(int theTime)
		{
			//! Important: Reset alignIndex to xAdmin.UNDEFINED before starting new timings and/or different alignTimes.
			//! alignTimes are assumed to be in ascending numerical order.

			int retIdx = -1;
			int diffLo = 0;
			int diffHi = 0;
			bool keepTrying = true;
			int matchTime = -1;
			int effectTime = 0;
			int newStart = theTime;
			string msg = "";
			List<xMarker> Markers = xAlignTo.Markers;

			if (xAlignTo == null)
			{
				Fyle.BUG("Trying to align to a null set of timings!");
			}
			else
			{
				if (Markers.Count < 1)
				{
					Fyle.BUG("AlignTo timings are empty!");
				}
				else
				{
					while (keepTrying)
					{
						effectTime = Markers[alignIndex].starttime;
						// Is the time passed in past or equal to our current effect?
						if (theTime > effectTime)
						{
							// Is the current effect the last one?
							if (alignIndex > (Markers.Count - 2))
							{
								// Yup, last effect, we reached the end
								// How far past the last one is it?
								diffLo = theTime - effectTime;
								// And how far is it from the end of the song?
								diffHi = songTimeMS - theTime;
								// Closer Zero or to the CURRENT effect.starttime?
								if (diffLo < diffHi)
								{
									// Closer to to that last effect
									matchTime = effectTime;
									msg = "Time " + theTime.ToString() + " is between the last effect " + effectTime.ToString();
									msg += " and the end of the song " + songTimeMS.ToString() + " but is closer to the last effect ";
									msg += diffLo.ToString() + "<" + diffHi.ToString() + " so set to last effect " + matchTime.ToString();
									Debug.WriteLine(msg);
									// Woo-Hoo!  Got it!  Done!
									keepTrying = false;
								}
								else
								{
									// Closer to the end of the song
									matchTime = songTimeMS;
									msg = "Time " + theTime.ToString() + " is between the last effect " + effectTime.ToString();
									msg += " and the end of the song " + songTimeMS.ToString() + " but is closer to the the end of the song ";
									msg += diffLo.ToString() + ">" + diffHi.ToString() + " so set to end of the song " + matchTime.ToString();
									Debug.WriteLine(msg);
									// Move along people, we're done here.
									keepTrying = false;
								} // End if closer to last effect or to end of song
							}
							else // it is or isn't the last effect (and the time is past the current one)
							{
								// So how does it compare to the next one
								if (theTime > Markers[alignIndex + 1].starttime)
								{
									// Past the next one too
									// So advance the counter and keep trying
									msg = "Time " + theTime.ToString() + " is past the current [" + alignIndex.ToString() + "] effect " + effectTime.ToString();
									msg += " and there are more effects left and it is past the next [";
									msg += (alignIndex + 1).ToString() + "] effect too " + Markers[alignIndex + 1].starttime.ToString();
									msg += " so advancing the index to " + (alignIndex + 1).ToString();
									Debug.WriteLine(msg);
									alignIndex++;
								} // End if at the last effect (or not)
								else
								{
									// So at this point, by process of elimination
									// 1. The time should be past the current effect
									// 2. There are more effects left
									// 3. The time should be before or equal the next effect
									// This is the typical most common situation

									// How far past the current effect is it?
									diffLo = theTime - effectTime;
									// And how far is it from the next effect
									diffHi = Markers[alignIndex + 1].starttime - theTime;
									// Closer to the current effect.starttime or the next one?
									if (diffLo < diffHi)
									{
										// Closer to current one
										matchTime = effectTime;
										msg = "Time " + theTime.ToString() + " is between the cuurent [" + alignIndex.ToString() + "] effect " + effectTime.ToString();
										msg += " and the next [" + (alignIndex + 1).ToString() + "] effect " + songTimeMS.ToString() + " but is closer to the current effect ";
										msg += diffLo.ToString() + "<" + diffHi.ToString() + " so set to current effect " + matchTime.ToString();
										Debug.WriteLine(msg);
										// Woo-Hoo!  Got it!  Done!
										keepTrying = false;
									}
									else
									{
										// Closer to the next one
										matchTime = Markers[alignIndex + 1].starttime;
										msg = "Time " + theTime.ToString() + " is between the cuurent [" + alignIndex.ToString() + "] effect " + effectTime.ToString();
										msg += " and the next [" + (alignIndex + 1).ToString() + "] effect " + songTimeMS.ToString() + " but is closer to the next effect ";
										msg += diffLo.ToString() + ">" + diffHi.ToString() + " so set to next effect " + matchTime.ToString();
										Debug.WriteLine(msg);
										// Move along people, we're done here.
										keepTrying = false;
									} // End if closer to current or next effect
								} // end if time is or isn't past the next effect
							} // end if there is or is not more effects after the current one
						}
						else // time passed before or equal the current effect
						{
							// This normally should only happen if we haven't reached the first one yet
							// Are we at the very first effect?
							if (alignIndex < 1)
							{
								// So time must be between Zero and the first effect
								// How far past Zero is it?
								diffLo = theTime;
								// And how far is it from the [first] effect
								diffHi = effectTime - theTime;
								// Closer Zero or to the current (first) effect.starttime?
								if (diffLo < diffHi)
								{
									// Closer to Zero
									matchTime = 0;
									msg = "Time " + theTime.ToString() + " is between Zero and the first effect " + effectTime.ToString();
									msg += " but is closer to Zero " + diffLo.ToString() + "<" + diffHi.ToString() + " so set to Zero " + matchTime.ToString();
									Debug.WriteLine(msg);
									// Woo-Hoo!  Got it!  Done!
									keepTrying = false;
								}
								else
								{
									// Closer to that first effect
									matchTime = effectTime;
									msg = "Time " + theTime.ToString() + " is between Zero and the first effect " + effectTime.ToString();
									msg += " but is closer to the first effect " + diffLo.ToString() + "<" + diffHi.ToString();
									msg += " so set to first effect " + matchTime.ToString();
									Debug.WriteLine(msg);
									// Move along people, we're done here.
									keepTrying = false;
								} // End if closer to Zero or to first effect
							}
							else // Not at the first effect (there are effects before this one), and the time we are looking for is before this one
							{
								//! THIS SHOULD NOT HAPPEN UNDER NORMAL CIRCUMSTANCES!
								//Fyle.BUG("Alignment Going Backwards!", Fyle.Noises.Boing);
								//? Investigate WHY!  Effects out of order?
								msg = "** ALIGNMENT GOING BACKWARDS *******************************\r\n";
								msg = "Time " + theTime.ToString() + " is before the current [" + alignIndex.ToString() + "] effect " + effectTime.ToString();
								msg += " and this is not the first effect!";
								Debug.WriteLine(msg);
								alignIndex--;
							} // End the current effect is or isn't the first one
						} // End time is before or after the current effect
					} // End While Keep Trying Loop
				} // end if there is (or isn't} any effects in this collection
			} // End if the AlignTo collection is (or is not) null


			return matchTime;
		}

		public static int AlignFPS
		{
			// Frames-Per-Second
			set
			{
				alignFPS = value;
				if (alignFPS < 10) alignFPS = 10; // 100 MS
				if (alignFPS > 60) alignFPS = 60; // 16.6666 FPS
				msPerFrame = (1000D / alignFPS);
			}
			get
			{
				return alignFPS;
			}
		}

		public static double AlignMilliseconds
		{
			// Milliseconds-Per-Frame
			set
			{
				alignFPS = (int)Math.Round(1000D / value);
				if (alignFPS < 10) alignFPS = 10; // 100 MS
				if (alignFPS > 60) alignFPS = 60; // 116.6666 FPS
				msPerFrame = (1000D / alignFPS);
			}
			get
			{
				return msPerFrame;
			}
		}

		public static int AlignCentiseconds
		{
			// Milliseconds-Per-Frame
			set
			{
				alignFPS = (int)Math.Round(100D / value);
				if (alignFPS < 10) alignFPS = 10;  // 10 centiseconds
				if (alignFPS > 60) alignFPS = 60; // 1.6666 centiseconds
				msPerFrame = (1000D / alignFPS);
			}
			get
			{
				return (int)Math.Round(100D / alignFPS);
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
					if ((value > LOR4Admin.MINCentiseconds) && (value < LOR4Admin.MAXCentiseconds))
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
