using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChannelUtils
{
    public partial class frmChannels : Form
    {
        private string lastFile = "";
        private LORutils.Sequence seq;
        
        public frmChannels()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            dlgFile.Filter = "Musical Sequences (*.lms)|*.lms|Animated Sequences (*.las)|*.las";
            dlgFile.DefaultExt = "*.lms";
            dlgFile.InitialDirectory = basePath;
            dlgFile.CheckFileExists = true;
            dlgFile.CheckPathExists = true;
            dlgFile.Multiselect = false;
            DialogResult result = dlgFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastFile = dlgFile.FileName;
                if (lastFile.Substring(1, 2) != ":\\")
                {
                    lastFile = basePath + "\\" + lastFile;
                }

                Properties.Settings.Default.lastFile = lastFile;
                Properties.Settings.Default.Save();

                if (lastFile.Length > basePath.Length)
                {
                    if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtFilename.Text = lastFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtFilename.Text = lastFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)

        } // end Browse

        private void frmLORutils_Load(object sender, EventArgs e)
        {
            initForm();
        } // end Form_Load
        
        private void initForm()
        {

            lastFile = Properties.Settings.Default.lastFile;
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            if (lastFile.Length > basePath.Length)
            {
                if (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                {
                    txtFilename.Text = lastFile.Substring(basePath.Length);
                }
                else
                {
                    txtFilename.Text = lastFile;
                } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
            } // end if (lastFile.Length > basePath.Length)
        } // end initForm

        private void saveSettings()
        {

            Properties.Settings.Default.lastFile = lastFile;

            Properties.Settings.Default.Save();

        } // end saveSettings

        private void frmLORutils_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();

        } // end Form_Closing

        private void btnReadMaster_Click(object sender, EventArgs e)
        {
            seq = new LORutils.Sequence(); 
            seq.readFile(lastFile);

        } // end FormClosing
        
        
    }
}
