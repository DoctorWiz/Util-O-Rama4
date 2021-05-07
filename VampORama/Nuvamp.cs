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


		private Annotator anno = null;
		private BarBeats transBarBeats = null;
		private NoteOnsets transOnsets = null;

		private string resultsBarBeats = "";
		private string resultsqmBeats = "";
		private string resultsBeatRoot = "";
		private string resultsPortoBeat = "";
		private string resultsAubioBeat = "";

		private string resultsNoteOnset = "";
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
			anno = new Annotator(annotatorProgram);

			return errs;
		}
		
		private int AnnotateSelectedVamps()
		{
			int completed = 0;
			
			// BARS AND BEATS
			if (chkBarsBeats.Checked)
			{
				transBarBeats = new BarBeats(anno);
				int pluginIndex = cboMethodBarsBeats.SelectedIndex;
				int detectionMethod = cboDetectBarBeats.SelectedIndex;
				whiten = chkWhiteBarBeats.Checked;

				resultsBarBeats = transBarBeats.AnnotateSong (fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				if (resultsBarBeats.Length > 4)
				{
					vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignBarsBeats.Text);
					transBarBeats.ResultsToxTimings(resultsBarBeats, algn, vamps.LabelTypes.Numbers);
					completed++;
				}
			}

			// NOTE ONSETS
			if (chkNoteOnsets.Checked)
			{
				transOnsets = new NoteOnsets(anno);
				int pluginIndex = cboMethodOnsets.SelectedIndex;
				int detectionMethod = cboDetectOnsets.SelectedIndex;
				whiten = chkWhiteOnsets.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				if (resultsBarBeats.Length > 4) completed++;
			}

			dirtyTimes = true;

			return completed;
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
			newFile = pathWork + "song" + originalExt;
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


	}
}
