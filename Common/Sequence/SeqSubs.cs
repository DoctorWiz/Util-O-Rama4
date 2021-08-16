//using FuzzyString;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
 

namespace LORUtils
{
		public class Music
	{
		public string Artist = "";
		public string Title = "";
		public string Album = "";
		public string File = "";

		public const string FIELDmusicAlbum = " musicAlbum";
		public const string FIELDmusicArtist = " musicArtist";
		public const string FIELDmusicFilename = " musicFilename";
		public const string FIELDmusicTitle = " musicTitle";

		public Music()
		{
			// Default Contructor
		}
		public Music(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			Artist = utils.getKeyWord(lineIn, FIELDmusicArtist);
			Album = utils.getKeyWord(lineIn, FIELDmusicAlbum);
			Title = utils.getKeyWord(lineIn, FIELDmusicTitle);
			File = utils.getKeyWord(lineIn, FIELDmusicFilename);
		}

		public string LineOut()
		{
			string ret = "";
			ret += FIELDmusicAlbum + utils.FIELDEQ + Album + utils.ENDQT;
			ret += FIELDmusicArtist + utils.FIELDEQ + Artist + utils.ENDQT;
			ret += FIELDmusicFilename + utils.FIELDEQ + File + utils.ENDQT;
			ret += FIELDmusicTitle + utils.FIELDEQ + Title + utils.ENDQT;
			return ret;
		}

		public Music Duplicate()
		{
			Music newTune = new Music();
			newTune.Album = Album;
			newTune.Artist = Artist;
			newTune.File = File;
			newTune.Title = Title;
			return newTune;
		}

	}

	public class SavedIndex
	{
		public MemberType MemberType = MemberType.None;
		public int objIndex = utils.UNDEFINED;

		public SavedIndex Duplicate()
		{
			SavedIndex newSI = new SavedIndex();
			newSI.MemberType = MemberType;
			newSI.objIndex = objIndex;
			return newSI;
		}
	}

	public class Output : IComparable<Output>
	{
		public DeviceType deviceType = DeviceType.None;
		public int unit = utils.UNDEFINED;
		public int circuit = utils.UNDEFINED;
		public int network = utils.UNDEFINED;
		private const char DELIM4 = '⬙';

		public const string FIELDdeviceType = " deviceType";

		// for LOR or DMX this is the channel
		public const string FIELDcircuit = " circuit";
		// for LOR, this is the network, for DMX it is the universe
		public const string FIELDnetwork = " network";
		// for LOR, this is the unit, for DMX it's not used
		public const string FIELDunit = " unit";

		public Output()
		{
			// Default Constructor
		}
		public Output(string lineIn)
		{
			Parse(lineIn);
		}

		public int CompareTo(Output other)
		{
			int ret = 0;
			if (deviceType < other.deviceType)
			{
				ret = 1;
			}
			else
			{
				if (deviceType > other.deviceType)
				{
					ret = -1;
				}
				else
				{
					if (deviceType == DeviceType.LOR)
					{
						ret = network.CompareTo(other.network);
						if (ret == 0)
						{
							ret = unit.CompareTo(other.unit);
							if (ret == 0)
							{
								ret = circuit.CompareTo(other.circuit);
							}
						}
					}
					if (deviceType == DeviceType.DMX)
					{
						ret = network.CompareTo(other.network);
						if (ret == 0)
						{
							ret = unit.CompareTo(other.circuit);
						}
					}
				}
			}
			return ret;
		}


		public string SortableComparableString()
		{
			int dt = (int)deviceType;  // Enumerate deviceType
			dt += 1; // Add 1 'cuz None=Undefined=-1 and for comparison's sake, we want 0+ positive numbers

			// Short Version
			//string ret = dt.ToString() + network.ToString("00000");
			//ret += unit.ToString("000") + circuit.ToString("000");
			// Long Version
			string ret = "T" + dt.ToString() + DELIM4 + "N" + network.ToString("00000");
			ret += DELIM4 + "U" + unit.ToString("000") + DELIM4 + "C" + circuit.ToString("000");
			return ret;
						 ;
		}

		public void Parse(string lineIn)
		{
			deviceType = SeqEnums.enumDevice(utils.getKeyWord(lineIn, FIELDdeviceType));
			unit = utils.getKeyValue(lineIn, FIELDunit);
			network = utils.getKeyValue(lineIn, FIELDnetwork);
			circuit = utils.getKeyValue(lineIn, FIELDcircuit);
		}

		public string LineOut()
		{
			string ret = "";
			if (deviceType == DeviceType.LOR)
			{
				ret += FIELDdeviceType + utils.FIELDEQ + SeqEnums.DeviceName(deviceType) + utils.ENDQT;
				ret += FIELDunit + utils.FIELDEQ + unit.ToString() + utils.ENDQT;
				ret += FIELDcircuit + utils.FIELDEQ + circuit.ToString() + utils.ENDQT;
				if (network != utils.UNDEFINED)
				{
					ret += FIELDnetwork + utils.FIELDEQ + network.ToString() + utils.ENDQT;
				}
			}
			else if (deviceType == DeviceType.DMX)
			{
				ret += FIELDdeviceType + utils.FIELDEQ + SeqEnums.DeviceName(deviceType) + utils.ENDQT;
				ret += FIELDcircuit + utils.FIELDEQ + circuit.ToString() + utils.ENDQT;
				ret += FIELDnetwork + utils.FIELDEQ + network.ToString() + utils.ENDQT;
			}
			return ret;
		}

		// An Alias: channel=circuit
		public int channel
		{
			get
			{
				return circuit;
			}
			set
			{
				circuit = value;
				//if (parentSequence != null) parentSequence.MakeDirty();
			}
		}

		// An Alias: universe=network for DMX
		public int universe
		{
			get
			{
				return network;
			}
			set
			{
				network = value;
				//if (parentSequence != null) parentSequence.MakeDirty();
			}
		}

		public Output Duplicate()
		{
			Output oout = new Output();
			oout.circuit = circuit;
			oout.deviceType = deviceType;
			oout.network = network;
			oout.unit = unit;
			return oout;
		}

		public override bool Equals(Object obj)
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType())
				return false;

			Output o = (Output)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}
	}
		
	public class Effect
	{
		public LORUtils.EffectType EffectType = EffectType.None;
		public int startCentisecond = utils.UNDEFINED;
		private int myEndCentisecond = 999999999;
		public int Intensity = utils.UNDEFINED;
		public int startIntensity = utils.UNDEFINED;
		public int endIntensity = utils.UNDEFINED;
		public Channel parent = null;
		public int myIndex = utils.UNDEFINED;

		public const string FIELDintensity = " intensity";
		public const string FIELDstartIntensity = " startIntensity";
		public const string FIELDendIntensity = " endIntensity";


		public Effect()
		{
		}
		public Effect(string lineIn)
		{
			Parse(lineIn);
		}

		public Effect Copy()
		{
			// See Also: Duplicate()
			Effect ret = new Effect();
			ret.EffectType = this.EffectType;
			ret.startCentisecond = startCentisecond;
			ret.endCentisecond = myEndCentisecond;
			ret.Intensity = Intensity;
			ret.startIntensity = startIntensity;
			ret.endIntensity = endIntensity;
			int pcs = parent.ParentSequence.Centiseconds;
			pcs = Math.Max(startCentisecond, pcs);
			pcs = Math.Max(endCentisecond, pcs);
			parent.ParentSequence.Centiseconds = pcs;
			return ret;
		}

		public void Parse(string lineIn)
		{
			this.EffectType = SeqEnums.enumEffectType(utils.getKeyWord(lineIn, utils.FIELDtype));
			startCentisecond = utils.getKeyValue(lineIn, utils.FIELDstartCentisecond);
			endCentisecond = utils.getKeyValue(lineIn, utils.FIELDendCentisecond);
			Intensity = utils.getKeyValue(lineIn, FIELDintensity);
			startIntensity = utils.getKeyValue(lineIn, FIELDstartIntensity);
			endIntensity = utils.getKeyValue(lineIn, FIELDendIntensity);

			if (parent != null)
			{
				if (parent.ParentSequence != null)
				{
					parent.ParentSequence.MakeDirty();
				}
			}
		}

		public string LineOut()
		{
			string ret = "";
			ret = utils.LEVEL3 + utils.STFLD + Sequence4.TABLEeffect;
			ret += utils.FIELDtype + utils.FIELDEQ + SeqEnums.EffectTypeName(this.EffectType).ToLower() + utils.ENDQT;
			ret += utils.FIELDstartCentisecond + utils.FIELDEQ + startCentisecond.ToString() + utils.ENDQT;
			ret += utils.FIELDendCentisecond + utils.FIELDEQ + myEndCentisecond.ToString() + utils.ENDQT;
			if (Intensity > utils.UNDEFINED)
			{
				ret += FIELDintensity + utils.FIELDEQ + Intensity.ToString() + utils.ENDQT;
			}
			if (startIntensity > utils.UNDEFINED)
			{
				ret += FIELDstartIntensity + utils.FIELDEQ + startIntensity.ToString() + utils.ENDQT;
				ret += FIELDendIntensity + utils.FIELDEQ + endIntensity.ToString() + utils.ENDQT;
			}
			ret += utils.ENDFLD;
			return ret;
		}

		public override string ToString()
		{
			string ret = SeqEnums.EffectTypeName(this.EffectType);
			ret += " From " + startCentisecond.ToString();
			ret += " to " + myEndCentisecond.ToString();
			if (Intensity > utils.UNDEFINED)
			{
				ret += " at " + Intensity.ToString();
			}
			if (startIntensity > utils.UNDEFINED)
			{
				ret += " at " + startIntensity.ToString();
			}
			return ret;
		}

		public int endCentisecond
		{
			get
			{
				return myEndCentisecond;
			}
			set
			{
				myEndCentisecond = value;
				if (parent != null)
				{
					if (parent.ParentSequence != null)
					{
						parent.ParentSequence.MakeDirty();
					}
				}

				if (parent != null)
				{
					if (myEndCentisecond > parent.Centiseconds)
					{
						parent.Centiseconds = myEndCentisecond;
					}
				}
			}
		}

		public LORUtils.EffectType EffectTypeEX
		{
			get
			{
				EffectTypeEX levelOut = LORUtils.EffectType.None;
				if (this.EffectType == EffectType.Shimmer) levelOut = LORUtils.EffectType.Shimmer;
				if (this.EffectType == EffectType.Twinkle) levelOut = LORUtils.EffectType.Twinkle;
				if (this.EffectType == EffectType.DMX) levelOut = LORUtils.EffectType.DMX;
				if (this.EffectType == EffectType.Intensity)
				{
					if (endIntensity < 0)
					{
						levelOut = LORUtils.EffectType.Constant;
					}
					else
					{
						if (endIntensity > startIntensity)
						{
							levelOut = LORUtils.EffectType.FadeUp;
						}
						else
						{
							levelOut = LORUtils.EffectType.FadeDown;
						}
					}
				}
				return levelOut;
			}
		}

		public Effect Duplicate()
		{
			// See Also: Copy()
			Effect ef = new Effect();
			ef.EffectType = this.EffectType;
			ef.startCentisecond = startCentisecond;
			ef.myEndCentisecond = myEndCentisecond;
			ef.Intensity = Intensity;
			ef.startIntensity = startIntensity;
			ef.endIntensity = endIntensity;
			ef.parent = parent;  // Will probably get changed
			ef.myIndex = myIndex;
			return ef;
		} // End Duplicate()
	} // End Effect Class

	public class Animation
	{
		//public int sections = 0;
		//public int columns = 0;
		public string image = "";
		public List<AnimationRow> animationRows = new List<AnimationRow>();
		public Sequence4 parentSequence = null;

		public const string FIELDrows = " sections";
		public const string FIELDcolumns = " columns";
		public const string FIELDimage = " image";

		public int sections
		{
			get
			{
				return animationRows.Count;
			}
		}

		public int columns
		{
			get
			{
				int c = 0;
				if (animationRows.Count > 0)
				{
					c = animationRows[0].animationColumns.Count;
				}
				return c;
			}
		}

		public Animation(Sequence4 myParent)
		{
			parentSequence = myParent;
		}
		public Animation(Sequence4 myParent, string lineIn)
		{
			parentSequence = myParent;
			Parse(lineIn);
		}

		public string LineOut()
		{
			string ret = "";

			//if (sections > 0)
			//{
			ret += utils.LEVEL1 + utils.STFLD + Sequence4.TABLEanimation;
			ret += FIELDrows + utils.FIELDEQ + sections.ToString() + utils.ENDQT;
			ret += FIELDcolumns + utils.FIELDEQ + columns.ToString() + utils.ENDQT;
			ret += FIELDimage + utils.FIELDEQ + image + utils.ENDQT;
			if (animationRows.Count > 0)
			{
				ret += utils.FINFLD;
				foreach (AnimationRow aniRow in animationRows)
				{
					ret += utils.CRLF + aniRow.LineOut();
				}
				ret += utils.CRLF + utils.LEVEL1 + utils.FINTBL + Sequence4.TABLEanimation + utils.FINFLD;
			}
			else
			{
				ret += utils.ENDFLD;
			}

			return ret;

		}

		public void Parse(string lineIn)
		{
			//sections = utils.getKeyValue(lineIn, AnimationRow.FIELDrow + utils.PLURAL);
			//columns = utils.getKeyValue(lineIn, FIELDcolumns);
			image = utils.getKeyWord(lineIn, FIELDimage);
			if (parentSequence != null) parentSequence.MakeDirty();

		}

		public AnimationRow AddRow(string lineIn)
		{
			AnimationRow newRow = new AnimationRow(lineIn);
			AddRow(newRow);
			return newRow;
		}

		public int AddRow(AnimationRow newRow)
		{
			animationRows.Add(newRow);
			if (parentSequence != null) parentSequence.MakeDirty();
			return animationRows.Count - 1;
		}

		public Animation Duplicate()
		{
			Animation dupOut = new Animation(null);
			dupOut.image = image;
			foreach(AnimationRow row in animationRows)
			{
				AnimationRow newRow = row.Duplicate();
				dupOut.animationRows.Add(newRow);
			}
			return dupOut;
		}

	} // end Animation class

	public class AnimationRow
	{
		public int rowIndex = utils.UNDEFINED;
		public List<AnimationColumn> animationColumns = new List<AnimationColumn>();
		public const string FIELDrow = "row";
		public const string FIELDindex = "index";

		public AnimationRow()
		{
			// Default Constructor
		}
		public AnimationRow(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			rowIndex = utils.getKeyValue(lineIn, FIELDrow + utils.SPC + FIELDindex);

		}

		public string LineOut()
		{
			string ret = "";

			ret += utils.CRLF + utils.LEVEL2 + utils.STFLD + FIELDrow + utils.SPC + FIELDindex + utils.FIELDEQ + rowIndex.ToString() + utils.ENDQT + utils.FINFLD;
			foreach (AnimationColumn aniCol in animationColumns)
			{
				ret += utils.CRLF + aniCol.LineOut();
			} // end columns loop
			ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + FIELDrow + utils.FINFLD;

			return ret;
		} // end sections loop

		public AnimationColumn AddColumn(string lineIn)
		{
			AnimationColumn newColumn = new AnimationColumn(lineIn);
			AddColumn(newColumn);
			return newColumn;
		}

		public int AddColumn(AnimationColumn animationColumn)
		{
			animationColumns.Add(animationColumn);
			return animationColumns.Count - 1;
		}

		public AnimationRow Duplicate()
		{
			AnimationRow rowOut = new AnimationRow();
			rowOut.rowIndex = rowIndex;
			foreach (AnimationColumn column in animationColumns)
			{
				AnimationColumn newCol = column.Duplicate();
				rowOut.animationColumns.Add(newCol);
			}
			return rowOut;
		}


	} // end AnimationRow class

	public class AnimationColumn
	{
		public int columnIndex = utils.UNDEFINED;
		public int channel = utils.UNDEFINED;
		public const string FIELDcolumnIndex = "column index";

		public AnimationColumn()
		{
			//Default Constructor
		}

		public AnimationColumn(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			columnIndex = utils.getKeyValue(lineIn, FIELDcolumnIndex);
			channel = utils.getKeyValue(lineIn, utils.TABLEchannel);

		}

		public string LineOut()
		{
			string ret = "";

			ret += utils.LEVEL3 + utils.STFLD + FIELDcolumnIndex + utils.FIELDEQ + columnIndex.ToString() + utils.ENDQT + utils.SPC;
			ret += utils.TABLEchannel + utils.FIELDEQ + channel.ToString() + utils.ENDQT + utils.ENDFLD;


			return ret;
		}

		public AnimationColumn Duplicate()
		{
			AnimationColumn colOut = new AnimationColumn();
			colOut.columnIndex = columnIndex;
			colOut.channel = channel;
			return colOut;
		}

	} // end AnimationColumn class

	public class Loop
	{
		public int startCentsecond = utils.UNDEFINED;
		public int endCentisecond = 999999999;
		public int loopCount = utils.UNDEFINED;
		public const string FIELDloopCount = "loopCount";
		public const string FIELDloop = "loop";

		public Loop()
		{
			// Default Constructor
		}
		public Loop(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			startCentsecond = utils.getKeyValue(lineIn, utils.FIELDstartCentisecond);
			endCentisecond = utils.getKeyValue(lineIn, utils.FIELDendCentisecond);
			loopCount = utils.getKeyValue(lineIn, FIELDloopCount);
		}

		public string LineOut()
		{
			string ret = "";
			ret += utils.LEVEL5 + utils.STFLD + FIELDloop + utils.SPC;
			ret += utils.FIELDstartCentisecond + utils.FIELDEQ + startCentsecond.ToString() + utils.ENDQT + utils.SPC;
			ret += utils.FIELDendCentisecond + utils.FIELDEQ + endCentisecond.ToString() + utils.ENDQT;
			ret += utils.LEVEL4 + utils.STFLD + Sequence4.TABLEloopLevel + utils.FINFLD;
			if (loopCount > 0)
			{
				ret += utils.SPC + FIELDloopCount + utils.FIELDEQ + loopCount.ToString() + utils.ENDQT;
			}
			ret += utils.ENDFLD;
			return ret;
		}

		public Loop Duplicate()
		{
			Loop dupLoop = new Loop();


			return dupLoop;
		}
	} // end Loop class

	public class LoopLevel
	{
		public List<Loop> loops = new List<Loop>();
		//public int loopsCount = utils.UNDEFINED;

		public LoopLevel()
		{
			// Default Constructor
		}
		public LoopLevel(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			//loops.Count = utils.getKeyValue(lineIn, Loop.FIELDloopCount);
		}

		public string LineOut()
		{
			string ret = ""; ;

			ret += utils.CRLF + utils.LEVEL4 + utils.STTBL + Sequence4.TABLEloopLevel;
			if (loops.Count > 0)
			{
				ret += utils.SPC + Loop.FIELDloopCount + utils.FIELDEQ + loops.Count.ToString() + utils.ENDQT;
			}
			if (loops.Count > 0)
			{
				foreach (Loop theLoop in loops)
				{
					ret += utils.CRLF + theLoop.LineOut();
				}
				ret += utils.CRLF + utils.LEVEL4 + utils.FINTBL + Sequence4.TABLEloopLevel + utils.ENDTBL;
			}
			else
			{
				ret += utils.ENDFLD;
			}
			return ret;
		}


		public Loop AddLoop(string lineIn)
		{
			Loop newLoop = new Loop(lineIn);
			AddLoop(newLoop);
			return newLoop;
		}

		public int AddLoop(Loop newLoop)
		{
			loops.Add(newLoop);
			return loops.Count - 1;
		}

		public LoopLevel Duplicate()
		{
			LoopLevel dupLevel = new LoopLevel();

			return dupLevel;
		}
	} // end LoopLevel class

	public class Info
	{
		public string filename = "*_UNNAMED_FILE_*";
		public string xmlInfo = ""; // <?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
		public int saveFileVersion = utils.UNDEFINED;
		public string author = "Util-O-Rama";
		public DateTime file_created = DateTime.Now;
		public DateTime file_modified = DateTime.Now;
		public DateTime file_accessed = DateTime.Now;
		// Created At defaults to the file's Created At timestamp, but is overridden by what's in the file information header
		public string createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		// Last defaults to the file's modified At timestamp, but is overridden by any data changes
		public DateTime lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
		public DateTime file_saved = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

		public Music music = new Music();
		public int videoUsage = 0;
		public string animationInfo = "";
		public SequenceType sequenceType = SequenceType.Undefined;
		public Sequence4 ParentSequence = null;


		public const string FIELDsaveFileVersion = " saveFileVersion";
		public const string FIELDchannelConfigFileVersion = " channelConfigFileVersion";
		public const string FIELDauthor = " author";
		public const string FIELDcreatedAt = " createdAt";
		public const string FIELDvideoUsage = " videoUsage";

		public Info(Sequence4 myParent)
		{
			ParentSequence = myParent;
		}
		public Info(Sequence4 myParent, string lineIn)
		{
			ParentSequence = myParent;
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			author = utils.getKeyWord(lineIn, FIELDauthor);
			createdAt = utils.getKeyWord(lineIn, FIELDcreatedAt);
			music = new Music(lineIn);
			bool musical = (music.File.Length > 4);
			saveFileVersion = utils.getKeyValue(lineIn, FIELDsaveFileVersion);
			if (saveFileVersion > 2)
			{
				if (musical)
				{
					sequenceType = SequenceType.Musical;
				}
				else
				{
					sequenceType = SequenceType.Animated;
				}
			}
			else
			{
				saveFileVersion = utils.getKeyValue(lineIn, FIELDchannelConfigFileVersion);
				if (saveFileVersion > 2)
				{
					sequenceType = SequenceType.ChannelConfig;
				}
			}
			videoUsage = utils.getKeyValue(lineIn, FIELDvideoUsage);

		} // end Parse

		public string LineOut()
		{
			string ret = "";

			// Use sequence.sequenceInfo when using WriteSequenceFile (which matches original unmodified file)
			// Use sequence.fileInfo.sequenceInfo when using WriteSequenceInxxxxOrder (which creates a whole new file)
			ret += utils.STFLD + Sequence4.TABLEsequence;
			ret += FIELDsaveFileVersion + utils.FIELDEQ + saveFileVersion.ToString() + utils.ENDQT;
			ret += FIELDauthor + utils.FIELDEQ + author + utils.ENDQT;

			// if Sequence's dirty flag is set, this returns a createdAt that is NOW
			// Whereas if Sequence is 'clean' this returns the createdAt of the original file
			ret += FIELDcreatedAt + utils.FIELDEQ;
			if (ParentSequence.dirty)
			{
				string nowtime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
				ret += nowtime;
			}
			else
			{
				ret += createdAt;
			}
			ret += utils.ENDQT;

			if (sequenceType == SequenceType.Musical)
			{
				ret += music.LineOut();
			}
			ret += FIELDvideoUsage + utils.FIELDEQ + videoUsage.ToString() + utils.ENDQT + utils.ENDTBL;

			return ret;
		}

		public string LastModified
		{
			get
			{ 	
				return lastModified.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
		}

		public string LineOut(string newCreatedAt)
		{
			string ret = xmlInfo + utils.CRLF;
			ret += utils.STFLD + Sequence4.TABLEsequence;
			ret += FIELDsaveFileVersion + utils.FIELDEQ + saveFileVersion.ToString() + utils.ENDQT;
			ret += FIELDauthor + utils.FIELDEQ + author + utils.ENDQT;
			ret += FIELDcreatedAt + utils.FIELDEQ + newCreatedAt + utils.ENDQT;
			if (sequenceType == SequenceType.Musical)
			{
				ret += music.ToString();
			}
			ret += FIELDvideoUsage + utils.FIELDEQ + videoUsage.ToString() + utils.ENDQT + utils.ENDTBL;

			return ret;
		} // end LineOut

		public Info Duplicate()
		{
			Info data = new Info(null);
			data.filename = this.filename;
			data.xmlInfo = xmlInfo;
			data.saveFileVersion = saveFileVersion;
			data.author = author;
			data.file_accessed = file_accessed;
			data.file_created = file_created;
			data.file_modified = file_modified;
			data.music = music.Duplicate();
			data.videoUsage = videoUsage;
			data.animationInfo = animationInfo;
			data.SequenceType = this.SequenceType;
			return data;
		}
	} // end Info class
	
	public class Err
	{
		// =0 means no errors or warnings
		// =1-100 are warnings, but not fatal errors

		// =101+ is a fatal error
		public const int ERR_WrongFileVersion = 114;

		public static string ErrName(int err)
		{
			string ret = "";

			switch (err)
			{
				case 114:
					ret = "Sequence is not version 4";
					break;
			}




			return ret;
		}

		public static string ErrInfo(int err)
		{
			string ret = "";

			switch (err)
			{
				case 114:
					ret = "Only Sequences and related files from Light-O-Rama Sequence Editor version 4.x are supported by this release ";
					ret += "of Util-O-Rama.  Version 5.x will be supported with a future release.";
					break;
			}




			return ret;

		}


	} // end Err class
	
}
