using System;

namespace Topaz.Common.Extensions
{
    public static class Csv
    {
        private static char[] quotedCharacters = { ',', '"', '\n' };
        private const string quote = "\"";
        private const string escapedQuote = "\"\"";

        public static string CsvEscape(this string value)
        {
            if (value == null) return "";
            if (value.Contains(quote)) value = value.Replace(quote, escapedQuote);
            if (value.IndexOfAny(quotedCharacters) > -1)
                value = quote + value + quote;
            return value;
        }

        public static string CsvUnescape(this string value)
        {
            if (value == null) return "";
            if (value.StartsWith(quote) && value.EndsWith(quote))
            {
                value = value.Substring(1, value.Length - 2);
                if (value.Contains(escapedQuote))
                    value = value.Replace(escapedQuote, quote);
            }
            return value;
        }
    }
}
