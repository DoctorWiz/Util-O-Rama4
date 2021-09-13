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
using LORUtils4; using FileHelper;
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
		private string annotatorProgram = "";

		private string StepSizeExample = "ERROR: BarBeatTracker::initialize: Unsupported step size for this sample rate: 441 (wanted 512)";
		//TODO: Scan output for StepSize Error, and correct it and re-run vamp if err detected
		private string StepSizeError = "ERROR: [Plugin]::initialize: Unsupported step size for this sample rate: [BadSize] (wanted [GoodSize])";



		//private static Annotator anno = null;
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

		private string resultsPolyphonic = "";
		private string resultsConstQ = "";
		private string resultsChroma = "";
		private string resultsSegments = "";
		private string resultsSpectrum = "";

		private string resultsPitchKey = "";
		private string resultsMelody = "";
		private string resultsVocals = "";
		private string resultsTempo = "";

		private Musik.AudioInfo audioData;
		string fileSongTemp = "song.mp3";
		//int beatsPerBar = 4;
		//int firstBeat = 1;
		//int stepSize = 512;
		//bool reuse = false;
		//bool whiten = false;
		
		private object syncGate = new object();
		private Process cmdProc;
		private StringBuilder outLog = new StringBuilder();
		private StreamWriter writer = null;
		private bool outputChanged;

		private int PrepareToAnnotate()
		{
			int errs = 0;

			//beatsPerBar = 4; // reset to default
			//if (swTrackBeat.Checked) beatsPerBar = 3;
			//firstBeat = 1; // reset to default
			//Int32.TryParse(txtStartBeat.Text, out firstBeat);
			//if (firstBeat < 1) firstBeat = 1;
			//if (firstBeat > beatsPerBar) firstBeat = beatsPerBar;

			//fileSongTemp = pathWork + "song.mp3";
			//errs = lutils.SafeCopy(fileAudioLast, fileSongTemp);
			//audioData.Filename = fileSongTemp;
			//audioData = ReadAudioFile(fileSongTemp);
			fileSongTemp = PrepAudioFile(fileAudioLast);


			//reuse = chkReuse.Checked;
			//Int32.TryParse(cboStepSize.Text, out stepSize);
			//if ((stepSize < 200) || (stepSize > 800)) stepSize = 512;
			//anno = new Annotator(annotatorProgram);
			//anno.songTimeMS

			return errs;
		}
		
		private int RunSelectedVamps()
		{
			int completed = 0;
			int err = 0;
			//vamps.AlignmentType algn = vamps.AlignmentType.None;
			//vamps.LabelTypes lbls = vamps.LabelTypes.None;
			bool reuse = chkReuse.Checked;
			Annotator.ReuseResults = chkReuse.Checked;
			Annotator.Whiten = chkWhiteBarBeats.Checked;
			Annotator.StepSize = Int32.Parse(cboStepSize.Text);
			if (swTrackBeat.Checked) Annotator.BeatsPerBar = 3; else Annotator.BeatsPerBar = 4;
			//Annotator.UseRamps = swRamps.Checked;
			Annotator.FirstBeat = Int32.Parse(txtStartBeat.Text);
			Annotator.WorkPath = pathWork;
			


			//!/////////////////
			//! BARS AND BEATS //
			//!/////////////////
			//if (chkBarsBeats.Checked)
			// ALWAYS run Bars and Beats since these timings are used for alignments
			if (true)
			{
				if ((!reuse) || (!System.IO.File.Exists(resultsBarBeats)))
				{
					int pluginIndex = cboMethodBarsBeats.SelectedIndex;
					int detectionMethod = cboDetectBarBeats.SelectedIndex;
					//whiten = chkWhiteBarBeats.Checked;

					err = VampBarBeats.PrepareVampConfig(pluginIndex, detectionMethod);
					if (err == 0)
					{
						string fileConfig = VampBarBeats.filesAvailableConfigs[pluginIndex];
						string vampParams = VampBarBeats.availablePluginCodes[pluginIndex];
						resultsBarBeats = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					}
				}
			}
			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();

			 
			//!////////////////
			//! NOTE ONSETS //
			//!/////////////
			if (chkNoteOnsets.Checked)
			{
				if ((!reuse) || (!System.IO.File.Exists(resultsNoteOnsets)))
				{
					int pluginIndex = cboOnsetsPlugin.SelectedIndex;
					int detectionMethod = cboOnsetsDetect.SelectedIndex;
					//whiten = chkOnsetsWhite.Checked;

					err = VampNoteOnsets.PrepareToVamp(fileSongTemp, pluginIndex, detectionMethod);
					if (err == 0)
					{
						string fileConfig = VampNoteOnsets.filesAvailableConfigs[pluginIndex];
						string vampParams = VampNoteOnsets.availablePluginCodes[pluginIndex];
						resultsNoteOnsets = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					}
				}
			}

			//!///////////////////////
			//! POLYPHONIC TRANSCRIPTION //
			//!/////////////////////
			if (chkPolyphonic.Checked)
			{
				if ((!reuse) || (!System.IO.File.Exists(resultsPolyphonic)))
				{
					int pluginIndex = cboMethodPolyphonic.SelectedIndex;
					//whiten = chkOnsetsWhite.Checked;

					err = VampPolyphonic.PrepareToVamp(fileSongTemp, pluginIndex);
					if (err == 0)
					{
						string fileConfig = VampPolyphonic.filesAvailableConfigs[pluginIndex];
						string vampParams = VampPolyphonic.availablePluginCodes[pluginIndex];
						resultsPolyphonic = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					}
				}
			}

			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();
			/*
			//!///////////////////////
			//! PITCH & KEY //
			//!/////////////////////
			if (chkPitchKey.Checked)
			{
				transPitchKey = new VampPitchKey();
				int pluginIndex = cboMethodPitchKey.SelectedIndex;
				//int detectionMethod = cboMethodPitchKey.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transPitchKey.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //,  reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transPitchKey transPitchKey.filesAvailableConfigs[pluginIndex];
					//string vampParams = transPitchKey.availablePluginCodes[pluginIndex];
					//resultsPitchKey = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsPitchKey.Length > 4)
					{
						algn = vamps.GetAlignmentTypeFromName(cboAlignPitchKey.Text);
						lbls = vamps.GetLabelTypeFromName(cboLabelsPitchKey.Text);
						transPitchKey.ResultsToxTimings(resultsPitchKey, algn, lbls); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}

			//!///////////////////////
			//!   TEMPO   //
			//!/////////////////////
			if (chkTempo.Checked)
			{
				transTempo = new VampTempo();
				int pluginIndex = cboMethodTempo.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transTempo.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transTempo.filesAvailableConfigs[pluginIndex];
					//string vampParams = transTempo.availablePluginCodes[pluginIndex];
					//resultsTempo = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsTempo.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignTempo.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transTempo.ResultsToxTimings(resultsTempo, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}


			//!///////////////////////
			//! CHROMAGRAM OR SPECTROGRAM
			//!/////////////////////
			if (chkSpectrum.Checked)
			{
				transSpectrum = new VampSpectrum();
				int pluginIndex = cboMethodSpectrum.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transSpectrum.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transSpectrum.filesAvailableConfigs[pluginIndex];
					//string vampParams = transSpectrum.availablePluginCodes[pluginIndex];
					//resultsSpectrum = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsSpectrum.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignSpectrum.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transSpectrum.ResultsToxTimings(resultsSpectrum, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}


			//!///////////////////////
			//!  SEGMENTS  ///
			//!/////////////////////
			if (chkSegments.Checked)
			{
				transSegments = new VampSegments();
				int pluginIndex = cboMethodSegments.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transSegments.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transSegments.filesAvailableConfigs[pluginIndex];
					//string vampParams = transSegments.availablePluginCodes[pluginIndex];
					//resultsSegments = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsSegments.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignPolyphonic.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transSegments.ResultsToxTimings(resultsSegments, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}
			*/




			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


			dirtyTimes = true;

			return completed;
		}


		private int ProcessSelectedVamps()
		{
			int completed = 0;
			int err = 0;
			vamps.AlignmentType algn = vamps.AlignmentType.None;
			vamps.LabelTypes lbls = vamps.LabelTypes.None;
			bool reuse = chkReuse.Checked;
			Annotator.ReuseResults = chkReuse.Checked;
			//Annotator.Whiten = chkWhiteBarBeats.Checked;
			//Annotator.StepSize = Int32.Parse(cboStepSize.Text);
			//if (swTrackBeat.Checked) Annotator.BeatsPerBar = 3; else Annotator.BeatsPerBar = 4;
			//Annotator.UseRamps = swRamps.Checked;
			//Annotator.FirstBeat = Int32.Parse(txtStartBeat.Text);
			//Annotator.WorkPath = pathWork;
			


			//!/////////////////
			//! BARS AND BEATS //
			//!/////////////////
			//if (chkBarsBeats.Checked)
			// ALWAYS annotate Bars and Beats since it is used for alignments
			if (true)
			{
				if (resultsBarBeats.Length < 4)
				{
					resultsBarBeats = pathWork + VampBarBeats.ResultsFile;
				}
				if (System.IO.File.Exists(resultsBarBeats))
				{
					algn = vamps.GetAlignmentTypeFromName(cboAlignBarsBeats.Text);
					// Note: Does all 5: Bars, Whole Beats, Half, Third, and Quarter
					VampBarBeats.ResultsToxTimings(resultsBarBeats, algn, vamps.LabelTypes.Numbers);
					completed++;
				}
				else
				{
					// No Results File!
					Fyle.BUG("Bar and Beat Tracker Failed, no results file!");
				}
			}
			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();

			 
			//!////////////////
			//! NOTE ONSETS //
			//!/////////////
			if ((chkNoteOnsets.Checked) || (Annotator.xOnsets.effects.Count < 1))
			{
				if (resultsNoteOnsets.Length > 4)
				{
					if (System.IO.File.Exists(resultsNoteOnsets))
					{
						algn = vamps.GetAlignmentTypeFromName(cboAlignOnsets.Text);
						lbls = vamps.GetLabelTypeFromName(cboOnsetsLabels.Text);
						VampNoteOnsets.ResultsToxTimings(resultsNoteOnsets, algn, lbls, VampNoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
					else
					{
						Fyle.BUG("Note Onsets Failed, no results file!");
					}
				}
			}

			//!///////////////////////
			//! POLYPHONIC TRANSCRIPTION //
			//!/////////////////////
			if ((chkPolyphonic.Checked) || (VampPolyphonic.Timings.effects.Count < 1))
			{
				if (System.IO.File.Exists(resultsPolyphonic))
				{
					algn = vamps.GetAlignmentTypeFromName(cboAlignPolyphonic.Text);
					lbls = vamps.GetLabelTypeFromName(cboLabelsPolyphonic.Text);
					VampPolyphonic.ResultsToxTimings(resultsPolyphonic, algn, lbls);
					completed++;
				}
				else
				{
					// No Results File!
					Fyle.BUG("Polyphonic Transcription failed, no results file!");
				}
			}

			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();
			/*
			//!///////////////////////
			//! PITCH & KEY //
			//!/////////////////////
			if (chkPitchKey.Checked)
			{
				transPitchKey = new VampPitchKey();
				int pluginIndex = cboMethodPitchKey.SelectedIndex;
				//int detectionMethod = cboMethodPitchKey.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transPitchKey.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //,  reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transPitchKey transPitchKey.filesAvailableConfigs[pluginIndex];
					//string vampParams = transPitchKey.availablePluginCodes[pluginIndex];
					//resultsPitchKey = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsPitchKey.Length > 4)
					{
						algn = vamps.GetAlignmentTypeFromName(cboAlignPitchKey.Text);
						lbls = vamps.GetLabelTypeFromName(cboLabelsPitchKey.Text);
						transPitchKey.ResultsToxTimings(resultsPitchKey, algn, lbls); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}

			//!///////////////////////
			//!   TEMPO   //
			//!/////////////////////
			if (chkTempo.Checked)
			{
				transTempo = new VampTempo();
				int pluginIndex = cboMethodTempo.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transTempo.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transTempo.filesAvailableConfigs[pluginIndex];
					//string vampParams = transTempo.availablePluginCodes[pluginIndex];
					//resultsTempo = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsTempo.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignTempo.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transTempo.ResultsToxTimings(resultsTempo, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}


			//!///////////////////////
			//! CHROMAGRAM OR SPECTROGRAM
			//!/////////////////////
			if (chkSpectrum.Checked)
			{
				transSpectrum = new VampSpectrum();
				int pluginIndex = cboMethodSpectrum.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transSpectrum.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transSpectrum.filesAvailableConfigs[pluginIndex];
					//string vampParams = transSpectrum.availablePluginCodes[pluginIndex];
					//resultsSpectrum = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsSpectrum.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignSpectrum.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transSpectrum.ResultsToxTimings(resultsSpectrum, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}


			//!///////////////////////
			//!  SEGMENTS  ///
			//!/////////////////////
			if (chkSegments.Checked)
			{
				transSegments = new VampSegments();
				int pluginIndex = cboMethodSegments.SelectedIndex;
				//int detectionMethod = cboMethodPolyphonic.SelectedIndex;
				whiten = chkOnsetsWhite.Checked;

				//resultsNoteOnsets = transOnsets.AnnotateSong(fileSongTemp, pluginIndex, beatsPerBar, stepSize, detectionMethod, reuse, whiten);
				//err = transOnsets.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize, reuse);
				err = transSegments.PrepareToVamp(fileSongTemp, pluginIndex, beatsPerBar, stepSize); //, reuse, whiten);
				if (err == 0)
				{
					//string fileConfig = transSegments.filesAvailableConfigs[pluginIndex];
					//string vampParams = transSegments.availablePluginCodes[pluginIndex];
					//resultsSegments = VampThatSong(fileSongTemp, vampParams, fileConfig, reuse);
					if (resultsSegments.Length > 4)
					{
						vamps.AlignmentType algn = vamps.GetAlignmentTypeFromName(cboAlignPolyphonic.Text);
						//TODO Get user choice of label type and detection method from combo boxes
						transSegments.ResultsToxTimings(resultsSegments, algn, vamps.LabelTypes.NoteNames); //, NoteOnsets.DetectionMethods.ComplexDomain);
						completed++;
					}
				}
			}
			*/




			lblVampTime.Text = FormatSongTime(Annotator.TotalCentiseconds); lblSongTime.Refresh();


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

			//! BARS AND BEATS
			//if (chkBars.Checked)
			//if (doBarsBeats)
			if (chkBarsBeats.Checked)
			{
				if (VampBarBeats.xBars != null)
				{
					if (VampBarBeats.xBars.effects.Count > 0)
					{
						if (!allInOne)
						{
							writer = BeginTimingsXFile(fileBaseName + " - " + VampBarBeats.xBars.Name + exten);
							writeCount++;
						}
						lineOut = xTimingsOutX(VampBarBeats.xBars, xTimings.LabelTypes.Numbers, allInOne);
						writer.WriteLine(lineOut);
					}
				}
				if (chkBeatsFull.Checked)
				{
					if (VampBarBeats.xBeatsFull != null)
					{
						if (VampBarBeats.xBeatsFull.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + VampBarBeats.xBeatsFull.Name + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(VampBarBeats.xBeatsFull, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsHalf.Checked)
				{
					if (VampBarBeats.xBeatsHalf != null)
					{
						if (VampBarBeats.xBeatsHalf.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + VampBarBeats.xBeatsHalf.Name + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(VampBarBeats.xBeatsHalf, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsThird.Checked)
				{
					if (VampBarBeats.xBeatsThird != null)
					{
						if (VampBarBeats.xBeatsThird.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + VampBarBeats.xBeatsThird.Name + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(VampBarBeats.xBeatsThird, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
				if (chkBeatsQuarter.Checked)
				{
					if (VampBarBeats.xBeatsQuarter != null)
					{
						if (VampBarBeats.xBeatsQuarter.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + VampBarBeats.xBeatsQuarter.Name + exten);
								writeCount++;
							}
							lineOut = xTimingsOutX(VampBarBeats.xBeatsQuarter, xTimings.LabelTypes.Numbers, allInOne);
							writer.WriteLine(lineOut);
						}
					}
				}
			}

			//! NOTE ONSETS
				if (chkNoteOnsets.Checked)
				{
					if (VampNoteOnsets.xOnsets != null)
					{
						if (VampNoteOnsets.xOnsets.effects.Count > 0)
						{
							if (!allInOne)
							{
								writer = BeginTimingsXFile(fileBaseName + " - " + VampNoteOnsets.xOnsets.Name + exten);
								writeCount++;
							}
							xTimings.LabelTypes lt = xTimings.LabelType(cboOnsetsLabels.Text);
							lineOut = xTimingsOutX(VampNoteOnsets.xOnsets, lt, allInOne);
							writer.WriteLine(lineOut);
							writeCount++;
						}
					}
				}

			//! POLYPHONIC TRANSCRIPTION
			if (chkPolyphonic.Checked)
			{
				if (VampPolyphonic.xPolyphonic != null)
				{
					if (VampPolyphonic.xPolyphonic.effects.Count > 0)
					{
						xTimings.LabelTypes lt = xTimings.LabelType(cboAlignPolyphonic.Text);
						lineOut = xTimingsOutX(VampPolyphonic.xPolyphonic, lt, allInOne);
						writer.WriteLine(lineOut);
						writeCount++;
					}
				}
			}



			/*
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

		private int ExportSelectedVampsToLOR()
		{
			int completed = 0;
			bool ramps = swRamps.Checked;
			Annotator.UseRamps = ramps;

			//Annotator.VampTrack = Annotator.Sequence.FindTrack("Vamp-O-Rama", true);

			//! BARS AND BEATS
			if (chkBarsBeats.Checked)
			{
				if (lightORamaInstalled && chkLOR.Checked)
				{
					VampBarBeats.xTimingsToLORtimings(); // Does all 5: Bars, Full Beats, Halves, Thirds, Quarters
					VampBarBeats.xTimingsToLORChannels(); // Does all 5
					if (resultsBarBeats.Length > 4) completed++;
				}
			}

			//! NOTE ONSETS
			if (chkNoteOnsets.Checked)
			{
				if (lightORamaInstalled && chkLOR.Checked)
				{
					if (VampNoteOnsets.xOnsets.effects.Count > 0)
					{
						VampNoteOnsets.xTimingsToLORtimings();
						VampNoteOnsets.xTimingsToLORChannels();
					}
				}
			}

			//! POLYPHONIC TRANSCRIPTION
			if (chkPolyphonic.Checked)
			{
				if (lightORamaInstalled && chkLOR.Checked)
				{
					if (VampPolyphonic.xPolyphonic.effects.Count > 0)
					{
						VampPolyphonic.xTimingsToLORtimings();
						VampPolyphonic.xTimingsToLORChannels();
					}
					if (resultsPolyphonic.Length > 4) completed++;
				}
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
			bool needCopy = false;


			originalPath = Path.GetDirectoryName(originalAudioFile);
			originalFileName = Path.GetFileNameWithoutExtension(originalAudioFile);
			originalExt = Path.GetExtension(originalAudioFile);

			newFile = pathWork + "song" + originalExt;
			
			// Do we need to copy or re-copy the original song file to the temp folder?
			// Is reuse turned off?  Does it exist?  Is it the same size and date?
			if (!chkReuse.Checked) needCopy = true;
			if (!System.IO.File.Exists(newFile))
			{
				needCopy = true;
			}
			else
			{
				FileInfo fio = new FileInfo(originalAudioFile);
				FileInfo fin = new FileInfo(newFile);
				if (fio.Length != fin.Length) needCopy = true;
				if (fio.LastWriteTime != fin.LastWriteTime) needCopy = true; ;
			}

			if (needCopy)
			{
				// Copy original audio file to temp folder if necessary
				err = Fyle.SafeCopy(originalAudioFile, newFile, true);
				if (err > 0)
				{
					string msg = "SafeCopy of the Audio File [" + originalAudioFile;
					msg += "]\r\nTo the Temp Folder [" + pathWork;
					msg += "]\r\nFailed with Error " + err.ToString();
					Fyle.BUG(msg);
					newFile = "";
				}
			}
			if (err == 0)
			{
				// if no errors so far
				audioData = ReadAudioFile(newFile);
				string songInfo = audioData.Title + " by " + audioData.Artist;
				this.Text = myTitle + " - " + songInfo;
				if (seq != null)
				{
					if (seq.Centiseconds < audioData.Centiseconds) seq.Centiseconds = audioData.Centiseconds;
				}
				//lblSeqTime.Text = FormatSongTime(seq.Centiseconds * 10);
				fileAudioLast = originalAudioFile;
				fileAudioWork = newFile;
			}

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
			ret.Append(timings.Name);
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
							//label = SequenceFunctions.noteNamesUnicode[timings.effects[i].Midi];
							label = MusicalNotation.noteNamesUnicode[effect.Midi];
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
							label = MusicalNotation.keyNamesUnicode[effect.Midi];
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
							label = MusicalNotation.noteFreqs[effect.Midi];
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
			//string pathWork = Path.GetDirectoryName(fileSong) + "\\";
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
					if (debugMode) Clipboard.SetText(emsg);
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
					if ((!reuse) || (!System.IO.File.Exists(resultsFile)))
					//if (true) 
					{
						lock (syncGate)
						{
							if (cmdProc != null) return "";
						}

						string runthis = annotatorProgram + " " + annotatorArguments;
						runthis = "/c " + runthis; // + " 2>output.txt";

						string vampCommandLast = runthis;
						if (debugMode) Clipboard.SetText(runthis);

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
						Fyle.BUG("Vamp-That-Song Failed, no results file!");
					}
				}
			}
			catch (Exception e)
			{
				if (debugMode)
				{
					string msg = e.Message;
					Fyle.MakeNoise(Fyle.Noises.Crash);
					System.Diagnostics.Debugger.Break();
				}
				resultsFile = "";
			}

			return resultsFile;
		}



		private void ProcessVampError(object sender, DataReceivedEventArgs drea)
		{
			int err = 0;
			lock (syncGate)
			{
				if (sender != cmdProc)
				{
					err = 99999;
				}
				else
				{
					string lineOut = drea.Data;
					if (lineOut != null)
					{
						if (lineOut.Length > 4)
						{
							if (lineOut.Substring(0, 5).ToLower() == "error")
							{
								string lo = lineOut.ToLower();
								//TODO Process this and look for step size error, return the DESIRED step size
								//err = (Desired Step Size)
							}
						}
						outLog.AppendLine(lineOut);
					}
					if (outputChanged)
					{ }
					else
					{
						outputChanged = true;
						BeginInvoke(new Action(OnOutputChanged));
					}
				}
			}
			return; // err;
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
