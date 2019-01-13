namespace PortableSettingsProvider
{
	public class U3SettingsProvider : PortableSettingsProvider
	{
		private const String U3APPDATAPATH = "U3_APP_DATA_PATH";

		public static override String GetAppSettingsPath()
		{
			// Get the environment variable set by the U3 application for a pointer to its data
			try
			{
				return System.Environment.GetEnvironmentVariable(U3APPDATAPATH);
			}
			catch (Exception ex)
			{
				// Not running in a U3 environment, just return the application path
				return MyBase.GetAppSettingsPath;
			} // End Try-Catch
		} // End Function
	} // End Class
} // End namespace
