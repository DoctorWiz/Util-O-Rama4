using System;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
  public partial class FuzzyFunctions // NeedlemanWunch : AbstractStringMetric
  {
    private const double DefaultGapCost = 2.0;
    private const double DefaultPerfectMatchScore = 1.0;

		//public static double GapCost { get; set; }


		public static double NeedlemanWunschSimilarity(this string source, string target)
    {
			double GapCost = DefaultGapCost; //? Correct????
			//IndexCostFunction DCostFunction; // = new IndexCostFunctions;

			if (source == null || target == null)
      {
          return 0.0D;
      }
      double unnormalisedSimilarity = UnnormalisedNeedlemanWunchSimilarity(source, target);
      double num2 = Math.Max(source.Length, target.Length);
      double num3 = num2;
      if (Max01Cost > GapCost)
      {
          num2 *= Max01Cost;
      }
      else
      {
          num2 *= GapCost;
      }
      if (Min01Cost < GapCost)
      {
          num3 *= Min01Cost;
      }
      else
      {
          num3 *= GapCost;
      }

      if (num3 < 0.0)
      {
          num2 -= num3;
          unnormalisedSimilarity -= num3;
      }

      if (num2 == 0.0)
      {
          return DefaultPerfectMatchScore;
      }

      return DefaultPerfectMatchScore - unnormalisedSimilarity / num2;
    }

    public static double UnnormalisedNeedlemanWunchSimilarity(string source, string target)
    {
			double GapCost = DefaultGapCost; //? Correct????
			//AbstractSubstitutionCost DCostFunction;

			if (source == null || target == null)
      {
          return 0.0D;
      }

      int length = source.Length;
      int index = target.Length;

      if (length == 0)
      {
          return index;
      }

      if (index == 0)
      {
          return length;
      }

      double[][] numArray = new double[length + 1][];

      for (int i = 0; i < length + 1; i++)
      {
          numArray[i] = new double[index + 1];
      }

      for (int j = 0; j <= length; j++)
      {
          numArray[j][0] = j;
      }

      for (int k = 0; k <= index; k++)
      {
          numArray[0][k] = k;
      }

      for (int m = 1; m <= length; m++)
      {
          for (int n = 1; n <= index; n++)
          {
              double num8 = Get01Cost(source, m - 1, target, n - 1);
              numArray[m][n] = MathFunctions.MinOf3(numArray[m - 1][n] + GapCost, numArray[m][n - 1] + GapCost, numArray[m - 1][n - 1] + num8);
          }
      }

      return numArray[length][index];
    }


	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
