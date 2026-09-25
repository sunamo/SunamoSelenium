namespace SunamoSelenium.Services;

/// <summary>
/// Provides navigation functionality for Selenium WebDriver with logging and page-ready waiting.
/// </summary>
public class SeleniumNavigateService(ILogger logger, IWebDriver driver, SeleniumService seleniumService)
{
    /// <summary>
    /// Navigates to the specified URL, waits for the page to be fully loaded, and logs the navigation.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    public async Task Go(string url)
    {
        try
        {
            logger.LogInformation($"Navigate to {url}");
            await driver.Navigate().GoToUrlAsync(url);
            seleniumService.WaitForPageReady();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}
