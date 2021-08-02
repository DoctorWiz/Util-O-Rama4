using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using LORUtils;



namespace Loracity
{
	public partial class frmAcity : Form
	{
		string sequenceFolder;
		string audioFolder;
		string sequenceFile;
		string audioFile;
		string transformFile;
		Sequence seq = new Sequence();

		public frmAcity()
		{
			InitializeComponent();
		}


		string getSequenceFolder()
		{
			const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
			string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
;			string fldr = (string)Registry.GetValue(keyName, "NonAudioPath", userDocs);
			return fldr;
		}

		string getAudioFolder()
		{
			const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Light-O-Rama\\Shared";
			string userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			; string fldr = (string)Registry.GetValue(keyName, "AudioPath", userDocs);
			return fldr;
		}

		private void frmAcity_Load(object sender, EventArgs e)
		{
			loadSettings();
			sequenceFolder = getSequenceFolder();
			audioFolder = getAudioFolder();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			//string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			string basePath = sequenceFolder;
			dlgOpenFile.Filter = "Musical Sequences (*.lms)|*.lms";
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = sequenceFolder;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			DialogResult result = dlgOpenFile.ShowDialog();

			if (result == DialogResult.OK)
			{
				sequenceFile = dlgOpenFile.FileName;
				if (sequenceFile.Substring(1, 2) != ":\\")
				{
					sequenceFile = basePath + "\\" + sequenceFile;
				}

				Properties.Settings.Default.sequenceFile = sequenceFile;
				Properties.Settings.Default.Save();

				if (sequenceFile.Length > basePath.Length)
				{
					if (sequenceFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
					{
						txtSequenceFile.Text = sequenceFile.Substring(basePath.Length);
					}
					else
					{
						txtSequenceFile.Text = sequenceFile;
					} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				} // end if (lastFile.Length > basePath.Length)
			} // end if (result = DialogResult.OK)

		}

		private void frmAcity_FormClosed(object sender, FormClosedEventArgs e)
		{
			saveSettings();
		}

		void loadSettings()
		{
			sequenceFile = Properties.Settings.Default.sequenceFile;
			transformFile = Properties.Settings.Default.transformFile;
			audioFile = Properties.Settings.Default.audioFile;
			txtSequenceFile.Text = sequenceFile;
			txtTransformFile.Text = transformFile;
			this.Top = Properties.Settings.Default.top;
			this.Left = Properties.Settings.Default.left;
		}

		void saveSettings()
		{
			Properties.Settings.Default.sequenceFile = sequenceFile;
			Properties.Settings.Default.transformFile = transformFile;
			Properties.Settings.Default.audioFile = audioFile;
			Properties.Settings.Default.top = this.Top;
			Properties.Settings.Default.left = this.Left;
			Properties.Settings.Default.Save();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			seq.readFile(sequenceFile);
			track acityTrack = new track();
			timingGrid acityGrid = new timingGrid();
			acityTrack.name = "Loracity";
			acityGrid.name = "Loracity";
			acityGrid.type = timingGridType.freeform;
			acityTrack.totalCentiseconds = seq.totalCentiseconds; // seq.tracks[0].totalCentiseconds;
			int saveID = seq.timingGrids[seq.timingGridCount - 1].saveID + 1;
			acityGrid.saveID = saveID;

			StreamReader reader = new StreamReader(transformFile);
			string lineIn = "";
			string[] parts;
			long timing;
			decimal position;
			
			while ((lineIn = reader.ReadLine()) != null)
			{
				parts = lineIn.Split('\t');
				position = decimal.Parse(parts[1]);
				timing = (long)(position * 1000 + 5) / 10;
				if (timing > 0)
				{
					acityGrid.AddTiming(timing);
				}
			}
			reader.Close();

			int gridIndex = seq.AddTimingGrid(acityGrid);
			acityTrack.timingGridIndex = gridIndex; //gridIndex
			acityTrack.timingGridSaveID = saveID;
			seq.AddTrack(acityTrack);
			string testFile = sequenceFolder + "\\Loracity Test.lms";
			//seq.WriteFile(testFile);
			seq.WriteFileInDisplayOrder(testFile);
			System.Media.SystemSounds.Exclamation.Play();
			MessageBox.Show("Try opening the test file and check for a new timing grid.", "Test Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		private void btnBrowseAudacity_Click(object sender, EventArgs e)
		{
		}

		private void btnBrowseTransform_Click(object sender, EventArgs e)
		{
			string basePath = audioFolder;
			dlgOpenFile.Filter = "Audio Transformation Files (*.txt, *.csv, *.lab)|*.txt;*.csv;*.lab";
			dlgOpenFile.DefaultExt = "*.lms";
			dlgOpenFile.InitialDirectory = audioFolder;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.CheckPathExists = true;
			dlgOpenFile.Multiselect = false;
			DialogResult result = dlgOpenFile.ShowDialog();

			if (result == DialogResult.OK)
			{
				transformFile = dlgOpenFile.FileName;
				if (transformFile.Substring(1, 2) != ":\\")
				{
					transformFile = basePath + "\\" + transformFile;
				}

				Properties.Settings.Default.transformFile = transformFile;
				Properties.Settings.Default.Save();

				if (transformFile.Length > basePath.Length)
				{
					if (transformFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
					{
						txtTransformFile.Text = transformFile.Substring(basePath.Length);
					}
					else
					{
						txtTransformFile.Text = transformFile;
					} // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
				} // end if (lastFile.Length > basePath.Length)
			} // end if (result = DialogResult.OK)
		}
	}

}
