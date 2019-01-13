using System;
//using SimMetrics.Net.API;
//using SimMetrics.Net.Utilities;

namespace FuzzyString
{
  public partial class FuzzyFunctions // SmithWaterman : AbstractStringMetric
  {
		//! NOT RETURNING 1.000 ON EXACT MATCH!

		public static double SmithWatermanSimilarity(this string source, string target)
    {
			double GapCost = DefaultGapCost;

			if (source == null || target == null)
        {
          return DefaultMismatchScore;
        }

        double unnormalisedSimilarity = source.UnnormalisedSmithWatermanSimilarity(target);
        double num2 = Math.Min(source.Length, target.Length);

        if (Max01Cost > -GapCost)
        {
          num2 *= Max01Cost;
        }
        else
        {
          num2 *= -GapCost;
        }

        if (num2 == 0.0)
        {
          return DefaultPerfectMatchScore;
        }

        return unnormalisedSimilarity / num2;
      }


      public static double UnnormalisedSmithWatermanSimilarity(string source, string target)
      {
				double GapCost = DefaultGapCost;

				if (source == null || target == null)
        {
          return DefaultMismatchScore;
        }
        int length = source.Length;
        int num2 = target.Length;
        if (length == 0)
        {
          return num2;
        }
        if (num2 == 0)
        {
            return length;
        }
        double[][] numArray = new double[length][];
        for (int i = 0; i < length; i++)
        {
          numArray[i] = new double[num2];
        }
        double num4 = 0.0;
        for (int j = 0; j < length; j++)
        {
          double thirdNumber = Get01Cost(source, j, target, 0);
          if (j == 0)
          {
            numArray[0][0] = MathFunctions.MaxOf3(0.0, -GapCost, thirdNumber);
          }
          else
          {
            numArray[j][0] = MathFunctions.MaxOf3(0.0, numArray[j - 1][0] - GapCost, thirdNumber);
          }
          if (numArray[j][0] > num4)
          {
            num4 = numArray[j][0];
          }
        }

        for (int k = 0; k < num2; k++)
        {
          double num8 = Get01Cost(source, 0, target, k);
          if (k == 0)
          {
            numArray[0][0] = MathFunctions.MaxOf3(0.0, -GapCost, num8);
          }
          else
          {
            numArray[0][k] = MathFunctions.MaxOf3(0.0, numArray[0][k - 1] - GapCost, num8);
          }
          if (numArray[0][k] > num4)
          {
            num4 = numArray[0][k];
          }
        }

        for (int m = 1; m < length; m++)
        {
          for (int n = 1; n < num2; n++)
          {
            double num11 = Get01Cost(source, m, target, n);
            numArray[m][n] = MathFunctions.MaxOf4(0.0, numArray[m - 1][n] - GapCost, numArray[m][n - 1] - GapCost, numArray[m - 1][n - 1] + num11);
            if (numArray[m][n] > num4)
            {
              num4 = numArray[m][n];
            }
          }
        }
        return num4;
      } // end unnormallized


    }
}