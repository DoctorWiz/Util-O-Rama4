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
using LOR4Utils;
using FileHelper;
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

namespace UtilORama4
{
	public partial class frmVamp : Form
	{
		#region Form Variables and Constants

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
		private static Properties.Settings userSettings = Properties.Settings.Default;
		private MRU mruTimings = new MRU(userSettings, "filesTiming", 10);
		private MRU mruAudio = new MRU(userSettings, "filesAudio", 10);
		//private List<xTimings> timingsList = new List<xTimings>();
		private const string OutputTitle = "Sonic Annotator Output Log";
		private const string ProcTitle = "Analyzing ";
		//private MRU mruAudio = new MRU();

		//private LOR4Sequence seq = new LOR4Sequence();
		private bool dirtyTimes = false;
		private const string helpPageV = "http://wizlights.com/xUtils/Vamperizer";
		private const string helpPageL = "http://wizlights.com/utilorama/vamporama";

		//private string applicationName = "Vamperizer";
		private string thisEXE = "VampORama.exe";
		//private string userSettingsLocalDir = "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string userSettingsRoamingDir = "C:\\Users\\Wizard\\AppData\\Roaming\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string machineSettingsDir = "C:\\ProgramData\\UtilORama\\Vamperizer";  // Gets overwritten with X:\\ProgramData\\UtilORama\\Vamperizer\\
		//private string pathWork = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Local\\Temp\\UtilORama\\SplitORama\\
		public string tempPath = Annotator.TempPath;
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte useSaveFormat = SAVEmixedDisplay;
		private int curStep = 0;
		public bool busy = true;
		public bool vamping = false;
		public bool analyzed = false;


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
		bool debugMode = Fyle.DebugMode;

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
		//private int firstCobjIdx = lutils.UNDEFINED;
		//private int firstCsavedIndex = lutils.UNDEFINED;
		//private LOR4Channel[] noteChannels = null;
		//private LOR4ChannelGroup[] octaveGroups = null;

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

		private string myTitle = "Vamp-O-Rama";

		[DllImport("shlwapi.dll")]
		static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

		#endregion

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
			ImBusy(true);

			string[] args = Environment.GetCommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				string lc = args[i].ToLower();
				if (lc.IndexOf("debug") >= 0)
				{
					Fyle.DebugMode = true;
				}
			}
			string mySubDir = "UtilORama\\";
			RestoreFormPosition();

			string sfoo = xUtils.ShowDirectory;
			if (sfoo.Length > 3) xLightsInstalled = true;
			sfoo = lutils.DefaultSequencesPath;
			if (sfoo.Length > 3) lightORamaInstalled = true;
			if (xLightsInstalled && !lightORamaInstalled)
			{
				vampMode = true;
				myTitle = "Vamperizer";
				mySubDir = "xUtils\\";
				applicationName = myTitle;
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
				grpPolyphonic.Top -= moveBy;
				grpPitchKey.Top -= moveBy;
				grpSegments.Top -= moveBy;
				grpChromagram.Top -= moveBy;
				grpVocals.Top -= moveBy;
				grpTempo.Top -= moveBy;

			}
			if (!lightORamaInstalled) chkLOR.Checked = false;
			if (!xLightsInstalled) chkxLights.Checked = false;
			grpSaveLOR.Visible = lightORamaInstalled;
			grpSavex.Visible = xLightsInstalled;

			this.Text = myTitle;
			//string appFolder = applicationName;
			//appFolder = appFolder.Replace("-", "");
			//mySubDir = mySubDir + appFolder + "\\";
			//string baseDir = Path.GetTempPath();
			//if (baseDir.Substring(baseDir.Length - 1, 1) != "\\") baseDir += "\\";
			//pathWork = baseDir + mySubDir;
			//if (!Directory.Exists(pathWork)) Directory.CreateDirectory(pathWork);

			if (debugMode)
			{
				chkReuse.Checked = userSettings.reuseFiles;
			}
			chkReuse.Visible = debugMode;
			lblWorkFolder.Visible = debugMode;
			btnCmdTemp.Visible = debugMode;
			btnExploreTemp.Visible = debugMode;
			btnExploreVamp.Visible = debugMode;
			lblSongTime.Visible = debugMode;

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

			// mruAudio = new MRU(userSettings, "Audio", 10);
			//mruAudio.ReadFromConfig(Properties.Settings.Default);
			//mruAudio.Validate();
			string f = mruAudio.GetItem(0);
			fileAudioLast = mruAudio.GetItem(0);
			if (f.Length > 5)
			{
				f = Path.GetDirectoryName(fileAudioLast);
			}
			else
			{
				f = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			}
			pathAudio = f;

			if (System.IO.File.Exists(fileAudioLast))
			{
				//audioData = ReadAudioFile(fileAudioLast);
				fileAudioWork = PrepAudioFile(fileAudioLast);
				string songInfo = audioData.Title + " by " + audioData.Artist;
				this.Text = myTitle + " - " + songInfo;
				lblSongTime.Text = FormatSongTime(audioData.Duration);
			}







			//mruTimings = new MRU(userSettings, "Timings", 10);
			//mruTimings.ReadFromConfig();
			//mruTimings.Validate();
			f = mruTimings.GetItem(0);
			if (f.Length < 5)
			{
				f = xUtils.ShowDirectory;
			}
			pathTimingsLast = f;

			RestoreUserSettings();

			if (!chkReuse.Checked)
			{
				int errs = xUtils.ClearTempDir(tempPath);
			}

			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			//TODO: These may get overridden by command line arguments (not yet supported)
			//! EXAMPLES FROM SPLIT-O-RAMA
			//useFuzzy = userSettings.useFuzzy;
			//minPreMatchScore = userSettings.preMatchScore;
			//saveFormat = userSettings.SaveFormat;
			//if ((minPreMatchScore < 500) || (minPreMatchScore > 1000))
			//{
			//	minPreMatchScore = 800;
			//}
			//minFinalMatchScore = userSettings.finalMatchScore;
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
					//string fileTimingsLast = userSettings.fileTimingsLast;
					//if (System.IO.File.Exists(fileTimingsLast))
					//{
					//Annotator.Sequence.ReadSequenceFile(fileSeqLast);
					//fileCurrent = fileSeqLast;
					//lutils.FillChannels(treChannels, seq, siNodes);
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
					//Annotator.Sequence.ReadSequenceFile(batch_fileList[0]);
					//fileSeqLast = batch_fileList[0];
					//lutils.FillChannels(treChannels, seq, siNodes);
					//userSettings.fileTimingsLast = fileSeqLast;
					//userSettings.Save();
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
					//this.Text = myTitle + " - " + Path.GetFileName(fileAudioLast);
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
		private void RestoreUserSettings()
		{
			SetTheControlsToDefaults();
			bool foo = false;

			fileAudioLast = userSettings.fileAudioLast;
			chkLOR.Checked = userSettings.UseLOR;
			chkxLights.Checked = userSettings.UsexLights;
			chkAutolaunch.Checked = userSettings.autoLaunch;
			optOnePer.Checked = userSettings.xTimesIndiv;
			chkChromagram.Checked = userSettings.doChromagram;
			FillCombos();

			RecallLastFile();
		}

		/////////////////////////////////////////////////////////////////////////////
		/// Save the current state of the form controls into Properties.Settings ///
		///       (Heart of the Sun = Properties.Settings)											///
		//////////////////////////////////////////////////////////////////////////
		private void SaveUserSettings()
		{
			//Properties.Settings userSettings = Properties.Settings.Default;

			userSettings.methodBarsBeats = cboMethodBarsBeats.Text;
			if (swTrackBeat.Checked)
			{ userSettings.timeSignature = 3; }
			else
			{ userSettings.timeSignature = 4; }
			userSettings.startBeat = Int32.Parse(txtStartBeat.Text); // startBeat;
			userSettings.detectBars = cboDetectBarBeats.Text;
			userSettings.whiteBarsBeats = chkWhiten.Checked;

			userSettings.fileAudioLast = fileAudioLast;

			userSettings.UseLOR = chkLOR.Checked;
			userSettings.UsexLights = chkxLights.Checked;
			userSettings.autoLaunch = chkAutolaunch.Checked;
			userSettings.doChromagram = chkChromagram.Checked;
			userSettings.xTimesIndiv = optOnePer.Checked;

			SaveCombos();

			userSettings.Save();

			mruAudio.AddNew(fileAudioLast);
			if (mruAudio.appSettings == null)
			{
				mruAudio.appSettings = userSettings;
			}
			mruAudio.SaveToConfig();

		}

		private void RecallLastFile()
		{
			fileAudioLast = userSettings.MRUfilesAudio0;
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
				userSettings.MRUfilesAudio0 = fileAudioLast;
				if (System.IO.File.Exists(fileAudioLast))
				{
					string sv = Path.GetFileNameWithoutExtension(fileAudioLast);

					sv += ".xtiming";
					txtSaveNamexL.Text = sv;
					//userSettings.filePhooLast = sv;

					//userSettings.fileAudioLast = fileAudioLast;
				}
				txtFileAudio.Text = ShortenPath(fileAudioLast, 100);
				//userSettings.Save();
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
					userSettings.fileTimingsLast = fileCurrent;
					userSettings.Save();

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

		private void ImVamping(bool amVamping)
		{
			lblWorkFolder.Visible = amVamping;
			pnlVamping.Visible = amVamping;
			//picVampire.Visible = !amVamping;
			picWorking.Visible = amVamping;
			lblPickYourPoison.Visible = !amVamping;
			picPoisonArrow.Visible = !amVamping;
			vamping = amVamping;

			ImBusy(amVamping);
		}

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
			busy = isBusy;
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
						if (!debugMode)
						{
							int errs = xUtils.ClearTempDir(tempPath);
						}
					}
					//CloseForm();
					SaveUserSettings();
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
					int errs = xUtils.ClearTempDir(tempPath);
				}
				//CloseForm();
				SaveUserSettings();
			}
			//CloseForm();

		}

		private void CloseForm()
		{
			SaveFormPosition();
			//SaveUserSettings();
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
			userSettings.Location = myLoc;
			userSettings.Size = mySize;
			userSettings.WindowState = (int)myState;
			userSettings.Save();
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

			Point savedLoc = userSettings.Location;
			Size savedSize = userSettings.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)userSettings.WindowState;
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

			dlgFileSave.Filter = filt;
			dlgFileSave.FilterIndex = 1;
			dlgFileSave.FileName = initFile; // xUtils.ShowDirectory + Path.GetFileNameWithoutExtension(fileAudioLast) + ".xtiming";
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.DefaultExt = ".xtiming";
			dlgFileSave.OverwritePrompt = false; // Use MY overwrite warning instead
			dlgFileSave.Title = tit;
			dlgFileSave.SupportMultiDottedExtensions = true;
			dlgFileSave.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
			//newFileIn = "Part Mother Fucker!!";
			//dlgFileSave.FileName = initFile;
			DialogResult result = dlgFileSave.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
				if (ow == DialogResult.Yes)
				{
					ImBusy(true);
					fileTimingsLast = dlgFileSave.FileName;
					Properties.Settings.Default.LastxLightsTimingsSavePath = Path.GetDirectoryName(dlgFileSave.FileName);
					ExportSelectedVampsToxLights(dlgFileSave.FileName);
					mruTimings.AddNew(fileTimingsLast);
					mruTimings.SaveToConfig();
					dirtyTimes = false;
					//SystemSounds.Beep.Play();
					Fyle.MakeNoise(Fyle.Noises.TaDa);
					ImBusy(false);
				}
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

				//userSettings.phoo9 = useRampsBeats;
				//userSettings.phoo2 = useRampsPoly;
				//userSettings.phoo6 = useOctaveGrouping;
				userSettings.timeSignature = timeSignature;
				//userSettings.phoo8 = trackBeatsX;
				// Time signature does not get saved, reverts back to default 4/4 next time program is run
				//userSettings.timeSignature = timeSignature; // 3 = 3/4 time, 4 = 4/4 time
				//userSettings.phoo3 = useSaveFormat;


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
			audioData = new Musik.AudioInfo(); // Reset/Clear old info
			TagLib.File MP3file = null;
			if (!System.IO.File.Exists(vFilename))
			{
				Fyle.BUG("Why are you trying to read tags from an audio file that doesn't exist:\r\n" + vFilename);
			}
			else
			{
				try
				{
					MP3file = TagLib.File.Create(@vFilename, ReadStyle.Average);
					Application.DoEvents();
				}
				catch (Exception ex1)
				{ }

				try
				{
					// WARNING: Library will not accept unicode in filename
					//TODO: Try to fix unicode filename problem

					audioData = ReadAudioTags(vFilename, MP3file);

					TimeSpan audioTime = audioData.Duration;
					int ms = audioTime.Minutes * 60000;
					ms += audioTime.Seconds * 1000;
					ms += audioTime.Milliseconds;
					if (ms < 100)
					{
						//Fyle.BUG("TagLib Failed to read the song's duration!\r\n(And probably the other metadata as well)");
						Fyle.MakeNoise(Fyle.Noises.SamCurseF);
					}
					//milliseconds = ms;
					audioData.Milliseconds = ms;
					audioData.Centiseconds = (int)Math.Round(ms / 10D);
					Annotator.songTimeMS = ms;

					// Fill in blank or missing tag info
					// Artist and Album Artist
					if (audioData.Artist == null) audioData.Artist = "";
					if (audioData.AlbumArtist == null) audioData.AlbumArtist = "";
					if (audioData.Artist.Length < 2)
					{
						if (audioData.AlbumArtist.Length > 1)
						{
							audioData.Artist = audioData.AlbumArtist;
						}
						else
						{
							audioData.Artist = "[Unknown Artist]";
						}
					}
					if (audioData.AlbumArtist.Length < 2) audioData.AlbumArtist = audioData.Artist;


					// Title, Album, other fields
					if (audioData.Title == null) audioData.Title = "";
					if (audioData.Title.Length < 2) audioData.Title = "[Unknown Title]";

					if (audioData.Album == null) audioData.Album = "";
					if (audioData.Album.Length < 2) audioData.Album = "[Unknown Album]";

					if (audioData.Comment == null) audioData.Comment = "";
					if (audioData.Composer == null) audioData.Composer = "";
					if (audioData.Bitrate == null) audioData.Bitrate = 0;
					if (audioData.DiscNo == null) audioData.DiscNo = 0;
					if (audioData.Genre == null) audioData.Genre = "";
					if (audioData.Track == null) audioData.Track = 0;
					if (audioData.VBR == null) audioData.VBR = false;
					if (audioData.Year == null) audioData.Year = "";

				} // END try
				catch (TagLib.CorruptFileException oEx)
				{
					//LogError(msErrorFile, vFilename);
					//LogError(msErrorFile, oEx.Message);
					//LogError(msErrorFile, "");

					string sMsg = "Corrupt File:" + vFilename + "\n" + oEx.Message;
					MessageBox.Show(sMsg, "Bad File", MessageBoxButtons.OK, MessageBoxIcon.Error);

				} // END catch (TagLib.CorruptFileException oEx)
				catch (Exception oEx)
				{
					//LogError(msErrorFile, vFilename);
					//LogError(msErrorFile, oEx.Message);
					//LogError(msErrorFile, "");

					string sMsg = "Error Reading File:" + vFilename + "\n" + oEx.Message;
					MessageBox.Show(sMsg, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				} // END catch (Exception oEx)
			}
			return audioData;
		} // END private AudioInfo ReadAudioFile(string vFilename)

		private Musik.AudioInfo ReadAudioTags(string vsFilename, TagLib.File fileMP3)
		{
			if (fileMP3 == null)
			{
				Fyle.BUG("TagLib failed to read the friggin' file tags!");
			}


			FileInfo oFile = new FileInfo(vsFilename);
			Musik.AudioInfo tAudio = new Musik.AudioInfo();
			//int dRate = 0;
			int iRate = 0;

			tAudio.Duration = fileMP3.Properties.Duration;
			tAudio.Path = oFile.DirectoryName;
			tAudio.Filename = oFile.Name;
			tAudio.Type = oFile.Extension.ToLower();
			tAudio.AlbumArtist = NullStringFix(fileMP3.Tag.FirstAlbumArtist);
			tAudio.Album = NullStringFix(fileMP3.Tag.Album);
			tAudio.Artist = NullStringFix(fileMP3.Tag.FirstPerformer);
			tAudio.Composer = NullStringFix(fileMP3.Tag.FirstComposer);
			tAudio.Title = NullStringFix(fileMP3.Tag.Title);
			tAudio.Year = fileMP3.Tag.Year.ToString();
			tAudio.Genre = NullStringFix(fileMP3.Tag.FirstGenre);
			tAudio.Comment = NullStringFix(fileMP3.Tag.Comment);
			//tAudio.Bitrate = fileMP3.Properties.AudioBitrate;
			tAudio.Size = oFile.Length;
			tAudio.DiscNo = fileMP3.Tag.Disc;
			tAudio.Track = fileMP3.Tag.Track;

			//tAudio.VBR = oA_MP3.Properties.Codecs.
			tAudio.Modified = oFile.LastWriteTime;


			iRate = fileMP3.Properties.AudioBitrate;
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
				// First, lets try the same folder the last audio file came from
				string initDir = Path.GetDirectoryName(fileAudioLast);
				if (!Directory.Exists(initDir))
				{
					// No good?  Next try the default audio path for Light-O-Rama Showtime
					initDir = lutils.DefaultAudioPath;
					if (!Directory.Exists(initDir))
					{
						// Still no good?  Try the Audio Folder under the xLights show directory
						initDir = xUtils.ShowDirectory + "\\Audio";
						if (!Directory.Exists(initDir))
						{
							// STILL no good?  Last chance- the user's MyMusic folder
							initDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
							if (!Directory.Exists(initDir))
							{
								//? What else do I try?
							}
						}
					}
				}
				string initFile = "";

				dlgFileOpen.Filter = MusicFilter();
				dlgFileOpen.FilterIndex = 1;
				dlgFileOpen.DefaultExt = "";
				dlgFileOpen.InitialDirectory = initDir;
				dlgFileOpen.FileName = initFile;
				dlgFileOpen.CheckFileExists = true;
				dlgFileOpen.CheckPathExists = true;
				dlgFileOpen.Multiselect = false;
				dlgFileOpen.Title = "Select Media File...";
				//pnlAll.Enabled = false;
				DialogResult result = dlgFileOpen.ShowDialog(this);

				if (result == DialogResult.OK)
				{
					fileAudioLast = dlgFileOpen.FileName;
					txtFileAudio.Text = ShrinkPath(fileAudioLast, 100);
					//int errs = ClearTempDir();
					//AnalyzeSong(dlgFileOpen.FileName);
					//grpSequence.Enabled = true;
					grpTimings.Enabled = true;
					//grpOptions.Enabled = true;
					grpAnalyze.Enabled = true;
					//fileAudioWork = ReadAudioFile(fileAudioLast);
					audioData = ReadAudioFile(fileAudioLast);
					string songInfo = audioData.Title + " by " + audioData.Artist;
					this.Text = myTitle + " - " + songInfo;
					lblSongTime.Text = FormatSongTime(Annotator.TotalMilliseconds);

					analyzed = false;
					grpTimings.Enabled = !analyzed;
					btnReanalyze.Location = btnOK.Location;
					btnReanalyze.Visible = analyzed;
					btnOK.Visible = !analyzed;


				} // end if (result = DialogResult.OK)


			}
			else
			{
			}


		} // end BrowseAudio_Click

		public string FormatSongTime(int milliseconds)
		{
			int ms = milliseconds;
			int min = ms / 60000;
			ms -= min * 60000;
			int sec = ms / 1000;
			ms -= sec * 1000;
			int cs = (int)Math.Round(ms / 10D);
			string time = min.ToString() + ":" + sec.ToString("00") + "." + cs.ToString("00");
			return time;
		}

		public string FormatSongTime(TimeSpan duration)
		{
			string timeOut = duration.Minutes.ToString() + ":";
			timeOut += duration.Seconds.ToString("00") + ".";
			timeOut += duration.Milliseconds.ToString("00");
			return timeOut;
		}
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
			annotatorProgram = userSettings.annotatorProgram;
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
					dlgFileOpen.InitialDirectory = initDir;
					dlgFileOpen.FileName = "sonic-annotator.exe";
					dlgFileOpen.Filter = "Sonic Annotator|*.exe";
					dlgFileOpen.FilterIndex = 1;
					dlgFileOpen.Multiselect = false;
					dlgFileOpen.SupportMultiDottedExtensions = false;
					dlgFileOpen.CheckPathExists = true;
					dlgFileOpen.CheckFileExists = true;
					dlgFileOpen.DefaultExt = ".exe";
					dlgFileOpen.Title = "Locate Sonic Annotator";
					dr = dlgFileOpen.ShowDialog(this);
					if (dr == DialogResult.OK)
					{
						string anoFile = dlgFileOpen.FileName;
						string anoExe = Path.GetFileName(anoFile).ToLower();
						if (anoExe.CompareTo("sonic-annotator.exe") != 0)
						{
							dr = DialogResult.Cancel;
						}
						else
						{
							annotatorProgram = anoFile;

							if (userSettings.annotatorProgram.Length < 6)
							{
								userSettings.Upgrade();
								userSettings.Save();
							}


							userSettings.annotatorProgram = annotatorProgram;
							userSettings.Save();

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

				//userSettings.phoo6 = useOctaveGrouping;
				//userSettings.phoo2 = useRampsPoly;
				//userSettings.phoo3 = useSaveFormat;
				userSettings.Save();
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

				dlgFileSave.Filter = filter;
				dlgFileSave.InitialDirectory = idr;
				dlgFileSave.FileName = ifile;
				dlgFileSave.FilterIndex = 1;
				dlgFileSave.OverwritePrompt = false; //  false; // Handled Below
				dlgFileSave.Title = "Save Timings As...";
				dlgFileSave.ValidateNames = true;

				while (saveAsk)
				{
					DialogResult result = dlgFileSave.ShowDialog(this);
					bool doSave = true;
					if (result == DialogResult.OK)
					{
						DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
						if (ow == DialogResult.No)
						{
							//if (System.IO.File.Exists(dlgFileSave.FileName))
							//{
							//string fn = Path.GetFileNameWithoutExtension(dlgFileSave.FileName).ToLower();
							//string ed = "";
							//string t = Path.GetFileName(dlgFileSave.FileName);
							//t += " already exists.\r\nDo you want to replace it?";
							//DialogResult dr3 = MessageBox.Show(this, t, "Confirm Save As", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
							//if (dr3 == DialogResult.Cancel) saveAsk = true;
							//if (dr3 == DialogResult.No) doSave = false;
							doSave = false;
						}
						if (doSave)
						{
							fileTimingsLast = dlgFileSave.FileName;
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
			bool success = Analyze();
			grpTimings.Enabled = !analyzed;
			btnReanalyze.Location = btnOK.Location;
			btnReanalyze.Visible = analyzed;
			btnOK.Visible = !analyzed;
		}

		private bool Analyze()
		{
			bool success = false;

			if (swTrackBeat.Checked) Annotator.BeatsPerBar = 3; else Annotator.BeatsPerBar = 4;
			Annotator.FirstBeat = Int32.Parse(txtStartBeat.Text);

			SelectStep(STEP_AnalyzeAudio);
			string musicFile = fileAudioLast;

			if (System.IO.File.Exists(fileAudioLast))
			{
				ImVamping(true);
				StatusUpdate("Preparing and Analyzing Audio File");
				// Remember all current user settings, options, selections, etc. on the main form
				SaveUserSettings();

				// First, do we need to re-prep the audio file?
				bool rePrepAudio = false; // create flag, default false
				if (!chkReuse.Checked) rePrepAudio = true;
				if (fileAudioWork.Length > 4)
				{
					if (!System.IO.File.Exists(fileAudioWork)) rePrepAudio = true;
				}
				if (rePrepAudio)
				{
					PrepAudioFile(fileAudioLast);
				}
				lblSongName.Text = audioData.Title + " by " + audioData.Artist;
				pnlVamping.Top = grpAnalyze.Top;
				pnlVamping.Left = grpAnalyze.Left;
				pnlVamping.Visible = true;

				// Next, do we need to re-prep the vamp results
				bool reRunVamps = false; // reset flag
				if (!chkReuse.Checked) reRunVamps = true;
				//string barBeatsResults = pathWork + VampBarBeats.ResultsName;
				VampBarBeats.FileResults = Annotator.TempPath + VampBarBeats.ResultsName;
				if (!Fyle.Exists(VampBarBeats.FileResults)) reRunVamps = true;
				if (reRunVamps)
				{
					Fyle.MakeNoise(Fyle.Noises.DrumRoll);
					// Clean up temp folder from previous run
					//! REMARKED OUT FOR TESTING DEBUGGING, LEAVE FILES
					if (!chkReuse.Checked)
					{
						//int errs = xUtils.ClearTempDir(tempPath);
					}
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
					//PrepareToAnnotate();
					RunSelectedVamps();

				}

				// Finally, do we need to re-prep the annotations
				bool rePrepTimes = false; // reset flag
				if (Annotator.xBars.effects.Count < 1) rePrepTimes = true;
				if (!chkReuse.Checked) rePrepTimes = true;
				rePrepTimes = true; //! Manual Override for debugging!
				if (rePrepTimes)
				{
					ProcessSelectedVamps();
					Fyle.MakeNoise(Fyle.Noises.TaDa);
					success = true; // TODO make sure it really was successfull
				}
				success = true;

				SelectStep(STEP_SaveTimings);

				if (reRunVamps)
				{
					// Did we run SonicAnnotator above, and thus opened the output window?
					// Don't need to wait on it any longer
					logWindow.Done = true;
					while (!logWindow.IsDisposed)
					{
						Application.DoEvents();
					}
				}
				StatusUpdate("Analysis Complete!");
				analyzed = true;
				pnlVamping.Visible = false;
				ImVamping(false);
			}
			else
			{
				string msg = "Song file '" + Path.GetFileName(fileAudioLast) + "' not found";
				msg += " in '" + Path.GetDirectoryName(fileAudioLast) + "'.";
				MessageBox.Show(this, msg, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			}
			return success;
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
			//SelectStep(4);
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
			dlgFileSave.DefaultExt = "xtiming";
			dlgFileSave.Filter = "xLights Timings (*.xtiming)|*.xTiming|All Files (*.*)|*.*";
			dlgFileSave.FilterIndex = 0;
			string initFile = Path.GetFileNameWithoutExtension(fileAudioLast);
			dlgFileSave.FileName = initFile;
			dlgFileSave.InitialDirectory = pathExport;
			dlgFileSave.OverwritePrompt = false; // Use mine instead!
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.SupportMultiDottedExtensions = true;
			dlgFileSave.ValidateNames = true;
			dlgFileSave.Title = "Save Timings As...";

			DialogResult dr = dlgFileSave.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
				if (ow == DialogResult.Yes)
				{
					string newFile = dlgFileSave.FileName;
					//ExportSelectedTimings(newFile);
					//xUtils.PlayNotifyGenericSound();
					SystemSounds.Exclamation.Play();
				}
			} // end dialog result = OK
			ImBusy(false);

		} // end SaveSelectionsAs...

		///////////////////////////////////////
		//! EXPORT Selected Timings to LOR  //
		/////////////////////////////////////
		private void ExportSelectedTimings_ToLOR(string fileName)
		{
			// Get Temp Directory
			bool startWritten = false;
			bool endWritten = false;
			int writeCount = 0;


			// Save Filename for next time (really only need the path, but...)
			fileTimingsLast = fileName;
			txtSaveNamexL.Text = ShortenPath(fileName, 100);
			userSettings.fileTimingsLast = fileTimingsLast;

			if (optMultiPer.Checked) userSettings.saveFormat = 2;
			else userSettings.saveFormat = 1;
			mruTimings.AddNew(fileName);
			mruTimings.SaveToConfig();
			// Get path and name for export files

			//if (chkBars.Checked)
			//if (doBarsBeats)
			//! BARS AND BEATS
			if (chkBarsBeats.Checked)
			{
				//! BARS
				if (VampBarBeats.xBars != null)
				{
					if (VampBarBeats.xBars.effects.Count > 0)
					{
						//WriteTimingFile4(transBarBeats.xBars, fileName);
						//WriteTimingFile5(transBarBeats.xBars, fileName);
						writeCount++;
					}
				}
				//! FULL BEATS WHOLE NOTES
				if (chkBeatsFull.Checked)
				{
					if (VampBarBeats.xBeatsFull != null)
					{
						if (VampBarBeats.xBeatsFull.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsFull, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsFull, fileName);
							writeCount++;
						}
					}
				}
				//! HALF BEATS EIGHTH NOTES
				if (chkBeatsHalf.Checked)
				{
					if (VampBarBeats.xBeatsHalf != null)
					{
						if (VampBarBeats.xBeatsHalf.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsHalf, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsHalf, fileName);
							writeCount++;
						}
					}
				}
				//! THIRD BEATS TWELTH NOTES
				if (chkBeatsThird.Checked)
				{
					if (VampBarBeats.xBeatsThird != null)
					{
						if (VampBarBeats.xBeatsThird.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsThird, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsThird, fileName);
							writeCount++;
						}
					}
				}
				//! QUARTER BEATS SIXTEENTH NOTES
				if (chkBeatsQuarter.Checked)
				{
					if (VampBarBeats.xBeatsQuarter != null)
					{
						if (VampBarBeats.xBeatsQuarter.effects.Count > 0)
						{
							//WriteTimingFile4(transBarBeats.xBeatsQuarter, fileName);
							//WriteTimingFile5(transBarBeats.xBeatsQuarter, fileName);
							writeCount++;
						}
					}
				}
			} // End Bars and Beats

			//! NOTE ONSETS
			// Note: Returns, effectively, the same timing grid as 'Polyphonic Transcription' (if selected)
			if (chkNoteOnsets.Checked)
			{
				if (VampNoteOnsets.Timings != null)
				{
					if (VampNoteOnsets.Timings.effects.Count > 0)
					{
						WriteTimingFile4(VampNoteOnsets.Timings, fileName);
						writeCount++;
					}
				}
			}

			//! POLYPHONIC TRANSCRIPTION
			// Note: Returns, effectively, the same timing grid as 'Note Onsets' (if selected)
			if (chkPolyphonic.Checked)
			{
				if (VampPolyphonic.Timings != null)
				{
					if (VampPolyphonic.Timings.effects.Count > 0)
					{
						WriteTimingFile4(VampPolyphonic.Timings, fileName);
						writeCount++;
					}
				}
			}

			//! PITCH AND KEY CHANGES
			if (chkPitchKey.Checked)
			{
				if (VampPitchKey.Timings != null)
				{
					if (VampPitchKey.Timings.effects.Count > 0)
					{
						WriteTimingFile4(VampPitchKey.Timings, fileName);
						writeCount++;
					}
				}
			}

			//! TEMPO CHANGES
			if (chkTempo.Checked)
			{
				if (VampTempo.Timings != null)
				{
					if (VampTempo.Timings.effects.Count > 0)
					{
						WriteTimingFile4(VampTempo.Timings, fileName);
						writeCount++;
					}
				}
			}

			//! SEGMENTS
			if (chkSegments.Checked)
			{
				if (VampSegments.Timings != null)
				{
					if (VampSegments.Timings.effects.Count > 0)
					{
						WriteTimingFile4(VampSegments.Timings, fileName);
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

			expFile = expPath + expBase + " - " + timings.Name + ".xtiming";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = lutils.SafeCopy(timingsTemp, expFile);
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
	pnlStatus.Text = "Writing " + timings.Name + "...";
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

			pnlStatus.Text = "Writing " + timings.Name + "...";
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
			//lineOut += timings.Name + "\">";
			//writer.WriteLine(lineOut);

			// Write this xTiming to an export file
			string xDat = timings.LineOut4();
			writer.WriteLine(xDat);

			writer.Close();
			expFile = expPath + expBase + " - " + timings.Name + ".lor4xml";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = Fyle.SafeCopy(timingsTemp, expFile);
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

			pnlStatus.Text = "Writing " + timings.Name + "...";
			staStatus.Refresh();
			// Create a temporary streamwriter file
			timingsTemp = tempDir + Path.GetRandomFileName();
			writer = new StreamWriter(timingsTemp);
			string lineOut = "";

			// Write this xTiming to an export file
			string xDat = timings.LineOut5();
			writer.WriteLine(xDat);

			writer.Close();
			expFile = expPath + expBase + " - " + timings.Name + ".lortime";
			if (System.IO.File.Exists(expFile))
			{
				// If already exists, delete it
				//TODO: Add Exception Catch
				System.IO.File.Delete(expFile);
			}
			// Copy the tempfile to the new file name and delete the old temp file
			err = Fyle.SafeCopy(timingsTemp, expFile);
			System.IO.File.Delete(timingsTemp);

		}

		private void lblHelpOnsets_Click(object sender, EventArgs e)
		{
			// Go to Vamp Plugins Web page
			System.Diagnostics.Process.Start("https://vamp-plugins.org/plugin-doc/qm-vamp-plugins.html#qm-onsetdetector");
		}


		private void vscStartBeat_Scroll_1(object sender, ScrollEventArgs e)
		{
			//txtStartBeat.Text = vscStartBeat.Value.ToString();
			txtStartBeat.Text = (5 - vscStartBeat.Value).ToString(); ;
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
				case VampBarBeats.PLUGINqmBarAndBeat:
					lblDetectBarBeats.Enabled = false;
					cboDetectBarBeats.Enabled = false;
					chkWhiten.Enabled = false;
					break;
				case VampBarBeats.PLUGINqmTempo:
					lblDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Enabled = true;
					cboDetectBarBeats.Items.Clear();
					cboDetectBarBeats.Items.Add("High-Frequence Content");
					cboDetectBarBeats.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboDetectBarBeats.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboDetectBarBeats.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboDetectBarBeats.Items.Add("Broadband Energy Rise (Percussion mixed with other)");
					chkWhiten.Enabled = true;
					break;
				case VampBarBeats.PLUGINbeatRoot:
					lblDetectBarBeats.Enabled = false;
					cboDetectBarBeats.Enabled = false;
					chkWhiten.Enabled = false;
					break;
				case VampBarBeats.PLUGINaubio:
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
					chkWhiten.Enabled = false;
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
			chkWhiten.Checked = false;
			chkBars.Checked = true;
			chkBeatsFull.Checked = true;
			chkBeatsHalf.Checked = true;
			chkBeatsThird.Checked = false;
			chkBeatsQuarter.Checked = true;
			cboAlignBarBeats.SelectedIndex = 1; // 25ms 40fps

			cboMethodOnsets.SelectedIndex = 0;
			cboOnsetsDetect.SelectedIndex = 0;
			vscSensitivity.Value = 50;
			chkWhiten.Checked = false;
			chkNoteOnsets.Checked = true;
			cboLabelsOnsets.SelectedIndex = 0;
			cboAlignOnsets.SelectedIndex = 0; // Quarter Beats Sixteenth Notes

			cboMethodPolyphonic.SelectedIndex = 0;
			chkPolyphonic.Checked = true;
			cboLabelsPolyphonic.SelectedIndex = 0;
			cboAlignPolyphonic.SelectedIndex = 6; // Note Onsets

			cboMethodChromagram.SelectedIndex = 0;
			chkChromagram.Checked = false;
			cboLabelsChromagram.SelectedIndex = 0;
			cboAlignChromagram.SelectedIndex = 0;

			cboMethodPitchKey.SelectedIndex = 0;
			chkPitchKey.Checked = true;
			cboLabelsPitchKey.SelectedIndex = 0;
			cboAlignPitchKey.SelectedIndex = 0; // Bars

			cboMethodTempo.SelectedIndex = 0;
			chkTempo.Checked = false;
			cboAlignTempo.SelectedIndex = 0;
			cboAlignTempo.SelectedIndex = 0; // Bars

			chkSegments.Checked = false;
			cboLabelsSegments.SelectedIndex = 0;
			cboAlignSegments.SelectedIndex = 0; // Bars

			chkVocals.Checked = false;
			cboAlignVocals.SelectedIndex = 0; // None

		}

		private int SetCombo(ComboBox cboSetting, string selection)
		{
			int i1 = cboSetting.FindStringExact(selection);
			if (i1 < 0)
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
			if (i1 >= 0)
			{
				cboSetting.SelectedIndex = i1;
			}
			if (cboSetting.SelectedIndex < 0) cboSetting.SelectedIndex = 0;

			return i1;
		}

		private void chkReuse_CheckedChanged(object sender, EventArgs e)
		{
			userSettings.reuseFiles = chkReuse.Checked;
			userSettings.Save();
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
			bool success = false;
			ImBusy(true);
			if (optSeqNew.Checked)
			{
				success = SaveAsNewSequence();
			}
			if (optSeqAppend.Checked)
			{
				success = SaveInExistingSequence();
			}
			dirtyTimes = false;
			//Fyle.MakeNoise(Fyle.Noises.Gong);
			ImBusy(false);
		}

		private void txtFileAudio_Leave(object sender, EventArgs e)
		{
			bool OK = false;
			try
			{
				if (audioData.Title == "")
				{
					string txt = txtFileAudio.Text;
					if (txt.Length > 3)
					{
						if (System.IO.File.Exists(txt))
						{
							fileAudioLast = txt;
							audioData = ReadAudioFile(fileAudioLast);
							string songInfo = audioData.Title + " by " + audioData.Artist;
							this.Text = myTitle + " - " + songInfo;
							lblSongTime.Text = FormatSongTime(Annotator.TotalMilliseconds);
							string tit = audioData.Title;
							if (tit.Length > 3)
							{
								OK = true;
								fileAudioWork = txt;
							}
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
			cmdPrompt.StartInfo.WorkingDirectory = tempPath;
			//cmdPrompt.StartInfo.Arguments = pathWork;
			cmdPrompt.Start();
		}

		private void btnExploreTemp_Click(object sender, EventArgs e)
		{
			Process explore = new Process();
			explore.StartInfo.FileName = "explorer.exe";
			explore.StartInfo.Arguments = tempPath;
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
			string f = lutils.DefaultSequencesPath;
			if (f.Length > 3)
			{
				btnExploreLOR.Visible = chkLOR.Checked;
			}
			f = lutils.SequenceEditor;
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
			explore.StartInfo.Arguments = lutils.DefaultSequencesPath;
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
			string exe = lutils.SequenceEditor;
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

		private void zzzOLD_SetAlignments()
		{
			cboAlignBarBeats.Items.Clear();
			if (chkLOR.Checked) cboAlignBarBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS10));
			cboAlignBarBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS20));
			if (chkLOR.Checked) cboAlignBarBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS30));
			if (chkxLights.Checked) cboAlignBarBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS40));
			if (chkLOR.Checked) cboAlignBarBeats.Items.Add(vamps.AlignmentName(vamps.AlignmentType.FPS60));
			SetCombo(cboAlignBarBeats, userSettings.alignBarsBeats);

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
			SetCombo(cboAlignOnsets, userSettings.alignBarsBeats);
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

			Fyle.MakeNoise(Fyle.Noises.Crash);
			DialogResult d = MessageBox.Show(this, msg.ToString(), "WARNING!", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;


		}

		private void cboOnsetsPlugin_SelectedIndexChanged(object sender, EventArgs e)
		{
			//TODO Move this to TransformNoteOnsets class

			/*int plugin = cboMethodOnsets.SelectedIndex;
			switch (plugin)
			{
				case VampNoteOnsets.PLUGINqmOnset:
					cboLabelsOnsets.Items.Clear();
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[0]);
					chkWhiten.Enabled = true;
					pnlOnsetSensitivity.Enabled = true;
					cboOnsetsDetect.Items.Clear();
					cboOnsetsDetect.Items.Add("High-Frequence Content");
					cboOnsetsDetect.Items.Add("Spectral Difference (Percussion: Drums, Chimes)");
					cboOnsetsDetect.Items.Add("Phase Deviation (Wind: Flute, Sax, Trumpet)");
					cboOnsetsDetect.Items.Add("Complex Domain (Strings/Mixed: Piano, Guitar)");
					cboOnsetsDetect.Items.Add("Broadband Energy Rise (Percussion mixed with other)");
					cboOnsetsDetect.Enabled = true;
					break;
				case VampNoteOnsets.PLUGINqmTranscribe:
					cboLabelsOnsets.Items.Clear();
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[0]);
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[1]);
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[2]);
					chkWhiten.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					break;
				case VampNoteOnsets.PLUGINOnsetDS:
					chkWhiten.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboLabelsOnsets.Items.Clear();
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[0]);
					break;
				case VampNoteOnsets.PLUGINSilvet:
					chkWhiten.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboLabelsOnsets.Items.Clear();
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[0]);
					break;
				case VampNoteOnsets.PLUGINaubioOnset:
					chkWhiten.Enabled = false;
					pnlOnsetSensitivity.Enabled = false;
					cboOnsetsDetect.Enabled = false;
					cboLabelsOnsets.Items.Clear();
					cboLabelsOnsets.Items.Add(VampNoteOnsets.availableLabels[0]);
					break;
				case VampNoteOnsets.PLUGINaubioTracker:
					chkWhiten.Enabled = false;
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
			*/

		}

		private void cboDetectBarBeats_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void swTrackBeat_CheckedChanged_1(object sender, EventArgs e)
		{

		}

		private void txtFileAudio_TextChanged(object sender, EventArgs e)
		{

		}

		private void lblTrackBeat34_Click(object sender, EventArgs e)
		{
			swTrackBeat.Checked = true;
		}

		private void lblTrackBeat44_Click(object sender, EventArgs e)
		{
			swTrackBeat.Checked = false;
		}

		private void lblBarsRampFade_Click(object sender, EventArgs e)
		{
			swRamps.Checked = true;
		}

		private void lblBarsOnOff_Click(object sender, EventArgs e)
		{
			swRamps.Checked = false;
		}

		private void StatusUpdate(string message)
		{
			pnlStatus.Text = message;
			staStatus.Refresh();
		}

		private void btnReanalyze_Click(object sender, EventArgs e)
		{
			analyzed = false;
			grpTimings.Enabled = !analyzed;
			//btnReanalyze.Location = btnOK.Location;
			btnReanalyze.Visible = analyzed;
			btnOK.Visible = !analyzed;

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
	//}// end namespace UtilORama4
