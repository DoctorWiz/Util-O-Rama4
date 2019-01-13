using System;
using System.Collections.ObjectModel;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
  public partial class FuzzyFunctions // MatchingCoefficient : AbstractStringMetric
  {
		//private static readonly ITokeniser _tokeniser;
		//private static readonly TokeniserUtilities<string> => _tokenUtilities;
		//private static readonly _tokeniser => TokeniserWhitespace;
		

		private static double ActualMatchingCoefficientSimilarity(Collection<string> firstTokens, Collection<string> secondTokens)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			_tokenUtilities.CreateMergedList(firstTokens, secondTokens);
      int num = 0;
      foreach (string str in firstTokens)
      {
        if (secondTokens.Contains(str))
        {
          num++;
        }
      }
      return num;
    }


		//! NOT WORKING
		public static double MatchingCoefficientSimilarity(this string source, string target)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			if (source != null && target != null)
      {
        double unnormalisedSimilarity = source.UnnormalisedJaroSimilarity(target);
        int num2 = Math.Max(_tokenUtilities.FirstTokenCount, _tokenUtilities.SecondTokenCount);
        return unnormalisedSimilarity / num2;
      }
			return 0;
    }


    public static double UnnormalisedMatchingCoefficientSimilarity(string source, string target)
    {
			//TokeniserWhitespace _tokeniser = new TokeniserWhitespace();
			//TokeniserWhitespace _tokeniser = new TokeniserWhitespace();
			Collection<string> firstTokens = _tokeniser.Tokenize(source);
      Collection<string> secondTokens = _tokeniser.Tokenize(target);
      return ActualMatchingCoefficientSimilarity(firstTokens, secondTokens);
    }

	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
