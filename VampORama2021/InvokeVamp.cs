using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace VampORama
{
	class InvokeVamp
	{
		const string VAMPPATH = "C:\\Program Files\\Vamp Plugins\\";
		const string DLL = ".dll";
		const string VAMP_QueenMary = "qm-vamp-plugins";
		const string VAMP_Azi = "azi";
		const string VAMP_BBC = "bbc-vamp-plugins";
		const string VAMP_BeatRoot = "beatroot-vamp";
		const string VAMP_CepstralPitch = "cepstral-pitchtracker";
		const string VAMP_CQ = "qcvamp";
		const string VAMP_Match = "match-vamp-plugin";
		
		
		// Import user32.dll (containing the function we need) and define
		// the method corresponding to the native function.
		//[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[DllImport(VAMPPATH + VAMP_QueenMary + DLL, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

		public static void Main(string[] args)
		{
			// Invoke the function as a regular managed method.
			MessageBox(IntPtr.Zero, "Command-line message box", "Attention!", 0);
		}

	}
}
