using System.Linq;
using Lift.Algorithms.Strings.EditDistance;
using Xunit;

namespace lift.algorithms.Strings.EditDistance
{
    public class LevenshteinDistanceTests
    {
        [Theory]
        [InlineData(null, null, 0)]
        [InlineData(null, "right", 5)]
        [InlineData("left", null, 4)]
        [InlineData("string", "string", 0)]
        [InlineData("", "", 0)]
        [InlineData("str", "string", 3)]
        [InlineData("string", "str", 3)]
        [InlineData("string", "strong", 1)]
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
            string @string,
            string[] strings,
            string[] expected)
        {
            IEditDistance distance = new LevenshteinDistance();

            var actual = distance.FindSimilar(@string, strings);

            Assert.Equal(
                expected.OrderBy(_ => _),
                actual.OrderBy(_ => _));
        }
    }
}
