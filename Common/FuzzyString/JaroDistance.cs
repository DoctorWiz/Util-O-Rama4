using System.Collections.Generic;
using System.Linq;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{
		public static double JaroDistance(this string source, string target)
		{
			int m = source.Intersect(target).Count();

			if (m == 0) { return 0; }
			else
			{
				string sourceTargetIntersetAsString = "";
				string targetSourceIntersetAsString = "";
				IEnumerable<char> sourceIntersectTarget = source.Intersect(target);
				IEnumerable<char> targetIntersectSource = target.Intersect(source);
				foreach (char character in sourceIntersectTarget) { sourceTargetIntersetAsString += character; }
				foreach (char character in targetIntersectSource) { targetSourceIntersetAsString += character; }
				//double t = sourceTargetIntersetAsString.DemerauLevenshteinDistance(targetSourceIntersetAsString) / 2;
				//int thr = (sourceTargetIntersetAsString.Length + targetSourceIntersetAsString.Length) / 6;
				double t = sourceTargetIntersetAsString.DamerauLevenshteinDistance(targetSourceIntersetAsString) / 2;

				double ret = ((m / source.Length) + (m / target.Length) + ((m - t) / m)) / 3;
				if (ret < 0) ret = 0;
				return ret;
			}
		}

		public static double JaroIndex(this string source, string target)
		{
			double distance = source.JaroDistance(target);
			return SimilarityFromDistance(distance, source.Length, target.Length);
		}


	}
}