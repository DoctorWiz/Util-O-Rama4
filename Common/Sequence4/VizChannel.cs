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
	public class LORVizChannel4 : LORMemberBase4, iLORMember4, IComparable<iLORMember4>
	{
		//private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;

		public int color = 0;
		public LOROutput4 output = new LOROutput4();
		public LORRGBChild4 rgbChild = LORRGBChild4.None;
		public LORVizDrawObject4 DrawObject = null;
		//public List<LOREffect4> effects = new List<LOREffect4>();
		public int SubType = lutils.UNDEFINED;
		public int SubParam = lutils.UNDEFINED;
		//public MultiColors MultiColors = new MultiColors();
		public int[] colors = new int[5];
		//protected LORMembership4 members = null;

		public bool LED = false;

		private static readonly string FIELDsubType = " Sub_Type";
		//public static readonly string FIELDsubParam = " Sub_Param";
		public static readonly string FIELDsubParam = " Sub_Parm";
		public static readonly string FIELDmultiColor = " Multi_";
		private static readonly string FIELDled = " LED";

		//! CONSTRUCTORS
		public LORVizChannel4(string theName, int theVizID)
		{
			myName = theName;
			mySavedIndex = theVizID;
		}

		public LORVizChannel4(string lineIn)
		{
			string seek = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname + lutils.FIELDEQ;
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


		//! PROPERTIES, METHODS, ETC.
		public new int CompareTo(iLORMember4 other)
		{
			int result = 0;
			if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
			{
				Type t = other.GetType();
				if (t == typeof(LORVizChannel4))
				{
					LORVizChannel4 ch = (LORVizChannel4)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
				if (t == typeof(LORChannel4))
				{
					// Note: VizChannel output can be compared to LORChannel output
					// but LORChannel output cannot be compared to VizChannel output
					LORChannel4 ch = (LORChannel4)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
			}
			else
			{
				result = base.CompareTo(other);
			}
			return result;
		}

		public int ItemID
		{
			get
			{
				return mySavedIndex;
			}
			set
			{
				mySavedIndex = value;
				//if (myParent != null) myParent.MakeDirty(true);
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
				//if (myParent != null) myParent.MakeDirty(true);
				//System.Diagnostics.Debugger.Break();
			}
		}


		public new int Centiseconds
		{
			get
			{
				int cs = 0;
				if (myParent != null)
				{ cs = myParent.Centiseconds; }
				return cs;
			}
			set
			{
				// Do nothing, throw value away, don't need it
			}
		}

		public LORVizDrawObject4 Owner
		{
			get { return DrawObject; }
			set { DrawObject = value; }
		}


		public new LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.VizChannel;
			}
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

		public new void Parse(string lineIn)
		{
			//LORSequence4 Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			ItemID = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizID);
			color = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizColor);
			//myCentiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
			output.Parse(lineIn);
			SubType = lutils.getKeyValue(lineIn, FIELDsubType);
			SubParam = lutils.getKeyValue(lineIn, FIELDsubParam);
			colors[0] = lutils.getKeyValue(lineIn, FIELDmultiColor + "1");
			colors[1] = lutils.getKeyValue(lineIn, FIELDmultiColor + "2");
			colors[2] = lutils.getKeyValue(lineIn, FIELDmultiColor + "3");
			colors[3] = lutils.getKeyValue(lineIn, FIELDmultiColor + "4");
			colors[4] = lutils.getKeyValue(lineIn, FIELDmultiColor + "5");
			LED = lutils.getKeyState(lineIn, FIELDled);
			if (myParent != null) myParent.MakeDirty(true);
		}

		public new string LineOut()
		{
			// <LORChannel4 ID="1" Name="M5 Center Bushes (G) [L1.10]"
			// LORDeviceType4 ="1" Network="0"
			// Controller ="1" LORChannel4="10"
			// Color ="65280"
			// Sub_Type ="0" Sub_Parm="0"
			// Multi_1 ="16777215" Multi_2="16777215" Multi_3="16777215" Multi_4="16777215" Multi_5="16777215"
			// LED ="True"/>

			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.StartTable(LORVisualization4.TABLEvizChannel, 2));

			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizID, ItemID));
			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizName, lutils.XMLifyName(myName)));
			ret.Append(output.LineOut());
			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizColor, (int)color));
			ret.Append(lutils.SetKey(FIELDsubType, SubType));
			ret.Append(lutils.SetKey(FIELDsubParam, SubParam));
			
			ret.Append(ColorsLineOut());

			ret.Append(FIELDled);
			ret.Append(lutils.FIELDEQ);
			if (LED)
			{
				ret.Append("True");  // Would be nice if LOR used standard "true" (Lower Case) but NOOOooooooo
			}
			else
			{
				ret.Append("False");
			}
			ret.Append(lutils.ENDQT);
			ret.Append(lutils.ENDFLD);

			return ret.ToString();
		}

		public string ColorsLineOut()
		{
			StringBuilder ret = new StringBuilder();

			for (int c=0; c<5; c++)
			{
				ret.Append(lutils.SetKey(FIELDmultiColor + c.ToString(), colors[c]));
			}

			return ret.ToString();
		}

		public new int UniverseNumber
		{ get { return output.UniverseNumber; } }
		public new int DMXAddress
		{ get { return output.channel; } }


		public void CloneTo(LORVizChannel4 destination)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.myParent == null) destination.myParent = this.myParent;
			destination.SubType = SubType;
			destination.SubParam = SubParam;
			if (destination.output.deviceType == LORDeviceType4.None)
			{
				destination.output.deviceType = output.deviceType;
				destination.output.circuit = output.circuit;
				destination.output.network = output.network;
				destination.output.unit = output.unit;
			}
			for (int c = 0; c < 5; c++)
			{
				destination.colors[c] = colors[c];
			}
			destination.LED = LED;
		}

		public void CloneFrom(LORVizChannel4 source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.myParent == null) this.myParent = source.myParent;
			SubType = source.SubType;
			SubParam = source.SubParam;
			if (output.deviceType == LORDeviceType4.None)
			{
				output.deviceType = source.output.deviceType;
				output.circuit = source.output.circuit;
				output.network = source.output.network;
				output.unit = source.output.unit;
			}
			for (int c = 0; color < 5; color++)
			{
				colors[color] = source.colors[c];
			}
			LED = source.LED;

		}

		public void CopyFromSeqChannel (LORChannel4 source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			output.deviceType = source.output.deviceType;
			output.circuit = source.output.circuit;
			output.network = source.output.network;
			output.unit = source.output.unit;

		}

		public new iLORMember4 Clone()
		{
			LORVizChannel4 vch = (LORVizChannel4)Clone();
			vch.output = output.Clone();
			vch.Owner = Owner;
			vch.color = color;
			vch.SubType = SubType;
			vch.SubParam = SubParam;
			vch.LED = LED;
			for (int c = 0; c < 5; c++)
			{
				vch.colors[c] = colors[c];
			}
			return vch;
		}

		public new iLORMember4 Clone(string newName)
		{
			LORVizChannel4 vch = (LORVizChannel4)this.Clone();
			ChangeName(newName);
			return vch;
		}


		public LORChannel4 CopyToSeqChannel()
		{
			// See Also: Duplicate()
			//int nextSI = ID.Parent.Members.highestSavedIndex + 1;
			LORChannel4 ret = new LORChannel4(myName, lutils.UNDEFINED);
			ret.color = color;
			ret.output.deviceType = output.deviceType;
			ret.output.circuit = output.circuit;
			ret.output.network = output.network;
			ret.output.unit = output.unit;
			return ret;
		}

	} // End LORVizChannel4

	/*
	public class MultiColors
	{
		// Consider using array...
		// Unfortunately, C# does not allow indexed properties
		// LOR limits it to just 5 colors anyway
		public int color1 = 0;
		public int color2 = 0;
		public int color3 = 0;
		public int color4 = 0;
		public int color5 = 0;

		public MultiColors()
		{
			// default constructor
		}

		public MultiColors(int color_1, int color_2, int color_3, int color_4, int color_5)
		{
			color1 = color_1;
			color2 = color_2;
			color3 = color_3;
			color4 = color_4;
			color5 = color_5;
		}

		public void Parse(string lineIn)
		{
			color1 = lutils.getKeyValue(lineIn, LORVizChannel4.FIELDmultiColor + "1");
			color2 = lutils.getKeyValue(lineIn, LORVizChannel4.FIELDmultiColor + "2");
			color3 = lutils.getKeyValue(lineIn, LORVizChannel4.FIELDmultiColor + "3");
			color4 = lutils.getKeyValue(lineIn, LORVizChannel4.FIELDmultiColor + "4");
			color5 = lutils.getKeyValue(lineIn, LORVizChannel4.FIELDmultiColor + "5");
		}


		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(LORVizChannel4.FIELDmultiColor);
			ret.Append("1");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color1);
			ret.Append(lutils.ENDQT);

			ret.Append(LORVizChannel4.FIELDmultiColor);
			ret.Append("2");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color2);
			ret.Append(lutils.ENDQT);

			ret.Append(LORVizChannel4.FIELDmultiColor);
			ret.Append("3");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color3);
			ret.Append(lutils.ENDQT);

			ret.Append(LORVizChannel4.FIELDmultiColor);
			ret.Append("4");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color4);
			ret.Append(lutils.ENDQT);

			ret.Append(LORVizChannel4.FIELDmultiColor);
			ret.Append("5");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color5);
			ret.Append(lutils.ENDQT);

			return ret.ToString();
		}

		public MultiColors Clone()
		{
			MultiColors ret = new MultiColors(color1, color2, color3, color4, color5);
			return ret;
		}


		//TODO create property gets for NetColors

	}
	*/

}
