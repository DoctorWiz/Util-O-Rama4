using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class xTimings
	{
		public string Name = "";
		public string sourceVersion = "2021.17";
		public List<xEffect> effects = new List<xEffect>();
		//public int effectCount = 0;
		public readonly static string SourceVersion = "2019.32";
		public readonly static string XMLinfo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		private int maxMillis = 0;

		public static string TABLE_timing = "timing";
		public static string FIELD_name = "name";
		public static string FIELD_source = "SourceVersion";
		public static string TABLE_layers = "EffectLayer";
		public static string TABLE_LORtime5 = "LORTiming";
		public static string FIELD_version = "version";
		public static string TABLE_Grids = "TimingGrids";
		public static string TABLE_grid = "timingGrid";
		public static string TABLE_FreeGrid = "TimingGridFree";
		public static string TABLE_BeatChans = "BeatChannels";
		//public static string FIELD_centisecond = "centisecond";
		public static string FIELD_millisecond = "millisecond";
		public static string FIELD_saveID = "saveID";
		public static string FIELD_type = "type";
		public static string TYPE_freeform = "freeform";
		public static string PLURAL = "s";
		public static string SAVEID_X = "X";
		public static string LEVEL0 = "";
		public static string LEVEL1 = "  ";
		public static string LEVEL2 = "    ";
		public static string RECORD_start = "<";
		public static string RECORD_end = ">";
		public static string RECORD_done = "/>";
		public static string SPC = " ";
		public static string CRLF = "\r\n";
		public static string VALUE_start = "=\"";
		public static string VALUE_end = "\"";
		public static string RECORDS_done = "</";

		//public enum LabelType {  None=0, Numbers=1, NoteNames=2, MidiNumbers=3, KeyNames=4, KeyNumbers=5, Frequency=6, Letters=7 }

		//public static readonly string[] availableLabels = {"None", "Numbers", "Note Names", "MIDI Note Numbers", "Key Names",
		//"Key Numbers", "Frequency", "Letters" };


		public xTimings(string theName)
		{
			Name = theName;
		}

		public void Add(xEffect newEffect)
		{
			if (effects.Count > 0)
			{
				if (newEffect.starttime < effects[effects.Count - 1].endtime)
				{
					// Is this truly an error?  How will xLights respond?
					//System.Diagnostics.Debugger.Break();
					// Raise Exception
				}
				else
				{
					effects.Add(newEffect);
					maxMillis = newEffect.endtime;
					//					effectCount++;
					//Array.Resize(ref effects, effectCount);
					//effects[effectCount - 1] = newEffect;
				}
			}
			else
			{
				effects.Add(newEffect);
				maxMillis = newEffect.endtime;
				//effectCount = 1;
				//Array.Resize(ref effects, 1);
				//effects[0] = newEffect;
			}
			//Annotator.HighTime = newEffect.endtime;

		}

		public xEffect Add(string theLabel, int startTime, int endTime, int number)
		{
			xEffect newEff = new xEffect(theLabel, startTime, endTime);
			newEff.Number = number;
			Add(newEff);
			return newEff;
		}

		public xEffect Add(string theLabel, int startTime, int endTime)
		{
			xEffect newEff = new xEffect(theLabel, startTime, endTime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(int startTime, int endTime)
		{
			xEffect newEff = new xEffect(startTime, endTime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(int endTime)
		{
			xEffect newEff = new xEffect(endTime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(string theLabel, int endTime)
		{
			xEffect newEff = new xEffect(theLabel, endTime);
			Add(newEff);
			return newEff;
		}

		public void Clear()
		{
			Name = "";
			sourceVersion = "2019.32";
			//effects = null;
			effects = new List<xEffect>();
			//effectCount = 0;
		}

		public string LineOutX()
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
			ret.Append(Name);
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

			for (int i = 0; i < effects.Count; i++)
			{
				ret.Append(effects[i].LineOutX());
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

		public string LineOut4()
		{
			const int ver = 1;
			StringBuilder ret = new StringBuilder();
			//  	<timingGrids>
			//ret.Append(LEVEL1);
			//ret.Append(RECORD_start);
			//ret.Append(TABLE_grid);
			//ret.Append(PLURAL);
			//ret.Append(RECORD_end);
			//ret.Append(CRLF);
			//
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_grid);
			ret.Append(SPC);

			ret.Append(FIELD_saveID);
			ret.Append(VALUE_start);
			ret.Append(SAVEID_X);
			ret.Append(VALUE_end);
			ret.Append(SPC);

			ret.Append(FIELD_name);
			ret.Append(VALUE_start);
			ret.Append(Name);
			ret.Append(VALUE_end);
			ret.Append(SPC);

			ret.Append(FIELD_type);
			ret.Append(VALUE_start);
			ret.Append(TYPE_freeform);
			ret.Append(VALUE_end);
			ret.Append(RECORD_end);
			ret.Append(CRLF);

			for (int i = 0; i < effects.Count; i++)
			{
				ret.Append(effects[i].LineOut4());
			}

			//      </TimingGridFree>
			ret.Append(LEVEL2);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_grid);
			ret.Append(RECORD_end);

			return ret.ToString();
		}

		public string LineOut5()
		{
			const int ver = 1;
			StringBuilder ret = new StringBuilder();
			//  <?xml version="1.0" encoding="utf-8"?>
			ret.Append(LEVEL0);
			ret.Append(XMLinfo);
			ret.Append(CRLF);
			//  <LORTiming version="1">
			ret.Append(LEVEL0);
			ret.Append(RECORD_start);
			ret.Append(TABLE_LORtime5);
			ret.Append(SPC);
			ret.Append(FIELD_version);
			ret.Append(VALUE_start);
			ret.Append(ver.ToString());
			ret.Append(VALUE_end);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//    <TimingGrids>
			ret.Append(LEVEL1);
			ret.Append(RECORD_start);
			ret.Append(TABLE_Grids);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//    <TimingGridFree name="Full Beats - Whole Notes">
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_FreeGrid);
			ret.Append(SPC);
			ret.Append(FIELD_name);
			ret.Append(VALUE_start);
			ret.Append(Name);
			ret.Append(VALUE_end);
			ret.Append(RECORD_end);
			ret.Append(CRLF);

			for (int i = 0; i < effects.Count; i++)
			{
				ret.Append(effects[i].LineOut5());
			}

			//      </TimingGridFree>
			ret.Append(LEVEL2);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_FreeGrid);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//    </TimingGrids>
			ret.Append(LEVEL1);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_Grids);
			ret.Append(RECORD_end);
			ret.Append(CRLF);
			//    <BeatChannels />
			ret.Append(LEVEL1);
			ret.Append(RECORD_start);
			ret.Append(TABLE_BeatChans);
			ret.Append(SPC);
			ret.Append(RECORD_done);
			ret.Append(CRLF);
			//  </LORTiming>
			ret.Append(LEVEL0);
			ret.Append(RECORDS_done);
			ret.Append(TABLE_LORtime5);
			ret.Append(RECORD_end);

			return ret.ToString();
		}

		public int Milliseconds
		{
			get
			{
				return maxMillis;
			}
		}
	}


	public class xEffect : IComparable<xEffect>
	{
		public string Label = "";
		public object Tag = null;
		public int Number = -1;  // For many annotations, this will be the MIDI number
		private int _starttime = 0;
		private int _endtime = 0; //999999999;
		private static int lastEnd = 0;

		public static string TABLE_Effect = "Effect";
		public static string FIELD_label = "label";
		public static string FIELD_start = "starttime";
		public static string FIELD_end = "endtime";
		public static string TABLE_timing = "timing";
		public static string TABLE_LORtime5 = "<Timing version=\"1\">";
		public static string TABLE_Grids = "<TimingGrids>";
		public static string TABLE_FreeGrid = "";
		//public static string FIELD_centisecond = "centisecond";
		public static string FIELD_millisecond = "millisecond";
		public static string LEVEL2 = "    ";
		public static string RECORD_start = "<";
		public static string RECORD_end = "/>";
		public static string RECORD_final = ">";
		public static string SPC = " ";
		public static string CRLF = "\r\n";
		public static string VALUE_start = "=\"";
		public static string VALUE_end = "\"";

		public xEffect(string theLabel, int startTime, int endTime)
		{
			if (startTime >= endTime)
			{
				// Raise Exception
				//System.Diagnostics.Debugger.Break();
			}
			else
			{
				Label = theLabel;
				_starttime = startTime;
				_endtime = endTime;
				lastEnd = endTime;
			}
		}

		public xEffect(string theLabel, int startTime, int endTime, int number)
		{
			if (startTime >= endTime)
			{
				// Raise Exception
				//System.Diagnostics.Debugger.Break();
			}
			else
			{
				Label = theLabel;
				_starttime = startTime;
				_endtime = endTime;
				lastEnd = endTime;
				Number = number;
			}
		}

		public xEffect(int startTime, int endTime)
		{
			if (startTime >= endTime)
			{
				// Raise Exception
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				_starttime = startTime;
				_endtime = endTime;
				lastEnd = endTime;
			}
		}

		public xEffect(int endTime)
		{
			if (lastEnd >= endTime)
			{
				// Raise Exception
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				_starttime = lastEnd;
				_endtime = endTime;
				lastEnd = endTime;
			}
		}

		public xEffect(string theLabel, int endTime)
		{
			if (lastEnd >= endTime)
			{
				// Raise Exception
				System.Diagnostics.Debugger.Break();
			}
			else
			{
				Label = theLabel;
				_starttime = lastEnd;
				_endtime = endTime;
				lastEnd = endTime;
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
				if (value >= _endtime)
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
				return _endtime;
			}
			set
			{
				if (_starttime >= value)
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

		public int CompareTo(xEffect otherEffect)
		{
			return _starttime.CompareTo(otherEffect.starttime);
		}
		public string LineOutX()
		{
			StringBuilder ret = new StringBuilder();
			//    <LOR4Effect 
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_Effect);
			ret.Append(SPC);
			//  label="foo" 
			ret.Append(FIELD_label);
			ret.Append(VALUE_start);
			ret.Append(Label);
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

		public string LineOut4()
		{
			StringBuilder ret = new StringBuilder();
			int cs = _starttime / 10;
			//    <timing 
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_timing);
			ret.Append(SPC);
			//  label="foo" 
			ret.Append(FIELD_millisecond);
			ret.Append(VALUE_start);
			ret.Append(cs.ToString());
			ret.Append(VALUE_end);
			ret.Append(SPC);

			ret.Append(RECORD_end);
			ret.Append(CRLF);

			return ret.ToString();
		}

		public string LineOut5()
		{
			StringBuilder ret = new StringBuilder();
			int cs = _starttime / 10;
			//    <timing 
			ret.Append(LEVEL2);
			ret.Append(RECORD_start);
			ret.Append(TABLE_timing);
			ret.Append(SPC);
			//  label="foo" 
			ret.Append(FIELD_millisecond);
			ret.Append(VALUE_start);
			ret.Append(cs.ToString());
			ret.Append(VALUE_end);
			ret.Append(SPC);

			ret.Append(RECORD_end);
			ret.Append(CRLF);

			return ret.ToString();
		}
	}
}
