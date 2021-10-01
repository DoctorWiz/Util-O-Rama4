﻿using System;
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
using LORUtils4;
using FileHelper;

namespace UtilORama4
{
	public partial class frmDim : Form
	{
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/blankorama";

		// Tab index on GUI, do not confuse with Rule numbers below
		private const int TAB_DIM = 0;
		private const int TAB_TRIM = 1;
		private const int TAB_ONOFF = 2;
		private const int TAB_MINTIME = 3;
		private const int TAB_NOCHANGE = 4;
		// Rule number (not the same as the GUI tab number above)
		private const int RULE_NOCHANGE = 0;
		private const int RULE_DIM = 1;
		private const int RULE_TRIM = 2;
		private const int RULE_ONOFF = 4;
		private const int RULE_MINTIME = 8;
		private const int RULES_DIM_MINTIME = RULE_DIM + RULE_MINTIME;
		private const int RULES_TRIM_MINTIME = RULE_TRIM + RULE_MINTIME;
		private const int RULES_ONOFF_MINTIME = RULE_ONOFF + RULE_MINTIME;
		// For groups and tracks only, indicates child members are in various states
		private const int RULES_MIXED = 128;

		// Convert Tab Index to Rule   0          1          2           3             4
		private int[] TabRules = { RULE_DIM, RULE_TRIM, RULE_ONOFF, RULE_MINTIME, RULE_NOCHANGE };

		// Colors for the various possible rules
		public readonly Color COLOR_NOCHANGE = Color.Black;
		public readonly Color COLOR_DIM = Color.Red;
		public readonly Color COLOR_TRIM = Color.Lime;
		public readonly Color COLOR_ONOFF = Color.Blue;
		public readonly Color COLOR_MINTIME = Color.FromArgb(50, 50, 50); // Very Dark Gray
		public readonly Color COLOR_DIM_MINTIME = Color.DarkRed;
		public readonly Color COLOR_TRIM_MINTIME = Color.Green;
		public readonly Color COLOR_ONOFF_MINTIME = Color.DarkBlue;


		private string filenameSource = "";
		private string filenameDest = "";
		private string filenameMap = "";
		private bool firstShown = false;

		private LORSequence4 seqSource = null;
		private LORSequence4 seqDest = null;
		private List<TreeNodeAdv>[] nodesBySI = null; // new List<TreeNodeAdv>();


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
			tabChannels.Controls.AddRange(new System.Windows.Forms.Control[] { tabDim, tabTrim, tabOnOff, tabMinTime, tabNoChange });


			int y = tabChannels.Top + 4;
			txtDim.Location = new Point(42, y);
			this.Controls.SetChildIndex(txtDim, 0);
			txtTrim.Location = new Point(104, y);
			this.Controls.SetChildIndex(txtTrim, 1);
			txtTime.Location = new Point(209, y);
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

			int mh = txtFilenameChannels.Top + txtFilenameChannels.Height + txtFilenameDest.Height + 70;
			tabChannels.Height = ClientRectangle.Height - mh;
			int th = tabChannels.Height - picPreviewSource.Height - 62;
			treeSource.Height = th;
			picPreviewSource.Top = treeSource.Top + treeSource.Height + 6;
			lblFilenameDest.Top = tabChannels.Top + tabChannels.Height + 5;
			txtFilenameDest.Top = lblFilenameDest.Top + lblFilenameDest.Height + 2;
			btnBrowseDest.Top = txtFilenameDest.Top;

		}

		private void tabChannels_SelectedIndexChanged(object sender, EventArgs e)
		{
			tabChannels.SelectedTab.Controls.Add(lblTabFunction);
			tabChannels.SelectedTab.Controls.Add(treeSource);
			tabChannels.SelectedTab.Controls.Add(picPreviewSource);
			switch (tabChannels.SelectedIndex)
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
			UpdateChannelTree(tabChannels.SelectedIndex);
		}

		private void UpdateChannelTree(int selectedTab)
		{
			//switch (selectedTab)




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
			string initDir = lutils.DefaultSequencesPath;
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


			dlgFileOpen.Filter = lutils.FILT_OPEN_ANY;
			dlgFileOpen.DefaultExt = lutils.EXT_LMS;
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
			seqSource = new LORSequence4();
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
				TreeUtils.TreeFillChannels(treeSource, seqSource, ref nodesBySI, false, false);
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
			LORSequence4 seqNew = seqMaster;

			seqNew.info = seqSource.info;
			seqNew.info.filename = newSeqFileName;
			seqNew.LORSequenceType4 = seqSource.LORSequenceType4;
			seqNew.Centiseconds = seqSource.Centiseconds;
			seqNew.animation = seqSource.animation;
			seqNew.videoUsage = seqSource.videoUsage;

			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;

			seqNew.ClearAllEffects();

			//seqNew.TimingGrids = new List<LORTimings4>();
			foreach (LORTimings4 sourceGrid in seqSource.TimingGrids)
			{
				LORTimings4 newGrid = null;
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
					newGrid = seqNew.ParseTimingGrid(sourceGrid.LineOut());
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
					iLORMember4 newChild = seqNew.Members.BySavedIndex[newSI];
					if (newChild.MemberType == LORMemberType4.Channel)
					{
						//int sourceSI = mapMastToSrc[mapLoop];
						//iLORMember4 sourceChildMember = seqSource.Members.BySavedIndex[sourceSI];
						iLORMember4 sourceChildMember = mapMastToSrc[mapLoop];
						if (sourceChildMember.MemberType == LORMemberType4.Channel)
						{
							LORChannel4 sourceChannel = (LORChannel4)sourceChildMember;
							if (sourceChannel.effects.Count > 0)
							{
								LORChannel4 newChannel = (LORChannel4)newChild;
								newChannel.CopyEffects(sourceChannel.effects, false);
								newChannel.Centiseconds = sourceChannel.Centiseconds;
							} // end if effects.Count
						} // end if source obj is channel
					}
					else // newer obj is NOT a channel
					{
						if (newChild.MemberType == LORMemberType4.RGBChannel)
						{
							//int sourceSI = mapMastToSrc[mapLoop];
							//iLORMember4 sourceChildMember = seqSource.Members.BySavedIndex[sourceSI];
							iLORMember4 sourceChildMember = mapMastToSrc[mapLoop];
							if (sourceChildMember.MemberType == LORMemberType4.RGBChannel)
							{
								LORRGBChannel4 sourceRGBchannel = (LORRGBChannel4)sourceChildMember;
								LORRGBChannel4 masterRGBchannel = (LORRGBChannel4)newChild;
								masterRGBchannel.Centiseconds = sourceRGBchannel.Centiseconds;
								LORChannel4 sourceChannel = sourceRGBchannel.redChannel;
								LORChannel4 newChannel;
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
						else // newer obj is NOT an LORRGBChannel4
						{
							// Channel Group = do nothing (?)
							// LORTrack4 = do nothing (?)
							// Timing Grid = do nothing (?)
						} // end if newer obj is an LORRGBChannel4 (or not)
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

			foreach (LORChannel4 ch in seqMaster.Channels)
			{
				ch.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LORRGBChannel4 rc in seqMaster.RGBchannels)
			{
				rc.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LORChannelGroup4 cg in seqMaster.ChannelGroups)
			{
				cg.Centiseconds = seqSource.Centiseconds;
			}
			foreach (LORTrack4 tr in seqMaster.Tracks)
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

		private void SaveMap(string mapFile)
		{
			string lineOut = "";
			StreamWriter writer = new StreamWriter(mapFile);
			// DELIMA = "🗲";
			// DELIMB = "🧙";
			// DELIMC = "👍";
			// DELIMD = "🐕";
			// DELIME = "💡"
			
			// File Header
			lineOut = lutils.DELIME + " Util-O-Rama Dim-O-Rama Channel List " + lutils.DELIME;
			writer.WriteLine(lineOut);
			// Dim Section; Channels and RGB Channels to be dimmed or scaled, and by how much
			lineOut = lutils.DELIMC + "Dim" + lutils.DELIMC + txtDim.Text;
			writer.WriteLine(lineOut);
			// Channels to be dimmed or scaled, Saved Index and Name
			lineOut = lutils.DELIMA + "Channels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c=0; c < seqSource.Channels.Count; c++)
			{
				int q = seqSource.Channels[c].RuleID & RULE_DIM;
				if (q == RULE_DIM)
				{
					lineOut = seqSource.Channels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.Channels[c].Name;
					writer.WriteLine(lineOut);
				}
			}
			// RGB Channels to be dimmed or scaled, Saved Index and Name
			lineOut = lutils.DELIMA + "RGBChannels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.RGBchannels.Count; c++)
			{
				int q = seqSource.RGBchannels[c].RuleID & RULE_DIM;
				if (q == RULE_DIM)
				{
					lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.RGBchannels[c].Name;
					writer.WriteLine(lineOut);
				}
			}

			// Trim Section; Channels and RGB Channels to be trimmed or truncated, and by how much
			lineOut = lutils.DELIMC + "Trim" + lutils.DELIMC + txtTrim.Text;
			writer.WriteLine(lineOut);
			// Channels to be trimmed or truncated, Saved Index and Name
			lineOut = lutils.DELIMA + "Channels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.Channels.Count; c++)
			{
				int q = seqSource.Channels[c].RuleID & RULE_TRIM;
				if (q == RULE_TRIM)
				{
					lineOut = seqSource.Channels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.Channels[c].Name;
					writer.WriteLine(lineOut);
				}
			}
			// RGB Channels to be trimmed or truncated, Saved Index and Name
			lineOut = lutils.DELIMA + "RGBChannels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.RGBchannels.Count; c++)
			{
				int q = seqSource.RGBchannels[c].RuleID & RULE_TRIM;
				if (q == RULE_TRIM)
				{
					lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.RGBchannels[c].Name;
					writer.WriteLine(lineOut);
				}
			}

			// On-Off Section; Channels and RGB Channels to set strictly On or Off
			lineOut = lutils.DELIMC + "OnOff" + lutils.DELIMC;
			writer.WriteLine(lineOut);
			// Channels to be set on or off, Saved Index and Name
			lineOut = lutils.DELIMA + "Channels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.Channels.Count; c++)
			{
				int q = seqSource.Channels[c].RuleID & RULE_ONOFF;
				if (q == RULE_ONOFF)
				{
					lineOut = seqSource.Channels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.Channels[c].Name;
					writer.WriteLine(lineOut);
				}
			}
			// RGB Channels to be set strictly on or off
			lineOut = lutils.DELIMA + "RGBChannels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.RGBchannels.Count; c++)
			{
				int q = seqSource.RGBchannels[c].RuleID & RULE_ONOFF;
				if (q == RULE_ONOFF)
				{
					lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.RGBchannels[c].Name;
					writer.WriteLine(lineOut);
				}
			}

			// Minimum Time Section; Channels and RGB Channels to be left on or off for a minimum number of seconds, and how many
			lineOut = lutils.DELIMC + "MinTime" + lutils.DELIMC + txtTime.Text;
			writer.WriteLine(lineOut);
			// Channels to be be on or off for a minimum time
			lineOut = lutils.DELIMA + "Channels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.Channels.Count; c++)
			{
				int q = seqSource.Channels[c].RuleID & RULE_MINTIME;
				if (q == RULE_MINTIME)
				{
					lineOut = seqSource.Channels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.Channels[c].Name;
					writer.WriteLine(lineOut);
				}
			}
			// RGB Channels to be on or off for a minimum time
			lineOut = lutils.DELIMA + "RGBChannels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.RGBchannels.Count; c++)
			{
				int q = seqSource.RGBchannels[c].RuleID & RULE_ONOFF;
				if (q == RULE_ONOFF)
				{
					lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.RGBchannels[c].Name;
					writer.WriteLine(lineOut);
				}
			}

			// And finally, just for the benefit of us humans
			// Will be ignored when this file is read back in, written out only for reference and debugging by humans
			// No Change Section; Channels and RGB Channels to be left alone and get no changes
			lineOut = lutils.DELIMC + "NoChange" + lutils.DELIMC;
			writer.WriteLine(lineOut);
			// Channels to get no changes and be left alone
			lineOut = lutils.DELIMA + "Channels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.Channels.Count; c++)
			{
				if (seqSource.Channels[c].RuleID == RULE_NOCHANGE)
				{
					lineOut = seqSource.Channels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.Channels[c].Name;
					writer.WriteLine(lineOut);
				}
			}
			// RGB Channels to be ignored, left alone, not changed
			lineOut = lutils.DELIMA + "RGBChannels" + lutils.DELIMA;
			writer.WriteLine(lineOut);
			for (int c = 0; c < seqSource.RGBchannels.Count; c++)
			{
				if(seqSource.RGBchannels[c].RuleID == RULE_NOCHANGE)
				{
					lineOut = seqSource.RGBchannels[c].SavedIndex.ToString() + lutils.DELIM5 + seqSource.RGBchannels[c].Name;
					writer.WriteLine(lineOut);
				}
			}

			// Again, just for us easily confused humans...
			lineOut = lutils.DELIME + " (End of File) " + lutils.DELIME;
			writer.WriteLine(lineOut);

			writer.Close();
			filenameMap = mapFile;
			Properties.Settings.Default.filenameMap = filenameMap;
			Properties.Settings.Default.Save();

		}

		private void BrowseForMap()
		{








			/*
			dlgFileOpen.DefaultExt = lutils.EXT_CHMAP;
			dlgFileOpen.Filter = lutils.FILE_CHMAP;
			dlgFileOpen.FilterIndex = 0;
			string initDir = lutils.DefaultChannelConfigsPath;
			string initFile = "";
			if (mapFile.Length > 4)
			{
				string pth = Path.GetDirectoryName(mapFile);
				if (Directory.Exists(pth))
				{
					initDir = pth;
				}
				if (File.Exists(mapFile))
				{
					initFile = Path.GetFileName(mapFile);
				}
			}
			dlgFileOpen.FileName = initFile;
			dlgFileOpen.InitialDirectory = initDir;
			dlgFileOpen.CheckPathExists = true;
			dlgFileOpen.Title = "Load-Apply Channel Map..";

			DialogResult dr = dlgFileOpen.ShowDialog();
			if (dr == DialogResult.OK)
			{
				mapFile = dlgFileOpen.FileName;
				txtMappingFile.Text = Path.GetFileName(mapFile);
				Properties.Settings.Default.LastMapFile = mapFile;
				Properties.Settings.Default.Save();
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.Save();
				if (seqMaster.Members.Items.Count > 1)
				{
					if (seqSource.Members.Items.Count > 1)
					{
						LoadApplyMapFile(mapFile);
					}
				}
				dirtyMap = false;
				btnSaveMap.Enabled = dirtyMap;
				mnuSaveNewMap.Enabled = dirtyMap;
			} // end dialog result = OK
			*/
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
					if (lineIn.Substring(0, 1) == lutils.DELIME)
					{
						int ipos = lineIn.IndexOf("Dim-O-Rama ");
						if (ipos > 0)
						{
							int rule = RULE_NOCHANGE;
							int percent = 0;
							bool rgb = false;
							string[] parts = null;
							int si = lutils.UNDEFINED;
							string name = "";
							LORChannel4 chan = null;
							LORRGBChannel4 rgbchan = null;
							// Keep reading lines until the end
							while (!reader.EndOfStream)
							{
								lineIn = reader.ReadLine();
								ipos = lineIn.IndexOf(lutils.DELIMA + "Channels" + lutils.DELIMA);
								if (ipos == 0)
								{ rgb = false; }
								else
								{
									ipos = lineIn.IndexOf(lutils.DELIMA + "RGBChannels" + lutils.DELIMA);
									if (ipos == 0)
									{ rgb = true; }
									else
									{
										ipos = lineIn.IndexOf(lutils.DELIMC + "Dim" + lutils.DELIMC);
										if (ipos == 0)
										{
											rule = RULE_DIM;
											string pct = lineIn.Substring(5);
											percent = int.Parse(pct);
											txtDim.Text = percent.ToString();
										}
										else
										{
											ipos = lineIn.IndexOf(lutils.DELIMC + "Trim" + lutils.DELIMC);
											if (ipos == 0)
											{
												rule = RULE_TRIM;
												string pct = lineIn.Substring(6);
												percent = int.Parse(pct);
												txtTrim.Text = percent.ToString();
											}
											else
											{
												ipos = lineIn.IndexOf(lutils.DELIMC + "OnOff" + lutils.DELIMC);
												if (ipos == 0)
												{
													rule = RULE_ONOFF;
												}
												else
												{
													ipos = lineIn.IndexOf(lutils.DELIMC + "MinTime" + lutils.DELIMC);
													if (ipos == 0)
													{
														rule = RULE_DIM;
														string pct = lineIn.Substring(9);
														percent = int.Parse(pct);
														txtDim.Text = percent.ToString();
													}
													else
													{
														ipos = lineIn.IndexOf(lutils.DELIMC + "NoChange" + lutils.DELIMC);
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
										parts = lineIn.Split(lutils.DELIM5);
										si = int.Parse(parts[0]);
										name = parts[1];
										if (rgb)
										{
											rgbchan = FindRGBChannel(si, name);
											if (rgbchan != null)
											{
												rgbchan.RuleID = rgbchan.RuleID | rule;
											}
										}
										else
										{
											chan = FindChannel(si, name);
											if (chan != null)
											{
												chan.RuleID = chan.RuleID | rule;
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

		private LORChannel4 FindChannel(int savedIndex, string name)
		{
			LORChannel4 chan = null;
			try
			{
				// Is saved index within valid range
				if (savedIndex <= seqSource.Members.HighestSavedIndex)
				{
					// Get the member at that saved index
					iLORMember4 member = seqSource.Members.BySavedIndex[savedIndex];
					// did we get Something, and is that something a channel?
					if (member != null)
					{
						if (member.MemberType == LORMemberType4.Channel	)
						{
							// So far so good!  Now, does the name match ?
							// Case insensitive
							int cmp = name.ToLower().CompareTo(member.Name.ToLower());
							// Case sensitive
							// int cmp = name.CompareTo(member.Name);
							if (cmp == 0)
							{
								// Got it!  Return it!
								chan = (LORChannel4)member;
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

		private LORRGBChannel4 FindRGBChannel(int savedIndex, string name)
		{
			LORRGBChannel4 rgbChan = null;
			try
			{
				// Is saved index within valid range
				if (savedIndex <= seqSource.Members.HighestSavedIndex)
				{
					// Get the member at that saved index
					iLORMember4 member = seqSource.Members.BySavedIndex[savedIndex];
					// did we get Something, and is that something a RGB Channel?
					if (member != null)
					{
						if (member.MemberType == LORMemberType4.RGBChannel)
						{
							// So far so good!  Now, does the name match ?
							// Case insensitive
							int cmp = name.ToLower().CompareTo(member.Name.ToLower());
							// Case sensitive
							// int cmp = name.CompareTo(member.Name);
							if (cmp == 0)
							{
								// Got it!  Return it!
								rgbChan = (LORRGBChannel4)member;
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
			int sourceSI = lutils.UNDEFINED;
			int masterSI = lutils.UNDEFINED;
			LORMemberType4 sourceType = LORMemberType4.None;
			LORMemberType4 masterType = LORMemberType4.None;
			iLORMember4 masterMember = null;
			iLORMember4 newSourceMember = null;
			iLORMember4 foundID = null;
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
						li = lutils.FastIndexOf(lineIn, TABLEchMap);
						if (li != 1)
						{
							errorStatus = 102;
						}
						else
						{
							int sfv = lutils.getKeyValue(lineIn, LORSeqInfo4.FIELDsaveFileVersion);
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
					//li = lineIn.IndexOf(lutils.STTBL + TABLEchannels + lutils.ENDTBL);
					li = lutils.FastIndexOf(lineIn, (lutils.STTBL + TABLEchannels + lutils.ENDTBL));
					if (li > 0)
					{
						if (!reader.EndOfStream)
						{
							// Next line is the Master LORChannel4
							lineIn = reader.ReadLine();
							lineNum++;
							//li = lineIn.IndexOf(FIELDmasterChannel);
							li = lutils.FastIndexOf(lineIn, FIELDmasterChannel);
							if (li > 0)
							{
								masterName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
								masterSI = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
								masterType = LORSeqEnums4.EnumMemberType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
								//lblMessage.Text = "Map \"" + masterName + "\" to...";
								//pnlMessage.Refresh();

								// Next line (let's assume its there) is the Source LORChannel4
								if (!reader.EndOfStream)
								{
									// Next line is the Source LORChannel4
									lineIn = reader.ReadLine();
									lineNum++;
									//li = lineIn.IndexOf(FIELDsourceChannel);
									li = lutils.FastIndexOf(lineIn, FIELDsourceChannel);
									if (li > 0)
									{
										sourceName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
										sourceSI = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
										sourceType = LORSeqEnums4.EnumMemberType(lutils.getKeyWord(lineIn, lutils.FIELDtype));

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
						//li = lineIn.IndexOf(lutils.STTBL + TABLEchannels + lutils.ENDTBL);
						li = lutils.FastIndexOf(lineIn, lutils.STTBL + TABLEchannels + lutils.ENDTBL);
						if (li > 0)
						{
							if (!reader.EndOfStream)
							{
								// Next line is the Master LORChannel4
								lineIn = reader.ReadLine();
								lineNum++;
								//li = lineIn.IndexOf(FIELDmasterChannel);
								li = lutils.FastIndexOf(lineIn, FIELDmasterChannel);
								if (li > 0)
								{
									masterName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
									//lblMessage.Text = "Map \"" + masterName + "\" to...";
									//pnlMessage.Refresh();
									masterSI = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
									masterType = LORSeqEnums4.EnumMemberType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
									// Next line (let's assume its there) is the Source LORChannel4
									if (!reader.EndOfStream)
									{
										// Next line is the Source LORChannel4
										lineIn = reader.ReadLine();
										lineNum++;
										//li = lineIn.IndexOf(FIELDsourceChannel);
										li = lutils.FastIndexOf(lineIn, FIELDsourceChannel);
										if (li > 0)
										{
											sourceName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
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
											sourceSI = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
											sourceType = LORSeqEnums4.EnumMemberType(lutils.getKeyWord(lineIn, lutils.FIELDtype));

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
													if (masterSI > lutils.UNDEFINED)
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
																if (sourceSI > lutils.UNDEFINED)
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
			dlgFileSave.Filter = lutils.FILT_SAVE_EITHER;
			dlgFileSave.FilterIndex = 0;
			if (xt.CompareTo(lutils.EXT_LAS) == 0) dlgFileSave.FilterIndex = 1;
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
					iLORMember4 member = (iLORMember4)treeSource.SelectedNode.Tag;
					if (member.MemberType == LORMemberType4.Channel)
					{
						Bitmap img = lutils.RenderEffects(member, 0, member.Centiseconds, picPreviewSource.Width, picPreviewSource.Height, true);
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
						if (member.MemberType == LORMemberType4.RGBChannel)
						{
							Bitmap img = lutils.RenderEffects(member, 0, member.Centiseconds, picPreviewSource.Width, picPreviewSource.Height, false);
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

		private int ScaleChannel(iLORMember4 member, int percent, bool force = false)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch(member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LORMemberType4.Channel:
					LORChannel4 chan = (LORChannel4)member;
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
							LOREffect4 effect = chan.effects[e];
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
							LOREffect4 effect = chan.effects[e];
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
				case LORMemberType4.RGBChannel:
					LORRGBChannel4 rgb = (LORRGBChannel4)member;
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
							LOREffect4 effect = rgb.redChannel.effects[e];
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
								LOREffect4 effect = rgb.grnChannel.effects[e];
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
								LOREffect4 effect = rgb.bluChannel.effects[e];
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
				case LORMemberType4.ChannelGroup:
					LORChannelGroup4 group = (LORChannelGroup4)member;
					foreach(iLORMember4 subMember in group.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LORMemberType4.Cosmic:
					LORCosmic4 cosmic = (LORCosmic4)member;
					foreach (iLORMember4 subMember in cosmic.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LORMemberType4.Track:
					LORTrack4 track = (LORTrack4)member;
					foreach (iLORMember4 subMember in track.Members)
					{
						// Recurse!
						count += ScaleChannel(subMember, percent, force);
					}
					break;
			}


			return count;
		}

		private int TrimChannel(iLORMember4 member, int percent)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch(member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LORMemberType4.Channel:
					LORChannel4 chan = (LORChannel4)member;
					// Loop thru all effects
					for (int e = 0; e < chan.effects.Count; e++)
					{
						LOREffect4 effect = chan.effects[e];
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
				case LORMemberType4.RGBChannel:
					LORRGBChannel4 rgb = (LORRGBChannel4)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					TrimChannel(rgb.redChannel, percent);
					TrimChannel(rgb.grnChannel, percent);
					TrimChannel(rgb.bluChannel, percent);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LORMemberType4.ChannelGroup:
					LORChannelGroup4 group = (LORChannelGroup4)member;
					foreach(iLORMember4 subMember in group.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LORMemberType4.Cosmic:
					LORCosmic4 cosmic = (LORCosmic4)member;
					foreach (iLORMember4 subMember in cosmic.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LORMemberType4.Track:
					LORTrack4 track = (LORTrack4)member;
					foreach (iLORMember4 subMember in track.Members)
					{
						// Recurse!
						count += TrimChannel(subMember, percent);
					}
					break;
			}


			return count;
		}

		private int OnOffChannel(iLORMember4 member)
		{
			// Returns the count of how many effects actually needed scaling
			int count = 0;
			bool needed = false;
			switch (member.MemberType)
			{
				//////////////
				// CHANNEL //
				////////////
				case LORMemberType4.Channel:
					LORChannel4 chan = (LORChannel4)member;
					// Loop thru all effects
					for (int e = 0; e < chan.effects.Count; e++)
					{
						LOREffect4 effect = chan.effects[e];
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
				case LORMemberType4.RGBChannel:
					LORRGBChannel4 rgb = (LORRGBChannel4)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					OnOffChannel(rgb.redChannel);
					OnOffChannel(rgb.grnChannel);
					OnOffChannel(rgb.bluChannel);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LORMemberType4.ChannelGroup:
					LORChannelGroup4 group = (LORChannelGroup4)member;
					foreach (iLORMember4 subMember in group.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LORMemberType4.Cosmic:
					LORCosmic4 cosmic = (LORCosmic4)member;
					foreach (iLORMember4 subMember in cosmic.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LORMemberType4.Track:
					LORTrack4 track = (LORTrack4)member;
					foreach (iLORMember4 subMember in track.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;
			}


			return count;
		}

		private int MinTimeChannel(iLORMember4 member, int seconds)
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
				case LORMemberType4.Channel:
					LORChannel4 chan = (LORChannel4)member;
					LOREffect4 thiseffect = null;
					LOREffect4 nexteffect = null;
					// Loop thru all but last effects
					for (int e = 0; e < (chan.effects.Count-1); e++)
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
						chan.effects.RemoveAt(chan.effects.Count-1);
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
				case LORMemberType4.RGBChannel:
					LORRGBChannel4 rgb = (LORRGBChannel4)member;
					// For RGB Channels, we need to check all 3 subchannels (Red, Grn, Blu)
					OnOffChannel(rgb.redChannel);
					OnOffChannel(rgb.grnChannel);
					OnOffChannel(rgb.bluChannel);
					break;

				////////////////////
				// CHANNEL GROUP //
				//////////////////
				case LORMemberType4.ChannelGroup:
					LORChannelGroup4 group = (LORChannelGroup4)member;
					foreach (iLORMember4 subMember in group.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////////////
				// COSMIC DEVICE //   (Treat the same as a Channel Group)
				//////////////////
				case LORMemberType4.Cosmic:
					LORCosmic4 cosmic = (LORCosmic4)member;
					foreach (iLORMember4 subMember in cosmic.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;

				////////////
				// TRACK //   (Effectively a group, so treat it like one)
				//////////
				case LORMemberType4.Track:
					LORTrack4 track = (LORTrack4)member;
					foreach (iLORMember4 subMember in track.Members)
					{
						// Recurse!
						count += OnOffChannel(subMember);
					}
					break;
			}


			return count;
		}




		private void treeSource_AfterCheck(object sender, TreeNodeAdvEventArgs e)
		{
			TreeNodeAdv node = e.Node;
			if (node != null)
			{
				bool chk = node.Checked;
				CheckNode(node, chk, tabChannels.SelectedIndex);
			}
		}

		private void CheckNode(TreeNodeAdv node, bool isChecked, int tabIndex)
		{
			CheckState newState = node.CheckState;
			if (node != null)
			{
				if (node.Tag != null)
				{
					iLORMember4 member = (iLORMember4) node.Tag;
					LORMemberType4 memberType = member.MemberType;
					int mtime = member.RuleID & RULE_MINTIME;
					//bool minTime = ((member.RuleID & RULE_MINTIME) == RULE_MINTIME);
					//int baseRule = (member.RuleID & 7);
					if (memberType == LORMemberType4.Channel)
					{
						if (isChecked)
						{
							// Check-ing Dim, Trim, or On-Off
							if (tabIndex < 3)
							{
								// Is MinTime set (need to preserve)
								// Add the rule, plus MinTime if set
								member.RuleID = TabRules[tabIndex] | mtime;
							}
							else
							{
								// Check-ing MinTime
								if (tabIndex == 3)
								{
									// Add MinTime rule to any other rules already there
									member.RuleID = member.RuleID | RULE_MINTIME;
								}
								else
								{
									// Set to 'NoChange' state
									if (tabIndex == 4)
									{
										member.RuleID = RULE_NOCHANGE;
									} // end if Tab 4 (NoChange)
								} // end if Tab 3 (MinTime), or not...
							} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
						}
						else // UN-checked
						{
							// Un-Check-ing Dim, Trim, or On-Off
							if (tabIndex < 3)
							{
								// Is MinTime set (need to preserve)
								// clear the rule, keep MinTime if set
								member.RuleID = mtime;
							}
							else
							{
								// Un-Check-ing MinTime
								if (tabIndex == 3)
								{
									// clear MinTime rule preserving Dim, Trim or On-Off if set
									member.RuleID = member.RuleID & 7;
								}
								else
								{
									// Un-check 'NoChange' state ?!?
									if (tabIndex == 4)
									{
										// Cannot be unchecked (as long as no other rules are set)
										if (member.RuleID == RULE_NOCHANGE)
										{
											node.Checked = true;
										}
									} // end if Tab 4 (NoChange)
								} // end if Tab 3 (MinTime), or not...
							} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
						} // end if checked, or not...
					}
					else // Not a Channel (something else)
					{
						if (memberType == LORMemberType4.RGBChannel)
						{
							LORRGBChannel4 rgbChan = (LORRGBChannel4)member;
							if (isChecked)
							{
								// Check-ing Dim, Trim, or On-Off
								if (tabIndex < 3)
								{
									// Is MinTime set (need to preserve)
									// Add the rule, plus MinTime if set
									rgbChan.RuleID = TabRules[tabIndex] | mtime;
								}
								else
								{
									// Check-ing MinTime
									if (tabIndex == 3)
									{
										// Add MinTime rule to any other rules already there
										rgbChan.RuleID = member.RuleID | RULE_MINTIME;
									}
									else
									{
										// Set to 'NoChange' state
										if (tabIndex == 4)
										{
											rgbChan.RuleID = RULE_NOCHANGE;
										} // end if Tab 4 (NoChange)
									} // end if Tab 3 (MinTime), or not...
								} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
							}
							else // UN-checked
							{
								// Un-Check-ing Dim, Trim, or On-Off
								if (tabIndex < 3)
								{
									// Is MinTime set (need to preserve)
									// clear the rule, keep MinTime if set
									rgbChan.RuleID = mtime;
								}
								else
								{
									// Un-Check-ing MinTime
									if (tabIndex == 3)
									{
										// clear MinTime rule preserving Dim, Trim or On-Off if set
										rgbChan.RuleID = member.RuleID & 7;
									}
									else
									{
										// Un-check 'NoChange' state ?!?
										if (tabIndex == 4)
										{
											// Cannot be unchecked (as long as no other rules are set)
											if (member.RuleID == RULE_NOCHANGE)
											{
												node.Checked = true;
											}
										} // end if Tab 4 (NoChange)
									} // end if Tab 3 (MinTime), or not...
								} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
							} // end if checked, or not...
							// Copy Rule State to the RGB Channels 3 subchannels
							rgbChan.redChannel.RuleID = rgbChan.RuleID;
							rgbChan.grnChannel.RuleID = rgbChan.RuleID;
							rgbChan.bluChannel.RuleID = rgbChan.RuleID;
						}
						else // Not a  RGBChannel (something else)  by process of elimination should be a Group or Track
						{
							if ((memberType == LORMemberType4.ChannelGroup) ||
									(memberType == LORMemberType4.Track))
							{
								if (isChecked)
								{
									// Check-ing Dim, Trim, or On-Off
									if (tabIndex < 3)
									{
										// Is MinTime set (need to preserve)
										// Add the rule, plus MinTime if set
										member.RuleID = TabRules[tabIndex] | mtime;
									}
									else
									{
										// Check-ing MinTime
										if (tabIndex == 3)
										{
											// Add MinTime rule to any other rules already there
											member.RuleID = member.RuleID | RULE_MINTIME;
										}
										else
										{
											// Set to 'NoChange' state
											if (tabIndex == 4)
											{
												member.RuleID = RULE_NOCHANGE;
											} // end if Tab 4 (NoChange)
										} // end if Tab 3 (MinTime), or not...
									} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
								}
								else // UN-checked
								{
									// Un-Check-ing Dim, Trim, or On-Off
									if (tabIndex < 3)
									{
										// Is MinTime set (need to preserve)
										// clear the rule, keep MinTime if set
										member.RuleID = mtime;
									}
									else
									{
										// Un-Check-ing MinTime
										if (tabIndex == 3)
										{
											// clear MinTime rule preserving Dim, Trim or On-Off if set
											member.RuleID = member.RuleID & 7;
										}
										else
										{
											// Un-check 'NoChange' state ?!?
											if (tabIndex == 4)
											{
												// Cannot be unchecked (as long as no other rules are set)
												if (member.RuleID == RULE_NOCHANGE)
												{
													node.Checked = true;
												}
											} // end if Tab 4 (NoChange)
										} // end if Tab 3 (MinTime), or not...
									} // end if Tab 0,1, or 2, (Dim, Trim, OnOff) or not...
								} // end if checked, or not...
									// Copy Rule State to its members
								if (member.MemberType == LORMemberType4.ChannelGroup)
								{
									LORChannelGroup4 group = (LORChannelGroup4)member;
									SetMemberRules(group.Members, TabRules[tabIndex], isChecked);
								}
								if (member.MemberType == LORMemberType4.Track)
								{
									LORTrack4 track = (LORTrack4)member;
									SetMemberRules(track.Members, TabRules[tabIndex], isChecked);
								}
							}
							else // Not a group or track
							{
								// What the fuck is it then
								string msg = "What kind of member are you trying to change the check state for?";
								LORMemberType4 memType = member.MemberType;
								Fyle.BUG(msg);
							} // end if group or track, or not...
						} // end if RGB Channel, or not...
					} // end if a Channel, or not...
				} // end if node.Tag ain't null
			} // end if node ain't null
		}

		private int SetMemberRules(LORMembership4 membership, int newRule, bool isChecked)
		{
			int count = 0;
			for (int m=0; m< membership.Items.Count ; m++)
			{
				iLORMember4 member = membership.Members[m];
				//! LEFT OFF HERE!
			}
			return count;
		}



	}
}
