namespace SunamoSelenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

public class SeleniumHelper
{
    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver> InitDriver(Func<IWebDriver, Task> logIn)
    {
        ArgumentNullException.ThrowIfNull(logIn, nameof(logIn));

        //string? html = null;
        IWebDriver? driver = null;
        //driver = new ChromeDriver(@"D:\pa\_dev\_selenium\chromedriver-win64\");

        EdgeOptions options = new();
        // toto tu je abych se vyhnul chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        driver = new EdgeDriver(@"D:\pa\_dev\edgedriver_win64\", options);
        // otevře se firefox ale žádnou stránku 
        //driver = new FirefoxDriver(@"D:\pa\_dev\_selenium\geckodriver-v0.35.0-win64\");
        driver.Manage().Window.Maximize();

        await logIn(driver);

        return driver;
    }
}