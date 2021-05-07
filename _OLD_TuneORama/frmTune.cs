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

		//private string fileCurrent = "";  // Currently loaded Sequence File
		private string fileSeqCur = ""; // Last Sequence File Loaded
		private string fileSeqSave = ""; // Last Saved Sequence
		private string fileSeqLast = ""; // last file ran.
		private string fileAudioOriginal = ""; // the file name extracted from the sequence
		private string fileAudioLast = ""; // Usually the same as fileAudioOriginal, but can be overriden
		private string fileAudioWork = ""; // fileAudioLast copied to the temp folder
		private string fileChanCfg = "";
		private string fileChanCfgLast = "";
		private string originalPath = "";
		private string originalFileName = "";
		private string originalExt = "";
		private string newFile = "";
		int centiseconds = 0;


		private Sequence4 seq = new Sequence4();
		private bool dirtySeq = false;
		private const string helpPage = "http://wizlights.com/utilorama/tuneorama";
		private string applicationName = "Tune-O-Rama";
		private string thisEXE = "tune-o-rama.exe";
		//private string userSettingsLocalDir = "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\TuneORama";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string userSettingsRoamingDir = "C:\\Users\\Wizard\\AppData\\Roaming\\UtilORama\\TuneORama";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\UtilORama\\SplitORama\\
		//private string machineSettingsDir = "C:\\ProgramData\\UtilORama\\TuneORama";  // Gets overwritten with X:\\ProgramData\\UtilORama\\TuneORama\\
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Local\\Temp\\UtilORama\\SplitORama\\
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
		private int rlevel = 0;


		private bool doAutoSave = false;
		private bool doAutoLaunch = true;
		private int timeSignature = 4;  // Default 4/4 time
		private bool doNoteOnsets = true;
		private bool doBeatGrid = true;
		private bool doPoly = true;
		private bool doBeatChannels = true;
		private bool doGroups = true;
		private bool useRampsPoly = false;
		private bool useRampsBeats = false;
		private int gridBeatsX = 4;
		private int trackBeatsX = 4;
		private bool useOctaveGrouping = true;
		private bool doConstQ = true;
		private bool doChroma = true;
		private bool doSegments = true;
		private bool doKey = true;
		private bool doSpeech = true;
		private bool useChanCfg = false;
		private int firstCobjIdx = utils.UNDEFINED;
		private int firstCsavedIndex = utils.UNDEFINED;
		private Channel[] noteChannels = null;
		private ChannelGroup[] octaveGroups = null;







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
			string mySubDir = "\\UtilORama\\" + appFolder + "\\";

			string baseDir = Path.GetTempPath();
			tempPath = baseDir + mySubDir;
			if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			//userSettingsLocalDir = baseDir + mySubDir;
			//if (!Directory.Exists(userSettingsLocalDir)) Directory.CreateDirectory(userSettingsLocalDir);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			//userSettingsRoamingDir = baseDir + mySubDir;
			//if (!Directory.Exists(userSettingsRoamingDir)) Directory.CreateDirectory(userSettingsRoamingDir);

			//baseDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			//machineSettingsDir = baseDir + mySubDir;
			//if (!Directory.Exists(machineSettingsDir)) Directory.CreateDirectory(machineSettingsDir);



			RetrieveSettings();

			SetTheControlsForTheHeartOfTheSun();

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
					fileSeqLast = Properties.Settings.Default.fileSeqLast;
					if (System.IO.File.Exists(fileSeqLast))
					{
						//seq.ReadSequenceFile(fileSeqLast);
						//fileCurrent = fileSeqLast;
						//utils.FillChannels(treChannels, seq, siNodes);
						//txtSequenceFile.Text = utils.ShortenLongPath(fileCurrent, 80);
					}
				}
				else
				{
					// 1 and only 1 file specified on command line
					//seq.ReadSequenceFile(batch_fileList[0]);
					fileSeqLast = batch_fileList[0];
					//utils.FillChannels(treChannels, seq, siNodes);
					Properties.Settings.Default.fileSeqLast = fileSeqLast;
					Properties.Settings.Default.Save();
				}
			}



			txtFileAudio.Text = utils.ShortenLongPath(fileSeqLast, 80);



			ImBusy(false);

		}

		private void RetrieveSettings()
		{
			//annotatorProgram = Properties.Settings.Default.annotatorProgram;
			doAutoSave = Properties.Settings.Default.doAutoSave;
			doAutoLaunch = Properties.Settings.Default.doAutoLaunch;
			doBeatGrid = Properties.Settings.Default.doBeatGrid;
			doBeatChannels = Properties.Settings.Default.doBeatChannels;
			doNoteOnsets = Properties.Settings.Default.doNoteOnsets;
			doPoly = Properties.Settings.Default.doPoly;
			doChroma = Properties.Settings.Default.doChroma;
			useSaveFormat = Properties.Settings.Default.useSaveFormat;
			useOctaveGrouping = Properties.Settings.Default.useOctaveGrouping;
			useRampsPoly = Properties.Settings.Default.useRampsPoly;
			//doChroma = Properties.Settings.Default.Spectrogram;
			doConstQ = Properties.Settings.Default.doConstQ;
			doSegments = Properties.Settings.Default.doSegments;
			doKey = Properties.Settings.Default.doKey;
			doSpeech = Properties.Settings.Default.doSpeech;
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
			chkPolyphonic.Checked = doPoly;
			//chkOctaveGrouping.Checked = doOctoGroup;
			chkBeatChannels.Checked = doBeatChannels;
			//optOnOff.Checked = !doRamps;
			//optRamps.Checked = doRamps;
			chkSegments.Checked = doSegments;
			chkPitch.Checked = doKey;
			chkChromagram.Checked = doChroma;
			chkConstQ.Checked = doConstQ;
			chkVocals.Checked = doSpeech;
			//SetUseChanConfig(useChanCfg);
			chkAutoLaunch.Checked = doAutoLaunch;

			RecallLastFile();
		}

		private void RecallLastFile()
		{
			fileSeqLast = Properties.Settings.Default.fileSeqLast;
			fileAudioLast = Properties.Settings.Default.fileAudioLast;
			SelectSequence(fileSeqLast);
		}

		private void SelectSequence(string theSeqFile)
		{
			string ex = Path.GetExtension(theSeqFile).ToLower();
			bool hasChanged = true;
			if (theSeqFile.ToLower().CompareTo(fileSeqLast.ToLower()) == 0) hasChanged = false;

			if (hasChanged)
			{
				fileSeqLast = theSeqFile;
				Properties.Settings.Default.fileSeqLast = fileSeqLast;
				if (System.IO.File.Exists(fileSeqLast))
				{
					string sv = Path.GetFileNameWithoutExtension(fileSeqLast);
					if (sv.Length > 6)
					{
						string tv = sv.Substring(sv.Length - 6).ToLower();
						if (tv != " tuned")
						{
							sv += " Tuned";
						}
					}
					sv += ".lms";
					txtSaveName.Text = sv;
					Properties.Settings.Default.fileSaveLast = sv;

					// If they picked an existing musical sequence
					if (ex == ".lms")
					{
						StreamReader rdr = new StreamReader(fileSeqLast);
						string lineIn = rdr.ReadLine();
						lineIn = rdr.ReadLine();
						fileAudioOriginal = utils.getKeyWord(lineIn, Music.FIELDmusicFilename);
						rdr.Close();
					}

					fileAudioLast = fileAudioOriginal;
					Properties.Settings.Default.fileAudioLast = fileAudioLast;
				}
				else
				{
					//fileAudioLast = Properties.Settings.Default.fileAudioLast;
				}
				txtFileAudio.Text = Path.GetFileName(fileAudioOriginal);
				Properties.Settings.Default.Save();
			}

			bool enable = false;
			if (System.IO.File.Exists(fileSeqLast))
			{

			}
			else
			{
				txtFileCurrent.ForeColor = System.Drawing.Color.DarkRed;
			}
			if (System.IO.File.Exists(fileAudioLast))
				{
				if (System.IO.File.Exists(fileSeqLast))
				{
					enable = true;
				}
			}
			else
			{
				txtFileAudio.ForeColor = System.Drawing.Color.DarkRed;
			}
			btnOK.Enabled = enable;
			grpGrids.Enabled = enable;
			grpTracks.Enabled = enable;
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
					if (dirtySeq)
					{
						//TODO Handle Dirty Sequence
					}
					ImBusy(true);
					string fileCurrent = batch_fileList[0];

					FileInfo fi = new FileInfo(fileCurrent);
					Properties.Settings.Default.fileSeqLast = fileCurrent;
					Properties.Settings.Default.Save();

					txtFileAudio.Text = utils.ShortenLongPath(fileCurrent, 80);
					seq.ReadSequenceFile(fileCurrent);
					SeqSelectEverthing();
					//utils.FillChannels(treChannels, seq, siNodes, false, false);
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

			if (dirtySeq)
			{
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
			string filt = "Musical Sequence (*.lms)|*.lms|Channel Configuration (*.lcc)|*.lcc";
			string tit = "Save Partial Sequence As...";
			string initDir = "";
			string initFile = "";
			initDir = utils.DefaultSequencesPath;

			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			//dlgSaveFile.FileName = Path.GetFullPath(fileCurrent) + Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString() + ext;
			dlgSaveFile.FileName = utils.DefaultSequencesPath + Path.GetFileNameWithoutExtension(fileAudioOriginal) + ".lms";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.DefaultExt = ".lms";
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = tit;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
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
			seq.WriteSequenceFile_DisplayOrder(newFilename, true, false);

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


		private void MakeDumbGrid()
		{
			//TimingGrid tg = new TimingGrid("Fixed Grid .05");
			TimingGrid tg = GetGrid("Fixed Grid .05");
			tg.spacing = 5;
			//seq.AddTimingGrid(tg);
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
					int errs = ClearTempDir();
					AnalyzeSong(dlgOpenFile.FileName);
					grpSequence.Enabled = true;
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


		private void AddChildToParent(IMember child, IMember parent)
		{
			// Tests for, and works with either a track or a channel group as the parent
			if (parent.MemberType == MemberType.Track)
			{
				Track trk = (Track)parent;
				trk.Members.Add(child);
			}
			if (parent.MemberType == MemberType.ChannelGroup)
			{
				ChannelGroup grp = (ChannelGroup)parent;
				grp.Members.Add(child);
			}


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
			doPoly = chkPolyphonic.Checked;
			Properties.Settings.Default.doPoly = doPoly;
			Properties.Settings.Default.Save();
		}

		private void chkBeatsTrack_CheckedChanged(object sender, EventArgs e)
		{
			doBeatChannels = chkBeatChannels.Checked;
			Properties.Settings.Default.doBeatChannels = doBeatChannels;
			Properties.Settings.Default.Save();
		}





		#endregion

		private void CentiFixx(int newCentiseconds)
		{
			// If we started from a Channel config, then none of the channels, groups, etc. has a length
			// Fill all those in with the sequence's total Centiseconds (taken from the audio file length)

			if (newCentiseconds < 1) newCentiseconds = seq.Tracks[0].Centiseconds;
			if (newCentiseconds > 75000) newCentiseconds = seq.Tracks[0].Centiseconds;
			seq.Centiseconds = newCentiseconds;

			for (int ch=0; ch< seq.Channels.Count; ch++)
			{
				seq.Channels[ch].Centiseconds = newCentiseconds;
			}
			for (int rch=0; rch< seq.RGBchannels.Count; rch++)
			{
				seq.RGBchannels[rch].Centiseconds = newCentiseconds;
			}
			for (int chg=0; chg< seq.ChannelGroups.Count; chg++)
			{
				seq.ChannelGroups[chg].Centiseconds = newCentiseconds;
			}
			for (int tr=0; tr<seq.Tracks.Count; tr++)
			{
				seq.Tracks[tr].Centiseconds = newCentiseconds;
			}
			
		} // end CentiFixx

		private void chkSpectrogram_CheckedChanged(object sender, EventArgs e)
		{
			//doChroma = chkSpectrogram.Checked;
			Properties.Settings.Default.doSpectrogram = doChroma;
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

			//! FOR DEBUGGING AND TESTING
			txtFileCurrent.Text = fileSeqLast;
			btnBrowseAudio.PerformClick();
			//! REMOVE WHEN DONE

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


		private void btnSaveSequence_Click(object sender, EventArgs e)
		{
			string filter = "Musical Sequence *.lms|*.lms";
			string idr = utils.DefaultSequencesPath;
			bool saveAsk = true;

			//string ifile = Path.GetFileNameWithoutExtension(fileCurrent);
			string ifile = txtSaveName.Text;
			if (ifile.Length < 2)
			{
				ifile = seq.info.music.Title + " by " + seq.info.music.Artist;
				string q = utils.FormatTime(seq.Tracks[0].Centiseconds);
				if (seq.Tracks[0].Centiseconds > 5999)
				{
					q = q.Replace(':', '.');
				}
				else
				{
					q = q.Substring(0, 5);
				}
				ifile += q + " Tuned";
			}
			ifile +=  ".lms";

			dlgSaveFile.Filter = filter;
			dlgSaveFile.InitialDirectory = idr;
			dlgSaveFile.FileName = ifile;
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.OverwritePrompt = true; //  false; // Handled Below
			dlgSaveFile.Title = "Save Sequence As...";
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
						if (fn.Length > 6)
						{
							ed = fn.Substring(fn.Length - 6);
						}
						if (ed != " tuned")
						{
							string t = Path.GetFileName(dlgSaveFile.FileName);
							t += " already exists.\r\nDo you want to replace it?";
							DialogResult dr3 = MessageBox.Show(this, t, "Confirm Save As", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
							//if (dr3 == DialogResult.Cancel) saveAsk = true;
							if (dr3 == DialogResult.No) doSave = false;
						}
					}
					if (doSave)
					{
						fileSeqSave = dlgSaveFile.FileName;
						CentiFixx(seq.Tracks[0].Centiseconds);
						txtSaveName.Text = Path.GetFileNameWithoutExtension(fileSeqSave);
						SaveSequence(fileSeqSave);
						saveAsk = false;
						if (doAutoLaunch)
						{
							System.Diagnostics.Process.Start(fileSeqSave);
						}
					}
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
				SelectSequence(dlgOpenFile.FileName);
					
					btnBrowseAudio.Focus();

			}
		}

		private void chkChromagram_CheckedChanged(object sender, EventArgs e)
		{
			doChroma = chkChromagram.Checked;
			Properties.Settings.Default.doChroma = doChroma;
			Properties.Settings.Default.Save();
		}

		private void chkSegments_CheckedChanged(object sender, EventArgs e)
		{
			doSegments = chkSegments.Checked;
			Properties.Settings.Default.doSegments = doSegments;
			Properties.Settings.Default.Save();
		}

		private void chkPitch_CheckedChanged(object sender, EventArgs e)
		{
			doKey = chkPitch.Checked;
			Properties.Settings.Default.doKey = doKey;
			Properties.Settings.Default.Save();
		}

		

		private void SeqSelectEverthing()
		{
			foreach(Channel ch in seq.Channels)
			{
				ch.Selected = true;
			}
			foreach (RGBchannel rch in seq.RGBchannels)
			{
				rch.Selected = true;
			}
			foreach (ChannelGroup chg in seq.ChannelGroups)
			{
				chg.Selected = true;
			}
			foreach (Track tr in seq.Tracks)
			{
				tr.Selected = true;
			}
			foreach (TimingGrid tg in seq.TimingGrids)
			{
				tg.Selected = true;
			}
		}

		private void chkVocals_CheckedChanged(object sender, EventArgs e)
		{
			doSpeech = chkVocals.Checked;
			Properties.Settings.Default.doSpeech = doSpeech;
			Properties.Settings.Default.Save();
		}

		private int ClearTempDir()
		{
			int errs = 0;

			string[] philes = Directory.GetFiles(tempPath);
			foreach (string phile in philes)
			{
				try
				{ 
					System.IO.File.Delete(phile);
				}
				catch
				{
					errs++;
				}
			}
			return errs;
		}

		private void pnlVamping_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.Blue, 0, 0, pnlVamping.Width - 1, pnlVamping.Height - 1);
		}

		private void SelectStep(int theStep)
		{
			if (theStep == 1)
			{
				lblStep1.ForeColor = System.Drawing.Color.Red;
				grpSequence.Font = new Font(grpSequence.Font, FontStyle.Bold);
				grpSequence.Enabled = true;
			}
			else
			{
				lblStep1.ForeColor = SystemColors.Highlight;
				grpSequence.Font = new Font(grpSequence.Font, FontStyle.Regular);
			}
			if (theStep == 2)
			{

			}

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{

			seq.ReadSequenceFile(fileSeqLast);
			SeqSelectEverthing();



			string musicFile = fileAudioLast;
			if (Path.GetDirectoryName(fileAudioLast).Length < 3)
			{
				fileAudioOriginal = utils.DefaultAudioPath + fileAudioOriginal;
				grpSequence.Enabled = true;
				btnSaveSequence.Focus();
			}


			if (System.IO.File.Exists(fileAudioLast))
			{
				int errs = ClearTempDir();
				AnalyzeSong(fileAudioLast);
				grpSequence.Enabled = true;

				List<TreeNode> nodesSI = new List<TreeNode>();
				utils.FillChannels(treeMaster, seq, nodesSI, true, false);

				btnSaveSequence.Focus();
			}
			else
			{
				string msg = "Song file '" + Path.GetFileName(fileAudioOriginal) + "' not found";
				msg += " in '" + Path.GetDirectoryName(fileAudioOriginal) + "'.";
				MessageBox.Show(this, msg, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			}

		}
	} // end frmTune
} // end namespace tuneorama
