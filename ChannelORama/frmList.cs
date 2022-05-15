using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using ReadWriteCsv;
using LOR4Utils;
using FileHelper;
using xUtils;
//using FuzzyString;
using Syncfusion.Windows.Forms.Tools;

//using xUtils;

namespace UtilORama4
{
	public partial class frmList : Form
	{
		private static Properties.Settings heartOfTheSun = Properties.Settings.Default;
		private const string helpPage = "http://wizlights.com/utilorama/channelorama";
		private List<DMXUniverse> universes = new List<DMXUniverse>();

		//private List<DMXChannel> AllChannels = new List<DMXChannel>();
		// Just creating a convenient reference to the static list in the DMXUniverse class
		private List<DMXChannel> AllChannels = DMXUniverse.AllChannels;

		//public List<DMXDevice> deviceTypes = new List<DMXDevice>();
		// Just creating a convenient reference to the static list in the DMXChannel class
		public List<DMXDeviceType> deviceTypes = DMXChannel.DeviceTypes;
		private const string myTitle = "Chan-O-Rama  Channel Manager";

		private string lastFile = "";
		private bool formShown = false;
		private string dbPath = "";
		private int year = 2021;
		private int lastID = -1;
		public bool dirty = false;
		private bool isWiz = Fyle.IsWizard || Fyle.IsAWizard;

		private xRGBEffects xSeq = null;
		private LORVisualization4 lViz = null;

		private int[] TREEICONuniverse = { 0 };
		private int[] TREEICONcontroller = { 1 };
		private int[] TREEICONtrack = { 2 };
		private int[] TREEICONchannel = { 3 }; // Generic, non colored (dark cyan with "Ch" on it)
		private int[] TREEICONrgbChannel = { 4 }; // #7 below looks better
		private int[] TREEICONchannelGroup = { 5 };
		private int[] TREEICONcosmic = { 6 };
		private int[] TREEICONrgbColor = { 7 };
		private int[] TREEICONmulticolor = { 8 };
		private int[] TREEICONred = { 9 };
		private int[] TREEICONgreen = { 10 };
		private int[] TREEICONblue = { 11 };



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


			xSeq = new xRGBEffects();
			lViz = new LORVisualization4(null, vizFile);

			int ccc = lViz.VizChannels.Count;
			int ddd = lViz.VizDrawObjects.Count;
			int ggg = lViz.VizItemGroups.Count;


			//MatchUp(seqFile);
			MatchUp(seqFile, true);

		}


		private void frmList_Load(object sender, EventArgs e)
		{
			RestoreFormPosition();
			GetTheControlsFromTheHeartOfTheSun();
		}


		private void SaveFormPosition()
		{
			// Get current location, size, and state
			Point myLoc = this.Location;
			Size mySize = this.Size;
			FormWindowState myState = this.WindowState;
			// if minimized or maximized
			if (myState != FormWindowState.Normal)
			{
				// override with the restore location and size
				myLoc = new Point(this.RestoreBounds.X, this.RestoreBounds.Y);
				mySize = new Size(this.RestoreBounds.Width, this.RestoreBounds.Height);
			}

			// Save it for later!
			int x = this.Left;
			heartOfTheSun.Location = myLoc;
			heartOfTheSun.Size = mySize;
			heartOfTheSun.WindowState = (int)myState;
			heartOfTheSun.Save();
		} // End SaveFormPostion

		private void RestoreFormPosition()
		{
			// Multi-Monitor aware
			// AND NOW with overlooked support for fixed borders!
			// with bounds checking
			// repositions as necessary
			// should(?) be able to handle an additional screen that is no longer there,
			// a repositioned taskbar or gadgets bar,
			// or a resolution change.

			// Note: If the saved position spans more than one screen
			// the form will be repositioned to fit all within the
			// screen containing the center point of the form.
			// Thus, when restoring the position, it will no longer
			// span monitors.
			// This is by design!
			// Alternative 1: Position it entirely in the screen containing
			// the top left corner

			Point savedLoc = heartOfTheSun.Location;
			Size savedSize = heartOfTheSun.Size;
			if (this.FormBorderStyle != FormBorderStyle.Sizable)
			{
				savedSize = new Size(this.Width, this.Height);
				this.MinimumSize = this.Size;
				this.MaximumSize = this.Size;
			}
			FormWindowState savedState = (FormWindowState)heartOfTheSun.WindowState;
			int x = savedLoc.X; // Default to saved postion and size, will override if necessary
			int y = savedLoc.Y;
			int w = savedSize.Width;
			int h = savedSize.Height;
			Point center = new Point(x + w / w, y + h / 2); // Find center point
			int onScreen = 0; // Default to primary screen if not found on screen 2+
			Screen screen = Screen.AllScreens[0];

			// Find which screen it is on
			for (int si = 0; si < Screen.AllScreens.Length; si++)
			{
				// Alternative 1: Change "Contains(center)" to "Contains(savedLoc)"
				if (Screen.AllScreens[si].WorkingArea.Contains(center))
				{
					screen = Screen.AllScreens[si];
					onScreen = si;
				}
			}
			Rectangle bounds = screen.WorkingArea;
			// Alternate 2:
			//Rectangle bounds = Screen.GetWorkingArea(center);

			// Test Horizontal Positioning, correct if necessary
			if (this.MinimumSize.Width > bounds.Width)
			{
				// Houston, we have a problem, monitor is too narrow
				System.Diagnostics.Debugger.Break();
				w = this.MinimumSize.Width;
				// Center it horizontally over the working area...
				//x = (bounds.Width - w) / 2 + bounds.Left;
				// OR position it on left edge
				x = bounds.Left;
			}
			else
			{
				// Should fit horizontally
				// Is it too far left?
				if (x < bounds.Left) x = bounds.Left; // Move over
																							// Is it too wide?
				if (w > bounds.Width) w = bounds.Width; // Shrink it
																								// Is it too far right?
				if ((x + w) > bounds.Right)
				{
					// Keep width, move it over
					x = (bounds.Width - w) + bounds.Left;
				}
			}

			// Test Vertical Positioning, correct if necessary
			if (this.MinimumSize.Height > bounds.Height)
			{
				// Houston, we have a problem, monitor is too short
				System.Diagnostics.Debugger.Break();
				h = this.MinimumSize.Height;
				// Center it vertically over the working area...
				//y = (bounds.Height - h) / 2 + bounds.Top;
				// OR position at the top edge
				y = bounds.Top;
			}
			else
			{
				// Should fit vertically
				// Is it too high?
				if (y < bounds.Top) y = bounds.Top; // Move it down
																						// Is it too tall;
				if (h > bounds.Height) h = bounds.Height; // Shorten it
																									// Is it too low?
				if ((y + h) > bounds.Bottom)
				{
					// Kepp height, raise it up
					y = (bounds.Height - h) + bounds.Top;
				}
			}

			// Position and Size should be safe!
			// Move and Resize the form
			this.SetDesktopLocation(x, y);
			this.Size = new Size(w, h);

			// Window State
			if (savedState == FormWindowState.Maximized)
			{
				if (this.MaximizeBox)
				{
					// Optional.  Personally, I think it should always be reloaded non-maximized.
					//this.WindowState = savedState;
				}
			}
			if (savedState == FormWindowState.Minimized)
			{
				if (this.MinimizeBox)
				{
					// Don't think it's right to reload to a minimized state (confuses the user),
					// but you can enable this if you want.
					//this.WindowState = savedState;
				}
			}
		}

		private void SetTheControlsForTheHeartOfTheSun()
		{

		}

		private void GetTheControlsFromTheHeartOfTheSun()
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
		{
			System.Diagnostics.Process.Start(helpPage);

		}

		private void pnlAbout_Click(object sender, EventArgs e)
		{
			ImBusy(true);
			frmAbout aboutBox = new frmAbout();
			// aboutBox.setIcon = picAboutIcon.Image;
			aboutBox.Icon = this.Icon;
			aboutBox.picIcon.Image = picAboutIcon.Image;
			aboutBox.ShowDialog(this);
			ImBusy(false);

		}

		private string PathToDB(int year)
		{
			//string ret = Fyle.DefaultDocumentsPath;
			string ret = "X:";
			//ret += "Christmas\\";
			ret += year.ToString();
			ret += "\\Docs\\ChannelDB\\";
			return ret;
		}

		public int LoadData(string filePath)
		{
			int errs = LoadUniverses(filePath);
			errs += LoadControllers(filePath);
			errs += LoadDevices(filePath);
			errs += LoadChannels(filePath);
			return errs;
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

						int i = -1;
						int.TryParse(row[0], out i);   // Field 0 Universe Number
						universe.UniverseNumber = i;

						universe.Name = row[1];  // Field 1 Name
						uniName = universe.Name;  // For debugging exceptions
						universe.Location = row[2]; // Field 2 Location
						universe.Comment = row[3]; // Field 3 Comment

						bool b = true;
						bool.TryParse(row[4], out b); // Field 4 Active
						universe.Active = b;

						i = 512;
						int.TryParse(row[5], out i);   // Field 5 Size Limit
						universe.SizeLimit = i;

						i = 1;
						int.TryParse(row[6], out i);   // Field 6 xLights Start
						universe.xLightsAddress = i;

						universe.Connection = row[7];  // Field 7 Connection
						lastID++;
						universe.ID = lastID;
						universes.Add(universe);
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
				universes.Sort();
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
			int errs = 0;
			string ctlName = ""; // For debugging exceptions
			if (universes.Count > 0)
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
							int universe = 1;
							int.TryParse(row[0], out universe);   // Field 0 Universe Number

							controller.LetterID = row[1];  // Field 1 Letter ID
							controller.Name = row[2];  // Field 2 Name
							ctlName = controller.Name; // For debugging exceptions
							controller.Location = row[3]; // Field 3 Location
							controller.Comment = row[4]; // Field 4 Comment

							bool b = true;
							bool.TryParse(row[5], out b); // Field 5 Active
							controller.Active = b;

							controller.ControllerBrand = row[6];
							controller.ControllerModel = row[7];

							int i = 16;
							int.TryParse(row[8], out i);   // Field 8 Channel Count
							controller.OutputCount = i;

							i = 1;
							int.TryParse(row[9], out i);   // Field 9 DMX Start LOR4Channel
							controller.DMXStartAddress = i;

							i = 120;
							int.TryParse(row[10], out i);   // Field 10 voltage
							controller.Voltage = i;

							bool uniFound = false;
							for (int u = 0; u < universes.Count; u++)
							{
								if (universe == universes[u].UniverseNumber)
								{
									universes[u].DMXControllers.Add(controller);
									controller.DMXUniverse = universes[u];
									uniFound = true;
									u = universes.Count; // Exit loop
								}
							}
							if (!uniFound)
							{
								string msg = "Universe not found for controller:" + controller.Name;
								int qqqqq = 1;
							}
							lastID++;
							controller.ID = lastID;
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
					for (int u = 0; u < universes.Count; u++)
					{
						universes[u].DMXControllers.Sort();
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
			}
			return errs;
		}

		public int LoadChannels(string filePath)
		{
			int errs = 0;
			string chanName = ""; // for debugging exceptions
			if (universes.Count > 0)
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
							int uniNum = 1;
							int.TryParse(row[0], out uniNum); // Field 0 = Universe Number

							string ctlrLetter = row[1];       // Field 1 = Controller Letter

							int i = 1;
							int.TryParse(row[2], out i);
							channel.OutputNum = i;               // Field 2 = LOROutput4 Number

							channel.Name = row[3];                // Field 3 = Name
							chanName = channel.Name;              // For debugging exceptions
							channel.Location = row[4];            // Field 4 = Location
							channel.Comment = row[5];             // Field 5 = Comment

							bool b = true;
							bool.TryParse(row[6], out b);     // Field 6 = Active
							channel.Active = b;

							int devID = -1;
							int.TryParse(row[7], out devID);      // Field 7 = Channel Type



							//! ONE TIME FIX
							//if (devID == 0) devID = 22;





							if (devID >= 0 && devID < deviceTypes.Count)
							{
								//! Note: Devices should not yet be sorted by Display Order
								//! Should still be sorted by ID
								string dn = deviceTypes[devID].Name;
								channel.DeviceType = deviceTypes[devID];
							}

							string colhex = row[8];           // Field 8 = Color
							if (chanName.Substring(0, 5).CompareTo("Eaves") == 0)
							{
								if (isWiz)
								{
									string foo = colhex;
								}
							}
							//Color color = System.Drawing.ColorTranslator.FromHtml(colhex);
							Color color = LOR4Utils.lutils.HexToColor(colhex);
							channel.Color = color;

							AllChannels.Add(channel);
							bool ctlFound = false;
							for (int u = 0; u < universes.Count; u++)
							{
								DMXUniverse universe = universes[u];
								for (int c = 0; c < universe.DMXControllers.Count; c++)
								{
									if (ctlrLetter == universe.DMXControllers[c].LetterID)
									{
										channel.DMXController = universe.DMXControllers[c];
										universe.DMXControllers[c].DMXChannels.Add(channel);
										ctlFound = true;
										c = universe.DMXControllers.Count;
										u = universes.Count; // Exit loop
									}
								}
							}
							if (!ctlFound)
							{
								string msg = "Controller not found for channel " + channel.Name;
								int qqq5 = 5;
							}
							lastID++;
							channel.ID = lastID;
						}
						catch (Exception ex)
						{
							int ln = LOR4Utils.lutils.ExceptionLineNumber(ex);
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
					for (int u = 0; u < universes.Count; u++)
					{
						DMXUniverse universe = universes[u];
						for (int c = 0; c < universe.DMXControllers.Count; c++)
						{
							universe.DMXControllers[c].DMXChannels.Sort();
						}
					}
					// *NOW* we can sort the deviceTypes by display order
					deviceTypes.Sort();
				}
				catch (Exception ex)
				{
					int ln = LOR4Utils.lutils.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;

				}
			}
			return errs;
		}

		public int LoadDevices(string filePath)
		{
			int errs = 0;
			string devName = ""; // for debugging exceptions
			if (universes.Count > 0)
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
									int.TryParse(row[2], out ord); // Field 2 = LOROutput4 Number
								}
								DMXDeviceType deviceType = new DMXDeviceType(devName, devID, ord);
								deviceTypes.Add(deviceType);
							}
						}
						catch (Exception ex)
						{
							int ln = LOR4Utils.lutils.ExceptionLineNumber(ex);
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
					int ln = LOR4Utils.lutils.ExceptionLineNumber(ex);
					string msg = "Error " + ex.ToString() + " on line " + ln.ToString() + " while reading Channels file " + chnFile;
					if (isWiz)
					{
						DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					errs++;
				}
			}
			int dc = deviceTypes.Count;
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

		public int SaveUniverses(string filePath)
		{
			int errs = 0;
			string uniName = "";
			string uniFile = filePath + "Universes.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(uniFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe#"); // Field 0
				row.Add("Name"); // Field 1
				row.Add("Location"); // Field 2
				row.Add("Comment"); // Field 3
				row.Add("Active"); // Field 4
				row.Add("Size Limit"); // Field 5
				row.Add("xLights Start#"); // Field 6
				row.Add("Connection"); // Field 7
				writer.WriteRow(row);

				universes.Sort();
				for (int u = 0; u < universes.Count; u++)
				{
					try
					{
						DMXUniverse universe = universes[u];
						uniName = universe.Name;
						row = new CsvRow();

						row.Add(universe.UniverseNumber.ToString());  // Field 0
						row.Add(universe.Name);  // Field 1
						row.Add(universe.Location);  // Field 2
						row.Add(universe.Comment); // Field 3
						row.Add(universe.Active.ToString());  // Field 4
						row.Add(universe.SizeLimit.ToString());  // Field 5
						row.Add(universe.xLightsAddress.ToString()); // Field 6
						row.Add(universe.Connection);  // Field 7

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
			int errs = 0;
			string ctlName = ""; // For debugging exceptions
			string ctlFile = filePath + "Controllers.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(ctlFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe"); // Field 0
				row.Add("Controller"); // Field 1
				row.Add("Name"); // Field 2
				row.Add("Location"); // Field 3
				row.Add("Comment"); // Field 4
				row.Add("Active"); // Field 5
				row.Add("Brand"); // Field 6
				row.Add("Model"); // Field 7
				row.Add("LOR4Channel Count"); // Field 8
				row.Add("DMX Start"); // Field 9
				row.Add("Voltage"); // Field 10

				writer.WriteRow(row);

				for (int u = 0; u < universes.Count; u++)
				{
					DMXUniverse universe = universes[u];
					universe.DMXControllers.Sort();
					for (int c = 0; c < universe.DMXControllers.Count; c++)
					{
						try
						{
							DMXController controller = universe.DMXControllers[c];
							ctlName = controller.Name;
							row = new CsvRow();

							row.Add(controller.DMXUniverse.UniverseNumber.ToString()); // Field 0
							row.Add(controller.LetterID); // Field 1
							row.Add(controller.Name); // Field 2
							row.Add(controller.Location); // Field 3
							row.Add(controller.Comment); // Field 4
							row.Add(controller.Active.ToString()); // Field 5
							row.Add(controller.ControllerBrand); // field 6
							row.Add(controller.ControllerModel); // Field 7
							row.Add(controller.OutputCount.ToString()); // Field 8
							row.Add(controller.DMXStartAddress.ToString()); // Field 9
							row.Add(controller.Voltage.ToString()); // Field 10

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
				row.Add("LOROutput4");      // Field 2
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
						row.Add(channel.DMXController.LetterID);                // Field 1
						row.Add(channel.OutputNum.ToString());                 // Field 2
						row.Add(channel.Name);                                  // Field 3
						row.Add(channel.Location);                              // Field 4
						row.Add(channel.Comment);                               // Field 5
						row.Add(channel.Active.ToString());                     // Field 6
						row.Add(channel.DeviceType.ID.ToString());         // Field 7
						row.Add(LOR4Utils.lutils.ColorToHex(channel.Color));                // Field 8

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
				dbPath = Properties.Settings.Default.DBPath;
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
					dbPath = PathToDB(year);  // Get and save this too
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
							Properties.Settings.Default.DBPath = dbPath;
							Properties.Settings.Default.Save();
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
					if (universes.Count > 0)
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
				dbPath = PathToDB(year);  // Get and save this too

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

				if (universes.Count > 0)
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
			treeChannels.Nodes.Clear();

			BuildTreeUniverses(universes);
		}

		public void BuildTreeUniverses(List<DMXUniverse> uniList)
		{
			string nodeText = "";
			for (int u = 0; u < uniList.Count; u++)
			{
				DMXUniverse univ = uniList[u];
				//nodeText = univ.UniverseNumber.ToString() + ": " + univ.Name;
				nodeText = univ.ToString();

				Syncfusion.Windows.Forms.Tools.TreeNodeAdv uniNode = new Syncfusion.Windows.Forms.Tools.TreeNodeAdv(nodeText);
				treeChannels.Nodes.Add(uniNode); // treeChannels.Nodes.Add(nodeText); //   treeChannels.Nodes.Add(nodeText);
				uniNode.LeftImageIndices = TREEICONuniverse;
				uniNode.Tag = uniList[u];
				uniList[u].Tag = uniNode;
				BuildTreeControllers(uniNode);
				if (univ.BadName || univ.BadNumber)
				{
					uniNode.TextColor = Color.Red;
				}
				else
				{
					uniNode.TextColor = Color.Black;
				}

			}
		}

		public void BuildTreeControllers(TreeNodeAdv uniNode)
		{
			string nodeText = "";
			DMXUniverse universe = (DMXUniverse)uniNode.Tag;
			for (int ct = 0; ct < universe.DMXControllers.Count; ct++)
			{
				DMXController controller = universe.DMXControllers[ct];
				//nodeText = controller.DMXStartAddress.ToString("000") + "-";
				//int l = controller.DMXStartAddress + controller.OutputCount - 1;
				//nodeText += l.ToString("000") + ": ";
				//nodeText += controller.LetterID + ": " + controller.Name;
				nodeText = controller.ToString();
				Syncfusion.Windows.Forms.Tools.TreeNodeAdv ctlNode = new Syncfusion.Windows.Forms.Tools.TreeNodeAdv(nodeText);
				uniNode.Nodes.Add(ctlNode); // treeChannels.Nodes.Add(nodeText); //   treeChannels.Nodes.Add(nodeText);
				ctlNode.LeftImageIndices = TREEICONcontroller;
				ctlNode.Tag = controller;
				controller.Tag = ctlNode;
				BuildTreeChannels(ctlNode);
				if (controller.BadLetter || controller.BadName)
				{
					ctlNode.TextColor = Color.Red;
				}
				else
				{
					ctlNode.TextColor = Color.Black;
				}

			}
		}

		public void BuildTreeChannels(TreeNodeAdv ctlNode)
		{
			string nodeText = "";
			DMXController controller = (DMXController)ctlNode.Tag;
			for (int ch = 0; ch < controller.DMXChannels.Count; ch++)
			{
				DMXChannel channel = controller.DMXChannels[ch];
				//nodeText = channel.LOROutput4.ToString() + ": " + channel.Name;
				nodeText = channel.ToString();
				Syncfusion.Windows.Forms.Tools.TreeNodeAdv chanNode = new Syncfusion.Windows.Forms.Tools.TreeNodeAdv(nodeText);
				ctlNode.Nodes.Add(chanNode); // treeChannels.Nodes.Add(nodeText); //   treeChannels.Nodes.Add(nodeText);
				ImageList icons = treeChannels.LeftImageList;
				int iconIndex = LOR4Utils.lutils.ColorIcon(icons, channel.Color);
				int[] ico = { iconIndex };
				chanNode.LeftImageIndices = ico;
				chanNode.Tag = channel;
				channel.Tag = chanNode;
				if (channel.BadName || channel.BadOutput)
				{
					chanNode.TextColor = Color.Red;
				}
				else
				{
					chanNode.TextColor = Color.Black;
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
						//nodeText += ctlr.LetterID + ": " + ctlr.Name;
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
						//string nodeText = channel.LOROutput4.ToString() + ": " + channel.Name;
						string nodeText = channel.ToString();
						node.Text = nodeText;
						ImageList icons = treeChannels.LeftImageList;
						int iconIndex = LOR4Utils.lutils.ColorIcon(icons, channel.Color);
						int[] ico = { iconIndex };
						node.LeftImageIndices = ico;
					}
				}
			}
		}


		private void treeChannels_AfterSelect(object sender, EventArgs e)
		{
			TreeNodeAdv node = treeChannels.SelectedNode;
			if (node != null)
			{
				int[] ni = node.LeftImageIndices;
				bool en = (ni == TREEICONuniverse);
				btnController.Enabled = en;
				en = (ni == TREEICONcontroller);
				btnChannel.Enabled = en;
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

		private void EditUniverseNode(TreeNodeAdv node)
		{
			bool needSort = false;
			DMXUniverse universe = (DMXUniverse)node.Tag;
			universe.Editing = true;
			DMXUniverse newUni = universe.Clone();
			int oldUniNum = universe.UniverseNumber;
			frmUniverse uniForm = new frmUniverse(newUni, universes);
			uniForm.universes = universes;
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

		}

		public void EditControllerNode(TreeNodeAdv node)
		{
			bool needSort = false;
			DMXController controller = (DMXController)node.Tag;
			controller.Editing = true;
			DMXUniverse oldUniv = controller.DMXUniverse;
			string oldLetter = controller.LetterID;
			int oldStart = controller.DMXStartAddress;
			DMXController newCtl = controller.Clone();
			frmController ctlForm = new frmController(newCtl, universes);
			ctlForm.universes = universes;
			DialogResult dr = ctlForm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				if (ctlForm.dirty)
				{
					MakeDirty(true);
					// Replace the original with the clone
					controller.Clone(newCtl); // Copy back
					UpdateControllerNode(node);
					// Did the start address change?
					if (controller.DMXStartAddress != oldStart)
					{
						controller.DMXUniverse.DMXControllers.Sort();
						needSort = true;
					}
					// Did the universe change?
					if (oldUniv.ID != controller.DMXUniverse.ID)
					{
						// Find old universe and remove this controller
						for (int c = 0; c < oldUniv.DMXControllers.Count; c++)
						{
							if (controller.DMXUniverse.ID == oldUniv.DMXControllers[c].ID)
							{
								oldUniv.DMXControllers.RemoveAt(c);
								c = oldUniv.DMXControllers.Count; // Force exit of loop
							}
						}
						controller.DMXUniverse.DMXControllers.Add(controller);
						controller.DMXUniverse.DMXControllers.Sort();
						needSort = true;
					}
					if (needSort)
					{
						treeChannels.Nodes.Sort();
					}
				}
			}
			if (controller.BadName || controller.BadLetter)
			{
				node.TextColor = Color.Red;
			}
			else
			{
				node.TextColor = Color.Black;
			}
			controller.Editing = false;
			ctlForm.Dispose();



		}

		public void EditChannelNode(TreeNodeAdv node)
		{
			bool needSort = false;
			DMXChannel channelToEdit = (DMXChannel)node.Tag;
			DMXController oldCtlr = channelToEdit.DMXController;
			//int oldOutput = channelToEdit.LOROutput4;
			//int oldUnivNum = channelToEdit.UniverseNumber;
			//int oldDMXaddr = channelToEdit.DMXAddress;
			//DMXChannel channelClone = channelToEdit.Clone();
			DMXChannel channelClone = new DMXChannel(channelToEdit);
			channelClone.Editing = true;
			frmChannel chanForm = new frmChannel(channelClone, universes);
			//chanForm.AllChannels = AllChannels;
			//chanForm.universes = universes;
			//chanForm.deviceTypes = deviceTypes;
			try
			{
				// For some strange reason I can't figure out---
				// Closing the form sometimes triggers an exception!
				DialogResult dr = chanForm.ShowDialog(this);
				if (dr == DialogResult.OK)
				{
					if (chanForm.dirty)
					{
						MakeDirty(true);
						channelToEdit.Clone(channelClone); // Copy back
						UpdateChannelNode(node);
						// Did output number change?
						//if (channelToEdit.LOROutput4 != channelClone.LOROutput4)
						if ((channelToEdit.UniverseNumber != channelClone.UniverseNumber) ||
								(channelToEdit.DMXAddress != channelClone.DMXAddress) ||
								(channelToEdit.xLightsAddress != channelClone.xLightsAddress))
						{
							channelToEdit.DMXController.DMXChannels.Sort();
							//node.Parent.Nodes.Sort(); // No Sort Function on Node Collection
							needSort = true;
						}
						// Did we change controllers?
						int newCtlrID = channelToEdit.DMXController.ID;
						if (oldCtlr.ID != newCtlrID)
						{
							// Find old controller and remove this channelToEdit
							for (int c = 0; c < oldCtlr.DMXChannels.Count; c++)
							{
								if (channelToEdit.ID == oldCtlr.DMXChannels[c].ID)
								{
									oldCtlr.DMXChannels.RemoveAt(c);
									c = oldCtlr.DMXChannels.Count; // Force loop exit
								}
							}
							node.Parent.Nodes.Remove(node);
							for (int u = 0; u < universes.Count; u++)
							//foreach(DMXUniverse universe in universes)
							{
								DMXUniverse universe = universes[u];
								for (int c = 0; c < universe.DMXControllers.Count; c++)
								//foreach(DMXController controller in universe.DMXControllers)
								{
									DMXController controller = universe.DMXControllers[c];
									if (channelToEdit.DMXController.ID == controller.ID)
									{
										TreeNodeAdv newp = (TreeNodeAdv)controller.Tag;
										newp.Nodes.Add(node);
										// Force exit of both loops
										c = universe.DMXControllers.Count;
										u = universes.Count;
									}
								}
							}
							channelToEdit.DMXController.DMXChannels.Add(channelToEdit);
							channelToEdit.DMXController.DMXChannels.Sort();
							needSort = true;
						}
						// So... Chan obj properly being moved, Resorting the tree is not reflecting that
						//Try another technique....
						if (needSort)
						{
							treeChannels.Nodes.Sort();
						}
					}
				}
				if (channelToEdit.BadName || channelToEdit.BadOutput)
				{
					node.TextColor = Color.Red;
				}
				else
				{
					node.TextColor = SystemColors.WindowText;
				}
			}
			catch (Exception ex)
			{
				// So when the effing form throws an effing excepttion for some effing reaason when it is effing closed
				// Consider that a cancel
				int ln = LOR4Utils.lutils.ExceptionLineNumber(ex);
				string msg = "Error on line " + ln.ToString() + "\r\n";
				msg += ex.ToString() + " while exiting Channel editor " + channelToEdit.Name;
				if (isWiz)
				{
					DialogResult dr = MessageBox.Show(this, msg, "EXCEPTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			channelToEdit.Editing = false;
			chanForm.Dispose();

		}

		private void btnChannel_Click(object sender, EventArgs e)
		{
			if (treeChannels.SelectedNode != null)
			{
				if (treeChannels.SelectedNode.Tag.GetType() == typeof(DMXController))
				{
					TreeNodeAdv parentNode = treeChannels.SelectedNode;
					DMXController parentCtl = (DMXController)treeChannels.SelectedNode.Tag;

					DMXChannel channel = new DMXChannel();
					lastID++;
					channel.ID = lastID;
					channel.DMXController = parentCtl;
					channel.Editing = true;
					DMXController oldCtlr = channel.DMXController;
					DMXChannel newChan = channel.Clone();
					frmChannel chanForm = new frmChannel(newChan, universes);
					//chanForm.AllChannels = AllChannels;
					//chanForm.universes = universes;
					//chanForm.deviceTypes = deviceTypes;
					DialogResult dr = chanForm.ShowDialog(this);
					if (dr == DialogResult.OK)
					{
						if (chanForm.dirty)
						{
							// Replace the original with the clone
							channel.Clone(newChan);
							// Did we change controllers?
							if (oldCtlr.ID != channel.DMXController.ID)
							{
								// Find old controller and remove this channel
								for (int c = 0; c < oldCtlr.DMXChannels.Count; c++)
								{
									if (channel.ID == oldCtlr.DMXChannels[c].ID)
									{
										oldCtlr.DMXChannels.RemoveAt(c);
										c = oldCtlr.DMXChannels.Count; // Force loop exit
									}
								}
								channel.DMXController.DMXChannels.Add(channel);
								parentNode = (TreeNodeAdv)channel.DMXController.Tag;
								channel.DMXController.DMXChannels.Sort();
							}
							AllChannels.Add(channel);
							AllChannels.Sort();
							TreeNodeAdv node = new TreeNodeAdv(channel.ToString());
							parentNode.Nodes.Add(node);
							node.Tag = channel;
							int ImageIndex = LOR4Utils.lutils.ColorIcon(imlTreeIcons, channel.Color);
							int[] ico = { ImageIndex };
							node.LeftImageIndices = ico;
							channel.Tag = node;
							MakeDirty(true);

							//BuildTree();
							treeChannels.Nodes.Sort();
						}
						treeChannels.SelectedNode = parentNode;
						treeChannels.Focus();
						channel.Editing = false;
						chanForm.Dispose();
					}
				}
			}
		}

		private void btnController_Click(object sender, EventArgs e)
		{
			if (treeChannels.SelectedNode != null)
			{
				if (treeChannels.SelectedNode.Tag.GetType() == typeof(DMXUniverse))
				{
					TreeNodeAdv uniNode = treeChannels.SelectedNode;
					DMXUniverse parentUni = (DMXUniverse)uniNode.Tag;

					DMXController ctlr = new DMXController();
					lastID++;
					ctlr.ID = lastID;
					ctlr.DMXUniverse = parentUni;
					ctlr.Editing = true;
					DMXUniverse oldUniv = ctlr.DMXUniverse;
					DMXController newCtlr = ctlr.Clone();
					frmController ctlrForm = new frmController(newCtlr, universes);
					//ctlrForm.AllChannels = AllChannels;
					ctlrForm.universes = universes;
					DialogResult dr = ctlrForm.ShowDialog(this);
					if (dr == DialogResult.OK)
					{
						// Replace the original with the clone
						ctlr = newCtlr;
						// Did we change controllers?
						if (oldUniv.ID != ctlr.DMXUniverse.ID)
						{
							// Find old controller and remove this channel
							for (int u = 0; u < oldUniv.DMXControllers.Count; u++)
							{
								if (ctlr.ID == oldUniv.DMXControllers[u].ID)
								{
									oldUniv.DMXControllers.RemoveAt(u);
									u = oldUniv.DMXControllers.Count; // Force loop exit
								}
							}
							ctlr.DMXUniverse.DMXControllers.Add(ctlr);
							ctlr.DMXUniverse.DMXControllers.Sort();
						}
						//BuildTree();
						treeChannels.Nodes.Sort();
					}
					treeChannels.SelectedNode = uniNode;
					ctlr.Editing = false;
					ctlrForm.Dispose();
				}
			}

		}

		private void btnUniverse_Click(object sender, EventArgs e)
		{
			DMXUniverse univ = new DMXUniverse();
			lastID++;
			univ.ID = lastID;
			univ.Editing = true;
			//DMXController oldCtlr = channel.DMXController;
			DMXUniverse newUniv = univ.Clone();
			frmUniverse univForm = new frmUniverse(newUniv, universes);
			//univForm.AllChannels = AllChannels;
			univForm.universes = universes;
			DialogResult dr = univForm.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				// Replace the original with the clone
				univ = newUniv;
				universes.Add(univ);
				universes.Sort();
				BuildTree();
			}
			univ.Editing = false;
			univForm.Dispose();

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
				string msg = "LOR4Channel information has changed.\r\nSave?";
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
				SaveFormPosition();
				GetTheControlsFromTheHeartOfTheSun();
			}
		}

		private int ExportToSpreadsheet(string filePath)
		{
			int errs = 0;
			string uniName = "";
			string ctlName = "";
			string chnName = ""; // debugging exceptions
			string sprFile = filePath + "ChannelSpreadsheet.csv";
			try
			{
				CsvFileWriter writer = new CsvFileWriter(sprFile);
				CsvRow row = new CsvRow();
				// Create first line which is headers;
				row.Add("Universe");         // Field 0
				row.Add("Controller");       // Field 1
				row.Add("LOROutput4");           // Field 2
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

				universes.Sort();
				for (int u = 0; u < universes.Count; u++)
				{
					DMXUniverse universe = universes[u];
					uniName = universe.Name;
					try
					{
						row = new CsvRow();
						row.Add(universe.UniverseNumber.ToString());       // Field 0 - Universe
						row.Add("");                                  // Field 1 - Controller
						row.Add("");                                  // Field 2 - LOROutput4
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
								row.Add(controller.LetterID);                        // Field 1 - Controller
								row.Add("");                                  // Field 2 - LOROutput4
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
										row.Add(channel.DMXController.LetterID);     // Field 1 - Controller
										row.Add(channel.OutputNum.ToString());       // Field 2 - LOROutput4
										row.Add(channel.DMXAddress.ToString());      // Field 3 - DMX Address
										row.Add(channel.xLightsAddress.ToString());  // Field 4 - xLights Address
										row.Add(channel.Active.ToString());          // Field 5 - Active
										row.Add(channel.Name);                       // Field 6 - Name
										row.Add(channel.DeviceType.Name);              // Field 7 - Type
										row.Add(LOR4Utils.lutils.ColorToHex(channel.Color));    // Field 8 - Color
										row.Add(channel.Location);                   // Field 9 - Location
										row.Add(channel.Comment);                    // Field 10 - comment
										writer.WriteRow(row);
									} // End Channel Try
									catch (Exception ex)
									{

									} // End Channel Catch
								} // End Channel LORLoop4
							} // End Controller Try
							catch (Exception ex)
							{

							} // End Controller Catch
						} // End Controller LORLoop4
					} // End Universe Try
					catch (Exception ex)
					{

					} // End Universe Catch
				} // End Universe LORLoop4
				writer.Close();
				Fyle.LaunchFile(sprFile);
			} // End Writer Try
			catch (Exception ex)
			{

			} // end Writer Catch

			return errs;
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			ExportToSpreadsheet(dbPath);
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
						msg += controller.LetterID + ": " + controller.Name + " which has ";
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
							if ((universe.DMXControllers.Count > 3) || (chanCount > 23))
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
								universes.Remove(universe);
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

		private int MatchUp(string seqFile, bool sortByName)
		{
			int ret = 0;
			int lorCN = 0;
			int vizCN = 0;
			int xCNM = 0;
			int xCNG = 0;
			LOR4Sequence lseq = new LOR4Sequence(seqFile);

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
			xModel.AllModels.Sort();
			xSeq.xModelGroups.Sort();
			List<FuzzyList> fuzzies = new List<FuzzyList>();
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
				} // LOR channel LORLoop4

				///////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to Viz Channels ///
				/////////////////////////////////////////////////////
				for (int v = vizCN; v < lViz.VizChannels.Count; v++)
				{
					LORVizChannel4 chanViz = lViz.VizChannels[v];
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
					LORVizItemGroup4 grpViz = lViz.VizItemGroups[v];
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
					LORVizDrawObject4 drobViz = lViz.VizDrawObjects[v];
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
				for (int x = xCNM; x < xModel.AllModels.Count; x++)
				{
					xModel chanx = xModel.AllModels[x];

					if (mgrName.CompareTo(chanx.Name.ToLower()) == 0)
					{
						chanMgr.TagX = chanx;
						chanx.Tag = chanMgr;
						chanMgr.ExactX = true;
						chanx.ExactMatch = true;
						xCNM = x + 1;
						x = xModel.AllModels.Count; // Force exit of loop
					}
				} // xModels LORLoop4

				///////////////////////////////////////////////////////
				/// Match Managed Channels Exactly to xModelGroups ///
				/////////////////////////////////////////////////////
				for (int x = xCNG; x < xSeq.xModelGroups.Count; x++)
				{
					xModelGroup groupx = xSeq.xModelGroups[x];

					if (mgrName.CompareTo(groupx.Name.ToLower()) == 0)
					{
						chanMgr.TagX = groupx;
						groupx.Tag = chanMgr;
						chanMgr.ExactX = true;
						groupx.ExactMatch = true;
						xCNG = x + 1;
						x = xSeq.xModelGroups.Count; // Force exit of loop
					}
				} // End xModelGroups LORLoop4
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
					fuzzies.Clear();
					for (int l = 0; l < lseq.Channels.Count; l++)
					{
						LOR4Channel chanLOR = lseq.Channels[l];
						if (chanLOR.Tag == null)
						{
							double score = mgrName.YetiLevenshteinSimilarity(chanLOR.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanLOR.Name);
							//if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							if (score > .30D)
							{
								score = mgrName.RankEquality(chanLOR.Name.ToLower());
								//if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								if (score > .40D)
								{
									FuzzyList fuzzie = new FuzzyList(chanMgr, chanLOR, score);
									fuzzies.Add(fuzzie);
								}
							}
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels
					if (fuzzies.Count > 0)
					{
						fuzzies.Sort();
						chanMgr.TagLOR = (iLOR4Member)fuzzies[0].Item2;
						LOR4Channel chanLor = (LOR4Channel)fuzzies[0].Item2;
						chanLor.Tag = chanMgr;
					}
				}

				/////////////////////////////////////////////////////
				/// Fuzzy Match Managed Channels to Viz Channels ///
				///////////////////////////////////////////////////
				if (chanMgr.TagViz == null)
				{
					fuzzies.Clear();
					for (int v = 0; v < lViz.VizChannels.Count; v++)
					{
						LORVizChannel4 chanViz = lViz.VizChannels[v];
						if (chanViz.Tag == null)
						{
							double score = mgrName.YetiLevenshteinSimilarity(chanViz.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanViz.Name);
							//if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							if (score > .30D)
							{
								score = mgrName.RankEquality(chanViz.Name.ToLower());
								//if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								if (score > .40D)
								{
									FuzzyList fuzzie = new FuzzyList(chanMgr, chanViz, score);
									fuzzies.Add(fuzzie);
								}
							}
						} // current Viz channel has no tag and thus no exact matches
					} // End loop thru Viz channels

					for (int v = 0; v < lViz.VizDrawObjects.Count; v++)
					{
						LORVizDrawObject4 chanViz = lViz.VizDrawObjects[v];
						if (chanViz.Tag == null)
						{
							double score = mgrName.YetiLevenshteinSimilarity(chanViz.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, chanViz.Name);
							//if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							if (score > .30D)
							{
								score = mgrName.RankEquality(chanViz.Name.ToLower());
								//if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								if (score > .40D)
								{
									FuzzyList fuzzie = new FuzzyList(chanMgr, chanViz, score);
									fuzzies.Add(fuzzie);
								}
							}
						} // current Viz channel has no tag and thus no exact matches
					} // End loop thru Viz channels



					if (fuzzies.Count > 0)
					{
						fuzzies.Sort();
						chanMgr.TagViz = (iLOR4Member)fuzzies[0].Item2;
						iLOR4Member chanViz = (iLOR4Member)fuzzies[0].Item2;
						chanViz.Tag = chanMgr;
					}
				}

				////////////////////////////////////////////////
				/// Fuzzy Match Managed Channels to xModels ///
				//////////////////////////////////////////////
				if (chanMgr.TagViz == null)
				{
					fuzzies.Clear();
					int attemptCount = 0;
					for (int x = 0; x < xModel.AllModels.Count; x++)
					{
						xModel chanx = xModel.AllModels[x];
						if (chanx.Tag == null)
						{
							attemptCount++;
							//double score = mgrName.YetiLevenshteinSimilarity(chanx.Name.ToLower());
							double score = mgrName.RankEquality(chanx.Name.ToLower(), FuzzyFunctions.USE_SUGGESTED_PREMATCH);
							ReportMatch(matchWriter, score, mgrName, chanx.Name);
							if (score > 0D)
							{
								// Hey! We Got Something!!
								if (isWiz)
								{
									//System.Diagnostics.Debugger.Break();
								}
							}

							if (score > .30D)
							{
								//score = mgrName.RankEquality(chanx.Name.ToLower());
								score = mgrName.RankEquality(chanx.Name.ToLower(), FuzzyFunctions.USE_SUGGESTED_FINALMATCH);
								if (score > .40D)
								{
									FuzzyList fuzzie = new FuzzyList(chanMgr, chanx, score);
									fuzzies.Add(fuzzie);
								}
							}
							int nn = attemptCount;
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels

					/////////////////////////////////////////////////////////
					/// Fuzzy Match Managed Channels to LOR xModelGroups ///
					///////////////////////////////////////////////////////
					for (int x = 0; x < xSeq.xModelGroups.Count; x++)
					{
						xModelGroup groupx = xSeq.xModelGroups[x];
						if (groupx.Tag == null)
						{
							double score = mgrName.YetiLevenshteinSimilarity(groupx.Name.ToLower());
							ReportMatch(matchWriter, score, mgrName, groupx.Name);
							//if (score > FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE)
							if (score > .30D)
							{
								score = mgrName.RankEquality(groupx.Name.ToLower());
								//if (score > FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE)
								if (score > .40D)
								{
									FuzzyList fuzzie = new FuzzyList(chanMgr, groupx, score);
									fuzzies.Add(fuzzie);
								}
							}
						} // current LOR channel has no tag and thus no exact matches
					} // End loop thru LOR channels

					if (fuzzies.Count > 0)
					{
						fuzzies.Sort();
						chanMgr.TagX = (xMember)fuzzies[0].Item2;
						xMember chanx = (xMember)fuzzies[0].Item2;
						chanx.Tag = chanMgr;
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
					xMember chanx = (xMember)chanMgr.TagX;
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
					xMember chanx = (xMember)chanMgr.TagX;
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
			for (int x = 0; x < xModel.AllModels.Count; x++)
			{
				xModel chanx = xModel.AllModels[x];
				if (chanx.Tag == null)
				{
					StringBuilder lineOut = new StringBuilder("  NoMatch Group: ");
					lineOut.Append(chanx.Name);
					writer.WriteLine(lineOut);
				}
			}
			for (int x = 0; x < xSeq.xModelGroups.Count; x++)
			{
				xModelGroup chanx = xSeq.xModelGroups[x];
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
			xModel.AllModels.Sort();
			xSeq.xModelGroups.Sort();

			Fyle.LaunchFile(reportFile);


			return ret;
		}

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

