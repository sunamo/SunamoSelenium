namespace SunamoSelenium.Services;

public class SeleniumService(IWebDriver driver, ILogger logger)
{
    public void WaitForPageReady()
    {
        var actual = DateTime.Now;
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        var waitResult = wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        var diff = DateTime.Now - actual;
        var part = waitResult ? "" : "NOT";
        logger.LogWarning($"Waiting for page {driver.Url} was {part} successful. Waiting seconds: {diff.TotalSeconds}");
    }
    /// <summary>
    /// Tuhle metodu jsem zakomentoval ale bez komentáře
    /// Nevím tedy jestli na ní bylo něco špatně.
    /// 
    /// </summary>
    /// <param name="by"></param>
    public void WaitForElementIsVisible(By by)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementIsVisible(by));
    }
    public IWebElement? FindElement(By by)
    {
        var elements = driver.FindElements(by);
        return elements.FirstOrDefault();
    }
    //public static System.Net.Cookie ToNetCookie(this OpenQA.Selenium.Cookie seleniumCookie)
    //{
    //    var netCookie = new System.Net.Cookie(
    //        seleniumCookie.Name,
    //        seleniumCookie.Value,
    //        seleniumCookie.Path,
    //        seleniumCookie.Domain)
    //    {
    //        HttpOnly = seleniumCookie.IsHttpOnly,
    //        Secure = seleniumCookie.Secure
    //    };
    //    if (seleniumCookie.Expiry.HasValue)
    //    {
    //        netCookie.Expires = seleniumCookie.Expiry.Value;
    //    }
    //    // SameSite mapování
    //    //switch (seleniumCookie.SameSite)
    //    //{
    //    //    case SameSiteMode.None:
    //    //        netCookie.SameSite = SameSiteMode.None;
    //    //        break;
    //    //    case SameSiteMode.Lax:
    //    //        netCookie.SameSite = SameSiteMode.Lax;
    //    //        break;
    //    //    case SameSiteMode.Strict:
    //    //        netCookie.SameSite = SameSiteMode.Strict;
    //    //        break;
    //    //    default:
    //    //        // Pro neznámé hodnoty SameSite v Selenium Cookie, 
    //    //        // nastavte SameSiteMode.Unspecified v .NET Cookie.
    //    //        netCookie.SameSite = SameSiteMode.Unspecified;
    //    //        break;
    //    //}
    //    return netCookie;
    //}
}