using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace LORUtils4
{
	public class LORVizChannel4 : LORMember4, IComparable<LORMember4>
	{
		protected string myName = "";
		//private int myCentiseconds = lutils.UNDEFINED;
		protected int myIndex = lutils.UNDEFINED;
		protected int VizID = lutils.UNDEFINED;
		protected int myAltVizID = lutils.UNDEFINED;
		protected LORVisualization4 parentVisualization = null;
		protected bool imSelected = false;
		//private const string STARTchannel = lutils.STFLD + lutils.TABLEchannel + lutils.FIELDname;

		public Int32 color = 0;
		public LOROutput4 output = new LOROutput4();
		public LORRGBChild4 rgbChild = LORRGBChild4.None;
		public LORVizDrawObject4 DrawObject = null;
		//public List<LOREffect4> effects = new List<LOREffect4>();
		public int SubType = lutils.UNDEFINED;
		public int SubParam = lutils.UNDEFINED;
		//public MultiColors MultiColors = new MultiColors();
		public int[] colors = new int[5];
		protected bool isDirty = false;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		//protected LORMembership4 members = null;

		public bool LED = false;
		protected object myTag = null;
		protected LORMember4 mappedTo = null;

		private static readonly string FIELDsubType = " Sub_Type";
		//public static readonly string FIELDsubParam = " Sub_Param";
		public static readonly string FIELDsubParam = " Sub_Parm";
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
			if (parentVisualization != null) parentVisualization.MakeDirty(true);

		}

		public int Centiseconds
		{
			get
			{
				int cs = 0;
				if (parentVisualization != null)
				{ cs = parentVisualization.Centiseconds; }
				return cs;
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
				return VizID;
			}
		}

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentVisualization != null)
				{
					parentVisualization.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
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

		public LORMember4 Parent
		{
			get
			{
				return parentVisualization;
			}
		}

		public LORVizDrawObject4 Owner
		{
			get { return DrawObject; }
			set { DrawObject = value; }
		}
		public void SetParent(LORMember4 newParent)
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

		public LORMemberType4 MemberType
		{
			get
			{
				return LORMemberType4.VizChannel4;
			}
		}

		public int CompareTo(LORMember4 other)
		{
			int result = 1;  // By default I win! (unless you can prove otherwise!)
			int myID = VizID;
			int otherID = lutils.UNDEFINED;
			try
			{


				if (other != null)
				{
					otherID = other.SavedIndex;
					if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
					{
						result = VizID.CompareTo(other.SavedIndex);
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
								result = myAltVizID.CompareTo(other.AltSavedIndex);
							}
							else
							{
								if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
								{
									result = UniverseNumber.CompareTo(other.UniverseNumber);
									if (result == 0)
									{
										result = DMXAddress.CompareTo(other.DMXAddress);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (lutils.IsWizard)
				{
					string msg = ex.ToString();
					Type t = GetType();
					string thisType = t.ToString();
					t = other.GetType();
					string otherType = t.ToString();
					int MYid = myID;
					int OTherid = otherID;
					string MYname = myName;
					string OHthername = other.Name;
					string MYoutput = output.ToString();
					string OtherDMX = other.DMXAddress.ToString();
					DialogResult dr = MessageBox.Show(msg, "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					System.Diagnostics.Debugger.Break();
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

		public override string ToString()
		{
			return myName;
		}

		public void Parse(string lineIn)
		{
			//LORSequence4 Parent = ID.Parent;
			myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
			VizID = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizID);
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
			if (parentVisualization != null) parentVisualization.MakeDirty(true);
		}

		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltVizID > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public string LineOut()
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

			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizID, VizID));
			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizName, lutils.XMLifyName(myName)));
			ret.Append(output.LineOut());
			ret.Append(lutils.SetKey(LORVisualization4.FIELDvizColor, color));
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
				//if (value.MemberType == LORMemberType4.LORVizChannel4)
				//{
					mappedTo = value;
				//}
			}
		}

		public bool ExactMatch
		{ get { return matchesExactly; } set { matchesExactly = value; } }
		public int UniverseNumber
		{ get { return output.UniverseNumber; } }
		public int DMXAddress
		{ get { return output.channel; } }

		public LORVizChannel4(string theName, int theVizID)
		{
			myName = theName;
			VizID = theVizID;
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

		public void CloneTo(LORVizChannel4 destination)
		{
			if (destination.color == 0) destination.color = color;
			if (destination.Name.Length == 0) destination.ChangeName(myName);
			if (destination.parentVisualization == null) destination.parentVisualization = this.parentVisualization;
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
			if (this.parentVisualization == null) this.parentVisualization = source.parentVisualization;
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

		public LORMember4 Clone()
		{
			return this.Clone(myName);
		}

		public LORMember4 Clone(string newName)
		{
			// See Also: Clone(), CopyTo(), and CopyFrom()
			LORVizChannel4 vch = new LORVizChannel4(newName, lutils.UNDEFINED);
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
			for (int c = 0; c < 5; c++)
			{
				vch.colors[c] = colors[c];
			}
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

	public class LORVizDrawObject4 : LORMember4, IComparable<LORMember4>
	{
		public int VizID = lutils.UNDEFINED;
		public bool isRGB = false;
		protected string myName = "";
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
		protected int myIndex = lutils.UNDEFINED;
		protected int myAltVizID = lutils.UNDEFINED;
		protected bool imSelected = false;
		protected bool matchesExactly = false;  // Used to distinguish exact name matches from fuzzy name matches
		protected object myTag = null;
		protected LORMember4 mappedTo = null;
		protected bool isDirty = false;
		protected LORVisualization4 parentVisualization = null;

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

		public string Name
		{ get { return myName; } }
		public void ChangeName(string newName)
		{
			MakeDirty(true);
			myName = newName;
		}
		public int Centiseconds
		{
			get
			{
				int cs = 0;
				if (redChannel != null)
				{ cs = redChannel.Centiseconds; }
				return cs;
			}
			set
			{
				// Do nothing, throw value away, don't need it
			}
		}
		public int Index
		{ get { return myIndex; } }
		public void SetIndex(int newIndex)
		{ myIndex = newIndex; }
		public int SavedIndex
		{
			get
			{
				return VizID;
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
		public bool Selected
		{ get { return imSelected; } set { imSelected = value; } }
		public LORMemberType4 MemberType
		{ get { return LORMemberType4.VizObject; } }

		public bool Dirty
		{ get { return isDirty; } }
		public void MakeDirty(bool dirtyState)
		{
			if (dirtyState)
			{
				if (parentVisualization != null)
				{
					parentVisualization.MakeDirty(true);
				}
			}
			isDirty = dirtyState;
		}

		public LORMember4 MapTo
		{ get { return mappedTo; } set { mappedTo = value; } }
		public bool ExactMatch
		{  get { return matchesExactly; } set { matchesExactly = value; } }

		public int UniverseNumber
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

		public int DMXAddress
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

		public LORMember4 Parent
		{ get { return parentVisualization; } }
		public void SetParent(LORMember4 newParent)
		{
			if (newParent != null)
			{
				if (newParent.MemberType == LORMemberType4.Visualization)
				{
					parentVisualization = (LORVisualization4)newParent;
				}
				else
				{
					if (lutils.IsWizard)
					{
						// Wront type: Parent should be Visualation
						System.Diagnostics.Debugger.Break();
					}
				}
			}
		}
		
		public object Tag
		{ get { return myTag; } set { myTag = value; } }
		public int CompareTo(LORMember4 other)
		{
			int result = 0;

			if (LORMembership4.sortMode == LORMembership4.SORTbySavedIndex)
			{
				result = VizID.CompareTo(other.SavedIndex);
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
						result = myAltVizID.CompareTo(other.AltSavedIndex);
					}
					else
					{
						if (LORMembership4.sortMode == LORMembership4.SORTbyOutput)
						{
							//LORChannel4 ch = (LORChannel4)other;
							//output.ToString().CompareTo(ch.output.ToString());
							//TODO Fix all other LORMembers to impliment this same comparison
							result = redChannel.UniverseNumber.CompareTo(other.UniverseNumber);
							if (result == 0)
							{
								result = redChannel.DMXAddress.CompareTo(other.DMXAddress);
							}
						}
					}
				}
			}

			return result;
		}
		public bool Written
		{
			get
			{
				bool r = false;
				if (myAltVizID > lutils.UNDEFINED) r = true;
				return r;
			}
		}

		public void Parse(string lineIn)
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
			VizID = lutils.getKeyValue(lineIn, LORVisualization4.FIELDvizID);
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

		public string LineOut()
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

		public LORMember4 Clone()
		{
			LORVizDrawObject4 newDO = new LORVizDrawObject4();
			newDO.VizID = VizID;
			newDO.isRGB = isRGB;
			newDO.myName = myName;
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
			newDO.myIndex = myIndex;
			newDO.myAltVizID = myAltVizID;
			newDO.imSelected = imSelected;
			newDO.matchesExactly = matchesExactly;
			newDO.myTag = myTag;
			newDO.mappedTo = mappedTo;
			newDO.isDirty = isDirty;
			newDO.parentVisualization = parentVisualization;

			return newDO;
		}

		public LORMember4 Clone(string newName)
		{
			LORMember4 newDO = Clone();
			newDO.ChangeName(newName);
			return newDO;
		}


	} // End LORVizDrawObject4 class

}
