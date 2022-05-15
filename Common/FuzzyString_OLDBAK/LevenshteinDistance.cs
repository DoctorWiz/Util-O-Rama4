using System;

namespace FuzzyString
{
	public static partial class FuzzyFunctions
	{

		// Broken, gets stuck in loop.  Use DemerauLevenshtein instead
		public static int BADLevenshteinDistance(this string source, string target)
		{
			if (source.Length == 0) { return target.Length; }
			if (target.Length == 0) { return source.Length; }

			int distance = 0;

			if (source[source.Length - 1] == target[target.Length - 1]) { distance = 0; }
			else { distance = 1; }

			return Math.Min(Math.Min(LevenshteinDistance(source.Substring(0, source.Length - 1), target) + 1,
															 LevenshteinDistance(source, target.Substring(0, target.Length - 1))) + 1,
															 LevenshteinDistance(source.Substring(0, source.Length - 1), target.Substring(0, target.Length - 1)) + distance);
		}

		public static int LevenshteinDistance(this string source, string target)
		{
			// From WikiBooks https://en.wikibooks.org/wiki/Algorithm_Implementation/Strings/Levenshtein_distance

			if (String.IsNullOrEmpty(source))
			{
				if (String.IsNullOrEmpty(target)) return 0;
				return target.Length;
			}
			if (String.IsNullOrEmpty(target)) return source.Length;

			if (source.Length > target.Length)
			{
				var temp = target;
				target = source;
				source = temp;
			}

			var m = target.Length;
			var n = source.Length;
			var distance = new int[2, m + 1];
			// Initialize the distance 'matrix'
			for (var j = 1; j <= m; j++) distance[0, j] = j;

			var currentRow = 0;
			for (var i = 1; i <= n; ++i)
			{
				currentRow = i & 1;
				distance[currentRow, 0] = i;
				var previousRow = currentRow ^ 1;
				for (var j = 1; j <= m; j++)
				{
					var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
					distance[currentRow, j] = Math.Min(Math.Min(
								distance[previousRow, j] + 1,
								distance[currentRow, j - 1] + 1),
								distance[previousRow, j - 1] + cost);
				}
			}
			return distance[currentRow, m];
		}

		public static double LevenshteinSimilarity(this string source, string target)
		{
			int distance = LevenshteinDistance(source, target);
			return SimilarityFromDistance(distance, source.Length, target.Length);
		}

		public static double NormalizedLevenshteinDistance(string source, string target)
		{
			int unnormalizedLevenshteinDistance = source.LevenshteinDistance(target);

			return unnormalizedLevenshteinDistance - LevenshteinDistanceLowerBounds(source, target);
		}

		public static double NormalizedLevenshteinSimilarity(this string source, string target)
		{
			double distance = NormalizedLevenshteinDistance(source, target);
			return SimilarityFromDistance(distance, source.Length, target.Length);
		}


		public static int LevenshteinDistanceUpperBounds(this string source, string target)
		{
			// If the two strings are the same length then the Hamming Distance is the upper bounds of the Levenshtien Distance.
			if (source.Length == target.Length) { return HammingDistance(source, target); }

			// Otherwise, the upper bound is the length of the longer string.
			else if (source.Length > target.Length) { return source.Length; }
			else if (target.Length > source.Length) { return target.Length; }

			return 9999;
		}

		public static int LevenshteinDistanceLowerBounds(string source, string target)
		{
			// If the two strings are the same length then the lower bound is zero.
			if (source.Length == target.Length) { return 0; }

			// If the two strings are different lengths then the lower bounds is the difference in length.
			else { return Math.Abs(source.Length - target.Length); }
		}
		
		
		public static double LevenshteinIndex2(string source, string target)
		{
			int sourceSize = source.Length;
			int targetSize = target.Length;
			int[,] distance = new int[sourceSize + 1, targetSize + 1];

			// Step 1
			if (sourceSize == 0)
			{
				return targetSize;
			}

			if (targetSize == 0)
			{
				return sourceSize;
			}

			// Step 2
			//for (int sourceLoop = 0; sourceLoop <= sourceSize; distance[sourceLoop, 0] = sourceLoop++)
			//{
			//}

			//for (int targetLoop = 0; targetLoop <= targetSize; distance[0, targetLoop] = targetLoop++)
			//{
			//}

			// Step 3
			for (int sourceLoop = 1; sourceLoop <= sourceSize; sourceLoop++)
			{
				//Step 4
				for (int targetLoop = 1; targetLoop <= targetSize; targetLoop++)
				{
					// Step 5
					int cost = (target[targetLoop - 1] == source[sourceLoop - 1]) ? 0 : 1;

					// Step 6
					distance[sourceLoop, targetLoop] = Math.Min(
							Math.Min(distance[sourceLoop - 1, targetLoop] + 1, distance[sourceLoop, targetLoop - 1] + 1),
							distance[sourceLoop - 1, targetLoop - 1] + cost);
				}
			}
			// Step 7
			return distance[sourceSize, targetSize];
		}



	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
