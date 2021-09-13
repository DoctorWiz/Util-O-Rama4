using LORUtils4; using FileHelper;
using System;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using LORUtils4; using FileHelper;

namespace StripORama
{
	public partial class frmStripMaker : Form
	{
		private string lastFile = "";
		private string[] channels;
		private int[] unitIDs;
		private int[] unitChs;
		private int chCount = 0;
		private int changesMade = 0;
		private LORSequence4 seq;

		private string stripName = "";
		private int stripNum = 1;
		private int stripCount = 150;
		private int stripStart = 1;
		private int stripEnd = 150;
		private int chOrder = 1;
		private int groupSize = 25;
		private int groupCount = 0;
		private int groupNumber = 1;
		private int chIncr = 1;
		private int universeNum;
		private int pixelNum = 1;
		private bool reversed = false;
		private int pixNumFirst = 1;
		private int pixNumLast = 20;
		private int stripNumFirst = 170;
		private int stripNumLast = 1;
		private int dmxChFirst = 1;
		private int dmxChLast = -1;

		private bool eaveNaming = true;		//  for MY use only

		


		public frmStripMaker()
		{
			InitializeComponent();
		}

		private void lorForm_Load(object sender, EventArgs e)
		{
			initForm();
		}

		private void initForm()
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

			loadFormSettings();
		} // end private void InitForm()

		private void EnableControls(bool newState)
		{
			btnMake.Enabled = newState;
			fraStrip1.Enabled = newState;
			fraStrip2.Enabled = newState;
			fraStrip3.Enabled = newState;
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
				openFileDialog.InitialDirectory = lutils.DefaultSequencesPath;
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
			saveFormSettings();
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

			System.Drawing.Point savedLoc = Properties.Settings.Default.Location;
			System.Drawing.Size savedSize = Properties.Settings.Default.Size;
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;
			System.Drawing.Point center = new System.Drawing.Point(x + w / w, y + h / 2); // Find center point
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

		private void saveFormSettings()
		{
			byte colorOrder;

			Properties.Settings.Default.FormLeft = this.Left;
			Properties.Settings.Default.FormTop = this.Top;

			Properties.Settings.Default.LORTrack4 = Int16.Parse(txtTrack.Text);

			Properties.Settings.Default.Make1 = chkMake1.Checked;
			Properties.Settings.Default.Universe1 = Convert.ToInt16(numUniverse1.Value);
			Properties.Settings.Default.StripName1 = txtStripName1.Text;
			Properties.Settings.Default.GroupSize1 = Int16.Parse(cboGroupSize1.Text);
			Properties.Settings.Default.StartCh1 = Int16.Parse(txtStartCh1.Text);
			Properties.Settings.Default.KeywdelCt1 = Int16.Parse(txtKeywdelCt1.Text);
			colorOrder = 0;
			if (optRGB1.Checked) colorOrder = 1;
			if (optGRB1.Checked) colorOrder = 2;
			Properties.Settings.Default.ColorOrder1 = colorOrder;
			Properties.Settings.Default.Reverse1 = optReverse1.Checked;

			Properties.Settings.Default.Make2 = chkMake2.Checked;
			Properties.Settings.Default.Universe2 = Convert.ToInt16(numUniverse2.Value);
			Properties.Settings.Default.StripName2 = txtStripName2.Text;
			Properties.Settings.Default.GroupSize2 = Int16.Parse(cboGroupSize2.Text);
			Properties.Settings.Default.StartCh2 = Int16.Parse(txtStartCh2.Text);
			Properties.Settings.Default.KeywdelCt2 = Int16.Parse(txtKeywdelCt2.Text);
			colorOrder = 0;
			if (optRGB2.Checked) colorOrder = 1;
			if (optGRB2.Checked) colorOrder = 2;
			Properties.Settings.Default.ColorOrder2 = colorOrder;
			Properties.Settings.Default.Reverse2 = optReverse2.Checked;

			Properties.Settings.Default.Make3 = chkMake3.Checked;
			Properties.Settings.Default.Universe3 = Convert.ToInt16(numUniverse3.Value);
			Properties.Settings.Default.StripName3 = txtStripName3.Text;
			Properties.Settings.Default.GroupSize3 = Int16.Parse(cboGroupSize3.Text);
			Properties.Settings.Default.StartCh3 = Int16.Parse(txtStartCh3.Text);
			Properties.Settings.Default.KeywdelCt3 = Int16.Parse(txtKeywdelCt3.Text);
			colorOrder = 0;
			if (optRGB3.Checked) colorOrder = 1;
			if (optGRB3.Checked) colorOrder = 2;
			Properties.Settings.Default.ColorOrder3 = colorOrder;
			Properties.Settings.Default.Reverse3 = optReverse3.Checked;

			Properties.Settings.Default.Make4 = chkMake4.Checked;
			Properties.Settings.Default.Universe4 = Convert.ToInt16(numUniverse4.Value);
			Properties.Settings.Default.StripName4 = txtStripName4.Text;
			Properties.Settings.Default.GroupSize4 = Int16.Parse(cboGroupSize4.Text);
			Properties.Settings.Default.StartCh4 = Int16.Parse(txtStartCh4.Text);
			Properties.Settings.Default.KeywdelCt4 = Int16.Parse(txtKeywdelCt4.Text);
			colorOrder = 0;
			if (optRGB4.Checked) colorOrder = 1;
			if (optGRB4.Checked) colorOrder = 2;
			Properties.Settings.Default.ColorOrder4 = colorOrder;
			Properties.Settings.Default.Reverse4 = optReverse4.Checked;

			Properties.Settings.Default.lastFile = txtFile.Text;

			Properties.Settings.Default.Save();
		}

		private void loadFormSettings()
		{
			int colorOrder;

			this.Left = Properties.Settings.Default.FormLeft;
			this.Top = Properties.Settings.Default.FormTop;

			txtTrack.Text = Properties.Settings.Default.LORTrack4.ToString();

			chkMake1.Checked = Properties.Settings.Default.Make1;
			numUniverse1.Value = Properties.Settings.Default.Universe1;
			txtStripName1.Text = Properties.Settings.Default.StripName1;
			SetGroupSize(cboGroupSize1, Properties.Settings.Default.GroupSize1);
			txtStartCh1.Text = Properties.Settings.Default.StartCh1.ToString();
			txtKeywdelCt1.Text = Properties.Settings.Default.KeywdelCt1.ToString();
			colorOrder = Properties.Settings.Default.ColorOrder1;
			if (colorOrder == 1) optRGB1.Checked = true;
			if (colorOrder == 2) optGRB1.Checked = true;
			optReverse1.Checked = Properties.Settings.Default.Reverse1;
			optForward1.Checked = !optReverse1.Checked;

			chkMake2.Checked = Properties.Settings.Default.Make2;
			numUniverse2.Value = Properties.Settings.Default.Universe2;
			txtStripName2.Text = Properties.Settings.Default.StripName2;
			SetGroupSize(cboGroupSize2, Properties.Settings.Default.GroupSize2);
			txtStartCh2.Text = Properties.Settings.Default.StartCh2.ToString();
			txtKeywdelCt2.Text = Properties.Settings.Default.KeywdelCt2.ToString();
			colorOrder = Properties.Settings.Default.ColorOrder2;
			if (colorOrder == 1) optRGB2.Checked = true;
			if (colorOrder == 2) optGRB2.Checked = true;
			optReverse2.Checked = Properties.Settings.Default.Reverse2;
			optForward2.Checked = !optReverse2.Checked;

			chkMake3.Checked = Properties.Settings.Default.Make3;
			numUniverse3.Value = Properties.Settings.Default.Universe3;
			txtStripName3.Text = Properties.Settings.Default.StripName3;
			SetGroupSize(cboGroupSize3, Properties.Settings.Default.GroupSize3);
			txtStartCh3.Text = Properties.Settings.Default.StartCh3.ToString();
			txtKeywdelCt3.Text = Properties.Settings.Default.KeywdelCt3.ToString();
			colorOrder = Properties.Settings.Default.ColorOrder3;
			if (colorOrder == 1) optRGB3.Checked = true;
			if (colorOrder == 2) optGRB3.Checked = true;
			optReverse3.Checked = Properties.Settings.Default.Reverse3;
			optForward3.Checked = !optReverse3.Checked;

			chkMake4.Checked = Properties.Settings.Default.Make4;
			numUniverse4.Value = Properties.Settings.Default.Universe4;
			txtStripName4.Text = Properties.Settings.Default.StripName4;
			SetGroupSize(cboGroupSize4, Properties.Settings.Default.GroupSize4);
			txtStartCh4.Text = Properties.Settings.Default.StartCh4.ToString();
			txtKeywdelCt4.Text = Properties.Settings.Default.KeywdelCt4.ToString();
			colorOrder = Properties.Settings.Default.ColorOrder4;
			if (colorOrder == 1) optRGB4.Checked = true;
			if (colorOrder == 2) optGRB4.Checked = true;
			optReverse4.Checked = Properties.Settings.Default.Reverse4;
			optForward4.Checked = !optReverse4.Checked;
		}

		private void SetGroupSize(ComboBox GrpSizeCbo, int size)
		{
			GrpSizeCbo.SelectedIndex = 0;
			for (int q = 0; q < GrpSizeCbo.Items.Count; q++)
			{
				if (Convert.ToInt16(GrpSizeCbo.Items[q]) == size)
				{
					GrpSizeCbo.SelectedIndex = q;
					q = GrpSizeCbo.Items.Count; // Break out of loop
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
				savedIndex = lutils.getKeyValue(lineIn, "SavedIndex");
				if (savedIndex > biggestIndex) biggestIndex = savedIndex;
			}
			reader.Close();
		}

		private void EnableAll(bool newState)
		{
			fraStrip1.Enabled = newState;
			fraStrip2.Enabled = newState;
			fraStrip3.Enabled = newState;
			fraStrip4.Enabled = newState;
			btnMake.Enabled = newState;
			txtFile.Enabled = newState;
			btnBrowse.Enabled = newState;
		}

		private void btnMake_Click(object sender, EventArgs e)
		{
			EnableAll(false);
			pixelNum = 1;
			groupCount = 0;
			groupNumber = 1;

			seq = new LORSequence4();
			if (File.Exists(lastFile))
			{
				seq.ReadSequenceFile(lastFile);
			}





			// Strip 1
			if (chkMake1.Checked)
			{
				stripName = txtStripName1.Text;
				universeNum = Convert.ToInt16(numUniverse1.Value);
				stripCount = Int16.Parse(txtKeywdelCt1.Text);
				stripNum = 1;
				pixelNum = 1;
				reversed = optReverse1.Checked;
				if (reversed)
				{
					stripStart = Int16.Parse(txtKeywdelCt1.Text);
					stripEnd = Int16.Parse(txtStartCh1.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh1.Text);
					stripEnd = Int16.Parse(txtKeywdelCt1.Text);
					chIncr = 1;
				}
				if (optRGB1.Checked) chOrder = 1;
				if (optGRB1.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize1.Text);

				pixNumFirst = 1;
				pixNumLast = groupSize;
				stripNumFirst = stripStart;
				stripNumLast = stripStart + (groupSize * chIncr);
				dmxChFirst = Math.Min(stripNumFirst, stripNumLast) * 3 + 1;
				dmxChLast = Math.Max(stripNumFirst, stripNumLast) * 3;

				AddStrip();
			}
			
			// Strip 2
			if (chkMake2.Checked)
			{
				stripName = txtStripName2.Text;
				universeNum = Convert.ToInt16(numUniverse2.Value);
				stripCount = Int16.Parse(txtKeywdelCt2.Text);
				stripNum = 2;
				reversed = optReverse2.Checked;
				if (reversed)
				{
					stripStart = Int16.Parse(txtKeywdelCt2.Text);
					stripEnd = Int16.Parse(txtStartCh2.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh2.Text);
					stripEnd = Int16.Parse(txtKeywdelCt2.Text);
					chIncr = 1;
				}
				if (optRGB2.Checked) chOrder = 1;
				if (optGRB2.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize2.Text);

				pixNumFirst = Int16.Parse(txtKeywdelCt1.Text) + 1;
				int gc = ((pixelNum / Int16.Parse(cboGroupSize1.Text)) + 1) * groupSize;
				pixNumLast = 180; // gc; // Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text);
				stripNumFirst = 1;
				stripNumLast = 10;
				dmxChFirst = Int16.Parse(txtStartCh2.Text);
				dmxChLast = 30; // groupSize * 3;

				AddStrip();
			}

			// Strip 3
			if (chkMake3.Checked)
			{
				stripName = txtStripName3.Text;
				universeNum = Convert.ToInt16(numUniverse3.Value);
				stripCount = Int16.Parse(txtKeywdelCt3.Text);
				stripNum = 3;
				reversed = optReverse3.Checked;
				if (reversed)
				{
					stripStart = Int16.Parse(txtKeywdelCt3.Text);
					stripEnd = Int16.Parse(txtStartCh3.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh3.Text);
					stripEnd = Int16.Parse(txtKeywdelCt3.Text);
					chIncr = 1;
				}
				if (optRGB3.Checked) chOrder = 1;
				if (optGRB3.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize3.Text);

				pixNumFirst = Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + 1;
				pixNumLast = 360; // Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + Int16.Parse(txtKeywdelCt3.Text);
				stripNumFirst = 170; // Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + stripStart;
				stripNumLast = 151; // stripNumFirst + (groupSize * chIncr);
				dmxChFirst = 451; // Int16.Parse(txtStartCh3.Text);
				dmxChLast = 510; //  (Int16.Parse(txtKeywdelCt3.Text) * 3) - 1;

				AddStrip();
			}

			// Strip 4
			if (chkMake4.Checked)
			{
				stripName = txtStripName4.Text;
				universeNum = Convert.ToInt16(numUniverse4.Value);
				stripCount = Int16.Parse(txtKeywdelCt4.Text);
				stripNum = 4;
				reversed = optReverse4.Checked;
				if (reversed)
				{
					stripStart = Int16.Parse(txtKeywdelCt4.Text);
					stripEnd = Int16.Parse(txtStartCh4.Text);
					chIncr = -1;
				}
				else
				{
					stripStart = Int16.Parse(txtStartCh4.Text);
					stripEnd = Int16.Parse(txtKeywdelCt4.Text);
					chIncr = 1;
				}
				if (optRGB4.Checked) chOrder = 1;
				if (optGRB4.Checked) chOrder = 2;
				groupSize = Int16.Parse(cboGroupSize4.Text);

				pixNumFirst = Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + Int16.Parse(txtKeywdelCt3.Text) + 1;
				pixNumLast = 520; // Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + Int16.Parse(txtKeywdelCt3.Text) + Int16.Parse(txtKeywdelCt4.Text);
				stripNumFirst = 1; // Int16.Parse(txtKeywdelCt1.Text) + Int16.Parse(txtKeywdelCt2.Text) + Int16.Parse(txtKeywdelCt3.Text) + stripStart;
				stripNumLast = 10; // stripNumFirst + (groupSize * chIncr);
				dmxChFirst = 1; // Int16.Parse(txtStartCh4.Text);
				dmxChLast = 30; // (Int16.Parse(txtKeywdelCt4.Text) * 3) - 1;

				AddStrip();
			}

			FileInfo oFilenfo = new FileInfo(lastFile);
			//string newFile = oFilenfo.Directory + "\\" + JustName(lastFile) + " + Strips" + oFilenfo.Extension;
			string newFile = oFilenfo.Directory + "\\" + Path.GetFileNameWithoutExtension(lastFile) + " + Strips" + oFilenfo.Extension;

			seq.WriteSequenceFile(newFile);
			//seq.WriteFileInDisplayOrder(newFile);

			EnableAll(true);
			SystemSounds.Exclamation.Play();

		} // end btnMake_Click()

		public void AddStrip()
		{
			int pixID = stripStart;
			int nextSI = seq.Members.HighestSavedIndex;
			int groupMember = 1;
			int uch = 1;
			if (chIncr < 0) uch = stripCount * 3 - 2;
			int trkNum = Int16.Parse(txtTrack.Text) - 1;
			string chName;
			LORChannel4 redChannel = new LORChannel4("(R)");  // Just placeholders
			LORChannel4 grnChannel = new LORChannel4("(G)");
			LORChannel4 bluChannel = new LORChannel4("(B)");
			//LORRGBChannel4 RGB_Channel = new LORRGBChannel4("RGB");
			//LORChannelGroup4 pixelGroup = new LORChannelGroup4();
			int chx;
			//groupCount = 0;
			int stripKeywdel = stripStart;
			int RGBFirstDMXchannel = 1;
			int RGBLastDMXchannel = -1;
			//int pixNumFirst = pixelNum;
			//int pixNumLast = pixNumFirst + groupSize - 1;
			//int stripNumFirst = stripStart;
			//int stripNumLast = 1;
			//int dmxChFirst = 1;
			//int dmxChLast = -1;


			/////////////////////////////////
			// MAKE A GROUP FOR THE STRIP //
			///////////////////////////////
			int dmxCount = stripCount * 3;
			if (reversed)
			{
				//Reverse
				int bb = stripEnd * 3 - 2;
				//chName = stripName + " Keywdels " + stripStart.ToString("000") + "-" + stripEnd.ToString("000") + " (U" + universeNum.ToString() + "." + bb.ToString("000") + "-" + dmxCount.ToString("000") + ")";
				int sst = pixelNum;
				int est = pixelNum + Math.Max(stripStart, stripEnd);
				chName = stripName + " Keywdels " + sst.ToString("000") + "-" + est.ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripStart.ToString("000") + "-" + stripEnd.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + bb.ToString("000") + "-" + dmxCount.ToString("000") + ")";
			}
			else
			{
				// Forward
				//chName = stripName + " Keywdels " + stripStart.ToString("000") + "-" + stripCount.ToString("000") + " (U" + universeNum.ToString() + "." + stripStart.ToString("000") + "-" + dmxCount.ToString("000") + ")";
				chName = stripName + " Keywdels " + pixNumFirst.ToString("000") + "-" + pixNumLast.ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripStart.ToString("000") + "-" + stripEnd.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + stripStart.ToString("000") + "-" + dmxCount.ToString("000") + ")";
			}
			LORChannelGroup4 stripGroup = seq.CreateChannelGroup(chName);

			///////////////////////////////////////////////////////////
			// MAKE AN INITIAL CHANNEL GROUP FOR THE PIXEL GROUPING //
			/////////////////////////////////////////////////////////
			if (eaveNaming)
			{
				string gn = " Group ";
				if (groupNumber < 18)
				{
					//gn += (18 - groupNumber).ToString("00") + "L";
					gn += (groupNumber).ToString("00") + "L";
				}
				else
				{
					//gn += (groupNumber - 17).ToString("00") + "R";
					gn += (35 - groupNumber).ToString("00") + "R";
				}
				if (groupCount > 0)
				{
					gn += "b";
				}
				chName = stripName + gn;

			}
			else
			{
				//chName = stripName + " Keywdels " + stripNumFirst.ToString("000") + "-" + stripKeywdel.ToString("000");
				chName = stripName;
				//chName = " Keywdels " + (pixelNum - groupSize).ToString("000") + "-" + pixelNum.ToString("000") + " / S" + stripNum.ToString() + "." + stripNumFirst.ToString("000") + "-" + stripKeywdel.ToString("000") + " / U" + universeNum.ToString() + "." + dmxChFirst.ToString("000") + "-" + dmxChLast.ToString("000");
			}
			/*
			if (reversed)
			{
				stripNumFirst = stripStart;
				stripNumLast = stripStart - groupSize + 1;
				dmxChFirst = dmxCount - groupSize * 3 + 1;
				dmxChLast = dmxCount;
			}
			else
			{
				stripNumFirst = stripStart;
				stripNumLast = stripStart + groupSize - 1;
				dmxChFirst = 1;
				dmxChLast = groupSize * 3 - 1;
			}
			*/
			chName += " Keywdels " + pixNumFirst.ToString("000") + "-" + pixNumLast.ToString("000");
			chName += " / S" + stripNum.ToString() + "." + stripNumFirst.ToString("000") + "-" + stripNumLast.ToString("000");
			chName += " / U" + universeNum.ToString() + "." + dmxChFirst.ToString("000") + "-" + dmxChLast.ToString("000") + ")";
			LORChannelGroup4 pixelGroup = seq.CreateChannelGroup(chName);
			stripGroup.AddItem(pixelGroup);


//			int gc = 
			pixNumFirst = (groupCount + 1) * groupSize + 1;
			pixNumLast = (groupCount + 2) * groupSize;
			stripNumFirst += (groupSize * chIncr);
			stripNumLast += (groupSize * chIncr) + 1;
			dmxChFirst += (groupSize * 3 * chIncr);
			dmxChLast += (groupSize * 3 * chIncr);
			
			if (stripNum == 2 )
			{
				pixNumFirst = 181;
				pixNumLast = 200;
				stripNumFirst = 11;
				stripNumLast = 30;
				dmxChFirst = 31;
				dmxChLast = 90;
			}
			if (stripNum == 3)
			{
				pixNumFirst = 361;
				pixNumLast = 380;
				stripNumFirst = 150;
				stripNumLast = 131;
				dmxChFirst = 391;
				dmxChLast = 450;
			}
			if (stripNum == 4)
			{
				pixNumFirst = 521;
				pixNumLast = 540;
				stripNumFirst = 11;
				stripNumLast = 30;
				dmxChFirst = 31;
				dmxChLast = 90;
			}



			string prfx = "";
			if (eaveNaming)
			{
				prfx = "Eave ";
			}

			//////////////////////
			// MAKE THE STRIP! //
			////////////////////
			while ((stripKeywdel > 0) && (stripKeywdel <= stripCount))
			{
				if (chOrder == 1) // RGB Order
				{
					//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + "(R) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (R) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					redChannel = seq.CreateChannel(chName);
					redChannel.output.circuit = uch;
					RGBFirstDMXchannel = uch;
					uch++;

					//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + "(G) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (G) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					grnChannel = seq.CreateChannel(chName);
					grnChannel.output.circuit = uch;
					uch++;

					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (B) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					bluChannel = seq.CreateChannel(chName);
					bluChannel.output.circuit = uch;
					RGBLastDMXchannel = uch;
					uch++;
				}

				if (chOrder == 2) // GRB Order
				{
					//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + "(G) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (G) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					grnChannel = seq.CreateChannel(chName);
					grnChannel.output.circuit = uch;
					RGBFirstDMXchannel = uch;
					uch++;

					//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + "(R) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (R) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					redChannel = seq.CreateChannel(chName);
					redChannel.output.circuit = uch;
					uch++;

					//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + "(B) (U" + universeNum.ToString() + "." + uch.ToString("000") + ")";
					chName = prfx + "Keywdel " + pixelNum.ToString("000");
					chName += " (B) / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
					chName += " / U" + universeNum.ToString() + "." + uch.ToString("000");
					bluChannel = seq.CreateChannel(chName);
					bluChannel.output.circuit = uch;
					RGBLastDMXchannel = uch;
					uch++;
				}
				redChannel.output.deviceType = LORDeviceType4.DMX;
				redChannel.output.network = universeNum;
				redChannel.color = lutils.LORCOLOR_RED;
				grnChannel.output.deviceType = LORDeviceType4.DMX;
				grnChannel.output.network = universeNum;
				grnChannel.color = lutils.LORCOLOR_GRN;
				bluChannel.output.deviceType = LORDeviceType4.DMX;
				bluChannel.output.network = universeNum;
				bluChannel.color = lutils.LORCOLOR_BLU;


				//dmxChLast = uch - 1;
				chx = uch - 2;
				//chName = stripName + " Keywdel " + stripKeywdel.ToString("000") + " (U" + universeNum.ToString() + "." + RGBFirstDMXchannel.ToString("000") + "-" + RGBLastDMXchannel.ToString("000") + ")";
				chName = prfx + "Keywdel " + pixelNum.ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripKeywdel.ToString("000");
				chName += " / U" + universeNum.ToString() + "." + RGBFirstDMXchannel.ToString("000") + "-" + RGBLastDMXchannel.ToString("000");
				LORRGBChannel4 RGB_Channel = seq.CreateRGBchannel(chName);
				RGB_Channel.redChannel = redChannel;
				RGB_Channel.grnChannel = grnChannel;
				RGB_Channel.bluChannel = bluChannel;
				pixelNum++;

				pixelGroup.AddItem(RGB_Channel);
				groupCount++;

				//////////////////////////////////
				// IS THE GROUP FULL?          //
				// iF SO, CREATE THE NEXT ONE //
				///////////////////////////////
				if (stripKeywdel < stripCount)
				{
					if (groupCount >= groupSize)
					{
						//stripNumLast = stripNumFirst + groupSize;
						//int uchStart = stripKeywdel * 3 - 2;
						if (chIncr < 0)
						{
							// Reverse
							//dmxChFirst = stripKeywdel * 3 - 2;
							//dmxChLast = stripNumFirst * 3;
						}
						else
						{
							// Forward
							// OK as-is
						}

						groupNumber++;
						//dmxChFirst = uch;
						//stripNumFirst = stripKeywdel + chIncr;
						if (eaveNaming)
						{
							string gn = " Group ";
							if (groupNumber < 18)
							{
								//gn += (18 - groupNumber).ToString("00") + "L";
								gn += (groupNumber).ToString("00") + "L";
							}
							else
							{
								//gn += (groupNumber - 17).ToString("00") + "R";
								gn += (35 - groupNumber).ToString("00") + "R";
							}
							chName = stripName + gn;
						}
						else
						{
							chName = stripName;
						}
						chName += " Keywdels " + pixNumFirst.ToString("000") + "-" + pixNumLast.ToString("000");
						chName += " / S" + stripNum.ToString() + "." + stripNumFirst.ToString("000") + "-" + stripNumLast.ToString("000");
						chName += " / U" + universeNum.ToString() + "." + dmxChFirst.ToString("000") + "-" + dmxChLast.ToString("000") + ")";
						pixelGroup = seq.CreateChannelGroup(chName);
						stripGroup.AddItem(pixelGroup);

						pixNumFirst = pixNumLast + 1;
						pixNumLast += groupSize;
						dmxChFirst += (groupSize * 3 * chIncr);
						dmxChLast += (groupSize * 3 * chIncr);
						stripNumFirst += (groupSize * chIncr);
						stripNumLast += (groupSize * chIncr);

						//pixelGroup = new LORChannelGroup4();
						groupCount = 0; // Reset

					}
				}
				else
				{
				}

				stripKeywdel += chIncr;
				if (chIncr < 0) uch -= 6;


				/////////////////////////////////
			} // end while pixel # in range //
				///////////////////////////////

			// Is there any leftover pixels?
			// (Happens when strip size is not evenly divisible by group size)
			if (groupCount > 0)
			{
				if (eaveNaming)
				{
					string gn = " Group ";
					if (groupNumber < 18)
					{
						//gn += (18 - groupNumber).ToString("00") + "aL";
						gn += (groupNumber).ToString("00") + "aL";
					}
					else
					{
						//gn += (groupNumber - 17).ToString("00") + "aR";
						gn += (35 - groupNumber).ToString("00") + "aR";
					}
					chName = stripName + gn;

				}
				else
				{
					chName = stripName;
				}
				pixNumFirst -= groupSize;
				pixNumLast = Math.Max(stripStart, stripEnd);
				stripNumFirst -= (groupSize * chIncr);
				stripNumLast = 1; //TODO Fix!
				dmxChFirst -= (groupSize * 3 * chIncr);
				dmxChLast = 1;
				chName += " Keywdels " + pixNumFirst.ToString("000") + "-" + pixNumLast.ToString("000");
				chName += " / S" + stripNum.ToString() + "." + stripNumFirst.ToString("000") + "-" + (stripKeywdel + 1).ToString("000");
				chName += " / U" + universeNum.ToString() + ".001" + "-" + dmxChLast.ToString("000") + ")";

				pixelGroup.ChangeName(chName);
				stripGroup.AddItem(pixelGroup);
				//dmxChFirst = uch;
				//stripNumFirst = stripKeywdel + chIncr;

			}
			if (groupCount >= groupSize)
			{
				groupNumber++;
				groupCount = 0;
			}

			// Add the Strip to the LORTrack4
			seq.Tracks[trkNum].AddItem(stripGroup);

		} // end void AddStrip();

		public string FOO_JustName(string fileName)
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

	} // end public partial class lorForm : Form
} // end namespace StripORama