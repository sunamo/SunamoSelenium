namespace SunamoSelenium.Services;

/// <summary>
/// Provides Selenium WebDriver utility methods for waiting and finding elements.
/// </summary>
public class SeleniumService(IWebDriver driver, ILogger logger)
{
    /// <summary>
    /// Waits until the page document.readyState equals "complete", with a 15-second timeout.
    /// Logs the result and elapsed time.
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
    /// Waits up to 3 seconds for the element specified by the locator to become visible.
    /// </summary>
    /// <param name="locator">The <see cref="By"/> locator identifying the element to wait for.</param>
    public void WaitForElementIsVisible(By locator)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    /// <summary>
    /// Finds the first element matching the specified locator, or returns null if not found.
    /// </summary>
    /// <param name="locator">The <see cref="By"/> locator identifying the element to find.</param>
    /// <returns>The first matching <see cref="IWebElement"/> or null if no elements match.</returns>
    public IWebElement? FindElement(By locator)
    {
        var elements = driver.FindElements(locator);
        return elements.FirstOrDefault();
    }
}
