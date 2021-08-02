using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xTune
{
	class xEffect
	{
		public string xlabel = "";
		private int _starttime = 0;
		private int _endtime = 999999999;
		private static int lastEnd = 0;

		private static string TABLE_Effect = "Effect";
		private static string FIELD_label = "label";
		private static string FIELD_start = "starttime";
		private static string FIELD_end = "endtime";
		private static string LEVEL2 = "    ";
		private static string RECORD_start = "<";
		private static string RECORD_end = "/>";
		private static string SPC = " ";
		private static string CRLF = "\r\n";
		private static string VALUE_start = "=\"";
		private static string VALUE_end = "\"";

		public xEffect(string theLabel, int Starttime, int Endtime)
		{
			int roundStart = RoundTime(Starttime);
			int roundEnd = RoundTime(Endtime);
			if (roundStart > roundEnd)
			{
				// Raise Exception
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				xlabel = theLabel;
				_starttime = roundStart;
				_endtime = roundEnd;
				lastEnd = roundEnd;
			}
		}

		public xEffect(int Starttime, int Endtime)
		{
			int roundStart = RoundTime(Starttime);
			int roundEnd = RoundTime(Endtime);
			if (roundStart > roundEnd)
			{
				System.Diagnostics.Debugger.Break();
				// Raise Exception
			}
			else
			{
				_starttime = roundStart;
				_endtime = roundEnd;
				lastEnd = roundEnd;
			}
		}

		public xEffect(int Endtime)
		{
			int roundEnd = RoundTime(Endtime);
			if (lastEnd > roundEnd)
			{
				System.Diagnostics.Debugger.Break();
				// Raise Exception
			}
			else
			{
				_starttime = lastEnd;
				_endtime = roundEnd;
				lastEnd = roundEnd;
			}
		}

		public xEffect(string theLabel, int Endtime)
		{
			int roundEnd = RoundTime(Endtime);
			if (lastEnd > roundEnd)
			{
				System.Diagnostics.Debugger.Break();
				// Raise Exception
			}
			else
			{
				xlabel = theLabel;
				_starttime = lastEnd;
				_endtime = roundEnd;
				lastEnd = roundEnd;
			}
		}

		public int starttime
		{
			get
			{
				return _starttime;
			}
			set
			{
				int roundStart = RoundTime(value);
				if (roundStart > _endtime)
				{
					System.Diagnostics.Debugger.Break();
					// Raise Exception
				}
				else
				{
					_starttime = value;
				}
			}
		}

		public int endtime
		{
			get
			{
				return _starttime;
			}
			set
			{
				int roundEnd = RoundTime(value);
				if (_starttime > roundEnd)
				{
					System.Diagnostics.Debugger.Break();
					// Raise Exception
				}
				else
				{
					_endtime = value;
				}
			}
		}


		public static int RoundTime(int theTime)
		{
			//double t1 = (double)theTime / 25D;
			//double t2 = Math.Round(t1);
			//int t3 = (int)t2 * 25;
			//return t3;
			//! DO NOT ROUND
			return theTime;
		}


		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();
			//    <Effect 
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_Effect);
			ret.Append(SPC);
			//  label="foo" 
			ret.Append(FIELD_label);
			ret.Append(VALUE_start);
			ret.Append(xlabel);
			ret.Append(VALUE_end);
			ret.Append(SPC);
			//  starttime="50" 
			ret.Append(FIELD_start);
			ret.Append(VALUE_start);
			ret.Append(_starttime.ToString());
			ret.Append(VALUE_end);
			ret.Append(SPC);
			//  endtime="350" />
			ret.Append(FIELD_end);
			ret.Append(VALUE_start);
			ret.Append(_endtime.ToString());
			ret.Append(VALUE_end);
			ret.Append(SPC);

			ret.Append(RECORD_end);
			ret.Append(CRLF);

			return ret.ToString();
		}

	}

	class xTimings
	{
		public string timingName = "";
		public string sourceVersion = "";
		public xEffect[] effects = null;
		public int effectCount = 0;
		public readonly static string SourceVersion = "2019.21";
		public readonly static string XMLinfo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

		private static string TABLE_timing = "timing";
		private static string FIELD_name = "name";
		private static string FIELD_source = "SourceVersion";
		private static string TABLE_layers = "EffectLayer";
		private static string LEVEL0 = "";
		private static string LEVEL1 = "  ";
		private static string RECORD_start = "<";
		private static string RECORD_end = ">";
		private static string SPC = " ";
		private static string CRLF = "\r\n";
		private static string VALUE_start = "=\"";
		private static string VALUE_end = "\"";
		private static string RECORDS_done = "</";

		public xTimings(string theName)
		{
			timingName = theName;
		}

		public void Add(xEffect newEffect)
		{
			if (effectCount > 0)
			{
				if (newEffect.starttime < effects[effectCount - 1].endtime)
				{
					System.Diagnostics.Debugger.Break();
					// Raise Exception
				}
				else
				{
					effectCount++;
					Array.Resize(ref effects, effectCount);
					effects[effectCount - 1] = newEffect;
				}
			}
			else
			{
				effectCount = 1;
				Array.Resize(ref effects, 1);
				effects[0] = newEffect;
			}
		}

		public string LineOut()
		{
			StringBuilder ret = new StringBuilder();
			//  <timing
			ret.Append(LEVEL0);
			ret.Append(RECORD_start);
			ret.Append(TABLE_timing);
			ret.Append(SPC);
			//  name="the Name"
			ret.Append(FIELD_name);
			ret.Append(VALUE_start);
			ret.Append(timingName);
			ret.Append(VALUE_end);
			ret.Append(SPC);
			//  SourceVersion="2019.21">
			ret.Append(FIELD_source);
			ret.Append(VALUE_start);
			ret.Append(sourceVersion);
			ret.Append(VALUE_end);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//    <EffectLayer>
			ret.Append(LEVEL1);
			ret.Append(RECORD_start);
			ret.Append(TABLE_layers);
			ret.Append(RECORD_end);
			ret.Append(CRLF);

			for (int i=0; i< effectCount; i++)
			{
				ret.Append(effects[i].LineOut());
			}

			//     </EffectLayer>
			ret.Append(LEVEL1);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_layers);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//  </timing>
			ret.Append(LEVEL0);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_timing);
			ret.Append(RECORD_end);

			return ret.ToString();
		}
	}
}
