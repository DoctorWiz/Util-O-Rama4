using LORUtils;
using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using LORUtils;

namespace TreeORama
{
	public partial class frmTree : Form
	{
		private string lastFile = "";
		private Sequence4 seq = null;
		private ChannelGroup[] colGroups = null;
		private ChannelGroup[] rowGroups = null;
		private ChannelGroup masterColGroup = null;
		private ChannelGroup masterRowGroup = null;
		private Track treeTrack = null;

		int pixel = 1;
		int row = 1;
		int col = 3;
		int px = 1;
		string dir = ""; // Direction a column faces, e,ne,n,nw,w,sw,s,se.  Not to be confused with direction which controls DMX channel numbers
		string face = "";
		int univ = 8;
		int chNum = 388;
		string pixelName = "";
		int direction = -1;  // Amount to increase/decrease DMX channel.  Should be +1 or -1.  Do not confused with dir which is which way a column faces

		int[] pxps =
		{
			1,0,0,0,0,0,0,0,
			2,1,1,1,1,1,0,1,
			2,2,1,2,1,1,1,1,
			2,2,1,2,1,1,2,1,
			3,2,2,2,2,2,2,1,
			3,3,1,4,3,2,2,3,
			3,3,2,3,2,3,2,3,
			3,4,3,4,3,3,3,3,
			5,3,4,3,4,4,4,4,
			4,4,4,3,4,4,4,4,
			5,3,6,4,4,5,3,5,
			5,4,6,5,4,6,5,4,
			5,4,6,5,4,5,5,4,
			5,5
		};


		public frmTree()
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
				if (File.Exists(lastFile))
				{
					seq = new Sequence4(lastFile);
					CreateGroups();
				}
				else
				{
					lastFile = "";
				}
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

		private void Reset()
		{
			ChannelGroup[] colGroups = null;
			ChannelGroup[] rowGroups = null;
			ChannelGroup masterColGroup = null;
			ChannelGroup masterRowGroup = null;
			Track treeTrack = null;

			pixel = 1;
			row = 1;
			col = 3;
			px = 1;
			dir = "";
			face = "";
			univ = 8;
			chNum = 388;
			pixelName = "";

			dir = DirName(col);
			face = FaceName(col);
			txtCol.Text = col.ToString();
			txtRow.Text = row.ToString();
			txtPx.Text = px.ToString();
			txtDir.Text = dir;
			txtFace.Text = face;
			txtPixel.Text = pixel.ToString("000");
			txtUniv.Text = univ.ToString();
			txtCh.Text = chNum.ToString("000");

			pixelName = MakePixelName();
			lblPixelName.Text = pixelName;

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
						Reset();
						seq = new Sequence4(lastFile);
						txtFile.Text = lastFile;
						CreateGroups();
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


			Properties.Settings.Default.lastFile = txtFile.Text;

			Properties.Settings.Default.Save();
		}


		private void SetTheControlsForTheHeartOfTheSun()
		{
			int which;
			Reset();
		}

		private void ImBusy(bool busyState)
		{
			btnNextPixel.Enabled = !busyState;
			txtFile.Enabled = !busyState;
			btnBrowse.Enabled = !busyState;
		}

		public string MakeBaseName()
		{
			string ret = "Tree Pixel ";
			ret += pixel.ToString("000");
			ret += " {R" + row.ToString("0");
			ret += "C" + col.ToString("0");
			ret += "P" + px.ToString("0");
			ret += dir + "}";
			if (face.Length > 0)
			{
				ret += "{" + face + "}";
			}
			ret += " [U" + univ.ToString("0");
			ret += "." + chNum.ToString("000");

			return ret;
		}

		private string MakePixelName()
		{
			string ret = MakeBaseName();
			ret += "-" + (chNum + 2).ToString("000") + "]";
			return ret;
		}

		private string MakeChannelName(string colorCode)
		{
			string ret = MakeBaseName();
			ret += "] (" + colorCode + ")";
			return ret;
		}

		private string DirName(int col)
		{
			string ret = "";
			switch (col)
			{
				case 1:
					ret = "s";
					break;
				case 2:
					ret = "se";
					break;
				case 3:
					ret = "e";
					break;
				case 4:
					ret = "ne";
					break;
				case 5:
					ret = "n";
					break;
				case 6:
					ret = "nw";
					break;
				case 7:
					ret = "w";
					break;
				case 8:
					ret = "sw";
					break;
			}
			return ret;
		}

		private string FaceName(int col)
		{
			string ret = "";
			switch (col)
			{
				case 1:
					ret = "L";
					break;
				case 3:
					ret = "F";
					break;
				case 5:
					ret = "R";
					break;
				case 7:
					ret = "B";
					break;
			}
			return ret;
		}


		private void frmString_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}

		private void CreateGroups()
		{
			ChannelGroup grp = null;
			string nm = "";
			Array.Resize(ref rowGroups, 14);
			Array.Resize(ref colGroups, 8);
			seq = new Sequence4(lastFile);
			treeTrack = seq.CreateTrack("Tree Pixels [U7.001-U8-388");
			masterRowGroup = seq.CreateChannelGroup("Tree Pixels by Row");
			masterColGroup = seq.CreateChannelGroup("Tree Pixels by Column");
			treeTrack.AddItem(masterRowGroup);
			treeTrack.AddItem(masterColGroup);
			for (int r = 0; r < 14; r++)
			{
				nm = "Tree Pixels Row " + (r + 1).ToString();
				grp = seq.CreateChannelGroup(nm);
				rowGroups[r] = grp;
				masterRowGroup.AddItem(grp);
			}
			for (int c = 0; c < 8; c++)
			{
				nm = "Tree Pixels Column " + (c + 1).ToString();
				nm += " {" + DirName(c + 1) + "}";
				string f = FaceName(c + 1);
				if (f.Length > 0)
				{
					nm += "{" + f + "}";
				}
				grp = seq.CreateChannelGroup(nm);
				colGroups[c] = grp;
				masterColGroup.AddItem(grp);
			}




		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void btnNextPixel_Click(object sender, EventArgs e)
		{
			NextPixel();
		}

		private void NextPixel()
		{
			// Create new RGB Channel
			RGBchannel rgbCh = seq.CreateRGBchannel(pixelName);

			string chName = MakeChannelName("R");
			Channel chan = seq.CreateChannel(chName);
			chan.color = utils.LORCOLOR_RED;
			chan.rgbChild = RGBchild.Red;
			rgbCh.redChannel = chan;
			chan.rgbParent = rgbCh;
			chan.output.deviceType = DeviceType.DMX;
			chan.output.universe = univ;
			chan.output.channel = chNum;
			IncrementDMXchannel(1);

			chName = MakeChannelName("G");
			chan = seq.CreateChannel(chName);
			chan.color = utils.LORCOLOR_GRN;
			chan.rgbChild = RGBchild.Green;
			rgbCh.grnChannel = chan;
			chan.rgbParent = rgbCh;
			chan.output.deviceType = DeviceType.DMX;
			chan.output.universe = univ;
			chan.output.channel = chNum;
			IncrementDMXchannel(1);

			chName = MakeChannelName("B");
			chan = seq.CreateChannel(chName);
			chan.color = utils.LORCOLOR_BLU;
			chan.rgbChild = RGBchild.Blue;
			rgbCh.bluChannel = chan;
			chan.rgbParent = rgbCh;
			chan.output.deviceType = DeviceType.DMX;
			chan.output.universe = univ;
			chan.output.channel = chNum;
			if (direction == 1)
			{
				IncrementDMXchannel(1);
			}
			else
			{
				IncrementDMXchannel(-5);
			}


			colGroups[col - 1].AddItem(rgbCh);
			rowGroups[row - 1].AddItem(rgbCh);

			lstPixelNames.Items.Add(pixelName);
			lstPixelNames.SelectedIndex = lstPixelNames.Items.Count - 1;


			// Update display for the NEXT pixel
			pixel++;
			px++;
			txtPixel.Text = pixel.ToString("000");
			txtPx.Text = px.ToString();
			txtUniv.Text = univ.ToString();
			txtCh.Text = chNum.ToString("000");
			pixelName = MakePixelName();
			lblPixelName.Text = pixelName;
			HighlightCol3(col);


			if (pixel == 301)
			{
				btnSave.Enabled = true;
				btnNextCol.Enabled = false;
				btnNextPixel.Enabled = false;
				SystemSounds.Asterisk.Play();
			}
			else
			{
				SystemSounds.Beep.Play();
			}


		}

		private void btnNextCol_Click(object sender, EventArgs e)
		{
			NextCol();
		}

		private void NextCol()
		{
			px = 1;
			col++;
			if (col > 8)
			{
				col = 1;
				//row++;
			}
			if (col == 3)
			{
				row++;
			}
			dir = DirName(col);
			face = FaceName(col);
			txtCol.Text = col.ToString();
			txtRow.Text = row.ToString();
			txtPx.Text = px.ToString();
			txtDir.Text = dir;
			txtFace.Text = face;

			pixelName = MakePixelName();
			lblPixelName.Text = pixelName;
			HighlightCol3(col);

		}

		private void IncrementDMXchannel(int direction)
		{
			chNum += direction;
			if (chNum < 1)
			{
				chNum += 510;
				univ -= 1;
			}
			if (chNum > 510)
			{
				chNum -= 510;
				univ += 1;
			}

		}

		private void HighlightCol3(int column)
		{
			if (column == 3)
			{
				lblRow.ForeColor = SystemColors.Highlight;
				lblCol.ForeColor = SystemColors.Highlight;
				txtRow.ForeColor = SystemColors.Highlight;
				txtCol.ForeColor = SystemColors.Highlight;
			}
			if (column == 4)
			{
				lblRow.ForeColor = SystemColors.ControlText;
				lblCol.ForeColor = SystemColors.ControlText;
				txtRow.ForeColor = SystemColors.ControlText;
				txtCol.ForeColor = SystemColors.ControlText;
			}
		}


		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void SaveFile()
		{ 
			string newName = Path.GetDirectoryName(lastFile) + "\\";
			newName += Path.GetFileNameWithoutExtension(lastFile);
			newName += " + Tree Pixels 6th Try";
			newName += Path.GetExtension(lastFile);
			seq.WriteSequenceFile(newName);
		}

		private void btnAuto_Click(object sender, EventArgs e)
		{
			btnAuto.BackColor = System.Drawing.Color.Yellow;
			btnAuto.Refresh();
			for (int q = 0; q < pxps.Length; q++)
			{
				for (int p = 0; p < pxps[q]; p++)
				{
					NextPixel();
				}

				NextCol();
			}
			SaveFile();
			btnAuto.BackColor = System.Drawing.Color.Lime;
			

		}
	} // end public partial class lorForm : Form
} // end namespace TreeORama