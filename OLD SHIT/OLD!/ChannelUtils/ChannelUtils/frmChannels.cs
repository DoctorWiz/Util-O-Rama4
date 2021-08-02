using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LORUtils;

namespace ChannelUtils
{
    
    
    public partial class frmChannels : Form
    {
        private string lastFile = "";
        private string lastMasterConfig = "";
        private string configFolder = "";
        private Sequence masterSeq;
        private Sequence compareSeq;
        private bool startupFlag = false;
        private int controllerCount = 0;
        private Controller[] controllers = new Controller[1];
        
        public frmChannels()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            if (configFolder.Length < 5)
            {
                configFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\ChannelTemplates\\";
                Directory.CreateDirectory(configFolder);
            }
            else
            {
                if (!Directory.Exists(configFolder))
                {
                    configFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\ChannelTemplates\\";
                }
            }

            dlgOpenFile.Filter = "Channel Configurations (*.lcc)|*.lcc";
            dlgOpenFile.DefaultExt = "*.lcc";
            dlgOpenFile.InitialDirectory = configFolder;
            dlgOpenFile.CheckFileExists = true;
            dlgOpenFile.CheckPathExists = true;
            dlgOpenFile.Multiselect = false;
            DialogResult result = dlgOpenFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastMasterConfig = dlgOpenFile.FileName;
                if (lastMasterConfig.Substring(1, 2) != ":\\")
                {
                    lastMasterConfig = configFolder + "\\" + lastMasterConfig;
                }

                Properties.Settings.Default.lastMasterConfig = lastMasterConfig;
                Properties.Settings.Default.configFolder = Path.GetDirectoryName(lastMasterConfig);
                Properties.Settings.Default.Save();

                if (lastMasterConfig.Length > configFolder.Length)
                {
                    if (lastMasterConfig.Substring(0, configFolder.Length).CompareTo(configFolder) == 0)
                    {
                        txtFilename.Text = lastMasterConfig.Substring(configFolder.Length);
                    }
                    else
                    {
                        txtFilename.Text = lastMasterConfig;
                    } // End else (lastMasterConfig.Substring(0, configFolder.Length).CompareTo(configFolder) == 0)
                } // end if (lastMasterConfig.Length > configFolder.Length)
                btnReadMaster.Enabled = true;
            } // end if (result = DialogResult.OK)

        } // end Browse

        private void frmLORutils_Load(object sender, EventArgs e)
        {
            initForm();
        } // end Form_Load
        
        private void initForm()
        {

            lastMasterConfig = Properties.Settings.Default.lastMasterConfig;
            configFolder = Properties.Settings.Default.configFolder;
            if (configFolder.Length < 5)
            {
                configFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\ChannelConfigs\\";
            }

            if (lastMasterConfig.Length > configFolder.Length)
            {
                if (lastMasterConfig.Substring(0, configFolder.Length).CompareTo(configFolder) == 0)
                {
                    txtFilename.Text = lastMasterConfig.Substring(configFolder.Length);
                }
                else
                {
                    txtFilename.Text = lastMasterConfig;
                } // End else (lastMasterConfig.Substring(0, configFolder.Length).CompareTo(configFolder) == 0)
            } // end if (lastMasterConfig.Length > configFolder.Length)

            if (File.Exists(lastMasterConfig))
            {
                btnReadMaster.Enabled = true;
                chkAutoRead.Checked = Properties.Settings.Default.AutoRead;
                if (chkAutoRead.Checked)
                {
                    //btnReadMaster_Click(btnReadMaster, EventArgs.Empty);
                }
            }


        } // end initForm

        private void saveSettings()
        {

            Properties.Settings.Default.lastMasterConfig = lastMasterConfig;
            Properties.Settings.Default.configFolder = Path.GetDirectoryName(lastMasterConfig);
            Properties.Settings.Default.AutoRead = chkAutoRead.Checked;

            Properties.Settings.Default.Save();
            

        } // end saveSettings

        private void frmLORutils_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();

        } // end Form_Closing

        private void btnReadMaster_Click(object sender, EventArgs e)
        {
            masterSeq = new Sequence(); 
            masterSeq.readFile(lastMasterConfig);

            AnalyzeMaster();

        } // end FormClosing

        private void AnalyzeMaster()
        {

            // Reset
            controllerCount = 0;
            
            
            string info = "";
            channel ch;
            int ctlFound = -1;


            for (int chan = 0; chan < masterSeq.channelCount; chan++)
            {
                ch = masterSeq.channels[chan];
                ctlFound = -1;
                if (controllerCount > 0)
                {
                    for (int ctl = 0; ctl < controllerCount; ctl++)
                    {
                        if (ch.deviceType == controllers[ctl].type)
                        {
                            if (ch.unit == controllers[ctl].unit)
                            {
                                ctlFound = ctl;
                                break;
                            }
                        }
                        
                    }
                }
                if (ctlFound < 0)
                {
                    Controller ctlr = new Controller();
                    ctlr.type = ch.deviceType;
                    ctlr.unit = ch.unit;
                    ctlr.highestCircuit = ch.circuit;
                    ctlr.channelCount = 1;
                    int[] chList = new int[1];
                    chList[0] = chan;
                    ctlr.channelList = chList;
                    int[] ckts = new int[1];
                    ckts[0] = ch.circuit;
                    ctlr.circuits = ckts;
                    Array.Resize(ref controllers, controllerCount+1);
                    controllers[controllerCount]=ctlr;
                    controllerCount++;
                }
                else
                {
                    Controller ctlr = controllers[ctlFound];
                    ctlr.channelCount++;
                    int[] chList = ctlr.channelList;
                    Array.Resize(ref chList, ctlr.channelCount);
                    chList[ctlr.channelCount - 1] = chan;
                    int[] ckts = ctlr.circuits;
                    ctlr.channelList = chList;
                    Array.Resize(ref ckts, ctlr.channelCount);
                    ckts[ctlr.channelCount - 1] = ch.circuit;
                    ctlr.circuits = ckts;
                    if (ch.circuit > ctlr.highestCircuit)
                    {
                        ctlr.highestCircuit = ch.circuit;
                    }
                }
            }



            DateTime creationTime = File.GetCreationTime(lastMasterConfig);
            DateTime lastWriteTime = File.GetLastWriteTime(lastMasterConfig);
            info = "created on " + creationTime.ToString() + "\n";
            info += "last modified on " + lastWriteTime.ToString() + "\n";
            long fileSize = new System.IO.FileInfo(lastMasterConfig).Length;
            info += fileSize.ToString("###,###,###") + " bytes\n";
            info += masterSeq.lineCount.ToString() + " lines\n";
            info += masterSeq.channelCount.ToString() + " channels\n";
            info += masterSeq.groupCount.ToString() + " groups\n";
            info += masterSeq.trackCount.ToString() + " tracks\n";
            info += masterSeq.timingGridCount.ToString() + " timing grids\n";
            info += controllerCount.ToString() + " controllers\n";



            frmResults resultsForm = new frmResults();

            resultsForm.SetInfo(info);
            resultsForm.SetFilename(Path.GetFileNameWithoutExtension(lastMasterConfig));
            resultsForm.ShowDialog();

            DialogResult = resultsForm.DialogResult;


        }

        private void frmChannels_Shown(object sender, EventArgs e)
        {
            if (File.Exists(lastMasterConfig))
            {
                if (chkAutoRead.Checked)
                {
                    if (!startupFlag)
                    {
                        startupFlag = true;
                        btnReadMaster_Click(btnReadMaster, EventArgs.Empty);
                    }
                }
            }

        }
    }

    class Controller
    {
        public deviceType type = deviceType.None;
        public int unit = 0;
        public int channelCount = 0;
        public int highestCircuit = 0;
        public int[] channelList;
        public int[] circuits;
    }
    
}
