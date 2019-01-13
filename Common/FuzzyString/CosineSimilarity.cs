using System;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
  public partial class FuzzyFunctions // CosineSimilarity : AbstractStringMetric
  {
    public static double CosineSimilarity2(this string source, string target)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			if (source != null && target != null && _tokenUtilities.CreateMergedSet(_tokeniser.Tokenize(source), _tokeniser.Tokenize(target)).Count > 0)
      {
        return _tokenUtilities.CommonSetTerms() / (Math.Pow(_tokenUtilities.FirstSetTokenCount, 0.5) * Math.Pow(_tokenUtilities.SecondSetTokenCount, 0.5));
      }
      return 0.0;
    }

    public static double UnnormalisedCosineSimilarity(string source, string target)
    {
      return source.CosineSimilarity2(target);
    }

	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
