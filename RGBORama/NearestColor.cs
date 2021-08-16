using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;  //!Temp
using System.Diagnostics; //!Temp
using LORUtils; using FileHelper;

namespace UtilORama
{
	class NearestColor
	{
		// NOT my names!  Based on .Net or HTML color names
		// Most common 8 colors moved to the front of the list
		// "LimeGreen" changed to just "Green" to match LOR
		// 140 total, 8 per line (thus 18 lines)
		public static readonly int ColorCount = 140;
		public static string[] ColorNames = {
			"Black-Off",			"Red",						"Green",						"Blue",						"White",						"Yellow",				"Cyan",						"Magenta",
			"AliceBlue",			"AntiqueWhite",		"Aqua",							"Aquamarine",			"Azure",						"Beige",				"Bisque",					"BlanchedAlmond",
			"BlueViolet",			"Brown",					"BurlyWood",				"CadetBlue",			"Chartreuse",				"Chocolate",		"Coral",					"CornflowerBlue",
			"Cornsilk",				"Crimson",				"DarkBlue",					"DarkCyan",				"DarkGoldenrod",		"DarkGray",			"DarkGreen",			"DarkKhaki",
			"DarkMagena",			"DarkOliveGreen",	"DarkOrange",				"DarkOrchid",			"DarkRed",					"DarkSalmon",		"DarkSeaGreen",		"DarkSlateBlue",
			"DarkSlateGray",	"DarkTurquoise",	"DarkViolet",				"DeepPink",				"DeepSkyBlue",			"DimGray",			"DodgerBlue",			"Firebrick",
			"FloralWhite",		"ForestGreen",		"Fuschia",					"Gainsboro",			"GhostWhite",				"Gold",					"Goldenrod",			"Gray",
			"DarkGreen",			"GreenYellow",		"Honeydew",					"HotPink",				"IndianRed",				"Indigo",				"Ivory",					"Khaki",
			"Lavender",				"LavenderBlush",	"LawnGreen",				"LemonChiffon",		"LightBlue",				"LightCoral",		"LightCyan",			"LightGoldenrodYellow",
			"LightGreen",			"LightGray",			"LightPink",				"LightSalmon",		"LightSeaGreen",		"LightSkyBlue",	"LightSlateGray",	"LightSteelBlue",
			"LightYellow",		"LimeGreen",			"Linen",						"Maroon",					"MediumAquamarine",	"MediumBlue",		"MediumOrchid",		"MediumPurple",
			"MediumSeaGreen",	"MediumSlateBlue","MediumSpringGreen","MediumTurquoise","MediumVioletRed",	"MidnightBlue",	"MintCream",			"MistyRose",
			"Moccasin",				"NavajoWhite",		"Navy",							"OldLace",				"Olive",						"OliveDrab",		"Orange",					"OrangeRed",
			"Orchid",					"PaleGoldenrod",	"PaleGreen",				"PaleTurquoise",	"PaleVioletRed",		"PapayaWhip",		"PeachPuff",			"Peru",
			"Pink",						"Plum",						"PowderBlue",				"Purple",					"RosyBrown",				"RoyalBlue",		"SaddleBrown",		"Salmon",
			"SandyBrown",			"SeaGreen",				"Seashell",					"Sienna",					"Silver",						"SkyBlue",			"SlateBlue",			"SlateGray",
			"Snow",						"SpringGreen",		"SteelBlue",				"Tan",						"Teal",							"Thistle",			"Tomato",					"Turquoise",
			"Violet",					"Wheat",					"WhiteSmoke",				"YellowGreen"     };

		// These colors are in .Net or HTML format Red-Grn-Blu
		public static Int32[] NetColorVals = {
			0x000000, 0xFF0000, 0X00FF00, 0X0000FF, 0XFFFFFF, 0XFFFF00, 0X00FFFF, 0XFF00FF,
			0xF0F8FF, 0xFAEBD7, 0x00FFFF, 0x7FFFD4, 0xF0FFFF, 0xF5F5DC, 0xFFE4C4, 0xFFEBCD,
			0x8A2BE2, 0xA52A2A, 0xDEB887, 0x5F9EA0, 0x7FFF00, 0xD2691E, 0xFF7F50, 0x6495ED,
			0xFFF8DC, 0xDC143C, 0x00008B, 0x008B8B, 0xB8860B, 0xA9A9A9, 0x006400, 0xBDB76B,
			0x8B008B, 0x556B2F, 0xFF8C00, 0x9932CC, 0x8B0000, 0xE9967A, 0x8FBC8B, 0x483D8B,
			0x2F4F4F, 0x00CED1, 0x9400D3, 0xFF1493, 0x00BFFF, 0x696969, 0x1E90FF, 0xB22222,
			0xFFFAF0, 0x228B22, 0xFF00FF, 0xDCDCDC, 0xF8F8FF, 0xFFD700, 0xDAA520, 0x808080,
			0x008000, 0xADFF2F, 0xF0FFF0, 0xFF69B4, 0xCD5C5C, 0x4B0082, 0xFFFFF0, 0xF0E68C,
			0xE6E6FA, 0xFFF0F5, 0x7CFC00, 0xFFFACD, 0xADD8E6, 0xF08080, 0xE0FFFF, 0xFAFAD2,
			0xD3D3D3, 0x90EE90, 0xFFB6C1, 0xFFA07A, 0x20B2AA, 0x87CEFA, 0x778899, 0xB0C4DE,
			0xFFFFE0, 0x32CD32, 0xFAF0E6, 0x800000, 0x66CDAA, 0x0000CD, 0xBA55D3, 0x9370DB,
			0x3CB371, 0x7B68EE, 0x00FA9A, 0x48D1CC, 0xC71585, 0x191970, 0xF5FFFA, 0xFFE4E1,
			0xFFE4B5, 0xFFDEAD, 0x000080, 0xFDF5E6, 0x808000, 0x6B8E23, 0xFFA500, 0xFF4500,
			0xDA70D6, 0xEEE8AA, 0x98FB98, 0xAFEEEE, 0xDB7093, 0xFFEFD5, 0xFFDAB9, 0xCD853F,
			0xFFC0CB, 0xDDA0DD, 0xB0E0E6, 0x800080, 0xBC8F8F, 0x4169E1, 0x8B4513, 0xFA8072,
			0xF4A460, 0x2E8B57, 0xFFF5EE, 0xA0522D, 0xC0C0C0, 0x87CEEB, 0x6A5ACD, 0x708090,
			0xFFFAFA, 0x00FF7F, 0x4682B4, 0xD2B48C, 0x008080, 0xD8BFD8, 0xFF6347, 0x40E0D0,
			0xEE82EE, 0xF5DEB3, 0xF5F5F5, 0x9ACD32 };

		// LOR colors as 24-bit ints in GBR order, full values 0-255 per color
		public static Int32[] LORcolorValsFull = {
			       0,      255,    65280, 16711680,	16777215,    65535,	16776960,	16711935,
			16775408,	14150650,	16776960,	13959039,	16777200,	14480885,	12903679,	13495295,
			14822282,  2763429,  8894686,	10526303,    65407,  1993170,  5275647,	15570276,
			14481663,  3937500,  9109504,  9145088,   755384, 11119017,    25600,  7059389,
			 9109643,  3107669,    36095, 13382297,      139,  8034025,  9157775,  9125192,
			 5197615,	13749760,	13828244,	 9639167,	16760576,	 6908265,	16748574,  2237106,
			15792895,  2263842,	16711935,	14474460,	16775416,    55295,  2139610,  8421504,
			   32768,  3145645,	15794160,	11823615,  6053069,  8519755,	15794175,  9234160,
			16443110,	16118015,    64636,	13499135,	15128749,  8421616,	16777184,	13826810,
			13882323,	 9498256,	12695295,	 8036607,	11186720,	16436871,	10061943,	14599344,
			14745599,	 3329330,	15134970,	     128,	11193702,	13434880,	13850042,	14381203,
			 7451452,	15624315,	10156544,	13422920,	 8721863,	 7346457,	16449525,	14804223,
			11920639,	11394815,	8388608,	15136253,	   32896,	 2330219,	   42495,    17919,
			14053594,	11200750,	10025880,	15658671,	 9662683,	14020607,	12180223,	 4163021,
			13353215,	14524637,	15130800,	 8388736,	 9408444,	14772545,	 1262987,  7504122,
			 6333684,	 5737262,	15660543,	 2970272,	12632256,	15453831,	13458026,	 9470064,
			16448255,	 8388352,	11829830,	 9221330,	 8421376,	14204888,	 4678655,	13688896,
			15631086,	11788021,	16119285,	3329434 };

		// LOR colors preprocessed as 24-bit ints, in GBR order, as percentages 0-100% per color, rounded to nearest 5%
		public static Int32[] LORcolorValsPct = {
		        0,     100,   25600, 6553600, 6579300,   25700, 6579200, 6553700,
			6578526, 5528674, 6579200, 5465138, 6579294, 5660768, 5069156, 5266532,
			5837110, 1052737, 3491927, 4144677,   25650,  797010, 2044516, 6109735,
			5661028, 1574998, 3604480, 3618560,  275784, 4342338,    9984, 2771018,
			3604535, 1190433,   14180, 5248060,      55, 3160923, 3623480, 3610652,
			2039570, 5394688, 5439546, 3803236, 6572800, 2697513, 6567948,  855366,
			6185572,  866061, 6553700, 5658198, 6578529,   21604,  868693, 3289650,
			  12800, 1205316, 6186078, 4663652, 2368592, 3342365, 6186084, 3627614,
			6445658, 6315620,   25393, 5268068, 5920068, 3289694, 6579288, 5399138,
			5460819, 3693880, 4999012, 3161956, 4408845, 6443317, 3945775, 5721413,
			5792868, 1331220, 5922402,      50, 4411432, 5242880, 5448009, 5647418,
			2901528, 6105392, 3957248, 5263900, 3409998, 2886154, 6448224, 5790052,
			4675940, 4478820, 3276800, 5922915,   12850,  931882,  	16740,    7012,
			5516373, 4414301, 3957308, 6118725, 3812438, 5529188, 4805988, 1651792,
			5262180, 5717847, 5920837, 3276850, 3684426, 5777689,  465719, 2962018,
			2506848, 2242322, 6119524, 1187903, 4934475, 6050101, 5251882, 3682860,
			6447716, 3302400, 4666139, 3622738, 3289600, 5589845, 1845092, 5396505,
			6107997, 4609888, 6316128, 1331260 };


		public static string FindNearestColorName(Color c)
		{
			int idx = FindNearestColorIndex(c);
			return ColorNames[idx];
		}

		public static string FindNearestColorName(int LORcolor)
		{
			Color c = utils.Color_LORtoNet(LORcolor);
			int idx = FindNearestColorIndex(c);
			return ColorNames[idx];
		}

		public static int FindNearestColorIndex(Color c)
		{
			int nearestSoFar = 999;
			int smallestDiff = 999;

			for (int n=0; n< ColorNames.Length; n++)
			{
				//int nr = (ColorVals[n] & 0xFF0000) >> 16;
				//int ng = (ColorVals[n] & 0xFF00) >> 8;
				//int nb = (ColorVals[n] & 0xFF);
				//int d = Math.Abs(c.R - nr);
				//d += Math.Abs(c.G - ng);
				//d += Math.Abs(c.B - nb);

				int d = ColorDistance(c, Color.FromArgb(NetColorVals[n]));
				if (d<smallestDiff)
				{
					smallestDiff = d;
					nearestSoFar = n;
					if (d == 0) n = ColorNames.Length; // If exact match found, no need to check the rest, force exit of loop
				}
			}
			return nearestSoFar;
		}

		public static int FindNearestColorIndexFull(int LORcolorFull)
		{
			int nearestSoFar = 999;
			int smallestDiff = 999;

			for (int n = 0; n < ColorNames.Length; n++)
			{
				int d = ColorDistance(LORcolorFull, LORcolorValsFull[n]);
				if (d < smallestDiff)
				{
					smallestDiff = d;
					nearestSoFar = n;
					if (d == 0) n = ColorNames.Length; // If exact match found, no need to check the rest, force exit of loop
				}
			}
			return nearestSoFar;
		}

	public static int FindNearestColorIndexPct(int LORcolorPct)
	{
		int nearestSoFar = 999;
		int smallestDiff = 999;

		for (int n = 0; n < ColorNames.Length; n++)
		{
			int d = ColorDistance(LORcolorPct, LORcolorValsPct[n]);
			if (d < smallestDiff)
			{
				smallestDiff = d;
				nearestSoFar = n;
				if (d == 0) n = ColorNames.Length; // If exact match found, no need to check the rest, force exit of loop
			}
		}
		return nearestSoFar;
	}

public static int ColorDistance(Color c1, Color c2)
	{
		int d = Math.Abs(c1.R - c2.R);
		d += Math.Abs(c1.G - c2.G);
		d += Math.Abs(c1.B - c2.B);
		return d;
	}

		public static int ColorDistance(int LORcolor, Color NetColor)
		{
			int lr = LORcolor & 0x0000FF;
			int lg = LORcolor & 0x00FF00 >> 8;
			int lb = LORcolor & 0xFF0000 >> 16;
			int d = Math.Abs(lr - NetColor.R);
			d += Math.Abs(lg - NetColor.G);
			d += Math.Abs(lb - NetColor.B);
			return d;
		}

		public static int ColorDistance(int LORcolor1, int LORcolor2)
		{
			int lr = LORcolor1 & 0x0000FF;
			int lg = LORcolor1 & 0x00FF00 >> 8;
			int lb = LORcolor1 & 0xFF0000 >> 16;
			int kr = LORcolor2 & 0x0000FF;
			int kg = LORcolor2 & 0x00FF00 >> 8;
			int kb = LORcolor2 & 0xFF0000 >> 16;

			int d = Math.Abs(lr - kr);
			d += Math.Abs(lg - kg);
			d += Math.Abs(lb - kb);
			return d;
		}

		public static bool ColorMatch(Color c1, Color c2)
		{
			int d = ColorDistance(c1, c2);
			if (d == 0) return true; else return false;
		}

		public static bool ColorMatch(int LORcolor, Color NetColor)
		{
			int d = ColorDistance(LORcolor, NetColor);
			if (d == 0) return true; else return false;
		}

		public static string NetListToLorListFull()
		{
			string ret = "";
			int cc = 0;

			for (int n = 0; n < ColorNames.Length; n++)
			{
				int nr = (NetColorVals[n] & 0xFF0000) >> 16;
				int ng = (NetColorVals[n] & 0xFF00) >> 8;
				int nb = (NetColorVals[n] & 0xFF);

				int lorColor = nb << 16;
				lorColor += (ng << 8);
				lorColor += nr;

				ret += lorColor.ToString() + ",";
				if (cc == 7)
				{
					ret += "\n\r";
				}
				cc++;
				cc %= 8;
			}

			Clipboard.SetText(ret);
			Debug.Write(ret);
			return ret;

		}

		public static string NetListToLorListPct()
		{
			string ret = "";
			int cc = 0;

			for (int n = 0; n < ColorNames.Length; n++)
			{
				// Get 0-255 values from Red, Grn and Blu from the .Net color in RGB order
				int nr = (NetColorVals[n] & 0xFF0000) >> 16;
				int ng = (NetColorVals[n] & 0xFF00) >> 8;
				int nb = (NetColorVals[n] & 0xFF);

				// Scale down from 0-255 to 0-100% by dividing by 2.55,
				// and divide again 5 in order to round to nearest 5%
				// (/ 2.55 / 5 = / 12.75)
				decimal mr = nr / 12.75M;
				decimal mg = ng / 12.75M;
				decimal mb = nb / 12.75M;

				// Scale back up to 0-100%, (rounded to the nearest 5%)
				decimal lr = Math.Round(mr * 5M);
				decimal lg = Math.Round(mg * 5M);
				decimal lb = Math.Round(mb * 5M);

				// Convert back to int
				nr = (byte)lr;
				ng = (byte)lg;
				nb = (byte)lb;

				// Combine 3 bytes into 24-bit int
				int lorColor = nb << 16;
				lorColor += (ng << 8);
				lorColor += nr;

				ret += lorColor.ToString() + ",";
				if (cc == 7)
				{
					ret += "\n";
				}
				cc++;
				cc %= 8;
			}

			Clipboard.SetText(ret);
			Debug.Write(ret);
			return ret;

		}



	}
}
