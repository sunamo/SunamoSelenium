// Instance variables refactored according to C# conventions
namespace SunamoSelenium;

public static class ByHelper
{

    public static By ClassName(string classes)
    {
        return By.CssSelector("." + classes.Replace(" ", "."));
    }
}