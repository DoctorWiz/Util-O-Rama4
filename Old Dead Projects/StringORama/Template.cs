using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using LORUtils;
//using FuzzyString;

namespace StringORama
{


	public partial class frmTemplate : Form
	{

		private const string CRLF = "\r\n";
		private const string helpPage = "http://wizlights.com/utilorama/maporama";

		private string applicationName = "Map-O-Rama";
		private string thisEXE = "map-o-rama.exe";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string[] commandArgs;
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private int batch_fileNumber = 0;
		private string[] batch_fileList = null;
		private string cmdSelectionsFile = "";
		private bool processDrop = false;


		private const double MINSCORE = 0.85D;
		private const double MINMATCH = 0.92D;
		private Color COLOR_BK_SELECTED = Color.FromName("DarkBlue");
		private Color COLOR_BK_SELCHILD = Color.FromName("LightBlue");
		private Color COLOR_BK_MATCHED = Color.FromName("DarkGreen");
		private Color COLOR_BK_MATCHCHILD = Color.FromName("LightGreen");
		private Color COLOR_BK_BOTH = Color.FromName("DarkCyan");
		private Color COLOR_BK_NONE = Color.FromName("White");
		private Color COLOR_FR_SELECTED = Color.FromName("White");
		private Color COLOR_FR_NONE = Color.FromName("Black");

		private bool firstShown = false;

		// These are used by MapList so need to be public
		public Sequence4 seqSource = new Sequence4();
		public Sequence4 seqMaster = new Sequence4();
		public List<List<TreeNode>> sourceNodesSI = new List<List<TreeNode>>();
		public List<List<TreeNode>> masterNodesSI = new List<List<TreeNode>>();
		public string destinationFile = "";
		public string sourceFile = "";
		private string saveFile = "";

		private string basePath = "";
		private string SeqFolder = "";

		private int nodeIndex = utils.UNDEFINED;
		// Note Master->Source mappings are a 1:Many relationship.
		// A Master channel can map to only one Source channel
		// But a Source channel may map to more than one Master channel

		private StreamWriter logWriter1 = null;
		private StreamWriter logWriter2 = null;
		private StreamWriter logWriter3 = null;
		private string logFile1 = "";
		private string logFile2 = "";
		private string logFile3 = "";
		private bool log1Open = false;
		private bool log2Open = false;
		private bool log3Open = false;
		private string logHomeDir = ""; // Gets set during InitForm to "C:\\Users\\Wizard\\AppData\\Local\\UtilORama\\MapORama\\" (sustitute your name)
		private string logMsg = "";


		const string xmlInfo = "<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?>";
		const string TABLEchMap = "channelMap";
		const string TABLEfiles = "files";
		const string FIELDdestinationFile = "destinationFile";
		const string FIELDsourceFile = "sourceFile";
		const string TABLEchannels = "channels";
		const string FIELDmasterChannel = "masterChannel";
		const string FIELDsourceChannel = "sourceChannel";
		const string TABLEmappings = "mappings";

		public frmTemplate()
		{
			InitializeComponent();
		}

		private void btnBrowseSource_Click(object sender, EventArgs e)
		{
			BrowseSourceFile();
		}

		private void BrowseSourceFile()
		{ 
			string initDir = utils.DefaultSequencesPath;
			string initFile = "";
			if (sourceFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(sourceFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(sourceFile))
					{
						initFile = Path.GetFileName(sourceFile);
					}
				}
			}


			dlgOpenFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Open Sequence...";
			DialogResult result = dlgOpenFile.ShowDialog();

			pnlAll.Enabled = false;
			if (result == DialogResult.OK)
			{
				int err = LoadSourceFile(dlgOpenFile.FileName);
			} // end if (result = DialogResult.OK)
			pnlAll.Enabled = true;
			//btnSummary.Enabled = (mappingCount > 0);
			//mnuSummary.Enabled = btnSummary.Enabled;

		}

		public int LoadSourceFile(string sourceChannelFile)
		{
			ImBusy(true);
			int err = seqSource.ReadSequenceFile(sourceChannelFile);
			if (err < 100)
			{
				sourceFile = sourceChannelFile;
				txtSourceFile.Text = utils.ShortenLongPath(sourceFile, 80);
				this.Text = "Map-O-Rama - " + Path.GetFileName(sourceFile);
				FileInfo fi = new FileInfo(sourceChannelFile);
				Properties.Settings.Default.BasePath = fi.DirectoryName;
				Properties.Settings.Default.LastSourceFile = sourceFile;
				Properties.Settings.Default.Save();
				utils.FillChannels(treeSource, seqSource, sourceNodesSI, false, false);
				// Erase any existing mappings, and create a new blank one of proper size
			}
			ImBusy(false);
			return err;
		}

		private void btnBrowseMaster_Click(object sender, EventArgs e)
		{
			BrowsedestinationFile();
		}

		private void BrowsedestinationFile()
		{ 

			string initDir = utils.DefaultSequencesPath;
			string initFile = "";
			if (destinationFile.Length > 4)
			{
				string ldir = Path.GetDirectoryName(destinationFile);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(destinationFile))
					{
						initFile = Path.GetFileName(destinationFile);
					}
				}
			}


			dlgOpenFile.Filter = "Channel Configs (*.lcc)|*.lcc|Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
			dlgOpenFile.DefaultExt = "*.lee";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select Master Channel Config File...";
			DialogResult result = dlgOpenFile.ShowDialog();

			//pnlAll.Enabled = false;
			if (result == DialogResult.OK)
			{
				//destinationFile = dlgOpenFile.FileName;

				int err = LoaddestinationFile(dlgOpenFile.FileName);

			} // end if (result = DialogResult.OK)
			//pnlAll.Enabled = true;
			if (treeSource.Nodes.Count > 0)
			{
				//btnSummary.Enabled = true;
			}

		}

		private int LoaddestinationFile(string masterChannelFile)
		{
			ImBusy(true);
			int err = seqMaster.ReadSequenceFile(masterChannelFile);
			if (err < 100)  // Error numbers below 100 are warnings, above 100 are fatal
			{
				for (int w = 0; w < seqMaster.Children.byName.Count; w++)
				{
					//Console.WriteLine(seqMaster.Children.byName[w].Name);
				}





				destinationFile = masterChannelFile;
				FileInfo fi = new FileInfo(destinationFile);
				Properties.Settings.Default.LastdestinationFile = destinationFile;
				Properties.Settings.Default.Save();

				txtDestinationFile.Text = utils.ShortenLongPath(destinationFile, 80);



				utils.FillChannels(treeDestination, seqMaster, masterNodesSI, false, false);


			}


			ImBusy(false);
			return err;
		}


		private void ImBusy(bool workingHard)
		{
			pnlAll.Enabled = !workingHard;
			if (workingHard)
			{
				this.Cursor = Cursors.WaitCursor;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}

		}


		private void InitForm()
		{
			SeqFolder = utils.DefaultSequencesPath;
			logHomeDir = utils.GetAppTempFolder();

			destinationFile = Properties.Settings.Default.LastdestinationFile;
			if (!File.Exists(destinationFile))
			{ 
				destinationFile = "";
				Properties.Settings.Default.LastdestinationFile = destinationFile;
			}

			sourceFile = Properties.Settings.Default.LastSourceFile;
			if (!File.Exists(sourceFile))
			{
				sourceFile = "";
				Properties.Settings.Default.LastSourceFile = sourceFile;
			}


			saveFile = Properties.Settings.Default.LastSaveFile;




			Properties.Settings.Default.BasePath = basePath;
			Properties.Settings.Default.Save();


			RestoreFormPosition();

		} // end InitForm


		private void SaveFormPosition()
		{
			// Called with form is closed
			for(int i = 0; i < Screen.AllScreens.Length; i++)
			{
				if (Screen.AllScreens[i].DeviceName == Screen.FromControl(this).DeviceName)
				{
					Properties.Settings.Default.Screen = i;
				}
			}

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

		} // End SaveFormPosition

		private TreeNode FindNode(TreeNodeCollection treeNodes, string tag)
		{
			int tl = tag.Length;
			string st = "";
			TreeNode ret = new TreeNode("NOT FOUND");
			bool found = false;
			foreach (TreeNode nOde in treeNodes)
			{
				if (!found)
				{
					if (nOde.Tag.ToString().Length >= tag.Length)
					{
						st = nOde.Tag.ToString().Substring(0, tl);
						if (tag.CompareTo(st) == 0)
						{
							ret = nOde;
							found = true;
							break;
						}
					}
					if (!found)
					{
						if (nOde.Nodes.Count > 0)
						{
							ret = FindNode(nOde.Nodes, tag);
							if (ret.Text.CompareTo("NOT FOUND") != 0)
							{
								found = true;
							}
						}
					}
				}
			}
			return ret;
		}

		private int HighlightSourceNode(string tag)
		{
			int tl = tag.Length;
			int found = 0;
			foreach (TreeNode nOde in treeSource.Nodes)
			{
				// Reset any previous selections
				HighlightNode(nOde, false);

				// If node is mapped, it's tag will include the node it is mapped to, and thus the tag length
				// will be longer than just it's own tag info
				if (nOde.Tag.ToString().Length > tl)
				{
					string myTag = nOde.Tag.ToString().Substring(0, tl);
					if (tag.CompareTo(myTag) == 0)
					{
						// The old channel is mapped to this one
						nOde.EnsureVisible();
						HighlightNode(nOde, true);
						treeSource.SelectedNode = nOde;
						found++;

					}
				} // end long tag
			} // end foreach
			return found;
		} // end HighlightNewNodes

		private void UnHighlightAllNodes(TreeNodeCollection treeNodes)
		{
			foreach (TreeNode nOde in treeNodes)
			{
				// Reset any previous selections
				HighlightNode(nOde, false);
				if (nOde.Nodes.Count > 0)
				{
					UnHighlightAllNodes(nOde.Nodes);
				}
			} // end foreach
		}

		private void treeSource_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode sn = e.Node;
			if (sn != null)
			{
				SeqPart sid = (SeqPart)sn.Tag;
				//int sourceSI = sid.SavedIndex;
				// Did the selection change?
					
			}
		} // end treeSource_AfterSelect

		private void treeMaster_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode mn = e.Node;
			if (mn != null)
			{
				SeqPart mid = (SeqPart)mn.Tag;
				// Did the selection change?
				
			}
		} // end treNewChannel_Click


		private void HighlightNode(TreeNode nOde, bool highlight)
		{
			if (highlight)
			{
				if (nOde.BackColor == COLOR_BK_SELECTED)
				{
					nOde.BackColor = COLOR_BK_BOTH;
				}
				else
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				nOde.ForeColor = COLOR_FR_SELECTED;
			}
			else
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
			}
		}

		private void SelectNode(TreeNode nOde, bool select)
		// BEWARE: Procedure name is a bit misleading
		// This procedure sets the appearance of a node to the 'Selected' appearance
		// (As opposed to the 'Matched' appearance (see HighlightNode procedure above)
		{
			if (select)
			{
				if (nOde.BackColor == COLOR_BK_MATCHED)
				{
					nOde.BackColor = COLOR_BK_BOTH;
				}
				else
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				nOde.ForeColor = COLOR_FR_SELECTED;
			}
			else
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
			}
		}

		private void SelectNodes(TreeNodeCollection nOdes, int SavedIndex, bool select)
		{
			//string nTag = "";
			SeqPart nID = null;
			foreach (TreeNode nOde in nOdes)
			{
				nID = (SeqPart)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					SelectNode(nOde, select);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					SelectNodes(nOde.Nodes, SavedIndex, select);
				}
			}
		}

		private void UnSelectAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNode nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_MATCHED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					UnSelectAllNodes(nOde.Nodes);
				}
			}
		}

		private void HighlightNodes(TreeNodeCollection nOdes, int SavedIndex, bool highlight)
		{
			//string nTag = "";
			SeqPart nID = null;
			foreach (TreeNode nOde in nOdes)
			{
				nID = (SeqPart)nOde.Tag;
				if (nID.SavedIndex == SavedIndex)
				{
					HighlightNode(nOde, highlight);
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					HighlightNodes(nOde.Nodes, SavedIndex, highlight);
				}
			}
		}

			private void UnhighlightAllNodes(TreeNodeCollection nOdes)
		{
			foreach (TreeNode nOde in nOdes)
			{
				if (nOde.BackColor == COLOR_BK_BOTH)
				{
					nOde.BackColor = COLOR_BK_SELECTED;
				}
				else
				{
					nOde.BackColor = COLOR_BK_NONE;
					nOde.ForeColor = COLOR_FR_NONE;
				}
				if (nOde.Nodes.Count > 0)
				{
					// Recurse if child nodes
					UnhighlightAllNodes(nOde.Nodes);
				}
			}
		}

		private void BoldNodes(List<TreeNode> nodeList, bool emBolden)
		{
			//string nTag = "";


			foreach (TreeNode thenOde in nodeList)
			{
					if (emBolden)
					{
						thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Bold);
					}
					else
					{
						thenOde.NodeFont = new Font(treeSource.Font, FontStyle.Regular);
					}
					thenOde.Checked = emBolden;
			}
		}

		private int addElement(ref int[] numbers)
		{
			int newIdx = utils.UNDEFINED;

			if (numbers == null)
			{
				Array.Resize(ref numbers, 1);
				newIdx = 0;
			}
			else
			{
				int l = numbers.Length;
				Array.Resize(ref numbers, l + 1);
				newIdx = l;
			}

			return newIdx;
		}

		private int removeElement(ref int[] numbers, int index)
		{
			int l = numbers.Length;
			if (index < l)
			{
				l--;
				if (index < (l))
				{
					for (int i = index; i < l; i++)
					{
						numbers[i] = numbers[i + 1];
					}
				}
			}
			if (l == 0)
			{
				numbers = null;
			}
			else
			{
				Array.Resize(ref numbers, l);
			}

			return l;
		}

		private int FindElement(ref int[] numbers, int SID)
		{
			int found = utils.UNDEFINED;
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] == SID)
				{
					found = i;
					break;
				}
			}
			return found;
		}



		private void HighlightNodes(bool master, int SID)
		{
			if (master)
			{
				foreach(TreeNode nOde in masterNodesSI[SID])
				{
					HighlightNode(nOde, true);
				}
			}
			else
			{
				foreach(TreeNode nOde in sourceNodesSI[SID])
				{
					HighlightNode(nOde, true);
				}
			}
		}


		private void StatusMessage(string message)
		{
			// For now at least... I think I'm gonna use this just to show WHY the 'Map' button is not enabled


			lblMessage.Text = message;
			lblMessage.Visible = (message.Length > 0);
			lblMessage.Left = (this.Width - lblMessage.ClientSize.Width) / 2;

		}

		private void LogMessage(string message)
		{
			//TODO: This
		}

		private void frmTemplate_Shown(object sender, EventArgs e)
		{ }

		private void FirstShow()
		{
			string msg;
			DialogResult dr;

			ProcessCommandLine();
			if (batch_fileCount < 1)
			{
				if (File.Exists(destinationFile))
				{
				}
				if (File.Exists(sourceFile))
				{
				} // end last sequence file exists
			}
		} // end form shown event


		private void frmTemplate_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				firstShown = true;
				FirstShow();
			}
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);
		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new About();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}


		private void RestoreFormPosition()
		{
			// Multi Monitor Aware - Restore Postition to last Used
			Screen screen = Screen.AllScreens[0]; ;
			// Read the previous screen used from the INI file
			int screenNum = Properties.Settings.Default.Screen; // Val(ReadIni(File, "Main", "PosScreen"))
			// Verify the Specified screen exists, if so then use it
			if (Screen.AllScreens.Length < screenNum + 1)
			{
				screen = Screen.AllScreens[0];
			}
			else
			{
				screen = Screen.AllScreens[screenNum];
			}
			// Read the Position from the INI File
			Point lastLocation = Properties.Settings.Default.Location;
			Point newLocation = new System.Drawing.Point(lastLocation.X, lastLocation.Y);
			if (newLocation.X < 0) newLocation.X = 0;
			if (newLocation.Y < 0) newLocation.Y = 0;
			if (screen.Bounds.Contains(newLocation))
			{
				if ((newLocation.X + Width) > screen.Bounds.Right)
				{
					newLocation.X = screen.Bounds.Right - Width;
				}
				if ((newLocation.Y + Height) > Bounds.Bottom)
				{
					newLocation.Y = screen.Bounds.Bottom - Height;
				}
			}
			else
			{
				newLocation.X = screen.Bounds.Left + (screen.Bounds.Width - this.Width)/2;
				newLocation.Y = screen.Bounds.Top +  (screen.Bounds.Height - this.Height)/2;
			}
			this.StartPosition = FormStartPosition.Manual;
			this.Location = newLocation;
		}

		private void Event_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				processDrop = true;
				ProcessFileList(files);
				//this.Cursor = Cursors.Default;
			}

		}

		private void Event_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
			//this.Cursor = Cursors.Cross;
		}

		private string SuggestedNewName(string originalName)
		{
			string p = Path.GetDirectoryName(originalName);
			string n = Path.GetFileNameWithoutExtension(originalName);
			string e = Path.GetExtension(originalName);
			string ty = DateTime.Now.Year.ToString();
			string ly = (DateTime.Now.Year - 1).ToString();
			n = n.Replace(ly, ty);
			ly = (DateTime.Now.Year - 2).ToString();
			n = n.Replace(ly, ty);
			int i = 1;
			string ret = p + n + " Remapped" + e;
			while(File.Exists(ret))
			{
				i++;
				ret = p + n + " Remapped (" + i.ToString() + ")" + e;
			}
			return ret;
		} // end SuggestedNewName

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
					ProcessSourcesBatch(batch_fileList);
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
				if (ext.CompareTo(".lcc") == 0)
				{
					if (File.Exists(file))
					{
						LoaddestinationFile(file);
					}
				}
				if (ext.CompareTo(".chmap") == 0)
				{
					if (File.Exists(file))
					{
						// Do something...
					}
				}
				if ((ext.CompareTo(".lms") == 0) ||
						(ext.CompareTo(".las") == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;

				}
				//	}
				//}
			}
			if (batch_fileCount > 1)
			{
				batchMode = true;
				ProcessSourcesBatch(batch_fileList);
			}
			else
			{
			}
		} // end ProcessDragDropFiles

		private void ProcessSourcesBatch(string[] batchFilenames)
		{
			//if (batch_fileCount > 0)
			if (seqMaster.Children.Items.Count > 1)
			{
				ShowProgressBars(2);
				for (int f = 0; f < batch_fileCount; f++)
				{
					LoadSourceFile(batch_fileList[f]);
					if (File.Exists(destinationFile))
					{
						batch_fileNumber = f;
						// Do Something...
						string oldnm = Path.GetFileNameWithoutExtension(sourceFile);
						string newnm = SuggestedNewName(oldnm); // R we gettin here?
						string newfl = Path.GetDirectoryName(saveFile) + "\\";
						newfl += newnm;
						newfl += Path.GetExtension(sourceFile).ToLower();
						//SaveNewMappedSequence(newfl);
					}
					else
					{
						f = batch_fileCount;
						break;
					}
				} // cmdSeqFileCount-- Batch Mode or Not
			}
			batchMode = false;
			ShowProgressBars(0);
			ImBusy(false);
			//string msg = "Batch mode is not supported... yet.\r\nLook for this feature in a future release (soon)!";
			//MessageBox.Show(this, msg, "Batch Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
		} // end ProcessSourcesBatch

		private void mnuOpenMaster_Click(object sender, EventArgs e)
		{
		}

		private void mnuOpenSource_Click(object sender, EventArgs e)
		{
		}

		private void mnuOpenMap_Click(object sender, EventArgs e)
		{
		}

		private void mnuSelect_Click(object sender, EventArgs e)
		{

		}

		private void mnuSourceLeft_Click(object sender, EventArgs e)
		{
		}

		private void mnuSourceRight_Click(object sender, EventArgs e)
		{
		}

		private void mnuMatchOptions_Click(object sender, EventArgs e)
		{
		}

		private void mnuAutoMap_Click(object sender, EventArgs e)
		{
		}



		public SeqPart FuzzyFindName(string theName, Sequence4 sequence, TableType theType)
		{
			SeqPart ret = null;
			bool useFuzzy = Properties.Settings.Default.FuzzyUse;
			bool writeLog = Properties.Settings.Default.FuzzyWriteLog;

			string logFile = "";
			StreamWriter writer = new StreamWriter(Stream.Null);
			string lineOut = "";
			if (writeLog)
			{
				logFile = utils.GetAppTempFolder() + "Fuzzy.log";
				writer = new StreamWriter(logFile, true);
				writer.WriteLine("");
				lineOut = "Looking for     \"" + theName + "\" in ";
				lineOut += SeqEnums.TableTypeName(theType) + "s in ";
				lineOut += Path.GetFileName(sequence.info.filename);
				writer.WriteLine(lineOut);
			}

			if (sequence.Children.byName.ContainsKey(theName))
			{
				ret = sequence.Children.byName[theName];
				if (writeLog)
				{
					lineOut = "Exact Match Found \"" + ret.Name + "\" ";
					lineOut += "Saved Index=" + ret.SavedIndex.ToString();
					writer.WriteLine(lineOut);
				}
			}
			else
			{
				if (useFuzzy)
				{
					List<SeqPart> IDs = null;
					double[] scores = null;
					int[] SIs = null;
					int count = 0;
					long[] totTimes = null;
					int[] totCount = null;
					Array.Resize(ref totTimes, FuzzyFunctions.ALGORITHM_COUNT + 1);
					Array.Resize(ref totCount, FuzzyFunctions.ALGORITHM_COUNT + 1);
					double score;
					long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
					long prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
					bool caseSensitive = Properties.Settings.Default.FuzzyCaseSensitive;
					double minPrematchScore = Properties.Settings.Default.FuzzyMinPrematch;
					double minFinalMatchScore = Properties.Settings.Default.FuzzyMinFinal;

					// Now perform all other requested algorithms
					if (writeLog)
					{
						lineOut = "Fuzzy Prematches with a minimum of " + minPrematchScore.ToString() + ":";
						writer.WriteLine(lineOut);
					}

					// Go thru all objects
					foreach (SeqPart id in IDs)
					{
						// get a quick prematch score
						score = theName.RankEquality(id.Name, prematchAlgorithm);
						// fi the score is above the minimum PreMatch
						if (score * 100 > minPrematchScore)
						{
							// Increment count and save the SavedIndex
							// NOte: No need to save the PreMatch score
							count++;
							Array.Resize(ref SIs, count);
							SIs[count - 1] = id.SavedIndex;
							if (writeLog)
							{
								lineOut = score.ToString("0.0000") + " SI:";
								lineOut += id.SavedIndex.ToString().PadLeft(5);
								lineOut += "=\"" + id.Name + "\"";
								writer.WriteLine(lineOut);
							}
						} // end score exceeds minimum
					} // end foreach loop
					if (writeLog)
					{
						lineOut = count.ToString() + " found.";
						writer.WriteLine(lineOut);
					}
					if (count > 0)
					{
						// Resize scores array to final size
						Array.Resize(ref scores, count);
						// Loop thru qualifying prematches
						for (int i = 0; i < count; i++)
						{
							// Get the ID, perform a more thorough final fuzzy match, and save the score
							SeqPart id = sequence.Children.bySavedIndex[SIs[i]];
							//score = theName.RankEquality(id.Name, finalAlgorithms);

							string source = theName;
							string target = id.Name;
							double avgScore = 0;
							List<double> comparisonResults = new List<double>();
							int methodCount = 0;
							double runningTotal = 0D;
							double scoreOut = 0D;
							double minScore = 0.4D;
							double maxScore = 0.99D;
							double thisScore = 0.5D;
							double WLscore = 0.8D;
							bool valid = false;

							lineOut = "<";
							if ((finalAlgorithms & FuzzyFunctions.USE_CASEINSENSITIVE) > 0)
							{
								source = source.Capitalize();
								target = target.Capitalize();
								lineOut += "Not ";
							}
							if (writeLog)
							{
								lineOut += "Case Sensitive>";
								writer.Write(lineOut);
							}

							// Use Weighted Levenshtein to set a baseline
							//  for what is an acceptable score from other algorithms
							WLscore = source.WeightedLevenshteinSimilarity(target);
							maxScore = WLscore + (1.0D - WLscore) * 0.75D;
							minScore = WLscore / 3.0D;
							minScore = Math.Max(minScore, 0.4D);

							if (writeLog)
							{
								lineOut = "Baseline Score (Weighted Levenshtein: ";
								lineOut += WLscore.ToString("0.0000 ");
								//writer.WriteLine(lineOut);
								lineOut += "   Min: " + minScore.ToString("0.0000") + "   Max: " + maxScore.ToString("0.0000");
								writer.WriteLine(lineOut);
							}



							for (int al =1; al <= FuzzyFunctions.ALGORITHM_COUNT; al++)
							{
								long mask = (long)Math.Pow(2D, (double) al) / 2;
								//if ((finalAlgorithms & mask) > 0)
								//{
									var watch = System.Diagnostics.Stopwatch.StartNew();
									thisScore = source.RankEquality(target, mask);
									watch.Stop();
									long elapsedMs = watch.ElapsedMilliseconds;
									totTimes[al] += elapsedMs;
									totCount[al]++;
									valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
									if (writeLog)
									{
										//lineOut = WankLine(thisScore, valid, mask, elapsedMs);
										writer.WriteLine(lineOut);
									}
								//}
							}
							if (methodCount > 0)
							{
								avgScore = runningTotal / methodCount;
								if (writeLog)
								{
									lineOut = avgScore.ToString("0.0000 ") + "Average";
									writer.WriteLine(lineOut);
								}
							}
							scores[i] = avgScore;
						}
						// Now sort the final scores (and the SavedIndexes along with them)
						Array.Sort(scores, SIs);
						if (writeLog)
						{
							lineOut = "Final Matches:";
							writer.WriteLine(lineOut);
							for (int f = 0; f < count; f++)
							{
								lineOut = scores[f].ToString("0.0000") + " SI:";
								SeqPart y = sequence.Children.bySavedIndex[SIs[f]];
								lineOut += y.SavedIndex.ToString().PadLeft(5);
								lineOut += "=\"" + y.Name + "\"";
								writer.WriteLine(lineOut);
							}
						} // end writelog

						// Is the best/highest above the required minimum Final Match score?
						if (scores[count - 1] * 100 > minFinalMatchScore)
						{
							// Return the ID with the best qualifying final match
							ret = sequence.Children.bySavedIndex[SIs[count - 1]];
							// Get name just for debugging
							string msg = theName + " ~= " + ret.Name;
							if (writeLog)
							{
								lineOut = "Best Match Is:";
								writer.WriteLine(lineOut);
								lineOut = scores[count - 1].ToString("0.0000") + " SI:";
								SeqPart y = sequence.Children.bySavedIndex[SIs[count - 1]];
								lineOut += y.SavedIndex.ToString().PadLeft(5);
								lineOut += "=\"" + y.Name + "\"";
								writer.WriteLine(lineOut);
							}
						}
						else
						{
							if (writeLog)
							{
								lineOut = "No Final Matches meet the minimum score of " + minFinalMatchScore.ToString();
								writer.WriteLine(lineOut);
							}
						} // end score beats min final (or not)
					} // end if count; prematches
				} // end if useFuzzy
			} // end if exact match found, or not
			if (writeLog)
			{
				writer.Close();
				//System.Diagnostics.Process.Start(logFile);
			}
			return ret;
		}

		private static bool WankEquality(double thisScore, double minScore, double maxScore, ref double runningTotal, ref int methodCount)
		{
			bool valid = false;
			if ((thisScore >= minScore) && (thisScore <= maxScore)) valid = true;
			if (valid)
			{
				runningTotal += thisScore;
				methodCount++;
			}
			return valid;
		}

		public static SeqPart FindName(string theName, List<SeqPart> IDs)
		{
			SeqPart ret = null;
			int idx = BinarySearch(theName, IDs);
			if (idx > utils.UNDEFINED)
			{
				ret = IDs[idx];
			}
			return ret;
		}

		public static int BinarySearch(string theName, List<SeqPart> IDs)
		{
			return BinarySearch3(theName, IDs, 0, IDs.Count - 1);
		}

		public static int BinarySearch3(string theName, List<SeqPart> IDs, int start, int end)
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

		private void ShowProgressBars(int howMany)
		{
			if (howMany == 0)
			{
				prgBarInner.Visible = false;
				prgBarOuter.Visible = false;
			}
			else
			{
				prgBarInner.Value = 0;
				prgBarInner.Visible = true;
			}
			if (howMany == 2)
			{
				prgBarInner.Value = 0;
				prgBarOuter.Value = 0;
				prgBarInner.Visible = true;
				prgBarOuter.Visible = true;
			}
			else
			{
				prgBarOuter.Visible = false;
			}
		} // end ShowProgressBars

		public SeqPart FindByName(string theName, PartsCollection children, TableType PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			SeqPart ret = null;
			if (children.byName.TryGetValue(theName, out ret))
			{
				// Found the name, is the type correct?
				if (ret.TableType != PartType)
				{
					ret = null;
				}
			}
			else
			{
				if (finalAlgorithms > 0)
				{
					ret = FuzzyFind(theName, children, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, ignoreSelected);
				}
			}

			return ret;
		}

		public SeqPart FindByName(string theName, PartsCollection children, TableType PartType)
		{
			SeqPart ret = null;
			ret = FindByName(theName, children, PartType, 0, 0, 0, 0, false);
			return ret;
		}

		public static SeqPart FindByName(string theName, PartsCollection children)
		{
			SeqPart ret = null;
			int idx = BinarySearch(theName, children.Items);
			if (idx > utils.UNDEFINED)
			{
				ret = children.Items[idx];
			}
			return ret;
		}


		public SeqPart FuzzyFind(string theName, PartsCollection children, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			SeqPart ret = null;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;

			// Go thru all objects
			foreach (SeqPart child in children.Items)
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
				// Loop thru qualifying prematches
				for (int i = 0; i < count; i++)
				{
					// Get the ID, perform a more thorough final fuzzy match, and save the score
					SeqPart child = children.bySavedIndex[SIs[i]];
					score = theName.RankEquality(child.Name, finalAlgorithms);
					scores[i] = score;
				}
				// Now sort the final scores (and the SavedIndexes along with them)
				Array.Sort(scores, SIs);
				// Is the best/highest above the required minimum Final Match score?
				if (scores[count - 1] > minFinalMatch)
				{
					// Return the ID with the best qualifying final match
					ret = children.bySavedIndex[SIs[count - 1]];
					// Get Name just for debugging
					string msg = theName + " ~= " + ret.Name;
				}
			}
			return ret;
		}

		private void txtSourceFile_TextChanged(object sender, EventArgs e)
		{

		}

		private void frmTemplate_Load(object sender, EventArgs e)
		{
			InitForm();

		}

		private void frmTemplate_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();

		}
	} // end class frmTemplate
} // end namespace MapORama
