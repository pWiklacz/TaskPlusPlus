using System.Text.RegularExpressions;

namespace TaskPlusPlus.Domain.Extensions;
public static class StringExtensions
{
    public static string SplitCamelCase(this string input)
    {
        string pattern = @"(\p{Lu}\p{Ll}*|\p{Ll}+|\d+)";
        MatchCollection matches = Regex.Matches(input, pattern);
        string result = "";

        for (var i = 0; i < matches.Count; i++)
        {
            var word = matches[i].Value;
            if (i == 0)
            {
                result += word;
            }
            else
            {
                result += char.ToLower(word[0]) + word[1..];
            }

            if (i < matches.Count - 1)
            {
                result += " ";
            }
        }

        return result;
    }
}