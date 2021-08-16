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
using LORUtils; using FileHelper;
using FuzzyString;
using Syncfusion.Windows.Forms.Tools;

namespace UtilORama4
{
	public partial class frmRemapper : Form
	{
		private bool firstShown = false;
		private Control currentToolTipControl = null;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;

		public frmRemapper()
		{
			InitializeComponent();
		}

		private void InitForm()
		{
			bool valid = false;
			SeqFolder = utils.DefaultSequencesPath;
			logHomeDir = Fyle.GetAppTempFolder();

			masterFile = Properties.Settings.Default.LastMasterFile;
			if (masterFile.Length > 6)
			{
				valid = Fyle.IsValidPath(masterFile, true);
			}
			if (!valid) masterFile = utils.DefaultChannelConfigsPath;
			if (!File.Exists(masterFile))
			{
				masterFile = utils.DefaultChannelConfigsPath;
				Properties.Settings.Default.LastMasterFile = masterFile;
			}

			valid = false;
			sourceFile = Properties.Settings.Default.LastSourceFile;
			if (sourceFile.Length > 6)
			{
				valid = Fyle.IsValidPath(sourceFile, true);
			}
			if (!valid) sourceFile = utils.DefaultSequencesPath;
			if (!File.Exists(sourceFile))
			{
				sourceFile = utils.DefaultSequencesPath;
				Properties.Settings.Default.LastSourceFile = sourceFile;
			}

			valid = false;
			mapFile = Properties.Settings.Default.LastMapFile;
			if (mapFile.Length > 6)
			{
				valid = Fyle.IsValidPath(mapFile, true);
			}
			if (!valid) mapFile = utils.DefaultChannelConfigsPath;
			if (!File.Exists(mapFile))
			{
				mapFile = utils.DefaultChannelConfigsPath;
			}

			valid = false;
			saveFile = Properties.Settings.Default.LastSaveFile;
			if (saveFile.Length > 6)
			{
				valid = Fyle.IsValidPath(saveFile, true);
			}
			if (!valid) saveFile = utils.DefaultSequencesPath;
			if (!File.Exists(saveFile))
			{
				saveFile = utils.DefaultSequencesPath;
			}

			sourceOnRight = Properties.Settings.Default.SourceOnRight;
			ArrangePanes(sourceOnRight);

			Properties.Settings.Default.BasePath = basePath;
			Properties.Settings.Default.Save();

			chkAutoLaunch.Checked = Properties.Settings.Default.AutoLaunch;
			btnEaves.Visible = isWiz;

			RestoreFormPosition();

		} // end InitForm

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
				System.Media.SystemSounds.Beep.Play();

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
			Properties.Settings.Default.Upgrade();
			Properties.Settings.Default.Save();

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

		} // End RestoreFormPostion

		private void btnBrowseSource_Click(object sender, EventArgs e)
		{
			BrowseSourceFile();
		}

		private void btnBrowseMaster_Click(object sender, EventArgs e)
		{
			BrowseMasterFile();
		}

		private void btnMap_Click(object sender, EventArgs e)
		{
			NodesMapSelected();

		} // end btnMap_Click

		private void btnUnmap_Click(object sender, EventArgs e)
		{
			UnmapSelectedMembers();
		}

		private void frmRemapper_Shown(object sender, EventArgs e)
		{ }

		private void FirstShow()
		{
			string msg;
			DialogResult dr;

			ProcessCommandLine();
			if (batch_fileCount < 1)
			{
				if (File.Exists(masterFile))
				{
					msg = "Load last Destination Sequence file: '" + Path.GetFileName(masterFile) + "'?";
					dr = MessageBox.Show(this, msg, "Load Last Destination File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					//dr = DialogResult.Yes; //! For Testing
					if (dr == DialogResult.Yes)
					{
						LoadMasterFile(masterFile);
					}
				}
				if (File.Exists(sourceFile))
				{
					msg = "Load last Source Sequence file: '" + Path.GetFileName(sourceFile) + "'?";
					dr = MessageBox.Show(this, msg, "Load Last Source File?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					//dr = DialogResult.Yes; //! For Testing
					if (dr == DialogResult.Yes)
					{
						//seqSource.ReadSequenceFile(sourceFile);
						LoadSourceFile(sourceFile);
						//utils.TreeFillChannels(treeSource, seqSource, sourceNodesBySI, false, false);
						// Is a master also already loaded?
					}
				} // end last sequence file exists
					//! Remarked for Testing
				AskToMap();
			}
		} // end form shown event

		private void btnSummary_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmMapList dlgList = new frmMapList(seqSource, seqMaster, mapSrcToMast, mapMastToSrc, imlTreeIcons);
			//dlgList.Left = this.Left + 4;
			//dlgList.Top = this.Top + 25;
			Point l = new Point(4, 25);
			dlgList.Location = l;

			dlgList.ShowDialog(this);
			//DialogResult result = dlgList.ShowDialog();
			ImBusy(false);
		}

		private void frmRemapper_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}

		private void btnSaveMap_Click(object sender, EventArgs e)
		{
			SaveMap();
		}

		private void pnlAll_Paint(object sender, PaintEventArgs e)
		{

		}

		private void frmRemapper_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void btnAutoMap_Click(object sender, EventArgs e)
		{
			AutoMap();
		}

		private void btnLoadMap_Click(object sender, EventArgs e)
		{
			BrowseForMap();
		}
		private void frmRemapper_Paint(object sender, PaintEventArgs e)
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
			//Form aboutBox = new About();
			//aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void btnSaveNewSeq_Click(object sender, EventArgs e)
		{
			SaveNewMappedSequence();
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

		private void mnuOpenMaster_Click(object sender, EventArgs e)
		{
			BrowseMasterFile();
		}

		private void mnuOpenSource_Click(object sender, EventArgs e)
		{
			BrowseSourceFile();
		}

		private void mnuOpenMap_Click(object sender, EventArgs e)
		{
			BrowseForMap();
		}

		private void mnuSelect_Click(object sender, EventArgs e)
		{

		}

		private void mnuSourceLeft_Click(object sender, EventArgs e)
		{
			ArrangePanes(false);
		}

		private void mnuSourceRight_Click(object sender, EventArgs e)
		{
			ArrangePanes(true);
		}

		private void mnuMatchOptions_Click(object sender, EventArgs e)
		{
			frmOptions opt = new frmOptions();
			opt.ShowDialog(this);
		}

		private void mnuAutoMap_Click(object sender, EventArgs e)
		{
			AutoMap();
		}

		private void chkAutoLaunch_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.AutoLaunch = chkAutoLaunch.Checked;
			Properties.Settings.Default.Save();
		}

		private void lblDebug_Click(object sender, EventArgs e)
		{
			string msg = "GLB";
			msg += LeftBushesChildCount().ToString();
			lblDebug.Text = msg;
		}

		private void btnEaves_Click(object sender, EventArgs e)
		{

			//RGBchannel oef = (RGBchannel)seqSource.Members.Find("Strip 1 Pixel 150 [U2.448-450]", MemberType.RGBchannel, false);
			RGBchannel se = (RGBchannel)seqSource.Members.Find("Eave Pixel 001 / S1.170 / U2.508-510", MemberType.RGBchannel, false);
			RGBchannel me = (RGBchannel)seqMaster.Members.Find("Eave Pixel 001 / S1.170 / U2.508-510", MemberType.RGBchannel, false);

			int masIdx = me.Index;
			int srcIdx = se.Index; // = oef.Index;



			for (int l = 0; l < 600; l++)
			{
				int mSI = seqMaster.RGBchannels[masIdx].SavedIndex;
				int sSI = seqSource.RGBchannels[srcIdx].SavedIndex;
				MapMembers(mSI, sSI, true, true);
				masIdx++;
				srcIdx++;
			}


			se = (RGBchannel)seqSource.Members.Find("Eave Pixel 172 / S2.002 / U3.004-006", MemberType.RGBchannel, false);
			RGBchannel mf = (RGBchannel)seqMaster.Members.Find("Fence Pixel 01-01 [U11.496-498]", MemberType.RGBchannel, false);

			srcIdx = se.Index; // + 171;
			masIdx = mf.Index;
			for (int l = 0; l < 336; l++)
			{
				int mSI = seqMaster.RGBchannels[masIdx].SavedIndex;
				int sSI = seqSource.RGBchannels[srcIdx].SavedIndex;
				MapMembers(mSI, sSI, true, true);
				masIdx++;
				srcIdx++;
			}
		}

		private void treeMaster_AfterSelect(object sender, EventArgs e)
		{
			TreeNodeAdv newMasterNode = treeMaster.SelectedNode;
			NodeMaster_Click(newMasterNode);

		}

		private void treeSource_AfterSelect(object sender, EventArgs e)
		{
			TreeNodeAdv newSourceNode = treeSource.SelectedNode;
			SourceNode_Click(newSourceNode);

		} // end class frmRemapper

		private void frmRemapper_MouseMove(object sender, MouseEventArgs e)
		{
			/*
			Control control = GetChildAtPoint(e.Location);
			if (control != null)
			{
				string toolTipString = ttip.GetToolTip(control);
				this.ttip.ShowAlways = true;
				// trigger the tooltip with no delay and some basic positioning just to give you an idea
				ttip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
			}
			*/
		}

		private void pnlAll_MouseMove(object sender, MouseEventArgs e)
		{
			/*
			 * Control control = GetChildAtPoint(e.Location);
			if (control != null)
			{
				if (!control.Enabled && currentToolTipControl == null)
				{
					string toolTipString = ttip.GetToolTip(control);
					// trigger the tooltip with no delay and some basic positioning just to give you an idea
					ttip.Show(toolTipString, control, control.Width / 2, control.Height / 2);
					currentToolTipControl = control;
				}
			}
			else
			{
				if (currentToolTipControl != null) ttip.Hide(currentToolTipControl);
				currentToolTipControl = null;
			}
			*/

			var parent = sender as Control;
			if (parent == null)
			{
				return;
			}
			var ctrl = parent.GetChildAtPoint(e.Location);
			if (ctrl != null && !ctrl.Enabled)
			{
				if (ctrl.Visible && ttip.Tag == null)
				{
					var tipstring = ttip.GetToolTip(ctrl);
					ttip.Show(tipstring, ctrl, ctrl.Width / 2, ctrl.Height / 2);
					ttip.Tag = ctrl;
				}
			}
			else
			{
				ctrl = ttip.Tag as Control;
				if (ctrl != null)
				{
					ttip.Hide(ctrl);
					ttip.Tag = null;
				}
			}
		}

		private void treeMaster_Click(object sender, EventArgs e)
		{

		}
	}
}// end namespace UtilORama4
