using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilORama4;
using VampORama;
using LOR4;	
using FileHelper;

namespace VampORama
{
	public static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static Form SplashForm;
		static Form VampForm;
		public static string Name = Fyle.ApplicationName;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//ApplicationConfiguration.Initialize();
			//Application.Run(new frmVamp());

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
