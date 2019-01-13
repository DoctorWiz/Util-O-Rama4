using System;
using System.Linq;
using System.Collections.Generic;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{
		public static double PaddedHammingSimilarity(this string source, string target)
		{
			double dist = 0;
			if (source.Length == target.Length)
			{
				dist = Hamming2(source, target);
			}
			else
			{
				string sss = source;
				string ttt = target;
				if (sss.Length < ttt.Length)
				{
					sss = sss.PadLeft(ttt.Length);
				}
				if (sss.Length > ttt.Length)
				{
					ttt = ttt.PadLeft(sss.Length);
				}
				double d1 = Hamming2(sss, ttt);

				sss = source;
				ttt = target;
				if (sss.Length < ttt.Length)
				{
					sss = sss.PadRight(ttt.Length);
				}
				if (sss.Length > ttt.Length)
				{
					ttt = ttt.PadRight(sss.Length);
				}
				double d2 = Hamming2(sss, ttt);

				double df = Math.Abs(source.Length - target.Length);
				dist = (d1 + d1) / 2.0D * df;

			}
			//return SimilarityFromDistance(dist, source.Length, target.Length); // For HammingDistance
			return dist;				// For Hamming2
		}

		public static double HammingWizmoSimilarity(this string source, string target)
		{
			double dist = 0;
			if (source.Length == target.Length)
			{
				dist = Hamming2(source, target);
			}
			else
			{
				string sss = source;
				string ttt = target;
				if (sss.Length < ttt.Length)
				{
					ttt = ttt.Substring(0, sss.Length);
				}
				if (sss.Length > ttt.Length)
				{
					sss = sss.Substring(0, ttt.Length);
				}
				double d1 = Hamming2(sss, ttt);

				sss = source;
				ttt = target;
				if (sss.Length < ttt.Length)
				{
					int dif = ttt.Length - sss.Length;
					ttt = ttt.Substring(dif - 1);
				}
				if (sss.Length > ttt.Length)
				{
					int dif = sss.Length - ttt.Length;
					sss = sss.Substring(dif - 1);
				}
				double d2 = Hamming2(sss, ttt);

				dist = (d1 + d1) / 2;

			}
			//return SimilarityFromDistance(dist, source.Length, target.Length); // For HammingDistance
			return dist;        // For Hamming2
		}



		public static int HammingDistance(string source, string target)
		{
			int distance = 0;

			if (source.Length == target.Length)
			{
				for (int i = 0; i < source.Length; i++)
				{
					if (!source[i].Equals(target[i]))
					{
						distance++;
					}
				}
				return distance;
			}
			else { return 99999; }
		}

		public static double HammingIndex(string source, string target)
		{
			int distance = HammingDistance(source, target);
			return SimilarityFromDistance(distance, source.Length, target.Length);
		}

		public static double Hamming2(string source, string target)
		{
			source = source ?? string.Empty;
			target = target ?? string.Empty;

			if (source.Length != target.Length)
				return -1;

			var diffs = source.Where((sourceSymbol, i) => !sourceSymbol.Equals(target[i])).Count();

			if (diffs == 0) return 1;
			return 1.0 - 1.0 / ((double)source.Length / diffs);
		}
	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
