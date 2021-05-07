using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampORama
{
	public class TimesCollection
	{
		public xTimings xBars = null;
		public xTimings xBeatsFull = null;
		public xTimings xBeatsHalf = null;
		public xTimings xBeatsThird = null;
		public xTimings xBeatsQuarter = null;
		public xTimings xOnsets = null;
		public xTimings xTranscription = null;
		public xTimings xSpectrum = null;
		public xTimings xKey = null;
		public xTimings xSegments = null;
		public xTimings xAlignTo = null;

		private int beatsPerBar = 4;
		private int firstBeat = 1;
		public bool Reuse = false;
		private int stepSize = 512;
		public bool Ramps = false;
		private int fps = 20;
		private int msPF = 50;

		public TimesCollection ()
		{
			// Constructor
		}

		public int BeatsPerBar
		{
			set
			{
				beatsPerBar = value;
				if (beatsPerBar < 3) beatsPerBar = 3;
				if (beatsPerBar > 4) beatsPerBar = 4;
			}
			get
			{
				return beatsPerBar;
			}
		}

		public int FirstBeat
		{
			set
			{
				firstBeat = value;
				if (firstBeat < 1) firstBeat = 1;
				if (firstBeat > BeatsPerBar) firstBeat = BeatsPerBar;
			}
			get
			{
				return firstBeat;
			}
		}

		public int StepSize
		{
			set
			{
				stepSize = value;
				if ((stepSize < 200) || (stepSize > 800)) stepSize = 512;
			}
			get
			{
				return stepSize;
			}
		}

		public int FramesPerSecond
		{
			set
			{
				fps = value;
				if (fps < 10) fps = 10;
				if (fps > 100) fps = 100;
				msPF = 1000 / fps;
			}
			get
			{
				return fps;
			}
		}

		public int msPerFrame
		{
			set
			{
				msPF = value;
				if (msPF < 10) msPF = 10;
				if (msPF > 100) msPF = 100;
				fps = 1000 / msPF;
			}
			get
			{
				return msPF;
			}
		}


	}

}
