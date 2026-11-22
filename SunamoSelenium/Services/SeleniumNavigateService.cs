namespace SunamoSelenium.Services;

/// <summary>
/// Selenium navigate service.
/// </summary>
/// <param name="logger">The logger.</param>
/// <param name="driver">The driver.</param>
/// <param name="seleniumService">The selenium service.</param>
/// <returns>The selenium navigate service result.</returns>
public class SeleniumNavigateService(ILogger logger, IWebDriver driver, SeleniumService seleniumService)
{
    /// <summary>
    /// Go.
    /// </summary>
    /// <param name="url">The url.</param>
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
