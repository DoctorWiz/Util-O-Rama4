using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lor_o_Tune
{
    public partial class frmLorOTune : Form
    {

        GroupBox[] channelGroups = new GroupBox[32];
        ComboBox[] channelDevices = new ComboBox[32];
        ComboBox[] channelPresets = new ComboBox[32];
        NumericUpDown[] channelHighs = new NumericUpDown[32];
        NumericUpDown[] channelLows = new NumericUpDown[32];
        NumericUpDown[] channelStarts = new NumericUpDown[32];
        NumericUpDown[] channelEnds = new NumericUpDown[32];
        TextBox[] channelNames = new TextBox[32];
        NumericUpDown[] channelUnits = new NumericUpDown[32];
        NumericUpDown[] channelChans = new NumericUpDown[32];
        PictureBox[] channelColors = new PictureBox[32];

        string lastSequenceFile;
        string lastMusicFile;
        string lastChannelFile;
        string lastPresetFile;

        bool dirtyPresets = false;
        
        public frmLorOTune()
        {
            InitializeComponent();
        }

        private void frmLorOTune_Load(object sender, EventArgs e)
        {
            fillOptions();
        }

        private void fillOptions()
        {

            ComboBox cboBox = cboPreset;

            cboBox.Items.Add("Custom...");          // index 0
            cboBox.Items.Add("C0->B0  20-31 Hz");   // index 1

            cboBox.Items.Add("C1      32-33 Hz");   // index 2
            cboBox.Items.Add("C#1/Db1 34-35 Hz");
            cboBox.Items.Add("D1      36-37 Hz");
            cboBox.Items.Add("D#1/Eb1 38-40 Hz");
            cboBox.Items.Add("E1      41-42 Hz");
            cboBox.Items.Add("F1      43-44 Hz");
            cboBox.Items.Add("F#1/Gb1 45-47 Hz");
            cboBox.Items.Add("G1      48-50 Hz");
            cboBox.Items.Add("G#1/Ab1 51-53 Hz");
            cboBox.Items.Add("A1      54-56 Hz");
            cboBox.Items.Add("A#1/Bb1 57-60 Hz");
            cboBox.Items.Add("B1      61-63 Hz");
            cboBox.Items.Add("C1->B1  32-63 Hz");   // index 14

            cboBox.Items.Add("C2      64-67 Hz");   // index 15
            cboBox.Items.Add("C#2/Db2 68-71 Hz");
            cboBox.Items.Add("D2      72-75 Hz");
            cboBox.Items.Add("D#2/Eb2 76-80 Hz");
            cboBox.Items.Add("E2      81-84 Hz");
            cboBox.Items.Add("F2      85-89 Hz");
            cboBox.Items.Add("F#2/Gb2 90-95 Hz");
            cboBox.Items.Add("G2      96-100 Hz");
            cboBox.Items.Add("G#2/Ab2 101-106 Hz");
            cboBox.Items.Add("A2      107-113 Hz");
            cboBox.Items.Add("A#2/Bb2 114-120 Hz");
            cboBox.Items.Add("B2      121-127 Hz");
            cboBox.Items.Add("C2->B2  64-127 Hz");   // index 27

            cboBox.Items.Add("C3      128-134 Hz");  // index 28
            cboBox.Items.Add("C#3/Db3 135-142 Hz");
            cboBox.Items.Add("D3      143-151 Hz");
            cboBox.Items.Add("D#3/Eb3 152-160 Hz");
            cboBox.Items.Add("E3      161-169 Hz");
            cboBox.Items.Add("F3      170-179 Hz");
            cboBox.Items.Add("F#3/Gb3 180-190 Hz");
            cboBox.Items.Add("G3      191-201 Hz");
            cboBox.Items.Add("G#3/Ab3 202-213 Hz");
            cboBox.Items.Add("A3      214-226 Hz");
            cboBox.Items.Add("A#3/Bb3 227-240 Hz");
            cboBox.Items.Add("B3      241-254 Hz");
            cboBox.Items.Add("C3->B3  128-254 Hz");  // index 40

            cboBox.Items.Add("C4      255-269 Hz");  // index 41
            cboBox.Items.Add("C#4/Db4 270-285 Hz");
            cboBox.Items.Add("D4      286-302 Hz");
            cboBox.Items.Add("D#4/Eb4 303-320 Hz");
            cboBox.Items.Add("E4      321-339 Hz");
            cboBox.Items.Add("F4      340-359 Hz");
            cboBox.Items.Add("F#4/Gb4 360-380 Hz");
            cboBox.Items.Add("G4      381-403 Hz");
            cboBox.Items.Add("G#4/Ab4 404-427 Hz");
            cboBox.Items.Add("A4      428-453 Hz");
            cboBox.Items.Add("A#4/Bb4 454-480 Hz");
            cboBox.Items.Add("B4      481-508 Hz");
            cboBox.Items.Add("C4->B4  255-508 Hz");  // index 53

            cboBox.Items.Add("C5      509-538 Hz");  // index 54
            cboBox.Items.Add("C#5/Db5 539-570 Hz");
            cboBox.Items.Add("D5      571-604 Hz");
            cboBox.Items.Add("D#5/Eb5 605-640 Hz");
            cboBox.Items.Add("E5      641-678 Hz");
            cboBox.Items.Add("F5      679-719 Hz");
            cboBox.Items.Add("F#5/Gb5 720-761 Hz");
            cboBox.Items.Add("G5      762-807 Hz");
            cboBox.Items.Add("G#5/Ab5 808-855 Hz");
            cboBox.Items.Add("A5      856-906 Hz");
            cboBox.Items.Add("A#5/Bb5 907-960 Hz");
            cboBox.Items.Add("B5      961-1017 Hz");
            cboBox.Items.Add("C5->B5  509-1017 Hz"); // index 66

            cboBox.Items.Add("C6      1018-1077 Hz"); // index 67
            cboBox.Items.Add("C#6/Db6 1078-1141 Hz");
            cboBox.Items.Add("D6      1142-1209 Hz");
            cboBox.Items.Add("D#6/Eb6 1210-1281 Hz");
            cboBox.Items.Add("E6      1282-1357 Hz");
            cboBox.Items.Add("F6      1358-1438 Hz");
            cboBox.Items.Add("F#6/Gb6 1439-1523 Hz");
            cboBox.Items.Add("G6      1524-1614 Hz");
            cboBox.Items.Add("G#6/Ab6 1615-1710 Hz");
            cboBox.Items.Add("A6      1711-1812 Hz");
            cboBox.Items.Add("A#6/Bb6 1813-1920 Hz");
            cboBox.Items.Add("B6      1921-2034 Hz");
            cboBox.Items.Add("C6->B6  1018-2034 Hz"); // index 79

            cboBox.Items.Add("C7      2035-2155 Hz"); // index 80
            cboBox.Items.Add("C#7/Db7 2156-2283 Hz");
            cboBox.Items.Add("D7      2284-2419 Hz");
            cboBox.Items.Add("D#7/Eb7 2420-2563 Hz");
            cboBox.Items.Add("E7      2564-2715 Hz");
            cboBox.Items.Add("F7      2716-2876 Hz");
            cboBox.Items.Add("F#7/Gb7 2877-3047 Hz");
            cboBox.Items.Add("G7      3048-3229 Hz");
            cboBox.Items.Add("G#7/Ab7 3230-3421 Hz");
            cboBox.Items.Add("A7      3422-3624 Hz");
            cboBox.Items.Add("A#7/Bb7 3625-3840 Hz");
            cboBox.Items.Add("B7      3841-4068 Hz");
            cboBox.Items.Add("C7->B7  2035-4068 Hz"); // index 92
            cboBox.Items.Add("C8->    4069-15000 Hz");   // index 93


            cboBox = cboDevice;
            cboBox.Items.Add("LOR");
            cboBox.Items.Add("DMX");






            cboPreset.SelectedIndex = 1;
            cboDevice.SelectedIndex = 0;

            grpChannel.Tag = 0;
            cboDevice.Tag = 0;
            cboPreset.Tag = 0;
            numHigh.Tag = 0;
            numLow.Tag = 0;
            numStart.Tag = 0;
            numEnd.Tag = 0;
            numUnit.Tag = 0;
            numChannel.Tag = 0;
            txtName.Tag = 0;
            
            channelGroups[0] = grpChannel;
            channelDevices[0] = cboDevice;
            channelPresets[0] = cboPreset;
            channelHighs[0] = numHigh;
            channelLows[0] = numLow;
            channelStarts[0] = numStart;
            channelEnds[0] = numEnd;
            channelNames[0] = txtName;
            channelUnits[0] = numUnit;
            channelChans[0] = numChannel;

            for (int i = 2; i < 33; i++)
            {
                makeNewChannel(i);
            }

            channelPresets[1].SelectedIndex = 14;
            channelPresets[2].SelectedIndex = 27;
            channelPresets[3].SelectedIndex = 40;
            channelPresets[4].SelectedIndex = 53;
            channelPresets[5].SelectedIndex = 66;
            channelPresets[6].SelectedIndex = 79;
            channelPresets[7].SelectedIndex = 92;
            channelPresets[8].SelectedIndex = 93;
            channelPresets[9].SelectedIndex = 28;
            channelPresets[10].SelectedIndex = 29;
            channelPresets[11].SelectedIndex = 30;
            channelPresets[12].SelectedIndex = 31;
            channelPresets[13].SelectedIndex = 32;
            channelPresets[14].SelectedIndex = 33;
            channelPresets[15].SelectedIndex = 34;
            channelPresets[16].SelectedIndex = 35;
            channelPresets[17].SelectedIndex = 36;
            channelPresets[18].SelectedIndex = 37;
            channelPresets[19].SelectedIndex = 38;
            channelPresets[20].SelectedIndex = 39;
            channelPresets[21].SelectedIndex = 41;
            channelPresets[22].SelectedIndex = 42;
            channelPresets[23].SelectedIndex = 43;
            channelPresets[24].SelectedIndex = 44;
            channelPresets[25].SelectedIndex = 45;
            channelPresets[26].SelectedIndex = 46;
            channelPresets[27].SelectedIndex = 47;
            channelPresets[28].SelectedIndex = 48;
            channelPresets[29].SelectedIndex = 49;
            channelPresets[30].SelectedIndex = 50;
            channelPresets[31].SelectedIndex = 51;
        
        }

        private void makeNewChannel(int newIndex)
        {

            
            
            
            GroupBox gBox = new GroupBox();
            gBox.Tag = newIndex;
            gBox.Left = grpChannel.Left;
            gBox.Height = grpChannel.Height;
            gBox.Width = grpChannel.Width;
            gBox.Top = grpChannel.Top + (grpChannel.Height + 7) * (newIndex-1);
            string title = " Channel " + newIndex.ToString();
            gBox.Text = title;
            pnlChannels.Controls.Add(gBox);
            gBox.Tag = newIndex - 1;
            channelGroups[newIndex - 1] = gBox;

            ComboBox cBox = new ComboBox();
            cBox.Tag = newIndex;
            gBox.Controls.Add(cBox);
            cBox.Left = cboDevice.Left;
            cBox.Top = cboDevice.Top;
            cBox.Height = cboDevice.Height;
            cBox.Width = cboDevice.Width;
            for (int i = 0; i < cboDevice.Items.Count; i++)
            {
                cBox.Items.Add(cboDevice.Items[i]);
            }
            cBox.SelectedIndex = cboDevice.SelectedIndex;
            cBox.Tag = newIndex - 1;
            channelDevices[newIndex - 1] = cBox;

            cBox = new ComboBox();
            cBox.Tag = newIndex;
            gBox.Controls.Add(cBox);
            cBox.Left = cboPreset.Left;
            cBox.Top = cboPreset.Top;
            cBox.Height = cboPreset.Height;
            cBox.Width = cboPreset.Width;
            cBox.Font = cboPreset.Font;
            for (int i = 0; i < cboPreset.Items.Count; i++)
            {
                cBox.Items.Add(cboPreset.Items[i]);
            }
            //cBox.Click = cboPreset.Click; // Event Handler
            cBox.SelectedIndexChanged  += new System.EventHandler(cboPreset_SelectedIndexChanged);
            cBox.Tag = newIndex - 1;
            channelPresets[newIndex - 1] = cBox;

            NumericUpDown nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numHigh.Left;
            nBox.Top = numHigh.Top;
            nBox.Height = numHigh.Height;
            nBox.Width = numHigh.Width;
            nBox.Minimum = numHigh.Minimum;
            nBox.Maximum = numHigh.Maximum;
            nBox.Value = numHigh.Value;
            nBox.Font = numHigh.Font;
            nBox.TextAlign = numHigh.TextAlign;
            nBox.Tag = newIndex - 1;
            channelHighs[newIndex - 1] = nBox;

            nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numLow.Left;
            nBox.Top = numLow.Top;
            nBox.Height = numLow.Height;
            nBox.Width = numLow.Width;
            nBox.Minimum = numLow.Minimum;
            nBox.Maximum = numLow.Maximum;
            nBox.Value = numLow.Value;
            nBox.Font = numLow.Font;
            nBox.TextAlign = numLow.TextAlign;
            nBox.Tag = newIndex - 1;
            channelLows[newIndex - 1] = nBox;

            nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numStart.Left;
            nBox.Top = numStart.Top;
            nBox.Height = numStart.Height;
            nBox.Width = numStart.Width;
            nBox.Minimum = numStart.Minimum;
            nBox.Maximum = numStart.Maximum;
            nBox.Font = numStart.Font;
            nBox.TextAlign = numStart.TextAlign;
            nBox.Tag = newIndex - 1;
            channelStarts[newIndex - 1] = nBox;

            nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numEnd.Left;
            nBox.Top = numEnd.Top;
            nBox.Height = numEnd.Height;
            nBox.Width = numEnd.Width;
            nBox.Minimum = numEnd.Minimum;
            nBox.Maximum = numEnd.Maximum;
            nBox.Font = numEnd.Font;
            nBox.TextAlign = numEnd.TextAlign;
            nBox.Tag = newIndex - 1;
            channelEnds[newIndex - 1] = nBox;

            nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numUnit.Left;
            nBox.Top = numUnit.Top;
            nBox.Height = numUnit.Height;
            nBox.Width = numUnit.Width;
            nBox.Minimum = numUnit.Minimum;
            nBox.Maximum = numUnit.Maximum;
            nBox.Value = numUnit.Value;
            nBox.Font = numUnit.Font;
            nBox.TextAlign = numUnit.TextAlign;
            nBox.Tag = newIndex - 1;
            channelUnits[newIndex - 1] = nBox;

            nBox = new NumericUpDown();
            nBox.Tag = newIndex;
            gBox.Controls.Add(nBox);
            nBox.Left = numChannel.Left;
            nBox.Top = numChannel.Top;
            nBox.Height = numChannel.Height;
            nBox.Width = numChannel.Width;
            nBox.Minimum = numChannel.Minimum;
            nBox.Maximum = numChannel.Maximum;
            nBox.Value = newIndex;
            nBox.Font = numChannel.Font;
            nBox.TextAlign = numChannel.TextAlign;
            nBox.Tag = newIndex - 1;
            channelChans[newIndex - 1] = nBox;

            TextBox tBox = new TextBox();
            tBox.Tag = newIndex;
            gBox.Controls.Add(tBox);
            tBox.Left = txtName.Left;
            tBox.Top = txtName.Top;
            tBox.Height = txtName.Height;
            tBox.Width = txtName.Width;
            tBox.Text = gBox.Text;
            tBox.Tag = newIndex - 1;
            channelNames[newIndex - 1] = tBox;

            Label lbl = new Label();
            gBox.Controls.Add (lbl);
            lbl.Left = lblPreset.Left;
            lbl.Top = lblPreset.Top;
            lbl.Width = lblPreset.Width;
            lbl.Height = lblPreset.Height;
            lbl.Text = lblPreset.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblDevice.Left;
            lbl.Top = lblDevice.Top;
            lbl.Width = lblDevice.Width;
            lbl.Height = lblDevice.Height;
            lbl.Text = lblDevice.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblHigh.Left;
            lbl.Top = lblHigh.Top;
            lbl.Width = lblHigh.Width;
            lbl.Height = lblHigh.Height;
            lbl.Text = lblHigh.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblLow.Left;
            lbl.Top = lblLow.Top;
            lbl.Width = lblLow.Width;
            lbl.Height = lblLow.Height;
            lbl.Text = lblLow.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblStart.Left;
            lbl.Top = lblStart.Top;
            lbl.Width = lblStart.Width;
            lbl.Height = lblStart.Height;
            lbl.Text = lblStart.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblEnd.Left;
            lbl.Top = lblEnd.Top;
            lbl.Width = lblEnd.Width;
            lbl.Height = lblEnd.Height;
            lbl.Text = lblEnd.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblName.Left;
            lbl.Top = lblName.Top;
            lbl.Width = lblName.Width;
            lbl.Height = lblName.Height;
            lbl.Text = lblName.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblFreq.Left;
            lbl.Top = lblFreq.Top;
            lbl.Width = lblFreq.Width;
            lbl.Height = lblFreq.Height;
            lbl.Text = lblFreq.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblDecibels.Left;
            lbl.Top = lblDecibels.Top;
            lbl.Width = lblDecibels.Width;
            lbl.Height = lblDecibels.Height;
            lbl.Text = lblDecibels.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblUnit.Left;
            lbl.Top = lblUnit.Top;
            lbl.Width = lblUnit.Width;
            lbl.Height = lblUnit.Height;
            lbl.Text = lblUnit.Text;

            lbl = new Label();
            gBox.Controls.Add(lbl);
            lbl.Left = lblChannel.Left;
            lbl.Top = lblChannel.Top;
            lbl.Width = lblChannel.Width;
            lbl.Height = lblChannel.Height;
            lbl.Text = lblChannel.Text;

            PictureBox pBox = new PictureBox();
            gBox.Controls.Add(pBox);
            pBox.Left = picColor.Left;
            pBox.Top = picColor.Top;
            pBox.Width = picColor.Width;
            pBox.Height = picColor.Height;
            pBox.Tag = newIndex - 1;
            channelColors[newIndex - 1] = pBox;




        }


        private void cboPreset_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox cBox = (ComboBox)sender;
            int iTag = Convert.ToInt16(cBox.Tag);
            NumericUpDown nStart = channelStarts[iTag];
            NumericUpDown nEnd = channelEnds[iTag];

            
            string Freqs = cBox.Text;
            if (Freqs.Length > 4)
            {
                Freqs = Freqs.Substring(0, Freqs.Length - 3);
                int iPos1 = Freqs.LastIndexOf(' ');
                Freqs = Freqs.Substring(iPos1 + 1);
                iPos1 = Freqs.IndexOf("-");
                nStart.Value = Convert.ToInt16(Freqs.Substring(0, iPos1));
                Freqs = Freqs.Substring(iPos1 + 1);
                nEnd.Value = Convert.ToInt16(Freqs);
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            dlgSave.Filter = "Musical Sequences (*.lms)|*.lms";
            dlgSave.DefaultExt = "*.lms";
            dlgSave.InitialDirectory = basePath;
            dlgSave.CheckFileExists = false;
            dlgSave.CheckPathExists = true;
            DialogResult result = dlgSave.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastSequenceFile = dlgSave.FileName;
                if (lastSequenceFile.Length > basePath.Length)
                {
                    if (lastSequenceFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtOutputFile.Text = lastSequenceFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtOutputFile.Text = lastSequenceFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)

        }

        private void btnBrowseMusic_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Audio\\";

            dlgOpen.Filter = "MP3 Songs (*.mp3)|*.mp3|WAVE Songs (*.wav)|*.wav";
            dlgOpen.DefaultExt = "*.mp3";
            dlgOpen.InitialDirectory = basePath;
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            dlgOpen.Multiselect = false;
            DialogResult result = dlgOpen.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastMusicFile = dlgOpen.FileName;
                if (lastMusicFile.Length > basePath.Length)
                {
                    if (lastMusicFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtMusicFile.Text = lastMusicFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtMusicFile.Text = lastMusicFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)

        }

        private void cmdBrowseChannelConfig_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            dlgOpen.Filter = "Channel Templates (*.lcc)|*.lcc";
            dlgOpen.DefaultExt = "*.lcc";
            dlgOpen.InitialDirectory = basePath;
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            dlgOpen.Multiselect = false;
            DialogResult result = dlgOpen.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastChannelFile = dlgOpen.FileName;
                if (lastChannelFile.Length > basePath.Length)
                {
                    if (lastChannelFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtChannelFile.Text = lastChannelFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtChannelFile.Text = lastChannelFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    readChannelConfig(txtChannelFile.Text);
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)

        }

        private void cmdBrowseFreqPreset_Click(object sender, EventArgs e)
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";

            dlgOpen.Filter = "Frequency Presets (*.freq)|*.freq";
            dlgOpen.DefaultExt = "*.freq";
            dlgOpen.InitialDirectory = basePath;
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            dlgOpen.Multiselect = false;
            DialogResult result = dlgOpen.ShowDialog();

            if (result == DialogResult.OK)
            {
                lastPresetFile = dlgOpen.FileName;
                if (lastPresetFile.Length > basePath.Length)
                {
                    if (lastPresetFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                    {
                        txtPresetFile.Text = lastPresetFile.Substring(basePath.Length);
                    }
                    else
                    {
                        txtPresetFile.Text = lastPresetFile;
                    } // End else (lastFile.Substring(0, basePath.Length).CompareTo(basePath) == 0)
                } // end if (lastFile.Length > basePath.Length)
            } // end if (result = DialogResult.OK)

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            analyzeMusicFile(txtMusicFile.Text);
            generateChannels(txtOutputFile.Text);
        }

        private int readChannelConfig(string configFilename)
        {
            int errStatus = 0;
            StreamReader reader = new StreamReader(configFilename);
            string lineIn; // line read in (does not get modified)
            int pos1 = -1; // positions of certain key text in the line
            int pos2 = -1;
            int pos3 = -1;
            string chName; // Lor Channel No. as text
            int chNo = 0; // Channel No. as int 1-16

            while ((lineIn = reader.ReadLine()) != null)
            {
                if (chNo < 32)
                {
                    pos1 = lineIn.IndexOf("<channel name=");
                    if (pos1 > 0)
                    {
                        chName = lineIn.Substring(pos1 + 15);
                        pos2 = chName.IndexOf("\"");
                        chName = chName.Substring(0,pos2);
                        TextBox tBox = channelNames[chNo];
                        tBox.Text = chName;

                        pos2 = lineIn.IndexOf(" deviceType=");
                        chName = lineIn.Substring(pos2 + 13);
                        pos3 = chName.IndexOf("\"");
                        chName = chName.Substring(0,pos3).ToUpper();
                        ComboBox cBox  = channelDevices[chNo];
                        if (chName == "LOR")
                        {
                            cBox.SelectedIndex = 0;
                        }
                        else if (chName == "DMX")
                        {
                            cBox.SelectedIndex = 1;
                        }
                        else
                        {
                            cBox.SelectedIndex = -1;
                        }

                        NumericUpDown nBox = channelUnits[chNo];
                        pos2 = lineIn.IndexOf(" unit=");
                        if (pos2 > 5)
                        {
                            chName = lineIn.Substring(pos2 + 7);
                            pos3 = chName.IndexOf("\"");
                            chName = chName.Substring(0,pos3).ToUpper();
                            nBox.Value = Convert.ToInt16(chName);
                        }
                        else
                        {
                            nBox.Value = 0;
                        }

                        nBox = channelChans[chNo];
                        pos2 = lineIn.IndexOf(" circuit=");
                        if (pos2 > 5)
                        {
                            chName = lineIn.Substring(pos2 + 10);
                            pos3 = chName.IndexOf("\"");
                            chName = chName.Substring(0,pos3).ToUpper();
                            nBox.Value = Convert.ToInt16(chName);
                        }
                        else
                        {
                            nBox.Value = 0;
                        }

                        PictureBox pBox = channelColors[chNo];
                        pos2 = lineIn.IndexOf(" color=");
                        if (pos2 > 5)
                        {
                            chName = lineIn.Substring(pos2 + 8);
                            pos3 = chName.IndexOf("\"");
                            chName = chName.Substring(0,pos3).ToUpper();
                            //pBox.BackColor  = Convert.ToUInt32  (chName);
                        }
                        else
                        { 
                            //pBox.BackColor = Grey;
                        }
    

                        chNo++;

                    }
                }
            }
            reader.Close();



            return errStatus;
        }

        private int analyzeMusicFile(string musicFilename)
        {
            int errStatus = 0;
            return errStatus;
        }
        private int generateChannels(string outputFilename)
        {
            int errStatus = 0;
            return errStatus;
        }

		private void pnlChannels_Paint(object sender, PaintEventArgs e)
		{

		}
	}



}
