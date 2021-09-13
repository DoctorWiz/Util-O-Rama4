using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

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
			set;
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
		LORMember4 Parent
		{
			get;
		}
		void SetParent(LORMember4 newParentSeq);
		// For Channels, RGBChannels, ChannelGroups, Tracks, Timings, etc.  This will be the parent Sequence
		// For VizChannels and VizDrawObjects this will be the parent Visualization
		bool Selected
		{
			get;
			set;
		}
		bool Dirty
		{
			get;
		}
		void MakeDirty(bool dirtyState);
		LORMemberType4 MemberType
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
		LORMember4 Clone();
		LORMember4 Clone(string newName);
		object Tag
		{
			get;
			set;
		}
		LORMember4 MapTo
		{
			get;
			set;
		}
		bool ExactMatch
		{
			get;
			set;
		}
		int UniverseNumber
		{
			get;
		}
		int DMXAddress
		{
			get;
		}

	}

	public class LORChannel4 : LORMember4, IComparable<LORMember4>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;

		public Int32 color = 0;
		public LOROutput4 output = new LOROutput4();
		public LORRGBChild4 rgbChild = LORRGBChild4.None;
		public LORRGBChannel4 rgbParent = null;
		public List<LOREffect4> effects = new List<LOREffect4>();
		private object tag = null;
		LORMember4 mappedTo = null;
		protected bool matchExact = false;
		protected bool isDirty = false;

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
			if (parentSequence != null) parentSequence.MakeDirty(true);

		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							//System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < lutils.MINCentiseconds)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.isWiz)
							{
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							// If shortening, Make sure no effects extend past new value
							if (value < myCentiseconds)
							{
								int e = 0;
								while (e < effects.Count)
								{
									// if it starts after the new end time, remove it entirely
									if (effects[e].startCentisecond > value)
									{
										effects.RemoveAt(e);
									}
									else
									{
										// or if it just ends after the new end time, shorten it
										if (effects[e].endCentisecond > value)
										{
											effects[e].endCentisecond = value + 1;
										}
									}
									e++;
								}
							}

							myCentiseconds = value;

							if (parentSequence != null) parentSequence.MakeDirty(true);

							if (myCentiseconds > Parent.Centiseconds)
							{
								//Parent.Centiseconds = value;
							}
							if (rgbParent != null)
							{
								if (myCentiseconds > rgbParent.Centiseconds)
								{
									//rgbParent.Centiseconds = myCentiseconds;
								}
							}
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

		public bool Dirty
		{ get { return isDirty; } }

		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}

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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Channel;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			//if (parentSequence.Members.sortMode == LORMembership4.SORTbySavedIndex)
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
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
			//LORSequence4 Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			color = lutils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			output.Parse(lineIn);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LORChannel4 chan = (LORChannel4)this.Clone(myName);
			return chan;
		}

		public LORMember4 Clone(string newName)
		{
			LORChannel4 chan = new LORChannel4(newName, lutils.UNDEFINED);
			chan.myCentiseconds = myCentiseconds;
			chan.myIndex = myIndex;
			chan.mySavedIndex = mySavedIndex;
			chan.myAltSavedIndex = myAltSavedIndex;
			chan.imSelected = imSelected;
			chan.color = color;
			chan.output = output.Clone();
			chan.rgbChild = this.rgbChild;
			chan.rgbParent = rgbParent; // should be changed/overridden
			chan.effects = CloneEffects();
			return chan;

		}

		public List<LOREffect4> CloneEffects()
		{
			List<LOREffect4> newList = new List<LOREffect4>();
			foreach(LOREffect4 ef in effects)
			{
				LOREffect4 F = ef.Clone();
				newList.Add(F);
			}
			return newList;
		}

		public string LineOut(bool noEffects)
		{
			StringBuilder ret = new StringBuilder();
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(lutils.TABLEchannel);

			ret.Append(lutils.FIELDname);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(myName));
			ret.Append(lutils.ENDQT);

			ret.Append(FIELDcolor);
			ret.Append(lutils.FIELDEQ);
			ret.Append(color.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDcentiseconds);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(output.LineOut());

			ret.Append(lutils.FIELDsavedIndex);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(lutils.ENDQT);

			// Are there any effects for this channel?
			if (!noEffects && (effects.Count > 0))
			{
				// complete channel line with regular '>' then do effects
				ret.Append(lutils.FINFLD);
				foreach (LOREffect4 thisEffect in effects)
				{
					ret.Append(lutils.CRLF);
					ret.Append(thisEffect.LineOut());
				}
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.FINTBL);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FINFLD);
			}
			else // NO effects for this channal
			{
				// complete channel line with field end '/>'
				ret.Append(lutils.ENDFLD);
			}

			return ret.ToString();
		}

		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.Channel)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }
		public int UniverseNumber
		{
			get
			{
				return output.network;
			}
		}
		public int DMXAddress
		{
			get
			{
				return output.channel;
			}
		}

		public LORChannel4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public LORChannel4(string lineIn)
		{
			string seek = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
					destination.effects.Add(thisEffect.Clone());
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
					//effects.Add(theEffect.Clone());
					AddEffect(theEffect.Clone());
				}
			}
		}

		public LORChannel4 Clone(bool withEffects)
		{
			// See Also: Duplicate()
			//int nextSI = ID.Parent.Members.highestSavedIndex + 1;
			LORChannel4 ret = new LORChannel4(myName, lutils.UNDEFINED);
			ret.color = color;
			ret.output = output;
			ret.rgbChild = rgbChild;
			ret.rgbParent = rgbParent;
			ret.SetParent(Parent);
			List<LOREffect4> newEffects = new List<LOREffect4>();
			if (withEffects)
			{
				foreach (LOREffect4 thisEffect in effects)
				{
					newEffects.Add(thisEffect.Clone());
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
			//ID.Parent.dirty = true;
		}

		public void AddEffect(string lineIn)
		{
			LOREffect4 theEffect = new LOREffect4(lineIn);
			AddEffect(theEffect);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
				newEffect = thisEffect.Clone();
				newEffect.parent = this;
				newEffect.myIndex = effects.Count;
				effects.Add(newEffect);
				Parent.MakeDirty(true);
			}
			if (Merge)
			{
				effects.Sort();
			}
			// Return how many effects were copied
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		protected object tag = null;
		protected LORMember4 mappedTo = null;
		protected bool matchExact = false;
		protected bool isDirty = false;

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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				int cs = 0;
				if (redChannel != null)
				{
					cs = redChannel.Centiseconds;
				}
				if (grnChannel != null)
				{
					if (grnChannel.Centiseconds > cs) cs = grnChannel.Centiseconds;
				}
				if (bluChannel != null)
				{
					if (bluChannel.Centiseconds > cs) cs = bluChannel.Centiseconds;
				}
				myCentiseconds = cs;
				return cs;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < lutils.MINCentiseconds)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.isWiz)
							{
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							if (redChannel != null)
							{
								if (redChannel.Centiseconds > value) redChannel.Centiseconds = value;
							}
							if (grnChannel != null)
							{
								if (grnChannel.Centiseconds > value) grnChannel.Centiseconds = value;
							}
							if (bluChannel != null)
							{
							  if (bluChannel.Centiseconds > value) bluChannel.Centiseconds = value;
							}

							if (parentSequence != null) parentSequence.MakeDirty(true);

							//if (myCentiseconds > ParentSequence.Centiseconds)
							//{
							//	ParentSequence.Centiseconds = value;
							//}
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

		public void SetIndex(int newSavedIndex)
		{
			myIndex = newSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}

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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.RGBChannel;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
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
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Clone()
		{
			LORRGBChannel4 rgb = (LORRGBChannel4)this.Clone(myName);
			return rgb;
		}

		public LORMember4 Clone(string newName)
		{
			LORRGBChannel4 rgb = new LORRGBChannel4(newName, lutils.UNDEFINED);
			rgb.myCentiseconds = myCentiseconds;
			rgb.myIndex = myIndex;
			rgb.mySavedIndex = mySavedIndex;
			rgb.myAltSavedIndex = myAltSavedIndex;
			rgb.imSelected = imSelected;

			// These are placeholders so you can get the Index, SavedIndex, Selected state, etc.
			// Either add these subchannels to the [new] sequence BEFORE adding the parent RGB
			// or replace them with new ones
			rgb.redChannel = (LORChannel4)redChannel.Clone();
			rgb.grnChannel = (LORChannel4)grnChannel.Clone();
			rgb.bluChannel = (LORChannel4)bluChannel.Clone();

			return rgb;
		}

		public object Tag
		{
			get
			{
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.RGBChannel)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }

		public LORRGBChannel4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public LORRGBChannel4(string lineIn)
		{
			string seek = lutils.STFLD + LORSequence4.TABLErgbChannel + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
			StringBuilder ret = new StringBuilder();

			//int redSavedIndex = lutils.UNDEFINED;
			//int grnSavedIndex = lutils.UNDEFINED;
			//int bluSavedIndex = lutils.UNDEFINED;

			int AltSavedIndex = lutils.UNDEFINED;

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.Channel))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent LORRGBChannel4 is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == LORMemberType4.Items) || (itemTypes == LORMemberType4.RGBChannel))
			{
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.STFLD);
				ret.Append(LORSequence4.TABLErgbChannel);
				ret.Append(lutils.FIELDtotalCentiseconds);
				ret.Append(lutils.FIELDEQ);
				ret.Append(myCentiseconds.ToString());
				ret.Append(lutils.ENDQT);

				ret.Append(lutils.FIELDname);
				ret.Append(lutils.FIELDEQ);
				ret.Append(lutils.XMLifyName(myName));
				ret.Append(lutils.ENDQT);

				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(myAltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);

				ret.Append(lutils.FINFLD);

				// Start SubChannels
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.PLURAL);
				ret.Append(lutils.FINFLD);

				// RED subchannel
				AltSavedIndex = redChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// GREEN subchannel
				AltSavedIndex = grnChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// BLUE subchannel
				AltSavedIndex = bluChannel.AltSavedIndex;
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL4);
				ret.Append(lutils.STFLD);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.FIELDsavedIndex);
				ret.Append(lutils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(lutils.ENDQT);
				ret.Append(lutils.ENDFLD);

				// End SubChannels
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.FINTBL);
				ret.Append(lutils.TABLEchannel);
				ret.Append(lutils.PLURAL);
				ret.Append(lutils.FINFLD);

				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.FINTBL);
				ret.Append(LORSequence4.TABLErgbChannel);
				ret.Append(lutils.FINFLD);
			} // end LORMemberType4 Channel or LORRGBChannel4

			return ret.ToString();
		} // end LineOut

		public int UniverseNumber
		{
			get
			{
				return redChannel.output.network;
			}
		}
		public int DMXAddress
		{
			get
			{
				return redChannel.output.channel;
			}
		}

	} // end LORRGBChannel4 Class

	public class LORChannelGroup4 : LORMember4, IComparable<LORMember4>
	{
		// Channel Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All Channel Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// Channel groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the LORTrack4 class)

		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		public const string TABLEchannelGroupList = "channelGroupList";
		private const string STARTchannelGroup = lutils.STFLD + TABLEchannelGroupList + lutils.SPC;
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected bool matchExact = false;
		protected bool isDirty = false;

		public LORMembership4 Members; // = new LORMembership4(this);

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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				int cs = 0;
				for (int idx = 0; idx < Members.Count; idx++)
				{
					LORMember4 mbr = Members[idx];
					if (Members.Items[idx].Centiseconds > cs)
					{
						cs = Members.Items[idx].Centiseconds;
					}
				}
				myCentiseconds = cs;
				return cs;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < lutils.MINCentiseconds)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.isWiz)
							{
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							for (int idx = 0; idx < Members.Count; idx++)
							{
								LORMember4 mbr = Members[idx];
								if (Members.Items[idx].Centiseconds > value)
								{
									Members.Items[idx].Centiseconds = value;
								}
							}
							if (parentSequence != null) parentSequence.MakeDirty(true);

							//if (myCentiseconds > Parent.Centiseconds)
							//{
							//	Parent.Centiseconds = value;
							//}
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

		public void SetIndex(int newSavedIndex)
		{
			myIndex = newSavedIndex;
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}

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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.ChannelGroup;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
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
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			if (myName.IndexOf("inese 27") > 0)
			{
				//System.Diagnostics.Debugger.Break();
			}
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			Members = new LORMembership4(this);
			//Members = new LORMembership4(this);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Clone()
		{
			LORChannelGroup4 grp = (LORChannelGroup4)this.Clone(myName);
			return grp;
		}

		public LORMember4 Clone(string newName)
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

		public object Tag
		{
			get
			{
				return myTag;
			}
			set
			{
				myTag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.ChannelGroup)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }

		public LORChannelGroup4(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new LORMembership4(this);
		}

		public LORChannelGroup4(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new LORMembership4(this);
			string seek = lutils.STFLD + LORSequence4.TABLEchannelGroupList + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEchannelGroupList);

			ret.Append(lutils.FIELDtotalCentiseconds);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDname);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(myName));
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDsavedIndex);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			foreach (LORMember4 member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > lutils.UNDEFINED)
				{
					ret.Append(lutils.CRLF);
					ret.Append(lutils.LEVEL4);
					ret.Append(lutils.STFLD);
					ret.Append(LORSequence4.TABLEchannelGroup);

					ret.Append(lutils.FIELDsavedIndex);
					ret.Append(lutils.FIELDEQ);
					ret.Append(asi.ToString());
					ret.Append(lutils.ENDQT);
					ret.Append(lutils.ENDFLD);
				}
			}
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEchannelGroupList);
			ret.Append(lutils.FINFLD);

			return ret.ToString();
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
					string sMsg = newPart.Name + " has already been added to this Channel Group '" + myName + "'.";
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
				if (parentSequence != null) parentSequence.MakeDirty(true);
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			LORMember4 newPart = parentSequence.Members.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		public int UniverseNumber
		{
			get
			{
				if (Members.Count > 0)
				{
					return Members[0].UniverseNumber;
				}
				else
				{
					return 0;
				}
			}
		}
		public int DMXAddress
		{
			get
			{
				if (Members.Count > 0)
				{
					return Members[0].DMXAddress;
				}
				else
				{
					return 0;
				}
			}
		}

		//TODO: add RemoveItem procedure
	}

	public class LORCosmic : LORMember4, IComparable<LORMember4>
	{
		// Channel Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All Channel Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// Channel groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the LORTrack4 class)

		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySavedIndex = lutils.UNDEFINED;
		protected int myAltSavedIndex = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		public const string TABLEcosmicDeviceDevice = "colorCosmicDevice";
		private const string STARTcosmicDevice = lutils.STFLD + TABLEcosmicDeviceDevice + lutils.SPC;
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected bool matchExact = false;
		protected bool isDirty = false;

		public LORMembership4 Members; // = new LORMembership4(this);

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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value != myCentiseconds)
				{
					myCentiseconds = value;
					for (int idx = 0; idx < Members.Count; idx++)
					{
						Members.Items[idx].Centiseconds = value;
					}
					if (parentSequence != null) parentSequence.MakeDirty(true);

					//if (myCentiseconds > Parent.Centiseconds)
					//{
					//	Parent.Centiseconds = value;
					//}
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}

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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Cosmic;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
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
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			Members = new LORMembership4(this);
			//Members = new LORMembership4(this);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			if (parentSequence != null) parentSequence.MakeDirty(true);
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

		public LORMember4 Clone()
		{
			LORCosmic cosm = (LORCosmic)this.Clone(myName);
			return cosm;
		}

		public LORMember4 Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			LORCosmic cosm = new LORCosmic(myName, lutils.UNDEFINED);
			cosm.myCentiseconds = myCentiseconds;
			cosm.myIndex = myIndex;
			cosm.mySavedIndex = mySavedIndex;
			cosm.myAltSavedIndex = myAltSavedIndex;
			cosm.imSelected = imSelected;

			return cosm;
		}

		public object Tag
		{
			get
			{
				return myTag;
			}
			set
			{
				myTag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.Cosmic)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}
		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }

		public LORCosmic(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new LORMembership4(this);
		}

		public LORCosmic(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new LORMembership4(this);
			string seek = lutils.STFLD + LORSequence4.TABLEcosmicDevice + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEcosmicDevice);

			ret.Append(lutils.FIELDtotalCentiseconds);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDname);
			ret.Append(lutils.FIELDEQ);
			ret.Append(lutils.XMLifyName(myName));
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.FIELDsavedIndex);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF); ;
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			foreach (LORMember4 member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > lutils.UNDEFINED)
				{
					ret.Append(lutils.CRLF);
					ret.Append(lutils.LEVEL4);
					ret.Append(lutils.STFLD);
					ret.Append(LORSequence4.TABLEchannelGroup);

					ret.Append(lutils.FIELDsavedIndex);
					ret.Append(lutils.FIELDEQ);
					ret.Append(asi.ToString());
					ret.Append(lutils.ENDQT);
					ret.Append(lutils.ENDFLD);
				}
			}
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEchannelGroup);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEcosmicDevice);
			ret.Append(lutils.FINFLD);

			return ret.ToString();
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
					string sMsg = newPart.Name + " has already been added to this Channel Group '" + myName + "'.";
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
				if (parentSequence != null) parentSequence.MakeDirty(true);
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			LORMember4 newPart = parentSequence.Members.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		public int UniverseNumber
		{
			get
			{
				return 0;
			}
		}
		public int DMXAddress
		{
			get
			{
				return 0;
			}
		}
		//TODO: add RemoveItem procedure
	}

	public class LORTimings4 : LORMember4, IComparable<LORMember4>
	{
		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int mySaveID = lutils.UNDEFINED;
		protected int myAltSaveID = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		protected bool isDirty = false;

		public LORUtils4.LORTimingGridType4 LORTimingGridType4 = LORUtils4.LORTimingGridType4.None;
		public int spacing = lutils.UNDEFINED;
		public List<int> timings = new List<int>();
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected bool matchExact = false;

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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				return myCentiseconds;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						myCentiseconds = value;
						if (parentSequence != null) parentSequence.MakeDirty(true);

						if (myCentiseconds > Parent.Centiseconds)
						{
							if (value > lutils.MINCentiseconds)
							{
								Parent.Centiseconds = value;
							}
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int SavedIndex
		{
			get
			{
				//int n = Parent.Channels.Count + Parent.RGBchannels.Count + Parent.ChannelGroups.Count + Parent.Tracks.Count;
				//return n + myIndex;
				int n = -mySaveID -1;
				if (LORSequence4.isWiz)
				{
					if (System.Diagnostics.Debugger.IsAttached)
					{
						string msg = "Why is something trying to get the SavedIndex of a Timing Grid?";
						System.Diagnostics.Debugger.Break();
					}
				}

				return n;
			}
		}

		public void SetSavedIndex(int theSaveID)
		{
			//mySavedIndex = theSaveID;
			//if (parentSequence != null) parentSequence.MakeDirty(true);
			//! We should not attempt to set the SavedIndex of a timing grid, set it's SaveID instead
			System.Diagnostics.Debugger.Break();
			if (theSaveID < 0)
			{
				mySaveID = Math.Abs(theSaveID) - 1;
			}
			else
			{
				mySaveID = theSaveID;
			}
		}

		public int AltSavedIndex
		// Required for iMember compatibility
		{
			get
			{
				// Check Call Stack - consider changing call
				//System.Diagnostics.Debugger.Break();
				int n = -myAltSaveID-1;
				return n;
			}
			set
			{
				// Check Call Stack - consider changing call
				//System.Diagnostics.Debugger.Break();
				if (value < 0)
				{
					myAltSaveID = Math.Abs(value) - 1;
				}
				else
				{
					myAltSaveID = value;
				}
			}
		}

		public int AltSaveID
		{
			get
			{
				return myAltSaveID;
			}
			set
			{
				myAltSaveID = Math.Abs(value);
			}
		}

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}
		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}

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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Timings;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
					{
						result = myAltSaveID.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEtimingGrid);

			ret.Append(lutils.SetKey(FIELDsaveID, myAltSaveID));
			if (myName.Length > 1)
			{
				ret.Append(lutils.SetKey(lutils.FIELDname, lutils.XMLifyName(myName)));
			}
			ret.Append(lutils.SetKey(lutils.FIELDtype, LORSeqEnums4.TimingName(this.LORTimingGridType4)));
			if (spacing > 1)
			{
				ret.Append(lutils.SetKey(FIELDspacing, spacing));
			}
			if (this.LORTimingGridType4 == LORUtils4.LORTimingGridType4.FixedGrid)
			{
				ret.Append(lutils.ENDFLD);
			}
			else if (this.LORTimingGridType4 == LORUtils4.LORTimingGridType4.Freeform)
			{
				ret.Append(lutils.FINFLD);

				//foreach (int tm in timings)
				for (int tm = 0; tm < timings.Count; tm++)
				{
					ret.Append(lutils.CRLF);
					ret.Append(lutils.LEVEL3);
					ret.Append(lutils.STFLD);
					ret.Append(TABLEtiming);

					ret.Append(lutils.SetKey(lutils.FIELDcentisecond, timings[tm]));
					ret.Append(lutils.ENDFLD);
				}

				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL2);
				ret.Append(lutils.FINTBL);
				ret.Append(LORSequence4.TABLEtimingGrid);
				ret.Append(lutils.FINFLD);
			}

			return ret.ToString();
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySaveID = lutils.getKeyValue(lineIn, FIELDsaveID);
			Centiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			this.LORTimingGridType4 = LORSeqEnums4.EnumGridType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
			spacing = lutils.getKeyValue(lineIn, FIELDspacing);
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSaveID > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Clone()
		{
			LORTimings4 grid = (LORTimings4)this.Clone(myName);
			return grid;
		}

		public LORMember4 Clone(string newName)
		{
			LORTimings4 grid = new LORTimings4(newName, lutils.UNDEFINED);
			grid.myCentiseconds = myCentiseconds;
			grid.myIndex = myIndex;
			grid.mySaveID = mySaveID; // SaveID
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
				return mySaveID;
			}
			set
			{
				mySaveID = value;
				if (parentSequence != null) parentSequence.MakeDirty(true);
				//System.Diagnostics.Debugger.Break();
			}
		}

		public object Tag
		{
			get
			{
				return myTag;
			}
			set
			{
				myTag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.Timings)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }

		public LORTimings4(string lineIn)
		{
#if DEBUG
			//string msg = "LORTimings4.LORTimings4(" + lineIn + ") // Constructor";
			//Debug.WriteLine(msg);
#endif
			string seek = lutils.STFLD + LORSequence4.TABLEtimingGrid + FIELDsaveID;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
			mySaveID = theSaveID;
		}


		public void AddTiming(int time)
		{
			if (timings.Count == 0)
			{
				timings.Add(time);
				if (parentSequence != null) parentSequence.MakeDirty(true);
			}
			else
			{
				if (timings[timings.Count - 1] < time)
				{
					timings.Add(time);
					if (parentSequence != null) parentSequence.MakeDirty(true);
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
					if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
			return count;
		}
		public int UniverseNumber
		{
			get
			{
				return 0;
			}
		}
		public int DMXAddress
		{
			get
			{
				return 0;
			}
		}


	} // end timingGrid class

	public class LORTrack4 : LORMember4, IComparable<LORMember4>
	{
		// Tracks are the ultimate top-level groups.  Level 2 and up are handled by 'ChannelGroups'
		// Channel groups are optional, a sequence many not have any groups, but it will always have at least one track
		// Tracks do not have savedIndexes.  They are just numbered instead.
		// Tracks can contain regular Channels, RGB Channels, and Channel Groups, but not other tracks
		// (ie: Tracks cannot be nested like Channel Groups (which can be nested many levels deep))
		// All Channel Groups, regular Channels, and RGB Channels must directly or indirectly belong to one or more tracks.
		// Channels, RGB Channels, and channel groups will not be displayed and will not be accessible unless added to one or
		// more tracks, directly or subdirectly (a subitem of a group in a track).
		// A LORTrack4 may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// (See related notes in the LORChannelGroup4 class)

		protected string myName = "";
		protected int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		//private int mySavedIndex = lutils.UNDEFINED;
		protected int myAltTrackIndex = lutils.UNDEFINED;
		protected int tempGridSaveID = lutils.UNDEFINED;
		protected LORSequence4 parentSequence = null;
		protected bool imSelected = false;
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected bool matchExact = false;
		protected bool isDirty = false;

		public LORMembership4 Members; // = new LORMembership4(null);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int Centiseconds
		{
			get
			{
				//return myCentiseconds;
				int cs = 0;
				for (int idx = 0; idx < Members.Count; idx++)
				{
					LORMember4 mbr = Members[idx];
					if (Members.Items[idx].Centiseconds > cs)
					{
						cs = Members.Items[idx].Centiseconds;
						myCentiseconds = cs;
					}
				}
				return cs;
			}
			set
			{
				if (value != myCentiseconds)
				{
					if (value > lutils.MAXCentiseconds)
					{
						string m = "WARNING!! Setting Centiseconds to more than 60 minutes!  Are you sure?";
						Fyle.WriteLogEntry(m, "Warning");
						if (Fyle.isWiz)
						{
							System.Diagnostics.Debugger.Break();
						}
					}
					else
					{
						if (value < lutils.MINCentiseconds)
						{
							string m = "WARNING!! Setting Centiseconds to less than 1 second!  Are you sure?";
							Fyle.WriteLogEntry(m, "Warning");
							if (Fyle.isWiz)
							{
								System.Diagnostics.Debugger.Break();
							}
						}
						else
						{
							myCentiseconds = value;
							for (int idx = 0; idx < Members.Count; idx++)
							{
								LORMember4 mbr = Members[idx];
								if (Members.Items[idx].Centiseconds > value)
								{
									Members.Items[idx].Centiseconds = value;
								}
							}

							if (myCentiseconds > Parent.Centiseconds)
							{
								//Parent.Centiseconds = value;
							}
							if (parentSequence != null) parentSequence.MakeDirty(true);
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
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public int SavedIndex
		{
			get
			{
				//int n = Parent.Channels.Count + Parent.RGBchannels.Count + Parent.ChannelGroups.Count;
				int n = -myIndex - 1;
				if (LORSequence4.isWiz)
				{
					if (System.Diagnostics.Debugger.IsAttached)
					{
						string msg = "Why is something trying to get the SavedIndex of a LORTrack4?";
						//System.Diagnostics.Debugger.Break();
					}
				}
				return n;
			}
		}

		public void SetSavedIndex(int theTrackNumber)
		{
			//mySavedIndex = theTrackNumber;
			//if (parentSequence != null) parentSequence.MakeDirty(true);
			//! Why are we trying to set the SavedIndex of a track?!?
			System.Diagnostics.Debugger.Break();
			//mySavedIndex = Math.Abs(theTrackNumber);
		}

		public int AltSavedIndex
		{
			get
			{
				return -myAltTrackIndex;
			}
			set
			{
				if (value < 0)
				{
					myAltTrackIndex = Math.Abs(value) - 1;
				}
				else
				{
					myAltTrackIndex = value;
				}
			}
		}

		public bool Dirty
		{	get { return isDirty; }	}
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentSequence != null)
				{
					parentSequence.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}

		public LORMember4 Parent
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				Type t = newParent.GetType();
				if (t.Equals(typeof(LORSequence4)))
				{
					this.parentSequence = (LORSequence4)newParent;
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Why are we trying to assign something other than a sequence?!?!
						System.Diagnostics.Debugger.Break();
					}
				}
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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Track;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (LORMembership4.sortMode == LORMembership4.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (LORMembership4.sortMode == LORMembership4.SORTbyAltSavedIndex)
					{
						result = myAltTrackIndex.CompareTo(other.AltSavedIndex);
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
			this.Members = new LORMembership4(this);
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			//mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDtotalCentiseconds);
			tempGridSaveID = lutils.getKeyValue(lineIn, LORSequence4.TABLEtimingGrid);
			if (this.parentSequence == null)
			{
				// If the parent has not been assigned yet, there is no way to get ahold of the grid
				// So temporarily set the AltSavedIndex to this LORTrack4's LORTimings4's SaveID
				//myAltSavedIndex = tempGridSaveID;
			}
			else
			{
				if (tempGridSaveID < 0)
				{
					// For Channel Configs, there will be no timing grid
					timingGrid = null;
				}
				else
				{
					// Assign the LORTimings4 based on the SaveID
					//LORMember4 member = parentSequence.Members.bySaveID[tempGridSaveID];
					LORTimings4 tg = null;
					for (int i=0; i< parentSequence.TimingGrids.Count; i++)
					{
						if (parentSequence.TimingGrids[i].SaveID == tempGridSaveID)
						{
							tg = parentSequence.TimingGrids[i];
							i = parentSequence.TimingGrids.Count; // force exit of loop
						}
					}
					if (tg == null)
					{
						string msg = "ERROR: Timing Grid with SaveID of " + tempGridSaveID.ToString() + " not found!";
						System.Diagnostics.Debugger.Break();
					}
					timingGrid = tg;
				}
			}
			if (parentSequence != null) parentSequence.MakeDirty(true);
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltTrackIndex > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public LORMember4 Clone()
		{
			LORTrack4 tr = (LORTrack4)this.Clone(myName);
			return tr;
		}

		public LORMember4 Clone(string newName)
		{
			LORTrack4 tr = new LORTrack4(newName, lutils.UNDEFINED);
			tr.myCentiseconds = myCentiseconds;
			tr.myIndex = myIndex;
			tr.myAltTrackIndex = myAltTrackIndex;
			tr.tempGridSaveID = tempGridSaveID;
			tr.imSelected = imSelected;
			tr.timingGrid = (LORTimings4)timingGrid.Clone();

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
			//	if (parentSequence != null) parentSequence.MakeDirty(true);
			//}
		}

		public object Tag
		{
			get
			{
				return myTag;
			}
			set
			{
				myTag = value;
			}
		}

		public LORMember4 MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == LORMemberType4.Track)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public bool ExactMatch
		{ get { return matchExact; } set { matchExact = value; } }

		public LORTrack4(string theName, int theTrackNo)
		{
			Members = new LORMembership4(this);
			myName = theName;
			myIndex= theTrackNo -1;  // Tracks are numbered starting with 1, Indexes start with 0
			//Members.Parent = ID.Parent;
		}

		public LORTrack4(string lineIn)
		{
			Members = new LORMembership4(this);
			string seek = lutils.STFLD + LORSequence4.TABLEtrack + lutils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
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
			StringBuilder ret = new StringBuilder();
			// Write info about track
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LORSequence4.TABLEtrack);
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += lutils.SPC + FIELDname + lutils.FIELDEQ + Name + lutils.ENDQT;
			//}
			ret.Append(lutils.FIELDtotalCentiseconds);
			ret.Append(lutils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(lutils.ENDQT);

			int altID = timingGrid.AltSavedIndex;
			ret.Append(lutils.SPC);
			ret.Append(LORSequence4.TABLEtimingGrid);
			ret.Append(lutils.FIELDEQ);
			ret.Append(altID.ToString());
			ret.Append(lutils.ENDQT);
			// LOR writes it with the Name last
			if (myName.Length > 1)
			{
				ret.Append(lutils.FIELDname);
				ret.Append(lutils.FIELDEQ);
				ret.Append(lutils.XMLifyName(myName));
				ret.Append(lutils.ENDQT);
			}
			ret.Append(lutils.FINFLD);

			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.STFLD);
			ret.Append(lutils.TABLEchannel);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			// LORLoop4 thru all items in this track
			foreach (LORMember4 subID in Members.Items)
			{
				bool sel = subID.Selected;
				if (!selectedOnly || sel)
				{
					// Write out the links to the items
					//masterSI = updatedTracks[trackIndex].newSavedIndexes[iti];

					//if (subID.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

					int siAlt = subID.AltSavedIndex;
					if (siAlt > lutils.UNDEFINED)
					{
						ret.Append(lutils.CRLF);
						ret.Append(lutils.LEVEL4);
						ret.Append(lutils.STFLD);
						ret.Append(lutils.TABLEchannel);

						ret.Append(lutils.FIELDsavedIndex);
						ret.Append(lutils.FIELDEQ);
						ret.Append(siAlt.ToString());
						ret.Append(lutils.ENDQT);
						ret.Append(lutils.ENDFLD);
					}
				}
			}

			// Close the list of items
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL3);
			ret.Append(lutils.FINTBL);
			ret.Append(lutils.TABLEchannel);
			ret.Append(lutils.PLURAL);
			ret.Append(lutils.FINFLD);

			// Write out any LoopLevels in this track
			//writeLoopLevels(trackIndex);
			if (loopLevels.Count > 0)
			{
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.STFLD);
				ret.Append(LORSequence4.TABLEloopLevels);
				ret.Append(lutils.FINFLD);
				foreach (LORLoopLevel4 ll in loopLevels)
				{
					ret.Append(lutils.CRLF);
					ret.Append(ll.LineOut());
				}
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.FINTBL);
				ret.Append(LORSequence4.TABLEloopLevels);
				ret.Append(lutils.FINFLD);
			}
			else
			{
				ret.Append(lutils.CRLF);
				ret.Append(lutils.LEVEL3);
				ret.Append(lutils.STFLD);
				ret.Append(LORSequence4.TABLEloopLevels);
				ret.Append(lutils.ENDFLD);
			}
			ret.Append(lutils.CRLF);
			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.FINTBL);
			ret.Append(LORSequence4.TABLEtrack);
			ret.Append(lutils.FINFLD);


			return ret.ToString();
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
				if (parentSequence != null) parentSequence.MakeDirty(true);
			}
			return retSI;
		}

		public int AddItem(int itemSavedIndex)
		{
			int ret = lutils.UNDEFINED;
			if (parentSequence != null)
			{
				LORMember4 newItem = parentSequence.Members.FindBySavedIndex(itemSavedIndex);
				if (newItem != null)
				{
					ret = AddItem(newItem);
					parentSequence.MakeDirty(true);
				}
				else
				{
					if (Fyle.isWiz)
					{
						// Trying to add a member which does not exist!
						System.Diagnostics.Debugger.Break();
					}
				}
			}
			return ret;
		}

		public LORLoopLevel4 AddLoopLevel(string lineIn)
		{
			LORLoopLevel4 newLL = new LORLoopLevel4(lineIn);
			AddLoopLevel(newLL);
			if (parentSequence != null) parentSequence.MakeDirty(true);
			return newLL;
		}

		public int AddLoopLevel(LORLoopLevel4 newLL)
		{
			loopLevels.Add(newLL);
			if (parentSequence != null) parentSequence.MakeDirty(true);
			return loopLevels.Count - 1;
		}

		public int UniverseNumber
		{
			get
			{
				return 0;
			}
		}
		public int DMXAddress
		{
			get
			{
				return 0;
			}
		}

		//TODO: add RemoveItem procedure
	} // end class track

	public class LORMembership4 : IEnumerator, IEnumerable  //  IEnumerator<LORMember4>
	{
		public List<LORMember4> Items = new List<LORMember4>();   // The Main List
		public List<LORMember4> bySavedIndex = new List<LORMember4>();
		public List<LORMember4> byAltSavedIndex = new List<LORMember4>();
		public List<LORTimings4> bySaveID = new List<LORTimings4>();
		public List<LORTimings4> byAltSaveID = new List<LORTimings4>();
		public List<LORTrack4> byTrackIndex = new List<LORTrack4>();
		public List<LORTrack4> byAltTrackIndex = new List<LORTrack4>();
		public SortedDictionary<string, LORMember4> byName = new SortedDictionary<string, LORMember4>();
		//public SortedList<string, LORMember4> byName = new SortedList<string, LORMember4>();

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
		public static int sortMode = SORTbySavedIndex;

		public int channelCount = 0;
		public int rgbChannelCount = 0;
		public int channelGroupCount = 0;
		public int cosmicDeviceCount = 0;
		public int trackCount = 0;
		public int timingGridCount = 0;
		public int allCount = 0;
		private int position = 0;

		//LORMember4 IEnumerator<LORMember4>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();

		public LORMembership4(LORMember4 myOwner)
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

		
		// LORMembership4.Add(Member)
		public int Add(LORMember4 newMember)
		{
			int memberSI = lutils.UNDEFINED;
			//#if DEBUG
			//	string msg = "LORMembership4.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//	Debug.WriteLine(msg);
			//#endif
			//#if DEBUG
			//	if ((newMember.SavedIndex == 6) || (newMember.MemberType == LORMemberType4.Timings))
			//	{
			//		string msg = "LORMembership4.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//		msg += " to owner " + this.owner.Name;
			//		Debug.WriteLine(msg);
				// Check to make sure timing grid is not being added to a channel group (while initial reading of file)
				// If so, trace stack, find out why
					//System.Diagnostics.Debugger.Break();
			//	}
			//#endif
			if (this.parentSequence == null)
			{
				if (newMember.Parent == null)
				{
					if (Fyle.isWiz)
					{
						//System.Diagnostics.Debugger.Break();
					}
				}
				else
				{
					this.parentSequence = (LORSequence4)newMember.Parent;
				}
			}
			else
			{
				newMember.SetParent(this.parentSequence);
			}


			if (parentSequence != null) parentSequence.MakeDirty(true);
			if ((newMember.MemberType == LORMemberType4.Channel) ||
				  (newMember.MemberType == LORMemberType4.RGBChannel) ||
				  (newMember.MemberType == LORMemberType4.ChannelGroup) ||
				  (newMember.MemberType == LORMemberType4.Cosmic))
			{
				// Reminder, Must be a member which really has a SavedIndex to hit this point
				memberSI = newMember.SavedIndex;
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
				
				if (newMember.MemberType == LORMemberType4.Channel)
				{
					//byAlphaChannelNames.Add(newMember);
					channelCount++;
				}
				if (newMember.MemberType == LORMemberType4.RGBChannel)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					rgbChannelCount++;
				}
				if (newMember.MemberType == LORMemberType4.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					channelGroupCount++;
				}
				if (newMember.MemberType == LORMemberType4.Cosmic)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					cosmicDeviceCount++;
				}

				while ((bySavedIndex.Count-1) < memberSI)
				{
					bySavedIndex.Add(null);
					byAltSavedIndex.Add(null);
				}
				bySavedIndex[memberSI] = newMember;
				byAltSavedIndex[memberSI] = newMember;
				Items.Add(newMember);
			}

			if (newMember.MemberType == LORMemberType4.Track)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newMember);
				//bySavedIndex.Add(newMember);
				//byAltSavedIndex.Add(newMember);
				LORTrack4 tr = (LORTrack4)newMember;
				int trackIdx = tr.Index;
				if (trackIdx < 0) // Sanity Check
				{
					System.Diagnostics.Debugger.Break();
				}
				while ((byTrackIndex.Count -1) < trackIdx)
				{
					byTrackIndex.Add(null);
					byAltTrackIndex.Add(null);
				}
				byTrackIndex[trackIdx] = tr;
				byAltTrackIndex[trackIdx] = tr;
				trackCount++;
			}

			if (newMember.MemberType == LORMemberType4.Timings)
			{
				LORTimings4 tg = (LORTimings4)newMember;
				int gridSID = tg.SaveID;
				if (gridSID < 0)
				{
					highestSaveID++;
					gridSID = highestSaveID;
					//newMember.SetSavedIndex(memberSI);
					tg.SaveID = gridSID;
				}
				if (gridSID > highestSaveID)
				{
					highestSaveID = gridSID;
				}
				while ((bySaveID.Count - 1) < gridSID)
				{
					bySaveID.Add(null);
					byAltSaveID.Add(null);
				}
				//! Exception Here!  memberSI not set!  (=-1 Undefined)
				//bySaveID[memberSI] = tg;
				bySaveID[tg.SaveID] = tg;
				//byAltSaveID[memberSI] = tg;
				byAltSaveID[tg.SaveID] = tg;
				timingGridCount++;
			}

			if (owner.MemberType == LORMemberType4.Sequence)
			{
				bool need2add = false; // Reset
															 //if (newMember.SavedIndex < 0)
															 //{
															 //	need2add = true;
															 //}
				if ((newMember.MemberType == LORMemberType4.Channel) ||
						(newMember.MemberType == LORMemberType4.RGBChannel) ||
						(newMember.MemberType == LORMemberType4.ChannelGroup) ||
						(newMember.MemberType == LORMemberType4.Cosmic))
				{
					memberSI = newMember.SavedIndex;
					//LORSequence4 myParentSeq = (LORSequence4)Parent;
					if (memberSI > parentSequence.Members.highestSavedIndex)
					{
						need2add = true;
					}
				}
				// Else new member is LORTrack4 or Timing Grid
				if (newMember.MemberType == LORMemberType4.Track)
				{
					LORTrack4 newTrack = (LORTrack4)newMember;
					int trkIdx = newTrack.Index;
					if (trkIdx > parentSequence.Tracks.Count)
					{
						need2add = true;
					}
				}
				if (newMember.MemberType == LORMemberType4.Timings)
				{
					LORTimings4 newGrid = (LORTimings4)newMember;
					int gridSaveID = newGrid.SaveID;
					if (gridSaveID > parentSequence.Members.highestSaveID)
					{ 
						need2add = true;
					}
				}


				if (need2add)
				{
					if (newMember.MemberType == LORMemberType4.Channel)
					{
						parentSequence.Channels.Add((LORChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.RGBChannel)
					{
						parentSequence.RGBchannels.Add((LORRGBChannel4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.ChannelGroup)
					{
						parentSequence.ChannelGroups.Add((LORChannelGroup4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Cosmic)
					{
						parentSequence.CosmicDevices.Add((LORCosmic)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Track)
					{
						parentSequence.Tracks.Add((LORTrack4)newMember);
					}
					if (newMember.MemberType == LORMemberType4.Timings)
					{
						parentSequence.TimingGrids.Add((LORTimings4)newMember);
					}

					Items.Add(newMember);
					allCount++;
				} // end if need2add
			} // end if owner is the sequenced

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

		public bool Includes(string memberName)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m=0; m< Items.Count; m++)
			{
				LORMember4 member = Items[m];
				if (member.Name.CompareTo(memberName) == 0)
				{
					found = true;
					m = Items.Count; // Exit loop
				}
			}
			return found;
		}

		public bool Includes(int savedIndex)
		{
			//! NOTE: Does NOT check sub-groups!
			bool found = false;
			for (int m = 0; m < Items.Count; m++)
			{
				LORMember4 member = Items[m];
				if (member.SavedIndex == savedIndex)
				{
					found = true;
					m = Items.Count; // Exit loop
				}
			}
			return found;
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

		public int SelectedMemberCount
		{
			// Besides getting the number of selected members and submembers
			// it also 'cleans up' the selection states

			get
			{
				int count = 0;
				if (this.owner.Selected)
				{
					foreach (LORMember4 m in Items)
					{
						if (m.MemberType == LORMemberType4.Channel)
						{
							if (m.Selected) count++;
						}
						if (m.MemberType == LORMemberType4.RGBChannel)
						{
							if (m.Selected)
							{
								int subCount = 0;
								LORRGBChannel4 r = (LORRGBChannel4)m;
								if (r.redChannel.Selected) subCount++;
								if (r.grnChannel.Selected) subCount++;
								if (r.bluChannel.Selected) subCount++;
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									//subCount++;
									count += subCount;
								}
							}
						}
						if (m.MemberType == LORMemberType4.ChannelGroup)
						{
							if (m.Selected)
							{
								LORChannelGroup4 g = (LORChannelGroup4)m;
								int subCount = g.Members.SelectedMemberCount;  // Recurse!
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									count += subCount;
								}
							}
						}
						if (m.MemberType == LORMemberType4.Cosmic)
						{
							if (m.Selected)
							{
								LORCosmic d = (LORCosmic)m;
								int subCount = d.Members.SelectedMemberCount;  // Recurse!
								if (subCount == 0)
								{
									m.Selected = false;
								}
								else
								{
									//m.Selected = true;
									count += subCount;
								}
							}
						}
					}
					if (count == 0)
					{
						if (this.owner != null)
						{
							this.owner.Selected = false;
						}
						else
						{
							//this.owner.Selected = true;
						}
					}
				}
				return count;
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
					itemName = LORSeqEnums4.MemberName(member.MemberType) + " " + member.Index.ToString();
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

				if (member.MemberType == LORMemberType4.Channel)
				{
					//byAlphaChannelNames.Add(member);
					//channelNames[channelCount] = member;
					byAltSavedIndex.Add(member);
					channelCount++;
				}
				else
				{
					if (member.MemberType == LORMemberType4.RGBChannel)
					{
						//byAlphaRGBchannelNames.Add(member);
						//rgbChannelNames[rgbChannelCount] = member;
						byAltSavedIndex.Add(member);
						rgbChannelCount++;
					}
					else
					{
						if (member.MemberType == LORMemberType4.ChannelGroup)
						{
							//byAlphaChannelGroupNames.Add(member);
							//channelGroupNames[channelGroupCount] = member;
							byAltSavedIndex.Add(member);
							channelGroupCount++;
						}
						else
						{
							if (member.MemberType == LORMemberType4.Cosmic)
							{
								//byAlphaChannelGroupNames.Add(member);
								//channelGroupNames[channelGroupCount] = member;
								byAltSavedIndex.Add(member);
								cosmicDeviceCount++;
							}
							else
							{
								if (member.MemberType == LORMemberType4.Track)
								{
									//byAlphaTrackNames.Add(member);
									//trackNames[trackCount] = member;
									byAltSavedIndex.Add(member);
									trackCount++;
								}
								else
								{
									if (member.MemberType == LORMemberType4.Timings)
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
				//AlphaSortSavedIndexes(byTimingGridName, 0, Parent.TimingGrids.Count - 1);
				//byAlphaTimingGridNames.Sort();
			}

		} // end ReIndex

		private void NULLITEMCHECK()
		{
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i] == null) System.Diagnostics.Debugger.Break();
				if (Items[i].Parent == null) System.Diagnostics.Debugger.Break();
				int v = Items[i].CompareTo(Items[0]);
			}

		}


		// LORMembership4.find(name, type, create)
		public LORMember4 Find(string theName, LORMemberType4 theType, bool createIfNotFound)
		{
#if DEBUG
			string msg = "LORMembership4.find(" + theName + ", ";
			msg += theType.ToString() + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			LORMember4 ret = null;
			if (ret==null)
			{
				if (this.parentSequence == null) this.parentSequence = (LORSequence4)owner.Parent;
				if (parentSequence != null)
				{
					if (theType == LORMemberType4.Channel)
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
					if (theType == LORMemberType4.RGBChannel)
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
					if (theType == LORMemberType4.ChannelGroup)
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
						if (theType == LORMemberType4.Cosmic)
						{
							if (cosmicDeviceCount > 0)
							{
								ret = FindByName(theName, theType);
							}
							if ((ret == null) && createIfNotFound)
							{
								ret = parentSequence.CreateCosmicDevice(theName);
								Add(ret);
							}
						}
						if (theType == LORMemberType4.Timings)
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
					if (theType == LORMemberType4.Track)
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
				if (ret.MemberType != PartType)
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
				LORMemberType4 t = Items[l].MemberType;
				if (t == LORMemberType4.Channel)
				{
					if (countPlain)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == LORMemberType4.RGBChannel)
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
				if (t == LORMemberType4.ChannelGroup)
				{
					LORChannelGroup4 grp = (LORChannelGroup4)Items[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == LORMemberType4.Cosmic)
				{
					LORCosmic dev = (LORCosmic)Items[l];
					// Recurse!!
					c += dev.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
			}


			return c;
		}

		public LORMember4 Parent
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
		// ~LORMembership4() {
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


	} // end LORMembership4 Class (Collection)




}
