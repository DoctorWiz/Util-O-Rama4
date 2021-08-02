using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChristmassTimingTrax
{
    public partial class FrequencySelection : Form
    {
        #region Fields
        public IList<CheckBox> FormCheckBoxes = new List<CheckBox>();
        public IList<Button> FormButtons = new List<Button>();
        private static StringCollection SelectedFrequencies;
        public static int MaxCheckboxes = 0;
        #endregion

        public FrequencySelection()
        {
            InitializeComponent();
            SelectedFrequencies = new StringCollection();
        }

        public static FrequencySelection SetupFrequencyForm()
        {
            FrequencySelection frFrequency = new FrequencySelection();
            frFrequency.Size = new Size(500, 465);
            // Add Instructions to the top of the form.
            Label lblTitle = new Label();
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Select the frequencies you want to generate timing files for.";
            lblTitle.Location = new Point(95, 3);
            lblTitle.Size = new Size(285, 13);
            frFrequency.Controls.Add(lblTitle);

            // Add Check-boxes to the form that allows the user to select the frequencies to generate timing files for.
            DataTable dtInput = AudacityForm.LastInputFileData;
            DataView dvInput = dtInput.DefaultView;
            dvInput.Sort = "Frequency ASC";
            dtInput = dvInput.ToTable();
            int iX = 13;
            int iY = 25;
            int iRow = 1;
            int iCol = 1;
            int iBoxCount = 0;
            var dtInputGrouped = from row in dtInput.AsEnumerable()
                                 group row by row.Field<int>("Frequency") into grpFrequency
                                 select new
                                 {
                                     Frequency = grpFrequency.Key,
                                     FrequencyCount = grpFrequency.Count()
                                 };
            foreach (var row in dtInputGrouped)
            {
                int iFreqCount = row.FrequencyCount;
                int iFreq = row.Frequency;
                string sFreqName = MIDI.GetFrequencyName(iFreq);
                string chkLabel = string.Format("[{0}] {1} ({2})", iFreq.ToString(), sFreqName.Replace("_", ""), iFreqCount.ToString());
                if (iCol < 4)
                {
                    if ((iX != 13) || ((iX == 13) && (iCol == 2)))
                    {
                        iX += 156;
                    }
                    iCol++;
                }
                else
                {
                    iCol = 1; iRow++; iX = 13; iY += 24;
                }
                CheckBox chbFrequency = new CheckBox();
                chbFrequency.Name = "chk" + iFreq.ToString();
                chbFrequency.Tag = iFreq.ToString();
                chbFrequency.Text = chkLabel;
                chbFrequency.AutoSize = false;
                chbFrequency.Size = new Size(155, 17);
                chbFrequency.Location = new Point(iX, iY);
                frFrequency.Controls.Add(chbFrequency);
                frFrequency.FormCheckBoxes.Add(chbFrequency);
                iBoxCount++;
            }
            
            // Add buttons to the bottom of the form.
            // Select All - 80, 25 / 12, 390
            Button btn = new Button();
            btn.Name = "btnSelectAll";
            btn.Text = "Select All";
            btn.Size = new Size(80, 25);
            btn.Location = new Point(12, 390);
            frFrequency.Controls.Add(btn);
            frFrequency.FormButtons.Add(btn);

            // Clear Selections - 110, 25 / 98, 390
            btn = new Button();
            btn.Name = "btnSelectNone";
            btn.Text = "Clear Selections";
            btn.Size = new Size(110, 25);
            btn.Location = new Point(98, 390);
            frFrequency.Controls.Add(btn);
            frFrequency.FormButtons.Add(btn);

            // Continue - 80, 25  / 306, 390
            btn = new Button();
            btn.Name = "btnContinue";
            btn.Text = "Continue";
            btn.DialogResult = DialogResult.OK;
            btn.Size = new Size(80, 25);
            btn.Location = new Point(306, 390);
            frFrequency.Controls.Add(btn);
            frFrequency.FormButtons.Add(btn);

            // Cancel - 80, 25 / 392, 390
            btn = new Button();
            btn.Name = "btnCancel";
            btn.Text = "Cancel";
            btn.DialogResult = DialogResult.Cancel;
            btn.Size = new Size(80, 25);
            btn.Location = new Point(392, 390);
            frFrequency.Controls.Add(btn);
            frFrequency.FormButtons.Add(btn);

            return frFrequency;
        }

        private void FrequencySelection_Shown(object sender, EventArgs e)
        {
            MaxCheckboxes = FormCheckBoxes.Count();
            foreach(CheckBox cb in FormCheckBoxes)
            {
                cb.CheckedChanged += CheckBox_Changed;
            }
            foreach (Button btn in FormButtons)
            {
                switch (btn.Name)
                {
                    case "btnContinue":
                        btn.Click += btnContinue_Click;
                        break;
                    case "btnSelectNone":
                        btn.Click += btnSelectNone_Click;
                        break;
                    case "btnSelectAll":
                        btn.Click += btnSelectAll_Click;
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Globals.LastSelectedFrequencies = SelectedFrequencies;
            AudacityForm.LastSelectedFrequencies = SelectedFrequencies;
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chkb in FormCheckBoxes)
            {
                chkb.Checked = false;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chkb in FormCheckBoxes)
            {
                chkb.Checked = true;
            }
        }

        protected void CheckBox_Changed(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked)
                SelectedFrequencies.Add(chk.Tag.ToString());
            else
                SelectedFrequencies.Remove(chk.Tag.ToString());

            if(SelectedFrequencies.Count == 0)
            {
                var bContinue = FormButtons.Where(btn => btn.Name == "Continue");
                var bSelectAll = FormButtons.Where(btn => btn.Name == "SelectAll");
                var bSelectNone = FormButtons.Where(btn => btn.Name == "SelectNone");
                foreach (Button btn in bContinue) btn.Enabled = false;
                foreach (Button btn in bSelectAll) btn.Enabled = true;
                foreach (Button btn in bSelectNone) btn.Enabled = false;
            }
            else
            {
                var bContinue = FormButtons.Where(btn => btn.Name == "Continue");
                var bSelectAll = FormButtons.Where(btn => btn.Name == "SelectAll");
                var bSelectNone = FormButtons.Where(btn => btn.Name == "SelectNone");
                foreach (Button btn in bContinue) btn.Enabled = true;
                foreach (Button btn in bSelectAll) btn.Enabled = true;
                foreach (Button btn in bSelectNone) btn.Enabled = false;
                if (SelectedFrequencies.Count != MaxCheckboxes)
                {
                    foreach (Button btn in bSelectAll) btn.Enabled = true;
                }
            }
        }
    }
}
