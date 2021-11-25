using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using xUtils;

namespace xUtils
{ 
	public class xEffect
	{
		public string xlabel = "";
		private int _starttime = 0;
		private int _endtime = 999999999;
		private static int lastEnd = 0;

		private static string TABLE_Effect = "LOREffect4";
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
			//int roundStart = RoundTime(Starttime);
			//int roundEnd = RoundTime(Endtime);
			int roundStart = Starttime;
			int roundEnd = Endtime;
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
			//int roundStart = RoundTime(Starttime);
			//int roundEnd = RoundTime(Endtime);
			int roundStart = Starttime;
			int roundEnd = Endtime;
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
			//int roundEnd = RoundTime(Endtime);
			//int roundStart = RoundTime(Starttime);
			int roundEnd = Endtime;
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
			//int roundStart = RoundTime(Starttime);
			//int roundEnd = RoundTime(Endtime);
			int roundEnd = Endtime;
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
				//int roundStart = RoundTime(Starttime);
				//int roundEnd = RoundTime(Endtime);
				int roundStart = value;
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
				return _endtime;
			}
			set
			{
				//int roundStart = RoundTime(Starttime);
				//int roundEnd = RoundTime(Endtime);
				int roundEnd = value;
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
			//    <LOREffect4 
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

		public string ToString()
		{
			return xlabel;
		}

	}

	public class xTimings
	{
		public string timingName = "";
		//public string sourceVersion = "2019.32";
		public List<xEffect> effects = new List<xEffect>();
		//public int effectCount = 0;
		public readonly static string SourceVersion = "2021.20";
		public readonly static string XMLinfo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		private int maxMillis = 0;

		private readonly static string TABLE_timing = "timing";
		private readonly static string FIELD_name = "name";
		private readonly static string FIELD_start = "starttime";
		private readonly static string FIELD_end = "endtime";
		private readonly static string FIELD_label = "label";
		private readonly static string FIELD_source = "SourceVersion";
		private readonly static string TABLE_layers = "EffectLayer";
		private readonly static string TABLE_effect = "LOREffect4";
		private readonly static string LEVEL0 = "";
		private readonly static string LEVEL1 = "  ";
		private readonly static string LEVEL2 = "    ";
		private readonly static string RECORD_start = "<";
		private readonly static string RECORD_end = ">";
		private readonly static string RECORD_finish = " />";
		private readonly static string SPC = " ";
		private readonly static string PLURAL = "s";
		private readonly static string CRLF = "\r\n";
		private readonly static string VALUE_start = "=\"";
		private readonly static string VALUE_end = "\"";
		private readonly static string RECORDS_done = "</";

		public xTimings(string theName)
		{
			timingName = theName;
		}

		public void Add(xEffect newEffect)
		{
			if (effects.Count > 0)
			{
				if (newEffect.starttime < effects[effects.Count - 1].endtime)
				{
					System.Diagnostics.Debugger.Break();
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
		}

		public xEffect Add(string theLabel, int Starttime, int Endtime)
		{
			xEffect newEff = new xEffect(theLabel, Starttime, Endtime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(int Starttime, int Endtime)
		{
			xEffect newEff = new xEffect(Starttime, Endtime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(int Endtime)
		{
			xEffect newEff = new xEffect(Endtime);
			Add(newEff);
			return newEff;
		}

		public xEffect Add(string theLabel, int Endtime)
		{
			xEffect newEff = new xEffect(theLabel, Endtime);
			Add(newEff);
			return newEff;
		}

		public void Clear()
		{
			timingName = "";
			//effects = null;
			effects = new List<xEffect>();
			//effectCount = 0;
		}

		public int EndTime
		{
			get
			{
				int ret = xutils.UNDEFINED;
				if (effects.Count > 0)
				{
					ret = effects[effects.Count - 1].endtime;
				}
				return ret;
			}
		}

		public int StartTime
		{
			get
			{
				int ret = xutils.UNDEFINED;
				if (effects.Count > 0)
				{
					ret = effects[0].starttime;
				}
				return ret;
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
			ret.Append(SourceVersion);
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
	
		public string ToString()
		{
			return timingName;
		}

		public int Milliseconds
		{
			get
				{
				return maxMillis;
			}
		}

		public static int WriteTimingsFile(xTimings[] timeTracks, string fileName)
		{
			int err = 0;
			bool lyrics = false;
			StringBuilder lineOut = new StringBuilder();
			StreamWriter writer = new StreamWriter(fileName);
			writer.WriteLine(xTimings.XMLinfo);
			if (timeTracks.Length == 3)
			{
				if ((timeTracks[0].timingName.CompareTo(timeTracks[1].timingName)==0) && (timeTracks[0].timingName.CompareTo(timeTracks[2].timingName)==0))
				{
					// 3 tracks all with the same names, almost certainly a set of lyric tracks
					lyrics = true;
				}
			}
			if (lyrics)
			{
				// <timing name="Lyrics" SourceVersion="2021.20">
				lineOut.Append(RECORD_start);
				lineOut.Append(SPC);
				lineOut.Append(FIELD_name);
				lineOut.Append(VALUE_start);
				string name = timeTracks[0].timingName;
				name = LORUtils4.lutils.XMLifyName(name);
				lineOut.Append(name);
				lineOut.Append(VALUE_end);
				lineOut.Append(SPC);
				lineOut.Append(FIELD_source);
				lineOut.Append(VALUE_start);
				lineOut.Append(SourceVersion);
				lineOut.Append(VALUE_end);
				lineOut.Append(RECORD_end);
				writer.WriteLine(lineOut.ToString());

				for (int i=0; i< timeTracks.Length; i++)
				{
					//    <EffectLayer>
					lineOut.Clear();
					lineOut.Append(LEVEL1);
					lineOut.Append(RECORD_start);
					lineOut.Append(TABLE_layers);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

					WriteTimings(timeTracks[i], writer, true);

					//    </EffectLayer>
					lineOut.Clear();
					lineOut.Append(LEVEL1);
					lineOut.Append(RECORDS_done);
					lineOut.Append(TABLE_layers);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());
				} // End loop thru 3 lyric timing tracks

				//  </timing>
				lineOut.Clear();
				lineOut.Append(RECORDS_done);
				lineOut.Append(TABLE_timing);
				lineOut.Append(RECORD_end);
				writer.WriteLine(lineOut.ToString());

			} // End lyric tracks
			else
			{
				// Single Timing LORTrack4
				if (timeTracks.Length == 1)
				{
					// <timing name="Whatever" SourceVersion="2021.20">
					lineOut.Append(RECORD_start);
					lineOut.Append(SPC);
					lineOut.Append(FIELD_name);
					lineOut.Append(VALUE_start);
					string name = timeTracks[0].timingName;
					name = LORUtils4.lutils.XMLifyName(name);
					lineOut.Append(name);
					lineOut.Append(VALUE_end);
					lineOut.Append(SPC);
					lineOut.Append(FIELD_source);
					lineOut.Append(VALUE_start);
					lineOut.Append(SourceVersion);
					lineOut.Append(VALUE_end);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

					//    <EffectLayer>
					lineOut.Clear();
					lineOut.Append(LEVEL1);
					lineOut.Append(RECORD_start);
					lineOut.Append(TABLE_layers);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

					WriteTimings(timeTracks[0], writer, true);

					//    </EffectLayer>
					lineOut.Clear();
					lineOut.Append(LEVEL1);
					lineOut.Append(RECORDS_done);
					lineOut.Append(TABLE_layers);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

					//  </timing>
					lineOut.Clear();
					lineOut.Append(RECORDS_done);
					lineOut.Append(TABLE_timing);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());
				}
				else // Multiple Seperate Timing Tracks
				{
					// <timings>
					lineOut.Append(RECORD_start);
					lineOut.Append(TABLE_timing);
					lineOut.Append(PLURAL);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

					for (int i=0; i < timeTracks.Length; i++)
					{
						//   <timing name="Whatever" SourceVersion="2021.20">
						lineOut.Clear();
						lineOut.Append(LEVEL1);
						lineOut.Append(RECORD_start);
						lineOut.Append(SPC);
						lineOut.Append(FIELD_name);
						lineOut.Append(VALUE_start);
						string name = timeTracks[0].timingName;
						name = LORUtils4.lutils.XMLifyName(name);
						lineOut.Append(name);
						lineOut.Append(VALUE_end);
						lineOut.Append(SPC);
						lineOut.Append(FIELD_source);
						lineOut.Append(VALUE_start);
						lineOut.Append(SourceVersion);
						lineOut.Append(VALUE_end);
						lineOut.Append(RECORD_end);
						writer.WriteLine(lineOut.ToString());

						//    <EffectLayer>
						lineOut.Clear();
						lineOut.Append(LEVEL2);
						lineOut.Append(RECORD_start);
						lineOut.Append(TABLE_layers);
						lineOut.Append(RECORD_end);
						writer.WriteLine(lineOut.ToString());

						WriteTimings(timeTracks[i], writer, true);

						//    </EffectLayer>
						lineOut.Clear();
						lineOut.Append(LEVEL2);
						lineOut.Append(RECORDS_done);
						lineOut.Append(TABLE_layers);
						lineOut.Append(RECORD_end);
						writer.WriteLine(lineOut.ToString());

						//  </timing>
						lineOut.Clear();
						lineOut.Append(LEVEL1);
						lineOut.Append(RECORDS_done);
						lineOut.Append(TABLE_timing);
						lineOut.Append(RECORD_end);
						writer.WriteLine(lineOut.ToString());
					} // End loop timeTracks

						//  </timings>
					lineOut.Clear();
					lineOut.Append(RECORDS_done);
					lineOut.Append(TABLE_timing);
					lineOut.Append(PLURAL);
					lineOut.Append(RECORD_end);
					writer.WriteLine(lineOut.ToString());

				} // End Single time track or multiple separate tracks
			}
			writer.Close();

			return err;
		}

		public static int WriteTimings(xTimings timings, StreamWriter writer, bool indent = false)
		{
			int err = 0;

			string label = "";
			string level0 = "";
			string level1 = "  ";
			string level2 = "    ";
			if (indent)
			{
				level0 = "  ";
				level1 = "    ";
				level2 = "      ";
			}
			xEffect effect = null;

			StringBuilder ret = new StringBuilder();
			for (int i = 0; i < timings.effects.Count; i++)
			{
				effect = timings.effects[i];
				ret.Append(level2);
				ret.Append(xTimings.RECORD_start);
				ret.Append(xTimings.TABLE_effect);
				ret.Append(xTimings.SPC);
				//  label="foo" 
				ret.Append(xTimings.FIELD_label);
				ret.Append(xTimings.VALUE_start);
				string lbl = LORUtils4.lutils.XMLifyName(label);
				ret.Append(lbl);
				ret.Append(xTimings.VALUE_end);
				ret.Append(xTimings.SPC);
				//  starttime="50" 
				ret.Append(xTimings.FIELD_start);
				ret.Append(xTimings.VALUE_start);
				ret.Append(timings.effects[i].starttime.ToString());
				ret.Append(xTimings.VALUE_end);
				ret.Append(xTimings.SPC);
				//  endtime="350" />
				ret.Append(xTimings.FIELD_end);
				ret.Append(xTimings.VALUE_start);
				ret.Append(timings.effects[i].endtime.ToString());
				ret.Append(xTimings.VALUE_end);

				ret.Append(xTimings.RECORD_finish);
				ret.Append(xTimings.CRLF);
			}

			return err;
		}

		public static xTimings[] ReadTimingsFile(string fileName)
		{
			int err = 0;
			bool multi = false;
			bool lyrics = false;
			string lineIn = "";
			string name = "";
			int timingCount = 0;
			xTimings[] timeTracks = null;
			try
			{
				StreamReader reader = new StreamReader(fileName);
				if (!reader.EndOfStream)
				//if ((lineIn = reader.ReadLine()) != null)
				{
					lineIn = reader.ReadLine();
					int i = lineIn.IndexOf("<?xml version=\"1.0\"");
					if (i>=0)
					{
						if (!reader.EndOfStream)
						//if ((lineIn = reader.ReadLine()) != null)
						{
							lineIn = reader.ReadLine();
							i = lineIn.IndexOf("<timings>");
							if (i >= 0)
							{
								// Multiple seperate timing tracks
								multi = true;
								if (!reader.EndOfStream)
								{
									lineIn = reader.ReadLine();
								}
							}
							i = lineIn.IndexOf("<timing name=");
							if (i >= 0)
							{
								// Get the name
								name = xutils.getKeyWord(lineIn, "name");
								name = LORUtils4.lutils.HumanizeName(name);
							}
							//while ((lineIn = reader.ReadLine()) != null)
							while (!reader.EndOfStream)
							{
								lineIn = reader.ReadLine();
								i = lineIn.IndexOf("</timing>");
								if (i >= 0)
								{
									// Ignore, move on to the next line
									//lineIn = reader.ReadLine();
								}
								else
								{
									i = lineIn.IndexOf("<timing name=");
									if (i >= 0)
									{
										// Get the name
										name = xutils.getKeyWord(lineIn, "name");
										name = LORUtils4.lutils.HumanizeName(name);
									}
									else
									{
										i = lineIn.IndexOf("<EffectLayer>");
										if (i >= 0)
										{
											// Start processing timings
											xTimings times = ReadTimings(reader, name);
											timingCount++;
											Array.Resize(ref timeTracks, timingCount);
											timeTracks[timingCount - 1] = times;
										}
									}
								}
							}
							//if ((lineIn = reader.ReadLine()) != null)
							//if (!reader.EndOfStream)
							//{
							//	lineIn = reader.ReadLine();
							//	int i2 = lineIn.IndexOf("<timing name=");
							//	if (i2 > 0)
							//	{
							//		name = xutils.getKeyWord(lineIn, "name");
							//	}
							//}
						}
					}
				}
				reader.Close();
				if (timeTracks.Length == 3)
				{
					if ((timeTracks[0].timingName.CompareTo(timeTracks[1].timingName) == 0) && (timeTracks[0].timingName.CompareTo(timeTracks[2].timingName) == 0))
					{
						// If 3 timing tracks, and all have the same name, safe to assume it is lyric tracks
						timeTracks[0].timingName += " - Lines";
						timeTracks[1].timingName += " - Words";
						timeTracks[2].timingName += " - Phonemes";
					}
				}

			}
			catch (Exception e)
			{

			}
			return timeTracks;
		}


		




		public static xTimings ReadTimings(StreamReader reader, string name)
		{
			xTimings timings = null;
			string lineIn = "";
			bool keepGoing = true;
			while (keepGoing)
			{
				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					int i = lineIn.IndexOf("</EffectLayer>");
					if (i>=0)
					{
						keepGoing = false;
					}
					else
					{
						i = lineIn.IndexOf("<LOREffect4 label=");
						if (i>=0)
						{
							string label = xutils.getKeyWord(lineIn, FIELD_label);
							label = LORUtils4.lutils.HumanizeName(label);
							int start = xutils.getKeyValue(lineIn, FIELD_start);
							int end = xutils.getKeyValue(lineIn, FIELD_end);
							if (end >= start)
							{
								if (timings == null)
								{
									timings = new xTimings(name);
								}
								xEffect eff = new xEffect(label, start, end);
								timings.Add(eff);
							}
						}
					}
				}
			}
			return timings;
		}

	}


}
