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
using LORUtils;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms.Tools.Win32API;
using Syncfusion.Windows.Forms.Grid;
using xTune;

namespace xTimeORama
{
	public partial class frmxTime : Form
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

		private string fileSeqLast = ""; // Last Sequence4 File Loaded
		private string fileExportLast = ""; // Currently loaded Selections File
		private Sequence4 seq = new Sequence4();
		// private int nodeIndex = utils.UNDEFINED;
		private int selectionCount = 0;
		private int member = 1;
		private bool dirtySel = false;
		private bool dirtySeq = false;
		private const string helpPage = "http://wizlights.com/xutilx/xtimeorama";
		private string applicationName = "xTime-O-Rama";
		private string thisEXE = "xTimeORama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private int saveFormat = 0;
		private int gridSelCount = 0;
		private bool[] gridItem_Checked = null;
		private bool bizzy = true;  // Busy
		private bool noKids = false; // used to stop recursive/cascading events
		private List<xTimings> timingsList = new List<xTimings>();

		//private List<TreeNodeAdv>[] siNodes;
		private List<TreeNodeAdv>[] siNodes = null; // new List<TreeNodeAdv>();
		
		private bool firstShown = false;
		private TreeNodeAdv previousNode = null;
		int exportID = 0;

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

		public frmxTime()
		{
			InitializeComponent();
			//? Why does this work in the about form, but not here???
			//applicationName = AssemblyTitle;
		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = Sequence4.DefaultSequencesPath;
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
				fileSeqLast = dlgOpenFile.FileName;
				Properties.Settings.Default.FileSeqLast = fileSeqLast;
				Properties.Settings.Default.Save();


			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;
		} // end browse for First File

		private void LoadSequence(string theFile, bool remember)
		{
			bool wasBusy = ImBusy(true);
			if (remember)
			{
				fileSeqLast = theFile;

				Properties.Settings.Default.FileSeqLast = fileSeqLast;
				Properties.Settings.Default.Save();
			}
			this.Text = applicationName + " - " + Path.GetFileName(theFile);

			txtSequenceFile.Text = utils.ShortenLongPath(theFile, 80);
			seq.ReadSequenceFile(theFile);
			Array.Resize(ref gridItem_Checked, seq.TimingGrids.Count);
			Array.Resize(ref siNodes, seq.Members.HighestSavedIndex + 1);
			ChannelTree.FillChannels(treChannels, seq, siNodes, false, true);
			//FillGridList();
			member = 1;
			dirtySeq = false;
			ImBusy(wasBusy);

		}


		private void frmxTime_Load(object sender, EventArgs e)
		{
			ImBusy(true);
			RestoreFormPosition();
			tempPath = utils.GetAppTempFolder();
			//InitForm();
			ImBusy(false);
		}

		private void InitForm()
		{
			bool valid = false;

			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			//TODO: These may get overridden by command line arguments (not yet supported)
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
					if (fileSeqLast.Length > 6)
					{
						valid = utils.IsValidPath(fileSeqLast, true);
					}
					if (!valid) fileSeqLast = utils.DefaultSequencesPath;
					if (File.Exists(fileSeqLast))
					{
						//utils.FillChannels(treChannels, seq, siNodes);
						txtSequenceFile.Text = utils.ShortenLongPath(fileSeqLast, 80);
						//seq.ReadSequenceFile(fileSeqLast);
						LoadSequence(fileSeqLast, false);

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

			/*
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
				valid = utils.IsValidPath(fileSelLast, true);
			}
			if (!valid) fileSelLast = utils.DefaultChannelConfigsPath;
			*/

			//txtSequenceFile.Text = utils.ShortenLongPath(fileSeqLast, 80);
			//txtSelectionsFile.Text = utils.ShortenLongPath(fileSelLast, 80);

			//cmdNothing.Visible = utils.IsWizard;

			//!treChannels.DrawMode = TreeViewDrawMode.OwnerDrawAll;
			treChannels.ShowCheckBoxes = true;
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
				if (arg.Substring(4).IndexOf(".") > utils.UNDEFINED) isFile++;  // contains a period
				if (utils.InvalidCharacterCount(arg) == 0) isFile++;
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
			string fileSeqCur = "";
			// DialogResult dr = DialogResult.None;

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
					bool wasBusy = ImBusy(true);
					fileSeqCur = batch_fileList[0];

					FileInfo fi = new FileInfo(fileSeqCur);
					Properties.Settings.Default.FileSeqLast = fileSeqCur;
					Properties.Settings.Default.Save();

					txtSequenceFile.Text = utils.ShortenLongPath(fileSeqCur, 80);
					seq.ReadSequenceFile(fileSeqCur);
					ChannelTree.FillChannels(treChannels, seq, siNodes, false, false);
					member = 1;
					dirtySeq = false;
					ImBusy(wasBusy);

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
					//ImBusy(true);
					//fileExportLast = cmdSelectionsFile;
					//txtSelectionsFile.Text = Path.GetFileName(fileExportLast);
					//Properties.Settings.Default.FileSelLast = fileExportLast;
					//Properties.Settings.Default.Save();
					// No new sequences, just the selections, apply selections to already loaded sequence
					//LoadApplySelections(fileExportLast, true, useFuzzy);
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

		private bool ImBusy(bool isBusy)
		{
			bool wasBusy = bizzy;
			if (isBusy != bizzy)
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
				bizzy = isBusy;
			}
			// return previous state
			return wasBusy;
		} // end ImBusy


		private int IncrementSelectionCount(int amount)
		{
			selectionCount += amount;
			dirtySel = true;
			dirtySeq = true;
			// bool stuffToSave = false;
			// if ((selectionCount > 0) && (gridSelCount > 0)) stuffToSave = true;

			//btnSaveSelections.Enabled = stuffToSave;
			//btnSaveSequence.Enabled = stuffToSave;
			//btnClear.Enabled = stuffToSave;
			lblSelectionCount.Text = selectionCount.ToString() + " / " + gridSelCount.ToString();

			return selectionCount;
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
		}



		private void frmxTime_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();

		}

		private void treChannels_AfterSelect(object sender, EventArgs e)
		{
			//! Debugging - Why is this event firinging seemingly more than it should?
			treeClicks++;
			lblTreeClicks.Text = treeClicks.ToString();

			TreeNodeAdv node = treChannels.SelectedNode;

			if (node.Tag != null)
			{
				IMember m = (IMember)node.Tag;
				if (m.MemberType == MemberType.Channel)
				{
					Channel ch = (Channel)m;
					Bitmap bmp = utils.RenderEffects(ch, 0, ch.Centiseconds, 300, 20, true);
					picPreview.Visible = true;
					picPreview.Image = bmp;
					//picPreview.Refresh();
				}
				if (m.MemberType == MemberType.RGBchannel)
				{
					RGBchannel rgb = (RGBchannel)m;
					Bitmap bmp = utils.RenderEffects(rgb, 0, seq.Centiseconds, 300, 21, false);
					picPreview.Image = bmp;
					picPreview.Visible = true;
				}
				if ((m.MemberType == MemberType.ChannelGroup) || (m.MemberType == MemberType.Track))
				{
					picPreview.Visible = false;
				}
			}

		}

		private void treChannels_AfterCheck(object sender, TreeNodeAdvEventArgs e)
		{
			// Is this a subsequent progmatically triggered change event?
			if (!noKids)
			{
				// Ignore all subsequent progrmatically triggered change events
				noKids = true;
					SelectNode(e.Node, e.Node.CheckState);
				// subsequent progmatically triggered change events should be done
				// resume responding to user triggered change events
				noKids = false;

				lblSelectionCount.Text = selectionCount.ToString() + " channels selected"; // Updated count display
				lblSelections.Text = "Selections: " + selectionCount.ToString();
				// Enable export button only if one or more channels are selected
				bool en = false;
				if (selectionCount > 0) en = true;
				btnExport.Enabled = en;
				if (selectionCount > 1) en = true; else en = false;
				optOnePer.Enabled = en;
				optMultiPer.Enabled = en;
				if (selectionCount == 1) optMultiPer.Checked = true;
				if (selectionCount == 2)
				{
					if (Properties.Settings.Default.SaveFormat == 1) optOnePer.Checked = true; else optMultiPer.Checked = true;
				}

			}
		}

		private void SelectNode(TreeNodeAdv node, CheckState newState)
		{
			if (node.Tag != null)
			{
				// Get reference to Sequence Channel or other object from tag
				IMember member = (IMember)node.Tag;
				if (member.MemberType == MemberType.Channel)
				{
					SelectMember(member, node.Checked); // Select it
				}
				if (member.MemberType == MemberType.RGBchannel)
				{

				}
				if ((member.MemberType == MemberType.ChannelGroup) || (member.MemberType == MemberType.CosmicDevice))
				{

				}
				if (member.MemberType == MemberType.Track)
				{

				}
				if (node.Parent != null)
				{

				}


			} // node.Tag NOT null, should contain a sequence member
		}


		private void SelectMember(IMember member, bool newState)
		{
			// temp store previous state
			bool wasSelected = member.Selected;
			// is it a regular plain channel?
			if (member.MemberType == MemberType.Channel)
			{
				// did it change states?
				if (newState != wasSelected)
				{
					// Select the Sequence Channel
					member.Selected = newState;
					// Update count
					if (newState) selectionCount++; else selectionCount--;
					// Get list of it's TreeNode(s)
					int si = member.SavedIndex;
					List<TreeNodeAdv> nodes = siNodes[si];
					// select all it's TreeNodes
					for (int n = 0; n < nodes.Count; n++)
					{
						TreeNodeAdv node = nodes[n];
						CheckChannelNode(node, newState);
					}
				}
			}
			else
			// It's not a regular channel, must be some form of group
			//    (ChannnelGroup, RGB Channel, Track, Cosmic Device)
			{
				if (!noKids)
				{
					noKids = true;
					if (member.MemberType == MemberType.Track)
					{

					}
					else
					{
						int si = member.SavedIndex;
						List<TreeNodeAdv> nodes = siNodes[si];
						for (int n = 0; n < nodes.Count; n++)
						{
							TreeNodeAdv node = nodes[n];
							CheckGroupNode(node, newState);
						}
					}
					noKids = false;
				}
			}
		}

		void CheckChannelNode(TreeNodeAdv node, bool newState)
		{
			IMember member = (IMember)node.Tag;
			MemberType type = member.MemberType;
			//member.Selected = newState;
			node.Checked = newState;
			if (node.Parent != null)
			{
				//int tot = 0;
				//int sel = 0;
				//CountSelectedChildChannels(nOde.Parent, ref tot, ref sel);
				CheckParentCheck(node.Parent);
			}
		}

		private void CheckGroupNode(TreeNodeAdv node, bool newState)
		{ 
			IMember member = (IMember)node.Tag;
			MemberType type = member.MemberType;
			//member.Selected = newState;
			if (type == MemberType.RGBchannel)
			{
				// Should be 3 subnodes
				RGBchannel rgb = (RGBchannel)member;
				SelectMember(rgb.redChannel, newState);
				SelectMember(rgb.grnChannel, newState);
				SelectMember(rgb.bluChannel, newState);
			}
			if ((type == MemberType.ChannelGroup) ||
				(type == MemberType.CosmicDevice) ||
				(type == MemberType.Track))
			{
				foreach (TreeNodeAdv n2 in node.Nodes)
				{
					IMember m2 = (IMember)n2.Tag;
					if (m2.MemberType == MemberType.Channel)
					{
						//CheckChannelNode(noode, newState);
						SelectMember(m2, newState);
					}
					else
					{
						//CheckGroupNode(noode, newState);
						SelectMember(m2, newState);
					}
				}
			}
			member.Selected = true;
			node.Checked = true;
			if (node.Parent != null)
			{
				CheckParentCheck(node.Parent);
			}
		}

		private void CheckParentCheck(TreeNodeAdv node)
		{
			int tot = 0;
			int sel = 0;
			CountSelectedChildChannels(node, ref tot, ref sel);
			if (tot == 0)
			{
				node.CheckState = CheckState.Unchecked;
				if (node.Parent != null)
				{
					CheckParentCheck(node.Parent);
				}
			}
			else
			{
				if (sel == tot)
				{
					node.CheckState = CheckState.Checked;
					if (node.Parent != null)
					{
						CheckParentCheck(node.Parent);
					}
				}
				else
				{
					node.CheckState = CheckState.Indeterminate;
					// If this node is Indeterminite (partial) then all nodes above it will be also, no need to count selected child channels
					TreeNodeAdv n2 = node.Parent;
					while (n2 != null)
					{
						n2.CheckState = CheckState.Indeterminate;
						n2 = n2.Parent;
					}
				}
			}

		}


		private void CountSelectedChildChannels(TreeNodeAdv node, ref int tot, ref int sel)
		{
			foreach (TreeNodeAdv n2 in node.Nodes)
			{
				IMember member = (IMember)n2.Tag;
				if (member.MemberType == MemberType.Channel)
				{
					tot++;
					if (member.Selected)
					{
						sel++;
					}
				}
				else
				{
					int subtot = 0;
					int subsel = 0;
					CountSelectedChildChannels(n2, ref subtot, ref subsel);
					tot += subtot;
					sel += subsel;
				}
			}
		}


		private void btnExport_Click(object sender, EventArgs e)
		{
			ImBusy(true);

			// NOTE: Default folder for Exports is the folder which was last used for exports
			// This is because the xTimings files probably go into the xLights folder (or subfolder of it)
			// Whereas the sequence probably came from the LOR folder (or subfolder of it)
			// And therefore quite different.
			string pathExport = "";
			if (fileExportLast.Length > 4)
			{
				pathExport = Path.GetDirectoryName(fileExportLast);
				if (!Directory.Exists(pathExport))
				{
					// No longer exists, so reset to blank
					pathExport = "";
				}
			}
			if (pathExport.Length < 4)
			{
				pathExport = Path.GetDirectoryName(fileSeqLast);
				if (!Directory.Exists(pathExport))
				{
					// No longer exists, so reset to blank
					pathExport = "";
				}
			}
			if (pathExport.Length < 4)
			{
				pathExport = utils.DefaultSequencesPath;
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
			string initFile = Path.GetFileNameWithoutExtension(fileSeqLast);
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
				utils.PlayNotifyGenericSound();
			} // end dialog result = OK
			ImBusy(false);

		} // end SaveSelectionsAs...

		private void ExportSelectedTimings(string fileName)
		{
			// Get Temp Directory
			string tempDir = System.IO.Path.GetTempPath();
			string timingsTemp = "";
			//  Allocate a stream writer
			StreamWriter writer = null;
			// Generate xTiming classes for all selected channels
			GeneratexTimingsForSelectedChannels(treChannels.Nodes);
			// Save Filename for next time (really only need the path, but...)
			fileExportLast = fileName;
			Properties.Settings.Default.FileExportLast = fileExportLast;
			if (optOnePer.Checked) Properties.Settings.Default.SaveFormat = 1;
			else Properties.Settings.Default.SaveFormat = 2;
			Properties.Settings.Default.Save();
			// Get path and name for export files
			string expPath = Path.GetDirectoryName(fileName);
			string seqName = Path.GetFileNameWithoutExtension(fileName);

			// Loop thru list of xTimings for selected channels
			for (int i = 0; i < timingsList.Count; i++)
			{
				if ((optOnePer.Checked) || (i == 0))
				{
					// If File-Per-Channel  OR  the very first channel
					// Create a temporrary streamwriter file
					timingsTemp = tempDir + Path.GetRandomFileName();
					writer = new StreamWriter(timingsTemp);
					string lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
					if (optMultiPer.Checked)
					{
						writer.WriteLine(lineOut);
						lineOut = "<timings>";
					}
					writer.WriteLine(lineOut);
				}

				// Write this xTiming to an export file
				string xDat = timingsList[i].LineOut();
				writer.WriteLine(xDat);

				if ((optOnePer.Checked) || (i == (timingsList.Count-1)))
				{
					// If file-per-channel  OR  the very last channel
					// Close the writer
					if (optMultiPer.Checked)
					{
						string lineOut = "</timings>";
						writer.WriteLine(lineOut);
					}
					writer.Close();

					// [Re-]Build file name for export...
					// Path + Name
					string expFile = Path.GetDirectoryName(fileName) + "\\";
					expFile += Path.GetFileNameWithoutExtension(fileName);
					if (optOnePer.Checked)
					{
						// If File-Per-Channel, add channel name to file name
						string wf = utils.WindowfyFilename(timingsList[i].timingName, true);
						expFile += " - " + wf;
					}
					// Finally, tack on the extension
					expFile += ".xtiming";

					if (File.Exists(expFile))
					{
						// If already exists, delete it
						//TODO: Add Exception Catch
						File.Delete(expFile);
					}
					// Copy the tempfile to the new file name and delete the old temp file
					File.Copy(timingsTemp, expFile);
					File.Delete(timingsTemp);
				}
			}
		} // end SaveSelections

		private void SaveSelectedTimings(StreamWriter writer, TreeNodeAdvCollection nOdes)
		{
			string lineOut = "";
			IMember m;
			foreach (TreeNodeAdv nOde in nOdes)
			{
				m = (IMember)nOde.Tag;
				if (m.Selected)
				{
					MemberType type;
					type = m.MemberType;
					if (type == MemberType.Channel)

					lineOut = m.MemberType.ToString() + DELIM1;
					lineOut += m.Name + DELIM1;
					lineOut += m.SavedIndex.ToString() + DELIM1;
					lineOut += m.Index.ToString() + DELIM1;
					if (type == MemberType.Channel)
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
						SaveSelectedTimings(writer, nOde.Nodes);
					} // has seq.Members
				} // node checked
			} // loop thru nodes
		} // end SaveSelectionsToSelections

		private void MakeGroupsUncheckable(TreeNodeAdvCollection baseNodes)
		{
			foreach(TreeNodeAdv node in baseNodes)
			{
				IMember member = (IMember)node.Tag;
				if (member.MemberType != MemberType.Channel)
				{
					//node.Checked = CheckState.Indeterminate;
				}
			}
		}


		private void btnBrowseSelections_Click(object sender, EventArgs e)
		{
			dlgOpenFile.DefaultExt = "ChSel";
			dlgOpenFile.Filter = "Channel Selections|*.ChSel";
			dlgOpenFile.DefaultExt = "ChSelections";
			dlgOpenFile.FilterIndex = 0;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.SupportMultiDottedExtensions = true;
			dlgOpenFile.ValidateNames = true;

			string initDir = utils.DefaultChannelConfigsPath;
			string initFile = "";
			if (fileExportLast.Length > 4)
			{
				string pth = Path.GetDirectoryName(fileExportLast);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(fileExportLast))
				{
					initFile = Path.GetFileName(fileExportLast);
				}
			}
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Title = "Load-Apply Channel Selections..";

			DialogResult dr = dlgOpenFile.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				if (seq.filename.Length > 5)
				{
					//LoadApplySelections(dlgOpenFile.FileName, true, useFuzzy);
					utils.PlayNotifyGenericSound();
				}
			} // end dialog result = OK

		} // end btnBrowseSelections_Click

		private void FillNameLists()
		{
			seq.Members.ReIndex();
		}

		private void ClearSelections(TreeNodeAdvCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				//if (nOde.Checked)
				//
					nOde.Checked = false;
				//	nOde.ForeColor = SystemColors.WindowText;
				//	nOde.BackColor = SystemColors.Window;
				//	IncrementSelectionCount(-1);
				//}
				//HighlightNodeBackground(nOde, false);
				//ColorNodeText(nOde, false);
				//ItalisizeNode(nOde, false);
				//EmboldenNode(nOde, false);
				//IMember nodeMem = (IMember)nOde.Tag;
				//nodeMem.Selected = false;
				IMember m = (IMember)nOde.Tag;
				m.Selected = false;
				m.AltSavedIndex = utils.UNDEFINED;
				if (nOde.Nodes.Count > 0)
				{
					ClearSelections(nOde.Nodes);
				}
			} // end for each node
			timingsList = new List<xTimings>();
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


		private void FirstShowing()
		{
			bool wasBusy = ImBusy(true);
			InitForm();
			ImBusy(wasBusy);

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


		private void treChannels_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.Tag != null)
				{
					IMember m = (IMember)e.Node.Tag;
					//if (m.MemberType == MemberType.ChannelGroup)
					//{
					//	ChannelGroup gr = (ChannelGroup)m;
					//	int sc = gr.Members.SelectedMemberCount;
					//}
					//if (m.MemberType == MemberType.Track)
					//{
					//	Track tr = (Track)m;
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

		private Rectangle NodeBounds(TreeNodeAdv node)
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

		private void treChannels_Click(object sender, EventArgs e)
		{

		}

		private void frmxTime_Paint(object sender, PaintEventArgs e)
		{
		
		}

		private void frmxTime_Shown(object sender, EventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				FirstShowing();
			}

		}

	

/*
 *private void BeginExport()
		{
			string pa = Path.GetDirectoryName(fileSeqCur);
			string fn = Path.GetFileNameWithoutExtension(fileSeqCur);
			string fileOut = pa + "\\" + fn + ".xtiming";
			timingsList = new List<xTimings> ();
			ExportSelected(treChannels.Nodes);
			WriteTimingsFile(fileOut);



		}
*/

		private void GeneratexTimingsForSelectedChannels(TreeNodeAdvCollection nodes)
		{
			// Recursive
			foreach (TreeNodeAdv node in nodes)
			{
				if (node.Checked)
				{
					if (node.Tag != null)
					{
						IMember member = (IMember)node.Tag;
						if (member.Selected)
						{
							if (member.MemberType == MemberType.Channel)
							{
								if (member.AltSavedIndex < 0)
								{
									AddChannelTimings((Channel)member);
									member.AltSavedIndex = 7;
								}
							}
						}
					}
				}
				if (node.Nodes.Count > 0)
				{
					GeneratexTimingsForSelectedChannels(node.Nodes); // Recurse!
				}
			}


		}

		private void AddChannelTimings(Channel chan)
		{
			if (chan.effects.Count > 0)
			{
				List<Effect> effects = chan.effects;
				Effect efOut = effects[0];
				string nm = utils.XMLifyName(chan.Name);
				xTimings times = new xTimings(nm);

				for (int i = 0; i < effects.Count; i++)
				{
					Effect effect = effects[i];
					// Changed my mind... DON'T round after all
					//int st = xEffect.RoundTime(effect.startCentisecond);
					// * 10 cuz LOR uses Centiseconds, xLights uses Milliseconds
					int st = effect.startCentisecond * 10;
					//int ed = xEffect.RoundTime(effect.endCentisecond);
					int ed = effect.endCentisecond * 10;
					if (st < ed)
					{
						xEffect xf = new xEffect(st, ed);
						times.Add(xf);
					}
				}
				timingsList.Add(times);
			}
		}

		private void WriteTimingsFile(string fileOut)
		{
			StreamWriter writer = new StreamWriter(fileOut);
			string txtOut = xTimings.XMLinfo;
			writer.WriteLine(txtOut);
			foreach (xTimings times in timingsList)
			{
				txtOut = times.LineOut();
				writer.WriteLine(txtOut);
			}
			writer.Close();


		}


	} // end frmxTime
} // end namespace xTimeORama
