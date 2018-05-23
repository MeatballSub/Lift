using System.Linq;
using Lift.Algorithms.Strings.EditDistance;
using Xunit;

namespace lift.algorithms.Strings.EditDistance
{
    public class LevenshteinDistanceTests
    {
        [Theory]
        [InlineData(null, null, 0)]
        [InlineData(null, "string", 6)]
        [InlineData("string", null, 6)]
        [InlineData("insertion", "insertioning", 3)]
        [InlineData("substitution", "subspipupion", 3)]
        [InlineData("deletion", "let", 5)]
        [InlineData("allthree", "ballpree", 3)]
        public void EditDistance_ShouldGenerateCorrectEditDistance(
            string string1,
            string string2,
            int expected)
        {
            IEditDistance distance = new LevenshteinDistance();

            var actual = distance.FindDistance(string1, string2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("string", new[] { "str" }, new[] { "str" })]
        [InlineData("string", new[] { "str", "stri" }, new[] { "stri" })]
        [InlineData("string", new[] { "stra", "strb" }, new[] { "stra", "strb" })]
        public void Similar_ShouldReturnSimilarStrings(
            string str,
            string[] strings,
            string[] expected)
        {
            IEditDistance distance = new LevenshteinDistance();

            var actual = str.SimilarTo(strings, distance);

            Assert.Equal(
                expected.OrderBy(_ => _),
                actual.OrderBy(_ => _));
        }
    }
}
