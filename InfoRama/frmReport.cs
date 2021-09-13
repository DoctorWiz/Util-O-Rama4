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
using LORUtils4; using FileHelper;

namespace UtilORama4
{
	public partial class frmReport : Form
	{
		private string fileSequence = "";
		private string fileReport = "";
		private LORSequence4 seq = new LORSequence4();
		private string applicationName = "LORSeqInfo4-Rama";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string thisEXE = "LORSeqInfo4-Rama.exe";
		private string[] commandArgs = null;
		private bool firstShown = false;
		const char DELIM1 = '⬖';
		const char DELIM4 = '⬙';
		private const string helpPage = "http://wizlights.com/util-o-rama/info-rama";
		public enum itemLevel { normal, warning, error, special };

		private string defaultStylesTemplate = "";
		private StreamWriter writer;
		private int lineCount = 0;
		private string lineOut = "";
		private bool batchMode = false;
		private int batch_fileCount = 0;
		private string[] batch_fileList = null;
		private bool processDrop = false;
		private Cursor prevCursor = Cursors.Default;
		private bool busy = false;
		private FileInfo fileinfo = null;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;


		#region HTML Tag Constants
		//private const string DOCTYPE = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
		//private const string HTML_BEGIN = "<html xmlns = \"http://www.w3.org/1999/xhtml\" >";
		//private const string META = "<meta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />";
		private const string HTML_END = "</html>";
		private const string BODY_BEGIN = "<body>";
		private const string BODY_END = "</body>";
		private const string HEAD_BEGIN = "<head>";
		private const string HEAD_END = "</head>";
		private const string TITLE_BEGIN = "<title>";
		private const string TITLE_END = "</title>";
		private const string SPAN_BEGIN = "<span ";
		private const string SPAN_END = "</span>";
		private const string BREAK = "<br />";
		private const string SPACE = "&nbsp";
		private const string ITALIC_BEGIN = "<i>";
		private const string ITALIC_END = "</i>";
		private const string BOLD_BEGIN = "<strong>";
		private const string BOLD_END = "</strong>";
		private const string PARA_BEGIN = "<p>";
		private const string PARA_START = "<p";
		private const string PARA_END = "</p>";
		private const string COLOR = "color: ";
		private const string FONT_SIZE = "font-size: ";
		private const string SIZE_XXSMALL = "xx-small;";
		private const string SISE_XSMALL = "x-small;";
		private const string SIZE_SMALL = "small;";
		private const string SIZE_MEDIUM = "medium;";
		private const string SIZE_LARGE = "large;";
		private const string SIZE_XLARGE = "x-large;";
		private const string SIZE_XXLARGE = "xx-large;";
		private const string TEXT_ALIGN = "text-align: ";
		private const string ALIGN_LEFT = "left;";
		private const string ALIGN_CENTER = "center;";
		private const string ALIGN_RIGHT = "right;";
		private const string ALIGN_TOP = "top;";
		private const string ALIGN_MIDDLE = "middle;";
		private const string ALIGN_BOTTOM = "bottom;";
		private const string QUOTE = "\"";
		private const string AMPERSAND = "&amp";
		private const string BACKSLASH = "\\";
		//private const string SPACE = "<br>";
		//private const string SPACE = "<br>";
		//private const string SPACE = "<br>";
		private const string CLASS_TITLE = " class=\"style-title\">";
		private const string CLASS_SUBTITLE = " class=\"style-subtitle\">";
		private const string CLASS_SECTION = " class=\"style-section\">";
		private const string CLASS_ITEMNORMAL = " class=\"style-itemnormal\">";
		private const string CLASS_VALUENORMAL = " class=\"style-valuenormal\">";
		private const string CLASS_ITEMWARNING = " class=\"style-itemwarning\">";
		private const string CLASS_VALUEWARNING = " class=\"style-valuewarning\">";
		private const string CLASS_ITEMERROR = " class=\"style-itemerror\">";
		private const string CLASS_VALUEERROR = " class=\"style-valueerror\">";
		private const string CLASS_INFONORMAL = " class=\"style-infonormal\">";
		private const string CLASS_INFOWARNING = " class=\"style-infowarning\">";
		private const string CLASS_INFOERROR = " class=\"style-infoerror\">";
		private const string CLASS_FOOTER = " class=\"style-footer\">";



		private const string COLOR_RED = "FF0000;";
		private const string COLOR_GREEN = "00FF00;";
		private const string COLR_BLACK = "000000;";
		private const string COLOR_BLUE = "0000FF;";
		private const string COLOR_WHITE = "FFFFFF;";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		//private const string COLOR_BLUE = "<br>";
		#endregion




		public frmReport()
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

			btnRename.Visible = isWiz;

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
				if (arg.Substring(4).IndexOf(".") > lutils.UNDEFINED) isFile++;  // contains a period
				if (Fyle.InvalidCharacterCount(arg) == 0) isFile++;
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
								defaultStylesTemplate = Path.GetDirectoryName(thisEXE) + "\\reportstyles.htm";
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

		private void ReportHeader(string reportFilename, string title, string subtitle)
		{
			bool keepGoing = true;
			string lineIn = "";
			lineCount = 0;
			StreamReader reader = new StreamReader(defaultStylesTemplate);
			writer = new StreamWriter(reportFilename);

			int pos = lutils.UNDEFINED;
			while (keepGoing && (lineIn = reader.ReadLine()) != null)
			{
				writer.WriteLine(lineIn); lineCount++;
				pos = lineIn.IndexOf("</style>");
				if (pos > lutils.UNDEFINED)
				{
					keepGoing = false;
				}
			}
			reader.Close();

			lineOut = TITLE_BEGIN + title + TITLE_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = HEAD_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = BODY_BEGIN;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_START + CLASS_TITLE + title + PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_START + CLASS_SUBTITLE + subtitle;
			writer.WriteLine(lineOut); lineCount++;




		} // end of ReportHeader procedure

		private void ReportFooter(string footer)
		{
			lineOut = PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_START + CLASS_FOOTER + footer + PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = BODY_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = HTML_END;
			writer.WriteLine(lineOut); lineCount++;

			writer.Flush();
			writer.Close();

		} // end of ReportFooter procedure

		private void AddSection(string sectionName)
		{
			lineOut = PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_START + CLASS_SECTION + sectionName + PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_BEGIN;
			writer.WriteLine(lineOut); lineCount++;

		} // end AddItem

		private void AddItem(string itemName, string itemValue)
		{
			AddItem(itemName, itemValue, itemLevel.normal);
		}

		private void AddItem(string itemName, string itemValue, itemLevel level)
		{
			lineOut = SPAN_BEGIN;
			if (level == itemLevel.normal)
			{
				lineOut += CLASS_ITEMNORMAL;
			}
			else
			{
				if (level == itemLevel.error)
				{
					lineOut += CLASS_ITEMERROR;
				}
				else
				{
					if (level == itemLevel.warning)
					{
						lineOut += CLASS_ITEMWARNING;
					}
					else
					{

					}
				}
			}
			lineOut += itemName + ": ";
			lineOut += SPAN_END;

			lineOut += SPAN_BEGIN;
			if (level == itemLevel.normal)
			{
				lineOut += CLASS_VALUENORMAL;
			}
			else
			{
				if (level == itemLevel.error)
				{
					lineOut += CLASS_VALUEERROR;
				}
				else
				{
					if (level == itemLevel.warning)
					{
						lineOut += CLASS_VALUEWARNING;
					}
					else
					{

					}
				}
			}
			lineOut += itemValue;
			lineOut += SPAN_END;
			lineOut += BREAK;

			writer.WriteLine(lineOut); lineCount++;


		} // end AddItem

		private void AddInfo(string info, itemLevel level)
		{

			lineOut = SPAN_BEGIN;
			if (level == itemLevel.normal)
			{
				lineOut += CLASS_INFONORMAL;
			}
			else
			{
				if (level == itemLevel.error)
				{
					lineOut += CLASS_INFOERROR;
				}
				else
				{
					if (level == itemLevel.warning)
					{
						lineOut += CLASS_INFOWARNING;
					}
					else
					{

					}
				}
			}
			lineOut += info;
			lineOut += SPAN_END;
			lineOut += BREAK;

			writer.WriteLine(lineOut); lineCount++;

		} // end AddInfo

		private void CreateReport(string sequenceFilename, string reportFilename)
		{
			ImBusy(true);
			string errInfo = "";
			fileReport = reportFilename;
			string title = "LORSeqInfo4&#45;Rama Report";
			string subtitle = "for \"" + Path.GetFileName(sequenceFilename) + "\"";
			ReportHeader(reportFilename, title, subtitle);

			try
			{
				fileinfo = new FileInfo(@sequenceFilename);
			}
			catch
			{
				// Can't get File LORSeqInfo4??  fileinfo will remain null
			}

			int err = seq.ReadSequenceFile(sequenceFilename);

			if (((err > 0) && (err < 100)) || (err > 200))
			{
				// Can't Open
				errInfo = "The sequence file contains some unrecognized, unsupported, or invalid ";
				errInfo += "data.  The information collected by LORSeqInfo4-Rama about this sequence may ";
				errInfo += "be incomplete and/or inaccurate.  If you continue to see this error for ";
				errInfo += "multiple different sequence files, please contact Doctor Wizard at ";
				errInfo += "<a href= \"mailto:support.utilorama@wizster.com?subject=Util-O-Rama Report\">support.utilorama@wizster.com</a>";
				errInfo += " for assistance.";
				errInfo += "The most recent error code is <i>" + seq.info.LastError.errMsg + "</i>.";
			}
			if (err == LORSequence4.ERROR_CantOpen)
			{
				// Can't Open
				errInfo = "The file cannot be opened by LORSeqInfo4-Rama.  Since selecting it, the file ";
				errInfo += "may have been deleted, renamed, or moved, or you may not have the ";
				errInfo += "necessary security permissions to access it.";
			}
			if (err == LORSequence4.ERROR_NotXML)
			{
				// Not XML
				errInfo = "The file is not in XML format and does not appear to be a valid Light-O-Rama sequence. ";
				errInfo += "LORSeqInfo4-Rama is unable to read this file or provide any further details about it.";
			}
			if (err == LORSequence4.ERROR_NotSequence)
			{
				// Not a Sequence
				errInfo = "The file is in XML format, but is not a valid Light-O-Rama sequence or visualizer file. ";
				errInfo += "The second line of the file should start with \"sequence\" and contain critical information ";
				errInfo += "about the sequence, but does not.  The second line is:<br><i>";
				errInfo += "'" + seq.info.infoLine + "'</i>";
			}
			if (err == LORSequence4.ERROR_EncryptedDemo)
			{
				// Encrypted Demo
				errInfo = "The file was saved by a Demo version of Light-O-Rama Showtime and is ";
				errInfo += "encrypted.  Information about the sequence cannot be decoded by ";
				errInfo += "LORSeqInfo4-Rama.  Please open the sequence in a licensed copy of Light-O-Rama ";
				errInfo += "Showtime and re-save it in a non-encrypted and non-compressed format.";
			}
			if (err == LORSequence4.ERROR_Compressed)
			{
				// Compressed
				errInfo = "The file has been saved in compressed format. ";
				errInfo += "Information about the sequence cannot be decoded by ";
				errInfo += "LORSeqInfo4-Rama.  Please open the sequence in a licensed copy of Light-O-Rama ";
				errInfo += "Showtime and re-save it in a non-compressed and non-encrypted format.";
			}
			if (err == LORSequence4.ERROR_UnsupportedVersion)
			{
				// Unsupported Version
				errInfo = "The file version is unrecognized and unsupported by this release of ";
				errInfo += "LORSeqInfo4-Rama.  The file version is <b>" + seq.info.saveFileVersion.ToString() + "</b>. ";
				errInfo += "This release of LORSeqInfo4-Rama recognizes and supportes file versions 11 thru 14 ";
				errInfo += "which corresponds to Light-O-Rama Showtime versions 2.0 thru 4.4.";
				errInfo += "Please open the sequence in a supported and licenced version of Light-O-Rama Showtime ";
				errInfo += "and re-save it from that version in a non-compressed and not-encrypted format.";
			}

			if ((err >= 100) && (err < 200))
			{
				ReportError(errInfo);
				if (fileinfo != null) ReportFileOS(sequenceFilename);
			}
			if ((err > 0) && (err <100))
			{
				ReportError(errInfo);
				//if (fileinfo != null)	ReportFileOS(sequenceFilename);
			}
			if ((err == 0) || (err < 100))
			{
				ReportSummary();
				ReportFileInfo();
				ReportSequenceInfo();
				if (seq.LORSequenceType4 == LORSequenceType4.Musical)
				{
					ReportMusicInfo();
				}
				ReportIntegrity();
				ReportChannelInfo();
				ReportDuplicateChannels();
				ReportDuplicateNames();
				ReportControllers();
				ReportEmpties();
				ReportOrphans();
			}
			string footerInfo = MakeFooter(sequenceFilename);
			ReportFooter(footerInfo);
			//Process.Start(reportFilename);
			webReport.Navigate(reportFilename);
			btnSaveReport.Enabled = true;
			this.Text = "LORSeqInfo4-Rama - " + Path.GetFileName(sequenceFilename);
			btnBrowseSeq.Text = "Analyze another Sequence...";

			ImBusy(false);

		} // end CreateReport

		private string MakeFooter(string sequenceFilename)
		{
			string footerInfo = "<p style=\"color:DarkBlue;\">";
			footerInfo += "<font size=\"2\"><center>";
			footerInfo += "LORSeqInfo4&#45;Rama Report for \"" + HTMLizeFilename(sequenceFilename) + "\"";
			footerInfo += " created at " + MyFavoriteDateFormat(DateTime.Now) + BREAK;
			footerInfo += "<a href = \"http://wizlights.com/util-o-rama/ifno-o-rama\">LORSeqInfo4&#45;Rama</a> is member of the ";
			footerInfo += "<a href = \"http://wizlights.com/util-o-rama\">Util&#45;O&#45;Rama</a> Suite for editing ";
			footerInfo += "<a href = \"http://www1.lightorama.com/\">Light&#45;O&#45;Rama</a> Sequences." + BREAK;
			footerInfo += "These utilities are released as freeware for the	benefit of the Light&#45;O&#45;Rama	community" + BREAK;
			footerInfo += "Source code is available on GitHub under a General Public License(GPL)" + BREAK;
			footerInfo += "Util&#45;O&#45;Rama and LORSeqInfo4&#45;Rama are &copy; copyright &#45;2018-19 by <a href = \"http://drwiz.net\">Doctor Wizard</a>";
			footerInfo += " and <a href = \"http://wizster.com\">W⚡zster Software.</a>" + BREAK;
			footerInfo += "The Util&#45;O&#45;Rama Suite is not a product of, nor officially endorsed in any way by the Light&#45;O&#45;Rama company.";
			footerInfo += "  This is purely the work of Dr. Wizard and W⚡zster.";
			footerInfo += "  Please do not contact Light&#45;O&#45;Rama for support regarding the Util&#45;O&#45;Rama applications." + BREAK;
			footerInfo += "Submit bug reports, suggestions, ideas, rants, cool sequences, and good dirty jokes to ";
			footerInfo += "<a href= \"mailto:support.utilorama@wizster.com?subject=Util-O-Rama Report\">support.utilorama@wizster.com</a>";
			footerInfo += "</center></font>";
			return footerInfo;
		}

		private string HTMLizeFilename(string name)
		{
			string ret = "";
			int pos1 = lutils.UNDEFINED;
			int pos2 = 0;

			pos1 = name.IndexOf('&', pos2);
			while (pos1 > lutils.UNDEFINED)
			{
				ret += name.Substring(pos2, pos1 - pos2) + "&amp;";
				pos2 = pos1 + 1;
				pos1 = name.IndexOf('\\', pos2);
			}
			ret += name.Substring(pos2);

			name = ret;
			ret = "";

			pos1 = lutils.UNDEFINED;
			pos2 = 0;
			pos1 = name.IndexOf('\\', pos2);
			while (pos1 > lutils.UNDEFINED)
			{
				ret += name.Substring(pos2, pos1 - pos2) + "&#92;";
				pos2 = pos1 + 1;
				pos1 = name.IndexOf('\\', pos2);
			}
			ret += name.Substring(pos2);

			return ret;
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

		private void ReportSummary()
		{
			itemLevel thisLevel = itemLevel.normal;


			AddSection("Quick Summary");

			string info = seq.info.author;
			if (info.Length < 1)
			{
				info = "(blank)";
				thisLevel = itemLevel.warning;
			}

			AddItem("Created At", ConvertDateFormat(seq.info.createdAt), itemLevel.normal);
			AddItem("Created By", info, thisLevel);
			thisLevel = itemLevel.normal;
			//AddItem("Last Modified", ConvertDateFormat(seq.info.lastModified), itemLevel.normal);
			//AddItem("Last Modified", Fyle.FormatDateTime(seq.info.lastModified), itemLevel.normal);
			AddItem("Last Modified At", Fyle.FileModifiedAt(fileSequence), itemLevel.normal);
			info = seq.info.modifiedBy;
			if (info.Length < 1) info = "(blank)";
			AddItem("Last Modified By", info);


			AddItem("File Size", Fyle.FileSizeFormatted(fileSequence), itemLevel.normal);
			AddItem("Sequence Type", seq.LORSequenceType4.ToString(), itemLevel.normal);
			if (seq.LORSequenceType4 == LORSequenceType4.Musical)
			{
				AddItem("Song Title", seq.info.music.Title, itemLevel.normal);
				AddItem("By Artist", seq.info.music.Artist, itemLevel.normal);
			}
			//AddItem("Length", FormattedLength(), itemLevel.normal);
			LORTrack4 tk0 = seq.Tracks[0];
			int centi = tk0.Centiseconds;
			AddItem("Length", lutils.FormatTime(centi));
			int v = seq.info.saveFileVersion - 10;
			if (v == 4) thisLevel = itemLevel.normal; else thisLevel = itemLevel.error;
			AddItem("Version", v.ToString(), thisLevel);
			thisLevel = itemLevel.normal; // Reset Default
			AddItem("Lines", seq.lineCount.ToString(), itemLevel.normal);
			//TODO: Handle clipboard files correctly
			int cc = seq.Channels.Count;
			int rc = seq.RGBchannels.Count;
			cc -= (rc * 3);
			if (cc > 0) thisLevel = itemLevel.normal; else thisLevel = itemLevel.error;
			AddItem("Regular Channels", cc.ToString(), thisLevel);
			thisLevel = itemLevel.normal; // Reset Default
			AddItem("RGB Channels", rc.ToString(), itemLevel.normal);
			AddItem("LORChannel4 Groups", seq.ChannelGroups.Count.ToString());
			AddItem("Tracks", seq.Tracks.Count.ToString(), itemLevel.normal);
			AddItem("Timing Grids", seq.TimingGrids.Count.ToString(), itemLevel.normal);
			AddItem("Last SavedIndex", seq.Members.HighestSavedIndex.ToString(), itemLevel.normal);
			
		}

		private void ReportFileInfo()
		{
			AddSection("File Information");

			AddItem("Sequence File Name", Path.GetFileName(seq.filename), itemLevel.normal);
			string filePath = Path.GetDirectoryName(seq.filename);
			itemLevel nextLevel = itemLevel.normal;
			if (lutils.DefaultSequencesPath.ToLower().CompareTo(filePath.ToLower()) != 0) nextLevel = itemLevel.warning;
			//TODO: Handle clipboard and channel config files correctly
			AddItem("In Folder", filePath, nextLevel);
			if (nextLevel == itemLevel.normal)
			{
				AddInfo("Which is your default Light-O-Rama Sequence folder.", nextLevel);
			}
			else
			{
				AddItem("Your default Light-O-Rama Sequence folder is", lutils.DefaultSequencesPath, nextLevel);
			}
			AddItem("Sequence Created at", ConvertDateFormat(seq.info.createdAt), itemLevel.normal);
			AddItem("File Created on (O/S)", Fyle.FormatDateTime(fileinfo.CreationTime), itemLevel.normal);
			AddItem("File Last Modified on", Fyle.FormatDateTime(fileinfo.LastWriteTime));
			AddItem("File Last Accessed On", Fyle.FormatDateTime(fileinfo.LastAccessTime));
			string sz = Fyle.FileSizeFormatted(fileSequence, "") + " (" + Fyle.FileSizeFormatted(fileSequence, "B") + ")";
			AddItem("File Size", sz, itemLevel.normal);
			AddItem("Line Count", seq.lineCount.ToString(), itemLevel.normal);
			if (seq.LORSequenceType4 == LORSequenceType4.Musical)
			{
				AddItem("LORMusic4 File Name", Path.GetFileName(seq.info.music.File), itemLevel.normal);
				filePath = Path.GetDirectoryName(seq.info.music.File);
				nextLevel = itemLevel.normal;
				if (lutils.DefaultAudioPath.ToLower().CompareTo(filePath.ToLower()) != 0) nextLevel = itemLevel.warning;
				AddItem("In Folder", filePath, nextLevel);
				if (nextLevel == itemLevel.normal)
				{
					AddInfo("Which is your default Light-O-Rama Audio folder.", nextLevel);
				}
				else
				{
					AddItem("Your default Light-O-Rama Audio folder is", lutils.DefaultAudioPath, nextLevel);
				}
			}

		} // end FileInfo

		private void ReportFileOS(string theFile)
		{
			AddSection("File Information");

			AddItem("File Name", Path.GetFileName(theFile), itemLevel.normal);
			string filePath = Path.GetDirectoryName(theFile);
			itemLevel nextLevel = itemLevel.normal;
			if (lutils.DefaultSequencesPath.ToLower().CompareTo(filePath.ToLower()) != 0) nextLevel = itemLevel.warning;
			//TODO: Handle clipboard and channel config files correctly
			AddItem("In Folder", filePath, nextLevel);
			if (nextLevel == itemLevel.normal)
			{
				AddInfo("Which is your default Light-O-Rama Sequence folder.", nextLevel);
			}
			else
			{
				AddItem("Your default Light-O-Rama Sequence folder is", lutils.DefaultSequencesPath, nextLevel);
			}
			AddItem("File Created on", Fyle.FormatDateTime(fileinfo.CreationTime), itemLevel.normal);
			AddItem("File Last Written On", Fyle.FormatDateTime(fileinfo.LastWriteTime));
			AddItem("File Last Accessed On", Fyle.FormatDateTime(fileinfo.LastAccessTime));
			
			string sz = Fyle.FileSizeFormatted(theFile, "") + " (" + Fyle.FileSizeFormatted(theFile, "B") + ")";
			AddItem("File Size", sz, itemLevel.normal);
			

		} // end FileInfo

		private void ReportSequenceInfo()
		{
			AddSection("Sequence Information");

			AddInfo("(this report section is still being developed)", itemLevel.warning);

		} // end SequenceInfo

		private void ReportMusicInfo()
		{
			AddSection("LORMusic4 Information");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		} // end MusicInfo

		private void ReportIntegrity()
		{
			AddSection("Sequence Integrity");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		}

		private void ReportChannelInfo()
		{
			AddSection("Channels");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		} // end ChannelInfo

		private void ReportDuplicateChannels()
		{
			AddSection("Duplicate Channels");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		}

		private void ReportDuplicateNames()
		{
			AddSection("Duplicate Names");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		}

		private void ReportControllers()
		{
			AddSection("Controllers");

			AddInfo("(this report section is still being developed)", itemLevel.warning);
		}

		private void ReportEmpties()
		{
			// IF any tracks are empty
			AddSection("Empty Tracks");
			AddInfo("(this report section is still being developed)", itemLevel.warning);
			// IF any channelGroups are empty
			AddSection("Empty Channel Groups");
			AddInfo("(this report section is still being developed)", itemLevel.warning);
			// IF any timiongGrids are empty
			AddSection("Empty Timing Grids");
			AddInfo("(this report section is still being developed)", itemLevel.warning);

		}

		private void ReportOrphans()
		{
			AddSection("Orphaned Channels");
			AddInfo("(this report section is still being developed)", itemLevel.warning);

		}

		private void ReportError(string errorMsg)
		{
			AddSection("ERRORS");
			AddInfo(errorMsg, itemLevel.error);
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
					string reportTempFile = tempPath + Path.GetFileNameWithoutExtension(thisFile) +" Report.htm";
					CreateReport(thisFile, reportTempFile);
				}
			} // batch_fileCount-- Batch Mode or Not
		} // end ProcessFileList

		private void ProcessFileBatch(string[] batchFilenames)
		{
			string thisFile = batch_fileList[0];
			string reportTempFile = tempPath + Path.GetFileNameWithoutExtension(thisFile) + " Report.htm";
			CreateReport(thisFile, reportTempFile);

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
			string initDir = lutils.DefaultSequencesPath;
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
				string thisRpt = tempPath + Path.GetFileNameWithoutExtension(thisFile) + "Report.htm";
				loadSequence(thisFile);
				CreateReport(thisFile, thisRpt);

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
			pnlReport.Width = this.ClientRectangle.Width - 10;
			pnlReport.Height = this.ClientRectangle.Height - 70;
			btnBrowseSeq.Top = pnlReport.Height + 15;
			btnSaveReport.Top = pnlReport.Height + 15;
			int w = this.ClientRectangle.Width - btnBrowseSeq.Width - btnSaveReport.Width;
			w /= 4;
			btnBrowseSeq.Left = w;
			btnSaveReport.Left = btnBrowseSeq.Width + w * 3;
			btnRename.Top = btnBrowseSeq.Top;



		}

		private void Event_DragEnter(object sender, DragEventArgs e)
		{
			e.LOREffect4 = DragDropEffects.Copy;
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
			string t = lutils.FormatTime(seq.Tracks[0].Centiseconds);
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
			newName = Fyle.ReplaceInvalidFilenameCharacters(newName);
			string msg = "... to '" + newName + "'" + lutils.CRLF + lutils.CRLF;
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

					string rmsg = "ERROR: Cannot Rename File" + lutils.CRLF;
					rmsg += seq.filename + lutils.CRLF;
					rmsg += "       to" + lutils.CRLF;
					rmsg += newFile + lutils.CRLF + lutils.CRLF;
					rmsg += ex.Message;
					DialogResult rdr = MessageBox.Show(this, rmsg, "Error renaming file", MessageBoxButtons.OK, MessageBoxIcon.Stop);

					//System.Diagnostics.Debugger.Break();
				}
			}
		}

		// STUFF FOR DAN
		/*
		private void btnProgLogin_Click(object sender, EventArgs e)
        {
            string sConn;
            string sQuery;
            sConn = "datasource = " + Properties.Settings.Default.hostname + "; username = " + Properties.Settings.Default.username + "; password = " + Properties.Settings.Default.password + ";database = " + Properties.Settings.Default.database;
            MySqlConnection conn = new MySqlConnection(sConn);
            MySqlCommand command = conn.CreateCommand();
            sQuery = "SELECT perms FROM login WHERE Username = '" + TxtProgUname.Text + "' AND passwords = '" + TxtProgPw.Text + "' ";
            try
            {
                command.CommandText = sQuery;
                conn.Open();
            }
            catch (Exception exc)
            {
                string sMsg;
                sMsg = exc.Message;
                MessageBox.Show(sMsg);
            }
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int iadminlevel;
					
					
					// STATUS DEBUGGING
					int stat = reader.Status;
					MessageBox.Show("Database Status is " + stat.ToString());
					stat = reader.ItemCount;
					MessageBox.Show("And it has " + stat.ToString() + " items.");
					if (reader.EndOfStream)
					{
						MessageBox.Show("You are at the end!");
					}
					// END STATUS DEBUGGING

					if (reader.IsDBNull(3) == false)
                    {
                        iadminlevel = reader.GetInt32(3);
                        if (iadminlevel == 0)
                        {
                            string NaMsg;
                            NaMsg = "Username " + TxtProgUname.Text + " Found. You Have No Access To Edit";
                            MessageBox.Show(NaMsg, "NoAccess Username Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMenu oMenuForm = new frmMenu();
                            oMenuForm.btnUseradmin.Enabled = false;
                            oMenuForm.btnConnections.Enabled = false;
                            oMenuForm.btnCpu.Enabled = false;
                            oMenuForm.btnDCLocation.Enabled = false;
                            oMenuForm.btnMachType.Enabled = false;
                            oMenuForm.btnOSType.Enabled = false;
                            oMenuForm.btnServices.Enabled = false;
                            oMenuForm.btnMainDb.Enabled = false;
                            oMenuForm.Show();
                            this.Close();
                        }
                        if (iadminlevel == 1)
                        {
                            string LmMsg;
                            LmMsg = "Username " + TxtProgUname.Text + " Found. You Have Limited Access";
                            MessageBox.Show(LmMsg, "Limited Username Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMenu oMenuForm = new frmMenu();
                            oMenuForm.btnUseradmin.Enabled = false;
                            oMenuForm.btnConnections.Enabled = true;
                            oMenuForm.btnCpu.Enabled = false;
                            oMenuForm.btnDCLocation.Enabled = false;
                            oMenuForm.btnMachType.Enabled = false;
                            oMenuForm.btnOSType.Enabled = false;
                            oMenuForm.btnServices.Enabled = false;
                            oMenuForm.btnMainDb.Enabled = true;
                            oMenuForm.Show();
                            this.Close();
                        }
                        if (iadminlevel == 2)
                        {
                            string aMsg;
                            aMsg = "Username " + TxtProgUname.Text + " Found. You Have Administrative Access";
                            MessageBox.Show(aMsg, "Administrative Username Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMenu oMenuForm = new frmMenu();
                            oMenuForm.btnUseradmin.Enabled = false;
                            oMenuForm.btnConnections.Enabled = true;
                            oMenuForm.btnCpu.Enabled = true;
                            oMenuForm.btnDCLocation.Enabled = true;
                            oMenuForm.btnMachType.Enabled = false;
                            oMenuForm.btnOSType.Enabled = true;
                            oMenuForm.btnServices.Enabled = false;
                            oMenuForm.btnMainDb.Enabled = true;
                            oMenuForm.Show();
                            this.Close();
                        }
                        if (iadminlevel == 3)
                        {
                            string aMsg;
                            aMsg = "Username " + TxtProgUname.Text + " Found. You Have Administrative Access";
                            MessageBox.Show(aMsg, "Administrative Username Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMenu oMenuForm = new frmMenu();
                            oMenuForm.btnUseradmin.Enabled = false;
                            oMenuForm.btnConnections.Enabled = true;
                            oMenuForm.btnCpu.Enabled = true;
                            oMenuForm.btnDCLocation.Enabled = true;
                            oMenuForm.btnMachType.Enabled = true;
                            oMenuForm.btnOSType.Enabled = true;
                            oMenuForm.btnServices.Enabled = false;
                            oMenuForm.btnMainDb.Enabled = true;
                            oMenuForm.Show();
                            this.Close();
                        }
                        if (iadminlevel == 4)
                        {
                            string SaMsg;
                            SaMsg = "Username " + TxtProgUname.Text + " Found. You Have Administrative Access";
                            MessageBox.Show(SaMsg, "Super Admin Username Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmMenu oMenuForm = new frmMenu();
                            oMenuForm.btnUseradmin.Enabled = true;
                            oMenuForm.btnConnections.Enabled = true;
                            oMenuForm.btnCpu.Enabled = true;
                            oMenuForm.btnDCLocation.Enabled = true;
                            oMenuForm.btnMachType.Enabled = true;
                            oMenuForm.btnOSType.Enabled = true;
                            oMenuForm.btnServices.Enabled = true;
                            oMenuForm.btnMainDb.Enabled = true;
                            oMenuForm.Show();
                            this.Close();
                        }
                    }
                }
            }
            else
            {
                string sMsg;
                sMsg = "Username " + TxtProgUname.Text + " not found in the list! Talk To Your DB Administrator";
                MessageBox.Show(sMsg, "Username Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
		*/
	} // end form frmReport
} // end namespace UtilORama4
