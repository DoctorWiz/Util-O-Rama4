using System.Diagnostics;

namespace Musik
{
	public struct AudioInfo
	{
		public string Path;
		public string Filename;
		public string Type;
		public string AlbumArtist;        // Album Artist
		public string Album;
		public string Year;
		public uint DiscNo;      // Disc Number
		public uint Track;       // LORTrack4 No. (May not be #, may be string in format ##/## (LORTrack4 x of count))
		public string Artist;      // Audio Artist
		public string Composer;
		public string Title;
		public string Genre;
		public string Comment;
		public System.TimeSpan Duration;
		public int Milliseconds;
		public int Centiseconds;
		public double Bitrate;
		public bool VBR;           // true if VBR, false if CBR
		public long Size;           // in Bytes
		public System.DateTime Modified;
		public bool Valid;
		public string SortKey;
	} // End struct AudioInfo

	public enum AudioField : int
	{
		Number = 0,
		Path = 1,
		Filename = 2,
		Type = 3,
		AlbumArtist = 4, // Band
		Album = 5,
		Year = 6,
		DiscNo = 7,
		Track = 8,
		Artist = 9,
		Composer = 10,
		Title = 11,
		Genre = 12,
		Comment = 13,
		Duration = 14,
		Bitrate = 15,
		VBR = 16,
		Size = 17,
		Modified = 18,
		Valid = 19,
		SortKey = 20
	} // End enum AudioFields

	public static class AudioFields
	{
		//private ResourceManager resMgr = new ResourceManager("PlayaLista", Assembly.GetExecutingAssembly());

		public static string FieldName(AudioField viField)
		{
			string sOut = "";
			switch (viField)
			{
				case AudioField.Number:
					//sOut = resMgr.GetString("AudioFieldName_Number");

					sOut = "Number";
					break;

				case AudioField.Path:
					sOut = "Path";
					break;

				case AudioField.Filename:
					sOut = "Filename";
					break;

				case AudioField.Type:
					sOut = "Type";
					break;

				case AudioField.AlbumArtist: // Album Artist
					sOut = "Album Artist";
					break;

				case AudioField.Album:
					sOut = "Album";
					break;

				case AudioField.Year:
					sOut = "Year";
					break;

				case AudioField.DiscNo:
					sOut = "DiscNo";
					break;

				case AudioField.Track:
					sOut = "Track";
					break;

				case AudioField.Artist:
					sOut = "Artist";
					break;

				case AudioField.Composer:
					sOut = "Composer";
					break;

				case AudioField.Title:
					sOut = "Title";
					break;

				case AudioField.Genre:
					sOut = "Genre";
					break;

				case AudioField.Comment:
					sOut = "Comment";
					break;

				case AudioField.Duration:
					sOut = "Duration";
					break;

				case AudioField.Bitrate:
					sOut = "Bitrate";
					break;

				case AudioField.VBR:
					sOut = "VBR";
					break;

				case AudioField.Size:
					sOut = "Size";
					break;

				case AudioField.Modified:
					sOut = "Modified";
					break;

				case AudioField.Valid:
					sOut = "Valid";
					break;

				case AudioField.SortKey:
					sOut = "SortKey";
					break;

				default:
					sOut = "UNDEFINED";
					break;
			} // End switch
			return sOut;
		}

		public static string FieldDescr(int viField)
		{
			string sOut = "";
			switch (viField)
			{
				case (int)AudioField.Number:
					sOut = "Audio Number (Play Order)";
					break;

				case (int)AudioField.Path:
					sOut = "File Path";
					break;

				case (int)AudioField.Filename:
					sOut = "Filename";
					break;

				case (int)AudioField.Type:
					sOut = "File Type";
					break;

				case (int)AudioField.AlbumArtist:
					sOut = "Album Artist (Band)";
					break;

				case (int)AudioField.Album:
					sOut = "Album Name";
					break;

				case (int)AudioField.Year:
					sOut = "Year Released";
					break;

				case (int)AudioField.DiscNo:
					sOut = "Disc No.";
					break;

				case (int)AudioField.Track:
					sOut = "Track No.";
					break;

				case (int)AudioField.Artist:
					sOut = "[Audio] Artist";
					break;

				case (int)AudioField.Composer:
					sOut = "Composer";
					break;

				case (int)AudioField.Title:
					sOut = "LORTrack4 Title";
					break;

				case (int)AudioField.Genre:
					sOut = "Audio Genre";
					break;

				case (int)AudioField.Comment:
					sOut = "Audio Comment";
					break;

				case (int)AudioField.Duration:
					sOut = "Audio Length/Duration (In Seconds)";
					break;

				case (int)AudioField.Bitrate:
					sOut = "Bitrate";
					break;

				case (int)AudioField.VBR:
					sOut = "Is Variable Bitrate";
					break;

				case (int)AudioField.Size:
					sOut = "File Size (In KB)";
					break;

				case (int)AudioField.Modified:
					sOut = "File Last Modified (Date/Time)";
					break;

				case (int)AudioField.Valid:
					sOut = "File Path\\Filename is Valid";
					break;

				case (int)AudioField.SortKey:
					sOut = "Sort Key";
					break;

				default:
					sOut = "UNDEFINED FIELD";
					break;
			} // End switch
			return sOut;
		}

		public static int FieldID(string vsFieldName)
		{
			int iOut = -1;
			vsFieldName = vsFieldName.ToLower().Trim();
			switch (vsFieldName)
			{
				case "number":
					iOut = (int)AudioField.Number;
					break;

				case "path":
					iOut = (int)AudioField.Path;
					break;

				case "filename":
					iOut = (int)AudioField.Filename;
					break;

				case "type":
					iOut = (int)AudioField.Type;
					break;

				case "albumartist":
					iOut = (int)AudioField.AlbumArtist; // band
					break;

				case "album":
					iOut = (int)AudioField.Album;
					break;

				case "year":
					iOut = (int)AudioField.Year;
					break;

				case "discno":
					iOut = (int)AudioField.DiscNo;
					break;

				case "track":
					iOut = (int)AudioField.Track;
					break;

				case "artist":
					iOut = (int)AudioField.Artist;
					break;

				case "composer":
					iOut = (int)AudioField.Composer;
					break;

				case "title":
					iOut = (int)AudioField.Title;
					break;

				case "genre":
					iOut = (int)AudioField.Genre;
					break;

				case "comment":
					iOut = (int)AudioField.Comment;
					break;

				case "duration":
					iOut = (int)AudioField.Duration;
					break;

				case "bitrate":
					iOut = (int)AudioField.Bitrate;
					break;

				case "vbr":
					iOut = (int)AudioField.VBR;
					break;

				case "size":
					iOut = (int)AudioField.Size;
					break;

				case "modified":
					iOut = (int)AudioField.Modified;
					break;

				case "valid":
					iOut = (int)AudioField.Valid;
					break;

				case "sortkey":
					iOut = (int)AudioField.SortKey;
					break;

				default:
					iOut = -1;
					break;
			} // End switch
			return iOut;
		}
	}
}