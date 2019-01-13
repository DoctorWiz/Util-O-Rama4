using FuzzyString;
using System;
using System.Collections.Generic;

namespace FuzzyStringConsole
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string kevin = "kevin";
			string kevyn = "kevyn";

			List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();
			options.Add(FuzzyStringComparisonOptions.UseJaccardDistance);
			options.Add(FuzzyStringComparisonOptions.UseNormalizedLevenshteinDistance);
			options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
			options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
			options.Add(FuzzyStringComparisonOptions.CaseSensitive);

			Console.WriteLine(kevin.ApproximatelyEquals(kevyn, FuzzyStringComparisonTolerance.Weak, options.ToArray()));
			Console.WriteLine(kevin.ApproximatelyEquals(kevyn, FuzzyStringComparisonTolerance.Normal, options.ToArray()));
			Console.WriteLine(kevin.ApproximatelyEquals(kevyn, FuzzyStringComparisonTolerance.Strong, options.ToArray()));

			Console.ReadLine();
		}
	}
}