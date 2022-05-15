using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using FileHelper;

namespace LOR4Utils
{

	public class LOR4VizDrawObject : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
	{
		public bool isRGB = false;
		public int BulbSize = lutils.UNDEFINED;
		public int BulbSpacing = lutils.UNDEFINED;
		public string Comment = "";
		public int BulbShape = lutils.UNDEFINED;  //? Enum?
		public int ZOrder = 0;
		public int AssignedItem = lutils.UNDEFINED;
		public bool Locked = false;
		public int FixtureType = lutils.UNDEFINED; //? Enum?
		public int ChannelType = lutils.UNDEFINED; //? Enum?
		public int MaxOpacity = 0; // Percent?
		public LOR4VizChannel redChannel = null;
		public LOR4VizChannel grnChannel = null;
		public LOR4VizChannel bluChannel = null;

		private static readonly string FIELDbulbSpacing = " BulbSpacing";
		private static readonly string FIELDcomment = " Comment";
		private static readonly string FIELDbulbShape = " BulbShape";
		private static readonly string FIELDzOrder = " ZOrder";
		private static readonly string FIELDassignedItem = " AssignedItem";
		private static readonly string FIELDlocked = " Locked";
		private static readonly string FIELDfixtureType = " Fixture_Type";
		private static readonly string FIELDchannelType = " Channel_Type";
		private static readonly string FIELDmaxOpacity = " Max_Opacity";

		public LOR4VizDrawObject()
		{
			// Default contstructor
		}

		public LOR4VizDrawObject(string lineIn)
		{
			Parse(lineIn);
		}

		public LOR4VizChannel subChannel
		{
			get
			{
				return redChannel;
			}
			set
			{
				redChannel = value;
			}
		}

		public int VizID
		{
			get
			{
				return mySavedIndex;
			}
		}

		public int AltVizID
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
		public new LOR4MemberType MemberType
		{ get { return LOR4MemberType.VizObject; } }


		public new int UniverseNumber
		{
			get
			{
				int ret = lutils.UNDEFINED;
				if (redChannel != null)
				{
					ret = redChannel.output.UniverseNumber;
				}
				return ret;
			}
		}

		public new int DMXAddress
		{
			get
			{
				int ret = lutils.UNDEFINED;
				if (redChannel != null)
				{
					ret = redChannel.output.DMXAddress;
				}
				return ret;
			}
		}

		public new void Parse(string lineIn)
		{
			// <LOR4VizDrawObject ID="141"
			// Name ="Pixel 154 / S2.004 / U3.010-012" BulbSize="1"
			// BulbSpacing ="1"
			// Comment =""
			// BulbShape ="1"
			// ZOrder ="1"
			// AssignedItem ="0"
			// Locked ="False"
			// Fixture_Type ="3"
			// Channel_Type ="2"
			// Max_Opacity ="0">

			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, LOR4Visualization.FIELDvizID);
			BulbSpacing = lutils.getKeyValue(lineIn, FIELDbulbSpacing);
			Comment = lutils.HumanizeName(lutils.getKeyWord(lineIn, FIELDcomment));
			BulbShape = lutils.getKeyValue(lineIn, FIELDbulbShape);
			ZOrder = lutils.getKeyValue(lineIn, FIELDzOrder);
			AssignedItem = lutils.getKeyValue(lineIn, FIELDassignedItem);
			Locked = lutils.getKeyState(lineIn, FIELDlocked);
			FixtureType = lutils.getKeyValue(lineIn, FIELDfixtureType);
			ChannelType = lutils.getKeyValue(lineIn, FIELDchannelType);
			MaxOpacity = lutils.getKeyValue(lineIn, FIELDmaxOpacity);

		}

		public new string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.StartTable(LOR4Visualization.TABLEdrawObject, 2));

			ret.Append(lutils.SetKey(LOR4Visualization.FIELDvizID, VizID));
			ret.Append(lutils.SetKey(LOR4Visualization.FIELDvizName, lutils.XMLifyName(myName)));
			ret.Append(lutils.SetKey(FIELDbulbSpacing, BulbSpacing));
			ret.Append(lutils.SetKey(FIELDcomment, Comment));
			ret.Append(lutils.SetKey(FIELDbulbShape, BulbShape));
			ret.Append(lutils.SetKey(FIELDzOrder, ZOrder));
			ret.Append(lutils.SetKey(FIELDassignedItem, AssignedItem));

			ret.Append(FIELDlocked);
			ret.Append(lutils.FIELDEQ);
			// Would be nice if LOR used standard "true" (Lower Case) but NOOOooooooo
			if (Locked)
			{
				ret.Append("True");
			}
			else
			{
				ret.Append("False");
			}
			ret.Append(lutils.ENDQT);

			ret.Append(lutils.SetKey(FIELDfixtureType, FixtureType));
			ret.Append(lutils.SetKey(FIELDchannelType, ChannelType));
			ret.Append(lutils.SetKey(FIELDmaxOpacity, MaxOpacity));
			ret.Append(lutils.ENDFLD);

			return ret.ToString();
		}

		public new iLOR4Member Clone()
		{
			LOR4VizDrawObject newDO = (LOR4VizDrawObject)Clone();
			newDO.isRGB = isRGB;
			newDO.BulbSize = BulbSize;
			newDO.BulbSpacing = BulbSpacing;
			newDO.BulbShape = BulbShape;
			newDO.Comment = Comment;
			newDO.ZOrder = ZOrder;
			newDO.AssignedItem = AssignedItem;
			newDO.Locked = Locked;
			newDO.FixtureType = FixtureType;
			newDO.ChannelType = ChannelType;
			newDO.MaxOpacity = MaxOpacity;
			newDO.redChannel = redChannel;
			newDO.grnChannel = grnChannel;
			newDO.bluChannel = bluChannel;

			return newDO;
		}

		public new iLOR4Member Clone(string newName)
		{
			iLOR4Member newDO = (LOR4VizDrawObject)this.Clone();
			newDO.ChangeName(newName);
			return newDO;
		}


	} // End LOR4VizDrawObject class







}