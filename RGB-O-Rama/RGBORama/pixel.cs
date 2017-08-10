using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LORUtils;

namespace RGBORama
{
    class pixel
    {
        public int redIntensity = 0;    // Or StartIntensity for Ramps (in which case redEndIntensity will be >= 0)
        public int grnIntensity = 0;
        public int bluIntensity = 0;
        public effectType redEffectType = effectType.intensity;
        public effectType grnEffectType = effectType.intensity;
        public effectType bluEffectType = effectType.intensity;
        public int redEndIntensity = -1;
        public int grnEndIntensity = -1;
        public int bluEndIntensity = -1;
        public long redEndCentisecond = -1;   // Note: redStartCentisecond is the index of this object in it's parent array
        public long grnEndCentisecond = -1;
        public long bluEndCentisecond = -1;

        public bool redEquals(pixel otherPixel)
        {
            bool isEqual = false;
            if (redIntensity == otherPixel.redIntensity &&
                redEffectType == otherPixel.redEffectType &&
                redEndIntensity == otherPixel.redEndIntensity &&
                redEndCentisecond == otherPixel.redEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool grnEquals(pixel otherPixel)
        {
            bool isEqual = false;
            if (grnIntensity == otherPixel.grnIntensity &&
                grnEffectType == otherPixel.grnEffectType &&
                grnEndIntensity == otherPixel.grnEndIntensity &&
                grnEndCentisecond == otherPixel.grnEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool bluEquals(pixel otherPixel)
        {
            bool isEqual = false;
            if (bluIntensity == otherPixel.bluIntensity &&
                bluEffectType == otherPixel.bluEffectType &&
                bluEndIntensity == otherPixel.bluEndIntensity &&
                bluEndCentisecond == otherPixel.bluEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool Equals(pixel otherPixel)
        {
            bool isEqual = false;
            if (redEquals(otherPixel) &
                bluEquals(otherPixel) &
                grnEquals(otherPixel))
            {
                isEqual = true;
            }
            return isEqual;
        }
    }
}
