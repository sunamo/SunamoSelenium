namespace SunamoSelenium.Services;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
/// <summary>
/// Když načtu např. https://www.firmy.cz/Banky-a-financni-sluzby/Ucetni-sluzby/Danove-poradenstvi/kraj-praha nepříhlášený
/// Zobrazí se mi CMP - i přes mnoho snahy nenašel jsem způsob jak přes něj v Selenium přejít
/// Stačí se ale přihlásit a je vše OK
/// </summary>
public class CmpWorkaroundService(ILogger logger)
{
    public async Task LoginSeznamCz(string email, string password)
    {
        // EN: Use Selenium Manager for automatic EdgeDriver download and management
        // CZ: Použij Selenium Manager pro automatické stažení a správu EdgeDriveru
        var driver = await SeleniumHelper.InitEdgeDriver(logger);

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