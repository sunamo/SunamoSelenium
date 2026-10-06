namespace SunamoSelenium.Services;

public class SeleniumNavigateService(ILogger logger, IWebDriver driver, SeleniumService seleniumService)
{
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
