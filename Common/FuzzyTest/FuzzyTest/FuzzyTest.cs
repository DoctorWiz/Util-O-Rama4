using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using FuzzyString;
//using FuzzyStringW;


namespace FuzzyTest
{
	public partial class frmFuzzy : Form
	{
		private string[] List1 = {"Big Stars","Tomato Tree A1","Red Roof Outline","Mega Tree 1","Mega Tree 2","Tomato Tree #B5",
															"Super Spinner","Wheel of Dharma","Nativity Flood","Little Stars","Blue Roof Outline","Spiral Trees Left",
															"Spiral Tree Right","Sidewalk Flood","Laser Projector","Monster Spinner","Top Snowflakes","Middle Snowflakes",
															"Super Silly Santa","Plywood Cutouts","Flying Spaghetti Monster","Neon Peace Sign","Red Rope Light","Blue Rope Lights" };

		private string[] List2 = {"Big stars","Tomato Tree A-1","Red-Roof Outline","Mega Tree 01","Mega Tree 2.","Tomato Tree B5",
															"Super-Spinner","Wheel O' Dharma","Nativity Floods","Little Starz","Blue Roof Outlines","Spiral Trees on the Left",
															"Spiral Tree Far Right","Sidewalk Floodlight","Laser Projector!!","Monstor Spinner","Topp Snowflakes","Middle snwflak",
															"SuperSillySanta","Wood Cutouts","Spaghetti Flying Monster","Neon Peez Sign","Redish Rope Light","Blue-Rope light 2" };






		public frmFuzzy()
		{
			InitializeComponent();
		}

		private void frmFuzzy_Load(object sender, EventArgs e)
		{
			/*
			chkChapmanLength.Checked						= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_CHAPMANLENGTHDEVIATION) > 0);
			chkChapmanMean.Checked							= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_CHAPMANMEANLENGTH) > 0);
			chkCosine.Checked										= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_COSINE) > 0);
			chkPaddedHamming.Checked						= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_PADDEDHAMMING) > 0);
			chkHammingWizmo.Checked							= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_HAMMINGWIZMO) > 0);
			chkJaccard.Checked									= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_JACCARD) > 0);
			chkJaroSimilarity.Checked						= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_JARO) > 0);
			chkJaroWinkler.Checked							= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_JAROWINKLER) > 0);
			chkLevenshtein.Checked							= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_LEVENSHTEIN) > 0);
			chkNormalizedLevenshtein.Checked		= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_NORMALIZEDLEVENSHTEIN) > 0);
			chkDamerauLevenshtein.Checked				= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_DAMERAULEVENSHTEIN) > 0);
			chkYetiLevenshtein.Checked					= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_YETILEVENSHTEIN) > 0);
			chkWeightedLevenshtein.Checked			= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_WEIGHTEDLEVENSHTEIN) > 0);
			chkLongestSubsequence.Checked				= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_LONGESTCOMMONSUBSEQUENCE) > 0);
			chkLongestSubstring.Checked					= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_LONGESTCOMMONSUBSTRING) > 0);
			chkMetric.Checked										= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_METRIC) > 0);
			chkNeedlemanWunsch.Checked					= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_NEEDLEMANWUNSCH) > 0);
			chkNGram.Checked										= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_NGRAM) > 0);
			chkQGram.Checked										= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_QGRAM) > 0);
			chkOptimalString.Checked						= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_OPTIMALSTRINGALIGNMENT) > 0);
			chkOverlapCoefficient.Checked				= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_OVERLAPCOEFFICIENT) > 0);
			chkSift.Checked											=	((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_SIFT) > 0);
			chkSorensenDice.Checked							= ((FuzzyFunctions.USE_ALLSIMILARITIES & FuzzyFunctions.USE_SORENSENDICE) > 0);

			*/

		}

		private void cmdTest_Click(object sender, EventArgs e)
		{
			const string MASK = "###0.0000;";
			const string TMASC = "0.000us";

			txtString1.Enabled = false;
			txtString2.Enabled = false;
			cmdTest.Enabled = false;
			this.Cursor = Cursors.WaitCursor;
			this.Refresh();
			long options = 0;
			long msStart = 0;
			long msEnd = 0;
			Stopwatch sw = new Stopwatch();
			double avgTime = 0;


			//	List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

			// Choose which algorithms should weigh in for the comparison
			//options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
			//options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
			//options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);

			// Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
			//var tolerance = FuzzyStringTolerance.Strong;
			string source = txtString1.Text;
			string target = txtString2.Text;

			double thisScore = 0;
			double totalScore = 0;
			int scoreCount = 0;

			// Chapman Length Similarity
			if (chkChapmanLength.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.ChapmanLengthDeviationSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_CHAPMANLENGTHDEVIATION);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeChapmanLength.Text = avgTime.ToString(TMASC);
				lblScoreChapmanLength.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Chapman Mean Length Similarity
			if (chkChapmanMean.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.ChapmanMeanLengthSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_CHAPMANMEANLENGTH);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeChapmanMean.Text = avgTime.ToString(TMASC);
				lblScoreChapmanMean.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Cosine Similarity
			if (chkCosine.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.CosineSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeCosine.Text = avgTime.ToString(TMASC);
				lblScoreCosine.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Padded Hamming Similarity
			if (chkPaddedHamming.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.PaddedHammingSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimePaddedHamming.Text = avgTime.ToString(TMASC);
				lblScorePaddedHamming.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Hamming Wizmo Similarity
			if (chkHammingWizmo.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.HammingWizmoSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeHammingWizmo.Text = avgTime.ToString(TMASC);
				lblScoreHammingWizmo.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Jaccard Similarity
			if (chkJaccard.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.JaccardSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeJaccard.Text = avgTime.ToString(TMASC);
				lblScoreJaccard.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Jaro Similarity
			if (chkJaroSimilarity.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.JaroSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeJaroSimilarity.Text = avgTime.ToString(TMASC);
				lblScoreJaroSimilarity.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Jaro-Winkler Similarity
			if (chkJaroWinkler.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.JaroWinklerSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeJaroWinkler.Text = avgTime.ToString(TMASC);
				lblScoreJaroWinkler.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// [Basic] Levenshtein Similarity
			if (chkLevenshtein.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.LevenshteinSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeLevenshtein.Text = avgTime.ToString(TMASC);
				lblScoreLevenshtein.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Normalized Levenshtein Similarity
			if (chkNormalizedLevenshtein.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.NormalizedLevenshteinSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeNormalizedLevenshtein.Text = avgTime.ToString(TMASC);
				lblScoreNormalizedLevenshtein.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Damerau-Levenshtein Similarity
			if (chkDamerauLevenshtein.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.DamerauLevenshteinSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeDamerauLevenshtein.Text = avgTime.ToString(TMASC);
				lblScoreDamerauLevenshtein.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Yeti-Levenshtein Similarity
			if (chkYetiLevenshtein.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.YetiLevenshteinSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeYetiLevenshtein.Text = avgTime.ToString(TMASC);
				lblScoreYetiLevenshtein.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Weighted Levenshtein Similarity
			if (chkWeightedLevenshtein.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.WeightedLevenshteinSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_WEIGHTEDLEVENSHTEIN);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeWeightedLevenshtein.Text = avgTime.ToString(TMASC);
				lblScoreWeightedLevenshtein.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Longest Common Subsequence Similarity
			if (chkLongestSubsequence.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.LongestCommonSubsequenceSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_LONGESTCOMMONSUBSEQUENCE);
					//double m7 = m7a / big;
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeLongestSubsequence.Text = avgTime.ToString(TMASC);
				lblScoreLongestSubsequence.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Longest Common Substring Similarity
			if (chkLongestSubstring.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.LongestCommonSubstringSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_LONGESTCOMMONSUBSTRING);
					//double m7 = m7a / big;
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeLongestSubstring.Text = avgTime.ToString(TMASC);
				lblScoreLongestCommonSubstring.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Metric Similarity
			if (chkMetric.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.MetricSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_METRIC);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeMetric.Text = avgTime.ToString(TMASC);
				lblScoreMetric.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Needleman-Wunsch Similarity
			if (chkNeedlemanWunsch.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.NeedlemanWunschSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeNeedlemanWunsch.Text = avgTime.ToString(TMASC);
				lblScoreNeedlemanWunsch.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// N-Gram Similarity
			if (chkNGram.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.NGramSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeNGram.Text = avgTime.ToString(TMASC);
				lblScoreNGram.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Q-Gram Similarity
			if (chkQGram.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					//thisScore = source.QGramSimilarity(target);
					thisScore = source.RankEquality(target, FuzzyFunctions.USE_QGRAM);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeQGram.Text = avgTime.ToString(TMASC);
				lblScoreQGram.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Optimal String Alignment Similarity
			if (chkOptimalString.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.OptimalStringAlignmentSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeOptimalString.Text = avgTime.ToString(TMASC);
				lblScoreOptimalString.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Overlap Coefficient Similarity
			if (chkOverlapCoefficient.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.OverlapCoefficientSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeOverlapCoefficient.Text = avgTime.ToString(TMASC);
				lblScoreOverlapCoefficient.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Sift Similarity
			if (chkSift.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.SiftSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeSift.Text = avgTime.ToString(TMASC);
				lblScoreSift.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}

			// Sorensen-Dice Similarity
			if (chkSorensenDice.Checked)
			{
				sw = new Stopwatch();
				sw.Start();
				for (int c = 0; c < 1000; c++)
				{
					thisScore = source.SorensenDiceSimilarity(target);
				}
				sw.Stop();
				avgTime = ((double)sw.ElapsedTicks) / 100;
				lblTimeSorensenDice.Text = avgTime.ToString(TMASC);
				lblScoreSorensenDice.Text = thisScore.ToString(MASK);
				totalScore += thisScore;
				scoreCount++;
				this.Refresh();
			}


			//double m99 = source.RankEquality(target, options);
			//lblScoreRankEquality.Text = m99.ToString(MASK);

			txtString1.Enabled = true;
			txtString2.Enabled = true;
			cmdTest.Enabled = true;
			this.Cursor = Cursors.Default;
			//this.Refresh();


		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void lblChapmanMean_Click(object sender, EventArgs e)
		{

		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			chkChapmanLength.Checked					= false;
			chkChapmanMean.Checked						= false;
			chkCosine.Checked									= false;
			chkPaddedHamming.Checked					= false;
			chkHammingWizmo.Checked						= false;
			chkJaccard.Checked								= false;
			chkJaroSimilarity.Checked					= false;
			chkJaroWinkler.Checked						= false;
			chkLevenshtein.Checked						= false;
			chkNormalizedLevenshtein.Checked	= false;
			chkDamerauLevenshtein.Checked			= false;
			chkYetiLevenshtein.Checked				= false;
			chkWeightedLevenshtein.Checked		= false;
			chkLongestSubsequence.Checked			= false;
			chkLongestSubstring.Checked				= false;
			chkMetric.Checked									= false;
			chkNeedlemanWunsch.Checked				= false;
			chkNGram.Checked									= false;
			chkQGram.Checked									= false;
			chkOptimalString.Checked					= false;
			chkOverlapCoefficient.Checked			= false;
			chkSift.Checked										= false;
			chkSorensenDice.Checked						= false;
		}

		private void btnSuper_Click(object sender, EventArgs e)
		{
			int c = 0;  // Count
			Stopwatch sw = null;
			double score = 0;
			// Array to store scores
			double[,,] scores = new double[24,24,24];
			const string fmtsc = "0.0000";
			long[] tix = new long[24];
			// Compare every item in List1, to every item in List2, using every algorithm
			for (int al = 0; al < 23; al++)
			{
				double sqr = 0;
				sqr = Math.Pow(2D, (double)al);
				long opt = Convert.ToInt64(sqr);
				sw = new Stopwatch();
				sw.Start();
				for (int l1 = 0; l1 < 24; l1++)
				{
					for (int l2 = 0; l2 < 24; l2++)
					{
						// Fuzzy Compare current items with current algorithm
						score = List1[l1].RankEquality(List2[l2], opt);
						// Save the score to be analyzed later
						scores[al, l1, l2] = score;
						c++;
						//lblAnalysis.Text = c.ToString() + " of 13248";
					} // End List2 Loop
				} // End List1 Loop
				sw.Stop();
				// Note: A tick is 100 nanoseconds, 10 ticks is a microsecond (us)
				tix[al] = sw.ElapsedTicks;

			} // End Algorithm Loop
				// Done with comparison rankings

			// Find BEST matches using each algorithm from List1 to List2
			double[,] scr1 = new double[24,24];
			int[,] mat1 = new int[24,24];
			for (int al = 0; al < 23; al++)
			{
				for (int l1 = 0; l1 < 24; l1++)
				{
					double hi = -1;
					int lm = -1;
					for (int l2=0; l2<24; l2++)
					{
						double sc = scores[al, l1, l2];
						if (sc > hi)
						{
							hi = sc;
							lm = l2;
						}
					} // End List2 Loop
					scr1[al,l1] = hi;
					mat1[al,l1] = lm;
				} // End List1 Loop
			} // End Algorithm Loop
			// Done finding BEST matches




			// Create Report
			string fileName = "W:\\Documents\\SourceCode\\Windows\\UtilORama4\\Common\\FuzzyTest\\AnalysisResults.txt";
			StreamWriter writer = new StreamWriter(fileName);
			StringBuilder lineOut = new StringBuilder("");
			for (int al=0; al<23; al++)
			{
				double sqr = 0;
				sqr = Math.Pow(2D, (double)al);
				long opt = Convert.ToInt64(sqr);
				string aName = FuzzyFunctions.AlgorithmNames(opt);
				lineOut.Clear();
				lineOut.Append("**Algorithm ");
				lineOut.Append(aName);
				writer.WriteLine(lineOut.ToString());
				double ts = 0;
				double ws = 100;
				int cc = 0;
				for (int l1 = 0; l1 < 24; l1++)
				{
					lineOut.Clear();
					if (mat1[al,l1] == l1)
					{
						lineOut.Append("*   ");
						ts += scr1[al, l1];
						cc++;
						if (scr1[al,l1] < ws)
						{
							ws = scr1[al, l1];
						}
					}
					else
					{
						lineOut.Append("  X ");
					}
					lineOut.Append("Best match to '");
					lineOut.Append(List1[l1]);
					lineOut.Append("' was '");
					lineOut.Append(List2[mat1[al, l1]]);
					lineOut.Append("' with a score of ");
					lineOut.Append(scr1[al, l1].ToString(fmtsc));
					writer.WriteLine(lineOut.ToString());
				}
				lineOut.Clear();
				lineOut.Append(" --   ");
				lineOut.Append(cc.ToString());
				lineOut.Append(" of 24 Correct in ");
				lineOut.Append(tix[al]);
				lineOut.Append(" ticks with average score of ");
				double av = ts / cc;
				lineOut.Append(av.ToString(fmtsc));
				lineOut.Append(" and worst matching score of ");
				lineOut.Append(ws.ToString(fmtsc));

				writer.WriteLine(lineOut);

				
			} // End Algorithm Loop

			writer.Close();

			System.Diagnostics.Process.Start(@fileName);




		}


		private void foo(string source, string target)
		{
			double minScore = 0.4D;
			double maxScore = 0.75D;
			double thisScore = 0.5D;
			double WLscore = 0.5D;

			WLscore = source.YetiLevenshteinSimilarity(target);
			maxScore = WLscore + (1.0D - thisScore) / 2.0D;
			minScore = WLscore / 3.0D;
			minScore = Math.Max(minScore, 0.4D);
		}

		private void btnAvg_Click(object sender, EventArgs e)
		{
			int c = 0;  // Count
			Stopwatch sw = null;
			double score = 0;
			// Array to store scores
			double[,,] scores = new double[24, 24, 24];
			const string fmtsc = "0.0000";
			long[] tix = new long[24];
			double[] min = new double[24];
			// Compare every item in List1, to every item in List2, using every algorithm
			for (int al = 0; al < 23; al++)
			{
				min[al] = 100;
				double sqr = 0;
				sqr = Math.Pow(2D, (double)al);
				long opt = Convert.ToInt64(sqr);
				//sw = new Stopwatch();
				//sw.Start();
				for (int l1 = 0; l1 < 24; l1++)
				{
					score = List1[l1].RankEquality(List2[l1],opt);
					scores[al, l1, 0] = score;
					if (score < min[al])
					{
						if (score > 0.1D)
						{
							min[al] = score;
						}
					}
					c++;
				} // End List1 Loop
				//sw.Stop();
				// Note: A tick is 100 nanoseconds, 10 ticks is a microsecond (us)
				//tix[al] = sw.ElapsedTicks;

			} // End Algorithm Loop
				// Done with comparison rankings




			// Create Report
			string fileName = "W:\\Documents\\SourceCode\\Windows\\UtilORama4\\Common\\FuzzyTest\\MinMatchScores.txt";
			StreamWriter writer = new StreamWriter(fileName);
			StringBuilder lineOut = new StringBuilder("");
			for (int al = 0; al < 23; al++)
			{
				double sqr = 0;
				sqr = Math.Pow(2D, (double)al);
				long opt = Convert.ToInt64(sqr);
				string aName = FuzzyFunctions.AlgorithmNames(opt);
				lineOut.Clear();
				lineOut.Append("**Algorithm ");
				lineOut.Append(aName);
				writer.WriteLine(lineOut.ToString());
				lineOut.Clear();
				lineOut.Append("Min Correct Score was ");
				lineOut.Append(min[al].ToString(fmtsc));
				writer.WriteLine(lineOut);
			} // End Algorithm Loop

			writer.Close();

			System.Diagnostics.Process.Start(@fileName);






		}
	}
}
