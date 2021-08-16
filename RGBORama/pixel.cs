using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LORUtils; using FileHelper;

namespace RGBORama
{
    class pixel
    {
        public int redIntensity = 0;    // Or StartIntensity for Ramps (in which case redEndIntensity will be >= 0)
        public int grnIntensity = 0;
        public int bluIntensity = 0;
        public EffectType redEffectType = EffectType.Intensity;
        public EffectType grnEffectType = EffectType.Intensity;
        public EffectType bluEffectType = EffectType.Intensity;
        public int redEndIntensity = -1;
        public int grnEndIntensity = -1;
        public int bluEndIntensity = -1;
        public int redEndCentisecond = -1;   // Note: redStartCentisecond is the index of this object in it's parent array
        public int grnEndCentisecond = -1;
        public int bluEndCentisecond = -1;

        public bool redEquals(pixel otherKeywdel)
        {
            bool isEqual = false;
            if (redIntensity == otherKeywdel.redIntensity &&
                redEffectType == otherKeywdel.redEffectType &&
                redEndIntensity == otherKeywdel.redEndIntensity &&
                redEndCentisecond == otherKeywdel.redEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool grnEquals(pixel otherKeywdel)
        {
            bool isEqual = false;
            if (grnIntensity == otherKeywdel.grnIntensity &&
                grnEffectType == otherKeywdel.grnEffectType &&
                grnEndIntensity == otherKeywdel.grnEndIntensity &&
                grnEndCentisecond == otherKeywdel.grnEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool bluEquals(pixel otherKeywdel)
        {
            bool isEqual = false;
            if (bluIntensity == otherKeywdel.bluIntensity &&
                bluEffectType == otherKeywdel.bluEffectType &&
                bluEndIntensity == otherKeywdel.bluEndIntensity &&
                bluEndCentisecond == otherKeywdel.bluEndCentisecond)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool Equals(pixel otherKeywdel)
        {
            bool isEqual = false;
            if (redEquals(otherKeywdel) &
                bluEquals(otherKeywdel) &
                grnEquals(otherKeywdel))
            {
                isEqual = true;
            }
            return isEqual;
        }
    }
}
