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
		public const long USE_CHAPMANLENGTHDEVIATION   = 0x000001; //       1
		public const long USE_CHAPMANMEANLENGTH        = 0x000002; //       2
		public const long USE_COSINE                   = 0x000004; //       4
		public const long USE_PADDEDHAMMING            = 0x000008; //       8
		public const long USE_HAMMINGWIZMO             = 0x000010; //      16
		public const long USE_JACCARD                  = 0x000020; //      32
		public const long USE_JARO                     = 0x000040; //      64
		public const long USE_JAROWINKLER              = 0x000080; //     128
		public const long USE_LEVENSHTEIN              = 0x000100; //     256
		public const long USE_NORMALIZEDLEVENSHTEIN    = 0x000200; //     512
		public const long USE_DAMERAULEVENSHTEIN       = 0x000400; //    1024
		public const long USE_YETILEVENSHTEIN          = 0x000800; //    2048
		public const long USE_WEIGHTEDLEVENSHTEIN      = 0x001000; //    4096
		public const long USE_LONGESTCOMMONSUBSEQUENCE = 0x002000; //    8192
		public const long USE_LONGESTCOMMONSUBSTRING   = 0x004000; //   16384
		public const long USE_METRIC                   = 0x008000; //   32768
		public const long USE_NEEDLEMANWUNSCH          = 0x010000; //   65536
		//public const long USE_NGRAM                    = 0x020000; //  131072
		public const long USE_QGRAM                    = 0x040000; //  262144
		public const long USE_OPTIMALSTRINGALIGNMENT   = 0x080000; //  524288
		public const long USE_OVERLAPCOEFFICIENT       = 0x100000; // 1048576
		public const long USE_SIFT                     = 0X200000; // 2097152
		public const long USE_SORENSENDICE             = 0x400000; // 4194304

		public const int ALGORITHM_COUNT = 23;

		//public const int USE_JACCARDDISTANCE = 0X0001;
		//public const int USE_SORENSENDICEDISTANCE = 0X0200;
		//public const int USE_RESERVED1 = 0X2000;
		//public const int USE_RESERVED2 = 0X4000;
		public const long USE_ALLSIMILARITIES = USE_CHAPMANLENGTHDEVIATION | // Meh.  Middle of pack for speed & accuracy
																						USE_CHAPMANMEANLENGTH |  // second fastest, OK (but not great) accuracy
																						//USE_COSINE |  // Not suggested, kinda slow, not real accurate
																						//USE_PADDEDHAMMING | // Not suggested, worse than cosine
																						//USE_HAMMINGWIZMO | // Better than PaddedHamming, but still not very good
																						USE_JACCARD | // middle speed, good accuracy
																						USE_JAROWINKLER | // middle speed, good accuracy
																						USE_LEVENSHTEIN | // The original version, middle of the pack
																						USE_NORMALIZEDLEVENSHTEIN | // mild improvement over the original
																						USE_DAMERAULEVENSHTEIN | // little slower than normalized but even more accurate
																						USE_YETILEVENSHTEIN | // The King!!  Fastest and very accurate!
																						//USE_WEIGHTEDLEVENSHTEIN |  // Works and accurage but also the slowest!
																						USE_LONGESTCOMMONSUBSEQUENCE |  // Accurate but one of the slower ones
																						USE_LONGESTCOMMONSUBSTRING |  // Accurate but one of the slower ones
																						USE_METRIC | // Accurate but the slowest of all being used
																						//USE_NEEDLEMANWUNSCH |  // Works but slow!  See also: https://en.wikipedia.org/wiki/Wagner%E2%80%93Fischer_algorithm
																						//USE_NGRAM |  // Broken!
																						USE_QGRAM | // slowing, semi-accurate
																						USE_OPTIMALSTRINGALIGNMENT | // sorta slow, but accurate
																						USE_OVERLAPCOEFFICIENT | // Third fastest, OK (but not great) accuracy
																						USE_SIFT | // Fast and Accurate
																						USE_SORENSENDICE ; // One of the fastest, good accuracy

		// Not only the fastest, but also one of the most accurate
		public const long USE_SUGGESTED_PREMATCH		= USE_YETILEVENSHTEIN;
		
		// The best and most accurate 5 algorithms, each a little different to give a good rounded score
		public const long USE_SUGGESTED_FINALMATCH = USE_JAROWINKLER |
																									USE_DAMERAULEVENSHTEIN |
																									USE_LONGESTCOMMONSUBSTRING |
																									USE_SIFT |
																									USE_SORENSENDICE;
		
		//USE_SORENSENDICE;  // Decent matching, medium speed
		//USE_OPTIMALSTRINGALIGNMENT;  // Excellent matching, slow
		public const double SUGGESTED_MIN_PREMATCH_SCORE = 0.40D;
		public const double SUGGESTED_MIN_FINAL_SCORE = 0.55D;


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
			//if ((algorithms & USE_NGRAM) >0)
			//{
			//	ret += "N-Gram Similarity, ";
			//}
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


			// Now perform all other requested algorithms
			if ((FuzzyStringComparisonOptions & USE_COSINE) > 0)
			{
				thisScore = source.CosineSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.40D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_JACCARD) > 0)
			{
				thisScore = source.JaccardSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.58D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_JARO) > 0)
			{
				thisScore = source.JaroSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.78D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_JAROWINKLER) > 0)
			{
				thisScore = source.JaroWinklerSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.40D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_LEVENSHTEIN) > 0)
			{
				thisScore = source.LevenshteinSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.41D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_NORMALIZEDLEVENSHTEIN) > 0)
			{
				thisScore = source.NormalizedLevenshteinSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.41D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_DAMERAULEVENSHTEIN) > 0)
			{
				thisScore = source.DamerauLevenshteinSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.41D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_YETILEVENSHTEIN) > 0)
			{
				thisScore = source.YetiLevenshteinSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.41D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_NEEDLEMANWUNSCH) > 0)
			{
				thisScore = source.NeedlemanWunschSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.66D, 1.0D, ref runningTotal, ref methodCount);
			}

			//if ((FuzzyStringComparisonOptions & USE_NGRAM) > 0)
			//{
			//	thisScore = source.NGramSimilarity(target);
			//	valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
			//}
			if ((FuzzyStringComparisonOptions & USE_OPTIMALSTRINGALIGNMENT) > 0)
			{
				thisScore = source.OptimalStringAlignmentSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.41D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_OVERLAPCOEFFICIENT) > 0)
			{
				thisScore = source.OverlapCoefficientSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.50D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_SIFT) > 0)
			{
				thisScore = source.SiftSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.58D, 1.0D, ref runningTotal, ref methodCount);
			}
			if ((FuzzyStringComparisonOptions & USE_SORENSENDICE) > 0)
			{
				thisScore = source.SorensenDiceSimilarity(target);
				//valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
				valid = AddIfValid(thisScore, 0.66D, 1.0D, ref runningTotal, ref methodCount);
			}

			if (((FuzzyStringComparisonOptions & USE_CHAPMANLENGTHDEVIATION) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_CHAPMANMEANLENGTH) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_PADDEDHAMMING) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_HAMMINGWIZMO) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_WEIGHTEDLEVENSHTEIN) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_LONGESTCOMMONSUBSEQUENCE) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_LONGESTCOMMONSUBSTRING) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_METRIC) > 0) ||
				 ((FuzzyStringComparisonOptions & USE_QGRAM) > 0))
			{
				// Use Yeti-Levenshtein to set a baseline
				//  for what is an acceptable score from other algorithms
				WLscore = source.YetiLevenshteinSimilarity(target);
				maxScore = WLscore + (1.0D - thisScore) / 2.0D;
				minScore = WLscore / 3.0D;
				minScore = Math.Max(minScore, 0.4D);

				if ((FuzzyStringComparisonOptions & USE_CHAPMANLENGTHDEVIATION) > 0)
				{
					thisScore = source.ChapmanLengthDeviationSimilarity(target);
					valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.70D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_CHAPMANMEANLENGTH) > 0)
				{
					thisScore = source.ChapmanMeanLengthSimilarity(target);
					valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.85D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_PADDEDHAMMING) > 0)
				{
					thisScore = source.PaddedHammingSimilarity(target);
					valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.70D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_HAMMINGWIZMO) > 0)
				{
					//thisScore = source.HammingWizmoSimilarity(target);
					valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.40D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_WEIGHTEDLEVENSHTEIN) > 0)
				{
					thisScore = source.WeightedLevenshteinSimilarity(target);
					valid = AddIfValid(WLscore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.80D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_LONGESTCOMMONSUBSEQUENCE) > 0)
				{
					thisScore = source.LongestCommonSubsequenceSimilarity(target);
					valid = AddIfValid(WLscore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.80D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_LONGESTCOMMONSUBSTRING) > 0)
				{
					thisScore = source.LongestCommonSubstringSimilarity(target);
					valid = AddIfValid(WLscore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.80D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_METRIC) > 0)
				{
					thisScore = source.MetricSimilarity(target);
					valid = AddIfValid(WLscore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.80D, 1.0D, ref runningTotal, ref methodCount);
				}
				if ((FuzzyStringComparisonOptions & USE_QGRAM) > 0)
				{
					thisScore = source.QGramSimilarity(target);
					valid = AddIfValid(thisScore, minScore, maxScore, ref runningTotal, ref methodCount);
					//valid = AddIfValid(thisScore, 0.58D, 1.0D, ref runningTotal, ref methodCount);
				}
			}







			if (methodCount > 0)
			{
				ret = runningTotal / methodCount;
			}

			return ret;
		}

		private static bool AddIfValid(double thisScore, double minScore, double maxScore, ref double runningTotal, ref int methodCount)
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
