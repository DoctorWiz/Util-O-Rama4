using System;
using System.Collections.ObjectModel;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
  public partial class FuzzyFunctions // MongeElkan : AbstractStringMetric
  {

		//! NOT RETURNING 1.000 ON EXACT MATCH
		//! ALWAYS RETURNS SAME VALUE AS SMITH-WATERMAN

		public static double MongeElkinSimilarity(this string source, string target)
    {
			//AbstractStringMetric _internalStringMetric;
      if (source == null || target == null)
      {
				return 0.0D;
      }

      Collection<string> collection = _tokeniser.Tokenize(source);
      Collection<string> collection2 = _tokeniser.Tokenize(target);
      double num = 0.0;
      for (int i = 0; i < collection.Count; i++)
      {
        string str = collection[i];
        double num3 = 0.0;
        for (int j = 0; j < collection2.Count; j++)
        {
          string str2 = collection2[j];
					//double similarity = _internalStringMetric.GetSimilarity(str, str2);
					//? Correct???
					//! Recursive?
					//double similarity = source.MongeElkinSimilarity(target);
					// Or maybe this one?
					double similarity = source.SmithWatermanSimilarity(target);
          if (similarity > num3)
          {
            num3 = similarity;
          }
        }
        num += num3;
      }
      return num / collection.Count;
    }

    public static double UnnormalisedMongeElkinSimilarity(string source, string target)
    {
      return source.MongeElkinSimilarity(source);
    }

  }
}