using System;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Text;
using System.Threading.Tasks;

namespace ChristmassTimingTrax
{
    class Utils
    {
        #region Utility Methods
        public static DataTable CreateDataTable(Type LayoutType)
        {
            DataTable retValue = new DataTable();
            foreach (PropertyInfo info in LayoutType.GetProperties())
            {
                retValue.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            return retValue;
        }

        public static double ConvertSecondsToMilliseconds(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
        }
        
        #endregion
    }

    class MIDI
    {
        #region MIDI Methods
        private static List<MIDIFerquencies> _MIDIFrequencies = new List<MIDIFerquencies>();

        public static List<MIDIFerquencies> GetFrequencyInformation
        {
            get
            {
                if (_MIDIFrequencies.Count == 0)
                    SetFrequencyInfo();
                return _MIDIFrequencies;
            }
        }

        public static string GetFrequencyName(int Frequency)
        {
            string retValue = "";
            List<MIDIFerquencies> lstFrequencies = GetFrequencyInformation;
            foreach (var Freq in lstFrequencies)
            {
                if (Freq.Frequency == Frequency)
                {
                    retValue = Freq.NoteName;
                    break;
                }
            }
            return retValue;
        }

        private static void SetFrequencyInfo()
        {
            var FrequencyInfo = new List<MIDIFerquencies>();
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 0, NoteName = "C", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 1, NoteName = "C#-Db", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 2, NoteName = "D", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 3, NoteName = "D#-Eb", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 4, NoteName = "E", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 5, NoteName = "F", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 6, NoteName = "F#-Gb", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 7, NoteName = "G", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 8, NoteName = "G#-Ab", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 9, NoteName = "A", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 10, NoteName = "A#-Bb", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 11, NoteName = "B", Octave = 0 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 12, NoteName = "C", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 13, NoteName = "C#-Db", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 14, NoteName = "D", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 15, NoteName = "D#-Eb", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 16, NoteName = "E", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 17, NoteName = "F", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 18, NoteName = "F#-Gb", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 19, NoteName = "G", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 20, NoteName = "G#-Ab", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 21, NoteName = "A", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 22, NoteName = "A#-Bb", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 23, NoteName = "B", Octave = 1 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 24, NoteName = "C", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 25, NoteName = "C#-Db", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 26, NoteName = "D", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 27, NoteName = "D#-Eb", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 28, NoteName = "E", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 29, NoteName = "F", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 30, NoteName = "F#-Gb", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 31, NoteName = "Low_G", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 32, NoteName = "Low_G#-Ab", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 33, NoteName = "Low_A", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 34, NoteName = "Low_A#-Bb", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 35, NoteName = "Low_B", Octave = 2 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 36, NoteName = "Low_C", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 37, NoteName = "Low_C#-Db", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 38, NoteName = "Low_D", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 39, NoteName = "Low_D#-Eb", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 40, NoteName = "Low_E", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 41, NoteName = "Low_F", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 42, NoteName = "Low_F#-Gb", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 43, NoteName = "Bass_G", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 44, NoteName = "Bass_G#-Ab", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 45, NoteName = "Bass_A", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 46, NoteName = "Bass_A#-Bb", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 47, NoteName = "Bass_B", Octave = 3 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 48, NoteName = "Bass_C", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 49, NoteName = "Bass_C#-Db", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 50, NoteName = "Bass_D", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 51, NoteName = "Bass_D#-Eb", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 52, NoteName = "Bass_E", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 53, NoteName = "Bass_F", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 54, NoteName = "Bass_F#-Gb", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 55, NoteName = "Middle_G", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 56, NoteName = "Middle_G#-Ab", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 57, NoteName = "Middle_A", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 58, NoteName = "Middle_A#-Bb", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 59, NoteName = "Middle_B", Octave = 4 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 60, NoteName = "Middle_C", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 61, NoteName = "Middle_C#-Db", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 62, NoteName = "Middle_D", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 63, NoteName = "Middle_D#-Eb", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 64, NoteName = "Middle_E", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 65, NoteName = "Middle_F", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 66, NoteName = "Treble_F#-Gb", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 67, NoteName = "Treble_G", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 68, NoteName = "Treble_G#-Ab", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 69, NoteName = "Treble_A", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 70, NoteName = "Treble_A#-Bb", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 71, NoteName = "Treble_B", Octave = 5 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 72, NoteName = "Treble_C", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 73, NoteName = "Treble_C#-Db", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 74, NoteName = "Treble_D", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 75, NoteName = "Treble_D#-Eb", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 76, NoteName = "Treble_E", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 77, NoteName = "Treble_F", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 78, NoteName = "High_F#-Gb", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 79, NoteName = "High_G", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 80, NoteName = "High_G#-Ab", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 81, NoteName = "High_A", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 82, NoteName = "High_A#-Bb", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 83, NoteName = "High_B", Octave = 6 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 84, NoteName = "High_C", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 85, NoteName = "High_C#-Db", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 86, NoteName = "High_D", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 87, NoteName = "High_D#-Eb", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 88, NoteName = "High_E", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 89, NoteName = "High_F", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 90, NoteName = "F#-Gb", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 91, NoteName = "G", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 92, NoteName = "G#-Ab", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 93, NoteName = "A", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 94, NoteName = "A#-Bb", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 95, NoteName = "B", Octave = 7 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 96, NoteName = "C", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 97, NoteName = "C#-Db", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 98, NoteName = "D", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 99, NoteName = "D#-Eb", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 100, NoteName = "E", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 101, NoteName = "F", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 102, NoteName = "F#-Gb", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 103, NoteName = "G", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 104, NoteName = "G#-Ab", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 105, NoteName = "A", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 106, NoteName = "A#-Bb", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 107, NoteName = "B", Octave = 8 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 108, NoteName = "C", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 109, NoteName = "C#-Db", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 110, NoteName = "D", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 111, NoteName = "D#-Eb", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 112, NoteName = "E", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 113, NoteName = "F", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 114, NoteName = "F#-Gb", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 115, NoteName = "G", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 116, NoteName = "G#-Ab", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 117, NoteName = "A", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 118, NoteName = "A#-Bb", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 119, NoteName = "B", Octave = 9 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 120, NoteName = "C", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 121, NoteName = "C#-Db", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 122, NoteName = "D", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 123, NoteName = "D#-Eb", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 124, NoteName = "E", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 125, NoteName = "F", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 126, NoteName = "F#-Gb", Octave = 10 });
            FrequencyInfo.Add(new MIDIFerquencies() { Frequency = 127, NoteName = "G", Octave = 10 });
            _MIDIFrequencies = FrequencyInfo;
        }

        #endregion
    }

}
