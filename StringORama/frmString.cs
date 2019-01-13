using LORUtils;
using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using LORUtils;

namespace StringORama
{
	public partial class frmString : Form
	{
		private const int MAX_PIX_PER_UNIV = 170;
		private const int MAX_CH_PER_UNIV = MAX_PIX_PER_UNIV * 3;

		private string lastFile = "";
		private string[] channels;
		private int[] unitIDs;
		private int[] unitChs;
		private RGBchannel[,] chanMatrix = null;
		private RGBchannel[,,] treeMatrix = null;
		private ChannelGroup[] sectionGroups = null;
		private ChannelGroup[] pixelGroups = null;
		//private ChannelGroup[] rowGroups = null;
		//private ChannelGroup[] colGroups = null;
		private int chCount = 0;
		private int changesMade = 0;
		private bool reversed = false;
		private int direction = 1;
		private Sequence4 seq;

		private string sectionName = "";
		private int sectionNum = 1;
		private int batchCount = MAX_PIX_PER_UNIV;
		private int batchStart = 1;
		private int batchEnd = MAX_PIX_PER_UNIV;
		private int chOrder = 1;
		private int groupSize = 16;
		private int chIncr = 1;
		private int universeNum = 5;
		private int pixelNum = 1;
		private int chanNum = 1;
		private int chanBegin = 1;
		private int chanEnd = 3;

		private int sections = 0;
		private int pixelsPerSect = 0;
		private int rows = 0;
		private int pixelsPerRow = 0;
		private int cols = 8;
		private int pixs = 3;

		private int airRows = 8;
		private int airCols = 8;

		public frmString()
		{
			InitializeComponent();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void InitForm()
		{
			lastFile = Properties.Settings.Default.lastFile;
			
			string ChannelList = Properties.Settings.Default.lastFile;
			//channels = ChannelList.Split(',');
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

			if (lastFile.Length > basePath.Length)
			{
				if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				{
					txtFile.Text = lastFile.Substring(basePath.Length);
				}
				else
				{
					txtFile.Text = lastFile;
				} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
			} // end if (lastFile.Length > basePath.Length)

			/*
			if (channels.Length > 0)
			{
					for (int loop = 0; loop < channels.Length; loop++)
					{
							if (channels[loop].Length > 0)
							{
									channelsListBox.Items.Add(channels[loop]);
							} // end if (channels[loop].Length > 0)
					} // end for (int loop = 0; loop <= channels.Length; loop++)
			} // end if (channels.Length > 0)
			 */
			RestoreFormPosition();
			SetTheControlsForTheHeartOfTheSun();
		} // end private void InitForm()

		private void EnableControls(bool newState)
		{
			btnMake.Enabled = newState;
			fraBatch1.Enabled = newState;
			fraTree.Enabled = newState;
			fraAirship.Enabled = newState;
			txtFile.Enabled = newState;
			btnBrowse.Enabled = newState;
		}

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			if (File.Exists(lastFile))
			{
				FileInfo fi = new FileInfo(lastFile);
				openFileDialog.InitialDirectory = fi.DirectoryName;
				openFileDialog.FileName = fi.Name;
			}
			else
			{
				openFileDialog.InitialDirectory = utils.DefaultSequencesPath;
				openFileDialog.FileName = "";
			}
			openFileDialog.Filter = "Sequences (*.lms,las)|*.lms;*.las|Just Musical Sequences (*.lms)|*.lms|Just Animated Sequences (*.las)|*.las";
			openFileDialog.DefaultExt = "*.lms;*.las";
			//openFileDialog.InitialDirectory = basePath;
			
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			openFileDialog.Multiselect = false;
			DialogResult result = openFileDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				lastFile = openFileDialog.FileName;
				if (lastFile.Length > basePath.Length)
				{
					if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
					{
						txtFile.Text = lastFile.Substring(basePath.Length);
					}
					else
					{
						txtFile.Text = lastFile;
					} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				} // end if (lastFile.Length > basePath.Length)
			} // end if (result = DialogResult.OK)
		} // end private void BrowseButton_Click(object sender, EventArgs e)

		private void lorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveFormSettings();
		}

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

		} // End LoadFormPostion

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

		private void SaveFormSettings()
		{
			byte which;

			
			Properties.Settings.Default.Track = Int16.Parse(txtTrack.Text);

			Properties.Settings.Default.MakeFence = chkDoFence.Checked;
			Properties.Settings.Default.FenceUniverse = Convert.ToInt16(numFenceUniverse.Value);
			Properties.Settings.Default.FenceBaseName = txtFenceBaseName.Text;
			Properties.Settings.Default.FenceStartCh = Int16.Parse(txtFenceStartCh.Text);
			Properties.Settings.Default.FenceSections = Int16.Parse(txtFenceSections.Text);
			which = 0;
			if (optFenceRGB.Checked) which = 1;
			if (optFenceGRB.Checked) which = 2;
			Properties.Settings.Default.FenceColorOrder = which;
			Properties.Settings.Default.FenceReverse = optFenceReverse.Checked;


			Properties.Settings.Default.MakeTree = chkDoTree.Checked;
			Properties.Settings.Default.TreeUniverse = Convert.ToInt16(numTreeUniverse.Value);
			Properties.Settings.Default.TreeBaseName = txtTreeBaseName.Text;
			Properties.Settings.Default.TreeStartCh = Int16.Parse(txtTreeStartCh.Text);
			Properties.Settings.Default.TreeCols = Int16.Parse(txtTreeCols.Text);
			Properties.Settings.Default.TreeRows = Int16.Parse(txtTreeRows.Text);
			Properties.Settings.Default.TreePixs = Int16.Parse(txtTreePixs.Text);
			which = 0;
			if (optTreeRGB.Checked) which = 1;
			if (optTreeGRB.Checked) which = 2;
			Properties.Settings.Default.TreeColorOrder = which;
			Properties.Settings.Default.TreeReverse = optTreeReverse.Checked;

			Properties.Settings.Default.MakeAirship = chkDoAirship.Checked;




			Properties.Settings.Default.lastFile = txtFile.Text;

			Properties.Settings.Default.Save();
		}


		private void SetTheControlsForTheHeartOfTheSun()
		{
			int which;
			txtTrack.Text = Properties.Settings.Default.Track.ToString();

			chkDoFence.Checked = Properties.Settings.Default.MakeFence;
			numFenceUniverse.Value = Properties.Settings.Default.FenceUniverse;
			txtFenceBaseName.Text = Properties.Settings.Default.FenceBaseName;
			txtFenceStartCh.Text = Properties.Settings.Default.FenceStartCh.ToString();
			txtFenceSections.Text = Properties.Settings.Default.FenceSections.ToString();
			which = Properties.Settings.Default.FenceColorOrder;
			if (which == 1) optFenceRGB.Checked = true;
			if (which == 2) optFenceGRB.Checked = true;
			optFenceReverse.Checked = Properties.Settings.Default.FenceReverse;
			chkDoFence.Checked = Properties.Settings.Default.MakeFence;

			chkDoTree.Checked = Properties.Settings.Default.MakeTree;
			numTreeUniverse.Value = Properties.Settings.Default.TreeUniverse;
			txtTreeBaseName.Text = Properties.Settings.Default.TreeBaseName;
			txtTreeStartCh.Text = Properties.Settings.Default.TreeStartCh.ToString();
			txtTreeCols.Text = Properties.Settings.Default.TreeCols.ToString();
			txtTreeRows.Text = Properties.Settings.Default.TreeRows.ToString();
			txtTreePixs.Text = Properties.Settings.Default.TreePixs.ToString();
			which = Properties.Settings.Default.TreeColorOrder;
			if (which == 1) optTreeRGB.Checked = true;
			if (which == 2) optTreeGRB.Checked = true;
			optTreeReverse.Checked = Properties.Settings.Default.TreeReverse;

			chkDoAirship.Checked = Properties.Settings.Default.MakeAirship;
		}

		private void SetGroupSize(ComboBox GrpSizeCbo, int size)
		{
			GrpSizeCbo.SelectedIndex = 0;
			for (int q = 0; q < GrpSizeCbo.Items.Count; q++)
			{
				if (Convert.ToInt16(GrpSizeCbo.Items[q]) == size)
				{
					GrpSizeCbo.SelectedIndex = q;
				}
			}
		}

		private void skimFile(string fileName)
		{
			int savedIndex = 0;
			int biggestIndex = 0;

			StreamReader reader = new StreamReader(fileName);
			string lineIn; // line to be written out, gets modified if necessary
			while ((lineIn = reader.ReadLine()) != null)
			{
				savedIndex = getNumberValue(lineIn, "SavedIndex");
				if (savedIndex > biggestIndex) biggestIndex = savedIndex;
			}
			reader.Close();
		}

		private int getNumberValue(string lineIn, string keyword)
		{
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			string part2 = lineIn.Substring(found);
			found = lineIn.IndexOf("\"");
			part2 = lineIn.Substring(0, found);
			int ret = Convert.ToInt16(lineIn);
			return ret;
		}

		private string getStringValue(string lineIn, string keyword)
		{
			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			string part2 = lineIn.Substring(found);
			found = lineIn.IndexOf("\"");
			string ret = lineIn.Substring(0, found);
			return ret;
		}

		private void putNumberValue(ref string lineIn, string keyword, int value)
		{
			string part1 = "";
			string part2 = "";

			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = lineIn.IndexOf("\"");
				part2 = lineIn.Substring(0, found);
				lineIn = part1 + value.ToString() + part2;
			}
			else
			{
				found = lineIn.IndexOf(">");
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = part1.IndexOf("\\");
				if (found >= 0)
				{
					part1 += part2.Substring(found);
					part2 = part2.Substring(found);
				}
				string lineOut = part1 + " " + keyword + "=\"" + value.ToString() + "\"" + part2;
				lineIn = lineOut;
			}
		}

		private void putStringValue(ref string lineIn, string keyword, string value)
		{
			string part1 = "";
			string part2 = "";

			keyword += "=\"";
			int found = lineIn.IndexOf(keyword);
			if (found >= 0)
			{
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = lineIn.IndexOf("\"");
				part2 = lineIn.Substring(0, found);
				lineIn = part1 + value + part2;
			}
			else
			{
				found = lineIn.IndexOf(">");
				part1 = lineIn.Substring(0, found);
				part2 = lineIn.Substring(found);
				found = part1.IndexOf("\\");
				if (found >= 0)
				{
					part1 += part2.Substring(found);
					part2 = part2.Substring(found);
				}
				string lineOut = part1 + " " + keyword + value + "\"" + part2;
				lineIn = lineOut;
			}
		}


		private void ImBusy(bool busyState)
		{
			fraBatch1.Enabled = !busyState;
			fraTree.Enabled = !busyState;
			fraAirship.Enabled = !busyState;
			fraBatch4.Enabled = !busyState;
			btnMake.Enabled = !busyState;
			txtFile.Enabled = !busyState;
			btnBrowse.Enabled = !busyState;
		}

		private void btnMake_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			
			seq = new Sequence4();
			if (File.Exists(lastFile))
			{
				seq.ReadSequenceFile(lastFile);
			}

			if (chkDoFence.Checked)
			{
				BuildFence();
			}

			if (chkDoAirship.Checked)
			{
				BuildShip();
			}

			if (chkDoTree.Checked)
			{
				BuildTreeNew();
			}

			FileInfo oFilenfo = new FileInfo(lastFile);
			string newFile = oFilenfo.Directory + "\\" + JustName(lastFile) + " + NEW" + oFilenfo.Extension;

			seq.WriteSequenceFile(newFile);
			//seq.WriteFileInDisplayOrder(newFile);

			ImBusy(false);
			SystemSounds.Exclamation.Play();

		}

		private void BuildFence()
		{
			/////////////////////////////////////////////////////////////
			//
			//   Batch 1 IS THE PERIMETER FENCE
			//   DONE AS MATRIX   ~~  x Sections by Y rgbChs-per-section
			//   Grouped by Section, and
			//   Grouped by rgbCh # (per section)

			pixelNum = 1;
			sections = Int16.Parse(txtFenceSections.Text);
			pixelsPerSect = Int16.Parse(txtFenseSectionSize.Text);
			int totalrgbChs = sections * pixelsPerSect;
			int totalChans = totalrgbChs * 3;
			universeNum = (int)numFenceUniverse.Value;
			int sectNum = 1;
			string baseName = txtFenceBaseName.Text;
			reversed = optFenceReverse.Checked;

			//Array.Resize(ref chanMatrix[], sections, pixelsPerSect);
			chanMatrix = new RGBchannel[sections, pixelsPerSect];
			sectionGroups = new ChannelGroup[sections];
			pixelGroups = new ChannelGroup[pixelsPerSect];

			sectionName = baseName + "s [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (166 * 3).ToString() + "]";
			int tknum = int.Parse(txtTrack.Text);
			Track trak = seq.CreateTrack(sectionName);

			sectionName = baseName + "s by Section [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (166 * 3).ToString() + "]";
			ChannelGroup grpSects =  seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpSects);

			sectionName = baseName + "s by rgbCh # [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (166 * 3).ToString() + "]";
			ChannelGroup grpPixels = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpPixels);

			


			sectionName = baseName;
			sectionNum = 1;
			pixelNum = 1;
			if (optFenceReverse.Checked)
			{
				universeNum++;
				int n1 = totalrgbChs - MAX_PIX_PER_UNIV;
				int n2 = n1 * 3 - 2;
				chanNum = n2;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceStartCh.Text);
				chIncr = -1;
			}
			else
			{
				chanNum = 1;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceSections.Text);
				chIncr = 1;
			}
				
			if (optFenceRGB.Checked) chOrder = 1;
			if (optFenceGRB.Checked) chOrder = 2;

			// Sections
			for (int sect = 0; sect < sections; sect++)
			{
				//TODO: Reverse Order
				// Use just forward order for now
				ChannelGroup theGroup = AddSection(sect);
				grpSects.AddItem(theGroup);
			}


			// Create the second major group
			// Another copy of the matrix, but pivoted
			// Grouped by rgbCh # instead of by sectioon #
			for (int pxl = 0; pxl < pixelsPerSect; pxl++)
			{
				string grName = sectionName + "s #" + (pxl+1).ToString("00") + "s";
				ChannelGroup theGroup = seq.CreateChannelGroup(grName);
				for (int s=0; s< sections; s++)
				{
					theGroup.AddItem(chanMatrix[s, pxl]);
				}
				grpPixels.AddItem(theGroup);

			}

			
		} // end btnMake_Click()

		private void BuildShip()
		{
			/////////////////////////////////////////////////////////////
			//
			//   Batch 3 IS THE AIRSHIP PROJECTOR
			//   DONE AS MATRIX   ~~  x Columns by Y Rows
			//   Grouped by Column and by Row
			
			pixelNum = 0;
			airCols = Int16.Parse(txtAirCols.Text);
			airRows = Int16.Parse(txtAirRows.Text);
			int totalrgbChs = airRows * airCols;
			int totalChans = totalrgbChs * 3;
			universeNum = (int)numUnivAirship.Value;
			int airCol = 1;
			string baseName = txtAirName.Text;
			reversed = false;

			//Array.Resize(ref chanMatrix[], sections, pixelsPerSect);
			chanMatrix = new RGBchannel[airCols, airRows];
			//colGroups = new ChannelGroup[airCols];
			//rowGroups = new ChannelGroup[airRows];

			sectionName = baseName + " [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum).ToString() + ".";
			sectionName += (64 * 3).ToString() + "]";
			int tknum = int.Parse(txtTrack.Text);
			Track trak = seq.CreateTrack(sectionName);

			sectionName = baseName + " by Column [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum).ToString() + ".";
			sectionName += (64 * 3).ToString() + "]";
			ChannelGroup grpCols = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpCols);

			sectionName = baseName + " by Row [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum).ToString() + ".";
			sectionName += (64 * 3).ToString() + "]";
			ChannelGroup grpRows = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpRows);




			sectionName = baseName;
			sectionNum = 1;
			pixelNum = 1;
			chanNum = 1;
			batchStart = Int16.Parse(txtFenceStartCh.Text);
			batchEnd = Int16.Parse(txtFenceSections.Text);
			chIncr = 1;

			if (optRGB3.Checked) chOrder = 1;
			if (optGRB3.Checked) chOrder = 2;

			// Sections
			for (int c = 0; c < airCols; c++)
			{
				//TODO: Reverse Order
				// Use just forward order for now
				ChannelGroup theGroup = AddAirColumn(c);
				grpCols.AddItem(theGroup);
			}


			// Create the second major group
			// Another copy of the matrix, but pivoted
			// Grouped by Row instead of by Column
			for (int r = 0; r < airRows; r++)
			{
				string grName = sectionName + " Row " + (r).ToString("0");
				ChannelGroup theGroup = seq.CreateChannelGroup(grName);
				for (int c = 0; c < airCols; c++)
				{
					theGroup.AddItem(chanMatrix[c, r]);
				}
				grpRows.AddItem(theGroup);

			}


		} // end btnMake_Click()

		private void BuildTreeNew()
		{
			/////////////////////////////////////////////////////////////
			//
			//   Batch 2 IS THE BIG CENTER PIXEL TREE
			//   DONE AS MATRIX   ~~  Initially with 12 rows with 28 pixels each
			//   For a total of 340 pixels (2 full universes)
			//   Each row has 8 sections with initially 3 or 4 pixels on each row.
			//   Once the tree is built and tested, pixels will get moved to the row
			//   and section they actually end up in

			pixelNum = 1;
			rows = int.Parse(txtTreeRows.Text);
			cols = int.Parse(txtTreeCols.Text);
			pixs = int.Parse(txtTreePixs.Text);
			int totalrgbChs = rows * pixelsPerRow;
			int totalChans = totalrgbChs * 3;
			universeNum = (int)numTreeUniverse.Value;
			int sectNum = 1;
			string baseName = txtTreeBaseName.Text;
			reversed = optTreeReverse.Checked;
			if (reversed) { direction = -1; } else { direction = 1; }

			treeMatrix = new RGBchannel[rows, cols, pixs];
			//rowGroups = new ChannelGroup[rows];
			//colGroups = new ChannelGroup[cols];

			sectionName = baseName + "s [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			int tknum = int.Parse(txtTrack.Text);
			Track trak = seq.CreateTrack(sectionName);

			sectionName = baseName + "s by Row [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			ChannelGroup grpRows = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpRows);

			sectionName = baseName + "s by Section [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			ChannelGroup grpCols = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpCols);





			sectionName = baseName;
			sectionNum = 1;
			pixelNum = 1;
			if (reversed)
			{
				universeNum++;
				int n1 = totalrgbChs - MAX_PIX_PER_UNIV;
				int n2 = n1 * 3 - 2;
				chanNum = n2;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceStartCh.Text);
				chIncr = -1;
			}
			else
			{
				chanNum = 1;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceSections.Text);
				chIncr = 1;
			}

			if (optTreeRGB.Checked) chOrder = 1;
			if (optTreeGRB.Checked) chOrder = 2;

			chanNum = 388;

			for (int row = 0; row < rows; row++)
			{
				//TODO: Reverse Order
				// Use just forward order for now
				ChannelGroup theGroup = AddTreeRow(row);
				grpRows.AddItem(theGroup);
			}

			for (int col = 0; col < cols; col++)
			{
				string grName = sectionName + "s Column " + col.ToString("00");
				ChannelGroup theGroup = seq.CreateChannelGroup(grName);
				for (int row = 0; row < rows; row++)
				{
					for (int pix = 0; pix < pixelsPerSect; pix++)
					{
						theGroup.AddItem(treeMatrix[row, col, pix]);
					}
				}
				grpCols.AddItem(theGroup);

			}


		} // end btnMake_Click()

		private string FaceID(int col)
			{
				string ret = "";
				switch(col)
				{
					case 1:
						ret = "s}{L}";
						break;
					case 2:
						ret = "se}";
						break;
					case 3:
						ret = "e}{F}";
						break;
					case 4:
						ret = "ne}";
						break;
					case 5:
						ret = "n}{R}";
						break;
					case 6:
						ret = "nw}";
						break;
					case 7:
						ret = "w}{B}";
						break;
					case 8:
						ret = "sw}";
						break;
				}
				return ret;
			}

		private void BuildTreeOld()
		{
			/////////////////////////////////////////////////////////////
			//
			//   Batch 2 IS THE BIG CENTER PIXEL TREE
			//   DONE AS MATRIX   ~~  Initially with 12 rows with 28 pixels each
			//   For a total of 340 pixels (2 full universes)
			//   Each row has 8 sections with initially 3 or 4 pixels on each row.
			//   Once the tree is built and tested, pixels will get moved to the row
			//   and section they actually end up in

			pixelNum = 1;
			rows = int.Parse(txtTreeRows.Text);
			pixelsPerRow = int.Parse(txtTreeCols.Text);
			int totalrgbChs = rows * pixelsPerRow;
			int totalChans = totalrgbChs * 3;
			universeNum = (int)numTreeUniverse.Value;
			int sectNum = 1;
			string baseName = txtTreeBaseName.Text;
			reversed = optTreeReverse.Checked;

			chanMatrix = new RGBchannel[rows, pixelsPerRow];
			sectionGroups = new ChannelGroup[rows];
			pixelGroups = new ChannelGroup[pixelsPerRow];

			sectionName = baseName + "s [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			int tknum = int.Parse(txtTrack.Text);
			Track trak = seq.CreateTrack(sectionName);

			sectionName = baseName + "s by Row [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			ChannelGroup grpRows = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpRows);

			sectionName = baseName + "s by Section [U" + universeNum.ToString();
			sectionName += ".001-U" + (universeNum + 1).ToString() + ".";
			sectionName += (MAX_CH_PER_UNIV).ToString() + "]";
			ChannelGroup grpCols = seq.CreateChannelGroup(sectionName);
			trak.AddItem(grpCols);

			int direction = 1; // forward, set to -1 for reverse


			sectionName = baseName;
			sectionNum = 1;
			pixelNum = 1;
			if (optTreeReverse.Checked)
			{
				universeNum++;
				int n1 = totalrgbChs - 160;
				int n2 = n1 * 3 - 2;
				chanNum = n2;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceStartCh.Text);
				chIncr = -1;
			}
			else
			{
				chanNum = 1;
				batchStart = Int16.Parse(txtFenceStartCh.Text);
				batchEnd = Int16.Parse(txtFenceSections.Text);
				chIncr = 1;
			}

			if (optTreeRGB.Checked) chOrder = 1;
			if (optTreeGRB.Checked) chOrder = 2;

			for (int r = 0; r < rows; r++)
			{
				//TODO: Reverse Order
				// Use just forward order for now
				//ChannelGroup theGroup = AddRow(r);
				//grpRows.AddItem(theGroup);
			}

			for (int pxl = 0; pxl < pixelsPerRow; pxl++)
			{
				string grName = sectionName + "s Section " + pxl.ToString("00");
				ChannelGroup theGroup = seq.CreateChannelGroup(grName);
				for (int row = 0; row < rows; row++)
				{
					theGroup.AddItem(chanMatrix[row, pxl]);
				}
				grpCols.AddItem(theGroup);

			}


		} // end btnMake_Click()

		public ChannelGroup AddSection(int sect)
		{
			int pixID = batchStart;
			int groupMember = 1;
			int uch = 1;
			if (chIncr < 0) uch = batchCount * 3 - 2;
			int trkNum = Int16.Parse(txtTrack.Text)-1;
			string theName = "";
			string pxName = "";
			string chName = "";
			string grName = "";

			grName = sectionName + "s Section " + (sect+1).ToString("00");
			grName += " [U" + universeNum.ToString() + ".";
			if (optFenceReverse.Checked)
			{
				grName += (chanNum - pixelsPerSect * 3+3).ToString("000") + "-" + (chanNum + 2).ToString("000") + "]";
			}
			else
			{
				grName += chanNum.ToString("000") + "-" + (chanNum + pixelsPerSect * 3 + 2).ToString("000") + "]";
			}
			ChannelGroup thisGroup = seq.CreateChannelGroup(grName);

			for (int pxl = 0; pxl < pixelsPerSect; pxl++)
			{
				pxName = sectionName + " " + (sect+1).ToString("00");
				pxName += "-" + (pxl+1).ToString("00");

				//RGBchannel theRGBch = MakeRGBch(pxName, sect, pxl);
				//thisGroup.AddItem(theRGBch);

			}

			return thisGroup;

		} // end void AddBatch();

		public ChannelGroup AddAirColumn(int c)
		{
			int pixID = batchStart;
			int groupMember = 1;
			int uch = 1;
			if (chIncr < 0) uch = batchCount * 3 - 2;
			int trkNum = Int16.Parse(txtTrack.Text) - 1;
			string theName = "";
			string pxName = "";
			string chName = "";
			string grName = "";

			grName = sectionName + " Column " + (c).ToString("0");
			grName += " [U" + universeNum.ToString() + ".";
			if (optFenceReverse.Checked)
			{
				grName += (chanNum - pixelsPerSect * 3 + 3).ToString("000") + "-" + (chanNum + 2).ToString("000") + "]";
			}
			else
			{
				grName += chanNum.ToString("000") + "-" + (chanNum + airRows * 3 + 2).ToString("000") + "]";
			}
			ChannelGroup thisGroup = seq.CreateChannelGroup(grName);

			for (int r = 0; r < airRows; r++)
			{
				pxName = sectionName + " " + (c).ToString("0");
				pxName += "," + (r).ToString("0");
				pxName += " #" + pixelNum.ToString("00");

				RGBchannel theRGBch = ShiprgbCh(pxName, c, r);
				thisGroup.AddItem(theRGBch);

			}

			return thisGroup;

		} // end void AddBatch();

		public ChannelGroup AddTreeRow(int row)
		{
			int pixID = batchStart;
			int groupMember = 1;
			int uch = 1;
			if (chIncr < 0) uch = batchCount * 3 - 2;
			int trkNum = Int16.Parse(txtTrack.Text) - 1;
			string theName = "";
			string pxName = "";
			string chName = "";
			string grName = "";
			int sxNo = 1;
			string sxName = "1S";
			int subNo = 1;

			grName = sectionName + "s Row " + (row+1).ToString("00");
			grName += " [U" + universeNum.ToString() + ".";
			if (optFenceReverse.Checked)
			{
				grName += (chanNum + 2).ToString("000") + "-" + (chanNum - pixelsPerRow * 3).ToString("000") + "]";
			}
			else
			{
				grName += chanNum.ToString("000") + "-" + (chanNum + pixelsPerRow * 3 + 2).ToString("000") + "]";
			}
			ChannelGroup thisGroup = seq.CreateChannelGroup(grName);

			for (int col = 7; col >= 0; col--)
			{
				for (int pix = 2; pix >= 0; pix--)
				{
					if (pixelNum < 301)
					{
						string pixName = "Tree Pixel " + pixelNum.ToString("000");
						pixName += " {R" + (row + 1).ToString("00");
						pixName += "C" + (col + 1).ToString("0");
						pixName += "P" + (pix + 1).ToString("0");
						pixName += FaceID(col);

						RGBchannel theRGBch = MakeRGBch(pixName, row, col, pix);
						thisGroup.AddItem(theRGBch);

						pixelNum ++;
						subNo++;
					} // End less than 301 pixels
				} // End pixels per column loop
			} // end columns per row loop

			return thisGroup;

		} // end void AddBatch();

		private RGBchannel MakeRGBch(string baseName, int row, int col, int pix)
		{
			string pxName = baseName;
			string chName = baseName;
			pxName = baseName + " [U" + universeNum.ToString() + ".";
			//if (optReverse1.Checked)
			//{
			//	pxName += (chanNum + 2).ToString() + "-" + chanNum.ToString() + "]";
			//}
			//else
			//{
				pxName += chanNum.ToString("000") + "-" + (chanNum + 2).ToString("000") + "]";
			//}
			RGBchannel theRGBch = seq.CreateRGBchannel(pxName);

			if (chOrder == 1) // RGB Order
			{
				chName = baseName + " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (R)";
				Channel redChannel = seq.CreateChannel(chName);
				redChannel.output.deviceType = DeviceType.DMX;
				redChannel.output.network = universeNum;
				redChannel.color = utils.LORCOLOR_RED;
				redChannel.Centiseconds = seq.Centiseconds;
				redChannel.output.circuit = chanNum;
				theRGBch.redChannel = redChannel;
				chanNum++;

				chName = baseName + " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (G)";
				Channel grnChannel = seq.CreateChannel(chName);
				grnChannel.output.deviceType = DeviceType.DMX;
				grnChannel.output.network = universeNum;
				grnChannel.color = utils.LORCOLOR_GRN;
				grnChannel.Centiseconds = seq.Centiseconds;
				grnChannel.output.circuit = chanNum;
				theRGBch.grnChannel = grnChannel;
				chanNum++;

				chName = baseName + " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (B)";
				Channel bluChannel = seq.CreateChannel(chName);
				bluChannel.output.deviceType = DeviceType.DMX;
				bluChannel.output.network = universeNum;
				bluChannel.color = utils.LORCOLOR_BLU;
				bluChannel.Centiseconds = seq.Centiseconds;
				bluChannel.output.circuit = chanNum;
				theRGBch.bluChannel = bluChannel;
				chanNum++;
			}


			//chanMatrix[sect, pxl] = theRGBch;
			treeMatrix[row, col, pix] = theRGBch;

			if (direction < 0)
			{
				chanNum -= 6;
				if (chanNum < 1)
				{
					chanNum = MAX_CH_PER_UNIV - 2;
					universeNum--;
				}
			}
			else
			{
				if (chanNum > (MAX_CH_PER_UNIV))
				{
					chanNum = 1;
					universeNum++;
				}
			}
			//pixelNum += direction; // Handled above in calling routine


			return theRGBch;
		}

		private RGBchannel ShiprgbCh(string theName, int col, int row)
		{
			string pxName = theName;
			string chName = theName;
			pxName += " [U" + universeNum.ToString() + ".";
			//if (optReverse1.Checked)
			//{
			//	pxName += (chanNum + 2).ToString() + "-" + chanNum.ToString() + "]";
			//}
			//else
			//{
			pxName += chanNum.ToString("000") + "-" + (chanNum + 2).ToString("000") + "]";
			//}
			RGBchannel theRGBch = seq.CreateRGBchannel(pxName);

			if (chOrder == 1) // RGB Order
			{
				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (R)";
				Channel redChannel = seq.CreateChannel(chName);
				redChannel.output.deviceType = DeviceType.DMX;
				redChannel.output.network = universeNum;
				redChannel.color = utils.LORCOLOR_RED;
				redChannel.Centiseconds = seq.Centiseconds;
				redChannel.output.circuit = chanNum;
				theRGBch.redChannel = redChannel;
				chanNum++;

				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (G)";
				Channel grnChannel = seq.CreateChannel(chName);
				grnChannel.output.deviceType = DeviceType.DMX;
				grnChannel.output.network = universeNum;
				grnChannel.color = utils.LORCOLOR_GRN;
				grnChannel.Centiseconds = seq.Centiseconds;
				grnChannel.output.circuit = chanNum;
				theRGBch.grnChannel = grnChannel;
				chanNum++;

				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (R)";
				Channel bluChannel = seq.CreateChannel(chName);
				bluChannel.output.deviceType = DeviceType.DMX;
				bluChannel.output.network = universeNum;
				bluChannel.color = utils.LORCOLOR_BLU;
				bluChannel.Centiseconds = seq.Centiseconds;
				bluChannel.output.circuit = chanNum;
				theRGBch.bluChannel = bluChannel;
				chanNum++;
			}

			if (chOrder == 2) // GRB Order
			{
				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (G)";
				Channel grnChannel = seq.CreateChannel(chName);
				grnChannel.output.deviceType = DeviceType.DMX;
				grnChannel.output.network = universeNum;
				grnChannel.color = utils.LORCOLOR_GRN;
				grnChannel.Centiseconds = seq.Centiseconds;
				grnChannel.output.circuit = chanNum;
				theRGBch.grnChannel = grnChannel;
				chanNum++;

				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (R)";
				Channel redChannel = seq.CreateChannel(chName);
				redChannel.output.deviceType = DeviceType.DMX;
				redChannel.output.network = universeNum;
				redChannel.color = utils.LORCOLOR_RED;
				redChannel.Centiseconds = seq.Centiseconds;
				redChannel.output.circuit = chanNum;
				theRGBch.redChannel = redChannel;
				chanNum++;

				chName += " [U" + universeNum.ToString() + "." + chanNum.ToString("000") + "] (R)";
				Channel bluChannel = seq.CreateChannel(chName);
				bluChannel.output.deviceType = DeviceType.DMX;
				bluChannel.output.network = universeNum;
				bluChannel.color = utils.LORCOLOR_BLU;
				bluChannel.Centiseconds = seq.Centiseconds;
				bluChannel.output.circuit = chanNum;
				theRGBch.bluChannel = bluChannel;
				chanNum++;
			}

			chanMatrix[col, row] = theRGBch;

			pixelNum++;


			return theRGBch;
		}


		public string JustName(string fileName)
		{
			// Returns just the name portion of a filename
			// without the path, without any /'s
			// and without the extension and it's associated .
			string nameOnly = "";
			int i = fileName.IndexOf("\\");
			if (i < 0)
			{
				nameOnly = fileName;
			}
			else
			{
				string[] parts = fileName.Split('\\');
				nameOnly = parts[parts.Length-1 ];
			}
			i = nameOnly.LastIndexOf(".");
			if (i > 0)
			{
				nameOnly = nameOnly.Substring(0, i );
			}


			return nameOnly;
		}

		private void optForward1_CheckedChanged(object sender, EventArgs e)
		{
			txtFenceStartCh.Text = "1";
		}

		private void optReverse1_CheckedChanged(object sender, EventArgs e)
		{
			int x = 160 * 3 - 2;
			txtFenceStartCh.Text = x.ToString();
		}

		private void label12_Click(object sender, EventArgs e)
		{

		}

		private void txtTreeRows_TextChanged(object sender, EventArgs e)
		{

		}

		private void frmString_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}
	} // end public partial class lorForm : Form
} // end namespace StringORama