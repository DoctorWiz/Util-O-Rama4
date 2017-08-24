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
using LORUtils;

namespace InfoRama
{
	public partial class frmReport : Form
	{
		private string fileSequence = "";
		private string fileReport = "";
		private Sequence seq = new Sequence();
		private string applicationName = "Info-Rama";
		private string tempPath = "C:\\Windows\\Temp\\";  // Gets overwritten with X:\\Username\\AppData\\Roaming\\Util-O-Rama\\Split-O-Rama\\
		private string thisEXE = "Info-Rama.exe";
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
			string mySubDir = "\\Util-O-Rama\\";
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir	+= Application.ProductName + "\\";
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
				if (arg.Substring(4).IndexOf(".") > LOR.UNDEFINED) isFile++;  // contains a period
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
			// Called with form is closed
			if (WindowState == FormWindowState.Normal)
			{
				Properties.Settings.Default.Location = Location;
				Properties.Settings.Default.Size = Size;
				Properties.Settings.Default.Minimized = false;
			}
			else
			{
				Properties.Settings.Default.Location = RestoreBounds.Location;
				Properties.Settings.Default.Size = RestoreBounds.Size;
				Properties.Settings.Default.Minimized = true;
			}
			Properties.Settings.Default.Save();
			this.Cursor = Cursors.Default;

		} // End SaveFormPosition

		private void RestoreFormPosition()
		{
			// Called when form is loaded
			//TODO: This only gets the area of the first screen in a multi-screen setup

			int myWidth = Properties.Settings.Default.Size.Width;
			int myHeight = Properties.Settings.Default.Size.Height;
			int myLeft = Properties.Settings.Default.Location.X;
			int myTop = Properties.Settings.Default.Location.Y;
			int scrWd = SystemInformation.WorkingArea.Width;
			int scrHt = SystemInformation.WorkingArea.Height;

			if (myTop > (scrHt - myHeight))
			{
				myTop = scrHt - myHeight;
			}
			if (myLeft > (scrWd - myWidth))
			{
				myLeft = scrWd - myWidth;
			}

			// Should get all screens and figure out if size/placement of the form is valid
			//TODO: Restore form.WindowState and if maximized use RestoreBounds()
			//this.SetDesktopLocation(myLeft, myTop);
			//this.Width = myWidth;
			//this.Height = myHeight;
			this.SetBounds(myLeft, myTop, myWidth, myHeight);
			frmReport_ResizeEnd(null, null);


		} // End RestoreFormPosition

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

		private void StartReport(string reportFilename, string title, string subtitle)
		{
			bool keepGoing = true;
			string lineIn = "";
			lineCount = 0;
			StreamReader reader = new StreamReader(defaultStylesTemplate);
			writer = new StreamWriter(reportFilename);

			int pos = LOR.UNDEFINED;
			while (keepGoing && (lineIn = reader.ReadLine()) != null)
			{
				writer.WriteLine(lineIn); lineCount++;
				pos = lineIn.IndexOf("</style>");
				if (pos > LOR.UNDEFINED)
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




		} // end of StartReport procedure

		private void CloseReport(string footer)
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

		} // end of CloseReport procedure

		private void AddSection(string sectionName)
		{
			lineOut = PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_START + CLASS_SECTION + sectionName + PARA_END;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = PARA_BEGIN;
			writer.WriteLine(lineOut); lineCount++;

		} // end AddItem

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

			lineOut = SPAN_BEGIN;
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
			seq.ReadSequenceFile(sequenceFilename);
			string title = "Info&#45;Rama Report";
			string subtitle = "for \"" + Path.GetFileName(sequenceFilename) + "\"";
			StartReport(reportFilename, title, subtitle);

			ReportSummary();
			ReportFileInfo();
			ReportSequenceInfo();
			if (seq.sequenceType == sequenceType.musical)
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

			string footerInfo = MakeFooter(sequenceFilename);
			CloseReport(footerInfo);
			//Process.Start(reportFilename);
			fileReport = reportFilename;
			ImBusy(false);

		} // end CreateReport

		private string MakeFooter(string sequenceFilename)
		{
			string footerInfo = "Info&#45;Rama Report for \"" + HTMLizeFilename(sequenceFilename) + "\"";
			footerInfo += " created at " + MyFavoriteDateFormat(DateTime.Now) + BREAK;
			footerInfo += "<a href = \"http://wizlights.com/util-o-rama/ifno-o-rama\">Info&#45;Rama</a> is part of the ";
			footerInfo += "<a href = \"http://wizlights.com/util-o-rama\">Util&#45;O&#45;Rama</a> Suite for editing ";
			footerInfo += "<a href = \"http://www1.lightorama.com/\">Light&#45;O&#45;Rama</a> Sequences." + BREAK;
			footerInfo += "These utilities are released as freeware for the	benefit of the Light&#45;O&#45;Rama	community" + BREAK;
			footerInfo += "Source code is available on GitHub under a General Public License(GPL)" + BREAK;
			footerInfo += "Util&#45;O&#45;Rama and Info&#45;Rama are &copy; copyright 2017&#45;2018 by <a href = \"http://drwiz.guru\">Doctor Wizard</a>";
			footerInfo += " and <a href = \"http://wizster.com\">Wizster Software.</a>" + BREAK;
			footerInfo += "The Util&#45;O&#45;Rama Suite is not a product of, nor officially endorsed in any way by the Light&#45;O&#45;Rama company.";
			footerInfo += "  This is purely the work of Dr. Wizard and Wizster.";
			footerInfo += "  Please do not contact Light&#45;O&#45;Rama for support regarding the Util&#45;O&#45;Rama applications." + BREAK;
			footerInfo += "Submit bug reports, suggestions, ideas, rants, cool sequences, and good dirty jokes to ";
			footerInfo += "<a href= \"mailto:dev.utilorama@wizster.com?subject=Util-O-Rama Report\">dev.utilorama@wizster.com</a>";

			return footerInfo;
		}

		private string HTMLizeFilename(string name)
		{
			string ret = "";
			int pos1 = LOR.UNDEFINED;
			int pos2 = 0;

			pos1 = name.IndexOf('&', pos2);
			while (pos1 > LOR.UNDEFINED)
			{
				ret += name.Substring(pos2, pos1 - pos2) + "&amp;";
				pos2 = pos1 + 1;
				pos1 = name.IndexOf('\\', pos2);
			}
			ret += name.Substring(pos2);

			name = ret;
			ret = "";

			pos1 = LOR.UNDEFINED;
			pos2 = 0;
			pos1 = name.IndexOf('\\', pos2);
			while (pos1 > LOR.UNDEFINED)
			{
				ret += name.Substring(pos2, pos1 - pos2) + "&#92;";
				pos2 = pos1 + 1;
				pos1 = name.IndexOf('\\', pos2);
			}
			ret += name.Substring(pos2);

			return ret;
		}


		private string MyFavoriteDateFormat(DateTime dt)
		{
			string ret = dt.ToString("dddd, MMMM d");
			int dx = dt.Day % 10;
			if ((dx == 0) || (dx > 3))
			{
				ret += "th";
			}
			else
			{
				if (dx == 1)
				{
					ret += "st";
				}
				else
				{
					if (dx == 2)
					{
						ret += "nd";
					}
					else
					{
						ret += "rd";
					}
				}
			}
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
			AddSection("Quick Summary");

			AddItem("Created By", seq.info.author, itemLevel.normal);
			AddItem("Created At", seq.info.createdAt, itemLevel.normal);
			AddItem("Created By", seq.info.author, itemLevel.normal);
			AddItem("Last Modified", seq.info.fileModiedAt, itemLevel.normal);
			AddItem("File Size", seq.info.fileSizeFormated, itemLevel.normal);
			AddItem("Sequence Type", seq.sequenceType.ToString(), itemLevel.normal);
			if (seq.sequenceType == sequenceType.musical)
			{
				AddItem("Song Title", seq.info.music.Title, itemLevel.normal);
				AddItem("By Artist", seq.info.music.Artist, itemLevel.normal);
			}
			AddItem("Length", FormattedLength(), itemLevel.normal);
			AddItem("Lines", seq.lineCount.ToString(), itemLevel.normal);
			//TODO: Handle clipboard files correctly
			AddItem("Regular Channels", seq.channels.Count.ToString(), itemLevel.normal);
			AddItem("RGB Channels", seq.rgbChannels.Count.ToString(), itemLevel.normal);
			AddItem("Tracks", seq.tracks.Count.ToString(), itemLevel.normal);
			AddItem("Timing Grids", seq.timingGrids.Count.ToString(), itemLevel.normal);
			
		}

		private void ReportFileInfo()
		{
			AddSection("File Information");

			AddItem("Sequence File Name", Path.GetFileName(seq.filename), itemLevel.normal);
			string filePath = Path.GetDirectoryName(seq.filename);
			itemLevel nextLevel = itemLevel.normal;
			if (Sequence.SequencePath.ToLower().CompareTo(filePath.ToLower()) != 0) nextLevel = itemLevel.warning;
			//TODO: Handle clipboard and channel config files correctly
			AddItem("In Folder", filePath, nextLevel);
			if (nextLevel == itemLevel.normal)
			{
				AddInfo("Which is your default Light-O-Rama Sequence folder.", nextLevel);
			}
			else
			{
				AddItem("Your default Light-O-Rama Sequence folder is", Sequence.SequencePath, nextLevel);
			}
			AddItem("File Created on", MyFavoriteDateFormat(seq.info.fileCreatedDateTime), itemLevel.normal);
			AddItem("File Last Modified On", MyFavoriteDateFormat(seq.info.fileModiedDateTime), itemLevel.normal);
			AddItem("File Size", seq.info.fileSizeFormated, itemLevel.normal);
			AddItem("Line Count", seq.lineCount.ToString(), itemLevel.normal);
			if (seq.sequenceType == sequenceType.musical)
			{
				AddItem("Music File Name", Path.GetFileName(seq.info.music.File), itemLevel.normal);
				filePath = Path.GetDirectoryName(seq.info.music.File);
				nextLevel = itemLevel.normal;
				if (Sequence.AudioPath.ToLower().CompareTo(filePath.ToLower()) != 0) nextLevel = itemLevel.warning;
				AddItem("In Folder", filePath, nextLevel);
				if (nextLevel == itemLevel.normal)
				{
					AddInfo("Which is your default Light-O-Rama Audio folder.", nextLevel);
				}
				else
				{
					AddItem("Your default Light-O-Rama Audio folder is", Sequence.AudioPath, nextLevel);
				}
			}

		} // end FileInfo

		private void ReportSequenceInfo()
		{
			AddSection("Sequence Information");

			AddInfo("(this report section is still being developed)", itemLevel.warning);

		} // end SequenceInfo

		private void ReportMusicInfo()
		{
			AddSection("Music Information");

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

		private string FormattedLength()
		{
			long len = seq.totalCentiseconds;
			int mm = (int)(len / 6000);
			int ss = (int)(len - mm * 6000);
			int cs = (int)(len - mm * 6000 - ss * 100);
			string length = mm.ToString("0") + " minutes, " + ss.ToString("00") + "." + cs.ToString("00") + " seconds ";
			length += "(" + mm.ToString("0") + ":" + ss.ToString("00") + "." + cs.ToString("00") + ") ";
			length += " or " + len.ToString() + " centiseconds";
			return length;
		}



		private void frmReport_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				ProcessFileList(files);
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
		} // end ImBusy

		private void frmReport_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void btnBrowseSeq_Click(object sender, EventArgs e)
		{
			string initDir = Sequence.SequenceFolder;
			string initFile = "";

			dlgOpenFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
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

				webReport.Navigate(thisRpt);
				btnSaveReport.Enabled = true;
				this.Text = "Info-Rama - " + Path.GetFileName(thisFile);
				btnBrowseSeq.Text = "Analyze another Sequence...";
				ImBusy(false);

			} // end if (result = DialogResult.OK)
				//pnlAll.Enabled = true;

		}

		private void loadSequence(string seqFile)
		{
			seq.ReadSequenceFile(seqFile);
			fileSequence = seqFile;
			seq.SortAllNames();

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



		}
	} // end form frmReport
} // end namespace InfoRama
