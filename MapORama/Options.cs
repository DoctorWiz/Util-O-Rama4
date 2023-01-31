using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuzzORama;
using LOR4;
using FileHelper;
using System.IO;

namespace UtilORama4
{
	public partial class frmOptions : Form
	{
		public bool useFuzzy = true;
		public bool caseSensitive = true;
		public int useAlgorithms = 0;
		public long prematchAlgorithm = 2048;
		public long finalAlgorithms = 6328448;
		public int minPrematchScore = 85;
		public int minFinalMatchScore = 95;
		public bool writeLog = true;
		private Settings userSettings = Settings.Default;

		public frmOptions()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void lblFinalMatchScore_Click(object sender, EventArgs e)
		{

		}

		private void chkFinal_Changed(object sender, EventArgs e)
		{
			long algorithms = 0;

			foreach (Control ctl in grpFinalMatch.Controls)
			{
				if (ctl is CheckBox)
				{
					CheckBox chkCtl = (CheckBox)ctl;
					string ht = (string)chkCtl.Name.Substring(3);
					ht = ht.Substring(3);
					long hi = Convert.ToInt32(ht, 16);
					if (chkCtl.Checked)
					{
						algorithms |= hi;  // Set that bit
					}
					else
					{
						algorithms &= ~hi; // clear that bit (by ORing with inverse)
					}

				}
			}
			finalAlgorithms = algorithms;
			//Properties.Settings.Default.FuzzyFinalAlgorithms = useAlgorithms;
			//Properties.Settings.Default.Save();
		}

		public void SetFinalAlgorChecks(long theAlgorithms)
		{
			foreach (Control ctl in grpFinalMatch.Controls)
			{
				if (ctl is CheckBox)
				{
					CheckBox chkCtl = (CheckBox)ctl;
					//if (chkCtl.Checked)
					//{
					string ht = (string)chkCtl.Name.Substring(3);
					long hi = Convert.ToInt32(ht, 16);
					if ((theAlgorithms & hi) > 0)
					{
						chkCtl.Checked = true;
					}
					else
					{
						chkCtl.Checked = false;
					}
					//}
				}
			}




		}

		private void cboPrematch_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SetPreCombo(long theAlgorithm)
		{
		}

		private void frmOptions_Load(object sender, EventArgs e)
		{
			InitControls();
			//finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
			SetFinalAlgorChecks(finalAlgorithms);
			//prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
			SetPreCombo(prematchAlgorithm);
			useFuzzy = userSettings.FuzzyUse;
			chkUseFuzzy.Checked = useFuzzy;
			caseSensitive = userSettings.FuzzyCaseSensitive;
			chkCase.Checked = caseSensitive;
			writeLog = userSettings.FuzzyWriteLog;
			chkLog.Checked = writeLog;
			//minPrematchScore = FuzzyFunctions.SUGGESTED_MIN_PREMATCH_SCORE; // Properties.Settings.Default.FuzzyMinPrematch;
			//sldPrematch.Value = minPrematchScore;
			lblPrematchValue.Text = minPrematchScore.ToString();
			//minFinalMatchScore = FuzzyFunctions.SUGGESTED_MIN_FINAL_SCORE; // Properties.Settings.Default.FuzzyMinFinal;
			sldFinalMatch.Value = minFinalMatchScore;
			lblFinalMatchValue.Text = minFinalMatchScore.ToString();
		}

		public void SaveOptions()
		{
			finalAlgorithms = ReadFinalChoices();
			//Properties.Settings.Default.FuzzyFinalAlgorithms = finalAlgorithms;
			//Properties.Settings.Default.FuzzyPrematchAlgorithm = prematchAlgorithm;
			userSettings.FuzzyUse = chkUseFuzzy.Checked;
			userSettings.FuzzyCaseSensitive = chkCase.Checked;
			userSettings.FuzzyWriteLog = chkLog.Checked;
			//Properties.Settings.Default.FuzzyMinFinal = sldFinalMatch.Value;
			//Properties.Settings.Default.FuzzyMinPrematch = sldPrematch.Value;
			userSettings.Save();
		}

		public long ReadFinalChoices()
		{
			long finals = 0;
			foreach (Control ctl in grpFinalMatch.Controls)
			{
				if (ctl is CheckBox)
				{
					CheckBox chkCtl = (CheckBox)ctl;
					if (chkCtl.Checked)
					{
						string ht = (string)chkCtl.Name.Substring(3);
						long hi = Convert.ToInt64(ht, 16);
						finals = finals | hi;
					}
				}
			}
			return finals;
		}


		private void InitControls()
		{
			int bit = 1;
			cboPrematch.Items.Clear();



		}




		private void chkUseFuzzy_CheckedChanged(object sender, EventArgs e)
		{
			useFuzzy = chkUseFuzzy.Checked;
		}

		private void chkCase_CheckedChanged(object sender, EventArgs e)
		{
			caseSensitive = chkCase.Checked;
			if (caseSensitive)
			{
			}

		}

		private void chkLog_CheckedChanged(object sender, EventArgs e)
		{
			writeLog = chkLog.Checked;
		}

		private void sldPrematch_Scroll(object sender, EventArgs e)
		{
			minPrematchScore = sldPrematch.Value;
			lblPrematchValue.Text = sldPrematch.Value.ToString();
		}

		private void sldFinalMatch_Scroll(object sender, EventArgs e)
		{
			minFinalMatchScore = sldFinalMatch.Value;
			lblFinalMatchValue.Text = sldFinalMatch.Value.ToString();
		}




		private void cmdOK_Click(object sender, EventArgs e)
		{
			SaveOptions();
			DialogResult = DialogResult.OK;
			//this.Close();
			this.Hide();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			//this.Close();
			this.Hide();
		}

		private void btnDefaults_Click(object sender, EventArgs e)
		{
			//TODO: Pick some defaults
		}

		private void frmOptions_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult = DialogResult.Abort;
			e.Cancel = true;
			this.Hide();
		}
	}
}
