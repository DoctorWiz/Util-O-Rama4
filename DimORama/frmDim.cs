using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using LOR4;
using FileHelper;

namespace UtilORama4
{
	public partial class frmDim : Form
	{
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/blankorama";

		// Tab index on GUI, do not confuse with Rule numbers below
		private const int TAB_NOCHANGE = 0;
		private const int TAB_DIM = 1;
		private const int TAB_TRIM = 2;
		private const int TAB_ONOFF = 3;
		private const int TAB_MINTIME = 4;
		// Rule number (not the same as the GUI tab number above)
		private const int RULE_NOCHANGE = 0;
		private const int RULE_DIM = 1;
		private const int RULE_TRIM = 2;
		private const int RULE_ONOFF = 3;
		private const int RULE_MINTIME = 4;
		private const int RULES_DIM_MINTIME = RULE_DIM + RULE_MINTIME;
		private const int RULES_TRIM_MINTIME = RULE_TRIM + RULE_MINTIME;
		private const int RULES_ONOFF_MINTIME = RULE_ONOFF + RULE_MINTIME;
		// For groups and tracks only, indicates child members are in various states
		private const int RULES_MIXED = 128;

		// Convert Tab Index to Rule   0          1          2           3             4
		private int[] TabRules = { RULE_NOCHANGE, RULE_DIM, RULE_TRIM, RULE_ONOFF, RULE_MINTIME };

		// Colors for the various possible rules
		public static readonly Color COLOR_NOCHANGE = Color.Black;
		public static readonly Color COLOR_DIM = Color.Red;
		public static readonly Color COLOR_TRIM = Color.Lime;
		public static readonly Color COLOR_ONOFF = Color.Blue;
		public static readonly Color COLOR_MINTIME = Color.FromArgb(50, 50, 50); // Very Dark Gray
		public static readonly Color COLOR_DIM_MINTIME = Color.DarkRed;
		public static readonly Color COLOR_TRIM_MINTIME = Color.Green;
		public static readonly Color COLOR_ONOFF_MINTIME = Color.DarkBlue;
		public static readonly Color COLOR_INVALID = Color.Orange;
		public static readonly Color COLOR_INDETERMINATE = Color.DarkSlateGray;
		//                                     0/4/8/12              1/5/9/13						 2/6/10/14						 3/7/11/15
		public readonly Color[] ruleColors = {COLOR_NOCHANGE,       COLOR_DIM,          COLOR_TRIM,           COLOR_ONOFF,
																					COLOR_MINTIME,        COLOR_DIM_MINTIME,  COLOR_TRIM_MINTIME,  COLOR_ONOFF_MINTIME };




		private string filenameSource = "";
		private string filenameDest = "";
		private string filenameMap = "";
		private bool firstShown = false;
		public const string EXT_DIMMAP = "DimMap";
		//public const string FILE_DIMMAP = "Channel Dimming Map *.DimMap|*DimMap";
		public const string FILE_DIMMAP = "Channel Dimming Map *." + EXT_DIMMAP + "|*." + EXT_DIMMAP;

		private LOR4Sequence seqSource = null;
		private LOR4Sequence seqDest = null;
		private bool dirtyMap = false;
		private bool dirtySeq = false;
		private int currentTab = 0;


		//! Try to avoid using this!  Now included in the new  .Nodes property of all iLORMembers
		private List<TreeNodeAdv>[] nodesBySI = null; // new List<TreeNodeAdv>();
																									//! If that works out I can rid of this and all the associated code needed to update it


		public frmDim()
		{
			InitializeComponent();
		}

		private void frmDim_Load(object sender, EventArgs e)
		{
			RestoreFormPosition();
			GetTheControlsFromTheHeartOfTheSun();
			TabTextToTop();
		}

		private void TabTextToTop()
		{
			// Reset the order in which the tab pages are added.   
			// See SyncFusion Knowledgebase article 1189
			// https://www.syncfusion.com/kb/1189/what-is-the-reason-for-position-of-the-tab-page-in-the-winforms-tabcontroladv-not-the-same
			tabChannels.Controls.Clear();
			// To add the tab pages in the order 1, 2, 3, 4
			//tabChannels.Controls.AddRange(new System.Windows.Forms.Control[] { tabDim, tabTrim, tabOnOff, tabMinTime, tabNoChange });
			tabChannels.Controls.AddRange(new System.Windows.Forms.Control[] { tabNoChange, tabDim, tabTrim, tabOnOff, tabMinTime });


			int y = tabChannels.Top + 4;
			txtDim.Location = new Point(110, y);
			this.Controls.SetChildIndex(txtDim, 0);
			txtTrim.Location = new Point(172, y);
			this.Controls.SetChildIndex(txtTrim, 1);
			txtTime.Location = new Point(278, y);
			this.Controls.SetChildIndex(txtTime, 2);

		}

		private void frmDim_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
			SetTheControlsForTheHeartOfTheSun();
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
			heartOfTheSun.Location = myLoc;
			heartOfTheSun.Size = mySize;
			heartOfTheSun.WindowState = (int)myState;
			heartOfTheSun.Save();
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

			Point savedLoc = heartOfTheSun.Location;
			Size savedSize = heartOfTheSun.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)heartOfTheSun.WindowState;
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

		private void SetTheControlsForTheHeartOfTheSun()
		{
			Properties.Settings.Default.filenameDest = filenameDest;
			Properties.Settings.Default.filenameSource = filenameSource;
			Properties.Settings.Default.Save();
		}

		private void GetTheControlsFromTheHeartOfTheSun()
		{
			filenameSource = Properties.Settings.Default.filenameSource;
			filenameDest = Properties.Settings.Default.filenameDest;

		}

		public void ImBusy(bool busy)
		{
			if (busy)
			{
				this.Cursor = Cursors.WaitCursor;
				this.Enabled = false;
			}
			else
			{
				this.Cursor = Cursors.Default;
				this.Enabled = true;
			}
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout();
			//aboutBox.SetIcon = picAboutIcon.Image;
			aboutBox.picIcon.Image = picAboutIcon.Image;
			aboutBox.Icon = this.Icon;
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private void frmDim_Resize(object sender, EventArgs e)
		{
			tabChannels.Width = ClientRectangle.Width - 20;
			treeSource.Width = tabChannels.Width - 20;
			picPreviewSource.Width = tabChannels.Width - 20;

			// Reserve 80 pixels at the bottom for the destination file
			int mh = txtFilenameMap.Top + txtFilenameMap.Height + txtFilenameDest.Height + 80;
			tabChannels.Height = ClientRectangle.Height - mh;
			int th = tabChannels.Height - picPreviewSource.Height - 62;
			treeSource.Height = th;
			picPreviewSource.Top = treeSource.Top + treeSource.Height + 6;
			lblFilenameDest.Top = tabChannels.Top + tabChannels.Height + 5;
			txtFilenameDest.Top = lblFilenameDest.Top + lblFilenameDest.Height + 2;
			chkLaunch.Top = txtFilenameDest.Top + txtFilenameDest.Height + 2;
			btnSaveSeq.Top = txtFilenameDest.Top;

		}

		private void tabChannels_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private int ChangeTab(int newTab)
		{
			int lastTab = currentTab;
			if (newTab != currentTab)
			{
				currentTab = newTab;
				tabChannels.SelectedIndex = newTab;
				tabChannels.SelectedTab.Controls.Add(lblTabFunction);
				tabChannels.SelectedTab.Controls.Add(treeSource);
				tabChannels.SelectedTab.Controls.Add(picPreviewSource);
				switch (newTab)
				{
					case TAB_DIM:
						lblTabFunction.Text = "These channels will be scaled to a maximum of " + txtDim.Text + "% intensity";
						break;
					case TAB_TRIM:
						lblTabFunction.Text = "These channels will be truncated to a maximum of " + txtTrim.Text + "% intensity";
						break;
					case TAB_ONOFF:
						lblTabFunction.Text = "These channels must be fully on, or fully off";
						break;
					case TAB_MINTIME:
						lblTabFunction.Text = "These channels must stay on or off for a minimum of " + txtTime.Text + " seconds";
						break;
					case TAB_NOCHANGE:
						lblTabFunction.Text = "These channels will be left as-is and not modified";
						break;
				}
				UpdateChannelTree(newTab);
			}
			return lastTab;
		}

		private void UpdateChannelTree(int selectedTab)
		{
			UpdateTreeFormatting(TabRules[selectedTab]);

		}

		private void txtDim1_Enter(object sender, EventArgs e)
		{
			tabChannels.SelectedTab = tabDim;
		}

		private void txtDim2_Enter(object sender, EventArgs e)
		{
			tabChannels.SelectedTab = tabTrim;
		}

		private void txtTime_Enter(object sender, EventArgs e)
		{
			tabChannels.SelectedTab = tabMinTime;
		}



		#region File Operations
		private void BrowseForSourceSequence()
		{
			string initDir = LOR4Admin.DefaultSequencesPath;
			string initFile = "";
			if (filenameSource.Length > 4)
			{
				string ldir = Path.GetDirectoryName(filenameSource);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(filenameSource))
					{
						initFile = Path.GetFileName(filenameSource);
					}
				}
			}


			dlgFileOpen.Filter = LOR4Admin.FILT_OPEN_ANY;
			dlgFileOpen.DefaultExt = LOR4Admin.EXT_LMS;
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.CheckFileExists = true;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Multiselect = false;
			dlgFileOpen.Title = "Open Sequence...";
			DialogResult result = dlgFileOpen.ShowDialog();

			if (result == DialogResult.OK)
			{
				ImBusy(true);
				int err = LoadSourceSequence(dlgFileOpen.FileName);
				ImBusy(false);
			} // end if (result = DialogResult.OK)

		}

		public int LoadSourceSequence(string sourceFilename)
		{
			ImBusy(true);
			string beatsName = "";
			seqSource = new LOR4Sequence();
			int err = seqSource.ReadSequenceFile(sourceFilename);
			if (err < 100)
			{
				filenameSource = sourceFilename;
				txtFilenameSource.Text = Fyle.ShortenLongPath(filenameSource, 120);
				this.Text = "Map-O-Rama - " + Path.GetFileName(filenameSource);
				FileInfo fi = new FileInfo(sourceFilename);
				Properties.Settings.Default.filepathBase = fi.DirectoryName;
				Properties.Settings.Default.filenameSource = filenameSource;
				Properties.Settings.Default.Save();
				TreeUtils.TreeFillChannels(treeSource, seqSource, false, false);
				UpdateTreeFormatting(TabRules[tabChannels.SelectedIndex]);
				//TODO: Re-select current tab, trigging refresh of the tree checkboxes, etc.
			}
			ImBusy(false);
			return err;
		}

		public int SaveNewMappedSequence(string newSeqFileName)
		{
			int err = 0;
			/*
			ImBusy(true);
			LOR4Sequence seqNew = seqMaster;

			seqNew.info = seqSource.info;
			seqNew.info.filename = newSeqFileName;
			seqNew.LOR4SequenceType = seqSource.LOR4SequenceType;
			seqNew.Centiseconds = seqSource.Centiseconds;
			seqNew.animation = seqSource.animation;
			seqNew.videoUsage = seqSource.videoUsage;

			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;

			seqNew.ClearAllEffects();

			//seqNew.TimingGrids = new List<LOR4Timings>();
			foreach (LOR4Timings sourceGrid in seqSource.TimingGrids)
			{
				LOR4Timings newGrid = null;
				for (int gridIndex = 0; gridIndex < seqNew.TimingGrids.Count; gridIndex++)
				{
					if (sourceGrid.Name.ToLower() == seqNew.TimingGrids[gridIndex].Name.ToLower())
					{
						newGrid = seqNew.TimingGrids[gridIndex];
						gridIndex = seqNew.TimingGrids.Count; // Force exit of loop
					}
				}
				if (newGrid == null)
				{
					newGrid = seqNew.CreateNewTimingGrid(sourceGrid.LineOut());
				}
				newGrid.CopyTimings(sourceGrid.timings, false);
			}


			// Copy beats enabled and checked?
			if (chkCopyBeats.Enabled)
			{
				if (chkCopyBeats.Checked)
				{
					// Is it the FIRST track?
					if (beatTracks > 0)
					{
						CopyBeats(seqNew);
					}
				}
			}


			for (int mapLoop = 0; mapLoop < mapMastToSrc.Length; mapLoop++)
			{
				if (mapMastToSrc[mapLoop] != null)
				{
					int newSI = mapLoop;
					iLOR4Member newChild = seqNew.Members.BySavedIndex[newSI];
					if (newChild.MemberType == LOR4MemberType.Channel)
					{
						//int sourceSI = mapMastToSrc[mapLoop];
						//iLOR4Member sourceChildMember = seqSource.Members.BySavedIndex[sourceSI];
						iLOR4Member sourceChildMember = mapMastToSrc[mapLoop];
						if (sourceChildMember.MemberType == LOR4MemberType.Channel)
						{
							LOR4Channel sourceChannel = (LOR4Channel)sourceChildMember;
							if (sourceChannel.effects.Count > 0)
							{
								LOR4Channel newChannel = (LOR4Channel)newChild;
								newChannel.CopyEffects(sourceChannel.effects, false);
								newChannel.Centiseconds = sourceChannel.Centiseconds;
							} // end if effects.Count
						} // end if source obj is channel
					}
					else // newer obj is NOT a channel
					{
						if (newChild.MemberType == LOR4MemberType.RGBChannel)
						{
							//int sourceSI = mapMastToSrc[mapLoop];
							//iLOR4Member sourceChildMember = seqSource.Members.BySavedIndex[sourceSI];
							iLOR4Member sourceChildMember = mapMastToSrc[mapLoop];
							if (sourceChildMember.MemberType == LOR4MemberType.RGBChannel)
							{
								LOR4RGBChannel sourceRGBchannel = (LOR4RGBChannel)sourceChildMember;
								LOR4RGBChannel masterRGBchannel = (LOR4RGBChannel)newChild;
								masterRGBchannel.Centiseconds = sourceRGBchannel.Centiseconds;
								LOR4Channel sourceChannel = sourceRGBchannel.redChannel;
								LOR4Channel newChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.redChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count
								sourceChannel = sourceRGBchannel.grnChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.grnChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count
								sourceChannel = sourceRGBchannel.bluChannel;
								if (sourceChannel.effects.Count > 0)
								{
									newChannel = masterRGBchannel.bluChannel;
									newChannel.CopyEffects(sourceChannel.effects, false);
								} // end if effects.Count

							} // end if source obj is RGB channel
						}
						else // newer obj is NOT an LOR4RGBChannel
						{
							// Channel Group = do nothing (?)
							// LOR4Track = do nothing (?)
							// Timing Grid = do nothing (?)
						} // end if newer obj is an LOR4RGBChannel (or not)
					} // end if newer obj is channel (or not)
				} // end if mapping is undefined
			} // loop thru newer Channels

			// Copy beats enabled and checked?
			if (chkCopyBeats.Enabled)
			{
				if (chkCopyBeats.Checked)
				{
					// Is it the NOT the FIRST track (at the end maybe?)
					if (beatTracks > 0)
					{
						CopyBeats(seqNew);
					}
				}
			}

			foreach (LOR4Channel ch in seqMaster.Channels)
			{
				ch.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LOR4RGBChannel rc in seqMaster.RGBchannels)
			{
				rc.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LOR4ChannelGroup cg in seqMaster.ChannelGroups)
			{
				cg.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LOR4Track tr in seqMaster.Tracks)
			{
				tr.Centiseconds = seqSource.Centiseconds;
			}


			//if (chkCopyBeats.Checked)
			//{
			//	CopyBeats(seqNew);
			//}

			msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;


			seqNew.CentiFix(seqSource.Centiseconds);

			err = seqNew.WriteSequenceFile_DisplayOrder(newSeqFileName);
			seqNew.ClearAllEffects();

			saveFile = newSeqFileName;
			Properties.Settings.Default.LastSaveFile = saveFile;
			Properties.Settings.Default.Save();

			ImBusy(false);
			*/
			return err;
		}

		private int SaveMap(string mapFile)
		{
			int err = 0;
			try
			{
				string lineOut = "";
				StreamWriter writer = new StreamWriter(mapFile);
				// DELIMA = "🗲";
				// DELIMB = "🧙";
				// DELIMC = "👍";
				// DELIMD = "🐕";
				// DELIME = "💡"

				// File Header
				lineOut = LOR4Admin.DELIME + " Util-O-Rama Dim-O-Rama Channel List " + LOR4Admin.DELIME;
				writer.WriteLine(lineOut);
				// Dim Section; Channels and RGB Channels to be dimmed or scaled, and by how much
				lineOut = LOR4Admin.DELIMC + "Dim" + LOR4Admin.DELIMC + txtDim.Text;
				writer.WriteLine(lineOut);
				// Channels to be dimmed or scaled, Saved Index and Name
				lineOut = LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.Channels.Count; c++)
				{
					int q = seqSource.Channels[c].ZCount & RULE_DIM;
					if (q == RULE_DIM)
					{
						lineOut = seqSource.Channels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.Channels[c].Name;
						writer.WriteLine(lineOut);
					}
				}
				// RGB Channels to be dimmed or scaled, Saved Index and Name
				lineOut = LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.RGBchannels.Count; c++)
				{
					int q = seqSource.RGBchannels[c].ZCount & RULE_DIM;
					if (q == RULE_DIM)
					{
						lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.RGBchannels[c].Name;
						writer.WriteLine(lineOut);
					}
				}

				// Trim Section; Channels and RGB Channels to be trimmed or truncated, and by how much
				lineOut = LOR4Admin.DELIMC + "Trim" + LOR4Admin.DELIMC + txtTrim.Text;
				writer.WriteLine(lineOut);
				// Channels to be trimmed or truncated, Saved Index and Name
				lineOut = LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.Channels.Count; c++)
				{
					int q = seqSource.Channels[c].ZCount & RULE_TRIM;
					if (q == RULE_TRIM)
					{
						lineOut = seqSource.Channels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.Channels[c].Name;
						writer.WriteLine(lineOut);
					}
				}
				// RGB Channels to be trimmed or truncated, Saved Index and Name
				lineOut = LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.RGBchannels.Count; c++)
				{
					int q = seqSource.RGBchannels[c].ZCount & RULE_TRIM;
					if (q == RULE_TRIM)
					{
						lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.RGBchannels[c].Name;
						writer.WriteLine(lineOut);
					}
				}

				// On-Off Section; Channels and RGB Channels to set strictly On or Off
				lineOut = LOR4Admin.DELIMC + "OnOff" + LOR4Admin.DELIMC;
				writer.WriteLine(lineOut);
				// Channels to be set on or off, Saved Index and Name
				lineOut = LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.Channels.Count; c++)
				{
					int q = seqSource.Channels[c].ZCount & RULE_ONOFF;
					if (q == RULE_ONOFF)
					{
						lineOut = seqSource.Channels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.Channels[c].Name;
						writer.WriteLine(lineOut);
					}
				}
				// RGB Channels to be set strictly on or off
				lineOut = LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.RGBchannels.Count; c++)
				{
					int q = seqSource.RGBchannels[c].ZCount & RULE_ONOFF;
					if (q == RULE_ONOFF)
					{
						lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.RGBchannels[c].Name;
						writer.WriteLine(lineOut);
					}
				}

				// Minimum Time Section; Channels and RGB Channels to be left on or off for a minimum number of seconds, and how many
				lineOut = LOR4Admin.DELIMC + "MinTime" + LOR4Admin.DELIMC + txtTime.Text;
				writer.WriteLine(lineOut);
				// Channels to be be on or off for a minimum time
				lineOut = LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.Channels.Count; c++)
				{
					int q = seqSource.Channels[c].ZCount & RULE_MINTIME;
					if (q == RULE_MINTIME)
					{
						lineOut = seqSource.Channels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.Channels[c].Name;
						writer.WriteLine(lineOut);
					}
				}
				// RGB Channels to be on or off for a minimum time
				lineOut = LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.RGBchannels.Count; c++)
				{
					int q = seqSource.RGBchannels[c].ZCount & RULE_ONOFF;
					if (q == RULE_ONOFF)
					{
						lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.RGBchannels[c].Name;
						writer.WriteLine(lineOut);
					}
				}

				// And finally, just for the benefit of us humans
				// Will be ignored when this file is read back in, written out only for reference and debugging by humans
				// No Change Section; Channels and RGB Channels to be left alone and get no changes
				lineOut = LOR4Admin.DELIMC + "NoChange" + LOR4Admin.DELIMC;
				writer.WriteLine(lineOut);
				// Channels to get no changes and be left alone
				lineOut = LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.Channels.Count; c++)
				{
					if (seqSource.Channels[c].ZCount == RULE_NOCHANGE)
					{
						lineOut = seqSource.Channels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.Channels[c].Name;
						writer.WriteLine(lineOut);
					}
				}
				// RGB Channels to be ignored, left alone, not changed
				lineOut = LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA;
				writer.WriteLine(lineOut);
				for (int c = 0; c < seqSource.RGBchannels.Count; c++)
				{
					if (seqSource.RGBchannels[c].ZCount == RULE_NOCHANGE)
					{
						lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + LOR4Admin.DELIM5 + seqSource.RGBchannels[c].Name;
						writer.WriteLine(lineOut);
					}
				}

				// Again, just for us easily confused humans...
				lineOut = LOR4Admin.DELIME + " (End of File) " + LOR4Admin.DELIME;
				writer.WriteLine(lineOut);

				writer.Close();
				filenameMap = mapFile;
				Properties.Settings.Default.filenameMap = filenameMap;
				Properties.Settings.Default.Save();
			}
			catch (Exception ex)
			{
				string msg = "Error saving map file!\r\n\r\n" + ex.Message;
				Fyle.BUG(msg);
			}
			return err;

		}

		private string BrowseForMap()
		{
			string theFile = "";
			dlgFileOpen.DefaultExt = EXT_DIMMAP;
			dlgFileOpen.Filter = FILE_DIMMAP;
			dlgFileOpen.FilterIndex = 0;
			string initDir = LOR4Admin.DefaultChannelConfigsPath;
			string initFile = "";
			if (filenameMap.Length > 4)
			{
				string pth = Path.GetDirectoryName(filenameMap);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(filenameMap))
				{
					initFile = Path.GetFileName(filenameMap);
				}
			}
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Title = "Load-Apply Channel Map..";

			DialogResult dr = dlgFileOpen.ShowDialog();
			if (dr == DialogResult.OK)
			{
				filenameMap = dlgFileOpen.FileName;
				theFile = filenameMap;
				txtFilenameMap.Text = Path.GetFileName(filenameMap);
				Properties.Settings.Default.filenameMap = filenameMap;
				Properties.Settings.Default.Save();
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.Save();
				if (seqSource.AllMembers.Items.Count > 1)
				{
					if (seqSource.AllMembers.Items.Count > 1)
					{
						LoadApplyMapFile(filenameMap);
					}
				}
				dirtyMap = false;
				btnSaveMap.Enabled = dirtyMap;
			} // end dialog result = OK
			return theFile;
		} // end btnLoadMap Click event

		private int LoadApplyMap(string mapFile)
		{
			int err = 0;
			// first some sanity checks, like does the file exist?
			if (Fyle.Exists(mapFile))
			{
				string lineIn = "";
				StreamReader reader = new StreamReader(mapFile);
				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					// And does it start with the proper header?
					if (lineIn.Substring(0, 1) == LOR4Admin.DELIME)
					{
						int ipos = lineIn.IndexOf("Dim-O-Rama ");
						if (ipos > 0)
						{
							int rule = RULE_NOCHANGE;
							int percent = 0;
							bool rgb = false;
							string[] parts = null;
							int si = LOR4Admin.UNDEFINED;
							string name = "";
							LOR4Channel chan = null;
							LOR4RGBChannel rgbchan = null;
							// Keep reading lines until the end
							while (!reader.EndOfStream)
							{
								lineIn = reader.ReadLine();
								ipos = lineIn.IndexOf(LOR4Admin.DELIMA + "Channels" + LOR4Admin.DELIMA);
								if (ipos == 0)
								{ rgb = false; }
								else
								{
									ipos = lineIn.IndexOf(LOR4Admin.DELIMA + "RGBChannels" + LOR4Admin.DELIMA);
									if (ipos == 0)
									{ rgb = true; }
									else
									{
										ipos = lineIn.IndexOf(LOR4Admin.DELIMC + "Dim" + LOR4Admin.DELIMC);
										if (ipos == 0)
										{
											rule = RULE_DIM;
											string pct = lineIn.Substring(5);
											percent = int.Parse(pct);
											txtDim.Text = percent.ToString();
										}
										else
										{
											ipos = lineIn.IndexOf(LOR4Admin.DELIMC + "Trim" + LOR4Admin.DELIMC);
											if (ipos == 0)
											{
												rule = RULE_TRIM;
												string pct = lineIn.Substring(6);
												percent = int.Parse(pct);
												txtTrim.Text = percent.ToString();
											}
											else
											{
												ipos = lineIn.IndexOf(LOR4Admin.DELIMC + "OnOff" + LOR4Admin.DELIMC);
												if (ipos == 0)
												{
													rule = RULE_ONOFF;
												}
												else
												{
													ipos = lineIn.IndexOf(LOR4Admin.DELIMC + "MinTime" + LOR4Admin.DELIMC);
													if (ipos == 0)
													{
														rule = RULE_DIM;
														string pct = lineIn.Substring(9);
														percent = int.Parse(pct);
														txtDim.Text = percent.ToString();
													}
													else
													{
														ipos = lineIn.IndexOf(LOR4Admin.DELIMC + "NoChange" + LOR4Admin.DELIMC);
														if (ipos == 0)
														{
															rule = RULE_NOCHANGE;
														}
													}
												}
											}
										}
									}
								}
								if (ipos < 0)
								{
									if (rule > RULE_NOCHANGE)
									{
										parts = lineIn.Split(LOR4Admin.DELIM5);
										si = int.Parse(parts[0]);
										name = parts[1];
										if (rgb)
										{
											rgbchan = FindRGBChannel(si, name);
											if (rgbchan != null)
											{
												rgbchan.ZCount = rgbchan.ZCount | rule;
											}
										}
										else
										{
											chan = FindChannel(si, name);
											if (chan != null)
											{
												chan.ZCount = chan.ZCount | rule;
											}
										}
									}
								}
							}
						}
					}
				}
			}


			return err;
		} // end SaveMap

		private LOR4Channel FindChannel(int savedIndex, string name)
		{
			LOR4Channel chan = null;
			try
			{
				// Is saved index within valid range
				if (savedIndex <= seqSource.AllMembers.HighestSavedIndex)
				{
					// Get the member at that saved index
					iLOR4Member member = seqSource.AllMembers.BySavedIndex[savedIndex];
					// did we get Something, and is that something a channel?
					if (member != null)
					{
						if (member.MemberType == LOR4MemberType.Channel)
						{
							// So far so good!  Now, does the name match ?
							// Case insensitive
							int cmp = name.ToLower().CompareTo(member.Name.ToLower());
							// Case sensitive
							// int cmp = name.CompareTo(member.Name);
							if (cmp == 0)
							{
								// Got it!  Return it!
								chan = (LOR4Channel)member;
							}
							else // Nope, name does NOT match
							{
								// Search by name (and do NOT create if not found)
								chan = seqSource.FindChannel(name);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				string msg = "Error during FindChannel(" + savedIndex.ToString() + ", " + name + ")\r\n";
				msg += ex.Message;
				Fyle.BUG(msg);
			}
			return chan;
		}

		private LOR4RGBChannel FindRGBChannel(int savedIndex, string name)
		{
			LOR4RGBChannel rgbChan = null;
			try
			{
				// Is saved index within valid range
				if (savedIndex <= seqSource.AllMembers.HighestSavedIndex)
				{
					// Get the member at that saved index
					iLOR4Member member = seqSource.AllMembers.BySavedIndex[savedIndex];
					// did we get Something, and is that something a RGB Channel?
					if (member != null)
					{
						if (member.MemberType == LOR4MemberType.RGBChannel)
						{
							// So far so good!  Now, does the name match ?
							// Case insensitive
							int cmp = name.ToLower().CompareTo(member.Name.ToLower());
							// Case sensitive
							// int cmp = name.CompareTo(member.Name);
							if (cmp == 0)
							{
								// Got it!  Return it!
								rgbChan = (LOR4RGBChannel)member;
							}
							else // Nope, name does NOT match
							{
								// Search by name (and do NOT create if not found)
								rgbChan = seqSource.FindRGBChannel(name);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				string msg = "Error during FindRGBChannel(" + savedIndex.ToString() + ", " + name + ")\r\n";
				msg += ex.Message;
				Fyle.BUG(msg);
			}
			return rgbChan;
		}



		private string LoadApplyMapFile(string fileName)
		{
			ImBusy(true);
			//int w = pnlStatus.Width;
			//pnlProgress.Width = w;
			//pnlProgress.Size = new Size(w, pnlProgress.Height);
			//pnlStatus.Visible = false;
			//pnlProgress.Visible = true;
			//staStatus.Refresh();
			//int mq = seqMaster.Tracks.Count + seqMaster.ChannelGroups.Count + seqMaster.Channels.Count;
			//mq -= seqMaster.RGBchannels.Count * 2;
			//int sq = seqSource.Tracks.Count + seqSource.ChannelGroups.Count + seqSource.Channels.Count;
			//sq -= seqSource.RGBchannels.Count * 2;
			//int qq = mq + sq;
			//pnlProgress.Maximum = qq + 1;
			//int pp = 0;
			int lineCount = 0;
			int lineNum = 0;
			string errMsgs = "";
			/*
			string lineIn;
			string[] mapData;
			string[] sides;
			string sourceName = "";
			string masterName = "";
			string temp = "";
			//int tempNum;
			int[] foundChannels;
			int sourceSI = LOR4Admin.UNDEFINED;
			int masterSI = LOR4Admin.UNDEFINED;
			LOR4MemberType sourceType = LOR4MemberType.None;
			LOR4MemberType masterType = LOR4MemberType.None;
			iLOR4Member masterMember = null;
			iLOR4Member newSourceMember = null;
			iLOR4Member foundID = null;
			string mfile = "";
			string sfile = "";
			long finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
			double minPreMatch = Properties.Settings.Default.FuzzyMinPrematch;
			double minFinalMatch = Properties.Settings.Default.FuzzyMinFinal;
			long preAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
			logFile1 = logHomeDir + "Apply" + batch_fileNumber.ToString() + ".log";
			logWriter1 = new StreamWriter(logFile1);
			log1Open = true;
			logMsg = "Destination File: " + seqMaster.info.filename;
			logWriter1.WriteLine(logMsg);
			logMsg = "Source File: " + seqSource.info.filename;
			logWriter1.WriteLine(logMsg);
			logMsg = "Map File: " + fileName;
			logWriter1.WriteLine(logMsg);
			mappedMemberCount = 0;
			int li = 0;
			int errorStatus = 0;

			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;

			if (batchMode)
			{
				ShowProgressBars(2);
			}
			else
			{
				ShowProgressBars(1);
			}
			ClearAllMappings();




			////////////////////////////////////////////////////////////////////////////////////////////////////////////
			// First Pass, just make sure it's valid and get line count so we can update the progress bar accordingly
			//////////////////////////////////////////////////////////////////////////////////////////////////////////
			#region First Pass
			StreamReader reader = new StreamReader(fileName);
			if (!reader.EndOfStream)
			{
				lineIn = reader.ReadLine();

				// Sanity Check #2, is it an XML file?
				li = lineIn.Substring(0, 6).CompareTo("<?xml ");
				if (li != 0)
				{
					errorStatus = 101;
				}
				else
				{
					//xmlInfo = lineIn;
					// Sanity Check #1B, does it have at least 2 lines?
					if (!reader.EndOfStream)
					{
						lineIn = reader.ReadLine();
						// Sanity Check #3, is it a sequence?
						//li = lineIn.IndexOf(TABLEchMap);
						li = LOR4Admin.FastIndexOf(lineIn, TABLEchMap);
						if (li != 1)
						{
							errorStatus = 102;
						}
						else
						{
							int sfv = LOR4Admin.getKeyValue(lineIn, LOR4SeqInfo.FIELDsaveFileVersion);
							// Sanity Checks #4A and 4B, does it have a 'SaveFileVersion' and is it '14'
							//   (SaveFileVersion="14" means it cane from LOR Sequence Editor ver 4.x)
							if (sfv != 14)
							{
								errorStatus = 114;
							}
							else
							{
								lineCount = 2;
								while ((lineIn = reader.ReadLine()) != null)
								{
									lineCount++;
								} // end while lines remain, counting them
							} // saveVersion = 14
						} // Its a channelMap
					} // It has at least 2 lines
				} //its in XML format
			} // it has at least 1 line
			reader.Close();
			#endregion // First Pass
			// end of first pass




			if ((errorStatus == 0) && (lineCount > 12))
			{
				#region Second Pass
				///////////////////////////////////////////////
				// Second Pass, look for EXACT matches only
				/////////////////////////////////////////////
				// All sanity checks passed

				//pnlProgress.Maximum = lineCount;
				prgBarInner.Maximum = lineCount * 10;
				if (batchMode)
				{
					prgBarOuter.Maximum = lineCount * 10 * batch_fileCount;
				}

				// Updating and repainting the status takes time, so set an update frequence resulting in no more that 200 updates
				int updFreq = lineCount / 200;

				reader = new StreamReader(fileName);
				// Read in and then ignore the first 8 lines
				//   xml, channelMap, files, mappings...
				for (int n = 0; n < 7; n++)
				{
					lineIn = reader.ReadLine();
				}
				lineNum = 7;

				// * PARSE LINES
				while ((lineIn = reader.ReadLine()) != null)
				{
					lineNum++;
					// Line just read should be "    <channels>"
					//li = lineIn.IndexOf(LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL);
					li = LOR4Admin.FastIndexOf(lineIn, (LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL));
					if (li > 0)
					{
						if (!reader.EndOfStream)
						{
							// Next line is the Master LOR4Channel
							lineIn = reader.ReadLine();
							lineNum++;
							//li = lineIn.IndexOf(FIELDmasterChannel);
							li = LOR4Admin.FastIndexOf(lineIn, FIELDmasterChannel);
							if (li > 0)
							{
								masterName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
								masterSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);
								masterType = LOR4SeqEnums.EnumMemberType(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDtype));
								//lblMessage.Text = "Map \"" + masterName + "\" to...";
								//pnlMessage.Refresh();

								// Next line (let's assume its there) is the Source LOR4Channel
								if (!reader.EndOfStream)
								{
									// Next line is the Source LOR4Channel
									lineIn = reader.ReadLine();
									lineNum++;
									//li = lineIn.IndexOf(FIELDsourceChannel);
									li = LOR4Admin.FastIndexOf(lineIn, FIELDsourceChannel);
									if (li > 0)
									{
										sourceName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
										sourceSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);
										sourceType = LOR4SeqEnums.EnumMemberType(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDtype));

										// is it time to update?
										if ((lineNum % updFreq) == 0)
										{
											if (sourceOnRight)
											{
												lblMessage.Text = masterName + " to " + sourceName;
											}
											else
											{
												lblMessage.Text = sourceName + " to " + masterName;
											}
											prgBarInner.CustomText = lblMessage.Text;
											pnlMessage.Refresh();
										} // end update if needed

										// Try to find the master member by SavedIndex first since that is the easiest.
										// May not be a match, but good chance it is.
										// Is the Saved Index within range?
										if (masterSI <= seqMaster.Members.HighestSavedIndex)
										{
											// In range, so fetch it
											masterMember = seqMaster.Members.BySavedIndex[masterSI];
											// Now compare the name, is it actually the same member?
											// (May not be a match, but good chance it is.)
											if (masterMember.Name.ToLower().CompareTo(masterName.ToLower()) != 0)
											{
												// Nope, name doesn't match, nullify the find (by SavedIndex)
												masterMember = null;
											}
										}
										// Did we get it?
										if (masterMember == null)
										{
											// No, search again by name this time
											masterMember = seqMaster.Members.Find(masterName, masterType, false);
										}
										// Did we get it (by name)?
										if (masterMember != null)
										{
											// Did we find the master?  If not, no sense in searching for the source.
											// Follow same procedure as above to try and find the source
											// (Read comments above)
											if (sourceSI <= seqSource.Members.HighestSavedIndex)
											{
												newSourceMember = seqSource.Members.BySavedIndex[sourceSI];
												if (newSourceMember.Name.ToLower().CompareTo(sourceName.ToLower()) != 0)
												{
													newSourceMember = null;
												}
											}
											if (newSourceMember == null)
											{
												newSourceMember = seqSource.Members.Find(sourceName, sourceType, false);
											}
											if (newSourceMember != null)
											{
												// So did we succeed in finding the source member?
												// Final Sanity Checks
												if (masterMember.MemberType == (newSourceMember.MemberType))
												{
													if (masterMember.MemberType == masterType)
													{
														logMsg = "Exact Matched " + masterName + " to " + sourceName;
														Console.WriteLine(logMsg);
														logWriter1.WriteLine(logMsg);
														//MapMembers(masterSI, sourceSI, true, false);
														//seqMaster.Members.BySavedIndex[masterSI].Selected = true;
														//seqSource.Members.BySavedIndex[sourceSI].Selected = true;
														//MapMembers(masterSI, sourceSI, false);
														int newMaps = MapMembers(masterMember, newSourceMember, true);
														mappedMemberCount += newMaps;
													}
												} // End final sanity checks
											} // End and found source member
										} // End found master member
										else
										{
											logMsg = masterName + " not found in Master.";
											Console.WriteLine(logMsg);
											logWriter1.WriteLine(logMsg);
										}
									} // End got source info from file line
									else
									{
										logMsg = sourceName + " not found in Source.";
										Console.WriteLine(logMsg);
										logWriter1.WriteLine(logMsg);
									}
								} // End not end of stream
							} // End got master info from file line
							prgBarInner.Value = lineNum;
							//staStatus.Refresh();
							prgBarInner.Refresh();
							if (batchMode)
							{
								int v = batch_fileNumber * lineCount * 10;
								v += lineNum;
								prgBarOuter.Value = v;
								prgBarOuter.Refresh();
							}
							// After every 256 lines, perform a DoEvents
							// (Can change to 128 with 0x7F or every 64 with 0x3F, etc.)
							int lcr = lineNum & 0xFF;
							if (lcr == 0xFF)
							{
								Application.DoEvents();
							}
						} // End not end of stream
					} // End line not null
				} // End got channels XML table header
				reader.Close();
				#endregion // Second Pass




				////////////////////////////////////////////////////////////////////
				// Third pass, attempt to fuzzy find anything not already matched
				//////////////////////////////////////////////////////////////////
				#region Third Pass
				// Don't think should do fuzzy find on reading a map file
				if (true == false)
				{
					reader = new StreamReader(fileName);
					// Read in and then ignore the first 8 lines
					//   xml, channelMap, files, mappings...
					for (int n = 0; n < 7; n++)
					{
						lineIn = reader.ReadLine();
					}
					lineNum = 7;

					// * PARSE LINES
					while ((lineIn = reader.ReadLine()) != null)
					{
						lineNum++;
						// Line just read should be "    <channels>"
						//li = lineIn.IndexOf(LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL);
						li = LOR4Admin.FastIndexOf(lineIn, LOR4Admin.STTBL + TABLEchannels + LOR4Admin.ENDTBL);
						if (li > 0)
						{
							if (!reader.EndOfStream)
							{
								// Next line is the Master LOR4Channel
								lineIn = reader.ReadLine();
								lineNum++;
								//li = lineIn.IndexOf(FIELDmasterChannel);
								li = LOR4Admin.FastIndexOf(lineIn, FIELDmasterChannel);
								if (li > 0)
								{
									masterName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
									//lblMessage.Text = "Map \"" + masterName + "\" to...";
									//pnlMessage.Refresh();
									masterSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);
									masterType = LOR4SeqEnums.EnumMemberType(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDtype));
									// Next line (let's assume its there) is the Source LOR4Channel
									if (!reader.EndOfStream)
									{
										// Next line is the Source LOR4Channel
										lineIn = reader.ReadLine();
										lineNum++;
										//li = lineIn.IndexOf(FIELDsourceChannel);
										li = LOR4Admin.FastIndexOf(lineIn, FIELDsourceChannel);
										if (li > 0)
										{
											sourceName = LOR4Admin.HumanizeName(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDname));
											if (sourceOnRight)
											{
												lblMessage.Text = masterName + " to " + sourceName;
											}
											else
											{
												lblMessage.Text = sourceName + " to " + masterName;
											}
											prgBarInner.CustomText = lblMessage.Text;
											pnlMessage.Refresh();
											sourceSI = LOR4Admin.getKeyValue(lineIn, LOR4Admin.FIELDsavedIndex);
											sourceType = LOR4SeqEnums.EnumMemberType(LOR4Admin.getKeyWord(lineIn, LOR4Admin.FIELDtype));

											if (!seqMaster.Members.BySavedIndex[masterSI].Selected)
											{
												if (seqMaster.Members.BySavedIndex[masterSI].MemberType == masterType)
												{
													// Search for it again by name, this time use fuzzy find
													logMsg = "Fuzzy searching Destination for ";
													logMsg += sourceName;
													Console.WriteLine(logMsg);
													logWriter1.WriteLine(logMsg);

													foundID = FindByName(masterName, seqMaster.Members, masterType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
													if (foundID != null)
													{
														masterSI = foundID.SavedIndex;
														masterName = foundID.Name;
													}

													// Got it yet?
													if (masterSI > LOR4Admin.UNDEFINED)
													{
														if (sourceSI < seqSource.Members.BySavedIndex.Count)
														{
															if (seqSource.Members.BySavedIndex[sourceSI].MemberType == sourceType)
															{
																if (!seqSource.Members.BySavedIndex[sourceSI].Selected)
																{
																	//try to find it again by name and this time use fuzzy matching
																	logMsg = "Fuzzy searching Source for ";
																	logMsg += sourceName;
																	Console.WriteLine(logMsg);
																	logWriter1.WriteLine(logMsg);
																	foundID = FindByName(sourceName, seqSource.Members, sourceType, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, true);
																	if (foundID != null)
																	{
																		sourceSI = foundID.SavedIndex;
																	}
																} // if not Selected

																// Got it yet?
																if (sourceSI > LOR4Admin.UNDEFINED)
																{
																	if (seqSource.Members.BySavedIndex[sourceSI].MemberType ==
																			seqMaster.Members.BySavedIndex[masterSI].MemberType)
																	{
																		logMsg = "Fuzzy Matched " + masterName + " to " + sourceName;
																		Console.WriteLine(logMsg);
																		logWriter1.WriteLine(logMsg);
																		int newMaps = MapMembers(masterSI, sourceSI, true, false);
																		mappedMemberCount += newMaps;
																		seqMaster.Members.BySavedIndex[masterSI].Selected = true;
																		seqSource.Members.BySavedIndex[sourceSI].Selected = true;
																	}
																}
															} // if source type matchies
														} // if source SavedIndex in range
													} // if source section line
												} // if lines remain
											} // if got a smaster channel
										} // if master type matches
									} // if master SI not Selected
								} // if line is a master channel
							} // if lines left
						} // if a channel section
							//pnlProgress.Value = lineNum;
						prgBarInner.Value = lineCount + lineNum * 9;
						//staStatus.Refresh();
						prgBarInner.Refresh();
						if (batchMode)
						{
							int v = batch_fileNumber * lineCount * 10;
							v += (lineCount + lineNum * 9);
							prgBarOuter.Value = v;
							prgBarOuter.Refresh();
						}
						// After every 256 lines, perform a DoEvents
						// (Can change to 128 with 0x7F or every 64 with 0x3F, etc.)
						int lcr = lineNum & 0xFF;
						if (lcr == 0xFF)
						{
							Application.DoEvents();
						}

					} // if lines remain
					reader.Close();
				} // End False=True
				#endregion
				// end third pass
			} // End no errors, and correct # of lines

			//pnlProgress.Visible = false;
			//pnlStatus.Visible = true;

			if (!batchMode)
			{
				ShowProgressBars(0);
				ImBusy(false);
			}

			logWriter1.Close();
			UpdateMappedCount(0);
			
			*/
			return errMsgs;
		} // end LoadApplyMapFile

		private void SaveNewMappedSequence()
		{
			/*
			ImBusy(true);
			string xt = Path.GetExtension(filenameSource).ToLower();
			dlgFileSave.DefaultExt = xt;
			dlgFileSave.Filter = LOR4Admin.FILT_SAVE_EITHER;
			dlgFileSave.FilterIndex = 0;
			if (xt.CompareTo(LOR4Admin.EXT_LAS) == 0) dlgFileSave.FilterIndex = 1;
			string initDir = SeqFolder;
			string initFile = "";
			if (filenameSource.Length > 4)
			{
				if (saveFile.Length > 3)
				{
					string pth = Path.GetFullPath(saveFile);
					if (Directory.Exists(pth))
					{
						initDir = pth;
					}
				}
				if (File.Exists(filenameSource))
				{
					initFile = Path.GetFileName(filenameSource);
				}
			}
			string newName = SuggestedNewName(initFile);
			dlgFileSave.FileName = newName;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.OverwritePrompt = true;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.Title = "Save New Sequence As...";

			DialogResult dr = dlgFileSave.ShowDialog();
			if (dr == DialogResult.OK)
			{

				string saveTemp = System.IO.Path.GetTempPath();
				saveTemp += Path.GetFileName(dlgFileSave.FileName);

				string msg = "GLB";
				msg += LeftBushesChildCount().ToString();
				lblDebug.Text = msg;

				int mapErr = SaveNewMappedSequence(saveTemp);
				if (mapErr == 0)
				{
					saveFile = dlgFileSave.FileName;
					if (File.Exists(saveFile))
					{
						//TODO: Add Exception Catch
						File.Delete(saveFile);
					}
					File.Copy(saveTemp, saveFile);
					File.Delete(saveTemp);

					if (chkAutoLaunch.Checked)
					{
						System.Diagnostics.Process.Start(saveFile);

					}
					//dirtyMap = false;
					//btnSaveMap.Enabled = dirtyMap;
					//txtMappingFile.Text = Path.GetFileName(mapFile);
				} // end no errors saving map
			} // end dialog result = OK
			ImBusy(false);
			*/
		}








		#endregion

		private void frmDim_Paint(object sender, PaintEventArgs e)
		{
			if (!firstShown)
			{
				FirstShow();
				firstShown = true;
			}
		}

		private void FirstShow()
		{
			if (Fyle.Exists(filenameSource))
			{
				LoadSourceSequence(filenameSource);
			}

		}

		private void btnBrowseSource_Click(object sender, EventArgs e)
		{
			BrowseForSourceSequence();
		}

		private void treeSource_AfterSelect(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeSource.SelectedNode;
			if (node != null)
			{
				if (node.Tag != null)
				{
					iLOR4Member member = (iLOR4Member)treeSource.SelectedNode.Tag;
					if (member.MemberType == LOR4MemberType.Channel)
					{
						Bitmap img = LOR4Admin.RenderEffects(member, 0, member.Centiseconds, picPreviewSource.Width, picPreviewSource.Height, true);
						if (tabChannels.SelectedIndex == TAB_DIM)
						{
							img = DrawCutoff(img, Color.Black, int.Parse(txtDim.Text));
						}
						if (tabChannels.SelectedIndex == TAB_TRIM)
						{
							img = DrawCutoff(img, Color.Black, int.Parse(txtTrim.Text));
						}
						picPreviewSource.Image = img;
						picPreviewSource.Visible = true;
					}
					else
					{
						if (member.MemberType == LOR4MemberType.RGBChannel)
						{
							Bitmap img = LOR4Admin.RenderEffects(member, 0, member.Centiseconds, picPreviewSource.Width, picPreviewSource.Height, false);
							if (tabChannels.SelectedIndex == TAB_DIM)
							{
								img = DrawCutoff(img, Color.White, int.Parse(txtDim.Text));
							}
							if (tabChannels.SelectedIndex == TAB_TRIM)
							{
								img = DrawCutoff(img, Color.White, int.Parse(txtTrim.Text));
							}
							picPreviewSource.Image = img;
							picPreviewSource.Visible = true;
						}
						else
						{
							picPreviewSource.Visible = false;
						}
					}
				}
			}
		}

		private Color GetRuleForecolor(int rules)
		{
			Color ret = Color.Magenta; // Default if not found
			switch (rules)
			{
				case RULE_NOCHANGE:
					// Colors for the various possible rules
					ret = COLOR_NOCHANGE;
					break;
				case RULE_DIM:
					ret = COLOR_DIM;
					break;
				case RULE_TRIM:
					ret = COLOR_TRIM;
					break;
				case RULE_ONOFF:
					ret = COLOR_ONOFF;
					break;
				case RULE_MINTIME:
					ret = COLOR_MINTIME;
					break;
				case RULES_DIM_MINTIME:
					ret = COLOR_DIM_MINTIME;
					break;
				case RULES_TRIM_MINTIME:
					ret = COLOR_TRIM_MINTIME;
					break;
				case RULES_ONOFF_MINTIME:
					ret = COLOR_ONOFF_MINTIME;
					break;
			}
			return ret;
		}


		private Bitmap DrawCutoff(Bitmap preview, Color lineColor, int percent)
		{
			int y = preview.Height * percent / 100;
			y = preview.Height - y;
			Graphics gr = Graphics.FromImage(preview);
			Pen p = new Pen(lineColor, 1);
			Brush br = new SolidBrush(lineColor);
			gr.DrawLine(p, 0, y, Width, y);
			return preview;
		}

		private int ScaleChannel(iLOR4Member member, int percent, bool force = false)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch (member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LOR4MemberType.Channel:
					LOR4Channel chan = (LOR4Channel)member;
					// if 'Force' is true, then definately needed
					needed = force;
					// If not Force, then check to see if needed
					if (!force)
					{
						// Loop thru all effects...
						for (int e = 0; e < chan.effects.Count; e++)
						{
							// Check all effect startIntensity and endIntensity
							// If *NONE* of them exceed the threshhold, then most likely this channel has already been scaled
							LOR4Effect effect = chan.effects[e];
							// Looking for any with too high of an intensity
							if ((effect.startIntensity > percent) ||
									(effect.endIntensity > percent))
							{
								// Too high, so we NEED to scale
								needed = true;
								e = chan.effects.Count; // Loopus Interruptus
							}
						}
					}
					// If scaling is needed--
					if (needed)
					{
						// If at least one startIntensity or at least one endIntensity surpassed the threshhold
						// then this channel has obviously not been scaled yet and thus
						// *ALL* effects need to be scaled down
						double pct = (percent / 100D);
						// Loop thru all effects
						for (int e = 0; e < chan.effects.Count; e++)
						{
							LOR4Effect effect = chan.effects[e];
							// Scale EACH AND EVERY effect startIntensity and endIntensity
							int si = (int)Math.Round(effect.startIntensity * pct);
							int ei = (int)Math.Round(effect.endIntensity * pct);
							effect.startIntensity = si;
							effect.endIntensity = ei;
							count++;
						}
					}
					break;

				//////////////////
				// RGB CHANNEL //
				////////////////
				case LOR4MemberType.RGBChannel:
					LOR4RGBChannel rgb = (LOR4RGBChannel)member;
					// if 'Force' is true, then definately needed
					needed = force;
					// If not Force, then check to see if needed
					if (!force)
					{
						// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
						// and if *NONE* of the effects on ALL 3 subchannels exceed the threshold
						// then this RGB channel as probably already been scaled
						// but if *ANY* effects on *ANY* of the subchannels exceed the threshold...

						// Loop thru all RED effects...
						for (int e = 0; e < rgb.redChannel.effects.Count; e++)
						{
							// Check all effect startIntensity and endIntensity
							// If *NONE* of them exceed the threshhold, then most likely this channel has already been scaled
							LOR4Effect effect = rgb.redChannel.effects[e];
							// Looking for any with too high of an intensity
							if ((effect.startIntensity > percent) ||
									(effect.endIntensity > percent))
							{
								// Too high, so we NEED to scale
								needed = true;
								e = rgb.redChannel.effects.Count; // Loopus Interruptus
							}
						}
						if (!needed)
						{
							// Again, Loop thru all GREEN effects...
							for (int e = 0; e < rgb.grnChannel.effects.Count; e++)
							{
								// Check all effect startIntensity and endIntensity
								// If *NONE* of them exceed the threshhold, then most likely this channel has already been scaled
								LOR4Effect effect = rgb.grnChannel.effects[e];
								// Looking for any with too high of an intensity
								if ((effect.startIntensity > percent) ||
										(effect.endIntensity > percent))
								{
									// Too high, so we NEED to scale
									needed = true;
									e = rgb.grnChannel.effects.Count; // Loopus Interruptus
								}
							}
						}
						if (!needed)
						{
							// Again, Loop thru all BLUE effects...
							for (int e = 0; e < rgb.bluChannel.effects.Count; e++)
							{
								// Check all effect startIntensity and endIntensity
								// If *NONE* of them exceed the threshhold, then most likely this channel has already been scaled
								LOR4Effect effect = rgb.bluChannel.effects[e];
								// Looking for any with too high of an intensity
								if ((effect.startIntensity > percent) ||
										(effect.endIntensity > percent))
								{
									// Too high, so we NEED to scale
									needed = true;
									e = rgb.bluChannel.effects.Count; // Loopus Interruptus
								}
							}
						}
					}

					// If at least one effect on at least one subchannel exceeded the threshhold
					// Then we need to FORCE scaling of all effects on all 3 subchannels
					if (needed)
					{
						// Recurse!
						count += ScaleChannel(rgb.redChannel, percent, true);
						count += ScaleChannel(rgb.grnChannel, percent, true);
						count += ScaleChannel(rgb.bluChannel, percent, true);
					}
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LOR4MemberType.ChannelGroup:
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					foreach (iLOR4Member subMember in group.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LOR4MemberType.Cosmic:
					LOR4Cosmic cosmic = (LOR4Cosmic)member;
					foreach (iLOR4Member subMember in cosmic.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LOR4MemberType.Track:
					LOR4Track track = (LOR4Track)member;
					foreach (iLOR4Member subMember in track.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;
			}


			return count;
		}

		private int TrimChannel(iLOR4Member member, int percent)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch (member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LOR4MemberType.Channel:
					LOR4Channel chan = (LOR4Channel)member;
					// Loop thru all effects
					for (int e = 0; e < chan.effects.Count; e++)
					{
						LOR4Effect effect = chan.effects[e];
						if ((effect.startIntensity > percent) &&
								(effect.endIntensity > percent))
						{
							// if BOTH start and end are over threshhold, easy peasy
							effect.startIntensity = percent;
							effect.endIntensity = percent;
							count++;
						}
						else
						{
							if ((effect.startIntensity > percent) &&
									(effect.endIntensity == 0))
							{
								// if standard intensity, or a ramp-down
								effect.startIntensity = percent;
								count++;
							}
							else
							{
								if ((effect.startIntensity == 0) &&
										(effect.endIntensity > percent))
								{
									// if a full ramp up from zero, easy
									effect.endIntensity = percent;
									count++;
								}
								else
								{
									// OK, so now things get complicated...
									// Its not a simple on-off or ramp-up/ramp-down to/from zero

									//TODO This!!
								}
							}
						}
					}
					break;

				//////////////////
				// RGB CHANNEL //
				////////////////
				case LOR4MemberType.RGBChannel:
					LOR4RGBChannel rgb = (LOR4RGBChannel)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					TrimChannel(rgb.redChannel, percent);
					TrimChannel(rgb.grnChannel, percent);
					TrimChannel(rgb.bluChannel, percent);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LOR4MemberType.ChannelGroup:
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					foreach (iLOR4Member subMember in group.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LOR4MemberType.Cosmic:
					LOR4Cosmic cosmic = (LOR4Cosmic)member;
					foreach (iLOR4Member subMember in cosmic.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LOR4MemberType.Track:
					LOR4Track track = (LOR4Track)member;
					foreach (iLOR4Member subMember in track.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;
			}


			return count;
		}

		private int OnOffChannel(iLOR4Member member)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch (member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LOR4MemberType.Channel:
					LOR4Channel chan = (LOR4Channel)member;
					// Loop thru all effects
					for (int e = 0; e < chan.effects.Count; e++)
					{
						LOR4Effect effect = chan.effects[e];
						if ((effect.startIntensity > 50) &&
								(effect.endIntensity > 50))
						{
							// if BOTH start and end are over threshhold, easy peasy
							effect.startIntensity = 100;
							effect.endIntensity = 100;
							count++;
						}
						else
						{
							if ((effect.startIntensity <= 50) &&
									(effect.endIntensity <= 50))
							{
								// if BOTH start and end are under threshhold, REMOVE the effect
								chan.effects.RemoveAt(e);
								count++;
							}
							else
							{
								if ((effect.startIntensity > 50) &&
									(effect.endIntensity == 0))
								{
									// if a ramp-down
									//TODO !This
									count++;
								}
								else
								{
									if ((effect.startIntensity == 0) &&
											(effect.endIntensity > 50))
									{
										// if a ramp up
										//TODO This!
										count++;
									}
									else
									{
										// OK, so now things get complicated...
										// Its not a simple on-off or ramp-up/ramp-down to/from zero

										//TODO This!!
									}
								}
							}
						}
					}
					break;

				//////////////////
				// RGB CHANNEL //
				////////////////
				case LOR4MemberType.RGBChannel:
					LOR4RGBChannel rgb = (LOR4RGBChannel)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					OnOffChannel(rgb.redChannel);
					OnOffChannel(rgb.grnChannel);
					OnOffChannel(rgb.bluChannel);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LOR4MemberType.ChannelGroup:
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					foreach (iLOR4Member subMember in group.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LOR4MemberType.Cosmic:
					LOR4Cosmic cosmic = (LOR4Cosmic)member;
					foreach (iLOR4Member subMember in cosmic.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LOR4MemberType.Track:
					LOR4Track track = (LOR4Track)member;
					foreach (iLOR4Member subMember in track.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;
			}


			return count;
		}

		private int MinTimeChannel(iLOR4Member member, int seconds)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			int mincs = seconds * 1000;
			int elen = 0;
			int egap = 0;
			switch (member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LOR4MemberType.Channel:
					LOR4Channel chan = (LOR4Channel)member;
					LOR4Effect thiseffect = null;
					LOR4Effect nexteffect = null;
					// Loop thru all but last effects
					for (int e = 0; e < (chan.effects.Count - 1); e++)
					{
						thiseffect = chan.effects[e];
						// Does this effect now start after the end of the channel
						//   (thanks to being moved on the last pass of the loop) ?
						if (thiseffect.startCentisecond > chan.Centiseconds)
						{
							// get rid of it
							chan.effects.RemoveAt(e);
							// Decrement counter so as to next check the next effect (which now is at THIS position!)
							e--;
						}
						else
						{
							nexteffect = chan.effects[e + 1];
							elen = thiseffect.endCentisecond - thiseffect.startCentisecond;
							// If time on is too short
							if (elen < mincs)
							{
								// extend the end
								thiseffect.endCentisecond = thiseffect.startCentisecond + mincs;
								count++;
								// if it now goes beyond the end of the channel
								if (thiseffect.endCentisecond > chan.Centiseconds)
								{
									// get rid of it
									chan.effects.RemoveAt(e);
									// Decrement counter so as to next check the next effect (which now is at THIS position!)
									e--;
								}
								else
								{
									egap = nexteffect.startCentisecond - thiseffect.endCentisecond;
									// if time off is too short
									if (egap < mincs)
									{
										// move the next effect later
										nexteffect.startCentisecond = thiseffect.endCentisecond + mincs;
										// if it now goes beyond the end of the channel
										if (nexteffect.startCentisecond > chan.Centiseconds)
										{
											// get rid of it
											chan.effects.RemoveAt(e + 1);
											// Decrement counter so as to next check the next effect (which now is at THIS position!)
											e--;
										}
									}
								}
							}
						}
					}
					// OK, now how about that last effect...
					// Repeat Same logic as above
					thiseffect = chan.effects[chan.effects.Count - 1];
					// Does this (the last) effect now start after the end of the channel
					//   (thanks to being moved on the last pass of the loop) ?
					if (thiseffect.startCentisecond > chan.Centiseconds)
					{
						// get rid of it
						chan.effects.RemoveAt(chan.effects.Count - 1);
					}
					else
					{
						elen = thiseffect.endCentisecond - thiseffect.startCentisecond;
						// If time on is too short
						if (elen < mincs)
						{
							// extend the end
							thiseffect.endCentisecond = thiseffect.startCentisecond + mincs;
							count++;
							// if it now goes beyond the end of the channel
							if (thiseffect.endCentisecond > chan.Centiseconds)
							{
								// get rid of it
								chan.effects.RemoveAt(chan.effects.Count - 1);
							}
						}
					}


					break;

				//////////////////
				// RGB CHANNEL //
				////////////////
				case LOR4MemberType.RGBChannel:
					LOR4RGBChannel rgb = (LOR4RGBChannel)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					OnOffChannel(rgb.redChannel);
					OnOffChannel(rgb.grnChannel);
					OnOffChannel(rgb.bluChannel);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LOR4MemberType.ChannelGroup:
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					foreach (iLOR4Member subMember in group.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LOR4MemberType.Cosmic:
					LOR4Cosmic cosmic = (LOR4Cosmic)member;
					foreach (iLOR4Member subMember in cosmic.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LOR4MemberType.Track:
					LOR4Track track = (LOR4Track)member;
					foreach (iLOR4Member subMember in track.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;
			}


			return count;
		}

		private void UpdateTreeFormatting(int theRule)
		{
			for (int n = 0; n < treeSource.Nodes.Count; n++)
			{
				// Is the nodes collection just at the root, or all of them?  Guess we're about to find out
				TreeNodeAdv node = treeSource.Nodes[n];
				{
					UpdateNodeChecks(node, theRule);
					//UpdateNodeFormatting(node);
				}
			}
		}

		private void UpdateNodeFormatting(TreeNodeAdv node, bool recurse = false)
		{
			if (node.Tag != null)
			{
				iLOR4Member member = (iLOR4Member)node.Tag;
				if (node.CheckState == CheckState.Indeterminate)
				{
					node.TextColor = COLOR_INDETERMINATE;
				}
				else
				{
					int rules = member.ZCount;
					node.TextColor = ruleColors[rules];
				}
				//If it has clones
				List<TreeNodeAdv> chNodes = (List<TreeNodeAdv>)member.Tag;
				if (chNodes.Count > 1)
				{
					for (int c = 0; c < chNodes.Count; c++)
					{
						if (chNodes[c].Text != node.Text)
						{
							//? Don't THINK I need to recurse this as all clones should have identical children (?)
							//? And might cause deadlock or infinate loop if I try (?)
							UpdateNodeFormatting(chNodes[c], false);
							//? Or should I copy the checked state thereby triggering the AfterCheck event (?)
							//? Also might cause deadlock or infinate loop if I try (?)
							chNodes[c].CheckState = node.CheckState;
						}
					}
				}
			}
			if (recurse)
			{
				// Recurse if it has children!
				for (int n = 0; n < node.Nodes.Count; n++)
				{
					// Keep Recursing going DOWN the tree
					UpdateNodeFormatting(node.Nodes[n], true);
				}
			}
			if (node.Parent != null)
			{
				// Recurse UP the tree, but NOT back down again
				UpdateNodeFormatting(node.Parent, false);
			}



		}

		private CheckState UpdateNodeChecks(TreeNodeAdv node, int theRule)
		{
			CheckState ret = CheckState.Unchecked;
			if (node.Tag != null)
			{
				iLOR4Member member = (iLOR4Member)node.Tag;
				int nodeRule = member.ZCount;
				if ((member.MemberType == LOR4MemberType.Channel) ||
					(member.MemberType == LOR4MemberType.RGBChannel))
				{
					if ((theRule == RULE_DIM) ||
						(theRule == RULE_TRIM) ||
						(theRule == RULE_ONOFF))
					{
						// if exclusive rule dim, trim or onoff, mask out the mintime rule
						bool chk = ((nodeRule & 3) == theRule);
						node.Checked = chk;
					}
					else
					{
						if (theRule == RULE_MINTIME)
						{
							// if non-exclusive mintime rule, mask out exclusive dim, trim, and onoff
							bool chk = ((nodeRule & RULE_MINTIME) == theRule);
							node.Checked = chk;
						}
						else
						{
							// if rule is very exclusive nochange, do masking
							if (theRule == RULE_NOCHANGE)
							{
								bool chk = (nodeRule == theRule);
								node.Checked = chk;
							}
						}
					}
					if (node.Checked) ret = CheckState.Checked;
				}
				else
				{
					if (member.MemberType == LOR4MemberType.ChannelGroup)
					{
						LOR4ChannelGroup group = (LOR4ChannelGroup)member;
						ret = GroupCheckState(group.Members, theRule);
					}
					else
					{
						if (member.MemberType == LOR4MemberType.Track)
						{
							LOR4Track track = (LOR4Track)member;
							ret = GroupCheckState(track.Members, theRule);
						}
					} // End if a group, or not
				} // end if a channel or rgb channel, or not
			} // end if tag ain't null
				// Recurse if it has children!
			for (int n = 0; n < node.Nodes.Count; n++)
			{
				UpdateNodeChecks(node.Nodes[n], theRule);
			}
			node.CheckState = ret;
			return ret;
		}


		private void treeSource_AfterCheck(object sender, TreeNodeAdvEventArgs e)
		{
			TreeNodeAdv node = e.Node;
			if (node != null)
			{
				// So if I'm reading the Syncfustion documentation correctly---
				// This should fire once for the checked node, again for the parent, and again for children?
				bool chk = node.Checked;
				string whichNode = node.Text;
				CheckNode(node, chk, tabChannels.SelectedIndex);
				//TODO Here or in CheckNode(...) below
				//TODO Update node appearance (forecolor, checkstate, ...) for all nodes of this member
				//  (keep in mind that a member channel can easily be in the tree in multiple places)
				UpdateNodeFormatting(node); // , TabRules[tabChannels.SelectedIndex]) ;
			}
		}

		private void CheckNode(TreeNodeAdv node, bool isChecked, int tabIndex)
		{
			CheckState newState = node.CheckState;
			if (node != null)
			{
				if (node.Tag != null)
				{
					iLOR4Member member = (iLOR4Member)node.Tag;
					SetMemberRules(member, TabRules[tabIndex], isChecked);
				} // end if node.Tag ain't null
			} // end if node ain't null
		}


		private int SetMemberRules(iLOR4Member member, int newRule, bool isChecked)
		{
			// Return old rules
			int ret = member.ZCount;
			// What is the CURRENT rule and MinTime of this member (before change)
			int rul = member.ZCount & 3;
			int mtime = member.ZCount & RULE_MINTIME;
			// If its one of the 3 exclusive rules
			if ((newRule == RULE_DIM) ||
				(newRule == RULE_TRIM) ||
				(newRule == RULE_ONOFF))
			{
				if (isChecked)
				{
					// Set the new rule, preserving MinTime if set
					member.ZCount = newRule | mtime;
				}
				else
				{
					// Clear the rule but preserve MinTime if set
					member.ZCount = RULE_NOCHANGE | mtime;
					// (RULE_NOCHANGE = 0 so just member.ZCount = mtime would have worked,
					// but this way its more obvious whats going on)
				}
				// If its the MinTime rule (which is not exclusive)
				if (newRule == RULE_MINTIME)
				{
					if (isChecked)
					{
						// Add the MinTime bit, preserving any previous other rules
						member.ZCount = rul | RULE_MINTIME;
					}
					else
					{
						// Erase the MinTime bit by setting it to just the other rule (if any)
						member.ZCount = rul;
					}
				}
				if (newRule == RULE_NOCHANGE)
				{
					if (isChecked)
					{
						// Clear any other rules including exclusive Dim, Trim, and OnOff, as well as Mintime
						member.ZCount = RULE_NOCHANGE;
					}
					else
					{
						// You can't turn off nothing, so what do we do here?
						// Assume clear
						member.ZCount = RULE_NOCHANGE;
					}
				}

				if (member.MemberType == LOR4MemberType.RGBChannel)
				{
					// If an RGB channel, copy it to the 3 subchannels
					LOR4RGBChannel rgbCh = (LOR4RGBChannel)member;
					rgbCh.redChannel.ZCount = rgbCh.ZCount;
					rgbCh.grnChannel.ZCount = rgbCh.ZCount;
					rgbCh.bluChannel.ZCount = rgbCh.ZCount;
				}
				// If a group or a track, we need to recurse
				if (member.MemberType == LOR4MemberType.ChannelGroup)
				{
					// Recurse!!
					LOR4ChannelGroup group = (LOR4ChannelGroup)member;
					for (int m = 0; m < group.Members.Count; m++)
					{
						iLOR4Member subMember = group.Members[m];
						SetMemberRules(subMember, newRule, isChecked);
					}
				}
				if (member.MemberType == LOR4MemberType.Track)
				{
					// Recurse!!
					LOR4Track track = (LOR4Track)member;
					for (int m = 0; m < track.Members.Count; m++)
					{
						iLOR4Member subMember = track.Members[m];
						SetMemberRules(subMember, newRule, isChecked);
					}
				}
			}
			return ret;
		}

		private CheckState GroupCheckState(LOR4Membership members, int theRule)
		{
			CheckState ret = CheckState.Unchecked;
			int countTotal = 0;  // How many channels or rgbchannels-- so far
			int countRule = 0; // how many of them-- so far-- have this rule
												 // as soon as we get out of balance we will exit, so these are NOT complete counts

			// Loop thru all members (duh)
			for (int m = 0; m < members.Count; m++)
			{
				// Get current member
				iLOR4Member member = members[m];
				// If channel or rgb channel, and thus no children
				if ((member.MemberType == LOR4MemberType.Channel) ||
					(member.MemberType == LOR4MemberType.RGBChannel))
				{
					// Raise total count
					countTotal++;
					// If one of the 3 exclusive rules
					if ((theRule == RULE_DIM) ||
						(theRule == RULE_TRIM) ||
						(theRule == RULE_ONOFF))
					{
						// mask out MinTime
						int mr = member.ZCount & 3;
						// Does rule match
						if (mr == theRule)
						{
							// Yep, matches, increase that counter
							countRule++;
							// If ALL of them so far have matched
							if (countRule == countTotal)
							// Then this parent will be checked (so far)
							{ ret = CheckState.Checked; }
							else
							{
								// else if a previous one(s) didn't match, and now we have have a match
								// we have a unbalanced sichiation, so return indetermine and exit the loop to avoid wasting in more time
								ret = CheckState.Indeterminate;
								m = members.Count; // Loopus Interruptus
							}
						}
					}
					else
					{
						// If the MinTime rule which is not exclusive
						if (theRule == RULE_MINTIME)
						{
							// mask out other rules
							int mr = member.ZCount & 4;
							// does rule match?
							if (mr == theRule)
							{
								// Yes Sir, increase counter
								countRule++;
								// If ALL of them so far have matched
								if (countRule == countTotal)
								// Then this parent will be checked (so far)
								{ ret = CheckState.Checked; }
								else
								{
									// else if a previous one(s) didn't match, and now we have have a match
									// we have a unbalanced sichiation, so return indetermine and exit the loop to avoid wasting in more time
									ret = CheckState.Indeterminate;
									m = members.Count; // Loopus Interruptus
								}
							}
						}
						else
						{
							// If the rule is NoChange, which is exclusive to all rules including MinTime
							if (theRule == RULE_NOCHANGE)
							{
								// NO masking
								int mr = member.ZCount;
								// Does it match the rule number (NoChange) (reminder: its zero by the way)
								if (mr == theRule)
								{
									// We got us another
									countRule++;
									// All of 'em so far?
									if (countRule == countTotal)
									{ ret = CheckState.Checked; }
									else
									{
										// Lost our balance and fell down
										ret = CheckState.Indeterminate;
										m = members.Count; // Loopus Interruptus
									}
								} // End if member rule == The NoChange Rule
							} // End if The Rule is NoChange
						} // End if The Rule is MinTime, or not
					} // End if The Rule is Dim, Trim, or OnOff
				}
				else // Member is not channel or rgb channel
						 // So by process of elimination must be group or track
				{
					// if a group
					if (member.MemberType == LOR4MemberType.ChannelGroup)
					{
						// type cast it so we can get its members
						LOR4ChannelGroup group = (LOR4ChannelGroup)member;
						// Run a test on them-- RECURSE!
						CheckState grpState = GroupCheckState(group.Members, theRule);
						// If the group is indeterminite, or doesn't match what we've got so far
						if ((grpState == CheckState.Indeterminate) ||
							(grpState != ret))
						{
							// That makes everything so far indetermint
							ret = CheckState.Indeterminate;
							// So lets get out of here and not waste any more time
							m = members.Count; // Loopus Interruptus
						}
					}
					else
					{
						// If a track (which is basically a group) follow same logic as group above
						if (member.MemberType == LOR4MemberType.Track)
						{
							LOR4Track track = (LOR4Track)member;
							CheckState trkState = GroupCheckState(track.Members, theRule);
							if ((trkState == CheckState.Indeterminate) ||
								(trkState != ret))
							{
								ret = CheckState.Indeterminate;
								m = members.Count; // Loopus Interruptus
							}
						} // End if a track
					} // end if a group, or not
				} // end if a channel or rgb channel, or not
			} // member loop
			return ret;
		}

		private void btnSaveChannels_Click(object sender, EventArgs e)
		{
			SaveMap();
		}

		private void SaveMap()
		{
			dlgFileSave.DefaultExt = EXT_DIMMAP;
			dlgFileSave.Filter = FILE_DIMMAP;
			dlgFileSave.FilterIndex = 0;
			string initDir = LOR4Admin.DefaultChannelConfigsPath;
			string initFile = "";
			if (filenameMap.Length > 4)
			{
				string pth = Path.GetFullPath(filenameMap);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(filenameMap))
				{
					initFile = Path.GetFileName(filenameMap);
				}
			}
			dlgFileSave.FileName = initFile;
			dlgFileSave.InitialDirectory = initDir;
			dlgFileSave.OverwritePrompt = false;
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.Title = "Save Channel Map As...";

			DialogResult dr = dlgFileSave.ShowDialog();
			if (dr == DialogResult.OK)
			{
				DialogResult ow = Fyle.SafeOverwriteFile(dlgFileSave.FileName);
				if (ow == DialogResult.Yes)
				{

					string mapTemp = System.IO.Path.GetTempPath();
					mapTemp += Path.GetFileName(dlgFileSave.FileName);
					int mapErr = SaveMap(mapTemp);
					if (mapErr == 0)
					{
						filenameMap = dlgFileSave.FileName;
						if (File.Exists(filenameMap))
						{
							//TODO: Add Exception Catch
							File.Delete(filenameMap);
						}
						File.Copy(mapTemp, filenameMap);
						File.Delete(mapTemp);
						dirtyMap = false;
						//btnSaveMap.Enabled = dirtyMap;
						txtFilenameMap.Text = Path.GetFileName(filenameMap);
						Properties.Settings.Default.filenameMap = filenameMap;
						Properties.Settings.Default.Save();
					} // end no errors saving map
				}
			} // end dialog result = OK
		} // end btnSaveMap Click event


		private void btnBrowseChannels_Click(object sender, EventArgs e)
		{
			BrowseForMap();
		}

		private bool MakeDirty(bool isDirty = true)
		{
			bool ret = dirtyMap; // Return previous state
			if (isDirty != dirtyMap) // did it change?
			{
				if (seqSource != null)
				{
					if (seqSource.Channels.Count > 0)
					{
						dirtyMap = isDirty;
						dirtySeq = isDirty;
						btnSaveMap.Enabled = isDirty;
						btnSaveSeq.Enabled = isDirty;
						string txt = "Dim-O-Rama - " + Path.GetFileNameWithoutExtension(filenameSource);
						if (isDirty)
						{
							txt += " (Modified)";
						}
						else
						{

						}
						this.Text = txt;
					}
				}
			}
			return ret;
		}



	}
}
