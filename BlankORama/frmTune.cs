using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using LORUtils;
using System.Windows.Forms;

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

		private string fileSeqCur = "";  // Currently loaded Sequence File
		private string fileSeqLast = ""; // Last Sequence File Loaded
		private string fileSeqSave = ""; // Last Saved Sequence
		private Sequence seq = new Sequence();
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
		private byte saveFormat = SAVEmixedDisplay;

		//private List<TreeNode>[] siNodes;
		private List<List<TreeNode>> siNodes = new List<List<TreeNode>>();

		private bool firstShown = false;
		private TreeNode previousNode = null;



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
			string mySubDir = "\\Util-O-Rama\\" + Application.ProductName + "\\";
			tempPath = appDataDir + mySubDir;

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
					fileSeqLast = Properties.Settings.Default.FileSeqLast;
					if (File.Exists(fileSeqLast))
					{
						//seq.ReadSequenceFile(fileSeqLast);
						//fileSeqCur = fileSeqLast;
						//utils.FillChannels(treChannels, seq, siNodes);
						//txtSequenceFile.Text = utils.ShortenLongPath(fileSeqCur, 80);
					}
				}
				else
				{
					// 1 and only 1 file specified on command line
					//seq.ReadSequenceFile(batch_fileList[0]);
					fileSeqLast = batch_fileList[0];
					//utils.FillChannels(treChannels, seq, siNodes);
					Properties.Settings.Default.FileSeqLast = fileSeqLast;
					Properties.Settings.Default.Save();
				}
			}



			txtSequenceFile.Text = utils.ShortenLongPath(fileSeqLast, 80);



			ImBusy(false);

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
				if (arg.Substring(4).IndexOf(".") > LOR.UNDEFINED) isFile++;  // contains a period
				if (utils.InvalidCharacterCount(arg) == 0) isFile++;
				if (isFile == 3)
				{
					if (File.Exists(arg))
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
				//if (arg.Substring(4).IndexOf(".") > LOR.UNDEFINED) isFile++;  // contains a period
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
					fileSeqCur = batch_fileList[0];

					FileInfo fi = new FileInfo(fileSeqCur);
					Properties.Settings.Default.FileSeqLast = fileSeqCur;
					Properties.Settings.Default.Save();

					txtSequenceFile.Text = utils.ShortenLongPath(fileSeqCur, 80);
					seq.ReadSequenceFile(fileSeqCur);
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
		} // end ImBusy


		private DialogResult AskSaveSequence()
		{
			DialogResult ret = DialogResult.OK;
			if (dirtySeq)
			{
				string msg = "Your selections have changed.\r\n\r\n";
				msg += "Do you want to save the selected channels to a new sequence?";
				ret = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1);
				if (ret == DialogResult.Yes)
				{
					btnSaveSequence.PerformClick();
				}
			}
			return ret;
		}

		private void frmTune_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Form closing?  Disable controls
			ImBusy(true);


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
				string where = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				string when = "\\Util-O-Rama\\" + applicationName + "\\";
				if (!Directory.Exists(where + "\\Util-O-Rama"))
				{
					Directory.CreateDirectory(where + when);
				}
				string what = "fileSelLast.ChSel";
				string file = where + when + what;
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

		private void staStatus_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = Sequence.DefaultSequencesPath;
			string initFile = "";



			string filt = "All Sequences (*.las, *.lms, *.lcc)|*.las;*.lms;*.lcc|Musical Sequences only (*.lms)|*.lms";
			filt += "|Animated Sequences only (*.las)|*.las|Channel Configurations only(*.lcc)|*.lcc";
			dlgOpenFile.Filter = filt;
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select a Sequence...";
			//pnlAll.Enabled = false;
			DialogResult result = dlgOpenFile.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				LoadSequence(dlgOpenFile.FileName, true);
			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;
		} // end browse for First File

		private void LoadSequence(string theFile, bool remember)
		{
			ImBusy(true);
			if (remember)
			{
				fileSeqLast = fileSeqCur;
				fileSeqCur = theFile;

				Properties.Settings.Default.FileSeqLast = fileSeqCur;
				Properties.Settings.Default.Save();
			}
			this.Text = applicationName + " - " + Path.GetFileName(theFile);

			txtSequenceFile.Text = utils.ShortenLongPath(theFile, 80);
			seq.ReadSequenceFile(theFile);
			//utils.FillChannels(treChannels, seq, siNodes, false, false);
			//part = 1;
			dirtySeq = false;
			ImBusy(false);
		} // end load sequence

		private void btnSave_Click(object sender, EventArgs e)
		{
			string newFileIn;
			string newFileOut;
			string filt = "";
			string tit = "";
			string ext = Path.GetExtension(fileSeqCur).ToLower();
			if (ext.CompareTo(".las") == 0)
			{
				filt = "Animated Sequence (*.las)|*.las|Channel Configuration (*.lcc)|*.lcc";
				tit = "Save Partial Sequence As...";
			}
			if (ext.CompareTo(".lms") == 0)
			{
				filt = "Musical Sequence (*.lms)|*.lms|Channel Configuration (*.lcc)|*.lcc";
				tit = "Save Partial Sequence As...";
			}
			if (ext.CompareTo(".lcc") == 0)
			{
				filt = "Channel Configuration (*.lcc)|*.lcc";
				tit = "Save Partial Channel Configuration As...";
			}
			string initDir = "";
			string initFile = "";
			if (fileSeqCur.Length > 5)
			{
				if (Directory.Exists(Path.GetDirectoryName(fileSeqCur)))
				{
					initDir = Path.GetDirectoryName(fileSeqCur);
					initFile = Path.GetFileNameWithoutExtension(fileSeqCur); // + " Part " + part.ToString(); // + ext;
				}
			}
			if (initDir.Length < 5)
			{
				if (fileSeqLast.Length > 5)
				{
					if (Directory.Exists(Path.GetDirectoryName(fileSeqLast)))
					{
						initDir = Path.GetDirectoryName(fileSeqLast);
						initFile = Path.GetFileNameWithoutExtension(fileSeqLast); // + " Part " + part.ToString(); // + ext;
					}
				}
			}
			if (initDir.Length < 5)
			{
				// Can't imagine that we would ever make it this far, but, just in case...
				if (ext.CompareTo(".lcc") == 0)
				{
					initDir = utils.DefaultChannelConfigsPath;
				}
				else
				{
					initDir = Sequence.DefaultSequencesPath;
				}
			}

			dlgSaveFile.Filter = filt;
			dlgSaveFile.FilterIndex = 1;
			//dlgSaveFile.FileName = Path.GetFullPath(fileSeqCur) + Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + part.ToString() + ext;
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.InitialDirectory = initDir;
			dlgSaveFile.DefaultExt = ext;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.Title = tit;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + part.ToString(); // + ext;
			//newFileIn = "Part " + part.ToString() + " of " + Path.GetFileNameWithoutExtension(fileSeqCur);
			//newFileIn = "Part Mother Fucker!!";
			dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				SaveSequence(dlgSaveFile.FileName);
			}
			/*
			//! For testing only...
				frmOptions options = new frmOptions();
				options.InitForm(false);
				DialogResult dr2 = options.ShowDialog(this);
				if (dr2 == DialogResult.OK)
				{
					this.Cursor = Cursors.WaitCursor;
					int saveFormat = options.saveFormat;
					CopySelectionsToSequence();
					if (saveFormat == frmOptions.SAVEmixedDisplay)
					{
						// normal default when not testing
						seq.WriteSequenceFileInDisplayOrder(newFileOut, true);
					}
					if (saveFormat == frmOptions.SAVEcrgDisplay)
					{
						seq.WriteSequenceFileInCRGDisplayOrder(newFileOut, true);
					}
					if (saveFormat == frmOptions.SAVEcrgAlpha)
					{
						seq.WriteSequenceFileInCRGAlphaOrder(newFileOut, true);
					}
					System.Media.SystemSounds.Beep.Play();
					part++;
					dirtySeq = false;
				}
			//}
			this.Cursor = Cursors.Default;
			*/

		} // end Save File As

		private void SaveSequence(string newFilename)
		{
			ImBusy(true);
			if (saveFormat == SAVEmixedDisplay)
			{
				// normal default when not testing
				seq.WriteSequenceFileInDisplayOrder(newFilename, true, false);
			}
			if (saveFormat == SAVEcrgDisplay)
			{
				seq.WriteSequenceFileInCRGDisplayOrder(newFilename, true, false);
			}
			if (saveFormat == SAVEcrgAlpha)
			{
				seq.WriteSequenceFileInCRGAlphaOrder(newFilename, true, false);
			}
			System.Media.SystemSounds.Beep.Play();
			dirtySeq = false;
			fileSeqSave = newFilename;
			ImBusy(false);


		}

		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			ShowSaveOptions();
		}

		private DialogResult ShowSaveOptions()
		{
			frmOptions options = new frmOptions();
			options.InitForm(false);
			DialogResult dr2 = options.ShowDialog(this);
			if (dr2 == DialogResult.OK)
			{
				//saveFormat = options.saveFormat;
				//Properties.Settings.Default.SaveFormat = saveFormat;
			}

			return dr2;
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



	} // end frmTune
} // end namespace tuneorama
