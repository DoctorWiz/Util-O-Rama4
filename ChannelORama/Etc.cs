using FileHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR4;

namespace UtilORama4
{
	internal class Etc
	{
		public static readonly Color Color_RGB = ColorTranslator.FromHtml("#000001");
		public static readonly Color Color_RGBW = ColorTranslator.FromHtml("#000100");
		public static readonly Color Color_Multi = ColorTranslator.FromHtml("#010000");

		private static int lorVer = 0;
		private static int xlVer = 0;
		
		/*
		 * DEPRECIATED - Use LOR4Admin.NearestColorName() instead
		public static string ColorName(Color color)
		{
			string name = "*";

			long cargb = color.ToArgb() & 0xFFFFFF; // Ignore alpha
																							// Special overrides for our custom colors, since they either won't maatch, or will override, any known color names.
																							// In the order mostly likely to be used, to minimize the time spent in the loop below.
			if (     cargb == 0x000001)
				name = "RGB";
			else if (cargb == 0x000100)
				name = "RGBW";
			else if (cargb == 0x010000)
				name = "Multi";
			else if (cargb == 0xD0FFFF)
				name = "Cool White";
			else if (cargb == 0xFFE0D0)
				name = "Warm White";
			else if (cargb == 0xFF000)
				name = "Red";
			else if (cargb == 0x00FF00)
				name = "Green";
			else if (cargb == 0x0000FF)
				name = "Blue";
			else if (cargb == 0xFFFF00)
				name = "Yellow";
			else if (cargb == 0xFF8000)
				name = "Orange";
			else if (cargb == 0xFF80FF)
				name = "Pink";
			else if (cargb == 0xC000C0)
				name = "Purple";
			else if (cargb == 0x000000)
				name = "Black";
			else
			{
				int cr = color.R; int cg = color.G; int cb = color.B;
				foreach (KnownColor c in Enum.GetValues(typeof(KnownColor)))
				{
					Color kc = Color.FromKnownColor(c);
					string kname = kc.Name;
					int kr = kc.R; int kg = kc.G; int kb = kc.B;
					int cdif = Math.Abs(cr - kr) + Math.Abs(cg - kg) + Math.Abs(cb - kb);
					// Is it close?
					if (cdif < 10)
					{
						name = kname;
						break;
					}
				}
				//name=LOR4.LOR4SeqAdmin.NearestColor(color);
				name = LOR4Admin.NearestColorName(color);
			}
			return name;
		} // End function ColorName
		*/

		public static int LORVersion
		{
			get
			{
				int ret = 0;
				// Already figured out?  Return the cached version.
				if (lorVer > 0)
				{
					ret = lorVer;
				}
				else
				{
					// use lorVer to cache the determined version number.
					string pgms32 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
					string xlor = pgms32 + "\\Light-O-Rama\\Showtime\\S4\\LOR4Showtime.exe";
					if (Fyle.Exists(xlor))
					{
						lorVer = 4;
					}
					else
					{
						xlor = pgms32 + "\\Light-O-Rama\\Showtime\\S5\\LOR5Showtime.exe";
						if (Fyle.Exists(xlor))
						{
							lorVer = 5;
						}
						else
						{
							xlor = pgms32 + "\\Light-O-Rama\\LORSequencer.exe";
							if (Fyle.Exists(xlor))
							{
								FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(xlor);
								// Typical version string (e.g., "1.2.3.4")
								//string fileVersion = versionInfo.FileVersion;
								// Alternatively, get specific parts
								int major = versionInfo.FileMajorPart;
								//int minor = versionInfo.FileMinorPart;
								if (major > 3)
								{
									lorVer = major;
								}
								//Console.WriteLine($"Version: {fileVersion}");
							}
						}
					}
					ret = lorVer;
				}
				return ret;
			}
		}

		public static int xLightsVersion
		{
			get
			{
				int ret = 0;
				// Already figured out?  Use the cached number
				if (xlVer > 0)
				{
					ret = xlVer;
				}
				else
				{
					string pgms64 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
					// Returns: C:\Program Files
					// Note: Folder [should] always be "Program Files" even on non-English versions of Windows, but just in case, we look it up instead of hard coding it.
					// But it might not be on the C: Drive, so we look it up instead of hard coding it.
					// Also, we look for xLights in Program Files, not Program Files (x86), because xLights is a 64 bit application and should be installed in Program Files, even on 64 bit Windows.
					string xlexe = pgms64 + "\\xLights\\xLights\\xLights.exe";
					if (Fyle.Exists(xlexe))
					{
						//FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(xlexe);
						//int major = versionInfo.FileMajorPart;
						// Keep the found version in a cache name xlVer
						//xlVer = major;
						//int minor = versionInfo.FileMinorPart;
						//if (minor > 0)
						//{
						//	xlVer += (minor / 100);
						//}
						DateTime xdt = Fyle.FileCreatedDateTime(xlexe);
						xlVer = xdt.Year;
						ret = xlVer;
					}
				}
				return ret;
			}
		}


	} // End form class
} // End name space
