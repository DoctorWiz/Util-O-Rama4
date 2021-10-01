using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Media;
using LORUtils4;
using FileHelper;
using FuzzyString;


namespace UtilORama4
{
	public partial class frmSplit : Form
	{
		// I decided not to enum these, may change my mind later
		public const byte SAVEmixedDisplay = 1;
		public const byte SAVEcrgDisplay = 2;
		public const byte SAVEcrgAlpha = 3;
		// end enums

		public const char DELIM1 = '⬖';
		public const char DELIM4 = '⬙';

		public bool useFuzzy = true;
		public bool caseSensitive = true;
		public int useAlgorithms = 0;
		public long prematchAlgorithm = 2048;
		public long finalAlgorithms = 6328448;
		public int minPrematchScore = 85;
		public int minFinalMatchScore = 95;
		public bool writeLog = true;

		private string fileSeqCur = "";  // Currently loaded LORSequence4 File
		private string fileSeqLast = ""; // Last LORSequence4 File Loaded
		private string fileSeqSave = ""; // Last Saved Sequence
		private string fileSelCur = ""; // Currently loaded Selections File
		private string fileSelLast = "";
		private LORSequence4 seq = new LORSequence4();
		private int nodeIndex = lutils.UNDEFINED;
		private int selectionCount = 0;
		private int member = 1;
		private bool dirtySel = false;
		private bool dirtySeq = false;
		private const string helpPage = "http://wizlights.com/utilorama/splitorama";
		private string applicationName = "Split-O-Rama";
		private string thisEXE = "split-o-rama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte saveFormat = SAVEmixedDisplay;
		private int gridSelCount = 0;
		private bool[] gridItem_Checked = null;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;

		//private List<TreeNode>[] nodesBySI;
		//private List<List<TreeNode>> nodesBySI = new List<List<TreeNode>>();
		private List<TreeNode>[] nodesBySI;

		private bool firstShown = false;
		private TreeNode previousNode = null;

		//private	string[] trackNames = null;
		//private int[] trackNums = null;
		//private string[] channelGroupNames = null;
		//private int[] channelGroupSIs = null;
		//private string[] rgbChannelNames = null;
		//private int[] rgbChannelSIs = null;
		//private string[] channelNames = null;
		//private int[] channelSIs = null;

		private Color COLOREDTEXT = System.Drawing.Color.Blue;
		private Color REGULARTEXT = System.Drawing.Color.Black;
		private Color HIGHLIGHTBACKGROUND = System.Drawing.Color.Yellow;
		private Color REGULARBACKGROUND = System.Drawing.Color.White;

		private int treeClicks = 0;

		public frmSplit()
		{
			InitializeComponent();
			//? Why does this work in the about form, but not here???
			//applicationName = AssemblyTitle;
		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = LORSequence4.DefaultSequencesPath;
			string initFile = "";



			string filt = "All Sequences (*.las, *.lms, *.lcc)|*.las;*.lms;*.lcc|Musical Sequences only (*.lms)|*.lms";
			filt += "|Animated Sequences only (*.las)|*.las|LORChannel4 Configurations only(*.lcc)|*.lcc";
			dlgFileOpen.Filter = filt;
			dlgFileOpen.DefaultExt = "*.lms";
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Select a Sequence...";
			//pnlAll.Enabled = false;
			DialogResult result = dlgFileOpen.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				LoadSequence(dlgFileOpen.FileName, true);



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

			txtSequenceFile.Text = Fyle.ShortenLongPath(theFile, 80);
			seq.ReadSequenceFile(theFile);
			Array.Resize(ref gridItem_Checked, seq.TimingGrids.Count);
			lutils.TreeFillChannels(treChannels, seq, ref nodesBySI, false, false);
			FillGridList();
			member = 1;
			dirtySeq = false;
			ImBusy(false);

			if (fileSelCur.Length > 4)
			{
				string msg = "Apply Selections to Sequence File \"" + Path.GetFileName(theFile) + "\" ?";
				DialogResult dr = MessageBox.Show(this, msg, "Apply Selections?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					LoadApplySelections(fileSelCur, false, useFuzzy);
				}
			}
			else
			{
				if (File.Exists(fileSelLast))
				{
					string msg = "Load and apply the last selections file:'";
					msg += Path.GetFileNameWithoutExtension(fileSelLast) + "'?";
					DialogResult dr2 = MessageBox.Show(this, msg, "Apply Selections?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (dr2 == DialogResult.Yes)
					{
						LoadApplySelections(fileSelLast, true, useFuzzy);
					}
				}
			}
		}

		private void FillGridList()
		{
			string descr = "";
			for (int tg = 0; tg < seq.TimingGrids.Count; tg++)
			{
				LORTimings4 theGrid = seq.TimingGrids[tg];
				descr = theGrid.Name;
				descr += " \t";
				if (theGrid.LORTimingGridType4 == LORTimingGridType4.FixedGrid)
				{
					descr += "Fixed: ";
					string tmg = lutils.FormatTime(theGrid.spacing);
					descr += tmg.Substring(tmg.Length - 4) + ": \t";
					for (int x=1; x<4; x++)
					{
						int c = theGrid.spacing * x;
						string tc = lutils.FormatTime(c);
						descr += tc.Substring(tc.Length - 4);
						if (x<3)
						{
							descr += ", ";
						}
					}
				}
				if (theGrid.LORTimingGridType4 == LORTimingGridType4.Freeform)
				{
					descr += "Freeform: \t";
					for (int x=0; x<3; x++)
					{
						if (x < theGrid.timings.Count)
						{
							string tc = lutils.FormatTime(theGrid.timings[x]);
							descr += tc.Substring(tc.Length - 4);
							if (x<2)
							{
								descr += ", ";
							}
						}
					}
				}
				lstGrids.Items.Add(descr);
			}



		}

		private void frmSplit_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{

			ImBusy(true);
			RestoreFormPosition();
			tempPath = Fyle.GetTempPath();
			bool valid = false;

			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			//TODO: These may get overridden by command line arguments (not yet supported)
			LoadFuzzyOptions();
			saveFormat = Properties.Settings.Default.SaveFormat;
			if ((minPrematchScore < 500) || (minPrematchScore > 1000))
			{
				minPrematchScore = 800;
			}
			if ((minFinalMatchScore < 500) || (minFinalMatchScore > 1000))
			{
				minFinalMatchScore = 950;
			}





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
					if (fileSelLast.Length > 6)
					{
						valid = Fyle.IsValidPath(fileSeqLast, true);
					}
					if (!valid) fileSeqLast = lutils.DefaultSequencesPath;
					if (File.Exists(fileSeqLast))
					{
						//seq.ReadSequenceFile(fileSeqLast);
						//fileSeqCur = fileSeqLast;
						//lutils.TreeFillChannels(treChannels, seq, nodesBySI);
						//txtSequenceFile.Text = Fyle.ShortenLongPath(fileSeqCur, 80);
					}
				}
				else
				{
					// 1 and only 1 file specified on command line
					//seq.ReadSequenceFile(batch_fileList[0]);
					fileSeqLast = batch_fileList[0];
					//lutils.TreeFillChannels(treChannels, seq, nodesBySI);
					Properties.Settings.Default.FileSeqLast = fileSeqLast;
					Properties.Settings.Default.Save();
				}
			}

			if (cmdSelectionsFile.Length > 6)
			{
				fileSelLast = cmdSelectionsFile;
				Properties.Settings.Default.FileSelLast = fileSelLast;
				Properties.Settings.Default.Save();
			}
			else
			{
				fileSelLast = Properties.Settings.Default.FileSelLast;
			}
			valid = false;
			if (fileSelLast.Length > 6)
			{
				valid = Fyle.IsValidPath(fileSelLast, true);
			}
			if (!valid) fileSelLast = lutils.DefaultChannelConfigsPath;


			//txtSequenceFile.Text = Fyle.ShortenLongPath(fileSeqLast, 80);
			//txtSelectionsFile.Text = Fyle.ShortenLongPath(fileSelLast, 80);

			cmdNothing.Visible = isWiz;

			treChannels.DrawMode = TreeViewDrawMode.OwnerDrawAll;

			ImBusy(false);

		}

		private void ProcessCommandLine()
		{
			commandArgs = Environment.GetCommandLineArgs();
			string arg;
			for (int f=0; f< commandArgs.Length; f++)
			{
				arg = commandArgs[f];
				// Does it LOOK like a file?
				byte isFile = 0;
				if (arg.Substring(1, 2).CompareTo(":\\") == 0) isFile = 1;  // Local File
				if (arg.Substring(0, 2).CompareTo("\\\\") == 0) isFile = 1; // UNC file
				if (arg.Substring(4).IndexOf(".") > lutils.UNDEFINED) isFile++;  // contains a period
				if (Fyle.InvalidCharacterCount(arg) == 0) isFile++;
				if (isFile == 3)
				{
					if (File.Exists(arg))
					{
						string ext = Path.GetExtension(arg).ToLower();
						if (ext.CompareTo( ".exe") == 0)
						{
							if (f == 0)
							{
								thisEXE = arg;
							}
						}
						if ((ext.CompareTo(".lms")== 0) || (ext.CompareTo(".las")==0))
						{
							Array.Resize(ref batch_fileList, batch_fileCount + 1);
							batch_fileList[batch_fileCount] = arg;
							batch_fileCount++;
						}
						else
						{
							if (ext.CompareTo("chsel")==0)
							{
								cmdSelectionsFile = arg;
							}
						}
					}
				}else
				{
					// Not a file, is it an argument
					if (arg.Substring(0,1).CompareTo("/") == 0)
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
				//if (arg.Substring(4).IndexOf(".") > lutils.UNDEFINED) isFile++;  // contains a period
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

					txtSequenceFile.Text = Fyle.ShortenLongPath(fileSeqCur, 80);
					seq.ReadSequenceFile(fileSeqCur);
					lutils.TreeFillChannels(treChannels, seq, ref nodesBySI, false, false);
					member = 1;
					dirtySeq = false;
					ImBusy(false);

				}
				if (inclSelections)
				{
					if (dirtySel)
					{
						//TODO: Handle Dirty Map
					}
				}
				if (inclSelections)
				{
					ImBusy(true);
					fileSelCur = cmdSelectionsFile;
					txtSelectionsFile.Text = Path.GetFileName(fileSelCur);
					Properties.Settings.Default.FileSelLast = fileSelCur;
					Properties.Settings.Default.Save();
					// No new sequences, just the selections, apply selections to already loaded sequence
					LoadApplySelections(fileSelCur, true, useFuzzy);
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

		private DialogResult AskSaveSelections()
		{
			DialogResult ret = DialogResult.OK;
			if (dirtySel)
			{
				string msg = "Your selections have changed.\r\n\r\n";
				msg += "Do you want to save the selections to a Channel Selection file?";
				ret = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button1);
				if (ret == DialogResult.Yes)
				{
					btnSaveSelections.PerformClick();
				}
			}
			return ret;
		}

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

		private int IncrementSelectionCount(int amount)
		{
			selectionCount += amount;
			dirtySel = true;
			dirtySeq = true;
			bool stuffToSave = false;
			if ((selectionCount > 0) && (gridSelCount > 0)) stuffToSave = true;

			btnSaveSelections.Enabled = stuffToSave;
			btnSaveSequence.Enabled = stuffToSave;
			btnClear.Enabled = stuffToSave;
			lblSelectionCount.Text = selectionCount.ToString() + " / " + gridSelCount.ToString();

			return selectionCount;
		}


		private void LoadApplyPreviousSelections()
		{
			string lastSelFile = "fileSelLast.ChSel";
			string file = tempPath + lastSelFile;
			if (File.Exists(file))
			{
				LoadApplySelections(file, false, useFuzzy);
			}
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
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;

			if ((FormBorderStyle == FormBorderStyle.Sizable) || (FormBorderStyle == FormBorderStyle.SizableToolWindow))
				{
				if (w < MinimumSize.Width) w = MinimumSize.Width;
				if (h < MinimumSize.Height) h = MinimumSize.Height;
			}
			else // Fixed Border Style
			{
				if (w < Size.Width) w = Size.Width;
				if (h < Size.Height) h = Size.Height;
			}
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

		} // End LoadFormPostion

		private void CloseForm()
		{
			SaveFormPosition();
		}



		private void frmSplit_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Form closing?  Disable controls
			ImBusy(true);

			DialogResult result = AskSaveSelections();
			if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
			else
			{
				if (result == DialogResult.Yes)
				{
					btnSaveSelections.PerformClick();
				}
			}

			result = AskSaveSequence();
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
				//string where = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				//string when = "\\Util-O-Rama\\" + applicationName + "\\";
				//if (!Directory.Exists(where + "\\Util-O-Rama"))
				//{
				//	Directory.CreateDirectory(where + when);
				//}
				string what = "LastSelections.ChSel";
				string file = tempPath + what;
				SaveSelections(file);
				CloseForm();
			}
			else
			{
				// Close cancelled, reenable controls
				ImBusy(false);
			}
		}

		private void treChannels_AfterSelect(object sender, TreeViewEventArgs e)
		{
			//! Debugging - Why is this event firinging seemingly more than it should?
			treeClicks++;
			lblTreeClicks.Text = treeClicks.ToString();


			if (previousNode == null)
			{
				previousNode = e.Node;
				UserSelectNode(e.Node);
			}
			else
			{
				if (previousNode != e.Node)
				{
					previousNode = e.Node;
					UserSelectNode(e.Node);
				}
			}

			if (e.Node.Tag != null)
			{
				iLORMember4 m = (iLORMember4)e.Node.Tag;
				if (m.MemberType == LORMemberType4.Channel)
				{
					LORChannel4 ch = (LORChannel4)m;
					Bitmap bmp = lutils.RenderEffects(ch, 0, ch.Centiseconds, 300, 20, true);
					picPreview.Visible = true;
					picPreview.Image = bmp;
					//picPreview.Refresh();
				}
				if (m.MemberType == LORMemberType4.RGBChannel)
				{
					LORRGBChannel4 rgb = (LORRGBChannel4)m;
					Bitmap bmp = lutils.RenderEffects(rgb, 0, seq.Centiseconds, 300, 21, false);
					picPreview.Image = bmp;
					picPreview.Visible = true;
				}
				if ((m.MemberType == LORMemberType4.ChannelGroup) || (m.MemberType == LORMemberType4.Track))
				{
					picPreview.Visible = false;
				}
			}


		}

		private void UserSelectNode(TreeNode nOde)
		{ 
			iLORMember4 m = null;
			bool ignoreRGB = false;
			m = (iLORMember4)nOde.Tag;
			string foo = nOde.Text;  //! FOR DEBUGGING, REMOVE LATER
			if (nOde.Parent != null)
			{
				iLORMember4 nt = (iLORMember4)nOde.Parent.Tag;
				if (nt.MemberType == LORMemberType4.RGBChannel)
				{
					// If the node clicked on has a parent,
					// and that parent is an LORRGBChannel4,
					// Then this is an RGB sub channel,
					// So IGNORE this click
					ignoreRGB = true;
				}
			}
			if (!ignoreRGB)
			{
				SelectNode(nOde, !m.Selected);
			}
			//? Why doesn't this work?
			//btnSaveSelections.Enabled = (selectionCount > 0);
			if (selectionCount > 0)
			{
				btnSaveSelections.Enabled = true;
				btnSaveSequence.Enabled = true;
				btnClear.Enabled = true;
			}
			else
			{
				btnSaveSelections.Enabled = false;
				btnSaveSequence.Enabled = false;
				btnClear.Enabled = false;
			}
			lblSelectionCount.Text = selectionCount.ToString();
			//cmdNothing.Focus();

			if (m.MemberType == LORMemberType4.Track)
			{
				if (m.Selected)
				{
					LORTrack4 trk = (LORTrack4)m;
					gridItem_Checked[trk.timingGrid.SaveID] = true;
					lstGrids.Refresh();
				}
			}

			treChannels.SelectedNode = null;
			//txtSelectionsFile.Focus();

		}

		void SelectNode(TreeNode nOde, bool select)
		{
			// Set this one
			iLORMember4 m = (iLORMember4)nOde.Tag;
			m.Selected = select;
			HighlightNodeBackground(nOde, select);
			//SelectChannel(ci.SavedIndex, select);
			SelectChannel(nOde, select);
			if (select)
			{
				IncrementSelectionCount(1);
			}
			else
			{
				IncrementSelectionCount(-1);
			}
			if (nOde.Nodes.Count > 0)
			{
				// And all seq.Members
				SelectChildNodes(nOde.Nodes, select);
			}
			// and now parents
			TreeNode pArent = nOde.Parent;
			while (pArent != null)
			{
				iLORMember4 pm = (iLORMember4)pArent.Tag;
				if (select)
				{
					// If turning on the select, turn on each parent
					if (pm.Selected != select)
					{
						pm.Selected = select;
						HighlightNodeBackground(pArent, select);
						IncrementSelectionCount(1);
					}
				}
				else
				{
					bool otherMembers = HasSelectedMembers(pArent);
					if (otherMembers)
					{
						// Has other seq.Members that are still selected
						// therefore, do not unhighlight it
					}
					else
					{
						// Has no remaining seq.Members selected
						// OK to unhighlight it
						if (pm.Selected != select)
						{
							pm.Selected = false;
							HighlightNodeBackground(pArent, select);
							IncrementSelectionCount(-1);
						}
					}
				}
				pArent = pArent.Parent;
			}
		} // end highlight node

		void SelectChildNodes(TreeNodeCollection nOdes, bool select)
		{
			foreach (TreeNode nOde in nOdes)
			{
				iLORMember4 m = (iLORMember4)nOde.Tag;
				// Highlight all seq.Members
				if (m.Selected != select)
				{
					m.Selected = select;
					HighlightNodeBackground(nOde, select);
					if (select)
					{
						IncrementSelectionCount(1);
					}
					else
					{
						IncrementSelectionCount(-1);
					}
					if (nOde.Nodes.Count > 0)
					{
						SelectChildNodes(nOde.Nodes, select);
					}
				}
			}
		} // end highlight child nodes

		void SelectChannel(TreeNode nOde, bool select)
		{
			iLORMember4 m = (iLORMember4)nOde.Tag;
			List<TreeNode> chNodes = nodesBySI[m.SavedIndex];
			int selCount = 0;
			bool selSome = false;

			for (int cx=0; cx< chNodes.Count; cx++)
			{
				iLORMember4 m2 = (iLORMember4)chNodes[cx].Tag;
				if (m2.Selected)
				{
					selCount++;
				}
			}
			if (selCount > 0) selSome = true; else selSome = false;
			//selSome = (selCount > 0);  //? Why doesn't this work?
			for (int cx = 0; cx < chNodes.Count; cx++)
			{
				ItalisizeNode(chNodes[cx], selSome);
			}
		}


		void SelectNodes(int nodeSI, bool select, bool andMembers)
		{
			iLORMember4 m;
			List<TreeNode> qlist;

			//if (nodesBySI[nodeSI]!= null)
			if (nodeSI == lutils.UNDEFINED)
			{
				// WHY?
				int xx = 1;
			}
			else
			{
				qlist = nodesBySI[nodeSI];
				if (qlist.Count > 0)
				{
					//if (nodesBySI[nodeSI].Length > 0)
					//if (nodesBySI[nodeSI])
					//{
					//foreach (TreeNode nOde in nodesBySI[nodeSI])
					for (int q = 0; q < nodesBySI[nodeSI].Count; q++)
					{
						TreeNode nOde = nodesBySI[nodeSI][q];
						m = (iLORMember4)nOde.Tag;
						if (select)
						{
							if (!m.Selected) // sanity check, should not be checked
							{
								m.Selected = true;
								IncrementSelectionCount(1);
								nOde.ForeColor = Color.Yellow;
								nOde.BackColor = Color.DarkBlue;
								if (andMembers)
								{
									if (nOde.Nodes.Count > 0)
									{
										foreach (TreeNode childNode in nOde.Nodes)
										{
											m = (iLORMember4)childNode.Tag;
											SelectNodes(m.SavedIndex, select, true);
										}
									}
								}
								if (nOde.Parent != null)
								{
									m = (iLORMember4)nOde.Parent.Tag;
									SelectNodes(m.SavedIndex, select, false);
								}
							} // node.!checked
						}
						else // !select
						{
							if (m.Selected) // sanity check, should be checked
							{
								m.Selected = false;
								IncrementSelectionCount(-1);
								nOde.ForeColor = SystemColors.WindowText;
								nOde.BackColor = SystemColors.Window;
								if (andMembers)
								{
									if (nOde.Nodes.Count > 0)
									{
										foreach (TreeNode childNode in nOde.Nodes)
										{
											m = (iLORMember4)childNode.Tag;
											SelectNodes(m.SavedIndex, select, true);
										}
									}
								}
								if (nOde.Parent != null)
								{
									if (!HasSelectedMembers(nOde.Parent))
									{
										m = (iLORMember4)nOde.Parent.Tag;
										SelectNodes(m.SavedIndex, select, false);
									}
								}
							} // node.checked
						} // if (select)
					} // foreach (TreeNode nOde in nodesBySI[nodeSI])
						//}
				} // nodesBySI[nodeSI].Count > 0
				else
				{
					// nodesBySI[nodeSI].Count = 0
					//? Why Not?
					int x = 1;
				}
			}
		} // end SelectNode

		private bool HasSelectedMembers(TreeNode nOde)
		{
			bool ret = false;
			if (nOde.Nodes.Count > 0)
			{
				foreach (TreeNode childNode in nOde.Nodes)
				{
					iLORMember4 m = (iLORMember4)childNode.Tag;
					ret = m.Selected;
					if (ret)
					{
						break;
					}
				}
			}
			return ret;
		} // end HasSelectedMembers

		#region Node Appearnce
		private void ItalisizeNode(TreeNode nOde, bool italisize)
		{
			if (italisize)
			{
				nOde.NodeFont = new Font(treChannels.Font, FontStyle.Italic);
							}
			else
			{
				nOde.NodeFont = new Font(treChannels.Font, FontStyle.Regular);
			}
		}
		private void EmboldenNode(TreeNode nOde, bool embolden)
		{
			if (embolden)
			{
				nOde.NodeFont = new Font(nOde.NodeFont, FontStyle.Bold);
			}
			else
			{
				nOde.NodeFont = new Font(nOde.NodeFont, FontStyle.Regular);
			}
		}
		private void HighlightNodeBackground(TreeNode nOde, bool highlight)
		{
			if (highlight)
			{
				nOde.BackColor = HIGHLIGHTBACKGROUND;
			}
			else
			{
				nOde.BackColor = REGULARBACKGROUND;
			}
		}
		private void ColorNodeText(TreeNode nOde, bool colorize)
		{
			if (colorize)
			{
				nOde.ForeColor = COLOREDTEXT;
			}
			else
			{
				nOde.ForeColor = REGULARTEXT;
			}
		}
		#endregion

		private void treChannels_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			//UserSelectNode(e.Node);
		}

		private void btnInvert_Click(object sender, EventArgs e)
		{
			//this.Cursor = Cursors.WaitCursor;
			ImBusy(true);
			InvertSelections(treChannels.Nodes);
			//this.Cursor = Cursors.Default;
			ImBusy(false);
		}

		void InvertSelections(TreeNodeCollection nOdes)
		{ 
			foreach (TreeNode nOde in nOdes)
			{
				iLORMember4 m = (iLORMember4)nOde.Tag;
				if (m.MemberType == LORMemberType4.Channel)
				{
					SelectNode(nOde, !m.Selected);
				}
				if (nOde.Nodes.Count > 0)
				{
					InvertSelections(nOde.Nodes);
				}
			} // foreach nodes
		} // end Invert Selection

		private void CopySelectionsToSequence()
		{
			/*
			// Clear any previous selections
			for (int tr=0; tr< seq.Tracks.Count; tr++)
			{
				seq.Tracks[tr].Selected = false;
			}
			for (int tg = 0; tg < seq.TimingGrids.Count; tg++)
			{
				seq.TimingGrids[tg].Selected = false;
			}
			for (int chg=0; chg<seq.ChannelGroups.Count; chg++)
			{
				seq.ChannelGroups[chg].Selected = false;
			}
			for (int rch=0; rch< seq.RGBchannels.Count; rch++)
			{
				seq.RGBchannels[rch].Selected = false;
			}
			for (int ch=0; ch< seq.Channels.Count; ch++)
			{
				seq.Channels[ch].Selected = false;
			}
			for (int tg=0; tg<seq.TimingGrids.Count; tg++)
			{
				seq.TimingGrids[tg].Selected = false;
			}

			///*
			// Select only timing grids used by selected tracks
			foreach (TreeNode nOde in treChannels.Nodes)
			{
				if (nOde.Checked)
				{
					iLORMember4 nodeTag = (iLORMember4)nOde.Tag;
					if (nodeTag.MemberType == LORMemberType4.Track)  // Just a sanity check, all first level nodes should be tracks
					{
						LORTrack4 t = seq.Tracks[nodeTag.Index];
						//int tgIdx = t.timingGridObjIndex;
						//for (int tg = 0; tg < seq.TimingGrids.Count; tg++)
						//{
						//	if (seq.TimingGrids[tg].saveID == tgIdx)
						//	{
						//		seq.TimingGrids[tg].Selected = true;
						//	}
						seq.TimingGrids[t.timingGrid.Index].Selected = true;
						//}
					}
				}
			}
			// * /

			//CopyNodeSelectionsToSequence(treChannels.Nodes);
			*/
		} // end CopySelectionToSequence

		private void CopyNodeSelectionsToSequence(TreeNodeCollection nOdes)
		{
			/*
			iLORMember4 nodeTag;

			for (int i = 0; i < gridItem_Checked.Length; i++)
			{
				seq.TimingGrids[i].Selected = gridItem_Checked[i];
			}

			foreach (TreeNode nOde in nOdes)
			{
				if (nOde.Checked)
				{
					nodeTag = (iLORMember4)nOde.Tag;
					if (nodeTag.MemberType == LORMemberType4.Track)
					{
						seq.Tracks[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == LORMemberType4.Channel)
					{
						seq.Channels[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == LORMemberType4.RGBChannel)
					{
						seq.RGBchannels[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nodeTag.MemberType == LORMemberType4.ChannelGroup)
					{
						seq.ChannelGroups[nodeTag.Index].Selected = nOde.Checked;
					}
					if (nOde.Nodes.Count > 0)
					{
						CopyNodeSelectionsToSequence(nOde.Nodes);
					}
				} // loop thru nodes
			}
			*/


		} // end CopyNodeSelectionToSequence

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
			if (ext.CompareTo(".lms")==0)
			{
				filt = "Musical Sequence (*.lms)|*.lms|Channel Configuration (*.lcc)|*.lcc";
				tit = "Save Partial Sequence As...";
			}
			if (ext.CompareTo(".lcc") == 0)
			{
				filt = "LORChannel4 Configuration (*.lcc)|*.lcc";
				tit = "Save Partial Channel Configuration As...";
			}
			string initDir = "";
			string initFile = "";
			if (fileSeqCur.Length > 5)
			{
				if (Directory.Exists(Path.GetDirectoryName(fileSeqCur)))
				{
					initDir = Path.GetDirectoryName(fileSeqCur);
					initFile = Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + member.ToString(); // + ext;
				}
			}
			if (initDir.Length < 5)
			{
				if (fileSeqLast.Length > 5)
				{
					if (Directory.Exists(Path.GetDirectoryName(fileSeqLast)))
					{
						initDir = Path.GetDirectoryName(fileSeqLast);
						initFile = Path.GetFileNameWithoutExtension(fileSeqLast) + " Part " + member.ToString(); // + ext;
					}
				}
			}
			if (initDir.Length < 5)
			{
				// Can't imagine that we would ever make it this far, but, just in case...
				if (ext.CompareTo(".lcc") == 0)
				{
					initDir = lutils.DefaultChannelConfigsPath;
				}
				else
				{
					initDir = LORSequence4.DefaultSequencesPath;
				}
			}

			dlgFileSave.Filter = filt;
			dlgFileSave.FilterIndex = 1;
			//dlgFileSave.FileName = Path.GetFullPath(fileSeqCur) + Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + member.ToString() + ext;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.DefaultExt = ext;
			dlgFileSave.OverwritePrompt = false;
			dlgFileSave.Title = tit;
			dlgFileSave.SupportMultiDottedExtensions = true;
			dlgFileSave.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileSeqCur) + " Part " + member.ToString(); // + ext;
																																														 //newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileSeqCur);
																																														 //newFileIn = "Part Mother Fucker!!";
			dlgFileSave.FileName = initFile;
			DialogResult result = dlgFileSave.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
				if (ow == DialogResult.Yes)
				{
					SaveSequence(dlgFileSave.FileName);
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
						member++;
						dirtySeq = false;
					}
				//}
				this.Cursor = Cursors.Default;
				*/
			}
		} // end Save File As

		private void SaveSequence(string newFilename)
		{
			ImBusy(true);
			CopySelectionsToSequence();
			//if (saveFormat == SAVEmixedDisplay)
			//{
				// normal default when not testing
				seq.WriteSequenceFile_DisplayOrder(newFilename, true, false);
			//}
			//if (saveFormat == SAVEcrgDisplay)
			//{
			//	seq.WriteSequenceFileInCRGDisplayOrder(newFilename, true, false);
			//}
			//if (saveFormat == SAVEcrgAlpha)
			//{
			//	seq.WriteSequenceFileInCRGAlphaOrder(newFilename, true, false);
			//}
			System.Media.SystemSounds.Beep.Play();
			member++;
			dirtySeq = false;
			fileSeqSave = newFilename;
			ImBusy(false);


		}

		private void btnSaveSelections_Click(object sender, EventArgs e)
		{
			dlgFileSave.DefaultExt = "ChSel";
			dlgFileSave.Filter = "LORChannel4 Selections|*.ChSel";
			dlgFileSave.FilterIndex = 0;
			string initDir = "";
			string initFile = "";
			if (fileSelCur.Length > 4)
			{
				if (Directory.Exists(Path.GetDirectoryName(fileSelCur)))
				{
					initDir = Path.GetDirectoryName(fileSelCur);
				}
				if (File.Exists(fileSelCur))
				{
					initFile = Path.GetFileName(fileSelCur);
				}
				else
				{
					if (File.Exists(fileSeqCur))
					{
						initFile = Path.GetFileNameWithoutExtension(fileSeqCur) +"LORChannel4 Selections";
					}
					if (initFile.Length < 5)
					{
						initFile = Path.GetFileNameWithoutExtension(fileSeqLast) + "LORChannel4 Selections";
					}
				}
			}
			if (initDir.Length < 5)
			{
				initDir = lutils.DefaultChannelConfigsPath;
			}
			dlgFileSave.FileName = initFile;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.OverwritePrompt = false;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.DefaultExt = "ChSel";
			dlgFileSave.SupportMultiDottedExtensions = true;
			dlgFileSave.ValidateNames = true;
			dlgFileSave.Title = "Save Channel Selections As...";

			DialogResult dr = dlgFileSave.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
					if (ow == DialogResult.Yes)
					{
						this.Cursor = Cursors.WaitCursor;
						string selectionsTemp = System.IO.Path.GetTempPath();
						selectionsTemp += Path.GetFileName(dlgFileSave.FileName);
						int selectionsErr = SaveSelections(selectionsTemp);
						if (selectionsErr == 0)
						{
							fileSelCur = dlgFileSave.FileName;
							if (File.Exists(fileSelCur))
							{
								//TODO: Add Exception Catch
								File.Delete(fileSelCur);
							}
							File.Copy(selectionsTemp, fileSelCur);
							File.Delete(selectionsTemp);
							dirtySel = false;
							//btnSaveSelections.Enabled = dirtySelections;
							//SystemSounds.Beep.Play();
							Fyle.PlayNotifyGenericSound();
							this.Cursor = Cursors.Default;
						} // end no errors saving selections
					} // end dialog result = OK
				}
		} // end SaveSelectionsAs...

		private int SaveSelections(string fileName)
		{
			int ret = 0;

			int lineCount = 0;
			StreamWriter writer = new StreamWriter(fileName);
			string lineOut = ""; // line to be written out, gets modified if necessary
													 //int pos1 = lutils.UNDEFINED; // positions of certain key text in the line

			SaveSelectionsToSelections(writer, treChannels.Nodes);

			writer.Close();
			dirtySel = false;
			return ret;
		} // end SaveSelections

		private void SaveSelectionsToSelections(StreamWriter writer, TreeNodeCollection nOdes)
		{
			string lineOut = "";
			iLORMember4 m;
			foreach (TreeNode nOde in nOdes)
			{
				m = (iLORMember4)nOde.Tag;
				if (m.Selected)
				{
					LORMemberType4 type;
					type = m.MemberType;
					lineOut = m.MemberType.ToString() + DELIM1;
					lineOut += m.Name + DELIM1;
					lineOut += m.SavedIndex.ToString() + DELIM1;
					lineOut += m.Index.ToString() + DELIM1;
					if (type == LORMemberType4.Channel)
					{
						lineOut += seq.Channels[m.Index].output.ToString();
					}
					else
					{
						lineOut += "None" + DELIM4 + "-1" + DELIM4 + "-1" + DELIM4 + "-1"; // + DELIM2;
					}

					writer.WriteLine(lineOut);

					if (nOde.Nodes.Count > 0)
					{
						SaveSelectionsToSelections(writer, nOde.Nodes);
					} // has seq.Members
				} // node checked
			} // loop thru nodes
		} // end SaveSelectionsToSelections

		private void btnBrowseSelections_Click(object sender, EventArgs e)
		{
			dlgFileOpen.DefaultExt = "ChSel";
			dlgFileOpen.Filter = "LORChannel4 Selections|*.ChSel";
			dlgFileOpen.DefaultExt = "ChSelections";
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.SupportMultiDottedExtensions = true;
			dlgFileOpen.ValidateNames = true;

			string initDir = lutils.DefaultChannelConfigsPath;
			string initFile = "";
			if (fileSelCur.Length > 4)
			{
				string pth = Path.GetDirectoryName(fileSelCur);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(fileSelCur))
				{
					initFile = Path.GetFileName(fileSelCur);
				}
			}
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Title = "Load-Apply Channel Selections..";

			DialogResult dr = dlgFileOpen.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				if (seq.filename.Length > 5)
				{
					LoadApplySelections(dlgFileOpen.FileName, true, useFuzzy);
					Fyle.PlayNotifyGenericSound();
				}
			} // end dialog result = OK

		} // end btnBrowseSelections_Click

		private DialogResult ShowMatchOptions()
		{
			frmOptions options = new frmOptions();
			options.useFuzzy = useFuzzy;
			options.minPrematchScore = minPrematchScore;
			options.minFinalMatchScore = minFinalMatchScore;
			//options.InitForm(true);
			DialogResult dr2 = options.ShowDialog();
			if (dr2 == DialogResult.OK)
			{
				useFuzzy = options.useFuzzy;
				Properties.Settings.Default.useFuzzy = useFuzzy;
				if (useFuzzy)
				{
					minPrematchScore = options.minPrematchScore;
					minFinalMatchScore = options.minFinalMatchScore;
					Properties.Settings.Default.preMatchScore = minPrematchScore;
					Properties.Settings.Default.finalMatchScore = minFinalMatchScore;
				}
				Properties.Settings.Default.Save();
			}
			return dr2;
		} // end ShowMatchOptions

		private DialogResult ShowSaveOptions()
		{
			frmOptions options = new frmOptions();
			//options.InitForm(false);
			DialogResult dr2 = options.ShowDialog(this);
			if (dr2 == DialogResult.OK)
			{
				LoadFuzzyOptions();
				//saveFormat = options.saveFormat;
				//Properties.Settings.Default.SaveFormat = saveFormat;
			}

			return dr2;
		}

		private void LoadFuzzyOptions()
		{
			finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
			prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
			useFuzzy = Properties.Settings.Default.FuzzyUse;
			caseSensitive = Properties.Settings.Default.FuzzyCaseSensitive;
			writeLog = Properties.Settings.Default.FuzzyWriteLog;
			minPrematchScore = Properties.Settings.Default.FuzzyMinPrematch;
			minFinalMatchScore = Properties.Settings.Default.FuzzyMinFinal;
		}


		private int LoadApplySelections(string selFile, bool remember, bool fuzzy)
		{
			ImBusy(true);
			if (remember)
			{
				fileSelLast = fileSelCur;
				fileSelCur = selFile;
				txtSelectionsFile.Text = Path.GetFileName(fileSelCur);
				Properties.Settings.Default.FileSelLast = fileSelCur;
				Properties.Settings.Default.Save();
				dirtySel = false;
			}
			//btnSaveSelections.Enabled = dirtySelections;
			int noFind = 0;

			if (seq.Tracks.Count > 0)
			{
				int lineCount = 0;
				string lineIn = ""; // line to be read in, gets parsed as requited
				StreamReader reader;

				//if (fuzzy)
				//{
				// get linecount for progress bar
				reader = new StreamReader(selFile);
				while ((lineIn = reader.ReadLine()) != null)
				{
					lineCount++;
				}
				reader.Close();
				pnlProgress.Maximum = lineCount;
				pnlProgress.Value = 0;
				//}


				//////////////////////////////////////////////
				// PART ONE -	build sorted arrays of names //
				////////////////////////////////////////////
				FillNameLists();
				seq.Members.ReIndex();
				//Array.Sort(trackNames, trackNums);
				//Array.Sort(channelGroupNames, channelGroupSIs);
				//Array.Sort(rgbChannelNames, rgbChannelSIs);
				//Array.Sort(channelNames, channelSIs);

				//////////////////////////////////////////////
				// PART TWO -	search sorted arrays of names //
				////////////////////////////////////////////
				int foundSI = lutils.UNDEFINED;
				int foundIdx = lutils.UNDEFINED;
				reader = new StreamReader(selFile);
				string[] parts = null;
				//string type = "";
				LORMemberType4 objType = LORMemberType4.None;
				string objName = "";
				string output = "";
				string prevSelection = "";
				string[] childNodeNames = null;
				TreeNode foundNode = null;
				TreeNode prevNOde = null;
				string prevParentText = "";
				ClearSelections(treChannels.Nodes);

				while ((lineIn = reader.ReadLine()) != null)
				{
					if (lineIn.Length > 7)
					{
						parts = lineIn.Split(DELIM1);
						if (parts.Length == 5)
						{
							objType = (LORMemberType4)Enum.Parse(typeof(LORMemberType4), parts[0]);
							objName = parts[1];
							foundNode = null;
							foundIdx = lutils.UNDEFINED;
							if (objType == LORMemberType4.Track)
							{
								foundNode = FindNodeByName(treChannels.Nodes, objName, LORMemberType4.Track, fuzzy);
								prevNOde = foundNode;
							}
							else
							{
								// else not a track
								if (prevNOde != null)
								{
									if (prevNOde.Nodes.Count > 0)
									{
										// check the seq.Members of the previous node
										foundNode = FindNodeByName(prevNOde.Nodes, objName, objType, fuzzy);
									}
									if (foundNode == null)
									{
										// not a child of the previous node
										// check it's siblings
										TreeNode prevParent = prevNOde.Parent;
										while ((foundNode == null) && (prevParent != null))
										{
											if (prevParent.Text.CompareTo(prevParentText) == 0)
											{
												int xyz = 1;
											}
											prevParentText = prevParent.Text;
											#if DEBUG
												string dbgmsg = "Searching seq.Members of '" + prevParent.Text + "' for '" + objName + "'";
												Console.WriteLine(dbgmsg);
											#endif
											foundNode = FindNodeByName(prevParent.Nodes, objName, objType, fuzzy);
											if (foundNode == null)
											{
												//recurse backwards up the tree
												prevParent = prevParent.Parent;
											}
										}
									}
									if (foundNode != null)
									{
										prevNOde = foundNode;
									}
									else
									{
										//TODO: Log not found
										noFind++;
										#if DEBUG
											string foooo = objName;
											System.Diagnostics.Debugger.Break();
										#endif
									}
								} // previous node is not null
							} // end if channel
						} // line split into exactly 4 parts
					} // end if length > 7
					pnlProgress.Value = lineCount;
					staStatus.Refresh();
				} // end while more lines

				reader.Close();
			}
			ImBusy(false);
			return noFind;

		} // end LoadApplySelections

		private TreeNode FindNodeByName(TreeNodeCollection nOdes, string name, LORMemberType4 tableType, bool fuzzy)
		{


			TreeNode ret = null;
			int foundIdx = lutils.UNDEFINED;
			string[] childNodeNames = null;
			Array.Resize(ref childNodeNames, nOdes.Count);
			for (int n = 0; n < nOdes.Count; n++)
			{
				if (nOdes[n].Text.CompareTo(name) == 0)
				{
					iLORMember4 m = (iLORMember4)nOdes[n].Tag;
					if (m.MemberType == tableType)
					{
						foundIdx = n;
						m.Selected = true;
						IncrementSelectionCount(1);
						HighlightNodeBackground(nOdes[n], true);
						SelectChannel(nOdes[n], true);
						//prevNOde = treChannels.Nodes[n];
						ret = nOdes[n];
						n = nOdes.Count; // break out
					}
				}
				else
				{
					childNodeNames[n] = nOdes[n].Text;
				} // end found, or not
			} // end loop thru child nodes
			if (foundIdx == lutils.UNDEFINED)
			{
				if (fuzzy)
				{
					//foundIdx = lutils.FuzzyFindName(childNodeNames, name, minPrematchScore/100, minFinalMatchScore/100);
					//foundIdx = FindByName(name, seq.Members, LORMemberType4.Items, prematchAlgorithm, minPrematchScore, finalAlgorithms, minFinalMatchScore, true);
					iLORMember4 found = FindByName(name, seq.Members, tableType, prematchAlgorithm, minPrematchScore, finalAlgorithms, minFinalMatchScore, true);
					if (found != null) foundIdx = found.SavedIndex;
					if (foundIdx > lutils.UNDEFINED)
					{
						iLORMember4 m = (iLORMember4)nOdes[foundIdx].Tag;
						//iLORMember4 nodeTag = (iLORMember4)nodesBySI[foundIdx];

						if (m.MemberType == tableType)
						{
							m.Selected = true;
							IncrementSelectionCount(1);
							HighlightNodeBackground(nOdes[foundIdx], true);
							SelectChannel(nOdes[foundIdx], true);
							//prevNOde = nOdes[foundIdx];
						}
						else
						{
							foundIdx = lutils.UNDEFINED;
						}
					}
				} // end use Fuzzu
			} // exact match found
			if (foundIdx == lutils.UNDEFINED)
			{
				// STILL not found
				//prevNOde = null;
				//TODO Log LORTrack4 Not Found
				int qq = 1;
			}
			return ret;
		}

		private void FillNameLists()
		{
			seq.Members.ReIndex();
		}

		private void ClearSelections(TreeNodeCollection nOdes)
		{
			foreach (TreeNode nOde in nOdes)
			{
				//if (nOde.Checked)
				//
				//	nOde.Checked = false;
				//	nOde.ForeColor = SystemColors.WindowText;
				//	nOde.BackColor = SystemColors.Window;
				//	IncrementSelectionCount(-1);
				//}
				HighlightNodeBackground(nOde, false);
				ColorNodeText(nOde, false);
				ItalisizeNode(nOde, false);
				EmboldenNode(nOde, false);
				//iLORMember4 nodeMem = (iLORMember4)nOde.Tag;
				//nodeMem.Selected = false;
				iLORMember4 m = (iLORMember4)nOde.Tag;
				m.Selected = false;
				if (nOde.Nodes.Count > 0)
				{
					ClearSelections(nOde.Nodes);
				}
			} // end for each node
			selectionCount = 0;
			IncrementSelectionCount(0);  //Incrementing by Zero just causes a refresh

		} // end ClearSelections



		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			ClearSelections(treChannels.Nodes);
			selectionCount = IncrementSelectionCount(-selectionCount);
			// make sure selectionCount is ZERO
			Console.WriteLine(selectionCount);
			System.Diagnostics.Debugger.Break();
			ImBusy(false);
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			selectionCount = 0;
			//foreach (TreeNode nOde in treChannels.Nodes)
			//{
			//	nOde.Checked = true;
			//	iLORMember4 nodeMem = (iLORMember4)nOde.Tag;
			//	nodeMem.Selected = true;
			//	IncrementSelectionCount(1);
			//	HighlightNodeBackground(nOde, true);
			//	ColorNodeText(nOde, true);
			//	SelectChildNodes(nOde.Nodes, true);
			//}
			SelectChildNodes(treChannels.Nodes, true);
			
			ImBusy(false);
		}

		private void lblSelectionCount_Click(object sender, EventArgs e)
		{

		}

		private void frmSplit_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				FirstShowing();
			}
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.Gray, 1);
			g.DrawRectangle(p, picPreview.Left - 1, picPreview.Top - 1, picPreview.Width + 1, picPreview.Height + 1);
		}

		private void FirstShowing()
		{
			if (File.Exists(fileSeqLast))
			{
				LoadSequence(fileSeqLast, false);
			}
			LoadApplyPreviousSelections();
			

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

		private void btnMatchOptions_Click(object sender, EventArgs e)
		{
			ShowMatchOptions();
		}

		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			ShowSaveOptions();
		}

		private void treChannels_MouseMove(object sender, MouseEventArgs e)
		{
			//previousNode = null;
		}

		private void mnuOpenSequence_Click(object sender, EventArgs e)
		{
			btnBrowseSequence.PerformClick();
		}

		private void mnuOpenSelections_Click(object sender, EventArgs e)
		{
			btnBrowseSelections.PerformClick();
		}

		private void mnuSaveAsSequence_Click(object sender, EventArgs e)
		{
			btnSaveSequence.PerformClick();
		}

		private void mnuSaveAsSelections_Click(object sender, EventArgs e)
		{
			btnSaveSelections.PerformClick();
		}
		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mnuSaveOptions_Click(object sender, EventArgs e)
		{
			btnSaveOptions.PerformClick();
		}

		private void mnuMatchOptions_Click(object sender, EventArgs e)
		{
			btnMatchOptions.PerformClick();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		public iLORMember4 FindByName(string theName, LORMembership4 members, LORMemberType4 PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			iLORMember4 ret = null;
			if (members.ByName.TryGetValue(theName, out ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != PartType)
				{
					ret = null;
				}
			}
			else
			{
				if (finalAlgorithms > 0)
				{
					ret = FuzzyFind(theName, members, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, ignoreSelected);
				}
			}

			return ret;
		}

		public iLORMember4 FindByName(string theName, LORMembership4 members, LORMemberType4 PartType)
		{
			iLORMember4 ret = null;
			ret = FindByName(theName, members, PartType, 0, 0, 0, 0, false);
			return ret;
		}

		public static iLORMember4 FindByName(string theName, LORMembership4 members)
		{
			iLORMember4 ret = null;
			int idx = BinarySearch(theName, members.Items);
			if (idx > lutils.UNDEFINED)
			{
				ret = members.Items[idx];
			}
			return ret;
		}


		public iLORMember4 FuzzyFind(string theName, LORMembership4 members, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			iLORMember4 ret = null;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;

			// Go thru all objects
			foreach (iLORMember4 child in members.Items)
			{
				if ((!child.Selected) || (!ignoreSelected))
				{
					// get a quick prematch score
					score = theName.RankEquality(child.Name, preAlgorithm);
					// fi the score is above the minimum PreMatch
					if (score > minPreMatch)
					{
						// Increment count and save the SavedIndex
						// NOte: No need to save the PreMatch score
						count++;
						Array.Resize(ref SIs, count);
						SIs[count - 1] = child.SavedIndex;
					}
				}
			}
			// Resize scores array to final size
			if (count > 0)
			{
				Array.Resize(ref scores, count);
				// LORLoop4 thru qualifying prematches
				for (int i = 0; i < count; i++)
				{
					// Get the ID, perform a more thorough final fuzzy match, and save the score
					iLORMember4 child = members.BySavedIndex[SIs[i]];
					score = theName.RankEquality(child.Name, finalAlgorithms);
					scores[i] = score;
				}
				// Now sort the final scores (and the SavedIndexes along with them)
				Array.Sort(scores, SIs);
				// Is the best/highest above the required minimum Final Match score?
				if (scores[count - 1] > minFinalMatch)
				{
					// Return the ID with the best qualifying final match
					ret = members.BySavedIndex[SIs[count - 1]];
					// Get Name just for debugging
					string msg = theName + " ~= " + ret.Name;
				}
			}
			return ret;
		}

		private int FuzzyFindName(string[] allNames, string theName, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			int foundIdx = lutils.UNDEFINED;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;

			// Go thru all objects
			//foreach (string aName in allNames)
			for (int l=0; l < allNames.Length; l++)
			{
				string aName = allNames[l];
				//if ((!child.Selected) || (!ignoreSelected))
				//{
					// get a quick prematch score
					score = theName.RankEquality(aName, preAlgorithm);
					// fi the score is above the minimum PreMatch
					if (score > minPreMatch)
					{
						// Increment count and save the SavedIndex
						// NOte: No need to save the PreMatch score
						count++;
						Array.Resize(ref SIs, count);
					SIs[count - 1] = l;
					}
				//}
			}
			// Resize scores array to final size
			if (count > 0)
			{
				Array.Resize(ref scores, count);
				// LORLoop4 thru qualifying prematches
				for (int i = 0; i < count; i++)
				{
					// Get the ID, perform a more thorough final fuzzy match, and save the score
					iLORMember4 child = seq.Members.BySavedIndex[SIs[i]];
					score = theName.RankEquality(child.Name, finalAlgorithms);
					scores[i] = score;
				}
				// Now sort the final scores (and the SavedIndexes along with them)
				Array.Sort(scores, SIs);
				// Is the best/highest above the required minimum Final Match score?
				if (scores[count - 1] > minFinalMatch)
				{
					// Return the ID with the best qualifying final match
					iLORMember4 ret = seq.Members.BySavedIndex[SIs[count - 1]];
					// Get Name just for debugging
					string msg = theName + " ~= " + ret.Name;
				}
			}



			return foundIdx;
		}

		public static int BinarySearch(string theName, List<iLORMember4> IDs)
		{
			return BinarySearch3(theName, IDs, 0, IDs.Count - 1);
		}

		public static int BinarySearch3(string theName, List<iLORMember4> IDs, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			if ((theName.CompareTo(IDs[start].Name) > 0) && (theName.CompareTo(IDs[end].Name) < 0))
			{
				int cmp = theName.CompareTo(IDs[mid].Name);
				if (cmp < 0)
					BinarySearch3(theName, IDs, start, mid);
				else if (cmp > 0)
					BinarySearch3(theName, IDs, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
				//Console.WriteLine("got it at " + index);
			}
			return index;
		}

		private void lstGrids_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool sel = !gridItem_Checked[lstGrids.SelectedIndex];
			gridItem_Checked[lstGrids.SelectedIndex] = sel;
			if (sel) gridSelCount++; else gridSelCount--;
			lstGrids.Refresh();
			IncrementSelectionCount(0);
		}

		private void lstGrids_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			Graphics g = e.Graphics;
			if ((e.Index >= 0) && (e.Index < lstGrids.Items.Count))
			{
				if (false) // (gridItem_Checked[e.Index])
				{
					g.FillRectangle(new SolidBrush(Color.Yellow), e.Bounds);
				}
				g.DrawString(lstGrids.Items[e.Index].ToString(), e.Font, new SolidBrush(SystemColors.WindowText), new Point(e.Bounds.X, e.Bounds.Y));
			}
		}

		private void cmdNothing_Click(object sender, EventArgs e)
		{

		}

		private void treChannels_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.Tag != null)
				{
					iLORMember4 m = (iLORMember4)e.Node.Tag;
					//if (m.MemberType == LORMemberType4.ChannelGroup)
					//{
					//	LORChannelGroup4 gr = (LORChannelGroup4)m;
					//	int sc = gr.Members.SelectedMemberCount;
					//}
					//if (m.MemberType == LORMemberType4.Track)
					//{
					//	LORTrack4 tr = (LORTrack4)m;
					//	int sc = tr.Members.SelectedMemberCount;
					//}
					if (m.Selected)
					{
						e.Node.BackColor = Color.Yellow;
					}
					else
					{
						e.Node.BackColor = Color.White;
					}
				}
			}
			e.DrawDefault = true;
		}

		private Rectangle NodeBounds(TreeNode node)
		{
			// Set the return value to the normal node bounds.
			Rectangle bounds = node.Bounds;
			if (node.Tag != null)
			{
				// Retrieve a Graphics object from the TreeView handle
				// and use it to calculate the display width of the tag.
				//Graphics g = treChannels.CreateGraphics();
				//int tagWidth = (int)g.MeasureString(node.Tag.ToString(), tagFont).Width + 6;

				// Adjust the node bounds using the calculated value.
				//bounds.Offset(tagWidth / 2, 0);
				//bounds = Rectangle.Inflate(bounds, tagWidth / 2, 0);
				//g.Dispose();
			}

			return bounds;

		}
	} // end frmSplit
} // end namespace UtilORama4
