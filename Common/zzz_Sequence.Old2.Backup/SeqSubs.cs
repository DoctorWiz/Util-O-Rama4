//using FuzzyString;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
 

namespace LORUtils4
{
		public class LORMusic4
	{
		public string Artist = "";
		public string Title = "";
		public string Album = "";
		public string File = "";

		public const string FIELDmusicAlbum = " musicAlbum";
		public const string FIELDmusicArtist = " musicArtist";
		public const string FIELDmusicFilename = " musicFilename";
		public const string FIELDmusicTitle = " musicTitle";

		public LORMusic4()
		{
			// Default Contructor
		}
		public LORMusic4(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			Artist = lutils.getKeyWord(lineIn, FIELDmusicArtist);
			Album = lutils.getKeyWord(lineIn, FIELDmusicAlbum);
			Title = lutils.getKeyWord(lineIn, FIELDmusicTitle);
			File = lutils.getKeyWord(lineIn, FIELDmusicFilename);
		}

		public string LineOut()
		{
			string ret = "";
			ret += FIELDmusicAlbum + lutils.FIELDEQ + Album + lutils.ENDQT;
			ret += FIELDmusicArtist + lutils.FIELDEQ + Artist + lutils.ENDQT;
			ret += FIELDmusicFilename + lutils.FIELDEQ + File + lutils.ENDQT;
			ret += FIELDmusicTitle + lutils.FIELDEQ + Title + lutils.ENDQT;
			return ret;
		}

		public LORMusic4 Duplicate()
		{
			LORMusic4 newTune = new LORMusic4();
			newTune.Album = Album;
			newTune.Artist = Artist;
			newTune.File = File;
			newTune.Title = Title;
			return newTune;
		}

	}

	public class SavedIndex
	{
		public LORMemberType4 LORMemberType4 = LORMemberType4.None;
		public int objIndex = lutils.UNDEFINED;

		public SavedIndex Duplicate()
		{
			SavedIndex newSI = new SavedIndex();
			newSI.LORMemberType4 = LORMemberType4;
			newSI.objIndex = objIndex;
			return newSI;
		}
	}

	public class LOROutput4 : IComparable<LOROutput4>
	{
		public LORDeviceType4 deviceType = LORDeviceType4.None;
		public int unit = lutils.UNDEFINED;
		public int circuit = lutils.UNDEFINED;
		public int network = lutils.UNDEFINED;
		private const char DELIM4 = '⬙';

		public const string FIELDdeviceType = " deviceType";

		// for LOR or DMX this is the channel
		public const string FIELDcircuit = " circuit";
		// for LOR, this is the network, for DMX it is the universe
		public const string FIELDnetwork = " network";
		// for LOR, this is the unit, for DMX it's not used
		public const string FIELDunit = " unit";

		public LOROutput4()
		{
			// Default Constructor
		}
		public LOROutput4(string lineIn)
		{
			Parse(lineIn);
		}

		public int CompareTo(LOROutput4 other)
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
					if (deviceType == LORDeviceType4.LOR)
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
					if (deviceType == LORDeviceType4.DMX)
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
			deviceType = LORSeqEnums4.LOREnumDevice4(lutils.getKeyWord(lineIn, FIELDdeviceType));
			unit = lutils.getKeyValue(lineIn, FIELDunit);
			network = lutils.getKeyValue(lineIn, FIELDnetwork);
			circuit = lutils.getKeyValue(lineIn, FIELDcircuit);
		}

		public string LineOut()
		{
			string ret = "";
			if (deviceType == LORDeviceType4.LOR)
			{
				ret += FIELDdeviceType + lutils.FIELDEQ + LORSeqEnums4.LORDeviceName4(deviceType) + lutils.ENDQT;
				ret += FIELDunit + lutils.FIELDEQ + unit.ToString() + lutils.ENDQT;
				ret += FIELDcircuit + lutils.FIELDEQ + circuit.ToString() + lutils.ENDQT;
				if (network != lutils.UNDEFINED)
				{
					ret += FIELDnetwork + lutils.FIELDEQ + network.ToString() + lutils.ENDQT;
				}
			}
			else if (deviceType == LORDeviceType4.DMX)
			{
				ret += FIELDdeviceType + lutils.FIELDEQ + LORSeqEnums4.LORDeviceName4(deviceType) + lutils.ENDQT;
				ret += FIELDcircuit + lutils.FIELDEQ + circuit.ToString() + lutils.ENDQT;
				ret += FIELDnetwork + lutils.FIELDEQ + network.ToString() + lutils.ENDQT;
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

		public LOROutput4 Duplicate()
		{
			LOROutput4 oout = new LOROutput4();
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

			LOROutput4 o = (LOROutput4)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}
	}
		
	public class LOREffect4
	{
		public LORUtils4.LOREffectType4 LOREffectType4 = LOREffectType4.None;
		public int startCentisecond = lutils.UNDEFINED;
		private int myEndCentisecond = 999999999;
		public int Intensity = lutils.UNDEFINED;
		public int startIntensity = lutils.UNDEFINED;
		public int endIntensity = lutils.UNDEFINED;
		public LORChannel4 parent = null;
		public int myIndex = lutils.UNDEFINED;

		public const string FIELDintensity = " intensity";
		public const string FIELDstartIntensity = " startIntensity";
		public const string FIELDendIntensity = " endIntensity";


		public LOREffect4()
		{
		}
		public LOREffect4(string lineIn)
		{
			Parse(lineIn);
		}

		public LOREffect4 Copy()
		{
			// See Also: Duplicate()
			LOREffect4 ret = new LOREffect4();
			ret.LOREffectType4 = this.LOREffectType4;
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
			this.LOREffectType4 = LORSeqEnums4.LOREnumEffectType4(lutils.getKeyWord(lineIn, lutils.FIELDtype));
			startCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDstartCentisecond);
			endCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDendCentisecond);
			Intensity = lutils.getKeyValue(lineIn, FIELDintensity);
			startIntensity = lutils.getKeyValue(lineIn, FIELDstartIntensity);
			endIntensity = lutils.getKeyValue(lineIn, FIELDendIntensity);

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
			ret = lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEeffect;
			ret += lutils.FIELDtype + lutils.FIELDEQ + LORSeqEnums4.LOREffectTypeName(this.LOREffectType4).ToLower() + lutils.ENDQT;
			ret += lutils.FIELDstartCentisecond + lutils.FIELDEQ + startCentisecond.ToString() + lutils.ENDQT;
			ret += lutils.FIELDendCentisecond + lutils.FIELDEQ + myEndCentisecond.ToString() + lutils.ENDQT;
			if (Intensity > lutils.UNDEFINED)
			{
				ret += FIELDintensity + lutils.FIELDEQ + Intensity.ToString() + lutils.ENDQT;
			}
			if (startIntensity > lutils.UNDEFINED)
			{
				ret += FIELDstartIntensity + lutils.FIELDEQ + startIntensity.ToString() + lutils.ENDQT;
				ret += FIELDendIntensity + lutils.FIELDEQ + endIntensity.ToString() + lutils.ENDQT;
			}
			ret += lutils.ENDFLD;
			return ret;
		}

		public override string ToString()
		{
			string ret = LORSeqEnums4.LOREffectTypeName(this.LOREffectType4);
			ret += " From " + startCentisecond.ToString();
			ret += " to " + myEndCentisecond.ToString();
			if (Intensity > lutils.UNDEFINED)
			{
				ret += " at " + Intensity.ToString();
			}
			if (startIntensity > lutils.UNDEFINED)
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

		public LORUtils4.LOREffectType4 EffectTypeEX
		{
			get
			{
				EffectTypeEX levelOut = LORUtils4.LOREffectType4.None;
				if (this.LOREffectType4 == LOREffectType4.Shimmer) levelOut = LORUtils4.LOREffectType4.Shimmer;
				if (this.LOREffectType4 == LOREffectType4.Twinkle) levelOut = LORUtils4.LOREffectType4.Twinkle;
				if (this.LOREffectType4 == LOREffectType4.DMX) levelOut = LORUtils4.LOREffectType4.DMX;
				if (this.LOREffectType4 == LOREffectType4.Intensity)
				{
					if (endIntensity < 0)
					{
						levelOut = LORUtils4.LOREffectType4.Constant;
					}
					else
					{
						if (endIntensity > startIntensity)
						{
							levelOut = LORUtils4.LOREffectType4.FadeUp;
						}
						else
						{
							levelOut = LORUtils4.LOREffectType4.FadeDown;
						}
					}
				}
				return levelOut;
			}
		}

		public LOREffect4 Duplicate()
		{
			// See Also: Copy()
			LOREffect4 ef = new LOREffect4();
			ef.LOREffectType4 = this.LOREffectType4;
			ef.startCentisecond = startCentisecond;
			ef.myEndCentisecond = myEndCentisecond;
			ef.Intensity = Intensity;
			ef.startIntensity = startIntensity;
			ef.endIntensity = endIntensity;
			ef.parent = parent;  // Will probably get changed
			ef.myIndex = myIndex;
			return ef;
		} // End Duplicate()
	} // End LOREffect4 Class

	public class LORAnimation
	{
		//public int sections = 0;
		//public int columns = 0;
		public string image = "";
		public List<LORAnimationRow4> animationRows = new List<LORAnimationRow4>();
		public LORSequence4 parentSequence = null;

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

		public LORAnimation(LORSequence4 myParent)
		{
			parentSequence = myParent;
		}
		public LORAnimation(LORSequence4 myParent, string lineIn)
		{
			parentSequence = myParent;
			Parse(lineIn);
		}

		public string LineOut()
		{
			string ret = "";

			//if (sections > 0)
			//{
			ret += lutils.LEVEL1 + lutils.STFLD + LORSequence4.TABLEanimation;
			ret += FIELDrows + lutils.FIELDEQ + sections.ToString() + lutils.ENDQT;
			ret += FIELDcolumns + lutils.FIELDEQ + columns.ToString() + lutils.ENDQT;
			ret += FIELDimage + lutils.FIELDEQ + image + lutils.ENDQT;
			if (animationRows.Count > 0)
			{
				ret += lutils.FINFLD;
				foreach (LORAnimationRow4 aniRow in animationRows)
				{
					ret += lutils.CRLF + aniRow.LineOut();
				}
				ret += lutils.CRLF + lutils.LEVEL1 + lutils.FINTBL + LORSequence4.TABLEanimation + lutils.FINFLD;
			}
			else
			{
				ret += lutils.ENDFLD;
			}

			return ret;

		}

		public void Parse(string lineIn)
		{
			//sections = lutils.getKeyValue(lineIn, LORAnimationRow4.FIELDrow + lutils.PLURAL);
			//columns = lutils.getKeyValue(lineIn, FIELDcolumns);
			image = lutils.getKeyWord(lineIn, FIELDimage);
			if (parentSequence != null) parentSequence.MakeDirty();

		}

		public LORAnimationRow4 AddRow(string lineIn)
		{
			LORAnimationRow4 newRow = new LORAnimationRow4(lineIn);
			AddRow(newRow);
			return newRow;
		}

		public int AddRow(LORAnimationRow4 newRow)
		{
			animationRows.Add(newRow);
			if (parentSequence != null) parentSequence.MakeDirty();
			return animationRows.Count - 1;
		}

		public LORAnimation Duplicate()
		{
			LORAnimation dupOut = new LORAnimation(null);
			dupOut.image = image;
			foreach(LORAnimationRow4 row in animationRows)
			{
				LORAnimationRow4 newRow = row.Duplicate();
				dupOut.animationRows.Add(newRow);
			}
			return dupOut;
		}

	} // end LORAnimation class

	public class LORAnimationRow4
	{
		public int rowIndex = lutils.UNDEFINED;
		public List<LORAnimationColumn4> animationColumns = new List<LORAnimationColumn4>();
		public const string FIELDrow = "row";
		public const string FIELDindex = "index";

		public LORAnimationRow4()
		{
			// Default Constructor
		}
		public LORAnimationRow4(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			rowIndex = lutils.getKeyValue(lineIn, FIELDrow + lutils.SPC + FIELDindex);

		}

		public string LineOut()
		{
			string ret = "";

			ret += lutils.CRLF + lutils.LEVEL2 + lutils.STFLD + FIELDrow + lutils.SPC + FIELDindex + lutils.FIELDEQ + rowIndex.ToString() + lutils.ENDQT + lutils.FINFLD;
			foreach (LORAnimationColumn4 aniCol in animationColumns)
			{
				ret += lutils.CRLF + aniCol.LineOut();
			} // end columns loop
			ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + FIELDrow + lutils.FINFLD;

			return ret;
		} // end sections loop

		public LORAnimationColumn4 AddColumn(string lineIn)
		{
			LORAnimationColumn4 newColumn = new LORAnimationColumn4(lineIn);
			AddColumn(newColumn);
			return newColumn;
		}

		public int AddColumn(LORAnimationColumn4 animationColumn)
		{
			animationColumns.Add(animationColumn);
			return animationColumns.Count - 1;
		}

		public LORAnimationRow4 Duplicate()
		{
			LORAnimationRow4 rowOut = new LORAnimationRow4();
			rowOut.rowIndex = rowIndex;
			foreach (LORAnimationColumn4 column in animationColumns)
			{
				LORAnimationColumn4 newCol = column.Duplicate();
				rowOut.animationColumns.Add(newCol);
			}
			return rowOut;
		}


	} // end LORAnimationRow4 class

	public class LORAnimationColumn4
	{
		public int columnIndex = lutils.UNDEFINED;
		public int channel = lutils.UNDEFINED;
		public const string FIELDcolumnIndex = "column index";

		public LORAnimationColumn4()
		{
			//Default Constructor
		}

		public LORAnimationColumn4(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			columnIndex = lutils.getKeyValue(lineIn, FIELDcolumnIndex);
			channel = lutils.getKeyValue(lineIn, lutils.TABLEchannel);

		}

		public string LineOut()
		{
			string ret = "";

			ret += lutils.LEVEL3 + lutils.STFLD + FIELDcolumnIndex + lutils.FIELDEQ + columnIndex.ToString() + lutils.ENDQT + lutils.SPC;
			ret += lutils.TABLEchannel + lutils.FIELDEQ + channel.ToString() + lutils.ENDQT + lutils.ENDFLD;


			return ret;
		}

		public LORAnimationColumn4 Duplicate()
		{
			LORAnimationColumn4 colOut = new LORAnimationColumn4();
			colOut.columnIndex = columnIndex;
			colOut.channel = channel;
			return colOut;
		}

	} // end LORAnimationColumn4 class

	public class LORLoop4
	{
		public int startCentsecond = lutils.UNDEFINED;
		public int endCentisecond = 999999999;
		public int loopCount = lutils.UNDEFINED;
		public const string FIELDloopCount = "loopCount";
		public const string FIELDloop = "loop";

		public LORLoop4()
		{
			// Default Constructor
		}
		public LORLoop4(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			startCentsecond = lutils.getKeyValue(lineIn, lutils.FIELDstartCentisecond);
			endCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDendCentisecond);
			loopCount = lutils.getKeyValue(lineIn, FIELDloopCount);
		}

		public string LineOut()
		{
			string ret = "";
			ret += lutils.LEVEL5 + lutils.STFLD + FIELDloop + lutils.SPC;
			ret += lutils.FIELDstartCentisecond + lutils.FIELDEQ + startCentsecond.ToString() + lutils.ENDQT + lutils.SPC;
			ret += lutils.FIELDendCentisecond + lutils.FIELDEQ + endCentisecond.ToString() + lutils.ENDQT;
			ret += lutils.LEVEL4 + lutils.STFLD + LORSequence4.TABLEloopLevel + lutils.FINFLD;
			if (loopCount > 0)
			{
				ret += lutils.SPC + FIELDloopCount + lutils.FIELDEQ + loopCount.ToString() + lutils.ENDQT;
			}
			ret += lutils.ENDFLD;
			return ret;
		}

		public LORLoop4 Duplicate()
		{
			LORLoop4 dupLoop = new LORLoop4();


			return dupLoop;
		}
	} // end LORLoop4 class

	public class LORLoopLevel4
	{
		public List<LORLoop4> loops = new List<LORLoop4>();
		//public int loopsCount = lutils.UNDEFINED;

		public LORLoopLevel4()
		{
			// Default Constructor
		}
		public LORLoopLevel4(string lineIn)
		{
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			//loops.Count = lutils.getKeyValue(lineIn, LORLoop4.FIELDloopCount);
		}

		public string LineOut()
		{
			string ret = ""; ;

			ret += lutils.CRLF + lutils.LEVEL4 + lutils.STTBL + LORSequence4.TABLEloopLevel;
			if (loops.Count > 0)
			{
				ret += lutils.SPC + LORLoop4.FIELDloopCount + lutils.FIELDEQ + loops.Count.ToString() + lutils.ENDQT;
			}
			if (loops.Count > 0)
			{
				foreach (LORLoop4 theLoop in loops)
				{
					ret += lutils.CRLF + theLoop.LineOut();
				}
				ret += lutils.CRLF + lutils.LEVEL4 + lutils.FINTBL + LORSequence4.TABLEloopLevel + lutils.ENDTBL;
			}
			else
			{
				ret += lutils.ENDFLD;
			}
			return ret;
		}


		public LORLoop4 AddLoop(string lineIn)
		{
			LORLoop4 newLoop = new LORLoop4(lineIn);
			AddLoop(newLoop);
			return newLoop;
		}

		public int AddLoop(LORLoop4 newLoop)
		{
			loops.Add(newLoop);
			return loops.Count - 1;
		}

		public LORLoopLevel4 Duplicate()
		{
			LORLoopLevel4 dupLevel = new LORLoopLevel4();

			return dupLevel;
		}
	} // end LORLoopLevel4 class

	public class LORSeqInfo4
	{
		public string filename = "*_UNNAMED_FILE_*";
		public string xmlInfo = ""; // <?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
		public int saveFileVersion = lutils.UNDEFINED;
		public string author = "Util-O-Rama";
		public DateTime file_created = DateTime.Now;
		public DateTime file_modified = DateTime.Now;
		public DateTime file_accessed = DateTime.Now;
		// Created At defaults to the file's Created At timestamp, but is overridden by what's in the file information header
		public string createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		// Last defaults to the file's modified At timestamp, but is overridden by any data changes
		public DateTime lastModified = DateTime.Now; //.ToString("MM/dd/yyyy hh:mm:ss tt");
		public DateTime file_saved = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

		public LORMusic4 music = new LORMusic4();
		public int videoUsage = 0;
		public string animationInfo = "";
		public LORSequenceType4 sequenceType = LORSequenceType4.Undefined;
		public LORSequence4 ParentSequence = null;


		public const string FIELDsaveFileVersion = " saveFileVersion";
		public const string FIELDchannelConfigFileVersion = " channelConfigFileVersion";
		public const string FIELDauthor = " author";
		public const string FIELDcreatedAt = " createdAt";
		public const string FIELDvideoUsage = " videoUsage";

		public LORSeqInfo4(LORSequence4 myParent)
		{
			ParentSequence = myParent;
		}
		public LORSeqInfo4(LORSequence4 myParent, string lineIn)
		{
			ParentSequence = myParent;
			Parse(lineIn);
		}

		public void Parse(string lineIn)
		{
			author = lutils.getKeyWord(lineIn, FIELDauthor);
			createdAt = lutils.getKeyWord(lineIn, FIELDcreatedAt);
			music = new LORMusic4(lineIn);
			bool musical = (music.File.Length > 4);
			saveFileVersion = lutils.getKeyValue(lineIn, FIELDsaveFileVersion);
			if (saveFileVersion > 2)
			{
				if (musical)
				{
					sequenceType = LORSequenceType4.Musical;
				}
				else
				{
					sequenceType = LORSequenceType4.Animated;
				}
			}
			else
			{
				saveFileVersion = lutils.getKeyValue(lineIn, FIELDchannelConfigFileVersion);
				if (saveFileVersion > 2)
				{
					sequenceType = LORSequenceType4.ChannelConfig;
				}
			}
			videoUsage = lutils.getKeyValue(lineIn, FIELDvideoUsage);

		} // end Parse

		public string LineOut()
		{
			string ret = "";

			// Use sequence.sequenceInfo when using WriteSequenceFile (which matches original unmodified file)
			// Use sequence.fileInfo.sequenceInfo when using WriteSequenceInxxxxOrder (which creates a whole new file)
			ret += lutils.STFLD + LORSequence4.TABLEsequence;
			ret += FIELDsaveFileVersion + lutils.FIELDEQ + saveFileVersion.ToString() + lutils.ENDQT;
			ret += FIELDauthor + lutils.FIELDEQ + author + lutils.ENDQT;

			// if Sequence's dirty flag is set, this returns a createdAt that is NOW
			// Whereas if Sequence is 'clean' this returns the createdAt of the original file
			ret += FIELDcreatedAt + lutils.FIELDEQ;
			if (ParentSequence.dirty)
			{
				string nowtime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
				ret += nowtime;
			}
			else
			{
				ret += createdAt;
			}
			ret += lutils.ENDQT;

			if (sequenceType == LORSequenceType4.Musical)
			{
				ret += music.LineOut();
			}
			ret += FIELDvideoUsage + lutils.FIELDEQ + videoUsage.ToString() + lutils.ENDQT + lutils.ENDTBL;

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
			string ret = xmlInfo + lutils.CRLF;
			ret += lutils.STFLD + LORSequence4.TABLEsequence;
			ret += FIELDsaveFileVersion + lutils.FIELDEQ + saveFileVersion.ToString() + lutils.ENDQT;
			ret += FIELDauthor + lutils.FIELDEQ + author + lutils.ENDQT;
			ret += FIELDcreatedAt + lutils.FIELDEQ + newCreatedAt + lutils.ENDQT;
			if (sequenceType == LORSequenceType4.Musical)
			{
				ret += music.ToString();
			}
			ret += FIELDvideoUsage + lutils.FIELDEQ + videoUsage.ToString() + lutils.ENDQT + lutils.ENDTBL;

			return ret;
		} // end LineOut

		public LORSeqInfo4 Duplicate()
		{
			LORSeqInfo4 data = new LORSeqInfo4(null);
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
			data.LORSequenceType4 = this.LORSequenceType4;
			return data;
		}
	} // end LORSeqInfo4 class
	
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
