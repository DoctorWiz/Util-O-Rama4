using System;
//using SimMetrics.Net.API;

namespace FuzzyString
{
  public partial class FuzzyFunctions // ChapmanLengthDeviation : AbstractStringMetric
  {
    public static double ChapmanLengthDeviationSimilarity(this string source, string target)
    {
      if (source == null || target == null)
      {
        return 0.0;
      }
      double length = source.Length;
      double num2 = target.Length;
      if (length >= num2)
      {
        return num2 / length;
      }
      return length / num2;
    }


    public static double UnnormalisedChapmanLengthDeviationSimilarity(string source, string target)
    {
      return source.ChapmanLengthDeviationSimilarity(target);
    }

	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
