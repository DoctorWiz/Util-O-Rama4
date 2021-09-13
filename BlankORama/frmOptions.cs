using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuneORama
{
	public partial class frmOptions : Form
	{
		public frmOptions()
		{
			InitializeComponent();
		}

		public bool useFuzzy = true;
		public int preMatchScore = 800;
		public int finalMatchScore = 950;
		//public const int SAVEmixedDisplay = 1;
		//public const int SAVEcrgDisplay = 2;
		//public const int SAVEcrgAlpha = 3;
		//public byte saveFormat = frmSplit.SAVEmixedDisplay;
		private bool modeNames = true;


		private void frmOptions_Load(object sender, EventArgs e)
		{
			this.Width = 322;
		}

		public void InitForm(bool namesMode)
		{
			modeNames = namesMode;
			if (modeNames)
			{
				grpNames.Left = 12;
				optFuzzy.Checked = useFuzzy;
				sldPreMatch.Value = preMatchScore;
				sldFinalMatch.Value = finalMatchScore;
				lblPreMatchScore.Text = preMatchScore.ToString();
				lblFinalMatchScore.Text = finalMatchScore.ToString();
				grpSave.Visible = false;
				grpSave.Enabled = false;
				grpNames.Visible = true;
				grpNames.Enabled = true;
			}
			else
			{
				grpSave.Left = 12;
				//if (saveFormat == frmSplit.SAVEmixedDisplay) optMixedDisplay.Checked = true;
				//if (saveFormat == frmSplit.SAVEcrgDisplay) optCRGDisplay.Checked = true;
				//if (saveFormat == frmSplit.SAVEcrgAlpha) optCRGAlpha.Checked = true;
				grpNames.Visible = false;
				grpNames.Enabled = false;
				grpSave.Visible = true;
				grpSave.Enabled = true;
			}
		}

		private void sldPreMatch_Scroll(object sender, EventArgs e)
		{
			lblPreMatchScore.Text = "." + sldPreMatch.Value.ToString();
		}

		private void sldFinalMatch_Scroll(object sender, EventArgs e)
		{
			lblFinalMatchScore.Text = "." + sldFinalMatch.Value.ToString();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (modeNames)
			{
				useFuzzy = optFuzzy.Checked;
				if (useFuzzy)
				{
					preMatchScore = sldPreMatch.Value;
					finalMatchScore = sldFinalMatch.Value;
				}
			}
			else
			{
				//if (optMixedDisplay.Checked) saveFormat = frmSplit.SAVEmixedDisplay;
				//if (optCRGDisplay.Checked) saveFormat = frmSplit.SAVEcrgDisplay;
				//if (optCRGAlpha.Checked) saveFormat = frmSplit.SAVEcrgAlpha;
			}
			DialogResult = DialogResult.OK;
			this.Hide();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Hide();
		}

		private void optExact_CheckedChanged(object sender, EventArgs e)
		{
			//sldFinalMatch.Enabled = !optExact.Checked;
			//sldPreMatch.Enabled = !optExact.Checked;
		}

		private void optFuzzy_CheckedChanged(object sender, EventArgs e)
		{
			sldPreMatch.Enabled = optFuzzy.Checked;
			sldFinalMatch.Enabled = optFuzzy.Checked;
			if (optFuzzy.Checked)
			{
				lblPreMatchSlider.ForeColor = SystemColors.WindowText;
			}
			else
			{
				lblPreMatchSlider.ForeColor = SystemColors.GrayText;
			}
			lblFinalMatchSlider.ForeColor = lblPreMatchSlider.ForeColor;
			lblPreMatchScore.ForeColor = lblPreMatchSlider.ForeColor;
			lblFinalMatchScore.ForeColor = lblPreMatchSlider.ForeColor;
		}
	}
}
