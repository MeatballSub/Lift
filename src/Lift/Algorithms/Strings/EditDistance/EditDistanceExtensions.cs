using System.Collections.Generic;
using System.Linq;

namespace Lift.Algorithms.Strings.EditDistance
{
    public static class EditDistanceExtensions
    {
        public static IEnumerable<string> FindSimilar(
            this IEditDistance editDistance, 
            string @string, 
            IEnumerable<string> strings)
        {
            return strings
               .GroupBy(_ => editDistance.FindDistance(@string, _))
               .Aggregate((i1, i2) => i1.Key < i2.Key ? i1 : i2);
        }
    }
}
