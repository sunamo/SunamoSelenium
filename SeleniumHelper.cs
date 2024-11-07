namespace SunamoSelenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

public class SeleniumHelper
{
    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver> InitDriver()
    {
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
        await driver.Navigate().GoToUrlAsync("https://www.seznamka.cz");
        //string uri = null;
        //uri = "https://www.seznamka.cz/moje/prihlaseni.aspx";
        ////uri = "https://www.seznamka.cz";
        //await seleniumNavigate.Go(uri);
        //seleniumService.WaitForNewUri(driver);
        //CL.Appeal("Press enter after login");
        //Console.ReadLine();
        //ReadOnlyCollection<OpenQA.Selenium.Cookie> cookies2 = driver.Manage().Cookies.AllCookies;
        //seleniumService.WaitForPageReady
        //(driver);
        //seleniumService.WaitForElementIsVisible( By.ClassName("fc-button fc-cta-consent fc-primary-button"));
        bool mainPage = true;
        if (mainPage)
        {
            var acceptCookies = driver.FindElement(By.CssSelector(".fc-button.fc-cta-consent.fc-primary-button"));
            acceptCookies?.Click();
        }
        var username = driver.FindElement(By.Id(mainPage ? "defsearch_login__TB_NICK" : "obsah_login_login_nick"));
        username.SendKeys("sunamo");
        var password = driver.FindElement(By.Id(mainPage ? "defsearch_login__TB_HESLO" : "obsah_login_login_heslo"));
        password.SendKeys("face2face");
        var loginButton = driver.FindElement(By.Id(mainPage ? "defsearch_login__LB_LOGIN" : "obsah_login_ctl00"));
        // furt vracelo element not interactable
        //var permanent = driver.FindElement(By.Id(mainPage ? "defsearch_login__CBX_TRVALE" : null));
        //permanent.Click();
        loginButton.Click();

        return driver;
    }
}