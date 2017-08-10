using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassTimingTrax
{
    public class TimingData
    {
        public double Start { get; set; }
        public double Stop { get; set; }
        public double Other { get; set; }
    }

    public class PolyphonicTimingData
    {
        public double Start { get; set; }
        public double Stop { get; set; }
        public int Frequency { get; set; }
        public string FrequencyName { get; set; }
    }

    public class NameValuePair
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MIDIFerquencies
    {
        public Int32 Frequency { get; set; }
        public Int32 Octave { get; set; }
        public String NoteName { get; set; }
    }
}
