using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xTimeORama
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// For TreeViewAdv control
			// Essential Studio Enterprise Edition - Community Licensee
			// Version 17.1.0.38
			// For WinForms
			// Key generated from Syncfusion's website on 5/10/2019
			Syncfusion.Licensing.SyncfusionLicenseProvider
				.RegisterLicense("OTk1OTZAMzEzNzJlMzEyZTMwQ084YXBOb01FZmhhNkYzYjFZSVo2RkdiOTA3dHA2eFJXS3hVeXM4UkV0cz0=");


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmxTime());
		}
	}
}
