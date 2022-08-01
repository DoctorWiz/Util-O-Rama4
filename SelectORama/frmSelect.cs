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
using LOR4;
using FileHelper;
using FormHelper;
using ReadWriteCsv;
using FuzzORama;
using Syncfusion.Windows.Forms.Tools;


namespace UtilORama4
{
	public struct SelectedMember
	{
		public LOR4MemberType MemberType;
		public string Name;
		public int SavedIndex;
	}



	public partial class frmSelect : Form
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

		private string fileSeqCur = "";  // Currently loaded LOR4Sequence File
		private string fileSequenceLast = ""; // Last LOR4Sequence File Loaded
																					//private string fileSeqSave = ""; // Last Saved Sequence
		private string fileSelCur = ""; // Currently loaded Selections File
		private string fileSelectionsLast = "";
		private LOR4Sequence seq = null; // new LOR4Sequence();
		private int nodeIndex = LOR4Admin.UNDEFINED;
		private int selectionCount = 0;
		private int member = 1;
		private bool dirtySel = false;
		//private bool dirtySeq = false;
		private const string helpPage = "http://wizlights.com/utilorama/selectorama";
		public string utilityName = Fyle.ApplicationName;
		private string thisEXE = "select-o-rama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Select-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private byte saveFormat = SAVEmixedDisplay;
		//private int gridSelCount = 0;
		private bool[] gridItem_Checked = null;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;
		private Settings userSettings = Settings.Default;
		private bool shown = false;
		//private int selectableChannelCount = 0;

		private int lastGridIndex = -1;
		private int lastMemberIndex = -1;
		private int selectedMemberCount = 0;
		private int selectedChannelCount = 0;
		private int selectedGridCount = 0;
		private int channelCount = 0;
		private int memberCount = 0;
		private int nodeCount = 0;
		private int gridCount = 0;

		//private List<TreeNode>[] nodesBySI;
		//private List<List<TreeNode>> nodesBySI = new List<List<TreeNode>>();
		//private List<TreeNodeAdv>[] nodesBySI;

		//private bool firstShown = false;
		private TreeNodeAdv lastNode = null;
		private TreeNodeAdv clickedNode = null;
		//private TreeNodeAdv previousNode = null;

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
		private bool timeDisplayMode = true;

		private List<SelectedMember> memberSelections = new List<SelectedMember>();
		private List<SelectedMember> unmatchedSelections = new List<SelectedMember>();
		private MRUoRama recentSequences;
		private MRUoRama recentSelections;


		public frmSelect()
		{
			InitializeComponent();


		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = LOR4Sequence.DefaultSequencesPath;
			string initFile = "";



			string filt = "All Sequences (*.las, *.lms, *.lcc)|*.las;*.lms;*.lcc|Musical Sequences only (*.lms)|*.lms";
			filt += "|Animated Sequences only (*.las)|*.las|LOR4Channel Configurations only(*.lcc)|*.lcc";
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

		private void LoadSequence(string theFile, bool remember = true)
		{
			ImBusy(true);
			try
			{
				if (remember)
				{
					fileSequenceLast = fileSeqCur;
					fileSeqCur = theFile;
					recentSequences.UseFile(theFile);
					recentSequences.FillFileComboBox(cboSequenceFile);


					userSettings.FileSeqLast = fileSeqCur;
					userSettings.Save();
				}
				this.Text = utilityName + " - " + Path.GetFileName(theFile);
			}
			catch (Exception els1)
			{
				Fyle.BUG("LoadSequence(" + theFile + ")/Remember", els1);
			}


			try
			{
				// seq.ReadSequenceFile(theFile);
				seq = new LOR4Sequence(theFile);
				//ShowSeqInfo(seq);
				//selectableChannelCount = seq.Channels.Count - (seq.RGBchannels.Count * 2);
				Array.Resize(ref gridItem_Checked, seq.TimingGrids.Count);
				TreeUtils.TreeFillChannels(treeSequence, seq, false, true);
				FillTimingGridsList();
				if (timeDisplayMode)
				{ lblCentiseconds.Text = LOR4Admin.FormatTime(seq.Centiseconds) + " long"; }
				else
				{ lblCentiseconds.Text = seq.Centiseconds.ToString() + " Centiseconds"; }
				// Right justify this info label
				lblCentiseconds.Left = picPreview.Left + picPreview.Width - lblCentiseconds.Width;
				member = 1;
			}
			catch (Exception els2)
			{
				Fyle.BUG("LoadSequence(" + theFile + ")/Read", els2);
			}

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
				if (File.Exists(fileSelectionsLast))
				{
					string msg = "Load and apply the last selections file:'";
					msg += Path.GetFileNameWithoutExtension(fileSelectionsLast) + "'?";
					DialogResult dr2 = MessageBox.Show(this, msg, "Apply Selections?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (dr2 == DialogResult.Yes)
					{
						LoadApplySelections(fileSelectionsLast, true, useFuzzy);
					}
				}
			}
			ImBusy(false);
		}

		private void FillTimingGridsList()
		{
			string descr = "";
			ListViewItem li;
			string[] litxt = { "", "", "" };
			string tms = "";
			lstTimingGrids.Items.Clear();
			gridCount = seq.TimingGrids.Count;

			for (int tg = 0; tg < gridCount; tg++)
			{
				tms = "";
				LOR4Timings theGrid = seq.TimingGrids[tg];
				litxt[0] = theGrid.Name;
				if (theGrid.TimingGridType == LOR4TimingGridType.FixedGrid)
				{
					// If long but generic name, shorten it
					if (litxt[0].Substring(0, 21) == "Fixed Timing Grid ")
					{
						litxt[0] = "FixGrid " + (tg + 1).ToString();
					}
					litxt[1] = "Fixed " + theGrid.spacing.ToString();
					for (int x = 1; x < 10; x++)
					{
						int c = theGrid.spacing * x;
						if (c < seq.Centiseconds)
						{
							decimal sec = (decimal)c / 100;
							tms += c.ToString(".00") + ", ";
						}
					}
				}
				if (theGrid.TimingGridType == LOR4TimingGridType.Freeform)
				{
					// If long but generic name, shorten it
					if (litxt[0].Substring(0, 21) == "Freeform Timing Grid ")
					{
						litxt[0] = "FF Grid " + (tg + 1).ToString();
					}
					litxt[1] = "Freeform";
					for (int x = 0; x < 10; x++)
					{
						if (x < theGrid.timings.Count)
						{
							decimal sec = (decimal)theGrid.timings[x] / 100;
							tms += sec.ToString(".00") + ", ";
						}
					}
				}
				// Remove trailing comma from times
				litxt[2] = tms.Substring(0, tms.Length - 2);
				li = new ListViewItem(litxt);
				li.ToolTipText = litxt[0];
				// Link them to each other
				li.Tag = theGrid;
				theGrid.ListViewItem = li;
				// Add
				lstTimingGrids.Items.Add(li);
			}
		}

		private void frmSelect_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{
			// These things are done before the form is even visable
			//  Only doing the bear necessities here to improve apparent
			//   load time.  Additional stuff is postponed untill after the
			//    form is first show.  See FirstShow() procedure
			ImBusy(true);
			this.RestoreView();
			RestoreUserSettings();
		}

		private void FirstShow()
		{
			bool valid = false;
			tempPath = Fyle.GetMyTempPath();
			if (!Fyle.DebugMode)
			{ Fyle.EraseMyTempFolder(); }


			ProcessCommandLine();
			if (batch_fileCount > 1)
			{
				batchMode = true;
			}

			saveFormat = userSettings.SaveFormat;

			treeSequence.OwnerDrawNodes = true; // .DrawMode = TreeViewDrawMode.OwnerDrawAll;

			// Create and load Most-Recently-Used file lists
			recentSequences = new MRUoRama(MRUoRama.FileType.Sequences, utilityName);
			recentSelections = new MRUoRama(MRUoRama.FileType.Selections, utilityName);
			// Populate the combo boxes with the file names
			//   Note that this will validate that they exist
			recentSequences.FillFileComboBox(cboSequenceFile);
			recentSelections.FillFileComboBox(cboSelectionsFile);

			// check the combo box for the most recently used sequence (that still exists)
			if ((cboSequenceFile.Items.Count > 0) &&
				(cboSequenceFile.SelectedIndex >= 0) &&
				(cboSequenceFile.Text.Length > 6))
			{
				// Got one?  Get the most recently used sequence (that still exists) in the combo box
				string cboText = cboSequenceFile.Text;
				fileSequenceLast = recentSequences.FindFullFilename(cboText);
				if (fileSequenceLast.Length > 6)
				{
					if (Fyle.Exists(fileSequenceLast))
					{
						// Everything looks just super!  Load that puppy!
						LoadSequence(fileSequenceLast, true);
					} // End and it exists
				} // End file name is not empty
			} // End something selected in combo box

			// Do we have a valid, non-empty sequence loaded?
			if (seq != null)
			{
				if (seq.Channels.Count > 0)
				{
					// Do the same with the most recent selections file that we did with the MRU sequence
					// check the combo box for the most recently used selections file (that still exists)
					if ((cboSelectionsFile.Items.Count > 0) &&
						(cboSelectionsFile.SelectedIndex >= 0) &&
						(cboSelectionsFile.Text.Length > 6))
					{
						// Got one?  Get the most recently used selection file (that still exists) in the combo box
						string cboText = cboSelectionsFile.Text;
						fileSelectionsLast = recentSelections.FindFullFilename(cboText);
						if (fileSelectionsLast.Length > 6)
						{
							if (Fyle.Exists(fileSelectionsLast))
							{
								// Everything looks just super!  Load and apply that puppy!
								LoadApplySelections(fileSelectionsLast, true, true);
							} // End and it exists
						} // End file name is not empty
					} // End something selected in combo box
				} // End sequence has channels
			} // End sequence not null

			picAboutIcon.Visible = false;
			ImBusy(false);
		}


		private void RestoreUserSettings()
		{
			//fileSequenceLast = userSettings.FileSeqLast;  // Currently loaded LOR4Sequence File
			//fileSelectionsLast = userSettings.FileSelLast;
			//recentSequences.FillFileComboBox(cboSequenceFile);
			//recentSelections.FillFileComboBox(cboSelectionsFile);
			timeDisplayMode = userSettings.TimeDisplayMode;
		}

		private void SaveUserSettings()
		{
			// We are using an MRU now, no real need to save these files, except maybe for debugging purposes
			userSettings.FileSelLast = fileSelectionsLast;
			userSettings.FileSeqLast = fileSequenceLast;
			// Save Time Display Mode (mm:ss.cc or centiseconds)
			userSettings.TimeDisplayMode = timeDisplayMode;
			userSettings.Save();
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
				if (arg.Substring(4).IndexOf(Fyle.CHAR_EXT) > LOR4Admin.UNDEFINED) isFile++;  // contains a period
				if (Fyle.InvalidCharacterCount(arg) == 0) isFile++;
				if (isFile == 3)
				{
					if (File.Exists(arg))
					{
						string ext = Fyle.Extension(arg);
						if (ext.CompareTo(Fyle.EXT_EXE) == 0)
						{
							if (f == 0)
							{
								thisEXE = arg;
							}
						}
						if ((ext.CompareTo(LOR4Admin.EXT_LMS) == 0) || (ext.CompareTo(LOR4Admin.EXT_LAS) == 0))
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
				//if (arg.Substring(4).IndexOf(".") > LOR4Admin.UNDEFINED) isFile++;  // contains a period
				//if (InvalidCharacterCount(arg) == 0) isFile++;
				//if (isFile == 2)
				//{
				//	if (File.Exists(arg))
				//	{
				string ext = Fyle.Extension(file);
				if ((ext.CompareTo(LOR4Admin.EXT_LMS) == 0) ||
						(ext.CompareTo(LOR4Admin.EXT_LAS) == 0) ||
						(ext.CompareTo(LOR4Admin.EXT_LEE) == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;
				}
				else
				{
					if (ext.CompareTo(LOR4Admin.EXT_CHLIST) == 0)
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
					ImBusy(true);
					fileSeqCur = batch_fileList[0];

					FileInfo fi = new FileInfo(fileSeqCur);
					userSettings.FileSeqLast = fileSeqCur;
					userSettings.Save();

					//TODO Update MRU
					//txtSequenceFile.Text = Fyle.ShortenLongPath(fileSeqCur, 80);
					seq.ReadSequenceFile(fileSeqCur);
					TreeUtils.TreeFillChannels(treeSequence, seq, false, false);
					member = 1;
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
					//TODO Update MRU
					//txtSelectionsFile.Text = Path.GetFileName(fileSelCur);
					userSettings.FileSelLast = fileSelCur;
					userSettings.Save();
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

		private int IncrementSelectionCount(int amount)
		{
			selectionCount += amount;
			dirtySel = true;
			bool stuffToSave = false;
			//if ((selectionCount > 0) && (gridSelCount > 0)) stuffToSave = true;

			btnClear.Enabled = stuffToSave;

			//string selText = selectionCount.ToString() + " of " + selectableChannelCount.ToString();
			//selText += " / " + gridSelCount.ToString() + " of " + seq.TimingGrids.Count.ToString();
			//lblSelectionCount.Text = selText;

			return selectionCount;
		}


		private void LoadApplyPreviousSelections()
		{
			string lastSelFile = "fileSelectionsLast.ChSel";
			string file = tempPath + lastSelFile;
			if (File.Exists(file))
			{
				LoadApplySelections(file, false, useFuzzy);
			}
		}


		private void CloseForm()
		{
			SaveUserSettings();
			this.SaveView();
		}



		private void frmSelect_FormClosing(object sender, FormClosingEventArgs e)
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
					if (!Fyle.DebugMode)
					{ Fyle.EraseMyTempFolder(); }

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

		private void treeChannels_AfterSelect(object sender, EventArgs e)
		{
			//! Debugging - Why is this event firinging seemingly more than it should?
			treeClicks++;
			lblTreeClicks.Text = treeClicks.ToString();

			TreeNodeAdv eNode = treeSequence.SelectedNode;
			if (eNode != null)
			{
				if (lastNode == null)
				{
					lastNode = eNode;
					//UserSelectNode(eNode);
				}
				else
				{
					if (lastNode != eNode)
					{
						lastNode = eNode;
						//UserSelectNode(eNode);
					}
				}

				if (eNode.Tag != null)
				{
					iLOR4Member m = (iLOR4Member)eNode.Tag;
					if (m.MemberType == LOR4MemberType.Channel)
					{
						LOR4Channel ch = (LOR4Channel)m;
						LOR4Admin.RenderEffects(ch, ref picPreview, chkRamps.Checked);
						picPreview.Visible = true;
					}
					if (m.MemberType == LOR4MemberType.RGBChannel)
					{
						LOR4RGBChannel rgb = (LOR4RGBChannel)m;
						LOR4Admin.RenderEffects(rgb, ref picPreview, chkRamps.Checked);
						picPreview.Visible = true;
					}
					if ((m.MemberType == LOR4MemberType.ChannelGroup) || (m.MemberType == LOR4MemberType.Track))
					{
						picPreview.Visible = false;
					}
				}
			}

		}

		/*
		private void UserSelectNode(TreeNodeAdv nOde)
		{
			iLOR4Member m = null;
			bool ignoreRGB = false;
			m = (iLOR4Member)nOde.Tag;
			string foo = nOde.Text;  //! FOR DEBUGGING, REMOVE LATER
			if (nOde.Parent != null)
			{
				string parentName = nOde.Parent.Text;
				// If clicking on a base node (track) the parent will be the tree and have no valid tag
				if (nOde.Parent.Tag != null)
				{
					iLOR4Member nt = (iLOR4Member)nOde.Parent.Tag;
					if (nt.MemberType == LOR4MemberType.RGBChannel)
					{
						// If the node clicked on has a parent,
						// and that parent is an LOR4RGBChannel,
						// Then this is an RGB sub channel,
						// So IGNORE this click
						ignoreRGB = true;
					}
				}
			}
			if (!ignoreRGB)
			{
				//! Is this supposed to INVERT the state?
				//SelectNode(nOde, !m.Selected);
				SelectNode(nOde, Selections.InvertCheckState(m.Selected));
			}
			//? Why doesn't this work?
			//btnSaveSelections.Enabled = (selectionCount > 0);
			if (selectionCount > 0)
			{
				btnSaveSelections.Enabled = true;
				btnClear.Enabled = true;
			}
			else
			{
				btnSaveSelections.Enabled = false;
				btnClear.Enabled = false;
			}
			lblSelectionCount.Text = selectionCount.ToString();
			//cmdNothing.Focus();

			if (m.MemberType == LOR4MemberType.Track)
			{
				// Note double negative: if not Unchecked (thus includes indeterminate)
				if (m.Selected != CheckState.Unchecked)
				{
					LOR4Track trk = (LOR4Track)m;
					int sid = trk.timingGrid.SaveID;
					lstTimingGrids.Items[sid].Checked = true;
				}
			}

			treeSequence.SelectedNode = null;
			//txtSelectionsFile.Focus();

		}
		*/

		void SelectNode(TreeNodeAdv nOde, CheckState select)
		{
			// Note: "Select" in this case only refers to clicking on it and highlighting it.
			// does not refer to Checking or unchecking the box, for that see:
			iLOR4Member m = (iLOR4Member)nOde.Tag;
			//TreeUtils.HiglightMemberBackground(m, select);
			//TreeUtils.HighlightMember(m, TreeUtils.textHighlightColor, TreeUtils.backgroundHighlightColor);
		} // end highlight node


		private void btnInvert_Click(object sender, EventArgs e)
		{
			//this.Cursor = Cursors.WaitCursor;
			ImBusy(true);
			InvertSelections(treeSequence.Nodes);
			//this.Cursor = Cursors.Default;
			ImBusy(false);
		}

		void InvertSelections(TreeNodeAdvCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				iLOR4Member m = (iLOR4Member)nOde.Tag;
				if (m.MemberType == LOR4MemberType.Channel)
				{
					SelectNode(nOde, Selections.InvertCheckState(m.Selected));
				}
				if (nOde.Nodes.Count > 0)
				{
					InvertSelections(nOde.Nodes);
				}
			} // foreach nodes
		} // end Invert Selection


		private void btnSaveSelections_Click(object sender, EventArgs e)
		{
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
						initFile = Path.GetFileNameWithoutExtension(fileSeqCur) + LOR4Admin.EXT_CHLIST;
					}
					if (initFile.Length < 5)
					{
						initFile = Path.GetFileNameWithoutExtension(fileSequenceLast) + LOR4Admin.EXT_CHLIST;
					}
				}
			}
			if (initDir.Length < 5)
			{
				initDir = LOR4Admin.DefaultChannelConfigsPath;
			}
			dlgFileSave.FileName = initFile;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.OverwritePrompt = false;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.DefaultExt = LOR4Admin.EXT_CHLIST;
			dlgFileSave.Filter = LOR4Admin.FILTER_CHLIST;
			dlgFileSave.FilterIndex = 0;
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
					string selectionsTemp = Fyle.GetMyTempPath(); // System.IO.Path.GetTempPath();
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
													 //int pos1 = LOR4Admin.UNDEFINED; // positions of certain key text in the line

			lineOut = LOR4Admin.FILEDESCR_CHLIST;
			writer.WriteLine(lineOut);
			lineOut = LOR4Admin.CSVHEAD_CHLIST;
			writer.WriteLine(lineOut);


			//TODO: Change LOR4Sequence class so that .Members contains only its direct descendants- which are
			//TODO:   tracks- to be consistant with ChannelGroups, Cosmic Devices, and Tracks.
			//TODO:     ALL members of the sequence (including grandchildren and descendants) are now in .AllMembers
			//TODO:       This change is in progress and partially complete.
			//TODO: SaveSelectedChannels(writer, seq.Members);
			foreach (LOR4Track track in seq.Tracks)
			{
				if (track.Selected == CheckState.Checked)
				{
					SaveSelectedMember(writer, track);
				}
				SaveSelectedMembers(writer, track.Members);
			}

			writer.Close();
			dirtySel = false;
			return ret;
		} // end SaveSelections

		private void SaveSelectedMembers(StreamWriter writer, LOR4Membership members)
		{
			string lineOut = "";
			foreach (iLOR4Member member in members)
			{
				LOR4MemberType type = member.MemberType;
				if (member.Selected == CheckState.Checked)
				{
					if (type == LOR4MemberType.Channel)
					{
						SaveSelectedMember(writer, member);
					}
					else
					{
						if (type == LOR4MemberType.RGBChannel)
						{
							SaveSelectedMember(writer, member);
							LOR4RGBChannel rgbch = (LOR4RGBChannel)member;
							SaveSelectedMember(writer, rgbch.redChannel);
							SaveSelectedMember(writer, rgbch.grnChannel);
							SaveSelectedMember(writer, rgbch.bluChannel);
						} // end is a RGB channel
					} // End is a channel, or not
				} // End member is selected
				if (type == LOR4MemberType.ChannelGroup)
				{
					if (member.Selected == CheckState.Checked)
					{
						SaveSelectedMember(writer, member);
					}
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					SaveSelectedMembers(writer, group.Members);
				}
				else
				{
					if (type == LOR4MemberType.Cosmic)
					{
						if (member.Selected == CheckState.Checked)
						{
							SaveSelectedMember(writer, member);
						}
						LOR4Cosmic cosmic = (LOR4Cosmic)member;
						SaveSelectedMembers(writer, cosmic.Members);
					} // end is a cosmic device
				} // End is a channel group
			} // end foreach member loop
		}

		private void SaveSelectedMember(StreamWriter writer, iLOR4Member member)
		{
			string lineOut = "";
			if (member.Selected == CheckState.Checked)
			{
				LOR4MemberType type = member.MemberType;
				lineOut = type.ToString() + Fyle.COMMA;
				lineOut += "\"" + LOR4Admin.XMLifyName(member.Name) + "\",";
				lineOut += member.SavedIndex.ToString(); // + Fyle.DELIM6;
																								 //lineOut += member.Index.ToString() + Fyle.DELIM6;
				if (type == LOR4MemberType.Channel)
				{
					//lineOut += seq.Channels[member.Index].output.ToString();
				}
				else
				{
					//lineOut += "None" + DELIM4 + "-1" + DELIM4 + "-1" + DELIM4 + "-1"; // + DELIM2;
				}
				writer.WriteLine(lineOut);
			}
		}


		private void btnBrowseSelections_Click(object sender, EventArgs e)
		{
			string newFile = BrowseSelections();

		}

		private string BrowseSelections()
		{
			//! Does NOT actually open or load the selections file, that is handled separately
			//    This just allows you to pick a file, and returns the name (if user doesn't cancel)
			string newFile = ""; // Default is blank in case of cancel or error
			dlgFileOpen.DefaultExt = LOR4Admin.EXT_CHLIST; // chList
			dlgFileOpen.Filter = LOR4Admin.FILTER_CHLIST; // Channel List *.chlist
			dlgFileOpen.FilterIndex = 0;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.SupportMultiDottedExtensions = true;
			dlgFileOpen.ValidateNames = true;

			string initDir = LOR4Admin.DefaultChannelConfigsPath;
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
					newFile = dlgFileOpen.FileName;
				}
			} // end dialog result = OK
			return newFile;

		} // end btnBrowseSelections_Click

		private int LoadApplySelections(string selFile, bool remember = true, bool fuzzy = true)
		{
			//TODO impliment remember and fuzzy flags
			int count = Selections.LoadSelections(seq, selFile);
			if (count > 0)
			{
				ImBusy(true);
				// Clear any previous selections from the sequence and the tree
				seq.Selected = CheckState.Unchecked;
				TreeUtils.ClearAllNodes(treeSequence.Nodes);
				// Update remembered settings and UI
				dirtySel = false;
				selectionCount = count;
				lblSelectionCount.Text = selectionCount.ToString();
				recentSelections.UseFile(selFile);
				fileSelectionsLast = fileSelCur;
				fileSelCur = selFile;
				userSettings.FileSelLast = fileSelCur;
				userSettings.Save();
				btnSaveSelections.Enabled = dirtySel;
				ImBusy(false);
			}
			return count;
		}

		private int OLD_LoadApplySelections(string selFile, bool remember = true, bool fuzzy = true)
		{
			int noFind = 0;
			ImBusy(true);
			// Clear any previous selections from the sequence and the tree
			seq.Selected = CheckState.Unchecked;
			TreeUtils.ClearAllNodes(treeSequence.Nodes);

			if (seq.Tracks.Count > 0)
			{
				int lineCount = 0;
				string lineIn = ""; // line to be read in, gets parsed as requited
				StreamReader reader;
				reader = new StreamReader(selFile);
				// Read (and throw away) the first line containing the file description
				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					lineCount++;
					if (lineIn == LOR4Admin.FILEDESCR_CHLIST)
					{
						// Read (and throw away) the second line containing the file description
						if (!reader.EndOfStream)
						{
							lineIn = reader.ReadLine();
							lineCount++;
							// Clear/Reset list of failed matches
							unmatchedSelections = new List<SelectedMember>();
							ClearSelections(treeSequence.Nodes);
							// Now read the rest of the lines until EOF containing selections
							while (!reader.EndOfStream)
							{
								lineIn = reader.ReadLine();
								lineCount++;
								string[] parts = lineIn.Split(',');
								if (parts.Length > 2)
								{
									// start by getting the type
									string theType = parts[0];
									LOR4MemberType itsType = LOR4SeqEnums.EnumMemberType(theType);
									// All previous selections were saved to the file including tracks and groups
									// But we are only going to reselect regular channels, RGB channels, cosmic devices and timing grids
									// because contents and submembers of tracks and groups may be different
									if ((itsType == LOR4MemberType.Channel) ||
											(itsType == LOR4MemberType.RGBChannel) ||
											(itsType == LOR4MemberType.Cosmic) ||
											(itsType == LOR4MemberType.Timings))
									{
										string theName = parts[1];
										string itsName = LOR4Admin.HumanizeName(theName);
										string theSI = parts[2];
										int itsSI = LOR4Admin.UNDEFINED;
										int.TryParse(theSI, out itsSI);
										iLOR4Member member = null;

										// Try to find it by SavedIndex first since that's the fastest
										// first, make sure SavedIndex isn't undefined, and within range
										if ((itsSI >= 0) && (itsSI <= seq.AllMembers.HighestSavedIndex))
										{
											// Did we fine it?
											member = seq.AllMembers.BySavedIndex[itsSI];
											// did we get the type from the file, and does it match
											if (member != null)
											{
												if (itsType == member.MemberType)
												{
													if (itsName == member.Name)
													{
														// Yay!  We got it!
													}
													else
													{
														// Wront one, nullify the previous find
														member = null;
													} // End name matches, or not
												}
												else
												{
													// does not match, so nullify our previous find
													member = null;
												} // End type matches, or not
											} // end found SOMETHING by saved index
										} // End saved index in range
										if (member == null)
										{
											// Couldn't find it by SavedIndex?  No big deal and common
											// problem.  Look for it the slow way, by name
											member = seq.AllMembers.FindByName(itsName, itsType, false);
										}
										if (member == null)
										{
											// Still don't have it, oh well, sigh.  Keep track of failed matches
											SelectedMember sm = new SelectedMember();
											sm.MemberType = itsType;
											sm.Name = itsName;
											sm.SavedIndex = itsSI;
											unmatchedSelections.Add(sm);
										}
										else
										{
											TreeUtils.SelectMember(member, CheckState.Checked);
										}
									} // End channel, RGB, cosmic, or timing
								} // End line has 3 or more parts (divided by commas)
							} // End while reading more lines loop
						}
						// Rebuild the tree
						//? is the the best way?  some other refresh possibly better?
						TreeUtils.TreeFillChannels(treeSequence, seq, false, false);
					} // end first line of file has proper descriptor
				}
				reader.Close();
			} // End sequence has tracks

			if (remember)
			{
				recentSelections.UseFile(selFile);
				fileSelectionsLast = fileSelCur;
				fileSelCur = selFile;
				userSettings.FileSelLast = fileSelCur;
				userSettings.Save();
				dirtySel = false;
			}
			btnSaveSelections.Enabled = dirtySel;






			ImBusy(false);
			return noFind;

		} // end LoadApplySelections


		private void FillNameLists()
		{
			seq.AllMembers.ReIndex();
		}

		private void ClearSelections(TreeNodeAdvCollection nOdes)
		{
			foreach (TreeNodeAdv nOde in nOdes)
			{
				//if (nOde.Checked)
				//
				//	nOde.Checked = false;
				//	nOde.ForeColor = SystemColors.WindowText;
				//	nOde.BackColor = SystemColors.Window;
				//	IncrementSelectionCount(-1);
				//}

				if (nOde.Tag != null)
				{
					iLOR4Member member = (iLOR4Member)nOde.Tag;
					TreeUtils.SelectMember(member, CheckState.Unchecked);
					//TreeUtils.HiglightMemberBackground	(member, false);
					TreeUtils.HighlightMember(member, TreeUtils.textNormalColor, TreeUtils.backgroundNormalColor);
					TreeUtils.ColorMemberText(member, false);
					TreeUtils.ItalisizeMember(member, false);
					TreeUtils.EmboldenMember(member, false);
				}
			} // end for each node
			selectionCount = 0;
			IncrementSelectionCount(0);  //Incrementing by Zero just causes a refresh

		} // end ClearSelections



		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout();
			aboutBox.Icon = this.Icon;
			aboutBox.Text = "About Select-O-Rama";
			aboutBox.AppIcon = picAboutIcon.Image;
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
			ClearSelections(treeSequence.Nodes);
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
			foreach (TreeNodeAdv nOde in treeSequence.Nodes)
			{
				if (nOde.Tag != null)
				{
					iLOR4Member member = (iLOR4Member)nOde.Tag;
					selectionCount += TreeUtils.SelectMember(member, CheckState.Checked);
				}
			}
			ImBusy(false);
		}

		private void lblSelectionCount_Click(object sender, EventArgs e)
		{
			List<string> names = new List<string>();
			foreach (LOR4Channel chan in seq.Channels)
			{
				if (chan.Selected == CheckState.Checked)
				{
					string suffix = chan.Name.Substring(chan.Name.Length - 4);
					if ((suffix == " (R)") ||
							(suffix == " (G)") ||
							(suffix == " (B)"))
					{
						// RGB SubChannel; Ignore and do not add to list!
					}
					else
					{
						names.Add(chan.Name);
					}
				}
			}
			foreach (LOR4RGBChannel rgb in seq.RGBchannels)
			{
				if (rgb.Selected == CheckState.Checked)
				{
					names.Add(rgb.Name);
				}
			}
			string msg = "";
			if (names.Count > 0)
			{
				names.Sort();
				foreach (string name in names)
				{
					msg += name + "\r\n";
				}
			}
			else
			{
				msg = "NONE!";
			}
			MessageBox.Show(this, msg, "Selected Channels", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		private void frmSelect_Paint(object sender, PaintEventArgs e)
		{
			//if (!shown)
			//{
			//	FirstShow();
			//	shown = true;
			//}
			Graphics g = e.Graphics;
			Pen p = new Pen(Color.Gray, 1);
			g.DrawRectangle(p, picPreview.Left - 1, picPreview.Top - 1, picPreview.Width + 1, picPreview.Height + 1);
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
			//ShowMatchOptions();
		}

		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			//ShowSaveOptions();
		}

		private void treChannels_MouseMove(object sender, MouseEventArgs e)
		{
			//previousNode = null;
		}


		public iLOR4Member FindByName(string theName, LOR4Membership members, LOR4MemberType PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			iLOR4Member ret = null;
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
					ret = FuzzyFind(theName, members, ignoreSelected);
				}
			}

			return ret;
		}

		public iLOR4Member FindByName(string theName, LOR4Membership members, LOR4MemberType PartType)
		{
			iLOR4Member ret = null;
			ret = FindByName(theName, members, PartType, 0, 0, 0, 0, false);
			return ret;
		}

		public static iLOR4Member FindByName(string theName, LOR4Membership members)
		{
			iLOR4Member ret = null;
			int idx = BinarySearch(theName, members.Items);
			if (idx > LOR4Admin.UNDEFINED)
			{
				ret = members.Items[idx];
			}
			return ret;
		}


		public iLOR4Member FuzzyFind(string theName, LOR4Membership members, bool ignoreSelected)
		{
			iLOR4Member ret = null;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;
			double bestScore = 0D;
			int bestIndex = -1;

			// Go thru all objects
			//foreach (iLOR4Member child in members.Items)
			for (int idx = 0; idx < members.Items.Count; idx++)
			{
				iLOR4Member child = members.Items[idx];
				if ((child.Selected != CheckState.Checked) || (!ignoreSelected))
				{
					bestScore = 0D;
					bestIndex = -1;
					// get a quick prematch score
					score = theName.FuzzyScoreFast(child.Name);
					// fi the score is above the minimum PreMatch
					if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
					{
						// Increment count and save the SavedIndex
						// NOte: No need to save the PreMatch score
						count++;
						score = theName.FuzzyScoreAccurate(child.Name);
						if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
						{
							if (score > bestScore)
							{
								bestScore = score;
								bestIndex = idx;
							}
						}
						//Array.Resize(ref SIs, count);
						//SIs[count - 1] = child.SavedIndex;
					}
				}
			}

			if (bestIndex >= 0)
			{
				ret = members.Items[bestIndex];
			}
			return ret;
		}

		private int FuzzyFindName(string[] allNames, string theName, bool ignoreSelected)
		{
			int foundIdx = LOR4Admin.UNDEFINED;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score = 0D;
			double bestScore = 0D;
			int bestIndex = -1;

			// Go thru all objects
			//foreach (string aName in allNames)
			for (int l = 0; l < allNames.Length; l++)
			{
				bestScore = 0D;
				bestIndex = -1;
				string aName = allNames[l];
				//if ((!child.Selected) || (!ignoreSelected))
				//{
				// get a quick prematch score
				score = theName.FuzzyScoreFast(aName);
				// fi the score is above the minimum PreMatch
				if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
				{
					score = theName.FuzzyScoreAccurate(aName);
					if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
					{
						if (score > bestScore)
						{
							bestScore = score;
							bestIndex = l;
						}
					}
					// Increment count and save the SavedIndex
					// NOte: No need to save the PreMatch score
					count++;
				}
			}

			if (bestIndex >= 0)
			{ foundIdx = bestIndex; }
			return foundIdx;
		}

		public static int BinarySearch(string theName, List<iLOR4Member> IDs)
		{
			return BinarySearch3(theName, IDs, 0, IDs.Count - 1);
		}

		public static int BinarySearch3(string theName, List<iLOR4Member> IDs, int start, int end)
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

		/*
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
		*/

		private void cmdNothing_Click(object sender, EventArgs e)
		{

		}

		/*
		private void treChannels_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			if (e.Node != null)
			{
				if (e.Node.Tag != null)
				{
					iLOR4Member m = (iLOR4Member)e.Node.Tag;
					//if (m.MemberType == LOR4MemberType.ChannelGroup)
					//{
					//	LOR4ChannelGroup gr = (LOR4ChannelGroup)m;
					//	int sc = gr.Members.SelectedMemberCount;
					//}
					//if (m.MemberType == LOR4MemberType.Track)
					//{
					//	LOR4Track tr = (LOR4Track)m;
					//	int sc = tr.Members.SelectedMemberCount;
					//}
					if (m.Selected == CheckState.Checked)
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
		*/

		private void treeChannels_AfterCheck(object sender, TreeNodeAdvEventArgs e)
		{
			TreeNodeAdv eNode = e.Node; // treeChannels.SelectedNode;
			string foo = eNode.Text;
			if (e.Action == TreeViewAdvAction.ByMouse)
			{
				if (eNode != null)
				{
					Node_AfterCheck(eNode);
				}
			}
		}

		private void Node_AfterCheck(TreeNodeAdv nOde)
		{
			// Occurs immediately after the check state of a node changes
			if (nOde.Tag != null)
			{
				iLOR4Member member = (iLOR4Member)nOde.Tag;
				if (member.Selected != nOde.CheckState)
				{
					int iC = TreeUtils.SelectMember(member, Selections.InvertCheckState(member.Selected));
					//selectionCount += iC;
					IncrementSelectionCount(iC);
				}
			}
			lastNode = nOde;
		}

		private void treeChannels_Click(object sender, EventArgs e)
		{
			Point pt = treeSequence.PointToClient(Cursor.Position);
			TreeNodeAdv node = treeSequence.GetNodeAtPoint(pt, true);
			//if (node != null && node == treeChannels.SelectedNode)
			if (node != null)
			{
				//RaiseClick(node);
				string foo = node.Text;
				clickedNode = node;
			}
		}


		private void treeChannels_BeforeSelect(object sender, TreeViewAdvCancelableSelectionEventArgs args)
		{
			Point pt = treeSequence.PointToClient(Cursor.Position);
			TreeNodeAdv node = treeSequence.GetNodeAtPoint(pt, true);
			if (args.Action == TreeViewAdvAction.ByMouse && node == null)
			{
				args.Cancel = true;
			}
		}

		void RaiseClick(TreeNodeAdv adv)
		{
			// please use your code here
		}

		private void lblNewSequence_Click(object sender, EventArgs e)
		{

		}

		private void frmSelect_Resize(object sender, EventArgs e)
		{
			int ofst = 416;
			treeSequence.Height = this.Height - ofst;
			picPreview.Top = treeSequence.Top + treeSequence.Height + 3;
			lblTimingGrids.Top = picPreview.Top + picPreview.Height + 3;
			lstTimingGrids.Top = lblTimingGrids.Top + lblTimingGrids.Height + 3;
			btnAll.Top = lstTimingGrids.Top + lstTimingGrids.Height + 3;

			btnInvert.Top = btnAll.Top;
			btnClear.Top = btnAll.Top;

			ofst = 50;
			treeSequence.Width = this.Width - ofst;
			picPreview.Width = treeSequence.Width;
			lstTimingGrids.Width = treeSequence.Width;

			btnBrowseSequence.Left = treeSequence.Left + treeSequence.Width - btnBrowseSequence.Width;
			btnBrowseSelections.Left = btnBrowseSequence.Left;
			btnSaveSelections.Left = btnBrowseSequence.Left;

			cboSequenceFile.Width = btnBrowseSelections.Left - cboSequenceFile.Left - 5;
			cboSelectionsFile.Width = cboSequenceFile.Width;



		}

		private void frmSelect_Shown(object sender, EventArgs e)
		{
			if (!shown)
			{
				FirstShow();
				shown = true;
			}
		}

		private void lblSelections_Click(object sender, EventArgs e)
		{
			lblSelectionCount_Click(sender, e);
		}

		private void cboSequenceFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			//! Does this trigger if user drops box but selects same item?
			if (!recentSequences.FillingCombo)
			{
				string getFile = recentSequences.FindFullFilename(cboSequenceFile.Text);
				if (Fyle.Exists(getFile))
				{
					LoadSequence(getFile);
					if (Fyle.Exists(fileSelectionsLast))
					{
						LoadApplyPreviousSelections();
					}
				}
			}
		}

		private void cboSelectionsFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			//! Does this trigger if user drops box but selects same item?
			bool cool = false; // Is it cool (OK) to load new selections?
			if (!recentSelections.FillingCombo)
			{
				string getFile = recentSelections.FindFullFilename(cboSelectionsFile.Text);
				if (Fyle.Exists(getFile))
				{
					if (dirtySel)
					{
						string msg = "";
						if (getFile == fileSelCur)
						{
							msg = "Selections have changed!  Save to a new file before reloading?";
						}
						else
						{
							msg = "Selections have changed!  Save to a new file?";
						}
						DialogResult dr = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (dr == DialogResult.Yes)
						{
							string fileName = BrowseSelections();
							if (fileName.Length > 0)
							{
								int sx = SaveSelections(fileName);
								if (sx == 0) { cool = true; }
							}
						}
						else if (dr == DialogResult.No)
						{
							cool = true;
						} // End Dialog Result
					}
					else // NOT dirty
					{
						// So don't need to worry about saving changes
						cool = true;
					} // End if dirty, or not

					if (cool)
					{
						LoadApplySelections(getFile);
					}
				}
			} // End Exists
		}

		private void picAboutIcon_Click(object sender, EventArgs e)
		{

		}

		public int ShowSeqInfo(LOR4Sequence s)
		{
			int errs = 0;
			StringBuilder i = new StringBuilder();
			i.Append("Information for Sequence File: '");
			i.Append(s.filename);
			i.Append("'\r\n\r\n");

			i.Append("Audio File: '");
			i.Append(s.info.music.Title);
			i.Append("'\r\n");

			i.Append(s.Centiseconds.ToString());
			i.Append(" Centiseconds\r\n");

			i.Append(s.Tracks.Count.ToString());
			i.Append(" Tracks\r\n");
			i.Append(s.Channels.Count.ToString());
			i.Append(" Channels\r\n");
			i.Append(s.Tracks[0].Members.Count.ToString());
			i.Append(" In track 1\r\n");
			i.Append(s.ChannelGroups.Count.ToString());
			i.Append(" Groupss\r\n");
			i.Append(s.TimingGrids.Count.ToString());
			i.Append(" Timing Grids\r\n");

			i.Append(" Highest Saved Index = ");
			i.Append(s.AllMembers.HighestAltSavedIndex.ToString());


			DialogResult d = MessageBox.Show(this, i.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


			return errs;
		}

		private void lblCentiseconds_Click(object sender, EventArgs e)
		{
			// Toggle it
			timeDisplayMode = !timeDisplayMode;
			// Display new mode
			if (timeDisplayMode)
			{ lblCentiseconds.Text = LOR4Admin.FormatTime(seq.Centiseconds) + " long"; }
			else
			{ lblCentiseconds.Text = seq.Centiseconds.ToString() + " Centiseconds"; }
			// Right justify this info label
			lblCentiseconds.Left = picPreview.Left + picPreview.Width - lblCentiseconds.Width;

		}

		private void lstTimingGrids_Click(object sender, EventArgs e)
		{
			try
			{
				if (lstTimingGrids.SelectedItems.Count > 1)
				{
					// Multiple items selected, but how?
					if (isWiz)
					{ Fyle.BUG("Multiple Timing Grids selected, but how?"); }
				}
				if (lstTimingGrids.SelectedItems.Count == 1)
				{
					ListViewItem li = lstTimingGrids.SelectedItems[0];
					int n = li.Index;
					LOR4Admin.RenderEffects(seq.TimingGrids[n], ref picPreview, false);
				}
			}
			catch (Exception ex)
			{
				Fyle.BUG("lstTimingGrids.Click(" + sender.ToString() + ", " + e.ToString(), ex);
			}
		}

		private void lstTimingGrids_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			try
			{
				int n = e.Item.Index;
				if (lstTimingGrids.Items[n].Checked == true)
				{
					// Make sure this is actually a change before incrementing counter
					if (seq.TimingGrids[n].Selected != CheckState.Checked)
					{
						seq.TimingGrids[n].Selected = CheckState.Checked;
						selectedGridCount++;
					}
				}
				else
				{
					// Make sure this wasn't already unchecked before decrementing counter
					if (seq.TimingGrids[n].Selected != CheckState.Unchecked)
					{
						seq.TimingGrids[n].Selected = CheckState.Unchecked;
						selectedGridCount--;
					}
				}
			}
			catch (Exception et)
			{
				if (isWiz)
				{ Fyle.BUG("TimingGrids_ItemCheck(" + sender.ToString() + ", " + e.ToString(), et); }
			}
		}

		private void lstTimingGrids_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			try
			{
				int n = e.ItemIndex;
				if (n != lastGridIndex)
				{
					if ((n >= 0) && (n < lstTimingGrids.Items.Count))
					{
						ListViewItem ti = lstTimingGrids.Items[n];
						if (ti != null)
						{
							if (ti.Selected)
							{
								LOR4Admin.RenderTimings(seq.TimingGrids[n], ref picPreview);
								lastNode = null;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				// If whatever got selected or deselected is not valid
				// Just ignore this event
			}
		}
	} // end form class
} // end namespace UtilORama4
