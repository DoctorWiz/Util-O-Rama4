//using FuzzyString;
using System;
using System.Collections.Generic;

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

	}

	public class SavedIndex
	{
		public TableType TableType = TableType.None;
		public int objIndex = utils.UNDEFINED;
	}

	public interface SeqPart : IComparable<SeqPart>
	{
		string Name
		{
			get;
		}
		void ChangeName(string newName);
		int Centiseconds
		{
			get;
		}
		int Index
		{
			get;
		}
		void SetIndex(int theIndex);
		int SavedIndex
		{
			get;
		}
		void SetSavedIndex(int theSavedIndex);
		int AltSavedIndex
		{
			get;
			set;
		}
		Sequence4 ParentSequence
		{
			get;
		}
		void SetParentSeq(Sequence4 newParentSeq);
		//List <SeqPart> parents
		//{
		//	get;
		//}
		bool Selected
		{
			get;
			set;
		}
		TableType TableType
		{
			get;
		}
		int CompareTo(SeqPart otherObj);
		string LineOut();
		string ToString();
		void Parse(string lineIn);
		bool Written
		{
			get;
		}

	}


public class Channel : SeqPart, IComparable<SeqPart>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public Int32 color = 0;
		public Output output = new Output();
		public RGBchild rgbChild = RGBchild.None;
		public RGBchannel rgbParent = null;
		public List<Effect> effects = new List<Effect>();

		public const string FIELDcolor = " color";

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
			if (parentSequence != null) parentSequence.MakeDirty();

		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					if (parentSequence != null) parentSequence.MakeDirty();

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
					if (rgbParent != null)
					{
						if (myCentiseconds > rgbParent.Centiseconds)
						{
							rgbParent.Centiseconds = myCentiseconds;
						}
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
		}

		public int SavedIndex
		{
			get
			{
				return mySavedIndex;
			}
		}
		
		public void SetSavedIndex(int theSavedIndex)
		{
			mySavedIndex = theSavedIndex;
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			parentSequence = newParentSeq;
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public TableType TableType
		{
			get
			{
				return TableType.Channel;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (parentSequence.Children.sortMode == PartsCollection.SORTbyOutput)
						{
							Channel ch = (Channel)other;
							output.ToString().CompareTo(ch.output.ToString());
						}
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			//Sequence4 ParentSequence = ID.ParentSequence;
			myName = utils.cleanName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			color = utils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
			output.Parse(lineIn);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public string LineOut(bool noEffects)
		{
			string ret = "";
			ret = utils.LEVEL2 + utils.STFLD + utils.TABLEchannel;
			ret += utils.FIELDname + utils.FIELDEQ + utils.dirtyName(myName) + utils.ENDQT;
			ret += FIELDcolor + utils.FIELDEQ + color.ToString() + utils.ENDQT;
			ret += utils.FIELDcentiseconds + utils.FIELDEQ + myCentiseconds.ToString() + utils.ENDQT;
			ret += output.LineOut();
			ret += utils.FIELDsavedIndex + utils.FIELDEQ + myAltSavedIndex.ToString() + utils.ENDQT;

			// Are there any effects for this channel?
			if (!noEffects && (effects.Count > 0))
			{
				// complete channel line with regular '>' then do effects
				ret += utils.FINFLD;
				foreach (Effect thisEffect in effects)
				{
					ret += utils.CRLF + thisEffect.LineOut();
				}
				ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + utils.TABLEchannel + utils.FINFLD;
			}
			else // NO effects for this channal
			{
				// complete channel line with field end '/>'
				ret += utils.ENDFLD;
			}

			return ret;
		}


		public Channel(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public Channel(string lineIn)
		{
			if (lineIn.Length > 5)
			{
				Parse(lineIn);
			}
		}


		public Channel Copy(bool withEffects)
		{
			//int nextSI = ID.ParentSequence.Children.highestSavedIndex + 1;
			Channel ret = new Channel(myName, utils.UNDEFINED);
			ret.color = color;
			ret.output = output;
			ret.rgbChild = rgbChild;
			ret.rgbParent = rgbParent;
			ret.SetParentSeq(ParentSequence);
			List<Effect> newEffects = new List<Effect>();
			if (withEffects)
			{
				foreach (Effect thisEffect in effects)
				{
					newEffects.Add(thisEffect.Copy());
				}
				ret.effects = newEffects;
			}
			return ret;
		}

		public void AddEffect(Effect theEffect)
		{
			theEffect.parent = this;
			theEffect.myIndex = effects.Count;
			effects.Add(theEffect);
			if (theEffect.endCentisecond > myCentiseconds)
			{
				Centiseconds = theEffect.endCentisecond;
			}
			if (parentSequence != null) parentSequence.MakeDirty();
			//ID.ParentSequence.dirty = true;
		}

		public void AddEffect(string lineIn)
		{
			Effect theEffect = new Effect(lineIn);
			AddEffect(theEffect);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int CopyEffects(List<Effect> effectList, bool Merge)
		{
			Effect newEffect = null;
			if (!Merge)
			{
				//! Note: clears any pre-existing effects!
				effects.Clear();
			}
			foreach (Effect thisEffect in effectList)
			{
				newEffect = thisEffect.Copy();
				newEffect.parent = this;
				newEffect.myIndex = effects.Count;
				effects.Add(newEffect);
				ParentSequence.dirty = true;
			}
			if (Merge)
			{
				effects.Sort();
			}
			// Return how many effects were copied
			if (parentSequence != null) parentSequence.MakeDirty();
			return effects.Count;
		}

		//TODO: add RemoveEffect procedure
		//TODO: add SortEffects procedure (by startCentisecond)
	} // end channel

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
					ret += FIELDcircuit + utils.FIELDEQ + network.ToString() + utils.ENDQT;
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




		public override bool Equals(Object obj)
		{
			// Check for null values and compare run-time types.
			if (obj == null || GetType() != obj.GetType())
				return false;

			Output o = (Output)obj;
			return ((deviceType == o.deviceType) && (unit == o.unit) && (circuit == o.circuit) && (network == o.network));
		}
	}

	/*
	public class SavedIndex
	{
		// savedIndexes are like array indexes or database record numbers.
		// They start at 0 (zero) and range up to the count-1.
		// -1 (minus one) is assigned to new Channels/groups at creation, but is not a
		// valid SavedIndex.
		// A new SavedIndex will be assigned to Channels, RGB Channels, or channel groups
		// when added to a sequence if they don't already have one.
		// (existing saved index will not be overwritten.)
		// savedIndexes may not be duplicated and/or skipped
		// Every channel, RGB channel, and channel group must have a SavedIndex.
		// The SavedIndex has no affect on the order items are displayed.
		// savedIDs used by TimingGrids are NOT the same as savedIndexes.
		public TableType PartType = TableType.None;
		public int objIndex = utils.UNDEFINED;
		public object objRef = null;
		public int AltSavedIndex = utils.UNDEFINED;
		public List<int> parents = new List<int>();

		public SavedIndex altCopy()
		{
			SavedIndex ret = new SavedIndex();
			ret.objIndex = objIndex;
			ret.PartType = PartType;
			ret.objRef = objRef;
			ret.parents = parents;
			return ret;
		}
	}
	*/ 

	//internal class rgbChannel
	public class RGBchannel : SeqPart, IComparable<SeqPart>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public Channel redChannel = null;
		public Channel grnChannel = null;
		public Channel bluChannel = null;

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					if (parentSequence != null) parentSequence.MakeDirty();

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int newSavedIndex)
		{
			myIndex = newSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int SavedIndex
		{
			get
			{
				return mySavedIndex;
			}
		}

		public void SetSavedIndex(int theSavedIndex)
		{
			mySavedIndex = theSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			parentSequence = newParentSeq;
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public TableType TableType
		{
			get
			{
				return TableType.RGBchannel;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false, false, TableType.All);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.cleanName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}



		public RGBchannel(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public RGBchannel(string lineIn)
		{
			if (lineIn.Length > 4)
			{
				Parse(lineIn);
			}
		}


		public string LineOut(bool selectedOnly, bool noEffects, TableType itemTypes)
		{
			string ret = "";

			int redSavedIndex = utils.UNDEFINED;
			int grnSavedIndex = utils.UNDEFINED;
			int bluSavedIndex = utils.UNDEFINED;

			int AltSavedIndex = utils.UNDEFINED;

			if ((itemTypes == TableType.Items) || (itemTypes == TableType.Channel))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent RGBchannel is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == TableType.Items) || (itemTypes == TableType.RGBchannel))
			{
				ret = utils.LEVEL2 + utils.STFLD + Sequence4.TABLErgbChannel;
				ret += utils.FIELDtotalCentiseconds + utils.FIELDEQ + myCentiseconds.ToString() + utils.ENDQT;
				ret += utils.FIELDname + utils.FIELDEQ + utils.dirtyName(myName) + utils.ENDQT;
				ret += utils.FIELDsavedIndex + utils.FIELDEQ + myAltSavedIndex.ToString() + utils.ENDQT;
				ret += utils.FINFLD;

				// Start SubChannels
				ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;

				// RED subchannel
				AltSavedIndex = redChannel.AltSavedIndex;
				ret += utils.CRLF + utils.LEVEL4 + utils.STFLD + utils.TABLEchannel;
				ret += utils.FIELDsavedIndex + utils.FIELDEQ + AltSavedIndex.ToString() + utils.ENDQT;
				ret += utils.ENDFLD;

				// GREEN subchannel
				AltSavedIndex = grnChannel.AltSavedIndex;
				ret += utils.CRLF + utils.LEVEL4 + utils.STFLD + utils.TABLEchannel;
				ret += utils.FIELDsavedIndex + utils.FIELDEQ + AltSavedIndex.ToString() + utils.ENDQT;
				ret += utils.ENDFLD;

				// BLUE subchannel
				AltSavedIndex = bluChannel.AltSavedIndex;
				ret += utils.CRLF + utils.LEVEL4 + utils.STFLD + utils.TABLEchannel;
				ret += utils.FIELDsavedIndex + utils.FIELDEQ + AltSavedIndex.ToString() + utils.ENDQT;
				ret += utils.ENDFLD;

				// End SubChannels
				ret += utils.CRLF + utils.LEVEL3 + utils.FINTBL + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;
				ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + Sequence4.TABLErgbChannel + utils.FINFLD;
			} // end TableType Channel or RGBchannel

			return ret;
		} // end LineOut

	} // end RGBchannel Class

	public class ChannelGroup : SeqPart, IComparable<SeqPart>
	{
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// Don't create circular references of groups in each other.
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public PartsCollection Children;

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					if (parentSequence != null) parentSequence.MakeDirty();

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int newSavedIndex)
		{
			myIndex = newSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int SavedIndex
		{
			get
			{
				return mySavedIndex;
			}
		}

		public void SetSavedIndex(int newSavedIndex)
		{
			mySavedIndex = newSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			parentSequence = newParentSeq;
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public TableType TableType
		{
			get
			{
				return TableType.ChannelGroup;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.cleanName(utils.getKeyWord(lineIn, utils.FIELDname));
			if (myName.IndexOf("inese 27") > 0)
			{
				int q = 5;
			}
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			Children = new PartsCollection(this);
			//Children = new PartsCollection(this);
			myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public ChannelGroup(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Children = new PartsCollection(this);
		}

		public ChannelGroup(string lineIn)
		{
			if (lineIn.Length > 4)
			{
				Parse(lineIn);
			}
		}

		public string LineOut(bool selectedOnly)
		{
			string ret = "";

			ret = utils.LEVEL2 + utils.STFLD + Sequence4.TABLEchannelGroupList;
			ret += utils.FIELDtotalCentiseconds + utils.FIELDEQ + myCentiseconds.ToString() + utils.ENDQT;
			ret += utils.FIELDname + utils.FIELDEQ + utils.dirtyName(myName) + utils.ENDQT;
			ret += utils.FIELDsavedIndex + utils.FIELDEQ + myAltSavedIndex.ToString() + utils.ENDQT;
			ret += utils.FINFLD;
			ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + Sequence4.TABLEchannelGroup + utils.PLURAL + utils.FINFLD;

			foreach (SeqPart part in Children.Items)
			{
				int osi = part.SavedIndex;
				int asi = part.AltSavedIndex;
				if (asi > utils.UNDEFINED)
				{
					ret += utils.CRLF + utils.LEVEL4 + utils.STFLD + Sequence4.TABLEchannelGroup;
					ret += utils.FIELDsavedIndex + utils.FIELDEQ + asi.ToString() + utils.ENDQT;
					ret += utils.ENDFLD;
				}
			}
			ret += utils.CRLF + utils.LEVEL3 + utils.FINTBL + Sequence4.TABLEchannelGroup + utils.PLURAL + utils.FINFLD;
			ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + Sequence4.TABLEchannelGroupList + utils.FINFLD;

			return ret;
		}

		public int AddItem(SeqPart newPart)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Children.Count; i++)
			{
				if (newPart.SavedIndex == Children.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this Channel Group '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
					//TODO: Make this just a warning, put "add" code below into an else block
					//TODO: Do the same with Tracks
					alreadyAdded = true;
					retSI = newPart.SavedIndex;
					i = Children.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				retSI = Children.Add(newPart);
				if (parentSequence != null) parentSequence.MakeDirty();
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			SeqPart newPart = parentSequence.Children.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		//TODO: add RemoveItem procedure
	}

	public class Effect
	{
		public EffectType type = EffectType.None;
		public int startCentisecond = utils.UNDEFINED;
		private int myEndCentisecond = 999999999;
		public int intensity = utils.UNDEFINED;
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
			Effect ret = new Effect();
			ret.type = type;
			ret.startCentisecond = startCentisecond;
			ret.endCentisecond = myEndCentisecond;
			ret.intensity = intensity;
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
			type = SeqEnums.enumEffect(utils.getKeyWord(lineIn, utils.FIELDtype));
			startCentisecond = utils.getKeyValue(lineIn, utils.FIELDstartCentisecond);
			endCentisecond = utils.getKeyValue(lineIn, utils.FIELDendCentisecond);
			intensity = utils.getKeyValue(lineIn, FIELDintensity);
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
			ret += utils.FIELDtype + utils.FIELDEQ + SeqEnums.EffectName(type) + utils.ENDQT;
			ret += utils.FIELDstartCentisecond + utils.FIELDEQ + startCentisecond.ToString() + utils.ENDQT;
			ret += utils.FIELDendCentisecond + utils.FIELDEQ + myEndCentisecond.ToString() + utils.ENDQT;
			if (intensity > utils.UNDEFINED)
			{
				ret += FIELDintensity + utils.FIELDEQ + intensity.ToString() + utils.ENDQT;
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
			string ret = SeqEnums.EffectName(type);
			ret += " From " + startCentisecond.ToString();
			ret += " to " + myEndCentisecond.ToString();
			if (intensity > utils.UNDEFINED)
			{
				ret += " at " + intensity.ToString();
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

	}

	public class TimingGrid : SeqPart, IComparable<SeqPart>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public TimingGridType type = TimingGridType.None;
		public int spacing = utils.UNDEFINED;
		public List<int> timings = new List<int>();

		public const string FIELDsaveID = " saveID";
		public const string TABLEtiming = "timing";
		public const string FIELDspacing = " spacing";

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					if (parentSequence != null) parentSequence.MakeDirty();

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int SavedIndex
		{
			get
			{
				int n = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count + ParentSequence.Tracks.Count;
				return n + mySavedIndex;
			}
		}

		public void SetSavedIndex(int theSaveID)
		{
			mySavedIndex = theSaveID;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			parentSequence = newParentSeq;
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public TableType TableType
		{
			get
			{
				return TableType.TimingGrid;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			string ret = "";

			ret += utils.LEVEL2 + utils.STFLD + Sequence4.TABLEtimingGrid;
			ret += FIELDsaveID + utils.FIELDEQ + myAltSavedIndex.ToString() + utils.ENDQT;
			if (myName.Length > 1)
			{
				ret += utils.FIELDname + utils.FIELDEQ + utils.dirtyName(myName) + utils.ENDQT;
			}
			ret += utils.FIELDtype + utils.FIELDEQ + SeqEnums.TimingName(type) + utils.ENDQT;
			if (spacing > 1)
			{
				ret += FIELDspacing + utils.FIELDEQ + spacing.ToString() + utils.ENDQT;
			}
			if (type == TimingGridType.FixedGrid)
			{
				ret += utils.ENDFLD;
			}
			else if (type == TimingGridType.Freeform)
			{
				ret += utils.FINFLD;

				//foreach (int tm in timings)
				for (int tm = 0; tm < timings.Count; tm++)
				{
					ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + TABLEtiming;
					ret += utils.FIELDcentisecond + utils.FIELDEQ + timings[tm].ToString() + utils.ENDQT;
					ret += utils.ENDFLD;
				}

				ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + Sequence4.TABLEtimingGrid + utils.FINFLD;
			}

			return ret;
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.cleanName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySavedIndex = utils.getKeyValue(lineIn, FIELDsaveID);
			Centiseconds = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
			type = SeqEnums.enumGridType(utils.getKeyWord(lineIn, utils.FIELDtype));
			spacing = utils.getKeyValue(lineIn, FIELDspacing);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public int SaveID
		{
			get
			{
				return mySavedIndex;
			}
			set
			{
				mySavedIndex = value;
				if (parentSequence != null) parentSequence.MakeDirty();
			}
		}

		public int AltSaveID
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}



		public TimingGrid(string lineIn)
		{
			if (lineIn.Length > 4)
			{
				Parse(lineIn);
			}
		}
		public TimingGrid(string theName, int theSaveID)
		{
			myName = theName;
			mySavedIndex = theSaveID;
		}


		public void AddTiming(int time)
		{
			if (timings.Count == 0)
			{
				timings.Add(time);
				if (parentSequence != null) parentSequence.MakeDirty();
			}
			else
			{
				if (timings[timings.Count - 1] < time)
				{
					timings.Add(time);
					if (parentSequence != null) parentSequence.MakeDirty();
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
									int temp = timings[n];
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
					if (parentSequence != null) parentSequence.MakeDirty();
				}
			}
			if (time > myCentiseconds)
			{
				Centiseconds = time;
			}
		}// end addTiming function

		public void Clear()
		{
			timings = new List<int>();
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int CopyTimings(TimingGrid otherGrid)
		{
			int count = 0;
			foreach(int tm in otherGrid.timings)
			{
				AddTiming(tm);
				count++;
			}
			if (parentSequence != null) parentSequence.MakeDirty();
			return count;
		}

	} // end timingGrid class

	public class Track : SeqPart, IComparable<SeqPart>
	{
		// Tracks are the ultimate top-level groups.
		// They do not have savedIndexes.
		// Channels, RGB Channels, and channel groups will not be displayed
		// and will not be accessible unless added to a track
		// or a subitem of a group in a track.
		// All sequences must have at least one track.
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public PartsCollection Children;
		public List<LoopLevel> loopLevels = new List<LoopLevel>();
		public TimingGrid timingGrid = null;

		public string Name
		{
			get
			{
				return myName;
			}
		}

		public void ChangeName(string newName)
		{
			myName = newName;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value > myCentiseconds)
				{
					myCentiseconds = value;
					if (parentSequence != null) parentSequence.MakeDirty();

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
				}
			}
		}

		public int Index
		{
			get
			{
				return myIndex;
			}
		}

		public void SetIndex(int theIndex)
		{
			myIndex = theIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int SavedIndex
		{
			get
			{
				int n = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count;
				return n + mySavedIndex;
			}
		}

		public void SetSavedIndex(int theTrackNumber)
		{
			mySavedIndex = theTrackNumber;
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltSavedIndex;
			}
			set
			{
				myAltSavedIndex = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			parentSequence = newParentSeq;
		}

		public bool Selected
		{
			get
			{
				return imSelected;
			}
			set
			{
				imSelected = value;
			}
		}

		public TableType TableType
		{
			get
			{
				return TableType.Track;
			}
		}

		public int CompareTo(SeqPart other)
		{
			int result = 0;
			if (parentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Children.sortMode == PartsCollection.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.cleanName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDtotalCentiseconds);
			Children = new PartsCollection(this);
			int tgsi = utils.getKeyValue(lineIn, Sequence4.TABLEtimingGrid);
			if (parentSequence == null)
			{
				// If the parent has not been assigned yet, there is no way to get ahold of the grid
				// So temporarily set the AltSavedIndex to this Track's TimingGrid's SaveID
				myAltSavedIndex = tgsi;
			}
			else
			{
				if (tgsi < 0)
				{
					// For Channel Configs, there will be no timing grid
					timingGrid = null;
				}
				else
				{
					// Assign the TimingGrid based on the SaveID
					SeqPart part = parentSequence.Children.bySaveID[tgsi];
					timingGrid = (TimingGrid)part;
				}
			}
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public int TrackNumber
		{
			get
			{
				return mySavedIndex;
			}
			set
			{
				mySavedIndex = value;
				if (parentSequence != null) parentSequence.MakeDirty();
			}
		}



		public Track(string theName, int theTrackNo)
		{
			myName = theName;
			mySavedIndex= theTrackNo;
			Children = new PartsCollection(this);
			//Children.ParentSequence = ID.ParentSequence;
		}
		public Track(string lineIn)
		{
			if (lineIn.Length > 5)
			{
				Parse(lineIn);
			}
		}
		
		public string LineOut(bool selectedOnly)
		{
			string ret = "";
			// Write info about track
			ret += utils.LEVEL2 + utils.STFLD + Sequence4.TABLEtrack;
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += utils.SPC + FIELDname + utils.FIELDEQ + Name + utils.ENDQT;
			//}
			ret += utils.FIELDtotalCentiseconds + utils.FIELDEQ + myCentiseconds.ToString() + utils.ENDQT;
			int altID = timingGrid.AltSavedIndex;
			ret += utils.SPC + Sequence4.TABLEtimingGrid + utils.FIELDEQ + altID.ToString() + utils.ENDQT;
			// LOR writes it with the Name last
			if (myName.Length > 1)
			{
				ret += utils.FIELDname + utils.FIELDEQ + utils.dirtyName(myName) + utils.ENDQT;
			}
			ret += utils.FINFLD;

			ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;

			// Loop thru all items in this track
			foreach (SeqPart subID in Children.Items)
			{
				bool sel = subID.Selected;
				if (!selectedOnly || sel)
				{
					// Write out the links to the items
					//masterSI = updatedTracks[trackIndex].newSavedIndexes[iti];

					if (subID.Name.IndexOf("inese 27") > 0)
					{
						int q = 5;
					}


					int siAlt = subID.AltSavedIndex;
					if (siAlt > utils.UNDEFINED)
					{
						ret += utils.CRLF + utils.LEVEL4 + utils.STFLD + utils.TABLEchannel;
						ret += utils.FIELDsavedIndex + utils.FIELDEQ + siAlt.ToString() + utils.ENDQT;
						ret += utils.ENDFLD;
					}
				}
			}

			// Close the list of items
			ret += utils.CRLF + utils.LEVEL3 + utils.FINTBL + utils.TABLEchannel + utils.PLURAL + utils.FINFLD;

			// Write out any LoopLevels in this track
			//writeLoopLevels(trackIndex);
			if (loopLevels.Count > 0)
			{
				ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + Sequence4.TABLEloopLevels + utils.FINFLD;
				foreach (LoopLevel ll in loopLevels)
				{
					ret += utils.CRLF + ll.LineOut();
				}
				ret += utils.CRLF + utils.LEVEL3 + utils.FINTBL + Sequence4.TABLEloopLevels + utils.FINFLD;
			}
			else
			{
				ret += utils.CRLF + utils.LEVEL3 + utils.STFLD + Sequence4.TABLEloopLevels + utils.ENDFLD;
			}
			ret += utils.CRLF + utils.LEVEL2 + utils.FINTBL + Sequence4.TABLEtrack + utils.FINFLD;


			return ret;
		}

		public int AddItem(SeqPart newPart)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Children.Count; i++)
			{
				if (newPart.SavedIndex == Children.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this Track '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "Channel Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
						//TODO: Make this just a warning, put "add" code below into an else block
						//TODO: Do the same with Tracks
						alreadyAdded = true;
					retSI = newPart.SavedIndex;
					i = Children.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				retSI = Children.Add(newPart);
				if (parentSequence != null) parentSequence.MakeDirty();
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			SeqPart newPart = ParentSequence.Children.bySavedIndex[itemSavedIndex];
			if (parentSequence != null) parentSequence.MakeDirty();
			return AddItem(newPart);
		}

		public LoopLevel AddLoopLevel(string lineIn)
		{
			LoopLevel newLL = new LoopLevel(lineIn);
			AddLoopLevel(newLL);
			if (parentSequence != null) parentSequence.MakeDirty();
			return newLL;
		}

		public int AddLoopLevel(LoopLevel newLL)
		{
			loopLevels.Add(newLL);
			if (parentSequence != null) parentSequence.MakeDirty();
			return loopLevels.Count - 1;
		}

		//TODO: add RemoveItem procedure
	} // end class track

	public class Animation
	{
		//public int rows = 0;
		//public int columns = 0;
		public string image = "";
		public List<AnimationRow> animationRows = new List<AnimationRow>();
		public Sequence4 parentSequence = null;

		public const string FIELDrows = " rows";
		public const string FIELDcolumns = " columns";
		public const string FIELDimage = " image";

		public int rows
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

			//if (rows > 0)
			//{
			ret += utils.LEVEL1 + utils.STFLD + Sequence4.TABLEanimation;
			ret += FIELDrows + utils.FIELDEQ + rows.ToString() + utils.ENDQT;
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
			//rows = utils.getKeyValue(lineIn, AnimationRow.FIELDrow + utils.PLURAL);
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
		} // end rows loop

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
	} // end Loop class

	public class LoopLevel
	{
		public List<Loop> loops = new List<Loop>();
		public int loopsCount = utils.UNDEFINED;

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
			loopsCount = utils.getKeyValue(lineIn, Loop.FIELDloopCount);
		}

		public string LineOut()
		{
			string ret = ""; ;

			ret += utils.CRLF + utils.LEVEL4 + utils.STTBL + Sequence4.TABLEloopLevel;
			if (loopsCount > 0)
			{
				ret += utils.SPC + Loop.FIELDloopCount + utils.FIELDEQ + loopsCount.ToString() + utils.ENDQT;
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

	} // end LoopLevel class

	public class Info
	{
		public string filename = "";
		public string xmlInfo = ""; // <?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
		public int saveFileVersion = utils.UNDEFINED;
		public string author = "Util-O-Rama";
		public DateTime file_created = DateTime.Now;
		public DateTime file_modified = DateTime.Now;
		public DateTime file_accessed = DateTime.Now;
		// Created At defaults to the file's Created At timestamp, but is overridden by what's in the file information header
		public string createdAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		// Last defaults to the file's modified At timestamp, but is overridden by any data changes
		public string lastModified = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
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


	} // end Info class

	/*
	public class Identity : IComparable<Identity>
	{
		public string Name = "";
		public int Centiseconds = utils.UNDEFINED;  // or 'totalCentiseconds'
		public int mySavedIndex = utils.UNDEFINED;    // or saveID for Timing Grids
		public int AltSavedIndex = utils.UNDEFINED; // or altSaveID for Timing Grids
		public int myIndex = utils.UNDEFINED;
		//private TableType thisObjType = TableType.None;
		public Sequence4 ParentSequence = null;
		public readonly SeqPart owner = null;
		//public bool Written = false;
		public bool Selected = false;
		//public Identity matchID = null;
		//public MatchType matchType = MatchType.Unmatched;

		private const string TABLEidentity = "<ID";
		private const string FIELDaltSavedIndex = " AltSavedIndex";
		private const string FIELDmyIndex = " myIndex";
		private const string FIELDTableType = " TableType";
		private const string FIELDselected = " Selected";

		public Identity(SeqPart newOwner)
		{
			owner = newOwner;
			//ParentSequence = myOwner.ParentSequence;
		}


		public Identity(SeqPart newOwner, string newName, int newSavedIndex)
		{
			owner = newOwner;
			Name = newName;
			mySavedIndex = newSavedIndex;
			
		}

		public int CompareTo(Identity other)
		{
			int ret = 0;
			if (ParentSequence.Children.sortMode == PartsCollection.SORTbyName)
			{
				string n1 = Name.ToLower();
				string n2 = other.Name.ToLower();
				ret = (n1.CompareTo(n2));
			}
			if (ParentSequence.Children.sortMode == PartsCollection.SORTbySavedIndex)
			{
				ret = SavedIndex.CompareTo(other.SavedIndex);
			}
			if (ParentSequence.Children.sortMode == PartsCollection.SORTbyAltSavedIndex)
			{
				ret = AltSavedIndex.CompareTo(other.AltSavedIndex);
			}
			if (ParentSequence.Children.sortMode == PartsCollection.SORTbyOutput)
			{
				if (PartType == TableType.Channel)
				{
					if (other.PartType == TableType.Channel)
					{
						Channel c1 = (Channel)owner;
						Channel c2 = (Channel)other.owner;
						string n1 = c1.output.SortableComparableString();
						string n2 = c2.output.SortableComparableString();
						ret = n1.CompareTo(n2);
					}
				}
			}
			return ret;
		}

		public bool Written
		{
			get
			{
				bool ret = true;
				if (AltSavedIndex == utils.UNDEFINED) ret = false;
				return ret;
			}
		}

		public TableType PartType
		{
			get
			{
				return owner.PartType;
			}
		}

		public int saveID  // Alias to SavedIndex for Timing Grids.
		{
			get
			{
				int ret = utils.UNDEFINED;
				if (owner.PartType == TableType.TimingGrid)
				{
					ret = SavedIndex;
				}
				return ret;
			}
			set
			{
				mySavedIndex = value;
			}
		}

		public int trackNo // Alias to SavedIndex for Tracks.
		{
			get
			{
				int ret = utils.UNDEFINED;
				if (owner.PartType == TableType.TimingGrid)
				{
					ret = SavedIndex;
				}
				return ret;
			}
			set
			{
				mySavedIndex = value;
			}
		}

		public int SavedIndex
		{
			get
			{
				int ret = utils.UNDEFINED;
				if ((owner.PartType == TableType.Channel) || (owner.PartType == TableType.RGBchannel) || (owner.PartType == TableType.ChannelGroup))
				{
					ret = mySavedIndex;
				}
				if (owner.PartType == TableType.Track)
				{
					int offset = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count;
					ret = offset + mySavedIndex;
				}
				if (owner.PartType == TableType.TimingGrid)
				{
					int offset = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count + ParentSequence.Tracks.Count ;
					ret = offset + mySavedIndex;
				}
				return ret;
			}
			
		}

		public void SetSavedIndex(int newSavedIndex)
		{
			// I made the saved index property (above) read only,
			// and this separate "Set" procedure to differentiate them
			// because normally, SavedIndex is set only when identity is first created.
			mySavedIndex = newSavedIndex;
		}


		public string mapLine()
		{
			StringBuilder sb = new StringBuilder(Name);

			sb.Append(utils.DELIM1);
			sb.Append(SavedIndex);
			sb.Append(utils.DELIM1);
			sb.Append(SeqEnums.TableTypeName(owner.PartType));

			return sb.ToString();

		}

		public string LineOut()
		{
			StringBuilder sb = new StringBuilder(TABLEidentity, 200);

			sb.Append(utils.FIELDname);        sb.Append(utils.FIELDEQ); sb.Append(Name);                        sb.Append(utils.ENDQT);
			sb.Append(utils.FIELDcentisecond); sb.Append(utils.FIELDEQ); sb.Append(Centiseconds);                sb.Append(utils.ENDQT);
			sb.Append(utils.FIELDsavedIndex);  sb.Append(utils.FIELDEQ); sb.Append(SavedIndex);                  sb.Append(utils.ENDQT);
			sb.Append(FIELDaltSavedIndex);     sb.Append(utils.FIELDEQ); sb.Append(AltSavedIndex);               sb.Append(utils.ENDQT);
			sb.Append(FIELDmyIndex);           sb.Append(utils.FIELDEQ); sb.Append(myIndex);                     sb.Append(utils.ENDQT);
			sb.Append(FIELDTableType); sb.Append(utils.FIELDEQ); sb.Append(SeqEnums.TableTypeName(owner.PartType)); sb.Append(utils.ENDQT);
			sb.Append(FIELDselected);          sb.Append(utils.FIELDEQ);
			if (Selected)
			{
				sb.Append("1\"");
			}
			else
			{
				sb.Append("0\"");
			}
			sb.Append(utils.ENDFLD);

			return sb.ToString();
		} // end LineOut

		public  override string ToString()
		{
			return Name;
		}

		public void Parse(string lineIn)
		{
			Name =          utils.getKeyWord(lineIn, utils.FIELDname);
			Centiseconds =  utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
			mySavedIndex =    utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			AltSavedIndex = utils.getKeyValue(lineIn, FIELDaltSavedIndex);
			myIndex =       utils.getKeyValue(lineIn, FIELDmyIndex);
			string s =      utils.getKeyWord(lineIn, FIELDTableType);
			//thisObjType =   SeqEnums.enumTableType(s);
			int i =         utils.getKeyValue(lineIn, FIELDselected);
			if (i==0)
			{
				Selected = false;
			}
			else
			{
				Selected = true;
			}
		} // end Parse

	} // end Identity class
	*/

	public class PartsCollection
	{
		public List<SeqPart> Items = new List<SeqPart>();   // The Main List
		//public List<SeqPart> Channels = new List<SeqPart>();   // The Main List
		//public List<SeqPart> RGBchannels = new List<SeqPart>();   // The Main List
		//public List<SeqPart> ChannelGroups = new List<SeqPart>();   // The Main List
		//public List<SeqPart> Tracks = new List<SeqPart>();   // The Main List
		//public List<SeqPart> TimingGrids = new List<SeqPart>();   // The Main List
		public List<SeqPart> bySavedIndex = new List<SeqPart>();
		public List<SeqPart> byAltSavedIndex = new List<SeqPart>();
		public List<TimingGrid> bySaveID = new List<TimingGrid>();
		public List<TimingGrid> byAltSaveID = new List<TimingGrid>();
		public SortedDictionary<string, SeqPart> byName = new SortedDictionary<string, SeqPart>();
		
		//public List<SeqPart> byAlphaChannelNames = new List<SeqPart>();
		//public List<SeqPart> byAlphaRGBchannelNames = new List<SeqPart>();
		//public List<SeqPart> byAlphaChannelGroupNames = new List<SeqPart>();
		//public List<SeqPart> byAlphaTrackNames = new List<SeqPart>();
		//public List<SeqPart> byAlphaTimingGridNames = new List<SeqPart>();
		//public int[] savedIndexes = { };
		//public int[] altSavedIndexes = { };
		//public int[] saveIDs = { };
		//public int[] altSaveIDs = { };
		private int highestSavedIndex = utils.UNDEFINED;
		public int altHighestSavedIndex = utils.UNDEFINED;
		private int highestSaveID = utils.UNDEFINED;
		public int altHighestSaveID = utils.UNDEFINED;
		public Sequence4 parentSequence = null;
		public SeqPart owner;


		public const int SORTbySavedIndex = 1;
		public const int SORTbyAltSavedIndex = 2;
		public const int SORTbyName = 3;
		public const int SORTbyOutput = 4;
		public int sortMode = SORTbySavedIndex;

		public int channelCount = 0;
		public int rgbChannelCount = 0;
		public int channelGroupCount = 0;
		public int trackCount = 0;
		public int timingGridCount = 0;
		public int allCount = 0;

		//public PartsCollection()
		//{
		//}


		public PartsCollection(SeqPart myOwner)
		{
			owner = myOwner;
		}

		public int Add(SeqPart newPart)
		{
			//newPart.SetParentSeq(ParentSequence);
			int masterSI = newPart.SavedIndex;
			if ((newPart.TableType == TableType.Channel) || (newPart.TableType == TableType.RGBchannel) || (newPart.TableType == TableType.ChannelGroup))
			{
				if (masterSI < 0)
				{
					highestSavedIndex++;
					masterSI = highestSavedIndex;
					newPart.SetSavedIndex(masterSI);
				}
				if (masterSI > highestSavedIndex)
				{
					highestSavedIndex = masterSI;
				}
				
				if (newPart.TableType == TableType.Channel)
				{
					//byAlphaChannelNames.Add(newPart);
					channelCount++;
				}
				if (newPart.TableType == TableType.RGBchannel)
				{
					//byAlphaRGBchannelNames.Add(newPart);
					rgbChannelCount++;
				}
				if (newPart.TableType == TableType.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newPart);
					channelGroupCount++;
				}

				while((bySavedIndex.Count-1) < masterSI)
				{
					bySavedIndex.Add(null);
					byAltSavedIndex.Add(null);
				}
				bySavedIndex[masterSI] = newPart;
				byAltSavedIndex[masterSI] = newPart;
				
			}

			if (newPart.TableType == TableType.Track)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newPart);
				bySavedIndex.Add(newPart);
				byAltSavedIndex.Add(newPart);
				trackCount++;
			}

			if (newPart.TableType == TableType.TimingGrid)
			{
				TimingGrid tg = (TimingGrid)newPart;
				masterSI = tg.SaveID;
				if (masterSI < 0)
				{
					highestSaveID++;
					masterSI = highestSaveID;
					newPart.SetSavedIndex(masterSI);
				}
				if (masterSI > highestSaveID)
				{
					highestSaveID = masterSI;
				}
				while ((bySaveID.Count - 1) < masterSI)
				{
					bySaveID.Add(null);
					byAltSaveID.Add(null);
				}
				bySaveID[masterSI] = tg;
				byAltSaveID[masterSI] = tg;

				bySavedIndex.Add(newPart);
				byAltSavedIndex.Add(newPart);
				timingGridCount++;
			}

			Items.Add(newPart);
			//bySavedIndex.Add(newPart);
			//byAltSavedIndex.Add(newPart);
			allCount++;
			if (parentSequence != null) parentSequence.MakeDirty();


			return masterSI;
		}

		public int Count
		{
			get
			{
				return Items.Count;
			}
		}

		public int GetNextAltSavedIndex(int originalSI)
		{
			SeqPart thePart = parentSequence.Children.bySavedIndex[originalSI];
			if (thePart.AltSavedIndex < 0)
			{
				parentSequence.Children.altHighestSavedIndex++;
				thePart.AltSavedIndex = parentSequence.Children.altHighestSavedIndex;
			}
			byAltSavedIndex[thePart.AltSavedIndex] = thePart;
			parentSequence.Children.byAltSavedIndex[thePart.AltSavedIndex] = thePart;
			return thePart.AltSavedIndex;
		}

		public int HighestSavedIndex
		{
			get
			{
				return highestSavedIndex;
			}
		}

		public int GetNextSavedIndex(SeqPart thePart)
		{
			parentSequence.Children.highestSavedIndex++;
			thePart.SetSavedIndex(parentSequence.Children.highestSavedIndex);
			return thePart.SavedIndex;
		}

		public int GetNextAltSaveID(TimingGrid theGrid)
		{
			if (theGrid.AltSavedIndex < 0)
			{
				altHighestSaveID++;
				byAltSaveID[altHighestSaveID] = theGrid;
				theGrid.AltSavedIndex = altHighestSaveID;
				byAltSaveID[altHighestSaveID] = theGrid;
			}
			return theGrid.AltSavedIndex;
		}

		public int HighestSaveID
		{
			get
			{
				return highestSaveID;
			}
		}

		public int GetNextSaveID()
		{
			highestSaveID++;
			return highestSaveID;
		}

		public void ReIndex()
		{
			// Clear previous run

			//Array.Resize(ref allNames, bySavedIndex.Count);
			//Array.Resize(ref byName, bySavedIndex.Count);
			//Array.Resize(ref byAltSavedIndex, bySavedIndex.Count);
			//Array.Resize(ref channelNames, ParentSequence.Channels.Count);
			//Array.Resize(ref byChannelName, ParentSequence.Channels.Count);
			//Array.Resize(ref rgbChannelNames, ParentSequence.RGBchannels.Count);
			//Array.Resize(ref byRGBchannelName, ParentSequence.RGBchannels.Count);
			//Array.Resize(ref channelGroupNames, ParentSequence.ChannelGroups.Count);
			//Array.Resize(ref byChannelGroupName, ParentSequence.ChannelGroups.Count);
			//Array.Resize(ref trackNames, ParentSequence.Tracks.Count);
			//Array.Resize(ref byTrackName, ParentSequence.Tracks.Count);
			//Array.Resize(ref timingGridNames, ParentSequence.TimingGrids.Count);
			//Array.Resize(ref byTimingGridName, ParentSequence.TimingGrids.Count);
			//Array.Resize(ref bySaveID, ParentSequence.TimingGrids.Count);
			//Array.Resize(ref byAltSaveID, ParentSequence.TimingGrids.Count);
			//Array.Resize(ref savedIndexes, 0);
			//Array.Resize(ref altSavedIndexes, 0);
			//Array.Resize(ref saveIDs, 0);
			//Array.Resize(ref altSaveIDs, 0);

			channelCount = 0;
			rgbChannelCount = 0;
			channelGroupCount = 0;
			trackCount = 0;
			timingGridCount = 0;
			allCount = 0;

			//SortBySavedIndex(0, bySavedIndex.Count - 1);
			sortMode = SORTbySavedIndex;

			byName = new SortedDictionary<string, SeqPart>();
			//byAlphaChannelNames = new List<SeqPart>();
			//byAlphaRGBchannelNames = new List<SeqPart>();
			//byAlphaChannelGroupNames = new List<SeqPart>();
			//byAlphaTrackNames = new List<SeqPart>();
			//byAlphaTimingGridNames = new List<SeqPart>();
			byAltSavedIndex = new List<SeqPart>();
			bySaveID = new List<TimingGrid>();
			byAltSaveID = new List<TimingGrid>();

			for (int i = 0; i < Items.Count; i++)
			{
				SeqPart part = Items[i];

				int n = 2;
				string itemName = part.Name;
				// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
				if (itemName == "")
				{
					// Make up a name based on type and index
					itemName = SeqEnums.TableTypeName(part.TableType) + " " + part.Index.ToString();
				}
				// Check for duplicate names
				while (byName.ContainsKey(itemName))
				{
					// Append a number
					itemName = part.Name + " ‹" + n.ToString() + "›";
					n++;
				}
				byName.Add(itemName, part);
				allCount++;

				if (part.TableType == TableType.Channel)
				{
					//byAlphaChannelNames.Add(part);
					//channelNames[channelCount] = part;
					byAltSavedIndex.Add(part);
					channelCount++;
				}
				else
				{
					if (part.TableType == TableType.RGBchannel)
					{
						//byAlphaRGBchannelNames.Add(part);
						//rgbChannelNames[rgbChannelCount] = part;
						byAltSavedIndex.Add(part);
						rgbChannelCount++;
					}
					else
					{
						if (part.TableType == TableType.ChannelGroup)
						{
							//byAlphaChannelGroupNames.Add(part);
							//channelGroupNames[channelGroupCount] = part;
							byAltSavedIndex.Add(part);
							channelGroupCount++;
						}
						else
						{
							if (part.TableType == TableType.Track)
							{
								//byAlphaTrackNames.Add(part);
								//trackNames[trackCount] = part;
								byAltSavedIndex.Add(part);
								trackCount++;
							}
							else
							{
								if (part.TableType == TableType.TimingGrid)
								{
									//byAlphaTimingGridNames.Add(part);
									//timingGridNames[timingGridCount] = part;
									byAltSavedIndex.Add(part);
									TimingGrid tg = (TimingGrid)part;
									bySaveID.Add(tg);
									byAltSaveID.Add(tg);
									timingGridCount++;
								}
							}
						}
					}
				}
			} // end foreach

			// Sort 'em all!
			//AlphaSortSavedIndexes(byName, 0, bySavedIndex.Count-1);
			//AlphaSortSavedIndexes(byChannelGroupName, 0, ParentSequence.ChannelGroups.Count-1);
			//AlphaSortSavedIndexes(byRGBchannelName, 0, ParentSequence.RGBchannels.Count - 1);
			//AlphaSortSavedIndexes(byChannelGroupName, 0, ParentSequence.ChannelGroups.Count - 1);
			//AlphaSortSavedIndexes(byTrackName, 0, ParentSequence.Tracks.Count - 1);
			sortMode = SORTbySavedIndex;
			bySavedIndex.Sort();
			bySaveID.Sort();

			sortMode = SORTbyName;
			//byName.Sort();
			//byAlphaChannelNames.Sort();
			//byAlphaRGBchannelNames.Sort();
			//byAlphaChannelGroupNames.Sort();
			//byAlphaTrackNames.Sort();


			if (parentSequence.TimingGrids.Count > 0)
			{
				//AlphaSortSavedIndexes(byTimingGridName, 0, ParentSequence.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			}
			//Array.Sort(savedIndexes, bySavedIndex);
			//Array.Sort(altSavedIndexes, byAltSavedIndex);
			//Array.Sort(saveIDs, bySaveID);
			//Array.Sort(altSaveIDs, byAltSaveID);

			// No longer need these, clear 'em
			//Array.Resize(ref allNames, 0);
			//Array.Resize(ref channelNames, 0);
			//Array.Resize(ref rgbChannelNames, 0);
			//Array.Resize(ref channelGroupNames, 0);
			//Array.Resize(ref trackNames, 0);
			//Array.Resize(ref timingGridNames, 0);
			//Array.Resize(ref savedIndexes, 0);
			//Array.Resize(ref altSavedIndexes, 0);
			//Array.Resize(ref saveIDs, 0);
			//Array.Resize(ref altSaveIDs, 0);

		} // end ReIndex

		/*
		public SeqPart FindByName(string theName, TableType PartType, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)
		{
			SeqPart ret = null;
			if (byName.TryGetValue(theName, out ret))
			{
				// Found the name, is the type correct?
				if (ret.TableType != PartType)
				{
					ret = null;
				}
			}
			else
			{ 
				if (finalAlgorithms > 0)
				{
					ret = FuzzyFind(theName, Items, preAlgorithm, minPreMatch, finalAlgorithms, minFinalMatch, ignoreSelected);
				}
			}

			return ret;
		}
		*/

		public SeqPart FindByName(string theName, TableType PartType)
		{
			//SeqPart ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			SeqPart ret = null;
			if (byName.TryGetValue(theName, out ret))
			{
				// Found the name, is the type correct?
				if (ret.TableType != PartType)
				{
					ret = null;
				}
			}
			return ret;
		}

		public static SeqPart FindByName(string theName, List<SeqPart> Children)
		{
			SeqPart ret = null;
			int idx = BinarySearch(theName, Children);
			if (idx > utils.UNDEFINED)
			{
				ret= Children[idx];
			}
			return ret;
		}

		public static int BinarySearch(string theName, List<SeqPart> Children)
		{
			return BinarySearch3(theName, Children, 0, Children.Count - 1);
		}

		public static int BinarySearch3(string theName, List<SeqPart> Children, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			string sname = Children[start].Name;
			string ename = Children[end].Name;
			string mname = Children[mid].Name;
			//if ((theName.CompareTo(Children[start].Name) > 0) && (theName.CompareTo(Children[end].Name) < 0))
			if ((theName.CompareTo(sname) >= 0) && (theName.CompareTo(ename) <= 0))
			{
				int cmp = theName.CompareTo(mname);
				if (cmp < 0)
					index = BinarySearch3(theName, Children, start, mid);
				else if (cmp > 0)
					index = BinarySearch3(theName, Children, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
				//	Console.WriteLine("got it at " + index);
			}
			return index;
		}

/*
		public SeqPart FuzzyFind(string theName, List<SeqPart> Children, long preAlgorithm, double minPreMatch, long finalAlgorithms, double minFinalMatch, bool ignoreSelected)

		{
			SeqPart ret = null;
			double[] scores = null;
			int[] SIs = null;
			int count = 0;
			double score;

			// Go thru all objects
			foreach (SeqPart child in Children)
			{
				if ((!child.Selected) || (!ignoreSelected))
				{
					// get a quick prematch score
					score = theName.RankEquality(child.Name, preAlgorithm);
					// fi the score is above the minimum PreMatch
					if (score > minPreMatch)
					{
						// Increment count and save the SavedIndex
						// NOte: No need to save the PreMatch score
						count++;
						Array.Resize(ref SIs, count);
						SIs[count - 1] = child.SavedIndex;
					}
				}
			}
			// Resize scores array to final size
			if (count > 0)
			{
				Array.Resize(ref scores, count);
				// Loop thru qualifying prematches
				for (int i = 0; i < count; i++)
				{
					// Get the ID, perform a more thorough final fuzzy match, and save the score
					SeqPart child = bySavedIndex[SIs[i]];
					score = theName.RankEquality(child.Name, finalAlgorithms);
					scores[i] = score;
				}
				// Now sort the final scores (and the SavedIndexes along with them)
				Array.Sort(scores, SIs);
				// Is the best/highest above the required minimum Final Match score?
				if (scores[count - 1] > minFinalMatch)
				{
					// Return the ID with the best qualifying final match
					ret = bySavedIndex[SIs[count - 1]];
					// Get Name just for debugging
					string msg = theName + " ~= " + ret.Name;
				}
			}
			return ret;
		}
		*/

		public void ResetWritten()
		{
			foreach(SeqPart part in bySavedIndex)
			{
				part.AltSavedIndex = utils.UNDEFINED;
				//part.Written = false;
			}
			altHighestSavedIndex = utils.UNDEFINED;
			altHighestSaveID = utils.UNDEFINED;
		}

		/*
		private void SortBySavedIndex(int left, int right)
		{
			int iq = bySavedIndex.Count;
			int hq = highestSavedIndex;
			int cq = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count;
			int rq = ParentSequence.Tracks.Count;
			int tq = ParentSequence.TimingGrids.Count;

			int errs = ParentSequence.SavedIndexIntegrityCheck();


			SeqPart[] idz = null;
			Array.Resize(ref idz, bySavedIndex.Count);
			int track1st = highestSavedIndex +1;
			int grid1st = track1st + ParentSequence.Tracks.Count;

			//foreach (SeqPart id in Children)
			for (int ii = 0; ii < bySavedIndex.Count; ii++)
			{
				SeqPart id = Children[ii];
				int si = id.SavedIndex;
				if (id.PartType == TableType.Track)
				{
					si = track1st + id.myIndex;
				}
				if (id.PartType == TableType.TimingGrid)
				{
					si = grid1st + id.SavedIndex;
				}
				if (idz[si] == null)
				{
					idz[si] = id;
				}
				else
				{
					string msg = "Houston, we have a duplicate index!";
				}

			}
			for (int x = 0; x < idz.Length; x++)
			{
				Children[x] = idz[x];
			}
		}
		

		
		public void AlphaSortSavedIndexes(List<SeqPart> IDs, int left, int right)
		{
			int i = left, j = right;
			string pivot = IDs[(left + right) / 2].Name;

			while (i <= j)
			{
				while (Children[SIs[i]].Name.CompareTo(pivot) < 0)
				{
					i++;
				}

				while (Children[SIs[j]].Name.CompareTo(pivot) > 0)
				{
					j--;
				}

				if (i <= j)
				{
					// Swap
					int tmp = SIs[i];
					SIs[i] = SIs[j];
					SIs[j] = tmp;

					i++;
					j--;
				}
			}

			// Recursive calls
			if (left < j)
			{
				AlphaSortSavedIndexes(SIs, left, j);
			}

			if (i < right)
			{
				AlphaSortSavedIndexes(SIs, i, right);
			}



		}
		*/



	} // end Children Class






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
