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
		// I decided not to enum these, may change my mind later
		public const byte SAVEmixedDisplay = 1;
		public const byte SAVEcrgDisplay = 2;
		public const byte SAVEcrgAlpha = 3;
		// end enums

		public const char DELIM1 = '⬖';
		public const char DELIM4 = '⬙';
		public DialogResult closeMode = DialogResult.Cancel;

		private string fileCurrent = "";  // Currently loaded Sequence File
		private string fileSeqCur = ""; // Last Sequence File Loaded
		private string fileSeqSave = ""; // Last Saved Sequence
		private string fileSeqLast = ""; // last file ran.
		private string fileChanCfg = "";
		private string fileChanCfgLast = "";
		private string fileAudioOriginal = "";
		private string fileAudioWork = "";
		private string fileResults = "";
		private string originalPath = "";
		private string originalFileName = "";
		private string originalExt = "";
		private string preppedFileName = "";
		private string newFile = "";
		private string prepFile = "";
		private string preppedPath = "";
		private string preppedExt = "";
		private string preppedAudio = "";
		int centiseconds = 0;


		private Sequence4 seq = new Sequence4();
		private bool dirtySeq = false;
		private const string helpPage = "http://wizlights.com/utilorama/tuneorama";
		private string applicationName = "Tune-O-Rama";
		private string thisEXE = "tune-o-rama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte useSaveFormat = SAVEmixedDisplay;

		//private List<TreeNode>[] siNodes;
		private List<List<TreeNode>> siNodes = new List<List<TreeNode>>();

		private bool firstShown = false;
		private TreeNode previousNode = null;

		//private string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";
		private string annotatorProgram = "";
		private string pluginsPath = "\"C:\\Program Files (x86)\\Vamp Plugins\\\"";
		private int rlevel = 0;

		const string VAMPnoteOnset = "vamp:qm-vamp-plugins:qm-onsetdetector:onsets";
		const string FILEnoteOnset = "_vamp_qm-vamp-plugins_qm-onsetdetector_onsets";
		const string VAMPPoly = "vamp:qm-vamp-plugins:qm-transcription:transcription";
		const string FILEPoly = "_vamp_qm-vamp-plugins_qm-transcription_transcription";
		const string VAMPspectrogram = "vamp:qm-vamp-plugins:qm-adaptivespectrogram:output";
		const string FILEspectrogram = "_vamp_qm-vamp-plugins_qm-adaptivespectrogram_output";
		const string VAMPbeats1 = "vamp:qm-vamp-plugins:qm-barbeattracker:beats";
		const string VAMPbeats2 = "vamp:qm-vamp-plugins:qm-tempotracker:beats";
		const string VAMPconstq = "vamp:qm-vamp-plugins:qm-constantq:constantq";
		const string FILEconstq = "_vamp_qm-vamp-plugins_qm-constantq_constantq";

		const string WRITEformat = " -f -w csv --csv-force ";

		private bool doAutoSave = false;
		private bool doAutoLaunch = true;
		private int timeSignature = 4;  // Default 4/4 time
		private bool doNoteOnsets = true;
		private bool doBeatGrid = true;
		private bool doPoly = true;
		private bool doBeatTrack = true;
		private bool doGroups = true;
		private bool useRampsPoly = false;
		private bool useRampsBeats = false;
		private int gridBeatsX = 4;
		private int trackBeatsX = 4;
		private bool useOctaveGrouping = true;
		private bool doSpectro = false;  // Full Spectrogram, not Const Q
		private bool doConstQ = true;
		private bool doChroma = true;
		private bool doSegments = true;
		private bool doSpeech = true;
		private bool useChanCfg = false;
		private int firstCobjIdx = utils.UNDEFINED;
		private int firstCsavedIndex = utils.UNDEFINED;
		private Channel[] noteChannels = null;
		private ChannelGroup octaveGroups = null;

		private const string MASTERTRACK = "Song Information [Tune-O-Rama]";
		private const string GROUPSPECTRO = "Spectrogram";
		private const string GROUPCONSTQ = "Constant Q Spectrogram";
		private const string GROUPPOLY = "Polyphonic Transcription";
		private const string GRIDONSETS = "Note Onsets";




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


		public frmTune()
		{
			InitializeComponent();
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private void frmTune_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{

			ImBusy(true);
			RestoreFormPosition();
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string appFolder = applicationName;
			appFolder = appFolder.Replace("-", "");
			string mySubDir = "\\UtilORama\\" + appFolder + "\\";
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
			RetrieveSettings();

			SetTheControlsForTheHeartOfTheSun();

			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			//TODO: These may get overridden by command line arguments (not yet supported)
			//! EXAMPLES FROM SPLIT-O-RAMA
			//useFuzzy = Properties.Settings.Default.useFuzzy;
			//minPreMatchScore = Properties.Settings.Default.preMatchScore;
			//saveFormat = Properties.Settings.Default.SaveFormat;
			//if ((minPreMatchScore < 500) || (minPreMatchScore > 1000))
			//{
			//	minPreMatchScore = 800;
			//}
			//minFinalMatchScore = Properties.Settings.Default.finalMatchScore;
			//if ((minFinalMatchScore < 500) || (minFinalMatchScore > 1000))
			//{
			//	minFinalMatchScore = 950;
			//}





			if (batchMode)
			{
				//TODO: Batch Mode
			}
			else
			{
				if (batch_fileCount == 0)
				{
					// No files specified on command line
					// Is the last file loaded from last run still valid?
					fileSeqLast = Properties.Settings.Default.fileSeqLast;
					if (System.IO.File.Exists(fileSeqLast))
					{
						//seq.ReadSequenceFile(fileSeqLast);
						//fileCurrent = fileSeqLast;
						//utils.TreeFillChannels(treChannels, seq, siNodes);
						//txtSequenceFile.Text = utils.ShortenLongPath(fileCurrent, 80);
					}
				}
				else
				{
					// 1 and only 1 file specified on command line
					//seq.ReadSequenceFile(batch_fileList[0]);
					fileSeqLast = batch_fileList[0];
					//utils.TreeFillChannels(treChannels, seq, siNodes);
					Properties.Settings.Default.fileSeqLast = fileSeqLast;
					Properties.Settings.Default.Save();
				}
			}



			txtFileAudio.Text = utils.ShortenLongPath(fileSeqLast, 80);



			ImBusy(false);

		}

		private void RetrieveSettings()
		{
			annotatorProgram = Properties.Settings.Default.annotatorProgram;
			doAutoSave = Properties.Settings.Default.doAutoSave;
			doAutoLaunch = Properties.Settings.Default.doAutoLaunch;
			doBeatGrid = Properties.Settings.Default.doBeatGrid;
			doBeatTrack = Properties.Settings.Default.doBeatTrack;
			doNoteOnsets = Properties.Settings.Default.doNoteOnsets;
			doPoly = Properties.Settings.Default.doPoly;
			doChroma = Properties.Settings.Default.doChroma;
			useSaveFormat = Properties.Settings.Default.useSaveFormat;
			useOctaveGrouping = Properties.Settings.Default.useOctaveGrouping;
			useRampsPoly = Properties.Settings.Default.useRampsPoly;
			//doSpectro = Properties.Settings.Default.Spectrogram;
			doConstQ = Properties.Settings.Default.doConstQ;
			useChanCfg = Properties.Settings.Default.useChanCfg;
			fileChanCfgLast = Properties.Settings.Default.fileChanCfgLast;
			if (System.IO.File.Exists(fileChanCfgLast))
			{
				SetUseChanConfig(true);
				txtFileCurrent.Text = Path.GetFileName(fileChanCfgLast);
			}
			else
			{
				SetUseChanConfig(false);
				useChanCfg = false;
			}

		}

		private void SetTheControlsForTheHeartOfTheSun()
		{


			chkAutoSave.Checked = doAutoSave;
			chkNoteOnsets.Checked = doNoteOnsets;
			chkBeatsGrid.Checked = doBeatGrid;
			chkPoly.Checked = doPoly;
			//chkOctaveGrouping.Checked = doOctoGroup;
			chkBeatsTrack.Checked = doBeatTrack;
			//optOnOff.Checked = !doRamps;
			//optRamps.Checked = doRamps;
			//chkSpectrogram.Checked = doSpectro;
			chkConstQ.Checked = doConstQ;
			//SetUseChanConfig(useChanCfg);
			chkAutoLaunch.Checked = doAutoLaunch;

		}

		private void ProcessCommandLine()
		{
			commandArgs = Environment.GetCommandLineArgs();
			string arg;
			for (int f = 0; f < commandArgs.Length; f++)
			{
				arg = commandArgs[f];
				// Does it LOOK like a file?
				byte isFile = 0;
				if (arg.Substring(1, 2).CompareTo(":\\") == 0) isFile = 1;  // Local File
				if (arg.Substring(0, 2).CompareTo("\\\\") == 0) isFile = 1; // UNC file
				if (arg.Substring(4).IndexOf(".") > utils.UNDEFINED) isFile++;  // contains a period
				if (utils.InvalidCharacterCount(arg) == 0) isFile++;
				if (isFile == 3)
				{
					if (System.IO.File.Exists(arg))
					{
						string ext = Path.GetExtension(arg).ToLower();
						if (ext.CompareTo(".exe") == 0)
						{
							if (f == 0)
							{
								thisEXE = arg;
							}
						}
						if ((ext.CompareTo(".lms") == 0) || (ext.CompareTo(".las") == 0))
						{
							Array.Resize(ref batch_fileList, batch_fileCount + 1);
							batch_fileList[batch_fileCount] = arg;
							batch_fileCount++;
						}
						else
						{
							if (ext.CompareTo("chsel") == 0)
							{
								cmdSelectionsFile = arg;
							}
						}
					}
				}
				else
				{
					// Not a file, is it an argument
					if (arg.Substring(0, 1).CompareTo("/") == 0)
					{
						//TODO: Process any commands
					}
				}
			} // foreach argument
			if (batch_fileCount == 1)
			{


			}
			else
			{
				if (batch_fileCount > 1)
				{
					ProcessFileBatch(batch_fileList);
				}
			}


		}

		private void ProcessFileList(string[] batchFilenames)
		{
			batch_fileCount = 0; // reset
			bool inclSelections = false;
			DialogResult dr = DialogResult.None;

			foreach (string file in batchFilenames)
			{
				// None of this should be necessary for a file dragdrop			

				// Does it LOOK like a file?
				//byte isFile = 0;
				//if (arg.Substring(1, 2).CompareTo(":\\") == 0) isFile = 1;  // Local File
				//if (arg.Substring(0, 2).CompareTo("\\\\") == 0) isFile = 1; // UNC file
				//if (arg.Substring(4).IndexOf(".") > utils.UNDEFINED) isFile++;  // contains a period
				//if (InvalidCharacterCount(arg) == 0) isFile++;
				//if (isFile == 2)
				//{
				//	if (File.Exists(arg))
				//	{
				string ext = Path.GetExtension(file).ToLower();
				if ((ext.CompareTo(".lms") == 0) ||
						(ext.CompareTo(".las") == 0) ||
						(ext.CompareTo(".lcc") == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;
				}
				else
				{
					if (ext.CompareTo(".chsel") == 0)
					{
						cmdSelectionsFile = file;
						inclSelections = true;
					}
				}
				//	}
				//}
			}
			if (batch_fileCount > 1)
			{
				batchMode = true;
				ProcessFileBatch(batch_fileList);
			}
			else
			{
				if (batch_fileCount == 1)
				{
					if (dirtySeq)
					{
						//TODO Handle Dirty Sequence
					}
					ImBusy(true);
					fileCurrent = batch_fileList[0];

					FileInfo fi = new FileInfo(fileCurrent);
					Properties.Settings.Default.fileSeqLast = fileCurrent;
					Properties.Settings.Default.Save();

					txtFileAudio.Text = utils.ShortenLongPath(fileCurrent, 80);
					seq.ReadSequenceFile(fileCurrent);
					//utils.TreeFillChannels(treChannels, seq, siNodes, false, false);
					dirtySeq = false;
					ImBusy(false);

				} // cmdSeqFileCount-- Batch Mode or Not
			}
		} // end ProcessDragDropFiles

		private void ProcessFileBatch(string[] batchFilenames)
		{
			//TODO Write Batch Mode
			batchMode = false;
			string msg = "Batch mode is not supported... yet.\r\nLook for this feature in a future release (soon)!";
			MessageBox.Show(this, msg, "Batch Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
		} // end ProcessFileBatch

		private void ImBusy(bool isBusy)
		{
			try
			{
				if (isBusy)
				{
					this.Cursor = Cursors.WaitCursor;
					this.Enabled = false;
					pnlProgress.Visible = true;
				}
				else
				{
					pnlProgress.Visible = false;
					this.Enabled = true;
					this.Cursor = Cursors.Default;
				}
			}
			catch
			{ }
		} // end ImBusy


		private DialogResult AskSaveSequence()
		{
			DialogResult ret = DialogResult.OK;
			if (dirtySeq)
			{
				string msg = "Your selections have changed.\r\n\r\n";
				msg += "Do you want to save the Selected Channels to a new sequence?";
				ret = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1);
				if (ret == DialogResult.Yes)
				{
					btnSaveSequence.PerformClick();
					ret = DialogResult.OK;
				}
				if (ret == DialogResult.No)
				{
					ret = DialogResult.OK;
				}
			}
			return ret;
		}

		private void frmTune_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Form closing?  Disable controls
			//ImBusy(true);


			DialogResult result = AskSaveSequence();
			if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
			else
			{
				if (result == DialogResult.Yes)
				{
					btnSaveSequence.PerformClick();
				}
			}

			if (!e.Cancel)
			{
				//string where = tempPath; // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				//string when = tempPath;
				//if (!Directory.Exists(where))
			//	{
			//		Directory.CreateDirectory(where);
				//}
				//string what = "fileSelLast.ChSel";
				//string file = where +  what;
				//SaveSelections(file);
				CloseForm();
			}
			else
			{
				// Close cancelled, reenable controls
				ImBusy(false);
			}

		}

		private void CloseForm()
		{
			SaveFormPosition();
		}

		private void SaveFormPosition()
		{
			// Called with form is closed
			if (WindowState == FormWindowState.Normal)
			{
				Properties.Settings.Default.Location = Location;
				Properties.Settings.Default.Size = Size;
				Properties.Settings.Default.Minimized = false;
			}
			else
			{
				Properties.Settings.Default.Location = RestoreBounds.Location;
				Properties.Settings.Default.Size = RestoreBounds.Size;
				Properties.Settings.Default.Minimized = true;
			}
			Properties.Settings.Default.Save();
			this.Cursor = Cursors.Default;

		} // End SaveFormPosition

		private void RestoreFormPosition()
		{
			// Called when form is loaded
			//TODO: This only gets the area of the first screen in a multi-screen setup

			int ileft = Properties.Settings.Default.Location.X;
			int itop = Properties.Settings.Default.Location.Y;
			//int scrHt = Screen.PrimaryScreen.Bounds.Height;
			//int scrWd = Screen.PrimaryScreen.Bounds.Width;
			//int scrHt = SystemInformation.VirtualScreen.Height;
			//int scrWd = SystemInformation.VirtualScreen.Width;
			int scrWd = SystemInformation.WorkingArea.Width;
			int scrHt = SystemInformation.WorkingArea.Height;



			if (itop > (scrHt - this.Height))
			{
				itop = scrHt - this.Height;
			}
			if (ileft > (scrWd - this.Width))
			{
				ileft = scrWd - this.Width;
			}


			// Should get all screens and figure out if size/placement of the form is valid
			//TODO: Restore form.WindowState and if maximized use RestoreBounds()
			this.SetDesktopLocation(ileft, itop);

		} // End RestoreFormPosition

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string newFileIn;
			string newFileOut;
			string filt = "Musical Sequence (*.lms)|*.lms|Channel Configuration (*.lcc)|*.lcc";
			string tit = "Save Partial Sequence As...";
			string initDir = "";
			string initFile = "";
			initDir = utils.DefaultSequencesPath;

			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			//dlgSaveFile.FileName = Path.GetFullPath(fileCurrent) + Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + part.ToString() + ext;
			dlgSaveFile.FileName = utils.DefaultSequencesPath + Path.GetFileNameWithoutExtension(fileAudioOriginal) + ".lms";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.DefaultExt = ".lms";
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = tit;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + part.ToString(); // + ext;
			//newFileIn = "Part " + part.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
			//newFileIn = "Part Mother Fucker!!";
			dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				SaveSequence(dlgSaveFile.FileName);
			}
		} // end Save File As

		private void SaveSequence(string newFilename)
		{
			ImBusy(true);
				// normal default when not testing
			seq.WriteSequenceFile_DisplayOrder(newFilename, false, false);
			System.Media.SystemSounds.Beep.Play();
			dirtySeq = false;
			fileSeqSave = newFilename;
			ImBusy(false);

		}

		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWsave);
		}

		private DialogResult ShowSettings(byte initialTab)
		{
			ImBusy(true);
			frmSettings options = new frmSettings();

			options.useRampsBeats = useRampsBeats;
			options.useRampsPoly = useRampsPoly;
			options.useOctaveGrouping = useOctaveGrouping;
			options.gridBeatsX = gridBeatsX;
			options.trackBeatsX = trackBeatsX;
			options.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
			options.useSaveFormat = useSaveFormat;

			options.InitForm(initialTab);
			options.ShowDialog(this);
			DialogResult dr = options.closeMode;
			if (dr == DialogResult.OK)
			{
				useRampsBeats = options.useRampsBeats;
				useRampsPoly = options.useRampsPoly;
				useOctaveGrouping = options.useOctaveGrouping;
				gridBeatsX = options.gridBeatsX;
				trackBeatsX = options.trackBeatsX;
				timeSignature = options.timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				useSaveFormat = options.useSaveFormat;

				Properties.Settings.Default.useRampsBeats = useRampsBeats;
				Properties.Settings.Default.useRampsPoly = useRampsPoly;
				Properties.Settings.Default.useOctaveGrouping = useOctaveGrouping;
				Properties.Settings.Default.gridBeatsX = gridBeatsX;
				Properties.Settings.Default.trackBeatsX = trackBeatsX;
				// Time signature does not get saved, reverts back to default 4/4 next time program is run
				//Properties.Settings.Default.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				Properties.Settings.Default.useSaveFormat = useSaveFormat;


			}
			ImBusy(false);
			return dr;
		}

		private void SaveSettings(frmSettings settingsForm)
		{
			useOctaveGrouping = settingsForm.useOctaveGrouping;
			useRampsPoly = settingsForm.useRampsPoly;
			useSaveFormat = settingsForm.useSaveFormat;
			
		}


		private void Event_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				ProcessFileList(files);
				//this.Cursor = Cursors.Default;
			}
		}

		private void Event_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
			//this.Cursor = Cursors.Cross;
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ProcessAudio(string audioFileName)
		{
			ImBusy(true);
			bool needsFix = false;
			// seq = new Sequence4();
			//if (useChanCfg)
			//{
			//	if (System.IO.File.Exists(fileChanCfgLast))
			//	{
			//		seq.ReadSequenceFile(fileChanCfgLast);
					needsFix = true;
			//		//MessageBox.Show(seq.summary());
			//	}
			//}


			txtFileAudio.Text = Path.GetFileName(audioFileName);
			preppedAudio = PrepAudioFile(audioFileName);

			if (needsFix)
			{
				// If we started from a Channel config, then none of the Channels, groups, etc. has a length
				// Fill all those in with the sequence's total Centiseconds (taken from the audio file length)
				//CentiFixx();
			}

			if (doNoteOnsets)
			{
				int timingCount = RunNoteOnsets(preppedAudio);
			}

			if ((!doNoteOnsets) && (!doBeatGrid))
			{
				MakeDumbGrid();
			}

			if (doPoly)
			{
				fileResults = tempPath + preppedFileName;  // reset
																									 //CreatePolyChannels();
				int polyNotes = RunPoly(preppedAudio);
			}

			//if (doSpectro)
			//{
			//	fileResults = tempPath + preppedFileName;  // reset
			//	int spectroNotes = RunSpectrogram(preppedAudio);
			//}
			if (doSpectro)
			{
				fileResults = tempPath + preppedFileName;  // reset
				int SpectroNotes = RunSpectro(preppedAudio);
			}

			if (doConstQ)
			{
				fileResults = tempPath + preppedFileName;
				int ConstQNotes = RunConstQ(preppedAudio);

			}





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



			btnSaveSequence.Enabled = true;
			fileAudioOriginal = audioFileName;

			ImBusy(false);
		}

		private void MakeDumbGrid()
		{
			//TimingGrid tg = new TimingGrid("Fixed Grid .05");
			TimingGrid tg = GetGrid("Fixed Grid .05");
			tg.spacing = 5;
			//seq.AddTimingGrid(tg);
		}


		private int RunNoteOnsets(string preppedAudio)
		{
			const string ERRproc = " in RunNoteOnsets(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";
			int err = 0;



			string annotatorCommand = BuildAnnotatorNoteOnsetCmdStr(preppedAudio);


			//try
			//{
				Debug.WriteLine(annotatorProgram + " " + annotatorCommand);

				ProcessStartInfo annotator = new ProcessStartInfo(annotatorProgram);
				annotator.Arguments = annotatorCommand;
				Process cmd = Process.Start(annotator);
				cmd.WaitForExit(30000);  // 30 second timeout
				if (System.IO.File.Exists(fileResults))
				{
					NoteOnsetsToTimingGrid(fileResults);
				}
			//! Remarked out for debugging, keep files for debugging
			//TODO Unremark for release, delete old files when done processing them
			//System.IO.File.Delete(fileAudioWork);
			//System.IO.File.Delete(fileResults);
			/*
		}
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + preppedAudio; // + ERRtrk + t.ToString() + ERRitem + ti.ToString();
				emsg += ERRline + sf.GetFileLineNumber();
#if DEBUG
				System.Diagnostics.Debugger.Break();
#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			}
			*/
			return err;
		}

		private int RunPoly(string preppedAudio)
		{
			const string ERRproc = " in RunPoly(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";
			int err = 0;



			string annotatorCommand = BuildAnnotatorPolyCmdStr(preppedAudio);


			//try
			//{
				Debug.WriteLine(annotatorProgram + " " + annotatorCommand);

				ProcessStartInfo annotator = new ProcessStartInfo(annotatorProgram);
				annotator.Arguments = annotatorCommand;
				Process cmd = Process.Start(annotator);
				cmd.WaitForExit(30000);  // 30 second timeout
				if (System.IO.File.Exists(fileResults))
				{
					PolyToChannels(fileResults);
				}
			//! Remarked out for debugging, keep files for debugging
			//TODO Unremark for release, delete old files when done processing them
			//System.IO.File.Delete(fileAudioWork);
			//System.IO.File.Delete(fileResults);
			/*
		}
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + preppedAudio; // + ERRtrk + t.ToString() + ERRitem + ti.ToString();
				emsg += ERRline + sf.GetFileLineNumber();
				Debug.WriteLine(emsg);
#if DEBUG
				Debugger.Break();
#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			}
			*/
			return err;
		}

		private int RunSpectro(string preppedAudio)
		{
			return utils.UNDEFINED;
		}



			private int RunConstQ(string preppedAudio)
		{
			const string ERRproc = " in RunConstQ(";
			const string ERRtrk = "), in Track #";
			const string ERRitem = ", Items #";
			const string ERRline = ", Line #";
			int err = 0;



			string annotatorCommand = BuildAnnotatorConstQCmdStr(preppedAudio);


			try
			{
				Debug.WriteLine(annotatorProgram + " " + annotatorCommand);

				ProcessStartInfo annotator = new ProcessStartInfo(annotatorProgram);
				annotator.Arguments = annotatorCommand;
				Process cmd = Process.Start(annotator);
				cmd.WaitForExit(90000);  // 30 second timeout
				if (System.IO.File.Exists(fileResults))
				{
					ConstQToChannels(fileResults);
				}
				//! Remarked out for debugging, keep files for debugging
				//TODO Unremark for release, delete old files when done processing them
				//System.IO.File.Delete(fileAudioWork);
				//System.IO.File.Delete(fileResults);
			}
			catch (Exception ex)
			{
				StackTrace st = new StackTrace(ex, true);
				StackFrame sf = st.GetFrame(st.FrameCount - 1);
				string emsg = ex.ToString();
				emsg += ERRproc + preppedAudio; // + ERRtrk + t.ToString() + ERRitem + ti.ToString();
				emsg += ERRline + sf.GetFileLineNumber();
				Debug.WriteLine(emsg);
				#if DEBUG
					Debugger.Break();
				#endif
				utils.WriteLogEntry(emsg, utils.LOG_Error, Application.ProductName);
			}

			return err;
		}


		private string PrepAudioFile(string originalAudioFile)
		{
			// Input: A fully qualified path and filename to a audio file
			// Example: D:\Light-O-Rama\Audio\Wizards in Winter by TSO.mp3
			// Output: The Name of the COPY of the audio file that is to be annotated, in annotator's required format
			// Example: c:/users/johndoe/appdata/utilorama/tuneorama/wizardsinwinter.mp3

			seq.sequenceType = SequenceType.Musical;
			seq.info.sequenceType = SequenceType.Musical;
			Musik.AudioInfo audioData = ReadAudioFile(originalAudioFile);
		 	if (seq.info.music.Title == "") seq.info.music.Title = audioData.Title;
			if(seq.info.music.Artist == "") seq.info.music.Artist = audioData.Artist;
			if(seq.info.music.Album == "") seq.info.music.Album = audioData.Album;
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
			//Track trk = new Track(applicationName);
			//trk.itemSavedIndexes.Add(chan.SavedIndex);
			//trk.totalCentiseconds = seq.totalCentiseconds;
			//trk.Selected = true;
			//seq.AddTrack(trk);



			originalPath = Path.GetDirectoryName(originalAudioFile);
			originalFileName = Path.GetFileNameWithoutExtension(originalAudioFile);
			originalExt = Path.GetExtension(originalAudioFile);

			preppedFileName = PrepName(originalFileName);
			newFile = tempPath + preppedFileName + originalExt;
			fileAudioWork = newFile;
			fileResults = tempPath + preppedFileName;


			System.IO.File.Copy(originalAudioFile, newFile, true);


			preppedPath = PrepPath(tempPath);
			preppedExt = originalExt.ToLower();

			prepFile = preppedPath + preppedFileName + preppedExt;
			return prepFile;

		}


		private string BuildAnnotatorNoteOnsetCmdStr(string audioFileName)
		{
			//! Note: audioFileName must be 'Prepped' BEFORE passing it to this function
			//! And the original audio file must be copied to the new path and new Name

			//tring ret = annotatorPath;
			//ret += "sonic-annotator.exe";
			string ret = "-d " + VAMPnoteOnset;
			ret += WRITEformat;
			ret += audioFileName;

			fileResults += FILEnoteOnset + ".csv";


			return ret;
		}

		private string BuildAnnotatorPolyCmdStr(string audioFileName)
		{
			//! Note: audioFileName must be 'Prepped' BEFORE passing it to this function
			//! And the original audio file must be copied to the new path and new Name

			//tring ret = annotatorPath;
			//ret += "sonic-annotator.exe";
			//string ret = "-d " + VAMPPoly;
			string ret = "-d " + VAMPPoly;
			ret += WRITEformat;
			ret += audioFileName;

			fileResults += FILEconstq + ".csv";


			return ret;
		}

		private string BuildAnnotatorConstQCmdStr(string audioFileName)
		{
			//! Note: audioFileName must be 'Prepped' BEFORE passing it to this function
			//! And the original audio file must be copied to the new path and new Name

			//tring ret = annotatorPath;
			//ret += "sonic-annotator.exe";
			string ret = "-t constq.param";
			ret += WRITEformat;
			ret += audioFileName;

			fileResults += FILEconstq + ".csv";


			return ret;
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

		private int PolyToChannels(string PolyFile)
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

			//Track trk = new Track("Polyphonic Transcription");
			Track trk = GetTrack(MASTERTRACK);
			//trk.Centiseconds = seq.Centiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPPOLY, trk);
			CreatePolyChannels(grp, "Poly ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
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
					ef.type = EffectType.intensity;
					ef.startCentisecond = centisecs;
					ef.endCentisecond = centisecs + ontime;
					if (useRampsPoly)
					{
						ef.startIntensity = 100;
						ef.endIntensity = 0;
					}
					else
					{
						ef.intensity = 100;
					}
					//ch.effects.Add(ef);
					ch.AddEffect(ef);
				}

			} // end while loop more lines remaining

			reader.Close();

			//seq.AddTrack(trk);



			return pcount;
		}

		private int SpectrogramToChannels(string spectroFile)
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

			//Track trk = new Track("Spectrogram");
			Track trk = GetTrack(MASTERTRACK);
			//trk.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPSPECTRO, trk);
			CreatePolyChannels(grp, "Spectro ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
						tgs = seq.TimingGrids.Count; // break loop
					}
				}
			}

			// Pass 1, Get max values
			double[] dVals = new double[1024];
			StreamReader reader = new StreamReader(spectroFile);

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

			reader = new StreamReader(spectroFile);

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
						for (int m=0; m<8; m++)
						{
							int i = n * 8 + m+1;
							double d = Double.Parse(parts[i]);
							d *= dVals[i];
							dt += d;
						}
						dt /= 8;
						int ix = (int)dt;
						if (ix<20)
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









			//seq.AddTrack(trk);



			return pcount;
		}

		private int ConstQToChannels(string constQFile)
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

			//Track trk = new Track("Constant Q Spectrogram");
			Track trk = GetTrack(MASTERTRACK);
			//trk.identity.Centiseconds = seq.totalCentiseconds;
			TimingGrid tg = seq.FindTimingGrid(GRIDONSETS);
			trk.timingGrid = tg;
			//trk.timingGridObjIndex = tg.identity.myIndex;
			ChannelGroup grp = GetGroup(GROUPCONSTQ, trk);
			CreatePolyChannels(grp, "ConstQ ", doGroups);
			if (tg == null)
			{
				trk.timingGrid = seq.TimingGrids[0];
				//trk.timingGridSaveID = 0;
			}
			else
			{
				trk.timingGrid = tg;
				for (int tgs = 0; tgs < seq.TimingGrids.Count; tgs++)
				{
					if (seq.TimingGrids[tgs].SaveID == tg.SaveID)
					{
						trk.timingGrid = seq.TimingGrids[tgs];
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
				dVals[n] = 140 / dVals[n];
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
						double d = Double.Parse(parts[note+1]);
						d *= dVals[note];
						dt += d;
						int iVal = (int)dt;
						if (iVal < 21)
						{
							iVal = 0;
						}
						else
						{
							if (iVal > 120)
							{
								iVal = 100;
							}
							else
							{
								iVal -= 20;
							}
						}

						if (iVal != lastiVal[note])
						{
							//ch = seq.Channels[firstCobjIdx + note];
							ch = noteChannels[note];
							//Identity id = seq.Children.bySavedIndex[noteChannels[note]];
							//if (id.PartType == TableType.Channel)
							//{
								//ch = (Channel)id.owner;
								ef = new Effect();
								ef.type = EffectType.intensity;
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









			//seq.AddTrack(trk);



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


		private int NoteOnsetsToTimingGrid(string noteOnsetFile)
		{
			int onsetCount = 0;
			string lineIn = "";
			int ppos = 0;
			int centisecs = 0;

			//TimingGrid grid = new TimingGrid("Note Onsets");
			TimingGrid grid = GetGrid(GRIDONSETS);
			grid.type = TimingGridType.Freeform;
			//grid.type = timingGridType.freeform;
			grid.AddTiming(0); // Needs a timing of zero at the beginning

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
			//Track trk = seq.FindTrack(applicationName);
			//trk.timingGridObjIndex = seq.TimingGrids.Count - 1;
			//trk.timingGridObjIndex = grid.identity.SavedIndex;
			//trk.totalCentiseconds = seq.totalCentiseconds;

			return onsetCount;
		} // end Note Onsets to Timing Grid

		private Musik.AudioInfo ReadAudioFile(string vFilename)
		{
			Musik.AudioInfo oSI = new Musik.AudioInfo();
			try
			{
				// WARNING: Library will not accept unicode in filename
				//TODO: Try to fix unicode filename problem
				TagLib.File oA_MP3 = TagLib.File.Create(vFilename);
				oSI = ReadAudioTags(vFilename, oA_MP3);
				return oSI;
			} // END try
			catch (TagLib.CorruptFileException oEx)
			{
				//LogError(msErrorFile, vFilename);
				//LogError(msErrorFile, oEx.Message);
				//LogError(msErrorFile, "");

				string sMsg = "Corrupt File:" + vFilename + "\n" + oEx.Message;
				MessageBox.Show(sMsg, "Bad File", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return BlankAudio(vFilename);
			} // END catch (TagLib.CorruptFileException oEx)
			catch (Exception oEx)
			{
				//LogError(msErrorFile, vFilename);
				//LogError(msErrorFile, oEx.Message);
				//LogError(msErrorFile, "");

				string sMsg = "Error Reading File:" + vFilename + "\n" + oEx.Message;
				MessageBox.Show(sMsg, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				return BlankAudio(vFilename);
			} // END catch (Exception oEx)
		} // END private AudioInfo ReadAudioFile(string vFilename)

		private Musik.AudioInfo ReadAudioTags(string vsFilename, TagLib.File voA_MP3)
		{
			FileInfo oFile = new FileInfo(vsFilename);
			Musik.AudioInfo tAudio = new Musik.AudioInfo();
			//int dRate = 0;
			int iRate = 0;

			tAudio.Duration = voA_MP3.Properties.Duration;
			tAudio.Path = oFile.DirectoryName;
			tAudio.Filename = oFile.Name;
			tAudio.Type = oFile.Extension.ToLower();
			tAudio.AlbumArtist = NullStringFix(voA_MP3.Tag.FirstAlbumArtist);
			tAudio.Album = NullStringFix(voA_MP3.Tag.Album);
			tAudio.Artist = NullStringFix(voA_MP3.Tag.FirstPerformer);
			tAudio.Composer = NullStringFix(voA_MP3.Tag.FirstComposer);
			tAudio.Title = NullStringFix(voA_MP3.Tag.Title);
			tAudio.Year = voA_MP3.Tag.Year.ToString();
			tAudio.Genre = NullStringFix(voA_MP3.Tag.FirstGenre);
			tAudio.Comment = NullStringFix(voA_MP3.Tag.Comment);
			//tAudio.Bitrate = voA_MP3.Properties.AudioBitrate;
			tAudio.Size = oFile.Length;
			tAudio.DiscNo = voA_MP3.Tag.Disc;
			tAudio.Track = voA_MP3.Tag.Track;

			//tAudio.VBR = oA_MP3.Properties.Codecs.
			tAudio.Modified = oFile.LastWriteTime;


			iRate = voA_MP3.Properties.AudioBitrate;
			tAudio.Bitrate = iRate;

			tAudio.Valid = true;

			return tAudio;
		} // End ReadAudioTags

		private string NullStringFix(string vsIn)
		{
			if (vsIn == null)
			{
				return "";
			}
			else
			{
				return vsIn;
			} // END if (vsIn ==null )
		} // END private string NullStringFix(string vsIn)

		private Musik.AudioInfo BlankAudio(string vsFilename)
		{
			Musik.AudioInfo tAudio = new Musik.AudioInfo();

			tAudio.Duration = new System.TimeSpan(0);
			tAudio.AlbumArtist = "";
			tAudio.Album = "[Unknown]";
			tAudio.Artist = "[Unknown]";
			tAudio.Composer = "";
			tAudio.Title = "[Unknown]";
			tAudio.Year = "";
			tAudio.Genre = "";
			tAudio.Comment = "";
			tAudio.Bitrate = 0;
			tAudio.DiscNo = 0;
			tAudio.Track = 0;
			tAudio.Valid = false;
			try
			{
				FileInfo oFile = new FileInfo(vsFilename);
				tAudio.Size = oFile.Length;
				tAudio.Modified = oFile.LastWriteTime;
				tAudio.Path = oFile.DirectoryName;
				tAudio.Filename = oFile.Name;
				tAudio.Type = oFile.Extension.ToLower();
			} // END try
			catch (Exception e)
			{
				tAudio.Size = 0;
				tAudio.Modified = new DateTime(1980, 1, 1);
				tAudio.Path = vsFilename.Substring(0, vsFilename.LastIndexOf("\\"));
				tAudio.Filename = vsFilename.Substring(vsFilename.LastIndexOf("\\") + 1);
				tAudio.Type = vsFilename.Substring(vsFilename.LastIndexOf(".")).ToLower();
			} // END catch (Exception e)

			return tAudio;
		} // End ReadAudioTags

		private void btnBrowseAudio_Click(object sender, EventArgs e)
		{

			if (btnBrowseAudio.Text == "Browse...")
			{
				string initDir = utils.DefaultAudioPath;
				string initFile = "";



				dlgOpenFile.Filter = MusicFilter();
				dlgOpenFile.FilterIndex = 1;
				dlgOpenFile.DefaultExt = "";
				dlgOpenFile.InitialDirectory = initDir;
				dlgOpenFile.FileName = initFile;
				dlgOpenFile.CheckFileExists = true;
				dlgOpenFile.CheckPathExists = true;
				dlgOpenFile.Multiselect = false;
				dlgOpenFile.Title = "Select Media File...";
				//pnlAll.Enabled = false;
				DialogResult result = dlgOpenFile.ShowDialog(this);

				if (result == DialogResult.OK)
				{
					ProcessAudio(dlgOpenFile.FileName);
					grpSequence.Enabled = true;
				} // end if (result = DialogResult.OK)


			}
			else
			{
				string musicFile = fileAudioOriginal;
				if (Path.GetDirectoryName(fileAudioOriginal).Length < 3)
				{
					fileAudioOriginal = utils.DefaultAudioPath + fileAudioOriginal;
					grpSequence.Enabled = true;
					btnSaveSequence.Focus();
				}


				if (System.IO.File.Exists(fileAudioOriginal))
				{
					ProcessAudio(fileAudioOriginal);
					grpSequence.Enabled = true;
					btnSaveSequence.Focus();
				}
			}


		} // end BrowseAudio_Click

		private string MusicFilter()
		{
			const string AIF = "*.aif;*.aifc;*.aiff";
			const string MIDI = "*.mid;*.midi;*.rmi";
			const string MPEGaudio = "*.mp2;*.mp3;*.mpa;*.m4a";
			const string Sound = "*.au;*.snd";
			const string WAV = "*.wav";
			const string WindowsMediaAudio = "*.wma";
			const string AVI = "*.avi";
			const string MPEGvideo = "*.mpe;*.mpeg;*.mp4;*.m1v";
			const string WindowsMediaVideo = "*.asf;*.wm;*.wmv";
			const string AllFiles = "*.*";
			string AudioFiles = AIF + ";" + MIDI + ";" + MPEGaudio + ";" + Sound + ";" + WAV + ";" + WindowsMediaAudio;
			string VideoFiles = AVI + ";" + MPEGvideo + ";" + WindowsMediaVideo;
			string AllSupportedFiles = AudioFiles + ";" + VideoFiles;


			string ret = "All Supported Files|" + AllSupportedFiles;
			ret += "|Audio Files|" + AudioFiles;
			ret += "|- AIF (*.aif, *.aifc, *.aiff)|" + AIF;
			ret += "|- MIDI (*.mid, *.midi, *.rmi)|" + MIDI;
			ret += "|- MPEG Audio (*.mp2, *.mp3, *.mpa, *.m4a)|" + MPEGaudio;
			ret += "|- Sound (*.au, *.snd)|" + Sound;
			ret += "|- WAV (*.wav)|" + WAV;
			ret += "|- Windows Media Audio (*.wma)|" + WindowsMediaAudio;
			ret += "|Video Files|" + VideoFiles;
			ret += "|- AVI (*.avi)|" + AVI;
			ret += "|- Windows Media Video (*.asf, *.wm, *.wmv)|" + WindowsMediaVideo;
			ret += "|All Files|" + AllFiles;

			return ret;
		}

		private void CreatePolyChannels(SeqPart parent, string prefix, bool useGroups)
		{
			string dmsg = "";
			//Channel chan;
			int octave = 0;
			int lastOctave = 0;
			PartsCollection parentSubs = new PartsCollection(seq);
			ChannelGroup grp = new ChannelGroup("null");
			if (useGroups)
			{ 
				grp = GetGroup(prefix + octaveNamesA[octave], parent);
				parentSubs = grp.Children;
				//grp.identity.Centiseconds = seq.totalCentiseconds;
			}
			else
			{
				if (parent.TableType == TableType.Track)
				{
					parentSubs = ((Track)parent).Children;
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
						//grp.identity.Centiseconds = seq.totalCentiseconds;
						lastOctave = octave;
						parentSubs = grp.Children;
						dmsg = "Adding Group '" + grp.Name + "' SI:" + grp.SavedIndex;
						dmsg += " Octave #" + octave.ToString();
						dmsg += " to Parent '" + parent.Name + "' SI:" + parent.SavedIndex;
						Debug.WriteLine(dmsg);
					}
				}
				Channel chan = GetChannel(prefix + noteNames[n], parentSubs);
				chan.color = NoteColor(n);
				//chan.identity.Centiseconds = seq.totalCentiseconds;
				noteChannels[n] = chan;
				//grp.Add(chan);
				dmsg = "Adding Channel '" + chan.Name + "' SI:" + chan.SavedIndex;
				dmsg += " Note #" + n.ToString();
				dmsg += " to Parent '" + parentSubs.owner.Name + "' SI:" + parentSubs.owner.SavedIndex;
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
			seq.Children.ReIndex();



		}

		private void AddChildToParent(SeqPart child, SeqPart parent)
		{
			// Tests for, and works with either a track or a channel group as the parent
			if (parent.TableType == TableType.Track)
			{
				Track trk = (Track)parent;
				trk.Children.Add(child);
			}
			if (parent.TableType == TableType.ChannelGroup)
			{
				ChannelGroup grp = (ChannelGroup)parent;
				grp.Children.Add(child);
			}


		}




		private int NoteColor(int note)
		{
			int hexClr = 0;
			int q = note % 12;
			switch (q)
			{
				case 0:
					hexClr = 255; // 0xFF0000; // Red
					break;
				case 1:
					hexClr = 32767; // 0xFF7F00;
					break;
				case 2:
					hexClr = 65535; // 0xFFFF00; // Yellow
					break;
				case 3:
					hexClr = 65407; // 0x7FFF00;
					break;
				case 4:
					hexClr = 65280; // 0x00FF00; // Green
					break;
				case 5:
					hexClr = 8388573; // 0xDDFF7F;
					break;
				case 6:
					hexClr = 16776960; // 0x00FFFF; // Cyan
					break;
				case 7:
					hexClr = 16744192; // 0x007FFF;
					break;
				case 8:
					hexClr = 16711680; // 0x0000FF; // Blue
					break;
				case 9:
					hexClr = 16711807; // 0x7F00FF;
					break;
				case 10:
					hexClr = 16711935; // 0xFF00FF; // Magenta
					break;
				case 11:
					hexClr = 8323327; // 0xFF007F;
					break;
			}

			//int lorClr = RGBtoLOR(hexClr);
			//Debug.Write(note);
			//Debug.Write(" ");
			//Debug.Write(hexClr.ToString("X6"));
			//Debug.Write(" ");
			//Debug.WriteLine(lorClr.ToString());


			//return lorClr;
			return hexClr;
		}

		private int RGBtoLOR(int RGBclr)
		{
			int b = RGBclr & 0xFF;
			int g = RGBclr & 0xFF00;
			g /= 0x100;
			int r = RGBclr & 0xFF0000;
			r /= 0x10000;

			int n = b * 0x10000;
			n += g * 0x100;
			n += r;

			return n;
		}

		#region Checkboxes and Radio Buttons
		private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
		{
			doAutoSave = chkAutoSave.Checked;
			Properties.Settings.Default.doAutoSave = doAutoSave;
			Properties.Settings.Default.Save();
		}

		private void chkNoteOnsets_CheckedChanged(object sender, EventArgs e)
		{
			doNoteOnsets = chkNoteOnsets.Checked;
			Properties.Settings.Default.doNoteOnsets = doNoteOnsets;
			Properties.Settings.Default.Save();
		}

		private void chkBeatsGrid_CheckedChanged(object sender, EventArgs e)
		{
			doBeatGrid = chkBeatsGrid.Checked;
			Properties.Settings.Default.doBeatGrid = doBeatGrid;
			Properties.Settings.Default.Save();
		}

		private void chkPoly_CheckedChanged(object sender, EventArgs e)
		{
			doPoly = chkPoly.Checked;
			Properties.Settings.Default.doPoly = doPoly;
			Properties.Settings.Default.Save();
		}

		private void chkBeatsTrack_CheckedChanged(object sender, EventArgs e)
		{
			doBeatTrack = chkBeatsTrack.Checked;
			Properties.Settings.Default.doBeatTrack = doBeatTrack;
			Properties.Settings.Default.Save();
		}





		#endregion

		private void CentiFixx()
		{
			// If we started from a Channel config, then none of the channels, groups, etc. has a length
			// Fill all those in with the sequence's total Centiseconds (taken from the audio file length)
			for (int ch=0; ch< seq.Channels.Count; ch++)
			{
				seq.Channels[ch].Centiseconds = seq.Centiseconds;
			}
			/*
			for (int rch=0; rch< seq.RGBchannels.Count; rch++)
			{
				seq.RGBchannels[rch].identity.Centiseconds = seq.totalCentiseconds;
			}
			for (int chg=0; chg< seq.ChannelGroups.Count; chg++)
			{
				seq.ChannelGroups[chg].identity.Centiseconds = seq.totalCentiseconds;
			}
			for (int tr=0; tr<seq.Tracks.Count; tr++)
			{
				seq.Tracks[tr].identity.Centiseconds = seq.totalCentiseconds;
			}
			*/
		} // end CentiFixx

		private void chkSpectrogram_CheckedChanged(object sender, EventArgs e)
		{
			//doSpectro = chkSpectrogram.Checked;
			Properties.Settings.Default.doSpectrogram = doSpectro;
			Properties.Settings.Default.Save();
		}

		private void chkConstQ_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void picHoleStart_Click(object sender, EventArgs e)
		{
			SetUseChanConfig(!useChanCfg);
		}

		private void SetUseChanConfig(bool state)
		{
			if (state != useChanCfg)
			{
				useChanCfg = state;
				PlayClickSound();
				if (useChanCfg)
				{
					picSliderStart.Left = 0;
					picHoleStart.Left = 16;
					lblUseConfig.ForeColor = Color.Black;
					lblDontUseConfig.ForeColor = Color.DarkSlateGray;
				}
				else
				{
					picSliderStart.Left = 16;
					picHoleStart.Left = 0;
					lblUseConfig.ForeColor = Color.DarkSlateGray;
					lblDontUseConfig.ForeColor = Color.Black;
				}
				Properties.Settings.Default.useChanCfg = useChanCfg;
				Properties.Settings.Default.Save();
			}
		}

		private void lblUseConfig_Click(object sender, EventArgs e)
		{
			SetUseChanConfig(true);
		}

		private void lblDontUseConfig_Click(object sender, EventArgs e)
		{
			SetUseChanConfig(false);
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		public static void PlayClickSound()
		{
			string sound = Path.GetDirectoryName(Application.ExecutablePath) + "\\Click.wav";
			SoundPlayer player = new SoundPlayer(sound);
			player.Play();
		}

		private void chkAutoLaunch_CheckedChanged(object sender, EventArgs e)
		{
			doAutoLaunch = chkAutoLaunch.Checked;
			Properties.Settings.Default.doAutoLaunch = doAutoLaunch;
			Properties.Settings.Default.Save();
		}

		private void frmTune_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				FirstShow();
			}
		}

		private void FirstShow()
		{
			if (!System.IO.File.Exists(annotatorProgram))
			{
				frmSettings setForm = new frmSettings() { Owner = this };
				//setForm.Parent = this;
				setForm.InitForm(frmSettings.SHOWannotator);
				//setForm.Left = (this.Width - setForm.Width) / 2 + this.Left;
				//setForm.Left = this.Left;
				//setForm.Top = this.Top + 50;

				//setForm.Location = new System.Drawing.Point(this.Location.X + 50, this.Location.Y + 50);

				ImBusy(true);
				DialogResult dr = setForm.ShowDialog(this);
				//if (dr == DialogResult.OK)
				if (setForm.closeMode == DialogResult.OK)
				{
					string initDir = "C:\\";
					string where = "C:\\PortableApps\\SonicAnnotator\\";
					if (Directory.Exists(where))
					{
						initDir = where;
					}
					dlgOpenFile.InitialDirectory = initDir;
					dlgOpenFile.FileName = "sonic-annotator.exe";
					dlgOpenFile.Filter = "Sonic Annotator|(sonic-annotator.exe";
					dlgOpenFile.FilterIndex = 1;
					dlgOpenFile.Multiselect = false;
					dlgOpenFile.SupportMultiDottedExtensions = false;
					dlgOpenFile.CheckPathExists = true;
					dlgOpenFile.CheckFileExists = true;
					dlgOpenFile.DefaultExt = ".exe";
					dlgOpenFile.Title = "Locate Sonic Annotator";
					dr = dlgOpenFile.ShowDialog(this);
					if (dr == DialogResult.OK)
					{
						string anoFile = dlgOpenFile.FileName;
						string anoExe = Path.GetFileName(anoFile).ToLower();
						if (anoExe.CompareTo("sonic-annotator.exe") != 0)
						{
							dr = DialogResult.Cancel;
						}
						else
						{
							annotatorProgram = anoFile;
							Properties.Settings.Default.annotatorProgram = annotatorProgram;
							Properties.Settings.Default.Save();
						}

					}
					if (!System.IO.File.Exists("C:\\Program Files (x86)\\Vamp Plugins\\qm-vamp-plugins.dll"))
					{
						string msg = "Tune-O-Rama cannot continue until the Queen Mary Vamp Plugin has been installed.";
						MessageBox.Show(this, msg, "Please install the Queen Mary Vamp Plugin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						this.Close();

					}
				}
				if (dr == DialogResult.Cancel)
				{
					string msg = "Tune-O-Rama cannot continue until Sonic Annotator and the Queen Mary Vamp Plugin have been installed.";
					MessageBox.Show(this, msg, "Please install Sonic Annotator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					this.Close();
				}
				ImBusy(false);
			}
		}

		private void TransferSettings(frmSettings setForm, bool toForm)
		{
			if (toForm)
			{
				setForm.useOctaveGrouping = useOctaveGrouping;
				setForm.useRampsPoly = useRampsPoly;
				setForm.useSaveFormat = useSaveFormat;
			}
			else // from form
			{
				useOctaveGrouping = setForm.useOctaveGrouping;
				useRampsPoly = setForm.useRampsPoly;
				useSaveFormat = setForm.useSaveFormat;

				Properties.Settings.Default.useOctaveGrouping = useOctaveGrouping;
				Properties.Settings.Default.useRampsPoly = useRampsPoly;
				Properties.Settings.Default.useSaveFormat = useSaveFormat;
				Properties.Settings.Default.Save();
			}




		}

		private void btnGridSettings_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWgridBeats);
		}

		private void chkSpectro_CheckedChanged(object sender, EventArgs e)
		{
			doConstQ = chkConstQ.Checked;
			Properties.Settings.Default.doConstQ = doConstQ;
			Properties.Settings.Default.Save();

		}

		private void btnTrackSettings_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWPoly);
		}

		private void btnSaveOptions_Click_1(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWsave);
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


		private ChannelGroup GetGroup(string groupName, SeqPart parent)
		{
			// Gets existing group specified by Name if it already exists in the track or group
			// Creates it if it does not
			// Can't use 'Find' functions because we only want to look in this one particular track or group

			// Make dummy item list
			PartsCollection Children = new PartsCollection(seq);
			// Get the parent
			TableType parentType = parent.TableType;
			// if parent is a group
			if (parent.TableType == TableType.ChannelGroup)
			{
				// Get it's items saved index list
				Children = ((ChannelGroup)parent).Children;
			}
			else // not a group
			{
				// if parent is a track
				if (parent.TableType == TableType.Track)
				{
					Children = ((Track)parent).Children;
				}
				else // not a track either
				{
					string emsg = "WTF? Parent is not group or track, but should be!";
				} // end if track, or not
			} // end if group, or not

			// Create blank/null return object
			ChannelGroup ret = null;
			int gidx = 0; // loop counter
			// loop while we still have no group, and we haven't reached to end of the list
			while ((ret == null) && (gidx < Children.Count))
			{
				// Get each item's ID
				//int SI = Children.Items[gidx].SavedIndex;
				SeqPart part = Children.Items[gidx];
				if (part.TableType == TableType.ChannelGroup)
				{
					ChannelGroup group = (ChannelGroup)part;
					if (part.Name == groupName)
					{
						ret = group;
						gidx = Children.Count;
					}
				}
				gidx++;
			}

			if (ret== null)
			{
				//int si = seq.Children.HighestSavedIndex + 1;
				ret = seq.CreateChannelGroup(groupName);
				ret.Centiseconds = centiseconds;
				//seq.AddChannelGroup(ret);
				//ID = seq.Children.bySavedIndex[parentSI];
				if (parent.TableType == TableType.Track)
				{
					((Track)parent).Children.Add(ret);
				}
				if (parent.TableType == TableType.ChannelGroup)
				{
					((ChannelGroup)parent).Children.Add(ret);
				}
			}

			return ret;
		}

		private Channel GetChannel(string channelName, PartsCollection parentSubItems)
		{
			// Gets existing channel specified by Name if it already exists in the group
			// Creates it if it does not
			Channel ret = null;
			SeqPart part = null;
			int gidx = 0;
			while ((ret == null) && (gidx < parentSubItems.Count))
			{
				part = parentSubItems.Items[gidx];
				if (part.TableType == TableType.Channel)
				{
					if (part.Name == channelName)
					{
						ret = (Channel)part;
						// Clear any existing effects from a previous run
						if (ret.effects.Count > 0)
						{
							ret.effects = new List<Effect>();
						}
					}
				}
				gidx++;
			}

			if (ret == null)
			{
				//int si = seq.Children.HighestSavedIndex + 1;
				ret = seq.CreateChannel(channelName);
				ret.Centiseconds = centiseconds;
				parentSubItems.Add(ret);
			}

			return ret;
		}

		private void btnSaveSequence_Click(object sender, EventArgs e)
		{
			string filter = "Musical Sequence *.lms|*.lms";
			string idr = utils.DefaultSequencesPath;
			
			string ifile = Path.GetFileNameWithoutExtension(fileCurrent);
			if (ifile.Length < 2)
			{
				ifile = seq.info.music.Title + " by " + seq.info.music.Artist;
			}
			ifile +=  ".lms";

			dlgSaveFile.Filter = filter;
			dlgSaveFile.InitialDirectory = idr;
			dlgSaveFile.FileName = ifile;
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = "Save Sequence As...";
			dlgSaveFile.ValidateNames = true;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				fileSeqSave = dlgSaveFile.FileName;
				txtSaveName.Text = Path.GetFileNameWithoutExtension(fileSeqSave);
				SaveSequence(fileSeqSave);
				if (doAutoLaunch)
				{
					System.Diagnostics.Process.Start(fileSeqSave);
				}


			}
			btnBrowseSequence.Focus();


		}

		private void btnBrowseSequence_Click(object sender, EventArgs e)
		{
			string filt = "Channel Configs *.lcc|*.lcc|Musical Sequences *.lms|*lms";
			string idir = utils.DefaultSequencesPath;

			dlgOpenFile.Filter = filt;
			dlgOpenFile.FilterIndex = 2;		//! Temporary?  Set back to 1 and/or change filter string?
			dlgOpenFile.InitialDirectory = idir;
			dlgOpenFile.FileName = Properties.Settings.Default.fileSeqLast;

			DialogResult dr = dlgOpenFile.ShowDialog(this);
			if (dr==DialogResult.OK)
			{
				fileCurrent = dlgOpenFile.FileName;
				txtFileCurrent.Text = Path.GetFileName(fileCurrent);
				string ex = Path.GetExtension(fileCurrent).ToLower();
				// If they picked an existing musical sequence
				if (ex == ".lms")
				{
					seq.ReadSequenceFile(fileCurrent);
					fileAudioOriginal = seq.info.music.File;
					txtFileAudio.Text = Path.GetFileNameWithoutExtension(fileAudioOriginal);
					grpAudio.Text = " Original Audio File ";
					btnBrowseAudio.Text = "Analyze";
					fileSeqCur = fileCurrent;
					fileChanCfg = "";
					Properties.Settings.Default.fileSeqLast = fileCurrent;
				}
				// If they picked an existing channel config
				if (ex == ".lcc")
				{
					seq.ReadSequenceFile(fileCurrent);
					fileAudioOriginal = "";
					txtFileAudio.Text = "";
					grpAudio.Text = " Select Audio File ";
					btnBrowseAudio.Text = "Browse...";
					fileChanCfg = fileCurrent;
					fileSeqCur = "";
					Properties.Settings.Default.fileChanCfgLast = fileCurrent;
				}
				txtFileCurrent.Text = Path.GetFileName(fileCurrent);
				Properties.Settings.Default.Save();

				grpGrids.Enabled = true;
				grpTracks.Enabled = true;
				grpAudio.Enabled = true;
				btnBrowseAudio.Focus();

			}
		}
	} // end frmTune





} // end namespace tuneorama
