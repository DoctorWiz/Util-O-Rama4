using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassTimingTrax
{
    class Audacity
    {
    }

    public class AudacityFileLayout
    {
        public double Start { get; set; }
        public double Stop { get; set; }
        public double Other { get; set; }
    }

    public class PolyphonicFileLayout
    {
        public double Start { get; set; }
        public double Stop { get; set; }
        public int Frequency { get; set; }
    }
}
