using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FuzzyString
{
	public partial class FuzzyFunctions
	{
		//! NOT AS GOOD AS THE OTHER NEEDLEMAN-WUNSCH
		public static double NeedlemanWunschSimilarity(this string source, string target)
		{
			double score = NeedlemanWunschRanking(source, target);
			double m = Math.Min(source.Length, target.Length);
			return score /m-1;

		}

		public static double NeedlemanWunschRanking(string source, string target)
		{
			int sourceCharCount = source.Length + 1;
			int targetCharCount = target.Length + 1;

			int[,] scoringMatrix = new int[targetCharCount, sourceCharCount];

			//Initailization Step - filled with 0 for the first row and the first column of matrix
			for (int i = 0; i < targetCharCount; i++)
			{
				for (int j = 0; j < sourceCharCount; j++)
				{
					scoringMatrix[i, j] = 0;
				}
				// scoringMatrix[i, 0] = 0; 
			}

			//Matrix Fill Step
			for (int i = 1; i < targetCharCount; i++)
			{
				for (int j = 1; j < sourceCharCount; j++)
				{
					int scroeDiag = 0;
					if (source.Substring(j - 1, 1) == target.Substring(i - 1, 1))
						scroeDiag = scoringMatrix[i - 1, j - 1] + 2;
					else
						scroeDiag = scoringMatrix[i - 1, j - 1] + -1;

					int scroeLeft = scoringMatrix[i, j - 1] - 2;
					int scroeUp = scoringMatrix[i - 1, j] - 2;

					int maxScore = Math.Max(Math.Max(scroeDiag, scroeLeft), scroeUp);

					scoringMatrix[i, j] = maxScore;
				}
			}

			//Traceback Step
			char[] targetSeqArray = target.ToCharArray();
			char[] sourceSeqArray = source.ToCharArray();

			string AlignmentA = string.Empty;
			string AlignmentB = string.Empty;
			int m = targetCharCount - 1;
			int n = sourceCharCount - 1;
			while (m > 0 || n > 0)
			{
				int scroeDiag = 0;

				if (m == 0 && n > 0)
				{
					AlignmentA = sourceSeqArray[n - 1] + AlignmentA;
					AlignmentB = "-" + AlignmentB;
					n = n - 1;
				}
				else if (n == 0 && m > 0)
				{
					AlignmentA = "-" + AlignmentA;
					AlignmentB = targetSeqArray[m - 1] + AlignmentB;
					m = m - 1;
				}
				else
				{
					//Remembering that the scoring scheme is +2 for a match, -1 for a mismatch, and -2 for a gap
					if (targetSeqArray[m - 1] == sourceSeqArray[n - 1])
						scroeDiag = 2;
					else
						scroeDiag = -1;

					if (m > 0 && n > 0 && scoringMatrix[m, n] == scoringMatrix[m - 1, n - 1] + scroeDiag)
					{
						AlignmentA = sourceSeqArray[n - 1] + AlignmentA;
						AlignmentB = targetSeqArray[m - 1] + AlignmentB;
						m = m - 1;
						n = n - 1;
					}
					else if (n > 0 && scoringMatrix[m, n] == scoringMatrix[m, n - 1] - 2)
					{
						AlignmentA = sourceSeqArray[n - 1] + AlignmentA;
						AlignmentB = "-" + AlignmentB;
						n = n - 1;
					}
					else //if (m > 0 && scoringMatrix[m, n] == scoringMatrix[m - 1, n] + -2)
					{
						AlignmentA = "-" + AlignmentA;
						AlignmentB = targetSeqArray[m - 1] + AlignmentB;
						m = m - 1;
					}
				}
			}

			int bestScore = 0;
			// Get the best result
			for (int i = 1; i < targetCharCount; i++)
			{
				for (int j = 1; j < sourceCharCount; j++)
				{
					if (scoringMatrix[i, j] > bestScore) bestScore = scoringMatrix[i, j];
				}
			}
			return bestScore;
		}

	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
