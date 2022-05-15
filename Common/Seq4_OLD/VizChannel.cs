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
	public class LOR4VizChannel : LOR4MemberBase, iLOR4Member, IComparable<iLOR4Member>
	{
		//private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;

		public Int32 color = 0;
		public LOR4Output output = new LOR4Output();
		public LOR4RGBChild rgbChild = LOR4RGBChild.None;
		public LOR4VizDrawObject DrawObject = null;
		//public List<LOR4Effect> effects = new List<LOR4Effect>();
		public int SubType = lutils.UNDEFINED;
		public int SubParam = lutils.UNDEFINED;
		//public MultiColors MultiColors = new MultiColors();
		public int[] colors = new int[5];
		//protected LOR4Membership members = null;

		public bool LED = false;

		private static readonly string FIELDsubType = " Sub_Type";
		//public static readonly string FIELDsubParam = " Sub_Param";
		public static readonly string FIELDsubParam = " Sub_Parm";
		public static readonly string FIELDmultiColor = " Multi_";
		private static readonly string FIELDled = " LED";

		//! CONSTRUCTORS
		public LOR4VizChannel(string theName, int theVizID)
		{
			myName = theName;
			mySavedIndex = theVizID;
		}

		public LOR4VizChannel(string lineIn)
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
		public new int CompareTo(iLOR4Member other)
		{
			int result = 0;
			if (LOR4Membership.sortMode == LOR4Membership.SORTbyOutput)
			{
				Type t = other.GetType();
				if (t == typeof(LOR4VizChannel))
				{
					LOR4VizChannel ch = (LOR4VizChannel)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
				if (t == typeof(LOR4Channel))
				{
					// Note: VizChannel output can be compared to LORChannel output
					// but LORChannel output cannot be compared to VizChannel output
					LOR4Channel ch = (LOR4Channel)other;
					result = output.ToString().CompareTo(ch.output.ToString());
				}
			}
			else
			{
				result = base.CompareTo(other);
			}
			return result;
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

		public LOR4VizDrawObject Owner
		{
			get { return DrawObject; }
			set { DrawObject = value; }
		}


		public new LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.VizChannel4;
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
			//LOR4Sequence Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			mySavedIndex = lutils.getKeyValue(lineIn, LOR4Visualization.FIELDvizID);
			color = lutils.getKeyValue(lineIn, LOR4Visualization.FIELDvizColor);
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
			// <LOR4Channel ID="1" Name="M5 Center Bushes (G) [L1.10]"
			// LOR4DeviceType ="1" Network="0"
			// Controller ="1" LOR4Channel="10"
			// Color ="65280"
			// Sub_Type ="0" Sub_Parm="0"
			// Multi_1 ="16777215" Multi_2="16777215" Multi_3="16777215" Multi_4="16777215" Multi_5="16777215"
			// LED ="True"/>

			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.StartTable(LOR4Visualization.TABLEvizChannel, 2));

			ret.Append(lutils.SetKey(LOR4Visualization.FIELDvizID, VizID));
			ret.Append(lutils.SetKey(LOR4Visualization.FIELDvizName, lutils.XMLifyName(myName)));
			ret.Append(output.LineOut());
			ret.Append(lutils.SetKey(LOR4Visualization.FIELDvizColor, color));
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

			for (int c = 0; c < 5; c++)
			{
				ret.Append(lutils.SetKey(FIELDmultiColor + c.ToString(), colors[c]));
			}

			return ret.ToString();
		}

		public new int UniverseNumber
		{ get { return output.UniverseNumber; } }
		public new int DMXAddress
		{ get { return output.channel; } }


		public void CloneTo(LOR4VizChannel destination)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.myParent == null) destination.myParent = this.myParent;
			destination.SubType = SubType;
			destination.SubParam = SubParam;
			if (destination.output.deviceType == LOR4DeviceType.None)
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

		public void CloneFrom(LOR4VizChannel source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.myParent == null) this.myParent = source.myParent;
			SubType = source.SubType;
			SubParam = source.SubParam;
			if (output.deviceType == LOR4DeviceType.None)
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

		public void CopyFromSeqChannel(LOR4Channel source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			output.deviceType = source.output.deviceType;
			output.circuit = source.output.circuit;
			output.network = source.output.network;
			output.unit = source.output.unit;

		}

		public new iLOR4Member Clone()
		{
			LOR4VizChannel vch = (LOR4VizChannel)Clone();
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

		public new iLOR4Member Clone(string newName)
		{
			LOR4VizChannel vch = (LOR4VizChannel)this.Clone();
			ChangeName(newName);
			return vch;
		}


		public LOR4Channel CopyToSeqChannel()
		{
			// See Also: Duplicate()
			//int nextSI = ID.Parent.Members.highestSavedIndex + 1;
			LOR4Channel ret = new LOR4Channel(myName, lutils.UNDEFINED);
			ret.color = color;
			ret.output.deviceType = output.deviceType;
			ret.output.circuit = output.circuit;
			ret.output.network = output.network;
			ret.output.unit = output.unit;
			return ret;
		}

	} // End LOR4VizChannel

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
			color1 = lutils.getKeyValue(lineIn, LOR4VizChannel.FIELDmultiColor + "1");
			color2 = lutils.getKeyValue(lineIn, LOR4VizChannel.FIELDmultiColor + "2");
			color3 = lutils.getKeyValue(lineIn, LOR4VizChannel.FIELDmultiColor + "3");
			color4 = lutils.getKeyValue(lineIn, LOR4VizChannel.FIELDmultiColor + "4");
			color5 = lutils.getKeyValue(lineIn, LOR4VizChannel.FIELDmultiColor + "5");
		}


		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(LOR4VizChannel.FIELDmultiColor);
			ret.Append("1");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color1);
			ret.Append(lutils.ENDQT);

			ret.Append(LOR4VizChannel.FIELDmultiColor);
			ret.Append("2");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color2);
			ret.Append(lutils.ENDQT);

			ret.Append(LOR4VizChannel.FIELDmultiColor);
			ret.Append("3");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color3);
			ret.Append(lutils.ENDQT);

			ret.Append(LOR4VizChannel.FIELDmultiColor);
			ret.Append("4");
			ret.Append(lutils.FIELDEQ);
			ret.Append(color4);
			ret.Append(lutils.ENDQT);

			ret.Append(LOR4VizChannel.FIELDmultiColor);
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
