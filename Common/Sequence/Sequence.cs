using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace LORUtils
{

	// A delegate type for hooking up change notifications.
	public delegate void ChangedEventHandler(object sender, EventArgs e);

	public class LOR
	{
		public const int UNDEFINED = -1;
	}

	//internal enum deviceType
	public enum deviceType
	{ None, LOR, DMX, Digital };

	public class music
	{
		public string Artist = "";
		public string Title = "";
		public string Album = "";
		public string File = "";
	}

	public enum effectType { None, intensity, shimmer, twinkle, DMX }

	//internal enum timingGridType
	public enum timingGridType
	{ None, freeform, fixedGrid };

	//internal enum tableType
	public enum tableType
	{ None, channel, rgbChannel, channelGroup, track, timingGrid }

	public enum rgbChild
	{ None, red, green, blue }

	public enum sequenceType
	{ Undefined, animated, musical, clipboard, channelConfig, visualizer }
	
	//internal class channel
	public class channel
	{
		private string channelName = "";
		public string name
		{ 
			get
			{
				return channelName;
			}
			set
			{
				// Allow it to be set initally, but then never changed
				//TODO Allow it to be changed, and raise a change event to set the alphaNameListIndexDirty flag of the parent sequence to true
				if (channelName.Length == 0)
				{
					channelName = value;
				}
			}
		}
		public Int32 color = 0;
		public long centiseconds = 0;
		public output output = new output();
		public int savedIndex = LOR.UNDEFINED;
		public int altSavedIndex = LOR.UNDEFINED;
		public bool written = false;
		public bool selected = false;
		public int mapsTo = LOR.UNDEFINED;
		public rgbChild rgbChild = rgbChild.None;
		public List<effect> effects = new List<effect>();

		public event ChangedEventHandler Changed;

		public channel Copy()
		{
			channel ret = new channel();
			ret.name = name;
			ret.color = color;
			ret.centiseconds = centiseconds;
			List<effect> newEffects = new List<effect>();
			for (int e=0; e<effects.Count; e++)
			{
				newEffects.Add(effects[e].Copy());
			}
			ret.effects = newEffects;

			return ret;
		}
	
		
		//TODO: add RemoveEffect procedure
		//TODO: add SortEffects procedure (by startCentisecond)
	} // end channel

	public class output
	{
		public deviceType deviceType = deviceType.None;
		public int unit = LOR.UNDEFINED;
		public int circuit = LOR.UNDEFINED;
		public int network = LOR.UNDEFINED;
		private const char DELIM4 = '⬙';
		public override string ToString()
		{
			return deviceType.ToString() + DELIM4 + unit.ToString("000") + DELIM4 + circuit.ToString("000") + DELIM4 +
			       network.ToString("00000");
		}

		public void Parse(string data)
		{
			string[] info = data.Split(DELIM4);
			if (info.Length == 4)
			{
				//TODO: Get DeviceType
				unit = Int16.Parse(info[1]);
				circuit = Int16.Parse(info[2]);
				network = Int32.Parse(info[3]);
			}
		}

		public override bool Equals(Object obj)
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType())
				return false;

			output o = (output)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}
	}

	//internal class savedIndex
	public class savedIndex
	{
		// savedIndexes are like array indexes or database record numbers.
		// They start at 0 (zero) and range up to the countLOR.UNDEFINED.
		// LOR.UNDEFINED (minus one) is assigned to new channels/groups at creation, but is not a
		// valid savedIndex.
		// A new savedIndex will be assigned to channels, RGB channels, or channel groups
		// when added to a sequence if they don't already have one.
		// (existing saved index will not be overwritten.)
		// savedIndexes may not be duplicated and/or skipped
		// Every channel, RGB channel, and channel group must have a savedIndex.
		// The savedIndex has no affect on the order items are displayed.
		// savedIDs used by TimingGrids are not the same as savedIndexes.
		public tableType objType = tableType.None;
		public int objIndex = LOR.UNDEFINED;
		public int altSavedIndex = LOR.UNDEFINED;
		public List<int> parents = new List<int>();
		public savedIndex altCopy()
		{
			savedIndex ret = new savedIndex();
			ret.objIndex = objIndex;
			ret.objType = objType;
			ret.parents = parents;
			return ret;
		}
	}

	//internal class rgbChannel
	public class rgbChannel
	{
		public string name = "";
		public long totalCentiseconds = 0;
		public int savedIndex = LOR.UNDEFINED;
		public int altSavedIndex = LOR.UNDEFINED;
		public int redChannelObjIndex = LOR.UNDEFINED;  // index/position in the channels[] array
		public int grnChannelObjIndex = LOR.UNDEFINED;
		public int bluChannelObjIndex = LOR.UNDEFINED;
		public int redSavedIndex = LOR.UNDEFINED;
		public int grnSavedIndex = LOR.UNDEFINED;
		public int bluSavedIndex = LOR.UNDEFINED;
		public bool written = false;
		public bool selected = false;
		public int mapsTo = LOR.UNDEFINED;
	}

	//internal class channelGroup : Sequence
	public class channelGroup
	{
		// Channel Groups can contain regular channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// channels and other groups may be in more than one group.
		// Don't create circular references of groups in each other.
		public int channelIndex = LOR.UNDEFINED;

		public int channelSavedIndex = LOR.UNDEFINED;
		public string name = "";
		public long totalCentiseconds = 0;
		public int savedIndex = LOR.UNDEFINED;
		public int altSavedIndex = LOR.UNDEFINED;
		public List<int> itemSavedIndexes = new List<int>();
		public bool written = false;
		public bool selected = false;
		public int mapsTo = LOR.UNDEFINED;

		public void AddItem(int itemSavedIndex)
		{
			bool alreadyAdded = false;
			for (int i = 0; i < itemSavedIndexes.Count; i++)
			{
				if (itemSavedIndex == itemSavedIndexes[i])
				{
					//TODO: Using saved index, look up name of item being added
					string sMsg = "This item has already been added to this Channel Group '" + name + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						System.Diagnostics.Debugger.Break();
					//TODO: Make this just a warning, put "add" code below into an else block
					//TODO: Do the same with Tracks
					alreadyAdded = true;
					i = itemSavedIndexes.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				itemSavedIndexes.Add(itemSavedIndex);
			}
		}

		//TODO: add RemoveItem procedure
	}

	public class effect
	{
		public effectType type = effectType.None;
		public long startCentisecond = LOR.UNDEFINED;
		public long endCentisecond = 9999999999L;
		public int intensity = LOR.UNDEFINED;
		public int startIntensity = LOR.UNDEFINED;
		public int endIntensity = LOR.UNDEFINED;

		public effect Copy()
		{
			effect ret = new effect();
			ret.startCentisecond = startCentisecond;
			ret.endCentisecond = endCentisecond;
			ret.intensity = intensity;
			ret.startIntensity = startIntensity;
			ret.endIntensity = endIntensity;

			return ret;
		}
	}

	public class timingGrid
	{
		// Tracks must have a timing grid assigned.
		// All sequences must contain at least one track, ergo all sequences neet at least one timing grid.
		public string name = "";

		// SaveID is like savedIndex for channels, but separate.
		// Same rules apply, see savedIndex above
		public int saveID = LOR.UNDEFINED;
		public int altSaveID = LOR.UNDEFINED;

		public timingGridType type = timingGridType.None;
		public int spacing = LOR.UNDEFINED;
		public List<long> timings = new List<long>();
		public bool selected = false;
		public bool written = false;

		public void AddTiming(long time)
		{
			if (timings.Count == 0)
			{
				timings.Add(time);
			}
			else
			{
				if (timings[timings.Count - 1] < time)
				{
					timings.Add(time);
				}
				else
				{
					if (timings.Count > 1)
					{
						if (timings[timings.Count - 2] > timings[timings.Count - 1])
						{
							// Array.Sort uses QuickSort, which is not the most efficient way to do this
							// Most efficient way is a (sort of) one-pass backwards bubble sort
							for (int n = timings.Count - 1; n > 0; n--)
							{
								if (timings[n] < timings[n - 1])
								{
									// Swap
									long temp = timings[n];
									timings[n] = timings[n - 1];
									timings[n - 1] = temp;
								}
							} // end shifting loop

							// Check for, and remove, duplicates
							int offset = 0;
							for (int n = 1; n < timings.Count; n++)
							{
								if (timings[n - 1] == timings[n])
								{
									offset++;
								}
								if (offset > 0)
								{
									timings[n - offset] = timings[n];
								}
							}
							if (offset > 0)
							{
								//itemCount -= offset;
							}
							// end duplicate check/removal
						} // end comparison
					} // end more than one
				}
			}
		} // end addTiming function
	
		//TODO: add RemovePosition
	} // end timingGrid class

	public class track
	{
		// Tracks are the ultimate top-level groups.
		// They do not have savedIndexes.
		// channels, RGB channels, and channel groups will not bee displayed
		// and will not be accessible unless added to a track
		// or a subitem of a group in a track.
		// All sequences must have at least one track.
		public string name = "";

		public long totalCentiseconds = 0;
		public int timingGridObjIndex = LOR.UNDEFINED;
		public int timingGridSaveID = LOR.UNDEFINED;
		public List<int> itemSavedIndexes = new List<int>();
		public List<loopLevel> loopLevels = new List<loopLevel>();
		public bool selected = false;
		public bool written = false;
		public int mapsTo = LOR.UNDEFINED;

		//TODO: add RemoveItem procedure
	} // end class track

	public class animation
	{
		public int rows = LOR.UNDEFINED;
		public int columns = LOR.UNDEFINED;
		public string image = "";
		public List<animationRow> animationRows = new List<animationRow>();
	} // end animation class

	public class animationRow
	{
		public int rowIndex = LOR.UNDEFINED;
		public List<animationColumn> animationColumns = new List<animationColumn>();
		
		public int AddAnimationColumn(animationColumn animationColumn)
		{
			animationColumns.Add(animationColumn);
			return animationColumns.Count - 1;
		}
	}

	public class animationColumn
	{
		public int columnIndex = LOR.UNDEFINED;
		public int channel = LOR.UNDEFINED;
	}

	public class loop
	{
		public long startCentsecond = LOR.UNDEFINED;
		public long endCentisecond = 9999999999L;
		public int loopCount = LOR.UNDEFINED;
	}

	public class loopLevel
	{
		public List<loop> loops = new List<loop>();
		public int loopsCount = 0;
	} // end loopLevel class

	public class info
	{
		public string filename = "";
		public string xmlInfo = "";
		public int saveFileVersion = 14;
		public string author = "";
		public string createdAt = "";
		public music music = new music();
		public int videoUsage = 0;
		public string animationInfo = "";
		protected sequenceType sequenceType = new sequenceType();
		public string sequenceInfo
		{
			get
			{
				//! Note: Getting sequence.info.sequenceInto returns a createdAt that is NOW
				//! Whereas getting sequence.sequenceInfo returns a createdAt of the original file
				// Use sequence.sequenceInfo when using WriteSequenceFile (which matches original unmodified file)
				// Use sequence.fileInfo.sequenceInfo when using WriteSequenceInxxxxOrder (which creates a whole new file)
				string ret = Sequence.STFLD + Sequence.TABLEsequence;
				ret += Sequence.SPC + Sequence.FIELDsaveFileVersion + Sequence.FIELDEQ + saveFileVersion.ToString() + Sequence.ENDQT;
				ret += Sequence.SPC + Sequence.FIELDauthor + Sequence.FIELDEQ + author + Sequence.ENDQT;
				string nowtime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
				ret += Sequence.SPC + Sequence.FIELDcreatedAt + Sequence.FIELDEQ + nowtime + Sequence.ENDQT;
				if (sequenceType == sequenceType.musical)
				{
					ret += Sequence.SPC + Sequence.FIELDmusicAlbum + Sequence.FIELDEQ + music.Album + Sequence.ENDQT;
					ret += Sequence.SPC + Sequence.FIELDmusicArtist + Sequence.FIELDEQ + music.Artist + Sequence.ENDQT;
					ret += Sequence.SPC + Sequence.FIELDmusicFilename + Sequence.FIELDEQ + music.File + Sequence.ENDQT;
					ret += Sequence.SPC + Sequence.FIELDmusicTitle + Sequence.FIELDEQ + music.Title + Sequence.ENDQT;
				}
				ret += Sequence.SPC + Sequence.FIELDvideoUsage + Sequence.FIELDEQ + videoUsage.ToString() + Sequence.ENDQT + Sequence.ENDTBL;
				return ret;
			}
		}

		public string fileCreatedAt
		{
			get
			{
				string ret = "";
				if (File.Exists(filename))
				{
					DateTime dt = File.GetCreationTime(filename);
					ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
				}
				return ret;
			}
		}

		public DateTime fileCreatedDateTime
		{
			get
			{
				DateTime ret = new DateTime();
				if (File.Exists(filename))
				{
					ret = File.GetCreationTime(filename);
				}
				return ret;
			}
		}

		public string fileModiedAt
		{
			get
			{
				string ret = "";
				if (File.Exists(filename))
				{
					DateTime dt = File.GetLastWriteTime(filename);
					ret = dt.ToString("MM/dd/yyyy hh:mm:ss tt");
				}
				return ret;
			}
		}

		public DateTime fileModiedDateTime
		{
			get
			{
				DateTime ret = new DateTime();
				if (File.Exists(filename))
				{
					ret = File.GetLastWriteTime(filename);
				}
				return ret;
			}
		}

		public string fileSizeFormated
		{
			get
			{
				string ret = "0";
				if (File.Exists(filename))
				{
					FileInfo fi = new FileInfo(filename);
					long l = fi.Length;
					if (l<1024)
					{
						ret = l.ToString() + " bytes";
					}
					else
					{
						if (l< 1048576)
						{
							ret = (l / 1024).ToString() + " KB";
						}
						else
						{
							if ( l< 1073741824)
							{
								ret = (l / 1048576).ToString() + " MB";
							}
							else
							{
								if (l < 1099511627776)
								{
									ret = (l / 1073741824).ToString() + " GB";
								}
							}
						}
					}
				}
				return ret;
			}
		}

		public long fileSize
		{
			get
			{
				long ret = 0;
				if (File.Exists(filename))
				{
					FileInfo fi = new FileInfo(filename);
					ret = fi.Length;
				}
				return ret;

			}
		}
	}

	public class Sequence
	{
		#region XML Tag Constants
		public const string TABLEchannel = "channel";
		public const string FIELDname = "name";
		public const string FIELDcolor = "color";
		public const string FIELDcentiseconds = "centiseconds";
		public const string FIELDdeviceType = "deviceType";
		public const string FIELDcircuit = "circuit";
		public const string FIELDnetwork = "network";
		public const string FIELDunit = "unit";
		public const string FIELDsavedIndex = "savedIndex";
		public const string TABLErgbChannel = "rgbChannel";
		public const string TABLEchannelGroup = "channelGroup";
		public const string TABLEchannelGroupList = "channelGroupList";
		public const string FIELDchannelGroup = "channelGroup";
		public const string TABLEcellDemarcation = "cellDemarcation";
		public const string TABLEchannelsClipboard = "channelsClipboard";
		public const string TABLEsequence = "sequence";
		public const string TABLEeffect = "effect";
		public const string FIELDtype = "type";
		public const string FIELDcentisecond = "centisecond";
		public const string FIELDtotalCentiseconds = "totalCentiseconds";
		public const string FIELDstartCentisecond = "startCentisecond";
		public const string FIELDendCentisecond = "endCentisecond";
		public const string FIELDintensity = "intensity";
		public const string FIELDstartIntensity = "startIntensity";
		public const string FIELDendIntensity = "endIntensity";
		public const string TABLEtimingGrid = "timingGrid";
		public const string FIELDsaveID = "saveID";
		public const string TABLEtiming = "timing";
		public const string FIELDspacing = "spacing";
		public const string TABLEtrack = "track";
		public const string STARTtracks = "<tracks>";
		public const string STARTgrids = "<timingGrids>";
		public const string FIELDsaveFileVersion = "saveFileVersion";
		public const string FIELDauthor = "author";
		public const string FIELDcreatedAt = "createdAt";
		public const string FIELDmusicAlbum = "musicAlbum";
		public const string FIELDmusicArtist = "musicArtist";
		public const string FIELDmusicFilename = "musicFilename";
		public const string FIELDmusicTitle = "musicTitle";
		public const string FIELDvideoUsage = "videoUsage";
		public const string TABLEloopLevels = "loopLevels";
		public const string TABLEloopLevel = "loopLevel";
		public const string FIELDloopCount = "loopCount";
		public const string FIELDloop = "loop";
		public const string STARTloops = "<loopLevels>";
		public const string TABLEanimation = "animation";
		public const string FIELDrow = "row";
		public const string FIELDcolumns = "columns";
		public const string FIELDindex = "index";
		public const string FIELDcolumnIndex = "column index";
		public const string FIELDimage = "image";
		public const string SPC = " ";
		public const string LEVEL0 = "";
		public const string LEVEL1 = "  ";
		public const string LEVEL2 = "    ";
		public const string LEVEL3 = "      ";
		public const string LEVEL4 = "        ";
		public const string LEVEL5 = "          ";
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
		public const string DEVICElor = "LOR";
		public const string DEVICEdmx = "DMX Universe";
		public const string DEVICEdigital = "Digital IO";
		public const string EFFECTintensity = "intensity";
		public const string EFFECTshimmer = "shimmer";
		public const string EFFECTtwinkle = "twinkle";
		public const string EFFECTdmx = "DMX intensity";
		public const string GRIDfreeform = "freeform";
		public const string GRIDfixed = "fixed";
		#endregion

		private StreamWriter writer;
		private string lineOut = ""; // line to be written out, gets modified if necessary
		//private int curSavedIndex = 0;
		public sequenceType sequenceType = sequenceType.Undefined;
		public List<channel> channels = new List<channel>();
		public List<rgbChannel> rgbChannels = new List<rgbChannel>();
		public List<channelGroup> channelGroups = new List<channelGroup>();
		public savedIndex[] savedIndexes = null;
		private savedIndex[] altSavedIndexes = null;
		private int[] altSaveIDs = null;
		public List<timingGrid> timingGrids = new List<timingGrid>();
		public List<track> tracks = new List<track>();
		public animation animation;
		public int lineCount = 0;
		public int highestSavedIndex = LOR.UNDEFINED;
		private int altHighestSavedIndex = LOR.UNDEFINED;
		public long totalCentiseconds = 0;
		public string filename
		{
			get
			{
				return info.filename;
			}
			set
			{
				info.filename = value;
			}
		}
		private string tempFileName;
		//private static string tempPath = "C:\\Windows\\Temp\\"; // Gets overwritten with X:\Username\AppData\Roaming\Util-O-Rama\
		private static string tempWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Util-O-Rama\\";
		private string newFilename;
		public int errorStatus = 0;
		public info info = new info();
		//public string animationInfo = "";
		public int videoUsage = 0;
		public string[] alphaTimingGridNames = null;
		public string[] alphaTrackNames = null;
		public string[] alphaChannelGroupNames = null;
		public string[] alphaRGBchannelNames = null;
		public string[] alphaChannelNames = null;
		public int[] alphaTrackNameIndexes = null;
		public int[] alphaChannelGroupNameIndexes = null;
		public int[] alphaRGBchannelNameIndexes = null;
		public int[] alphaChannelNameIndexes = null;
		public int[] alphaTimingGridNameIndexes = null;
		public bool alphaTrackNameIndexesDirty = true;
		public bool alphaChannelGroupNameIndexesDirty = true;
		public bool alphaRGBchannelNameIndexesDirty = true;
		public bool alphaChannelNameIndexesDirty = true;
		public bool alphaTimingGridNameIndexesDirty = true;

		public void sequence()
		{
			//! CONSTRUCTOR

		}


		public void SortChannelNames()
		{
			Array.Resize(ref alphaChannelNames, channels.Count);
			Array.Resize(ref alphaChannelNameIndexes, channels.Count);
			for (int ch=0; ch< channels.Count; ch++)
			{
				alphaChannelNames[ch] = channels[ch].name;
				alphaChannelNameIndexes[ch] = ch;
			}
			Array.Sort(alphaChannelNames, alphaChannelNameIndexes);
			alphaChannelNameIndexesDirty = false;

			/*
			Array.Resize(ref alphaTrackNames, tracks.Count);
			for (int tr = 0; tr < tracks.Count; tr++)
			{
				alphaTrackNames[tr] = tracks[tr].name;
			}
			Array.Resize(ref alphaTimingGridNames, timingGrids.Count);
			for (int tg=0; tg< timingGrids.Count; tg++)
			{
				alphaTimingGridNames[tg] = timingGrids[tg].name;
			}
			*/
		}

		public void SortRGBchannelNames()
		{
			Array.Resize(ref alphaRGBchannelNames, rgbChannels.Count);
			Array.Resize(ref alphaRGBchannelNameIndexes, rgbChannels.Count);
			for (int rch = 0; rch < rgbChannels.Count; rch++)
			{
				alphaRGBchannelNames[rch] = rgbChannels[rch].name;
				alphaRGBchannelNameIndexes[rch] = rch;
			}
			Array.Sort(alphaRGBchannelNames, alphaRGBchannelNameIndexes);
			alphaRGBchannelNameIndexesDirty = false;
		}

		public void SortChannelGroupNames()
		{
			Array.Resize(ref alphaChannelGroupNames, channelGroups.Count);
			Array.Resize(ref alphaChannelGroupNameIndexes, channelGroups.Count);
			for (int chg = 0; chg < channelGroups.Count; chg++)
			{
				alphaChannelGroupNames[chg] = channelGroups[chg].name;
				alphaChannelGroupNameIndexes[chg] = chg;
			}
			Array.Sort(alphaChannelGroupNames, alphaChannelGroupNameIndexes);
			alphaChannelGroupNameIndexesDirty = false;
		}

		public void SortTrackNames()
		{
			Array.Resize(ref alphaTrackNames, tracks.Count);
			Array.Resize(ref alphaTrackNameIndexes, tracks.Count);
			for (int tr=0; tr < tracks.Count; tr++)
			{
				alphaTrackNames[tr] = tracks[tr].name;
				alphaTrackNameIndexes[tr] = tr;
			}
			Array.Sort(alphaTrackNames, alphaTrackNameIndexes);
			alphaTrackNameIndexesDirty = false;
		}

		public void SortTimingGridNames()
		{
			Array.Resize(ref alphaTimingGridNames, timingGrids.Count);
			Array.Resize(ref alphaTimingGridNameIndexes, timingGrids.Count);
			for (int tg = 0; tg < timingGrids.Count; tg++)
			{
				alphaTimingGridNames[tg] = timingGrids[tg].name;
				alphaTimingGridNameIndexes[tg] = tg;
			}
			Array.Sort(alphaTimingGridNames, alphaTimingGridNameIndexes);
			alphaTimingGridNameIndexesDirty = false;
		}

		public void SortAllNames()
		{
			SortChannelNames();
			SortRGBchannelNames();
			SortChannelGroupNames();
			SortTrackNames();
			SortTimingGridNames();
		}

		public string sequenceInfo
		{
			//! Note: Getting sequence.info.sequenceInto returns a createdAt that is NOW
			//! Whereas getting sequence.sequenceInfo returns a createdAt of the original file
			// Use sequence.sequenceInfo when using WriteSequenceFile (which matches original unmodified file)
			// Use sequence.info.sequenceInfo when using WriteSequenceInxxxxOrder (which creates a whole new file)
			get
			{
				string ret = STFLD + TABLEsequence;
				ret += SPC + FIELDsaveFileVersion + FIELDEQ + info.saveFileVersion.ToString() + ENDQT;
				ret += SPC + FIELDauthor + FIELDEQ + info.author + ENDQT;
				ret += SPC + FIELDcreatedAt + FIELDEQ + info.createdAt + ENDQT;
				if (sequenceType == sequenceType.musical)
				{
					ret += SPC + FIELDmusicAlbum + FIELDEQ + info.music.Album + ENDQT;
					ret += SPC + FIELDmusicArtist + FIELDEQ + info.music.Artist + ENDQT;
					ret += SPC + FIELDmusicFilename + FIELDEQ + info.music.File + ENDQT;
					ret += SPC + FIELDmusicTitle + FIELDEQ + info.music.Title + ENDQT;
				}
				ret += SPC + FIELDvideoUsage + FIELDEQ + videoUsage.ToString() + ENDQT + ENDTBL;
				return ret;
			}
			set
			{
				info.saveFileVersion = getKeyValue(value, FIELDsaveFileVersion);
				info.author = getKeyWord(value, FIELDauthor);
				info.createdAt = getKeyWord(value, FIELDcreatedAt);
				info.music.File = getKeyWord(value, FIELDmusicFilename);
				bool musical = (info.music.File.Length > 4);
				if (musical)
				{
					sequenceType = sequenceType.musical;
					info.music.Album = getKeyWord(value, FIELDmusicAlbum);
					info.music.Artist = getKeyWord(value, FIELDmusicArtist);
					info.music.Title = getKeyWord(value, FIELDmusicTitle);
				}
				else
				{
					sequenceType = sequenceType.animated;
				}
				videoUsage = getKeyValue(value, FIELDvideoUsage);
			}
		}

		private class updatedTrack
		{
			public List<int> newSavedIndexes = new List<int>();
		}

		private struct match
		{
			public string name;
			public int savedIdx;
			public tableType type;
			public int itemIdx;
		}

		// CONSTRUCTOR
		//public void Sequence()
		//{
		//}

		public static string UserDataPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "UserDataPath", userDocs);
				return fldr;
			} // End get UserDataPath
		}

		public static string NonAudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "NonAudioPath", userDocs);
				return fldr;
			} // End get NonAudioPath (Sequences)
		}

		public static string SequencePath
		{
			get
			{
				return NonAudioPath;
			}
		}
			

		public static string AudioPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "AudioPath", userDocs);
				return fldr;
			} // End get AudioPath
		}

		public static string ClipboardsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "ClipboardsPath", userDocs);
				return fldr;
			} // End get ClipboardsPath
		}

		public static string ChannelConfigsPath
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "AudioPath", userDocs);
				if (fldr.Length < 5)
				{
					fldr = SequencePath + "ChannelConfigs\\";
					Registry.SetValue(keyName, "ChannelConfigsPath", fldr);
				}

				return fldr;
			} // End get ChannelConfigsPath
		}

		public int AddChannel(channel newChan)
		{
			if (newChan.savedIndex < 0)
			{
				highestSavedIndex++;
				newChan.savedIndex = highestSavedIndex;
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.channel;
				savedIndexes[highestSavedIndex].objIndex = channels.Count;
			}
			channels.Add(newChan);

			if (newChan.centiseconds > totalCentiseconds)
			{
				totalCentiseconds = newChan.centiseconds;
			}

			return highestSavedIndex;
		} // end AddChannel

		public int AddRGBChannel(rgbChannel newChan)
		{
			if (newChan.savedIndex < 1)
			{
				highestSavedIndex++;
				newChan.savedIndex = highestSavedIndex;
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.rgbChannel;
				savedIndexes[highestSavedIndex].objIndex = rgbChannels.Count;
			}
			rgbChannels.Add(newChan);
			if (newChan.totalCentiseconds > totalCentiseconds)
			{
				totalCentiseconds = newChan.totalCentiseconds;
			}
			return highestSavedIndex;
		} // end AddChannel

		public int AddChannelGroup(channelGroup newGroup)
		{
			if (newGroup.savedIndex < 1)
			{
				highestSavedIndex++;
				newGroup.savedIndex = highestSavedIndex;
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.channelGroup;
				savedIndexes[highestSavedIndex].objIndex = channelGroups.Count;
			}

			channelGroups.Add(newGroup);
			if (newGroup.totalCentiseconds > totalCentiseconds)
			{
				totalCentiseconds = newGroup.totalCentiseconds;
			}
			return highestSavedIndex;
		}

		//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures

		public int AddTrack(track newTrack)
		{
			tracks.Add(newTrack);
			if (newTrack.totalCentiseconds > totalCentiseconds)
			{
				totalCentiseconds = newTrack.totalCentiseconds;
			}
			return tracks.Count - 1;
		}
		
		public int AddSavedIndex(savedIndex si, int position)
		{
			if (position >= highestSavedIndex)
			{
				highestSavedIndex = position;
				Array.Resize(ref savedIndexes, highestSavedIndex + 3);
			}
			savedIndexes[position] = si;
			return position;
		}
		
		public string getItemName(int savedIndex)
		{
			string nameOut = "";
			if (this.savedIndexes[savedIndex].objType == tableType.channel)
			{
				nameOut = channels[savedIndexes[savedIndex].objIndex].name;
			}
			if (savedIndexes[savedIndex].objType == tableType.rgbChannel)
			{
				nameOut = rgbChannels[savedIndexes[savedIndex].objIndex].name;
			}
			if (savedIndexes[savedIndex].objType == tableType.channelGroup)
			{
				nameOut = channelGroups[savedIndexes[savedIndex].objIndex].name;
			}

			return nameOut;
		}

		public void Clear(bool areYouReallySureYouWantToDoThis)
		{
			if (areYouReallySureYouWantToDoThis)
			{
				// Zero these out from any previous run
				lineCount = 0;
				channels = new List<channel>();
				rgbChannels = new List<rgbChannel>();
				channelGroups = new List<channelGroup>();
				tracks = new List<track>();
				timingGrids = new List<timingGrid>();
				savedIndexes = null;

				highestSavedIndex = 0;
				highestSavedIndex = 0;
				totalCentiseconds = 0;

				info.filename = "";
				info.xmlInfo = "";
				sequenceInfo = "";
				info.animationInfo = "";
			} // end Are You Sure
		} // end Clear Sequence

		public int ReadSequenceFile(string existingFileName)
		{
			errorStatus = 0;
			StreamReader reader = new StreamReader(existingFileName);
			string lineIn; // line read in (does not get modified)
			int pos1 = LOR.UNDEFINED; // positions of certain key text in the line
			track trk = new track();
			loopLevel ll = new loopLevel();
			animationRow aniRow = new animationRow();

			Clear(true);

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// Regular Channels
				pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					channel chan = new channel();
					savedIndex si = new savedIndex();
					chan.name = cleanName(getKeyWord(lineIn, FIELDname));
					chan.color = getKeyValue(lineIn, FIELDcolor);
					chan.centiseconds = getKeyValue(lineIn, FIELDcentiseconds);
					chan.output.deviceType = enumDevice(getKeyWord(lineIn, FIELDdeviceType));
					chan.output.unit = getKeyValue(lineIn, FIELDunit);
					chan.output.network = getKeyValue(lineIn, FIELDnetwork);
					chan.output.circuit = getKeyValue(lineIn, FIELDcircuit);
					chan.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);

					AddChannel(chan);

					si.objType = tableType.channel;
					si.objIndex = channels.Count - 1;

					AddSavedIndex(si, chan.savedIndex);

					pos1 = lineIn.IndexOf(ENDFLD);
					if (pos1 < 0)
					{
						lineIn = reader.ReadLine();
						lineCount++;
						pos1 = lineIn.IndexOf(TABLEeffect + SPC + FIELDtype);
						if (pos1 > 0)
						{
							effect ef = new effect();
							ef.type = enumEffect(getKeyWord(lineIn, FIELDtype));
							ef.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
							ef.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
							ef.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
							ef.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
							ef.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
							chan.effects.Add(ef);

							lineIn = reader.ReadLine();
							lineCount++;
							pos1 = lineIn.IndexOf(TABLEeffect + SPC + FIELDtype);
						}
					}
				}
				else // Not a regular channel
				{
					// RGB Channels
					pos1 = lineIn.IndexOf(STFLD + TABLErgbChannel + SPC);
					if (pos1 > 0)
					{

						rgbChannel rgbc = new rgbChannel();
						savedIndex si = new savedIndex();
						rgbc.name = cleanName(getKeyWord(lineIn, FIELDname));
						rgbc.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
						rgbc.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						lineIn = reader.ReadLine();
						lineCount++;
						lineIn = reader.ReadLine();

						lineCount++;
						// RED
						rgbc.redSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.redChannelObjIndex = savedIndexes[rgbc.redSavedIndex].objIndex;
						channels[savedIndexes[rgbc.redSavedIndex].objIndex].rgbChild = rgbChild.red;
						savedIndexes[rgbc.redSavedIndex].parents.Add(rgbc.savedIndex);
						lineIn = reader.ReadLine();

						lineCount++;
						// GREEN
						rgbc.grnSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.grnChannelObjIndex = savedIndexes[rgbc.grnSavedIndex].objIndex;
						channels[savedIndexes[rgbc.grnSavedIndex].objIndex].rgbChild = rgbChild.green;
						savedIndexes[rgbc.grnSavedIndex].parents.Add(rgbc.savedIndex);
						lineIn = reader.ReadLine();

						lineCount++;
						// BLUE
						rgbc.bluSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.bluChannelObjIndex = savedIndexes[rgbc.bluSavedIndex].objIndex;
						channels[savedIndexes[rgbc.bluSavedIndex].objIndex].rgbChild = rgbChild.blue;
						savedIndexes[rgbc.bluSavedIndex].parents.Add(rgbc.savedIndex);

						AddRGBChannel(rgbc);

						si.objType = tableType.rgbChannel;
						si.objIndex = rgbChannels.Count - 1;

						AddSavedIndex(si, rgbc.savedIndex);
					}
					else  // Not an RGB Channel
					{
						// Channel Groups
						pos1 = lineIn.IndexOf(STFLD + TABLEchannelGroupList + SPC);
						if (pos1 > 0)
						{
							channelGroup changl = new channelGroup();
							savedIndex si = new savedIndex();
							changl.name = cleanName(getKeyWord(lineIn, FIELDname));
							changl.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);

							changl.channelSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
							changl.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);

							AddChannelGroup(changl);

							si.objType = tableType.channelGroup;
							si.objIndex = channelGroups.Count - 1;

							AddSavedIndex(si, changl.savedIndex);

							pos1 = lineIn.IndexOf(ENDFLD);
							if (pos1 < 0)
							{
								lineIn = reader.ReadLine();
								lineCount++;
								lineIn = reader.ReadLine();
								lineCount++;
								pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
								while (pos1 > 0)
								{
									int isl = getKeyValue(lineIn, FIELDsavedIndex);
									changl.AddItem(isl);
									savedIndexes[isl].parents.Add(changl.savedIndex);

									lineIn = reader.ReadLine();
									lineCount++;
									pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
								}
							}
						}
						else // Not a channel group
						{
							// Timing Grids
							pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
							if (pos1 > 0)
							{
								timingGrid tg = new timingGrid();
								tg.name = cleanName(getKeyWord(lineIn, FIELDname));
								tg.type = enumGridType(getKeyWord(lineIn, FIELDtype));
								tg.saveID = getKeyValue(lineIn, FIELDsaveID);
								tg.spacing = getKeyValue(lineIn, FIELDspacing);

								timingGrids.Add(tg);

								if (tg.type == timingGridType.freeform)
								{
									lineIn = reader.ReadLine();
									lineCount++;
									pos1 = lineIn.IndexOf(TABLEtiming + SPC + FIELDcentisecond);
									while (pos1 > 0)
									{
										int gpos = getKeyValue(lineIn, FIELDcentisecond);
										tg.AddTiming(gpos);
										lineIn = reader.ReadLine();
										lineCount++;
										pos1 = lineIn.IndexOf(TABLEtiming + SPC + FIELDcentisecond);
									}
								}
							}
							else // Not a timing grid
							{
								// Tracks
								pos1 = lineIn.IndexOf(STFLD + TABLEtrack + SPC);
								if (pos1 > 0)
								{
									trk = new track();
									trk.name = cleanName(getKeyWord(lineIn, FIELDname));
									trk.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
									totalCentiseconds = trk.totalCentiseconds;
									trk.timingGridSaveID = getKeyValue(lineIn, TABLEtimingGrid);
									for (int tg = 0; tg < timingGrids.Count; tg++)
									{
										if (trk.timingGridSaveID == timingGrids[tg].saveID)
										{
											trk.timingGridObjIndex = tg;
											tg = timingGrids.Count; // break
										}
									}

									AddTrack(trk);

									pos1 = lineIn.IndexOf(ENDFLD);
									if (pos1 < 0)
									{
										lineIn = reader.ReadLine();
										lineCount++;
										lineIn = reader.ReadLine();
										lineCount++;
										pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
										while (pos1 > 0)
										{
											int isi = getKeyValue(lineIn, FIELDsavedIndex);
											trk.itemSavedIndexes.Add(isi);
											savedIndexes[isi].parents.Add(-100 - tracks.Count);

											lineIn = reader.ReadLine();
											lineCount++;
											pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
										}
									}
								} // end if a track
								else // not a track
								{
									pos1 = lineIn.IndexOf(STFLD + TABLEloopLevel + FINFLD);
									if (pos1 > 0)
									{
										ll = new loopLevel();
										trk.loopLevels.Add(ll);
									}
									else
									{
										pos1 = lineIn.IndexOf(STFLD + FIELDloop + SPC);
										if (pos1>0)
										{
											loop loooop = new loop();
											loooop.startCentsecond = getKeyValue(lineIn, FIELDstartCentisecond);
											loooop.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
											loooop.loopCount = getKeyValue(lineIn, FIELDloopCount);
											ll.loops.Add(loooop);
										}
										else
										{
											pos1 = lineIn.IndexOf(STFLD + TABLEanimation + SPC);
											if (pos1 > 0)
											{
												animation = new animation();
												animation.rows = getKeyValue(lineIn, FIELDrow + PLURAL);
												animation.columns = getKeyValue(lineIn, FIELDcolumns);
												animation.image = getKeyWord(lineIn, FIELDimage);
											}
											else
											{
												pos1 = lineIn.IndexOf(STFLD + FIELDrow + SPC + FIELDindex);
												if (pos1 > 0)
												{
													aniRow = new animationRow();
													aniRow.rowIndex = getKeyValue(lineIn, FIELDrow + SPC + FIELDindex);
													animation.animationRows.Add(aniRow);
												}
												else
												{
													pos1 = lineIn.IndexOf(STFLD + FIELDcolumnIndex);
													if (pos1 > 1)
													{
														animationColumn aniCol = new animationColumn();
														aniCol.columnIndex = getKeyValue(lineIn, FIELDcolumnIndex);
														aniCol.channel = getKeyValue(lineIn, TABLEchannel);
														aniRow.AddAnimationColumn(aniCol);
													} // end animationColumn
													else
													{
														pos1 = lineIn.IndexOf("xml version=");
														if (pos1 > 0)
														{
															info.xmlInfo = lineIn;
														}
														pos1 = lineIn.IndexOf("saveFileVersion=");
														if (pos1 > 0)
														{
															sequenceInfo = lineIn;
														} // end if header lines, or not
													} // end if animationColumn, or not
												} // end if animationRow, or not
											} // end if animation, or not
										} // end if loop, or not (as in a loopLevel loop, not a for loop)
									} // end if loopLevel, or not
								} // end if a track, or not
							} // end if a timing grid, or not
						} // end if a channel group, or not
					} // end if a RGB channel, or not
				} // end if a regular channel, or not
			} // while lines remain
			reader.Close();

			if (errorStatus <= 0)
			{
				info.filename = existingFileName;
				//! for debugging
				//string sMsg = summary();
				//MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return errorStatus;
		} // end ReadSequenceFile

		public string summary()
		{
			string sMsg = "";
			sMsg += "           Filename: " + info.filename + CRLF + CRLF;
			sMsg += "              Lines: " + lineCount.ToString() + CRLF;
			sMsg += "   Regular Channels: " + channels.Count.ToString() + CRLF;
			sMsg += "       RGB Channels: " + rgbChannels.Count.ToString() + CRLF;
			sMsg += "     Channel Groups: " + channelGroups.Count.ToString() + CRLF;
			sMsg += "       Timing Grids: " + timingGrids.Count.ToString() + CRLF;
			sMsg += "             Tracks: " + tracks.Count.ToString() + CRLF;
			sMsg += "       Centiseconds: " + totalCentiseconds.ToString() + CRLF;
			sMsg += "Highest Saved Index: " + highestSavedIndex.ToString() + CRLF;
			return sMsg;
		}

		public int[] DuplicateNameCheck()
		{
			// returns null if no matches
			// if matches found, returns array with pairs of matches
			// Thus array size will always be factor of 2
			// with even numbers being a match to the odd number just after it
			// (or odd numbers being a match to the even number just before it)
			int[] ret = null;
			match[] matches = null;
			int q = 0;
			// Do we even have any channels?  If so...
			if (channels.Count > 0)
			{
				// resize list to channel count
				Array.Resize(ref matches, channels.Count);
				// loop thru channels, collect name and info
				for (int ch = 0; ch < channels.Count; ch++)
				{
					matches[ch].name = channels[ch].name;
					matches[ch].savedIdx = channels[ch].savedIndex;
					matches[ch].type = tableType.channel;
					matches[ch].itemIdx = ch;
				}
				q = channels.Count;
			} // channel count > 0

			// Any RGB Channels?
			if (rgbChannels.Count > 0)
			{
				// Loop thru 'em and add their name and info to the list
				for (int rg = 0; rg < rgbChannels.Count; rg++)
				{
					Array.Resize(ref matches, q + 1);
					matches[q].name = rgbChannels[rg].name;
					matches[q].savedIdx = rgbChannels[rg].savedIndex;
					matches[q].type = tableType.rgbChannel;
					matches[q].itemIdx = rg;

					q++;
				}
			} // RGB Channel Count > 0

			// Again for channel groups
			if (channelGroups.Count > 0)
			{
				for (int gr = 0; gr < channelGroups.Count; gr++)
				{
					Array.Resize(ref matches, q + 1);
					matches[q].name = channelGroups[gr].name;
					matches[q].savedIdx = channelGroups[gr].savedIndex;
					matches[q].type = tableType.channelGroup;
					matches[q].itemIdx = gr;

					q++;
				}
			} // end groups

			// Do we have at least 2 names
			if (q > 1)
			{
				// Sort by Name!
				SortMatches(matches, 0, matches.Length);
				int y = 0;
				// Loop thru sorted list, comparing each member to the previous one
				for (int ql=1; ql<q; ql++)
				{
					if (matches[ql].name.CompareTo(matches[q].name) == 0)
					{
						// If they match, add 2 elements to the output array
						Array.Resize(ref ret, y + 2);
						// and add their saved indexes
						ret[y] = matches[ql - 1].savedIdx;
						ret[y + 1] = matches[ql].savedIdx;
						y += 2;
					}
				} // end loop thru sorted list
			} // end at least 2 names

			return ret;
		}

		private void SortMatches(match[] matches, int low, int high)
		{
			int pivot_loc = (low + high) / 2;

			if (low < high)
			pivot_loc = PartitionMatches(matches, low, high);
			SortMatches(matches, low, pivot_loc - 1);
			SortMatches(matches, pivot_loc + 1, high);
		}

		private int PartitionMatches(match[] matches, int low, int high)
		{
			string pivot = matches[high].name;
			int i = low - 1;

			for (int j = low; j < high - 1; j++)
			{
				if (matches[j].name.CompareTo(pivot) <= 0)
				{
					i++;
					SwapMatches(matches, i, j);
				}
			}
			SwapMatches(matches, i + 1, high);
			return i + 1;
		}

		private void SwapMatches(match[] matches, int idx1, int idx2)
		{
			match temp = matches[idx1];
			matches[idx1] = matches[idx2];
			matches[idx2] = temp;
		
		}

		public int ReadClipboardFile(string existingFilename)
		{
			errorStatus = 0;
			StreamReader reader = new StreamReader(existingFilename);
			string lineIn; // line read in (does not get modified)
			int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			Clear(true);

			int curChannel = LOR.UNDEFINED;
			int curSavedIndex = LOR.UNDEFINED;
			int curEffect = LOR.UNDEFINED;
			int curTimingGrid = LOR.UNDEFINED;
			int curGridItem = LOR.UNDEFINED;
	
			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				pos1 = lineIn.IndexOf("xml version=");
				if (pos1 > 0)
				{
					info.xmlInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("saveFileVersion=");
				if (pos1 > 0)
				{
					sequenceInfo = lineIn;
				}
				pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					//channelsCount++;
				}
				pos1 = lineIn.IndexOf(STFLD + TABLEeffect + SPC);
				if (pos1 > 0)
				{
					//effectCount++;
				}
				if (tracks.Count == 0)
				{
				}
				pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
				if (pos1 > 0)
				{
					//timingGridCount++;
				}
				pos1 = lineIn.IndexOf(STFLD + TABLEtiming + SPC);
				if (pos1 > 0)
				{
					//gridItemCount++;
				}

				pos1 = lineIn.IndexOf(FIELDsavedIndex);
				if (pos1 > 0)
				{
					curSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					if (curSavedIndex > highestSavedIndex)
					{
						highestSavedIndex = curSavedIndex;
					}
				}
			}

			reader.Close();
			// CREATE ARRAYS TO HOLD OBJECTS
			//channels = new channel[channelsCount + 2];
			//savedIndexes = new savedIndex[highestSavedIndex + 3];
			//rgbChannels = new rgbChannel[rgbChannelCount + 2];
			//timingGrids = new timingGrid[timingGridCount + 2];
			//tracks = new track[1];
			int pixNo = 0;
			int chwhich = 0;

			//////////////////////////////////
			// * PASS 2 - COLLECT OBJECTS * //
			//////////////////////////////////
			reader = new StreamReader(existingFilename);
			lineCount = 0;
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// have we reached the tracks section?
				// does this line mark the start of a regular channel?
				pos1 = lineIn.IndexOf(TABLEchannel + ENDFLD);
				if (pos1 > 0)
				{
					curChannel++;
					channel chan = new channel();
					chan.name = "ch" + curChannel.ToString("00000");
					if (chwhich == 0)
					{
						chan.name += "(R)";
					}
					if (chwhich == 1)
					{
						chan.name += "(G)";
					}
					if (chwhich == 2)
					{
						chan.name += "(B)";
					}

					chan.name += "p" + pixNo.ToString("00000");
					//chan.color = getKeyValue(lineIn, FIELDcolor);
					//chan.centiseconds = getKeyValue(lineIn, FIELDcentiseconds);
					//chan.deviceType = enumDevice(getKeyWord(lineIn, FIELDdeviceType));
					//chan.unit = getKeyValue(lineIn, FIELDunit);
					//chan.network = getKeyValue(lineIn, FIELDnetwork);
					//chan.circuit = getKeyValue(lineIn, FIELDcircuit);
					//chan.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					//channels[curChannel] = chan;
					//curSavedIndex = chan.savedIndex;

					//si.objType = tableType.channel;
					//si.objIndex = curChannel;
					//savedIndexes[curSavedIndex] = si;
					if (chwhich == 2)
					{ }
					chwhich++;
					chwhich %= 3;

				}

				// does this line mark the start of an Effect?
				pos1 = lineIn.IndexOf(TABLEeffect + SPC + FIELDtype);
				if (pos1 > 0)
				{
					curEffect++;

					//DEBUG!
					if (curEffect > 638)
					{
						errorStatus = 1;
					}

					effect ef = new effect();
					ef.type = enumEffect(getKeyWord(lineIn, FIELDtype));
					ef.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
					ef.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
					ef.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
					ef.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
					ef.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
					channels[curChannel].effects.Add(ef);
				}

				// does this line mark the start of a Timing Grid?
				pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
				if (pos1 > 0)
				{
					curTimingGrid++;
					timingGrid tg = new timingGrid();
					tg.name = getKeyWord(lineIn, FIELDname);
					tg.type = enumGridType(getKeyWord(lineIn, FIELDtype));
					tg.saveID = getKeyValue(lineIn, FIELDsaveID);
					tg.spacing = getKeyValue(lineIn, FIELDspacing);
					timingGrids[curTimingGrid] = tg;

					if (tg.type == timingGridType.freeform)
					{
						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEtiming + SPC + FIELDcentisecond);
						while (pos1 > 0)
						{
							curGridItem++;
							int gpos = getKeyValue(lineIn, FIELDcentisecond);
							timingGrids[curTimingGrid].AddTiming(gpos);

							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TABLEtiming + SPC + FIELDcentisecond);
						}
					} // end grid is freeform
				} // end if timingGrid
			} // end while line is valid

			reader.Close();
			totalCentiseconds = tracks[0].totalCentiseconds;

			if (errorStatus <= 0)
			{
				info.filename = existingFilename;
			}

			return errorStatus;
		}

		public int WriteSequenceFile(string newFileName)
		{
			int curChannel = 0;
			int currgbChannel = 0;
			int curSavedIndex = 1;
			int curChannelGroupList = 0;
			int curGroupItem = 0;
			int curTimingGrid = 0;
			int curGridItem = 0;
			int curTrack = 0;
			int curTrackItem = 0;
			bool closeChannel = false;

			writeSequenceStart(newFileName);

			while (curChannel < channels.Count)
			{
				channel chnl = channels[curChannel];
				closeChannel = false;
				lineOut = LEVEL2 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDname + FIELDEQ + chnl.name + ENDQT;
				lineOut += SPC + FIELDcolor + FIELDEQ + chnl.color.ToString() + ENDQT;
				lineOut += SPC + FIELDcentiseconds + FIELDEQ + chnl.centiseconds.ToString() + ENDQT;
				if (chnl.output.deviceType == deviceType.LOR)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(chnl.output.deviceType) + ENDQT;
					lineOut += SPC + FIELDunit + FIELDEQ + chnl.output.unit.ToString() + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + chnl.output.circuit.ToString() + ENDQT;
				}
				else if (chnl.output.deviceType == deviceType.DMX)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(chnl.output.deviceType) + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + chnl.output.circuit.ToString() + ENDQT;
					lineOut += SPC + FIELDnetwork + FIELDEQ + chnl.output.network.ToString() + ENDQT;
				}
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + chnl.savedIndex.ToString() + ENDQT;
				curSavedIndex = chnl.savedIndex;
				// Are there any effects for this channel?
				if (chnl.effects.Count > 0)
				{
					// complete channel line with regular '>' then do effects
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					writeEffects(channels[curChannel]);
				} // while (effects[curEffect].channelIndex == curChannel)
				
				else // NO effects for this channal
				{
					// complete channel line with field end '/>'
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				if (closeChannel)
				{
					lineOut = LEVEL2 + FINTBL + TABLEchannel + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				// Was this an RGB Channel?
				if (currgbChannel < rgbChannels.Count)
				{
					if (rgbChannels[currgbChannel].bluSavedIndex == curSavedIndex)
					{
						lineOut = LEVEL2 + STFLD + TABLErgbChannel;
						lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + rgbChannels[currgbChannel].totalCentiseconds.ToString() + ENDQT;
						lineOut += SPC + FIELDname + FIELDEQ + rgbChannels[currgbChannel].name + ENDQT;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + rgbChannels[currgbChannel].savedIndex.ToString() + ENDQT;
						lineOut += FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL3 + STFLD + TABLEchannel + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + rgbChannels[currgbChannel].redSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + rgbChannels[currgbChannel].grnSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + rgbChannels[currgbChannel].bluSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL2 + FINTBL + TABLErgbChannel + FINFLD;
						writer.WriteLine(lineOut); lineCount++;

						curSavedIndex = rgbChannels[currgbChannel].savedIndex;
						currgbChannel++;
					}
				}

				if (curChannelGroupList < channelGroups.Count)
				{ // is a group coming up next?
					while (channelGroups[curChannelGroupList].savedIndex == curSavedIndex + 1)
					{
						lineOut = LEVEL2 + STFLD + TABLEchannelGroupList;
						lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + channelGroups[curChannelGroupList].totalCentiseconds.ToString() + ENDQT;
						lineOut += SPC + FIELDname + FIELDEQ + channelGroups[curChannelGroupList].name + ENDQT;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + channelGroups[curChannelGroupList].savedIndex.ToString() + ENDQT;
						lineOut += FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL3 + STFLD + TABLEchannelGroup + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						for (int igi = 0; igi < channelGroups[curChannelGroupList].itemSavedIndexes.Count; igi++)
						{
							lineOut = LEVEL4 + STFLD + TABLEchannelGroup;
							lineOut += SPC + FIELDsavedIndex + FIELDEQ + channelGroups[curChannelGroupList].itemSavedIndexes[igi].ToString() + ENDQT;
							lineOut += ENDFLD;
							writer.WriteLine(lineOut); lineCount++;

							curGroupItem++;
						}
						lineOut = LEVEL3 + FINTBL + TABLEchannelGroup + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL2 + FINTBL + TABLEchannelGroupList + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						curSavedIndex = channelGroups[curChannelGroupList].savedIndex;
						curChannelGroupList++;
					}
				}

				curChannel++;
			} // curChannel < channelCount

			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TIMING GRIDS
			writeTimingGrids(false);

			// TRACKS
			lineOut = "  <" + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			for (int trk=0; trk < tracks.Count; trk++)
			{
				curTrack = trk;
				lineOut = LEVEL2 + STFLD + TABLEtrack;
				if (tracks[curTrack].name.Length > 1)
				{
					lineOut += SPC + FIELDname + FIELDEQ + tracks[curTrack].name + ENDQT;
				}
				lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + tracks[curTrack].totalCentiseconds.ToString() + ENDQT;
				lineOut += SPC + TABLEtimingGrid + FIELDEQ + tracks[curTrack].timingGridSaveID.ToString() + ENDQT;
				lineOut += FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				lineOut = LEVEL3 + STFLD + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				for (int iti = 0; iti < tracks[curTrack].itemSavedIndexes.Count; iti++)
				{
					lineOut = LEVEL4 + STFLD + TABLEchannel;
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + tracks[curTrack].itemSavedIndexes[iti].ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;

					curTrackItem++;
				}

				lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				if (curTrack < tracks.Count)
				{
					writeLoopLevels(curTrack);
				}

				lineOut = LEVEL2 + FINTBL + TABLEtrack + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				curTrack++;
			}
			
			writeSequenceClose();

			errorStatus = renameTempFile(newFileName);

			return errorStatus;
		}

		private int renameTempFile(string finalFilename)
		{

			if (errorStatus == 0)
			{
				if (File.Exists(finalFilename))
				{
					//string bakFile = newFilename.Substring(0, newFilename.Length - 3) + "bak";
					string bakFile = finalFilename + ".bak";
					if (File.Exists(bakFile))
					{
						File.Delete(bakFile);
					}
					File.Move(finalFilename, bakFile);
				}
				File.Move(tempFileName, finalFilename);

				if (errorStatus <= 0)
				{
					//info.filename = finalFilename;
				}
			}

			return errorStatus;

		}

		private void writeAnimation()
		{
			if (animation != null)
			{
				if (animation.rows > 0)
				{
					lineOut = LEVEL1 + STFLD + TABLEanimation + SPC;
					lineOut += FIELDrow + PLURAL + FIELDEQ + animation.rows.ToString() + ENDQT + SPC;
					lineOut += FIELDcolumns + FIELDEQ + animation.columns.ToString() + ENDQT + SPC;
					lineOut += FIELDimage + FIELDEQ + animation.image + ENDQT + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
					for (int ar = 0; ar < animation.animationRows.Count; ar++)
					{
						lineOut = LEVEL2 + STFLD + FIELDrow + SPC + FIELDindex + FIELDEQ + animation.animationRows[ar].rowIndex.ToString() + ENDQT + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						for (int ac = 0; ac < animation.animationRows[ar].animationColumns.Count; ac++)
						{
							animationColumn aniCol = animation.animationRows[ar].animationColumns[ac];
							lineOut = LEVEL3 + STFLD + FIELDcolumnIndex + FIELDEQ + aniCol.columnIndex.ToString() + ENDQT + SPC;
							lineOut += TABLEchannel + FIELDEQ + aniCol.channel.ToString() + ENDQT + ENDFLD;
							writer.WriteLine(lineOut); lineCount++;
						} // end columns loop
						lineOut = LEVEL2 + FINTBL + FIELDrow + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
					} // end rows loop
					lineOut = LEVEL1 + FINTBL + TABLEanimation + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				} // end if animation row count
			} // end if animation != null
		} // end writeAnimation

		public int WriteClipboardFile(string newFilename)
		{
			//TODO: This procedure is totally untested!!

			errorStatus = 0;
			lineCount = 0;

			//backupFile(fileName);

			string tmpFile = newFilename + ".tmp";

			writer = new StreamWriter(tmpFile);
			lineOut = ""; // line to be written out, gets modified if necessary
										//int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			int curTimingGrid = 0;
			//int curGridItem = 0;
			//int curTrack = 0;
			//int curTrackItem = 0;
			int[] newSIs = new int[1];
			//int newSI = LOR.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[tracks.Count];

			lineOut = info.xmlInfo;
			writer.WriteLine(lineOut); lineCount++;
			lineOut = STFLD + TABLEchannelsClipboard + " version=\"1\" name=\"" + Path.GetFileNameWithoutExtension(newFilename) + "\"" + ENDFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write Timing Grid aka cellDemarcation
			lineOut = LEVEL1 + STFLD + TABLEcellDemarcation + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			for (int tm = 0; tm < timingGrids[0].timings.Count; tm++)
			{
				lineOut = LEVEL2 + STFLD + TABLEcellDemarcation;
				lineOut += SPC + FIELDcentisecond + FIELDEQ + timingGrids[curTimingGrid].timings[tm].ToString() + ENDQT;
				lineOut += ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
			}
			lineOut = LEVEL1 + FINTBL + TABLEcellDemarcation + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write JUST CHANNELS in display order
			// DO NOT write track, RGB group, or channel group info
			for (int trk = 0; trk < tracks.Count; trk++)
			{
				for (int ti = 0; ti < tracks[trk].itemSavedIndexes.Count; ti++)
				{
					int si = tracks[trk].itemSavedIndexes[ti];
					ParseItemsToClipboard(si);
				} // end for track items loop
			} // end for tracks loop

			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = FINTBL + TABLEchannelsClipboard + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			writer.Flush();
			writer.Close();

			if (File.Exists(newFilename))
			{
				string bakFile = newFilename.Substring(0, newFilename.Length - 3) + "bak";
				if (File.Exists(bakFile))
				{
					File.Delete(bakFile);
				}
				File.Move(newFilename, bakFile);
			}
			File.Move(tmpFile, newFilename);

			if (errorStatus <= 0)
			{
				info.filename = newFilename;
			}

			return errorStatus;
		} // end WriteClipboardFile

		private void ParseItemsToClipboard(int saveID)
		{
			int oi = savedIndexes[saveID].objIndex;
			tableType itemType = savedIndexes[saveID].objType;
			if (itemType == tableType.channel)
			{
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[oi]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
			} // end if channel

			if (itemType == tableType.rgbChannel)
			{
				rgbChannel rgbch = rgbChannels[oi];
				// Get and write Red Channel
				int ci = rgbChannels[oi].redChannelObjIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;

				// Get and write Green Channel
				ci = rgbChannels[oi].grnChannelObjIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;

				// Get and write Blue Channel
				ci = rgbChannels[oi].bluChannelObjIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
			} // end if rgbChannel

			if (itemType == tableType.channelGroup)
			{
				channelGroup grp = channelGroups[oi];
				for (int itm = 0; itm < grp.itemSavedIndexes.Count; itm++)
				{
					ParseItemsToClipboard(grp.itemSavedIndexes[itm]);
				}
			} // end if channelGroup
		} // end ParseChannelGroupToClipboard

		public int WriteFileInDisplayOrder(string newFilename)
		{
			return WriteSequenceFileInDisplayOrder(newFilename, false);
		}

		public int WriteSequenceFileInDisplayOrder(string newFileName, bool selectedOnly)
		{
			List<int> newSIs = new List<int>();
			int altSI = LOR.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[tracks.Count];
			altHighestSavedIndex = LOR.UNDEFINED;
			altSavedIndexes = null;
			Array.Resize(ref altSavedIndexes, highestSavedIndex + 3);
			Array.Resize(ref altSaveIDs, timingGrids.Count + 1);

			// Clear any 'written' flags from a previous save
			clearWrittenFlags();

			// Write the first line of the new sequence, containing the XML info
			writeSequenceStart(newFileName);

			// Timing Grids do not get written to the file yet
			// But we must renumber the saveIDs
			int altSaveID = 0;
			// Assign new altSaveIDs in the order they appear in the tracks
			for (int tr = 0; tr < tracks.Count; tr++)
			{
				if ((!selectedOnly) || (tracks[tr].selected))
				{
					// If the track is selected, it's timingGrid is also supposed to be selected
					// We will assume it is
					int tgoi = tracks[tr].timingGridObjIndex;
					if (timingGrids[tgoi].altSaveID == LOR.UNDEFINED)
					{
						timingGrids[tgoi].altSaveID = altSaveID;
						altSaveIDs[tgoi] = altSaveID;
						altSaveID++;
					}
				}
			}
			for (int tg = 0; tg < timingGrids.Count; tg++)
			{
				// Any remaining timing grids that are selected, but not used by any tracks
				if ((!selectedOnly) || (timingGrids[tg].selected))
				{
					if (timingGrids[tg].altSaveID == LOR.UNDEFINED)
					{
						timingGrids[tg].altSaveID = altSaveID;
						altSaveIDs[tg] = altSaveID;
						altSaveID++;
					}
				}
			}
			
			// loop thru tracks
			for (int t = 0; t < tracks.Count; t++)
			{
				// write out all items for this track
				//! Note: In this case, tableType.None is actually tableType.ALL
				newSIs = writeTrackItems(t, selectedOnly, tableType.None);
				updatedTrack ut = new updatedTrack();
				ut.newSavedIndexes = newSIs;
				updatedTracks[t] = ut;
			}

			// All channels should now be written, close this section
			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TIMING GRIDS
			writeTimingGrids(selectedOnly);

			// TRACKS
			lineOut = LEVEL1 + STFLD + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			// Loop thru tracks
			for (int trk=0; trk< tracks.Count; trk++)
			{
				if ((!selectedOnly) || (tracks[trk].selected))
				{
					writeTrack(trk, selectedOnly);
				}
			}
			
			writeSequenceClose();

			errorStatus = renameTempFile(newFileName);

			return errorStatus;
		} // end WriteSequenceFileInDisplayOrder

		private int writeSequenceStart(string newFileName)
		{
			//string lineOut = "";

			errorStatus = 0;
			lineCount = 0;
			newFilename = newFileName;
			tempFileName = tempWorkPath + Path.GetFileNameWithoutExtension(newFilename) + ".tmp";
			writer = new StreamWriter(tempFileName);
			
			// Write the first line of the new sequence, containing the XML info
			lineOut = info.xmlInfo;
			writer.WriteLine(lineOut); lineCount++;
			//string createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
			lineOut = info.sequenceInfo;
			writer.WriteLine(lineOut); lineCount++;

			// Start with Channels (regular, RGB, and Groups)
			lineOut = LEVEL1 + STFLD + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = "";

			return errorStatus;
		}

		public int WriteSequenceFileInCRGDisplayOrder(string newFileName, bool selectedOnly)
		{
			List<int> newChannelSIs = new List<int>();
			List<int> newRGBchannelSIs = new List<int>();
			List<int> newChannelGroupSIs = new List<int>();
			altSavedIndexes = null;
			Array.Resize(ref altSavedIndexes, highestSavedIndex + 3);
			Array.Resize(ref altSaveIDs, timingGrids.Count + 1);

			// Clear any 'written' flags from a previous save
			clearWrittenFlags();

			// Write the first line of the new sequence, containing the XML info
			writeSequenceStart(newFileName);

			// Timing Grids do not get written to the file yet
			// But we must renumber the saveIDs
			int altSaveID = 0;
			// Assign new altSaveIDs in the order they appear in the tracks
			for (int tr=0; tr< tracks.Count; tr++)
			{
				if ((!selectedOnly) || (tracks[tr].selected))
				{
					// If the track is selected, it's timingGrid is supposed to be selected also
					// We will assume it is
					int tgoi = tracks[tr].timingGridObjIndex;
					if (timingGrids[tgoi].altSaveID == LOR.UNDEFINED)
					{
						timingGrids[tgoi].altSaveID = altSaveID;
						altSaveIDs[tgoi] = altSaveID;
						altSaveID++;
					}
				}
			}
			for (int tg = 0; tg < timingGrids.Count; tg++)
			{
				// Any more timing grids that are selected, but aren't used by any tracks
				if ((!selectedOnly) || (timingGrids[tg].selected))
				{
					if (timingGrids[tg].altSaveID == LOR.UNDEFINED)
					{
						timingGrids[tg].altSaveID = altSaveID;
						altSaveIDs[tg] = altSaveID;
						altSaveID++;
					}
				}
			}

			// loop thru tracks, write items
			for (int t = 0; t < tracks.Count; t++)
			{
				// write out all items for this track
				newChannelSIs = writeTrackItems(t, selectedOnly, tableType.channel);
				newRGBchannelSIs = writeTrackItems(t, selectedOnly, tableType.rgbChannel);
				newChannelGroupSIs = writeTrackItems(t, selectedOnly, tableType.channelGroup);
			}

			// All channels should now be written, close this section
			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TIMING GRIDS
			writeTimingGrids(selectedOnly);

			// TRACKS
			lineOut = LEVEL1 + STFLD + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			// Loop thru tracks
			for (int trk = 0; trk < tracks.Count; trk++)
			{
				if ((!selectedOnly) || (tracks[trk].selected))
				{
					writeTrack(trk, selectedOnly);
				}
			}
			
			writeSequenceClose();

			errorStatus = renameTempFile(newFileName);

			return errorStatus;
		} // end WriteSequenceFileInCRGDisplayOrder

		private void writeTrack(int trackIndex, bool selectedOnly)
		{
			string lineOut = "";
			int siOld = LOR.UNDEFINED;
			int siAlt = LOR.UNDEFINED;
			track thisTrack = tracks[trackIndex];

			// Write info about track
			lineOut = LEVEL2 + STFLD + TABLEtrack;
			if (thisTrack.name.Length > 1)
			{
				lineOut += SPC + FIELDname + FIELDEQ + thisTrack.name + ENDQT;
			}
			lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + thisTrack.totalCentiseconds.ToString() + ENDQT;
			int altID = timingGrids[thisTrack.timingGridObjIndex].altSaveID;
			lineOut += SPC + TABLEtimingGrid + FIELDEQ + altID.ToString() + ENDQT;
			lineOut += FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			lineOut = LEVEL3 + STFLD + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Loop thru all items in this track
			for (int iti = 0; iti < thisTrack.itemSavedIndexes.Count; iti++)
			{
			// Write out the links to the items
				lineOut = LEVEL4 + STFLD + TABLEchannel;
				//newSI = updatedTracks[trackIndex].newSavedIndexes[iti];
				siOld = thisTrack.itemSavedIndexes[iti];
				siAlt = savedIndexes[siOld].altSavedIndex;
				if (siAlt > LOR.UNDEFINED)
				{
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + siAlt.ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
			}

			// Close the list of items
			lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write out any LoopLevels in this track
			writeLoopLevels(trackIndex);

			lineOut = LEVEL2 + FINTBL + TABLEtrack + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

		}

		private void writeTimingGrids(bool selectedOnly)
		{
			int nextID = 0;
			timingGrid thisGrid;
			
			// TIMING GRIDS
			lineOut = LEVEL1 + STFLD + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			for (int x=0; x<timingGrids.Count; x++)
			{
				for (int curTimingGrid = 0; curTimingGrid < timingGrids.Count; curTimingGrid++)
				{
					thisGrid = timingGrids[curTimingGrid];
					if (thisGrid.altSaveID == LOR.UNDEFINED)
					{
						// we're done, exit both loops
						curTimingGrid = timingGrids.Count;
						x = timingGrids.Count;
					}
					else
					{
						if (thisGrid.altSaveID == nextID)
						{
							// Should be prequalified... should not have a altSaveID unless selected
							lineOut = LEVEL2 + STFLD + TABLEtimingGrid;
							lineOut += SPC + FIELDsaveID + FIELDEQ + thisGrid.saveID.ToString() + ENDQT;
							if (thisGrid.name.Length > 1)
							{
								lineOut += SPC + FIELDname + FIELDEQ + thisGrid.name + ENDQT;
							}
							lineOut += SPC + FIELDtype + FIELDEQ + timingName(thisGrid.type) + ENDQT;
							if (thisGrid.spacing > 1)
							{
								lineOut += SPC + FIELDspacing + FIELDEQ + thisGrid.spacing.ToString() + ENDQT;
							}
							if (thisGrid.type == timingGridType.fixedGrid)
							{
								lineOut += ENDFLD;
								writer.WriteLine(lineOut); lineCount++;
							}
							else if (thisGrid.type == timingGridType.freeform)
							{
								lineOut += FINFLD;
								writer.WriteLine(lineOut); lineCount++;

								for (int tm = 0; tm < thisGrid.timings.Count; tm++)
								{
									lineOut = LEVEL4 + STFLD + TABLEtiming;
									lineOut += SPC + FIELDcentisecond + FIELDEQ + thisGrid.timings[tm].ToString() + ENDQT;
									lineOut += ENDFLD;
									writer.WriteLine(lineOut); lineCount++;

									//curGridItem++;
								}

								lineOut = LEVEL2 + FINTBL + TABLEtimingGrid + FINFLD;
								writer.WriteLine(lineOut); lineCount++;
								nextID++;
								curTimingGrid = timingGrids.Count;
							}

						}
					}
				}
			}
			lineOut = LEVEL1 + FINTBL + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

		} // end writeTimingGrids

		public int WriteSequenceFileInCRGAlphaOrder(string newFileName, bool selectedOnly)
		{
			altHighestSavedIndex = LOR.UNDEFINED;
			int curTimingGrid = 0;
			int curGridItem = 0;
			int curTrack = 0;
			int curTrackItem = 0;
			List<int> newChannelSIs = new List<int>();
			List<int> newRGBchannelSIs = new List<int>();
			List<int> newChannelGroupSIs = new List<int>();
			int altSI = LOR.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[tracks.Count];
			altSavedIndexes = null;
			Array.Resize(ref altSavedIndexes, highestSavedIndex + 2);
			Array.Resize(ref altSaveIDs, timingGrids.Count+1);

			// Clear any 'written' flags from a previous save
			clearWrittenFlags();

			// Write the first line of the new sequence, containing the XML info
			writeSequenceStart(newFileName);

			// Write all channels in Alpha Order
			if (alphaChannelNameIndexesDirty)
			{
				SortChannelNames();
			}
			for (int chai = 0; chai < channels.Count; chai++)
			{
				int chIdx = alphaChannelNameIndexes[chai];
				if ((!selectedOnly) || (channels[chIdx].selected))
				{
					altSI = writeChannel(chIdx);
				}
			}
			
			// Write all RGB Channels in Alpha Order
			if (alphaRGBchannelNameIndexesDirty)
			{
				SortRGBchannelNames();
			}
			for (int rchai = 0; rchai < rgbChannels.Count; rchai++)
			{
				int rchIdx = alphaRGBchannelNameIndexes[rchai];
				if ((!selectedOnly) || (rgbChannels[rchIdx].selected))
				{
					altSI = writergbChannel(rchIdx, selectedOnly, tableType.rgbChannel);
				}
			}

			// Write all ChannelGroups in Alpha Order
			if (alphaChannelGroupNameIndexesDirty)
			{
				SortChannelGroupNames();
			}
			for (int chgai = 0; chgai < channelGroups.Count; chgai++)
			{
				int chgIdx = alphaChannelGroupNameIndexes[chgai];
				if ((!selectedOnly) || (channelGroups[chgIdx].selected))
				{
					altSI = writeChannelGroup(chgIdx, selectedOnly, tableType.channelGroup);
				}
			}

			// All channels should now be written, close this section
			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Timing Grids
			if (alphaTimingGridNameIndexesDirty)
			{
				SortTimingGridNames();
			}
			// Timing Grids do not get written to the file yet
			// But we must renumber the saveIDs
			int altSaveID = 0;
			for (int tg=0; tg< timingGrids.Count; tg++)
			{
				int atg = alphaTimingGridNameIndexes[tg];
				if ((!selectedOnly) || (timingGrids[atg].selected))
				{
					timingGrids[atg].altSaveID = altSaveID;
					altSaveIDs[atg] = altSaveID;
					altSaveID++;
				}
			}
			writeTimingGrids(selectedOnly);


			if (alphaTrackNameIndexesDirty)
			{
				SortTrackNames();
			}
			// loop thru tracks
			lineOut = LEVEL1 + STFLD + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			for (int t = 0; t < tracks.Count; t++)
			{
				int trk = alphaTrackNameIndexes[t];
				if ((!selectedOnly) || (tracks[trk].selected))
				{
					writeTrack(trk, selectedOnly);
				}
			}

			writeSequenceClose();

			errorStatus = renameTempFile(newFileName);

			return errorStatus;
		} // end WriteSequenceFileInCRGAlphaOrder

		private void writeSequenceClose()
		{
			string lineOut = "";

			// Close the tracks setion
			lineOut = LEVEL1 + FINTBL + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write out Animation info, it it exists
			writeAnimation();

			// Close the sequence
			lineOut = FINTBL + TABLEsequence + FINFLD; // "</sequence>";
			writer.WriteLine(lineOut); lineCount++;

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			// We're done writing the file
			writer.Flush();
			writer.Close();
		}

		private void clearWrittenFlags()
		{
			for (int ch = 0; ch < channels.Count; ch++)
			{
				channels[ch].written = false;
				channels[ch].altSavedIndex = LOR.UNDEFINED;
			}
			for (int rch = 0; rch < rgbChannels.Count; rch++)
			{
				rgbChannels[rch].written = false;
				rgbChannels[rch].altSavedIndex = LOR.UNDEFINED;
			}
			for (int chg = 0; chg < channelGroups.Count; chg++)
			{
				channelGroups[chg].written = false;
				channelGroups[chg].altSavedIndex = LOR.UNDEFINED;
			}
			for (int tr = 0; tr < tracks.Count; tr++)
			{
				tracks[tr].written = false;
			}
			for (int tg = 0; tg < timingGrids.Count; tg++)
			{
				timingGrids[tg].written = false;
				timingGrids[tg].altSaveID = LOR.UNDEFINED;
			}
		}

		private void writeLoopLevels(int trackObjIndex)
		{
			if (tracks[trackObjIndex].loopLevels.Count > 0)
			{
				lineOut = LEVEL3 + STFLD + TABLEloopLevels + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
			}

			for (int ll = 0; ll < tracks[trackObjIndex].loopLevels.Count; ll++)
			{
				lineOut = LEVEL4 + STFLD + TABLEloopLevel + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				for (int lp = 0; lp < tracks[trackObjIndex].loopLevels[ll].loopsCount; lp++)
				{
					loop loooop = tracks[trackObjIndex].loopLevels[ll].loops[lp];
					lineOut = LEVEL5 + STFLD + FIELDloop + SPC;
					lineOut += FIELDstartCentisecond + FIELDEQ + loooop.startCentsecond.ToString() + ENDQT + SPC;
					lineOut += FIELDendCentisecond + FIELDEQ + loooop.endCentisecond.ToString() + ENDQT;
					if (loooop.loopCount > 0)
					{
						lineOut += SPC + FIELDloopCount + FIELDEQ + loooop.loopCount.ToString() + ENDQT;
					}
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				lineOut = LEVEL4 + FINTBL + TABLEloopLevel + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
			}

			if (tracks[trackObjIndex].loopLevels.Count > 0)
			{
				lineOut = LEVEL3 + FINTBL + TABLEloopLevels + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
			} // end if loops

		}

		public channel FindChannel(int savedIndex)
		{
			channel ret = null;

			if (savedIndex <= highestSavedIndex)
			{
				if (savedIndexes[savedIndex].objType == tableType.channel)
				{
					ret = channels[savedIndexes[savedIndex].objIndex];
				}
			}
			return ret;
		} // end FindChannel

		public channel FindChannel(string channelName)
		{
			channel ret = null;
			if (alphaChannelNameIndexesDirty)
			{
				for (int ch = 0; ch < channels.Count; ch++)
				{
					if (channels[ch].name.CompareTo(channelName) == 0)
					{
						ret = channels[ch];
						ch = channels.Count; // force break
					}
				}
			}
			else
			{
				int idx = BTreeFindName(alphaChannelNames, channelName);
				ret = channels[alphaChannelNameIndexes[idx]];
			}
			return ret;
		}

		public int FindChannelIndex(string channelName)
		{
			int ret = LOR.UNDEFINED;
			if (alphaChannelNameIndexesDirty)
			{
				for (int ch = 0; ch < channels.Count; ch++)
				{
					if (channels[ch].name.CompareTo(channelName) == 0)
					{
						ret = ch;
						ch = channels.Count; // force break
					}
				}
			}
			else
			{
				int idx = BTreeFindName(alphaChannelNames, channelName);
				ret = idx;
			}
			return ret;

		}

		public rgbChannel FindrgbChannel(int savedIndex)
		{
			rgbChannel ret = null;

			if (savedIndex <= highestSavedIndex)
			{
				if (savedIndexes[savedIndex].objType == tableType.rgbChannel)
				{
					ret = rgbChannels[savedIndexes[savedIndex].objIndex];
				}
			}
			return ret;
		} // end FindrgbChannel

		public rgbChannel FindrgbChannel(string rgbChannelName)
		{
			rgbChannel ret = null;
			for (int rch = 0; rch < rgbChannels.Count; rch++)
			{
				if (rgbChannels[rch].name.CompareTo(rgbChannelName) == 0)
				{
					ret = rgbChannels[rch];
					rch = rgbChannels.Count; // force break
				}
			}
			return ret;
		}

		public channelGroup FindChannelGroup(int savedIndex)
		{
			channelGroup ret = null;

			if (savedIndex <= highestSavedIndex)
			{
				if (savedIndexes[savedIndex].objType == tableType.channelGroup)
				{
					ret = channelGroups[savedIndexes[savedIndex].objIndex];
				}
			}
			return ret;
		} // end FindChannelGroup

		public channelGroup FindChannelGroup(string channelGroupName)
		{
			channelGroup ret = null;
			for (int chg = 0; chg < channelGroups.Count; chg++)
			{
				if (channelGroups[chg].name.CompareTo(channelGroupName) == 0)
				{
					ret = channelGroups[chg];
					chg = channelGroups.Count; // force break
				}
			}
			return ret;
		}

		public track FindTrack(string trackName)
		{
			track ret = null;
			for (int tr = 0; tr < tracks.Count; tr++)
			{
				if (tracks[tr].name.CompareTo(trackName) == 0)
				{
					ret = tracks[tr];
					tr = tracks.Count; // force break
				}
			}
			return ret;
		}

		public int FindTrackIndex(string trackName)
		{
			int ret = LOR.UNDEFINED;
			for (int tr = 0; tr < tracks.Count; tr++)
			{
				if (tracks[tr].name.CompareTo(trackName) == 0)
				{
					ret = tr;
					tr = channels.Count; // force break
				}
			}
			return ret;
		}

		public timingGrid FindTimingGrid(string timingGridName)
		{
			timingGrid ret = null;
			for (int tg = 0; tg < channels.Count; tg++)
			{
				if (timingGrids[tg].name.CompareTo(timingGridName) == 0)
				{
					ret = timingGrids[tg];
					tg = timingGrids.Count; // force break
				}
			}
			return ret;
		}

		private List<int> writeTrackItems(int trackIndex, bool selectedOnly, tableType itemTypes)
		{
			int altSaveIndex = LOR.UNDEFINED;
			List<int> altSIs = new List<int>();
			track tr = tracks[trackIndex];
			string itsName = "";  //! for debugging

			for (int iti = 0; iti
				< tracks[trackIndex].itemSavedIndexes.Count; iti++)
			{
				int si = tracks[trackIndex].itemSavedIndexes[iti];
				if (savedIndexes[si].objType == tableType.channel)
				{
					itsName = channels[savedIndexes[si].objIndex].name;
					if ((!selectedOnly) || (channels[savedIndexes[si].objIndex].selected))
					{
						// Prevents unnecessary processing of channels which have already been written, during RGB channel and group processing
						if ((itemTypes == tableType.None) || (itemTypes == tableType.channel))
						// Type NONE actually means ALL in this case
						{
							altSaveIndex = writeChannel(savedIndexes[si].objIndex);
							altSIs.Add(altSaveIndex);
						}
					}
				}
				else
				{
					if (savedIndexes[si].objType == tableType.rgbChannel)
					{
						itsName = rgbChannels[savedIndexes[si].objIndex].name;
						if ((!selectedOnly) || (rgbChannels[savedIndexes[si].objIndex].selected))
						{
							// prevents unnecessary processing of RGB channels that have already been written, during groupd processing
							if ((itemTypes == tableType.None) || (itemTypes == tableType.rgbChannel) || (itemTypes == tableType.channel))
							// Type NONE actually means ALL in this case
							{
								altSaveIndex = writergbChannel(savedIndexes[si].objIndex, selectedOnly, itemTypes);
								altSIs.Add(altSaveIndex);
							}
						}
					}
					else
					{
						if (savedIndexes[si].objType == tableType.channelGroup)
						{
							itsName = channelGroups[savedIndexes[si].objIndex].name;
							if ((!selectedOnly) || (channelGroups[savedIndexes[si].objIndex].selected))
							{
								//if (itemTypes == tableType.channelGroup)
								//if ((itemTypes == tableType.None) ||
								//    (itemTypes == tableType.rgbChannel) ||
								//    (itemTypes == tableType.channel) ||
								//    (itemTypes == tableType.channelGroup))
								// Type NONE actually means ALL in this case
								//{
								altSaveIndex = writeChannelGroup(savedIndexes[si].objIndex, selectedOnly, itemTypes);
									altSIs.Add(altSaveIndex);
								//}
							}
						} // if channelgroup, or not
					} // if rgb channel, or not
				} // if regular channel, or not
			} // loop thru items

			return altSIs;
		}

		private int writeChannel(int channelIndex)
		{
			channel thisChannel = channels[channelIndex];

			if (!thisChannel.written)
			{
				lineOut = LEVEL2 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDname + FIELDEQ + thisChannel.name + ENDQT;
				lineOut += SPC + FIELDcolor + FIELDEQ + thisChannel.color.ToString() + ENDQT;
				lineOut += SPC + FIELDcentiseconds + FIELDEQ + thisChannel.centiseconds.ToString() + ENDQT;
				if (thisChannel.output.deviceType == deviceType.LOR)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(thisChannel.output.deviceType) + ENDQT;
					lineOut += SPC + FIELDunit + FIELDEQ + thisChannel.output.unit.ToString() + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + thisChannel.output.circuit.ToString() + ENDQT;
				}
				else if (thisChannel.output.deviceType == deviceType.DMX)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(thisChannel.output.deviceType) + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + thisChannel.output.circuit.ToString() + ENDQT;
					lineOut += SPC + FIELDnetwork + FIELDEQ + thisChannel.output.network.ToString() + ENDQT;
				}
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + altHighestSavedIndex.ToString() + ENDQT;

				//curSavedIndex++;
				altHighestSavedIndex++;
				thisChannel.altSavedIndex = altHighestSavedIndex;
				// copy the original savedIndex to the new altSavedIndexes at the altHighestSavedIndex position
				altSavedIndexes[altHighestSavedIndex] = savedIndexes[thisChannel.savedIndex].altCopy();
				// cross reference the new altSavedIndex to the original savedIndex
				altSavedIndexes[altHighestSavedIndex].altSavedIndex = thisChannel.savedIndex;
				// and cross reference the original saved index to the new altSavedIndex
				savedIndexes[thisChannel.savedIndex].altSavedIndex = altHighestSavedIndex;


				// Are there any effects for this channel?
				if (thisChannel.effects.Count > 0)
				{
					// complete channel line with regular '>' then do effects
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					writeEffects(thisChannel);

					lineOut = LEVEL2 + FINTBL + TABLEchannel + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				else // NO effects for this channal
				{
					// complete channel line with field end '/>'
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				thisChannel.written = true;
			}
			return thisChannel.altSavedIndex;
		}

		private int writergbChannel(int rgbChannelIndex, bool selectedOnly, tableType itemTypes)
		{
			rgbChannel thisRGB = rgbChannels[rgbChannelIndex];
			int redSavedIndex = LOR.UNDEFINED;
			int grnSavedIndex = LOR.UNDEFINED;
			int bluSavedIndex = LOR.UNDEFINED;

			int altSavedIndex = LOR.UNDEFINED;
			if (!thisRGB.written)
			{
				if ((selectedOnly) || (thisRGB.selected))
				{
					if ((itemTypes == tableType.None) || (itemTypes == tableType.channel))
					// Type NONE actually means ALL in this case
					{
						redSavedIndex = writeChannel(thisRGB.redChannelObjIndex);
						grnSavedIndex = writeChannel(thisRGB.grnChannelObjIndex);
						bluSavedIndex = writeChannel(thisRGB.bluChannelObjIndex);
					}

					if ((itemTypes == tableType.None) || (itemTypes == tableType.rgbChannel))
					{
						lineOut = LEVEL2 + STFLD + TABLErgbChannel;
						lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + thisRGB.totalCentiseconds.ToString() + ENDQT;
						lineOut += SPC + FIELDname + FIELDEQ + thisRGB.name + ENDQT;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + altHighestSavedIndex.ToString() + ENDQT;
						lineOut += FINFLD;
						writer.WriteLine(lineOut); lineCount++;

						//curSavedIndex++;
						altHighestSavedIndex++;
						thisRGB.altSavedIndex = altHighestSavedIndex;
						// copy the original savedIndex to the altSavedIndexes at the altHighestSavedIndex position
						altSavedIndexes[altHighestSavedIndex] = savedIndexes[thisRGB.savedIndex].altCopy();
						// cross reference the new altSavedIndex to the original savedIndex
						altSavedIndexes[altHighestSavedIndex].altSavedIndex = thisRGB.savedIndex;
						// and cross reference the original saved index to the new altSavedIndex
						savedIndexes[thisRGB.savedIndex].altSavedIndex = altHighestSavedIndex;

						// Start SubChannels
						lineOut = LEVEL3 + STFLD + TABLEchannel + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;

						// RED subchannel
						altSavedIndex = savedIndexes[thisRGB.redSavedIndex].altSavedIndex;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + altSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;

						// GREEN subchannel
						altSavedIndex = savedIndexes[thisRGB.grnSavedIndex].altSavedIndex;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + altSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;

						// BLUE subchannel
						altSavedIndex = savedIndexes[thisRGB.bluSavedIndex].altSavedIndex;
						lineOut = LEVEL4 + STFLD + TABLEchannel;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + altSavedIndex.ToString() + ENDQT;
						lineOut += ENDFLD;

						// End SubChannels
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL2 + FINTBL + TABLErgbChannel + FINFLD;
						writer.WriteLine(lineOut); lineCount++;

						thisRGB.written = true;
					}
				}
			}
			return thisRGB.altSavedIndex;
		} // end writergbChannel

		private int writeChannelGroup(int channelGroupIndex, bool selectedOnly, tableType itemTypes)
		{
			int[] altSIs = null;
			int itemNo = 0;
			channelGroup thisGroup = channelGroups[channelGroupIndex];
			bool keepGoing = true;

			if (!thisGroup.written)
			{
				if ((!selectedOnly) || (thisGroup.selected))
				{
					if (itemTypes != tableType.channelGroup)
					{
						writeChannelGroupItems(channelGroupIndex, selectedOnly, itemTypes);
					}

					if ((itemTypes == tableType.None) || (itemTypes == tableType.channelGroup))
					{
						lineOut = LEVEL2 + STFLD + TABLEchannelGroupList;
						lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + thisGroup.totalCentiseconds.ToString() + ENDQT;
						lineOut += SPC + FIELDname + FIELDEQ + thisGroup.name + ENDQT;
						lineOut += SPC + FIELDsavedIndex + FIELDEQ + altHighestSavedIndex.ToString() + ENDQT;
						lineOut += FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL3 + STFLD + TABLEchannelGroup + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;

						//curSavedIndex++;
						altHighestSavedIndex++;
						thisGroup.altSavedIndex = altHighestSavedIndex;
						// copy the original savedIndex to the altSavedIndexes at the altHighestSavedIndex position
						altSavedIndexes[altHighestSavedIndex] = savedIndexes[thisGroup.savedIndex].altCopy();
						// cross reference the new altSavedIndex to the original savedIndex
						altSavedIndexes[altHighestSavedIndex].altSavedIndex = thisGroup.savedIndex;
						// and cross reference the original saved index to the new altSavedIndex
						savedIndexes[thisGroup.savedIndex].altSavedIndex = altHighestSavedIndex;

						for (int igi = 0; igi < thisGroup.itemSavedIndexes.Count; igi++)
						{
							lineOut = LEVEL4 + STFLD + TABLEchannelGroup;
							int osi = thisGroup.itemSavedIndexes[igi];
							int asi = savedIndexes[osi].altSavedIndex;
							if (asi > LOR.UNDEFINED)
							{
								lineOut += SPC + FIELDsavedIndex + FIELDEQ + asi.ToString() + ENDQT;
								lineOut += ENDFLD;
								writer.WriteLine(lineOut); lineCount++;
							}
						}

						lineOut = LEVEL3 + FINTBL + TABLEchannelGroup + PLURAL + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						lineOut = LEVEL2 + FINTBL + TABLEchannelGroupList + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
					}
				}
			}
			return thisGroup.altSavedIndex;
		} // end writeChannelGroup

		private List<int> writeChannelGroupItems(int channelGroupIndex, bool selectedOnly, tableType itemTypes)
		{
			List<int> altSIs = new List<int>();
			int itemCount = 0;
			int si = LOR.UNDEFINED;
			int altSaveIndex = LOR.UNDEFINED;
			channelGroup thisGroup = channelGroups[channelGroupIndex];
			string itsName = "";  //! For debugging

			if ((selectedOnly) || (thisGroup.selected))
			{
				for (int igi = 0; igi < thisGroup.itemSavedIndexes.Count; igi++)
				{
					si = thisGroup.itemSavedIndexes[igi];
					if (savedIndexes[si].objType == tableType.channel)
					{
						itsName = channels[savedIndexes[si].objIndex].name;
						if ((!selectedOnly) || (channels[savedIndexes[si].objIndex].selected))
						{
							// Prevents unnecessary processing of channels which have already been written, during RGB channel and group processing
							if ((itemTypes == tableType.None) || (itemTypes == tableType.channel))
							// Type NONE actually means ALL in this case
							{
								altSaveIndex = writeChannel(savedIndexes[si].objIndex);
								altSIs.Add(altSaveIndex);
							}
						}
					}
					else
					{
						if (savedIndexes[si].objType == tableType.rgbChannel)
						{
							itsName = rgbChannels[savedIndexes[si].objIndex].name;
							if ((!selectedOnly) || (rgbChannels[savedIndexes[si].objIndex].selected))
							{
								// prevents unnecessary processing of RGB channels that have already been written, during groupd processing
								if ((itemTypes == tableType.None) || (itemTypes == tableType.rgbChannel) || (itemTypes == tableType.channel))
								// Type NONE actually means ALL in this case
								{
									altSaveIndex = writergbChannel(savedIndexes[si].objIndex, selectedOnly, itemTypes);
									altSIs.Add(altSaveIndex);
								}
							}
						}
						else
						{
							if (savedIndexes[si].objType == tableType.channelGroup)
							{
								itsName = channelGroups[savedIndexes[si].objIndex].name;
								if ((!selectedOnly) || (channelGroups[savedIndexes[si].objIndex].selected))
								{
									//if (itemTypes == tableType.channelGroup)
									//if ((itemTypes == tableType.None) ||
									//    (itemTypes == tableType.rgbChannel) ||
									//    (itemTypes == tableType.channel) ||
									//    (itemTypes == tableType.channelGroup))
									// Type NONE actually means ALL in this case
									//{
									altSaveIndex = writeChannelGroup(savedIndexes[si].objIndex, selectedOnly, itemTypes);
									altSIs.Add(altSaveIndex);
									//}
								}
							} // if channelgroup, or not
						} // if rgb channel, or not
					} // if regular channel, or not
				}
			}
			return altSIs;
		} // end writeChannelGroupItems

		private void writeEffects(channel thisChannel)
		{
			for (int effectIndex = 0; effectIndex < thisChannel.effects.Count; effectIndex++)
			{
				lineOut = LEVEL3 + STFLD + TABLEeffect;
				lineOut += SPC + FIELDtype + FIELDEQ + effectName(thisChannel.effects[effectIndex].type) + ENDQT;
				lineOut += SPC + FIELDstartCentisecond + FIELDEQ + thisChannel.effects[effectIndex].startCentisecond.ToString() + ENDQT;
				lineOut += SPC + FIELDendCentisecond + FIELDEQ + thisChannel.effects[effectIndex].endCentisecond.ToString() + ENDQT;
				if (thisChannel.effects[effectIndex].intensity > LOR.UNDEFINED)
				{
					lineOut += SPC + FIELDintensity + FIELDEQ + thisChannel.effects[effectIndex].intensity.ToString() + ENDQT;
				}
				if (thisChannel.effects[effectIndex].startIntensity > LOR.UNDEFINED)
				{
					lineOut += SPC + FIELDstartIntensity + FIELDEQ + thisChannel.effects[effectIndex].startIntensity.ToString() + ENDQT;
					lineOut += SPC + FIELDendIntensity + FIELDEQ + thisChannel.effects[effectIndex].endIntensity.ToString() + ENDQT;
				}
				lineOut += ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
			}
		}

		public int SavedIndexIntegrityCheck()
			// Returns 0 if no problems found
			// number > 0 = number of problems
		{
			int errs = 0;

			int tot = channels.Count + rgbChannels.Count + channelGroups.Count;
			savedIndex[] siCheck = null;
			Array.Resize(ref siCheck, tot);
			for (int t = 0; t < tot; t++)
			{
				siCheck[t].objIndex = LOR.UNDEFINED;
				siCheck[t].objType = tableType.None;
			}

			for (int ch=0; ch< channels.Count; ch++)
			{
				if (channels[ch].savedIndex < tot)
				{
					if (siCheck[channels[ch].savedIndex].objIndex == LOR.UNDEFINED)
					{
						siCheck[channels[ch].savedIndex].objIndex = ch;
					}
					else
					{
						errs++;
					}
					if (siCheck[channels[ch].savedIndex].objType == tableType.None)
					{
						siCheck[channels[ch].savedIndex].objType = tableType.channel;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end channels loop

			for (int rch = 0; rch < rgbChannels.Count; rch++)
			{
				if (rgbChannels[rch].savedIndex < tot)
				{
					if (siCheck[rgbChannels[rch].savedIndex].objIndex == LOR.UNDEFINED)
					{
						siCheck[rgbChannels[rch].savedIndex].objIndex = rch;
					}
					else
					{
						errs++;
					}
					if (siCheck[rgbChannels[rch].savedIndex].objType == tableType.None)
					{
						siCheck[rgbChannels[rch].savedIndex].objType = tableType.rgbChannel;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end rgbChannels loop

			for (int chg = 0; chg < channelGroups.Count; chg++)
			{
				if (channelGroups[chg].savedIndex < tot)
				{
					if (siCheck[channelGroups[chg].savedIndex].objIndex == LOR.UNDEFINED)
					{
						siCheck[channelGroups[chg].savedIndex].objIndex = chg;
					}
					else
					{
						errs++;
					}
					if (siCheck[channelGroups[chg].savedIndex].objType == tableType.None)
					{
						siCheck[channelGroups[chg].savedIndex].objType = tableType.channelGroup;
					}
					else
					{
						errs++;
					}
				}
				else
				{
					errs++;
				}
			} // end channels loop

			if (tot != highestSavedIndex+1)
			{
				errs++;
			}

			for (int s=0; s< tot; s++)
			{
				if (siCheck[s].objIndex != savedIndexes[s].objIndex)
				{
					errs++;
				}
				if (siCheck[s].objType != savedIndexes[s].objType)
				{
					errs++;
				}
			} // end comparison loop

			return errs;
		}

		public int[] OutputConflicts()
		{
			int[] ret = null;  // holds pairs of matches with identical outputs
			string[] outputs = null;  // holds outputs of each channel in string format (that are not controllerType None)
			int[] indexes = null; // holds SavedIndex of channels
			int outputCount = 0; // how many channels (so far) are not controllerType None
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
			//                                                                   So ret[even#] matches ret[odd#]
			// Loop thru all regular channels
			for (int ch=0; ch< channels.Count; ch++)
			{
				// if deviceType != None
				if (channels[ch].output.deviceType != deviceType.None)
				{
					// store output info in string format
					Array.Resize(ref outputs, outputCount + 1);
					outputs[outputCount] = channels[ch].output.ToString();
					// store the savedIndex of the channel
					Array.Resize(ref indexes, outputCount + 1);
					indexes[outputCount] = channels[ch].savedIndex;
					// incremnt count
					outputCount++;
				}
			} // end channel loop
			// if at least 2 channels deviceType != None
			if (outputCount > 1)
			{
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(outputs, indexes);
				// loop thru sorted arrays
				for (int q = 1; q< outputCount; q++)
				{
					// compare output info
					if (outputs[q-1].CompareTo(outputs[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
			// return any matches found
			return ret;
		} // end output conflicts

		public int[] DuplicateChannelNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds name of each channel
			int[] indexes = null; // holds SavedIndex of channels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (channels.Count > 1)
			{
				Array.Resize(ref names, channels.Count);
											 // Loop thru all regular channels
				for (int ch = 0; ch < channels.Count; ch++)
				{
					names[ch] = channels[ch].name;
					indexes[ch] = channels[ch].savedIndex;
				} // end channel loop
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channels.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end DuplicateChannelNames function

		public int[] DuplicateChannelGroupNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds name of each channelGroup
			int[] indexes = null; // holds SavedIndex of channelGroups
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (channelGroups.Count > 1)
			{
				Array.Resize(ref names, channelGroups.Count);
				// Loop thru all regular channels
				for (int chg = 0; chg < channelGroups.Count; chg++)
				{
					names[chg] = channelGroups[chg].name;
					indexes[chg] = channelGroups[chg].savedIndex;
				} // end channelGroup loop
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channelGroups.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end Duplicate Channel Group Names

		public int[] DuplicatergbChannelNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds name of each rgbChannel
			int[] indexes = null; // holds SavedIndex of rgbChannels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (rgbChannels.Count > 1)
			{
				Array.Resize(ref names, rgbChannels.Count);
				// Loop thru all regular channels
				for (int rch = 0; rch < rgbChannels.Count; rch++)
				{
					names[rch] = rgbChannels[rch].name;
					indexes[rch] = rgbChannels[rch].savedIndex;
				} // end channel loop
					// if at least 2 channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < rgbChannels.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end Duplicate RGB Channel Names

		public int[] DuplicateTrackNames()
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds name of each track
			int[] indexes = null; // holds ObjectIndex of track
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			if (tracks.Count > 1)
			{
				Array.Resize(ref names, tracks.Count);
				// Loop thru all regular channels
				for (int tr = 0; tr < tracks.Count; tr++)
				{
					names[tr] = tracks[tr].name;
					indexes[tr] = tr;
				} // end channel loop
					// if at least 2 channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < tracks.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if outputCount > 1
				// return any matches found
			return ret;
		} // end DuplicateTrackNames function

		public int[] DuplicateNames()
			// Looks for duplicate names amongst regular channels, RGB channels, channel groups, and tracks
			// Does not include timing grid names
		{
			int[] ret = null;  // holds pairs of matches with identical names
			string[] names = null;  // holds name of each channel
			int nameCount = 0;
			int[] indexes = null; // holds SavedIndex of channels
			int matches = 0; // how many matches (X2) have been found (so far)  NOTE: matches are returned in pairs
											 //                                                                   So ret[even#] matches ret[odd#]

			nameCount = channels.Count + rgbChannels.Count + channelGroups.Count + tracks.Count;
			if (nameCount > 1)
			{ 
				Array.Resize(ref names, nameCount);
				// Loop thru all regular channels
				for (int ch = 0; ch < channels.Count; ch++)
				{
					names[ch] = channels[ch].name;
					indexes[ch] = channels[ch].savedIndex;
				} // end channel loop

				for (int rch = 0; rch < rgbChannels.Count; rch++)
				{
					names[rch + channels.Count] = rgbChannels[rch].name;
					indexes[rch + channels.Count] = rgbChannels[rch].savedIndex;
				} // end RGB channel loop

				for (int chg=0; chg< channelGroups.Count; chg++)
				{
					names[chg + channels.Count + rgbChannels.Count] = channelGroups[chg].name;
					indexes[chg + channels.Count + rgbChannels.Count] = channelGroups[chg].savedIndex;
				} // end Channel Group Loop

				int trIdx;
				for (int tr=0; tr< tracks.Count; tr++)
				{
					names[tr + channels.Count + rgbChannels.Count + channelGroups.Count] = tracks[tr].name;
					// use negative numbers for track indexes
					trIdx = LOR.UNDEFINED + (-tr);
					indexes[tr + channels.Count + rgbChannels.Count + channelGroups.Count] = trIdx;
				}

				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channels.Count; q++)
				{
					// compare output info
					if (names[q - 1].CompareTo(names[q]) == 0)
					{
						// if match, store savedIndexes in pair of return values
						Array.Resize(ref ret, matches + 2);
						ret[matches] = indexes[q - 1];
						ret[matches + 1] = indexes[q];
						// increment return value count by one pair
						matches += 2;
					}
				} // end output loop
			} // end if nameCount > 1
				// return any matches found
			return ret;
		} // end DuplicateNames function

		public static Int32 getKeyValue(string lineIn, string keyWord)
		{
			Int32 valueOut = LOR.UNDEFINED;
			int pos1 = LOR.UNDEFINED;
			int pos2 = LOR.UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = Convert.ToInt32(fooo);
			}
			else
			{
				valueOut = LOR.UNDEFINED;
			}

			return valueOut;
		}

		public static string getKeyWord(string lineIn, string keyWord)
		{
			string valueOut = "";
			int pos1 = LOR.UNDEFINED;
			int pos2 = LOR.UNDEFINED;
			string fooo = "";

			pos1 = lineIn.IndexOf(keyWord + "=");
			if (pos1 > 0)
			{
				fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
				pos2 = fooo.IndexOf("\"");
				fooo = fooo.Substring(0, pos2);
				valueOut = fooo;
			}
			else
			{
				valueOut = "";
			}

			return valueOut;
		}

		public static deviceType enumDevice(string deviceName)
		{
			deviceType valueOut = deviceType.None;

			if (deviceName == DEVICElor)
			{
				valueOut = deviceType.LOR;
			}
			else if (deviceName == DEVICEdmx)
			{
				valueOut = deviceType.DMX;
			}
			else if (deviceName == DEVICEdigital)
			{
				valueOut = deviceType.Digital;
			}
			else if (deviceName == "")
			{
				valueOut = deviceType.None;
			}
			else
			{
				// TODO: throw exception here!
				valueOut = deviceType.None;
				string sMsg = "Unrecognized Device Type: ";
				sMsg += deviceName;
				DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static effectType enumEffect(string effectName)
		{
			effectType valueOut = effectType.None;

			if (effectName == EFFECTintensity)
			{
				valueOut = effectType.intensity;
			}
			else if (effectName == EFFECTshimmer)
			{
				valueOut = effectType.shimmer;
			}
			else if (effectName == EFFECTtwinkle)
			{
				valueOut = effectType.twinkle;
			}
			else if (effectName == EFFECTdmx)
			{
				valueOut = effectType.DMX;
			}
			else
			{
				// TODO: throw exception here
				valueOut = effectType.None;
				string sMsg = "Unrecognized Effect Name: ";
				sMsg += effectName;
				DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static timingGridType enumGridType(string typeName)
		{
			timingGridType valueOut = timingGridType.None;

			if (typeName == GRIDfreeform)
			{
				valueOut = timingGridType.freeform;
			}
			else if (typeName == "fixed")
			{
				valueOut = timingGridType.fixedGrid;
			}
			else
			{
				// TODO: throw exception here
				valueOut = timingGridType.None;
				string sMsg = "Unrecognized Timing Grid Type: ";
				sMsg += typeName;
				DialogResult dr = MessageBox.Show(sMsg, "Unrecognized Keyword", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return valueOut;
		}

		public static string deviceName(deviceType devType)
		{
			string valueOut = "";
			switch (devType)
			{
				case deviceType.LOR:
					valueOut = DEVICElor;
					break;

				case deviceType.DMX:
					valueOut = DEVICEdmx;
					break;

				case deviceType.Digital:
					valueOut = DEVICEdigital;
					break;

				//TODO: Other device types, such as cosmic color ribbon and ...
			}
			return valueOut;
		}

		public static string effectName(effectType effType)
		{
			string valueOut = "";
			switch (effType)
			{
				case effectType.intensity:
					valueOut = EFFECTintensity;
					break;

				case effectType.shimmer:
					valueOut = EFFECTshimmer;
					break;

				case effectType.twinkle:
					valueOut = EFFECTtwinkle;
					break;

				case effectType.DMX:
					valueOut = EFFECTdmx;
					break;
			}
			return valueOut;
		}

		public static string timingName(timingGridType grdType)
		{
			string valueOut = "";
			switch (grdType)
			{
				case timingGridType.freeform:
					valueOut = GRIDfreeform;
					break;

				case timingGridType.fixedGrid:
					valueOut = GRIDfixed;
					break;
			}
			return valueOut;
		}

		public static string cleanName(string originalName)
		{
			string ret = originalName;
			ret = ret.Replace("&quot","\"");
			return ret;
		}

		private int BTreeFindName(string[] names, string name)
		{
			// looks for an EXACT match (see also: FuzzyFindName)
			// names[] must be sorted!

			//TODO Test this THOROUGHLY!

			int ret = LOR.UNDEFINED;
			int bot = 0;
			int top = tracks.Count - 1;
			int mid = ((top - bot) / 2) + bot;

			while (top > bot)
			{
				mid = ((top - bot) / 2) + bot;

				if (names[mid].CompareTo(name) < 0)
				{
					mid = top;
				}
				else
				{
					if (names[mid].CompareTo(name) > 0)
					{
						mid = bot;
					}
					else
					{
						return mid;
					}
				}
			}
			return ret;
		} // end bTreeFindName

	} // end sequence class
} // end namespace LORUtils