using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyString
{

	public static partial class FuzzyFunctions
	{
		public const long USE_CASEINSENSITIVE         = 0x8000000;

		//! Very Important: First algorith should start with 1
		//   each subsequent algoritm should shift 1 bit to the left
		//    no gaps
		public const long USE_CHAPMANLENGTHDEVIATION   = 0x000001;
		public const long USE_CHAPMANMEANLENGTH        = 0x000002;
		public const long USE_COSINE                   = 0x000004;
		public const long USE_PADDEDHAMMING            = 0x000008;
		public const long USE_HAMMINGWIZMO             = 0x000010;
		public const long USE_JACCARD                  = 0x000020;
		public const long USE_JARO                     = 0x000040;
		public const long USE_JAROWINKLER              = 0x000080;
		public const long USE_LEVENSHTEIN              = 0x000100;
		public const long USE_NORMALIZEDLEVENSHTEIN    = 0x000200;
		public const long USE_DAMERAULEVENSHTEIN       = 0x000400;
		public const long USE_YETILEVENSHTEIN          = 0x000800;
		public const long USE_WEIGHTEDLEVENSHTEIN      = 0x001000;
		public const long USE_LONGESTCOMMONSUBSEQUENCE = 0x002000;
		public const long USE_LONGESTCOMMONSUBSTRING   = 0x004000;
		public const long USE_METRIC                   = 0x008000;
		public const long USE_NEEDLEMANWUNSCH          = 0x010000;
		public const long USE_NGRAM                    = 0x020000;
		public const long USE_QGRAM                    = 0x040000;
		public const long USE_OPTIMALSTRINGALIGNMENT   = 0x080000;
		public const long USE_OVERLAPCOEFFICIENT       = 0x100000;
		public const long USE_SIFT                     = 0X200000;
		public const long USE_SORENSENDICE             = 0x400000;

		public const int ALGORITHM_COUNT = 23;

		//public const int USE_JACCARDDISTANCE = 0X0001;
		//public const int USE_SORENSENDICEDISTANCE = 0X0200;
		//public const int USE_RESERVED1 = 0X2000;
		//public const int USE_RESERVED2 = 0X4000;
		public const long USE_ALLSIMILARITIES = USE_CHAPMANLENGTHDEVIATION |
																				USE_COSINE |
																				USE_HAMMINGWIZMO |
																				USE_DAMERAULEVENSHTEIN |
																				USE_WEIGHTEDLEVENSHTEIN |
																				USE_JACCARD |
																				USE_JAROWINKLER |
																				USE_LONGESTCOMMONSUBSEQUENCE |
																				USE_METRIC |
																				USE_NEEDLEMANWUNSCH |
																				USE_NGRAM |
																				USE_OPTIMALSTRINGALIGNMENT |
																				USE_QGRAM |
																				USE_SIFT |
																				USE_SORENSENDICE ;

		public static string AlgorithmNames(long algorithms)
		{
			string ret = "";

			if ((algorithms & USE_CHAPMANLENGTHDEVIATION) > 0)
			{
				ret += "Chapman Length Deviation, ";
			}
			if ((algorithms & USE_CHAPMANMEANLENGTH) > 0)
			{
				ret += "Chapman Mean Length, ";
			}
			if ((algorithms & USE_COSINE) > 0)
			{
				ret += "Cosine Similarity, ";
			}
			if ((algorithms & USE_PADDEDHAMMING) > 0)
			{
				ret += "Padded Hamming, ";
			}
			if ((algorithms & USE_HAMMINGWIZMO) > 0)
			{
				ret += "Hamming-Wizmo, ";
			}
			if ((algorithms & USE_JACCARD) > 0)
			{
				ret += "Jaccard Similarity, ";
			}
			if ((algorithms & USE_JARO) > 0)
			{
				ret += "Jaro Similarity, ";
			}
			if ((algorithms & USE_JAROWINKLER) > 0)
			{
				ret += "Jaro-Winkler, ";
			}
			if ((algorithms & USE_LEVENSHTEIN) > 0)
			{
				ret += "Levenshtein Similarity, ";
			}
			if ((algorithms & USE_NORMALIZEDLEVENSHTEIN) > 0)
			{
				ret += "Normalized Levenshtein, ";
			}
			if ((algorithms & USE_DAMERAULEVENSHTEIN) > 0)
			{
				ret += "Damerau-Levenshtein, ";
			}
			if ((algorithms & USE_YETILEVENSHTEIN) > 0)
			{
				ret += "Yeti-Levenshtein, ";
			}
			if ((algorithms & USE_WEIGHTEDLEVENSHTEIN) > 0)
			{
				ret += "Weighted Levenshtein, ";
			}
			if ((algorithms & USE_LONGESTCOMMONSUBSEQUENCE) > 0)
			{
				ret += "Longest Common Subsequence, ";
			}
			if ((algorithms & USE_LONGESTCOMMONSUBSTRING) > 0)
			{
				ret += "Longest Common Substring, ";
			}
			if ((algorithms & USE_METRIC) > 0)
			{
				ret += "Metric Similarity, ";
			}
			if ((algorithms & USE_NEEDLEMANWUNSCH) > 0)
			{
				ret += "Needleman-Wunsch, ";
			}
			if ((algorithms & USE_NGRAM) >0)
			{
				ret += "N-Gram Similarity, ";
			}
			if ((algorithms & USE_QGRAM) > 0)
			{
				ret += "Q-Gram Similarity, ";
			}
			if ((algorithms & USE_OPTIMALSTRINGALIGNMENT) > 0)
			{
				ret += "Optimal String Alignment, ";
			}
			if ((algorithms & USE_OVERLAPCOEFFICIENT) > 0)
			{
				ret += "Overlap Coefficient, ";
			}
			if ((algorithms & USE_SIFT) > 0)
			{
				ret += "Sift Similarity, ";
			}
			if ((algorithms & USE_SORENSENDICE) > 0)
			{
				ret += "Sorensen-Dice, ";
			}
			if ((algorithms &  USE_CASEINSENSITIVE) > 0)
			{
				ret += "and Case Insensitive, ";
			}

			if (ret.Length > 5)
			{
				ret = ret.Substring(0, ret.Length - 2);
			}




			return ret;
		}



		public static double RankEquality(this string source, string target)
		{
			return source.RankEquality(target, USE_ALLSIMILARITIES);
		}

		public static double RankEquality(this string source, string target, long FuzzyStringComparisonOptions)
		{
			int methodCount = 0;
			double runningTotal = 0D;
			double ret = 0D;
			List<double> comparisonResults = new List<double>();
			double minScore = 0.4D;
			double maxScore = 0.99D;
			double thisScore = 0.5D;
			double WLscore = 0.8D;
			bool valid = false;

			if ((FuzzyStringComparisonOptions & USE_CASEINSENSITIVE) > 0)
			{
				source = source.Capitalize();
				target = target.Capitalize();
			}

			//string foo = "Fuzzy " + source + " vs. " + target;
			//Console.WriteLine(foo);
			//if (target.IndexOf("llow Snow") > 0)
			//{
			//	foo += "STOP!";
			//}

			// Use Weighted Levenshtein to set a baseline
			//  for what is an acceptable score from other algorithms
			WLscore = source.WeightedLevenshteinSimilarity(target);
			maxScore = WLscore + (1.0D - thisScore) / 2.0D;
			minScore = WLscore / 3.0D;
			minScore = Math.Max(minScore, 0.4D);

			// Now perform all other requested algorithms
			if ((FuzzyStringComparisonOptions & USE_CHAPMANLENGTHDEVIATION) > 0)
			{
				thisScore = source.ChapmanLengthDeviationSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_CHAPMANMEANLENGTH) > 0)
			{
				thisScore = source.ChapmanMeanLengthSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_PADDEDHAMMING) > 0)
			{
				thisScore = source.PaddedHammingSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_HAMMINGWIZMO) > 0)
			{
				thisScore = source.HammingWizmoSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_JACCARD) > 0)
			{
				thisScore = source.JaccardSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_JARO) > 0)
			{
				thisScore = source.JaroSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_JAROWINKLER) > 0)
			{
				thisScore = source.JaroWinklerSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_LEVENSHTEIN) > 0)
			{
				thisScore = source.LevenshteinSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_NORMALIZEDLEVENSHTEIN) > 0)
			{
				thisScore = source.NormalizedLevenshteinSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_DAMERAULEVENSHTEIN) > 0)
			{
				thisScore = source.DamerauLevenshteinSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_YETILEVENSHTEIN) > 0)
			{
				thisScore = source.YetiLevenshteinSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_WEIGHTEDLEVENSHTEIN) > 0)
			{
				thisScore = source.WeightedLevenshteinSimilarity(target);
				valid = WankEquality(WLscore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_METRIC) > 0)
			{
				thisScore = source.MetricSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_NEEDLEMANWUNSCH) > 0)
			{
				thisScore = source.NeedlemanWunschSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_NGRAM) > 0)
			{
				thisScore = source.NGramSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_QGRAM) > 0)
			{
				thisScore = source.QGramSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_OPTIMALSTRINGALIGNMENT) > 0)
			{
				thisScore = source.OptimalStringAlignmentSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_OVERLAPCOEFFICIENT) > 0)
			{
				thisScore = source.OverlapCoefficientSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_SIFT) > 0)
			{
				thisScore = source.SiftSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}

			if ((FuzzyStringComparisonOptions & USE_SORENSENDICE) > 0)
			{
				thisScore = source.SorensenDiceSimilarity(target);
				valid = WankEquality(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			}



			if (methodCount > 0)
			{
				ret = runningTotal / methodCount;
			}

			return ret;
		}

		private static bool WankEquality(double thisScore, double minScore, double maxScore, ref double runningTotal, ref int methodCount)
		{
			bool valid = false;
			if ((thisScore >= minScore) && (thisScore <= maxScore)) valid = true;
			if (valid)
			{
				runningTotal += thisScore;
				methodCount++;
			}
			return valid;
		}




		public static double SimilarityFromDistance(this double distanceScore, int sourceLength, int targetLength)
		{
			double ret = 0;
			int mn = Math.Min(sourceLength, targetLength);
			if (distanceScore <= 0)
			{
				ret = 1;
			}
			else
			{
				if (mn > 0)
				{
					if (distanceScore < mn)
					{
						ret = 1 - (distanceScore / mn);
					}
				}
			}
			return ret;
		} // end Fuzzy Index Distance

		public static double DistanceToSimilarity(this double distanceScore, int sourceLength, int targetLength)
		{
			return distanceScore.SimilarityFromDistance(sourceLength, targetLength);
		}


	} // end partial class FuzzyFunctions
} //end namespace FuzzyString
