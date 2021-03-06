using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Configuration;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using LORUtils;
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

namespace VampORama
{
	public partial class frmVamp : Form
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
		private string pathTimingsLast = xUtils.ShowDirectory;
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
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private MRU mruTimings = new MRU(heartOfTheSun, "filesTiming", 10);
		private MRU mruAudio = new MRU(heartOfTheSun, "filesAudio", 10);
		//private List<xTimings> timingsList = new List<xTimings>();

		//private MRU mruAudio = new MRU();

		//private Sequence4 seq = new Sequence4();
		private bool dirtyTimes = false;
		private const string helpPageV = "http://wizlights.com/xUtils/Vamperizer";
		private const string helpPageL = "http://wizlights.com/utilorama/vamporama";

		//private string applicationName = "Vamperizer";
		private string thisEXE = "VampORama.exe";
		//private string userSettingsLocalDir = "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string userSettingsRoamingDir = "C:\\Users\\Wizard\\AppData\\Roaming\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string machineSettingsDir = "C:\\ProgramData\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\ProgramData\\UtilORama\\Vamperizer\\
		private string pathWork = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Local\\Temp\\UtilORama\\SplitORama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte useSaveFormat = SAVEmixedDisplay;
		private int curStep = 0;

		public bool lightORamaInstalled = false;
		public bool xLightsInstalled = false;
		public string applicationName = "Vamp-O-Rama";  // Or changed to "Vamperizer" if only xLights is installed (but not Light-O-Rama)
		public bool vampMode = false; // Set to true if xLightsInstalled && !lightORamaInstalled  (xLights only, no LOR)

		//private List<TreeNode>[] siNodes;
		private List<List<TreeNode>> siNodes = new List<List<TreeNode>>();

		private bool firstShown = false;
		private TreeNode previousNode = null;

		//private string annotatorProgram = "C:\\PortableApps\\SonicAnnotator\\sonic-annotator.exe";
		private int rlevel = 0;
		bool izwiz = xUtils.IsWizard;

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

		private const int STEP_SelectAudioFile = 1;
		private const int STEP_SetOptions = 2;
		private const int STEP_AnalyzeAudio = 3;
		private const int STEP_SaveTimings = 4;

		private frmOutputLog logWindow = null;




		//public frmConsole consoleWindow;


		[DllImport("shlwapi.dll")]
		static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);





		public frmVamp()
		{
			InitializeComponent();
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			if (vampMode)
			{
				System.Diagnostics.Process.Start(helpPageV);
			}
			else
			{
				System.Diagnostics.Process.Start(helpPageL);
			}

		}

		private void frmVamp_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{
			string mySubDir = "UtilORama\\";
			//string appFolder = "VampORama";
			ImBusy(true);
			RestoreFormPosition();

			string sfoo = xUtils.ShowDirectory;
			if (sfoo.Length > 3) xLightsInstalled = true;
			sfoo = utils.DefaultSequencesPath;
			if (sfoo.Length > 3) lightORamaInstalled = true;
			if (xLightsInstalled && !lightORamaInstalled)
			{
				vampMode = true;
				applicationName = "Vamperizer";
				mySubDir = "xUtils\\";
				//appFolder = "Vamperizer";
			}

			if (xLightsInstalled && lightORamaInstalled)
			{
				grpPlatform.Visible = true;
			}
			else
			{
				grpPlatform.Visible = false;
				int moveBy = grpBarsBeats.Top - grpPlatform.Top;
				grpBarsBeats.Top -= moveBy;
				grpOnsets.Top -= moveBy;
				grpTranscription.Top -= moveBy;
				grpPitchKey.Top -= moveBy;
				grpSegments.Top -= moveBy;
				grpSpectrum.Top -= moveBy;
				grpVocals.Top -= moveBy;
				grpTempo.Top -= moveBy;

			}
			if (!lightORamaInstalled) chkLOR.Checked = false;
			if (!xLightsInstalled) chkxLights.Checked = false;
			grpSaveLOR.Visible = lightORamaInstalled;
			grpSavex.Visible = xLightsInstalled;

			this.Text = applicationName;
			string appFolder = applicationName;
			appFolder = appFolder.Replace("-", "");
			mySubDir = mySubDir + appFolder + "\\";
			string baseDir = Path.GetTempPath();
			if (baseDir.Substring(baseDir.Length - 1, 1) != "\\") baseDir += "\\";
			pathWork = baseDir + mySubDir;
			if (!Directory.Exists(pathWork)) Directory.CreateDirectory(pathWork);

			if (izwiz)
			{
				chkReuse.Visible = true;
				chkReuse.Checked = heartOfTheSun.reuseFiles;
				lblWorkFolder.Visible = true;
				btnCmdTemp.Visible = true;
				btnExploreTemp.Visible = true;
				btnExploreVamp.Visible = true;
			}

			chaos = new Random();
			panelX = (this.ClientSize.Width - pnlVamping.Width) / 2;
			panelY = (this.ClientSize.Height - pnlVamping.Height) / 2;
			panelAddX = chaos.NextDouble() * 2 + .5D;
			panelAddY = chaos.NextDouble() * 2 + .5D;


			//myBackgroundWorker = new BackgroundWorker();
			//myBackgroundWorker.WorkerReportsProgress = true;
			//myBackgroundWorker.WorkerSupportsCancellation = true;
			//myBackgroundWorker.DoWork += new DoWorkEventHandler(myBackgroundWorker1_DoWork);
			//myBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myBackgroundWorker1_RunWorkerCompleted);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			//userSettingsLocalDir = baseDir + mySubDir;
			//if (!Directory.Exists(userSettingsLocalDir)) Directory.CreateDirectory(userSettingsLocalDir);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			//userSettingsRoamingDir = baseDir + mySubDir;
			//if (!Directory.Exists(userSettingsRoamingDir)) Directory.CreateDirectory(userSettingsRoamingDir);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			//machineSettingsDir = baseDir + mySubDir;
			//if (!Directory.Exists(machineSettingsDir)) Directory.CreateDirectory(machineSettingsDir);

			// mruAudio = new MRU(heartOfTheSun, "Audio", 10);
			//mruAudio.ReadFromConfig(Properties.Settings.Default);
			//mruAudio.Validate();
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

			//mruTimings = new MRU(heartOfTheSun, "Timings", 10);
			//mruTimings.ReadFromConfig();
			//mruTimings.Validate();
			f = mruTimings.GetItem(0);
			if (f.Length < 5)
			{
				f = xUtils.ShowDirectory;
			}
			pathTimingsLast = f;

			GetTheControlsFromTheHeartOfTheSun();

			int errs = xUtils.ClearTempDir(pathWork);

			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			//TODO: These may get overridden by command line arguments (not yet supported)
			//! EXAMPLES FROM SPLIT-O-RAMA
			//useFuzzy = heartOfTheSun.useFuzzy;
			//minPreMatchScore = heartOfTheSun.preMatchScore;
			//saveFormat = heartOfTheSun.SaveFormat;
			//if ((minPreMatchScore < 500) || (minPreMatchScore > 1000))
			//{
			//	minPreMatchScore = 800;
			//}
			//minFinalMatchScore = heartOfTheSun.finalMatchScore;
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
					//string fileTimingsLast = heartOfTheSun.fileTimingsLast;
					//if (System.IO.File.Exists(fileTimingsLast))
					//{
					//seq.ReadSequenceFile(fileSeqLast);
					//fileCurrent = fileSeqLast;
					//utils.FillChannels(treChannels, seq, siNodes);
					//txtSequenceFile.Text = xUtils.ShortenLongPath(fileCurrent, 80);
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
					//heartOfTheSun.fileTimingsLast = fileSeqLast;
					//heartOfTheSun.Save();
				}
			}



			//txtFileAudio.Text = xUtils.ShortenLongPath(fileAudioLast, 80);
			grpAudio.Enabled = true;

			bool gotit = false;
			if (fileAudioLast.Length > 5)
			{
				if (System.IO.File.Exists(fileAudioLast))
				{
					txtFileAudio.Text = ShortenPath(fileAudioLast, 100);
					grpAnalyze.Enabled = true;
					this.Text = applicationName + " - " + Path.GetFileName(fileAudioLast);
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

		///////////////////////////////////////////////////////////////////////////////////
		/// Restore the saved last state of the form controls from Properties.Settings ///
		/////////////////////////////////////////////////////////////////////////////////
		private void GetTheControlsFromTheHeartOfTheSun()
		{
			SetTheControlsToDefaults();
			bool foo = false;

			SetCombo(cboMethodBarsBeats, heartOfTheSun.methodBarsBeats);
			timeSignature = heartOfTheSun.timeSignature;
			if (timeSignature == 3) swTrackBeat.Checked = true; else swTrackBeat.Checked = false;
			startBeat = heartOfTheSun.startBeat;
			vscStartBeat.Value = startBeat;
			SetCombo(cboDetectBarBeats, heartOfTheSun.detectBars);
			chkWhiteBarBeats.Checked = heartOfTheSun.whiteBarsBeats;
			chkBars.Checked = heartOfTheSun.doBars;
			chkBeatsFull.Checked = heartOfTheSun.DoBeatsFull;
			chkBeatsHalf.Checked = heartOfTheSun.doBeatsHalf;
			chkBeatsThird.Checked = heartOfTheSun.doBeatsThird;
			chkBeatsQuarter.Checked = heartOfTheSun.doBeatsQuarter;
			//SetCombo(cboAlignBarsBeats,				heartOfTheSun.alignBarsBeats);

			SetCombo(cboOnsetsPlugin, heartOfTheSun.methodOnsets);
			SetCombo(cboOnsetsDetect, heartOfTheSun.detectOnsets);
			vscSensitivity.Value = heartOfTheSun.sensitivityOnsets;
			chkOnsetsWhite.Checked = heartOfTheSun.whiteOnsets;
			chkNoteOnsets.Checked = heartOfTheSun.doOnsets;
			SetCombo(cboOnsetsLabels, heartOfTheSun.labelOnsets);
			//SetCombo(cboAlignOnsets,					heartOfTheSun.alignOnsets);
			SetCombo(cboStepSize, heartOfTheSun.stepSize);
			swRamps.Checked = heartOfTheSun.Ramps;

			SetCombo(cboMethodTranscription, heartOfTheSun.methodTranscribe);
			chkTranscribe.Checked = heartOfTheSun.doTranscribe;
			SetCombo(cboLabelsTranscription, heartOfTheSun.labelTranscribe);
			SetCombo(cboAlignTranscribe, heartOfTheSun.alignTranscribe);

			SetCombo(cboMethodSpectrum, heartOfTheSun.methodSpectrum);
			chkSpectrum.Checked = heartOfTheSun.doSpectrum;
			SetCombo(cboLabelsSpectrum, heartOfTheSun.labelSpectrum);
			SetCombo(cboAlignSpectrum, heartOfTheSun.alignSpectrum);

			SetCombo(cboMethodPitchKey, heartOfTheSun.methodPitchKey);
			chkPitchKey.Checked = heartOfTheSun.doPitchKey;
			SetCombo(cboLabelsPitchKey, heartOfTheSun.labelPitchKey);
			SetCombo(cboAlignPitch, heartOfTheSun.alignPitchKey);

			SetCombo(cboMethodTempo, heartOfTheSun.methodTempo);
			chkTempo.Checked = heartOfTheSun.doTempo;
			SetCombo(cboLabelsTempo, heartOfTheSun.labelTempo);
			SetCombo(cboAlignTempo, heartOfTheSun.alignTempo);

			chkSegments.Checked = heartOfTheSun.doSegments;
			SetCombo(cboAlignSegments, heartOfTheSun.alignSegments);
			SetCombo(cboMethodSegments, heartOfTheSun.methodSegments);
			SetCombo(cboLabelsSegments, heartOfTheSun.labelSegments);

			chkVocals.Checked = heartOfTheSun.doVocals;
			SetCombo(cboAlignVocals, heartOfTheSun.alignVocals);

			chkFlux.Checked = heartOfTheSun.doFlux;
			chkChords.Checked = heartOfTheSun.doChords;
			chkVocals.Checked = heartOfTheSun.doVocals;

			fileAudioLast = heartOfTheSun.fileAudioLast;

			chkLOR.Checked = heartOfTheSun.UseLOR;
			chkxLights.Checked = heartOfTheSun.UsexLights;
			chkAutolaunch.Checked = heartOfTheSun.autoLaunch;

			SetAlignments();


			RecallLastFile();
		}

		/////////////////////////////////////////////////////////////////////////////
		/// Save the current state of the form controls into Properties.Settings ///
		///       (Heart of the Sun = Properties.Settings)											///
		//////////////////////////////////////////////////////////////////////////
		private void SetTheControlsForTheHeartOfTheSun()
		{
			Properties.Settings heartOfTheSun = Properties.Settings.Default;

			heartOfTheSun.methodBarsBeats = cboMethodBarsBeats.Text;
			heartOfTheSun.timeSignature = timeSignature;
			heartOfTheSun.startBeat = startBeat;
			heartOfTheSun.detectBars = cboDetectBarBeats.Text;
			heartOfTheSun.whiteBarsBeats = chkWhiteBarBeats.Checked;
			heartOfTheSun.doBars = chkBars.Checked;
			heartOfTheSun.DoBeatsFull = chkBeatsFull.Checked;
			heartOfTheSun.doBeatsHalf = chkBeatsHalf.Checked;
			heartOfTheSun.doBeatsThird = chkBeatsThird.Checked;
			heartOfTheSun.doBeatsQuarter = chkBeatsQuarter.Checked;
			heartOfTheSun.alignBarsBeats = cboAlignBarsBeats.Text;
			heartOfTheSun.Ramps = swRamps.Checked;

			heartOfTheSun.methodOnsets = cboOnsetsPlugin.Text;
			heartOfTheSun.detectOnsets = cboOnsetsDetect.Text;
			heartOfTheSun.sensitivityOnsets = vscSensitivity.Value;
			heartOfTheSun.whiteOnsets = chkOnsetsWhite.Checked;
			heartOfTheSun.doOnsets = chkNoteOnsets.Checked;
			heartOfTheSun.labelOnsets = cboOnsetsLabels.Text;
			heartOfTheSun.alignOnsets = cboAlignOnsets.Text;
			heartOfTheSun.stepSize = cboStepSize.Text;

			heartOfTheSun.methodTranscribe = cboMethodTranscription.Text;
			heartOfTheSun.doTranscribe = chkTranscribe.Checked;
			heartOfTheSun.labelTranscribe = cboLabelsTranscription.Text;
			heartOfTheSun.alignTranscribe = cboAlignTranscribe.Text;

			heartOfTheSun.methodSpectrum = cboMethodSpectrum.Text;
			heartOfTheSun.doSpectrum = chkSpectrum.Checked;
			heartOfTheSun.labelSpectrum = cboLabelsSpectrum.Text;
			heartOfTheSun.alignSpectrum = cboAlignSpectrum.Text;

			heartOfTheSun.methodPitchKey = cboMethodPitchKey.Text;
			heartOfTheSun.doPitchKey = chkPitchKey.Checked;
			heartOfTheSun.labelPitchKey = cboLabelsPitchKey.Text;
			heartOfTheSun.alignPitchKey = cboAlignPitch.Text;

			heartOfTheSun.methodTempo = cboMethodTempo.Text;
			heartOfTheSun.doTempo = chkTempo.Checked;
			heartOfTheSun.labelTempo = cboLabelsTempo.Text;
			heartOfTheSun.alignTempo = cboAlignTempo.Text;

			heartOfTheSun.doSegments = chkSegments.Checked;
			heartOfTheSun.methodSegments = cboMethodSegments.Text;
			heartOfTheSun.alignSegments = cboAlignSegments.Text;
			heartOfTheSun.labelSegments = cboLabelsSegments.Text;

			heartOfTheSun.doVocals = chkVocals.Checked;
			heartOfTheSun.alignVocals = cboAlignVocals.Text;

			heartOfTheSun.doFlux = chkFlux.Checked;
			heartOfTheSun.doChords = chkChords.Checked;
			heartOfTheSun.doVocals = chkVocals.Checked;

			heartOfTheSun.fileAudioLast = fileAudioLast;

			heartOfTheSun.UseLOR = chkLOR.Checked;
			heartOfTheSun.UsexLights = chkxLights.Checked;
			heartOfTheSun.autoLaunch = chkAutolaunch.Checked;

			heartOfTheSun.Save();

			mruAudio.AddNew(fileAudioLast);
			if (mruAudio.appSettings == null)
			{
				mruAudio.appSettings = heartOfTheSun;
			}
			mruAudio.SaveToConfig();

		}

		private void RecallLastFile()
		{
			fileAudioLast = heartOfTheSun.MRUfilesAudio0;
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
				heartOfTheSun.MRUfilesAudio0 = fileAudioLast;
				if (System.IO.File.Exists(fileAudioLast))
				{
					string sv = Path.GetFileNameWithoutExtension(fileAudioLast);

					sv += ".xtiming";
					txtSaveNamexL.Text = sv;
					//heartOfTheSun.filePhooLast = sv;

					//heartOfTheSun.fileAudioLast = fileAudioLast;
				}
				txtFileAudio.Text = ShortenPath(fileAudioLast, 100);
				//heartOfTheSun.Save();
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
				if (arg.Substring(4).IndexOf(".") > xUtils.UNDEFINED) isFile++;  // contains a period
				if (xUtils.InvalidCharacterCount(arg) == 0) isFile++;
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
				//if (arg.Substring(4).IndexOf(".") > xUtils.UNDEFINED) isFile++;  // contains a period
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
					heartOfTheSun.fileTimingsLast = fileCurrent;
					heartOfTheSun.Save();

					txtFileAudio.Text = ShrinkPath(fileCurrent, 80);
					//xUtils.FillChannels(treChannels, seq, siNodes, false, false);
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
					btnSavexL.PerformClick();
					ret = DialogResult.OK;
				}
				if (ret == DialogResult.No)
				{
					ret = DialogResult.OK;
				}
			}
			return ret;
		}

		private void frmVamp_FormClosing(object sender, FormClosingEventArgs e)
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
						e.Cancel = true;
						if (lightORamaInstalled)
						{
							if (chkLOR.Checked)
							{
								btnSaveSeq.PerformClick();
							}
						}
						if (xLightsInstalled)
						{
							if (chkxLights.Checked)
							{
								btnSavexL.PerformClick();
							}
						}
						e.Cancel = false;
					}
				}

				if (!e.Cancel)
				{
					//string where = pathWork; // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					//string when = pathWork;
					//if (!Directory.Exists(where))
					//	{
					//		Directory.CreateDirectory(where);
					//}
					//string what = "fileSelLast.ChSel";
					//string file = where +  what;
					//SaveSelections(file);
					if (!chkReuse.Checked)
					{
						int errs = xUtils.ClearTempDir(pathWork);
					}
					//CloseForm();
					SetTheControlsForTheHeartOfTheSun();
				}
				else
				{
					// Close cancelled, reenable controls
					ImBusy(false);
				}
			}
			else
			{
				if (!chkReuse.Checked)
				{
					int errs = xUtils.ClearTempDir(pathWork);
				}
				//CloseForm();
				SetTheControlsForTheHeartOfTheSun();
			}
			//CloseForm();

		}

		private void CloseForm()
		{
			SaveFormPosition();
			//SetTheControlsForTheHeartOfTheSun();
			//this.Close();
			//Application.Exit();
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
			int x = this.Left;
			heartOfTheSun.Location = myLoc;
			heartOfTheSun.Size = mySize;
			heartOfTheSun.WindowState = (int)myState;
			heartOfTheSun.Save();
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

			Point savedLoc = heartOfTheSun.Location;
			Size savedSize = heartOfTheSun.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)heartOfTheSun.WindowState;
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

		private void btnSavexL_Click(object sender, EventArgs e)
		{
			string newFileIn;
			string newFileOut;
			string filt = "xLights Timings *.xtimings|*.xtiming";
			string tit = "Save Timings As...";
			string initDir = Properties.Settings.Default.LastxLightsTimingsSavePath;
			if (initDir.Length < 5) initDir = xUtils.ShowDirectory;
			if (!Directory.Exists(initDir)) initDir = xUtils.ShowDirectory;

			string initFile = Path.GetFileNameWithoutExtension(fileAudioLast);

			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.FileName = initFile; // xUtils.ShowDirectory + Path.GetFileNameWithoutExtension(fileAudioLast) + ".xtiming";
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
				ImBusy(true);
				fileTimingsLast = dlgSaveFile.FileName;
				Properties.Settings.Default.LastxLightsTimingsSavePath = Path.GetDirectoryName(dlgSaveFile.FileName);
				ExportSelectedxTimings(dlgSaveFile.FileName);
				mruTimings.AddNew(fileTimingsLast);
				mruTimings.SaveToConfig();
				dirtyTimes = false;
				//SystemSounds.Beep.Play();
				utils.MakeNoise(utils.Noises.TaDa);
				ImBusy(false);
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

				//heartOfTheSun.phoo9 = useRampsBeats;
				//heartOfTheSun.phoo2 = useRampsPoly;
				//heartOfTheSun.phoo6 = useOctaveGrouping;
				heartOfTheSun.timeSignature = timeSignature;
				//heartOfTheSun.phoo8 = trackBeatsX;
				// Time signature does not get saved, reverts back to default 4/4 next time program is run
				//heartOfTheSun.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				//heartOfTheSun.phoo3 = useSaveFormat;


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
				if (vampMode)
				{
					string tryDir = xUtils.ShowDirectory + "\\Audio";
					if (Directory.Exists(tryDir))
					{
						initDir = tryDir;
					}
				}
				else
				{
					string tryDir = utils.DefaultAudioPath;
					if (Directory.Exists(tryDir))
					{
						initDir = tryDir;
					}
				}


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

		#region Checkboxes and Radio Buttons

		#endregion

		public static void PlayClickSound()
		{
			string sound = Path.GetDirectoryName(Application.ExecutablePath) + "\\Click.wav";
			SoundPlayer player = new SoundPlayer(sound);
			player.Play();
		}

		private void frmVamp_Paint(object sender, PaintEventArgs e)
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
			annotatorProgram = heartOfTheSun.annotatorProgram;
			if (!System.IO.File.Exists(annotatorProgram))
			//if (true) // For Testing
			{
				frmSettings setForm = new frmSettings() { Owner = this };
				//setForm.Parent = this;
				if (vampMode)
				{
					setForm.InitForm(frmSettings.SHOWannotatorIzer);
				}
				else
				{
					setForm.InitForm(frmSettings.SHOWannotatorORama);
				}
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
					dlgOpenFile.Filter = "Sonic Annotator|*.exe";
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

							if (heartOfTheSun.annotatorProgram.Length < 6)
							{
								heartOfTheSun.Upgrade();
								heartOfTheSun.Save();
							}


							heartOfTheSun.annotatorProgram = annotatorProgram;
							heartOfTheSun.Save();

						}

					}
					//if (!System.IO.File.Exists("C:\\Program Files (x86)\\Vamp Plugins\\qm-vamp-plugins.dll")) // 32-bit
					if (!System.IO.File.Exists("C:\\Program Files\\Vamp Plugins\\qm-vamp-plugins.dll")) // 64-bit
					{
						string msg = applicationName + " cannot continue until the Queen Mary Vamp Plugin has been installed.";
						MessageBox.Show(this, msg, "Please install the Queen Mary Vamp Plugin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						this.Close();

					}
				}
				if (dr == DialogResult.Cancel)
				{
					string msg = applicationName + " cannot continue until Sonic Annotator and the Queen Mary Vamp Plugin have been installed.";
					MessageBox.Show(this, msg, "Please install Sonic Annotator", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					this.Close();
					Application.Exit();
				}
				setForm.Close();
				setForm = null;
			}
			ImBusy(false);

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

				//heartOfTheSun.phoo6 = useOctaveGrouping;
				//heartOfTheSun.phoo2 = useRampsPoly;
				//heartOfTheSun.phoo3 = useSaveFormat;
				heartOfTheSun.Save();
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
			string ifile = txtSaveNamexL.Text;
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
							txtSaveNamexL.Text = Path.GetFileNameWithoutExtension(fileSeqSave);
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
				if (theStep == STEP_SelectAudioFile)
				{
					lblStep1.ForeColor = System.Drawing.Color.Red;
					grpAudio.Font = new Font(grpSavex.Font, FontStyle.Bold);
					grpAudio.Enabled = true;
					btnBrowseAudio.Focus();
				}
				else
				{
					lblStep1.ForeColor = SystemColors.Highlight;
					grpAudio.Font = new Font(grpSavex.Font, FontStyle.Regular);
				}

				if (theStep == STEP_SetOptions)
				{
					lblStep2A.ForeColor = System.Drawing.Color.Red;
					lblStep2B.ForeColor = System.Drawing.Color.Red;
					grpOptions.Font = new Font(grpSavex.Font, FontStyle.Bold);
					grpTimings.Font = new Font(grpSavex.Font, FontStyle.Bold);
					grpOptions.Enabled = true;
					grpTimings.Enabled = true;
					chkBars.Focus();
				}
				else
				{
					lblStep2A.ForeColor = SystemColors.Highlight;
					lblStep2B.ForeColor = SystemColors.Highlight;
					grpOptions.Font = new Font(grpSavex.Font, FontStyle.Regular);
					grpTimings.Font = new Font(grpSavex.Font, FontStyle.Regular);
				}

				if (theStep == STEP_AnalyzeAudio)
				{
					lblStep3.ForeColor = System.Drawing.Color.Red;
					grpAnalyze.Font = new Font(grpSavex.Font, FontStyle.Bold);
					grpAnalyze.Enabled = true;
					btnOK.Focus();
				}
				else
				{
					lblStep3.ForeColor = SystemColors.Highlight;
					grpAnalyze.Font = new Font(grpSavex.Font, FontStyle.Regular);
				}

				if (theStep == STEP_SaveTimings)
				{
					if (chkxLights.Checked)
					{
						lblStep4A.ForeColor = System.Drawing.Color.Red;
						grpSavex.Font = new Font(grpSavex.Font, FontStyle.Bold);
						grpSavex.Enabled = true;
						picxLights.Left = 74;
						btnSavexL.Focus();
					}
					if (chkLOR.Checked)
					{
						lblStep4B.ForeColor = System.Drawing.Color.Red;
						grpSaveLOR.Font = new Font(grpSaveLOR.Font, FontStyle.Bold);
						grpSaveLOR.Enabled = true;
						picLOR.Left = 74;
						if (!chkxLights.Checked)
						{
							btnSaveSeq.Focus();
						}
					}
				}
				else
				{
					lblStep4A.ForeColor = SystemColors.Highlight;
					grpSavex.Font = new Font(grpSavex.Font, FontStyle.Regular);
					lblStep4B.ForeColor = SystemColors.Highlight;
					grpSaveLOR.Font = new Font(grpSavex.Font, FontStyle.Regular);
					picxLights.Left = 66;
					picLOR.Left = 66;
				}
			}

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			SelectStep(STEP_AnalyzeAudio);
			string musicFile = fileAudioLast;

			if (System.IO.File.Exists(fileAudioLast))
			{
				ImBusy(true);
				utils.MakeNoise(utils.Noises.DrumRoll);
				// Remember all current user settings, options, selections, etc. on the main form
				SetTheControlsForTheHeartOfTheSun();

				// Clean up temp folder from previous run
				//! REMARKED OUT FOR TESTING DEBUGGING, LEAVE FILES
				//int errs = ClearTempDir();
				//! UNREMARK AFTER TESTING!
				//Vamperize(fileAudioLast);
				outLog.Clear();
				outputChanged = false;
				
				if (logWindow == null)
				{
					logWindow = new frmOutputLog(this);
				}
				if (logWindow.IsDisposed)
				{
					logWindow = new frmOutputLog(this);
				}
				logWindow.Show(this);
				logWindow.LogText = "";
				PrepareToAnnotate();
				AnnotateSelectedVamps();


				SelectStep(STEP_SaveTimings);
				logWindow.Done = true;
				utils.MakeNoise(utils.Noises.TaDa);

				while (!logWindow.IsDisposed)
				{
					Application.DoEvents();
				}


				ImBusy(false);
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
			// This is because the xTimings files probably go into the xLights folder (or subfolder of it)
			// Whereas the sequence probably came from the LOR folder (or subfolder of it)
			// And therefore quite different.
			string pathExport = "";
			mruTimings.Validate();
			pathExport = Path.GetDirectoryName(mruTimings.GetItem(0));
			if (pathExport.Length < 4)
			{
				pathExport = xUtils.ShowDirectory;

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
				//ExportSelectedTimings(newFile);
				//xUtils.PlayNotifyGenericSound();
				SystemSounds.Exclamation.Play();
			} // end dialog result = OK
			ImBusy(false);

		} // end SaveSelectionsAs...




		private void ExportSelectedLORTimings(string fileName)
		{
			// Get Temp Directory
			bool startWritten = false;
			bool endWritten = false;
			int writeCount = 0;


			// Save Filename for next time (really only need the path, but...)
			fileTimingsLast = fileName;
			txtSaveNamexL.Text = ShortenPath(fileName, 100);
			heartOfTheSun.fileTimingsLast = fileTimingsLast;

			if (optMultiPer.Checked) heartOfTheSun.saveFormat = 2;
			else heartOfTheSun.saveFormat = 1;
			mruTimings.AddNew(fileName);
			mruTimings.SaveToConfig();
			// Get path and name for export files

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
							//WriteTimingFile4(transBarBeats.xBars, fileName);
							//WriteTimingFile5(transBarBeats.xBars, fileName);
							writeCount += 3;
						}
					}
				}
				if (chkBeatsFull.Checked)
				{
					if (transBarBeats.xBeatsFull != null)
					{
						if (transBarBeats.xBeatsFull.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsFull, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsFull, fileName);
							writeCount += 3;
						}
					}
				}
				if (chkBeatsHalf.Checked)
				{
					if (transBarBeats.xBeatsHalf != null)
					{
						if (transBarBeats.xBeatsHalf.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsHalf, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsHalf, fileName);
							writeCount += 3;
						}
					}
				}
				if (chkBeatsThird.Checked)
				{
					if (transBarBeats.xBeatsThird != null)
					{
						if (transBarBeats.xBeatsThird.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsThird, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsThird, fileName);
							writeCount += 3;
						}
					}
				}
				if (chkBeatsQuarter.Checked)
				{
					if (transBarBeats.xBeatsQuarter != null)
					{
						if (transBarBeats.xBeatsQuarter.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsQuarter, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsQuarter, fileName);
							writeCount += 3;
						}
					}
				}
			}
			if (transOnsets != null)
			{
				if (chkNoteOnsets.Checked)
				{
					if (transOnsets.xOnsets != null)
					{
						if (transOnsets.xOnsets.effects.Count > 0)
						{
							//WriteTimingFile4(transOnsets.xOnsets, fileName);
							//WriteTimingFile5(transOnsets.xOnsets, fileName);
							writeCount += 3;
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
			pnlStatus.Text = writeCount.ToString() + " files writen.";
		} // end SaveSelections


		/*
		private void WriteTimingFileX(xTimings timings, string fileName)
		{
			int err = 0;
			//  Allocate a stream writer
			StreamWriter writer = null;
			string expPath = Path.GetDirectoryName(fileName) + "\\";
			string expBase = Path.GetFileNameWithoutExtension(fileName);
			string expFile = "x.x";
			string tempDir = System.IO.Path.GetTempPath();
			string timingsTemp = tempDir + Path.GetRandomFileName();

			writer = new StreamWriter(timingsTemp);
			string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			writer.WriteLine(lineOut);
			WriteTimingFileX(timings, writer);

			expFile = expPath + expBase + " - " + timings.timingName + ".xtiming";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = utils.SafeCopy(timingsTemp, expFile);
			System.IO.File.Delete(timingsTemp);
		}
		*/

		private void WriteTimingFileXX(xTimings timings, StreamWriter writer)
		{
			/*
			private bool closeWriter = false;
		string fileName = "";
		//	if (writer == null)
		//	{
		//		int err = 0;
		////  Allocate a stream writer
		//string expPath = Path.GetDirectoryName(fileName) + "\\";
		//string expBase = Path.GetFileNameWithoutExtension(fileName);
		//string expFile = "x.x";
		//string tempDir = System.IO.Path.GetTempPath();
		//string timingsTemp = tempDir + Path.GetRandomFileName();

		//writer = new StreamWriter(timingsTemp);
		//string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		//writer.WriteLine(lineOut);
		//	}
	pnlStatus.Text = "Writing " + timings.timingName + "...";
			staStatus.Refresh();
			// Create a temporary streamwriter file
			
			// Write this xTiming to an export file
			string xDat = timings.LineOutX();
	writer.WriteLine(xDat);

			writer.Close();
			*/
		}

		private void WriteTimingFile4(xTimings timings, string fileName)
		{
			string timingsTemp = "";
			string tempDir = System.IO.Path.GetTempPath();
			int err = 0;
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
			string lineOut = "";
			//string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
			//writer.WriteLine(lineOut);
			//lineOut = "<LORTiming version=\"1\">";
			//writer.WriteLine(lineOut);
			//lineOut = "  <TimingGrids>";
			//writer.WriteLine(lineOut);
			//lineOut = "    <TimingGridFree name=\"";
			//lineOut += timings.timingName + "\">";
			//writer.WriteLine(lineOut);

			// Write this xTiming to an export file
			string xDat = timings.LineOut4();
			writer.WriteLine(xDat);

			writer.Close();
			expFile = expPath + expBase + " - " + timings.timingName + ".lor4xml";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = utils.SafeCopy(timingsTemp, expFile);
			System.IO.File.Delete(timingsTemp);

		}

		private void WriteTimingFile5(xTimings timings, string fileName)
		{
			string timingsTemp = "";
			string tempDir = System.IO.Path.GetTempPath();
			int err = 0;
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
			string lineOut = "";

			// Write this xTiming to an export file
			string xDat = timings.LineOut5();
			writer.WriteLine(xDat);

			writer.Close();
			expFile = expPath + expBase + " - " + timings.timingName + ".lortime";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = utils.SafeCopy(timingsTemp, expFile);
			System.IO.File.Delete(timingsTemp);

		}

		private void lblHelpOnsets_Click(object sender, EventArgs e)
		{
			// Go to Vamp Plugins Web page
			System.Diagnostics.Process.Start("https://vamp-plugins.org/plugin-doc/qm-vamp-plugins.html#qm-onsetdetector");
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
			//TODO Move this to TransformBarBeats class
			int plugin = cboMethodBarsBeats.SelectedIndex;
			bool b = false;
			switch (plugin)
			{
				case BarBeats.PLUGINqmBarAndBeat:
					lblDetectBarBeats.Enabled = false;
					cboDetectBarBeats.Enabled = false;
					chkWhiteBarBeats.Enabled = false;
					break;
				case BarBeats.PLUGINqmTempo:
					lblDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Items.Clear();
					cboDetectBarBeats.Items.Add("High-Frequence Content");
					cboDetectBarBeats.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboDetectBarBeats.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboDetectBarBeats.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboDetectBarBeats.Items.Add("Broadband Energy Rise (Percussion mixed with other)");
					chkWhiteBarBeats.Enabled = true;
					break;
				case BarBeats.PLUGINbeatRoot:
					lblDetectBarBeats.Enabled = false;
					cboDetectBarBeats.Enabled = false;
					chkWhiteBarBeats.Enabled = false;
					break;
				case BarBeats.PLUGINaubio:
					lblDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Items.Clear();
					cboDetectBarBeats.Items.Add("Energy Based");
					cboDetectBarBeats.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboDetectBarBeats.Items.Add("High-Frequence Content");
					cboDetectBarBeats.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboDetectBarBeats.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboDetectBarBeats.Items.Add("Kullback-Liebler");
					cboDetectBarBeats.Items.Add("Modified Kullback-Liebler");
					cboDetectBarBeats.Items.Add("Spectral Flux");
					chkWhiteBarBeats.Enabled = false;
					break;
			}
		}

		private void labelTweaks_Click(object sender, EventArgs e)
		{
			Form ft = new frmTweakInfo();
			ft.ShowDialog(this);
		}

		private void btnResetDefaults_Click(object sender, EventArgs e)
		{
			SetTheControlsToDefaults();
		}

		private void SetTheControlsToDefaults()
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

			cboOnsetsPlugin.SelectedIndex = 0;
			cboOnsetsDetect.SelectedIndex = 0;
			vscSensitivity.Value = 50;
			chkOnsetsWhite.Checked = false;
			chkNoteOnsets.Checked = true;
			cboOnsetsLabels.SelectedIndex = 0;
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
			cboLabelsSegments.SelectedIndex = 2;
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
			if (cboSetting.SelectedIndex < 0)	cboSetting.SelectedIndex = 0;
			
			return i1;
		}

		private void chkReuse_CheckedChanged(object sender, EventArgs e)
		{
			heartOfTheSun.reuseFiles = chkReuse.Checked;
			heartOfTheSun.Save();
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

		private void optSeqNew_CheckedChanged(object sender, EventArgs e)
		{
			btnSaveSeq.Text = "Save &As...";
		}

		private void optSeqAppend_CheckedChanged(object sender, EventArgs e)
		{
			btnSaveSeq.Text = "S&ave Into...";
		}

		private void btnSaveSeq_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			if (optSeqNew.Checked)
			{
				SaveAsNewSequence();
			}
			if (optSeqAppend.Checked)
			{
				SaveInExistingSequence();
			}
			dirtyTimes = false;
			utils.MakeNoise(utils.Noises.Gong);
			ImBusy(false);
		}

		private void txtFileAudio_Leave(object sender, EventArgs e)
		{
			bool OK = false;
			try
			{
				string txt = txtFileAudio.Text;
				if (txt.Length > 3)
				{
					if (System.IO.File.Exists(txt))
					{
						audioData = ReadAudioFile(txt);
						string tit = audioData.Title;
						if (tit.Length > 3)
						{
							OK = true;
							fileAudioWork = txt;
						}
					}
				}
			}
			catch
			{
				OK = false;
			}
			if (OK)
			{
				txtFileAudio.ForeColor = SystemColors.ControlText;
			}
			else
			{
				txtFileAudio.ForeColor = Color.Red;
			}
			grpAnalyze.Enabled = OK;
			grpTimings.Enabled = OK;

		}

		private void btnCmdTemp_Click(object sender, EventArgs e)
		{
			Process cmdPrompt = new Process();
			cmdPrompt.StartInfo.FileName = "cmd.exe";
			cmdPrompt.StartInfo.WorkingDirectory = pathWork;
			//cmdPrompt.StartInfo.Arguments = pathWork;
			cmdPrompt.Start();
		}

		private void btnExploreTemp_Click(object sender, EventArgs e)
		{
			Process explore = new Process();
			explore.StartInfo.FileName = "explorer.exe";
			explore.StartInfo.Arguments = pathWork;
			explore.Start();

		}

		private void grpTimings_Enter(object sender, EventArgs e)
		{

		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			
		}

		private void chkLOR_CheckedChanged(object sender, EventArgs e)
		{
			string f = utils.DefaultSequencesPath;
			if (f.Length > 3)
			{
				btnExploreLOR.Visible = chkLOR.Checked;
			}
			f = utils.SequenceEditor;
			if (f.Length > 3)
			{
				btnSequenceEditor.Visible = chkLOR.Checked;
			}
		}

		private void chkxLights_CheckedChanged(object sender, EventArgs e)
		{
			string f = xUtils.ShowDirectory;
			if (f.Length > 3)
			{
				btnExplorexLights.Visible = chkxLights.Checked;
			}
			f = xUtils.SequenceEditor;
			if (f.Length > 3)
			{
				btnLaunchxLights.Visible = chkxLights.Checked;
			}
		}

		private void btnExploreLOR_Click(object sender, EventArgs e)
		{
			Process explore = new Process();
			explore.StartInfo.FileName = "explorer.exe";
			explore.StartInfo.Arguments = utils.DefaultSequencesPath;
			explore.Start();

		}

		private void btnExplorexLights_Click(object sender, EventArgs e)
		{
			Process explore = new Process();
			explore.StartInfo.FileName = "explorer.exe";
			explore.StartInfo.Arguments = xUtils.ShowDirectory;
			explore.Start();

		}

		private void btnSequenceEditor_Click(object sender, EventArgs e)
		{
			Process app = new Process();
			string exe = utils.SequenceEditor;
			if (exe.Length > 10)
			{
				app.StartInfo.FileName = exe;
				app.StartInfo.Arguments = "";
				app.Start();
			}
		}

		private void btnLaunchxLights_Click(object sender, EventArgs e)
		{
			Process app = new Process();
			string exe = xUtils.SequenceEditor;
			if (exe.Length > 10)
			{
				app.StartInfo.FileName = exe;
				app.StartInfo.Arguments = "";
				app.Start();
			}

		}

		private void btnExploreVamp_Click(object sender, EventArgs e)
		{
			string source = "W:\\Documents\\SourceCode\\Windows\\UtilORama4\\VampORama";
			Process explore = new Process();
			explore.StartInfo.FileName = "explorer.exe";
			explore.StartInfo.Arguments = source;
			explore.Start();

		}

		private void SetAlignments()
		{
			cboAlignBarsBeats.Items.Clear();
			if (chkLOR.Checked) cboAlignBarsBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS10));
			cboAlignBarsBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS20));
			if (chkLOR.Checked) cboAlignBarsBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS30));
			if (chkxLights.Checked) cboAlignBarsBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS40));
			if (chkLOR.Checked) cboAlignBarsBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS60));
			SetCombo(cboAlignBarsBeats, heartOfTheSun.alignBarsBeats);

			cboAlignOnsets.Items.Clear();
			if (chkLOR.Checked) cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS10));
			cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS20));
			if (chkLOR.Checked) cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS30));
			if (chkxLights.Checked) cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS40));
			if (chkLOR.Checked) cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS60));
			cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.BeatsFull));
			cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.BeatsHalf));
			cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.BeatsThird));
			cboAlignOnsets.Items.Add(vamps.AlignmentName(vamps.AlignmentType.BeatsQuarter));
			SetCombo(cboAlignOnsets, heartOfTheSun.alignBarsBeats);
		}



		private void frmVamp_FormClosed(object sender, FormClosedEventArgs e)
		{
			CloseForm();
		}

		private void AlphaWarning()
		{
			StringBuilder msg = new StringBuilder();
			msg.Append("This is an ALPHA release of Vamperizer.  ");
			msg.Append("It is absolutely, positively guaranteed to be buggy as hell and many features may be broken or not finished yet.  ");
			msg.Append("Do not use it for production work.  Keep backups of all your files.\r\n");
			msg.Append("Use only as directed.  Use at your own risk.  Do not ingest.  Biohazard.  ");
			msg.Append("Not responsible for climate change, war, hurricanes, volcanic erruptions, ");
			msg.Append("divorce, explosive diarrhea, hair loss, blindness, headaches, ");
			msg.Append("ornery mother-in-laws, ADD, ADHD, PMS, UFOs, purple urine, or death.  Other drastic conditions may occur.  ");
			msg.Append("Take with Tylenol, Xanax, Valium, or Prozac as needed.  ");
			msg.Append("Keep out of the reach of children and IT specialists.");
			msg.Append("No warranty express or implied (except to be buggy).  No refunds, credits, or exchanges.  Not for resale.");
			msg.Append("\r\nSend bug reports and good dirty jokes to wizard@wizlights.com");

			utils.MakeNoise(utils.Noises.Crash);
			DialogResult d = MessageBox.Show(this, msg.ToString(), "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;


		}

		private void cboOnsetsPlugin_SelectedIndexChanged(object sender, EventArgs e)
		{
			//TODO Move this to TransformNoteOnsets class
			int plugin = cboOnsetsPlugin.SelectedIndex;
			switch (plugin)
			{
				case NoteOnsets.PLUGINqmOnset:
					cboOnsetsLabels.Items.Clear();
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[0]);
					chkOnsetsWhite.Enabled = true;
					pnlOnsetSensitivity.Enabled = true;
					cboOnsetsDetect.Items.Clear();
					cboOnsetsDetect.Items.Add("High-Frequence Content");
					cboOnsetsDetect.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboOnsetsDetect.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboOnsetsDetect.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboOnsetsDetect.Items.Add("Broadband Energy Rise (Percussion mixed with other)");
					cboOnsetsDetect.Enabled = true;
					break;
				case NoteOnsets.PLUGINqmTranscribe:
					cboOnsetsLabels.Items.Clear();
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[0]);
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[1]);
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[2]);
					chkOnsetsWhite.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					break;
				case NoteOnsets.PLUGINOnsetDS:
					chkOnsetsWhite.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboOnsetsLabels.Items.Clear();
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[0]);
					break;
				case NoteOnsets.PLUGINSilvet:
					chkOnsetsWhite.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboOnsetsLabels.Items.Clear();
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[0]);
					break;
				case NoteOnsets.PLUGINaubioOnset:
					chkOnsetsWhite.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboOnsetsLabels.Items.Clear();
					cboOnsetsLabels.Items.Add(NoteOnsets.availableLabels[0]);
					break;
				case NoteOnsets.PLUGINaubioTracker:
					chkOnsetsWhite.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Items.Add("Energy Based");
					cboOnsetsDetect.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboOnsetsDetect.Items.Add("High-Frequence Content");
					cboOnsetsDetect.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboOnsetsDetect.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboOnsetsDetect.Items.Add("Kullback-Liebler");
					cboOnsetsDetect.Items.Add("Modified Kullback-Liebler");
					cboOnsetsDetect.Items.Add("Spectral Flux");
					cboOnsetsDetect.Enabled = true;
					break;

			}


		}

		private void cboDetectBarBeats_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}

		/*
		public void Log2Console(Level level, string MsgLine)
		{
			try
			{
				if (consoleWindow == null)
				{
					consoleWindow = new frmConsole();
					consoleWindow.Show(this);
					consoleWindow.logConsole.Items.Clear();
					this.Refresh();
				}
			}
			catch
			{ }
			try
			{
				consoleWindow.Log(level, MsgLine);
				consoleWindow.Refresh();
			}
			catch
			{ }

		}
		*/
	} // End Form Partial Class
//}// end namespace VampORama
