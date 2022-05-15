/*
 * The MIT License
 *
 * Copyright 2016 feature[23]
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
//using F23.StringSimilarity.Interfaces;

namespace FuzzyString
{
  /// <summary>
  /// Distance metric based on Longest Common Subsequence, from the notes "An
  /// LCS-based string metric" by Daniel Bakkelund.
  /// </summary>
  public  partial class FuzzyFunctions // MetricLCS : IMetricStringDistance, INormalizedStringDistance
  {
		//private readonly LongestCommonSubsequence lcs = new LongestCommonSubsequence();

		/// <summary>
		/// Distance metric based on Longest Common Subsequence, computed as
		/// 1 - |LCS(source, target)| / max(|source|, |target|).
		/// </summary>
		/// <param name="source">The first string to compare.</param>
		/// <param name="target">The second string to compare.</param>
		/// <returns>LCS distance metric</returns>
		/// <exception cref="ArgumentNullException">If source or target is null.</exception>

		public static double MetricSimilarity(this string source, string target)
		{
			double score = source.MetricDistance(target);
			return SimilarityFromDistance(score, source.Length, target.Length);
		}



		public static double MetricDistance(this string source, string target)
    {
      if (source == null)
      {
          throw new ArgumentNullException(nameof(source));
      }

      if (target == null)
      {
          throw new ArgumentNullException(nameof(target));
      }

      if (source.Equals(target))
      {
          return 0;
      }

      int m_len = Math.Max(source.Length, target.Length);

      if (m_len == 0) return 0.0;

			double ret = 1.0-  (1.0 * source.LongestCommonSubsequence(target).Length) / m_len;
			return ret;
    }
	} // end public partial class FuzzyFunctions
} // end namespace FuzzyString
