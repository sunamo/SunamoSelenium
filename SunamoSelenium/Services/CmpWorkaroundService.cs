namespace SunamoSelenium.Services;

/// <summary>
/// Workaround service for CMP (Consent Management Platform) dialogs.
/// When loading certain pages (e.g. firmy.cz) without authentication, a CMP dialog appears.
/// Logging in first bypasses the CMP dialog.
/// </summary>
public class CmpWorkaroundService(ILogger logger)
{
    /// <summary>
    /// Logs into Seznam.cz account using Edge WebDriver to bypass CMP dialogs on Seznam services.
    /// </summary>
    /// <param name="email">The Seznam.cz account email address.</param>
    /// <param name="password">The account password.</param>
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
