using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LOR4Utils
{

	public class LOR4Channel : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
	{
		private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;
		public Int32 color = 0;
		public LOR4Output output = new LOR4Output();
		public LOR4RGBChild rgbChild = LOR4RGBChild.None;
		public LOR4RGBChannel rgbParent = null;
		public List<LOR4Effect> effects = new List<LOR4Effect>();
		public const string FIELDcolor = " color";

		//! CONSTRUCTORS
		public LOR4Channel(string theName, int theSavedIndex)
		{
			myName = theName;
			mySavedIndex = theSavedIndex;
		}
		public LOR4Channel(string lineIn)
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


		//! METHODS, PROPERTIES, ETC.

		public new LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.Channel;
			}
		}

		public new int CompareTo(iLOR4Member other)
		{
			int result = 0;
			if (LOR4Membership.sortMode == LOR4Membership.SORTbyOutput)
			{
				Type t = other.GetType();
				if (t == typeof(LOR4Channel))
				{
					LOR4Channel ch = (LOR4Channel)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
				if (t == typeof(LOR4VizChannel))
				{
					LOR4VizChannel ch = (LOR4VizChannel)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
			}
			else
			{
				result = base.CompareTo(other);
			}
			return result;
		}

		// I'm not a big fan of case sensitivity, but I'm gonna take advantage of it here
		// color with lower c is the LOR color, a 32 bit int in BGR order
		// Color with capital C is the .Net Color object (aka Web Color)
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

		public new string LineOut()
		{
			return LineOut(false);
		}

		public new void Parse(string lineIn)
		{
			//LOR4Sequence Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			color = lutils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			output.Parse(lineIn);
			//if (myParent != null) myParent.MakeDirty(true);
		}

		public new iLOR4Member Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LOR4Channel chan = (LOR4Channel)base.Clone();
			chan.color = color;
			chan.output = output.Clone();
			chan.rgbChild = this.rgbChild;
			chan.rgbParent = rgbParent; // should be changed/overridden
			chan.effects = CloneEffects();
			return chan;
		}

		public new iLOR4Member Clone(string newName)
		{
			//LOR4Channel chan = new LOR4Channel(newName, lutils.UNDEFINED);
			LOR4Channel chan = (LOR4Channel)base.Clone();
			chan.ChangeName(newName);
			return chan;
		}

		public List<LOR4Effect> CloneEffects()
		{
			List<LOR4Effect> newList = new List<LOR4Effect>();
			foreach (LOR4Effect ef in effects)
			{
				LOR4Effect F = ef.Clone();
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
				foreach (LOR4Effect thisEffect in effects)
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


		public void CopyTo(LOR4Channel destination, bool withEffects)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.myParent == null) destination.myParent = this.myParent;
			//if (destination.output.deviceType == LOR4DeviceType.None)
			//{
			destination.output.deviceType = output.deviceType;
			destination.output.circuit = output.circuit;
			destination.output.network = output.network;
			destination.output.unit = output.unit;
			//}
			if (withEffects)
			{
				//destination.Centiseconds = myCentiseconds;
				//foreach (LOR4Effect thisEffect in effects)
				//{
				//	destination.effects.Add(thisEffect.Clone());
				//}
				destination.effects = CloneEffects();
			}
		}

		public void CopyFrom(LOR4Channel source, bool withEffects)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.myParent == null) this.myParent = source.myParent;
			//if (output.deviceType == LOR4DeviceType.None)
			//{
			output.deviceType = source.output.deviceType;
			output.circuit = source.output.circuit;
			output.network = source.output.network;
			output.unit = source.output.unit;
			//}
			if (withEffects)
			{
				//myCentiseconds = source.Centiseconds;
				//foreach (LOR4Effect theEffect in source.effects)
				//{
				//effects.Add(theEffect.Clone());
				//	AddEffect(theEffect.Clone());
				//}
				effects = source.CloneEffects();
			}
		}

		public LOR4Channel Clone(bool withEffects)
		{
			// See Also: Duplicate()
			//int nextSI = ID.Parent.Members.highestSavedIndex + 1;
			LOR4Channel ret = new LOR4Channel(myName, lutils.UNDEFINED);
			ret.color = color;
			ret.output = output;
			ret.rgbChild = rgbChild;
			ret.rgbParent = rgbParent;
			ret.SetParent(Parent);
			List<LOR4Effect> newEffects = new List<LOR4Effect>();
			if (withEffects)
			{
				//foreach (LOR4Effect thisEffect in effects)
				//{
				//	newEffects.Add(thisEffect.Clone());
				//}
				//ret.effects = newEffects;
				ret.effects = CloneEffects();
			}
			return ret;
		}

		public void AddEffect(LOR4Effect theEffect)
		{
			theEffect.parent = this;
			theEffect.myIndex = effects.Count;
			effects.Add(theEffect);
			if (theEffect.endCentisecond > myCentiseconds)
			{
				Centiseconds = theEffect.endCentisecond;
			}
			if (myParent != null) myParent.MakeDirty(true);
			//ID.Parent.dirty = true;
		}

		public void AddEffect(string lineIn)
		{
			LOR4Effect theEffect = new LOR4Effect(lineIn);
			AddEffect(theEffect);
			if (myParent != null) myParent.MakeDirty(true);
		}

		public int CopyEffects(List<LOR4Effect> effectList, bool Merge)
		{
			LOR4Effect newEffect = null;
			if (!Merge)
			{
				//! Note: clears any pre-existing effects!
				effects.Clear();
			}
			foreach (LOR4Effect thisEffect in effectList)
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
			if (myParent != null) myParent.MakeDirty(true);
			return effects.Count;
		}

		//TODO: add RemoveEffect procedure
		//TODO: add SortEffects procedure (by startCentisecond)
	} // end channel
}
