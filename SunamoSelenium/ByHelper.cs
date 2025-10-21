// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
namespace SunamoSelenium;

public static class ByHelper
{

    public static By ClassName(string classes)
    {
        return By.CssSelector("." + classes.Replace(" ", "."));
    }
}