using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilORama4
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static Form SplashForm;
		static Form VampForm;
		[STAThread]


		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//Show Splash Form
			SplashForm = new frmSplash(); // Form();
			var splashThread = new Thread(new ThreadStart(
					() => Application.Run(SplashForm)));
			splashThread.SetApartmentState(ApartmentState.STA);
			splashThread.Start();

			//Create and Show Main Form
			VampForm = new frmVamp(); // Form();
			VampForm.Load += VampForm_LoadCompleted;
			Application.Run(VampForm);
		}

		private static void VampForm_LoadCompleted(object sender, EventArgs e)
		{
			if (SplashForm != null && !SplashForm.Disposing && !SplashForm.IsDisposed)
				SplashForm.Invoke(new Action(() => SplashForm.Close()));
			VampForm.TopMost = true;
			VampForm.Activate();
			VampForm.TopMost = false;
		}

	}
}