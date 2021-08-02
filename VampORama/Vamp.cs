using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Configuration;
//using System.Threading;
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

namespace UtilORama4
{
	public partial class frmVamp : Form
	{
		//! For testing, set this to false
		//! causes prog NOT to regenerate vamp files on every run and use whatever already exists in the temp folder
		const bool overwriteExistingVamps = true;
		//! for normal operation, set to true

		#region 'Global' variables

		private int errLevel = 0;
		private bool doBarsBeats = false;
		private bool doNoteOnsets = false;
		private bool doTranscribe = false;
		private bool  doSpectrum = false;
		private bool doPitchKey = false;
		private bool doSegments = false;
		private bool keepGoing = true;
		//public int beatsPerBar = 4;
		//public int firstBeat = 1;
		
		private int[] barsTimes = null;
		private int[] beatsFull = null;
		private int[] beatsHalf = null;
		private int[] beatsThird = null;
		private int[] beatsQuarter = null;
		private int[] onsets = null;
		private int[,] transcription = null;
		private int[,] spectrum = null;

		private int alignIdx = 0;

		//private int[] barsB = null;
		//private double[,] spectro = null;
		//private int[] spectroMS = null;
		//private int[] polyNotes = null;
		//private int[] polyMSstart = null;
		//private int[] polyMSlen = null;
		//private int[] keys = null;
		//private int[] keyMS = null;
		//private double[,] chroma = null;
		//private int[] chromaMS = null;




		xTimings[] noteTimings = null;
		private string songTitle = "[Unknown]";
		private string songArtist = "[Unknown]";
		private const string BY = " by ";
		private string theSong = "[Unknown] by [Unknown]";
		private int songTimeMS = 0;
		string preppedAudio = "";
		private int cmdLineCount = 0;
		private int vampErrCount = 0;
		private int step_size = 0;

		string vampCommandLast = ""; // Remember last command just in case of error


		//private int lastFoundIdx = xUtils.UNDEFINED;
		//private bool[] doTimes = null;
		//private xTimings[] timingsList = null;
		//string vampConfigs = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";



		//enum TimingTypes { Bars = 0, BeatsFull, BeatsHalf, BeatsThird, BeatsQuarter, NoteOnsets, PitchKey, Segments, Chromagram, Tempo, Polyphonic, ConstQ, Flux, Chords, Vocals }



		//private string fileResults = "";
		//private string preppedFileName = "";
		//private string prepFile = "";
		//private string preppedPath = "";
		//private string preppedExt = "";
		//private string preppedAudio = "";
		private string fileCurrent = "";
		private string pluginsPath = "\"C:\\Program Files (x86)\\Vamp Plugins\\\"";
		//private Track tuneTrack = null;
		private xTimings beatsGrid = null;
		

		#endregion

		/*
		private void Vamperize(string songFile)
		{
			string songFileTemp = utils.GetAppTempFolder() + "song.mp3";
			int errs = utils.SafeCopy(songFile, songFileTemp, true);
			if (errs == 0)
			{
				// Fetch common info used by all or most plugins
				int beatsPerPar = 4;
				if (swTrackBeat.Checked) beatsPerBar = 3;
				int firstBeat = 1;
				Int32.TryParse(txtStartBeat.Text, out firstBeat);
				if (firstBeat < 1) firstBeat = 1;
				if (firstBeat > beatsPerBar) firstBeat = beatsPerBar;
				bool reuse = chkReuse.Checked;
				int stepSize = 512;
				Int32.TryParse(cboStepSize.Text, out stepSize);
				if ((stepSize < 200) || (stepSize > 800)) stepSize = 512;
				bool ramps = swRamps.Checked;
				string resultsFile = "";

				if (chkBarsBeats.Checked)
				{
					BarBeats barBeats = new BarBeats();
					int i = cboDetectBarBeats.SelectedIndex;
					// ARRRRGGGHH!!!  Can't figure out the syntax to make this work...
					//BarBeats.DetectionMethods detMethod = )BarBeats.DetectionMethods)i;
					// So I guess I gotta do it the friggin' hard way...
					BarBeats.DetectionMethods detMethod = BarBeats.DetectionMethods.ComplexDomain; // default 0
					if (i == 1) detMethod = BarBeats.DetectionMethods.SpectralDifference;
					if (i == 2) detMethod = BarBeats.DetectionMethods.PhaseDeviation;
					if (i == 3) detMethod = BarBeats.DetectionMethods.BroadbandEnergyRise;

					resultsFile = barBeats.AnnotateSong(songFile, cboMethodBarsBeats.SelectedIndex, beatsPerBar, stepSize,
									detMethod, reuse, chkWhiteBarBeats.Checked);
				}
			}

		}
		*/


		/*


		private int RunBarBeats()
		{
			int failedAttempts = 0;
			int errLevel = 5001;

			//while (failedAttempts < cboStepSize.Items.Count)
			//{
				resultsBarBeats = GenerateBarBeatsData();
				if (resultsBarBeats.Length > 4)
				{
				transposer.ReadBeatData(resultsBarBeats, beatsPerBar, firstBeat, xBars, xBeatsFull, xBeatsHalf, xBeatsThird, xBeatsQuarter, int align, int bpb = 4, int fb = 1, bool reuse = false)
		{);
					errLevel = 0;
					failedAttempts = 9999; // Force exit of while loop and also indicate success
				}
				else
				{
					string msg = cboMethodBarsBeats.Text + " failed to generate a valid results file!\r\n";
					msg += "Try running '" + vampCommandLast + "' in '" + tempPath + "'.\r\n";
					msg += "(You may need to change the step size)";
					MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			//}
			//if (failedAttempts ==9999)
			return errLevel;
		}
	
		private int RunNoteOnsets()
		{
			int errLevel = 6001;
			resultsNoteOnsets = GenerateNoteOnsetsData();
			// Note Onsets Data
			if (resultsNoteOnsets.Length > 4)
			{
				ReadOnsetData(resultsNoteOnsets);
				errLevel = 0;
			}
			else
			{
				string msg = cboMethodOnsets.Text + " failed to generate a valid results file!\r\n";
				msg += "Try running '" + vampCommandLast + "' in '" + tempPath + "'.\r\n";
				msg += "(You may need to change the step size)";
				MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return errLevel;
		}

		private int RunTranscribe()
		{
			int errLevel = 70001;
			resultsTranscribe = GenerateTransciptionData();
			if (resultsTranscribe.Length > 4)
			{
				ReadTranscriptionData(resultsTranscribe);
				errLevel = 0;
			}
			return errLevel;
		}

		private int RunPitchKey()
		{
			int errLevel = 8001;
			resultsKey = GenerateKeyData();
			if (resultsKey.Length > 4)
			{
				ReadKeyData(resultsKey);
				errLevel = 0;
			}
			else
			{
				string msg = cboMethodPitchKey.Text + " failed to generate a valid results file!\r\n";
				msg += "Try running '" + vampCommandLast + "' in '" + tempPath + "'.\r\n";
				msg += "(You may need to change the step size)";
				MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return errLevel;
		}

		private int RunSegments()
		{
			int errLevel = 9001;
			resultsSegments = GenerateSegmentData();
			if (resultsSegments.Length > 4)
			{
				ReadSegmentData(resultsSegments);
				errLevel = 0;
			}
			else
			{
				string msg = cboMethodSegments.Text + " failed to generate a valid results file!\r\n";
				msg += "Try running '" + vampCommandLast + "' in '" + tempPath + "'.\r\n";
				msg += "(You may need to change the step size)";
				MessageBox.Show(this, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return errLevel;
		}


		
		private string PrepPath(string pathIn)
		{
			return pathIn.Replace('\\', '/');
		}

		private string PrepName(string nameIn)
		{
			string ret = nameIn.Replace(" ", "");
			ret = ret.Replace(".", "");
			ret = ret.Replace("-", "");
			if (ret.Length > 16) ret = ret.Substring(0, 16);
			ret = ret.ToLower();
			return ret;
		}

		private string GenerateBarBeatsData()
		{
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory + "VampConfigs\\";
			int lineCount = 0;
			string lineIn = "";
			string fromVamp = vampConfigs;
			string toVamp = tempPath;
			string vampFile = "";
			int err = 0;
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;

			switch (cboMethodBarsBeats.SelectedIndex)
			{
				case 0:
					// Queen Mary Bar and Beat Tracker
					vampParams = VAMPbarBeats;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					reader = new StreamReader(fromVamp);
					writer = new StreamWriter(toVamp);
					while (!reader.EndOfStream)
					{
						lineCount++;
						lineIn = reader.ReadLine();
						if (lineCount == 7)
						{
							lineIn = lineIn.Replace("557", cboStepSize.Text);
						}
						if (lineCount == 16)
						{
							if (swTrackBeat.Checked)
							{
								beatsPerBar = 3;
								lineIn = lineIn.Replace('4', '3');
							}
							else
							{
								// Reset back to default in case it was changed
								beatsPerBar = 4;
							}
							// Get First Beat from textbox
							int fb = 1; // default
							int.TryParse(txtStartBeat.Text, out fb); // try to parse textbox text
							if ((fb < 1) || (fb > beatsPerBar)) fb = 1; // check value, if out of range set to default
							firstBeat = fb; // remember it for later
						}
						writer.WriteLine(lineIn);
					}
					reader.Close();
					writer.Close();
					break;

				case 1:
					// Queen Mary Tempo and Beat Tracker
					vampParams = VAMPqmBeats;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					reader = new StreamReader(fromVamp);
					writer = new StreamWriter(toVamp);
					while (!reader.EndOfStream)
					{
						lineCount++;
						lineIn = reader.ReadLine();
						if (lineCount == 7)
						{
							lineIn = lineIn.Replace("557", cboStepSize.Text);
						}
						if (lineCount == 20)
						{
							string m = (cboDetectBarBeats.SelectedIndex + 1).ToString();
							lineIn = lineIn.Replace("3", m);
						}
						if (lineCount == 32)
						{
							if (chkWhiteBarBeats.Checked)
							{
								lineIn = lineIn.Replace('0', '1');
							}
						}
						writer.WriteLine(lineIn);
					}
					reader.Close();
					writer.Close();
					break;
				case 2:
					// BeatRoot Beat Tracker
					vampParams = VAMPbeatRoot;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 3:
					// Porto Beat Tracker
					vampParams = VAMPportoBeat;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 4:
					// Aubio Beat Tracker
					vampParams = VAMPaubioBeat;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
			}
			//rezults = tempPath + "song_" + vampFile + ".csv";

			return VampIt(vampParams, toVamp);
		}

		private int StepSizeError(string outputFile)
		{
			int sizeNeeded = 0;
			int lineCount = 0;
			string lineIn = "";
			//StreamReader reader = new StreamReader(outputFile);
			string outFile = tempPath + "output.log";
			StreamReader reader = new StreamReader(outFile);

			//StreamWriter writer = new StreamWriter(System.IO.Path.GetFileName(outputFile) + ".errors");
			string logFile = tempPath + "error.log";
			StreamWriter writer = new StreamWriter(logFile);
			while (!reader.EndOfStream)
			{
				lineCount++;
				lineIn = reader.ReadLine();
				int p = lineIn.ToLower().IndexOf("error");
				if (p >= 0)
				{
					writer.WriteLine(lineIn);
					p = lineIn.ToLower().IndexOf("step_size");
					if (p >= 0)
					{
						if (izwiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
				}
			}
			reader.Close();
			writer.Close();


			return sizeNeeded;
		}

		private bool ChangeStepSize(string n3file, int newStepSize)
		{
			bool ret = false;
			int lineCount = 0;
			string lineIn = "";
			string newFile = System.IO.Path.GetFileNameWithoutExtension(n3file) + ".n4";
			StreamReader reader = new StreamReader(n3file);
			StreamWriter writer = new StreamWriter(newFile);
			while (!reader.EndOfStream)
			{
				lineCount++;
				lineIn = reader.ReadLine();
				int p = lineIn.ToLower().IndexOf("step_size");
				if (p>=0)
				{
					if (izwiz)
					{
						System.Diagnostics.Debugger.Break();
					}

				}
				writer.WriteLine(lineIn);
			}
			reader.Close();
			writer.Close();
			System.IO.File.Delete(n3file);
			System.IO.File.Move(newFile, n3file);
			ret = true;
			return ret;
		}

		private string GenerateNoteOnsetsData()
		{
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory;
			int lineCount = 0;
			string lineIn = "";
			string vampFile = "";
			string fromVamp = vampConfigs;
			string toVamp = tempPath;
			int err = 0;
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;
			string msg = "";
			DialogResult dr;

			switch (cboMethodOnsets.SelectedIndex)
			{
				case 0:
					// Queen Mary Note Onset Detector
					vampParams = VAMPnoteOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					reader = new StreamReader(fromVamp);
					writer = new StreamWriter(toVamp);
					while (!reader.EndOfStream)
					{
						lineCount++;
						lineIn = reader.ReadLine();
						if (lineCount == 7)
						{
							lineIn = lineIn.Replace("557", cboStepSize.Text);
						}
						if (lineCount == 12)
						{
							string m = (cboDetectOnsets.SelectedIndex + 1).ToString();
							lineIn = lineIn.Replace("3", m);
						}
						if (lineCount == 16)
						{
								lineIn = lineIn.Replace("50", txtOnsetSensitivity.Text);
						}
						if (lineCount == 20)
						{
							if (chkWhiteOnsets.Checked)
							{
								lineIn = lineIn.Replace('0', '1');
							}
						}
						writer.WriteLine(lineIn);
					}
					reader.Close();
					writer.Close();
					break;

				case 1:
					// Queen Mary Polyphonic Transcription
					vampParams = VAMPqmPoly;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 2:
					// OnsetDS Onset Detector
					vampParams = VAMPonsetDS;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					reader = new StreamReader(fromVamp);
					writer = new StreamWriter(toVamp);
					while (!reader.EndOfStream)
					{
						lineCount++;
						lineIn = reader.ReadLine();
						if (lineCount == 19)
						{
							string m = (cboDetectOnsets.SelectedIndex + 1).ToString();
							lineIn = lineIn.Replace("3", m);
						}
						writer.WriteLine(lineIn);
					}
					reader.Close();
					writer.Close();
					break;
				case 3:
					// Silvet Note Transcription
					vampParams = VAMPsilvetOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 6:
					// Alicante Note Onset Detector
					msg = "Alicante Note Onset Detector is not currently available.  Using Queen Mary Note Onsets instead.";
					dr = MessageBox.Show(this, msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					vampParams = VAMPnoteOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3"; ;
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 7:
					// Alicante Polyphonic Transcription
					msg = "Alicante Polyphonic Transcription is not currently available.  Using Queen Mary Note Onsets instead.";
					dr = MessageBox.Show(this, msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					vampParams = VAMPnoteOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 4:
					// Aubio Onset Detector
					vampParams = VAMPaubioOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 5:
					// Aubio Note Tracker
					vampParams = VAMPaubioPoly;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
			}
			//rezults = tempPath + "song_" + vampFile + ".csv";

			return VampIt(vampParams, toVamp);
		}

		private string GenerateTransciptionData()
		{
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory;
			int lineCount = 0;
			string lineIn = "";
			string vampFile = "";
			string fromVamp = vampConfigs;
			string toVamp = tempPath;
			int err = 0;
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;
			string msg = "";
			DialogResult dr;





			switch (cboMethodTranscription.SelectedIndex)
			{
				case 0:
					// Queen Mary Polyphonic Transcription
					vampParams = VAMPqmPoly;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;

				case 1:
					// Silvet Note Transcription
					vampParams = VAMPsilvetOnset;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 2:
					// Aubio Note Tracker
					vampParams = VAMPaubioPoly;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 3:
					// *Alicante Polyphonic Transcription
					msg = "Alicante Polyphonic Transcription is not currently available.  Using Queen Mary Transcription instead.";
					dr = MessageBox.Show(this, msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					vampParams = VAMPqmPoly;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
			}
			//rezults = tempPath + "song_" + vampFile + ".csv";

			return VampIt(vampParams, toVamp);
		}

		private string GenerateKeyData()
		{
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory;
			int lineCount = 0;
			string lineIn = "";
			string vampFile = "";
			string fromVamp = vampConfigs;
			string toVamp = tempPath;
			int err = 0;
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;
			string msg = "";
			DialogResult dr;

			switch (cboMethodPitchKey.SelectedIndex)
			{
				case 0:
					// Queen Mary Key Detector
					vampParams = VAMPkey;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;

				case 1:
					// Queen Mary Tonal Change
					vampParams = VAMPtonal;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 2:
					// Melodia Melody Extraction
					vampParams = VAMPmelody;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 3:
					// Cepstral Pitch Tracker
					//msg = "Cepstral Pitch Tracker is not currently available.  Using Queen Key Detector instead.";
					//dr = MessageBox.Show(this, msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					//vampParams = VAMPkey;
					vampParams = VAMPpitch;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 4:
					// libxtract Fundamental Frequency
					vampParams = VAMPfundFreq;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 5:
					// pYIN Monophonic Pitch
					msg = "pYIN Monophonic Pitch Plugin is not currently available.  Using Queen Key Detector instead.";
					dr = MessageBox.Show(this, msg, "Plugin not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					vampParams = VAMPkey;
					//vampParams = VAMPpYIN;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
				case 6:
					// Aubio Pitch Detector
					vampParams = VAMPaubioPitch;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
			}
			//rezults = tempPath + "song_" + vampFile + ".csv";

			return VampIt(vampParams, toVamp);
		}

		private string GenerateSegmentData()
		{
			string vampParams = "";
			//string homedir = AppDomain.CurrentDomain.BaseDirectory;
			int lineCount = 0;
			string lineIn = "";
			string vampFile = "";
			string fromVamp = vampConfigs;
			string toVamp = tempPath;
			int err = 0;
			//string rezults = "";
			StreamReader reader;
			StreamWriter writer;
			string msg = "";
			DialogResult dr;

			switch (cboMethodSegments.SelectedIndex)
			{
				case 0:
					// Queen Mary Segmenter
					vampParams = VAMPsegmenter;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;

				case 1:
					// Segmentino
					vampParams = VAMPsegmentino;
					vampFile = vampParams.Replace(':', '_') + ".n3";
					fromVamp += vampFile;
					toVamp = tempPath + vampFile;
					err = SafeCopy(fromVamp, toVamp);
					break;
			}
			//rezults = tempPath + "song_" + vampFile + ".csv";

			return VampIt(vampParams, toVamp);
		}

		private int SafeCopy(string fromFile, string toFile, bool overWrite = true)
		{ 
			int err = 0;
			try
			{
				System.IO.File.Copy(fromFile, toFile, overWrite);
			}
			catch(Exception e)
			{
				err = e.HResult;
			}
			return err;
		}

		private string GenerateSpectralData()
		{
			string rezults = "";
			return rezults;
		}

		private string VampIt(string VampParams, string configFile)
		{
			string resultsFile = "";

			const string ERRproc = " in RunConstQ(";
			const string ERRtuneTrack = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";
			int err = 0;
			int errCount = 0;
			int logErrs = 0;
			string ex = Path.GetExtension(fileAudioLast).ToLower();
			string annotatorArguments = "-t " + configFile;
			annotatorArguments += " " + fileAudioWork; // + ex;
			annotatorArguments += WRITEformat;
			string outputFile = tempPath + "output.log";

			//string configs = tempPath + configFile;
			//string configs = configFile;
			//if (!System.IO.File.Exists(configs))
			//{
			//	string homedir = AppDomain.CurrentDomain.BaseDirectory;
			//	string srcconfig = homedir + Path.GetFileName( configFile);
			//	err = SafeCopy(srcconfig, configs);
			//}
			try
			{
				//while (errCount < cboStepSize.Items.Count)
				//{
					string emsg = annotatorProgram + " " + annotatorArguments;
					Console.WriteLine(emsg);
					//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
					DialogResult dr = DialogResult.Yes;

					if (dr == DialogResult.Yes)
					{
						if (izwiz) Clipboard.SetText(emsg);
					}
					if (dr != DialogResult.Cancel)
					{
						//string vampback = VampParams.Replace(':', '_');
						resultsFile = tempPath;
						resultsFile += "song_"; // Path.GetFileNameWithoutExtension(fileAudioWork);
																		//resultsFile += vampback + ".csv";
						resultsFile += Path.GetFileNameWithoutExtension(configFile);
						resultsFile += ".csv";

						//! FOR TESTING DEBUGGING, if set to re-use old files, OR if no file from a previous run exists
						if ((!chkReuse.Checked) || (!System.IO.File.Exists(resultsFile)))
						{
							//ProcessStartInfo annotator = new ProcessStartInfo(annotatorProgram);
							//annotator.Arguments = annotatorArguments;
							string runthis = annotatorProgram + " " + annotatorArguments;
							vampCommandLast = runthis;
							if (izwiz) Clipboard.SetText(runthis);



							//Level ll = Level.Info; // Initialize to default
							cmdLineCount = 0; //Reset these
							vampErrCount = 0;
							step_size = 0;

							Process cmdProc = new Process();
							ProcessStartInfo procInfo = new ProcessStartInfo();
							procInfo.FileName = annotatorProgram;
							//annotatorArguments += " > " + outputFile;

							//procInfo.CreateNoWindow = true;
							//procInfo.RedirectStandardOutput = true;
							//procInfo.RedirectStandardInput = true;
							//procInfo.RedirectStandardError = true;
							//procInfo.UseShellExecute = false;
							procInfo.Arguments = annotatorArguments;
							procInfo.WorkingDirectory = tempPath;
							cmdProc.StartInfo = procInfo;
							//cmdProc.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
							//cmdProc.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);


							
							cmdProc.Start();
							//cmdProc.BeginOutputReadLine();
							//string cmdOut = cmdProc.StandardOutput.ReadToEnd();
							//while (!cmdProc.StandardOutput.EndOfStream)
							//{
							//	string xx = cmdProc.StandardOutput.ReadLine();
							//}
							//Debug.WriteLine("read");
							//while (!cmdProc.StandardOutput.EndOfStream)
							//{
							//	string lineX = cmdProc.StandardOutput.ReadLine();
							//	string lowline = lineX.ToLower();
							//	ll = Level.Info;
							//	int px = lowline.IndexOf("error");
							//	if (px >= 0)
							//	{
							//		ll = Level.Error;
							//		logErrs++;
							//	}
							//	//consoleWindow.Log(ll,lineX);
							//	Log2Console(ll, lineX);
							//}


							//ThreadStart ths = new ThreadStart(() => cmdProc.Start());
							//Thread th = new Thread(ths);
							//th.Start();

							//ThreadStart ths = new ThreadStart(() =>
							//{
							//	bool b = cmdProc.Start();
							//});
							//Thread th = new Thread(ths);
							//th.Start();





							//Process cmd = Process.Start(annotator);
							cmdProc.WaitForExit(120000);  // 2 minute timeout
							int x = cmdProc.ExitCode;
						}

						if (System.IO.File.Exists(resultsFile))
						{
					//		return resultsFile;
					//		errCount = 99999;
						}
						else
						{
						// NO RESULTS FILE!	
						System.Diagnostics.Debugger.Break();
					//		int ss = StepSizeError(outputFile);
					//		if (ss==0)
					//		{
					//			errCount = 99998;
					//		}
					//		else
					//		{
					//			int q = 99997;
					//			if (ss == Int32.Parse(cboStepSize.Text))
					//			{

					//			}
					//			else
					//			{
					//				ChangeStepSize(configFile, ss);
					//				errCount++;
					//			}
					//		}
						}
					}
				//}
			}
			catch
			{
				errCount = 99995;
				if (izwiz)
				{

				}
				resultsFile = "";
			}

			return resultsFile;
		}

		/*private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			string lineX = outLine.Data.ToString();
			cmdLineCount++;
			string lowline = lineX.ToLower();
			Level ll = Level.Info;
			int px = lowline.IndexOf("error");
			if (px >= 0)
			{
				ll = Level.Error;
				vampErrCount++;
				px = lowline.IndexOf("step_size");
				if (px >= 0)
				{
					int ist = px + 11; // Magic Number: begining of 'step_size' + it's length (9) +1 for the space, and +1 for the beginning quote
					string stpsz = lowline.Substring(ist, 6); // Magic Number: 6 'cuz step size is 3 digits but there maybe multiple quote marks around it
					int nss = 0;
					Int32.TryParse(stpsz, out nss);
					if ((nss > 200) && (nss < 600))
					{
						step_size = nss;
					}
				}
			}
			//consoleWindow.Log(ll,lineX);
			Log2Console(ll, lineX);
		}
		*/
		/*
			private int PHOO_ParseMilliseconds(string secondsValue)
		{
			int ppos = secondsValue.IndexOf('.');
			// Get number of seconds before the period
			int sec = Int16.Parse(secondsValue.Substring(0, ppos));
			// Get the fraction of a second after the period, only keep most significant 4 digits
			//int dotsec = Int16.Parse(secondsValue.Substring(ppos + 1, 4));
			// Get the fraction of a second after the period
			string subsec = secondsValue.Substring(ppos + 1);
			int dotsec = Int16.Parse(subsec);

			// turn it from an int into an actual fraction
			//decimal ds = (dotsec / 10000000);
			// Round up or down from 4 digits to 2
			//dotsec = (int)Math.Round(ds);  // man is this stupid call picky as hell about syntax
			double div = Math.Pow(10, subsec.Length - 3);
			double dss = Math.Round(dotsec / div);
			int ms = (int)dss;


			// Combine seconds and fraction of a second into Milliseconds
			int millisecs = sec * 1000 + ms;

			return millisecs;

		}


		private int ClearLastRun()
		{
			int errs = 0;
			if (!chkReuse.Checked)
			{
				try { if (System.IO.File.Exists(resultsNoteOnsets)) System.IO.File.Delete(resultsNoteOnsets); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsBarBeats)) System.IO.File.Delete(resultsBarBeats); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsqmBeats)) System.IO.File.Delete(resultsqmBeats); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsTranscribe)) System.IO.File.Delete(resultsTranscribe); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsConstQ)) System.IO.File.Delete(resultsConstQ); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsChroma)) System.IO.File.Delete(resultsChroma); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsSegments)) System.IO.File.Delete(resultsSegments); }
				catch { errs++; }
				try { if (System.IO.File.Exists(resultsSpectro)) System.IO.File.Delete(resultsSpectro); }
				catch { errs++; }
				try { if (System.IO.File.Exists(fileAudioWork)) System.IO.File.Delete(fileAudioWork); }
				catch { errs++; }

				errs += ClearTempDir();

				resultsNoteOnsets = "";
				resultsBarBeats = "";
				resultsqmBeats = "";
				resultsTranscribe = "";
				resultsConstQ = "";
				resultsChroma = "";
				resultsSegments = "";
				resultsSpectro = "";
			}

			return errs;
		}

		private void SelectUsedTimings(int threshold)
		{

			/*
			 // NEEDS REWORK!!
			for (int n = 0; n < noteTimings.Length; n++)
			{
				if (noteTimings[n].effects.Count > threshold)
				{
					noteTimings[n].Selected = true;
				}
				else
				{
					noteTimings[n].Selected = false;
				}
			}

			for (int g = 0; g < octaveGroups.Length; g++)
			{
				if (octaveGroups[g] != null)
				{
					bool hasSelChild = false;
					IMember chld = null;
					for (int c = 0; c < octaveGroups[g].Members.Count; c++)
					{
						chld = octaveGroups[g].Members.Items[c];
						if (chld.Selected)
						{
							hasSelChild = true;
							c = octaveGroups[g].Members.Count;
						}
					}
					octaveGroups[g].Selected = hasSelChild;
				}
			}
		*/
		//} // end SelectUsedxTimingss
	/*
		private void ShowVamping(string songName)
		{
			if (songName.Length > 1)
			{
				lblSongName.Text = songName;
				panelX = (this.ClientSize.Width - pnlVamping.Width) / 2;
				panelY = (this.ClientSize.Height - pnlVamping.Height) / 2;
				//int x = grpAnalyze.Left;
				//int y = grpAnalyze.Top;
				//pnlVamping.Location = new Point(x, y);
				pnlVamping.BringToFront();
				pnlVamping.Visible = true;
				pnlVamping.Location = new Point((int)panelX, (int)panelY);

				grpAudio.Enabled = false;
				grpAnalyze.Enabled = false;
				grpSavex.Enabled = false;
				grpTimings.Enabled = false;

				tmrAni.Enabled = true;
				//pnlVamping.Refresh();
				this.Refresh();
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				tmrAni.Enabled = false;
				pnlVamping.SendToBack();
				pnlVamping.Visible = false;

				grpAudio.Enabled = true;
				grpAnalyze.Enabled = true;
				grpSavex.Enabled = true;
				grpTimings.Enabled = true;

				this.Cursor = Cursors.Default;

			}
		}

		void myBackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			int n = Convert.ToInt32(e.Argument);
			e.Result = PerformComplexComputations(n, worker, e);
		}
		private long PerformComplexComputations(int n, BackgroundWorker worker, DoWorkEventArgs e)
		{
			long result = 0;
			if (worker.CancellationPending)
			{
				e.Cancel = true;
			}
			else
			{
				if (n < 2) return 1;
				result = PerformComplexComputations(n - 1, worker, e) + PerformComplexComputations(n - 2, worker, e);
			}
			return result;
		}

		void myBackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//this.radWaitingBar1.StopWaiting();
			//this.radWaitingBar1.ResetWaiting();
			//this.radButton1.Enabled = true;
			//this.radButton2.Enabled = false;
			//end = DateTime.Now;
			if ((e.Cancelled == true))
			{
				//this.radLabel1.Text = "Calculations are canceled!";
			}
			else if (!(e.Error == null))
			{
				//this.radLabel1.Text = ("Error: " + e.Error.Message);
			}
			else
			{
				//this.radLabel1.Text = "The " + this.radSpinEditor1.Value.ToString() + "th member of the Fibonacci sequence is: " + e.Result.ToString();
				TimeSpan span = new TimeSpan();
				//span = end - begin;
				//this.radLabel1.Text += "\nCalculating time: " + span.TotalSeconds + " seconds";
			}
		}

		#region Timings
		

		private int ReadOnsetData(string noteOnsetFile)
		{
			int onsetCount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int lastOnset = 0;
			int align = SetAlignmentHost(cboAlignOnsets.Text);
			string onsetLabel = "";
			int duration = 0;
			int note = 0;


			StreamReader reader = new StreamReader(noteOnsetFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			xOnsets = new xTimings("Note Onsets");

			reader = new StreamReader(noteOnsetFile);



			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = ParseMilliseconds(parts[0]);
					if (align > ALIGNnone)
					{
						if ((align == ALIGN25) || (align == ALIGN50))
						{
							millisecs = AlignStartTo(millisecs, align);
						}
						else
						{
							if ((align == ALIGNbar) ||
								(align == ALIGNbeatFull) ||
								(align == ALIGNbeatHalf) ||
								(align == ALIGNbeatThird) ||
								(align == ALIGNbeatQuarter) ||
								(align == ALIGNOnset))
							{
								millisecs = AlignStartTo(millisecs, align);
							}
						}
					}
					lastOnset = millisecs;
				} // end line contains a period
			} // end while loop more lines remaining


			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = ParseMilliseconds(parts[0]);
					if (align > ALIGNnone)
					{
						if ((align == ALIGN25) || (align == ALIGN50))
						{
							millisecs = AlignStartTo(millisecs, align);
						}
						else
						{
							if ((align == ALIGNbar) ||
								(align == ALIGNbeatFull) ||
								(align == ALIGNbeatHalf) ||
								(align == ALIGNbeatThird) ||
								(align == ALIGNbeatQuarter) ||
								(align == ALIGNOnset))
							{
								millisecs = AlignStartTo(millisecs, align);
							}
						}
					}

					// Avoid closely spaced duplicates
					if (millisecs > lastOnset)
					{
						// Get label, if available
						if (parts.Length == 3)
						{
							duration = ParseMilliseconds(parts[1]);
							note = Int16.Parse(parts[2]);

							if (cboLabelsOnsets.SelectedIndex == 1)
							{
								onsetLabel = noteNames[note];
							}
							if (cboLabelsOnsets.SelectedIndex == 2)
							{
								onsetLabel = note.ToString();
							}
						}
						if (parts.Length == 4)
						{
							//duration = ParseMilliseconds(parts[1]);
							string nn = parts[3];

							if (cboLabelsOnsets.SelectedIndex == 1)
							{
								onsetLabel = nn;
							}
							if (cboLabelsOnsets.SelectedIndex == 2)
							{
								//onsetLabel = note.ToString();
							}
						}


						xOnsets.Add(onsetLabel, lastOnset, millisecs);
						lastOnset = millisecs;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Note Onsets

		private int ReadTranscriptionData(string transcriptionFile)
		{
			int onsetCount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int align = SetAlignmentHost(cboAlignTranscribe.Text);
			string noteLabel = "";
			int duration = 0;
			int note = 0;
			int lastNote = -1;

			StreamReader reader = new StreamReader(transcriptionFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			xTranscription = new xTimings("Polyphonic Transcription");

			reader = new StreamReader(transcriptionFile);



			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					millisecs = ParseMilliseconds(parts[0]);
					if (align > ALIGNnone)
					{
						if ((align == ALIGN25) || (align == ALIGN50))
						{
							millisecs = AlignStartTo(millisecs, align);
						}
						else
						{
							if ((align == ALIGNbar) ||
								(align == ALIGNbeatFull) ||
								(align == ALIGNbeatHalf) ||
								(align == ALIGNbeatThird) ||
								(align == ALIGNbeatQuarter) ||
								(align == ALIGNOnset))
							{
								millisecs = AlignStartTo(millisecs, align);
							}
						}
					}

					// Avoid closely spaced duplicates
					if ((align < 1) || (millisecs > lastNote))
					{
						// Get label, if available
						if (parts.Length == 3)
						{
							duration = ParseMilliseconds(parts[1]);
							note = Int16.Parse(parts[2]);

							if (cboLabelsSpectrum.SelectedIndex == 1)
							{
								noteLabel = noteNames[note];
							}
							if (cboLabelsOnsets.SelectedIndex == 2)
							{
								noteLabel = note.ToString();
							}
						}

						xTranscription.Add(noteLabel, millisecs, millisecs + duration);
						lastNote = millisecs;
					}
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Note Onsets




		private int AddNoteOnsetTimings(int alignTo = ALIGNnone)
		{
			int onsetCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int lastNote = 0;

			xOnsets = new xTimings("Note Onsets");
			//timingsList[LISTnotes] = times;

			lastNote = onsets[0];
			//if (!reader.EndOfStream)
			for (int i=1; i< onsets.Length; i++)
			{
				millisecs = onsets[i];
				// Add millisecond value to the timing times
				//TODO (maybe) Align with quarter beats (?)
				//TODO (maybe) Get note name or midi number from Polyphonic Transcription (?)
				xOnsets.Add("", lastNote, millisecs);
				onsetCount++;
				lastNote = millisecs;
			} // end line contains a period
	
			
			return onsetCount;
		} // end Note Onsets

		private int ReadKeyData(string keyFile)
		{
			int onsetCount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int nextMS = 0;
			int align = SetAlignmentHost(cboAlignPitch.Text);
			string noteLabel = "";
			int note = 0;
			int lastNote = -1;
			string[] parts = null;

			StreamReader reader = new StreamReader(keyFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			xKey = new xTimings(cboMethodPitchKey.Text);

			reader = new StreamReader(keyFile);

			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{
					parts = lineIn.Split(',');
					millisecs = ParseMilliseconds(parts[0]);
					if (align > ALIGNnone)
					{
						if ((align == ALIGN25) || (align == ALIGN50))
						{
							millisecs = AlignStartTo(millisecs, align);
						}
						else
						{
							if ((align == ALIGNbar) ||
								(align == ALIGNbeatFull) ||
								(align == ALIGNbeatHalf) ||
								(align == ALIGNbeatThird) ||
								(align == ALIGNbeatQuarter) ||
								(align == ALIGNOnset))
							{
								millisecs = AlignStartTo(millisecs, align);
							}
						}
					}
				}
			}

			while (lineIn.Length > 0)
			{
				// Avoid closely spaced duplicates
				if ((align < 1) || (millisecs > lastNote))
				{
					// Get label, if available
					if (parts.Length == 3)
					{
						note = Int16.Parse(parts[1]);
						noteLabel = parts[2];
						if (cboLabelsPitchKey.SelectedIndex == 0)
						{
							noteLabel = "";
						}
						if (cboLabelsPitchKey.SelectedIndex == 1)
						{
							// Override the returned note name with a better formatted one
							noteLabel = keyNames[note];
						}
						if (cboLabelsPitchKey.SelectedIndex == 2)
						{
							noteLabel = note.ToString();
						}
						if (cboLabelsPitchKey.SelectedIndex == 3)
						{
							int n = note + 59;
							if (n > 71) n -= 12;
							noteLabel = n.ToString();
						}
						if (cboLabelsPitchKey.SelectedIndex == 4)
						{
							int n = note + 59;
							if (n > 71) n -= 12;
							noteLabel = noteFreqs[n] + "Hz";
						}
					}



					if (!reader.EndOfStream)
					{
						lineIn = reader.ReadLine();
						ppos = lineIn.IndexOf('.');
						if (ppos > xUtils.UNDEFINED)
						{
							parts = lineIn.Split(',');
							nextMS = ParseMilliseconds(parts[0]);
						}
						if (align > ALIGNnone)
						{
							if ((align == ALIGN25) || (align == ALIGN50))
							{
								nextMS = AlignStartTo(nextMS, align);
							}
							else
							{
								if ((align == ALIGNbar) ||
									(align == ALIGNbeatFull) ||
									(align == ALIGNbeatHalf) ||
									(align == ALIGNbeatThird) ||
									(align == ALIGNbeatQuarter) ||
									(align == ALIGNOnset))
								{
									nextMS = AlignStartTo(nextMS, align);
								}
							}
						}
					}
					else
					{
						nextMS = songTimeMS;
						lineIn = "";
					}







					xKey.Add(noteLabel, millisecs, nextMS);
					millisecs = nextMS;
				}
					// end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Key & Pitch

		private int ReadSegmentData(string segmentFile)
		{
			int onsetCount = 0;
			int lineCount = 0;
			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			int nextMS = 0;
			int align = SetAlignmentHost(cboAlignSegments.Text);
			string noteLabel = "";
			int note = 0;
			int lastNote = -1;
			string[] parts = null;

			StreamReader reader = new StreamReader(segmentFile);

			while (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				lineCount++;
			} // end while loop more lines remaining
			reader.Close();
			xSegments = new xTimings("Segments");

			reader = new StreamReader(segmentFile);

			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();
				ppos = lineIn.IndexOf('.');
				if (ppos > xUtils.UNDEFINED)
				{
					parts = lineIn.Split(',');
					millisecs = ParseMilliseconds(parts[0]);
					if (align > ALIGNnone)
					{
						if ((align == ALIGN25) || (align == ALIGN50))
						{
							millisecs = AlignStartTo(millisecs, align);
						}
						else
						{
							if ((align == ALIGNbar) ||
								(align == ALIGNbeatFull) ||
								(align == ALIGNbeatHalf) ||
								(align == ALIGNbeatThird) ||
								(align == ALIGNbeatQuarter) ||
								(align == ALIGNOnset))
							{
								millisecs = AlignStartTo(millisecs, align);
							}
						}
					}
				}
			}

			while (lineIn.Length > 0)
			{
				// Avoid closely spaced duplicates
				if ((align < 1) || (millisecs > lastNote))
				{
					// Get label, if available
					if (parts.Length == 4)
					{
						//note = Int16.Parse(parts[1]);
						if (cboLabelsSegments.SelectedIndex == 0)
						{
							noteLabel = "";
						}
						if (cboLabelsSegments.SelectedIndex == 1)
						{
							noteLabel = parts[2];
						}
						if (cboLabelsSegments.SelectedIndex == 2)
						{
							noteLabel = parts[3];
							noteLabel = noteLabel.Replace("\"", "");
						}
					}



					if (!reader.EndOfStream)
					{
						lineIn = reader.ReadLine();
						ppos = lineIn.IndexOf('.');
						if (ppos > xUtils.UNDEFINED)
						{
							parts = lineIn.Split(',');
							nextMS = ParseMilliseconds(parts[0]);
						}
						if (align > ALIGNnone)
						{
							if ((align == ALIGN25) || (align == ALIGN50))
							{
								nextMS = AlignStartTo(nextMS, align);
							}
							else
							{
								if ((align == ALIGNbar) ||
									(align == ALIGNbeatFull) ||
									(align == ALIGNbeatHalf) ||
									(align == ALIGNbeatThird) ||
									(align == ALIGNbeatQuarter) ||
									(align == ALIGNOnset))
								{
									nextMS = AlignStartTo(nextMS, align);
								}
							}
						}
					}
					else
					{
						nextMS = songTimeMS;
						lineIn = "";
					}







					xSegments.Add(noteLabel, millisecs, nextMS);
					millisecs = nextMS;
				}
				// end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			return onsetCount;
		} // end Key & Pitch


		private int AddKeyTimings(int alignTo = ALIGNnone)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int lastms = xUtils.UNDEFINED;
			string lastKey = "";
			//int note = 0;
			//xPitch = new xTimings("Key & Pitch");
			xEffect ef;


			int imax = 0; // keys.Length - 1;
			for (int i = 0; i < imax; i++)
			{
				//TODO (maybe) Align with bars, beats, or note onsets (?)
				//ef = new xEffect(keyNames[keys[i]], keyMS[i], keyMS[i + 1]);
				//xPitch.Add(ef);
				pcount++;
			}   // end while loop more lines remaining
			//ef = new xEffect(keyNames[keys[pcount]], keyMS[pcount], songTimeMS);
			//xPitch.Add(ef);


			return pcount;
		} // end Key & Pitch

		private int AddSegmentTimings(string resultsFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			xTimings ch;
			xEffect ef;

			// Pass 1, Get max values
			double[] dVals = new double[1024];
			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					//millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
					//Debug.Write(":");
					for (int n = 0; n < 1024; n++)
					{
						double d = Double.Parse(parts[n]);
						if (d > dVals[n]) dVals[n] = d;
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Pass 2, Convert those maxvals to a scale factor
			for (int n = 0; n < 1024; n++)
			{
				dVals[n] = 140 / dVals[n];
			}

			// Pass 3, convert to percents
			int lastcs = xUtils.UNDEFINED;
			double lastdt = 0;
			int lastix = 0;

			reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
					//Debug.Write(":");
					for (int n = 0; n < 128; n++)
					{
						double dt = 0;
						for (int m = 0; m < 8; m++)
						{
							int i = n * 8 + m + 1;
							double d = Double.Parse(parts[i]);
							d *= dVals[i];
							dt += d;
						}
						dt /= 8;
						int ix = (int)dt;
						if (ix < 20)
						{
							ix = 0;
						}
						else
						{
							if (ix > 120)
							{
								ix = 100;
							}
							else
							{
								ix -= 20;
							}
						}
						if (millisecs == lastcs)
						{
							ix += lastix;
							ix /= 2;
						}



						lastix = ix;
						lastdt = dt;
						lastcs = millisecs;
					}
				}
			} // end while loop more lines remaining
			reader.Close();
			return pcount;
		} // end Segment Timings

		private int AddChromaTimings(string chromaFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			xTimings ch = new xTimings("Chroma");
			xEffect ef;

			// Pass 1, Get max values
			double[] dVals = new double[12];
			StreamReader reader = new StreamReader(chromaFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 13)
				{
					pcount++;
					//millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
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

			reader = new StreamReader(chromaFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 13)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
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
							//ch = seq.xTimingss[firstCobjIdx + note];
							ch = noteTimings[note];
							//Identity id = seq.Members.bySavedIndex[noteTimings[note]];
							//if (id.PartType == MemberType.xTimings)
							//{
							//ch = (xTimings)id.owner;
							ef = new xEffect("", lastcs[note], millisecs);
							//ef.starttime = lastcs[note];
							//ef.endtime = millisecs;
							//ef.startIntensity = lastiVal[note];
							//ef.endIntensity = iVal;
							ch.Add(ef);
							lastcs[note] = millisecs;
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

			return pcount;
		} // end Chromagram

		private void AddChromaTimings_Other(string prefix)
		{
			string dmsg = "";
			//xTimings times;
			//int octave = 0;
			//int lastOctave = 0;
			Array.Resize(ref noteTimings, chromaNames.Length);
			for (int n = 0; n < chromaNames.Length; n++)
			{
				xTimings times = new xTimings(prefix + chromaNames[n]);
				noteTimings[n] = times;
				//grp.Add(times);
				//dmsg = "Adding xTimings '" + times.Name + "' SI:" + times.SavedIndex;
				//dmsg += " Note #" + n.ToString();
				//dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				//Debug.WriteLine(dmsg);
			}

		} // end Chromagram - Other

		private int AddTempoTimings(string TempoFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			xTimings ch;
			xEffect ef;

			xTimings timesOnsets = new xTimings(GRIDONSETS);

			StreamReader reader = new StreamReader(TempoFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					ontime = ParseMilliseconds(parts[1]);
					note = Int16.Parse(parts[2]);
					timesOnsets.Add(note.ToString(), millisecs, millisecs + ontime);
				}

			} // end while loop more lines remaining

			reader.Close();

			return pcount;
		} // End Tempo Timings

		private int ReadPolyData(string PolyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;


			StreamReader reader = new StreamReader(PolyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
					pcount++;
			} // end while loop more lines remaining

			reader.Close();
			//Array.Resize(ref polyNotes, pcount);
			//Array.Resize(ref polyMSstart, pcount);
			//Array.Resize(ref polyMSlen, pcount);
			pcount = 0; // reset for re-use

			reader = new StreamReader(PolyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					millisecs = ParseMilliseconds(parts[0]);
					//TODO (maybe) Align with note onsets and/or beats (?)
					ontime = ParseMilliseconds(parts[1]);
					note = Int16.Parse(parts[2]);
					//polyNotes[pcount] = note;
					//polyMSstart[pcount] = millisecs;
					//polyMSlen[pcount] = ontime;
					pcount++;
				}
			} // end while loop more lines remaining

			reader.Close();

			return pcount;
		} // end Polyphonic Transcription

		private int AddPolyTimings(int alignTo = ALIGNnone)
		{
			int pcount = 0;
			xTimings times = new xTimings("Polyphonic Transcription");
			//timingsList[LISTpoly] = times;Fsequence

			//for (int i=0; i< polyNotes.Length; i++)
			//{
				//times.Add(polyNotes[i].ToString(), polyMSstart[i], polyMSstart[i] + polyMSlen[i]);
				pcount++;
			//}

			return pcount;
		} // end Polyphonic Transcription

		private void AddPolyTimings_Other(string prefix)
		{
			string dmsg = "";
			//xTimings times;
			int octave = 0;
			int lastOctave = 0;

			Array.Resize(ref noteTimings, noteNames.Length);
			for (int n = 0; n < noteNames.Length; n++)
			{
				xTimings times = new xTimings(prefix + noteNames[n]);
				noteTimings[n] = times;

				//dmsg = "Adding xTimings '" + times.Name + "' SI:" + times.SavedIndex;
				//dmsg += " Note #" + n.ToString();
				//dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				//Debug.WriteLine(dmsg);

			}
		} // end Polyphonic Transcription - Other

		private int AddConstQTimings(string constQFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			xTimings ch;
			xEffect ef;

			//xTimings timesOnsets = new xTimings(GRIDONSETS);
			AddPolyTimings_Other("ConstQ ");

			// Pass 1, Get max values
			double[] dVals = new double[128];
			StreamReader reader = new StreamReader(constQFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 129)
				{
					pcount++;
					//millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
					//Debug.Write(":");
					for (int note = 0; note < 128; note++)
					{
						double d = Double.Parse(parts[note + 1]);
						if (d > dVals[note]) dVals[note] = d;
					}
				}
			} // end while loop more lines remaining
			reader.Close();

			// Pass 2, Convert those maxvals to a scale factor
			for (int n = 0; n < 128; n++)
			{
				dVals[n] = 160 / dVals[n];
			}

			// Pass 3, convert to percents
			int[] lastcs = new int[128];
			double lastdVal = 0;
			int[] lastiVal = new int[128];

			reader = new StreamReader(constQFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 129)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					//Debug.Write(millisecs);
					//Debug.Write(":");
					for (int note = 0; note < 128; note++)
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
							//ch = seq.xTimingss[firstCobjIdx + note];
							ch = noteTimings[note];
							//Identity id = seq.Members.bySavedIndex[noteTimings[note]];
							//if (id.PartType == MemberType.xTimings)
							//{
							//ch = (xTimings)id.owner;
							ef = new xEffect(note.ToString(), lastcs[note], millisecs);
							//ef.xEffectType = xEffectType.Intensity;
							//ef.startIntensity = lastiVal[note];
							//ef.endIntensity = iVal;
							lastcs[note] = millisecs;
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
			return pcount;
		} // end Constant Q Spectrogram

		private int AddFluxTimings(string fluxFile)
		{
			int pcount = 0;

			return pcount;
		} // end Flux Timings

		private int AddChordsTimings(string chordsFile)
		{
			int pcount = 0;

			return pcount;
		} // end Chords Timings

		private int AddVocalsTimings(string vocalsFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			xTimings ch;
			xEffect ef;

			int[] lastcs = new int[12];
			double lastdVal = 0;
			int[] lastiVal = new int[12];

			ef = new xEffect("", 0);
			//ef.starttime = 0;

			StreamReader reader = new StreamReader(vocalsFile);

			lineIn = reader.ReadLine();
			parts = lineIn.Split(',');
			if (parts.Length == 3)
			{
				pcount++;
				millisecs = ParseMilliseconds(parts[0]);
				double note = int.Parse(parts[1]);
				if (note > 12) note--;
				note--;

				ef = new xEffect(note.ToString(), millisecs);
				//ef.xEffectType = xEffectType.Intensity;
				//ef.Intensity = 100;
				//ef.starttime = millisecs;
				int nt = (int)Math.Round(note);
				noteTimings[nt].Add(ef);
			}

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					ef.endtime = millisecs;

					int note = int.Parse(parts[1]);
					if (note > 12) note--;
					note--;

					ef = new xEffect(note.ToString(), millisecs);
					//ef.xEffectType = xEffectType.Intensity;
					//ef.Intensity = 100;
					//ef.starttime = millisecs;
					noteTimings[note].Add(ef);
				}
			}   // end while loop more lines remaining

			//ef.endtime = noteTimings[0].Milliseconds;

			reader.Close();

			return pcount;
		} // end Vocals Timings

		private int AddMelodyTimings(string melodyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int millisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			xTimings ch;
			xEffect ef;

			int[] lastcs = new int[12];
			double lastdVal = 0;
			int[] lastiVal = new int[12];

			ef = new xEffect("", 0);
			//ef.starttime = 0;

			StreamReader reader = new StreamReader(melodyFile);

			lineIn = reader.ReadLine();
			parts = lineIn.Split(',');
			if (parts.Length == 3)
			{
				pcount++;
				millisecs = ParseMilliseconds(parts[0]);
				int note = int.Parse(parts[1]);
				if (note > 12) note--;
				note--;

				ef = new xEffect(note.ToString(), millisecs);
				//ef.xEffectType = xEffectType.Intensity;
				//ef.Intensity = 100;
				//ef.starttime = millisecs;
				noteTimings[note].Add(ef);
			}

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					millisecs = ParseMilliseconds(parts[0]);
					ef.endtime = millisecs;

					int note = int.Parse(parts[1]);
					if (note > 12) note--;
					note--;

					ef = new xEffect(note.ToString(), millisecs);
					//ef.xEffectType = xEffectType.Intensity;
					//ef.Intensity = 100;
					//ef.starttime = millisecs;
					noteTimings[note].Add(ef);
				}
			}   // end while loop more lines remaining

			//ef.endtime = noteTimings[0].

			reader.Close();
			return pcount;
		} // end Melody Timings
		#endregion

		private int GetAlignment(string alignmentName)
		{
			int align = ALIGNnone;
			string an = alignmentName.Trim();
			if (an.Length >= 4)
				{
				an = an.Substring(0, 4).ToLower();
				// in guestimated order of likelyhood
				if (an.CompareTo("none") == 0) align = ALIGNnone;
				else
				if (an.CompareTo("25ms") == 0) align = ALIGN25;
				else
				if (an.CompareTo("50ms") == 0) align = ALIGN50;
				else
				if (an.CompareTo("quar") == 0) align = ALIGNbeatQuarter;
				else
				if (an.CompareTo("bars") == 0) align = ALIGNbar;
				else
				if (an.CompareTo("note") == 0) align = ALIGNOnset;
				else
				if (an.CompareTo("full") == 0) align = ALIGNbeatFull;
				else
				if (an.CompareTo("half") == 0) align = ALIGNbeatHalf;
				else
				if (an.CompareTo("thir") == 0) align = ALIGNbeatThird;
				// else nothing, keep default of 'none'
			}
			return align;
		}

		private int SetAlignmentUsingText(ComboBox alignmentCombo, string textToMatch)
		{
			alignmentCombo.SelectedIndex = alignmentCombo.FindStringExact(textToMatch);
			return alignmentCombo.SelectedIndex;
		}


		private int AlignStartTo(int startTime, int alignTo = ALIGNnone)
		{
			int newStart = xUtils.UNDEFINED;
			int diff = 0;
			switch (alignTo)
			{
				case ALIGNnone:
					newStart = startTime;
					break;
				case ALIGN25:
					// 25ms or 40 fps
					newStart = RoundTimeTo(startTime, alignTo);
					break;
				case ALIGN50:
					// 50ms or 20 fps
					newStart = RoundTimeTo(startTime, alignTo);
					break;
				case ALIGNbar:
					newStart = FindNearestStartTime(startTime);
					break;
				case ALIGNbeatFull:
					newStart = FindNearestStartTime(startTime);
					break;
				case ALIGNbeatHalf:
					newStart = FindNearestStartTime(startTime);
					break;
				case ALIGNbeatQuarter:
					newStart = FindNearestStartTime(startTime);
					break;
				case ALIGNbeatThird:
					newStart = FindNearestStartTime(startTime);
					break;
				case ALIGNOnset:
					newStart = FindNearestStartTime(startTime);
					break;
			}
			return newStart;
		}

		private int FindNearestStartTime(int startTime)
		{
			int retTime = -1;
			int retIdx = FindNearestStartIndex(startTime);
			if (retIdx < 0)
			{
				retTime = 0;
			}
			else
			{
				if (retIdx >= xAlignTo.effects.Count)
				{
					retTime = songTimeMS;
				}
				else
				{
					retTime = xAlignTo.effects[retIdx].starttime;
				}
			}
			return retTime;
		}


		private int FindNearestStartIndex(int startTime)
		{
			//! Important: Reset lastFoundIdx to xUtils.UNDEFINED before starting new timings and/or different alignTimes.
			//! alignTimes are assumed to be in ascending numerical order.

			int retIdx = -1;
			int diffLo = 0;
			int diffHi = 0;
			bool keepTrying = true;
			int matchTime = -1;

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
			if (izwiz)
			{
				string msg = "Start time " + startTime.ToString() + " aligned to " + matchTime.ToString();
				Console.WriteLine(msg);
			}
			return retIdx;
		}

		private int FindDominantNote(int startTime, int endTime)
		{
			int domNote = xUtils.UNDEFINED;
			//TODO write this...

			return domNote;
		}

		private int FindDominantOctave(int startTime, int endTime)
		{
			int domOct = xUtils.UNDEFINED;
			//TODO write this...

			return domOct;
		}

		private int SetAlignmentHost(string alignmentName)
		{
			int al = GetAlignment(alignmentName);
			SetAlignmentHost(al);
			return al;
		}

		private void SetAlignmentHost(int alignTo)
		{
			switch (alignTo)
			{
				case ALIGNbar:
					xAlignTo = xBars;
					break;
				case ALIGNbeatFull:
					xAlignTo = xBeatsFull;
					break;
				case ALIGNbeatHalf:
					xAlignTo = xBeatsHalf;
					break;
				case ALIGNbeatThird:
					xAlignTo = xBeatsThird;
					break;
				case ALIGNbeatQuarter:
					xAlignTo = xBeatsQuarter;
					break;
				case ALIGNOnset:
					xAlignTo = xOnsets;
					break;
				default:
					string msg = "Invalid 'AlignTo";
					System.Diagnostics.Debugger.Break();
					break;
			}
			alignIdx = 0;
		}

		*/

	} // end partial class frmVamp
} // end namespace UtilORama4
