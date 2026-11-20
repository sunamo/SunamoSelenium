namespace SunamoSelenium.Services;

/// <summary>
/// Selenium service.
/// </summary>
/// <param name="driver">The driver.</param>
/// <param name="logger">The logger.</param>
/// <returns>The selenium service result.</returns>
public class SeleniumService(IWebDriver driver, ILogger logger)
{
    /// <summary>
    /// Wait for page ready.
    /// </summary>
    public void WaitForPageReady()
    {
        var startTime = DateTime.Now;
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        var isSuccessful = wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState")?.Equals("complete") == true);
        var elapsedTime = DateTime.Now - startTime;
        var statusText = isSuccessful ? "" : "NOT";
        logger.LogWarning($"Waiting for page {driver.Url} was {statusText} successful. Waiting seconds: {elapsedTime.TotalSeconds}");
    }

    /// <summary>
    /// Wait for element is visible.
    /// </summary>
    /// <param name="locator">The locator.</param>
    public void WaitForElementIsVisible(By locator)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    /// <summary>
    /// Find element.
    /// </summary>
    /// <param name="locator">The locator.</param>
    /// <returns>The find element result.</returns>
    public IWebElement? FindElement(By locator)
    {
        var elements = driver.FindElements(locator);
        return elements.FirstOrDefault();
    }
}
