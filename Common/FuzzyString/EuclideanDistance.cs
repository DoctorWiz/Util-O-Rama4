using System;
using System.Collections.ObjectModel;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString // SimMetrics.Net.Metric
{
  public partial class FuzzyFunctions // EuclideanDistance : AbstractStringMetric
  {
    private const double DefaultMismatchScore = 0.0;
    //private readonly double _estimatedTimingConstant;
    //private readonly ITokeniser _tokeniser;
    //private readonly TokeniserUtilities<string> _tokenUtilities;

    //public EuclideanDistance() : this(new TokeniserWhitespace())
    //{
    //}

    //public EuclideanDistance(ITokeniser tokeniserToUse)
    //{
    //    _estimatedTimingConstant = 7.4457137088757008E-05;
    //    _tokeniser = tokeniserToUse;
    //    _tokenUtilities = new TokeniserUtilities<string>();
    //}

    private static double GetActualDistance(Collection<string> firstTokens, Collection<string> secondTokens)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			Collection<string> collection = _tokenUtilities.CreateMergedList(firstTokens, secondTokens);
      int num = 0;
      foreach (string str in collection)
      {
        int num2 = 0;
        int num3 = 0;
        if (firstTokens.Contains(str))
        {
          num2++;
        }
        if (secondTokens.Contains(str))
        {
          num3++;
        }
        num += (num2 - num3) * (num2 - num3);
      }
      return Math.Sqrt(num);
      }

		//! NOT WORKING, RETURNS INFINITY !!

		public static double EuclideanDistance(string source, string target)
      {
        if (source != null && target != null)
        {
          Collection<string> firstTokens = _tokeniser.Tokenize(source);
          Collection<string> secondTokens = _tokeniser.Tokenize(target);
          return GetActualDistance(firstTokens, secondTokens);
        }
        return 0.0;
      }

      public static double EuclideanSimilarity(this string source, string target)
      {
			TokeniserUtilities<int> _tokenUtilities = new TokeniserUtilities<int>();
			if (source != null && target != null)
        {
          double unnormalisedSimilarity = source.UnnormalisedEuclideanSimilarity(target);
          double num2 = Math.Sqrt(_tokenUtilities.FirstTokenCount + _tokenUtilities.SecondTokenCount);
          return (num2 - unnormalisedSimilarity) / num2;
        }
        return DefaultMismatchScore;
      }

      public static double UnnormalisedEuclideanSimilarity(this string source, string target)
      {
        return EuclideanDistance(source, target);
      }

  }
}