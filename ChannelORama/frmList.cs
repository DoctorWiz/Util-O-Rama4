/*
 * TODO: Allow anything for Controller ID (Currently A,B,C. etc.)
 *           And then order them by Universe and Start Channel.
 *       Add a "None" option for Controller and make sure it works correctly. (?)
 *       Add a "Multi" option for color and make sure it works correctly.
 *       Add a list of LOR Controller Models (with channel count) and make sure it works correctly.
 *       Add check for overlapping controller start channels, and gaps between channels, and warn about them.
 *       Track down index out of range error when loading files, and fix it, and make file loading more robust in general.
 *       Add a "New Channel" button to the main form and make sure it works correctly. (?)
 *       Add a "Delete Channel" button to the main form and make sure it works correctly. (?)
 *       Add Chain Location (how far down the dmx cable, 1 being closest to controller).
 *           And then add a warning if there are multiple channels with the same location on the same controller.
 *       Detect DPI and scale icons accordingly. (Currently hard coded for 96 DPI)
 */


using FileHelper;   // Extended file functions (example: SafeRename)
using FormHelper;   // Extended WinForms functions such as SaveView and RestoreView
using FuzzORama;    // Fuzzy String Matching-- Util-O-Rama version (optimized for channel names)
using LOR4;     // Light-O-Rama Showtime vS4 Sequences and Visualizations
using ReadWriteCsv;   // Comma Separated Values
using Syncfusion.Grouping;
using Syncfusion.Linq;
using Syncfusion.Windows.Forms.Tools;   // SyncFusion TreeView Advanced
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using xLights22;    // xLights RGBEffects and Sequences

namespace UtilORama4
{
	public partial class frmList : Form
	{
		private static Settings userSettings = Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/channelorama";
		public List<DMXUniverse> AllUniverses = new List<DMXUniverse>();
		public List<DMXController> AllControllers = DMXUniverse.AllControllers;
		public List<DMXChannel> AllChannels = DMXUniverse.AllChannels;
		public List<DMXDeviceType> DeviceTypes = new List<DMXDeviceType>();
		private const string myTitle = "Channel-O-Rama  Channel Manager";

		LOR4Visualization lViz = null;
		private string lastFile = "";
		private bool formShown = false;
		private string dbPath = "";
		private int year = 2023;
		public int lastID = -1;
		public int writeID = 0;
		public bool dirty = false;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;
		public float dpi = 96;
		public float mdpi = 96; // Monitor DPI (for scaling icons)
														//private xRGBEffects xSeq = null;
														//private LOR4Visualization lViz = null;

		private int[] TREEICONuniverse     = { 0 };
		private int[] TREEICONcontroller   = { 1 };
		//private int[] TREEICONtrack        = { 3 };
		private int[] TREEICONchannel      = { 4 }; // Multicolored
		private int[] TREEICONrgbChannel   = { 2 }; // #7 below looks better
		//private int[] TREEICONchannelGroup = { 6 };
		//private int[] TREEICONcosmic       = { 7 };
		private int[] TREEICONrgbColor     = { 2 };
		private int[] TREEICONrgbwColor    = { 3 };
		private int[] TREEICONmulticolor   = { 4 };
		private int[] TREEICONred          = { 8 };
		private int[] TREEICONgreen        = { 11 };
		private int[] TREEICONblue         = { 12 };

		public frmList()
		{
			InitializeComponent();
		}

		private void btnWiz_Click(object sender, EventArgs e)
		{
			//////////////////////////////
			//!/  WIZARD TEST BUTTON   ///
			//!/  DO SOMETHING COOL!  ///
			///////////////////////////

			string seqFile = "W:\\Documents\\Christmas\\2021\\Light-O-Rama\\Wizlights\\Sequences\\Wizlights 2021 !Master Channel List (cf21a).las";
			string vizFile = "W:\\Documents\\Christmas\\2021\\Light-O-Rama\\Wizlights\\Visualizations\\Wizlights 2021.lee";


			//xSeq = new xRGBEffects();
			lViz = new LOR4Visualization(null, vizFile);

			//int ccc = lViz.VizChannels.Count;
			//int ddd = lViz.VizDrawObjects.Count;
			//int ggg = lViz.VizItemGroups.Count;


			//MatchUp(seqFile);
			MatchUp(seqFile, true);

		}


		private void frmList_Load(object sender, EventArgs e)
		{
			this.dpi = this.DeviceDpi;
			this.RestoreView(); // Use default parameters
			RestoreUserSettings();
			lblVersions.Text = Etc.LORVersion.ToString() + " + " + Etc.xLightsVersion.ToString();
			lblVersions.Visible = true;
		}

		private void SaveUserSettings()
		{

		}

		private void RestoreUserSettings()
		{


			btnWiz.Visible = isWiz;
		}

		public void ImBusy(bool busy)
		{
			if (busy)
			{
				this.Cursor = Cursors.WaitCursor;
				this.Enabled = false;
			}
			else
			{
				this.Cursor = Cursors.Default;
				this.Enabled = true;
			}
		}

		private void pnlHelp_Click(object sender, EventArgs e)
		{ System.Diagnostics.Process.Start(helpPage); }

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout("Channel-O-Rama");
			aboutBox.Icon = this.Icon;
			//aboutBox.appName = "Channel-O-Rama";
			//aboutBox.Text = "About Channel-O-Rama";
			aboutBox.AppIcon = picAboutIcon.Image;
			aboutBox.ShowDialog(this);
			ImBusy(false);
		}

		private string SelectDBPath(bool required = false)
		{
			string ret = "";
			// Never run before ?
			// Prompt user to select the base path for the channel database, which should be the folder that contains the year folders (e.g. "Christmas 2023", "Christmas 2024", etc.)
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			string dtxt = "Select the base folder for the channel database."; // (the folder that contains the year folders such as Christmas 2023, Christmas 2024, etc.)";
			dtxt += "\r\n\r\nIf you have not yet created a channel database, you can just select or create an empty folder to use as the base path for the channel database.";
			dtxt += "\r\n\t (Hint: You may wish to include the holiday and/or year in the folder name, such as 'Christmas 2026', to help keep things organized.)";
			fbd.Description = dtxt;
			DialogResult dr = fbd.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				ret = fbd.SelectedPath;
			}
			else
			{
				if (required)
				{
					MessageBox.Show(this, "No folder selected.  Cannot continue.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}

			string dtfile = ret + "\\DeviceTypes.csv";
			bool datExists = Fyle.Exists(dtfile);
			if (datExists)
			{
				dtfile = ret + "\\Universes.csv";
				datExists = Fyle.Exists(dtfile);
				if (datExists)
				{
					dtfile = ret + "\\Controllers.csv";
					datExists = Fyle.Exists(dtfile);
					if (datExists)
					{
						dtfile = ret + "\\Channels.csv";
						datExists = Fyle.Exists(dtfile);
					}
				}
			}
			if (!datExists)
			{
				dtxt = "Folder '" + ret + "' does not contain a channel database.";
				dtxt += "\r\nCreate a new channel database here?";
				dtxt += "\r\n\r\n\tYes to creat a new database in '" + ret + "'.";
				dtxt += "\r\n\tNo to go back and select a different folder.";
				dtxt += "\r\n\tCancel to exit the program.";
				dr = MessageBox.Show(this, dtxt, "Create new database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					Application.Exit();
				}
				else if (dr == DialogResult.No)
				{
					ret = SelectDBPath(required); // Recursive call to prompt user to select a different folder
				}
				else if (dr == DialogResult.Yes)
				{
					CreateNewDatabase(ret + "\\ChannelDB");
				}
			}
			return ret;
		}

		private string PathToDB()
		{
			string dpth = Settings.Default.DBPath;
			string ret = dpth;

			if (ret.Length < 4)
			{
				// Never run before ?
				// Prompt user to select the base path for the channel database, which should be the folder that contains the year folders (e.g. "Christmas 2023", "Christmas 2024", etc.)
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				string dtxt = "Select the new base folder for the new channel database."; // (the folder that contains the year folders such as Christmas 2023, Christmas 2024, etc.)";
				dtxt += "\r\n\r\nIf you have not yet created a channel database, you can just select or create an empty folder to use as the base path for the channel database.";
				dtxt += "\r\n\t (Hint: You may wish to include the holiday and/or year in the folder name, such as 'Christmas 2026', to help keep things organized.)";
				fbd.Description = dtxt;
				DialogResult dr = fbd.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					ret = fbd.SelectedPath;
				}
				else
				{
					MessageBox.Show(this, "No folder selected.  Cannot continue.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
			}
			// Already have a path, but check that it exists
			if (!Fyle.PathExists(ret))
			{
				ret = SelectDBPath(true);

				string dtfile = ret + "\\DeviceTypes.csv";
				bool datExists = Fyle.Exists(dtfile);
				if (datExists)
				{
					dtfile = ret + "\\Universes.csv";
					datExists = Fyle.Exists(dtfile);
					if (datExists)
					{
						dtfile = ret + "\\Controllers.csv";
						datExists = Fyle.Exists(dtfile);
						if (datExists)
						{
							dtfile = ret + "\\Channels.csv";
							datExists = Fyle.Exists(dtfile);
						}
					}
				}
				if (!datExists)
				{
					string dtxt = "Folder '" + ret + "' does not contain a channel database.";
					dtxt += "\r\nCreate a new channel database here?";
					dtxt += "\r\n\r\n\tYes to creat a new database in '" + ret + "'.";
					dtxt += "\r\n\tNo to go back and select a different folder.";
					dtxt += "\r\n\tCancel to exit the program.";
					DialogResult dr = MessageBox.Show(this, dtxt, "Create new database?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dr == DialogResult.Cancel)
					{
						Application.Exit();
					}
					else if (dr == DialogResult.Yes)
					{
						CreateNewDatabase(ret + "\\ChannelDB");
					}
					else if (dr == DialogResult.No)
					{
						ret = PathToDB(); // Recursive call to prompt user to select a different folder
					}
				}
			}
			return ret;
		}

		private void CreateNewDatabase(string folder)
		{
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\desktop.ini", folder + "\\desktop.ini");
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\folder.ico", folder + "\\folder.ico");
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\DeviceTypes.csv", folder + "\\DeviceTypes.csv");
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\Universes.csv", folder + "\\Universes.csv");
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\Controllers.csv", folder + "\\Controllers.csv");
			Fyle.SafeCopy(Application.StartupPath + "\\TemplateDB\\Channels.csv", folder + "\\Channels.csv");
			Settings.Default.DBPath = folder;
			Settings.Default.Save();
		}
		public int LoadData(string filePath)
		{
			int errs = LoadDevices(filePath);
			errs = LoadUniverses(filePath);
			errs += LoadControllers(filePath);
			errs += LoadChannels(filePath);
			UpdateStatus();
			this.Text = filePath + " - " + myTitle;
			return errs;
		}

		private void UpdateStatus()
		{
			string stxt = AllUniverses.Count.ToString() + " Universes, ";
			int c = 0;
			for (int u = 0; u < AllUniverses.Count; u++)
			{
				c += AllUniverses[u].DMXControllers.Count;
			}
			stxt += c.ToString() + " Controllers, ";
			stxt += AllChannels.Count.ToString() + " Channels";
			pnlStatus.Text = stxt;
		}


		public int LoadUniverses(string filePath)
		{
			int errs = 0;
			string uniName = ""; // for debugging exceptions
			string uniFile = filePath + "Universes.csv";
			try
			{
				CsvFileReader reader = new CsvFileReader(uniFile);
				CsvRow row = new CsvRow();
				// Read and throw away first line which is headers;
				reader.ReadRow(row);
				while (reader.ReadRow(row))
				{
					try
					{
						DMXUniverse universe = new DMXUniverse();

						int uid = 1;
						int.TryParse(row[0], out uid);   // Field 0 Universe ID
						universe.ID = uid;

						int i = -1;
						int.TryParse(row[1], out i);   // Field 0 Universe Number
						universe.UniverseNumber = i;

						universe.Name = row[2];  // Field 1 Name
						uniName = universe.Name;  // For debugging exceptions
						universe.Location = row[3]; // Field 2 Location
						universe.Comment = row[4]; // Field 3 Comment

						bool b = true;
						bool.TryParse(row[5], out b); // Field 4 Active
						universe.Active = b;

						i = 512;
						int.TryParse(row[6], out i);   // Field 5 Size Limit
						universe.MaxChannelsAllowed = i;

						i = 1;
						int.TryParse(row[7], out i);   // Field 6 xLights Start
						universe.xLightsAddress = i;

						universe.Connection = row[8];  // Field 7 Connection
						lastID = Math.Max(lastID, universe.ID);
						//universe.ID = lastID;
						AllUniverses.Add(universe);
					}
					catch (Exception ex)
					{
						string msg = "Error " + ex.ToString() + " while reading Universe " + uniName;
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						errs++;
					}
				}
				reader.Close();
				AllUniverses.Sort();
				DMXUniverse.AllUniverses = AllUniverses; // Set the static list in DMXUniverse class to the list we just loaded and sorted, so that it can be accessed from the DMXController and DMXChannel classes when they are loading and need to find their parent universe.
			}
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while reading Universes file " + uniFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		public int LoadControllers(string filePath)
		{
			//("ID");           // Field 0
			//("Universe");     // Field 1
			//("Controller");   // Field 2
			//("Name");         // Field 3
			//("Location");     // Field 4
			//("Comment");      // Field 5
			//("Active");       // Field 6
			//("Brand");        // Field 7
			//("Model");        // Field 8
			//("Unit#");        // Field 9
			//("Output Count"); // Field 10
			//("DMX Start");    // Field 11
			//("Voltage");      // Field 12

			int errs = 0;
			string ctlName = ""; // For debugging exceptions
			if (AllUniverses.Count > 0)
			{
				string ctlFile = filePath + "Controllers.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(ctlFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							DMXController controller = new DMXController();
							int ctlid = 1;
							int.TryParse(row[0], out ctlid);   // Field 1 Controller ID (number)
							controller.ID = ctlid;

							int uniID = -1;
							int.TryParse(row[1], out uniID);   // Field 0 Universe ID (number)
							controller.Universe = GetUniverseByID(uniID);
							//controller.Universe = GetUniverseByNumber(uniID); // TEMP!!!
							if (controller.Universe == null)
							{
								//string msg = "Universe not found for controller:" + controller.Name;
								if (Fyle.isWiz)
								{
									string mtxt = "Controller with ID:" + uniID.ToString() + " not found for controller:" + row[3];
									MessageBox.Show(this, mtxt, "Universe Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									int xx = 0;
								}
							}
							else
							{
								controller.Universe.DMXControllers.Add(controller);
							}	

							controller.Identifier = row[2];  // Field 1 Letter ID
							controller.Name = row[3];  // Field 2 Name
							ctlName = controller.Name; // For debugging exceptions
							controller.Location = row[4]; // Field 3 Location
							controller.Comment = row[5]; // Field 4 Comment

							bool b = true;
							bool.TryParse(row[6], out b); // Field 5 Active
							controller.Active = b;

							controller.ControllerBrand = row[7];
							controller.ControllerModel = row[8];

							int un = -1;
							int.TryParse(row[9], out un);

							int cnt = 16;
							int.TryParse(row[10], out cnt);   // Field 8 Channel Count
							controller.OutputCount = cnt;

							int adr = 1;
							int.TryParse(row[11], out adr);   // Field 9 DMX Start LOR4Channel
							controller.DMXStartAddress = adr;

							int volts = 120;
							int.TryParse(row[12], out volts);   // Field 10 voltage
							controller.Voltage = volts;

							/*
							bool uniFound = false;
							for (int u = 0; u < AllUniverses.Count; u++)
							{
								if (universe == AllUniverses[u].UniverseNumber)
								{
									AllUniverses[u].DMXControllers.Add(controller);
									controller.DMXUniverse = AllUniverses[u];
									uniFound = true;
									u = AllUniverses.Count; // Exit loop
								}
							}
							if (!uniFound)
							{
								string msg = "Universe not found for controller:" + controller.Name;
								int qqqqq = 1;
							}
							*/

							// Unit Number Madness
							if (controller.ControllerBrand == "LOR")
							{
								if (un < 2)
								{
									if (adr > 16)
									{
										un = (adr - 1) / 16 + 1;
									}
									int sc = (un - 1) * 16 + 1;
									adr = sc;
								}
								else
								{
									if (un > 0)
									{
										int sc = (un - 1) * 16 + 1;
										adr = sc;
									}
									else
									{
										int un2 = (adr - 1) / 16 + 1;
									}
								}
							}
							string foo = ctlName;
							controller.UnitID = un;
							controller.DMXStartAddress = adr;
							controller.OutputCount = cnt;




							lastID=Math.Max(lastID, controller.ID);
							//controller.ID = lastID;
							AllControllers.Add(controller);
						}
						catch (Exception ex)
						{
							string msg = "Error " + ex.ToString() + " while reading Controller " + ctlName;
							if (isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}

					reader.Close();
					for (int u = 0; u < AllUniverses.Count; u++)
					{
						AllUniverses[u].DMXControllers.Sort();
					}
				}
				catch (Exception ex)
				{
					string msg = "Error " + ex.ToString() + " while reading Controllers file " + ctlFile;
					if (isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;

				}
				DMXUniverse.AllControllers = AllControllers; // Set the static list in DMXUniverse class to the list we just loaded and sorted, so that it can be accessed from the DMXChannel class when it is loading and needs to find its parent controller.
			}
			return errs;
		}

		public int LoadChannels(string filePath)
		{
			int errs = 0;
			int nextID = 1;
			string chanName = ""; // for debugging exceptions
			if (AllUniverses.Count > 0)
			{
				string chnFile = filePath + "Channels.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(chnFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							DMXChannel channel = new DMXChannel();
							int ix = 1;
							int.TryParse(row[0], out ix); // Field 0 = Channel ID
																						// Ignore it.  We are going to renumber them as we read them in
							channel.ID = nextID;
							nextID++;

							int uniNum = 1;
							int.TryParse(row[1], out uniNum); // Field 1 = Universe Number
							// Ignore the universe number, get it from the controller's universe number instead

							int ctlid = 1;
							int.TryParse(row[2], out ctlid);
							//channel.DMXController = AllControllers[ctlid-1]; // Field 2 = Controller ID
							// Safert way to get controller by ID instead of assuming they are in order and contigous in memory
							channel.DMXController = GetControllerByID(ctlid); // Field 2 = Controller ID
							if (channel.DMXController == null)
							{
								string mtxt = "Controller with ID:" + ctlid.ToString() + " not found for channel:" + row[5];
								MessageBox.Show(this, mtxt, "Controller Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
							else
							{
								channel.DMXController.DMXChannels.Add(channel); // Add the channel to the controller's list of channels
							}

							//string ctlIdentifier = row[3]; // Field 2 = Controller Letter or ID
																						 // Ignore it, we have the controller already, and can get the identifier from the controller

							int nout = 1;
							int.TryParse(row[3], out nout);
							channel.OutputNum = nout;               // Field 4 = Output Number

							channel.Name = row[4];                // Field 5 = Name
							chanName = channel.Name;              // For debugging exceptions
							channel.Location = row[5];            // Field 6 = Location
							channel.Comment = row[6];             // Field 7 = Comment

							bool b = true;
							bool.TryParse(row[7], out b);     // Field 8 = Active
							channel.Active = b;

							int devID = -1;
							int.TryParse(row[8], out devID);      // Field 9 = Channel Type



							//! ONE TIME FIX
							//if (devID == 0) devID = 22;





							if (devID >= 0 && devID < DeviceTypes.Count)
							{
								//! Note: Devices should not yet be sorted by Display Order
								//! Should still be sorted by ID
								//string dn = DeviceTypes[devID].Name;
								//channel.DeviceType = DeviceTypes[devID];
								channel.DeviceType = GetDeviceTypeByID(devID);
							}

							string colhex = row[9];           // Field 9 = Color (hex)
							//Color color = System.Drawing.ColorTranslator.FromHtml(colhex);
							Color color = LOR4.LOR4Admin.HexToColor(colhex);
							channel.Color = color;

							if (row.Count > 10)
							{
								string coname = row[10];
								channel.ColorName = coname;           // Field 10 = Color Name (optional)
							}
							else
							{
								channel.ColorName = LOR4Admin.NearestColorName(channel.Color); //Etc.ColorName(channel.Color);
							}


								AllChannels.Add(channel);
							/*
							bool ctlFound = false;
							for (int u = 0; u < AllUniverses.Count; u++)
							{
								DMXUniverse universe = AllUniverses[u];
								for (int c = 0; c < universe.DMXControllers.Count; c++)
								{
									if (ctlid == universe.DMXControllers[c].ID)
									{
										channel.DMXController = universe.DMXControllers[c];
										universe.DMXControllers[c].DMXChannels.Add(channel);
										ctlFound = true;
										c = universe.DMXControllers.Count;
										u = AllUniverses.Count; // Exit loop
									}
								}
							}
							if (!ctlFound)
							{
								string msg = "Controller not found for channel " + channel.Name;
								int qqq5 = 5;
							}
							*/
							lastID = Math.Max(lastID, channel.ID);
							//channel.ID = lastID;
						}
						catch (Exception ex)
						{
							int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
							string msg = "Error on line " + ln.ToString() + "\r\n";
							msg += ex.ToString() + " while reading Channel " + chanName;
							if (isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}
					reader.Close();
					for (int u = 0; u < AllUniverses.Count; u++)
					{
						DMXUniverse universe = AllUniverses[u];
						for (int c = 0; c < universe.DMXControllers.Count; c++)
						{
							universe.DMXControllers[c].DMXChannels.Sort();
						}
					}
					// *NOW* we can sort the deviceTypes by display order
					DeviceTypes.Sort();
				}
				catch (Exception ex)
				{
					int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;

				}
				DMXUniverse.AllChannels = AllChannels; // Set the static list in DMXUniverse class to the list we just loaded and sorted, so that it can be accessed from anywhere if needed.
			}
			return errs;
		}

		private DMXUniverse GetUniverseByID(int ID)
		{
			DMXUniverse ret = null;
			for (int c = 0; c < AllUniverses.Count; c++)
			{
				if (AllUniverses[c].ID == ID)
				{
					ret = AllUniverses[c];
					c = AllUniverses.Count; // Exit loop
				}
			}
			return ret;
		}

		private DMXUniverse GetUniverseByNumber(int unum)
		{
			DMXUniverse ret = null;
			for (int c = 0; c < AllUniverses.Count; c++)
			{
				int uid = AllUniverses[c].ID;
				string uname = AllUniverses[c].Name;
				if (AllUniverses[c].UniverseNumber == unum)
				{
					ret = AllUniverses[c];
					c = AllUniverses.Count; // Exit loop
				}
			}
			return ret;
		}



		private DMXController GetControllerByID(int ID)
		{
			DMXController ret = null;
			for (int c = 0; c < AllControllers.Count; c++)
			{
				int cid = AllControllers[c].ID;
				string cname = AllControllers[c].Name;
				if (AllControllers[c].ID == ID)
				{
					ret = AllControllers[c];
					c = AllControllers.Count; // Exit loop
				}
			}
			return ret;
		}

		private DMXChannel GetChannelByID(int ID)
		{
			DMXChannel ret = null;
			for (int c = 0; c < AllChannels.Count; c++)
			{
				if (AllChannels[c].ID == ID)
				{
					ret = AllChannels[c];
					c = AllChannels.Count; // Exit loop
				}
			}
			return ret;
		}

		private DMXDeviceType GetDeviceTypeByID(int ID)
		{
			DMXDeviceType ret = null;
			for (int c = 0; c < DeviceTypes.Count; c++)
			{
				if (DeviceTypes[c].ID == ID)
				{
					ret = DeviceTypes[c];
					c = DeviceTypes.Count; // Exit loop
				}
			}
			return ret;
		}

		public int LoadDevices(string filePath)
		{
			int errs = 0;
			string devName = ""; // for debugging exceptions
			//if (AllUniverses.Count > 0)
			{
				string chnFile = filePath + "DeviceTypes.csv";
				try
				{
					CsvFileReader reader = new CsvFileReader(chnFile);
					CsvRow row = new CsvRow();
					// Read and throw away first line which is headers;
					reader.ReadRow(row);
					while (reader.ReadRow(row))
					{
						try
						{
							if (row.Count > 1)
							{
								int devID = -1;
								int.TryParse(row[0], out devID); // Field 0 = Device ID
								devName = row[1];   // Field 1 = Name
								int ord = 0;
								if (row.Count > 2)
								{
									int.TryParse(row[2], out ord); // Field 2 = LOR4Output Number
								}
								DMXDeviceType deviceType = new DMXDeviceType(devName, devID, ord);
								DeviceTypes.Add(deviceType);
							}
						}
						catch (Exception ex)
						{
							int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
							string msg = "Error on line " + ln.ToString() + "\r\n";
							msg += ex.ToString() + " while reading Channel " + devName;
							if (isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;
				}
			}
			int dc = DeviceTypes.Count;
			//! No! do not sort [by display order] yet!  Leave sorted by order added which is also by ID until AFTER the channels have been
			//! loaded, THEN sort by display order
			//deviceTypes.Sort();
			return errs;
		}

		public int SaveData(string filePath)
		{
			int errs = SaveUniverses(filePath);
			errs += SaveControllers(filePath);
			errs += SaveChannels(filePath);
			MakeDirty(false);
			return errs;
		}

		public void ClearData()
		{
			treeChannels.Nodes.Clear();
			AllUniverses = new List<DMXUniverse>();
			AllChannels = DMXUniverse.AllChannels;
			lViz = null;
			lastFile = "";
			lastID = -1;
			dirty = false;

			//private xRGBEffects xSeq = null;
			//private LOR4Visualization lViz = null;

		}


		public int SaveUniverses(string filePath)
		{
			// Start ID numering at ONE instead of zero for user friendliness when editing raw CSV files.
			// Note that they may be out of order or non-contigous in memory, but when we save them, we renumber them starting at 1 and make them contigous for user friendliness when editing raw CSV files.
			writeID = 1; 
			int errs = 0;
			string uniName = "";
			string uniFile = filePath + "Universes.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(uniFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("ID"); // Field 0
				row.Add("Universe#");      // Field 1
				row.Add("Name");           // Field 2
				row.Add("Location");       // Field 3
				row.Add("Comment");        // Field 4
				row.Add("Active");         // Field 5
				row.Add("Size Limit");     // Field 6
				row.Add("xLights Start#"); // Field 7
				row.Add("Connection");     // Field 8
				writer.WriteRow(row);

				// Make sure they are sorted by universe number before we write them out, so that they will be in order when we read them back in, and also so that they will be in order for the user when they open the CSV file to edit it.
				AllUniverses.Sort();
				for (int u = 0; u < AllUniverses.Count; u++)
				{
					try
					{
						DMXUniverse universe = AllUniverses[u];
						uniName = universe.Name;
						row = new CsvRow();

						//row.Add(universe.ID.ToString());                 // Field 0
						// Lets renumber all the IDs while we'ere at it.
						row.Add(writeID.ToString());                     // Field 0
						universe.ID = writeID;
						writeID++;
						row.Add(universe.UniverseNumber.ToString());     // Field 1
						row.Add(universe.Name);                          // Field 1
						row.Add(universe.Location);                      // Field 3
						row.Add(universe.Comment);                       // Field 4
						row.Add(universe.Active.ToString());             // Field 5
						row.Add(universe.MaxChannelsAllowed.ToString()); // Field 6
						row.Add(universe.xLightsAddress.ToString());     // Field 7
						row.Add(universe.Connection);                    // Field 8

						writer.WriteRow(row);
					}
					catch (Exception ex)
					{
						string msg = "Error " + ex.ToString() + " while saving Universe " + uniName;
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						errs++;
					}
				}
				writer.Close();
			}
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while saving Universes file " + uniFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		public int SaveControllers(string filePath)
		{
			// Start ID numering at ONE instead of zero for user friendliness when editing raw CSV files.
			// Note that they may be out of order or non-contigous in memory, but when we save them, we renumber them starting at 1 and make them contigous for user friendliness when editing raw CSV files.
			writeID = 1;
			int errs = 0;
			string ctlName = ""; // For debugging exceptions
			string ctlFile = filePath + "Controllers.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(ctlFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("ID");           // Field 0
				row.Add("Universe");     // Field 1
				row.Add("Controller");   // Field 2
				row.Add("Name");         // Field 3
				row.Add("Location");     // Field 4
				row.Add("Comment");      // Field 5
				row.Add("Active");       // Field 6
				row.Add("Brand");        // Field 7
				row.Add("Model");        // Field 8
				row.Add("Unit#");				 // Field 9
				row.Add("Output Count"); // Field 10
				row.Add("DMX Start");    // Field 11
				row.Add("Voltage");      // Field 12

				writer.WriteRow(row);

				for (int u = 0; u < AllUniverses.Count; u++)
				{
					DMXUniverse universe = AllUniverses[u];
					universe.DMXControllers.Sort();
					for (int c = 0; c < universe.DMXControllers.Count; c++)
					{
						try
						{
							DMXController controller = universe.DMXControllers[c];
							ctlName = controller.Name;
							row = new CsvRow();

							//row.Add(controller.DMXUniverse.ID.ToString());             // Field 0
							// Lets renumber all the IDs while we'ere at it.
							row.Add(writeID.ToString());                     // Field 0
							controller.ID = writeID;
							writeID++;
							row.Add(controller.DMXUniverse.ID.ToString()); // Field 1
							row.Add(controller.Identifier);                            // Field 2
							row.Add(controller.Name);                                  // Field 3
							row.Add(controller.Location);                              // Field 4
							row.Add(controller.Comment);                               // Field 5
							row.Add(controller.Active.ToString());                     // Field 6
							row.Add(controller.ControllerBrand);                       // field 7
							row.Add(controller.ControllerModel);                       // Field 8
							row.Add(controller.UnitID.ToString());							  		 // Filed 9									
							row.Add(controller.OutputCount.ToString());                // Field 10
							row.Add(controller.DMXStartAddress.ToString());            // Field 11
							row.Add(controller.Voltage.ToString());                    // Field 12

							writer.WriteRow(row);
						}
						catch (Exception ex)
						{
							string msg = "Error " + ex.ToString() + " while saving Controller " + ctlName;
							if (isWiz)
							{
								DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							errs++;
						}
					}
				}
				writer.Close();
			}
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while saving Controllers file " + ctlFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}
		public int SaveChannels(string filePath)
		{
			// Note: Channels are saved in order of universe, then controller, then output number.
			// Also, we need to save the universe and controller IDs in the channel file so that we can link the channels to the correct universe and controller when loading, rather than relying on the order of the universes and controllers in the file which could get messed up if there are any errors in the file
			// Start ID numering at ONE instead of zero for user friendliness when editing raw CSV files.
			// Note that they may be out of order or non-contigous in memory, but when we save them, we renumber them starting at 1 and make them contigous for user friendliness when editing raw CSV files.
			writeID = 1;
			int errs = 0;
			string chnName = ""; // debugging exceptions
			string chnFile = filePath + "Channels.csv";

			try
			{
				CsvFileWriter writer = new CsvFileWriter(chnFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("ID");           // Field 0
				row.Add("Universe#");    // Field 1
				row.Add("ControllerID"); // Field 2
				row.Add("Output#");      // Field 3
				row.Add("Name");         // Field 4
				row.Add("Location");     // Field 5
				row.Add("Comment");      // Field 6
				row.Add("Active");       // Field 7
				row.Add("Type");         // Field 8
				row.Add("Color");        // Field 9
				row.Add("ColorName");    // Field 10

				writer.WriteRow(row);

				AllUniverses.Sort();
				for (int u = 0; u < AllUniverses.Count; u++)
				{
					DMXUniverse universe = AllUniverses[u];
					universe.DMXControllers.Sort();
					for (int q = 0; q < universe.DMXControllers.Count; q++)
					{
						DMXController controller = universe.DMXControllers[q];
						controller.DMXChannels.Sort();
						for (int c = 0; c < controller.DMXChannels.Count; c++)
						{
							try
							{
								DMXChannel channel = controller.DMXChannels[c];
								chnName = channel.Name;
								row = new CsvRow();

								//row.Add(channel.DMXUniverse.ID.ToString());           // Field 0
								// Lets renumber all the IDs while we'ere at it.
								row.Add(writeID.ToString());                            // Field 0
								channel.ID = writeID;
								writeID++;
								row.Add(channel.DMXUniverse.UniverseNumber.ToString()); // Field 1
								row.Add(channel.DMXController.ID.ToString());           // Field 2
								//row.Add(channel.DMXController.Identifier);            // Field 2
								row.Add(channel.OutputNum.ToString());                  // Field 3
								row.Add(channel.Name);                                  // Field 4
								row.Add(channel.Location);                              // Field 5
								row.Add(channel.Comment);                               // Field 6
								row.Add(channel.Active.ToString());                     // Field 7
								row.Add(channel.DeviceType.ID.ToString());              // Field 8
								row.Add(LOR4.LOR4Admin.ColorToHex(channel.Color));      // Field 9
								row.Add(LOR4.LOR4Admin.NearestColorName(channel.Color));       // Field 10
								//row.Add(Etc.ColorName(channel.Color));       // Field 10

								writer.WriteRow(row);
							} // end try for Channels
							catch (Exception ex)
							{
								string msg = "Error " + ex.ToString() + " while saving Channel " + chnName;
								if (isWiz)
								{
									DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								errs++;
							}
						} // End Channel loop
					} // End Controller loop
				} // End Universe loop
				writer.Close();
			} // End try for StreamWriter (create file)
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while saving Channels file " + chnFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		public int ExportChannels(string filePath)
		{
			// Note: Channels are saved in order of universe, then controller, then output number.
			// Also, we need to save the universe and controller IDs in the channel file so that we can link the channels to the correct universe and controller when loading, rather than relying on the order of the universes and controllers in the file which could get messed up if there are any errors in the file
			// Start ID numering at ONE instead of zero for user friendliness when editing raw CSV files.
			// Note that they may be out of order or non-contigous in memory, but when we save them, we renumber them starting at 1 and make them contigous for user friendliness when editing raw CSV files.
			writeID = 1;
			int errs = 0;
			string chnName = ""; // debugging exceptions
			string chnFile = filePath + "Channels.csv";

			try
			{
				CsvFileWriter writer = new CsvFileWriter(chnFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe#");      // Field 0
				row.Add("UniverseName");   // Field 1
				row.Add("ControllerID");   // Field 2
				row.Add("ControllerName"); // Field 3
				row.Add("Output#");        // Field 4
				row.Add("DMXAddress");     // Field 5
				row.Add("Name");           // Field 6
				row.Add("Location");       // Field 7
				row.Add("Comment");        // Field 8
				row.Add("Active");         // Field 9
				row.Add("Type");           // Field 10
				row.Add("Color");          // Field 11
				row.Add("ColorName");      // Field 12

				writer.WriteRow(row);

				AllUniverses.Sort();
				for (int u = 0; u < AllUniverses.Count; u++)
				{
					DMXUniverse universe = AllUniverses[u];
					universe.DMXControllers.Sort();
					for (int q = 0; q < universe.DMXControllers.Count; q++)
					{
						DMXController controller = universe.DMXControllers[q];
						controller.DMXChannels.Sort();
						for (int c = 0; c < controller.DMXChannels.Count; c++)
						{
							try
							{
								DMXChannel channel = controller.DMXChannels[c];
								chnName = channel.Name;
								row = new CsvRow();
								row.Add(channel.UniverseNumber.ToString());        // Field 0
								row.Add(channel.DMXUniverse.Name);								 // Field 1
								row.Add(channel.DMXController.Identifier);         // Field 2
								row.Add(channel.DMXController.Name);							 // Field 3
								row.Add(channel.OutputNum.ToString());             // Field 4
								row.Add(channel.DMXAddress.ToString());            // Field 5
								row.Add(channel.Name);                             // Field 6
								row.Add(channel.Location);                         // Field 7
								row.Add(channel.Comment);                          // Field 8
								row.Add(channel.Active.ToString());                // Field 9
								row.Add(channel.DeviceType.Name);                  // Field 10
								row.Add(LOR4.LOR4Admin.ColorToHex(channel.Color)); // Field 11
								row.Add(channel.ColorName);                        // Field 12
								writer.WriteRow(row);
							} // end try for Channels
							catch (Exception ex)
							{
								string msg = "Error " + ex.ToString() + " while saving Channel " + chnName;
								if (isWiz)
								{
									DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
								errs++;
							}
						} // End Channel loop
					} // End Controller loop
				} // End Universe loop
				writer.Close();
			} // End try for StreamWriter (create file)
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while saving Channels file " + chnFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		public int OLD_SaveChannels(string filePath)
		{
			int errs = 0;
			string chnName = ""; // debugging exceptions
			string chnFile = filePath + "Channels.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(chnFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe");    // Field 0
				row.Add("Controller");  // Field 1
				row.Add("LOR4Output");      // Field 2
				row.Add("Name");        // Field 3
				row.Add("Location");    // Field 4
				row.Add("Comment");     // Field 5
				row.Add("Active");      // Field 6
				row.Add("Type");        // Field 7
				row.Add("Color");       // Field 8

				writer.WriteRow(row);

				AllChannels.Sort();
				for (int c = 0; c < AllChannels.Count; c++)
				{
					try
					{
						DMXChannel channel = AllChannels[c];
						chnName = channel.Name;
						row = new CsvRow();

						row.Add(channel.DMXUniverse.UniverseNumber.ToString()); // Field 0
						row.Add(channel.DMXController.Identifier);                // Field 1
						row.Add(channel.OutputNum.ToString());                 // Field 2
						row.Add(channel.Name);                                  // Field 3
						row.Add(channel.Location);                              // Field 4
						row.Add(channel.Comment);                               // Field 5
						row.Add(channel.Active.ToString());                     // Field 6
						row.Add(channel.DeviceType.ID.ToString());         // Field 7
						row.Add(LOR4.LOR4Admin.ColorToHex(channel.Color));                // Field 8

						writer.WriteRow(row);
					}
					catch (Exception ex)
					{
						string msg = "Error " + ex.ToString() + " while saving Channel " + chnName;
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						errs++;
					}
				}
				writer.Close();
			}
			catch (Exception ex)
			{
				string msg = "Error " + ex.ToString() + " while saving Channels file " + chnFile;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				errs++;
			}
			return errs;
		}

		private void frmList_Shown(object sender, EventArgs e)
		{
			bool gotData = false;
			int errs = 0;

			if (!formShown)
			{
				dbPath = userSettings.DBPath;
				if (dbPath.Length > 6)
				{
					if (Fyle.IsValidPath(dbPath))
					{
						string testFile = dbPath + "Channels.csv";
						if (Fyle.Exists(testFile))
						{
							gotData = true;
						}
					}
				}

				if (!gotData)
				{
					year = DateTime.Now.Year;  // Get it and store it so don't have to keep looking it back up
					dbPath = PathToDB();  // Get and save this too
					dlgFileOpen.InitialDirectory = dbPath;
					dlgFileOpen.Filter = "Channels.csv|Channels.csv";
					dlgFileOpen.DefaultExt = "csv";
					dlgFileOpen.Title = "Location of Channel Database";
					dlgFileOpen.CheckPathExists = true;
					dlgFileOpen.FileName = "Channels.csv";


					DialogResult dr = dlgFileOpen.ShowDialog();
					if (dr == DialogResult.OK)
					{
						if (Fyle.Exists(dlgFileOpen.FileName))
						{
							dbPath = Path.GetDirectoryName(dlgFileOpen.FileName) + "\\";
							userSettings.DBPath = dbPath;
							userSettings.Save();
							gotData = true;
						}
						else
						{
							//TODO: Handle file not found, perhaps prompt to create new database?
						}
					}
					else
					{
						//TODO: Handle file open dialog canceled
					}
				}
				if (gotData)
				{
					errs = LoadData(dbPath);
					if (AllUniverses.Count > 0)
					{
						BuildTree();
						btnCompareLOR.Enabled = true;
						btnComparex.Enabled = true;
						btnReport.Enabled = true;
					}
				}
				else
				{
					//TODO: Handle still not having data, perhaps prompt to create new database?
				}
				formShown = true;
			}
		}

		private void frmList_Shown_Old(object sender, EventArgs e)
		{
			if (!formShown)
			{
				year = DateTime.Now.Year;  // Get it and store it so don't have to keep looking it back up
				dbPath = PathToDB();  // Get and save this too

				string uniFile = dbPath + "Universes.csv";
				if (File.Exists(uniFile))
				{
					int errs = LoadData(dbPath);
				}
				else
				{
					string msg = "Data files not found in folder " + dbPath;
					msg += ".  If you have never used Channel-O-Rama, create some Universes, Controllers, and Channels.  ";
					msg += "If you have used Channel-O-Rama in previous years, create a folder for this year's data and ";
					msg += "copy last year's data to it as a starting point.  If you have already used it this year and ";
					msg += "think you should have data with Universe, Controllers, and Channels, please check the path ";
					msg += dbPath + " and make sure it exists, has not been deleted or moved, and that it contains 3 ";
					msg += "csv datafiles for Universes, Controllers, and Channels.";

					DialogResult dr = MessageBox.Show(this, msg, "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}

				if (AllUniverses.Count > 0)
				{
					BuildTree();
					btnCompareLOR.Enabled = true;
					btnComparex.Enabled = true;
					btnReport.Enabled = true;

				}

				formShown = true;
			}
		}

		private void frmList_Resize(object sender, EventArgs e)
		{
			int treeHt = this.ClientSize.Height - staStatus.Height - 16;
			int treeWd = this.ClientSize.Width - btnUniverse.Width - 24;
			int btnLeft = ClientSize.Width - btnUniverse.Width - 8;
			int okTop = treeChannels.Top + treeHt - btnOK.Height;
			treeChannels.Height = treeHt;
			treeChannels.Width = treeWd;
			btnOK.Top = okTop;
			btnUniverse.Left = btnLeft;
			btnController.Left = btnLeft;
			btnChannel.Left = btnLeft;
			btnReport.Left = btnLeft;
			btnFind.Left = btnLeft;
			btnSave.Left = btnLeft;
			btnCompareLOR.Left = btnLeft;
			btnComparex.Left = btnLeft;
			btnWiz.Left = btnLeft;
			btnOK.Left = btnLeft;
			int y = treeChannels.Top + treeChannels.Height - btnSave.Height;
			btnSave.Top = y;
		}

		private void BuildTree()
		{
			// Erases and rebuilds the entire tree from the Universes list.
			BuildUniverseTree(AllUniverses);
		}

		public void BuildUniverseTree(List<DMXUniverse> uniList)
		{
			// Erases and rebuilds the entire tree from the Universes list.
			// Note: Clears the universes list.
			treeChannels.Nodes.Clear();
			// Loop thru all universes and add them to the tree, then loop thru their controllers and add them, then loop thru their channels and add them.
			for (int u = 0; u < uniList.Count; u++)
			{
				// Grab a convenient reference to this universe.
				DMXUniverse universe = uniList[u];
				// Create new node for this universe and add to tree.
				TreeNodeAdv uniNode = new TreeNodeAdv("(New Universe)");
				// Use the node's tag to hold a reference to the DMXUniverse object, and the universe's tag to hold a reference to the node so we can easily find the node later when we need to update it
				uniNode.Tag = uniList[u];
				uniList[u].Tag = uniNode;
				// Add the universe to the tree as a top level node.
				treeChannels.Nodes.Add(uniNode);
				// Build the text and icon for this node based on the properties of the universe.
				BuildUniverseNode(uniNode);
				// Now loop thru the controllers for this universe and add them as child nodes, then loop thru their channels and add them as child nodes of the controller nodes.
				BuildControllerTree(uniNode);
			} // End loop for universes
		}

		private void BuildUniverseNode(TreeNodeAdv uniNode)
		{
			// Updates the text and icon for a universe node based on the current properties of the DMXUniverse object in the tag for this node.
			if (uniNode != null)
			{
				if (uniNode.Tag != null)
				{
					if (uniNode.Tag.GetType() == typeof(DMXUniverse))
					{
						DMXUniverse universe = (DMXUniverse)uniNode.Tag;
						//string nodeText = universe.UniverseNumber.ToString() + ": " + universe.Name;
						string nodeText = universe.FullName;
						int ImageIndex = TREEICONuniverse[0];
						int[] ico = { ImageIndex };
						uniNode.LeftImageIndices = ico;
						uniNode.Text = nodeText;
						if (universe.BadName || universe.BadNumber)
						{
							uniNode.TextColor = Color.Red;
						}
						else
						{
							uniNode.TextColor = Color.Black;
						}
					}
					else
					{
						string msg = "ERROR: BuildUniverseNode called for a node that does not have a DMXUniverse in its tag.";
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		public void BuildControllerTree(TreeNodeAdv uniNode)
		{
			// Builds the subtree for the controllers under a universe node, then calls BuildChannelTree for each controller node to build the channels under each controller.
			// Note: Does not clear existing controller nodes before building, so if rebuilding, be sure to clear them first.
			if (uniNode.Tag.GetType() == typeof(DMXUniverse))
			{
				// Get the DMXUniverse object for this node from the tag.
				DMXUniverse universe = (DMXUniverse)uniNode.Tag;
				// Loop thru all it's child controllers.
				for (int ct = 0; ct < universe.DMXControllers.Count; ct++)
				{
					// Grab a convenient reference to this controller.
					DMXController controller = universe.DMXControllers[ct];
					// Make a new node for this controller and add it to the tree as a child of the universe node.	
					TreeNodeAdv ctlNode = new TreeNodeAdv("(New Controller)");
					// Add a reference to the DMXController object in the tag for this node,
					ctlNode.Tag = controller;
					controller.Tag = ctlNode;
					// Add the node to the tree as a child of the universe node.
					uniNode.Nodes.Add(ctlNode);
					// Build the text for the node, include the Identifier only if it is not blank.
					BuildControllerNode(ctlNode);
					// Now loop thru the channels for this controller and add them as child nodes of this controller node.
					BuildChannelTree(ctlNode);
				} // End loop for controllers
			}
			else
			{
				string msg = "ERROR: BuildControllerTree called for a node that does not have a DMXUniverse in its tag.";
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			} // End if node's tag IS a universe
		}

		private void BuildControllerNode(TreeNodeAdv ctlNode)
		{
			// Updates the text and icon for a controller node based on the current properties of the DMXController object in the tag for this node.
			if (ctlNode != null)
			{
				if (ctlNode.Tag != null)
				{
					if (ctlNode.Tag.GetType() == typeof(DMXController))
					{
						DMXController controller = (DMXController)ctlNode.Tag;
						string nodeText = controller.FullName;
						int foo = controller.UnitID;
						foo = controller.DMXStartAddress;
						foo = controller.OutputCount;
						/*
						if (Etc.LORVersion > 0)
						{
							 nodeText = "Unit:" + controller.UnitID.ToString("00") + ": ";
						}
						nodeText += controller.DMXStartAddress.ToString("000") + "-";
						nodeText += l.ToString("000") + ": ";
						if (controller.Identifier.Length > 0)
						{
							nodeText += controller.Identifier + ": ";
						}
						nodeText += controller.Name;
						*/
						ctlNode.Text = nodeText;
						int l = controller.DMXStartAddress + controller.OutputCount - 1;
						int ImageIndex = TREEICONcontroller[0];
						int[] ico = { ImageIndex };
						ctlNode.LeftImageIndices = ico;
						if (controller.BadIdentity || controller.BadName || controller.BadAddress)
						{
							ctlNode.TextColor = Color.Red;
						}
						else
						{
							ctlNode.TextColor = Color.Black;
						}
					}
					else
					{
						string msg = "ERROR: BuildControllerNode called for a node that does not have a DMXController in its tag.";
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}
		public void BuildChannelTree(TreeNodeAdv ctlNode)
		{
			// Builds the subtree for the channels under a controller node.
			// Note: Does not clear existing channel nodes before building, so if rebuilding, be sure to clear them first.
			// Grab a handy-dandy reference to the DMXController object for this node from the tag.
			if (ctlNode.Tag.GetType() == typeof(DMXController))
			{
				DMXController controller = (DMXController)ctlNode.Tag;
				// Loop thru all channels for this controller and add them as child nodes of this controller node.
				for (int ch = 0; ch < controller.DMXChannels.Count; ch++)
				{
					// Make a new channel node
					DMXChannel channel = controller.DMXChannels[ch];
					// Make a new node for this channel and add it to the tree as a child of the controller node.
					TreeNodeAdv chanNode = new TreeNodeAdv("(New Channel)");
					// Add a reference to the DMXChannel object in the tag for this node, and a reference to this node in the channel's tag so we can easily find it later when we need to update it.
					chanNode.Tag = channel;
					channel.Tag = chanNode;
					ctlNode.Nodes.Add(chanNode);
					// Add the node to the tree as a child of the controller node.
					BuildChannelNode(chanNode);


				} // End loop for channels
			} // End if node's tag IS a controller
		  else
			{
				string msg = "ERROR: BuildChannelTree called for a node that does not have a DMXController in its tag.";
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return;
			}
		}

		private void BuildChannelNode(TreeNodeAdv chanNode)
		{
			// Updates the text and icon for a channel node based on the current properties of the DMXChannel object in the tag for this node.
			if (chanNode != null)
			{
				if (chanNode.Tag != null)
				{
					IDMXThingy thing=(IDMXThingy)chanNode.Tag;
					string sinfo = "Edit Node for " + thing.ObjectType.ToString() + thing.FullName;
					Debug.WriteLine(sinfo);
					if (chanNode.Tag.GetType() == typeof(DMXChannel))
					{
						DMXChannel channel = (DMXChannel)chanNode.Tag;
						//string nodeText = channel.OutputNum.ToString() + ": " + channel.Name;
						string nodeText = channel.FullName;
						chanNode.Text = nodeText;
						ImageList icons = treeChannels.LeftImageList;
						int iconIndex = LOR4.LOR4Admin.ColorIcon(icons, channel.Color,24);
						int[] ico = { iconIndex };
						chanNode.LeftImageIndices = ico;
						if (channel.BadName || channel.BadOutput)
						{
							chanNode.TextColor = Color.Red;
						}
						else
						{
							chanNode.TextColor = Color.Black;
						}
					}
					else
					{
						string msg = "ERROR: BuildChannelNode called for a node that does not have a DMXChannel in its tag.";
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			else
			{
				string sinfo = "BuildChannelNode called, but node.tag is null for " + chanNode.Text;
				Debug.WriteLine(sinfo);
				if (isWiz)
				{
					Debugger.Break();	
				}
			}
		}

		public void UpdateUniveseNode(TreeNodeAdv node)
		{
			if (node != null)
			{
				if (node.Tag != null)
				{
					if (node.Tag.GetType() == typeof(DMXUniverse))
					{
						DMXUniverse univ = (DMXUniverse)node.Tag;
						//string nodeText = univ.UniverseNumber.ToString() + ": " + univ.Name;
						string nodeText = univ.ToString();
						node.Text = nodeText;
					}
				}
			}
		}

		public void UpdateControllerNode(TreeNodeAdv node)
		{
			if (node != null)
			{
				if (node.Tag != null)
				{
					if (node.Tag.GetType() == typeof(DMXController))
					{
						DMXController ctlr = (DMXController)node.Tag;
						//string nodeText = ctlr.DMXStartAddress.ToString("000") + "-";
						//int l = ctlr.DMXStartAddress + ctlr.OutputCount - 1;
						//nodeText += l.ToString("000") + ": ";
						//nodeText += ctlr.ControllerID + ": " + ctlr.Name;
						string nodeText = ctlr.ToString();
						node.Text = nodeText;
					}
				}
			}
		}

		public void UpdateChannelNode(TreeNodeAdv node)
		{
			if (node != null)
			{
				if (node.Tag != null)
				{
					if (node.Tag.GetType() == typeof(DMXChannel))
					{
						DMXChannel channel = (DMXChannel)node.Tag;
						//string nodeText = channel.LOR4Output.ToString() + ": " + channel.Name;
						string nodeText = channel.ToString();
						node.Text = nodeText;
						ImageList icons = treeChannels.LeftImageList;
						int iconIndex = LOR4.LOR4Admin.ColorIcon(icons, channel.Color);
						int[] ico = { iconIndex };
						node.LeftImageIndices = ico;
						//TODO: Check for problem and update foreground text color (either way)
					}
				}
			}
		}


		private void treeChannels_AfterSelect(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			string ttxt = "";
			if (Fyle.isWiz)
			{
				//MessageBox.Show("Selected node: " + node.Text);
			}
			if (node != null)
			{
				// Reset to default, for now, may get changed again below
				btnChannel.Enabled = true;
				btnChannel.Text = "Edit\r\nChannel";
				tipTool.SetToolTip(btnChannel, "Edit the selected Channel.");
				btnController.Text = "Edit\r\nController";
				tipTool.SetToolTip(btnController, "Edit the selected Controller.");
				btnUniverse.Text = "Edit\r\nUniverse";
				tipTool.SetToolTip(btnUniverse, "Edit the selected Universe.");

				//int[] ni = node.LeftImageIndices;
				// Did user selected a Universe node?
				//bool en = (ni == TREEICONuniverse);
				//if (en)
				if (node.Tag.GetType() == typeof(DMXUniverse))
				{
					DMXUniverse uni = (DMXUniverse)node.Tag;
					btnUniverse.Text = "Edit\r\nUniverse";
					ttxt = "Edit the selected Universe.\r\n";
					ttxt += uni.FullName;
					tipTool.SetToolTip(btnUniverse, ttxt);
					string xc = ""; //  "xLights Channels: " + ctl.xLightsAddress + "-" + (ctl.xLightsAddress + ctl.OutputCount - 1).ToString();
					lblxChannel.Text = xc;
					xc = " @ " + uni.Location;
					pnlStatus.Text = xc;
					xc = uni.Comment;
					treeChannels.HelpTextControl.Text = xc;
					btnChannel.Enabled = false;
				}
				else
				{
					btnUniverse.Text = "Add\r\nUniverse";
					tipTool.SetToolTip(btnUniverse, "Add a new Universe.");
				}
				//btnChannel.Enabled = !en;

				// Did user selected a Controller node?
				//en = (ni == TREEICONcontroller);
				//if (en)
				if (node.Tag.GetType() == typeof(DMXController))
				{
					DMXController ctl = (DMXController)node.Tag;
					btnController.Text = "Edit\r\nController";
					ttxt = "Edit the selected Controller.\r\n";
					ttxt += ctl.FullName;
					tipTool.SetToolTip(btnController, ttxt);
					string xc = "xLights Channels: " + ctl.xLightsAddress + "-" + (ctl.xLightsAddress + ctl.OutputCount - 1).ToString();
					lblxChannel.Text = xc;
					xc = ctl.ControllerBrand + " " + ctl.ControllerModel + " @ " + ctl.Location;
					pnlStatus.Text = xc;
					xc = ctl.Comment;
					treeChannels.HelpTextControl.Text = xc;
				}
				else
				{
					btnController.Text = "Add\r\nController";
					ttxt= "Add a new Controller to the selected Universe.";
					tipTool.SetToolTip(btnController, ttxt);

				}

				// If it is NOT a Universe, AND it is NOT a controller--
				//   Then by process of elimination, it must be a channel!
				//     (Channels have numerous and various colored icons)
				// Did user selected a Channel node?
				//en = ((ni != TREEICONuniverse) && (ni != TREEICONcontroller));
				//if (en)
				if (node.Tag.GetType() == typeof(DMXChannel))
				{
					DMXChannel ch = (DMXChannel)node.Tag;
					btnChannel.Text = "Edit\r\nChannel";
					ttxt= "Edit the selected Channel.\r\n";
					ttxt += ch.FullName;
					tipTool.SetToolTip(btnChannel, ttxt);
					btnRemove.Visible = true;
					if (node.Tag.GetType() != typeof(DMXChannel))
					{
						string msg = "ERROR: Selected node has an icon that indicates it is a channel, but its tag does not contain a DMXChannel object.";
						if (isWiz)
						{
							DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						return;
					}
					else 
					{
						string xc = "xLights Channel: " + ch.xLightsAddress.ToString();
						lblxChannel.Text = xc;
						xc = ch.DeviceType + " @ " + ch.Location;
						pnlStatus.Text = xc;
						xc = ch.Comment;
						treeChannels.HelpTextControl.Text = xc;
					}
				}
				else
				{
					btnChannel.Text = "Add\r\nChannel";
					tipTool.SetToolTip(btnChannel, "Add a new Channel to the selected Controller.");
					btnRemove.Visible = false;
				}



			}
		}

		private void treeChannelList_DoubleClick(object sender, EventArgs e)
		{
			bool needRebuild = false;
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{
				int[] ni = node.LeftImageIndices;
				if (ni == TREEICONcontroller)
				{
					EditControllerNode(node);
				}
				else
				{
					if (ni == TREEICONuniverse)
					{
						EditUniverseNode(node);
					}
					else
					{
						int ix = ni[0];
						if (ix > 2)
						{
							EditChannelNode(node);
						}
					}
				}
			}
		}

		private bool EditUniverseNode(TreeNodeAdv node)
		{
			//bool needSort = false;
			DMXUniverse universe = (DMXUniverse)node.Tag;
			bool success = EditUniverse(universe);
			/*
			universe.Editing = true;
			DMXUniverse newUni = universe.Clone();
			int oldUniNum = universe.UniverseNumber;
			frmUniverse uniForm = new frmUniverse(newUni);
			//uniForm.universes = Universes;
			DialogResult dr = uniForm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				if (uniForm.dirty)
				{
					MakeDirty(true);
					universe.Clone(newUni);
					UpdateUniveseNode(node);
					// Did the Universe Number change?
					if (universe.UniverseNumber != oldUniNum)
					{
						needSort = true;
					}
					if (needSort)
					{
						treeChannels.Nodes.Sort();
					}
				}
			}
			if (universe.BadName || universe.BadNumber)
			{
				node.TextColor = Color.Red;
			}
			else
			{
				node.TextColor = Color.Black;
			}

			universe.Editing = false;
			uniForm.Dispose();
			*/
			return success;
		}

		public bool EditControllerNode(TreeNodeAdv node)
		{
			DMXController controller = (DMXController)node.Tag;
			bool success = EditController(controller);
			return success;
		}

		public bool EditChannelNode(TreeNodeAdv node)
		{
			//bool needSort = false;
			DMXChannel channel = (DMXChannel)node.Tag;
			bool success = EditChannel(channel);
			return success;
		}

		public bool EditChannel(DMXChannel channel)
		{
			// Return True if channel was (successfully) edited.
			// Returns False if User Canceled the Edit dialog, or in case of an error.
			bool success = false;
			// Get the controller it is connnected to, in case that changes
			DMXController controller = channel.DMXController;
			channel.Editing = true;
			// Create a new instance of the channel edit form, passing the channel to edit and a reference to this form (for callbacks and such)
			frmChannel chanForm = new frmChannel(channel, this);
			// Show the channel edit dialog, modal
			DialogResult dr = chanForm.ShowDialog(this);
			try
			{
				// Upon return from the edit dialog, did they Cancel, or Accept Changes
				if (dr == DialogResult.OK)  // Clicked OK
				{
					if (controller.Tag.GetType() == typeof(TreeNodeAdv))
					{
						//TreeNodeAdv ctlNode = (TreeNodeAdv)controller.Tag;
						lblDirty.Text = chanForm.changes.ToString();
						// Was anything even changed (on the channel)?
						if (chanForm.isDirty)
						{
							// Declare here, cuz will need later
							TreeNodeAdv ctlNode = (TreeNodeAdv)controller.Tag;
							TreeNodeAdv chNode = (TreeNodeAdv)channel.Tag;
							// Did the Controller change?
							if (channel.DMXController.ID != controller.ID)
							{
								// Find old controller and remove the Original Channel
								for (int c = 0; c < controller.DMXChannels.Count; c++)
								{
									if (channel.ID == controller.DMXChannels[c].ID)
									{
										controller.DMXChannels.RemoveAt(c);
										chNode.Remove();
										c = controller.DMXChannels.Count; // Force loop exit
									}
								}
								// Fetch the new controller, that the channel was moved _TO_
								controller = channel.DMXController;
								// And add the modified channel
								controller.DMXChannels.Add(channel);
								// Re-Sort the controller channels (by number) to put the newly moved channel in proper position
								controller.DMXChannels.Sort();
								// Fetch the node of the new controller
								ctlNode = (TreeNodeAdv)controller.Tag;
								// Rebuild it
								ctlNode.Nodes.Clear();
								BuildChannelTree(ctlNode);
							} // End Controller Changed
							else // Still on the same controller
							{
								// Update the same node, which should be the same controller node, since still on same controller
								controller.DMXChannels.Sort(); // In case the output number changed, need to re-sort the channels on this controller
																							 // Get the (original) controller node (Note: did not change)
																							 // And re-build it
								BuildChannelNode(chNode); // Just update this one node, since still on same controller, so no need to re-build the entire tree for this controller
																				//BuildChannelTree(ctlrNode);
																				//}
							} // End Still on same controller
							MakeDirty(true);
							success = true;
						} // End dirty
					} // End controller's tag is a TreeNodeAdv
					else
					{
						string msg = "ERROR: Channel's controller does not have a TreeNodeAdv in its tag.";
						if (isWiz)
						{
							DialogResult dr2 = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				} // End DialogResult is OK
			} // End Try
			catch (Exception ex)
			{
				// So when the effing form throws an effing excepttion for some effing reaason when it is effing closed
				// Or some effing other thing goes wrong for some effing reason
				// Consider that a cancel
				int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
				string msg = "Error on line " + ln.ToString() + "\r\n";
				msg += ex.ToString() + " while exiting Channel editor " + channel.Name;
				if (Fyle.isWiz || Fyle.InIDE)
				{
					Fyle.BUG("EditChannel", ex);
					//DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			// Clear the editing flag (necessary if cloned back)
			channel.Editing = false;
			// Dispose of, clear, and remove Channel Editor form from memory
			chanForm.Dispose();
			return success;
		}

		private void btnChannel_Click(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{
				//if (treeChannels.SelectedNode.Tag.GetType() == typeof(DMXController))
				string funct = btnChannel.Text.Substring(0, 3);
				// ** ADD MODE **
				if (funct == "Add")
				{
					TreeNodeAdv parentNode = treeChannels.SelectedNode;
					DMXController parentCtl = (DMXController)treeChannels.SelectedNode.Tag;
					AddNewChannel(parentCtl);
				} // End funct [first 3 characters of button text] was "Add";
					// ** EDIT MODE **
				else if (funct == "Edi")
				{
					int[] ni = node.LeftImageIndices;
					bool en = ((ni != TREEICONuniverse) && (ni != TREEICONcontroller));
					if (en)
					{
						DMXChannel chanOriginal = (DMXChannel)node.Tag;
						EditChannel(chanOriginal);
					}
				} // End funct [first 4 characters of button text] was "Edit";
			} // End a node was selected (treeChannels.selectedNode != null)
		}

		private bool AddNewChannel(DMXController parentCtl)
		{
			// Create and prepare the new channel to be added to the parent controller
			TreeNodeAdv parentNode = treeChannels.SelectedNode;
			DMXChannel channel = new DMXChannel();
			bool success = false;
			lastID++;
			channel.ID = lastID;
			channel.DMXController = parentCtl;
			channel.Editing = true;
			DMXController oldCtlr = channel.DMXController;
			frmChannel chanForm = new frmChannel(channel,this);

			// Try to find an unused output
			int o = 1;
			// Loop thru all channels/outputs already on this controller
			for (int ci = 0; ci < oldCtlr.DMXChannels.Count; ci++)
			{
				if (oldCtlr.DMXChannels[ci].OutputNum == o)
				{
					// If this output number is already used, try the next one
					o++;
				}
				else
				{
					// output number 'o' is not used, so start with it
					// Break from loop
					ci = oldCtlr.DMXChannels.Count;
				}
			}
			channel.OutputNum = o;

			// Show the Channel edit dialog, modal
			DialogResult dr = chanForm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				// User accepted changes on the ChannelEdit form (clicked OK)
				if (chanForm.isDirty)
				{
					channel.DMXController.DMXChannels.Add(channel);
					channel.DMXController.DMXChannels.Sort();
					parentNode = (TreeNodeAdv)channel.DMXController.Tag;
					BuildChannelTree(parentNode);
					AllChannels.Add(channel);
					AllChannels.Sort();
					//TreeNodeAdv nodeNewChan = new TreeNodeAdv(channel.ToString());
					//parentNode.Nodes.Add(nodeNewChan);
					//nodeNewChan.Tag = channel;
					int ImageIndex = LOR4.LOR4Admin.ColorIcon(imlTreeIcons, channel.Color);
					int[] ico = { ImageIndex };
					//nodeNewChan.LeftImageIndices = ico;
					//channel.Tag = nodeNewChan;
					success = true;
					MakeDirty(true);
					UpdateStatus();

					//BuildTree();
					//treeChannels.Nodes.Sort();
					//BuildChannelTree(parentNode);
				}
				if (channel.BadName || channel.BadOutput)
				{
					parentNode.TextColor = Color.Red;
				}
				else
				{
					parentNode.TextColor = Color.Black;
				}
				treeChannels.SelectedNode = parentNode;
			} // End user accepted changes on ChannelEdit form (clicked OK)
			else
			{
				// User canceled out of the ChannelEdit form
				// Remove the new temporary channel from the parent controller's channel list
				for (int c = 0; c < oldCtlr.DMXChannels.Count; c++)
				{
					if (channel.ID == oldCtlr.DMXChannels[c].ID)
					{
						oldCtlr.DMXChannels.RemoveAt(c);
						//BuildChannelTree(parentNode);
						c = oldCtlr.DMXChannels.Count; // Force loop exit
					}
				}
			}
			treeChannels.Focus();
			channel.Editing = false;
			chanForm.Dispose();
			return success;
		}

		private bool AddNewController(DMXUniverse parentUni)
		{
			bool success = false;
			TreeNodeAdv parentNode = treeChannels.SelectedNode;
			DMXController controller = new DMXController();
			lastID++;
			controller.ID = lastID;
			controller.DMXUniverse = parentUni;
			controller.Editing = true;
			DMXUniverse universe = controller.DMXUniverse;
			frmController ctlForm = new frmController(controller, this);
			//ctlForm.universes = universes;

			// Try to find an unused start address
			int unit = 1;
			int addr = 1;
			int size = 16;
			for (int ui = 0; ui < universe.DMXControllers.Count; ui++)
			{
				DMXController ctl = universe.DMXControllers[ui];
				if (Etc.LORVersion > 0)
				{
					if (ctl.UnitID == unit)
					{
						// This unit number is already used, so try the next one
						unit++;
					}
					else
					{
						// This start address is not used, so start with it
						// Break from loop
						controller.UnitID = unit;
						ui= universe.DMXControllers.Count; // Force exit of loop
					}
				}
				else
				{
					if (ctl.DMXStartAddress <= addr)
					{
						// This start address is already used, so try the next one
						addr += ctl.OutputCount;
					}
					else
					{
						// This start address is not used, so start with it
						// Break from loop
						controller.DMXStartAddress = addr;
						ui= universe.DMXControllers.Count; // Force exit of loop
					}
				}
			}
			// Show the Channel edit dialog, modal
			DialogResult dr = ctlForm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				// User accepted changes on the ChannelEdit form (clicked OK)
				if (ctlForm.isDirty)
				{
					controller.DMXUniverse.DMXControllers.Add(controller);
					controller.DMXUniverse.DMXControllers.Sort();
					parentNode = (TreeNodeAdv)controller.DMXUniverse.Tag;
					BuildChannelTree(parentNode);
					//AllControllers.Add(controller);
					//AllControllers.Sort();
					int ImageIndex = TREEICONcontroller[0];
					int[] ico = { ImageIndex };
					MakeDirty(true);
					UpdateStatus();
					success = true;
				}
			}
			if (controller.BadName || controller.BadIdentity || controller.BadAddress)
			{
				parentNode.TextColor = Color.Red;
			}
			else
			{
				parentNode.TextColor = Color.Black;
			}
			controller.Editing = false;
			ctlForm.Dispose();
			treeChannels.Focus();
			return success;
		}

		private bool AddNewUniverse()
		{
			bool success = false;
			//TreeNodeAdv parentNode = treeChannels.SelectedNode;
			DMXUniverse universe = new DMXUniverse();
			lastID++;
			universe.ID = lastID;
			//universe.DMXUniverse = parentUni;
			universe.Editing = true;
			//DMXUniverse universe = universe.DMXUniverse;
			frmUniverse ctlForm = new frmUniverse(universe, this);
			//ctlForm.universes = universes;

			// Try to find an unused start address
			int uni = 1;
			for (int ui = 0; ui < AllUniverses.Count; ui++)
			{
				DMXUniverse verse = AllUniverses[ui];
				if (verse.UniverseNumber <= uni)
				{
					// This unit number is already used, so try the next one
					uni++;
				}
				else
				{
					// This start address is not used, so start with it
					// Break from loop
					universe.UniverseNumber = uni;
					ui = AllUniverses.Count; // Force exit of loop
				}
			}
			// Show the Channel edit dialog, modal
			DialogResult dr = ctlForm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				// User accepted changes on the ChannelEdit form (clicked OK)
				if (ctlForm.isDirty)
				{
					AllUniverses.Add(universe);
					AllUniverses.Sort();
					//parentNode = (TreeNodeAdv)universe.DMXUniverse.Tag;
					//BuildChannelTree(parentNode);
					BuildTree();
					//Alluniverses.Add(universe);
					//Alluniverses.Sort();
					int ImageIndex = TREEICONuniverse[0];
					int[] ico = { ImageIndex };
					MakeDirty(true);
					UpdateStatus();
					success = true;
				}
			}
			TreeNodeAdv parentNode = (TreeNodeAdv)universe.Tag;
			if (universe.BadName || universe.BadNumber)
			{
				parentNode.TextColor = Color.Red;
			}
			else
			{
				parentNode.TextColor = Color.Black;
			}
			universe.Editing = false;
			ctlForm.Dispose();
			treeChannels.Focus();
			return success;
		}

		private bool EditController(DMXController controller)
		{
			// Return True if channel was (successfully) edited.
			// Returns False if User Canceled the Edit dialog, or in case of an error.
			bool success = false;
			// Get the universe it is connnected to, in case that changes
			DMXUniverse universe = controller.DMXUniverse;
			controller.Editing = true;
			// Create a new instance of the controller edit form, passing the controller to edit and a reference to this form (for callbacks and such)
			frmController ctlForm = new frmController(controller, this);
			// Show the controller edit dialog, modal
			DialogResult dr = ctlForm.ShowDialog(this);
			try
			{
				// Upon return from the edit dialog, did they Cancel, or Accept Changes
				if (dr == DialogResult.OK)
				{
					if (universe.Tag.GetType() == typeof(TreeNodeAdv))
					{
						//TreeNodeAdv uniNode = (TreeNodeAdv)universe.Tag;
						lblDirty.Text = ctlForm.changes.ToString();
						// Was anything even changed (on the controller)?
						if (ctlForm.isDirty)
						{
							// Declare here, cuz will need later
							TreeNodeAdv uniNode = (TreeNodeAdv)universe.Tag;
							TreeNodeAdv ctlNode = (TreeNodeAdv)controller.Tag;
							// Did the Universe change?
							if (controller.DMXUniverse.ID != universe.ID)
							{
								// Find old Universe and remove the Original Controller
								for (int c = 0; c < universe.DMXControllers.Count; c++)
								{
									if (controller.ID == universe.DMXControllers[c].ID)
									{
										universe.DMXControllers.RemoveAt(c);
										ctlNode.Remove();
										c = universe.DMXControllers.Count; // Force loop exit
									}
								}
								// Fetch the new universe, that the controller was moved _TO_
								universe = controller.DMXUniverse;
								// And add the modified controller
								universe.DMXControllers.Add(controller);
								// Re-Sort the universe controllers (by number) to put the newly moved controller in proper position
								universe.DMXControllers.Sort();
								// Fetch the node of the new universe
								uniNode = (TreeNodeAdv)controller.Tag;
								// Rebuild it
								uniNode.Nodes.Clear();
								BuildControllerTree(uniNode);
							}  // End Universe Changed
							else // Still on the same universe
							{
								// Update the same node, which should be the same universe node, since still on same universe
								universe.DMXControllers.Sort(); // In case the start address changed, need to re-sort the controllers on this universe
																								// Get the (original) universe node (Note: did not change)
								uniNode.Nodes.Clear();                                // And re-build it
								BuildUniverseNode(uniNode);
								BuildControllerNode(ctlNode); // Just update this one node, since still on same universe, so no need to re-build the entire tree for this universe
																							//BuildControllerTree(uniNode);
							}
							MakeDirty(true);
							success = true;
						} // End dirty
					} // End universe's tag is a TreeNodeAdv
					else
					{
						string msg = "ERROR: Controller's universe does not have a TreeNodeAdv in its tag.";
						if (isWiz)
						{
							DialogResult dr2 = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				} // End DialogResult is OK\
			} // End Try
			catch (Exception ex)
			{
				// So when the effing form throws an effing excepttion for some effing reaason when it is effing closed
				// Or some effing other thing goes wrong for some effing reason
				// Consider that a cancel
				int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
				string msg = "Error on line " + ln.ToString() + "\r\n";
				msg += ex.ToString() + " while exiting Controller editor " + controller.Name;
					
				if (Fyle.isWiz || Fyle.InIDE)
				{
					Fyle.BUG("EditController", ex);
					//DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (controller.BadName || controller.BadIdentity || controller.BadAddress)
			{
				TreeNodeAdv node = (TreeNodeAdv)controller.Tag;
				node.TextColor = Color.Red;
			}
			else
			{
				TreeNodeAdv node = (TreeNodeAdv)controller.Tag;
				node.TextColor = Color.Black;
			}
			controller.Editing = false;
			ctlForm.Dispose();
			return success;
		}

		private bool EditUniverse(DMXUniverse	universe)
		{
			// Return True if universe was (successfully) edited.
			// Returns False if User Canceled the Edit dialog, or in case of an error.
			bool success = false;

			int oldUni = universe.UniverseNumber;
			universe.Editing = true;
			// Create a new instance of the universe edit form, passing the universe to edit and a reference to this form (for callbacks and such)
			frmUniverse ctlForm = new frmUniverse(universe, this);
			// Show the universe edit dialog, modal
			DialogResult dr = ctlForm.ShowDialog(this);
			try
			{
				// Upon return from the edit dialog, did they Cancel, or Accept Changes
				if (dr == DialogResult.OK)
				{
					//lblDirty.Text = ctlForm.changes.ToString();
					if (ctlForm.isDirty)
					{
						TreeNodeAdv uniNode = (TreeNodeAdv)universe.Tag;
						// Did the universe change?
						if (universe.UniverseNumber != oldUni)
						{
							// Find old Universe and remove the Original universe
							//for (int c = 0; c < universe.DMXuniverses.Count; c++)
							//{
							//	if (universe.ID == universe.DMXuniverses[c].ID)
							//	{
							//		universe.DMXuniverses.RemoveAt(c);
							//		BuildChannelTree(uniNode);
							//		c = universe.DMXChannels.Count; // Force loop exit
							//	}
							//}
							// Fetch the new universe, that the channel was moved _TO_
							//universe = universe.DMXUniverse;
							// And add the modified channel
							//universe.DMXuniverses.Add(universe);
							// Re-Sort the universe channels (by number) to put the newly moved channel in proper position
							//universe.DMXuniverses.Sort();
							treeChannels.Nodes.Sort();
							// Fetch the node of the new universe
							//uniNode = (TreeNodeAdv)universe.Tag;
							// Rebuild it
							//BuildChannelTree(uniNode);
						}  // End Universe Changed
						else
						{
							// Update the same node, which should be the same universe node, since still on same universe
							//universe.DMXuniverses.Sort(); // In case the start address changed, need to re-sort the universes on this universe
							//uniNode = (TreeNodeAdv)universe.Tag;
							//BuildChannelTree(uniNode);
						}
						MakeDirty(true);
						success = true;
					} // End dirty
				} // End DialogResult is OK\
			} // End Try
			catch (Exception ex)
			{
				// So when the effing form throws an effing excepttion for some effing reaason when it is effing closed
				// Or some effing other thing goes wrong for some effing reason
				// Consider that a cancel
				int ln = LOR4.LOR4Admin.ExceptionLineNumber(ex);
				string msg = "Error on line " + ln.ToString() + "\r\n";
				msg += ex.ToString() + " while exiting universe editor " + universe.Name;

				if (Fyle.isWiz || Fyle.InIDE)
				{
					Fyle.BUG("Edituniverse", ex);
					//DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (universe.BadName || universe.BadNumber)
			{
				TreeNodeAdv node = (TreeNodeAdv)universe.Tag;
				node.TextColor = Color.Red;
			}
			else
			{
				TreeNodeAdv node = (TreeNodeAdv)universe.Tag;
				node.TextColor = Color.Black;
			}
			universe.Editing = false;
			ctlForm.Dispose();
			return success;
		}



		private void btnController_Click(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{
				//if (treeChannels.SelectedNode.Tag.GetType() == typeof(DMXUniverse))
				string funct = btnController.Text.Substring(0, 3);
				if (funct == "Add")
				{
					//TODO: Make sure selected node is a Universe
					//TODO: Or if not, traverse up the tree to find the parent universe
					TreeNodeAdv parentNode = treeChannels.SelectedNode;
					DMXUniverse parentUni = (DMXUniverse)treeChannels.SelectedNode.Tag;
					AddNewController(parentUni);

				} // End funct [first 3 characters of button text] was "Add";
				if (funct == "Edi")
				{
					int[] ni = node.LeftImageIndices;
					bool en = (ni[0] == TREEICONcontroller[0]);
					if (en)
					{
						DMXController ctlrOriginal = (DMXController)node.Tag;
						EditController(ctlrOriginal);
						//EditController(controller);
					}

				} // End funct [first 3 characters of button text] was "Edi";
			}

		}

		private void btnControllerOldCode()
		{
			/*
//DMXController ctlr = new DMXController();
//lastID++;
//ctlr.ID = lastID;
//ctlr.DMXUniverse = parentUni;
//ctlr.Editing = true;
//DMXUniverse oldUniv = ctlr.DMXUniverse;
//DMXController newCtlr = ctlr.Clone();
frmController ctlrForm = new frmController(ctlr, universes);
//ctlrForm.AllChannels = AllChannels;
//ctlrForm.universes = universes;

// This is _supposed_ to minimize the main form when the modal child is minimized, but it is not working, and I don't know why, so commenting out for now.  May revisit later.
using (ctlrForm modalChild = new ctlrForm())
{
	// Subscribe to the child's Resize event
	modalChild.Resize += (s, ev) =>
	{
		if (modalChild.WindowState == FormWindowState.Minimized)
		{
			this.WindowState = FormWindowState.Minimized;
		}
	};

	modalChild.ShowDialog(this);
}

//DialogResult dr = ctlrForm.ShowDialog(this);
//if (dr == DialogResult.OK)
//{
	// Find its universe (may have changed)
	for (int u = 0; u < Universes.Count; u++)
	{
		if (ctlr.UniverseNumber == AllUniverses[u].UniverseNumber)
		{
			parentUni = AllUniverses[u];
			u = AllUniverses.Count; // Force loop exit
		}
	}
	parentUni.DMXControllers.Add(ctlr);
	parentUni.DMXControllers.Sort();
	parentNode = (TreeNodeAdv)parentUni.Tag;
	// Create a bunch of channels for it
	// 'Silver' color is a light grayish color #C0C0C0
	//   Not the same as the 'Light Gray' color which is #D3D3D3
	int ImageIndex = LOR4.LOR4Admin.ColorIcon(imlTreeIcons, Color.Silver);
	int[] ico = { ImageIndex };
	TreeNodeAdv ctlrNode = new TreeNodeAdv(ctlr.ToString());
	ctlrNode.LeftImageIndices = TREEICONcontroller;
	ctlrNode.Tag = ctlr;
	ctlr.Tag = ctlrNode;
	for (int chx = 1; chx <= ctlr.OutputCount; chx++)
	{
		string chName = "Spare " + ctlr.ControllerID + chx.ToString("00");
		DMXChannel dch = new DMXChannel(ctlr, chName);
		dch.OutputNum = chx;
		dch.Color = Color.Silver;
		dch.DeviceType = GetTypeByName("Spare");
		lastID++;
		dch.ID = lastID;
		dch.Active = true;
		//dch.DMXController = ctlr; // Shouldn't need this, should be assigned at creation
		dch.Dirty = true;
		TreeNodeAdv chNode = new TreeNodeAdv(dch.Name);
		chNode.LeftImageIndices = ico;
		dch.Tag = chNode;
		chNode.Tag = dch;
		//ctlr.DMXChannels.Add(dch); // Shouldn't need this either
		ctlrNode.Nodes.Add(chNode);
		AllChannels.Add(dch);
	}
	ctlr.DMXChannels.Sort();
	parentNode.Nodes.Add(ctlrNode);
	//ImageIndex = TREEICONcontroller[0];
	//ico = { ImageIndex};
	//ico = TREEICONcontroller;
	//node.LeftImageIndices = ico;
	parentNode.Sort();

	MakeDirty(true);
	//BuildTree();
		}
					//ctlr.Editing = false;
					//ctlrForm.Dispose();
*/

		}



		private void btnUniverse_Click(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{

				string funct = btnUniverse.Text.Substring(0, 3);

				if (funct == "Add")
				{


					AddNewUniverse();
				} // End funct [first 3 characters of button text] was "Add";

				if (funct == "Edi")
				{
					int[] ni = node.LeftImageIndices;
					bool en = (ni[0] == TREEICONuniverse[0]);
					if (en)
					{
						DMXUniverse uniOriginal = (DMXUniverse)node.Tag;
						EditUniverse(uniOriginal);
					}
				} // End funct [first 4 characters of button text] was "Edi";
			}
		}

		public void MakeDirty(bool isDirty)
		{
			if (isDirty != dirty)
			{
				if (isDirty)
				{
					// anything else we need to do?
					dirty = isDirty;
					btnSave.Enabled = true;
					this.Text = myTitle + " *";
				}
				else
				{
					// anything else we need to do?
					dirty = isDirty;
					btnSave.Enabled = false;
					this.Text = myTitle;
				}
			}
		}


		private void frmList_Click(object sender, EventArgs e)
		{
			int x = 1;

		}

		private void frmList_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseReason cr = e.CloseReason;
			string rc = cr.ToString();

			if (dirty)
			{
				string msg = "Channel information has changed.\r\nSave?";
				DialogResult dr = MessageBox.Show(this, msg, "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				if (dr == DialogResult.Yes)
				{
					SaveData(dbPath);
				}
			}
			if (!e.Cancel)
			{
				this.SaveView();
				RestoreUserSettings();
			}
		}

		private int ExportToSpreadsheet(string filePath, bool includeHeaders = false)
		{
			int errs = 0;
			string fooPath = filePath.Substring(0, filePath.Length - 1);
			int q = fooPath.LastIndexOf("\\");
			string parentPath = fooPath.Substring(0, q) + "\\";
			string sprFile = parentPath + "ChannelSpreadsheet.csv";
			dlgFileSave.FileName = "ChannelSpreadsheet.csv";
			dlgFileSave.Filter = "Comma-Separated-Values *.csv|*.csv";
			dlgFileSave.DefaultExt = "*.csv";
			dlgFileSave.Title = "Export Channels to Spreadsheet";
			dlgFileSave.CheckPathExists = true;
			dlgFileSave.ValidateNames = true;
			dlgFileSave.OverwritePrompt = true;
			dlgFileSave.InitialDirectory = parentPath;
			DialogResult dr = dlgFileSave.ShowDialog();
			if (dr == DialogResult.OK)
			{
				errs = ExportSpreadsheet(dlgFileSave.FileName, includeHeaders);
			}
			else
			{
				errs = 999;
			}
			return errs;
		}

		private int ExportSpreadsheet(string fileName, bool includeHeaders = false)
		{
			int errs = 0;
			string uniName = "";
			string ctlName = "";
			string chnName = ""; // debugging exceptions
													 //string sprFile = filePath + "ChannelSpreadsheet.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(fileName);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe");         // Field 0
				row.Add("Controller");       // Field 1
				row.Add("LOR4Output");           // Field 2
				row.Add("DMX Address");      // Field 3
				row.Add("xLights Address");  // Field 4

				// Is this really the column I wanna put this in?
				row.Add("Active");           // Field 5

				row.Add("Name");             // Field 6
				row.Add("Type");            // Field 7
				row.Add("Color");            // Field 8
				row.Add("Location");         // Field 9
				row.Add("Comment");          // Field 10

				writer.WriteRow(row);

				AllUniverses.Sort();
				for (int u = 0; u < AllUniverses.Count; u++)
				{
					DMXUniverse universe = AllUniverses[u];
					uniName = universe.Name;
					try
					{
						row = new CsvRow();
						row.Add(universe.UniverseNumber.ToString());       // Field 0 - Universe
						row.Add("");                                  // Field 1 - Controller
						row.Add("");                                  // Field 2 - LOR4Output
						row.Add("");                                  // Field 3 - DMX Address
						row.Add(universe.xLightsAddress.ToString());  // Field 4 - xLights Address
						row.Add(universe.Active.ToString());               // Field 5 - Active
						row.Add(universe.Name);                            // Field 6 - Name
						row.Add(""); // Field 7 - Type
						row.Add("");                                  // Field 8 - Color
						row.Add(universe.Location);                        // Field 9 - Location
						row.Add(universe.Comment);                         // Field 10 - Comment
						writer.WriteRow(row);

						for (int ct = 0; ct < universe.DMXControllers.Count; ct++)
						{
							DMXController controller = universe.DMXControllers[ct];
							ctlName = controller.Name;
							try
							{
								row = new CsvRow();
								row.Add(universe.UniverseNumber.ToString());       // Field 0 - Universe
								row.Add(controller.Identifier);                        // Field 1 - Controller
								row.Add("");                                  // Field 2 - LOR4Output
								row.Add(controller.DMXStartAddress.ToString());      // Field 3 - DMX Address
								row.Add(controller.xLightsAddress.ToString());  // Field 4 - xLights Address
								row.Add(controller.Active.ToString());               // Field 5 - Active
								row.Add(controller.Name);                            // Field 6 - Name
								row.Add("");                                  // Field 7 - Type
								row.Add("");                                  // Field 8 - Color
								row.Add(controller.Location);                        // Field 9 - Location
								row.Add(controller.Comment);                         // Field 10 - Comment
								writer.WriteRow(row);

								for (int cl = 0; cl < controller.DMXChannels.Count; cl++)
								{
									DMXChannel channel = controller.DMXChannels[cl];
									chnName = channel.Name;
									try
									{
										row = new CsvRow();
										row.Add(channel.UniverseNumber.ToString());  // Field 0 - Universe
										row.Add(channel.DMXController.Identifier);     // Field 1 - Controller
										row.Add(channel.OutputNum.ToString());       // Field 2 - LOR4Output
										row.Add(channel.DMXAddress.ToString());      // Field 3 - DMX Address
										row.Add(channel.xLightsAddress.ToString());  // Field 4 - xLights Address
										row.Add(channel.Active.ToString());          // Field 5 - Active
										row.Add(channel.Name);                       // Field 6 - Name
										row.Add(channel.DeviceType.Name);              // Field 7 - Type
										row.Add(LOR4.LOR4Admin.ColorToHex(channel.Color));    // Field 8 - Color
										row.Add(channel.Location);                   // Field 9 - Location
										row.Add(channel.Comment);                    // Field 10 - comment
										writer.WriteRow(row);
									} // End Channel Try
									catch (Exception ex)
									{

									} // End Channel Catch
								} // End Channel LOR4Loop
							} // End Controller Try
							catch (Exception ex)
							{

							} // End Controller Catch
						} // End Controller LOR4Loop
					} // End Universe Try
					catch (Exception ex)
					{

					} // End Universe Catch
				} // End Universe LOR4Loop
				writer.Close();
				Fyle.LaunchFile(fileName);
			} // End Writer Try
			catch (Exception ex)
			{
				if (Fyle.DebugMode)
				{
					Fyle.BUG("ExportSpreadsheet", ex);
				}
			} // end Writer Catch

			return errs;
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			ExportToSpreadsheet(dbPath, true);
		}

		private void treeChannels_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (treeChannels.SelectedNode != null)
			{
				if (e.KeyChar == (char)Keys.Delete)
				{
					// Note: non-success does not mean the code failed, it's pro'ly cuz the user said 'No' to delete
					bool deleted = DeleteNode(treeChannels.SelectedNode);
				}
				if ((e.KeyChar == (char)Keys.Enter) || (e.KeyChar == (char)Keys.Return))
				{
					treeChannelList_DoubleClick(sender, e);
				}
				if ((e.KeyChar == 99) || (e.KeyChar == (char)Keys.Space))
				{
					IDMXThingy d = (IDMXThingy)treeChannels.SelectedNode.Tag;
					string theName = d.Name;
					Clipboard.SetText(theName);
					Fyle.MakeNoise(Fyle.Noises.Pop);
				}
			}
		}

		private bool DeleteNode(TreeNodeAdv node)
		{
			// Note: non-success does not mean the code failed, it's pro'ly cuz the user said 'No' to delete
			bool success = false;
			string msg = "";

			if (node.Tag != null)
			{
				if (node.Tag.GetType() == typeof(DMXChannel))
				{
					DMXChannel channel = (DMXChannel)node.Tag;
					msg = "Are you sure you want to delete channel ";
					msg += channel.OutputNum + ": " + channel.Name + "?";
					DialogResult dr = MessageBox.Show(this, msg, "Delete LOR4Channel?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					if (dr == DialogResult.Yes)
					{
						node.Parent.Nodes.Remove(node);
						AllChannels.Remove(channel);
						channel.DMXController.DMXChannels.Remove(channel);
						MakeDirty(true);
						success = true;
					}
				}
				else
				{
					if (node.Tag.GetType() == typeof(DMXController))
					{
						DMXController controller = (DMXController)node.Tag;
						msg = "Are you sure you want to delete controller ";
						if (controller.Identifier.Length > 0)
						{
							msg += controller.Identifier + ": ";
						}
						msg += controller.Name + " which has ";
						msg += controller.DMXChannels.Count.ToString() + " channels?";
						DialogResult dr = MessageBox.Show(this, msg, "Delete Controller?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						if (dr == DialogResult.Yes)
						{
							for (int c = 0; c < controller.DMXChannels.Count; c++)
							{
								DMXChannel channel = controller.DMXChannels[c];
								AllChannels.Remove(channel);
							}
							node.Parent.Nodes.Remove(node);
							AllControllers.Remove(controller);
							controller.DMXUniverse.DMXControllers.Remove(controller);
							MakeDirty(true);
							success = true;
						}
					}
					else
					{
						if (node.Tag.GetType() == typeof(DMXUniverse))
						{
							DMXUniverse universe = (DMXUniverse)node.Tag;
							msg = "Are you really sure you want to delete universe ";
							msg += universe.UniverseNumber + ": " + universe.Name + " which has ";
							msg += universe.DMXControllers.Count.ToString() + " controllers and ";
							int chanCount = 0;
							for (int c1 = 0; c1 < universe.DMXControllers.Count; c1++)
							{
								DMXController controller = universe.DMXControllers[c1];
								chanCount += controller.DMXChannels.Count;
							}
							msg += chanCount.ToString() + " channels?";
							DialogResult dr = MessageBox.Show(this, msg, "Delete Universe?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							if ((universe.DMXControllers.Count > 2) || (chanCount > 23))
							{
								msg = "That's a lot of controllers (" + universe.DMXControllers.Count.ToString() + ") and ";
								msg += " a lot of channels (" + chanCount.ToString() + ").";
								msg += "\r\n\r\nAre you really really sure?";
								dr = MessageBox.Show(this, msg, "Delete Universe?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
							}
							if (dr == DialogResult.Yes)
							{
								for (int c1 = 0; c1 < universe.DMXControllers.Count; c1++)
								{
									DMXController controller = universe.DMXControllers[c1];
									for (int c2 = 0; c2 < controller.DMXChannels.Count; c2++)
									{
										DMXChannel channel = controller.DMXChannels[c2];
										AllChannels.Remove(channel);
									} // End loop thru controller's channels
								} // End loop thru universe's controllers
								node.Parent.Nodes.Remove(node);
								AllUniverses.Remove(universe);
								MakeDirty(true);
								success = true;
							} // End if user said Yes
						} // End if selected node was a Universe
					} // End if selected node was a controller, or not
				} // End if selected node was a channel, or not
			} // End if node's .Tag was not null
				// Note: non-success does not mean the code failed, it's pro'ly cuz the user said 'No' to delete
			return success;
		}

		private void treeChannels_KeyUp(object sender, KeyEventArgs e)
		{
			if (treeChannels.SelectedNode != null)
			{
				if (e.KeyCode == Keys.Delete)
				{
					// Note: non-success does not mean the code failed, it's pro'ly cuz the user said 'No' to delete
					bool deleted = DeleteNode(treeChannels.SelectedNode);
				}
				else
				{
					if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
					{
						treeChannelList_DoubleClick(sender, e);
					}
				}
			}
		}

		public DMXDeviceType GetTypeByName(string theName)
		{
			string tn = theName.ToLower();
			DMXDeviceType ret = null;
			for (int i = 0; i < DeviceTypes.Count; i++)
			{
				if (tn == DeviceTypes[i].Name.ToLower())
				{
					ret = DeviceTypes[i];
					i = DeviceTypes.Count; // Force exit of loop
				}
			}
			return ret;
		}

		private int MatchUp(string seqFile, bool sortByName)
		{
			int ret = 0;
			int lorCN = 0;
			int vizCN = 0;
			int xCNM = 0;
			int xCNG = 0;
			LOR4Sequence lseq = new LOR4Sequence(seqFile);
			//xSequence xSeq = new xSequence(seqFile);
			xRGBEffects RGBEffects = new xRGBEffects();
			double score = 0;
			double bestScore = 0;
			int bestIndex = -1;
			LOR4MemberType bestType = LOR4MemberType.None;

			/////////////////////////////
			/// GENERATE REPORT DATA ///
			///////////////////////////

			// Resort by name
			//DMXChannel.SortByName = true;
			DMXChannel.SortByName = sortByName;
			AllChannels.Sort();
			int oldLORsort = LOR4Membership.sortMode;
			if (sortByName)
			{
				LOR4Membership.sortMode = LOR4Membership.SORTbyName;
			}
			else
			{
				LOR4Membership.sortMode = LOR4Membership.SORTbyOutput;
			}
			lseq.Channels.Sort();
			//xRGBEffects.SortByName = true;
			xRGBEffects.SortByName = sortByName;
			//List<FuzzyList> fuzzies = new List<FuzzyList>();
			lViz.VizChannels.Sort();

			string matchReportFile = "W:\\Documents\\SourceCode\\Windows\\UtilORama4\\ChannelORama\\!Temp!\\FuzzyReport.txt";
			StreamWriter matchWriter = new StreamWriter(matchReportFile);




			////////////////////////////////
			///  **  EXACT MATCHES  **  ///
			//////////////////////////////

			///////////////////////////////////////////////////////
			/// Match Managed Channels Exactly to LOR Channels ///
			/////////////////////////////////////////////////////
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				string mgrName = chanMgr.Name.ToLower();

				for (int l = lorCN; l < lseq.Channels.Count; l++)
				{
					LOR4Channel chanLOR = lseq.Channels[l];

					if (mgrName.CompareTo(chanLOR.Name.ToLower()) == 0)
					{
						chanMgr.TagLOR = chanLOR;
						chanLOR.Tag = chanMgr;
						chanMgr.ExactLOR = true;
						chanLOR.ExactMatch = true;
						lorCN = l + 1;
						l = lseq.Channels.Count; // Force exit of loop
					}
				} // LOR channel LOR4Loop

				///////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to Viz Channels ///
				/////////////////////////////////////////////////////
				for (int v = vizCN; v < lViz.VizChannels.Count; v++)
				{
					LOR4VizChannel chanViz = lViz.VizChannels[v];
					if (mgrName.CompareTo(chanViz.Name.ToLower()) == 0)
					{
						chanMgr.TagViz = chanViz;
						chanViz.Tag = chanMgr;
						chanMgr.ExactViz = true;
						chanViz.ExactMatch = true;
						vizCN = v + 1;
						v = lViz.VizChannels.Count; // Exit loop
					}
				} // VizChannel Loop

				////////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to VizItemGroups ///
				//////////////////////////////////////////////////////
				for (int v = vizCN; v < lViz.VizItemGroups.Count; v++)
				{
					LOR4VizItemGroup grpViz = lViz.VizItemGroups[v];
					if (mgrName.CompareTo(grpViz.Name.ToLower()) == 0)
					{
						chanMgr.TagViz = grpViz;
						grpViz.Tag = chanMgr;
						chanMgr.ExactViz = true;
						grpViz.ExactMatch = true;
						vizCN = v + 1;
						v = lViz.VizItemGroups.Count; // Exit loop
					}
				} // VizItemGroups Loop

				/////////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to VizDrawObjects ///
				///////////////////////////////////////////////////////
				for (int v = vizCN; v < lViz.VizDrawObjects.Count; v++)
				{
					LOR4VizDrawObject drobViz = lViz.VizDrawObjects[v];
					if (mgrName.CompareTo(drobViz.Name.ToLower()) == 0)
					{
						chanMgr.TagViz = drobViz;
						drobViz.Tag = chanMgr;
						chanMgr.ExactViz = true;
						drobViz.ExactMatch = true;
						vizCN = v + 1;
						v = lViz.VizDrawObjects.Count; // Exit loop
					}
				} // VizDrawObjects Loop

				//////////////////////////////////////////////////
				/// Match Managed Channels Exactly to xModels ///
				////////////////////////////////////////////////
				for (int x = xCNM; x < RGBEffects.Models.Count; x++)
				{
					xModel chanx = RGBEffects.Models[x];

					if (mgrName.CompareTo(chanx.Name.ToLower()) == 0)
					{
						chanMgr.TagX = chanx;
						chanx.Tag = chanMgr;
						chanMgr.ExactX = true;
						chanx.ExactMatch = true;
						xCNM = x + 1;
						x = RGBEffects.Models.Count; // Force exit of loop
					}
				} // RGBEffectss LOR4Loop

				///////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to xModelGroups ///
				/////////////////////////////////////////////////////
				for (int x = xCNG; x < RGBEffects.ModelGroups.Count; x++)
				{
					xModelGroup groupx = RGBEffects.ModelGroups[x];

					if (mgrName.CompareTo(groupx.Name.ToLower()) == 0)
					{
						chanMgr.TagX = groupx;
						groupx.Tag = chanMgr;
						chanMgr.ExactX = true;
						groupx.ExactMatch = true;
						xCNG = x + 1;
						x = RGBEffects.ModelGroups.Count; // Force exit of loop
					}
				} // End RGBEffectsGroups LOR4Loop
			} // End Channel Manager DMX Channels loop looking for Exact Matches

			////////////////////////////////
			///  **  FUZZY MATCHES  **  ///
			//////////////////////////////

			/////////////////////////////////////////////////////
			/// Fuzzy Match Managed Channels to LOR Channels ///
			///////////////////////////////////////////////////
			// Now, let's do it again looking for fuzzy matches
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				string mgrName = chanMgr.Name.ToLower();

				if (chanMgr.TagLOR == null)
				{
					bestScore = 0;
					bestIndex = -1;
					for (int l = 0; l < lseq.Channels.Count; l++)
					{
						LOR4Channel chanLOR = lseq.Channels[l];
						if (chanLOR.Tag == null)
						{
							score = mgrName.FuzzyScoreFast(chanLOR.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanLOR.Name);
							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							//if (score > .30D)
							{
								score = mgrName.FuzzyScoreAccurate(chanLOR.Name.ToLower());
								if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								//if (score > .40D)
								{
									if (score > bestScore)
									{
										bestScore = score;
										bestIndex = l;
									}

								}
							}
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels
					if (bestIndex >= 0)
					{
						chanMgr.Tag = lseq.Channels[bestIndex];
						lseq.Channels[bestIndex].Tag = chanMgr;

					}
				}

				/////////////////////////////////////////////////////
				/// Fuzzy Match Managed Channels to Viz Channels ///
				///////////////////////////////////////////////////
				if (chanMgr.TagViz == null)
				{
					bestScore = 0;
					bestIndex = -1;
					for (int v = 0; v < lViz.VizChannels.Count; v++)
					{
						LOR4VizChannel chanViz = lViz.VizChannels[v];
						if (chanViz.Tag == null)
						{
							score = mgrName.FuzzyScoreFast(chanViz.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanViz.Name);
							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							//if (score > .30D)
							{
								score = mgrName.FuzzyScoreAccurate(chanViz.Name.ToLower());
								if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								//if (score > .40D)
								{
									if (score > bestScore)
									{
										bestScore = score;
										bestIndex = v;
										bestType = LOR4MemberType.VizChannel;
									}
								}
							}
						} // current Viz channel has no tag and thus no exact matches
					} // End loop thru Viz channels

					for (int v = 0; v < lViz.VizDrawObjects.Count; v++)
					{
						LOR4VizDrawObject chanViz = lViz.VizDrawObjects[v];
						if (chanViz.Tag == null)
						{
							score = mgrName.FuzzyScoreFast(chanViz.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanViz.Name);
							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							//if (score > .30D)
							{
								score = mgrName.FuzzyScoreAccurate(chanViz.Name.ToLower());
								if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								//if (score > .40D)
								{
									if (score > bestScore)
									{
										bestScore = score;
										bestIndex = v;
										bestType = LOR4MemberType.VizDrawObject;
									}
								}
							}
						} // current Viz channel has no tag and thus no exact matches
					} // End loop thru Viz channels

					//! What about VizItemGroups??

					if (bestIndex >= 0)
					{
						if (bestType == LOR4MemberType.VizChannel)
						{
							chanMgr.TagViz = lViz.VizChannels[bestIndex];
							lViz.VizChannels[bestIndex].Tag = chanMgr;
						}
						if (bestType == LOR4MemberType.VizDrawObject)
						{
							chanMgr.TagViz = lViz.VizChannels[bestIndex];
							lViz.VizChannels[bestIndex].Tag = chanMgr;
						}
					}
				}

				////////////////////////////////////////////////
				/// Fuzzy Match Managed Channels to xModels ///
				//////////////////////////////////////////////
				if (chanMgr.TagViz == null)
				{
					bestScore = 0;
					bestIndex = -1;
					int attemptCount = 0;
					for (int x = 0; x < RGBEffects.Models.Count; x++)
					{
						xModel chanx = RGBEffects.Models[x];
						if (chanx.Tag == null)
						{
							attemptCount++;
							//double score = mgrName.YetiLevenshteinSimilarity(chanx.Name.ToLower());
							score = mgrName.FuzzyScoreFast(chanx.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanx.Name);
							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								// Hey! We Got Something!!
								if (isWiz)
								{
									//System.Diagnostics.Debugger.Break();
								}
							}

							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							{
								//score = mgrName.RankEquality(chanx.Name.ToLower());
								score = mgrName.FuzzyScoreAccurate(chanx.Name.ToLower());
								if (score > bestScore)
								{
									bestScore = score;
									bestIndex = x;
									bestType = LOR4MemberType.Channel;
								}
							}
							int nn = attemptCount;
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels

					/////////////////////////////////////////////////////////
					/// Fuzzy Match Managed Channels to xModelGroups ///
					///////////////////////////////////////////////////////
					for (int x = 0; x < RGBEffects.ModelGroups.Count; x++)
					{
						xModelGroup groupx = RGBEffects.ModelGroups[x];
						if (groupx.Tag == null)
						{
							score = mgrName.FuzzyScoreFast(groupx.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, groupx.Name);
							if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							//if (score > .30D)
							{
								score = mgrName.FuzzyScoreAccurate(groupx.Name.ToLower());
								if (score > bestScore)
								//if (score > .40D)
								{
									bestScore = score;
									bestIndex = x;
									bestType = LOR4MemberType.ChannelGroup;
								}
							}
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels

					//! What about xRGBModels and xPixelModels?

					if (bestIndex >= 0)
					{
						if (bestType == LOR4MemberType.Channel)
						{
							chanMgr.TagX = RGBEffects.Models[bestIndex];
							RGBEffects.Models[bestIndex].Tag = chanMgr;
						}
						if (bestType == LOR4MemberType.ChannelGroup)
						{
							chanMgr.TagX = RGBEffects.ModelGroups[bestIndex];
							RGBEffects.ModelGroups[bestIndex].Tag = chanMgr;
						}
					}

				} // current chanMgr has no tag and thus no exact match
			} // End second loop[ thru Channel Manager DMXChannels looking for fuzzy matches


			matchWriter.Close();
			Fyle.LaunchFile(matchReportFile);



			///////////////////////
			///  WRITE REPORT  ///
			/////////////////////

			string reportFile = dbPath + "MatchReport";
			if (sortByName)
			{
				reportFile += "_ByName.txt";
			}
			else
			{
				reportFile += "_ByAddress.txt";
			}
			StreamWriter writer = new StreamWriter(reportFile);
			writer.WriteLine("*** Channel Manager Report ***");
			writer.WriteLine("[Exact Matches to LOR Channels]");

			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagLOR != null)
				{
					Type t = chanMgr.TagLOR.GetType();
					if (t != typeof(LOR4Channel))
					{
						string xxxx = "Why Not!";
					}

					LOR4Channel chanLor = (LOR4Channel)chanMgr.TagLOR;
					if (chanLor.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Exact: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanLor.Name);
						writer.WriteLine(lineOut);
						if (chanMgr.UniverseNumber != chanLor.UniverseNumber || chanMgr.DMXAddress != chanLor.DMXAddress)
						{
							lineOut.Clear();
							lineOut.Append("    But the DMX Address does not match! ");
							lineOut.Append(chanMgr.UniverseNumber.ToString());
							lineOut.Append("/");
							lineOut.Append(chanMgr.DMXAddress.ToString());
							lineOut.Append(" != ");
							lineOut.Append(chanLor.UniverseNumber.ToString());
							lineOut.Append("/");
							lineOut.Append(chanLor.DMXAddress.ToString());
							writer.WriteLine(lineOut);
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Fuzzy Matches to LOR Channels]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagLOR != null)
				{
					LOR4Channel chanLor = (LOR4Channel)chanMgr.TagLOR;
					if (!chanLor.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Fuzzy: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanLor.Name);
						writer.WriteLine(lineOut);
						if (chanMgr.UniverseNumber != chanLor.UniverseNumber || chanMgr.DMXAddress != chanLor.DMXAddress)
						{
							lineOut.Clear();
							lineOut.Append("    But the DMX Address does not match! ");
							lineOut.Append(chanMgr.UniverseNumber.ToString());
							lineOut.Append("/");
							lineOut.Append(chanMgr.DMXAddress.ToString());
							lineOut.Append(" != ");
							lineOut.Append(chanLor.UniverseNumber.ToString());
							lineOut.Append("/");
							lineOut.Append(chanLor.DMXAddress.ToString());
							writer.WriteLine(lineOut);
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Managed Channels with NO LOR Match]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagLOR == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch: ");
					lineOut.Append(chanMgr.Name);
					writer.WriteLine(lineOut);
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[LOR Channels with NO Managed Match]");
			for (int c = 0; c < lseq.Channels.Count; c++)
			{
				LOR4Channel chanLOR = lseq.Channels[c];
				if (chanLOR.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch: ");
					lineOut.Append(chanLOR.Name);
					writer.WriteLine(lineOut);
				}
			}




			writer.WriteLine("");
			writer.WriteLine("");
			writer.WriteLine("[Exact Matches to Viz Channels or Groups]");

			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagViz != null)
				{
					iLOR4Member chanViz = (iLOR4Member)chanMgr.TagViz;
					if (chanViz.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Exact: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanViz.Name);
						writer.WriteLine(lineOut);
						if (chanViz.DMXAddress > 0)
						{
							if ((chanMgr.DMXAddress != chanViz.DMXAddress) || (chanMgr.UniverseNumber != chanViz.UniverseNumber))
							{
								lineOut.Clear();
								lineOut.Append("    But the Viz Channel Address does not match! ");
								lineOut.Append(chanMgr.UniverseNumber.ToString());
								lineOut.Append("/");
								lineOut.Append(chanMgr.DMXAddress.ToString());
								lineOut.Append(" != ");
								lineOut.Append(chanViz.UniverseNumber.ToString());
								lineOut.Append("/");
								lineOut.Append(chanViz.DMXAddress.ToString());
								writer.WriteLine(lineOut);
							}
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Fuzzy Matches to Viz Channels or Groups]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagViz != null)
				{
					iLOR4Member chanViz = (iLOR4Member)chanMgr.TagViz;
					if (!chanViz.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Fuzzy: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanViz.Name);
						writer.WriteLine(lineOut);
						if (chanViz.DMXAddress > 0)
						{
							if ((chanMgr.DMXAddress != chanViz.DMXAddress) || (chanMgr.UniverseNumber != chanViz.UniverseNumber))
							{
								lineOut.Clear();
								lineOut.Append("    But the Viz Channel Address does not match! ");
								lineOut.Append(chanMgr.UniverseNumber.ToString());
								lineOut.Append("/");
								lineOut.Append(chanMgr.DMXAddress.ToString());
								lineOut.Append(" != ");
								lineOut.Append(chanViz.UniverseNumber.ToString());
								lineOut.Append("/");
								lineOut.Append(chanViz.DMXAddress.ToString());
								writer.WriteLine(lineOut);
							}
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Managed Channels with NO Viz Channel or Group Match]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagViz == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch: ");
					lineOut.Append(chanMgr.Name);
					writer.WriteLine(lineOut);
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Viz Channels and Groups with NO Managed Match]");
			for (int v = 0; v < lViz.VizChannels.Count; v++)
			{
				iLOR4Member chanViz = lViz.VizChannels[v];
				if (chanViz.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch Channel: ");
					lineOut.Append(chanViz.Name);
					writer.WriteLine(lineOut);
				}
			}
			for (int v = 0; v < lViz.VizItemGroups.Count; v++)
			{
				iLOR4Member chanViz = lViz.VizItemGroups[v];
				if (chanViz.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch Group: ");
					lineOut.Append(chanViz.Name);
					writer.WriteLine(lineOut);
				}
			}
			for (int v = 0; v < lViz.VizDrawObjects.Count; v++)
			{
				iLOR4Member chanViz = lViz.VizDrawObjects[v];
				if (chanViz.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch DrOb: ");
					lineOut.Append(chanViz.Name);
					writer.WriteLine(lineOut);
				}
			}












			writer.WriteLine("");
			writer.WriteLine("");
			writer.WriteLine("[Exact Matches to xLights Models or Groups]");

			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagX != null)
				{
					ixMember chanx = (ixMember)chanMgr.TagX;
					if (chanx.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Exact: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanx.Name);
						writer.WriteLine(lineOut);
						if (chanx.xLightsAddress > 0)
						{
							if (chanMgr.xLightsAddress != chanx.xLightsAddress)
							{
								lineOut.Clear();
								lineOut.Append("    But the xLights Channel Address does not match! ");
								lineOut.Append(chanMgr.xLightsAddress.ToString());
								lineOut.Append(" != ");
								lineOut.Append(chanx.xLightsAddress.ToString());
								writer.WriteLine(lineOut);
							}
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Fuzzy Matches to xLights Models or Groups]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagX != null)
				{
					ixMember chanx = (ixMember)chanMgr.TagX;
					if (!chanx.ExactMatch)
					{
						StringBuilder lineOut = new StringBuilder("  Fuzzy: ");
						lineOut.Append(chanMgr.Name);
						lineOut.Append(" to ");
						lineOut.Append(chanx.Name);
						writer.WriteLine(lineOut);
						if (chanx.xLightsAddress > 0)
						{
							if (chanMgr.xLightsAddress != chanx.xLightsAddress)
							{
								lineOut.Clear();
								lineOut.Append("    But the xLights Channel Address does not match! ");
								lineOut.Append(chanMgr.xLightsAddress.ToString());
								lineOut.Append(" != ");
								lineOut.Append(chanx.xLightsAddress.ToString());
								writer.WriteLine(lineOut);
							}
						}
					}
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[Managed Channels with NO xLights Match]");
			for (int m = 0; m < AllChannels.Count; m++)
			{
				DMXChannel chanMgr = AllChannels[m];
				if (chanMgr.TagX == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch Channel: ");
					lineOut.Append(chanMgr.Name);
					writer.WriteLine(lineOut);
				}
			}

			writer.WriteLine("");
			writer.WriteLine("[xLights Models and Groups with NO Managed Match]");
			for (int x = 0; x < RGBEffects.Models.Count; x++)
			{
				xModel chanx = RGBEffects.Models[x];
				if (chanx.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch Group: ");
					lineOut.Append(chanx.Name);
					writer.WriteLine(lineOut);
				}
			}
			for (int x = 0; x < RGBEffects.ModelGroups.Count; x++)
			{
				xModelGroup chanx = RGBEffects.ModelGroups[x];
				if (chanx.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch: ");
					lineOut.Append(chanx.Name);
					writer.WriteLine(lineOut);
				}
			}

			writer.WriteLine("");
			writer.WriteLine("* ( End of Report ) *");
			writer.Close();



			// Resort back to default by channel number
			DMXChannel.SortByName = false;
			AllChannels.Sort();
			LOR4Membership.sortMode = oldLORsort;
			lseq.Channels.Sort();
			xRGBEffects.SortByName = false;
			RGBEffects.Models.Sort();
			RGBEffects.ModelGroups.Sort();

			Fyle.LaunchFile(reportFile);


			return ret;
		}

		/*
		private int FuzzyFindInList(string findName, List<iLOR4Member> members, ref double score)
		{
			int idx = -1;
			double s = 0;
			double bestScore = 0;

			for (int i = 0; i < members.Count; i++)
			{
				iLOR4Member member = members[i];
				if (member.Tag == null)
				{
					s = findName.FuzzyScoreFast(member.Name.ToLower());
					//ReportMatch(matchWriter, s, findName, member.Name);
					if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
					{
						s = findName.FuzzyScoreAccurate(member.Name.ToLower());
						if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
						{
							if (s > score)
							{
								score = s;
								idx = 1;
							} // if this is the best score yet
						} // if final score passes threshold
					} // if prematch score passes threshold
				} // current member has no tag and thus no exact matches
			} // Loop thru member list

			return idx;
		}
		*/



		private void ReportMatch(StreamWriter writer, double score, string name1, string name2)
		{
			StringBuilder sb = new StringBuilder();
			string scr = score.ToString("0.0000");
			sb.Append(scr);
			sb.Append(" = ");
			string n1 = name1.PadRight(33);
			sb.Append(n1);
			sb.Append(" --> ");
			sb.Append(name2.ToLower());
			writer.WriteLine(sb);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveData(dbPath);
			//Application.DoEvents();
			// Clear and Reload
			//TODO: Shouldn't need this, but tree is not refreshing correctly
			//TODO: Fix Tree Refresh after adds, deletes, edits, moves, etc.
			//ClearData();
			//Application.DoEvents();
			//LoadData(dbPath);
			//Application.DoEvents();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{
				DMXChannel killChannel = (DMXChannel)node.Tag;
				RemoveChannel(killChannel);
			}
		}

		private bool RemoveChannel(DMXChannel channel)
		{
			// Returns True if channel successfully removed
			bool success = false;
			DMXController controller = channel.DMXController;
			int foundAt = -1;
			for (int i = 0; i < controller.DMXChannels.Count; i++)
			{
				if (controller.DMXChannels[i].ID == channel.ID)
				{
					controller.DMXChannels.RemoveAt(i);
					foundAt = i;
					i = controller.DMXChannels.Count; // Break out of loop
				}
			}
			if (foundAt >= 0)
			{
				TreeNodeAdv chNode = (TreeNodeAdv)channel.Tag;
				chNode.Remove();
				if (AllChannels.Contains(channel))
				{
					AllChannels.Remove(channel);
				}
				UpdateStatus();
				success = true;
			}
			return success;
		}

		private bool RemoveController(DMXController controller)
		{
			// Returns True if controller successfully removed
			// Important: Does NOT ask the user for confirmation, so be sure to call this from a method that does (like DeleteNode)
			bool success = false;
			DMXUniverse universe = controller.DMXUniverse;
			int foundAt = -1;
			for (int i = 0; i < universe.DMXControllers.Count; i++)
			{
				if (universe.DMXControllers[i].ID == controller.ID)
				{
					universe.DMXControllers.RemoveAt(i);
					foundAt = i;
					i = universe.DMXControllers.Count; // Break out of loop
				}
			}
			if (foundAt >= 0)
			{
				TreeNodeAdv ctrlNode = (TreeNodeAdv)controller.Tag;
				ctrlNode.Remove();
				if (AllControllers.Contains(controller))
				{
					AllControllers.Remove(controller);
				}
				UpdateStatus();
				success = true;
			}
			return success;
		}

		private bool RemoveUniverse(DMXUniverse universe)
		{
			// Returns True if universe successfully removed
			// Important: Does NOT ask the user for confirmation, so be sure to call this from a method that does (like DeleteNode)
			bool success = false;
			int foundAt = -1;
			for (int i = 0; i < AllUniverses.Count; i++)
			{
				if (AllUniverses[i].ID == universe.ID)
				{
					AllUniverses.RemoveAt(i);
					foundAt = i;
					i = AllUniverses.Count; // Break out of loop
				}
			}
			if (foundAt >= 0)
			{
				TreeNodeAdv uniNode = (TreeNodeAdv)universe.Tag;
				uniNode.Remove();
				if (AllUniverses.Contains(universe))
				{
					AllUniverses.Remove(universe);
				}
				UpdateStatus();
				success = true;
			}
			return success;
		}
		private void changeDatabaseLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectDBPath();
		}

		private void dropModeToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			mnuSettings.Show(btnSettings, new Point(0, btnSettings.Height));
		}
	}

	public class FuzzyList : IComparable<FuzzyList>
	{
		public double Score = 0;
		public object Item1 = null;
		public object Item2 = null;


		public FuzzyList(object firstItem, object secondItem, double fuzzyScore)
		{
			Item1 = firstItem;
			Item2 = secondItem;
			Score = fuzzyScore;
		}

		public int CompareTo(FuzzyList otherItem)
		{
			// Note: Compare other item to this item (instead of normal this item to other) to get a REVERSE sort with highest scores first
			return otherItem.Score.CompareTo(Score);
		}
	}
}

