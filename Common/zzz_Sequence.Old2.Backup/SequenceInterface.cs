using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace LORUtils
{
	interface LORsequence
	{ 
		public class LOR
		{
			public const int UNDEFINED = -1;
		}

		//internal enum deviceType
		public enum deviceType
		{ None, LOR, DMX, Digital };

		public class music
		{
			public string Artist;
			public string Title;
			public string Album;
			public string File;
			public music();
			public music(string theTitle);
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
			public channel();
			public channel(string theName);
			public channel(string theName, int theSavedIndex);
			private string channelName = "";
			public string name;
			public Int32 color;
			public int centiseconds;
			public output output;
			public int savedIndex;
			public int altSavedIndex;
			public bool written;
			public bool selected;
			public int mapsTo;
			public rgbChild rgbChild;
			public List<effect> effects;
			public event ChangedEventHandler Changed;
			public channel Copy();
		} // end channel

		public class output
		{
			public deviceType deviceType = deviceType.None;
			public int unit;
			public int circuit;
			public int network;
			public override string ToString();
			public void Parse(string data);
			public override bool Equals(Object obj);
		}

		//internal class savedIndex
		public class savedIndex
		{
			public tableType objType;
			public int objIndex;
			public int altSavedIndex;
			public List<int> parents;
			public savedIndex altCopy();
		}

		//internal class rgbChannel
		public class rgbChannel
		{
			public string name;
			public int savedIndex;
			public int totalCentiseconds;
			public int altSavedIndex;
			public int redChannelObjIndex;  // index/position in the channels[] array
			public int grnChannelObjIndex;
			public int bluChannelObjIndex;
			public int redSavedIndex;
			public int grnSavedIndex;
			public int bluSavedIndex;
			public bool written;
			public bool selected;
			public int mapsTo;
			public rgbChannel();
			public rgbChannel(string theName);
			public rgbChannel(string theName, int theSavedIndex);
		}

		//internal class channelGroup : Sequence
		public class channelGroup
		{
			// Channel Groups can contain regular channels, RGB Channels, and other groups.
			// Groups can be nested many levels deep (limit?).
			// channels and other groups may be in more than one group.
			// Don't create circular references of groups in each other.
			public int channelIndex;
			public int channelSavedIndex;
			public string name;
			public int totalCentiseconds;
			public int savedIndex;
			public int altSavedIndex;
			public List<int> itemSavedIndexes;
			public bool written;
			public bool selected;
			public int mapsTo;
			public channelGroup();
			public channelGroup(string theName);
			public channelGroup(string theName, int theSavedIndex);
			public void AddItem(int itemSavedIndex);
			//TODO: add RemoveItem procedure
		}

		public class effect
		{
			public effectType type;
			public int startCentisecond;
			public int endCentisecond;
			public int intensity;
			public int startIntensity;
			public int endIntensity;
			public effect();
			public effect(effectType theType);
			public effect Copy();
		}

		public class timingGrid
		{
			// Tracks must have a timing grid assigned.
			// All sequences must contain at least one track, ergo all sequences neet at least one timing grid.
			public string name;
			// SaveID is like savedIndex for channels, but separate.
			// Same rules apply, see savedIndex above
			public int saveID;
			public int altSaveID;
			public timingGridType type;
			public int spacing;
			public List<int> timings;
			public bool selected;
			public bool written;
			public timingGrid();
			public timingGrid(string theName);
			public timingGrid(string theName, int theSaveID);
			public timingGrid(string theName, timingGridType theType);
			public void AddTiming(int time);
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
			public string name;
			public int totalCentiseconds;
			public int timingGridObjIndex;
			public int timingGridSaveID;
			public List<int> itemSavedIndexes;
			public List<loopLevel> loopLevels;
			public bool selected;
			public bool written;
			public int mapsTo;
				public track();
			public track(string theName);
			public int AddItem(int savedIndex);
			//TODO: add RemoveItem procedure
		} // end class track

		public class animation
		{
			public int rows;
			public int columns;
			public string image;
			public List<animationRow> animationRows;
		} // end animation class

		public class animationRow
		{
			public int rowIndex;
			public List<animationColumn> animationColumns;
				public int AddAnimationColumn(animationColumn animationColumn);
		}

		public class animationColumn
		{
			public int columnIndex;
			public int channel;
		}

		public class loop
		{
			public int startCentsecond;
			public int endCentisecond;
			public int loopCount;
		}

		public class loopLevel
		{
			public List<loop> loops;
			public int loopsCount;
		} // end loopLevel class

		public class info
		{
			public string filename;
			public string xmlInfo;
			public string author;
			public string createdAt;
			public music music;
			public int videoUsage;
			public string animationInfo;
			public sequenceType sequenceType;
			public int saveFileVersion;
			public string sequenceInfo;
			public string fileCreatedAt;
			public DateTime fileCreatedDateTime;
			public string fileModiedAt;
			public DateTime fileModiedDateTime;
			public string fileSizeFormated;
	;		public long fileSize;
		}

		public class Sequence
		{
			public sequenceType sequenceType;
			public List<channel> channels;
			public List<rgbChannel> rgbChannels;
			public List<channelGroup> channelGroups;
			public savedIndex[] savedIndexes;
			public List<timingGrid> timingGrids;
			public List<track> tracks;
			public animation animation;
			public int lineCount;
			public int highestSavedIndex;
			public int highestSaveIDD;
			public int totalCentiseconds;
			public string filename;
			public int errorStatus;
			public info info;
			public int videoUsage;
			public string[] alphaTimingGridNames;
			public string[] alphaTrackNames;
			public string[] alphaChannelGroupNames;
			public string[] alphaRGBchannelNames;
			public string[] alphaChannelNames;
			public int[] alphaTrackNameIndexes;
			public int[] alphaChannelGroupNameIndexes;
			public int[] alphaRGBchannelNameIndexes;
			public int[] alphaChannelNameIndexes;
			public int[] alphaTimingGridNameIndexes;
			public bool alphaTrackNameIndexesDirty;
			public bool alphaChannelGroupNameIndexesDirty;
			public bool alphaRGBchannelNameIndexesDirty;
			public bool alphaChannelNameIndexesDirty;
			public bool alphaTimingGridNameIndexesDirty;
			public void Sequence();
			public void sequence(string theFilename);
			public void SortChannelNames();
			public void SortRGBchannelNames();
			public void SortChannelGroupNames();
			public void SortTrackNames();
			public void SortTimingGridNames();
			public void SortAllNames();
			public string sequenceInfo;
			public static string DefaultSequencesPath;
			public int AddChannel(channel newChan);
			public int AddRGBChannel(rgbChannel newChan);
			public int AddChannelGroup(channelGroup newGroup);
			//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures
			public int AddTrack(track newTrack);
			public int AddTimingGrid(timingGrid newGrid);
			public int AddSavedIndex(savedIndex si, int position);
			public string getItemName(int savedIndex);
			public void Clear(bool areYouReallySureYouWantToDoThis);
			public int ReadSequenceFile(string existingFileName);
			public string summary();
			public int[] DuplicateNameCheck();
			public int ReadClipboardFile(string existingFilename);
			public int WriteSequenceFile(string newFileName);
			public int WriteSequenceFile(string newFileName, bool selectedOnly, bool noEffects);
			public int WriteClipboardFile(string newFilename);
			public int WriteSequenceFileInDisplayOrder(string newFileName);
			public int WriteSequenceFileInDisplayOrder(string newFileName, bool selectedOnly, bool noEffects);
			public int WriteSequenceFileInCRGBDisplayOrder(string newFileName);
			public int WriteSequenceFileInCRGDisplayOrder(string newFileName, bool selectedOnly, bool noEffects);
			public int WriteSequenceFileInCRGAlphaOrder(string newFileName);
			public int WriteSequenceFileInCRGAlphaOrder(string newFileName, bool selectedOnly, bool noEffects);
			public channel FindChannel(int savedIndex);
			public channel FindChannel(string channelName);
			public int FindChannelIndex(string channelName);
			public rgbChannel FindrgbChannel(int savedIndex);
			public rgbChannel FindrgbChannel(string rgbChannelName);
			public channelGroup FindChannelGroup(int savedIndex);
			public channelGroup FindChannelGroup(string channelGroupName);
			public track FindTrack(string trackName);
			public int FindTrackIndex(string trackName);
			public timingGrid FindTimingGrid(string timingGridName);
			public int SavedIndexIntegrityCheck();
			public int[] OutputConflicts();
			public int[] DuplicateChannelNames();
			public int[] DuplicateChannelGroupNames();
			public int[] DuplicatergbChannelNames();
			public int[] DuplicateTrackNames();
			public int[] DuplicateNames();
			public static Int32 getKeyValue(string lineIn, string keyWord);
			public static string getKeyWord(string lineIn, string keyWord);
			public static deviceType enumDevice(string deviceName);
			public static effectType enumEffect(string effectName);
			public static timingGridType enumGridType(string typeName);
			public static string deviceName(deviceType devType);
			public static string effectName(effectType effType);
			public static string timingName(timingGridType grdType);
		} // end sequence class
	} // end sequence interface
} // end namespace LORUtils