using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Windows.Forms;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Xml;

namespace PortableSettingsProvider
{
	public static class PortableSettingsProvider : SettingsProvider
	{
		private const String SETTINGSROOT = "Settings"; // XML Root Node
		private static Xml.XmlDocument m_SettingsXML = Nothing;

		public static override void Initialize(String name, NameValueCollection col)
		{
			MyBase.Initialize(Me.ApplicationName, col);
		} // End Sub

		public static override String ApplicationName
		{ 
			get
			{ 
				if (Application.ProductName.Trim.Length > 0)
				{
					return Application.ProductName;
				}
				else
				{
					IO.FileInfo fi = new IO.FileInfo(Application.ExecutablePath);
					return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);
				} //  End If
			} // End Get

			set
			{
				// (ByVal value As String)
        // Do nothing
      } // End Set

		} //End Property


		private static virtual String GetAppSettingsPath()
		{
			// Used to determine where to store the settings
			System.IO.FileInfo fi = new System.IO.FileInfo(Application.ExecutablePath);
			return fi.DirectoryName;
		} // End Function

		private static virtual string GetAppSettingsFilename()
		{
			// Used to determine the filename to store the settings
			return ApplicationName & ".settings";
		} // End Function


		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection propvals)
		{
			// Iterate through the settings to be stored
			// Only dirty settings are included in propvals, and only ones relevant to this provider
			foreach (SettingsPropertyValue propval in propvals)
			{
				SetValue(propval);
			}

			try
			{
				SettingsXML.Save(IO.Path.Combine(GetAppSettingsPath, GetAppSettingsFilename));
			}
			catch(Exception ex)
			{
				// Ignore if cant save, device been ejected
			} // End Try-Catch
		} // End Sub


		public static override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection props)
		{
			// Create new collection of values
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

			// Iterate through the settings to be retrieved
			foreach (SettingsProperty setting in props)
			{
				SettingsPropertyValue value = new SettingsPropertyValue(setting);

				value.IsDirty = false;
				value.SerializedValue = GetValue(setting);
				values.Add(value);
			} //  Next

			return values;
		} // End Function

		private static Xml.XmlDocument SettingsXML
		{
			get
			{
				// If we don't hold an xml document, try opening one.  
				// If it doesn't exist then create a new one ready.

				if (m_SettingsXML == null)
				{
					m_SettingsXML = new Xml.XmlDocument();

					try
					{
						m_SettingsXML.Load(IO.Path.Combine(GetAppSettingsPath, GetAppSettingsFilename));
					}
					catch (Exception ex)
					{
						// Create new document
						XmlDeclaration dec = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", String.Empty);
						m_SettingsXML.AppendChild(dec);
						XmlNode nodeRoot;
						nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "");
						m_SettingsXML.AppendChild(nodeRoot);
					} // End Try-Catch
				} // End If
				return m_SettingsXML;
			} // End Get
		} // End Property

		private String GetValue(SettingsProperty setting)
		{
			String ret = "";

			try
			{
				if (IsRoaming(setting))
				{
					ret = SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & setting.Name).InnerText;
				}
				else
				{
					ret = SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name & "/" & setting.Name).InnerText;
				} // End If
			}
			catch (Exception ex)
			{
				if (!setting.DefaultValue == null)
				{
					ret = setting.DefaultValue.ToString;
				}
				else
				{
					ret = "";
				} //  End If
			} // End Try-Catch
			return ret;
		} // End Function


		private static void SetValue(SettingsPropertyValue propVal)
		{
			Xml.XmlElement MachineNode;
			Xml.XmlElement SettingNode;

			// Determine if the setting is roaming.
			// If roaming then the value is stored as an element under the root
			// Otherwise it is stored under a machine name node 

			try
			{
				if (IsRoaming(propVal.Property))
				{
					SettingNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & propVal.Name), XmlElement);
				}
				else
				{
					SettingNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name & "/" & propVal.Name), XmlElement);
				} // End If
			}
			catch (Exception ex)
			{
				SettingNode = Nothing;
			} //End Try-Catch

			// Check to see if the node exists, if so then set its new value
			if (!SettingNode == null)
			{
				SettingNode.InnerText = propVal.SerializedValue.ToString;
			}
			else
			{
				if (IsRoaming(propVal.Property))
				{
					// Store the value as an element of the Settings Root Node
					SettingNode = SettingsXML.CreateElement(propVal.Name);
					SettingNode.InnerText = propVal.SerializedValue.ToString;
					SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(SettingNode);
				}
				else
				{
					// Its machine specific, store as an element of the machine name node,
					// creating a new machine name node if one doesnt exist.
					try
					{
						MachineNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & My.Computer.Name), XmlElement);
					}
					catch (Exception ex)
					{
						MachineNode = SettingsXML.CreateElement(My.Computer.Name);
						SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode);
					} // End Try-Catch

					if (MachineNode == null)
					{
						MachineNode = SettingsXML.CreateElement(My.Computer.Name);
						SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(MachineNode);
					} // End If

					SettingNode = SettingsXML.CreateElement(propVal.Name);
					SettingNode.InnerText = propVal.SerializedValue.ToString;
					MachineNode.AppendChild(SettingNode);
				} //  End If
			} // End If
		} // End Sub

		private static bool IsRoaming(SettingsProperty prop)
		{
			// Determine if the setting is marked as Roaming
			foreach (DictionaryEntry d in prop.Attributes)
			{
				DAttribute a = DirectCast(d.Value, Attribute);

				if (typeof(a) == System.Configuration.SettingsManageabilityAttribute)
				{
					return true;
				} // End If
			} // Next
			return false;
		} //	End Function

	} // End Class
} // End Namespace