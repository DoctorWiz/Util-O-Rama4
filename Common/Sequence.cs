using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace LORUtils
{
	public class LOR
	{
		public const int UNDEFINED = -1;
	}
	
	//internal enum deviceType
	public enum deviceType
	{ None, LOR, DMX, Digital };

	public enum effectType { None, intensity, shimmer, twinkle, DMX }

	//internal enum timingGridType
	public enum timingGridType
	{ None, freeform, fixedGrid };

	//internal enum tableType
	public enum tableType
	{ None, channel, rgbChannel, channelGroup, track, timingGrid }

	//internal class channel
	public class channel
	{
		public string name = "";
		public long color = 0;
		public long centiseconds = 0;
		public output output = new output();
		//public deviceType deviceType = deviceType.None;  // Depreciated
		//public int circuit = LOR.UNDEFINED;  // Depreciated
		//public int network = LOR.UNDEFINED;
		//public int unit = LOR.UNDEFINED;
		public int savedIndex = LOR.UNDEFINED;

		//public int firstEffectIndex = LOR.UNDEFINED;
		//public int finalEffectIndex = LOR.UNDEFINED;
		public bool written = false;

		public effect[] effects;
		public int effectCount = 0;

		public void AddEffect(effect newEffect)
		{
			Array.Resize(ref effects, effectCount + 1);
			effects[effectCount] = newEffect;
			//if (newEffect.endCentisecond > totalCentiseconds)
			//{
			//	totalCentiseconds = newEffect.totalCentiseconds;
			//}
		} // end channel.AddEffect

		//TODO: add RemoveEffect procedure
		//TODO: add SortEffects procedure (by startCentisecond)
	} // end channel

	public class output
	{
		public deviceType deviceType = deviceType.None;
		public int unit = LOR.UNDEFINED;
		public int circuit = LOR.UNDEFINED;
		public int network = LOR.UNDEFINED;

		public override string ToString()
		{
			return deviceType.ToString() + "~" + unit.ToString("000") + "~" + circuit.ToString("000") + "~" +
			       network.ToString("00000");
		}

		public void Parse(string data)
		{
			string[] info = data.Split('~');
			if (info.Length == 4)
			{
				//TODO: Get DeviceType
				unit = Int16.Parse(info[1]);
				circuit = Int16.Parse(info[2]);
				network = Int32.Parse(info[3]);
			}
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
	}

	//internal class rgbChannel
	public class rgbChannel
	{
		public string name = "";
		public long totalCentiseconds = 0;
		public int savedIndex = LOR.UNDEFINED;
		public int redChannelIndex = LOR.UNDEFINED;  // index/position in the channels[] array
		public int grnChannelIndex = LOR.UNDEFINED;
		public int bluChannelIndex = LOR.UNDEFINED;
		public int redSavedIndex = LOR.UNDEFINED;
		public int grnSavedIndex = LOR.UNDEFINED;
		public int bluSavedIndex = LOR.UNDEFINED;
		public bool written = false;
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
		public int[] itemSavedIndexes;
		public int itemCount = 0;
		public bool written = false;

		public void AddItem(int itemSavedIndex)
		{
			bool alreadyAdded = false;
			for (int i = 0; i < itemCount; i++)
			{
				if (itemSavedIndex == itemSavedIndexes[i])
				{
					//TODO: Using saved index, look up name of item being added
					string sMsg = "This item has already been added to this Channel Group '" + name + "'.";
					DialogResult rs = MessageBox.Show(sMsg, "Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						System.Diagnostics.Debugger.Break();
					//TODO: Make this just a warning, put "add" code below into an else block
					//TODO: Do the same with Tracks
					alreadyAdded = true;
					i = itemCount; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				Array.Resize(ref itemSavedIndexes, itemCount + 3);
				itemSavedIndexes[itemCount] = itemSavedIndex;
				itemCount++;
			}
		}

		//TODO: add RemoveItem procedure
	}

	/*
	internal class channelGroupItem
	{
		public int channelGroupListIndex;
		public int GroupItemSavedIndex;
		//public int channelSavedIndex;
	}
	*/

	//internal class effect
	public class effect
	{
		//public int channelIndex = LOR.UNDEFINED;
		//public int savedIndex = LOR.UNDEFINED;
		public effectType type = effectType.None;

		public long startCentisecond = LOR.UNDEFINED;
		public long endCentisecond = 9999999999L;
		public int intensity = LOR.UNDEFINED;
		public int startIntensity = LOR.UNDEFINED;
		public int endIntensity = LOR.UNDEFINED;
	}

	//internal class timingGrid
	public class timingGrid
	{
		// Tracks must have a timing grid assigned.
		// All sequences must contain at least one track, ergo all sequences neet at least one timing grid.
		public string name = "";

		// SaveID is like savedIndex for channels, but separate.
		// Same rules apply, see savedIndex above
		public int saveID = LOR.UNDEFINED;

		public timingGridType type = timingGridType.None;
		public int spacing = LOR.UNDEFINED;
		public long[] timings;
		public int itemCount = 0;

		public void AddTiming(long time)
		{
			Array.Resize(ref timings, itemCount + 1);
			timings[itemCount] = time;
			itemCount++;
			if (itemCount > 1)
			{
				if (timings[itemCount - 2] > timings[itemCount - 1])
				{
					// Array.Sort uses QuickSort, which is not the most efficient way to do this
					// Most efficient way is a (sort of) one-pass backwards bubble sort
					for (int n = itemCount - 1; n > 0; n--)
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
					for (int n = 1; n < itemCount; n++)
					{
						if (timings[n-1] == timings[n])
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
						itemCount -= offset;
					}
					// end duplicate check/removal
				} // end comparison
			} // end more than one
		} // end addTiming function
	

		public void Sort()
		{
			Array.Sort(timings);
		}
		//TODO: add RemovePosition
	} // end timingGrid class

	/*
	internal class timingGridItem
	{
		public int gridIndex = LOR.UNDEFINED;
		public long centisecond = LOR.UNDEFINED;
	}
	*/

	//internal class track
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
		public int timingGridIndex = LOR.UNDEFINED;
		public int timingGridSaveID = LOR.UNDEFINED;
		public int[] itemSavedIndexes;
		public int itemCount;
		public loopLevel[] loopLevels;
		public int loopLevelCount = 0;

		public void AddItem(int itemSavedIndex)
		{
			for (int i = 0; i < itemCount; i++)
			{
				if (itemSavedIndex == itemSavedIndexes[i])
				{
					string sMsg = "Item being Added to a Track '" + name + "' already exists in that Track!";
					DialogResult rs = MessageBox.Show(sMsg, "Tracks", MessageBoxButtons.OK, MessageBoxIcon.Error);
					if (System.Diagnostics.Debugger.IsAttached)
						System.Diagnostics.Debugger.Break();
				}
			}

			Array.Resize(ref itemSavedIndexes, itemCount + 1);
			itemSavedIndexes[itemCount] = itemSavedIndex;
			itemCount++;
		}
		//TODO: add RemoveItem procedure

		public int addLoopLevel(loopLevel loopLevel)
		{
			Array.Resize(ref loopLevels, loopLevelCount + 1);
			loopLevels[loopLevelCount] = loopLevel;
			loopLevelCount++;
			return loopLevelCount - 1;
		}
		public int addLoopLevel()
		{
			Array.Resize(ref loopLevels, loopLevelCount + 1);
			loopLevels[loopLevelCount] = new loopLevel();
			loopLevelCount++;
			return loopLevelCount - 1;
		}
	}

	public class animation
	{
		public int rows = LOR.UNDEFINED;
		public int columns = LOR.UNDEFINED;
		public string image = "";
		public animationRow[] animationRows;
		public int animationRowCount = 0;

		public int AddAnimationRow(animationRow animationRow)
		{
			Array.Resize(ref animationRows, animationRowCount + 1);
			animationRows[animationRowCount] = animationRow;
			animationRowCount++;
			return animationRowCount - 1;
		}
		public int AddAnimationRow()
		{
			Array.Resize(ref animationRows, animationRowCount + 1);
			animationRows[animationRowCount] = new animationRow();
			animationRowCount++;
			return animationRowCount - 1;
		}
	} // end animation class

	public class animationRow
	{
		public int rowIndex = LOR.UNDEFINED;
		public animationColumn[] animationColumns;
		public int animationColumnCount = 0;

		public int AddAnimationColumn(animationColumn animationColumn)
		{
			Array.Resize(ref animationColumns, animationColumnCount + 1);
			animationColumns[animationColumnCount] = animationColumn;
			animationColumnCount++;
			return animationColumnCount - 1;
		}
		public int AddAnimationColumn()
		{
			Array.Resize(ref animationColumns, animationColumnCount + 1);
			animationColumns[animationColumnCount] = new animationColumn();
			animationColumnCount++;
			return animationColumnCount - 1;
		}
		public int AddAnimationColumn(int columnIndex, int channel)
		{
			Array.Resize(ref animationColumns, animationColumnCount + 1);
			animationColumns[animationColumnCount] = new animationColumn();
			animationColumns[animationColumnCount].columnIndex = columnIndex;
			animationColumns[animationColumnCount].channel = channel;
			animationColumnCount++;
			return animationColumnCount - 1;
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
		public loop[] loops;
		public int loopsCount = 0;
		public int AddLoop(long startCentisecond, long endCentisecond, int loopCount)
		{
			Array.Resize(ref loops, loopsCount + 1);
			loops[loopsCount] = new loop();
			loops[loopsCount].startCentsecond = startCentisecond;
			loops[loopsCount].endCentisecond = endCentisecond;
			loops[loopsCount].loopCount = loopCount;
			loopsCount++;
			return loopsCount - 1;
		}
		public int AddLoop()
		{
			Array.Resize(ref loops, loopsCount + 1);
			loops[loopsCount] = new loop();
			loopsCount++;
			return loopsCount - 1;
		}
		public int AddLoop(loop loop)
		{
			Array.Resize(ref loops, loopsCount + 1);
			loops[loopsCount] = loop;
			loopsCount++;
			return loopsCount - 1;
		}
	} // end loopLevel class

	/*
	internal class trackItem
	{
		public int trackIndex = LOR.UNDEFINED;
		public int trackItemSavedIndex = LOR.UNDEFINED;
	}
	*/

	//internal class Sequence
	public class Sequence
	{
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

		private StreamWriter writer;
		private string lineOut = ""; // line to be written out, gets modified if necessary
		private int curSavedIndex = 0;
		//private string dbgMsg = "";

		public channel[] channels;
		public savedIndex[] savedIndexes;
		public rgbChannel[] rgbChannels;
		public channelGroup[] channelGroups;

		//public channelGroupItem[] channelGroupItems;
		//public effect[] effects;
		public timingGrid[] timingGrids;

		//public timingGridItem[] timingGridItems;
		public track[] tracks;
		public animation animation;

		//public trackItem[] trackItems;

		public int lineCount = 0;
		public int channelCount = 0;
		public int rgbChannelCount = 0;
		public int channelGroupCount = 0;
		public int groupItemCount = 0;
		public int effectCount = 0;
		public int timingGridCount = 0;
		public int gridItemCount = 0;
		public int trackCount = 0;
		public int trackItemCount = 0;
		public int highestSavedIndex = LOR.UNDEFINED;
		public long totalCentiseconds = 0;

		public string FileName = "";
		public string xmlInfo;
		//public string sequenceInfo;
		public string animationInfo;

		public int saveFileVersion = 14;
		public string author = "";
		public string createdAt = "";
		public string musicAlbum = "";
		public string musicArtist = "";
		public string musicFilename = "";
		public string musicTitle = "";
		public int videoUsage = 0;
		public bool musical = false;

		public string sequenceInfo
		{
			get
			{
				string ret = STFLD + TABLEsequence;
				ret += SPC + FIELDsaveFileVersion + FIELDEQ + saveFileVersion.ToString() + ENDQT;
				//ret += SPC + FIELDcreatedAt + FIELDEQ + now.toString("mm/dd/yyyy hh:mm:ss PM") + ENDQT;
				ret += SPC + FIELDauthor + FIELDEQ + author + ENDQT;
				ret += SPC + FIELDcreatedAt + FIELDEQ + createdAt + ENDQT;
				if (musical)
				{
					ret += SPC + FIELDmusicAlbum + FIELDEQ + musicAlbum + ENDQT;
					ret += SPC + FIELDmusicArtist + FIELDEQ + musicArtist + ENDQT;
					ret += SPC + FIELDmusicFilename + FIELDEQ + musicFilename + ENDQT;
					ret += SPC + FIELDmusicTitle + FIELDEQ + musicTitle + ENDQT;
				}
				ret += SPC + FIELDvideoUsage + FIELDEQ + videoUsage.ToString() + ENDQT + ENDTBL;
				return ret;
			}
			set
			{
				saveFileVersion = getKeyValue(value, FIELDsaveFileVersion);
				author = getKeyWord(value, FIELDauthor);
				createdAt = getKeyWord(value, FIELDcreatedAt);
				musicFilename = getKeyWord(value, FIELDmusicFilename);
				musical = (musicFilename.Length > 4);
				if (musical)
				{
					musicAlbum = getKeyWord(value, FIELDmusicAlbum);
					musicArtist = getKeyWord(value, FIELDmusicArtist);
					musicTitle = getKeyWord(value, FIELDmusicTitle);
				}
				videoUsage = getKeyValue(value, FIELDvideoUsage);
			}
		}



		private struct updatedTrack
		{
			public int[] newSavedIndexes;
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
		public static string SequenceFolder
		{
			get
			{
				const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
				string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				string fldr = (string)Registry.GetValue(keyName, "NonAudioPath", userDocs);
				return fldr;
			} // End getSequenceFolder
		}

		public int AddChannel(channel newChan)
		{
			if (newChan.savedIndex < 0)
			{
				highestSavedIndex++;
				newChan.savedIndex = highestSavedIndex;
				Array.Resize(ref savedIndexes, highestSavedIndex + 3);
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.channel;
				savedIndexes[highestSavedIndex].objIndex = channelCount;
			}
			Array.Resize(ref channels, channelCount + 3);
			channels[channelCount] = newChan;
			channelCount++;

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
				Array.Resize(ref savedIndexes, highestSavedIndex + 3);
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.rgbChannel;
				savedIndexes[highestSavedIndex].objIndex = rgbChannelCount;
			}
			Array.Resize(ref rgbChannels, rgbChannelCount + 3);
			rgbChannels[rgbChannelCount] = newChan;
			rgbChannelCount++;
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
				Array.Resize(ref savedIndexes, highestSavedIndex + 3);
				savedIndexes[highestSavedIndex] = new savedIndex();
				savedIndexes[highestSavedIndex].objType = tableType.channelGroup;
				savedIndexes[highestSavedIndex].objIndex = channelGroupCount;
			}


			Array.Resize(ref channelGroups, channelGroupCount + 3);
			channelGroups[channelGroupCount] = newGroup;
			channelGroupCount++;
			if (newGroup.totalCentiseconds > totalCentiseconds)
			{
				totalCentiseconds = newGroup.totalCentiseconds;
			}
			return highestSavedIndex;
		}

		//TODO: add RemoveChannel, RemoveRGBchannel, RemoveChannelGroup, and RemoveTrack procedures

		public int AddTimingGrid(timingGrid newGrid)
		{
			Array.Resize(ref timingGrids, timingGridCount + 3);
			timingGrids[timingGridCount] = newGrid;
			timingGridCount++;
			return timingGridCount - 1;
		}

		public int AddTrack(track newTrack)
		{
			Array.Resize(ref tracks, trackCount + 3);
			tracks[trackCount] = newTrack;
			trackCount++;
			if (newTrack.totalCentiseconds > totalCentiseconds)
			{
				totalCentiseconds = newTrack.totalCentiseconds;
			}
			return trackCount - 1;
		}

		public int AddSavedIndex(savedIndex si, int position)
		{
			if (position > highestSavedIndex)
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

		public int readFile_NOTWORKING(string existingFileName)
		{
			int errState = 0;
			lineCount = 0;
			StreamReader reader = new StreamReader(existingFileName);
			string lineIn; // line read in (does not get modified)
			int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			//lineCount = 0;
			channelCount = 0;
			rgbChannelCount = 0;
			channelGroupCount = 0;
			highestSavedIndex = 0;
			groupItemCount = 0;
			effectCount = 0;
			timingGridCount = 0;
			gridItemCount = 0;
			trackCount = 0;
			trackItemCount = 0;
			highestSavedIndex = 0;
			totalCentiseconds = 0;

			int curChannel = LOR.UNDEFINED;
			int currgbChannel = LOR.UNDEFINED;
			int curSavedIndex = LOR.UNDEFINED;
			int curChannelGroupList = LOR.UNDEFINED;
			//int curGroupItem = LOR.UNDEFINED;
			int curEffect = LOR.UNDEFINED;
			int curTimingGrid = LOR.UNDEFINED;
			int curGridItem = LOR.UNDEFINED;
			int curTrack = LOR.UNDEFINED;

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				pos1 = lineIn.IndexOf("xml version=");
				if (pos1 > 0)
				{
					xmlInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("saveFileVersion=");
				if (pos1 > 0)
				{
					sequenceInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("animation rows=");
				if (pos1 > 0)
				{
					animationInfo = lineIn;
				}
				//pos1 = lineIn.IndexOf("<channel name=");
				pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					channelCount++;
				}
				//pos1 = lineIn.IndexOf("<rgbChannel totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLErgbChannel + SPC);
				if (pos1 > 0)
				{
					rgbChannelCount++;
				}
				//pos1 = lineIn.IndexOf("<channelGroup totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLEchannelGroupList + SPC);
				if (pos1 > 0)
				{
					channelGroupCount++;
				}
				//pos1 = lineIn.IndexOf("<effect type=");
				pos1 = lineIn.IndexOf(STFLD + TABLEeffect + SPC);
				if (pos1 > 0)
				{
					effectCount++;
				}
				if (trackCount == 0)
				{
					pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
					if (pos1 > 0)
					{
						groupItemCount++;
					}
				}
				//pos1 = lineIn.IndexOf("<timingGrid ");
				pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
				if (pos1 > 0)
				{
					timingGridCount++;
				}
				//pos1 = lineIn.IndexOf("<timing centisecond=");
				pos1 = lineIn.IndexOf(STFLD + TABLEtiming + SPC);
				if (pos1 > 0)
				{
					gridItemCount++;
				}
				//pos1 = lineIn.IndexOf("<track totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLEtrack + SPC);
				if (pos1 > 0)
				{
					trackCount++;
				}
				if (trackCount > 0)
				{
					pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC);
					if (pos1 > 0)
					{
						trackItemCount++;
					}
				}

				//pos1 = lineIn.IndexOf(" savedIndex=");
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
			//rgbChannel[] rgbChannels;
			//int[] channelGroup;
			//savedIndex[] savedIndexes;
			channels = new channel[channelCount + 2];
			savedIndexes = new savedIndex[highestSavedIndex + 3];
			rgbChannels = new rgbChannel[rgbChannelCount + 2];
			channelGroups = new channelGroup[channelGroupCount + 2];
			//channelGroupItems = new channelGroupItem[groupItemCount + 2];
			//effects = new effect[effectCount + 2];
			timingGrids = new timingGrid[timingGridCount + 2];
			//timingGridItems = new timingGridItem[gridItemCount + 2];
			tracks = new track[trackCount + 2];
			//trackItems = new trackItem[trackItemCount + 2];

			// * PASS 2 - COLLECT OBJECTS
			reader = new StreamReader(existingFileName);
			lineCount = 0;
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a regular channel?
				pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					curChannel++;
					channel chan = new channel();
					savedIndex si = new savedIndex();
					chan.name = getKeyWord(lineIn, FIELDname);
					chan.color = getKeyValue(lineIn, FIELDcolor);
					chan.centiseconds = getKeyValue(lineIn, FIELDcentiseconds);
					chan.output.deviceType = enumDevice(getKeyWord(lineIn, FIELDdeviceType));
					chan.output.unit = getKeyValue(lineIn, FIELDunit);
					chan.output.network = getKeyValue(lineIn, FIELDnetwork);
					chan.output.circuit = getKeyValue(lineIn, FIELDcircuit);
					chan.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					channels[curChannel] = chan;
					curSavedIndex = chan.savedIndex;

					si.objType = tableType.channel;
					si.objIndex = curChannel;
					savedIndexes[curSavedIndex] = si;
				}

				// does this line mark the start of a RGB channel?
				pos1 = lineIn.IndexOf(TABLErgbChannel + SPC + FIELDtotalCentiseconds);
				if (pos1 > 0)
				{
					currgbChannel++;
					rgbChannel rgbc = new rgbChannel();
					savedIndex si = new savedIndex();
					rgbc.name = getKeyWord(lineIn, FIELDname);
					rgbc.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
					rgbc.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					curSavedIndex = rgbc.savedIndex;
					lineIn = reader.ReadLine();
					lineIn = reader.ReadLine();
					rgbc.redSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					rgbc.redChannelIndex = savedIndexes[rgbc.redSavedIndex].objIndex;
					lineIn = reader.ReadLine();
					rgbc.grnSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					rgbc.grnChannelIndex = savedIndexes[rgbc.grnSavedIndex].objIndex;
					lineIn = reader.ReadLine();
					rgbc.bluSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
					rgbc.bluChannelIndex = savedIndexes[rgbc.bluSavedIndex].objIndex;
					rgbChannels[currgbChannel] = rgbc;

					si.objType = tableType.rgbChannel;
					si.objIndex = currgbChannel;
					savedIndexes[curSavedIndex] = si;
				}

				// does this line mark the start of a Channel Group List?
				if (curTrack < 0)
				{
					pos1 = lineIn.IndexOf(TABLEchannelGroupList + SPC + FIELDtotalCentiseconds);
					if (pos1 > 0)
					{
						curChannelGroupList++;
						channelGroup changl = new channelGroup();
						savedIndex si = new savedIndex();
						changl.name = getKeyWord(lineIn, FIELDname);
						changl.channelIndex = curChannel;
						changl.channelSavedIndex = curSavedIndex;
						changl.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
						changl.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						curSavedIndex = changl.savedIndex;
						channelGroups[curChannelGroupList] = changl;

						si.objType = tableType.channelGroup;
						si.objIndex = curChannelGroupList;
						savedIndexes[curSavedIndex] = si;
						lineIn = reader.ReadLine();
						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
						while (pos1 > 0)
						{
							//curGroupItem++;
							//channelGroupItem gi = new channelGroupItem();
							//gi.channelGroupListIndex = curChannelGroupList;
							//gi.GroupItemSavedIndex = curSavedIndex;
							//gi.GroupItemSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
							int isl = getKeyValue(lineIn, FIELDsavedIndex);
							//channelGroupItems[curGroupItem] = gi;
							changl.AddItem(isl);

							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
						}
					}
				}

				// does this line mark the start of an Effect?
				pos1 = lineIn.IndexOf(TABLEeffect + SPC + FIELDtype);
				if (pos1 > 0)
				{
					curEffect++;

					//DEBUG!
					if (curEffect > 638)
					{
						errState = 1;
					}

					effect ef = new effect();
					//ef.channelIndex = curChannel;
					//ef.savedIndex = curSavedIndex;
					ef.type = enumEffect(getKeyWord(lineIn, FIELDtype));
					ef.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
					ef.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
					ef.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
					ef.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
					ef.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
					//if (channels[curChannel].firstEffectIndex < 0)
					//{
					//	channels[curChannel].firstEffectIndex = curEffect;
					//}
					//channels[curChannel].finalEffectIndex = curEffect;
					//effects[curEffect] = ef;
					channels[curChannel].AddEffect(ef);
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
							//timingGridItem grit = new timingGridItem();
							//grit.gridIndex = curTimingGrid;
							int gpos = getKeyValue(lineIn, FIELDcentisecond);
							timingGrids[curTimingGrid].AddTiming(gpos);

							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TABLEtiming + SPC + FIELDcentisecond);
						}
					}
				}

				// does this line mark the start of a Track?
				pos1 = lineIn.IndexOf(TABLEtrack + SPC);
				if (pos1 > 0)
				{
					curTrack++;
					track trk = new track();
					trk.name = getKeyWord(lineIn, FIELDname);
					trk.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
					totalCentiseconds = trk.totalCentiseconds;
					trk.timingGridSaveID = getKeyValue(lineIn, TABLEtimingGrid);
					tracks[curTrack] = trk;

					lineIn = reader.ReadLine();
					lineIn = reader.ReadLine();
					pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
					while (pos1 > 0)
					{
						//curTrackItem++;
						//trackItem tit = new trackItem();
						//tit.trackIndex = curTrack;
						int isi = getKeyValue(lineIn, FIELDsavedIndex);
						//tit.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						trk.AddItem(isi);
						//trackItems[curTrackItem] = tit;

						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
					}
				}

				// does this line mark the start of a Track Item?
			}

			/*
			channel chan2 = new channel();
			channels[curChannel + 1] = chan2;
			rgbChannel rgbc2 = new rgbChannel();
			rgbChannels[currgbChannel + 1] = rgbc2;
			savedIndex si2 = new savedIndex();
			savedIndexes[curSavedIndex + 1] = si2;
			channelGroup changl2 = new channelGroup();
			channelGroups[curChannelGroupList + 1] = changl2;
			//channelGroupItem gi2 = new channelGroupItem();
			//channelGroupItems[curGroupItem + 1] = gi2;
			effect ef2 = new effect();
			ef2.type = enumEffect(getKeyWord(lineIn, FIELDtype));
			ef2.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
			ef2.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
			ef2.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
			ef2.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
			ef2.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
			channels[curChannel+1].AddEffect(ef2);

			timingGrid tg2 = new timingGrid();
			AddTimingGrid(tg2);
			tg2.AddTiming(0);
			track trk2 = new track();
			tracks[curTrack + 1] = trk2;
			//trackItem tit2 = new trackItem();
			//trackItems[curTrackItem + 1] = tit2;
			*/

			reader.Close();
			totalCentiseconds = tracks[0].totalCentiseconds;

			if (errState <= 0)
			{
				FileName = existingFileName;
			}

			return errState;
		}

		public int Old_ReadSequenceFile(string existingFileName)
		{
			int errState = 0;
			StreamReader reader = new StreamReader(existingFileName);
			string lineIn; // line read in (does not get modified)
			int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			lineCount = 0;
			channelCount = 0;
			rgbChannelCount = 0;
			channelGroupCount = 0;
			highestSavedIndex = 0;
			groupItemCount = 0;
			effectCount = 0;
			timingGridCount = 0;
			gridItemCount = 0;
			trackCount = 0;
			trackItemCount = 0;
			highestSavedIndex = 0;
			totalCentiseconds = 0;

			int curChannel = LOR.UNDEFINED;
			int currgbChannel = LOR.UNDEFINED;
			int curSavedIndex = LOR.UNDEFINED;
			int curChannelGroupList = LOR.UNDEFINED;
			//int curGroupItem = LOR.UNDEFINED;
			int curEffect = LOR.UNDEFINED;
			int curTimingGrid = LOR.UNDEFINED;
			int curGridItem = LOR.UNDEFINED;
			int curTrack = LOR.UNDEFINED;
			bool trackMode = false;

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				pos1 = lineIn.IndexOf("xml version=");
				if (pos1 > 0)
				{
					xmlInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("saveFileVersion=");
				if (pos1 > 0)
				{
					sequenceInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("animation rows=");
				if (pos1 > 0)
				{
					animationInfo = lineIn;
				}
				//pos1 = lineIn.IndexOf("<channel name=");
				pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					channelCount++;
				}
				//pos1 = lineIn.IndexOf("<rgbChannel totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLErgbChannel + SPC);
				if (pos1 > 0)
				{
					rgbChannelCount++;
				}
				//pos1 = lineIn.IndexOf("<channelGroup totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLEchannelGroupList + SPC);
				if (pos1 > 0)
				{
					channelGroupCount++;
				}
				//pos1 = lineIn.IndexOf("<effect type=");
				pos1 = lineIn.IndexOf(STFLD + TABLEeffect + SPC);
				if (pos1 > 0)
				{
					effectCount++;
				}
				if (trackCount == 0)
				{
					pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
					if (pos1 > 0)
					{
						groupItemCount++;
					}
				}
				//pos1 = lineIn.IndexOf("<timingGrid ");
				pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
				if (pos1 > 0)
				{
					timingGridCount++;
				}
				//pos1 = lineIn.IndexOf("<timing centisecond=");
				pos1 = lineIn.IndexOf(STFLD + TABLEtiming + SPC);
				if (pos1 > 0)
				{
					gridItemCount++;
				}
				//pos1 = lineIn.IndexOf("<track totalCentiseconds=");
				pos1 = lineIn.IndexOf(STFLD + TABLEtrack + SPC);
				if (pos1 > 0)
				{
					trackCount++;
				}
				if (trackCount > 0)
				{
					pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC);
					if (pos1 > 0)
					{
						trackItemCount++;
					}
				}

				//pos1 = lineIn.IndexOf(" savedIndex=");
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
			channels = new channel[channelCount + 2];
			savedIndexes = new savedIndex[highestSavedIndex + 3];
			rgbChannels = new rgbChannel[rgbChannelCount + 2];
			channelGroups = new channelGroup[channelGroupCount + 2];
			timingGrids = new timingGrid[timingGridCount + 2];
			tracks = new track[trackCount + 2];

			//////////////////////////////////
			// * PASS 2 - COLLECT OBJECTS * //
			//////////////////////////////////
			reader = new StreamReader(existingFileName);
			lineCount = 0;
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// have we reached the tracks section?
				pos1 = lineIn.IndexOf(STARTtracks);
				if (pos1 > 0)
				{
					trackMode = true;
				}
				if (!trackMode)
				{
					// does this line mark the start of a regular channel?
					pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDname);
					if (pos1 > 0)
					{
						curChannel++;
						channel chan = new channel();
						savedIndex si = new savedIndex();
						chan.name = getKeyWord(lineIn, FIELDname);
						chan.color = getKeyValue(lineIn, FIELDcolor);
						chan.centiseconds = getKeyValue(lineIn, FIELDcentiseconds);
						chan.output.deviceType = enumDevice(getKeyWord(lineIn, FIELDdeviceType));
						chan.output.unit = getKeyValue(lineIn, FIELDunit);
						chan.output.network = getKeyValue(lineIn, FIELDnetwork);
						chan.output.circuit = getKeyValue(lineIn, FIELDcircuit);
						chan.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						channels[curChannel] = chan;
						curSavedIndex = chan.savedIndex;

						si.objType = tableType.channel;
						si.objIndex = curChannel;
						savedIndexes[curSavedIndex] = si;
					}

					// does this line mark the start of a RGB channel?
					pos1 = lineIn.IndexOf(TABLErgbChannel + SPC + FIELDtotalCentiseconds);
					if (pos1 > 0)
					{
						currgbChannel++;
						rgbChannel rgbc = new rgbChannel();
						savedIndex si = new savedIndex();
						rgbc.name = getKeyWord(lineIn, FIELDname);
						rgbc.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
						rgbc.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						curSavedIndex = rgbc.savedIndex;
						lineIn = reader.ReadLine();
						lineIn = reader.ReadLine();
						rgbc.redSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.redChannelIndex = savedIndexes[rgbc.redSavedIndex].objIndex;
						lineIn = reader.ReadLine();
						rgbc.grnSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.grnChannelIndex = savedIndexes[rgbc.grnSavedIndex].objIndex;
						lineIn = reader.ReadLine();
						rgbc.bluSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.bluChannelIndex = savedIndexes[rgbc.bluSavedIndex].objIndex;
						rgbChannels[currgbChannel] = rgbc;

						si.objType = tableType.rgbChannel;
						si.objIndex = currgbChannel;
						savedIndexes[curSavedIndex] = si;
					}

					// does this line mark the start of a Channel Group List?
					if (curTrack < 0)
					{
						pos1 = lineIn.IndexOf(TABLEchannelGroupList + SPC + FIELDtotalCentiseconds);
						if (pos1 > 0)
						{
							curChannelGroupList++;
							channelGroup changl = new channelGroup();
							savedIndex si = new savedIndex();
							changl.name = getKeyWord(lineIn, FIELDname);
							changl.channelIndex = curChannel;
							changl.channelSavedIndex = curSavedIndex;
							changl.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
							changl.savedIndex = getKeyValue(lineIn, FIELDsavedIndex);
							curSavedIndex = changl.savedIndex;
							channelGroups[curChannelGroupList] = changl;

							si.objType = tableType.channelGroup;
							si.objIndex = curChannelGroupList;
							savedIndexes[curSavedIndex] = si;
							lineIn = reader.ReadLine();
							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
							while (pos1 > 0)
							{
								int isl = getKeyValue(lineIn, FIELDsavedIndex);
								changl.AddItem(isl);
								lineIn = reader.ReadLine();
								pos1 = lineIn.IndexOf(TABLEchannelGroup + SPC + FIELDsavedIndex);
							}
						}
					}

					// does this line mark the start of an Effect?
					pos1 = lineIn.IndexOf(TABLEeffect + SPC + FIELDtype);
					if (pos1 > 0)
					{
						curEffect++;

						//DEBUG!
						if (curEffect > 638)
						{
							errState = 1;
						}

						effect ef = new effect();
						ef.type = enumEffect(getKeyWord(lineIn, FIELDtype));
						ef.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
						ef.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
						ef.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
						ef.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
						ef.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
						channels[curChannel].AddEffect(ef);
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
						}
					}
				}
				else // in Track Mode
				{
					// does this line mark the start of a Track?
					pos1 = lineIn.IndexOf(TABLEtrack + SPC);
					if (pos1 > 0)
					{
						curTrack++;
						track trk = new track();
						trk.name = getKeyWord(lineIn, FIELDname);
						trk.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
						totalCentiseconds = trk.totalCentiseconds;
						trk.timingGridSaveID = getKeyValue(lineIn, TABLEtimingGrid);
						tracks[curTrack] = trk;

						lineIn = reader.ReadLine();
						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
						while (pos1 > 0)
						{
							int isi = getKeyValue(lineIn, FIELDsavedIndex);
							trk.AddItem(isi);
							lineIn = reader.ReadLine();
							pos1 = lineIn.IndexOf(TABLEchannel + SPC + FIELDsavedIndex);
						}
					} // end if start of track
				} // end Track Mode (or not)
			} // end while line is valid

			reader.Close();
			totalCentiseconds = tracks[0].totalCentiseconds;

			if (errState <= 0)
			{
				FileName = existingFileName;
			}

			return errState;
		} // end Old_ReadSequenceFile

		public void Clear(bool areYouReallySureYouWantToDoThis)
		{
			if (areYouReallySureYouWantToDoThis)
			{
				// Zero these out from any previous run
				lineCount = 0;
				channelCount = 0;
				rgbChannelCount = 0;
				channelGroupCount = 0;
				highestSavedIndex = 0;
				groupItemCount = 0;
				effectCount = 0;
				timingGridCount = 0;
				gridItemCount = 0;
				trackCount = 0;
				trackItemCount = 0;
				highestSavedIndex = 0;
				totalCentiseconds = 0;

				channels = null;
				Array.Resize(ref channels, 2);
				rgbChannels = null;
				Array.Resize(ref rgbChannels, 2);
				channelGroups = null;
				Array.Resize(ref channelGroups, 2);
				tracks = null;
				Array.Resize(ref tracks, 2);
				timingGrids = null;
				Array.Resize(ref timingGrids, 2);
				savedIndexes = null;
				Array.Resize(ref savedIndexes, 2);

				FileName = "";
				xmlInfo = "";
				sequenceInfo = "";
				animationInfo = "";
			} // end Are You Sure
		} // end Clear Sequence

		public int ReadSequenceFile(string existingFileName)
		{
			int errState = 0;
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
					si.objIndex = channelCount - 1;

					AddSavedIndex(si, chan.savedIndex);

					pos1 = lineIn.IndexOf(ENDFLD);
					if (pos1 < 0)
					{
						//lineIn = reader.ReadLine();
						//lineCount++;
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
							chan.AddEffect(ef);

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
						rgbc.redSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.redChannelIndex = savedIndexes[rgbc.redSavedIndex].objIndex;
						lineIn = reader.ReadLine();
						lineCount++;
						rgbc.grnSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.grnChannelIndex = savedIndexes[rgbc.grnSavedIndex].objIndex;
						lineIn = reader.ReadLine();
						lineCount++;
						rgbc.bluSavedIndex = getKeyValue(lineIn, FIELDsavedIndex);
						rgbc.bluChannelIndex = savedIndexes[rgbc.bluSavedIndex].objIndex;

						AddRGBChannel(rgbc);

						si.objType = tableType.rgbChannel;
						si.objIndex = rgbChannelCount - 1;

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
							//changl.channelIndex = curChannel;

							changl.channelSavedIndex = curSavedIndex;
							changl.totalCentiseconds = getKeyValue(lineIn, FIELDtotalCentiseconds);
							//channelGroups[curChannelGroupList] = changl;

							AddChannelGroup(changl);

							si.objType = tableType.channelGroup;
							si.objIndex = channelGroupCount - 1;

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

								AddTimingGrid(tg);

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
											trk.AddItem(isi);

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
										trk.addLoopLevel(ll);
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
											ll.AddLoop(loooop);
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
													animation.AddAnimationRow(aniRow);
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
															xmlInfo = lineIn;
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
			//totalCentiseconds = tracks[0].totalCentiseconds;

			if (errState <= 0)
			{
				FileName = existingFileName;
				string sMsg = summary();
				MessageBox.Show(sMsg, "Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return errState;
		} // end ReadSequenceFile

		public string summary()
		{
			string sMsg = "";
			sMsg += "           Filename: " + FileName + CRLF + CRLF;
			sMsg += "              Lines: " + lineCount.ToString() + CRLF;
			sMsg += "   Regular Channels: " + channelCount.ToString() + CRLF;
			sMsg += "       RGB Channels: " + rgbChannelCount.ToString() + CRLF;
			sMsg += "     Channel Groups: " + channelGroupCount.ToString() + CRLF;
			sMsg += "       Timing Grids: " + timingGridCount.ToString() + CRLF;
			sMsg += "             Tracks: " + trackCount.ToString() + CRLF;
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
			if (channelCount > 0)
			{
				// resize list to channel count
				Array.Resize(ref matches, channelCount);
				// loop thru channels, collect name and info
				for (int ch = 0; ch < channelCount; ch++)
				{
					matches[ch].name = channels[ch].name;
					matches[ch].savedIdx = channels[ch].savedIndex;
					matches[ch].type = tableType.channel;
					matches[ch].itemIdx = ch;
				}
				q = channelCount;
			} // channel count > 0

			// Any RGB Channels?
			if (rgbChannelCount > 0)
			{
				// Loop thru 'em and add their name and info to the list
				for (int rg = 0; rg < rgbChannelCount; rg++)
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
			if (channelGroupCount > 0)
			{
				for (int gr = 0; gr < channelGroupCount; gr++)
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
			int errState = 0;
			StreamReader reader = new StreamReader(existingFilename);
			string lineIn; // line read in (does not get modified)
			int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			// Zero these out from any previous run
			lineCount = 0;
			channelCount = 0;
			rgbChannelCount = 0;
			highestSavedIndex = 0;
			groupItemCount = 0;
			effectCount = 0;
			timingGridCount = 0;
			gridItemCount = 0;
			trackCount = 0;
			trackItemCount = 0;
			totalCentiseconds = 0;

			int curChannel = LOR.UNDEFINED;
			//int currgbChannel = LOR.UNDEFINED;
			int curSavedIndex = LOR.UNDEFINED;
			int curEffect = LOR.UNDEFINED;
			int curTimingGrid = LOR.UNDEFINED;
			int curGridItem = LOR.UNDEFINED;
			//int curTrack = LOR.UNDEFINED;
			//bool trackMode = false;

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a channel?
				pos1 = lineIn.IndexOf("xml version=");
				if (pos1 > 0)
				{
					xmlInfo = lineIn;
				}
				pos1 = lineIn.IndexOf("saveFileVersion=");
				if (pos1 > 0)
				{
					sequenceInfo = lineIn;
				}
				//pos1 = lineIn.IndexOf("<channel name=");
				pos1 = lineIn.IndexOf(STFLD + TABLEchannel + SPC + FIELDname);
				if (pos1 > 0)
				{
					channelCount++;
				}
				//pos1 = lineIn.IndexOf("<effect type=");
				pos1 = lineIn.IndexOf(STFLD + TABLEeffect + SPC);
				if (pos1 > 0)
				{
					effectCount++;
				}
				if (trackCount == 0)
				{
				}
				//pos1 = lineIn.IndexOf("<timingGrid ");
				pos1 = lineIn.IndexOf(STFLD + TABLEtimingGrid + SPC);
				if (pos1 > 0)
				{
					timingGridCount++;
				}
				//pos1 = lineIn.IndexOf("<timing centisecond=");
				pos1 = lineIn.IndexOf(STFLD + TABLEtiming + SPC);
				if (pos1 > 0)
				{
					gridItemCount++;
				}

				//pos1 = lineIn.IndexOf(" savedIndex=");
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
			channels = new channel[channelCount + 2];
			//savedIndexes = new savedIndex[highestSavedIndex + 3];
			rgbChannels = new rgbChannel[rgbChannelCount + 2];
			timingGrids = new timingGrid[timingGridCount + 2];
			tracks = new track[1];
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
						errState = 1;
					}

					effect ef = new effect();
					ef.type = enumEffect(getKeyWord(lineIn, FIELDtype));
					ef.startCentisecond = getKeyValue(lineIn, FIELDstartCentisecond);
					ef.endCentisecond = getKeyValue(lineIn, FIELDendCentisecond);
					ef.intensity = getKeyValue(lineIn, SPC + FIELDintensity);
					ef.startIntensity = getKeyValue(lineIn, FIELDstartIntensity);
					ef.endIntensity = getKeyValue(lineIn, FIELDendIntensity);
					channels[curChannel].AddEffect(ef);
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

			if (errState <= 0)
			{
				FileName = existingFilename;
			}

			return errState;
		}

		public int WriteSequenceFile(string newFilename)
		{
			int errState = 0;
			lineCount = 0;

			//backupFile(fileName);

			string tmpFile = newFilename + ".tmp";

			writer = new StreamWriter(tmpFile);
			string lineOut = ""; // line to be written out, gets modified if necessary
													 //int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

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

			lineOut = xmlInfo;
			writer.WriteLine(lineOut); lineCount++;
			lineOut = sequenceInfo;
			writer.WriteLine(lineOut); lineCount++;
			lineOut = LEVEL1 + STFLD + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			while (curChannel < channelCount)
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
				//if (effects[curEffect].channelIndex == curChannel)
				if (chnl.effectCount > 0)
				{
					// complete channel line with regular '>' then do effects
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					writeEffects(channels[curChannel]);
					//while (effects[curEffect].channelIndex == curChannel)
				}
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
				if (currgbChannel < rgbChannelCount)
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

				if (curChannelGroupList < channelGroupCount)
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
						/*
						while (channelGroupItems[curGroupItem].channelGroupListIndex == curChannelGroupList)
						{
							lineOut = LEVEL4 + STFLD + TABLEchannelGroup;
							lineOut += SPC + FIELDsavedIndex + FIELDEQ + channelGroupItems[curGroupItem].GroupItemSavedIndex.ToString() + ENDQT;
							lineOut += ENDFLD;
							writer.WriteLine(lineOut); lineCount++;

							curGroupItem++;
						}
						*/
						for (int igi = 0; igi < channelGroups[curChannelGroupList].itemCount; igi++)
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
			lineOut = LEVEL1 + STFLD + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			while (curTimingGrid < timingGridCount)
			{
				lineOut = LEVEL2 + STFLD + TABLEtimingGrid;
				lineOut += SPC + FIELDsaveID + FIELDEQ + timingGrids[curTimingGrid].saveID.ToString() + ENDQT;
				if (timingGrids[curTimingGrid].name.Length > 1)
				{
					lineOut += SPC + FIELDname + FIELDEQ + timingGrids[curTimingGrid].name + ENDQT;
				}
				lineOut += SPC + FIELDtype + FIELDEQ + timingName(timingGrids[curTimingGrid].type) + ENDQT;
				if (timingGrids[curTimingGrid].spacing > 1)
				{
					lineOut += SPC + FIELDspacing + FIELDEQ + timingGrids[curTimingGrid].spacing.ToString() + ENDQT;
				}
				if (timingGrids[curTimingGrid].type == timingGridType.fixedGrid)
				{
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				else if (timingGrids[curTimingGrid].type == timingGridType.freeform)
				{
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					for (int tm = 0; tm < timingGrids[curTimingGrid].itemCount; tm++)
					//while (timingGridItems[curGridItem].gridIndex == curTimingGrid)
					{
						lineOut = LEVEL4 + STFLD + TABLEtiming;
						lineOut += SPC + FIELDcentisecond + FIELDEQ + timingGrids[curTimingGrid].timings[tm].ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;

						curGridItem++;
					}

					lineOut = LEVEL1 + FINTBL + TABLEtimingGrid + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				curTimingGrid++;
			}
			lineOut = LEVEL1 + FINTBL + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TRACKS
			lineOut = "  <" + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			//while (curTrack < trackCount)
			for (int trk=0; trk < trackCount; trk++)
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

				/*
				while (trackItems[curTrackItem].trackIndex == curTrack)
				{
					lineOut = LEVEL4 + STFLD + TABLEchannel;
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + trackItems[curTrackItem].savedIndex.ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;

					curTrackItem++;
				}
				*/
				for (int iti = 0; iti < tracks[curTrack].itemCount; iti++)
				{
					lineOut = LEVEL4 + STFLD + TABLEchannel;
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + tracks[curTrack].itemSavedIndexes[iti].ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;

					curTrackItem++;
				}

				lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				if (curTrack < trackCount)
				{
					if (tracks[curTrack].loopLevelCount > 0)
					{
						lineOut = LEVEL3 + STFLD + TABLEloopLevels + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
					}

					for (int ll = 0; ll < tracks[curTrack].loopLevelCount; ll++)
					{
						lineOut = LEVEL4 + STFLD + TABLEloopLevel + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						for (int lp = 0; lp < tracks[curTrack].loopLevels[ll].loopsCount; lp++)
						{
							loop loooop = tracks[curTrack].loopLevels[ll].loops[lp];
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

					if (tracks[curTrack].loopLevelCount > 0)
					{
						lineOut = LEVEL3 + FINTBL + TABLEloopLevels + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						curTrack++;
					} // end if loops
				}

				lineOut = LEVEL2 + FINTBL + TABLEtrack + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				curTrack++;
			}
			lineOut = LEVEL1 + FINTBL + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			if (animation != null)
			{
				if (animation.rows > 0)
				{
					lineOut = LEVEL1 + STFLD + TABLEanimation + SPC;
					lineOut += FIELDrow + PLURAL + FIELDEQ + animation.rows.ToString() + ENDQT + SPC;
					lineOut += FIELDcolumns + FIELDEQ + animation.columns.ToString() + ENDQT + SPC;
					lineOut += FIELDimage + FIELDEQ + animation.image + ENDQT + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
					for (int ar = 0; ar < animation.animationRowCount; ar++)
					{
						lineOut = LEVEL2 + STFLD + FIELDrow + SPC + FIELDindex + FIELDEQ + animation.animationRows[ar].rowIndex.ToString() + ENDQT + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						for (int ac = 0; ac < animation.animationRows[ar].animationColumnCount; ac++)
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

			lineOut = FINTBL + TABLEsequence + FINFLD;  // "</sequence>";
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

			if (errState <= 0)
			{
				FileName = newFilename;
			}

			return errState;
		}

		public int WriteClipboardFile(string newFilename)
		{
			//TODO: This procedure is totally untested!!

			int errState = 0;
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
			updatedTrack[] updatedTracks = new updatedTrack[trackCount];

			lineOut = xmlInfo;
			writer.WriteLine(lineOut); lineCount++;
			lineOut = STFLD + TABLEchannelsClipboard + " version=\"1\" name=\"" + Path.GetFileNameWithoutExtension(newFilename) + "\"" + ENDFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write Timing Grid aka cellDemarcation
			lineOut = LEVEL1 + STFLD + TABLEcellDemarcation + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			for (int tm = 0; tm < timingGrids[0].itemCount; tm++)
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
			for (int trk = 0; trk < trackCount; trk++)
			{
				for (int ti = 0; ti < tracks[trk].itemCount; ti++)
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

			if (errState <= 0)
			{
				FileName = newFilename;
			}

			return errState;
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
				int ci = rgbChannels[oi].redChannelIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;

				// Get and write Green Channel
				ci = rgbChannels[oi].grnChannelIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;

				// Get and write Blue Channel
				ci = rgbChannels[oi].bluChannelIndex;
				lineOut = LEVEL2 + STFLD + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				writeEffects(channels[ci]);
				lineOut = LEVEL2 + FINTBL + TABLEchannel + ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
			} // end if rgbChannel

			if (itemType == tableType.channelGroup)
			{
				channelGroup grp = channelGroups[oi];
				for (int itm = 0; itm < grp.itemCount; itm++)
				{
					ParseItemsToClipboard(grp.itemSavedIndexes[itm]);
				}
			} // end if channelGroup
		} // end ParseChannelGroupToClipboard

		public int WriteFileInDisplayOrder(string newFilename)
		{
			int errState = 0;
			lineCount = 0;

			//backupFile(fileName);

			string tmpFile = newFilename + ".tmp";

			writer = new StreamWriter(tmpFile);
			lineOut = ""; // line to be written out, gets modified if necessary
										//int pos1 = LOR.UNDEFINED; // positions of certain key text in the line

			int curTimingGrid = 0;
			int curGridItem = 0;
			int curTrack = 0;
			int curTrackItem = 0;
			int[] newSIs = new int[1];
			int newSI = LOR.UNDEFINED;
			updatedTrack[] updatedTracks = new updatedTrack[trackCount];

			// Clear any 'written' flags from a previous save
			for (int ch=0; ch<channelCount; ch++)
			{
				channels[ch].written = false;
			}
			for (int rch=0; rch< rgbChannelCount; rch++)
			{
				rgbChannels[rch].written = false;
			}
			for (int chg=0; chg<channelGroupCount; chg++)
			{
				channelGroups[chg].written = false;
			}

			// Write the first line of the new sequence, containing the XML info
			lineOut = xmlInfo;
			writer.WriteLine(lineOut); lineCount++;
			createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
			lineOut = sequenceInfo;
			writer.WriteLine(lineOut); lineCount++;

			// Start with Channels (regular, RGB, and Groups)
			lineOut = LEVEL1 + STFLD + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// loop thru tracks
			for (int t = 0; t < trackCount; t++)
			{
				// write out all items for this track
				newSIs = writeTrackItems(t);
				updatedTrack ut = new updatedTrack();
				ut.newSavedIndexes = newSIs;
				updatedTracks[t] = ut;
			}

			// All channels should now be written, close this section
			lineOut = LEVEL1 + FINTBL + TABLEchannel + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TIMING GRIDS
			lineOut = LEVEL1 + STFLD + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			while (curTimingGrid < timingGridCount)
			{
				lineOut = LEVEL2 + STFLD + TABLEtimingGrid;
				lineOut += SPC + FIELDsaveID + FIELDEQ + timingGrids[curTimingGrid].saveID.ToString() + ENDQT;
				if (timingGrids[curTimingGrid].name.Length > 1)
				{
					lineOut += SPC + FIELDname + FIELDEQ + timingGrids[curTimingGrid].name + ENDQT;
				}
				lineOut += SPC + FIELDtype + FIELDEQ + timingName(timingGrids[curTimingGrid].type) + ENDQT;
				if (timingGrids[curTimingGrid].spacing > 1)
				{
					lineOut += SPC + FIELDspacing + FIELDEQ + timingGrids[curTimingGrid].spacing.ToString() + ENDQT;
				}
				if (timingGrids[curTimingGrid].type == timingGridType.fixedGrid)
				{
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				else if (timingGrids[curTimingGrid].type == timingGridType.freeform)
				{
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					for (int tm = 0; tm < timingGrids[curTimingGrid].itemCount; tm++)
					//while (timingGridItems[curGridItem].gridIndex == curTimingGrid)
					{
						lineOut = LEVEL4 + STFLD + TABLEtiming;
						lineOut += SPC + FIELDcentisecond + FIELDEQ + timingGrids[curTimingGrid].timings[tm].ToString() + ENDQT;
						lineOut += ENDFLD;
						writer.WriteLine(lineOut); lineCount++;

						curGridItem++;
					}

					lineOut = LEVEL2 + FINTBL + TABLEtimingGrid + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				curTimingGrid++;
			}
			lineOut = LEVEL1 + FINTBL + TABLEtimingGrid + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// TRACKS
			lineOut = LEVEL1 + STFLD + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;
			// Loop thru tracks
			for (int trk=0; trk< trackCount; trk++)
			{
				curTrack = trk;
				// Write info about track
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

				// Loop thru all items in this track
				for (int iti = 0; iti < tracks[curTrack].itemCount; iti++)
				{
					// Write out the links to the items
					lineOut = LEVEL4 + STFLD + TABLEchannel;
					//lineOut += SPC + FIELDsavedIndex + FIELDEQ + trackItems[curTrackItem].savedIndex.ToString() + ENDQT;
					newSI = updatedTracks[curTrack].newSavedIndexes[iti];
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + newSI.ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;

					curTrackItem++;
				}

				// Close the list of items
				lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				// Write out any LoopLevels in this track
				if (tracks[curTrack].loopLevelCount > 0)
				{
					lineOut = LEVEL3 + STFLD + TABLEloopLevels + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}

				for (int ll = 0; ll< tracks[curTrack].loopLevelCount; ll++)
				{
					lineOut = LEVEL4 + STFLD + TABLEloopLevel + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
					for (int lp =0; lp< tracks[curTrack].loopLevels[ll].loopsCount; lp++)
					{
						loop loooop = tracks[curTrack].loopLevels[ll].loops[lp];
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

				if (tracks[curTrack].loopLevelCount > 0)
				{
					lineOut = LEVEL3 + FINTBL + TABLEloopLevels + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
					curTrack++;
				} // end if loops

				lineOut = LEVEL2 + FINTBL + TABLEtrack + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
			}
			lineOut = LEVEL1 + FINTBL + TABLEtrack + PLURAL + FINFLD;
			writer.WriteLine(lineOut); lineCount++;

			// Write out Animation info, it it exists
			if (animation != null)
			{
				if (animation.rows > 0)
				{
					lineOut = LEVEL1 + STFLD + TABLEanimation + SPC;
					lineOut += FIELDrow + PLURAL + FIELDEQ + animation.rows.ToString() + ENDQT + SPC;
					lineOut += FIELDcolumns + FIELDEQ + animation.columns.ToString() + ENDQT + SPC;
					lineOut += FIELDimage + FIELDEQ + animation.image + ENDQT + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
					for (int ar = 0; ar < animation.animationRowCount; ar++)
					{
						lineOut = LEVEL2 + STFLD + FIELDrow + SPC + FIELDindex + FIELDEQ + animation.animationRows[ar].rowIndex.ToString() + ENDQT + FINFLD;
						writer.WriteLine(lineOut); lineCount++;
						for (int ac=0; ac < animation.animationRows[ar].animationColumnCount; ac++)
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

			// Close the sequence
			lineOut = FINTBL + TABLEsequence + FINFLD; // "</sequence>";
			writer.WriteLine(lineOut); lineCount++;

			Console.WriteLine(lineCount.ToString() + " Out:" + lineOut);
			Console.WriteLine("");

			// We're done writing the file
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

			if (errState <= 0)
			{
				FileName = newFilename;
			}

			return errState;
		} // end WriteClipboardFile

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
			for (int ch=0; ch< channelCount; ch++)
			{
				if (channels[ch].name.CompareTo(channelName) == 0)
				{
					ret = channels[ch];
					ch = channelCount; // force break
				}
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
			for (int rch = 0; rch < rgbChannelCount; rch++)
			{
				if (rgbChannels[rch].name.CompareTo(rgbChannelName) == 0)
				{
					ret = rgbChannels[rch];
					rch = rgbChannelCount; // force break
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
			for (int chg = 0; chg < channelGroupCount; chg++)
			{
				if (channelGroups[chg].name.CompareTo(channelGroupName) == 0)
				{
					ret = channelGroups[chg];
					chg = channelGroupCount; // force break
				}
			}
			return ret;
		}

		public track FindTrack(string trackName)
		{
			track ret = null;
			for (int tr = 0; tr < trackCount; tr++)
			{
				if (tracks[tr].name.CompareTo(trackName) == 0)
				{
					ret = tracks[tr];
					tr = trackCount; // force break
				}
			}
			return ret;
		}

		public timingGrid FindTimingGrid(string timingGridName)
		{
			timingGrid ret = null;
			for (int tg = 0; tg < channelCount; tg++)
			{
				if (timingGrids[tg].name.CompareTo(timingGridName) == 0)
				{
					ret = timingGrids[tg];
					tg = timingGridCount; // force break
				}
			}
			return ret;
		}




		private int[] writeTrackItems(int trackIndex)
		{
			int saveIndex = LOR.UNDEFINED;
			int itemCount = 0;
			int[] newSIs = new int[1];
			track tr = tracks[trackIndex];
			//int curTrackItem = findFirstTrackItem(trackIndex);
			for (int iti = 0; iti < tracks[trackIndex].itemCount; iti++)
			{
				int si = tracks[trackIndex].itemSavedIndexes[iti];
				if (savedIndexes[si].objType == tableType.channel)
				{
					saveIndex = writeChannel(savedIndexes[si].objIndex);
				}
				else if (savedIndexes[si].objType == tableType.rgbChannel)
				{
					saveIndex = writergbChannel(savedIndexes[si].objIndex);
				}
				else if (savedIndexes[si].objType == tableType.channelGroup)
				{
					saveIndex = writeChannelGroup(savedIndexes[si].objIndex);
				}
				Array.Resize(ref newSIs, itemCount + 1);
				newSIs[itemCount] = saveIndex;
				itemCount++;
				//curTrackItem++;
			}
			return newSIs;
		}

		private int writeChannel(int channelIndex)
		{
			if (!channels[channelIndex].written)
			{
				lineOut = LEVEL2 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDname + FIELDEQ + channels[channelIndex].name + ENDQT;
				lineOut += SPC + FIELDcolor + FIELDEQ + channels[channelIndex].color.ToString() + ENDQT;
				lineOut += SPC + FIELDcentiseconds + FIELDEQ + channels[channelIndex].centiseconds.ToString() + ENDQT;
				if (channels[channelIndex].output.deviceType == deviceType.LOR)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(channels[channelIndex].output.deviceType) + ENDQT;
					lineOut += SPC + FIELDunit + FIELDEQ + channels[channelIndex].output.unit.ToString() + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + channels[channelIndex].output.circuit.ToString() + ENDQT;
				}
				else if (channels[channelIndex].output.deviceType == deviceType.DMX)
				{
					lineOut += SPC + FIELDdeviceType + FIELDEQ + deviceName(channels[channelIndex].output.deviceType) + ENDQT;
					lineOut += SPC + FIELDcircuit + FIELDEQ + channels[channelIndex].output.circuit.ToString() + ENDQT;
					lineOut += SPC + FIELDnetwork + FIELDEQ + channels[channelIndex].output.network.ToString() + ENDQT;
				}
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + curSavedIndex.ToString() + ENDQT;
				channels[channelIndex].savedIndex = curSavedIndex;
				curSavedIndex++;

				// Are there any effects for this channel?
				//firstEffect = findFirstEffect(channelIndex);
				if (channels[channelIndex].effectCount > 0)
				{
					// complete channel line with regular '>' then do effects
					lineOut += FINFLD;
					writer.WriteLine(lineOut); lineCount++;

					writeEffects(channels[channelIndex]);

					lineOut = LEVEL2 + FINTBL + TABLEchannel + FINFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				else // NO effects for this channal
				{
					// complete channel line with field end '/>'
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
				}
				channels[channelIndex].written = true;
			}
			return channels[channelIndex].savedIndex;
		}

		private int writergbChannel(int rgbChannelIndex)
		{
			if (!rgbChannels[rgbChannelIndex].written)
			{
				int redSavedIndex = writeChannel(rgbChannels[rgbChannelIndex].redChannelIndex);
				int grnSavedIndex = writeChannel(rgbChannels[rgbChannelIndex].grnChannelIndex);
				int bluSavedIndex = writeChannel(rgbChannels[rgbChannelIndex].bluChannelIndex);

				lineOut = LEVEL2 + STFLD + TABLErgbChannel;
				lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + rgbChannels[rgbChannelIndex].totalCentiseconds.ToString() + ENDQT;
				lineOut += SPC + FIELDname + FIELDEQ + rgbChannels[rgbChannelIndex].name + ENDQT;
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + curSavedIndex.ToString() + ENDQT;
				rgbChannels[rgbChannelIndex].savedIndex = curSavedIndex;
				curSavedIndex++;
				lineOut += FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL3 + STFLD + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL4 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + redSavedIndex.ToString() + ENDQT;
				lineOut += ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL4 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + grnSavedIndex.ToString() + ENDQT;
				lineOut += ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL4 + STFLD + TABLEchannel;
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + bluSavedIndex.ToString() + ENDQT;
				lineOut += ENDFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL3 + FINTBL + TABLEchannel + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL2 + FINTBL + TABLErgbChannel + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				rgbChannels[rgbChannelIndex].written = true;
			}
			return rgbChannels[rgbChannelIndex].savedIndex;
		}

		private int writeChannelGroup(int channelGroupIndex)
		{
			//dbgMsg = "writeChannelGroup(Index=" + channelGroupIndex.ToString() + ") SavedIndex=" + channelGroups[channelGroupIndex].savedIndex + " Name=" + channelGroups[channelGroupIndex].name   ;
			//Debug.Print(dbgMsg);

			int[] newSIs;
			int itemNo = 0;

			if (!channelGroups[channelGroupIndex].written)
			{
				//int curItem = findFirstGroupItem(channelGroupIndex);

				newSIs = writeChannelGroupItems(channelGroupIndex);

				lineOut = LEVEL2 + STFLD + TABLEchannelGroupList;
				lineOut += SPC + FIELDtotalCentiseconds + FIELDEQ + channelGroups[channelGroupIndex].totalCentiseconds.ToString() + ENDQT;
				lineOut += SPC + FIELDname + FIELDEQ + channelGroups[channelGroupIndex].name + ENDQT;
				lineOut += SPC + FIELDsavedIndex + FIELDEQ + curSavedIndex.ToString() + ENDQT;
				channelGroups[channelGroupIndex].savedIndex = curSavedIndex;
				curSavedIndex++;
				lineOut += FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL3 + STFLD + TABLEchannelGroup + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;

				for (int igi = 0; igi < channelGroups[channelGroupIndex].itemCount; igi++)
				{
					lineOut = LEVEL4 + STFLD + TABLEchannelGroup;
					lineOut += SPC + FIELDsavedIndex + FIELDEQ + newSIs[itemNo].ToString() + ENDQT;
					lineOut += ENDFLD;
					writer.WriteLine(lineOut); lineCount++;
					itemNo++;
					//curItem++;
				}

				lineOut = LEVEL3 + FINTBL + TABLEchannelGroup + PLURAL + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
				lineOut = LEVEL2 + FINTBL + TABLEchannelGroupList + FINFLD;
				writer.WriteLine(lineOut); lineCount++;
			}
			return channelGroups[channelGroupIndex].savedIndex;
		}

		private int[] writeChannelGroupItems(int channelGroupIndex)
		{
			int[] newSIs = new int[1];
			int itemCount = 0;
			int si = LOR.UNDEFINED;
			int saveIndex = LOR.UNDEFINED;
			//int curGroupItem = firstGroupItem;

			for (int igi = 0; igi < channelGroups[channelGroupIndex].itemCount; igi++)
			{
				si = channelGroups[channelGroupIndex].itemSavedIndexes[igi];
				if (savedIndexes[si].objType == tableType.channel)
				{
					saveIndex = writeChannel(savedIndexes[si].objIndex);
				}
				else if (savedIndexes[si].objType == tableType.rgbChannel)
				{
					saveIndex = writergbChannel(savedIndexes[si].objIndex);
				}
				else if (savedIndexes[si].objType == tableType.channelGroup)
				{
					saveIndex = writeChannelGroup(savedIndexes[si].objIndex);
				}

				Array.Resize(ref newSIs, itemCount + 1);
				newSIs[itemCount] = saveIndex;
				itemCount++;

				//curGroupItem++;
			}
			return newSIs;
		}

		private void writeEffects(channel thisChannel)
		{
			for (int effectIndex = 0; effectIndex < thisChannel.effectCount; effectIndex++)
			//while (effects[curEffect].channelIndex == channelIndex)
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

			int tot = channelCount + rgbChannelCount + channelGroupCount;
			savedIndex[] siCheck = null;
			Array.Resize(ref siCheck, tot);
			for (int t = 0; t < tot; t++)
			{
				siCheck[t].objIndex = LOR.UNDEFINED;
				siCheck[t].objType = tableType.None;
			}

			for (int ch=0; ch< channelCount; ch++)
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

			for (int rch = 0; rch < rgbChannelCount; rch++)
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

			for (int chg = 0; chg < channelGroupCount; chg++)
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
			for (int ch=0; ch< channelCount; ch++)
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

			if (channelCount > 1)
			{
				Array.Resize(ref names, channelCount);
											 // Loop thru all regular channels
				for (int ch = 0; ch < channelCount; ch++)
				{
					names[ch] = channels[ch].name;
					indexes[ch] = channels[ch].savedIndex;
				} // end channel loop
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channelCount; q++)
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

			if (channelGroupCount > 1)
			{
				Array.Resize(ref names, channelGroupCount);
				// Loop thru all regular channels
				for (int chg = 0; chg < channelGroupCount; chg++)
				{
					names[chg] = channelGroups[chg].name;
					indexes[chg] = channelGroups[chg].savedIndex;
				} // end channelGroup loop
				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channelGroupCount; q++)
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

			if (rgbChannelCount > 1)
			{
				Array.Resize(ref names, rgbChannelCount);
				// Loop thru all regular channels
				for (int rch = 0; rch < rgbChannelCount; rch++)
				{
					names[rch] = rgbChannels[rch].name;
					indexes[rch] = rgbChannels[rch].savedIndex;
				} // end channel loop
					// if at least 2 channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < rgbChannelCount; q++)
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

			if (trackCount > 1)
			{
				Array.Resize(ref names, trackCount);
				// Loop thru all regular channels
				for (int tr = 0; tr < trackCount; tr++)
				{
					names[tr] = tracks[tr].name;
					indexes[tr] = tr;
				} // end channel loop
					// if at least 2 channels deviceType != None
					// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < trackCount; q++)
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

			nameCount = channelCount + rgbChannelCount + channelGroupCount + trackCount;
			if (nameCount > 1)
			{ 
				Array.Resize(ref names, nameCount);
				// Loop thru all regular channels
				for (int ch = 0; ch < channelCount; ch++)
				{
					names[ch] = channels[ch].name;
					indexes[ch] = channels[ch].savedIndex;
				} // end channel loop

				for (int rch = 0; rch < rgbChannelCount; rch++)
				{
					names[rch + channelCount] = rgbChannels[rch].name;
					indexes[rch + channelCount] = rgbChannels[rch].savedIndex;
				} // end RGB channel loop

				for (int chg=0; chg< channelGroupCount; chg++)
				{
					names[chg + channelCount + rgbChannelCount] = channelGroups[chg].name;
					indexes[chg + channelCount + rgbChannelCount] = channelGroups[chg].savedIndex;
				} // end Channel Group Loop

				int trIdx;
				for (int tr=0; tr< trackCount; tr++)
				{
					names[tr + channelCount + rgbChannelCount + channelGroupCount] = tracks[tr].name;
					// use negative numbers for track indexes
					trIdx = LOR.UNDEFINED + (-tr);
					indexes[tr + channelCount + rgbChannelCount + channelGroupCount] = trIdx;
				}

				// Sort the output info (in string format) along with the savedIndexes
				Array.Sort(names, indexes);
				// loop thru sorted arrays
				for (int q = 1; q < channelCount; q++)
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


		/*
		private string fileNameOnly(string fullFileName)
		{
			FileInfo fi = new FileInfo(fullFileName);
			string nameOnly = fi.Name;
			nameOnly = nameOnly.Substring(nameOnly.Length - fi.Extension.Length);
			return nameOnly;
		}
		*/

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

		/*
		private int findFirstTrackItem(int trackIndex)
		{
			int firstItem = LOR.UNDEFINED;
			for (int i = 0; i < trackItemCount; i++)
			{
				if (trackItems[i].trackIndex == trackIndex)
				{
					firstItem = i;
					break;
				}
			}
			return firstItem;
		}
		*/

		/*
	private int findFirstEffect(int channelIndex)
	{
		int firstItem = LOR.UNDEFINED;
		for (int i = 0; i < effectCount; i++)
		{
			if (effects[i].channelIndex == channelIndex)
			{
				firstItem = i;
				break;
			}
		}
		return firstItem;
	}
	*/

		/*
		 * private int findFirstGroupItem(int channelGroupIndex)
		{
			int firstItem = LOR.UNDEFINED;
			for (int i = 0; i < groupItemCount; i++)
			{
				if (channelGroupItems[i].channelGroupListIndex == channelGroupIndex)
				{
					firstItem = i;
					break;
				}
			}
			return firstItem;
		}
		*/
	}
}