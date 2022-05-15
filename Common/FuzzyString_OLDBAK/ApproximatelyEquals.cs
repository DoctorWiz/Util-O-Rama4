using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{
		public static bool ApproximatelyEquals(this string source, string target, long options, FuzzyStringComparisonTolerance tolerance)
		{
			double results = source.RankEquality(target, options);

			if (tolerance == FuzzyStringComparisonTolerance.Strong)
			{
				if (results < 0.25)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (tolerance == FuzzyStringComparisonTolerance.Normal)
			{
				if (results < 0.5)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (tolerance == FuzzyStringComparisonTolerance.Weak)
			{
				if (results < 0.75)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (tolerance == FuzzyStringComparisonTolerance.Manual)
			{
				if (results > 0.6)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}