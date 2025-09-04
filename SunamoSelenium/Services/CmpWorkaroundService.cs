// Instance variables refactored according to C# conventions
namespace SunamoSelenium.Services;
/// <summary>
/// Když načtu např. https://www.firmy.cz/Banky-a-financni-sluzby/Ucetni-sluzby/Danove-poradenstvi/kraj-praha nepříhlášený
/// Zobrazí se mi CMP - i přes mnoho snahy nenašel jsem způsob jak přes něj v Selenium přejít
/// Stačí se ale přihlásit a je vše OK
/// </summary>
public class CmpWorkaroundService(ILogger logger)
{
    public async Task LoginSeznamCz(string email, string password)
    {
        var driver = await SeleniumHelper.InitEdgeDriver(logger, @"D:\pa\_dev\edgedriver_win64\msedgedriver.exe");

        SeleniumService seleniumService = new SeleniumService(driver, logger);

        SeleniumNavigateService seleniumNavigateService = new(logger, driver, seleniumService);

        await seleniumNavigateService.Go(@"https://login.szn.cz/");


        var mail = driver.FindElement(By.Id("login-username"));

        mail.SendKeys(email);

        IWebElement submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

        submitButton.Click();

        var pw = driver.FindElement(By.Id("login-password"));

        pw.SendKeys(password);

        submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        submitButton.Click();


    }
}
