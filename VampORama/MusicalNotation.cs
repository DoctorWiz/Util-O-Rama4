using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public static class MusicalNotation
	{
	
			// Note that the index corresponds with the MIDI note numbers
		public readonly static string[] noteNamesUnicode = {"C0","C♯0-D♭0","D0","D♯0-E♭0","E0","F0","F♯0-G♭0","G0","G♯0-A♭0","A0","A♯0-B♭0","B0",
													"C1","C♯1-D♭1","D1","D♯1-E♭1","E1","F1","F♯1-G♭1","G1","G♯1-A♭1","A1","A♯1-B♭1","B1",
													"C2","C♯2-D♭2","D2","D♯2-E♭2","E2","F2","F♯2-G♭2","Low_G","Low_G♯-A♭","Low_A","Low_A♯-B♭","Low_B",
													"Low_C","Low_C♯-D♭","Low_D","Low_D♯-E♭","Low_E","Low_F","Low_F♯-G♭","Bass_G","Bass_G♯-A♭","Bass_A","Bass_A♯-B♭","Bass_B",
													"Bass_C","Bass_C♯-D♭","Bass_D","Bass_D♯-E♭","Bass_E","Bass_F","Bass_F♯-G♭","Middle_G","Middle_G♯-A♭","Middle_A","Middle_A♯-B♭","Middle_B",
													"Middle_C","Middle_C♯-D♭","Middle_D","Middle_D♯-Eb","Middle_E","Middle_F","Treble_F♯-G♭","Treble_G","Treble_G♯-A♭","Treble_A","Treble_A♯-B♭","Treble_B",
													"Treble_C","Treble_C♯-D♭","Treble_D","Treble_D♯-E♭","Treble_E","Treble_F","High_F♯-G♭","High_G","High_G♯-A♭","High_A","High_A♯-B♭","High_B",
													"High_C","High_C♯-D♭","High_D","High_D♯-E♭","High_E","High_F","F♯7-G♭7","G7","G♯7-A♭7","A7","A♯7-B♭7","B7",
													"C8","C♯8-D♭8","D8","D♯8-E♭8","E8","F8","F♯8-G♭8","G8","G♯8-A♭8","A8","A♯8-B♭8","B8",
													"C9","C♯9-D♭9","D9","D♯9-E♭9","E9","F9","F♯9-G♭9","G9","G♯9-A♭9","A9","A♯9-B♭9","B9",
													"C10","C♯10-D♭10","D10","D♯10-E♭10","E10","F10","F♯10-G♭10","G10"};
		// For things that DON'T support Unicode...
		public readonly static string[] noteNamesASCII = {"C0","C#0-Db0","D0","D#0-Eb0","E0","F0","F#0-Gb0","G0","G#0-Ab0","A0","A#0-Bb0","B0",
													"C1","C#1-Db1","D1","D#1-Eb1","E1","F1","F#1-Gb1","G1","G#1-Ab1","A1","A#1-Bb1","B1",
													"C2","C#2-Db2","D2","D#2-Eb2","E2","F2","F#2-Gb2","Low_G","Low_G#-Ab","Low_A","Low_A#-Bb","Low_B",
													"Low_C","Low_C#-Db","Low_D","Low_D#-Eb","Low_E","Low_F","Low_F#-Gb","Bass_G","Bass_G#-Ab","Bass_A","Bass_A#-Bb","Bass_B",
													"Bass_C","Bass_C#-Db","Bass_D","Bass_D#-Eb","Bass_E","Bass_F","Bass_F#-Gb","Middle_G","Middle_G#-Ab","Middle_A","Middle_A#-Bb","Middle_B",
													"Middle_C","Middle_C#-Db","Middle_D","Middle_D#-Eb","Middle_E","Middle_F","Treble_F#-Gb","Treble_G","Treble_G#-Ab","Treble_A","Treble_A#-Bb","Treble_B",
													"Treble_C","Treble_C#-Db","Treble_D","Treble_D#-Eb","Treble_E","Treble_F","High_F#-Gb","High_G","High_G#-Ab","High_A","High_A#-Bb","High_B",
													"High_C","High_C#-Db","High_D","High_D#-Eb","High_E","High_F","F#7-Gb7","G7","G#7-Ab7","A7","A#7-Bb7","B7",
													"C8","C#8-Db8","D8","D#8-Eb8","E8","F8","F#8-Gb8","G8","G#8-Ab8","A8","A#8-Bb8","B8",
													"C9","C#9-Db9","D9","D#9-Eb9","E9","F9","F#9-Gb9","G9","G#9-Ab9","A9","A#9-Bb9","B9",
													"C10","C#10-Db10","D10","D#10-Eb10","E10","F10","F#10-Gb10","G10"};

		//																								 C 	  C♯-D♭        D    D♯-E♭        E        F    F♯-G♭        G    G♯-A♭        A    A♯-B♭        B
		public readonly static string[] noteFreqs = { "16.35", "17.32", "18.35", "19.45", "20.60", "21.83", "23.12", "24.50", "25.96", "27.50", "29.14", "30.87",
																									"32.70", "34.65", "36.71", "38.89", "41.20", "43.65", "46.25", "49.00", "51.91", "55.00", "58.27", "61.74",
																									"65.41", "69.30", "73.42", "77.78", "82.41", "87.31", "92.50", "98.00","103.83","110.00","116.54","123.47",
																								 "130.81","138.59","146.83","155.56","164.81","174.61","185.00","196.00","207.65","220.00","233.08","246.94",
																								 "261.63","277.18","293.66","311.13","329.63","349.23","369.99","392.00","415.30","440.00","466.16","493.88",
																								 "523.25","554.37","587.33","622.25","659.25","698.46","739.99","783.99","830.61","880,00","932.33","987.77",
																								"1046.5","1108.7","1174.7","1244.5","1318.5","1396.9","1480.0","1568.0","1661.2","1760.0","1864.7","1975.3",
																								"2093.0","2217.5","2349.3","2489.0","2637.0","2793.8","2960.0","3136.0","3322.4","3520.0","3729.3","3951.1",
																								"4186.0","4434.2","4698.6","4978.0","5274.0","5587.6","5919.9","6271.9","6644.9","7040.0","7458.6","7902.1",
																								"8372",  "8870",  "9397",  "9956", "10548", "11175", "11840", "12544", "13290", "14080", "14917", "15804",
																							 "16744", "17740", "18795", "19912", "21096", "22351", "23680", "25088", "26579", "28160", "29834", "31608" };


		//																										C 			C♯-D♭   D				D♯-E♭   E		    F				F♯-G♭   G				G♯-A♭   A				A♯-B♭   B
		public readonly static double[] noteFrequencies = { 16.35D, 17.32D, 18.35D, 19.45D, 20.60D, 21.83D, 23.12D, 24.50D, 25.96D, 27.50D, 29.14D, 30.87D,
																												32.70D, 34.65D, 36.71D, 38.89D, 41.20D, 43.65D, 46.25D, 49.00D, 51.91D, 55.00D, 58.27D, 61.74D,
																												65.41D, 69.30D, 73.42D, 77.78D, 82.41D, 87.31D, 92.50D, 98.00D,103.83D,110.00D,116.54D,123.47D,
																											 130.81D,138.59D,146.83D,155.56D,164.81D,174.61D,185.00D,196.00D,207.65D,220.00D,233.08D,246.94D,
																											 261.63D,277.18D,293.66D,311.13D,329.63D,349.23D,369.99D,392.00D,415.30D,440.00D,466.16D,493.88D,
																											 523.25D,554.37D,587.33D,622.25D,659.25D,698.46D,739.99D,783.99D,830.61D,880D,00D,932.33D,987.77D,
																											1046.5D,1108.7D,1174.7D,1244.5D,1318.5D,1396.9D,1480.0D,1568.0D,1661.2D,1760.0D,1864.7D,1975.3D,
																											2093.0D,2217.5D,2349.3D,2489.0D,2637.0D,2793.8D,2960.0D,3136.0D,3322.4D,3520.0D,3729.3D,3951.1D,
																											4186.0D,4434.2D,4698.6D,4978.0D,5274.0D,5587.6D,5919.9D,6271.9D,6644.9D,7040.0D,7458.6D,7902.1D,
																											8372D,  8870D,  9397D,  9956D, 10548D, 11175D, 11840D, 12544D, 13290D, 14080D, 14917D, 15804D,
																										 16744D, 17740D, 18795D, 19912D, 21096D, 22351D, 23680D, 25088D, 26579D, 28160D, 29834D, 31608 };



		public readonly static string[] octaveFreqs = { "0-31", "31-63", "63-127", "127-253", "253-507", "507-1015", "1015-2034", "2035-4068", "4068-8137", "8137-16274", "16274-32000", "Err", "Err" };

		public readonly static string[] octaveNamesA = { "CCCCCC 128'", "CCCCC 64'", "CCCC 32'", "CCC 16'", "CC 8'", "C4'",	"c1 2'", "c2 1'",
																										"c3 1/2'", "c4 1/4'", "c5 1/8'", "c6 1/16'", "Err", "Err" };
		public readonly static string[] octaveNamesB = { "Sub-Sub-Sub Contra", "Sub-Sub Contra", "Sub-Contra", "Contra", "Great", "Small", "1-Line", "2-Line", "3-Line", "4-Line", "5-Line", "6-Line", "Err", "Err" };

		public readonly static string[] chromaNamesUnicode = { "C", "C♯-D♭", "D", "D♯-E♭", "E", "F", "F♯-G♭", "G", "G♯-A♭", "A", "A♯-B♭", "B" };
		public readonly static string[] chromaNamesAscii = { "C", "C#-Db", "D", "D#-Eb", "E", "F", "F#-Gb", "G", "G#-Ab", "A", "A#-Bb", "B" };
		public readonly static string[] keyNamesUnicode = { "", "C major", "C♯ major", "D major", "D♯ major", "E major", "F major", "F♯ major", "G major", "G♯ major", "A major", "A♯ major", "B major",
															"C minor", "C♯ minor", "D minor", "D♯ minor", "E minor", "F minor", "F♯ minor", "G minor", "G♯ minor", "A minor", "A♯ minor", "B minor" };
		public readonly static string[] keyNamesAscii = { "", "C major", "C# major", "D major", "D# major", "E major", "F major", "F# major", "G major", "G# major", "A major", "A# major", "B major",
															"C minor", "C# minor", "D minor", "D# minor", "E minor", "F minor", "F# minor", "G minor", "G# minor", "A minor", "A# minor", "B minor" };
	}
}
