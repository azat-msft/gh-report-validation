namespace StringUtilsTests;

/// <summary>
/// A tiny string utility used to exercise the GitHub Actions report extension.
/// </summary>
public sealed class StringUtils
{
    public string Reverse(string value)
    {
        char[] chars = value.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public bool IsPalindrome(string value)
        => string.Equals(value, Reverse(value), StringComparison.Ordinal);

    public int CountWords(string value)
        => string.IsNullOrWhiteSpace(value)
            ? 0
            : value.Split([' ', '\t', '\n'], StringSplitOptions.RemoveEmptyEntries).Length;
}
