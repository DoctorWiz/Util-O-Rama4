using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace LORUtils4
{
	public interface LORMember4 : IComparable<LORMember4>
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
		LORSequence4 ParentSequence
		{
			get;
		}
		void SetParentSeq(LORSequence4 newParentSeq);
		//List <LORMember4> parents
		//{
		//	get;
		//}
		bool Selected
		{
			get;
			set;
		}
		LORMemberType4 LORMemberType4
		{
			get;
		}
		int CompareTo(LORMember4 otherObj);
		string LineOut();
		string ToString();
		void Parse(string lineIn);
		bool Written
		{
			get;
		}
		LORMember4 Duplicate();

	}

	public class LORChannel4 : LORMember4, IComparable<LORMember4>
	{
		private string myName = "";
		private int myCentiseconds = lutils.UNDEFINED;
		private int myIndex = lutils.UNDEFINED;
		private int mySavedIndex = lutils.UNDEFINED;
		private int myAltSavedIndex = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		private bool imSelected = false;
		private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;

		public Int32 color = 0;
		public LOROutput4 output = new LOROutput4();
		public LORRGBChild4 rgbChild = LORRGBChild4.None;
		public LORRGBChannel4 rgbParent = null;
		public List<LOREffect4> effects = new List<LOREffect4>();

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

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(LORSequence4 newParentSeq)
		{
			if (newParentSeq == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				this.parentSequence = newParentSeq;
			}
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

		public LORMemberType4 LORMemberType4
		{
			get
			{
				return LORMemberType4.LORChannel4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership.sortMode == LORMembership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership.sortMode == LORMembership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership.sortMode == LORMembership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership.sortMode == LORMembership.SORTbyOutput)
						{
							LORChannel4 ch = (LORChannel4)other;
							output.ToString().CompareTo(ch.output.ToString());
						}
					}
				}
			}
			return result;
		}

		// I'm not a big fan of case sensitivity, but I'm gonna take advantage of it here
		// color with lower c is the LOR color, a 32 bit int in BGR order
		// Color with capital C is the .Net Color object
		public Color Color
		{
			get
			{
				return lutils.Color_LORtoNet(color);
			}
			set
			{
				color = lutils.Color_NettoLOR(value);
			}

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
			//LORSequence4 ParentSequence = ID.ParentSequence;
			myName = lutils.cleanName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			color = lutils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			output.Parse(lineIn);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Duplicate()
		{
			// See Also: Copy(), CopyTo(), and CopyFrom()
			LORChannel4 chan = new LORChannel4(myName, lutils.UNDEFINED);
			chan.myCentiseconds = myCentiseconds;
			chan.myIndex = myIndex;
			chan.mySavedIndex = mySavedIndex;
			chan.myAltSavedIndex = myAltSavedIndex;
			chan.imSelected = imSelected;
			chan.color = color;
			chan.output = output.Duplicate();
			chan.rgbChild = this.rgbChild;
			chan.rgbParent = rgbParent; // should be changed/overridden
			chan.effects = DuplicateEffects();
			return chan;
		}

		public List<LOREffect4> DuplicateEffects()
		{
			List<LOREffect4> newList = new List<LOREffect4>();
			foreach(LOREffect4 ef in effects)
			{
				LOREffect4 F = ef.Duplicate();
				newList.Add(F);
			}
			return newList;
		}

		public string LineOut(bool noEffects)
		{
			string ret = "";
			ret = lutils.LEVEL2 + lutils.STFLD + lutils.TABLEchannel;
			ret += lutils.FIELDname + lutils.FIELDEQ + lutils.dirtyName(myName) + lutils.ENDQT;
			ret += FIELDcolor + lutils.FIELDEQ + color.ToString() + lutils.ENDQT;
			ret += lutils.FIELDcentiseconds + lutils.FIELDEQ + myCentiseconds.ToString() + lutils.ENDQT;
			ret += output.LineOut();
			ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + myAltSavedIndex.ToString() + lutils.ENDQT;

			// Are there any effects for this channel?
			if (!noEffects && (effects.Count > 0))
			{
				// complete channel line with regular '>' then do effects
				ret += lutils.FINFLD;
				foreach (LOREffect4 thisEffect in effects)
				{
					ret += lutils.CRLF + thisEffect.LineOut();
				}
				ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + lutils.TABLEchannel + lutils.FINFLD;
			}
			else // NO effects for this channal
			{
				// complete channel line with field end '/>'
				ret += lutils.ENDFLD;
			}

			return ret;
		}


		public LORChannel4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public LORChannel4(string lineIn)
		{
			string seek = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname + lutils.FIELDEQ;
			int pos = lineIn.IndexOf(seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				if (lineIn.Length > 1)
				{
					myName = lineIn;
				}
			}
		}

		public void CopyTo(LORChannel4 destination, bool withEffects)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.parentSequence == null) destination.parentSequence = this.parentSequence;
			if (destination.output.deviceType == LORDeviceType4.None)
			{
				destination.output.deviceType = output.deviceType;
				destination.output.circuit = output.circuit;
				destination.output.network = output.network;
				destination.output.unit = output.unit;
			}
			if (withEffects)
			{
				destination.Centiseconds = myCentiseconds;
				foreach (LOREffect4 thisEffect in effects)
				{
					destination.effects.Add(thisEffect.Copy());
				}
			}
		}

		public void CopyFrom(LORChannel4 source, bool withEffects)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.parentSequence == null) this.parentSequence = source.parentSequence;
			if (output.deviceType == LORDeviceType4.None)
			{
				output.deviceType = source.output.deviceType;
				output.circuit = source.output.circuit;
				output.network = source.output.network;
				output.unit = source.output.unit;
			}
			if (withEffects)
			{
				myCentiseconds = source.Centiseconds;
				foreach (LOREffect4 theEffect in source.effects)
				{
					//effects.Add(theEffect.Copy());
					AddEffect(theEffect.Copy());
				}
			}
		}

		public LORChannel4 Copy(bool withEffects)
		{
			// See Also: Duplicate()
			//int nextSI = ID.ParentSequence.Members.highestSavedIndex + 1;
			LORChannel4 ret = new LORChannel4(myName, lutils.UNDEFINED);
			ret.color = color;
			ret.output = output;
			ret.rgbChild = rgbChild;
			ret.rgbParent = rgbParent;
			ret.SetParentSeq(ParentSequence);
			List<LOREffect4> newEffects = new List<LOREffect4>();
			if (withEffects)
			{
				foreach (LOREffect4 thisEffect in effects)
				{
					newEffects.Add(thisEffect.Copy());
				}
				ret.effects = newEffects;
			}
			return ret;
		}

		public void AddEffect(LOREffect4 theEffect)
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
			LOREffect4 theEffect = new LOREffect4(lineIn);
			AddEffect(theEffect);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public int CopyEffects(List<LOREffect4> effectList, bool Merge)
		{
			LOREffect4 newEffect = null;
			if (!Merge)
			{
				//! Note: clears any pre-existing effects!
				effects.Clear();
			}
			foreach (LOREffect4 thisEffect in effectList)
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

	//////////////////////////////////////////////
	// 
	//		RGB CHANNEL
	//
	////////////////////////////////////////////////

	public class LORRGBChannel4 : LORMember4, IComparable<LORMember4>
	{
		private string myName = "";
		private int myCentiseconds = lutils.UNDEFINED;
		private int myIndex = lutils.UNDEFINED;
		private int mySavedIndex = lutils.UNDEFINED;
		private int myAltSavedIndex = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		private bool imSelected = false;

		public LORChannel4 redChannel = null;
		public LORChannel4 grnChannel = null;
		public LORChannel4 bluChannel = null;

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

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(LORSequence4 newParentSeq)
		{
			this.parentSequence = newParentSeq;
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

		public LORMemberType4 LORMemberType4
		{
			get
			{
				return LORMemberType4.LORRGBChannel4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership.sortMode == LORMembership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership.sortMode == LORMembership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership.sortMode == LORMembership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false, false, LORMemberType4.FullTrack);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = lutils.cleanName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Duplicate()
		{
			LORRGBChannel4 rgb = new LORRGBChannel4(myName,lutils.UNDEFINED);
			rgb.myCentiseconds = myCentiseconds;
			rgb.myIndex = myIndex;
			rgb.mySavedIndex = mySavedIndex;
			rgb.myAltSavedIndex = myAltSavedIndex;
			rgb.imSelected = imSelected;
			
			// These are placeholders so you can get the Index, SavedIndex, Selected state, etc.
			// Either add these subchannels to the [new] sequence BEFORE adding the parent RGB
			// or replace them with new ones
			rgb.redChannel = (LORChannel4)redChannel.Duplicate();
			rgb.grnChannel = (LORChannel4)grnChannel.Duplicate();
			rgb.bluChannel = (LORChannel4)bluChannel.Duplicate();

			return rgb;
		}


		public LORRGBChannel4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public LORRGBChannel4(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLErgbChannel + lutils.FIELDtotalCentiseconds + lutils.FIELDEQ;
			int pos = lineIn.IndexOf(seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}


		public string LineOut(bool selectedOnly, bool noEffects, LORMemberType4 itemTypes)
		{
			string ret = "";

			//int redSavedIndex = lutils.UNDEFINED;
			//int grnSavedIndex = lutils.UNDEFINED;
			//int bluSavedIndex = lutils.UNDEFINED;

			int AltSavedIndex = lutils.UNDEFINED;

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.LORChannel4))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent LORRGBChannel4 is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.LORRGBChannel4))
			{
				ret = lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLErgbChannel;
				ret += lutils.FIELDtotalCentiseconds + lutils.FIELDEQ + myCentiseconds.ToString() + lutils.ENDQT;
				ret += lutils.FIELDname + lutils.FIELDEQ + lutils.dirtyName(myName) + lutils.ENDQT;
				ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + myAltSavedIndex.ToString() + lutils.ENDQT;
				ret += lutils.FINFLD;

				// Start SubChannels
				ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;

				// RED subchannel
				AltSavedIndex = redChannel.AltSavedIndex;
				ret += lutils.CRLF + lutils.LEVEL4 + lutils.STFLD + lutils.TABLEchannel;
				ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + AltSavedIndex.ToString() + lutils.ENDQT;
				ret += lutils.ENDFLD;

				// GREEN subchannel
				AltSavedIndex = grnChannel.AltSavedIndex;
				ret += lutils.CRLF + lutils.LEVEL4 + lutils.STFLD + lutils.TABLEchannel;
				ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + AltSavedIndex.ToString() + lutils.ENDQT;
				ret += lutils.ENDFLD;

				// BLUE subchannel
				AltSavedIndex = bluChannel.AltSavedIndex;
				ret += lutils.CRLF + lutils.LEVEL4 + lutils.STFLD + lutils.TABLEchannel;
				ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + AltSavedIndex.ToString() + lutils.ENDQT;
				ret += lutils.ENDFLD;

				// End SubChannels
				ret += lutils.CRLF + lutils.LEVEL3 + lutils.FINTBL + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;
				ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLErgbChannel + lutils.FINFLD;
			} // end LORMemberType4 LORChannel4 or LORRGBChannel4

			return ret;
		} // end LineOut

	} // end LORRGBChannel4 Class

	public class LORChannelGroup4 : LORMember4, IComparable<LORMember4>
	{
		// LORChannel4 Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// LORChannel4 Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All LORChannel4 Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// LORChannel4 groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the LORTrack4 class)

		private string myName = "";
		private int myCentiseconds = lutils.UNDEFINED;
		private int myIndex = lutils.UNDEFINED;
		private int mySavedIndex = lutils.UNDEFINED;
		private int myAltSavedIndex = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		private bool imSelected = false;
		public const string TABLEchannelGroupList = "channelGroupList";
		private const string STARTchannelGroup = lutils.STFLD + TABLEchannelGroupList + lutils.SPC;

		public LORMembership Members; // = new LORMembership(this);

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
				//if (myName.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();
				//! Polyphonic Transcription Group
				//! Getting set to 5479, but should be getting set to 5480
				//! 5479 is supposed to be "Song Parts"
				//! Somewhere along the way, some one thing is not getting written
				//TODO Need to track down which one item, and why it (and just it)






				myAltSavedIndex = value;
			}
		}

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(LORSequence4 newParentSeq)
		{
			this.parentSequence = newParentSeq;
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

		public LORMemberType4 LORMemberType4
		{
			get
			{
				return LORMemberType4.LORChannelGroup4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership.sortMode == LORMembership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership.sortMode == LORMembership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership.sortMode == LORMembership.SORTbyAltSavedIndex)
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
			myName = lutils.cleanName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			if (myName.IndexOf("inese 27") > 0)
			{
				//System.Diagnostics.Debugger.Break();
			}
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			Members = new LORMembership(this);
			//Members = new LORMembership(this);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Duplicate()
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			LORChannelGroup4 grp = new LORChannelGroup4(myName, lutils.UNDEFINED);
			grp.myCentiseconds = myCentiseconds;
			grp.myIndex = myIndex;
			grp.mySavedIndex = mySavedIndex;
			grp.myAltSavedIndex = myAltSavedIndex;
			grp.imSelected = imSelected;

			return grp;
		}

		public LORChannelGroup4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new LORMembership(this);
		}

		public LORChannelGroup4(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new LORMembership(this);
			string seek = lutils.STFLD + LORSequence4.TABLEchannelGroupList + lutils.FIELDtotalCentiseconds + lutils.FIELDEQ;
			int pos = lineIn.IndexOf(seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				if (lineIn.Length > 1)
				{
					myName = lineIn;
				}
			}
		}

		public string LineOut(bool selectedOnly)
		{
			string ret = "";

			ret = lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLEchannelGroupList;
			ret += lutils.FIELDtotalCentiseconds + lutils.FIELDEQ + myCentiseconds.ToString() + lutils.ENDQT;
			ret += lutils.FIELDname + lutils.FIELDEQ + lutils.dirtyName(myName) + lutils.ENDQT;
			ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + myAltSavedIndex.ToString() + lutils.ENDQT;
			ret += lutils.FINFLD;
			ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEchannelGroup + lutils.PLURAL + lutils.FINFLD;

			foreach (LORMember4 member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > lutils.UNDEFINED)
				{
					ret += lutils.CRLF + lutils.LEVEL4 + lutils.STFLD + LORSequence4.TABLEchannelGroup;
					ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + asi.ToString() + lutils.ENDQT;
					ret += lutils.ENDFLD;
				}
			}
			ret += lutils.CRLF + lutils.LEVEL3 + lutils.FINTBL + LORSequence4.TABLEchannelGroup + lutils.PLURAL + lutils.FINFLD;
			ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLEchannelGroupList + lutils.FINFLD;

			return ret;
		}

		public int AddItem(LORMember4 newPart)
		{
			int retSI = lutils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this LORChannel4 Group '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "LORChannel4 Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
					//TODO: Make this just a warning, put "add" code below into an else block
					//TODO: Do the same with Tracks
					alreadyAdded = true;
					retSI = newPart.SavedIndex;
					i = Members.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				retSI = Members.Add(newPart);
				if (parentSequence != null) parentSequence.MakeDirty();
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			LORMember4 newPart = parentSequence.Members.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		//TODO: add RemoveItem procedure
	}

	public class LORTimings4 : LORMember4, IComparable<LORMember4>
	{
		private string myName = "";
		private int myCentiseconds = lutils.UNDEFINED;
		private int myIndex = lutils.UNDEFINED;
		private int mySavedIndex = lutils.UNDEFINED;
		private int myAltSavedIndex = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		private bool imSelected = false;

		public LORUtils4.LORTimingGridType4 LORTimingGridType4 = LORUtils4.LORTimingGridType4.None;
		public int spacing = lutils.UNDEFINED;
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
				return n + myIndex;
			}
		}

		public void SetSavedIndex(int theSaveID)
		{
			//mySavedIndex = theSaveID;
			//if (parentSequence != null) parentSequence.MakeDirty();
			//! We should not attempt to set the SavedIndex of a timing grid, set it's SaveID instead
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

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(LORSequence4 newParentSeq)
		{
			this.parentSequence = newParentSeq;
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

		public LORMemberType4 LORMemberType4
		{
			get
			{
				return LORMemberType4.LORTimings4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership.sortMode == LORMembership.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership.sortMode == LORMembership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership.sortMode == LORMembership.SORTbyAltSavedIndex)
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

			ret += lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLEtimingGrid;
			ret += FIELDsaveID + lutils.FIELDEQ + myAltSavedIndex.ToString() + lutils.ENDQT;
			if (myName.Length > 1)
			{
				ret += lutils.FIELDname + lutils.FIELDEQ + lutils.dirtyName(myName) + lutils.ENDQT;
			}
			ret += lutils.FIELDtype + lutils.FIELDEQ + LORSeqEnums4.LORTimingName4(this.LORTimingGridType4) + lutils.ENDQT;
			if (spacing > 1)
			{
				ret += FIELDspacing + lutils.FIELDEQ + spacing.ToString() + lutils.ENDQT;
			}
			if (this.LORTimingGridType4 == LORUtils4.LORTimingGridType4.FixedGrid)
			{
				ret += lutils.ENDFLD;
			}
			else if (this.LORTimingGridType4 == LORUtils4.LORTimingGridType4.Freeform)
			{
				ret += lutils.FINFLD;

				//foreach (int tm in timings)
				for (int tm = 0; tm < timings.Count; tm++)
				{
					ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + TABLEtiming;
					ret += lutils.FIELDcentisecond + lutils.FIELDEQ + timings[tm].ToString() + lutils.ENDQT;
					ret += lutils.ENDFLD;
				}

				ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLEtimingGrid + lutils.FINFLD;
			}

			return ret;
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = lutils.cleanName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, FIELDsaveID);
			Centiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			this.LORTimingGridType4 = LORSeqEnums4.LOREnumGridType4(lutils.getKeyWord(lineIn, lutils.FIELDtype));
			spacing = lutils.getKeyValue(lineIn, FIELDspacing);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Duplicate()
		{
			LORTimings4 grid = new LORTimings4(myName, lutils.UNDEFINED);
			grid.myCentiseconds = myCentiseconds;
			grid.myIndex = myIndex;
			grid.mySavedIndex = mySavedIndex; // SaveID
			grid.LORTimingGridType4 = this.LORTimingGridType4;
			grid.spacing = spacing;
			if (this.LORTimingGridType4 == LORUtils4.LORTimingGridType4.Freeform)
			{
				foreach(int time in timings)
				{
					grid.timings.Add(time);
				}
			}

			return grid;
		}


		public int SaveID
		{
			get
			{
				return mySavedIndex;
			}
			set
			{
				//mySavedIndex = value;
				//if (parentSequence != null) parentSequence.MakeDirty();
				//System.Diagnostics.Debugger.Break();
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



		public LORTimings4(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLEtimingGrid + FIELDsaveID + lutils.FIELDEQ;
			int pos = lineIn.IndexOf(seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}

		public LORTimings4(string theName, int theSaveID)
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

		public int CopyTimings(List<int> newTimes, bool merge)
		{
			int count = 0;
			if (!merge)
			{
				timings = new List<int>(); 
			}
			foreach (int tm in newTimes)
			{
				AddTiming(tm);
				count++;
			}
			if (parentSequence != null) parentSequence.MakeDirty();
			return count;
		}

	} // end timingGrid class

	public class LORTrack4 : LORMember4, IComparable<LORMember4>
	{
		// Tracks are the ultimate top-level groups.  Level 2 and up are handled by 'ChannelGroups'
		// LORChannel4 groups are optional, a sequence many not have any groups, but it will always have at least one track
		// Tracks do not have savedIndexes.  They are just numbered instead.
		// Tracks can contain regular Channels, RGB Channels, and LORChannel4 Groups, but not other tracks
		// (ie: Tracks cannot be nested like LORChannel4 Groups (which can be nested many levels deep))
		// All LORChannel4 Groups, regular Channels, and RGB Channels must directly or indirectly belong to one or more tracks.
		// Channels, RGB Channels, and channel groups will not be displayed and will not be accessible unless added to one or
		// more tracks, directly or subdirectly (a subitem of a group in a track).
		// A LORTrack4 may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// (See related notes in the LORChannelGroup4 class)

		private string myName = "";
		private int myCentiseconds = lutils.UNDEFINED;
		private int myIndex = lutils.UNDEFINED;
		private int mySavedIndex = lutils.UNDEFINED;
		private int myAltSavedIndex = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		private bool imSelected = false;

		public LORMembership Members; // = new LORMembership(null);
		public List<LORLoopLevel4> loopLevels = new List<LORLoopLevel4>();
		public LORTimings4 timingGrid = null;

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
				return n + myIndex + 1;
			}
		}

		public void SetSavedIndex(int theTrackNumber)
		{
			//mySavedIndex = theTrackNumber;
			//if (parentSequence != null) parentSequence.MakeDirty();
			//! Why are we trying to set the SavedIndex of a track?!?
			System.Diagnostics.Debugger.Break();
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

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(LORSequence4 newParentSeq)
		{
			this.parentSequence = newParentSeq;
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

		public LORMemberType4 LORMemberType4
		{
			get
			{
				return LORMemberType4.LORTrack4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership.sortMode == LORMembership.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership.sortMode == LORMembership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership.sortMode == LORMembership.SORTbyAltSavedIndex)
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
			this.Members = new LORMembership(this);
			myName = lutils.cleanName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			int tgsi = lutils.getKeyValue(lineIn, LORSequence4.TABLEtimingGrid);
			if (this.parentSequence == null)
			{
				// If the parent has not been assigned yet, there is no way to get ahold of the grid
				// So temporarily set the AltSavedIndex to this LORTrack4's LORTimings4's SaveID
				myAltSavedIndex = tgsi;
			}
			else
			{
				if (tgsi < 0)
				{
					// For LORChannel4 Configs, there will be no timing grid
					timingGrid = null;
				}
				else
				{
					// Assign the LORTimings4 based on the SaveID
					LORMember4 member = parentSequence.Members.bySaveID[tgsi];
					timingGrid = (LORTimings4)member;
				}
			}
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSavedIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Duplicate()
		{
			LORTrack4 tr = new LORTrack4(myName, lutils.UNDEFINED);
			tr.myCentiseconds = myCentiseconds;
			tr.myIndex = myIndex;
			tr.mySavedIndex = mySavedIndex;
			tr.imSelected = imSelected;
			tr.timingGrid = (LORTimings4)timingGrid.Duplicate();

			return tr;
		}

		public int TrackNumber
		{
			get
			{
				// LORTrack4 numbers are one based, the index is zero based, so just add 1 to the index for the track number
				return myIndex + 1;
			}
			// Read-Only!
			//set
			//{
			//	myIndex = value;
			//	if (parentSequence != null) parentSequence.MakeDirty();
			//}
		}



		public LORTrack4(string theName, int theTrackNo)
		{
			Members = new LORMembership(this);
			myName = theName;
			mySavedIndex= theTrackNo;
			//Members.ParentSequence = ID.ParentSequence;
		}

		public LORTrack4(string lineIn)
		{
			Members = new LORMembership(this);
			string seek = lutils.STFLD + LORSequence4.TABLEtrack + lutils.FIELDtotalCentiseconds + lutils.FIELDEQ;
			int pos = lineIn.IndexOf(seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}
		
		public string LineOut(bool selectedOnly)
		{
			string ret = "";
			// Write info about track
			ret += lutils.LEVEL2 + lutils.STFLD + LORSequence4.TABLEtrack;
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += lutils.SPC + FIELDname + lutils.FIELDEQ + Name + lutils.ENDQT;
			//}
			ret += lutils.FIELDtotalCentiseconds + lutils.FIELDEQ + myCentiseconds.ToString() + lutils.ENDQT;
			int altID = timingGrid.AltSavedIndex;
			ret += lutils.SPC + LORSequence4.TABLEtimingGrid + lutils.FIELDEQ + altID.ToString() + lutils.ENDQT;
			// LOR writes it with the Name last
			if (myName.Length > 1)
			{
				ret += lutils.FIELDname + lutils.FIELDEQ + lutils.dirtyName(myName) + lutils.ENDQT;
			}
			ret += lutils.FINFLD;

			ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;

			// LORLoop4 thru all items in this track
			foreach (LORMember4 subID in Members.Items)
			{
				bool sel = subID.Selected;
				if (!selectedOnly || sel)
				{
					// Write out the links to the items
					//masterSI = updatedTracks[trackIndex].newSavedIndexes[iti];

					if (subID.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

					int siAlt = subID.AltSavedIndex;
					if (siAlt > lutils.UNDEFINED)
					{
						ret += lutils.CRLF + lutils.LEVEL4 + lutils.STFLD + lutils.TABLEchannel;
						ret += lutils.FIELDsavedIndex + lutils.FIELDEQ + siAlt.ToString() + lutils.ENDQT;
						ret += lutils.ENDFLD;
					}
				}
			}

			// Close the list of items
			ret += lutils.CRLF + lutils.LEVEL3 + lutils.FINTBL + lutils.TABLEchannel + lutils.PLURAL + lutils.FINFLD;

			// Write out any LoopLevels in this track
			//writeLoopLevels(trackIndex);
			if (loopLevels.Count > 0)
			{
				ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEloopLevels + lutils.FINFLD;
				foreach (LORLoopLevel4 ll in loopLevels)
				{
					ret += lutils.CRLF + ll.LineOut();
				}
				ret += lutils.CRLF + lutils.LEVEL3 + lutils.FINTBL + LORSequence4.TABLEloopLevels + lutils.FINFLD;
			}
			else
			{
				ret += lutils.CRLF + lutils.LEVEL3 + lutils.STFLD + LORSequence4.TABLEloopLevels + lutils.ENDFLD;
			}
			ret += lutils.CRLF + lutils.LEVEL2 + lutils.FINTBL + LORSequence4.TABLEtrack + lutils.FINFLD;


			return ret;
		}

		public int AddItem(LORMember4 newPart)
		{
			int retSI = lutils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
				{
					//TODO: Using saved index, look up Name of item being added
					string sMsg = newPart.Name + " has already been added to this LORTrack4 '" + myName + "'.";
					//DialogResult rs = MessageBox.Show(sMsg, "LORChannel4 Groups", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					if (System.Diagnostics.Debugger.IsAttached)
						//System.Diagnostics.Debugger.Break();
						//TODO: Make this just a warning, put "add" code below into an else block
						//TODO: Do the same with Tracks
						alreadyAdded = true;
					retSI = newPart.SavedIndex;
					i = Members.Count; // Break out of loop
				}
			}
			if (!alreadyAdded)
			{
				retSI = Members.Add(newPart);
				if (parentSequence != null) parentSequence.MakeDirty();
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			LORMember4 newPart = ParentSequence.Members.bySavedIndex[itemSavedIndex];
			if (parentSequence != null) parentSequence.MakeDirty();
			return AddItem(newPart);
		}

		public LORLoopLevel4 AddLoopLevel(string lineIn)
		{
			LORLoopLevel4 newLL = new LORLoopLevel4(lineIn);
			AddLoopLevel(newLL);
			if (parentSequence != null) parentSequence.MakeDirty();
			return newLL;
		}

		public int AddLoopLevel(LORLoopLevel4 newLL)
		{
			loopLevels.Add(newLL);
			if (parentSequence != null) parentSequence.MakeDirty();
			return loopLevels.Count - 1;
		}

		//TODO: add RemoveItem procedure
	} // end class track

	public class LORMembership : IEnumerable  //  IEnumerator<LORMember4>
	{
		public List<LORMember4> Items = new List<LORMember4>();   // The Main List
		public List<LORMember4> bySavedIndex = new List<LORMember4>();
		public List<LORMember4> byAltSavedIndex = new List<LORMember4>();
		public List<LORTimings4> bySaveID = new List<LORTimings4>();
		public List<LORTimings4> byAltSaveID = new List<LORTimings4>();
		public SortedDictionary<string, LORMember4> byName = new SortedDictionary<string, LORMember4>();

		private int highestSavedIndex = lutils.UNDEFINED;
		public int altHighestSavedIndex = lutils.UNDEFINED;
		private int highestSaveID = lutils.UNDEFINED;
		public int altHighestSaveID = lutils.UNDEFINED;
		private LORSequence4 parentSequence = null;
		public LORMember4 owner;

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
		private int position = 0;

		//LORMember4 IEnumerator<LORMember4>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();

		public LORMembership(LORMember4 myOwner)
		{
			if (myOwner == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				this.owner = myOwner;
			}
		}

		public int Add(LORMember4 newMember)
		{
			if (this.parentSequence == null)
			{
				if (newMember.ParentSequence == null)
				{
					System.Diagnostics.Debugger.Break();
				}
				else
				{
					this.parentSequence = newMember.ParentSequence;
				}
			}
			else
			{
				newMember.SetParentSeq(this.parentSequence);
			}


			int memberSI = newMember.SavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty();
			if ((newMember.LORMemberType4 == LORMemberType4.LORChannel4) || (newMember.LORMemberType4 == LORMemberType4.LORRGBChannel4) || (newMember.LORMemberType4 == LORMemberType4.LORChannelGroup4))
			{
				if (memberSI < 0)
				{
					highestSavedIndex++;
					memberSI = highestSavedIndex;
					newMember.SetSavedIndex(memberSI);
				}
				if (memberSI > highestSavedIndex)
				{
					highestSavedIndex = memberSI;
				}
				
				if (newMember.LORMemberType4 == LORMemberType4.LORChannel4)
				{
					//byAlphaChannelNames.Add(newMember);
					channelCount++;
				}
				if (newMember.LORMemberType4 == LORMemberType4.LORRGBChannel4)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					rgbChannelCount++;
				}
				if (newMember.LORMemberType4 == LORMemberType4.LORChannelGroup4)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					channelGroupCount++;
				}

				while((bySavedIndex.Count-1) < memberSI)
				{
					bySavedIndex.Add(null);
					byAltSavedIndex.Add(null);
				}
				bySavedIndex[memberSI] = newMember;
				byAltSavedIndex[memberSI] = newMember;
				
			}

			if (newMember.LORMemberType4 == LORMemberType4.LORTrack4)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newMember);
				bySavedIndex.Add(newMember);
				byAltSavedIndex.Add(newMember);
				trackCount++;
			}

			if (newMember.LORMemberType4 == LORMemberType4.LORTimings4)
			{
				LORTimings4 tg = (LORTimings4)newMember;
				memberSI = tg.SaveID;
				if (memberSI < 0)
				{
					highestSaveID++;
					memberSI = highestSaveID;
					newMember.SetSavedIndex(memberSI);
				}
				if (memberSI > highestSaveID)
				{
					highestSaveID = memberSI;
				}
				while ((bySaveID.Count - 1) < memberSI)
				{
					bySaveID.Add(null);
					byAltSaveID.Add(null);
				}
				bySaveID[memberSI] = tg;
				byAltSaveID[memberSI] = tg;

				bySavedIndex.Add(newMember);
				byAltSavedIndex.Add(newMember);
				timingGridCount++;
			}

			if (owner.LORMemberType4 == LORMemberType4.Sequence)
			{
				bool need2add = false;
				if (newMember.SavedIndex < 0)
				{
					need2add = true;
				}
				if (memberSI > ParentSequence.Members.highestSavedIndex)
				{
					if ((newMember.LORMemberType4 == LORMemberType4.LORChannel4) ||
							(newMember.LORMemberType4 == LORMemberType4.LORRGBChannel4) ||
							(newMember.LORMemberType4 == LORMemberType4.LORChannelGroup4))
					{
						need2add = true;
					}
				}
				else
				{
					if (newMember.Name.CompareTo(ParentSequence.Members.bySavedIndex[memberSI].Name) != 0)
					{
						if (newMember.LORMemberType4 == ParentSequence.Members.bySavedIndex[memberSI].LORMemberType4)
						{
							need2add = true;
						}
					}
				}
				if (need2add)
				{
					if (newMember.LORMemberType4 == LORMemberType4.LORChannel4)
					{
						ParentSequence.Channels.Add((LORChannel4)newMember);
					}
					if (newMember.LORMemberType4 == LORMemberType4.LORRGBChannel4)
					{
						ParentSequence.RGBchannels.Add((LORRGBChannel4)newMember);
					}
					if (newMember.LORMemberType4 == LORMemberType4.LORChannelGroup4)
					{
						ParentSequence.ChannelGroups.Add((LORChannelGroup4)newMember);
					}
					if (newMember.LORMemberType4 == LORMemberType4.LORTrack4)
					{
						ParentSequence.Tracks.Add((LORTrack4)newMember);
					}
					if (newMember.LORMemberType4 == LORMemberType4.LORTimings4)
					{
						ParentSequence.TimingGrids.Add((LORTimings4)newMember);
					}
				} // end if need2add
			} // end if owner is the sequenced




			Items.Add(newMember);
			//bySavedIndex.Add(newMember);
			//byAltSavedIndex.Add(newMember);
			allCount++;



			return memberSI;
		}

		// For iEnumerable
		public LORMember4 this[int index]
		{
			get { return Items[index]; }
			set { Items.Insert(index, value); }
		}

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}


		public bool MoveNext()
		{
			position++;
			return (position < Items.Count);
		}

		public void Reset()
		{ position = -1; }

		public object Current
		{
			get { return Items[position]; }
		}


		public int Count
		{
			get
			{
				return Items.Count;
			}
		}


		public int HighestSavedIndex
		{

			get
			{
				return highestSavedIndex;
			}
		}

		public int HighestSaveID
		{
			get
			{
				return highestSaveID;
			}
			set
			{
				highestSaveID = value;
			}
		}

		public void ReIndex()
		{
			// Clear previous run

			channelCount = 0;
			rgbChannelCount = 0;
			channelGroupCount = 0;
			trackCount = 0;
			timingGridCount = 0;
			allCount = 0;

			sortMode = SORTbySavedIndex;

			byName = new SortedDictionary<string, LORMember4>();
			byAltSavedIndex = new List<LORMember4>();
			bySaveID = new List<LORTimings4>();
			byAltSaveID = new List<LORTimings4>();

			for (int i = 0; i < Items.Count; i++)
			{
				LORMember4 member = Items[i];

				int n = 2;
				string itemName = member.Name;
				// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
				if (itemName == "")
				{
					// Make up a name based on type and index
					itemName = LORSeqEnums4.LORMemberName4(member.LORMemberType4) + " " + member.Index.ToString();
				}
				// Check for duplicate names
				while (byName.ContainsKey(itemName))
				{
					// Append a number
					itemName = member.Name + " ‹" + n.ToString() + "›";
					n++;
				}
				byName.Add(itemName, member);
				allCount++;

				if (member.LORMemberType4 == LORMemberType4.LORChannel4)
				{
					//byAlphaChannelNames.Add(member);
					//channelNames[channelCount] = member;
					byAltSavedIndex.Add(member);
					channelCount++;
				}
				else
				{
					if (member.LORMemberType4 == LORMemberType4.LORRGBChannel4)
					{
						//byAlphaRGBchannelNames.Add(member);
						//rgbChannelNames[rgbChannelCount] = member;
						byAltSavedIndex.Add(member);
						rgbChannelCount++;
					}
					else
					{
						if (member.LORMemberType4 == LORMemberType4.LORChannelGroup4)
						{
							//byAlphaChannelGroupNames.Add(member);
							//channelGroupNames[channelGroupCount] = member;
							byAltSavedIndex.Add(member);
							channelGroupCount++;
						}
						else
						{
							if (member.LORMemberType4 == LORMemberType4.LORTrack4)
							{
								//byAlphaTrackNames.Add(member);
								//trackNames[trackCount] = member;
								byAltSavedIndex.Add(member);
								trackCount++;
							}
							else
							{
								if (member.LORMemberType4 == LORMemberType4.LORTimings4)
								{
									//byAlphaTimingGridNames.Add(member);
									//timingGridNames[timingGridCount] = member;
									byAltSavedIndex.Add(member);
									LORTimings4 tg = (LORTimings4)member;
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
			sortMode = SORTbySavedIndex;




			//System.Diagnostics.Debugger.Break();
			// Sort is failing, supposedly because array elements are not set (null/empty)
			//  -- but a quick check of 'Locals' doesn't show any empties
			NULLITEMCHECK();
			bySavedIndex.Sort();
			bySaveID.Sort();

			sortMode = SORTbyName;


			if (parentSequence.TimingGrids.Count > 0)
			{
				//AlphaSortSavedIndexes(byTimingGridName, 0, ParentSequence.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			}

		} // end ReIndex

		private void NULLITEMCHECK()
		{
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i] == null) System.Diagnostics.Debugger.Break();
				if (Items[i].ParentSequence == null) System.Diagnostics.Debugger.Break();
				int v = Items[i].CompareTo(Items[0]);
			}

		}



		public LORMember4 Find(string theName, LORMemberType4 theType, bool createIfNotFound)
		{
			LORMember4 ret = null;
			if (ret==null)
			{
				if (this.parentSequence == null) this.parentSequence = owner.ParentSequence;
				if (parentSequence != null)
				{
					if (theType == LORMemberType4.LORChannel4)
					{
						if (channelCount > 0)
						{
							ret = FindByName(theName, theType);
						}
						if ((ret == null) && createIfNotFound)
						{
							ret = parentSequence.CreateChannel(theName);
							Add(ret);
						}
					}
					if (theType == LORMemberType4.LORRGBChannel4)
					{
						if (rgbChannelCount > 0)
						{
							ret = FindByName(theName, theType);
						}
						if ((ret == null) && createIfNotFound)
						{
							ret = parentSequence.CreateRGBchannel(theName);
							Add(ret);
						}
					}
					if (theType == LORMemberType4.LORChannelGroup4)
					{
						if (channelGroupCount > 0)
						{
							ret = FindByName(theName, theType);
						}
						if ((ret == null) && createIfNotFound)
						{
							ret = parentSequence.CreateChannelGroup(theName);
							Add(ret);
						}
					}
					if (theType == LORMemberType4.LORTimings4)
					{
						if (timingGridCount > 0)
						{
							ret = FindByName(theName, theType);
						}
						if ((ret == null) && createIfNotFound)
						{
							ret = parentSequence.CreateTimingGrid(theName);
							Add(ret);
						}
					}
					if (theType == LORMemberType4.LORTrack4)
					{
						if (trackCount > 0)
						{
							ret = FindByName(theName, theType);
						}
						if ((ret == null) && createIfNotFound)
						{
							ret = parentSequence.CreateTrack(theName);
							Add(ret);
						}
					}
				}
			}

			return ret;
		}



		public LORMember4 FindBySavedIndex(int theSavedIndex)
		{
			LORMember4 ret = bySavedIndex[theSavedIndex];
			return ret;
		}

		private LORMember4 FindByName(string theName, LORMemberType4 PartType)
		{
			//LORMember4 ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			if (byName.TryGetValue(theName, out LORMember4 ret))
			{
				// Found the name, is the type correct?
				if (ret.LORMemberType4 != PartType)
				{
					ret = null;
				}
			}
			return ret;
		}

		private static LORMember4 FindByName(string theName, List<LORMember4> Members)
		{
			LORMember4 ret = null;
			int idx = BinarySearch(theName, Members);
			if (idx > lutils.UNDEFINED)
			{
				ret= Members[idx];
			}
			return ret;
		}

		private static int BinarySearch(string theName, List<LORMember4> Members)
		{
			return TreeSearch(theName, Members, 0, Members.Count - 1);
		}

		private static int TreeSearch(string theName, List<LORMember4> Members, int start, int end)
		{
			int index = -1;
			int mid = (start + end) / 2;
			string sname = Members[start].Name;
			string ename = Members[end].Name;
			string mname = Members[mid].Name;
			//if ((theName.CompareTo(Members[start].Name) > 0) && (theName.CompareTo(Members[end].Name) < 0))
			if ((theName.CompareTo(sname) >= 0) && (theName.CompareTo(ename) <= 0))
			{
				int cmp = theName.CompareTo(mname);
				if (cmp < 0)
					index = TreeSearch(theName, Members, start, mid);
				else if (cmp > 0)
					index = TreeSearch(theName, Members, mid + 1, end);
				else if (cmp == 0)
					index = mid;
				//if (index != -1)
				//	Console.WriteLine("got it at " + index);
			}
			return index;
		}

		public void ResetWritten()
		{
			foreach(LORMember4 member in bySavedIndex)
			{
				member.AltSavedIndex = lutils.UNDEFINED;
				//member.Written = false;
			}
			altHighestSavedIndex = lutils.UNDEFINED;
			altHighestSaveID = lutils.UNDEFINED;
		}


		public int DescendantCount(bool selectedOnly, bool countPlain, bool countRGBparents, bool countRGBchildren)
		{
			// Depending on situation/usaage, you will probably want to count
			// The RGBchannels, OR count their 3 children.
			//    Unlikely you will need to count neither or both, but you can if you want
			// ChannelGroups themselves are not counted, but all their descendants are (except descendant groups).
			// Tracks are not counted.

			int c = 0;
			for (int l = 0; l < Items.Count; l++)
			{
				LORMemberType4 t = Items[l].LORMemberType4;
				if (t == LORMemberType4.LORChannel4)
				{
					if (countPlain)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.LORRGBChannel4)
				{
					if (countRGBparents)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						LORRGBChannel4 rgbCh = (LORRGBChannel4)Items[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.LORChannelGroup4)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)Items[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
			}


			return c;
		}

		public LORSequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSequence(LORSequence4 theParent)
		{
			if (theParent == null)
			{
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				this.parentSequence = theParent;
			}
		}


		//bool IEnumerator.MoveNext()
		//{
		//	throw new NotImplementedException();
		//}

		//void IEnumerator.Reset()
		//{
		//	throw new NotImplementedException();
		//}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~LORMembership() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		//void IDisposable.Dispose()
		//{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			//Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		//}
		#endregion


	} // end LORMembership Class (Collection)




}
