namespace SunamoSelenium.Services;

public class CmpWorkaroundService(ILogger logger)
{
    public async Task LoginSeznamCz(string email, string password)
    {
        var driver = await SeleniumHelper.InitEdgeDriver(logger);

        if (driver == null)
        {
            logger.LogError("Failed to initialize Edge driver for Seznam.cz login");
            return;
        }

        SeleniumService seleniumService = new SeleniumService(driver, logger);

        SeleniumNavigateService seleniumNavigateService = new(logger, driver, seleniumService);

        await seleniumNavigateService.Go(@"https://login.szn.cz/");

        var usernameField = driver.FindElement(By.Id("login-username"));

        usernameField.SendKeys(email);

        IWebElement submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));

        submitButton.Click();

        var passwordField = driver.FindElement(By.Id("login-password"));

        passwordField.SendKeys(password);

        submitButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        submitButton.Click();
    }
}
