using System;
using System.Globalization;

namespace Lift.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length == 0) return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ToPascalCase(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length == 0) return str;

            return char.ToUpperInvariant(str[0]) + str.Substring(1);
        }

        public static string ToTitleCase(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length == 0) return str;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
    }
}
