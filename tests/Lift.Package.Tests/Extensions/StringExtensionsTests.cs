using Xunit;
using Lift.Extensions;

namespace Lift.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("camelCase", "camelCase")]
        [InlineData("CamelCase", "camelCase")]
        [InlineData("c", "c")]
        [InlineData("C", "c")]
        [InlineData("", "")]
        public void ToCamelCase_ShouldUppercaseFirstLetter(string text, string expected)
        {
            var actual = text.ToCamelCase();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("pascalCase", "PascalCase")]
        [InlineData("PascalCase", "PascalCase")]
        [InlineData("p", "P")]
        [InlineData("P", "P")]
        [InlineData("", "")]
        public void ToPascalCase_ShouldLowercaseFirstLetter(string text, string expected)
        {
            var actual = text.ToPascalCase();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("title case", "Title Case")]
        [InlineData("Title case", "Title Case")]
        [InlineData("title Case", "Title Case")]
        [InlineData("TiTlE CaSe", "Title Case")]
        [InlineData("", "")]
        public void ToTitleCase_ShouldUppercaseFirstLetterOfEveryWord(string text, string expected)
        {
            var actual = text.ToTitleCase();

            Assert.Equal(expected, actual);
        }
    }
}
