//using SimMetrics.Net.API;

namespace FuzzyString
{
  public partial class FuzzyFunctions // SubCostRange0To1 : AbstractSubstitutionCost
  {
		//public DCostFunction => IndexCostFunctions
		//public static class IndexCostFunctions
		//{
			private const int Char01ExactMatchScore = 1;
			private const double Char01MismatchScore = 0.0;

			public static double Get01Cost(string source, int sourceIndex, string target, int targetIndex)
			{
				if (source != null && target != null)
				{
					return source[sourceIndex] != target[targetIndex] ? Char01ExactMatchScore : Char01MismatchScore;
				}

				return Char01MismatchScore;
			}

			public static double Max01Cost => Char01ExactMatchScore;

			public static double Min01Cost => Char01MismatchScore;
		//}
  }
}