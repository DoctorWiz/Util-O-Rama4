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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace xTune
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

		//private string fileCurrent = "";  // Currently loaded Sequence File
		//private string fileSeqCur = ""; // Last Sequence File Loaded
		private string fileTimingsLast
			= ""; // Last Saved Sequence
		private string pathTimingsLast = utils.ShowDirectory;
		private string pathAudio = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
		//private string fileSeqLast = ""; // last file ran.
		//private string fileAudioOriginal = ""; // the file name extracted from the sequence
		private string fileAudioLast = ""; // Usually the same as fileAudioOriginal, but can be overriden
		private string fileAudioWork = ""; // fileAudioLast copied to the temp folder
																			 //private string fileChanCfg = "";
																			 //private string fileChanCfgLast = "";
		private string originalPath = "";
		private string originalFileName = "";
		private string originalExt = "";
		private string newFile = "";
		//int milliseconds = 0;
		private MRU mruTimings = new MRU("filesTiming", 10);
		private MRU mruAudio = new MRU("filesAudio", 10);
		//private List<TimingGrid> timingsList = new List<TimingGrid>();
		
		//private MRU mruAudio = new MRU();

		//private Sequence4 seq = new Sequence4();
		private bool dirtyTimes = false;
		private const string helpPage = "http://wizlights.com/xUtils/xTune";
		private string applicationName = "xTune";
		private string thisEXE = "xTune.exe";
		//private string userSettingsLocalDir = "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\xTune";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string userSettingsRoamingDir = "C:\\Users\\Wizard\\AppData\\Roaming\\UtilORama\\xTune";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string machineSettingsDir = "C:\\ProgramData\\UtilORama\\xTune";  // Gets overwritten with X:\\ProgramData\\UtilORama\\xTune\\
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Local\\Temp\\UtilORama\\SplitORama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte useSaveFormat = SAVEmixedDisplay;
		private int curStep = 0;

		//private List<TreeNode>[] siNodes;
		private List<List<TreeNode>> siNodes = new List<List<TreeNode>>();

		private bool firstShown = false;
		private TreeNode previousNode = null;

		//private string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";
		private int rlevel = 0;
		bool izwiz = utils.IsWizard;

		//private bool doAutoSave = false;
		//private bool doAutoLaunch = true;
		private int timeSignature = 4;  // Default 4/4 time
		private int startBeat = 1;
		//private bool doNoteOnsets = true;
		//private bool doBeats = true;
		//private bool doPoly = true;
		//private bool doGroups = true;
		//private bool useRampsPoly = false;
		//private bool useRampsBeats = false;
		//private int timesBeatsX = 4;
		//private int trackBeatsX = 4;
		//private bool useOctaveGrouping = true;
		//private bool doConstQ = true;
		//private bool doChroma = true;
		//private bool doSegments = true;
		//private bool doKey = true;
		//private bool doSpeech = true;
		//private bool useChanCfg = false;
		//private int firstCobjIdx = utils.UNDEFINED;
		//private int firstCsavedIndex = utils.UNDEFINED;
		//private Channel[] noteChannels = null;
		//private ChannelGroup[] octaveGroups = null;

		private double panelX = 300;
		private double panelY = 300;
		private double panelAddX = 1.6666;
		private double panelAddY = .7311;
		private Random chaos = null;
		BackgroundWorker myBackgroundWorker;




		[DllImport("shlwapi.dll")]
		static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);





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

			string appFolder = applicationName;
			appFolder = appFolder.Replace("-", "");
			string mySubDir = "\\xUtils\\" + appFolder + "\\";

			string baseDir = Path.GetTempPath();
			tempPath = baseDir + mySubDir;
			if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

			if (izwiz)
			{
				chkReuse.Visible = true;
				chkReuse.Checked = Properties.Settings.Default.reuseFiles;
			}

			chaos = new Random();
			panelX = (this.ClientSize.Width - pnlVamping.Width)/ 2;
			panelY = (this.ClientSize.Height - pnlVamping.Height) / 2;
			panelAddX = chaos.NextDouble() * 2 + .5D;
			panelAddY = chaos.NextDouble() * 2 + .5D;


			myBackgroundWorker = new BackgroundWorker();
			myBackgroundWorker.WorkerReportsProgress = true;
			myBackgroundWorker.WorkerSupportsCancellation = true;
			myBackgroundWorker.DoWork += new DoWorkEventHandler(myBackgroundWorker1_DoWork);
			myBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myBackgroundWorker1_RunWorkerCompleted);
		
		//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		//userSettingsLocalDir = baseDir + mySubDir;
		//if (!Directory.Exists(userSettingsLocalDir)) Directory.CreateDirectory(userSettingsLocalDir);

		//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		//userSettingsRoamingDir = baseDir + mySubDir;
		//if (!Directory.Exists(userSettingsRoamingDir)) Directory.CreateDirectory(userSettingsRoamingDir);

		//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		//machineSettingsDir = baseDir + mySubDir;
		//if (!Directory.Exists(machineSettingsDir)) Directory.CreateDirectory(machineSettingsDir);

		mruAudio = new MRU("Audio", 10);
			mruAudio.ReadFromConfig(Properties.Settings.Default);
			mruAudio.Validate();
			string f = mruAudio.GetItem(0);
			fileAudioLast = mruAudio.GetItem(0);
			if (f.Length > 5)
			{
				f = Path.GetDirectoryName(fileAudioLast);
			}
			else { 
				f = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			}
			pathAudio = f;

			mruTimings = new MRU("Timings", 10);
			mruTimings.ReadFromConfig(Properties.Settings.Default);
			mruTimings.Validate();
			f = mruTimings.GetItem(0);
			if (f.Length < 5)
			{
				f = utils.ShowDirectory;
			}
			pathTimingsLast = f;

			GetTheControlsFromTheHeartOfTheSun();

			int errs = ClearTempDir();

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
					//string fileTimingsLast = Properties.Settings.Default.fileTimingsLast;
					//if (System.IO.File.Exists(fileTimingsLast))
					//{
					//seq.ReadSequenceFile(fileSeqLast);
					//fileCurrent = fileSeqLast;
					//utils.FillChannels(treChannels, seq, siNodes);
					//txtSequenceFile.Text = utils.ShortenLongPath(fileCurrent, 80);
					//}
					//string pathLast = Path.GetDirectoryName(fileTimingsLast);
					//if (Directory.Exists(pathLast)
					//{
					//	pathTimingsLast = pathLast;
					//}

				}
				else
				{
					// 1 and only 1 file specified on command line
					//seq.ReadSequenceFile(batch_fileList[0]);
					//fileSeqLast = batch_fileList[0];
					//utils.FillChannels(treChannels, seq, siNodes);
					//Properties.Settings.Default.fileTimingsLast = fileSeqLast;
					//Properties.Settings.Default.Save();
				}
			}



			//txtFileAudio.Text = utils.ShortenLongPath(fileAudioLast, 80);
			grpAudio.Enabled = true;

			bool gotit = false;
			if (fileAudioLast.Length > 5)
			{
				if (System.IO.File.Exists(fileAudioLast))
				{
					txtFileAudio.Text = ShortenPath(fileAudioLast, 100);
					grpAnalyze.Enabled = true;
					this.Text = "Vamperizer - " + Path.GetFileName(fileAudioLast);
					//grpOptions.Enabled = true;
					SelectStep(2);
					gotit = true;
				}
			}
			if (!gotit)
			{
				btnBrowseAudio.PerformClick();
			}



			ImBusy(false);

		}

		private void GetTheControlsFromTheHeartOfTheSun()
		{
			SetCombo(cboMethodBarsBeats,			Properties.Settings.Default.methodBarsBeats);
			timeSignature =										Properties.Settings.Default.timeSignature;
			if (timeSignature == 3) swTrackBeat.Checked = true; else swTrackBeat.Checked = false;
			startBeat =												Properties.Settings.Default.startBeat;
			vscStartBeat.Value = startBeat;
			SetCombo(cboDetectBarBeats,				Properties.Settings.Default.detectBars);
			chkWhiteBarBeats.Checked =				Properties.Settings.Default.whiteBarsBeats;
			chkBars.Checked =									Properties.Settings.Default.doBars;
			chkBeatsFull.Checked =						Properties.Settings.Default.DoBeatsFull;
			chkBeatsHalf.Checked =						Properties.Settings.Default.doBeatsHalf;
			chkBeatsThird.Checked =						Properties.Settings.Default.doBeatsThird;
			chkBeatsQuarter.Checked =					Properties.Settings.Default.doBeatsQuarter;
			SetCombo(cboAlignBarsBeats,				Properties.Settings.Default.alignBarsBeats);

			SetCombo(cboMethodOnsets,					Properties.Settings.Default.methodOnsets);
			SetCombo(cboDetectOnsets,					Properties.Settings.Default.detectOnsets);
			vscSensitivity.Value =						Properties.Settings.Default.sensitivityOnsets;
			chkWhiteOnsets.Checked =					Properties.Settings.Default.whiteOnsets;
			chkNoteOnsets.Checked =						Properties.Settings.Default.doOnsets;
			SetCombo(cboLabelsOnsets,					Properties.Settings.Default.labelOnsets);
			SetCombo(cboAlignOnsets,					Properties.Settings.Default.alignOnsets);

			SetCombo(cboMethodTranscription,	Properties.Settings.Default.methodTranscribe);
			chkTranscribe.Checked =						Properties.Settings.Default.doTranscribe;
			SetCombo(cboLabelsTranscription,	Properties.Settings.Default.labelTranscribe);
			SetCombo(cboAlignTranscribe,			Properties.Settings.Default.alignTranscribe);

			SetCombo(cboMethodSpectrum,				Properties.Settings.Default.methodSpectrum);
			chkSpectrum.Checked =							Properties.Settings.Default.doSpectrum;
			SetCombo(cboLabelsSpectrum,				Properties.Settings.Default.labelSpectrum);
			SetCombo(cboAlignSpectrum,				Properties.Settings.Default.alignSpectrum);

			SetCombo(cboMethodPitchKey,				Properties.Settings.Default.methodPitchKey);
			chkPitchKey.Checked =							Properties.Settings.Default.doPitchKey;
			SetCombo(cboLabelsPitchKey,				Properties.Settings.Default.labelPitchKey);
			SetCombo(cboAlignPitch,						Properties.Settings.Default.alignPitchKey);

			SetCombo(cboMethodTempo,					Properties.Settings.Default.methodTempo);
			chkTempo.Checked =								Properties.Settings.Default.doTempo;
			SetCombo(cboLabelsTempo,					Properties.Settings.Default.labelTempo);
			SetCombo(cboAlignTempo,						Properties.Settings.Default.alignTempo);

			chkSegments.Checked =							Properties.Settings.Default.doSegments;
			SetCombo(cboAlignSegments,				Properties.Settings.Default.alignSegments);

			chkVocals.Checked =								Properties.Settings.Default.doVocals;
			SetCombo(cboAlignVocals,					Properties.Settings.Default.alignVocals);

			chkFlux.Checked =									Properties.Settings.Default.doFlux;
			chkChords.Checked =								Properties.Settings.Default.doChords;
			chkVocals.Checked =								Properties.Settings.Default.doVocals;

			fileAudioLast =										Properties.Settings.Default.fileAudioLast;

			RecallLastFile();
		}

		private void SetTheControlsForTheHeartOfTheSun()
		{
			Properties.Settings.Default.methodBarsBeats = cboMethodBarsBeats.Text;
			Properties.Settings.Default.timeSignature = timeSignature;
			Properties.Settings.Default.startBeat = startBeat;
			Properties.Settings.Default.detectBars = cboDetectBarBeats.Text;
			Properties.Settings.Default.whiteBarsBeats = chkWhiteBarBeats.Checked;
			Properties.Settings.Default.doBars = chkBars.Checked;
			Properties.Settings.Default.DoBeatsFull = chkBeatsFull.Checked;
			Properties.Settings.Default.doBeatsHalf = chkBeatsHalf.Checked;
			Properties.Settings.Default.doBeatsThird = chkBeatsThird.Checked;
			Properties.Settings.Default.doBeatsQuarter = chkBeatsQuarter.Checked;
			Properties.Settings.Default.alignBarsBeats = cboAlignBarsBeats.Text;

			Properties.Settings.Default.methodOnsets = cboMethodOnsets.Text;
			Properties.Settings.Default.detectOnsets = cboDetectOnsets.Text;
			Properties.Settings.Default.sensitivityOnsets = vscSensitivity.Value;
			Properties.Settings.Default.whiteOnsets = chkWhiteOnsets.Checked;
			Properties.Settings.Default.doOnsets = chkNoteOnsets.Checked;
			Properties.Settings.Default.labelOnsets = cboLabelsOnsets.Text;
			Properties.Settings.Default.alignOnsets = cboAlignOnsets.Text;

			Properties.Settings.Default.methodTranscribe = cboMethodTranscription.Text;
			Properties.Settings.Default.doTranscribe = chkTranscribe.Checked;
			Properties.Settings.Default.labelTranscribe = cboLabelsTranscription.Text;
			Properties.Settings.Default.alignTranscribe = cboAlignTranscribe.Text;

			Properties.Settings.Default.methodSpectrum = cboMethodSpectrum.Text;
			Properties.Settings.Default.doSpectrum = chkSpectrum.Checked;
			Properties.Settings.Default.labelSpectrum = cboLabelsSpectrum.Text;
			Properties.Settings.Default.alignSpectrum = cboAlignSpectrum.Text;

			Properties.Settings.Default.methodPitchKey = cboMethodPitchKey.Text;
			Properties.Settings.Default.doPitchKey = chkPitchKey.Checked;
			Properties.Settings.Default.labelPitchKey = cboLabelsPitchKey.Text;
			Properties.Settings.Default.alignPitchKey = cboAlignPitch.Text;

			Properties.Settings.Default.methodTempo = cboMethodTempo.Text;
			Properties.Settings.Default.doTempo = chkTempo.Checked;
			Properties.Settings.Default.labelTempo = cboLabelsTempo.Text;
			Properties.Settings.Default.alignTempo = cboAlignTempo.Text;

			Properties.Settings.Default.doSegments = chkSegments.Checked;
			Properties.Settings.Default.alignSegments = cboAlignSegments.Text;

			Properties.Settings.Default.doVocals = chkVocals.Checked;
			Properties.Settings.Default.alignVocals = cboAlignVocals.Text;

			Properties.Settings.Default.doFlux = chkFlux.Checked;
			Properties.Settings.Default.doChords = chkChords.Checked;
			Properties.Settings.Default.doVocals = chkVocals.Checked;

			Properties.Settings.Default.fileAudioLast = fileAudioLast;

			Properties.Settings.Default.Save();

			mruAudio.AddNew(fileAudioLast);
			mruAudio.SaveToConfig(Properties.Settings.Default);

		}

		private void RecallLastFile()
		{
			fileAudioLast = Properties.Settings.Default.MRUAudio0;
			if (System.IO.File.Exists(fileAudioLast))
			{
				txtFileAudio.Text = Path.GetFileName(fileAudioLast);
				txtFileAudio.ForeColor = SystemColors.ControlText;
			}
			else
			{
				txtFileAudio.Text = "Select Audio File...";
				txtFileAudio.ForeColor = SystemColors.GrayText;
			}

		}

		private void SelectFileAudio(string theFileAudio)
		{
			string ex = Path.GetExtension(theFileAudio).ToLower();
			bool hasChanged = true;
			if (theFileAudio.ToLower().CompareTo(fileAudioLast.ToLower()) == 0) hasChanged = false;

			if (hasChanged)
			{
				fileAudioLast = theFileAudio;
				Properties.Settings.Default.MRUAudio0 = fileAudioLast;
				if (System.IO.File.Exists(fileAudioLast))
				{
					string sv = Path.GetFileNameWithoutExtension(fileAudioLast);

					sv += ".xtiming";
					txtSaveName.Text = sv;
					//Properties.Settings.Default.filePhooLast = sv;

					//Properties.Settings.Default.fileAudioLast = fileAudioLast;
				}
				txtFileAudio.Text = ShortenPath(fileAudioLast, 100);
				//Properties.Settings.Default.Save();
			}

			bool enable = false;
			if (System.IO.File.Exists(fileAudioLast))
			{
				txtFileAudio.ForeColor = SystemColors.ControlText;
				enable = false;
			}
			else
			{
				txtFileAudio.ForeColor = System.Drawing.Color.DarkRed;
				enable = false;
			}
			btnOK.Enabled = enable;
			grpAudio.Enabled = enable;

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
					if (dirtyTimes)
					{
						//TODO Handle Dirty Sequence
					}
					ImBusy(true);
					string fileCurrent = batch_fileList[0];

					FileInfo fi = new FileInfo(fileCurrent);
					Properties.Settings.Default.fileTimingsLast = fileCurrent;
					Properties.Settings.Default.Save();

					txtFileAudio.Text = ShrinkPath(fileCurrent, 80);
					//utils.FillChannels(treChannels, seq, siNodes, false, false);
					dirtyTimes = false;
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

		private DialogResult AskSaveTimings()
		{
			DialogResult ret = DialogResult.OK;
			if (dirtyTimes)
			{
				string msg = "Your selections have changed.\r\n\r\n";
				msg += "Do you want to save the Selected Timings to a new file?";
				ret = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1);
				if (ret == DialogResult.Yes)
				{
					btnSave.PerformClick();
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

			if (dirtyTimes)
			{
				DialogResult result = AskSaveTimings();
				if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				else
				{
					if (result == DialogResult.Yes)
					{
						btnSave.PerformClick();
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
					int errs = ClearTempDir();
					CloseForm();
				}
				else
				{
					// Close cancelled, reenable controls
					ImBusy(false);
				}
			}
			else
			{
				int errs = ClearTempDir();
				CloseForm();
			}

		}

		private void CloseForm()
		{
			SaveFormPosition();
		}

		private void SaveFormPosition()
		{
			// Get current location, size, and state
			Point myLoc = this.Location;
			Size mySize = this.Size;
			FormWindowState myState = this.WindowState;
			// if minimized or maximized
			if (myState != FormWindowState.Normal)
			{
				// override with the restore location and size
				myLoc = new Point(this.RestoreBounds.X, this.RestoreBounds.Y);
				mySize = new Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
			}

			// Save it for later!
			Properties.Settings.Default.Location = myLoc;
			Properties.Settings.Default.Size = mySize;
			Properties.Settings.Default.WindowState = (int)myState;
			Properties.Settings.Default.Save();
		} // End SaveFormPostion

		private void RestoreFormPosition()
		{
			// Multi-Monitor aware
			// AND NOW with overlooked support for fixed borders!
			// with bounds checking
			// repositions as necessary
			// should(?) be able to handle an additional screen that is no longer there,
			// a repositioned taskbar or gadgets bar,
			// or a resolution change.

			// Note: If the saved position spans more than one screen
			// the form will be repositioned to fit all within the
			// screen containing the center point of the form.
			// Thus, when restoring the position, it will no longer
			// span monitors.
			// This is by design!
			// Alternative 1: Position it entirely in the screen containing
			// the top left corner

			Point savedLoc = Properties.Settings.Default.Location;
			Size savedSize = Properties.Settings.Default.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;
			Point center = new Point(x + w / w, y + h / 2); // Find center point
			int onScreen = 0; // Default to primary screen if not found on screen 2+
			Screen screen = Screen.AllScreens[0];

			// Find which screen it is on
			for (int si = 0; si < Screen.AllScreens.Length; si++)
			{
				// Alternative 1: Change "Contains(center)" to "Contains(savedLoc)"
				if (Screen.AllScreens[si].WorkingArea.Contains(center))
				{
					screen = Screen.AllScreens[si];
					onScreen = si;
				}
			}
			Rectangle bounds = screen.WorkingArea;
			// Alternate 2:
			//Rectangle bounds = Screen.GetWorkingArea(center);

			// Test Horizontal Positioning, correct if necessary
			if (this.MinimumSize.Width > bounds.Width)
			{
				// Houston, we have a problem, monitor is too narrow
				System.Diagnostics.Debugger.Break();
				w = this.MinimumSize.Width;
				// Center it horizontally over the working area...
				//x = (bounds.Width - w) / 2 + bounds.Left;
				// OR position it on left edge
				x = bounds.Left;
			}
			else
			{
				// Should fit horizontally
				// Is it too far left?
				if (x < bounds.Left) x = bounds.Left; // Move over
																							// Is it too wide?
				if (w > bounds.Width) w = bounds.Width; // Shrink it
																								// Is it too far right?
				if ((x + w) > bounds.Right)
				{
					// Keep width, move it over
					x = (bounds.Width - w) + bounds.Left;
				}
			}

			// Test Vertical Positioning, correct if necessary
			if (this.MinimumSize.Height > bounds.Height)
			{
				// Houston, we have a problem, monitor is too short
				System.Diagnostics.Debugger.Break();
				h = this.MinimumSize.Height;
				// Center it vertically over the working area...
				//y = (bounds.Height - h) / 2 + bounds.Top;
				// OR position at the top edge
				y = bounds.Top;
			}
			else
			{
				// Should fit vertically
				// Is it too high?
				if (y < bounds.Top) y = bounds.Top; // Move it down
																						// Is it too tall;
				if (h > bounds.Height) h = bounds.Height; // Shorten it
																									// Is it too low?
				if ((y + h) > bounds.Bottom)
				{
					// Kepp height, raise it up
					y = (bounds.Height - h) + bounds.Top;
				}
			}

			// Position and Size should be safe!
			// Move and Resize the form
			this.SetDesktopLocation(x, y);
			this.Size = new Size(w, h);

			// Window State
			if (savedState == FormWindowState.Maximized)
			{
				if (this.MaximizeBox)
				{
					// Optional.  Personally, I think it should always be reloaded non-maximized.
					//this.WindowState = savedState;
				}
			}
			if (savedState == FormWindowState.Minimized)
			{
				if (this.MinimizeBox)
				{
					// Don't think it's right to reload to a minimized state (confuses the user),
					// but you can enable this if you want.
					//this.WindowState = savedState;
				}
			}

		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string newFileIn;
			string newFileOut;
			string filt = "xLights Timings *.xtimings|*.xtiming";
			string tit = "Save Timings As...";
			string initDir = pathTimingsLast;
			if (initDir.Length < 5) initDir = utils.ShowDirectory;
			string initFile = Path.GetFileNameWithoutExtension(fileAudioLast);
			
			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.FileName = initFile; // utils.ShowDirectory + Path.GetFileNameWithoutExtension(fileAudioLast) + ".xtiming";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.DefaultExt = ".xtiming";
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = tit;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
			//newFileIn = "Part Mother Fucker!!";
			//dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				fileTimingsLast = dlgSaveFile.FileName;
				ExportSelectedTimings(dlgSaveFile.FileName);
				mruTimings.AddNew(fileTimingsLast);
				mruTimings.SaveToConfig(Properties.Settings.Default);

			}
		} // end Save File As

		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWsave);
		}

		private DialogResult ShowSettings(byte initialTab)
		{
			ImBusy(true);
			frmSettings options = new frmSettings();

			//ptions.useRampsBeats = useRampsBeats;
			//options.useRampsPoly = useRampsPoly;
			//options.useOctaveGrouping = useOctaveGrouping;
			options.timesBeatsX = timeSignature;
			//options.trackBeatsX = trackBeatsX;
			options.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
			options.useSaveFormat = useSaveFormat;

			options.InitForm(initialTab);
			options.ShowDialog(this);
			DialogResult dr = options.closeMode;
			if (dr == DialogResult.OK)
			{
				//useRampsBeats = options.useRampsBeats;
				//useRampsPoly = options.useRampsPoly;
				//useOctaveGrouping = options.useOctaveGrouping;
				timeSignature = options.timesBeatsX;
				//trackBeatsX = options.trackBeatsX;
				timeSignature = options.timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				useSaveFormat = options.useSaveFormat;

				//Properties.Settings.Default.phoo9 = useRampsBeats;
				//Properties.Settings.Default.phoo2 = useRampsPoly;
				//Properties.Settings.Default.phoo6 = useOctaveGrouping;
				Properties.Settings.Default.timeSignature = timeSignature;
				//Properties.Settings.Default.phoo8 = trackBeatsX;
				// Time signature does not get saved, reverts back to default 4/4 next time program is run
				//Properties.Settings.Default.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				//Properties.Settings.Default.phoo3 = useSaveFormat;


			}
			ImBusy(false);
			return dr;
		}

		private void SaveSettings(frmSettings settingsForm)
		{
			//useOctaveGrouping = settingsForm.useOctaveGrouping;
			//useRampsPoly = settingsForm.useRampsPoly;
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
				string initDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
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
					fileAudioLast = dlgOpenFile.FileName;
					txtFileAudio.Text = ShrinkPath(fileAudioLast, 100);
					//int errs = ClearTempDir();
					//AnalyzeSong(dlgOpenFile.FileName);
					//grpSequence.Enabled = true;
					grpTimings.Enabled = true;
					//grpOptions.Enabled = true;
					grpAnalyze.Enabled = true;
				} // end if (result = DialogResult.OK)


			}
			else
			{
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

		private Int32 NoteColor(int note)
		{
			// Returned value is LOR color, NOT Web or .Net color!
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
					hexClr = 8388573; // 0x00FF7F;
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

		#region Checkboxes and Radio Buttons

		#endregion

		public static void PlayClickSound()
		{
			string sound = Path.GetDirectoryName(Application.ExecutablePath) + "\\Click.wav";
			SoundPlayer player = new SoundPlayer(sound);
			player.Play();
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
			ConfirmAnnotator();
		}

		private void ConfirmAnnotator()
		{
			annotatorProgram = Properties.Settings.Default.annotatorProgram;
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

							if (Properties.Settings.Default.annotatorProgram.Length < 6)
							{
								Properties.Settings.Default.Upgrade();
								Properties.Settings.Default.Save();
							}


							Properties.Settings.Default.annotatorProgram = annotatorProgram;
							Properties.Settings.Default.Save();

						}

					}
					if (!System.IO.File.Exists("C:\\Program Files (x86)\\Vamp Plugins\\qm-vamp-plugins.dll"))
					{
						string msg = "xTune cannot continue until the Queen Mary Vamp Plugin has been installed.";
						MessageBox.Show(this, msg, "Please install the Queen Mary Vamp Plugin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						this.Close();

					}
				}
				if (dr == DialogResult.Cancel)
				{
					string msg = "xTune cannot continue until Sonic Annotator and the Queen Mary Vamp Plugin have been installed.";
					MessageBox.Show(this, msg, "Please install Sonic Annotator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					this.Close();
				}
				ImBusy(false);
			}

			//! FOR DEBUGGING AND TESTING
			//btnBrowseAudio.PerformClick();
			//! REMOVE WHEN DONE

		}

		private void TransferSettings(frmSettings setForm, bool toForm)
		{
			if (toForm)
			{
				//setForm.useOctaveGrouping = useOctaveGrouping;
				//setForm.useRampsPoly = useRampsPoly;
				setForm.useSaveFormat = useSaveFormat;
			}
			else // from form
			{
				//useOctaveGrouping = setForm.useOctaveGrouping;
				//useRampsPoly = setForm.useRampsPoly;
				useSaveFormat = setForm.useSaveFormat;

				//Properties.Settings.Default.phoo6 = useOctaveGrouping;
				//Properties.Settings.Default.phoo2 = useRampsPoly;
				//Properties.Settings.Default.phoo3 = useSaveFormat;
				Properties.Settings.Default.Save();
			}




		}

		private void btnGridSettings_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWtimesBeats);
		}

		private void btnTrackSettings_Click(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWPoly);
		}

		private void btnSaveOptions_Click_1(object sender, EventArgs e)
		{
			ShowSettings(frmSettings.SHOWsave);
		}

		private void SaveTimings(object sender, EventArgs e)
		{
			string filter = "xLights Timings *.xtiming|*.xtiming";
			string idr = mruTimings.GetItem(0);
			bool saveAsk = true;
			string fileSeqSave = "!!!!!";

			//string ifile = Path.GetFileNameWithoutExtension(fileCurrent);
			string ifile = txtSaveName.Text;
			if (ifile.Length < 2)
			{

				dlgSaveFile.Filter = filter;
				dlgSaveFile.InitialDirectory = idr;
				dlgSaveFile.FileName = ifile;
				dlgSaveFile.FilterIndex = 1;
				dlgSaveFile.OverwritePrompt = true; //  false; // Handled Below
				dlgSaveFile.Title = "Save Timings As...";
				dlgSaveFile.ValidateNames = true;

				while (saveAsk)
				{
					DialogResult result = dlgSaveFile.ShowDialog(this);
					bool doSave = true;
					if (result == DialogResult.OK)
					{
						if (System.IO.File.Exists(dlgSaveFile.FileName))
						{
							string fn = Path.GetFileNameWithoutExtension(dlgSaveFile.FileName).ToLower();
							string ed = "";
							string t = Path.GetFileName(dlgSaveFile.FileName);
							t += " already exists.\r\nDo you want to replace it?";
							DialogResult dr3 = MessageBox.Show(this, t, "Confirm Save As", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
							//if (dr3 == DialogResult.Cancel) saveAsk = true;
							if (dr3 == DialogResult.No) doSave = false;
						}
						if (doSave)
						{
							fileTimingsLast = dlgSaveFile.FileName;
							txtSaveName.Text = Path.GetFileNameWithoutExtension(fileSeqSave);
							saveAsk = false;
						}
					}
				}


			}
		}

		private void SelectStep(int theStep)
		{
			if (theStep != curStep)
			{
				if (theStep == 1)
				{
					lblStep1.ForeColor = System.Drawing.Color.Red;
					grpAudio.Font = new Font(grpSave.Font, FontStyle.Bold);
					grpAudio.Enabled = true;
					btnBrowseAudio.Focus();
				}
				else
				{
					lblStep1.ForeColor = SystemColors.Highlight;
					grpAudio.Font = new Font(grpSave.Font, FontStyle.Regular);
				}

				if (theStep == 2)
				{
					lblStep2A.ForeColor = System.Drawing.Color.Red;
					lblStep2B.ForeColor = System.Drawing.Color.Red;
					grpOptions.Font = new Font(grpSave.Font, FontStyle.Bold);
					grpTimings.Font = new Font(grpSave.Font, FontStyle.Bold);
					grpOptions.Enabled = true;
					grpTimings.Enabled = true;
					chkBars.Focus();
				}
				else
				{
					lblStep2A.ForeColor = SystemColors.Highlight;
					lblStep2B.ForeColor = SystemColors.Highlight;
					grpOptions.Font = new Font(grpSave.Font, FontStyle.Regular);
					grpTimings.Font = new Font(grpSave.Font, FontStyle.Regular);
				}

				if (theStep == 3)
				{
					lblStep3.ForeColor = System.Drawing.Color.Red;
					grpAnalyze.Font = new Font(grpSave.Font, FontStyle.Bold);
					grpAnalyze.Enabled = true;
					btnOK.Focus();
				}
				else
				{
					lblStep3.ForeColor = SystemColors.Highlight;
					grpAnalyze.Font = new Font(grpSave.Font, FontStyle.Regular);
				}

				if (theStep == 4)
				{
					lblStep4.ForeColor = System.Drawing.Color.Red;
					grpSave.Font = new Font(grpSave.Font, FontStyle.Bold);
					grpSave.Enabled = true;
					btnSave.Focus();
				}
				else
				{
					lblStep4.ForeColor = SystemColors.Highlight;
					grpSave.Font = new Font(grpSave.Font, FontStyle.Regular);
				}
			}

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SelectStep(4);
			string musicFile = fileAudioLast;

			if (System.IO.File.Exists(fileAudioLast))
			{
				SetTheControlsForTheHeartOfTheSun();
				// Clean up temp folder from previous run
				//! REMARKED OUT FOR TESTING DEBUGGING, LEAVE FILES
				//int errs = ClearTempDir();
				//! UNREMARK AFTER TESTING!
				AnalyzeSong(fileAudioLast);


				SelectStep(5);
				btnSave.Focus();
			}
			else
			{
				string msg = "Song file '" + Path.GetFileName(fileAudioLast) + "' not found";
				msg += " in '" + Path.GetDirectoryName(fileAudioLast) + "'.";
				MessageBox.Show(this, msg, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			}

		}

		private void txtSaveName_TextChanged(object sender, EventArgs e)
		{

		}

		private void lblSongName_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="absolutepath">The path to compress</param>
		/// <param name="limit">The maximum length</param>
		/// <param name="delimiter">The character(s) to use to imply incompleteness</param>
		/// <returns></returns>
		public string ShrinkPath(string absolutepath, int limit, string delimiter = "…")
		{
			//no path provided
			if (string.IsNullOrEmpty(absolutepath))
			{
				return "";
			}

			var name = Path.GetFileName(absolutepath);
			int namelen = name.Length;
			int pathlen = absolutepath.Length;
			var dir = absolutepath.Substring(0, pathlen - namelen);

			int delimlen = delimiter.Length;
			int idealminlen = namelen + delimlen;

			var slash = (absolutepath.IndexOf("/") > -1 ? "/" : "\\");

			//less than the minimum amt
			if (limit < ((2 * delimlen) + 1))
			{
				return "";
			}

			//fullpath
			if (limit >= pathlen)
			{
				return absolutepath;
			}

			//file name condensing
			if (limit < idealminlen)
			{
				return delimiter + name.Substring(0, (limit - (2 * delimlen))) + delimiter;
			}

			//whole name only, no folder structure shown
			if (limit == idealminlen)
			{
				return delimiter + name;
			}

			return dir.Substring(0, (limit - (idealminlen + 1))) + delimiter + slash + name;
		}

		public static string GetCompactedString(string stringToCompact, Font font, int maxWidth)
		{
			// Copy the string passed in since this string will be
			// modified in the TextRenderer's MeasureText method
			string compactedString = string.Copy(stringToCompact);
			var maxSize = new Size(maxWidth, 0);
			var formattingOptions = TextFormatFlags.PathEllipsis
														| TextFormatFlags.ModifyString;
			TextRenderer.MeasureText(compactedString, font, maxSize, formattingOptions);
			return compactedString;
		}

		//[DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
		//static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

		public static string CompactPath(string longPathName, int wantedLength)
		{
			// NOTE: You need to create the builder with the 
			//       required capacity before calling function.
			// See http://msdn.microsoft.com/en-us/library/aa446536.aspx
			StringBuilder sb = new StringBuilder(wantedLength + 1);
			PathCompactPathEx(sb, longPathName, wantedLength + 1, 0);
			return sb.ToString();
		}
		/// <summary>
		/// Shortens a file path to the specified length
		/// </summary>
		/// <param name="path">The file path to shorten</param>
		/// <param name="maxLength">The max length of the output path (including the ellipsis if inserted)</param>
		/// <returns>The path with some of the middle directory paths replaced with an ellipsis (or the entire path if it is already shorter than maxLength)</returns>
		/// <remarks>
		/// Shortens the path by removing some of the "middle directories" in the path and inserting an ellipsis. If the filename and root path (drive letter or UNC server name)     in itself exceeds the maxLength, the filename will be cut to fit.
		/// UNC-paths and relative paths are also supported.
		/// The inserted ellipsis is not a true ellipsis char, but a string of three dots.
		/// </remarks>
		/// <example>
		/// ShortenPath(@"c:\websites\myproject\www_myproj\App_Data\themegafile.txt", 50)
		/// Result: "c:\websites\myproject\...\App_Data\themegafile.txt"
		/// 
		/// ShortenPath(@"c:\websites\myproject\www_myproj\App_Data\theextremelylongfilename_morelength.txt", 30)
		/// Result: "c:\...gfilename_morelength.txt"
		/// 
		/// ShortenPath(@"\\myserver\theshare\myproject\www_myproj\App_Data\theextremelylongfilename_morelength.txt", 30)
		/// Result: "\\myserver\...e_morelength.txt"
		/// 
		/// ShortenPath(@"\\myserver\theshare\myproject\www_myproj\App_Data\themegafile.txt", 50)
		/// Result: "\\myserver\theshare\...\App_Data\themegafile.txt"
		/// 
		/// ShortenPath(@"\\192.168.1.178\theshare\myproject\www_myproj\App_Data\themegafile.txt", 50)
		/// Result: "\\192.168.1.178\theshare\...\themegafile.txt"
		/// 
		/// ShortenPath(@"\theshare\myproject\www_myproj\App_Data\", 30)
		/// Result: "\theshare\...\App_Data\"
		/// 
		/// ShortenPath(@"\theshare\myproject\www_myproj\App_Data\themegafile.txt", 35)
		/// Result: "\theshare\...\themegafile.txt"
		/// </example>
		public static string ShortenPath(string path, int maxLength)
		{
			string ellipsisChars = "...";
			char dirSeperatorChar = Path.DirectorySeparatorChar;
			string directorySeperator = dirSeperatorChar.ToString();

			//simple guards
			if (path.Length <= maxLength)
			{
				return path;
			}
			int ellipsisLength = ellipsisChars.Length;
			if (maxLength <= ellipsisLength)
			{
				return ellipsisChars;
			}


			//alternate between taking a section from the start (firstPart) or the path and the end (lastPart)
			bool isFirstPartsTurn = true; //drive letter has first priority, so start with that and see what else there is room for

			//vars for accumulating the first and last parts of the final shortened path
			string firstPart = "";
			string lastPart = "";
			//keeping track of how many first/last parts have already been added to the shortened path
			int firstPartsUsed = 0;
			int lastPartsUsed = 0;

			string[] pathParts = path.Split(dirSeperatorChar);
			for (int i = 0; i < pathParts.Length; i++)
			{
				if (isFirstPartsTurn)
				{
					string partToAdd = pathParts[firstPartsUsed] + directorySeperator;
					if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
					{
						break;
					}
					firstPart = firstPart + partToAdd;
					if (partToAdd == directorySeperator)
					{
						//this is most likely the first part of and UNC or relative path 
						//do not switch to lastpart, as these are not "true" directory seperators
						//otherwise "\\myserver\theshare\outproject\www_project\file.txt" becomes "\\...\www_project\file.txt" instead of the intended "\\myserver\...\file.txt")
					}
					else
					{
						isFirstPartsTurn = false;
					}
					firstPartsUsed++;
				}
				else
				{
					int index = pathParts.Length - lastPartsUsed - 1; //-1 because of length vs. zero-based indexing
					string partToAdd = directorySeperator + pathParts[index];
					if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
					{
						break;
					}
					lastPart = partToAdd + lastPart;
					if (partToAdd == directorySeperator)
					{
						//this is most likely the last part of a relative path (e.g. "\websites\myproject\www_myproj\App_Data\")
						//do not proceed to processing firstPart yet
					}
					else
					{
						isFirstPartsTurn = true;
					}
					lastPartsUsed++;
				}
			}

			if (lastPart == "")
			{
				//the filename (and root path) in itself was longer than maxLength, shorten it
				lastPart = pathParts[pathParts.Length - 1];//"pathParts[pathParts.Length -1]" is the equivalent of "Path.GetFileName(pathToShorten)"
				lastPart = lastPart.Substring(lastPart.Length + ellipsisLength + firstPart.Length - maxLength, maxLength - ellipsisLength - firstPart.Length);
			}

			return firstPart + ellipsisChars + lastPart;
		}

		static string PathShortener(string path)
		{
			const string pattern = @"^(w+:|)([^]+[^]+).*([^]+[^]+)$";
			const string replacement = "$1$2...$3";
			if (Regex.IsMatch(path, pattern))
			{
				return Regex.Replace(path, pattern, replacement);
			}
			else
			{
				return path;
			}
		}

		static string TruncatePath(string path, int length)
		{
			StringBuilder sb = new StringBuilder();
			PathCompactPathEx(sb, path, length, 0);
			return sb.ToString();
		}

		private void pnlVamping_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.Blue, 0, 0, pnlVamping.Width - 1, pnlVamping.Height - 1);
		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void chkTiming_CheckedChanged(object sender, EventArgs e)
		{
			SelectStep(2);
		}

		private void vscStartBeat_Scroll(object sender, ScrollEventArgs e)
		{
			startBeat = vscStartBeat.Value;
			txtStartBeat.Text = startBeat.ToString();
			SelectStep(4);
		}

		private void swTrackBeat_CheckedChanged(object sender, EventArgs e)
		{
			if (swTrackBeat.Checked) timeSignature = 3; else timeSignature = 4;
			SelectStep(4);
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			ImBusy(true);

			// NOTE: Default folder for Exports is the folder which was last used for exports
			// This is because the TimingGrid files probably go into the xLights folder (or subfolder of it)
			// Whereas the sequence probably came from the LOR folder (or subfolder of it)
			// And therefore quite different.
			string pathExport = "";
			mruTimings.Validate();
			pathExport = Path.GetDirectoryName(mruTimings.GetItem(0));
			if (pathExport.Length < 4)
			{
				pathExport = utils.ShowDirectory;

				if (!Directory.Exists(pathExport))
				{
					// No longer exists, so reset to blank
					pathExport = "";
				}
			}
			if (pathExport.Length < 4)
			{
				pathExport = Path.GetDirectoryName(mruAudio.GetItem(0));
				if (!Directory.Exists(pathExport))
				{
					// No longer exists, so reset to blank
					pathExport = "";
				}
			}

			// Setup File Save Dialog
			dlgSaveFile.DefaultExt = "xtiming";
			dlgSaveFile.Filter = "xLights Timings (*.xtiming)|*.xTiming|All Files (*.*)|*.*";
			dlgSaveFile.FilterIndex = 0;
			string initFile = Path.GetFileNameWithoutExtension(fileAudioLast);
			dlgSaveFile.FileName = initFile;
			dlgSaveFile.InitialDirectory = pathExport;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			dlgSaveFile.Title = "Save Timings As...";

			DialogResult dr = dlgSaveFile.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				string newFile = dlgSaveFile.FileName;
				ExportSelectedTimings(newFile);
				//utils.PlayNotifyGenericSound();
				SystemSounds.Exclamation.Play();
			} // end dialog result = OK
			ImBusy(false);

		} // end SaveSelectionsAs...

		private void ExportSelectedTimings(string fileName)
		{
			// Get Temp Directory
			bool startWritten = false;
			bool endWritten = false;
			int writeCount = 0;


			// Save Filename for next time (really only need the path, but...)
			fileTimingsLast = fileName;
			txtSaveName.Text = ShortenPath(fileName, 100);
			Properties.Settings.Default.fileTimingsLast = fileTimingsLast;

			if (optMultiPer.Checked) Properties.Settings.Default.saveFormat = 2;
			else Properties.Settings.Default.saveFormat = 1;
			mruTimings.AddNew(fileName);
			mruTimings.SaveToConfig(Properties.Settings.Default);
			// Get path and name for export files
			
			if (chkBars.Checked)
			{
				if (xBars != null)
				{
					if (xBars.effects.Count > 0)
					{
						WriteTimingFile(xBars, fileName);
						writeCount++;
					}
				}
			}
			if (chkBeatsFull.Checked)
			{
				if (xBeatsFull != null)
				{
					if (xBeatsFull.effects.Count > 0)
					{
						WriteTimingFile(xBeatsFull, fileName);
						writeCount++;
					}
				}
			}
			if (chkBeatsHalf.Checked)
			{
				if (xBeatsHalf != null)
				{
					if (xBeatsHalf.effects.Count > 0)
					{
						WriteTimingFile(xBeatsHalf, fileName);
						writeCount++;
					}
				}
			}
			if (chkBeatsThird.Checked)
			{
				if (xBeatsThird != null)
				{
					if (xBeatsThird.effects.Count > 0)
					{
						WriteTimingFile(xBeatsThird, fileName);
						writeCount++;
					}
				}
			}
			if (chkBeatsQuarter.Checked)
			{
				if (xBeatsQuarter != null)
				{
					if (xBeatsQuarter.effects.Count > 0)
					{
						WriteTimingFile(xBeatsQuarter, fileName);
						writeCount++;
					}
				}
			}
			if (chkNoteOnsets.Checked)
			{
				if (xOnsets != null)
				{
					if (xOnsets.effects.Count > 0)
					{
						WriteTimingFile(xOnsets, fileName);
						writeCount++;
					}
				}
			}
			if (chkTranscribe.Checked)
			{
				if (xTranscription != null)
				{
					if (xTranscription.effects.Count > 0)
					{
						WriteTimingFile(xTranscription, fileName);
					}
				}
			}


			//}
			pnlStatus.Text = writeCount.ToString() + " files writen.";
		} // end SaveSelections

		private void WriteTimingFile(TimingGrid timings, string fileName)
		{
			string timingsTemp = "";
			string tempDir = System.IO.Path.GetTempPath();
			//  Allocate a stream writer
			StreamWriter writer = null;
			string expPath = Path.GetDirectoryName(fileName) + "\\";
			string expBase = Path.GetFileNameWithoutExtension(fileName);
			string expFile = "x.x";

			pnlStatus.Text = "Writing " + timings.timingName + "...";
		staStatus.Refresh();
		// Create a temporary streamwriter file
		timingsTemp = tempDir + Path.GetRandomFileName();
		writer = new StreamWriter(timingsTemp);
		string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		writer.WriteLine(lineOut);

		// Write this xTiming to an export file
		string xDat = timings.LineOut();
		writer.WriteLine(xDat);

		writer.Close();
		expFile = expPath + expBase + " - " + timings.timingName + ".xtiming";
		if (System.IO.File.Exists(expFile))
		{
			// If already exists, delete it
			//TODO: Add Exception Catch
			System.IO.File.Delete(expFile);
		}
		// Copy the tempfile to the new file name and delete the old temp file
		System.IO.File.Copy(timingsTemp, expFile);
		System.IO.File.Delete(timingsTemp);

	}

		private void lblHelpOnsets_Click(object sender, EventArgs e)
		{
			// Go to Vamp Plugins Web page
			System.Diagnostics.Process.Start("https://vamp-plugins.org/plugin-doc/qm-vamp-plugins.html#qm-onsetdetector");
		}

		private void cboOnsetsMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool b = false;
			if ((cboMethodOnsets.SelectedIndex == 0) || (cboMethodOnsets.SelectedIndex == 2)) b = true;
			lblDetectOnsets.Enabled = b;
			cboDetectOnsets.Enabled = b;
			if (cboMethodOnsets.SelectedIndex == 0) b = true; else b = false;
			pnlOnsetSensitivity.Enabled = b;
			chkWhiteOnsets.Enabled = b;
		}

		private void vscStartBeat_Scroll_1(object sender, ScrollEventArgs e)
		{
			txtStartBeat.Text = vscStartBeat.Value.ToString();
		}

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			txtOnsetSensitivity.Text = vscSensitivity.Value.ToString();
		}

		private void pnlNotes_Paint(object sender, PaintEventArgs e)
		{
			Font fnt = new Font("Segoe UI Symbol", 15.75F, FontStyle.Regular);
			// Bars
			TextRenderer.DrawText(e.Graphics, "𝄁", fnt, new Point(0, -4), SystemColors.ControlText);
			// Beats - Quarter Note
			TextRenderer.DrawText(e.Graphics, "𝅘𝅥", fnt, new Point(0, 17), SystemColors.ControlText);
			// Half Beats - Eighth Note
			TextRenderer.DrawText(e.Graphics, "𝅘𝅥𝅮", fnt, new Point(0, 38), SystemColors.ControlText);
			// Quarter Beats - Sixteenth Note
			TextRenderer.DrawText(e.Graphics, "𝅘𝅥𝅯", fnt, new Point(0, 80), SystemColors.ControlText);
			// Full Note
			fnt = new Font("Segoe UI Symbol", 24F, FontStyle.Regular);
			TextRenderer.DrawText(e.Graphics, "𝅗", fnt, new Point(3, -19), SystemColors.ControlText);
		}

		private void cboBarBeatMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool b = false;
			if (cboMethodBarsBeats.SelectedIndex == 1) ;
			{
				lblDetectBarBeats.Enabled = b;
				cboDetectBarBeats.Enabled = b;
				chkWhiteBarBeats.Enabled = b;
			}
		}

		private void labelTweaks_Click(object sender, EventArgs e)
		{
			Form ft = new frmTweakInfo();
			ft.ShowDialog(this);
		}

		private void btnResetDefaults_Click(object sender, EventArgs e)
		{
			cboMethodBarsBeats.SelectedIndex = 0;
			swTrackBeat.Checked = false;
			timeSignature = 4;
			vscStartBeat.Value = 1;
			startBeat = 1;
			cboDetectBarBeats.SelectedIndex = 0;
			chkWhiteBarBeats.Checked = false;
			chkBars.Checked = true;
			chkBeatsFull.Checked = true;
			chkBeatsHalf.Checked = true;
			chkBeatsThird.Checked = false;
			chkBeatsQuarter.Checked = true;
			cboAlignBarsBeats.SelectedIndex = 1; // 25ms 40fps

			cboMethodOnsets.SelectedIndex = 0;
			cboDetectOnsets.SelectedIndex = 0;
			vscSensitivity.Value = 50;
			chkWhiteOnsets.Checked = false;
			chkNoteOnsets.Checked = true;
			cboLabelsOnsets.SelectedIndex = 1;
			cboAlignOnsets.SelectedIndex = 5; // Quarter Beats Sixteenth Notes

			cboMethodTranscription.SelectedIndex = 0;
			chkTranscribe.Checked = true;
			cboLabelsTranscription.SelectedIndex = 0;
			cboAlignTranscribe.SelectedIndex = 6; // Note Onsets

			cboMethodSpectrum.SelectedIndex = 0;
			chkSpectrum.Checked = false;
			cboLabelsSpectrum.SelectedIndex = 1;
			cboAlignSpectrum.SelectedIndex = 6;

			cboMethodPitchKey.SelectedIndex = 0;
			chkPitchKey.Checked = true;
			cboLabelsPitchKey.SelectedIndex = 1;
			cboAlignPitch.SelectedIndex = 3; // Bars

			cboMethodTempo.SelectedIndex = 0;
			chkTempo.Checked = false;
			cboAlignTempo.SelectedIndex = 1;
			cboAlignTempo.SelectedIndex = 3; // Bars

			chkSegments.Checked = false;
			cboAlignSegments.SelectedIndex = 3; // Bars

			chkVocals.Checked = false;
			cboAlignVocals.SelectedIndex = 0; // None

		}

		private int SetCombo(ComboBox cboSetting, string selection)
		{
			int i1 = cboSetting.FindStringExact(selection);
			if (i1<0)
			{
				if (selection.Length > 3)
				{
					string mat = selection.Trim().ToLower().Substring(0, 4);
					for (int s = 0; s < cboSetting.Items.Count; s++)
					{
						string tem = cboSetting.Items[s].ToString().Trim().ToLower().Substring(0, 4);
						if (mat.CompareTo(tem) == 0)
						{
							i1 = s;
							s = 99999;  // force exit of loop
						}
					}
				}
			}
			if (i1>=0)
				{
				cboSetting.SelectedIndex = i1;
			}
			return i1;
		}

		private void chkReuse_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.reuseFiles = chkReuse.Checked;
			Properties.Settings.Default.Save();
		}

		private void tmrAni_Tick(object sender, EventArgs e)
		{
			panelX += panelAddX;
			if (panelX < 0)
			{
				panelAddX = chaos.NextDouble() * 2 + .5D;
			}
			if (panelX + pnlVamping.Width > this.ClientSize.Width)
			{
				panelAddX = chaos.NextDouble() * -2 - .5D;
			}
			panelY += panelAddY;
			if (panelY < 0)
			{
				panelAddY = chaos.NextDouble() * 2 + .5D;
			}
			if (panelY > this.ClientSize.Height + pnlVamping.Height)
			{
				panelAddY = chaos.Next() * -2 - .5D;
			}
			//pnlVamping.Left = (int)panelX;
			//pnlVamping.Top = (int)panelY;
			pnlVamping.Location = new Point((int)panelX, (int)panelY);
			pnlVamping.Refresh();

		}

		private void WaitBounce()
		{
			panelX += panelAddX;
			if (panelX < 0)
			{
				panelAddX = chaos.NextDouble() * 2 + .5D;
			}
			if (panelX + pnlVamping.Width > this.ClientSize.Width)
			{
				panelAddX = chaos.NextDouble() * -2 - .5D;
			}
			panelY += panelAddY;
			if (panelY < 0)
			{
				panelAddY = chaos.NextDouble() * 2 + .5D;
			}
			if (panelY > this.ClientSize.Height + pnlVamping.Height)
			{
				panelAddY = chaos.Next() * -2 - .5D;
			}
			//pnlVamping.Left = (int)panelX;
			//pnlVamping.Top = (int)panelY;
			pnlVamping.Location = new Point((int)panelX, (int)panelY);
			pnlVamping.Refresh();


			System.Threading.Thread.Sleep(50);

		}


	} // End Form Partial Class
}// end namespace xTune
