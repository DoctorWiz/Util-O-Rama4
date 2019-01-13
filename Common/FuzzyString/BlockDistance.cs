using System;
using System.Collections.ObjectModel;

namespace FuzzyString
{
  public partial class FuzzyFunctions // BlockDistance : AbstractStringMetric
  {
    private static double ActualBlockDistanceSimilarity(Collection<string> firstTokens, Collection<string> secondTokens)
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
        if (num2 > num3)
        {
          num += num2 - num3;
        }
        else
        {
          num += num3 - num2;
        }
    }
      return num;
    }

    public static double BlockDistanceSimilarity(this string source, string target)
    {
			//TokeniserWhitespace _tokeniser = new TokeniserWhitespace();
			Collection<string> firstTokens = _tokeniser.Tokenize(source);
      Collection<string> secondTokens = _tokeniser.Tokenize(target);
      int num = firstTokens.Count + secondTokens.Count;
      double actualSimilarity = ActualBlockDistanceSimilarity(firstTokens, secondTokens);
      return (num - actualSimilarity) / num;
    }

    public static double UnnormalisedBlockDistanceSimilarity(string source, string target)
    {
      Collection<string> firstTokens = _tokeniser.Tokenize(source);
      Collection<string> secondTokens = _tokeniser.Tokenize(target);
      return ActualBlockDistanceSimilarity(firstTokens, secondTokens);
    }

  } // end public partial class FuzzyFunctions
} // end namespace FuzzyString
