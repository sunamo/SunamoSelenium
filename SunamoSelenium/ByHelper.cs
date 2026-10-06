namespace SunamoSelenium;

public static class ByHelper
{
    /// <summary>
    /// Creates a CSS selector <see cref="By"/> locator from space-separated class names.
    /// Converts "class1 class2" to ".class1.class2" CSS selector format.
    /// </summary>
    /// <param name="text">Space-separated CSS class names.</param>
    /// <returns>A <see cref="By"/> CSS selector locator matching all specified classes.</returns>
    public static By ClassName(string text) => By.CssSelector("." + text.Replace(" ", "."));
}
