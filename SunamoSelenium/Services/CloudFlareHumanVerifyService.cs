namespace SunamoSelenium.Services;

internal class CloudFlareHumanVerifyService(IWebDriver driver)
{
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
