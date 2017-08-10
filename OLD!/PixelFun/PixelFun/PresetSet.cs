using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PixelFun
{
    class PresetSet
    {

        public string name = "Black to White";
        public ColorChange[] colorChanges;
        public int colorChangeCount = 0;
        public int changeCount = 0;



        private static Int32 getKeyValue(string lineIn, string keyWord)
        {
            Int32 valueOut = -1;
            int pos1 = -1;
            int pos2 = -1;
            string fooo = "";

            pos1 = lineIn.IndexOf(keyWord + "=");
            if (pos1 > 0)
            {
                fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
                pos2 = fooo.IndexOf("\"");
                fooo = fooo.Substring(0, pos2);
                valueOut = Convert.ToInt32(fooo);
            }
            else
            {
                valueOut = -1;
            }

            return valueOut;
        }

        private static string getKeyWord(string lineIn, string keyWord)
        {
            string valueOut = "";
            int pos1 = -1;
            int pos2 = -1;
            string fooo = "";

            pos1 = lineIn.IndexOf(keyWord + "=");
            if (pos1 > 0)
            {
                fooo = lineIn.Substring(pos1 + keyWord.Length + 2);
                pos2 = fooo.IndexOf("\"");
                fooo = fooo.Substring(0, pos2);
                valueOut = fooo;
            }
            else
            {
                valueOut = "";
            }

            return valueOut;
        }

    
    }


}
