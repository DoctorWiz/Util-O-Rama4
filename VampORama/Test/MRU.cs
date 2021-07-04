using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.IO;

namespace RecentlyUsed
{
	class MRU
	{
		// Type 'Properties' not found when using a different namespace
		private Properties.Settings appSettings;
		//private ConfigurationSettings appSettings;
		//private System.Configuration.ApplicationSettingsBase set2;
		public MRU(ConfigurationSettings settings)
		{
			appSettings = settings;
		}
	}
}
