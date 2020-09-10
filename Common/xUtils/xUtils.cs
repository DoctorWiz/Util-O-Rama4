using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;

//using FuzzyString;

namespace xUtils
{
	public class utils
	{
		public const int UNDEFINED = -1;
		//public const string ICONtrack = "Track";
		//public const string ICONchannelGroup = "ChannelGroup";
		//public const string ICONcosmicDevice = "CosmicDevice";
		//public const string ICONchannel = "channel";
		//public const string ICONrgbChannel = "RGBchannel";
		//public const string ICONredChannel = "redChannel";
		//public const string ICONgrnChannel = "grnChannel";
		//public const string ICONbluChannel = "bluChannel";
		//public const string ICONredChannel = "FF0000";
		//public const string ICONgrnChannel = "00FF00";
		//public const string ICONbluChannel = "0000FF";


		public const string LOG_Error = "Error";
		public const string LOG_Info = "Info";
		public const string LOG_Debug = "Debug";
		private const string FORMAT_DATETIME = "MM/dd/yyyy hh:mm:ss tt";
		private const string FORMAT_FILESIZE = "###,###,###,###,##0";

		public static int nodeIndex = UNDEFINED;

		public const string FIELDname = " name";
		public const string FIELDtype = " type";
		//public const string FIELDsavedIndex = " savedIndex";
		//public const string FIELDcentisecond = " centisecond";
		//public const string FIELDmilliseconds = FIELDcentisecond + PLURAL;
		//public const string FIELDtotalmilliseconds = " totalmilliseconds";
		//public const string FIELDstartCentisecond = " startCentisecond";
		//public const string FIELDendCentisecond = " endCentisecond";

		//public const string FILE_SEQ = "All Sequences *.las, *.lms|*.las;*.lms";
		//public const string FILE_CFG = "All Sequences *.las, *.lms, *.lcc|*.las;*.lms;*.lcc";
		//public const string FILE_LMS = "Musical Sequence *.lms|*.lms";
		//public const string FILE_LAS = "Animated Sequence *.las|*.las";
		//public const string FILE_LCC = "Channel Configuration *.lcc|*.lcc";
		//public const string FILE_LEE = "Visualization *.lee|*.lee";
		//public const string FILE_CHMAP = "Channel Map *.ChMap|*.ChMap";
		//public const string FILE_ALL = "All Files *.*|*.*";
		//public const string FILT_OPEN_ANY = FILE_SEQ + "|" + FILE_LMS + "|" + FILE_LAS;
		//public const string FILT_OPEN_CFG = FILE_CFG + "|" + FILE_LMS + "|" + FILE_LAS + "|" + FILE_LCC;
		//public const string FILT_SAVE_EITHER = FILE_LMS + "|" + FILE_LAS;
		//public const string EXT_CHMAP = "ChMap";
		//public const string EXT_LAS = "las";
		//public const string EXT_LMS = "lms";
		//public const string EXT_LEE = "lee";
		//public const string EXT_LCC = "lcc";


		public const string SPC = " ";
		public const string LEVEL0 = "";
		//public const string LEVEL1 = "  ";
		//public const string LEVEL2 = "    ";
		//public const string LEVEL3 = "      ";
		//public const string LEVEL4 = "        ";
		//public const string LEVEL5 = "          ";
		public const string LEVEL1 = "\t";
		public const string LEVEL2 = "\t\t";
		public const string LEVEL3 = "\t\t\t";
		public const string LEVEL4 = "\t\t\t\t";
		public const string LEVEL5 = "\t\t\t\t\t";
		public const string CRLF = "\r\n";
		// Or, if you prefer tabs instead of spaces...
		//public const string LEVEL1 = "\t";
		//public const string LEVEL2 = "\t\t";
		//public const string LEVEL3 = "\t\t\t";
		//public const string LEVEL4 = "\t\t\t\t";
		public const string PLURAL = "s";
		public const string FIELDEQ = "=\"";
		public const string ENDQT = "\"";
		public const string STFLD = "<";
		public const string ENDFLD = "/>";
		public const string FINFLD = ">";
		public const string STTBL = "<";
		public const string FINTBL = "</";
		public const string ENDTBL = ">";

		public const string COMMA = ",";
		public const string SLASH = "/";
		public const char DELIM1 = '⬖';
		public const char DELIM2 = '⬘';
		public const char DELIM3 = '⬗';
		public const char DELIM4 = '⬙';
		private const char DELIM_Map = (char)164;  // ¤
		private const char DELIM_SID = (char)177;  // ±
		private const char DELIM_Name = (char)167;  // §
		private const char DELIM_X = (char)182;  // ¶
		private const string ROOT = "C:\\";
		private const string REGKEYx = "HKEY_CURRENT_USER\\SOFTWARE\\xLights";


		public struct RGB
		{
			private byte _r;
			private byte _g;
			private byte _b;

			public RGB(byte r, byte g, byte b)
			{
				this._r = r;
				this._g = g;
				this._b = b;
			}

			public byte R
			{
				get { return this._r; }
				set { this._r = value; }
			}

			public byte G
			{
				get { return this._g; }
				set { this._g = value; }
			}

			public byte B
			{
				get { return this._b; }
				set { this._b = value; }
			}

			public bool Equals(RGB rgb)
			{
				return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
			}
		}

		public struct HSV
		{
			private double _h;
			private double _s;
			private double _v;

			public HSV(double h, double s, double v)
			{
				this._h = h;
				this._s = s;
				this._v = v;
			}

			public double H
			{
				get { return this._h; }
				set { this._h = value; }
			}

			public double S
			{
				get { return this._s; }
				set { this._s = value; }
			}

			public double V
			{
				get { return this._v; }
				set { this._v = value; }
			}

			public bool Equals(HSV hsv)
			{
				return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
			}
		}

		public static Int32 HSVToRGB(HSV hsv)
		{
			double r = 0, g = 0, b = 0;

			if (hsv.S == 0)
			{
				r = hsv.V;
				g = hsv.V;
				b = hsv.V;
			}
			else
			{
				int i;
				double f, p, q, t;

				if (hsv.H == 360)
					hsv.H = 0;
				else
					hsv.H = hsv.H / 60;

				i = (int)Math.Truncate(hsv.H);
				f = hsv.H - i;

				p = hsv.V * (1.0 - hsv.S);
				q = hsv.V * (1.0 - (hsv.S * f));
				t = hsv.V * (1.0 - (hsv.S * (1.0 - f)));

				switch (i)
				{
					case 0:
						r = hsv.V;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = hsv.V;
						b = p;
						break;

					case 2:
						r = p;
						g = hsv.V;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = hsv.V;
						break;

					case 4:
						r = t;
						g = p;
						b = hsv.V;
						break;

					default:
						r = hsv.V;
						g = p;
						b = q;
						break;
				}

			}

			RGB x = new RGB((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
			Int32 ret = x.R * 0x10000 + x.G * 0x100 + x.B;
			return ret;
		}

		public static int ColorIcon(ImageList icons, Int32 colorVal)
		{
			int ret = -1;
			string tempID = colorVal.ToString("X6");
			// LOR's Color Format is in BGR format, so have to reverse the Red and the Blue
			string colorID = tempID.Substring(4, 2) + tempID.Substring(2, 2) + tempID.Substring(0, 2);
			ret = icons.Images.IndexOfKey (colorID);
			if (ret < 0)
			{
				// Convert rearranged hex value a real color
				Color theColor = System.Drawing.ColorTranslator.FromHtml("#" + colorID);
				// Create a temporary working bitmap
				Bitmap bmp = new Bitmap(16,16);
				// get the graphics handle from it
				Graphics gr = Graphics.FromImage(bmp);
				// A colored solid brush to fill the middle
				SolidBrush b = new SolidBrush(theColor);
				// define a rectangle for the middle
				Rectangle r = new Rectangle(2, 2, 12, 12);
				// Fill the middle rectangle with color
				gr.FillRectangle(b, r);
				// Draw a 3D button around it
				Pen p = new Pen(Color.Black);
				gr.DrawLine(p, 1, 15, 15, 15);
				gr.DrawLine(p, 15, 1, 15, 14);
				p = new Pen(Color.DarkGray);
				gr.DrawLine(p, 2, 14, 14, 14);
				gr.DrawLine(p, 14, 2, 14, 13);
				p = new Pen(Color.White);
				gr.DrawLine(p, 0, 0, 15, 0);
				gr.DrawLine(p, 0, 1, 0, 15);
				p = new Pen(Color.LightGray);
				gr.DrawLine(p, 1, 1, 14, 1);
				gr.DrawLine(p, 1, 2, 1, 14);

				// Add it to the image list, using it's hex color code as the key
				icons.Images.Add(colorID, bmp);
				// get it's numeric index
				ret = icons.Images.Count - 1;
			}
			// Return the numeric index of the new image
			return ret;
		}
		
		public static int InvalidCharacterCount(string testName)
		{
			int ret = 0;
			int pos1 = 0;
			int pos2 = UNDEFINED;

			// These are not valid anywhere
			char[] badChars = { '<', '>', '\"', '/', '|', '?', '*' };
			for (int c = 0; c < badChars.Length; c++)
			{
				pos1 = 0;
				pos2 = testName.IndexOf(badChars[c], pos1);
				while (pos2 > UNDEFINED)
				{
					ret++;
					pos1 = pos2 + 1;
					pos2 = testName.IndexOf(badChars[c], pos1);
				}
			}

			// and the colon is not valid past position 2
			pos1 = 2;
			pos2 = testName.IndexOf(':', pos1);
			while (pos2 > UNDEFINED)
			{
				ret++;
				pos1 = pos2 + 1;
				pos2 = testName.IndexOf(':', pos1);
			}

			return ret;
		}

		private static string NotifyGenericSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\Notification.Default";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayNotifyGenericSound()
		{
			string sound = NotifyGenericSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}

		private static string MenuClickSound
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\AppEvents\\Schemes\\Apps\\.Default\\MenuCommand";
				string sound = (string)Registry.GetValue(keyName, null, "");
				return sound;
			} // End get the Notify.Generic Sound filename
		}

		public static void PlayMenuClickSound()
		{
			string sound = MenuClickSound;
			if (sound.Length > 6)
			{
				if (File.Exists(sound))
				{
					SoundPlayer player = new SoundPlayer(sound);
					player.Play();
				}
			}
		}

		public static void WriteLogEntry(string message, string logType, string applicationName)
		{
			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string mySubDir = "\\UtilORama\\";
			if (applicationName.Length > 2)
			{
				applicationName.Replace("-", "");
				mySubDir += applicationName + "\\";
			}
			string file = appDataDir + mySubDir;
			if (!Directory.Exists(file)) Directory.CreateDirectory(file);
			file += logType + ".log";
			//string dt = DateTime.Now.ToString("yyyy-MM-dd ")
			string dt = DateTime.Now.ToString("s") + "=";
			string msgLine = dt + message;
			try
			{
				StreamWriter writer = new StreamWriter(file, append: true);
				writer.WriteLine(msgLine);
				writer.Flush();
				writer.Close();
			}
			catch (System.IO.IOException ex)
			{
				string errMsg = "An error has occurred in this application!\r\n";
				errMsg += "Another error has occurred while trying to write the details of the first error to a log file!\r\n\r\n";
				errMsg += "The first error was: " + message + "\r\n";
				errMsg += "The second error was: " + ex.ToString();
				errMsg += "The log file is: " + file;
				DialogResult dr = MessageBox.Show(errMsg, "Errors!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
			}
			finally
			{
			}

		} // end write log entry

		public static bool IsValidPath(string pathToCheck)
		{
			// Checks to see if path looks valid, does NOT check if it exists
			bool ret = false;
			try
			{
				string x = Path.GetFullPath(pathToCheck);
				ret = true;
				ret = Path.IsPathRooted(pathToCheck);
			}
			catch
			{
				ret = false;
			}
			return ret;
		}

		public static bool IsValidPath(string pathToCheck, bool andExists)
		{
			// Checks to see if path looks valid AND if it exists
			bool ret = IsValidPath(pathToCheck);
			if (ret)
			{
				try
				{
					string p = Path.GetDirectoryName(pathToCheck);
					ret = Directory.Exists(p);
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		public static bool IsValidPath(string pathToCheck, bool andExists, bool tryToCreate)
		{
			// Checks to see if path looks valid AND if it exists
			// Tries to create it seemingly valid but not existent
			// Returns true only if successfully created/exists
			// Note: andExists parameter ignored, not used
			bool ret = IsValidPath(pathToCheck);
			if (ret)
			{
				try
				{
					ret = Directory.Exists(pathToCheck);
					if (!ret)
					{
						Directory.CreateDirectory(pathToCheck);
						ret = Directory.Exists(pathToCheck);
					}
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		public static string WindowfyFilename(string proposedName, bool justName)
		{
			string finalName = proposedName;
			finalName = finalName.Replace('?', 'Ɂ');
			finalName = finalName.Replace('%', '‰');
			finalName = finalName.Replace('*', '＊');
			finalName = finalName.Replace('|', '∣');
			finalName = finalName.Replace("\"", "ʺ");
			finalName = finalName.Replace("&quot;", "ʺ");
			finalName = finalName.Replace('<', '˂');
			finalName = finalName.Replace('>', '˃');
			finalName = finalName.Replace('$', '﹩');
			if (justName)
			{
				// These are valid in a path as separators, but not in a file name
				finalName = finalName.Replace('/', '∕');
				finalName = finalName.Replace("\\", "╲");
				finalName = finalName.Replace(':', '：');
			}
			else
			{
				// Not valid in a WINDOWS path
				finalName = finalName.Replace('/', '\\');
			}


			return finalName;
		}

		public static bool IsWizard
		{
			get
			{
				bool ret = false;
				string usr = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				usr = usr.ToLower();
				int p = usr.IndexOf("\\");
				if (p >= 0) usr = usr.Substring(p+1);
				if (usr.CompareTo("wizard") == 0) ret = true;
				return ret;
			}
		}

		public static bool IsAWizard
		{
			get
			{
				bool ret = false;
				string usr = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
				usr = usr.ToLower();
				int i = usr.IndexOf("wizard");
				if (i >= 0) ret = true;
				return ret;
			}
		}

		public static string FileCreatedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetCreationTime(filename);
				ret = dt.ToString(FORMAT_DATETIME);
			}
			return ret;
		}

		public static DateTime FileCreatedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetCreationTime(filename);
			}
			return ret;
		}

		public static string FileModiedAt(string filename)
		{
			string ret = "";
			if (File.Exists(filename))
			{
				DateTime dt = File.GetLastWriteTime(filename);
				ret = dt.ToString(FORMAT_DATETIME);
			}
			return ret;
		}

		public static string FormatDateTime(DateTime dt)
		{
			string ret = dt.ToString(FORMAT_DATETIME);
			return ret;
		}

		public static DateTime fileModiedDateTime(string filename)
		{
			DateTime ret = new DateTime();
			if (File.Exists(filename))
			{
				ret = File.GetLastWriteTime(filename);
			}
			return ret;
		}

		public static string FileSizeFormated(string filename)
		{
			long sz = GetFileSize(filename);
			return FileSizeFormated(sz, "");
		}

		public static string FileSizeFormated(string filename, string thousands)
		{
			long sz = GetFileSize(filename);
			return FileSizeFormated(sz, thousands);
		}

		public static string FileSizeFormated(long filesize)
		{
			return FileSizeFormated(filesize, "");
		}

		public static string FileSizeFormated(long filesize, string thousands)
		{
			string thou = thousands.ToUpper();
			string ret = "0";
			if (thou == "B") // Force value in Bytes
			{
				ret = Bytes(filesize);
			}
			else
			{
				if (thou == "K") // Force value in KiloBytes
				{
					ret = KiloBytes(filesize);
				}
				else
				{
					if (thou == "M") // Force value in MegaBytes
					{
						ret = MegaBytes(filesize);
					}
					else
					{
						if (thou == "G") // Force value in GigaBytes
						{
							ret = GigaBytes(filesize);
						}
						else
						{
							thou = ""; // Return value in nearest size group
							if (filesize < 100000)
							{
								ret = Bytes(filesize);
							}
							else
							{
								if (filesize < 100000000)
								{
									ret = KiloBytes(filesize);
								}
								else
								{
									if (filesize < 100000000000)
									{
										ret = MegaBytes(filesize);
									}
									else
									{
										//if (filesize < 1099511627776)
										//{
										ret = GigaBytes(filesize);
										//}
									}
								}
							}
						}
					}
				}
			}
			return ret;
		}

		private static string Bytes(long size)
		{
			return size.ToString(FORMAT_FILESIZE) + " Bytes";
		}

		private static string KiloBytes(long size)
		{
			long k = size >> 10;
			string ret = k.ToString(FORMAT_FILESIZE);
			if (k < 100)
			{
				double r = (int)(size % 0x400);
				int d = 0;
				double f = 0;
				if (k < 10) f = (r / 10D); else f = (r / 100D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dk = Math.Round(ds / 1024D);
				k = (int)dk;
				ret = k.ToString(FORMAT_FILESIZE);
			}
			ret += " KB";
			return ret;
		}

		private static string MegaBytes(long size)
		{
			long m = size >> 20;
			string ret = m.ToString(FORMAT_FILESIZE);
			if (m < 100)
			{
				double r = (int)(size % 0x10000);
				int d = 0;
				double f = 0;
				if (m < 10) f = (r / 10000D); else f = (r / 100000D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dm = Math.Round(ds / 1048576D);
				m = (int)dm;
				ret = m.ToString(FORMAT_FILESIZE);
			}
			ret += " MB";
			return ret;
		}

		private static string GigaBytes(long size)
		{
			long g = size >> 30;
			string ret = g.ToString(FORMAT_FILESIZE);
			if (g < 100)
			{
				double r = (int)(size % 0x40000000);
				int d = 0;
				double f = 0;
				if (g < 10) f = (r / 10000000D); else f = (r / 100000000D);
				d = (int)Math.Round(f);
				ret += "." + d.ToString();
			}
			else
			{
				double ds = (double)size;
				double dg = Math.Round(ds / 1073741824D);
				g = (int)dg;
				ret = g.ToString(FORMAT_FILESIZE);
			}
			ret += " GB";
			return ret;
		}

		public static long GetFileSize(string filename)
		{
				long ret = 0;
				if (File.Exists(filename))
				{
					FileInfo fi = new FileInfo(filename);
					ret = fi.Length;
				}
				return ret;
		}

		public static string HumanizeName(string XMLizedName)
		{
			// Takes a name from XML and converts symbols back to the real thing
			string ret = XMLizedName;
			ret = ret.Replace("&quot", "\"");
			return ret;
		}

		public static string XMLifyName(string HumanizedName)
		{
			// Takes a human friendly name, possibly with illegal symbols in it
			// And replaces the illegal symbols with codes to make it XML friendly
			string ret = HumanizedName;
			ret = ret.Replace("\"", "&quot");
			return ret;
		}

		public static int ContainsKey(string lineIn, string keyWord)
		{
			string lowerLine = lineIn.ToLower();
			string lowerWord = keyWord.ToLower();
			int pos1 = UNDEFINED;
			// int pos2 = UNDEFINED;
			// string fooo = "";

			//pos1 = lowerLine.IndexOf(lowerWord + "=");
			pos1 = FastIndexOf(lowerLine, lowerWord);
			return pos1;
		}

		public static Int32 getKeyValue(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyValue(p, lineIn, keyWord);
			}
			else
			{
				return utils.UNDEFINED;
			}
		}

		public static Int32 getKeyValue(int pos1, string lineIn, string keyWord)
		{
			Int32 valueOut = UNDEFINED;
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2);
			valueOut = Convert.ToInt32(fooo);
			return valueOut;
		}

		public static string getKeyWord(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyWord(p, lineIn, keyWord);
			}
			else
			{
				return "";
			}

		}

		public static string getKeyWord(int pos1, string lineIn, string keyWord)
		{
			string valueOut = "";
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2);
			valueOut = fooo;

			return valueOut;
		}

		public static bool getKeyState(string lineIn, string keyWord)
		{
			int p = ContainsKey(lineIn, keyWord + "=\"");
			if (p >= 0)
			{
				return getKeyState(p, lineIn, keyWord);
			}
			else
			{
				return false;
			}
		}

		public static bool getKeyState(int pos1, string lineIn, string keyWord)
		{
			bool stateOut = false;
			int pos2 = UNDEFINED;
			string fooo = "";

			fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
			pos2 = fooo.IndexOf("\"");
			fooo = fooo.Substring(0, pos2).ToLower();
			if (fooo.CompareTo("true") == 0) stateOut = true;
			if (fooo.CompareTo("yes") == 0) stateOut = true;
			if (fooo.CompareTo("1") == 0) stateOut = true;
			return stateOut;
		}

		public static string SetKey(string fieldName, string value)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(fieldName);
			ret.Append(FIELDEQ);
			ret.Append(value);
			ret.Append(ENDQT);

			return ret.ToString();
		}

		public static string SetKey(string fieldName, int value)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(fieldName);
			ret.Append(FIELDEQ);
			ret.Append(value);
			ret.Append(ENDQT);

			return ret.ToString();
		}

		public static string StartTable(string tableName, int level)
		{
			StringBuilder ret = new StringBuilder();

			for (int l=0; l<level; l++)
			{
				ret.Append(LEVEL1);
			}
			ret.Append(STFLD);
			ret.Append(tableName);
			return ret.ToString();
		}

		public static string EndTable(string tableName, int level)
		{
			StringBuilder ret = new StringBuilder();

			for (int l = 0; l < level; l++)
			{
				ret.Append(LEVEL1);
			}
			ret.Append(utils.FINTBL);
			ret.Append(tableName);
			ret.Append(utils.ENDTBL);
			return ret.ToString();
		}

		public static void ClearOutputWindow()
		{
			if (!Debugger.IsAttached)
			{
				return;
			}

			/*
			//Application.DoEvents();  // This is for Windows.Forms.
			// This delay to get it to work. Unsure why. See http://stackoverflow.com/questions/2391473/can-the-visual-studio-debug-output-window-be-programatically-cleared
			Thread.Sleep(1000);
			// In VS2008 use EnvDTE80.DTE2
			EnvDTE.DTE ide = (EnvDTE.DTE)Marshal.GetActiveObject("VisualStudio.DTE.14.0");
			if (ide != null)
			{
				ide.ExecuteCommand("Edit.ClearOutputWindow", "");
				Marshal.ReleaseComObject(ide);
			}
			*/
		}

		public static string GetAppTempFolder()
		{

			string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string mySubDir = "\\UtilORama\\";
			string tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			mySubDir += Application.ProductName + "\\";
			mySubDir = mySubDir.Replace("-", "");
			tempPath = appDataDir + mySubDir;
			if (!Directory.Exists(tempPath))
			{
				Directory.CreateDirectory(tempPath);
			}
			return tempPath;
		}

		public static string FormatTime(int milliseconds)
		{
			string timeOut = "";

			int totsecs = (int)(milliseconds / 1000);
			int millis = milliseconds % 1000;
			int min = (int)(totsecs / 60);
			int secs = totsecs % 60;

			if (min>0)
			{
				timeOut = min.ToString() + ":";
				timeOut += secs.ToString("00");
			}
			else
			{
				timeOut = secs.ToString();
			}
			timeOut += "." + millis.ToString("00");

			return timeOut;
		}

		public static int DecodeTime(string theTime)
		{
			// format mm:ss.cs
			int msOut = UNDEFINED;
			int msTmp = UNDEFINED;

			// Split time by :
			string[] tmpM = theTime.Split(':');
			// Not more than 1 : ?
			if (tmpM.Length < 3)
			{
				// has a : ?
				if (tmpM.Length == 2)
				{
					// first part is minutes
					int min = 0;
					// try to parse minutes from first part of string
					int.TryParse(tmpM[0], out min);
					// each minute is 60000 milliseconds
					msTmp = min * 60000;
					// place second part of split into first part for next step of decoding
					tmpM[0] = tmpM[1];
				}
				// split seconds by . ?
				string[] tmpS = tmpM[0].Split('.');
				// not more than 1 . ?
				if (tmpS.Length < 3)
				{
					// has a . ?
					if (tmpS.Length == 2)
					{
						// next part is seconds
						int sec = 0;
						// try to parse seconds from first part of remaining string
						int.TryParse(tmpS[0], out sec);
						// each second is 1000 milliseconds (duh!)
						msTmp += (sec * 1000);
						// no more than 2 decimal places allowed
						if (tmpS[1].Length > 2)
						{
							tmpS[1] = tmpS[1].Substring(0, 2);
						}
						// place second part into first part for next step of decoding
						tmpS[0] = tmpS[1];
					}
					int cs = 0;
					int.TryParse(tmpS[0], out cs);
					msTmp += cs;
					msOut = msTmp;
				}
			}

			return msOut;
		}

		public static int FastIndexOf(string source, string pattern)
		{
			if (pattern == null) throw new ArgumentNullException();
			if (pattern.Length == 0) return 0;
			if (pattern.Length == 1) return source.IndexOf(pattern[0]);
			bool found;
			int limit = source.Length - pattern.Length + 1;
			if (limit < 1) return -1;
			// Store the first 2 characters of "pattern"
			char c0 = pattern[0];
			char c1 = pattern[1];
			// Find the first occurrence of the first character
			int first = source.IndexOf(c0, 0, limit);
			while (first != -1)
			{
				// Check if the following character is the same like
				// the 2nd character of "pattern"
				if (source[first + 1] != c1)
				{
					first = source.IndexOf(c0, ++first, limit - first);
					continue;
				}
				// Check the rest of "pattern" (starting with the 3rd character)
				found = true;
				for (int j = 2; j < pattern.Length; j++)
					if (source[first + j] != pattern[j])
					{
						found = false;
						break;
					}
				// If the whole word was found, return its index, otherwise try again
				if (found) return first;
				first = source.IndexOf(c0, ++first, limit - first);
			}
			return -1;
		}

		public static string ReplaceInvalidFilenameCharacters(string oldName)
		{
			//! This is for the main part of the filename only!
			//! It replaces things like \ and : so it can't be used for full paths + names
			string newName = oldName.Replace('<', '＜');
			newName = newName.Replace('>', '＞');
			newName = newName.Replace(':', '﹕');
			newName = newName.Replace('\"', '＂');
			newName = newName.Replace('/', '∕');
			newName = newName.Replace('\\', '＼');
			newName = newName.Replace('?', '？');
			newName = newName.Replace('|', '￤');
			newName = newName.Replace('$', '§');
			newName = newName.Replace('*', '＊');
			return newName;
		}

		public static string Time_millisecondsToMinutes(int milliseconds)
		{
			int mm = (int)(milliseconds / 60000);
			int ss = (int)((milliseconds - mm * 60000)/1000);
			int ms = (int)(milliseconds - mm * 60000 - ss * 1000);
			string ret = mm.ToString("0") + ":" + ss.ToString("00") + "." + ms.ToString("00");
			return ret;
		}

		public static int Time_MinutesTomilliseconds(string timeInMinutes)
		{
			// Time string must be formated as mm:ss.cs
			// Where mm is minutes.  Must be specified, even if zero.
			// Where ss is seconds 0-59.
			// Where cs is milliseconds 0-99.  Must be specified, even if zero.
			// Time string must contain one colon (:) and one period (.)
			// Maximum of 60 minutes  (Anything longer can result in unmanageable sequences)
			string newTime = timeInMinutes.Trim();
			int ret = UNDEFINED;
			int posColon = newTime.IndexOf(':');
			if ((posColon > 0) && (posColon < 3))
			{
				int posc2 = newTime.IndexOf(':', posColon + 1);
				if (posc2 < 0)
				{
					string min = newTime.Substring(0, posColon);
					string rest = newTime.Substring(posColon + 1);
					int posPer = rest.IndexOf('.');
					if ((posPer == 2))
					{
						int posp2 = rest.IndexOf('.', posPer + 1);
						if (posp2 < 0)
						{
							string sec = rest.Substring(0, posPer);
							string cs = rest.Substring(posPer + 1);
							int mn = utils.UNDEFINED;
							int.TryParse(min, out mn);
							if ((mn >=0) && (mn<61))
							{
								int sc = utils.UNDEFINED;
								int.TryParse(sec, out sc);
								if ((sc >=0 ) && (sc<60))
								{
									int c = utils.UNDEFINED;
									int.TryParse(cs, out c);
									if ((c >=0) && (c<1000))
									{
										ret = mn * 60000 + sc * 1000 + c;
									}
								}
							}
						}
					}
				}
			}

			return ret;
		}

		public static string DefaultDocumentsPath
		{
			get
			{
				string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				if (myDocs.Substring(myDocs.Length - 1, 1).CompareTo("\\") != 0) myDocs += "\\";
				return myDocs;
			}
		}

		public static string ShowDirectory
		{
			// AKA Sequences Folder
			get
			{
				string fldr = "";
				string root = ROOT;
				string userDocs = DefaultDocumentsPath;
				try
				{
					fldr = (string)Registry.GetValue(REGKEYx, "LastDir", root);
					if (fldr.Length > 6)
					{
						if (!Directory.Exists(fldr))
						{
							Directory.CreateDirectory(fldr);
						}
					}
				}
				catch
				{
					fldr = userDocs;
				}
				return fldr;
			} // End get ShowDirectory
		}

	} // end class utils
} // end namespace LORUtils
