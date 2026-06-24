namespace SunamoSelenium;

public static class ByHelper
{
    public static By ClassName(string text) => By.CssSelector("." + text.Replace(" ", "."));
}
