namespace SunamoSelenium.Extensions;

public static class StringToUnixLineEndingExtensions
{
    public static string ToUnixLineEnding(this string text) => text.ReplaceLineEndings("\n");
}
