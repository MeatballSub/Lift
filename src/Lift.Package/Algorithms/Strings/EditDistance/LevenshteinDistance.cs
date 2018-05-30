using System;

namespace Lift.Algorithms.Strings.EditDistance
{
    public class LevenshteinDistance : IEditDistance
    {
        public int FindDistance(string string1, string string2)
        {
            if (string1 == string2)
            {
                return 0;
            }

            if (string.IsNullOrEmpty(string1))
            {
                if (!string.IsNullOrEmpty(string2))
                {
                    return string2.Length;
                }
            }

            if (string.IsNullOrEmpty(string2))
            {
                if (!string.IsNullOrEmpty(string1))
                {
                    return string1.Length;
                }
            }

            var matrix = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 1; i <= string1.Length; ++i)
            {
                matrix[i, 0] = i;
            }

            for (int i = 1; i <= string2.Length; ++i)
            {
                matrix[0, i] = i;
            }

            for (int i = 1; i <= string1.Length; ++i)
            {
                for (int j = 1; j <= string2.Length; ++j)
                {
                    var cost = (string1[i - 1] == string2[j - 1] ? 0 : 1);

                    var substitution = matrix[i - 1, j - 1] + cost;
                    var insertion = matrix[i, j - 1] + 1;
                    var deletion = matrix[i - 1, j] + 1;
   
                    matrix[i, j] = 
                        Math.Min(substitution,
                        Math.Min(insertion, deletion));
                }
            }

            return matrix[string1.Length, string2.Length];
        }
    }
}
