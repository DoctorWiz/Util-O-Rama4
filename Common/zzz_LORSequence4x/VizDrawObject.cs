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

namespace LORUtils4
{

		public class LORVizDrawObject4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
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
		public LORVizChannel4 redChannel = null;
		public LORVizChannel4 grnChannel = null;
		public LORVizChannel4 bluChannel = null;

		private static readonly string FIELDbulbSpacing = " BulbSpacing";
		private static readonly string FIELDcomment = " Comment";
		private static readonly string FIELDbulbShape = " BulbShape";
		private static readonly string FIELDzOrder = " ZOrder";
		private static readonly string FIELDassignedItem = " AssignedItem";
		private static readonly string FIELDlocked = " Locked";
		private static readonly string FIELDfixtureType = " Fixture_Type";
		private static readonly string FIELDchannelType = " Channel_Type";
		private static readonly string FIELDmaxOpacity = " Max_Opacity";

		public LORVizDrawObject4()
		{
			// Default contstructor
		}

		public LORVizDrawObject4(string lineIn)
		{
			Parse(lineIn);
		}

		public LORVizChannel4 subChannel
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
		public new LORMemberType4 MemberType
		{ get { return LORMemberType4.VizObject; } }


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
			// <LORVizDrawObject4 ID="141"
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
			mySavedIndex = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizID);
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

			ret.Append(lutils.StartTable(LORVisualization4.TABLEdrawObject, 2));

			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizID, VizID));
			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizName, lutils.XMLifyName(myName)));
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

		public new iLORMember4 Clone()
		{
			LORVizDrawObject4 newDO = (LORVizDrawObject4)Clone();
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

		public new iLORMember4 Clone(string newName)
		{
			iLORMember4 newDO = (LORVizDrawObject4)this.Clone();
			newDO.ChangeName(newName);
			return newDO;
		}


	} // End LORVizDrawObject4 class







}