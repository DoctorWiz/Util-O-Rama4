//using FuzzyString;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Text;
 

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
		// TODO Add properties/methods for these
		protected LORSequence4 parentSequence = null;
		protected bool isDirty = false;

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
			Artist = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDmusicArtist));
			Album = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDmusicAlbum));
			Title = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDmusicTitle));
			File = lutils.getKeyWord(lineIn, FIELDmusicFilename);
		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();
			ret.Append(lutils.SetKey(FIELDmusicAlbum, lutils.XMLifyName(Album)));
			ret.Append(lutils.SetKey(FIELDmusicArtist, lutils.XMLifyName(Artist)));
			ret.Append(lutils.SetKey(FIELDmusicFilename, File));
			ret.Append(lutils.SetKey(FIELDmusicTitle, lutils.XMLifyName(Title)));
			return ret.ToString();
		}

		public LORMusic4 Clone()
		{
			LORMusic4 newTune = new LORMusic4();
			newTune.Album = Album;
			newTune.Artist = Artist;
			newTune.File = File;
			newTune.Title = Title;
			return newTune;
		}

	} // End LORMusic4 Class

	public class LORSavedIndex4
	{
		public LORMemberType4 MemberType = LORMemberType4.None;
		public int objIndex = lutils.UNDEFINED;

		public LORSavedIndex4 Clone()
		{
			LORSavedIndex4 newSI = new LORSavedIndex4();
			newSI.MemberType = MemberType;
			newSI.objIndex = objIndex;
			return newSI;
		} // End SavedIndex class
	}

	public class LOROutput4 : IComparable<LOROutput4>, IEquatable<LOROutput4>
	{
		public LORDeviceType4 deviceType = LORDeviceType4.None;

		// for LOR, this is the unit, for DMX it's not used
		public int unit = lutils.UNDEFINED;

		// for LOR or DMX this is the channel
		public int circuit = lutils.UNDEFINED;

		// for LOR, this is the network, for DMX it is the universe
		public int network = lutils.UNDEFINED;

		public bool isViz = false;

		private const char DELIM4 = '⬙';
		private static readonly string FIELDdeviceType = " deviceType"; // For Sequence Files, all lower case
		private static readonly string FIELDnetwork = " network";
		private static readonly string FIELDcircuit = " circuit";
		private static readonly string FIELDunit = " unit";

		private static readonly string FIELDDeviceType = " DeviceType"; //for Visualizer Files, capitalized
		private static readonly string FIELDNetwork = " Network";
		private static readonly string FIELDChannel = " Channel";
		private static readonly string FIELDController = " Controller";

		// TODO Add properties and methods to access these
		protected LORChannel4 ownerChannel = null;
		protected bool isDirty = false;

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
					if (deviceType == LORDeviceType4.Dasher)
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
		
		// For IEquatible
			public bool Equals(LOROutput4 obj)
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType())
				return false;

			LOROutput4 o = (LOROutput4)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}
		
		public override bool Equals(Object obj)
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType())
				return false;

			LOROutput4 o = (LOROutput4)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}

		public override int GetHashCode()
		{
			var hashCode = 1137;
			hashCode += (int)deviceType;
			hashCode += unit * 0x10;
			hashCode += circuit * 0x1000;
			hashCode += network * 0x2000000;
			return hashCode;
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
			//System.Diagnostics.Debugger.Break();
			//string keywd = lutils.getKeyWord(lineIn, LORVizChannel4.FIELDsubParam);
			//string keywd = lutils.getKeyWord(lineIn, LORChannel4.FIELDcolor);
			int idt = lineIn.IndexOf(FIELDdeviceType);  // Check for "deviceType" (lower case)
																									//if (keywd.Length == 0)
			if (idt > 0)
			{
				//isViz = false;
				string dev = lutils.getKeyWord(lineIn, FIELDdeviceType); // Note: deviceType is NOT capitalized and is a String
				deviceType = LORSeqEnums4.EnumDevice(dev);

				// for LOR, this is the unit, for DMX it's not used
				unit = lutils.getKeyValue(lineIn, FIELDunit);

				// for LOR, this is the network, for DMX it is the UniverseNumber
				network = lutils.getKeyValue(lineIn, FIELDnetwork);

				// for LOR, this is the channel, for DMX this is the DMXAddress
				circuit = lutils.getKeyValue(lineIn, FIELDcircuit);
			}
			else
			{
				idt = lineIn.IndexOf(FIELDDeviceType);  // Check for "DeviceType" (upper case)
				if (idt > 0)
				{
					isViz = true;
					int dt = lutils.getKeyValue(lineIn, FIELDDeviceType); // Note: DeviceType IS captialized and is an Int
					if (dt == 1) deviceType = LORDeviceType4.LOR;
					if (dt == 7) deviceType = LORDeviceType4.DMX;

					// for LOR, this is the unit, for DMX it's not used
					unit = lutils.getKeyValue(lineIn, FIELDController);

					// for LOR, this is the network, for DMX it is the universe
					network = lutils.getKeyValue(lineIn, FIELDNetwork);

					// for LOR or DMX this is the channel
					circuit = lutils.getKeyValue(lineIn, FIELDChannel);

				}
				else
				{
					// if neither "deviceType" or "DeviceType" was found
					// The Device Type is 'None'
					deviceType = LORDeviceType4.None;
				}
			}

		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();
			if (isViz)  // Visualizer
			{
				if (deviceType == LORDeviceType4.LOR)
				{
					ret.Append(lutils.SetKey(FIELDDeviceType, 1));
				}
				else if (deviceType == LORDeviceType4.DMX)
				{
					ret.Append(lutils.SetKey(FIELDDeviceType, 7));
				}
				ret.Append(lutils.SetKey(FIELDNetwork, network));
				ret.Append(lutils.SetKey(FIELDController, unit));
				ret.Append(lutils.SetKey(FIELDChannel, circuit));
			}
			else  // Sequence (not visualizer)
			{
				if ((network < 0) || (circuit < 0))
				{
					// Assume DeviceType = None
					// So-- Don't append this info
					LORDeviceType4 ldt = deviceType;
				}
				else
				{
					if (deviceType == LORDeviceType4.LOR)
					{
						ret.Append(lutils.SetKey(FIELDdeviceType, LORSeqEnums4.DeviceName(deviceType)));
						ret.Append(lutils.SetKey(FIELDunit, unit));
						ret.Append(lutils.SetKey(FIELDcircuit, circuit));
						if (network != lutils.UNDEFINED)
						{
							ret.Append(lutils.SetKey(FIELDnetwork, network));
						}
					}
					else if (deviceType == LORDeviceType4.DMX)
					{
						ret.Append(lutils.SetKey(FIELDdeviceType, LORSeqEnums4.DeviceName(deviceType)));
						ret.Append(lutils.SetKey(FIELDcircuit, circuit));
						ret.Append(lutils.SetKey(FIELDnetwork, network));
					}
					else if (deviceType == LORDeviceType4.Dasher)
					{
						ret.Append(lutils.SetKey(FIELDdeviceType, LORSeqEnums4.DeviceName(deviceType)));
						ret.Append(lutils.SetKey(FIELDunit, unit));
						ret.Append(lutils.SetKey(FIELDcircuit, circuit));
					}
					else if (deviceType == LORDeviceType4.None)
					{
						// Don't Append Anything!
					}
				}
			}
			return ret.ToString();
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
		public int UniverseNumber
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

		public int DMXAddress
		{ get { return circuit; } set { circuit = value; } }

		public string networkName
		{
			get
			{
				string n = "";
				if (network < 0)
				{
					n= "Regular";
				}
				else
				{
					n= "Aux" + (char)(64 + network);
				}
				return n;
			}
		}

		public LOROutput4 Clone()
		{
			LOROutput4 oout = new LOROutput4();
			oout.circuit = circuit;
			oout.deviceType = deviceType;
			oout.network = network;
			oout.unit = unit;
			return oout;
		}


	} // End LOROutput4 Class
		
	public class LOREffect4
	{
		public LORUtils4.LOREffectType4 EffectType = LOREffectType4.None;
		private int myStartCentisecond = lutils.UNDEFINED;
		private int myEndCentisecond = 360001;
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

		public LOREffect4(LOREffectType4 effectType, int startTime, int endTime)
		{
			if (startTime >= endTime)
			{
				// Raise Error to Debugger
				System.Diagnostics.Debugger.Break();
			}
			this.EffectType = effectType;
			myStartCentisecond = startTime;
			myEndCentisecond = endTime;
		}
		public LOREffect4(LOREffectType4 effectType, int startTime, int endTime, int intensity)
		{
			if (startTime >= endTime)
			{
				// Raise Error to Debugger
				System.Diagnostics.Debugger.Break();
			}
			this.EffectType = effectType;
			myStartCentisecond = startTime;
			myEndCentisecond = endTime;
			Intensity = intensity;
		}

		public LOREffect4(LOREffectType4 effectType, int startTime, int endTime, int start_Intensity, int end_Intensity)
		{
			if (startTime >= endTime)
			{
				// Raise Error to Debugger
				System.Diagnostics.Debugger.Break();
			}
			this.EffectType = effectType;
			myStartCentisecond = startTime;
			myEndCentisecond = endTime;
			startIntensity = start_Intensity;
			endIntensity = end_Intensity;
		}


		public void Parse(string lineIn)
		{
			this.EffectType = LORSeqEnums4.EnumEffectType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
			myStartCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDstartCentisecond);
			myEndCentisecond = lutils.getKeyValue(lineIn, lutils.FIELDendCentisecond);
			Intensity = lutils.getKeyValue(lineIn, FIELDintensity);
			startIntensity = lutils.getKeyValue(lineIn, FIELDstartIntensity);
			endIntensity = lutils.getKeyValue(lineIn, FIELDendIntensity);

			if (parent != null)
			{
				if (parent.Parent != null)
				{
					parent.Parent.MakeDirty(true);
				}
			}
		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.StartTable(LORSequence4.TABLEeffect, 3));

			ret.Append(lutils.SetKey(lutils.FIELDtype, LORSeqEnums4.EffectName(this.EffectType).ToLower()));
			ret.Append(lutils.SetKey(lutils.FIELDstartCentisecond, startCentisecond));
			ret.Append(lutils.SetKey(lutils.FIELDendCentisecond, myEndCentisecond));
			// For steady intensity (non-ramp/fade)
			if (Intensity > lutils.UNDEFINED)
			{
				ret.Append(lutils.SetKey(FIELDintensity, Intensity));
			}
			// For ramps/fades
			if (startIntensity > lutils.UNDEFINED)
			{
				ret.Append(lutils.SetKey(FIELDstartIntensity, startIntensity));
				ret.Append(lutils.SetKey(FIELDendIntensity, endIntensity));
			}
			ret.Append(lutils.ENDFLD);
			return ret.ToString();
		}

		public override string ToString()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(LORSeqEnums4.EffectTypeName(this.EffectTypeEX));
			ret.Append(" From ");
			ret.Append(startCentisecond);
			ret.Append(" to ");
			ret.Append(myEndCentisecond);
			if (Intensity > lutils.UNDEFINED)
			{
				ret.Append(" at ");
				ret.Append(Intensity);
			}
			if (startIntensity > lutils.UNDEFINED)
			{
				ret.Append(" at ");
				ret.Append(startIntensity);
			}
			return ret.ToString();
		}

		public int startCentisecond
		{
			get
			{
				return myStartCentisecond;
			}
			set
			{
				myStartCentisecond = value;
				if (parent != null)
				{
					if (parent.Parent != null)
					{
						parent.Parent.MakeDirty(true);
					}
				}
				if (parent != null)
				{
					if (myStartCentisecond >= parent.Centiseconds)
					{
						// Raise Error
						parent.Centiseconds = myStartCentisecond + 1;
					}
				}
			}
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
					if (parent.Parent != null)
					{
						parent.Parent.MakeDirty(true);
					}
				}
				if (parent != null)
				{
					if (myEndCentisecond > parent.Centiseconds)
					{
						// Raise Error
						parent.Centiseconds = myEndCentisecond;
					}
				}
			}
		}

		public int Direction
		{

			get
			{
				int ret = 0;
				if (startIntensity < endIntensity) ret = 1;
				if (startIntensity > endIntensity) ret = -1;
				return ret;
			}
		}

		public bool Steady
		{
			get
			{
				if (Direction == 0) return true; else return false;
			}
		}	


		public LOREffectType4 EffectTypeEX
		{
			get
			{
				LOREffectType4 levelOut = LOREffectType4.None;
				if (this.EffectType == LOREffectType4.Shimmer) levelOut = LOREffectType4.Shimmer;
				if (this.EffectType == LOREffectType4.Twinkle) levelOut = LOREffectType4.Twinkle;
				if (this.EffectType == LOREffectType4.DMX) levelOut = LOREffectType4.DMX;
				if (this.EffectType == LOREffectType4.Intensity)
				{
					if (endIntensity < 0)
					{
						levelOut = LOREffectType4.Constant;
					}
					else
					{
						if (endIntensity > startIntensity)
						{
							levelOut = LOREffectType4.FadeUp;
						}
						else
						{
							levelOut = LOREffectType4.FadeDown;
						}
					}
				}
				return levelOut;
			}
		}


		public LOREffect4 Clone()
		{
			// See Also: Duplicate()
			LOREffect4 ret = new LOREffect4(this.EffectType, this.startCentisecond,this.endCentisecond);
			ret.Intensity = Intensity;
			ret.startIntensity = startIntensity;
			ret.endIntensity = endIntensity;
			ret.parent = parent;
			//int pcs = parent.Parent.Centiseconds;
			//pcs = Math.Max(startCentisecond, pcs);
			//pcs = Math.Max(endCentisecond, pcs);
			//parent.Parent.Centiseconds = pcs;
			return ret;
		}


	} // End LOREffect4 Class

	public class LORAnimation4
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

		public LORAnimation4(LORSequence4 myParent)
		{
			parentSequence = myParent;
		}
		public LORAnimation4(LORSequence4 myParent, string lineIn)
		{
			parentSequence = myParent;
			Parse(lineIn);
		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			//if (sections > 0)
			//{
			ret.Append(lutils.StartTable(LORSequence4.TABLEanimation, 1));

			ret.Append(lutils.SetKey(FIELDrows, sections));
			ret.Append(lutils.SetKey(FIELDcolumns, columns));
			ret.Append(lutils.SetKey(FIELDimage, image));
			if (animationRows.Count > 0)
			{
				ret.Append(lutils.FINFLD);
				foreach (LORAnimationRow4 aniRow in animationRows)
				{
					ret.Append(lutils.CRLF);
					ret.Append(aniRow.LineOut());
				}
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL1);
				ret.Append(lutils.FINTBL);
				ret.Append(LORSequence4.TABLEanimation);
				ret.Append(lutils.FINFLD);
			}
			else
			{
				ret.Append(lutils.ENDFLD);
			}

			return ret.ToString();

		}

		public void Parse(string lineIn)
		{
			//sections = lutils.getKeyValue(lineIn, LORAnimationRow4.FIELDrow + lutils.PLURAL);
			//columns = lutils.getKeyValue(lineIn, FIELDcolumns);
			image = lutils.getKeyWord(lineIn, FIELDimage);
			if (parentSequence != null) parentSequence.MakeDirty(true);

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
			if (parentSequence != null) parentSequence.MakeDirty(true);
			return animationRows.Count - 1;
		}

		public LORAnimation4 Clone()
		{
			LORAnimation4 dupOut = new LORAnimation4(null);
			dupOut.image = image;
			foreach(LORAnimationRow4 row in animationRows)
			{
				LORAnimationRow4 newRow = row.Clone();
				dupOut.animationRows.Add(newRow);
			}
			return dupOut;
		}
	} // end LORAnimation4 class

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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(FIELDrow);
			ret.Append(lutils.SPC);
			ret.Append(lutils.SetKey(FIELDindex, rowIndex));
			ret.Append(lutils.FINFLD);
			foreach (LORAnimationColumn4 aniCol in animationColumns)
			{
				ret.Append(lutils.CRLF);
				ret.Append(aniCol.LineOut());
			} // end columns loop
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(FIELDrow);
			ret.Append(lutils.FINFLD);

			return ret.ToString();
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

		public LORAnimationRow4 Clone()
		{
			LORAnimationRow4 rowOut = new LORAnimationRow4();
			rowOut.rowIndex = rowIndex;
			foreach (LORAnimationColumn4 column in animationColumns)
			{
				LORAnimationColumn4 newCol = column.Clone();
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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.STFLD);
			ret.Append(lutils.SetKey(FIELDcolumnIndex, columnIndex));
			ret.Append(lutils.SPC);

			ret.Append(lutils.SetKey(lutils.TABLEchannel, channel));
			ret.Append(lutils.ENDFLD);


			return ret.ToString();
		}

		public LORAnimationColumn4 Clone()
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
		public int endCentisecond = 360001;
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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.StartTable(FIELDloop, 5));
			ret.Append(lutils.SPC);

			ret.Append(lutils.SetKey(lutils.FIELDstartCentisecond, startCentsecond));
			ret.Append(lutils.SPC);
			ret.Append(lutils.SetKey(lutils.FIELDendCentisecond, endCentisecond));

			ret.Append(lutils.StartTable(LORSequence4.TABLEloopLevel, 4));
			ret.Append(lutils.FINFLD);
			if (loopCount > 0)
			{
				ret.Append(lutils.SPC);
				ret.Append(lutils.SetKey(FIELDloopCount, loopCount));
			}
			ret.Append(lutils.ENDFLD);

			return ret.ToString();
		}

		public LORLoop4 Clone()
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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.CRLF);
			ret.Append(lutils.StartTable(LORSequence4.TABLEloopLevel, 4));
			if (loops.Count > 0)
			{
				ret.Append(lutils.SPC);
				ret.Append(lutils.SetKey(LORLoop4.FIELDloopCount, loops.Count));
			}
			if (loops.Count > 0)
			{
				foreach (LORLoop4 theLoop in loops)
				{
					ret.Append(lutils.CRLF);
					ret.Append(theLoop.LineOut());
				}
				ret.Append(lutils.CRLF);
				ret.Append(lutils.EndTable(LORSequence4.TABLEloopLevel, 4));
			}
			else
			{
				ret.Append(lutils.ENDFLD);
			}
			return ret.ToString();
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

		public LORLoopLevel4 Clone()
		{
			LORLoopLevel4 dupLevel = new LORLoopLevel4();

			return dupLevel;
		}
	} // end LORLoopLevel4 class

	public class ErrInfo
	{
		public int fileLine = lutils.UNDEFINED;
		public int linePos = lutils.UNDEFINED;
		public int codeLine = lutils.UNDEFINED;
		public string errName = "";
		public string errMsg = "";
		public string lineIn = "";
	}


	public class LORSeqInfo4
	{
		public string filename = "*_UNNAMED_FILE_*";
		public string xmlInfo = lutils.XMLINFO;
		public string infoLine = ""; // only populated when the info line is considered invalid, and saved only for debugging purposes.
		public int saveFileVersion = 14; // lutils.UNDEFINED;
		public string author = "Util-O-Rama";
		//public string modifiedBy = "Util-O-Rama / " + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		public string modifiedBy = "";
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
		// TODO Change this to protected and add properties / methods for it
		// Note: May be a Sequence or a Vizualization!
		public iLORMember4 Parent = null;
		protected bool isDirty = false;


		//public LORVisualization4  ParentVisualization = null;
		public ErrInfo LastError = new ErrInfo();

		public const string FIELDsaveFileVersion = " saveFileVersion";
		public const string FIELDlvizSaveFileVersion = " lvizSaveFileVersion";
		public const string FIELDchannelConfigFileVersion = " channelConfigFileVersion";
		public const string FIELDauthor = " author";
		public const string FIELDcreatedAt = " createdAt";
		public const string FIELDmodifiedBy = " modifiedBy";
		public const string FIELDvideoUsage = " videoUsage";

		public LORSeqInfo4(LORSequence4 myParent)
		{
			Parent = myParent;
		}
		public LORSeqInfo4(LORSequence4 myParent, string lineIn)
		{
			Parent = myParent;
			Parse(lineIn);
		}

		//public LORSeqInfo4(LORVisualization4 myParent, string lineIn)
		//{
		//	ParentVisualization = myParent;
		//	Parse(lineIn);
		//}

		public void Parse(string lineIn)
		{
			author = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDauthor));
			modifiedBy = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDmodifiedBy));
			createdAt = lutils.getKeyWord(lineIn, FIELDcreatedAt);
			music = new LORMusic4(lineIn);
			bool musical = (music.File.Length > 4);
			saveFileVersion = lutils.getKeyValue(lineIn, FIELDsaveFileVersion);
			if (saveFileVersion > 0)
			{
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
					videoUsage = lutils.getKeyValue(lineIn, FIELDvideoUsage);
				}
				else
				{
					saveFileVersion = lutils.getKeyValue(lineIn, FIELDchannelConfigFileVersion);
					if (saveFileVersion > 2)
					{
						sequenceType = LORSequenceType4.ChannelConfig;
					}
				}
			}
			else // First fetch returned undefined
			{ 
				string sfv = lutils.getKeyWord(lineIn, FIELDlvizSaveFileVersion);
				if (sfv.Length > 0)
				{
					saveFileVersion = Int32.Parse(sfv.Substring(0,1));
					sequenceType = LORSequenceType4.Visualizer;
  			}
			}

		} // end Parse

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			// Use sequence.sequenceInfo when using WriteSequenceFile (which matches original unmodified file)
			// Use sequence.fileInfo.sequenceInfo when using WriteSequenceInxxxxOrder (which creates a whole new file)
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEsequence);

			ret.Append(FIELDsaveFileVersion);
			ret.Append(lutils.FIELDEQ);
			ret.Append(saveFileVersion);
			ret.Append(lutils.ENDQT);

			ret.Append(FIELDauthor);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(author));
			ret.Append(lutils.ENDQT);

			// if Sequence's dirty flag is set, this returns a createdAt that is NOW
			// Whereas if Sequence is 'clean' this returns the createdAt of the original file
			ret.Append(FIELDcreatedAt);
			ret.Append(lutils.FIELDEQ);
			//if (Parent.dirty)
			//{
			//	string nowtime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
			//	ret += nowtime;
			//}
			//else
			//{
			//!NO! Always return THE ORIGINAL date/time of creation!
			ret.Append(createdAt);
			ret.Append(lutils.ENDQT);
			//}

			// if Sequence's dirty flag is set, this returns a modifiedBy that is NOW
			// Whereas if Sequence is 'clean' this returns the createdAt of the original file
			//if (modifiedBy.Length < 1)
			//{
			if (Parent.Dirty)
			{
				if (author.Length < 2)
				{
					author = lutils.DefaultAuthor;
				}
				modifiedBy = author + " / Util-O-Rama";
			}
			//}
			ret.Append(FIELDmodifiedBy);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(modifiedBy));
			ret.Append(lutils.ENDQT);

			if (sequenceType == LORSequenceType4.Musical)
			{
				ret.Append(music.LineOut());
			}
			ret.Append(FIELDvideoUsage);
			ret.Append(lutils.FIELDEQ);
			ret.Append(videoUsage);
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.ENDTBL);

			return ret.ToString();
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
			StringBuilder ret = new StringBuilder();

			ret.Append(xmlInfo);
			ret.Append(lutils.CRLF);

			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEsequence);

			ret.Append(FIELDsaveFileVersion);
			ret.Append(lutils.FIELDEQ);
			ret.Append(saveFileVersion.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(FIELDauthor);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(author));
			ret.Append(lutils.ENDQT);

			ret.Append(FIELDcreatedAt);
			ret.Append(lutils.FIELDEQ);
			ret.Append(newCreatedAt);
			ret.Append(lutils.ENDQT);
			if (sequenceType == LORSequenceType4.Musical)
			{
				ret.Append(music.ToString());
			}
			ret.Append(FIELDvideoUsage);
			ret.Append(lutils.FIELDEQ);
			ret.Append(videoUsage.ToString());
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.ENDTBL);

			return ret.ToString();
		} // end LineOut

		public LORSeqInfo4 Clone()
		{
			LORSeqInfo4 data = new LORSeqInfo4(null);
			data.filename = this.filename;
			data.xmlInfo = xmlInfo;
			data.saveFileVersion = saveFileVersion;
			data.author = author;
			data.file_accessed = file_accessed;
			data.file_created = file_created;
			data.file_modified = file_modified;
			data.music = music.Clone();
			data.videoUsage = videoUsage;
			data.animationInfo = animationInfo;
			data.sequenceType = this.sequenceType;
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
