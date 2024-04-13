using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Application;

public static partial class SlugGenerator
{
    [GeneratedRegex(@"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]")]
    private static partial Regex InvalidCharacters();
    [GeneratedRegex(@"\s+")]
    private static partial Regex MoreThanOneSpaces();
    [GeneratedRegex(@"\s")]
    private static partial Regex Spaces();
    [GeneratedRegex(@"‌")]
    private static partial Regex HalfSpaces();

    public static string Slugify(this string phrase)
    {
        var s = phrase.RemoveDiacritics().ToLower();
        s = InvalidCharacters().Replace(s, "");
        s = MoreThanOneSpaces().Replace(s, " ").Trim();
        s = s[..(s.Length <= 100 ? s.Length : 45)].Trim();
        s = Spaces().Replace(s, "-");
        s = HalfSpaces().Replace(s, "-");
        return s.ToLower();
    }

    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;

        var normalizedString = text.Normalize(NormalizationForm.FormKC);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark) stringBuilder.Append(c);
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
