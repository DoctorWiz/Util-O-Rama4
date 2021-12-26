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
using System.Diagnostics;
using FileHelper;

namespace UtilORama4
{
	public partial class frmLaunch : Form
	{
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/launchorama";
		private string basePath = "";
		private bool inIDE = false;

		public frmLaunch()
		{
			InitializeComponent();
			inIDE = Fyle.InIDE;
			if (inIDE)
			{
				basePath = Path.GetDirectoryName( Application.ExecutablePath) + "\\";
			}
			else
			{
				basePath = Path.GetDirectoryName(Application.ExecutablePath);
				// Continue processing, until we get the base path for the Util-O-Rama source code

			}
		}

		private void frmLaunch_Load(object sender, EventArgs e)
		{
			RestoreFormPosition();
			GetTheControlsFromTheHeartOfTheSun();
		}

		private void frmLaunch_FormClosing(object sender, FormClosingEventArgs e)
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

		}

		private void GetTheControlsFromTheHeartOfTheSun()
		{

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
			//System.Diagnostics.Process.Start(helpPage);
			Fyle.LaunchFile(helpPage);
		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout();
			aboutBox.picIcon.Image = picAboutIcon.Image;
			aboutBox.Icon = this.Icon;
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void btnMap_Click(object sender, EventArgs e)
		{
			LaunchApp("map");
		}

		private void LaunchApp(string appName)
		{
			string app = "";
			// Prepare to activate!
			// If NOT running in the IDE
			//if (!Debugger.IsAttached)
			//{
			// get full path and app name and run it!
			//if (Fyle.InIDE)
			//{
				app = Application.ExecutablePath.ToLower();
				app = app.Replace("launch", appName);
			//}
			//else
			//{
			//	app = basePath + appName + "orama.exe";
			//}
			if (File.Exists(app))
			{
				Fyle.MakeNoise(Fyle.Noises.Activate);
				Fyle.LaunchFile(app);
			}
			else
			{
				Fyle.MakeNoise(Fyle.Noises.WahWahWah);
			}
			//}
		}

		private void btnMerge_Click(object sender, EventArgs e)
		{
			LaunchApp("merge");
		}

		private void btnSplit_Click(object sender, EventArgs e)
		{
			LaunchApp("split");
		}

		private void btnChannel_Click(object sender, EventArgs e)
		{
			LaunchApp("channel");
		}

		private void btnVamp_Click(object sender, EventArgs e)
		{
			LaunchApp("vamp");

		}

		private void btnCompare_Click(object sender, EventArgs e)
		{
			LaunchApp("compare");
		}

		private void btnTime_Click(object sender, EventArgs e)
		{
			LaunchApp("time");
		}

		private void btnRGB_Click(object sender, EventArgs e)
		{
			LaunchApp("rgb");
		}

		private void btnSparkle_Click(object sender, EventArgs e)
		{
			LaunchApp("sparkle");
		}

		private void btnDim_Click(object sender, EventArgs e)
		{
			LaunchApp("dim");
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			LaunchApp("info");
		}

		private void lblMap_Click(object sender, EventArgs e)
		{
			btnMap_Click(sender, e);
		}

		private void lblMerge_Click(object sender, EventArgs e)
		{
			btnMerge_Click(sender, e);
		}

		private void lblSplit_Click(object sender, EventArgs e)
		{
			btnSplit_Click(sender, e);
		}

		private void lblChannel_Click(object sender, EventArgs e)
		{
			btnChannel_Click(sender, e);
		}

		private void blbVamp_Click(object sender, EventArgs e)
		{
			btnVamp_Click(sender, e);
		}

		private void lblCompare_Click(object sender, EventArgs e)
		{
			btnCompare_Click(sender, e);
		}

		private void lblTime_Click(object sender, EventArgs e)
		{
			btnTime_Click(sender, e);
		}

		private void lblInfo_Click(object sender, EventArgs e)
		{
			btnInfo_Click(sender, e);
		}

		private void lblRGB_Click(object sender, EventArgs e)
		{
			btnRGB_Click(sender, e);
		}

		private void lblSparkle_Click(object sender, EventArgs e)
		{
			btnSparkle_Click(sender, e);
		}

		private void lblDim_Click(object sender, EventArgs e)
		{
			btnDim_Click(sender, e);
		}

		private void btnLight_Click(object sender, EventArgs e)
		{

		}

		private void lblLight_Click(object sender, EventArgs e)
		{
			btnLight_Click(sender, e);
		}

		public string WhereIsApp(string which)
		{
			string ret = "";
			switch(which)
			{

			}

			return ret;
		}


		private void picAboutIcon_Click(object sender, EventArgs e)
		{

		}
	}
}
