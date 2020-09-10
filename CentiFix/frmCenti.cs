using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Media;
using LORUtils;

namespace CentiFix
{
	public partial class frmCenti : Form
	{
		private string fileSequence = "";
		private Sequence4 seq = new Sequence4();
		private string applicationName = "CentiFix";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string thisEXE = "CentiFix.exe";
		private string[] commandArgs = null;
		private bool firstShown = false;
		const char DELIM1 = '⬖';
		const char DELIM4 = '⬙';
		private const string helpPage = "http://wizlights.com/util-o-rama/centifix";
		public enum itemLevel { normal, warning, error, special };

		private int lineCount = 0;
		private string lineOut = "";
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private bool processDrop = false;
		private Cursor prevCursor = Cursors.Default;
		private bool busy = false;
		private FileInfo fileinfo = null;
		private int highEnd = utils.UNDEFINED;




		public frmCenti()
		{
			InitializeComponent();
		}

		private void InitForm()
		{
			//this.Cursor = Cursors.WaitCursor;
			RestoreFormPosition();
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string mySubDir = "\\UtilORama\\";
			tempPath = appDataDir + mySubDir;

			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir += Application.ProductName + "\\";
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			ProcessCommandLine();
			//this.Cursor = DefaultCursor;
		}

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
						if ((ext.CompareTo(".lms") == 0) ||
								(ext.CompareTo(".las") == 0) ||
								(ext.CompareTo(".lcc") == 0) ||
								(ext.CompareTo(".lcb") == 0))
						{
							Array.Resize(ref batch_fileList, batch_fileCount + 1);
							batch_fileList[batch_fileCount] = arg;
							batch_fileCount++;
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
					ProcessFileBatch(batch_fileList);
				}
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
			frmReport_ResizeEnd(this, null);

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

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			Form aboutBox = new frmAbout();
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(helpPage);
		}

		private void frmReport_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveFormPosition();
		}


		private string ConvertDateFormat(string LORdate)
		{
			string ret = LORdate;
			if (LORdate.Length > 7)
			{
				if (LORdate.Substring(2, 1) == "/")
				{
					if (LORdate.Substring(5, 1) == "/")
					{
						string[] pieces = LORdate.Split(' ');
						string[] dates = pieces[0].Split('/');
						if (pieces.Length > 1)
						{
							string[] times = pieces[1].Split(':');

							int month = 1;
							int day = 1;
							int year = 2018;
							int hour = 0;
							int minute = 0;
							int second = 0;
							Int32.TryParse(dates[0], out month);
							if (dates.Length > 1)
							{
								Int32.TryParse(dates[1], out day);
								if (dates.Length > 2)
								{
									Int32.TryParse(dates[2], out year);
								}
							}
							Int32.TryParse(times[0], out hour);
							if (times.Length > 1)
							{
								Int32.TryParse(times[1], out minute);
								if (times.Length > 2)
								{
									Int32.TryParse(times[2], out second);
								}
							}
							if (pieces.Length > 2)
							{
								if (pieces[2].Substring(0, 1).ToUpper() == "P")
								{
									hour += 12;
									if (hour > 23)
									{
										hour -= 24;
									}
								}
							}
							if ((month > 12) && (day < 13))
							{
								// Euro Date Format
								int x = day; day = month; month = x;
							}
							if ((year < 2006) || (year > 2019))
							{
								year = 2012; // 2012 seems a good a year as any, lot of sequences downloaded from that year
							}
							DateTime dt = new DateTime(year, month, day, hour, minute, second);
							ret = MyFavoriteDateFormat(dt);
						}
					}
				}
			}
			else
			{
				ret = "(Undefined)";
			}
			return ret;
		}

		private string MyFavoriteDateFormat(DateTime dt)
		{
			string ret = dt.ToString("dddd, MMMM d");
			int dx = dt.Day % 10;
			string suf = "th";
			if ((dt.Day > 10) && (dt.Day < 14))
			{
				// suf = "th";
			}
			else
			{
				if ((dx == 0) || (dx > 3))
				{
					//suf = "th";
				}
				else
				{
					if (dx == 1)
					{
						suf = "st";
					}
					else
					{
						if (dx == 2)
						{
							suf = "nd";
						}
						else
						{
							suf = "rd";
						}
					}
				}
			}
			ret += suf;
			ret += dt.ToString(", yyyy (MM/dd/yyyy)");
			ret += " at ";
			ret += dt.ToString("h:mm:ss tt ");
			ret += TimeZoneAbbrev(dt);

			return ret;
		}

		private string TimeZoneAbbrev(DateTime dt)
		{
			string ret = "";
			bool isDST = dt.IsDaylightSavingTime();
			TimeSpan ts = TimeZoneInfo.Local.GetUtcOffset(dt);
			int offset = ts.Hours;

			if (isDST)
			{
				switch (offset)
				{
					case -4:
						ret = "EDT";
						break;
					case -5:
						ret = "CDT";
						break;
					case -6:
						ret = "MDT";
						break;
					case -7:
						ret = "PDT";
						break;
				}
			}
			else
			{
				switch (offset)
				{
					case -5:
						ret = "EST";
						break;
					case -6:
						ret = "CST";
						break;
					case -7:
						ret = "MST";
						break;
					case -8:
						ret = "PST";
						break;
				}
			}


			return ret;
		}


		private string FormattedLength()
		{
			long len = seq.Centiseconds;
			int mm = (int)(len / 6000);
			int ss = (int)(len - mm * 6000);
			int cs = (int)(len - mm * 6000 - ss * 100);
			string length = mm.ToString("0") + " minutes, " + ss.ToString("00") + "." + cs.ToString("00") + " seconds ";
			length += "(" + mm.ToString("0") + ":" + ss.ToString("00") + "." + cs.ToString("00") + ") ";
			length += " or " + len.ToString() + " centiseconds";
			return length;
		}



		private void Event_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				processDrop = true;
				ProcessFileList(files);
				//this.Cursor = prevCursor;
				this.Cursor = Cursors.Default;
			}
		}


		private void ProcessFileList(string[] batchFilenames)
		{
			batch_fileCount = 0; // reset
			DialogResult dr = DialogResult.None;

			foreach (string file in batchFilenames)
			{
				string ext = Path.GetExtension(file).ToLower();
				if ((ext.CompareTo(".lms") == 0) ||
					  (ext.CompareTo(".las") == 0) ||
						(ext.CompareTo(".lcc") == 0) ||
					  (ext.CompareTo(".lcb") == 0))
				{
					Array.Resize(ref batch_fileList, batch_fileCount + 1);
					batch_fileList[batch_fileCount] = file;
					batch_fileCount++;
				}
			}
			if (batch_fileCount > 1)
			{
				batchMode = true;
				ProcessFileBatch(batch_fileList);
			}
			else
			{
				if (batch_fileCount == 1)
				{
					string thisFile = batch_fileList[0];
					fileSequence = thisFile;
				}
			} // batch_fileCount-- Batch Mode or Not
		} // end ProcessFileList

		private void ProcessFileBatch(string[] batchFilenames)
		{
			string thisFile = batch_fileList[0];

			for (int f=1; f< batchFilenames.Length; f++)
			{
				thisFile = batch_fileList[f];
				Process.Start(thisEXE, thisFile);
			}
			batchMode = false;
		} // end ProcessFileBatch

		private void ImBusy(bool isBusy)
		{
			if (isBusy)
			{
				this.Cursor = Cursors.WaitCursor;
				this.Enabled = false;
				pnlProgress.Visible = true;
			}
			else
			{
				pnlProgress.Visible = false;
				this.Enabled = true;
				this.Cursor = Cursors.Default;
			}
			busy = isBusy;
		} // end ImBusy

		private void frmReport_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = utils.DefaultSequencesPath;
			string lf = Properties.Settings.Default.fileLast;
			string id = Path.GetDirectoryName(lf);
			if (Directory.Exists(id))
			{
				initDir = id;
			}
			string initFile = "";

			dlgOpenFile.Filter = "Sequence Files *.las, *.lms|*.las;*.lms|Musical Sequences *.lms|*.lms|Animated Sequences *.las|*.las";
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = initDir;
			dlgOpenFile.FileName = initFile;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			dlgOpenFile.Title = "Select a Sequence...";
			//pnlAll.Enabled = false;
			DialogResult result = dlgOpenFile.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				ImBusy(true);
				string thisFile  = dlgOpenFile.FileName;
				txtFile.Text = thisFile;
				Properties.Settings.Default.fileLast = thisFile;
				Properties.Settings.Default.Save();
				loadSequence(thisFile);
				fileSequence = thisFile;
				this.Text = "CentiFix - " + Path.GetFileName(thisFile);
				highEnd = CentiScann(false);
				string msg = "Last Effect ends at ";
				if (highEnd < 360000)
				{
					msg += utils.Time_CentisecondsToMinutes(highEnd);
				}
				msg += "  (" + highEnd.ToString() + " Centiseconds)";
				lblLastEnd.Text = msg;
				int curCS = CentiScann(true);
				msg = "Current Sequence Length is ";
				if (curCS < 360000)
				{
					msg += utils.Time_CentisecondsToMinutes(curCS);
				}
				msg += "  (" + curCS.ToString() + " Centiseconds)";
				lblCurCS.Text = msg;
				txtNewCS.Text = highEnd.ToString();
				//txtNewCS.Enabled = true;
				txtNewLength.Text = utils.Time_CentisecondsToMinutes(highEnd);
				//txtNewLength.Enabled = true;
				ActivateLabels(true);
				//btnSave.Enabled = true;


				ImBusy(false);

			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;

		}

		private void loadSequence(string seqFile)
		{
			seq.ReadSequenceFile(seqFile);
			fileSequence = seqFile;
			seq.Members.ReIndex();

			string rpt = tempPath + Path.GetFileNameWithoutExtension(seqFile) + " Report.htm";
			//CreateReport(seqFile, rpt);
		}

		private void frmReport_ResizeEnd(object sender, EventArgs e)
		{

		}

		private void Event_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
			prevCursor = this.Cursor;
			this.Cursor = Cursors.Cross;
			processDrop = true;


		}

		private void Event_DragLeave(object sender, EventArgs e)
		{
			processDrop = false;
			this.Cursor = Cursors.Default;
		}

		private void btnRename_Click(object sender, EventArgs e)
		{
			// The rename button is visible only when running on MY computer (User = Wizard) 
			// Rename to MY favorite format
			string i = seq.info.music.Title;
			if (i.Length < 1) i = "(Untitled)";
			string newName = i;
			newName += " by ";
			i = seq.info.music.Artist;
			if (i.Length < 1) i = "(Unknown)";
			newName += i;
			string t = utils.FormatTime(seq.Tracks[0].Centiseconds);
			if (seq.Tracks[0].Centiseconds > 5999)
			{
				t = t.Replace(':', '.');
			}
			else
			{
				t = t.Substring(0, 5);
			}
			newName += " " + t + " [";
			i = seq.info.author;
			if (i.Length < 1)
			{
				i = seq.info.modifiedBy;
				if (i.Length < 1) i = "Unknown";
			}
			//string y = seq.info.file_created.Year.ToString();
			string y1 = seq.info.createdAt;
			int yp = y1.LastIndexOf("/20");
			string y2 = y1.Substring(yp + 1, 4);

			newName += i;
			newName += " " + y2 + "] ";
			newName += seq.Channels.Count.ToString() + "ch";
			newName = utils.ReplaceInvalidFilenameCharacters(newName);
			string msg = "... to '" + newName + "'" + utils.CRLF + utils.CRLF;
			msg += "(In folder '" + Path.GetDirectoryName(seq.filename) + "')";
			DialogResult dr = MessageBox.Show(this, msg, "Rename file...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (dr == DialogResult.Yes)
			{
				string newFile = Path.GetDirectoryName(seq.filename) + "\\" + newName + Path.GetExtension(seq.filename);
				try
				{
					System.IO.File.Move(seq.filename, newFile);
				}
				catch (Exception ex)
				{

					string rmsg = "ERROR: Cannot Rename File" + utils.CRLF;
					rmsg += seq.filename + utils.CRLF;
					rmsg += "       to" + utils.CRLF;
					rmsg += newFile + utils.CRLF + utils.CRLF;
					rmsg += ex.Message;
					DialogResult rdr = MessageBox.Show(this, rmsg, "Error renaming file", MessageBoxButtons.OK, MessageBoxIcon.Stop);

					//System.Diagnostics.Debugger.Break();
				}
			}
		}

		private void CentiFixx(int newCentiseconds)
		{
			/*seq.Centiseconds = newCentiseconds;
			foreach (IMember member in seq.Members)
			{
				member.Centiseconds = newCentiseconds;
			}
			foreach (Channel ch in seq.Channels)
			{
				foreach (Effect ef in ch.effects)
				{
					if (ef.startCentisecond >= newCentiseconds)
					{
						// Remove the Effect
					}
					else
					{
						if (ef.endCentisecond > newCentiseconds)
						{
							ef.endCentisecond = newCentiseconds;
						}
					}
				}
			}
			*/
			seq.SetTotalTime(newCentiseconds);
		}

		private int CentiScann(bool everything)
		{
			int hi = utils.UNDEFINED;
			if (everything)
			{
				foreach (IMember mbr in seq.Members)
				{
					hi = Math.Max(hi, mbr.Centiseconds);
				}
				hi = Math.Max(hi, seq.Centiseconds);
			}
			else
			{
				foreach (Channel ch in seq.Channels)
				{
					foreach (Effect ef in ch.effects)
					{
						hi = Math.Max(hi, ef.endCentisecond);
					}
				}

			}

			return hi;
		}


		private void btnSave_Click(object sender, EventArgs e)
		{
			int newCS = utils.UNDEFINED;
			int.TryParse(txtNewCS.Text, out newCS);
			if ((newCS > 100) && (newCS < 360000))
			{
				string ext = Path.GetExtension(fileSequence).ToLower();
				string pth = Path.GetDirectoryName(fileSequence);
				string fn = Path.GetFileNameWithoutExtension(fileSequence) + " CentiFixed";
				dlgSaveFile.DefaultExt = ext;
				dlgSaveFile.InitialDirectory = pth;
				dlgSaveFile.FileName = fn;
				if (ext == "lms")
				{
					dlgSaveFile.Filter = "Musical Sequence *.lms|*.lms";
				}
				else
				{
					dlgSaveFile.Filter = "Animated Sequence *.las|*.las";
				}
				dlgSaveFile.FilterIndex = 1;
				dlgSaveFile.OverwritePrompt = true;
				dlgSaveFile.SupportMultiDottedExtensions = true;
				dlgSaveFile.ValidateNames = true;
				dlgSaveFile.Title = "Save Fixed Sequence As...";
				DialogResult dr = dlgSaveFile.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					string newFile = dlgSaveFile.FileName;

					ImBusy(true);
					CentiFixx(newCS);
					seq.WriteSequenceFile(newFile);
					SystemSounds.Exclamation.Play();
					ImBusy(false);
				}
			}
			
		}

		private void txtNewCS_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Check for a naughty character in the KeyDown event.
			if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^]"))
			{
				// Stop the character from being entered into the control since it is illegal.
				e.Handled = true;
			}
		}

		private void txtNewLength_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Check for a naughty character in the KeyDown event.
			if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^:^\.]"))
			{
				// Stop the character from being entered into the control since it is illegal.
				e.Handled = true;
			}
		}

		private void txtNewCS_TextChanged(object sender, EventArgs e)
		{
			int newCS = utils.UNDEFINED;
			int.TryParse(txtNewCS.Text, out newCS);
			if ((newCS > 100) && (newCS < 360000))
			{
				txtNewCS.ForeColor = SystemColors.ControlText;
				txtNewLength.Text = utils.Time_CentisecondsToMinutes(newCS);
				btnSave.Enabled = true;
			}
			else
			{
				txtNewCS.ForeColor = Color.DarkRed;
				btnSave.Enabled = false;
			}
		}

		private void txtNewCS_Validating(object sender, CancelEventArgs e)
		{
			int newCS = utils.UNDEFINED;
			int.TryParse(txtNewCS.Text, out newCS);
			if ((newCS > 100) && (newCS < 360000))
			{
				// OK, Do nothing
				btnSave.Enabled = true;
			}
			else
			{
				btnSave.Enabled = false;
				//e.Cancel = true;
			}
		}

		private void txtNewLength_Validating(object sender, CancelEventArgs e)
		{
			int newCS = utils.Time_MinutesToCentiseconds(txtNewLength.Text);
			if ((newCS > 100) && (newCS < 360000))
			{
				// OK, Do nothing
				btnSave.Enabled = true;
			}
			else
			{
				btnSave.Enabled = false;
				//e.Cancel = true;
			}
		}

		private void txtNewLength_TextChanged(object sender, EventArgs e)
		{
			int newCS = utils.Time_MinutesToCentiseconds(txtNewLength.Text);
			if ((newCS > 100) && (newCS < 360000))
			{
				txtNewLength.ForeColor = SystemColors.ControlText;
				txtNewCS.Text = newCS.ToString();
				btnSave.Enabled = true;
			}
			else
			{
				txtNewLength.ForeColor = Color.DarkRed;
				btnSave.Enabled = false;
			}
		}

		private void ActivateLabels(bool newState)
		{
			txtFormat.Enabled = newState;
			lblCurCS.Enabled = newState;
			lblLastEnd.Enabled = newState;
			lblNewTime.Enabled = newState;
			lblNewCS.Enabled = newState;
			txtNewCS.Enabled = newState;
			txtNewLength.Enabled = newState;
		}

	} // end form frmReport
} // end namespace CentiFix
