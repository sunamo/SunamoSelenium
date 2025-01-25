namespace SunamoSelenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

public class SeleniumHelper
{
    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// Poté se přihlaš. Nemá smysl to tu předávat jako metodu, do logIn bych potřeboval seleniumNavigateService, které bych musel vytvořit ručně. BuildServiceProvider volám až po přidání IWebDriver do services
    /// </summary>
    /// <returns></returns>
    public static IWebDriver InitDriver(string pathToEdgeDriver)
    {
        EdgeOptions options = new();
        // toto tu je abych se vyhnul chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        //@"D:\pa\_dev\edgedriver_win64\"
        var driver = new EdgeDriver(pathToEdgeDriver, options);
        // otevře se firefox ale žádnou stránku 
        //driver = new FirefoxDriver(@"D:\pa\_dev\_selenium\geckodriver-v0.35.0-win64\");
        driver.Manage().Window.Maximize();

        return driver;
    }
}