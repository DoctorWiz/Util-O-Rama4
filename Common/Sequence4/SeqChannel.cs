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

	public class LORChannel4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;
		public int color = 0;
		public LOROutput4 output = new LOROutput4();
		public LORRGBChild4 rgbChild = LORRGBChild4.None;
		public LORRGBChannel4 rgbParent = null;
		public List<LOREffect4> effects = new List<LOREffect4>();
		public const string FIELDcolor = " color";

		//! CONSTRUCTORS
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
				if (lineIn.Length > 0)
				{
					myName = lineIn;
				}
			}
		}


		//! METHODS, PROPERTIES, ETC.

		public new LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.Channel;
			}
		}

		public new int CompareTo(iLORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
			{
				Type t = other.GetType();
				if (t == typeof(LORChannel4))
				{
					LORChannel4 ch = (LORChannel4)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
				if (t == typeof(LORVizChannel4))
				{
					LORVizChannel4 ch = (LORVizChannel4)other;
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
			//LORSequence4 Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, lutils.FIELDsavedIndex);
			color = (int)lutils.getKeyValue(lineIn, FIELDcolor);
			myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			
			//! NOT supported by ShowTime.  Will not exist in sequence files saved from ShowTime (return blank "")
			// Preserved only in sequence files saved in Util-O-Rama
			// Only intended for temporary use within Util-O-Rama anyway so no big deal
			myComment = lutils.getKeyWord(lineIn, lutils.FIELDcomment);

			output.Parse(lineIn);
			//if (myParent != null) myParent.MakeDirty(true);
		}

		public new iLORMember4 Clone()
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LORChannel4 chan = (LORChannel4)base.Clone();
			chan.color = color;
			chan.output = output.Clone();
			chan.rgbChild = this.rgbChild;
			chan.rgbParent = rgbParent; // should be changed/overridden
			chan.effects = CloneEffects();
			return chan;
		}

		public new iLORMember4 Clone(string newName)
		{
			//LORChannel4 chan = new LORChannel4(newName, lutils.UNDEFINED);
			LORChannel4 chan = (LORChannel4)base.Clone();
			chan.ChangeName(newName);
			return chan;
		}

		public List<LOREffect4> CloneEffects()
		{
			List<LOREffect4> newList = new List<LOREffect4>();
			foreach (LOREffect4 ef in effects)
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


			//! EXPERIMENTAL-- MAY CRASH SHOWTIME
			//ret.Append(lutils.FIELDcomment);
			//ret.Append(lutils.FIELDEQ);
			//ret.Append(myComment);
			//ret.Append(lutils.ENDQT);
			//! Yup!  Error "Unexpected save file information found" when trying to open the file.
			//! So much for being able to save any extraneous data.


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


		public void CopyTo(LORChannel4 destination, bool withEffects)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.myParent == null) destination.myParent = this.myParent;
			//if (destination.output.deviceType == LORDeviceType4.None)
			//{
				destination.output.deviceType = output.deviceType;
				destination.output.circuit = output.circuit;
				destination.output.network = output.network;
				destination.output.unit = output.unit;
			//}
			if (withEffects)
			{
				//destination.Centiseconds = myCentiseconds;
				//foreach (LOREffect4 thisEffect in effects)
				//{
				//	destination.effects.Add(thisEffect.Clone());
				//}
				destination.effects = CloneEffects();
			}
		}

		public void CopyFrom(LORChannel4 source, bool withEffects)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.myParent == null) this.myParent = source.myParent;
			//if (output.deviceType == LORDeviceType4.None)
			//{
				output.deviceType = source.output.deviceType;
				output.circuit = source.output.circuit;
				output.network = source.output.network;
				output.unit = source.output.unit;
			//}
			if (withEffects)
			{
				//myCentiseconds = source.Centiseconds;
				//foreach (LOREffect4 theEffect in source.effects)
				//{
				//effects.Add(theEffect.Clone());
				//	AddEffect(theEffect.Clone());
				//}
				effects = source.CloneEffects();
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
				//foreach (LOREffect4 thisEffect in effects)
				//{
				//	newEffects.Add(thisEffect.Clone());
				//}
				//ret.effects = newEffects;
				ret.effects = CloneEffects();
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
			if (myParent != null) myParent.MakeDirty(true);
			//ID.Parent.dirty = true;
		}

		public void AddEffect(string lineIn)
		{
			LOREffect4 theEffect = new LOREffect4(lineIn);
			AddEffect(theEffect);
			if (myParent != null) myParent.MakeDirty(true);
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
			if (myParent != null) myParent.MakeDirty(true);
			return effects.Count;
		}

		//TODO: add RemoveEffect procedure
		//TODO: add SortEffects procedure (by startCentisecond)
	} // end channel
}
