using System.Collections.Generic;
using System.Linq;

namespace Lift.Algorithms.Strings.EditDistance
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SimilarTo(
            this string str,
            IEnumerable<string> strings)
        {
            return SimilarTo(str, strings, new LevenshteinDistance());
        }

        public static IEnumerable<string> SimilarTo( 
            this string str, 
            IEnumerable<string> strings,
            IEditDistance editDistance)
        {
            return strings
               .GroupBy(_ => editDistance.FindDistance(str, _))
               .Aggregate((i1, i2) => i1.Key < i2.Key ? i1 : i2);
        }
    }
}
