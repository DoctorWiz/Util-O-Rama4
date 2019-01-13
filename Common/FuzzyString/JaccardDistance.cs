using System;
using System.Linq;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{
		public static double JaccardDistance(string source, string target)
		{
			return 1 - source.JaccardSimilarity(target);
		}

		public static double JaccardSimilarity(this string source, string target)
		{
			return (Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Union(target).Count()));
		}
	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
