using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UtilORama4
{
	public partial class frmOutputLog : Form
	{
		Form ownerForm = null;
		public frmOutputLog()
		{
			InitializeComponent();
		}

		public frmOutputLog(Form parentForm)
		{
			InitializeComponent();
			ownerForm = parentForm;
			RestoreFormPosition();
		}
		public string LogText
		{
			get
			{
				return txtOutput.Text;
			}
			set
			{
				txtOutput.Text = value;
				txtOutput.SelectionStart = txtOutput.TextLength;
				txtOutput.ScrollToCaret();
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
				myLoc = new Point(this.RestoreBounds.X - ownerForm.RestoreBounds.X,
													this.RestoreBounds.Y - ownerForm.RestoreBounds.Y);
				mySize = new Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
			}
			else
			{
				// Save relative to the onwer form
				// myLoc = new Point(this.Left - ownerForm.RestoreBounds.X,
				//									this.Top - ownerForm.RestoreBounds.Y);
				// Nevermind, it already IS relative to the owner form
			}

			// Save it for later!
			int x = this.Left;
			 
			Properties.Settings.Default.OutputLocation = myLoc;
			Properties.Settings.Default.OutputSize = mySize;
			Properties.Settings.Default.OutputWindowState = (int)myState;
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

			Point savedLoc = Properties.Settings.Default.OutputLocation;
			Size savedSize = Properties.Settings.Default.OutputSize;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)Properties.Settings.Default.OutputWindowState;

			// Set X and Y relative to the owner form
			// int x = ownerForm.RestoreBounds.X + savedLoc.X; // Default to saved postion and size, will override if necessary
			// int y = ownerForm.RestoreBounds.Y + savedLoc.Y;
			// Nevermind, it already IS relative to the owner form
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




		public bool Done
		{
			get
			{
				return btnClose.Enabled;
			}
			set
			{
				btnClose.Enabled = value;
				btnSaveLog.Enabled = value;
				if (value)
				{
					this.Cursor = Cursors.Default;
				}
				else
				{
					this.Cursor = Cursors.WaitCursor;
				}
			}
		}
		private void frmOutputLog_ResizeEnd(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				txtOutput.Height = ClientSize.Height - 40;
				btnClose.Top = ClientSize.Height - 30;
				btnClose.Left = ClientSize.Width / 2 - btnClose.Width - 16;
				btnSaveLog.Top = ClientSize.Height - 30;
				btnSaveLog.Left = ClientSize.Width / 2 + 16;
				string t = btnClose.Left.ToString() + ", " + btnSaveLog.Left.ToString();
				lblDebug.Text = t;
				lblDebug.Top = btnClose.Top;
			}
			if (this.WindowState == FormWindowState.Minimized)
			{
				// Handled in [regular] Resize event
				//ownerForm.WindowState = FormWindowState.Minimized;
			}
		}

		private void frmOutputLog_Load(object sender, EventArgs e)
		{

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			//this.Hide();
			//SaveFormPosition();
			this.Close(); //?
		}

		private void frmOutputLog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!btnClose.Enabled)
			{
				e.Cancel = true;
			}
			SaveFormPosition(); // Even if cancelling
			// Do Nothing?
		}

		private void btnSaveLog_Click(object sender, EventArgs e)
		{
			dlgSaveFile.Title = "Save Output Log File As...";
			dlgSaveFile.InitialDirectory = "shell:::{679F85CB-0220-4080-B29B-5540CC05AAB6}";
			dlgSaveFile.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt";
			dlgSaveFile.FilterIndex = 1;
			dlgSaveFile.DefaultExt = ".log";
			dlgSaveFile.FileName = "Sonic Annotator Output.log";
			dlgSaveFile.CheckPathExists = true;
			dlgSaveFile.OverwritePrompt = true;
			dlgSaveFile.SupportMultiDottedExtensions = true;
			dlgSaveFile.ValidateNames = true;
			//newFileIn = Path.GetFileNameWithoutExtension(fileCurrent) + " Part " + member.ToString(); // + ext;
			//newFileIn = "Part " + member.ToString() + " of " + Path.GetFileNameWithoutExtension(fileCurrent);
			//newFileIn = "Part Mother Fucker!!";
			//dlgSaveFile.FileName = initFile;
			DialogResult result = dlgSaveFile.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				StreamWriter writer = new StreamWriter(dlgSaveFile.FileName);
				writer.Write(txtOutput.Text);
				writer.Close();
			}
		}

		private void frmOutputLog_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				// Ignore for now, handle at ResizeEnd
			}
			if (this.WindowState == FormWindowState.Minimized)
			{
				ownerForm.WindowState = FormWindowState.Minimized;
			}

		}
	}
}
