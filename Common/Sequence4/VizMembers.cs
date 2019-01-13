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
	public class VizChannel : IMember, IComparable<IMember>
	{
		private string myName = "";
		//private int myCentiseconds = utils.UNDEFINED;
		private int myIndex = utils.UNDEFINED;
		public int VizID = utils.UNDEFINED;
		private int myAltVizID = utils.UNDEFINED;
		private Visualization4 parentVisualization = null;
		private bool imSelected = false;
		//private const string STARTchannel = utils.STFLD + utils.TABLEchannel + utils.FIELDname;

		public Int32 color = 0;
		public Output output = new Output();
		public RGBchild rgbChild = RGBchild.None;
		public DrawObject Parent = null;
		//public List<Effect> effects = new List<Effect>();
		public int SubType = utils.UNDEFINED;
		public int SubParam = utils.UNDEFINED;
		public MultiColors MultiColors = new MultiColors();
		public bool LED = false;

		private static readonly string FIELDsubType = " Sub_Type";
		public static readonly string FIELDsubParam = " Sub_Param";
		public static readonly string FIELDmultiColor = " Multi_";
		private static readonly string FIELDled = " LED";

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
			if (parentVisualization != null) parentVisualization.MakeDirty();

		}

		public int Centiseconds
		{
			get
			{
				return 0;
			}
			set
			{
				// Do nothing, throw value away, don't need it
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
				if (Parent != null)
				{
					return Parent.VizID;
				}
				else
				{
					System.Diagnostics.Debugger.Break();
					return utils.UNDEFINED;
				}
			}
		}

		public void SetSavedIndex(int theSavedIndex)
		{
			// Depreciated, at least for now
			System.Diagnostics.Debugger.Break();
			//myVizID = theSavedIndex;
		}

		public int AltSavedIndex
		{
			get
			{
				return myAltVizID;
			}
			set
			{
				myAltVizID = value;
			}
		}

		public Sequence4 ParentSequence
		{
			get
			{
				return null;
			}
		}

		public void SetParentSeq(Sequence4 newParentSeq)
		{
			// Do Nothing
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
				return MemberType.VizChannel;
			}
		}

		public int CompareTo(IMember other)
		{
			int result = 0;
			/*
			if (parentVisualization.Members.sortMode == Membership.SORTbySavedIndex)
			{
				result = myVizID.CompareTo(other.SavedIndex);
			}
			else
			{
				if (parentVisualization.Members.sortMode == Membership.SORTbyName)
				{
					result = myName.CompareTo(other.Name);
				}
				else
				{
					if (parentVisualization.Members.sortMode == Membership.SORTbyAltSavedIndex)
					{
						result = myAltVizID.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (parentVisualization.Members.sortMode == Membership.SORTbyOutput)
						{
							Channel ch = (Channel)other;
							output.ToString().CompareTo(ch.output.ToString());
						}
					}
				}
			}
			*/
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

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			//Sequence4 ParentSequence = ID.ParentSequence;
			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			VizID = utils.getKeyValue(lineIn, Visualization4.FIELDvizID);
			color = utils.getKeyValue(lineIn, Visualization4.FIELDvizColor);
			//myCentiseconds = utils.getKeyValue(lineIn, utils.FIELDcentiseconds);
			output.Parse(lineIn);
			SubType = utils.getKeyValue(lineIn, FIELDsubType);
			SubParam = utils.getKeyValue(lineIn, FIELDsubParam);
			MultiColors.color1 = utils.getKeyValue(lineIn, FIELDmultiColor + "1");
			MultiColors.color2 = utils.getKeyValue(lineIn, FIELDmultiColor + "2");
			MultiColors.color3 = utils.getKeyValue(lineIn, FIELDmultiColor + "3");
			MultiColors.color4 = utils.getKeyValue(lineIn, FIELDmultiColor + "4");
			MultiColors.color5 = utils.getKeyValue(lineIn, FIELDmultiColor + "5");
			LED = utils.getKeyState(lineIn, FIELDled);
			if (parentVisualization != null) parentVisualization.MakeDirty();
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltVizID > utils.UNDEFINED) r = true;
				return r;
			}
		}

		public string LineOut()
		{
			// <Channel ID="1" Name="M5 Center Bushes (G) [L1.10]"
			// DeviceType ="1" Network="0"
			// Controller ="1" Channel="10"
			// Color ="65280"
			// Sub_Type ="0" Sub_Parm="0"
			// Multi_1 ="16777215" Multi_2="16777215" Multi_3="16777215" Multi_4="16777215" Multi_5="16777215"
			// LED ="True"/>

			string ret = "";
			ret = utils.LEVEL2 + utils.STFLD + Visualization4.TABLEvizChannel;
			ret += Visualization4.FIELDvizID + utils.FIELDEQ + VizID.ToString() + utils.ENDQT;
			ret += Visualization4.FIELDvizName + utils.FIELDEQ + utils.XMLifyName(myName) + utils.ENDQT;
			ret += output.LineOut();
			ret += Visualization4.FIELDvizColor + utils.FIELDEQ + color.ToString() + utils.ENDQT;
			ret += FIELDsubType + utils.FIELDEQ + SubType.ToString() + utils.ENDQT;
			ret += FIELDsubParam + utils.FIELDEQ + SubParam.ToString() + utils.ENDQT;
			ret += MultiColors.LineOut();
			ret += FIELDled + utils.FIELDEQ;
			if (LED)
			{
				ret += "True";  // Would be nice if LOR used standard "true" (Lower Case) but NOOOooooooo
			}
			else
			{
				ret += "False";
			}
			ret += utils.ENDQT + utils.ENDFLD;

			return ret;
		}


		public VizChannel(string theName, int theVizID)
		{
			myName = theName;
			VizID = theVizID;
		}

		public VizChannel(string lineIn)
		{
			string seek = utils.STFLD + utils.TABLEchannel + utils.FIELDname + utils.FIELDEQ;
			//int pos = lineIn.IndexOf(seek);
			int pos = utils.FastIndexOf(lineIn, seek);
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

		public void CloneTo(VizChannel destination)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.parentVisualization == null) destination.parentVisualization = this.parentVisualization;
			destination.SubType = SubType;
			destination.SubParam = SubParam;
			if (destination.output.deviceType == DeviceType.None)
			{
				destination.output.deviceType = output.deviceType;
				destination.output.circuit = output.circuit;
				destination.output.network = output.network;
				destination.output.unit = output.unit;
			}
			destination.MultiColors = MultiColors.Clone();
			destination.LED = LED;
		}

		public void CloneFrom(VizChannel source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			if (this.parentVisualization == null) this.parentVisualization = source.parentVisualization;
			SubType = source.SubType;
			SubParam = source.SubParam;
			if (output.deviceType == DeviceType.None)
			{
				output.deviceType = source.output.deviceType;
				output.circuit = source.output.circuit;
				output.network = source.output.network;
				output.unit = source.output.unit;
			}
			MultiColors = source.MultiColors.Clone();
			LED = source.LED;

		}

		public void CopyFromSeqChannel (Channel source)
		{
			if (color == 0) color = source.color;
			if (myName.Length == 0) ChangeName(source.Name);
			output.deviceType = source.output.deviceType;
			output.circuit = source.output.circuit;
			output.network = source.output.network;
			output.unit = source.output.unit;

		}

		public IMember Clone()
		{
			return this.Clone(myName);
		}

		public IMember Clone(string newName)
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			VizChannel vch = new VizChannel(newName, utils.UNDEFINED);
			//vch.myCentiseconds = myCentiseconds;
			//vch.myIndex = myIndex;
			//vch.VizID = VizID;
			//vch.myAltVizID = myAltVizID;
			vch.imSelected = imSelected;
			vch.color = color;
			vch.output = output.Clone();
			vch.SubType = SubType;
			vch.SubParam = SubParam;
			vch.LED = LED;
			vch.MultiColors = MultiColors.Clone();
			return vch;
		}


		public Channel CopyToSeqChannel()
		{
			// See Also: Duplicate()
			//int nextSI = ID.ParentSequence.Members.highestSavedIndex + 1;
			Channel ret = new Channel(myName, utils.UNDEFINED);
			ret.color = color;
			ret.output.deviceType = output.deviceType;
			ret.output.circuit = output.circuit;
			ret.output.network = output.network;
			ret.output.unit = output.unit;
			return ret;
		}

	} // End VizChannel

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
			color1 = utils.getKeyValue(lineIn, VizChannel.FIELDmultiColor + "1");
			color2 = utils.getKeyValue(lineIn, VizChannel.FIELDmultiColor + "2");
			color3 = utils.getKeyValue(lineIn, VizChannel.FIELDmultiColor + "3");
			color4 = utils.getKeyValue(lineIn, VizChannel.FIELDmultiColor + "4");
			color5 = utils.getKeyValue(lineIn, VizChannel.FIELDmultiColor + "5");
		}


		public string LineOut()
		{
			string ret = VizChannel.FIELDmultiColor + "1" + utils.FIELDEQ + color1.ToString() + utils.ENDQT;
			ret += VizChannel.FIELDmultiColor + "2" + utils.FIELDEQ + color2.ToString() + utils.ENDQT;
			ret += VizChannel.FIELDmultiColor + "3" + utils.FIELDEQ + color3.ToString() + utils.ENDQT;
			ret += VizChannel.FIELDmultiColor + "4" + utils.FIELDEQ + color4.ToString() + utils.ENDQT;
			ret += VizChannel.FIELDmultiColor + "5" + utils.FIELDEQ + color5.ToString() + utils.ENDQT;
			return ret;
		}

		public MultiColors Clone()
		{
			MultiColors ret = new MultiColors(color1, color2, color3, color4, color5);
			return ret;
		}


		//TODO create property gets for NetColors

	}

	public class DrawObject
	{
		public int VizID = utils.UNDEFINED;
		public bool isRGB = false;
		private string myName = "";
		public int BulbSize = utils.UNDEFINED;
		public int BulbSpacing = utils.UNDEFINED;
		public string Comment = "";
		public int BulbShape = utils.UNDEFINED;  //? Enum?
		public int ZOrder = 0;
		public int AssignedItem = utils.UNDEFINED;
		public bool Locked = false;
		public int FixtureType = utils.UNDEFINED; //? Enum?
		public int ChannelType = utils.UNDEFINED; //? Enum?
		public int MaxOpacity = 0; // Percent?
		public VizChannel redChannel = null;
		public VizChannel grnChannel = null;
		public VizChannel bluChannel = null;
		private int myIndex = utils.UNDEFINED;


		private static readonly string FIELDbulbSpacing = " BulbSpacing";
		private static readonly string FIELDcomment = " Comment";
		private static readonly string FIELDbulbShape = " BulbShape";
		private static readonly string FIELDzOrder = " ZOrder";
		private static readonly string FIELDassignedItem = " AssignedItem";
		private static readonly string FIELDlocked = " Locked";
		private static readonly string FIELDfixtureType = " Fixture_Type";
		private static readonly string FIELDchannelType = " Channel_Type";
		private static readonly string FIELDmaxOpacity = " Max_Opacity";

		public DrawObject()
		{
			// Default contstructor
		}

		public DrawObject(string lineIn)
		{
			Parse(lineIn);
		}

		public VizChannel subChannel
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

		public void Parse(string lineIn)
		{
			// <DrawObject ID="141"
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

			myName = utils.HumanizeName(utils.getKeyWord(lineIn, utils.FIELDname));
			VizID = utils.getKeyValue(lineIn, Visualization4.FIELDvizID);
			BulbSpacing = utils.getKeyValue(lineIn, FIELDbulbSpacing);
			Comment = utils.HumanizeName(utils.getKeyWord(lineIn, FIELDcomment));
			BulbShape = utils.getKeyValue(lineIn, FIELDbulbShape);
			ZOrder = utils.getKeyValue(lineIn, FIELDzOrder);
			AssignedItem = utils.getKeyValue(lineIn, FIELDassignedItem);
			Locked = utils.getKeyState(lineIn, FIELDlocked);
			FixtureType = utils.getKeyValue(lineIn, FIELDfixtureType);
			ChannelType = utils.getKeyValue(lineIn, FIELDchannelType);
			MaxOpacity = utils.getKeyValue(lineIn, FIELDmaxOpacity);

		}

		public string LineOut()
		{
			string ret = "";
			ret = utils.LEVEL2 + utils.STFLD + Visualization4.TABLEdrawObject;
			ret += Visualization4.FIELDvizID + utils.FIELDEQ + VizID.ToString() + utils.ENDQT;
			ret += Visualization4.FIELDvizName + utils.FIELDEQ + utils.XMLifyName(myName) + utils.ENDQT;
			ret += FIELDbulbSpacing + utils.FIELDEQ + BulbSpacing.ToString() + utils.ENDQT;
			ret += FIELDcomment + utils.FIELDEQ + Comment + utils.ENDQT;
			ret += FIELDbulbShape + utils.FIELDEQ + BulbShape.ToString() + utils.ENDQT;
			ret += FIELDzOrder + utils.FIELDEQ + ZOrder.ToString() + utils.ENDQT;
			ret += FIELDassignedItem + utils.FIELDEQ + AssignedItem.ToString() + utils.ENDQT;
			ret += FIELDlocked + utils.FIELDEQ;
			if (Locked)
			{
				ret += "True" + utils.ENDQT;  // Would be nice if LOR used standard "true" (Lower Case) but NOOOooooooo
			}
			else
			{
				ret = "False" + utils.ENDQT;

			}
			ret += FIELDfixtureType + utils.FIELDEQ + FixtureType.ToString() + utils.ENDQT;
			ret += FIELDchannelType + utils.FIELDEQ + ChannelType.ToString() + utils.ENDQT;
			ret += FIELDmaxOpacity + utils.FIELDEQ + MaxOpacity.ToString() + utils.ENDQT;
			ret += utils.ENDFLD;

			return ret;
		}

		public void SetIndex(int newIndex)
		{
			myIndex = newIndex;
		}


	} // End DrawObject class

}
