using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace xTune
{
	// Most-Recently-Used
	// Holds 10 strings, could be files, paths, or ???
	//    Validate function must be customized for string type/purpose
	// Item 0 is the most recently used, 1, 2, 3. etc after that
	// If new item was already in the list (case-INsensitive match)
	//  it is moved to the top (index 0)

	class MRU
	{
		private List<string> files = new List<string>();
		private string myName = "";
		private int entryCount = 0;
		private MRUtype myType = MRUtype.Other;
		public enum MRUtype { File, Path, URL, Other}

		// Constructor
		public MRU(string theName, int maxEntries, MRUtype type = MRUtype.File)
		{
			if (theName.Length > 1)
			{
				if (maxEntries > 1)
				{
					myName = theName;
					entryCount = maxEntries;
					try
					{
						// Create initial blank entries
						for (int i = 0; i < maxEntries; i++)
						{
							files.Add("");
						}
					}
					catch (Exception e)
					{

					}
				}
				else
				{
					// Raise Error
				}
			}
			else
			{
				// Raise Error
			}
		}

		public string GetItem(int index)
		{
			string s = "";
			if (index < files.Count)
			{
				s = files[index];
			}
			return s;
		}

		public void AddNew(string fileName)
		{
			int idx = -1;
			for (int i=0; i< files.Count; i++)
			{
				if (idx<0)
				{
					bool b = false;
					if (String.Compare(fileName, files[i], StringComparison.OrdinalIgnoreCase) == 0) b = true;
					if (b)
					{
						idx = i;
						i = files.Count; // force early exit of for loop
					}
				}
			}

			if (idx < 0)
			{
				// Not in the list, insert it at the top
				files.Insert(0, fileName);
			}

			if (idx == 0)
			{
				// New File is also Last File
				// We don't have to do anything!
			}

			if (idx > 0)
			{
				// Found in the list, but not at the top, need to move it to the top
				files.RemoveAt(idx);
				files.Insert(0, fileName);
			}
		}

		public bool Remove(string fileName)
		{
			bool success = false;
			try
			{
				files.Remove(fileName);
				success = true;
			}
			catch { }
			return success;
		}

		public bool Remove(int index)
		{
			bool success = false;
			if (index < files.Count)
			{
				files.RemoveAt(index);
				success = true;
			}
			return success;
		}

		public void SaveToConfig(System.Configuration.ApplicationSettingsBase settings)
		{
			//settings.MRPath0 = files[0];
			for (int q = 0; q < files.Count; q++)
			{
				string pName = "MRU" + myName + q.ToString();
				try
				{
					//if (settings[pName] == null)
					if (!DoesSettingExist(pName))
					{
						//SettingsProvider prov = Properties.Settings.Default.Providers.
						SettingsProperty prop = new System.Configuration.SettingsProperty(pName);
						prop.PropertyType = typeof(string);
						prop.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
						//prop.Provider = prov;
						prop.SerializeAs = SettingsSerializeAs.Xml;
						SettingsPropertyValue valu = new SettingsPropertyValue(prop);
						settings.Properties.Add(prop);


					
						//settings[pName] = files[q];
						settings.Save();
						settings.Reload();
					}
					settings[pName] = files[q];
				}
				catch (Exception e)
				{
					if (IsWizard)
					{
						string msg = e.Message;
						//System.Diagnostics.Debugger.Break();
					}
				}
			}
			settings.Save();
		}

		private static bool DoesSettingExist(string settingName)
		{
			return Properties.Settings.Default.Properties.Cast<SettingsProperty>().Any(prop => prop.Name == settingName);
		}

		private static bool IsWizard
		{
			get
			{
				bool ret = false;
				string usr = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				usr = usr.ToLower();
				int i = usr.IndexOf("wizard");
				if (i >= 0) ret = true;
				return ret;
			}
		}



		public void ReadFromConfig(Properties.Settings settings)
		{
			files = new List<string>();  // Clear, reset
			for (int q = 0; q < entryCount; q++)
			{
				string pName = "MRU" + myName + q.ToString();
				try
				{
					string f = "";
					if (settings[pName] != null)
					{
						f = settings[pName].ToString();
					}
					files.Add(f);
				}
				catch (Exception e)
				{
					if (IsWizard)
					{
						string msg = e.Message;
						//System.Diagnostics.Debugger.Break();
					}
				}
			}
		}

		public int Validate()
		{
			int valid = 0;
			if (myType == MRUtype.File)
			{
				for (int i = 0; i < files.Count; i++)
				{
					if (File.Exists(files[i]))
					{
						valid++;
					}
					else
					{
						files.RemoveAt(i);
						files.Add("");
					}
				}
			}
			if (myType == MRUtype.Path)
			{
				for (int i = 0; i < files.Count; i++)
				{
					if (Directory.Exists(files[i]))
					{
						valid++;
					}
					else
					{
						files.RemoveAt(i);
						files.Add("");
					}
				}
			}
			if (myType == MRUtype.URL)
			{
				//TODO: figure out how to validate a URL
				//for (int i = 0; i < files.Count; i++)
				//{
				//		if (URL.Exists(files[i]))
				//	{
				//		valid++;
				//	}
				//	else
				//	{
				//		files.RemoveAt(i);
				//		files.Add("");
				//	}
				//}
				valid = files.Count;
			}
			if (myType == MRUtype.Other)
			{
				// No way to validate 'other'
				valid = files.Count;
			}
			return valid;
		}
	}





}
