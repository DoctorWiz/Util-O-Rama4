using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils4; using FileHelper;
using System.IO;
using System.Media;

namespace UtilORama4
{
	public partial class frmValidate : Form
	{
		LORSequence4 seq;
		string fileName = "";
		int errCount = 0;
		bool aborted = false;

		public frmValidate()
		{
			InitializeComponent();
		}

		private void frmFoobar_Load(object sender, EventArgs e)
		{

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
		} // End SaveFormPostion

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

		private void BrowseSourceFile()
		{
			string initDir = lutils.DefaultSequencesPath;
			string initFile = "";
			if (fileName.Length > 4)
			{
				string ldir = Path.GetDirectoryName(fileName);
				if (Directory.Exists(ldir))
				{
					initDir = ldir;
					if (File.Exists(fileName))
					{
						initFile = Path.GetFileName(fileName);
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
				int err = ValidateFile(dlgFileOpen.FileName);
				ImBusy(false);
			} // end if (result = DialogResult.OK)

		}

		public void ImBusy(bool busyState)
		{

		}

		public int ValidateFile(string theFile)
		{
			int errCount = 0;
			int warnCount = 0;
			int channelCount = 0;
			int groupCount = 0;
			int trackCount = 0;
			int rgbChannelCount = 0;
			int timingsCount = 0;
			int allCount = 0;
			int lineCount = 0;
			int highestSavedIndex = -1;
			string[] channelNames = null;
			string[] rgbChannelNames = null;
			string[] groupNames = null;
			string[] trackNames = null;
			string[] timingNames = null;
			string[] allNames = null;
			int param = -1;
			string theName = "";
			string logPath = Path.GetDirectoryName(theFile);
			string seqName = Path.GetFileNameWithoutExtension(theFile);
			string logFile = "Valid-O-Rama " + seqName + " analysis.txt";
			StringBuilder lineOut = new StringBuilder();
			bool channelsSection = false;
			bool tracksSection = false;
			bool timingsSection = false;
			bool effectsSection = false;
			bool groupList = false;
				
			
			
			StreamReader reader = new StreamReader(theFile);
			StreamWriter writer = new StreamWriter(logFile);
			while (!reader.EndOfStream)
			{
				string lineIn = reader.ReadLine();
				lineCount++;
				lineOut.Clear();

				if (lineCount == 1)
				{
					int cmp = lineIn.CompareTo("<?xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"?>");
					if (cmp != 0)
					{
						lineOut.Append("Line     1: (xml version) appears invalid!");
						errCount++;
						writer.WriteLine(lineOut);
					}
				}
				else
				{
					if (lineCount == 2)
					{
						string seqinfo = lineIn.Substring(0, 28);
						int cmp = seqinfo.CompareTo("<sequence saveFileVersion=\"");
						if (cmp != 0)
						{
							lineOut.Append("Line     2: <sequence> appears invalid!");
							errCount++;
							writer.WriteLine(lineOut);
							lineOut.Clear();
							//TODO Validate info in sequence info
						}
					}
					else
					{
						if (lineCount == 3)
						{
							int cmp = lineIn.CompareTo("	<channels>");
							if (cmp != 0)
							{
								lineOut.Append("Line     3: Start of <channels> table appears invalid!");
								errCount++;
								writer.WriteLine(lineOut);
							}
							else
							{
								channelsSection = true;
							}
						}
						else
						{
							// Lines 4 onward
							if (channelsSection)
							{
								string lineStart = lineIn.Substring(0, 17);
								if (lineStart.CompareTo("		<channel name=\"") == 0)
								{

								}
								else
								{
									if (lineStart.CompareTo("			<effect type=") == 0)
									{

									}
								}


							}
						}


					}






				}
			
			
			
			
			
			
			
			
			
			}




			return errCount;
		}


	}
}
