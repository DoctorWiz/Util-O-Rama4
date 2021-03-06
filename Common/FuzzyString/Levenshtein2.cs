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
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable TooWideLocalVariableScope

namespace FuzzyString
{
    /// The Levenshtein distance between two words is the Minimum number of
    /// single-character edits (insertions, deletions or substitutions) required to
    /// change one string into the other.
    public partial class FuzzyFunctions // Levenshtein : IMetricStringDistance
    {
        /// <summary>
        /// The Levenshtein distance, or edit distance, between two words is the
        /// Minimum number of single-character edits (insertions, deletions or
        /// substitutions) required to change one word into the other.
        ///
        /// http://en.wikipedia.org/wiki/Levenshtein_distance
        ///
        /// It is always at least the difference of the sizes of the two strings.
        /// It is at most the length of the longer string.
        /// It is zero if and only if the strings are equal.
        /// If the strings are the same size, the HamMing distance is an upper bound
        /// on the Levenshtein distance.
        /// The Levenshtein distance verifies the triangle inequality (the distance
        /// between two strings is no greater than the sum Levenshtein distances from
        /// a third string).
        ///
        /// Implementation uses dynamic programMing (Wagner–Fischer algorithm), with
        /// only 2 rows of data. The space requirement is thus O(m) and the algorithm
        /// runs in O(mn).
        /// </summary>
        /// <param name="source">The first string to compare.</param>
        /// <param name="target">The second string to compare.</param>
        /// <returns>The Levenshtein distance between strings</returns>
        /// <exception cref="ArgumentNullException">If source or target is null.</exception>
        public static double Levenshtein2(string source, string target)
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

            if (source.Length == 0)
            {
                return target.Length;
            }

            if (target.Length == 0)
            {
                return source.Length;
            }

            // create two work vectors of integer distances
            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];
            int[] vtemp;

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
            {
                v0[i] = i;
            }

            for (int i = 0; i < source.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0
                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < target.Length; j++)
                {
                    int cost = 1;
                    if (source[i] == target[j])
                    {
                        cost = 0;
                    }
                    v1[j + 1] = Math.Min(
                            v1[j] + 1,              // Cost of insertion
                            Math.Min(
                                    v0[j + 1] + 1,  // Cost of remove
                                    v0[j] + cost)); // Cost of substitution
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                //System.arraycopy(v1, 0, v0, 0, v0.length);

                // Flip references to current and previous row
                vtemp = v0;
                v0 = v1;
                v1 = vtemp;
            }

            return v0[target.Length];
        }
    }
}
