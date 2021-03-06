using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace LORUtils
{
	public interface IMember : IComparable<IMember>
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
		Sequence4 ParentSequence
		{
			get;
		}
		void SetParentSeq(Sequence4 newParentSeq);
		//List <IMember> parents
		//{
		//	get;
		//}
		bool Selected
		{
			get;
			set;
		}
		MemberType MemberType
		{
			get;
		}
		int CompareTo(IMember otherObj);
		string LineOut();
		string ToString();
		void Parse(string lineIn);
		bool Written
		{
			get;
		}
		IMember Clone();
		IMember Clone(string newName);
		object Tag
		{
			get;
			set;
		}
		IMember MapTo
		{
			get;
			set;
		}

	}

	public class Channel : IMember, IComparable<IMember>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
		private const string STARTchannel = utils.STFLD + utils.TABLEchannel + utils.FIELDname;

		public Int32 color = 0;
		public Output output = new Output();
		public RGBchild rgbChild = RGBchild.None;
		public RGBchannel rgbParent = null;
		public List<Effect> effects = new List<Effect>();
		private object tag = null;
		IMember mappedTo = null;

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
				if (value != myCentiseconds)
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.Channel;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (parentSequence.Members.sortMode == Membership.SORTbyOutput)
						{
							Channel ch = (Channel)other;
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
				return utils.Color_LORtoNet(color);
			}
			set
			{
				color = utils.Color_NettoLOR(value);
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
			//Sequence4 ParentSequence = ID.ParentSequence;
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
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

		public IMember Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			Channel chan = (Channel)this.Clone(myName);
			return chan;
		}

		public IMember Clone(string newName)
		{
			Channel chan = new Channel(newName, utils.UNDEFINED);
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

		public List<Effect> CloneEffects()
		{
			List<Effect> newList = new List<Effect>();
			foreach(Effect ef in effects)
			{
				Effect F = ef.Clone();
				newList.Add(F);
			}
			return newList;
		}

		public string LineOut(bool noEffects)
		{
			StringBuilder ret = new StringBuilder();
			ret.Append(utils.LEVEL2);
			ret.Append(utils.STFLD);
			ret.Append(utils.TABLEchannel);

			ret.Append(utils.FIELDname);
			ret.Append(utils.FIELDEQ);
			ret.Append(utils.XMLifyName(myName));
			ret.Append(utils.ENDQT);

			ret.Append(FIELDcolor);
			ret.Append(utils.FIELDEQ);
			ret.Append(color.ToString());
			ret.Append(utils.ENDQT);

			ret.Append(utils.FIELDcentiseconds);
			ret.Append(utils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(utils.ENDQT);

			ret.Append(output.LineOut());

			ret.Append(utils.FIELDsavedIndex);
			ret.Append(utils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(utils.ENDQT);

			// Are there any effects for this channel?
			if (!noEffects && (effects.Count > 0))
			{
				// complete channel line with regular '>' then do effects
				ret.Append(utils.FINFLD);
				foreach (Effect thisEffect in effects)
				{
					ret.Append(utils.CRLF);
					ret.Append(thisEffect.LineOut());
				}
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL2);
				ret.Append(utils.FINTBL);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.FINFLD);
			}
			else // NO effects for this channal
			{
				// complete channel line with field end '/>'
				ret.Append(utils.ENDFLD);
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

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.Channel)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}


		public Channel(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public Channel(string lineIn)
		{
			string seek = utils.STFLD + utils.TABLEchannel + utils.FIELDname;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
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

		public void CopyTo(Channel destination, bool withEffects)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.parentSequence == null) destination.parentSequence = this.parentSequence;
			if (destination.output.deviceType == DeviceType.None)
			{
				destination.output.deviceType = output.deviceType;
				destination.output.circuit = output.circuit;
				destination.output.network = output.network;
				destination.output.unit = output.unit;
			}
			if (withEffects)
			{
				destination.Centiseconds = myCentiseconds;
				foreach (Effect thisEffect in effects)
				{
					destination.effects.Add(thisEffect.Clone());
				}
			}
		}

		public void CopyFrom(Channel source, bool withEffects)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.parentSequence == null) this.parentSequence = source.parentSequence;
			if (output.deviceType == DeviceType.None)
			{
				output.deviceType = source.output.deviceType;
				output.circuit = source.output.circuit;
				output.network = source.output.network;
				output.unit = source.output.unit;
			}
			if (withEffects)
			{
				myCentiseconds = source.Centiseconds;
				foreach (Effect theEffect in source.effects)
				{
					//effects.Add(theEffect.Clone());
					AddEffect(theEffect.Clone());
				}
			}
		}

		public Channel Clone(bool withEffects)
		{
			// See Also: Duplicate()
			//int nextSI = ID.ParentSequence.Members.highestSavedIndex + 1;
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
					newEffects.Add(thisEffect.Clone());
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
				newEffect = thisEffect.Clone();
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

	public class RGBchannel : IMember, IComparable<IMember>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
		private object tag = null;
		private IMember mappedTo = null;

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
				if (value != myCentiseconds)
				{
					myCentiseconds = value;

					if (redChannel != null)
					{
						redChannel.Centiseconds = value;
					}
					if (grnChannel != null)
					{
						grnChannel.Centiseconds = value;
					}
					if (bluChannel != null)
					{
						bluChannel.Centiseconds = value;
					}

					if (parentSequence != null) parentSequence.MakeDirty();

					//if (myCentiseconds > ParentSequence.Centiseconds)
					//{
					//	ParentSequence.Centiseconds = value;
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.RGBchannel;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
					{
						result = myAltSavedIndex.CompareTo(other.AltSavedIndex);
					}
				}
			}
			return result;
		}

		public string LineOut()
		{
			return LineOut(false, false, MemberType.FullTrack);
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
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

		public IMember Clone()
		{
			RGBchannel rgb = (RGBchannel)this.Clone(myName);
			return rgb;
		}

		public IMember Clone(string newName)
		{
			RGBchannel rgb = new RGBchannel(newName, utils.UNDEFINED);
			rgb.myCentiseconds = myCentiseconds;
			rgb.myIndex = myIndex;
			rgb.mySavedIndex = mySavedIndex;
			rgb.myAltSavedIndex = myAltSavedIndex;
			rgb.imSelected = imSelected;

			// These are placeholders so you can get the Index, SavedIndex, Selected state, etc.
			// Either add these subchannels to the [new] sequence BEFORE adding the parent RGB
			// or replace them with new ones
			rgb.redChannel = (Channel)redChannel.Clone();
			rgb.grnChannel = (Channel)grnChannel.Clone();
			rgb.bluChannel = (Channel)bluChannel.Clone();

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

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.RGBchannel)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public RGBchannel(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}

		public RGBchannel(string lineIn)
		{
			string seek = utils.STFLD + Sequence4.TABLErgbChannel + utils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}


		public string LineOut(bool selectedOnly, bool noEffects, MemberType itemTypes)
		{
			StringBuilder ret = new StringBuilder();

			//int redSavedIndex = utils.UNDEFINED;
			//int grnSavedIndex = utils.UNDEFINED;
			//int bluSavedIndex = utils.UNDEFINED;

			int AltSavedIndex = utils.UNDEFINED;

			if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.Channel))
			// Type NONE actually means ALL in this case
			{
				// not checking .Selected flag 'cuz if parent RGBchannel is Selected 
				//redSavedIndex = ID.ParentSequence.WriteChannel(redChannel, noEffects);
				//grnSavedIndex = ID.ParentSequence.WriteChannel(grnChannel, noEffects);
				//bluSavedIndex = ID.ParentSequence.WriteChannel(bluChannel, noEffects);
			}

			if ((itemTypes == MemberType.Items) || (itemTypes == MemberType.RGBchannel))
			{
				ret.Append(utils.LEVEL2);
				ret.Append(utils.STFLD);
				ret.Append(Sequence4.TABLErgbChannel);
				ret.Append(utils.FIELDtotalCentiseconds);
				ret.Append(utils.FIELDEQ);
				ret.Append(myCentiseconds.ToString());
				ret.Append(utils.ENDQT);

				ret.Append(utils.FIELDname);
				ret.Append(utils.FIELDEQ);
				ret.Append(utils.XMLifyName(myName));
				ret.Append(utils.ENDQT);

				ret.Append(utils.FIELDsavedIndex);
				ret.Append(utils.FIELDEQ);
				ret.Append(myAltSavedIndex.ToString());
				ret.Append(utils.ENDQT);

				ret.Append(utils.FINFLD);

				// Start SubChannels
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL3);
				ret.Append(utils.STFLD);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.PLURAL);
				ret.Append(utils.FINFLD);

				// RED subchannel
				AltSavedIndex = redChannel.AltSavedIndex;
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL4);
				ret.Append(utils.STFLD);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.FIELDsavedIndex);
				ret.Append(utils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(utils.ENDQT);
				ret.Append(utils.ENDFLD);

				// GREEN subchannel
				AltSavedIndex = grnChannel.AltSavedIndex;
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL4);
				ret.Append(utils.STFLD);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.FIELDsavedIndex);
				ret.Append(utils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(utils.ENDQT);
				ret.Append(utils.ENDFLD);

				// BLUE subchannel
				AltSavedIndex = bluChannel.AltSavedIndex;
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL4);
				ret.Append(utils.STFLD);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.FIELDsavedIndex);
				ret.Append(utils.FIELDEQ);
				ret.Append(AltSavedIndex.ToString());
				ret.Append(utils.ENDQT);
				ret.Append(utils.ENDFLD);

				// End SubChannels
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL3);
				ret.Append(utils.FINTBL);
				ret.Append(utils.TABLEchannel);
				ret.Append(utils.PLURAL);
				ret.Append(utils.FINFLD);

				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL2);
				ret.Append(utils.FINTBL);
				ret.Append(Sequence4.TABLErgbChannel);
				ret.Append(utils.FINFLD);
			} // end MemberType Channel or RGBchannel

			return ret.ToString();
		} // end LineOut

	} // end RGBchannel Class

	public class ChannelGroup : IMember, IComparable<IMember>
	{
		// Channel Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All Channel Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// Channel groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the Track class)

		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
		public const string TABLEchannelGroupList = "channelGroupList";
		private const string STARTchannelGroup = utils.STFLD + TABLEchannelGroupList + utils.SPC;
		private object tag = null;
		private IMember mappedTo = null;

		public Membership Members; // = new Membership(this);

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
				if (value != myCentiseconds)
				{
					myCentiseconds = value;
					for (int idx = 0; idx < Members.Count; idx++)
					{
						Members.Items[idx].Centiseconds = value;
					}
					if (parentSequence != null) parentSequence.MakeDirty();

					//if (myCentiseconds > ParentSequence.Centiseconds)
					//{
					//	ParentSequence.Centiseconds = value;
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

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.ChannelGroup;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
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
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			if (myName.IndexOf("inese 27") > 0)
			{
				//System.Diagnostics.Debugger.Break();
			}
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			Members = new Membership(this);
			//Members = new Membership(this);
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

		public IMember Clone()
		{
			ChannelGroup grp = (ChannelGroup)this.Clone(myName);
			return grp;
		}

		public IMember Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			ChannelGroup grp = new ChannelGroup(myName, utils.UNDEFINED);
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
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.ChannelGroup)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public ChannelGroup(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new Membership(this);
		}

		public ChannelGroup(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new Membership(this);
			string seek = utils.STFLD + Sequence4.TABLEchannelGroupList + utils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
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

			ret.Append(utils.LEVEL2);
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEchannelGroupList);

			ret.Append(utils.FIELDtotalCentiseconds);
			ret.Append(utils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(utils.ENDQT);

			ret.Append(utils.FIELDname);
			ret.Append(utils.FIELDEQ);
			ret.Append(utils.XMLifyName(myName));
			ret.Append(utils.ENDQT);

			ret.Append(utils.FIELDsavedIndex);
			ret.Append(utils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(utils.ENDQT);
			ret.Append(utils.FINFLD);

			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL3);
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEchannelGroup);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			foreach (IMember member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > utils.UNDEFINED)
				{
					ret.Append(utils.CRLF);
					ret.Append(utils.LEVEL4);
					ret.Append(utils.STFLD);
					ret.Append(Sequence4.TABLEchannelGroup);

					ret.Append(utils.FIELDsavedIndex);
					ret.Append(utils.FIELDEQ);
					ret.Append(asi.ToString());
					ret.Append(utils.ENDQT);
					ret.Append(utils.ENDFLD);
				}
			}
			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL3);
			ret.Append(utils.FINTBL);
			ret.Append(Sequence4.TABLEchannelGroup);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL2);
			ret.Append(utils.FINTBL);
			ret.Append(Sequence4.TABLEchannelGroupList);
			ret.Append(utils.FINFLD);

			return ret.ToString();
		}

		public int AddItem(IMember newPart)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
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
			IMember newPart = parentSequence.Members.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		//TODO: add RemoveItem procedure
	}

	public class CosmicDevice : IMember, IComparable<IMember>
	{
		// Channel Groups are Level 2 and Up, Level 1 is the Tracks (which are similar to a group)
		// Channel Groups can contain regular Channels, RGB Channels, and other groups.
		// Groups can be nested many levels deep (limit?).
		// Channels and other groups may be in more than one group.
		// A group may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// Don't create circular references of groups in each other.
		// All Channel Groups (and regular Channels and RGB Channels) must directly or indirectly belong to a track
		// Channel groups are optional, a sequence many not have any groups, but it will have at least one track
		// (See related notes in the Track class)

		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySavedIndex = utils.UNDEFINED;
		private int myAltSavedIndex = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
		public const string TABLEcosmicDeviceDevice = "colorCosmicDevice";
		private const string STARTcosmicDevice = utils.STFLD + TABLEcosmicDeviceDevice + utils.SPC;
		private object tag = null;
		private IMember mappedTo = null;

		public Membership Members; // = new Membership(this);

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
				if (value != myCentiseconds)
				{
					myCentiseconds = value;
					for (int idx = 0; idx < Members.Count; idx++)
					{
						Members.Items[idx].Centiseconds = value;
					}
					if (parentSequence != null) parentSequence.MakeDirty();

					//if (myCentiseconds > ParentSequence.Centiseconds)
					//{
					//	ParentSequence.Centiseconds = value;
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

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.CosmicDevice;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = mySavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
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
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			Members = new Membership(this);
			//Members = new Membership(this);
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

		public IMember Clone()
		{
			CosmicDevice cosm = (CosmicDevice)this.Clone(myName);
			return cosm;
		}

		public IMember Clone(string newName)
		{
			// Returns an EMPTY group with same name, index, centiseconds, etc.
			CosmicDevice cosm = new CosmicDevice(myName, utils.UNDEFINED);
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
				return tag;
			}
			set
			{
				tag = value;
			}
		}

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.CosmicDevice)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}

		public CosmicDevice(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
			Members = new Membership(this);
		}

		public CosmicDevice(string lineIn)
		{
			//int li = lineIn.IndexOf(STARTchannelGroup);
			Members = new Membership(this);
			string seek = utils.STFLD + Sequence4.TABLEcosmicDevice + utils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
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

			ret.Append(utils.LEVEL2);
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEcosmicDevice);

			ret.Append(utils.FIELDtotalCentiseconds);
			ret.Append(utils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(utils.ENDQT);

			ret.Append(utils.FIELDname);
			ret.Append(utils.FIELDEQ);
			ret.Append(utils.XMLifyName(myName));
			ret.Append(utils.ENDQT);

			ret.Append(utils.FIELDsavedIndex);
			ret.Append(utils.FIELDEQ);
			ret.Append(myAltSavedIndex.ToString());
			ret.Append(utils.ENDQT);
			ret.Append(utils.FINFLD);

			ret.Append(utils.CRLF); ;
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEchannelGroup);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			foreach (IMember member in Members.Items)
			{
				int osi = member.SavedIndex;
				int asi = member.AltSavedIndex;
				if (asi > utils.UNDEFINED)
				{
					ret.Append(utils.CRLF);
					ret.Append(utils.LEVEL4);
					ret.Append(utils.STFLD);
					ret.Append(Sequence4.TABLEchannelGroup);

					ret.Append(utils.FIELDsavedIndex);
					ret.Append(utils.FIELDEQ);
					ret.Append(asi.ToString());
					ret.Append(utils.ENDQT);
					ret.Append(utils.ENDFLD);
				}
			}
			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL3);
			ret.Append(utils.FINTBL);
			ret.Append(Sequence4.TABLEchannelGroup);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL2);
			ret.Append(utils.FINTBL);
			ret.Append(Sequence4.TABLEcosmicDevice);
			ret.Append(utils.FINFLD);

			return ret.ToString();
		}

		public int AddItem(IMember newPart)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
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
			IMember newPart = parentSequence.Members.bySavedIndex[itemSavedIndex];
			return AddItem(newPart);
		}

		//TODO: add RemoveItem procedure
	}

	public class TimingGrid : IMember, IComparable<IMember>
	{
		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		private int mySaveID = utils.UNDEFINED;
		private int myAltSaveID = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;

		public LORUtils.TimingGridType TimingGridType = LORUtils.TimingGridType.None;
		public int spacing = utils.UNDEFINED;
		public List<int> timings = new List<int>();
		private object tag = null;
		private IMember mappedTo = null;

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
				if (value != myCentiseconds)
				{
					// Not used, but save it anyway, what the heck
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
				//int n = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count + ParentSequence.Tracks.Count;
				//return n + myIndex;
				int n = -mySaveID -1;
				if (utils.IsWizard)
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
			//if (parentSequence != null) parentSequence.MakeDirty();
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


		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.TimingGrid;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
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

			ret.Append(utils.LEVEL2);
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEtimingGrid);

			ret.Append(utils.SetKey(FIELDsaveID, myAltSaveID));
			if (myName.Length > 1)
			{
				ret.Append(utils.SetKey(utils.FIELDname, utils.XMLifyName(myName)));
			}
			ret.Append(utils.SetKey(utils.FIELDtype, SeqEnums.TimingName(this.TimingGridType)));
			if (spacing > 1)
			{
				ret.Append(utils.SetKey(FIELDspacing, spacing));
			}
			if (this.TimingGridType == LORUtils.TimingGridType.FixedGrid)
			{
				ret.Append(utils.ENDFLD);
			}
			else if (this.TimingGridType == LORUtils.TimingGridType.Freeform)
			{
				ret.Append(utils.FINFLD);

				//foreach (int tm in timings)
				for (int tm = 0; tm < timings.Count; tm++)
				{
					ret.Append(utils.CRLF);
					ret.Append(utils.LEVEL3);
					ret.Append(utils.STFLD);
					ret.Append(TABLEtiming);

					ret.Append(utils.SetKey(utils.FIELDcentisecond, timings[tm]));
					ret.Append(utils.ENDFLD);
				}

				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL2);
				ret.Append(utils.FINTBL);
				ret.Append(Sequence4.TABLEtimingGrid);
				ret.Append(utils.FINFLD);
			}

			return ret.ToString();
		}

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			mySaveID = utils.getKeyValue(lineIn, FIELDsaveID);
			Centiseconds = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
			this.TimingGridType = SeqEnums.enumGridType(utils.getKeyWord(lineIn, utils.FIELDtype));
			spacing = utils.getKeyValue(lineIn, FIELDspacing);
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltSaveID > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public IMember Clone()
		{
			TimingGrid grid = (TimingGrid)this.Clone(myName);
			return grid;
		}

		public IMember Clone(string newName)
		{
			TimingGrid grid = new TimingGrid(newName, utils.UNDEFINED);
			grid.myCentiseconds = myCentiseconds;
			grid.myIndex = myIndex;
			grid.mySaveID = mySaveID; // SaveID
			grid.TimingGridType = this.TimingGridType;
			grid.spacing = spacing;
			if (this.TimingGridType == LORUtils.TimingGridType.Freeform)
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
				if (parentSequence != null) parentSequence.MakeDirty();
				//System.Diagnostics.Debugger.Break();
			}
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

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.TimingGrid)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}


		public TimingGrid(string lineIn)
		{
#if DEBUG
			//string msg = "TimingGrid.TimingGrid(" + lineIn + ") // Constructor";
			//Debug.WriteLine(msg);
#endif
			string seek = utils.STFLD + Sequence4.TABLEtimingGrid + FIELDsaveID;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				Parse(lineIn);
			}
			else
			{
				myName = lineIn;
			}
		}

		public TimingGrid(string theName, int theSaveID)
		{
			myName = theName;
			mySaveID = theSaveID;
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

	public class Track : IMember, IComparable<IMember>
	{
		// Tracks are the ultimate top-level groups.  Level 2 and up are handled by 'ChannelGroups'
		// Channel groups are optional, a sequence many not have any groups, but it will always have at least one track
		// Tracks do not have savedIndexes.  They are just numbered instead.
		// Tracks can contain regular Channels, RGB Channels, and Channel Groups, but not other tracks
		// (ie: Tracks cannot be nested like Channel Groups (which can be nested many levels deep))
		// All Channel Groups, regular Channels, and RGB Channels must directly or indirectly belong to one or more tracks.
		// Channels, RGB Channels, and channel groups will not be displayed and will not be accessible unless added to one or
		// more tracks, directly or subdirectly (a subitem of a group in a track).
		// A Track may not contain more than one copy of the same item-- directly.  Within a subgroup is OK.
		// (See related notes in the ChannelGroup class)

		private string myName = "";
		private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		//private int mySavedIndex = utils.UNDEFINED;
		private int myAltTrackIndex = utils.UNDEFINED;
		private int tempGridSaveID = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		private bool imSelected = false;
		private object tag = null;
		private IMember mappedTo = null;

		public Membership Members; // = new Membership(null);
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
				if (value != myCentiseconds)
				{
					myCentiseconds = value;

					for (int idx=0; idx< Members.Count; idx++)
					{
						Members.Items[idx].Centiseconds = value;
					}

					if (myCentiseconds > ParentSequence.Centiseconds)
					{
						ParentSequence.Centiseconds = value;
					}
					if (parentSequence != null) parentSequence.MakeDirty();
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
				//int n = ParentSequence.Channels.Count + ParentSequence.RGBchannels.Count + ParentSequence.ChannelGroups.Count;
				int n = -myIndex - 1;
				if (utils.IsWizard)
				{
					if (System.Diagnostics.Debugger.IsAttached)
					{
						string msg = "Why is something trying to get the SavedIndex of a Track?";
						//System.Diagnostics.Debugger.Break();
					}
				}
				return n;
			}
		}

		public void SetSavedIndex(int theTrackNumber)
		{
			//mySavedIndex = theTrackNumber;
			//if (parentSequence != null) parentSequence.MakeDirty();
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

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
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

		public MemberType MemberType
		{
			get
			{
				return MemberType.Track;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			if (parentSequence.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = SavedIndex.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentSequence.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentSequence.Members.sortMode == Membership.SORTbyAltSavedIndex)
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
			this.Members = new Membership(this);
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			//mySavedIndex = utils.getKeyValue(lineIn, utils.FIELDsavedIndex);
			myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDtotalCentiseconds);
			tempGridSaveID = utils.getKeyValue(lineIn, Sequence4.TABLEtimingGrid);
			if (this.parentSequence == null)
			{
				// If the parent has not been assigned yet, there is no way to get ahold of the grid
				// So temporarily set the AltSavedIndex to this Track's TimingGrid's SaveID
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
					// Assign the TimingGrid based on the SaveID
					//IMember member = parentSequence.Members.bySaveID[tempGridSaveID];
					TimingGrid tg = null;
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
			if (parentSequence != null) parentSequence.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltTrackIndex > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public IMember Clone()
		{
			Track tr = (Track)this.Clone(myName);
			return tr;
		}

		public IMember Clone(string newName)
		{
			Track tr = new Track(newName, utils.UNDEFINED);
			tr.myCentiseconds = myCentiseconds;
			tr.myIndex = myIndex;
			tr.myAltTrackIndex = myAltTrackIndex;
			tr.tempGridSaveID = tempGridSaveID;
			tr.imSelected = imSelected;
			tr.timingGrid = (TimingGrid)timingGrid.Clone();

			return tr;
		}

		public int TrackNumber
		{
			get
			{
				// Track numbers are one based, the index is zero based, so just add 1 to the index for the track number
				return myIndex + 1;
			}
			// Read-Only!
			//set
			//{
			//	myIndex = value;
			//	if (parentSequence != null) parentSequence.MakeDirty();
			//}
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

		public IMember MapTo
		{
			get
			{
				return mappedTo;
			}
			set
			{
				if (value.MemberType == MemberType.Track)
				{
					mappedTo = value;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
				}
			}
		}


		public Track(string theName, int theTrackNo)
		{
			Members = new Membership(this);
			myName = theName;
			myIndex= theTrackNo -1;  // Tracks are numbered starting with 1, Indexes start with 0
			//Members.ParentSequence = ID.ParentSequence;
		}

		public Track(string lineIn)
		{
			Members = new Membership(this);
			string seek = utils.STFLD + Sequence4.TABLEtrack + utils.FIELDtotalCentiseconds;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.ContainsKey(lineIn, seek);
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
			ret.Append(utils.LEVEL2);
			ret.Append(utils.STFLD);
			ret.Append(Sequence4.TABLEtrack);
			//! LOR writes it with the Name last
			// In theory, it shouldn't matter
			//if (Name.Length > 1)
			//{
			//	ret += utils.SPC + FIELDname + utils.FIELDEQ + Name + utils.ENDQT;
			//}
			ret.Append(utils.FIELDtotalCentiseconds);
			ret.Append(utils.FIELDEQ);
			ret.Append(myCentiseconds.ToString());
			ret.Append(utils.ENDQT);

			int altID = timingGrid.AltSavedIndex;
			ret.Append(utils.SPC);
			ret.Append(Sequence4.TABLEtimingGrid);
			ret.Append(utils.FIELDEQ);
			ret.Append(altID.ToString());
			ret.Append(utils.ENDQT);
			// LOR writes it with the Name last
			if (myName.Length > 1)
			{
				ret.Append(utils.FIELDname);
				ret.Append(utils.FIELDEQ);
				ret.Append(utils.XMLifyName(myName));
				ret.Append(utils.ENDQT);
			}
			ret.Append(utils.FINFLD);

			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL3);
			ret.Append(utils.STFLD);
			ret.Append(utils.TABLEchannel);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			// Loop thru all items in this track
			foreach (IMember subID in Members.Items)
			{
				bool sel = subID.Selected;
				if (!selectedOnly || sel)
				{
					// Write out the links to the items
					//masterSI = updatedTracks[trackIndex].newSavedIndexes[iti];

					//if (subID.Name.IndexOf("lyphonic") > 0) System.Diagnostics.Debugger.Break();

					int siAlt = subID.AltSavedIndex;
					if (siAlt > utils.UNDEFINED)
					{
						ret.Append(utils.CRLF);
						ret.Append(utils.LEVEL4);
						ret.Append(utils.STFLD);
						ret.Append(utils.TABLEchannel);

						ret.Append(utils.FIELDsavedIndex);
						ret.Append(utils.FIELDEQ);
						ret.Append(siAlt.ToString());
						ret.Append(utils.ENDQT);
						ret.Append(utils.ENDFLD);
					}
				}
			}

			// Close the list of items
			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL3);
			ret.Append(utils.FINTBL);
			ret.Append(utils.TABLEchannel);
			ret.Append(utils.PLURAL);
			ret.Append(utils.FINFLD);

			// Write out any LoopLevels in this track
			//writeLoopLevels(trackIndex);
			if (loopLevels.Count > 0)
			{
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL3);
				ret.Append(utils.STFLD);
				ret.Append(Sequence4.TABLEloopLevels);
				ret.Append(utils.FINFLD);
				foreach (LoopLevel ll in loopLevels)
				{
					ret.Append(utils.CRLF);
					ret.Append(ll.LineOut());
				}
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL3);
				ret.Append(utils.FINTBL);
				ret.Append(Sequence4.TABLEloopLevels);
				ret.Append(utils.FINFLD);
			}
			else
			{
				ret.Append(utils.CRLF);
				ret.Append(utils.LEVEL3);
				ret.Append(utils.STFLD);
				ret.Append(Sequence4.TABLEloopLevels);
				ret.Append(utils.ENDFLD);
			}
			ret.Append(utils.CRLF);
			ret.Append(utils.LEVEL2);
			ret.Append(utils.FINTBL);
			ret.Append(Sequence4.TABLEtrack);
			ret.Append(utils.FINFLD);


			return ret.ToString();
		}

		public int AddItem(IMember newPart)
		{
			int retSI = utils.UNDEFINED;
			bool alreadyAdded = false;
			for (int i = 0; i < Members.Count; i++)
			{
				if (newPart.SavedIndex == Members.Items[i].SavedIndex)
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
			IMember newPart = ParentSequence.Members.bySavedIndex[itemSavedIndex];
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

	public class Membership : IEnumerator, IEnumerable  //  IEnumerator<IMember>
	{
		public List<IMember> Items = new List<IMember>();   // The Main List
		public List<IMember> bySavedIndex = new List<IMember>();
		public List<IMember> byAltSavedIndex = new List<IMember>();
		public List<TimingGrid> bySaveID = new List<TimingGrid>();
		public List<TimingGrid> byAltSaveID = new List<TimingGrid>();
		public List<Track> byTrackIndex = new List<Track>();
		public List<Track> byAltTrackIndex = new List<Track>();
		public SortedDictionary<string, IMember> byName = new SortedDictionary<string, IMember>();
		//public SortedList<string, IMember> byName = new SortedList<string, IMember>();

		private int highestSavedIndex = utils.UNDEFINED;
		public int altHighestSavedIndex = utils.UNDEFINED;
		private int highestSaveID = utils.UNDEFINED;
		public int altHighestSaveID = utils.UNDEFINED;
		private Sequence4 parentSequence = null;
		public IMember owner;

		public const int SORTbySavedIndex = 1;
		public const int SORTbyAltSavedIndex = 2;
		public const int SORTbyName = 3;
		public const int SORTbyOutput = 4;
		public int sortMode = SORTbySavedIndex;

		public int channelCount = 0;
		public int rgbChannelCount = 0;
		public int channelGroupCount = 0;
		public int cosmicDeviceCount = 0;
		public int trackCount = 0;
		public int timingGridCount = 0;
		public int allCount = 0;
		private int position = 0;

		//IMember IEnumerator<IMember>.Current => throw new NotImplementedException();

		//object IEnumerator.Current => throw new NotImplementedException();

		public Membership(IMember myOwner)
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

		
		// Membership.Add(Member)
		public int Add(IMember newMember)
		{
			int memberSI = utils.UNDEFINED;
			//#if DEBUG
			//	string msg = "Membership.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//	Debug.WriteLine(msg);
			//#endif
			//#if DEBUG
			//	if ((newMember.SavedIndex == 6) || (newMember.MemberType == MemberType.TimingGrid))
			//	{
			//		string msg = "Membership.Add(" + newMember.Name + ":" + newMember.MemberType.ToString() + ")";
			//		msg += " to owner " + this.owner.Name;
			//		Debug.WriteLine(msg);
				// Check to make sure timing grid is not being added to a channel group (while initial reading of file)
				// If so, trace stack, find out why
					//System.Diagnostics.Debugger.Break();
			//	}
			//#endif
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


			if (parentSequence != null) parentSequence.MakeDirty();
			if ((newMember.MemberType == MemberType.Channel) ||
				  (newMember.MemberType == MemberType.RGBchannel) ||
				  (newMember.MemberType == MemberType.ChannelGroup) ||
				  (newMember.MemberType == MemberType.CosmicDevice))
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
				
				if (newMember.MemberType == MemberType.Channel)
				{
					//byAlphaChannelNames.Add(newMember);
					channelCount++;
				}
				if (newMember.MemberType == MemberType.RGBchannel)
				{
					//byAlphaRGBchannelNames.Add(newMember);
					rgbChannelCount++;
				}
				if (newMember.MemberType == MemberType.ChannelGroup)
				{
					//byAlphaChannelGroupNames.Add(newMember);
					channelGroupCount++;
				}
				if (newMember.MemberType == MemberType.CosmicDevice)
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

			if (newMember.MemberType == MemberType.Track)
			{
				// No special handling of SavedIndex for Tracks
				// Tracks don't use saved Indexes
				// but they get assigned one anyway for matching purposes
				//byAlphaTrackNames.Add(newMember);
				//bySavedIndex.Add(newMember);
				//byAltSavedIndex.Add(newMember);
				Track tr = (Track)newMember;
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

			if (newMember.MemberType == MemberType.TimingGrid)
			{
				TimingGrid tg = (TimingGrid)newMember;
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

			if (owner.MemberType == MemberType.Sequence)
			{
				bool need2add = false; // Reset
															 //if (newMember.SavedIndex < 0)
															 //{
															 //	need2add = true;
															 //}
				if ((newMember.MemberType == MemberType.Channel) ||
						(newMember.MemberType == MemberType.RGBchannel) ||
						(newMember.MemberType == MemberType.ChannelGroup) ||
						(newMember.MemberType == MemberType.CosmicDevice))
				{
					memberSI = newMember.SavedIndex;
					if (memberSI > ParentSequence.Members.highestSavedIndex)
					{
						need2add = true;
					}
				}
				// Else new member is Track or Timing Grid
				if (newMember.MemberType == MemberType.Track)
				{
					Track newTrack = (Track)newMember;
					int trkIdx = newTrack.Index;
					if (trkIdx > parentSequence.Tracks.Count)
					{
						need2add = true;
					}
				}
				if (newMember.MemberType == MemberType.TimingGrid)
				{
					TimingGrid newGrid = (TimingGrid)newMember;
					int gridSaveID = newGrid.SaveID;
					if (gridSaveID > parentSequence.Members.highestSaveID)
					{ 
						need2add = true;
					}
				}


				if (need2add)
				{
					if (newMember.MemberType == MemberType.Channel)
					{
						ParentSequence.Channels.Add((Channel)newMember);
					}
					if (newMember.MemberType == MemberType.RGBchannel)
					{
						ParentSequence.RGBchannels.Add((RGBchannel)newMember);
					}
					if (newMember.MemberType == MemberType.ChannelGroup)
					{
						ParentSequence.ChannelGroups.Add((ChannelGroup)newMember);
					}
					if (newMember.MemberType == MemberType.CosmicDevice)
					{
						ParentSequence.CosmicDevices.Add((CosmicDevice)newMember);
					}
					if (newMember.MemberType == MemberType.Track)
					{
						ParentSequence.Tracks.Add((Track)newMember);
					}
					if (newMember.MemberType == MemberType.TimingGrid)
					{
						ParentSequence.TimingGrids.Add((TimingGrid)newMember);
					}

					Items.Add(newMember);
					allCount++;
				} // end if need2add
			} // end if owner is the sequenced

			return memberSI;
		}

		// For iEnumerable
		public IMember this[int index]
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

		public int SelectedMemberCount
		{
			// Besides getting the number of selected members and submembers
			// it also 'cleans up' the selection states

			get
			{
				int count = 0;
				if (this.owner.Selected)
				{
					foreach (IMember m in Items)
					{
						if (m.MemberType == MemberType.Channel)
						{
							if (m.Selected) count++;
						}
						if (m.MemberType == MemberType.RGBchannel)
						{
							if (m.Selected)
							{
								int subCount = 0;
								RGBchannel r = (RGBchannel)m;
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
						if (m.MemberType == MemberType.ChannelGroup)
						{
							if (m.Selected)
							{
								ChannelGroup g = (ChannelGroup)m;
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
						if (m.MemberType == MemberType.CosmicDevice)
						{
							if (m.Selected)
							{
								CosmicDevice d = (CosmicDevice)m;
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

			byName = new SortedDictionary<string, IMember>();
			byAltSavedIndex = new List<IMember>();
			bySaveID = new List<TimingGrid>();
			byAltSaveID = new List<TimingGrid>();

			for (int i = 0; i < Items.Count; i++)
			{
				IMember member = Items[i];

				int n = 2;
				string itemName = member.Name;
				// Check for blank name (common with Tracks and TimingGrids if not changed/set by the user)
				if (itemName == "")
				{
					// Make up a name based on type and index
					itemName = SeqEnums.MemberName(member.MemberType) + " " + member.Index.ToString();
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

				if (member.MemberType == MemberType.Channel)
				{
					//byAlphaChannelNames.Add(member);
					//channelNames[channelCount] = member;
					byAltSavedIndex.Add(member);
					channelCount++;
				}
				else
				{
					if (member.MemberType == MemberType.RGBchannel)
					{
						//byAlphaRGBchannelNames.Add(member);
						//rgbChannelNames[rgbChannelCount] = member;
						byAltSavedIndex.Add(member);
						rgbChannelCount++;
					}
					else
					{
						if (member.MemberType == MemberType.ChannelGroup)
						{
							//byAlphaChannelGroupNames.Add(member);
							//channelGroupNames[channelGroupCount] = member;
							byAltSavedIndex.Add(member);
							channelGroupCount++;
						}
						else
						{
							if (member.MemberType == MemberType.CosmicDevice)
							{
								//byAlphaChannelGroupNames.Add(member);
								//channelGroupNames[channelGroupCount] = member;
								byAltSavedIndex.Add(member);
								cosmicDeviceCount++;
							}
							else
							{
								if (member.MemberType == MemberType.Track)
								{
									//byAlphaTrackNames.Add(member);
									//trackNames[trackCount] = member;
									byAltSavedIndex.Add(member);
									trackCount++;
								}
								else
								{
									if (member.MemberType == MemberType.TimingGrid)
									{
										//byAlphaTimingGridNames.Add(member);
										//timingGridNames[timingGridCount] = member;
										byAltSavedIndex.Add(member);
										TimingGrid tg = (TimingGrid)member;
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


		// Membership.find(name, type, create)
		public IMember Find(string theName, MemberType theType, bool createIfNotFound)
		{
#if DEBUG
			string msg = "Membership.find(" + theName + ", ";
			msg += theType.ToString() + ", " + createIfNotFound.ToString() + ")";
			Debug.WriteLine(msg);
#endif
			IMember ret = null;
			if (ret==null)
			{
				if (this.parentSequence == null) this.parentSequence = owner.ParentSequence;
				if (parentSequence != null)
				{
					if (theType == MemberType.Channel)
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
					if (theType == MemberType.RGBchannel)
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
					if (theType == MemberType.ChannelGroup)
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
						if (theType == MemberType.CosmicDevice)
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
						if (theType == MemberType.TimingGrid)
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
					if (theType == MemberType.Track)
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



		public IMember FindBySavedIndex(int theSavedIndex)
		{
			IMember ret = bySavedIndex[theSavedIndex];
			return ret;
		}

		private IMember FindByName(string theName, MemberType PartType)
		{
			//IMember ret = null;
			//ret = FindByName(theName, PartType, 0, 0, 0, 0, false);
			if (byName.TryGetValue(theName, out IMember ret))
			{
				// Found the name, is the type correct?
				if (ret.MemberType != PartType)
				{
					ret = null;
				}
			}
			return ret;
		}

		private static IMember FindByName(string theName, List<IMember> Members)
		{
			IMember ret = null;
			int idx = BinarySearch(theName, Members);
			if (idx > utils.UNDEFINED)
			{
				ret= Members[idx];
			}
			return ret;
		}

		private static int BinarySearch(string theName, List<IMember> Members)
		{
			return TreeSearch(theName, Members, 0, Members.Count - 1);
		}

		private static int TreeSearch(string theName, List<IMember> Members, int start, int end)
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
			foreach(IMember member in bySavedIndex)
			{
				member.AltSavedIndex = utils.UNDEFINED;
				//member.Written = false;
			}
			altHighestSavedIndex = utils.UNDEFINED;
			altHighestSaveID = utils.UNDEFINED;
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
				MemberType t = Items[l].MemberType;
				if (t == MemberType.Channel)
				{
					if (countPlain)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
				}
				if (t == MemberType.RGBchannel)
				{
					if (countRGBparents)
					{
						if (Items[l].Selected || !selectedOnly) c++;
					}
					if (countRGBchildren)
					{
						RGBchannel rgbCh = (RGBchannel)Items[l];
						if (rgbCh.redChannel.Selected || !selectedOnly) c++;
						if (rgbCh.grnChannel.Selected || !selectedOnly) c++;
						if (rgbCh.bluChannel.Selected || !selectedOnly) c++;
					}
				}
				if (t == MemberType.ChannelGroup)
				{
					ChannelGroup grp = (ChannelGroup)Items[l];
					// Recurse!!
					c += grp.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
				if (t == MemberType.CosmicDevice)
				{
					CosmicDevice dev = (CosmicDevice)Items[l];
					// Recurse!!
					c += dev.Members.DescendantCount(selectedOnly, countPlain, countRGBparents, countRGBchildren);
				}
			}


			return c;
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return parentSequence;
			}
		}

		public void SetParentSequence(Sequence4 theParent)
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
		// ~Membership() {
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


	} // end Membership Class (Collection)




}
