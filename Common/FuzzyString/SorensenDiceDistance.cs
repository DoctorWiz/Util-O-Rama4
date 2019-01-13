using System;
using System.Linq;
using System.Collections.Generic;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{
		public static double SorensenDiceDistance(string source, string target)
		{
			return 1 - SorensenDiceIndex(source, target);
		}

		public static double SorensenDiceIndex(string source, string target)
		{
			return (2 * Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Length + target.Length));
		}

		public static double SorensenDiceSimilarity(this string source, string target)
		{
			HashSet<string> setA = new HashSet<string>();
			HashSet<string> setB = new HashSet<string>();

			for (int i = 0; i < source.Length - 1; ++i)
				setA.Add(source.Substring(i, 2));

			for (int i = 0; i < target.Length - 1; ++i)
				setB.Add(target.Substring(i, 2));

			HashSet<string> intersection = new HashSet<string>(setA);
			intersection.IntersectWith(setB);

			return (2.0 * intersection.Count) / (setA.Count + setB.Count);
		}
	}

}