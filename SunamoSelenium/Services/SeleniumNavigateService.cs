// Instance variables refactored according to C# conventions
namespace SunamoSelenium.Services;

public class SeleniumNavigateService(ILogger logger, IWebDriver driver, SeleniumService selenium)
{
    public async Task Go(string newUri)
    {
        try
        {
            logger.LogInformation($"Navigate to {newUri}");
            await driver.Navigate().GoToUrlAsync(newUri);
            selenium.WaitForPageReady();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}