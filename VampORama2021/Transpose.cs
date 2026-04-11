using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using xUtilities;

namespace VampORama
{
	public class Transpose
	{

		public static int ReadBeatData(string resultsFile, ref TimesCollection xTimes, xTimings alignTimes)
		{
			int err = 0;
			if ((xTimes.xBars == null) || (!xTimes.Reuse))
			{
				int onsetCount = 0;
				string lineIn = "";
				int lastBeat = 0;
				int lastBar = -1;
				int beatLength = 0;
				int ppos = 0;
				int millisecs = 0;
				int subBeat = 0;
				int subSubBeat = 0;
				int subSubSubBeat = 0;
				int theTime = 0;

				int countLines = 0;
				int countBars = 1;
				int countBeats = xTimes.FirstBeat;
				int countHalves = 1;
				int countThirds = 1;
				int countQuarters = 1;
				int maxBeats = xTimes.BeatsPerBar;
				int maxHalves = xTimes.BeatsPerBar * 2;
				int maxThirds = xTimes.BeatsPerBar * 3;
				int maxQuarters = xTimes.BeatsPerBar * 4;

				//int align = GetAlignment(cboAlignBarsBeats.Text);



				// Pass 1, count lines
				StreamReader reader = new StreamReader(resultsFile);
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					countLines++;
				}
				reader.Close();

				xTimes.xBars = new xTimings("Bars" + " (Whole notes, (" + xTimes.BeatsPerBar.ToString() + " Quarter notes))");
				xTimes.xBeatsFull = new xTimings("Beats-Full (Quarter notes)");
				xTimes.xBeatsHalf = new xTimings("Beats-Half (Eighth notes)");
				xTimes.xBeatsThird = new xTimings("Beats-Third (Twelth notes)");
				xTimes.xBeatsQuarter = new xTimings("Beats-Quarter (Sixteenth notes)");

				// Pass 2, read data into arrays
				reader = new StreamReader(resultsFile);

				if (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();

					ppos = lineIn.IndexOf('.');
					if (ppos > xUtils.UNDEFINED)
					{

						string[] parts = lineIn.Split(',');

						millisecs = xUtils.ParseMilliseconds(parts[0]);
						millisecs = xUtils.RoundTimeTo(millisecs, align);
						lastBeat = millisecs;
						lastBar = millisecs;
					} // end line contains a period
				} // end while loop more lines remaining
				while (!reader.EndOfStream)
				{
					lineIn = reader.ReadLine();
					ppos = lineIn.IndexOf('.');
					if (ppos > xUtils.UNDEFINED)
					{

						string[] parts = lineIn.Split(',');

						millisecs = xUtils.ParseMilliseconds(parts[0]);
						// FULL BEATS - QUARTER NOTES
						millisecs = xUtils.RoundTimeTo(millisecs, alignTimes);
						beatLength = millisecs - lastBeat;
						xTimes.xBeatsFull.Add(countBeats.ToString(), lastBeat, millisecs);
						countBeats++;

						// BARS
						if (countBeats > maxBeats)
						{
							countBeats = 1;
							xTimes.xBars.Add(countBars.ToString(), lastBar, millisecs);
							countBars++;
							lastBar = millisecs;
						}

						// HALF BEATS - EIGHTH NOTES
						subBeat = lastBeat + (beatLength / 2);
						subBeat = xUtils.RoundTimeTo(subBeat, alignTimes);
						xTimes.xBeatsHalf.Add(countHalves.ToString(), lastBeat, subBeat);
						countHalves++;

						xTimes.xBeatsHalf.Add(countHalves.ToString(), subBeat, millisecs);
						countHalves++;
						if (countHalves > maxHalves) countHalves = 1;

						// THIRD BEATS - TWELTH NOTES
						subBeat = lastBeat + (beatLength / 3);
						subBeat = xUtils.RoundTimeTo(subBeat, alignTimes);
						xTimes.xBeatsThird.Add(countThirds.ToString(), lastBeat, subBeat);
						countThirds++;

						subSubBeat = lastBeat + (beatLength * 2 / 3);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, alignTimes);
						xTimes.xBeatsThird.Add(countThirds.ToString(), subBeat, subSubBeat);
						countThirds++;

						xTimes.xBeatsThird.Add(countThirds.ToString(), subSubBeat, millisecs);
						countThirds++;
						if (countThirds > maxThirds) countThirds = 1;

						// QUARTER BEATS - SIXTEENTH NOTES
						subBeat = lastBeat + (beatLength / 4);
						subBeat = xUtils.RoundTimeTo(subBeat, align);
						xTimes.xBeatsQuarter.Add(countQuarters.ToString(), lastBeat, subBeat);
						countQuarters++;

						subSubBeat = lastBeat + (beatLength / 2);
						subSubBeat = xUtils.RoundTimeTo(subSubBeat, align);
						xTimes.xBeatsQuarter.Add(countQuarters.ToString(), subBeat, subSubBeat);
						countQuarters++;

						subSubSubBeat = lastBeat + (beatLength * 3 / 4);
						subSubSubBeat = xUtils.RoundTimeTo(subSubSubBeat, align);
						xTimes.xBeatsQuarter.Add(countQuarters.ToString(), subSubBeat, subSubSubBeat);
						countQuarters++;

						xTimes.xBeatsQuarter.Add(countQuarters.ToString(), subSubSubBeat, millisecs);
						countQuarters++;
						if (countQuarters > maxQuarters) countQuarters = 1;



						lastBeat = millisecs;
						onsetCount++;
					} // end line contains a period
				} // end while loop more lines remaining

				reader.Close();
			}
			return err;
		} // end Beats













		public static int RoundTimeTo(int startTime, xTimings alignTimes )
		{
			if (alignTimes != null)
			{
				if (alignTimes.effects.Count > 0)
				{

				}
			}
			if (roundVal > 1)
			{
				int half = roundVal / 2;
				int newStart = startTime / roundVal * roundVal;
				int diff = startTime - newStart;
				if (diff > half) newStart += roundVal;
				return newStart;
			}
			else
			{
				return startTime;
			}
		}



	}
}
