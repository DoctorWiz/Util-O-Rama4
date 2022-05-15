using System;
//using SimMetrics.Net.API;

namespace FuzzyString
{
  public partial class FuzzyFunctions // ChapmanMeanLength : AbstractStringMetric
  {
    private const int ChapmanMeanLengthMaxString = 500;
    private const double DefaultPerfectScore = 1.0;

		public static double ChapmanMeanLengthSimilarity(this string source, string target)
		{
			if (source == null || target == null)
			{
				return DefaultMismatchScore;
			}
			double combined = target.Length + source.Length;
			double longest = Math.Max(target.Length, source.Length);
			double score = combined / (longest * 2);
			return score;
		}

		// Original
		/*
		public static double ChapmanMeanLengthSimilarity(this string source, string target)
    {
      if (source == null || target == null)
      {
          return DefaultMismatchScore;
      }
      double num = target.Length + source.Length;
      if (num > ChapmanMeanLengthMaxString)
      {
          return DefaultPerfectScore;
      }

			double num2 = (ChapmanMeanLengthMaxString - num) / ChapmanMeanLengthMaxString;
      return 1.0 - num2 * num2 * num2 * num2;
    }
		*/

    public static double UnnormalisedChapmanMeanLengthSimilarity(string source, string target)
    {
      return source.ChapmanMeanLengthSimilarity(target);
    }

  } // end public partial class FuzzyFunctions
} // end namespace FuzzyString

