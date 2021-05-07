using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Configuration;
using Microsoft.Win32;
using xUtils;
using Musik;

namespace xTune
{
	public partial class frmTune : Form
	{ 

		static readonly int ALGORbarBeats = 1;
		static readonly int ALGORqmBeats = 2;
		static readonly int ALGORbeatRoot = 3;
		static readonly int ALGORportoBeat = 4;
		static readonly int ALGORaubioBeat = 5;

		static readonly int ALGORnoteOnset = 6;
		static readonly int ALGORonsetDS = 7;
		static readonly int ALGORsilvetOnset = 8;
		static readonly int ALGORaubioOnset = 9;
		static readonly int ALGORaubioPoly = 10;

		static readonly int ALGORqmPoly = 11;
		static readonly int ALGORconstq = 12;
		static readonly int ALGORchroma = 13;
		static readonly int ALGORsegments = 14;
		static readonly int ALGORspectro = 15;

		static readonly int ALGORkey = 16;
		static readonly int ALGORmelody = 17;
		static readonly int ALGORvocals = 18;
		static readonly int ALGORtempo = 19;

		static readonly string VAMPbarBeats = "vamp:qm-vamp-plugins:qm-barbeattracker:beats";
		static readonly string VAMPqmBeats = "vamp:qm-vamp-plugins:qm-tempotracker:beats";
		static readonly string VAMPbeatRoot = "vamp:beatroot-vamp:beatroot:beats";
		static readonly string VAMPportoBeat = "vamp:mvamp-ibt:marsyas_ibt:beat_times";
		static readonly string VAMPaubioBeat = "vamp:vamp-aubio:aubiotempo:beats";

		static readonly string VAMPnoteOnset = "vamp:qm-vamp-plugins:qm-onsetdetector:onsets";
		static readonly string VAMPonsetDS = "vamp:vamp-onsetsds:onsetsds:onsets";
		static readonly string VAMPsilvetOnset = "vamp:silvet:silvet:onsets";
		static readonly string VAMPaubioOnset = "vamp:vamp-aubio:aubioonset:onsets";
		static readonly string VAMPaubioPoly = "vamp:vamp-aubio:aubionotes:notes";

		static readonly string VAMPqmPoly = "vamp:qm-vamp-plugins:qm-transcription:transcription";
		static readonly string VAMPconstq = "vamp:qm-vamp-plugins:qm-constantq:constantq";
		static readonly string VAMPchroma = "vamp:qm-vamp-plugins:qm-chromagram:chromagram";
		static readonly string VAMPsegments = "vamp:qm-vamp-plugins:qm-segmenter:segmentation";
		static readonly string VAMPspectro = "vamp:qm-vamp-plugins:qm-adaptivespectrogram";
		static readonly string VAMPkey = "vamp:qm-vamp-plugins:qm-keydetector:key";
		static readonly string VAMPmelody = "vamp:mtg-melodia:melodia:melody";
		static readonly string VAMPvocals = "vamp:bbc-vamp-plugins:bbc-speechmusic-segmenter:segmentation";
		static readonly string VAMPtempo = "vamp:blahblahblah";

		static readonly string FILEbarBeats = "qm-barbeattracker.n3";
		static readonly string FILEqmBeats = "qm-tempotracker_output_beats.n3";
		static readonly string FILEbeatRoot = "vamp_beatroot_beats.n3";
		static readonly string FILEportoBeat = "mvamp-ibt_marsyas_ibt_beat_times.n3";
		static readonly string FILEaubioBeat = "vamp-aubio_aubiotempo_beats.n3";

		static readonly string FILEnoteOnset = "qm-onsetdetector.n3";
		static readonly string FILEonsetDS = "onsetsds_onsetsds_onsets.n3";
		static readonly string FILEsilvetOnset = "silvet_silvet_onsets.n3";
		static readonly string FILEaubioOnset = "aubio_aubioonset_onsets.n3";
		static readonly string FILEaubioPoly = "aubio_aubionotes_notes.n3";

		static readonly string FILEqmPoly = "qm-transcription.n3";
		static readonly string FILEconstq = "qm-constantq.n3";
		static readonly string FILEchroma = "qm-chromagram.n3";
		static readonly string FILEsegments = "qm-segmenter.n3";
		static readonly string FILEspectro = "qm-adaptivespectrogram.n3";

		static readonly string FILEkey = "qm-keydetector.n3";
		static readonly string FILEmelody = "mtg-melodia.n3";
		static readonly string FILEvocals = "bbc-speech.n3";
		static readonly string FILEtempo = "qm-tempophoo.n3";


		private const int LISTbars = 0;
		private const int LISTbeatsFull = 1;
		private const int LISTbeatsHalf = 2;
		private const int LISTbeatsThird = 3;
		private const int LISTbeatsQuarter = 4;
		private const int LISTnotes = 5;
		private const int LISTpitch = 6;
		private const int LISTsegments = 7;
		private const int LISTchroma = 8;
		private const int LISTtempo = 9;
		private const int LISTpoly = 10;
		private const int LISTconstQ = 11;
		private const int LISTflux = 12;
		private const int LISTchords = 13;
		private const int LISTvocals = 14;
		private const int LISTcount = 15;

		private const int ALIGNnone = 0;
		private const int ALIGNbar = 2;
		private const int ALIGNbeatFull = 3;
		private const int ALIGNbeatHalf = 4;
		private const int ALIGNbeatThird = 5;
		private const int ALIGNbeatQuarter = 6;
		private const int ALIGNOnset = 7;
		private const int ALIGN25 = 25;
		private const int ALIGN50 = 50;


		private static readonly string GRIDBEATS = "Beats";
		private static readonly string GRIDONSETS = "Note Onsets";
		//private const string MASTERTRACK = "Song Information [Tune-O-Rama]";
		private const string MASTERTRACK = "Beats + Song Information [xTune]";
		private const string MASTERMATCH = "Beats + Song ";
		private const string GROUPBEATS = "Bars and Beats";
		private const string GROUPPOLY = "Polyphonic Transcription";
		private const string GROUPCONSTQ = "Constant Q Spectrogram";
		private const string GROUPSEGMENTS = "Segments";
		private const string GROUPSPECTRO = "Spectrogram";
		private const string GROUPCHROMA = "Chromagram";
		private const string GROUPKEY = "Key";
		private const string GROUPMELODY = "Melody";
		private const string GROUPSPEECH = "Speech";
		private const string CHANbeats = "Beats";

		private const string ALGOqm = " (Queen Mary)";
		private const string ALGOaubio = " (Aubio)";
		private const string ALGObbc = " (BBC)";

		string[] noteNames = {"C0","C♯0-D♭0","D0","D♯0-E♭0","E0","F0","F♯0-G♭0","G0","G♯0-A♭0","A0","A♯0-B♭0","B0",
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

		//													C		 C♯-D♭        D    D♯-E♭        E        F    F♯-G♭        G    G♯-A♭        A    A♯-B♭        B
		string[] noteFreqs = { "16.35", "17.32", "18.35", "19.45", "20.60", "21.83", "23.12", "24.50", "25.96", "27.50", "29.14", "30.87",
													 "32.70", "34.65", "36.71", "38.89", "41.20", "43.65", "46.25", "49.00", "51.91", "55.00", "58.27", "61.74",
													 "65.41", "69.30", "73.42", "77.78", "82.41", "87.31", "92.50", "98.00","103.83","110.00","116.54","123.47",
													"130.81","138.59","146.83","155.56","164.81","174.61","185.00","196.00","207.65","220.00","233.08","246.94",
													"261.63","277.18","293.66","311.13","329.63","349.23","369.99","392.00","415.30","440.00","466.16","493.88",
													"523.25","554.37","587.33","622.25","659.25","698.46","739.99","783.99","830.61","880,00","932.33","987.77",
													"1046.5","1108.7","1174.7","1244.5","1318.5","1396.9","1480.0","1568.0","1661.2","1760.0","1864.7","1975.3",
													"2093.0","2217.5","2349.3","2489.0","2637.0","2793.8","2960.0","3136.0","3322.4","3520.0","3729.3","3951.1",
													"4186.0","4434.2","4698.6","4978.0","5274.0","5587.6","5919.9","6271.9","6644.9","7040.0","7458.6","7902.1",
													  "8372",  "8870",  "9397",  "9956", "10548", "11175", "11840", "12544", "13290", "14080", "14917", "15804",
													 "16744", "17740", "18795", "19912", "21096", "22351", "23680", "25088", "26579", "28160", "29834", "31608"	};

		string[] octaveFreqs = { "0-31", "31-63", "63-127", "127-253", "253-507", "507-1015", "1015-2034", "2035-4068", "4068-8137", "8137-16274", "16274-32000" };

		string[] octaveNamesA = { "CCCCCC 128'", "CCCCC 64'", "CCCC 32'", "CCC 16'", "CC 8'", "C4'", "c1 2'", "c2 1'", "c3 1/2'", "c4 1/4'", "c5 1/8'", "c6 1/16'", "Err", "Err" };
		string[] octaveNamesB = { "Sub-Sub-Sub Contra", "Sub-Sub Contra", "Sub-Contra", "Contra", "Great", "Small", "1-Line", "2-Line", "3-Line", "4-Line", "5-Line", "6-Line", "Err", "Err" };

		string[] chromaNames = { "C", "C♯-D♭", "D", "D♯-E♭", "E", "F", "F♯-G♭", "G", "G♯-A♭", "A", "A♯-B♭", "B" };
		string[] keyNames = { "", "C major", "C♯ major", "D major", "D♯ major", "E major", "F major", "F♯ major", "G major", "G♯ major", "A major", "A♯ major", "B major",
															"C minor", "C♯ minor", "D minor", "D♯ minor", "E minor", "F minor", "F♯ minor", "G minor", "G♯ minor", "A minor", "A♯ minor", "B minor" };




}
}
