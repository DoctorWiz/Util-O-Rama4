using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LORUtils4; using FileHelper;

namespace UtilORama4
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmRemapper());
		}
	}

	public class MapInfo
	{
		public iLORMember4 Source = null;
		public iLORMember4 Master = null;
	}

}
