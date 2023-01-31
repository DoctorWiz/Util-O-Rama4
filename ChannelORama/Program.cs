using FileHelper;

namespace UtilORama4
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		public static string Name = Fyle.ApplicationName;
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new frmList());
		}
	}
}