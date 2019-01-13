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
using Microsoft.Win32;
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

namespace TuneORama
{
	public partial class frmTune : Form
	{
		//! For testing, set this to false
		//! causes prog NOT to regenerate vamp files on every run and use whatever already exists in the temp folder
		const bool overwriteExistingVamps = true;
		//! for normal operation, set to true

		const string VAMPnoteOnset = "vamp:qm-vamp-plugins:qm-onsetdetector:onsets";
		const string VAMPbars = "vamp:qm-vamp-plugins:qm-barbeattracker:bars";
		const string VAMPbeats = "vamp:qm-vamp-plugins:qm-tempotracker:beats";
		const string VAMPpoly = "vamp:qm-vamp-plugins:qm-transcription:transcription";
		const string VAMPconstq = "vamp:qm-vamp-plugins:qm-constantq:constantq";
		const string VAMPchroma = "vamp:qm-vamp-plugins:qm-chromagram:chromagram";
		const string VAMPsegments = "vamp:qm-vamp-plugins:qm-segmenter:segmentation";
		const string VAMPspectro = "vamp:qm-vamp-plugins:qm-adaptivespectrogram";
		const string VAMPbeats1 = "vamp:qm-vamp-plugins:qm-barbeattracker";
		const string VAMPkey = "vamp:qm-vamp-plugins:qm-keydetector:key";
		const string VAMPmelody = "vamp:mtg-melodia:melodia:melody";
		const string VAMPspeech = "vamp:bbc-vamp-plugins:bbc-speechmusic-segmenter:segmentation";

		const string FILEnoteOnset = "qm-onsetdetector.n3";
		const string FILEbars = "qm-barbeattracker.n3";
		const string FILEbeats = "qm-tempotracker.n3";
		const string FILEpoly = "qm-transcription.n3";
		const string FILEconstq = "qm-constantq.n3";
		const string FILEchroma = "qm-chromagram.n3";
		const string FILEsegments = "qm-segmenter.n3";
		const string FILEspectro = "qm-adaptivespectrogram.n3";
		const string FILEkey = "qm-keydetector.n3";
		const string FILEmelody = "mtg-melodia.n3";
		const string FILEspeech = "bbc-speech.n3";

		private const string GRIDBEATS = "Beats";
		private const string GRIDONSETS = "Note Onsets";
		//private const string MASTERTRACK = "Song Information [Tune-O-Rama]";
		private const string MASTERTRACK = "Beats + Song Information [Tune-O-Rama]";
		private const string MASTERMATCH = "Beats + Song ";
		private const string GROUPBEATS = "Bars and Beats";
		private const string GROUPPOLY = "Polyphonic Transcription";
		private const string GROUPCONSTQ = "Constant Q Spectrogram";
		private const string GROUPSEGMENTS = "Segments";
		private const string GROUPSPECTRO = "Spectrogram";
		private const string GROUPCHROMA = "Chromagram";
		private const string GROUPKEY = "Key";
		private const string GROUPMELODY = "Melody";
		private const string GROUPSPEECH = "Speech";
		private const string CHANbeats = "Beats";

		private const string ALGOqm = " (Queen Mary)";
		private const string ALGOaubio = " (Aubio)";
		private const string ALGObbc = " (BBC)";

		string[] noteNames = {"C0","C#0-Db0","D0","D#0-Eb0","E0","F0","F#0-Gb0","G0","G#0-Ab0","A0","A#0-Bb0","B0",
													"C1","C#1-Db1","D1","D#1-Eb1","E1","F1","F#1-Gb1","G1","G#1-Ab1","A1","A#1-Bb1","B1",
													"C2","C#2-Db2","D2","D#2-Eb2","E2","F2","F#2-Gb2","Low_G","Low_G#-Ab","Low_A","Low_A#-Bb","Low_B",
													"Low_C","Low_C#-Db","Low_D","Low_D#-Eb","Low_E","Low_F","Low_F#-Gb","Bass_G","Bass_G#-Ab","Bass_A","Bass_A#-Bb","Bass_B",
													"Bass_C","Bass_C#-Db","Bass_D","Bass_D#-Eb","Bass_E","Bass_F","Bass_F#-Gb","Middle_G","Middle_G#-Ab","Middle_A","Middle_A#-Bb","Middle_B",
													"Middle_C","Middle_C#-Db","Middle_D","Middle_D#-Eb","Middle_E","Middle_F","Treble_F#-Gb","Treble_G","Treble_G#-Ab","Treble_A","Treble_A#-Bb","Treble_B",
													"Treble_C","Treble_C#-Db","Treble_D","Treble_D#-Eb","Treble_E","Treble_F","High_F#-Gb","High_G","High_G#-Ab","High_A","High_A#-Bb","High_B",
													"High_C","High_C#-Db","High_D","High_D#-Eb","High_E","High_F","F#7-Gb7","G7","G#7-Ab7","A7","A#7-Bb7","B7",
													"C8","C#8-Db8","D8","D#8-Eb8","E8","F8","F#8-Gb8","G8","G#8-Ab8","A8","A#8-Bb8","B8",
													"C9","C#9-Db9","D9","D#9-Eb9","E9","F9","F#9-Gb9","G9","G#9-Ab9","A9","A#9-Bb9","B9",
													"C10","C#10-Db10","D10","D#10-Eb10","E10","F10","F#10-Gb10","G10"};

		string[] octaveNamesA = { "CCCCCC 128'", "CCCCC 64'", "CCCC 32'", "CCC 16'", "CC 8'", "C4'", "c1 2'", "c2 1'", "c3 1/2'", "c4 1/4'", "c5 1/8'", "c6 1/16'", "Err", "Err" };
		string[] octaveNamesB = { "Sub-Sub-Sub Contra", "Sub-Sub Contra", "Sub-Contra", "Contra", "Great", "Small", "1-Line", "2-Line", "3-Line", "4-Line", "5-Line", "6-Line", "Err", "Err" };

		string[] chromaNames = { "C", "C#-Db", "D", "D#-Eb", "E", "F", "F#-Gb", "G", "G#-Ab", "A", "A#-Bb", "B" };




		private string resultsNoteOnset = "";
		private string resultsBars = "";
		private string resultsBeats = "";
		private string resultsPoly = "";
		private string resultsConstQ = "";
		private string resultsChroma = "";
		private string resultsSegments = "";
		private string resultsSpectro = "";
		private string resultsKey = "";
		private string resultsMelody = "";
		private string resultsSpeech = "";



		const string WRITEformat = " -f -w csv --csv-force ";

		private string fileAudioOriginal = "";
		private string fileAudioWork = "";
		//private string fileResults = "";
		//private string preppedFileName = "";
		//private string prepFile = "";
		//private string preppedPath = "";
		//private string preppedExt = "";
		//private string preppedAudio = "";
		private string annotatorProgram = "";
		private string pluginsPath = "\"C:\\Program Files (x86)\\Vamp Plugins\\\"";
		private Track tuneTrack = null;
		private TimingGrid beatsGrid = null;
		private Musik.AudioInfo audioData;
		

		private void AnalyzeSong(string audioFileName)
		{
			ImBusy(true);
			pnlVamping.Visible = true;

			bool needsFix = false;
			//string resultsFile = "";


			// Clean up temp folder from previous run
			//! REMARKED OUT FOR TESTING DEBUGGING, LEAVE FILES
			//ClearLastRun();
			//! UNREMARK AFTER TESTING!

			txtFileAudio.Text = Path.GetFileName(audioFileName);
			string preppedAudio = PrepAudioFile(audioFileName);
			string theSong = audioData.Title;
			if (audioData.Artist.Length > 1)
			{
				theSong += " by " + audioData.Artist;
			}
			else
			{
				if (audioData.AlbumArtist.Length > 1)
				{
					theSong += " by " + audioData.AlbumArtist;
				}
			}
			ShowVamping(theSong);


			if (doBeatChannels || doChroma || doConstQ || doPoly || doSegments || doKey || doSpeech)
			{
				tuneTrack = GetMasterTrack();
			}





			// Note Onsets Grid
			if (doNoteOnsets)
			{
				resultsNoteOnset = AnnotateSong(VAMPnoteOnset, FILEnoteOnset);
				if (resultsNoteOnset.Length > 4)
				{
					AddNoteOnsetsGrid(resultsNoteOnset);
				}
				//TODO:
				//resultsNoteOnset = AnnotateSong(VAMPpercussionOnset, FILEpercussionOnset);
				//if (resultsPercussionOnset.Length > 4)
				//{
					//TODO:
				//	AddNotePercussionOnsetsGrid(resultsPercussionOnset);
				//}
			}

			// Bars and Beats

			if (doBeatGrid || doBeatChannels)
			{
				//TODO:
				//resultsBars = AnnotateSong(VAMPbars, FILEbars);
				if (resultsBars.Length > 4)
				{
					if (doBeatGrid)
					{
						//TODO:
						//AddBarsGrids(resultsBars);
					}
					if (doBeatChannels)
					{
						//TODO:
						//AddBarsChannels(resultsBars);
					}
				}
				resultsBeats = AnnotateSong(VAMPbeats, FILEbeats);
				if (resultsBeats.Length > 4)
				{
					if (doBeatGrid)
					{
						AddBeatsGrids(resultsBeats);
					}
					if (doBeatChannels)
					{
						AddBeatChannels(resultsBeats);
					}
				}
			}

			// If we did not create any grids above,
			// and if not starting from a sequence which already had grids
			if (seq.TimingGrids.Count < 1)
			{
				// then we need to create at least one basic no-frills grid
				MakeDumbGrid();
			}

			// Polyphonic Transcription
			if (doPoly)
			{
				resultsPoly = AnnotateSong(VAMPpoly, FILEpoly);
				if (resultsPoly.Length > 4)
				{
					AddPolyChannels(resultsPoly);
				}
			}

			// Constant Q Spectrogram
			if (doConstQ)
			{
				resultsConstQ = AnnotateSong(VAMPconstq, FILEconstq);
				if (resultsConstQ.Length > 4)
				{
					AddConstQChannels(resultsConstQ);
				}

			}

			// Chromagram
			if (doChroma)
			{
				resultsChroma = AnnotateSong(VAMPchroma, FILEchroma);
				if (resultsChroma.Length > 4)
				{
					AddChromaChannels(resultsChroma);
				}
			}

			// Song Segments
			if (doSegments)
			{
				//TODO: does not provide meaningful results.  Can the Vamp Plugin be tweaked?
				//resultsSegments = AnnotateSong(VAMPsegments, FILEsegments);
				if (resultsSegments.Length > 4)
				{
					//AddSegmentChannels(resultsSegments);
				}
			}

			// Key, Pitch, Melody
			if (doKey)
			{
				resultsKey = AnnotateSong(VAMPkey, FILEkey);
				if (resultsKey.Length > 4)
				{
					AddKeyChannels(resultsKey);
				}
				//TODO:
				//resultsPitch = AnnotateSong(VAMPpitch, FILEpitch);
				//if (resultsPitch.Length > 4)
				//{
					//AddMelodyChannels(resultsMelody);
				//}
				//TODO: does not provide meaningful results.  Can the Vamp Plugin be tweaked?
				//resultsMelody = AnnotateSong(VAMPmelody, FILEmelody);
				if (resultsMelody.Length > 4)
				{
					//AddMelodyChannels(resultsMelody);
				}
			}

			// Speech / Singing
			//TODO: does not provide meaningful results.  Can the Vamp Plugin be tweaked?
			if (doSpeech)
			{
				//resultsSpeech = AnnotateSong(VAMPspeech, FILEspeech);
				if (resultsSpeech.Length > 4)
				{
					//AddSpeechChannels(resultsSpeech);
				}
			}


			//ClearLastRun(); // Not yet, keep 'em for diagnostics


			//MessageBox.Show(seq.summary());

			fileAudioOriginal = audioFileName;
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
					autoSeqPath = utils.DefaultSequencesPath;
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




			btnSaveSequence.Enabled = true;
			fileAudioOriginal = audioFileName;
			ShowVamping("");
			ImBusy(false);
		}

		private string PrepAudioFile(string originalAudioFile)
		{
			// Input: A fully qualified path and filename to a audio file
			// Example: D:\Light-O-Rama\Audio\Wizards in Winter by TSO.mp3
			// Output: The Name of the COPY of the audio file that is to be annotated, in annotator's required format
			// Example: c:/users/johndoe/appdata/utilorama/tuneorama/wizardsinwinter.mp3

			seq.sequenceType = SequenceType.Musical;
			seq.info.sequenceType = SequenceType.Musical;
			audioData = ReadAudioFile(originalAudioFile);
			if (seq.info.music.Title == "") seq.info.music.Title = audioData.Title;
			if (seq.info.music.Artist == "") seq.info.music.Artist = audioData.Artist;
			if (seq.info.music.Album == "") seq.info.music.Album = audioData.Album;
			// Save filename only if in LOR default audio path, save full path+filename if elsewhere
			if (seq.info.music.File == "")
			{
				string pf = Path.GetDirectoryName(originalAudioFile).ToLower();
				string pd = utils.DefaultAudioPath.ToLower();
				if (pf.CompareTo(pd) == 0)
				{
					seq.info.music.File = Path.GetFileName(originalAudioFile);
				}
				else
				{
					seq.info.music.File = originalAudioFile;
				}
			}
			TimeSpan audioTime = audioData.Duration;
			int cs = audioTime.Minutes * 6000;
			cs += audioTime.Seconds * 100;
			cs += audioTime.Milliseconds / 10;
			centiseconds = cs;
			seq.Centiseconds = cs;

			string sex = Path.GetExtension(fileChanCfg).ToLower();
			if (sex == ".lcc")
			{
				CentiFixx();
			}

			// Fill in [Sequence] Author information, who created the sequence (not the audio artist)
			if (seq.info.author == "")
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Editor\\NewSequence";
				string author = (string)Registry.GetValue(keyName, "Author", "");
				if (author.Length < 2) author = applicationName;
				seq.info.author = author;
			}
			//Channel chan = new Channel("Beat-O-Rama");
			//chan.Centiseconds = seq.totalCentiseconds;
			//chan.Selected = true;
			//seq.AddChannel(chan);
			//Track tuneTrack = new Track(applicationName);
			//tuneTrack.itemSavedIndexes.Add(chan.SavedIndex);
			//tuneTrack.totalCentiseconds = seq.totalCentiseconds;
			//tuneTrack.Selected = true;
			//seq.AddTrack(tuneTrack);



			originalPath = Path.GetDirectoryName(originalAudioFile);
			originalFileName = Path.GetFileNameWithoutExtension(originalAudioFile);
			originalExt = Path.GetExtension(originalAudioFile);

			//string preppedFileName = PrepName(originalFileName);
			newFile = tempPath + "song" + originalExt;
			//fileAudioWork = newFile;
			//fileResults = tempPath + preppedFileName;

			if (overwriteExistingVamps || !System.IO.File.Exists(newFile))
			{
				System.IO.File.Copy(originalAudioFile, newFile, true);
			}


			//string preppedPath = PrepPath(tempPath);
			//string preppedExt = originalExt.ToLower();

			//string prepFile = preppedPath + preppedFileName + preppedExt;
			fileAudioOriginal = originalAudioFile;
			fileAudioWork = newFile;
			return newFile;

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

		public string AnnotateSong(string Vamparams, string configFile)
		{
			string resultsFile = "";

			const string ERRproc = " in RunConstQ(";
			const string ERRtuneTrack = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";
			int err = 0;



			string ex = Path.GetExtension(fileAudioOriginal).ToLower();
			string cmdStr = "-t " + configFile;
			cmdStr += " " + fileAudioWork; // + ex;
			cmdStr += WRITEformat;

			string configs = tempPath + configFile;
			if (!System.IO.File.Exists(configs))
			{
				string homedir = AppDomain.CurrentDomain.BaseDirectory;
				string srcconfig = homedir + configFile;
				System.IO.File.Copy(srcconfig, configs);
			}




			try
			{
				string emsg = annotatorProgram + " " + cmdStr;
				Console.WriteLine(emsg);
				//DialogResult dr = MessageBox.Show(this, emsg, "About to launch", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
				DialogResult dr = DialogResult.Yes;

				if (dr == DialogResult.Yes)
				{
					Clipboard.SetText(emsg);
				}
				if (dr != DialogResult.Cancel)
				{
					string vampback = Vamparams.Replace(':', '_');
					resultsFile = tempPath + "song_"; // Path.GetFileNameWithoutExtension(fileAudioWork);
					resultsFile += vampback + ".csv";
					//! FOR TESTING DEBUGGING, set overwriteExistingVamps to false
					if (overwriteExistingVamps || !System.IO.File.Exists(resultsFile))
					{  
						ProcessStartInfo annotator = new ProcessStartInfo(annotatorProgram);
						annotator.Arguments = cmdStr;
						annotator.WorkingDirectory = tempPath;
						Process cmd = Process.Start(annotator);
						cmd.WaitForExit(90000);  // 30 second timeout
						int x = cmd.ExitCode;
					}

					if (System.IO.File.Exists(resultsFile))
					{
						return resultsFile;
					}
				}
			}
			catch
			{
			}

			return "";
		}

		private int AddBeatsGrids(string resultsFile)
		{
			int err = 0;
			int onsetCount = 0;
			string lineIn = "";
			int lastBeat = 0;
			int beatLength = 0;
			int ppos = 0;
			int centisecs = 0;

			//TimingGrid grid = new TimingGrid("Note Onsets");
			TimingGrid grid1 = GetGrid(GRIDBEATS);
			TimingGrid grid2 = GetGrid("Half " + GRIDBEATS);
			TimingGrid grid3 = GetGrid("Third " + GRIDBEATS);
			TimingGrid grid4 = GetGrid("Quarter " + GRIDBEATS);
			grid1.TimingGridType = TimingGridType.Freeform;
			grid1.Clear();
			grid1.AddTiming(0); // Needs a timing of zero at the beginning
			grid1.Selected = true;
			grid2.TimingGridType = TimingGridType.Freeform;
			grid2.Clear();
			grid2.AddTiming(0);
			grid2.Selected = true;
			grid3.TimingGridType = TimingGridType.Freeform;
			grid3.Clear();
			grid3.AddTiming(0);
			grid3.Selected = true;
			grid4.TimingGridType = TimingGridType.Freeform;
			grid4.Clear();
			grid4.AddTiming(0); // Needs a timing of zero at the beginning
			grid4.Selected = true;


			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				ppos = lineIn.IndexOf('.');
				if (ppos > utils.UNDEFINED)
				{

					string[] parts = lineIn.Split(',');

					centisecs = ParseCentiseconds(parts[0]);
					beatLength = centisecs - lastBeat;
					// Add centisecond value to the timing grid
					grid1.AddTiming(centisecs);

					grid2.AddTiming(lastBeat + (beatLength / 2));
					grid2.AddTiming(centisecs);

					grid3.AddTiming(lastBeat + (beatLength / 3));
					grid3.AddTiming(lastBeat + (beatLength * 2 / 3));
					grid3.AddTiming(centisecs);

					grid4.AddTiming(lastBeat + (beatLength / 4));
					grid4.AddTiming(lastBeat + (beatLength / 2));
					grid4.AddTiming(lastBeat + (beatLength * 3 / 4));
					grid4.AddTiming(centisecs);



					lastBeat = centisecs;
					onsetCount++;
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			beatLength = grid1.Centiseconds - lastBeat-1;
			// Divy up the final remaining beat
			if (beatLength > 3)
			{
				grid2.AddTiming(lastBeat + (beatLength / 2));
			}
			if (beatLength > 5)
			{
				grid3.AddTiming(lastBeat + (beatLength / 3));
				grid3.AddTiming(lastBeat + (beatLength * 2 / 3));
			}
			if (beatLength > 7)
			{
				grid4.AddTiming(lastBeat + (beatLength / 4));
				grid4.AddTiming(lastBeat + (beatLength / 2));
				grid4.AddTiming(lastBeat + (beatLength * 3 / 4));
			}

			beatsGrid = grid1;
			return err;
		}

		private int AddNoteOnsetsGrid(string noteOnsetFile)
		{
			int onsetCount = 0;
			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;

			//TimingGrid grid = new TimingGrid("Note Onsets");
			TimingGrid grid = GetGrid(GRIDONSETS);
			grid.TimingGridType = TimingGridType.Freeform;
			//grid.type = timingGridType.freeform;
			grid.AddTiming(0); // Needs a timing of zero at the beginning
			grid.Selected = true;

			StreamReader reader = new StreamReader(noteOnsetFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				ppos = lineIn.IndexOf('.');
				if (ppos > utils.UNDEFINED)
				{
					centisecs = ParseCentiseconds(lineIn);
					// Add centisecond value to the timing grid
					grid.AddTiming(centisecs);
					onsetCount++;
				} // end line contains a period
			} // end while loop more lines remaining

			reader.Close();

			//seq.TimingGrids.Add(grid);
			//seq.AddTimingGrid(grid);
			//Track tuneTrack = seq.FindTrack(applicationName);
			//tuneTrack.timingGridObjIndex = seq.TimingGrids.Count - 1;
			//tuneTrack.timingGridObjIndex = grid.identity.SavedIndex;
			//tuneTrack.totalCentiseconds = seq.totalCentiseconds;

			return onsetCount;
		} // end Note Onsets to Timing Grid

		private int AddBeatChannels(string resultsFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int lastBeat = 0;
			int lastTempo = 0;
			bool toggled = false;
			int beatLength = 0;
			double divd = 0;
			double tempo = 0;
			int divi8 = 0;
			int divi4 = 0;
			int divi3 = 0;
			int divi2 = 0;
			int divi0 = 0;
			


			Channel chbx2;
			Channel chb;
			Channel chbd2;
			Channel chbd3;
			Channel chbd4;
			Channel chbd8;
			Channel cht;
			Effect ef;

			//Track tuneTrack = new Track("Spectrogram");
			//Track tuneTrack = GetTrack(MASTERTRACK);
			tuneTrack.Selected = true;
			//tuneTrack.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(utils.XMLifyName(GROUPBEATS), tuneTrack);
			grp.Selected = true;
			chbx2 = GetChannel(CHANbeats + " x 2",grp);
			chbx2.color = NoteColor(10);
			chbx2.Selected = true;
			chb = GetChannel(CHANbeats, grp);
			chb.color = NoteColor(0);
			chb.Selected = true;
			chbd2 = GetChannel(CHANbeats + " / 2", grp);
			chbd2.color = NoteColor(2);
			chbd2.Selected = true;
			chbd3 = GetChannel(CHANbeats + " / 3", grp);
			chbd3.color = NoteColor(4);
			chbd3.Selected = true;
			chbd4 = GetChannel(CHANbeats + " / 4", grp);
			chbd4.color = NoteColor(6);
			chbd4.Selected = true;
			chbd8 = GetChannel(CHANbeats + " / 8", grp);
			chbd8.color = NoteColor(8);
			chbd8.Selected = true;
			cht = GetChannel("Tempo", grp);
			cht.Selected = true;
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tg.Selected = true;
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						seq.TimingGrids[tgs].Selected = true;
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				ppos = lineIn.IndexOf('.');
				if (ppos > utils.UNDEFINED)
				{

					parts = lineIn.Split(',');

					centisecs = ParseCentiseconds(parts[0]);
					beatLength = centisecs - lastBeat;

					if (toggled)
					{
						ef = new Effect();
						ef.startCentisecond = lastBeat;
						ef.endCentisecond = centisecs;
						ef.Intensity = 100;
						ef.EffectType = EffectType.Intensity;
						chbx2.AddEffect(ef);
					}
					toggled = !toggled;


					divd = ((double)beatLength) / 2;
					divi0 = (int)Math.Round(divd);

					ef = new Effect();
					ef.startCentisecond = lastBeat;
					ef.endCentisecond = lastBeat + divi0;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chb.AddEffect(ef);


					divd = ((double)beatLength) / 4;
					divi2 = (int)Math.Round(divd);

					for (int n = 0; n < 2; n++)
					{
						ef = new Effect();
						divi2 = (int)Math.Round(n * divd * 2);
						ef.startCentisecond = lastBeat + divi2;
						divi2 = (int)Math.Round(n * divd * 2 + divd);
						ef.endCentisecond = lastBeat + divi2;
						ef.Intensity = 100;
						ef.EffectType = EffectType.Intensity;
						chbd2.AddEffect(ef);
					}


					divd = ((double)beatLength) / 6;
					divi3 = (int)Math.Round(divd);

					for (int n = 0; n < 3; n++)
					{
						ef = new Effect();
						divi3 = (int)Math.Round(n * divd * 2);
						ef.startCentisecond = lastBeat + divi3;
						divi3 = (int)Math.Round(n * divd * 2 + divd);
						ef.endCentisecond = lastBeat + divi3;
						ef.Intensity = 100;
						ef.EffectType = EffectType.Intensity;
						chbd3.AddEffect(ef);
					}


					divd = ((double)beatLength) / 8;
					divi4 = (int)Math.Round(divd);

					for (int n = 0; n < 4; n++)
					{
						ef = new Effect();
						divi4 = (int)Math.Round(n * divd * 2);
						ef.startCentisecond = lastBeat + divi4;
						divi4 = (int)Math.Round(n * divd * 2 + divd);
						ef.endCentisecond = lastBeat + divi4;
						ef.Intensity = 100;
						ef.EffectType = EffectType.Intensity;
						chbd4.AddEffect(ef);
					}


					divd = ((double)beatLength) / 16;
					divi8 = (int)Math.Round(divd);

					for (int n=0; n<8; n++)
					{
						ef = new Effect();
						divi8 = (int)Math.Round(n * divd * 2);
						ef.startCentisecond = lastBeat + divi8;
						divi8 = (int)Math.Round(n * divd * 2 + divd);
						ef.endCentisecond = lastBeat + divi8;
						ef.Intensity = 100;
						ef.EffectType = EffectType.Intensity;
						chbd8.AddEffect(ef);
					}


					if (parts.Length > 1)
					{
						if (parts[1].Length > 2)
						{
							string bpm = parts[1];
							int pos = bpm.IndexOf(' ');
							if (pos > 0)
							{
								bpm = bpm.Substring(1, pos);
							}
							tempo = double.Parse(bpm);
							tempo = Math.Max(0, tempo);
							tempo = Math.Min(200, tempo);
							if (lastTempo == 0) lastTempo = (int)(tempo / 2);
							ef = new Effect();
							ef.startCentisecond = lastBeat;
							ef.endCentisecond = centisecs;
							ef.startIntensity = lastTempo;
							ef.endIntensity = (int)(tempo / 2);
							ef.EffectType = EffectType.Intensity;
							cht.AddEffect(ef);
							lastTempo = ef.endIntensity;
						}


					}

					lastBeat = centisecs;

				} // end line contains a period
			} // end while loop more lines remaining
			reader.Close();

			// Divy up the very last beat
			centisecs = chb.Centiseconds-1;
			beatLength = centisecs - lastBeat;

			if (beatLength > 4)
			{
				if (toggled)
				{
					ef = new Effect();
					ef.startCentisecond = lastBeat;
					ef.endCentisecond = centisecs;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chbx2.AddEffect(ef);
				}
				toggled = !toggled;


				divd = ((double)beatLength) / 2;
				divi0 = (int)Math.Round(divd);

				ef = new Effect();
				ef.startCentisecond = lastBeat;
				ef.endCentisecond = lastBeat + divi0;
				ef.Intensity = 100;
				ef.EffectType = EffectType.Intensity;
				chb.AddEffect(ef);
			}
			if (beatLength > 7)
			{
				divd = ((double)beatLength) / 4;
				divi2 = (int)Math.Round(divd);

				for (int n = 0; n < 2; n++)
				{
					ef = new Effect();
					divi2 = (int)Math.Round(n * divd * 2);
					ef.startCentisecond = lastBeat + divi2;
					divi2 = (int)Math.Round(n * divd * 2 + divd);
					ef.endCentisecond = lastBeat + divi2;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chbd2.AddEffect(ef);
				}
			}
			if (beatLength > 9)
			{
				divd = ((double)beatLength) / 6;
				divi3 = (int)Math.Round(divd);

				for (int n = 0; n < 3; n++)
				{
					ef = new Effect();
					divi3 = (int)Math.Round(n * divd * 2);
					ef.startCentisecond = lastBeat + divi3;
					divi3 = (int)Math.Round(n * divd * 2 + divd);
					ef.endCentisecond = lastBeat + divi3;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chbd3.AddEffect(ef);
				}
			}
			if (beatLength > 11)
			{
				divd = ((double)beatLength) / 8;
				divi4 = (int)Math.Round(divd);

				for (int n = 0; n < 4; n++)
				{
					ef = new Effect();
					divi4 = (int)Math.Round(n * divd * 2);
					ef.startCentisecond = lastBeat + divi4;
					divi4 = (int)Math.Round(n * divd * 2 + divd);
					ef.endCentisecond = lastBeat + divi4;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chbd4.AddEffect(ef);
				}
			}
			if (beatLength > 20)
			{
				divd = ((double)beatLength) / 16;
				divi8 = (int)Math.Round(divd);

				for (int n = 0; n < 8; n++)
				{
					ef = new Effect();
					divi8 = (int)Math.Round(n * divd * 2);
					ef.startCentisecond = lastBeat + divi8;
					divi8 = (int)Math.Round(n * divd * 2 + divd);
					ef.endCentisecond = lastBeat + divi8;
					ef.Intensity = 100;
					ef.EffectType = EffectType.Intensity;
					chbd8.AddEffect(ef);
				}
			}


			if (lastTempo == 0) lastTempo = (int)(tempo / 2);
			ef = new Effect();
			ef.startCentisecond = lastBeat;
			ef.endCentisecond = centisecs;
			ef.startIntensity = lastTempo;
			ef.endIntensity = (int)(tempo / 2);
			ef.EffectType = EffectType.Intensity;
			cht.AddEffect(ef);








			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddPolyChannels(string PolyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			Channel ch;
			Effect ef;

			//Track tuneTrack = new Track("Polyphonic Transcription");
			//Track tuneTrack = GetTrack(MASTERTRACK);
			tuneTrack.Selected = true;
			//tuneTrack.Centiseconds = seq.Centiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			if (tg == null)
			{
				//tuneTrack.timingGrid = tg;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				tg.Selected = true;
			}
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPPOLY, tuneTrack);
			grp.Selected = true;
			CreatePolyChannels(grp, "Poly ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						seq.TimingGrids[tgs].Selected = true;
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			StreamReader reader = new StreamReader(PolyFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					ontime = ParseCentiseconds(parts[1]);
					note = Int16.Parse(parts[2]);
					//ch = seq.Channels[firstCobjIdx + note];
					//ch = GetChannel("theName");
					ch = noteChannels[note];
					ef = new Effect();
					ef.EffectType = EffectType.Intensity;
					ef.startCentisecond = centisecs;
					ef.endCentisecond = centisecs + ontime;
					if (useRampsPoly)
					{
						ef.startIntensity = 100;
						ef.endIntensity = 0;
					}
					else
					{
						ef.Intensity = 100;
					}
					//ch.effects.Add(ef);
					ch.AddEffect(ef);
				}

			} // end while loop more lines remaining

			reader.Close();

			//seq.AddTrack(tuneTrack);
			SelectUsedChannels(0);


			return pcount;
		}

		private int AddConstQChannels(string constQFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			tuneTrack.Selected = true;
			//tuneTrack.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tg.Selected = true;
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPCONSTQ, tuneTrack);
			grp.Selected = true;
			CreatePolyChannels(grp, "ConstQ ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						seq.TimingGrids[tgs].Selected = true;
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[128];
			StreamReader reader = new StreamReader(constQFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 129)
				{
					pcount++;
					//centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
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
					centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
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
							//ch = seq.Channels[firstCobjIdx + note];
							ch = noteChannels[note];
							//Identity id = seq.Members.bySavedIndex[noteChannels[note]];
							//if (id.PartType == MemberType.Channel)
							//{
							//ch = (Channel)id.owner;
							ef = new Effect();
							ef.EffectType = EffectType.Intensity;
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


			SelectUsedChannels(2);






			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddChromaChannels(string chromaFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			tuneTrack.Selected = true;
		
			//tuneTrack.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tuneTrack.timingGrid = tg;
			tg.Selected = true;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPCHROMA, tuneTrack);
			grp.Selected = true;
			CreateChromaChannels(grp, "Chroma ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
					//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						seq.TimingGrids[tgs].Selected = true;
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[12];
			StreamReader reader = new StreamReader(chromaFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
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

			reader = new StreamReader(chromaFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 13)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
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
							ch = noteChannels[note];
							//Identity id = seq.Members.bySavedIndex[noteChannels[note]];
							//if (id.PartType == MemberType.Channel)
							//{
							//ch = (Channel)id.owner;
							ef = new Effect();
							ef.EffectType = EffectType.Intensity;
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









			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddKeyChannels(string keyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			tuneTrack.Selected = true;
			//tuneTrack.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tg.Selected = true;
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPKEY, tuneTrack);
			grp.Selected = true;
			CreateKeyChannels(grp, "Key ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						seq.TimingGrids[tgs].Selected = true;
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}


			int[] lastcs = new int[12];
			double lastdVal = 0;
			int[] lastiVal = new int[12];


			ef = new Effect();
			ef.startCentisecond = 0;

			StreamReader reader = new StreamReader(keyFile);

			lineIn = reader.ReadLine();
			parts = lineIn.Split(',');
			if (parts.Length == 3)
			{
				pcount++;
				centisecs = ParseCentiseconds(parts[0]);
				int note = int.Parse(parts[1]);
				if (note > 12) note--;
				note--;

				ef = new Effect();
				ef.EffectType = EffectType.Intensity;
				ef.Intensity = 100;
				ef.startCentisecond = centisecs;
				noteChannels[note].AddEffect(ef);
			}

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					ef.endCentisecond = centisecs;

					int note = int.Parse(parts[1]);
					if (note > 12) note--;
					note--;

					ef = new Effect();
					ef.EffectType = EffectType.Intensity;
					ef.Intensity = 100;
					ef.startCentisecond = centisecs;
					noteChannels[note].AddEffect(ef);
				}
			}   // end while loop more lines remaining

			ef.endCentisecond = noteChannels[0].Centiseconds;

			reader.Close();

			SelectUsedChannels(0);







			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddMelodyChannels(string melodyFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			tuneTrack.Selected = true;
			//tuneTrack.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tg.Selected = true;
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPMELODY, tuneTrack);
			grp.Selected = true;
			CreateKeyChannels(grp, "Melody ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				seq.TimingGrids[0].Selected = true;
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						seq.TimingGrids[tgs].Selected = true;
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}


			int[] lastcs = new int[12];
			double lastdVal = 0;
			int[] lastiVal = new int[12];


			ef = new Effect();
			ef.startCentisecond = 0;

			StreamReader reader = new StreamReader(melodyFile);

			lineIn = reader.ReadLine();
			parts = lineIn.Split(',');
			if (parts.Length == 3)
			{
				pcount++;
				centisecs = ParseCentiseconds(parts[0]);
				int note = int.Parse(parts[1]);
				if (note > 12) note--;
				note--;

				ef = new Effect();
				ef.EffectType = EffectType.Intensity;
				ef.Intensity = 100;
				ef.startCentisecond = centisecs;
				noteChannels[note].AddEffect(ef);
			}

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					ef.endCentisecond = centisecs;

					int note = int.Parse(parts[1]);
					if (note > 12) note--;
					note--;

					ef = new Effect();
					ef.EffectType = EffectType.Intensity;
					ef.Intensity = 100;
					ef.startCentisecond = centisecs;
					noteChannels[note].AddEffect(ef);
				}
			}   // end while loop more lines remaining

			ef.endCentisecond = noteChannels[0].Centiseconds;

			reader.Close();

			SelectUsedChannels(0);







			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddSpeechChannels(string speechFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			//int note = 0;
			Channel ch;
			Effect ef;

			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPSPEECH, tuneTrack);
			grp.Selected = true;
			CreateKeyChannels(grp, "Speech ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}


			int[] lastcs = new int[12];
			double lastdVal = 0;
			int[] lastiVal = new int[12];


			ef = new Effect();
			ef.startCentisecond = 0;

			StreamReader reader = new StreamReader(speechFile);

			lineIn = reader.ReadLine();
			parts = lineIn.Split(',');
			if (parts.Length == 3)
			{
				pcount++;
				centisecs = ParseCentiseconds(parts[0]);
				double note = int.Parse(parts[1]);
				if (note > 12) note--;
				note--;

				ef = new Effect();
				ef.EffectType = EffectType.Intensity;
				ef.Intensity = 100;
				ef.startCentisecond = centisecs;
				int nt = (int)Math.Round(note);
				noteChannels[nt].AddEffect(ef);
			}

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 3)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					ef.endCentisecond = centisecs;

					int note = int.Parse(parts[1]);
					if (note > 12) note--;
					note--;

					ef = new Effect();
					ef.EffectType = EffectType.Intensity;
					ef.Intensity = 100;
					ef.startCentisecond = centisecs;
					noteChannels[note].AddEffect(ef);
				}
			}   // end while loop more lines remaining

			ef.endCentisecond = noteChannels[0].Centiseconds;

			reader.Close();

			SelectUsedChannels(0);







			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int AddSegmentChannels(string resultsFile)
		{
			int pcount = 0;

			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;
			string[] parts;
			int ontime = 0;
			int note = 0;
			Channel ch;
			Effect ef;

			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			tuneTrack.timingGrid = tg;
			//tuneTrack.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPSEGMENTS, tuneTrack);
			grp.Selected = true;
			CreatePolyChannels(grp, "Segments ", doGroups);
			if (tg == null)
			{
				tuneTrack.timingGrid = seq.TimingGrids[0];
				//tuneTrack.timingGridSaveID = 0;
			}
			else
			{
				tuneTrack.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						tuneTrack.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[1024];
			StreamReader reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					//centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
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
			int lastcs = utils.UNDEFINED;
			double lastdt = 0;
			int lastix = 0;

			reader = new StreamReader(resultsFile);

			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split(',');
				if (parts.Length == 1025)
				{
					pcount++;
					centisecs = ParseCentiseconds(parts[0]);
					//Debug.Write(centisecs);
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
						if (centisecs == lastcs)
						{
							ix += lastix;
							ix /= 2;
						}



						lastix = ix;
						lastdt = dt;
						lastcs = centisecs;
					}
				}
			} // end while loop more lines remaining
			reader.Close();









			//seq.AddTrack(tuneTrack);



			return pcount;
		}

		private int ParseCentiseconds(string secondsValue)
		{
			int ppos = secondsValue.IndexOf('.');
			// Get number of seconds before the period
			int sec = Int16.Parse(secondsValue.Substring(0, ppos));
			// Get the fraction of a second after the period, only keep most significant 4 digits
			int dotsec = Int16.Parse(secondsValue.Substring(ppos + 1, 4));
			// turn it from an int into an actual fraction
			decimal ds = (dotsec / 100);
			// Round up or down from 4 digits to 2
			dotsec = (int)Math.Round(ds);  // man is this stupid call picky as hell about syntax
																		 // Combine seconds and fraction of a second into Centiseconds
			int centisecs = sec * 100 + dotsec;

			return centisecs;

		}


		private int ClearLastRun()
		{
			if (System.IO.File.Exists(resultsNoteOnset)) System.IO.File.Delete(resultsNoteOnset);
			if (System.IO.File.Exists(resultsBars)) System.IO.File.Delete(resultsBars);
			if (System.IO.File.Exists(resultsBeats)) System.IO.File.Delete(resultsBeats);
			if (System.IO.File.Exists(resultsPoly)) System.IO.File.Delete(resultsPoly);
			if (System.IO.File.Exists(resultsConstQ)) System.IO.File.Delete(resultsConstQ);
			if (System.IO.File.Exists(resultsChroma)) System.IO.File.Delete(resultsChroma);
			if (System.IO.File.Exists(resultsSegments)) System.IO.File.Delete(resultsSegments);
			if (System.IO.File.Exists(resultsSpectro)) System.IO.File.Delete(resultsSpectro);

			if (System.IO.File.Exists(fileAudioWork)) System.IO.File.Delete(fileAudioWork);


			resultsNoteOnset = "";
			resultsBars = "";
			resultsBeats = "";
			resultsPoly = "";
			resultsConstQ = "";
			resultsChroma = "";
			resultsSegments = "";
			resultsSpectro = "";


			return 0;
		}

		private void CreatePolyChannels(IMember parent, string prefix, bool useGroups)
		{
			string dmsg = "";
			//Channel chan;
			int octave = 0;
			int lastOctave = 0;
			IMember chanParent = null;
			ChannelGroup grp = null; // new ChannelGroup("null");
			Array.Resize(ref octaveGroups, octaveNamesA.Length);
			if (useGroups)
			{
				grp = GetGroup(prefix + octaveNamesA[octave], parent);
				octaveGroups[octave] = grp;
				chanParent = grp;
				//grp.identity.Centiseconds = seq.totalCentiseconds;
			}
			else
			{
				if (parent.MemberType == MemberType.Track)
				{
					chanParent = (Track)parent;
				}
				else
				{
					// useGroups is false, so the parent should be a track, but it's not!
					Debug.Assert(true);
				}
			}
			Array.Resize(ref noteChannels, noteNames.Length);
			for (int n = 0; n < noteNames.Length; n++)
			{
				if (useGroups)
				{
					octave = n / 12;
					if (octave != lastOctave)
					{
						// add group from last octave
						//AddChildToParent(grp, parent);
						// then create new octave group
						grp = GetGroup(prefix + octaveNamesA[octave], parent);
						octaveGroups[octave] = grp;
						//grp.identity.Centiseconds = seq.totalCentiseconds;
						lastOctave = octave;
						chanParent = grp;
						dmsg = "Adding Group '" + grp.Name + "' SI:" + grp.SavedIndex;
						dmsg += " Octave #" + octave.ToString();
						dmsg += " to Parent '" + parent.Name + "' SI:" + parent.SavedIndex;
						Debug.WriteLine(dmsg);
					}
				}
				Channel chan = GetChannel(prefix + noteNames[n], chanParent);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				Debug.WriteLine(dmsg);


				if (n == 0)
				{
					firstCobjIdx = seq.Channels.Count - 1;
					firstCsavedIndex = chan.SavedIndex;
				}
			}
			if (useGroups)
			{
				//AddChildToParent(grp, parent);
			}
			seq.Members.ReIndex();



		}

		private void CreateChromaChannels(IMember parent, string prefix, bool useGroups)
		{
			string dmsg = "";
			//Channel chan;
			//int octave = 0;
			//int lastOctave = 0;
			IMember chanParent = null;
				if (parent.MemberType == MemberType.ChannelGroup)
				{
				chanParent = (ChannelGroup)parent;
				}
				else
				{
					// useGroups is false, so the parent should be a track, but it's not!
					Debug.Assert(true);
				}
			Array.Resize(ref noteChannels, chromaNames.Length);
			for (int n = 0; n < chromaNames.Length; n++)
			{
				Channel chan = GetChannel(prefix + chromaNames[n], chanParent);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				Debug.WriteLine(dmsg);


				if (n == 0)
				{
					firstCobjIdx = seq.Channels.Count - 1;
					firstCsavedIndex = chan.SavedIndex;
				}
			}
			seq.Members.ReIndex();
		} // end CreateChromaChannels

		private void CreateKeyChannels(IMember parent, string prefix, bool useGroups)
		{
			string dmsg = "";
			//Channel chan;
			//int octave = 0;
			//int lastOctave = 0;
			IMember chanParent = null;
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				chanParent = (ChannelGroup)parent;
			}
			else
			{
				// useGroups is false, so the parent should be a track, but it's not!
				Debug.Assert(true);
			}
			Array.Resize(ref noteChannels, 24);
			for (int n = 0; n < chromaNames.Length; n++)
			{
				Channel chan = GetChannel(prefix + chromaNames[n] + " Major", chanParent);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				Debug.WriteLine(dmsg);


				if (n == 0)
				{
					firstCobjIdx = seq.Channels.Count - 1;
					firstCsavedIndex = chan.SavedIndex;
				}
			}
			for (int n = 0; n < chromaNames.Length; n++)
			{
				Channel chan = GetChannel(prefix + chromaNames[n] + " Minor", chanParent);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				noteChannels[n+12] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + chanParent.Name + "' SI:" + chanParent.SavedIndex;
				Debug.WriteLine(dmsg);


			}
			seq.Members.ReIndex();
		} // end CreateChromaChannels

		private void SelectUsedChannels(int threshold)
		{
			for (int n = 0; n < noteChannels.Length; n++)
			{
				if (noteChannels[n].effects.Count > threshold)
				{
					noteChannels[n].Selected = true;
				}
				else
				{
					noteChannels[n].Selected = false;
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

		} // end SelectUsedChannels

		private Track GetMasterTrack()
		{
			Track master = null;
			for (int t = 0; t<seq.Tracks.Count; t++)
			{
				int p = seq.Tracks[t].Name.IndexOf("Tune-O-Rama");
				if (p>0)
				{
					master = seq.Tracks[t];
					t = seq.Tracks.Count; // Force exit of for loop
				}
			}
			if (master == null)
			{
				master = seq.CreateTrack(MASTERTRACK);
				//master.SetParentSeq(seq);
				master.Centiseconds = seq.Centiseconds;
				master.timingGrid = seq.TimingGrids[0];
				//master.Members.SetParentSequence(seq)

			}
			return master;
		}

		private Track GetTrack(string trackName)
		{
			// Gets existing track specified by Name if it already exists
			// Creates it if it does not
			Track ret = seq.FindTrack(trackName);
			if (ret == null)
			{
				ret = seq.CreateTrack(trackName);
				ret.Centiseconds = centiseconds;
				//ret.SetParentSeq(seq);
				ret.timingGrid = seq.TimingGrids[0];
				//ret.Members.parentSequence = seq;
				//seq.AddTrack(ret);
			}
			return ret;
		}

		private TimingGrid GetGrid(string gridName)
		{
			// Gets existing track specified by Name if it already exists
			// Creates it if it does not
			TimingGrid ret = seq.FindTimingGrid(gridName);
			if (ret == null)
			{
				ret = seq.CreateTimingGrid(gridName);
				ret.Centiseconds = centiseconds;
				//ret.SetParentSeq(seq);
				//seq.AddTimingGrid(ret);
			}
			else
			{
				// Clear any existing timings from a previous run
				if (ret.timings.Count > 0)
				{
					ret.timings = new List<int>();
				}
			}
			return ret;
		}

		private Channel GetChannel(string channelName, IMember parent)
		{
			Channel ret = null;
			if (parent.MemberType == MemberType.Track)
			{
				Track trk = (Track)parent;
				ret = (Channel)trk.Members.Find(channelName, MemberType.Channel, true);
			}
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				ChannelGroup group = (ChannelGroup)parent;
				ret = (Channel)group.Members.Find(channelName, MemberType.Channel, true);
			}
			if (parent.MemberType == MemberType.Sequence)
			{
				Sequence4 seq = (Sequence4)parent;
				ret = (Channel)seq.Members.Find(channelName, MemberType.Channel, true);
			}

			ret.effects.Clear();
			return ret;
		}

		private ChannelGroup GetGroup(string groupName, IMember parent)
		{
			ChannelGroup ret = null;
			if (parent.MemberType == MemberType.Track)
			{
				// Level 2, belongs to a track
				Track trk = (Track)parent;
				ret = (ChannelGroup)trk.Members.Find(groupName, MemberType.ChannelGroup, true);
			}
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				// Level 3+, belongs to another group
				ChannelGroup group = (ChannelGroup)parent;
				ret = (ChannelGroup)group.Members.Find(groupName, MemberType.ChannelGroup, true);
			}
			if (parent.MemberType == MemberType.Sequence)
			{
				// Level 1, Invalid!
				// Sequences should never (directly) contain groups!
				System.Diagnostics.Debugger.Break();
			}

			return ret;
		}

		private void ShowVamping(string songName)
		{
			if (songName.Length > 1)
			{
				lblSongName.Text = songName;
				int x = (this.ClientSize.Width - pnlVamping.Width) / 2;
				int y = (this.ClientSize.Height - pnlVamping.Height) / 2;
				pnlVamping.Location = new Point(x, y);
				pnlVamping.BringToFront();
				pnlVamping.Visible = true;
			}
			else
			{
				pnlVamping.SendToBack();
				pnlVamping.Visible = false;
			}
		}


	} // end partial class frmTune
} // end namespace TuneORama
