namespace SunamoSelenium;

public static class ByHelper
{

    public static By ClassName(string classes)
    {
        return By.CssSelector("." + classes.Replace(" ", "."));
    }
}