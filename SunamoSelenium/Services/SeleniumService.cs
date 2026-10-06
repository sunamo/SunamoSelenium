namespace SunamoSelenium.Services;

public class SeleniumService(IWebDriver driver, ILogger logger)
{
    public void WaitForPageReady()
    {
        var startTime = DateTime.Now;
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        var isSuccessful = wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState")?.Equals("complete") == true);
        var elapsedTime = DateTime.Now - startTime;
        var statusText = isSuccessful ? "" : "NOT";
        logger.LogWarning($"Waiting for page {driver.Url} was {statusText} successful. Waiting seconds: {elapsedTime.TotalSeconds}");
    }

    public void WaitForElementIsVisible(By locator)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    public IWebElement? FindElement(By locator)
    {
        var elements = driver.FindElements(locator);
        return elements.FirstOrDefault();
    }
}
