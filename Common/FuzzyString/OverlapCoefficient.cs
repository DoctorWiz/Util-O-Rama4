using System;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
	public partial class FuzzyFunctions // OverlapCoefficient : AbstractStringMetric
  {
		public static double DefaultMismatchScore = 0.0D;

		public static double OverlapCoefficientSimilarity(this string firstWord, string secondWord)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			if (firstWord != null && secondWord != null)
      {
        _tokenUtilities.CreateMergedSet(_tokeniser.Tokenize(firstWord), _tokeniser.Tokenize(secondWord));
        return _tokenUtilities.CommonSetTerms() / (double)Math.Min(_tokenUtilities.FirstSetTokenCount, _tokenUtilities.SecondSetTokenCount);
      }
      return DefaultMismatchScore;
    }

  }
}