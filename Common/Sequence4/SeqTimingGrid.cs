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
	public class LORTimings4 : LORMemberBase4, iLOR4Member, IComparable<iLOR4Member>
	{
		public LOR4Utils.LORTimingGridType4 TimingGridType = LOR4Utils.LORTimingGridType4.None;
		public int spacing = lutils.UNDEFINED;
		public List<int> timings = new List<int>();
		public const string FIELDsaveID = " saveID";
		public const string TABLEtiming = "timing";
		public const string FIELDspacing = " spacing";

		//! CONSTRUCTORS
		public LORTimings4(iLOR4Member theParent, string lineIn)
		{
			myParent = theParent;
			Parse(lineIn);


#if DEBUG
			//string msg = "LORTimings4.LORTimings4(" + lineIn + ") // Constructor";
			//Debug.WriteLine(msg);
#endif
		}


		//! PROPERTIES, METHODS, ETC.

		public override LOR4MemberType MemberType
		{
			get
			{
				return LOR4MemberType.Timings;
			}
		}


		public override string LineOut()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(lutils.LEVEL2);
			ret.Append(lutils.STFLD);
			ret.Append(LOR4Sequence.TABLEtimingGrid);

			ret.Append(lutils.SetKey(FIELDsaveID, myAltID));
			if (myName.Length > 1)
			{
				ret.Append(lutils.SetKey(lutils.FIELDname, lutils.XMLifyName(myName)));
			}
			ret.Append(lutils.SetKey(lutils.FIELDtype, LOR4SeqEnums.TimingName(this.TimingGridType)));
			if (spacing > 1)
			{
				ret.Append(lutils.SetKey(FIELDspacing, spacing));
			}
			if (this.TimingGridType == LOR4Utils.LORTimingGridType4.FixedGrid)
			{
				ret.Append(lutils.ENDFLD);
			}
			else if (this.TimingGridType == LOR4Utils.LORTimingGridType4.Freeform)
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
				ret.Append(LOR4Sequence.TABLEtimingGrid);
				ret.Append(lutils.FINFLD);
			}

			return ret.ToString();
		}

		public override void Parse(string lineIn)
		{
			string seek = lutils.STFLD + LOR4Sequence.TABLEtimingGrid + FIELDsaveID;
			//int pos = lineIn.IndexOf(seek);
			int pos = lutils.ContainsKey(lineIn, seek);
			if (pos > 0)
			{
				myName = lutils.HumanizeName(lutils.getKeyWord(lineIn, lutils.FIELDname));
				myID = lutils.getKeyValue(lineIn, FIELDsaveID);
				Centiseconds = lutils.getKeyValue(lineIn, lutils.FIELDcentiseconds);
				this.TimingGridType = LOR4SeqEnums.EnumGridType(lutils.getKeyWord(lineIn, lutils.FIELDtype));
				spacing = lutils.getKeyValue(lineIn, FIELDspacing);
			}
			else
			{
				myName = lineIn;
			}
			//if (myParent != null) myParent.MakeDirty(true);
		}


		public override iLOR4Member Clone()
		{
			LORTimings4 grid = (LORTimings4)Clone();
			grid.TimingGridType = this.TimingGridType;
			grid.spacing = spacing;
			if (this.TimingGridType == LOR4Utils.LORTimingGridType4.Freeform)
			{
				foreach (int time in timings)
				{
					grid.timings.Add(time);
				}
			}
			return grid;
		}

		public override iLOR4Member Clone(string newName)
		{
			LORTimings4 grid = (LORTimings4)this.Clone();
			ChangeName(newName);
			return grid;
		}


		public int SaveID
		{
			get
			{
				return myID;
			}
		}

		public void SetSaveID(int newSaveID)
		{
			myID = newSaveID;
			myIndex = newSaveID;
		}

		public int AltSaveID
		{ get { return myAltID; } set { myAltID = value; } }


		public void AddTiming(int time)
		{
			if (timings.Count == 0)
			{
				timings.Add(time);
				if (myParent != null) myParent.MakeDirty(true);
			}
			else
			{
				if (timings[timings.Count - 1] < time)
				{
					timings.Add(time);
					if (myParent != null) myParent.MakeDirty(true);
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
					if (myParent != null) myParent.MakeDirty(true);
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
			if (myParent != null) myParent.MakeDirty(true);
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
			if (myParent != null) myParent.MakeDirty(true);
			return count;
		}
		public new int UniverseNumber
		{
			get
			{
				return lutils.UNDEFINED;
			}
		}
		public override int DMXAddress
		{
			get
			{
				return lutils.UNDEFINED;
			}
		}


	} // end timingGrid class
}

