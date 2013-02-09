using System.Text.RegularExpressions;

namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Common Regular expressions
    /// </summary>
    public struct CommonRegEx
    {
        public const string EmailAddress = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        public const string WebUrl = @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        public const string IsDescending = @"(desc|descending)";
        public const string IsAscending = @"(asc|ascending)";
        public const string EmailParameter = @"%%[\w\d/@\.|]*%%";
        public const string SportWebPtAddress = @"^[\w-\.]+@swpt.com";
        public const string Password = @"^.{5,32}$";
        public const string Date = @"(?<Month>\d{1,2})/(?<Day>\d{1,2})/(?<Year>(?:\d{4}|\d{2}))";
        public const string SplitCapitalLetters = @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))";
        public const string IcomWord = @"(\bicom\b)";
        public const string WhiteSpace = @"(?:^(\s)+|(\s)+$)";
        public const string EscapeCharacters = @"["",\n]";
        public const string QuoteCharacters = @"([""])";


        public static readonly Regex FindQuoteCharactersExpression = new Regex(QuoteCharacters, RegexOptions.Compiled);
        public static readonly Regex FindEscapeCharactersExpression = new Regex(EscapeCharacters, RegexOptions.Compiled);
        public static readonly Regex FindWhiteSpaceExpression = new Regex(WhiteSpace, RegexOptions.Compiled);
        public static readonly Regex FindICom = new Regex(IcomWord, RegexOptions.Compiled);
        public static readonly Regex CapitalLettersExpression = new Regex(SplitCapitalLetters, RegexOptions.Compiled);
        public static readonly Regex IsDescendingExpression = new Regex(IsDescending, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly Regex IsAscendingExpression = new Regex(IsAscending, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly Regex WebUrlExpression = new Regex(WebUrl, RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex EmailExpression = new Regex(EmailAddress, RegexOptions.Singleline | RegexOptions.Compiled);
        public static readonly Regex StripHtmlLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public static readonly char[] IllegalUrlCharacters = new[] { ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$', ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"', '(', ')', '[', ']', '{', '}', '|', '^', '`', '~', '–', '‘', '’', '“', '”', '»', '«' };

    }
}