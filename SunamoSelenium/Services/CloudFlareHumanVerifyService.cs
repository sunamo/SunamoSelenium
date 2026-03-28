namespace SunamoSelenium.Services;

/// <summary>
/// Service for handling CloudFlare human verification challenges in Selenium.
/// Note: The shadow-root used by CloudFlare is closed, making direct interaction very difficult.
/// Consider using retries with delays as an alternative approach.
/// </summary>
internal class CloudFlareHumanVerifyService(IWebDriver driver)
{
    /// <summary>
    /// Attempts to complete the CloudFlare human verification challenge by navigating
    /// through the shadow DOM and clicking the verification checkbox.
    /// </summary>
    public async Task VerifyHuman()
    {
        var root = driver.FindElement(By.CssSelector(".main-content"));
        root = root.FindElement(By.Id("DPxlC8"));

        root = root.FindElement(By.TagName("div"));
        root = root.FindElement(By.TagName("div"));

        var shadowRoot = root.GetShadowRoot();

        var iframe = shadowRoot.FindElement(By.TagName("iframe"));

        driver.SwitchTo().Frame(iframe);

        var checkArea = shadowRoot.FindElement(By.Id("DPxlC8"));
        var checkboxInput = checkArea.FindElement(By.TagName("input"));
        checkboxInput.Click();

        driver.SwitchTo().DefaultContent();
    }
}
