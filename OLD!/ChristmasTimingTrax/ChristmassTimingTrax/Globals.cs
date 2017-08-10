using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassTimingTrax
{
    class Globals
    {
        public static string LastInputFile_Audacity
        {
            get { return Properties.Settings.Default.LastInputFile_Audacity; }
            set { Properties.Settings.Default.LastInputFile_Audacity = value; Properties.Settings.Default.Save(); }
        }

        public static bool LastInputFileIsPoly_Audacity
        {
            get { return Properties.Settings.Default.LastInputFileIsPoly_Audacity; }
            set { Properties.Settings.Default.LastInputFileIsPoly_Audacity = value; Properties.Settings.Default.Save(); }
        }

        public static string LastOutputFolder
        {
            get { return Properties.Settings.Default.LastOutputFolder; }
            set { Properties.Settings.Default.LastOutputFolder = value; Properties.Settings.Default.Save(); }
        }

        public static string LastOutputFileName
        {
            get { return Properties.Settings.Default.LastOutputFileName; }
            set { Properties.Settings.Default.LastOutputFileName = value; Properties.Settings.Default.Save(); }
        }

        public static string LastOutputFormat
        {
            get { return Properties.Settings.Default.LastOutputFormat; }
            set { Properties.Settings.Default.LastOutputFormat = value; Properties.Settings.Default.Save(); }
        }

        public static   bool LastOutputFileNameUseFrequencyName
        {
            get { return Properties.Settings.Default.LastOutputFileNameUseFrequencyName; }
            set { Properties.Settings.Default.LastOutputFileNameUseFrequencyName = value; Properties.Settings.Default.Save(); }
        }

        public static List<NameValuePair> AvailableOutputs
        {
            get
            {
                List<NameValuePair> AvailableOutputFormats = new List<NameValuePair>();
                AvailableOutputFormats.Add(new NameValuePair() { Name = "Light-O-Rama", Value = "LOR" });
                AvailableOutputFormats.Add(new NameValuePair() { Name = "xLights", Value = "xLights" });
                AvailableOutputFormats.Add(new NameValuePair() { Name = "Vixen", Value = "Vixen" });
                AvailableOutputFormats.Add(new NameValuePair() { Name = "JavaScript Object Notation", Value = "JSON" });
                AvailableOutputFormats = AvailableOutputFormats.OrderBy(o => o.Name).ToList();
                return AvailableOutputFormats;
            }
        }

        public static string GetTimingFileExtention(string format)
        {
            string retValue = "";
            switch (format)
            {
                case "LOR":
                    retValue = ".lor";
                    break;
                case "xLights":
                    retValue = ".xtiming";
                    break;
                case "Vixen":
                    retValue = ".vixenTiming";
                    break;
                case "JSON":
                    retValue = ".json";
                    break;
            }
            return retValue;
        }

        public static StringCollection LastSelectedFrequencies
        {
            get { return Properties.Settings.Default.LastSelectedFrequencies; }
            set{ Properties.Settings.Default.LastSelectedFrequencies = value; Properties.Settings.Default.Save(); }
        }

    }
}
