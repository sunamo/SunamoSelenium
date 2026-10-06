namespace SunamoSelenium.Extensions;

public static class StringToUnixLineEndingExtensions
{
    /// <summary>
    /// Converts all line endings in the string to Unix-style line endings (LF).
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <returns>The string with Unix-style line endings.</returns>
    public static string ToUnixLineEnding(this string text) => text.ReplaceLineEndings("\n");
}
