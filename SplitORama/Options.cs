using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuzzyString;
using LORUtils;
using System.IO;

namespace SplitORama
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
			long alg = 0;
			string txt = cboPrematch.Text;
			string start = txt.Substring(0, 3);

			if (start == "Cos")
			{
				alg = FuzzyString.FuzzyFunctions.USE_COSINE;
			}
			else if (start == "Pad")
			{
				alg = FuzzyString.FuzzyFunctions.USE_PADDEDHAMMING;
			}
			else if (start == "Ham")
			{
				alg = FuzzyString.FuzzyFunctions.USE_HAMMINGWIZMO;
			}
			else if (start == "Jac")
			{
				alg = FuzzyString.FuzzyFunctions.USE_JACCARD;
			}
			else if (start == "Lev")
			{
				alg = FuzzyString.FuzzyFunctions.USE_LEVENSHTEIN;
			}
			else if (start == "Dam")
			{
				alg = FuzzyString.FuzzyFunctions.USE_DAMERAULEVENSHTEIN;
			}
			else if (start == "Nor")
			{
				alg = FuzzyString.FuzzyFunctions.USE_NORMALIZEDLEVENSHTEIN;
			}
			else if (start == "Yet")
			{
				alg = FuzzyString.FuzzyFunctions.USE_YETILEVENSHTEIN;
			}
			else if (start == "Wei")
			{
				alg = FuzzyString.FuzzyFunctions.USE_WEIGHTEDLEVENSHTEIN;
			}
			else if (start == "Met")
			{
				alg = FuzzyString.FuzzyFunctions.USE_METRIC;
			}
			else if (start == "Nee")
			{
				alg = FuzzyString.FuzzyFunctions.USE_NEEDLEMANWUNSCH;
			}
			else if (start == "N-G")
			{
				alg = FuzzyString.FuzzyFunctions.USE_NGRAM;
			}
			else if (start == "Q-G")
			{
				alg = FuzzyString.FuzzyFunctions.USE_QGRAM;
			}
			else if (start == "Opt")
			{
				alg = FuzzyString.FuzzyFunctions.USE_OPTIMALSTRINGALIGNMENT;
			}
			else if (start == "Ove")
			{
				alg = FuzzyString.FuzzyFunctions.USE_OVERLAPCOEFFICIENT;
			}
			else if (start == "Sif")
			{
				alg = FuzzyString.FuzzyFunctions.USE_SIFT;
			}
			else if (start == "Sor")
			{
				alg = FuzzyString.FuzzyFunctions.USE_SORENSENDICE;
			}
			else if (txt.IndexOf("Dev") > 0)
			{
				alg = FuzzyString.FuzzyFunctions.USE_CHAPMANLENGTHDEVIATION;
			}
			else if (txt.IndexOf("Mean") > 0)
			{
				alg = FuzzyString.FuzzyFunctions.USE_CHAPMANMEANLENGTH;
			}
			else if (txt.Substring(0, 6) == "Jaro Si")
			{
				alg = FuzzyString.FuzzyFunctions.USE_JARO;
			}
			else if (txt.IndexOf("Wink") > 0)
			{
				alg = FuzzyString.FuzzyFunctions.USE_JAROWINKLER;
			}
			else if (txt.IndexOf("Seq") > 0)
			{
				alg = FuzzyString.FuzzyFunctions.USE_LONGESTCOMMONSUBSEQUENCE;
			}
			else if (txt.IndexOf("String") > 0)
			{
				alg = FuzzyString.FuzzyFunctions.USE_LONGESTCOMMONSUBSTRING;
			}

			if (alg == 0)
			{
				string msg = "Houston, We have no match!";
			}
			else
			{
				prematchAlgorithm = alg;
				//Properties.Settings.Default.FuzzyPrematchAlgorithm = alg;
				//Properties.Settings.Default.Save();
			}

		}

		private void SetPreCombo(long theAlgorithm)
		{
			int idx = utils.UNDEFINED;
			switch (theAlgorithm)
			{
				case FuzzyFunctions.USE_CHAPMANLENGTHDEVIATION:
					idx = 0;
					break;
				case FuzzyFunctions.USE_CHAPMANMEANLENGTH:
					idx = 1;
					break;
				case FuzzyFunctions.USE_COSINE:
					idx = 1;
					break;
				case FuzzyFunctions.USE_PADDEDHAMMING:
					idx = 3;
					break;
				case FuzzyFunctions.USE_HAMMINGWIZMO:
					idx = 4;
					break;
				case FuzzyFunctions.USE_JACCARD:
					idx = 5;
					break;
				case FuzzyFunctions.USE_JARO:
					idx = 6;
					break;
				case FuzzyFunctions.USE_JAROWINKLER:
					idx = 7;
					break;
				case FuzzyFunctions.USE_LEVENSHTEIN:
					idx = 8;
					break;
				case FuzzyFunctions.USE_NORMALIZEDLEVENSHTEIN:
					idx = 9;
					break;
				case FuzzyFunctions.USE_DAMERAULEVENSHTEIN:
					idx = 10;
					break;
				case FuzzyFunctions.USE_YETILEVENSHTEIN:
					idx = 11;
					break;
				case FuzzyFunctions.USE_WEIGHTEDLEVENSHTEIN:
					idx = 12;
					break;
				case FuzzyFunctions.USE_LONGESTCOMMONSUBSEQUENCE:
					idx = 13;
					break;
				case FuzzyFunctions.USE_LONGESTCOMMONSUBSTRING:
					idx = 14;
					break;
				case FuzzyFunctions.USE_METRIC:
					idx = 15;
					break;
				case FuzzyFunctions.USE_NEEDLEMANWUNSCH:
					idx = 16;
					break;
				case FuzzyFunctions.USE_NGRAM:
					idx = 17;
					break;
				case FuzzyFunctions.USE_QGRAM:
					idx = 18;
					break;
				case FuzzyFunctions.USE_OPTIMALSTRINGALIGNMENT:
					idx = 19;
					break;
				case FuzzyFunctions.USE_OVERLAPCOEFFICIENT:
					idx = 20;
					break;
				case FuzzyFunctions.USE_SIFT:
					idx = 21;
					break;
				case FuzzyFunctions.USE_SORENSENDICE:
					idx = 22;
					break;
			}

			if (idx < 0)
			{
				string msg = "Houston, we have no match!";
			}
			else
			{
				cboPrematch.SelectedIndex = idx;
			}

		}

		private void frmOptions_Load(object sender, EventArgs e)
		{
			InitControls();
			LoadFuzzyOptions();
			SetTheControlsForTheHeartOfTheSun();
		}

		private void LoadFuzzyOptions()
		{ 
			finalAlgorithms = Properties.Settings.Default.FuzzyFinalAlgorithms;
			prematchAlgorithm = Properties.Settings.Default.FuzzyPrematchAlgorithm;
			useFuzzy = Properties.Settings.Default.FuzzyUse;
			caseSensitive = Properties.Settings.Default.FuzzyCaseSensitive;
			writeLog = Properties.Settings.Default.FuzzyWriteLog;
			minPrematchScore = Properties.Settings.Default.FuzzyMinPrematch;
			minFinalMatchScore = Properties.Settings.Default.FuzzyMinFinal;
		}

		private void SetTheControlsForTheHeartOfTheSun()
		{
			SetFinalAlgorChecks(finalAlgorithms);
			SetPreCombo(prematchAlgorithm);
			chkUseFuzzy.Checked = useFuzzy;
			chkCase.Checked = caseSensitive;
			chkLog.Checked = writeLog;
			sldPrematch.Value = minPrematchScore;
			lblPrematchValue.Text = minPrematchScore.ToString();
			sldFinalMatch.Value = minFinalMatchScore;
			lblFinalMatchValue.Text = minFinalMatchScore.ToString();

		}

		public void SaveFuzzyOptions()
		{
			finalAlgorithms = ReadFinalChoices();
			Properties.Settings.Default.FuzzyFinalAlgorithms = finalAlgorithms;
			Properties.Settings.Default.FuzzyPrematchAlgorithm = prematchAlgorithm;
			Properties.Settings.Default.FuzzyUse = chkUseFuzzy.Checked;
			Properties.Settings.Default.FuzzyCaseSensitive = chkCase.Checked;
			Properties.Settings.Default.FuzzyWriteLog = chkLog.Checked;
			Properties.Settings.Default.FuzzyMinFinal = sldFinalMatch.Value;
			Properties.Settings.Default.FuzzyMinPrematch = sldPrematch.Value;
			Properties.Settings.Default.Save();
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
			for (int p = 0; p < FuzzyFunctions.ALGORITHM_COUNT; p++)
			{
				string n = "chk" + bit.ToString("X6");
				string d = FuzzyFunctions.AlgorithmNames(bit);
				cboPrematch.Items.Add(d);
				Control c = grpFinalMatch.Controls[n];
				c.Text = d;
				c.Visible = true;
				bit <<= 1;
			}



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
				finalAlgorithms &= (FuzzyFunctions.USE_CASEINSENSITIVE-1);
				prematchAlgorithm &= (FuzzyFunctions.USE_CASEINSENSITIVE - 1);
			}
			else
			{
				finalAlgorithms |= FuzzyFunctions.USE_CASEINSENSITIVE;
				prematchAlgorithm |= FuzzyFunctions.USE_CASEINSENSITIVE;
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
			SaveFuzzyOptions();
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
