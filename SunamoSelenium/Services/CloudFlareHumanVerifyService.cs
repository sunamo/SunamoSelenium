namespace SunamoSelenium.Services;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
// Instance variables refactored according to C# conventions
internal class CloudFlareHumanVerifyService(IWebDriver driver)
{
    public async Task VerifyHuman()
    {
        /*
Adresa na chess.com zůstane stejná, zobrazí se abych odkliknul že nejsem člověk
        Jedná se o Cloudflare ověření: https://www.google.com/search?sca_esv=ab17197b43d8bab3&sxsrf=AHTn8zryHSObn_XiPpdTiG-c31zfECtotw:1743502449865&q=cloudflare+verify+human&udm=2&fbs=ABzOT_CGHNgROzTrfye-u2LQKYbNdoiqXffZpadQunPyAP9GMCnXYV3lmGuFu_8-6P4Y4A9Iztq8QsG-Mcz1uavH9Aat_qeTmNQoKYnzuptN8wUjogpvc0-6k2uW9qBck1vxF8CXs_bnpelrNYLQR3al8NfAP8BWWq4UyjgFn60IJBTLwUkUWMzP60s0-ALF2-4LRnBO1kXj&sa=X&ved=2ahUKEwjXucHhzLaMAxUtg_0HHV9-O64QtKgLegQIExAB&biw=1270&bih=1272
Po nějakém čase se mi stránka načte i bez potvrzení
        Problém zde je že shadow-root je closed
        Mělo by to jít obejít ale je to hodně složité: https://www.google.com/search?q=open+shadow+root+in+selenium+which+is+closed

        Už jsem na tom strávil mnoho času, buď:
        1/ To načíst na vícekrát
        2/ Dát timeout mezi jednotlivými načtením
 */

        #region Pokud nastane, je třeba zvýšit čas. Oni si myslí že nejsem člověk
        // Nutno použít CssSelector, not ClassName - ten nefunguje!!!
        var root = driver.FindElement(By.CssSelector(".main-content"));
        root = root.FindElement(By.Id("DPxlC8"));

        IReadOnlyCollection<IWebElement> allChildElementsByXPath = root.FindElements(By.XPath(".//*"));

        //https://www.chess.com/games/archive?gameOwner=my_game&gameType=live&page=11

        root = root.FindElement(By.TagName("div"));
        root = root.FindElement(By.TagName("div"));

        var shadowRoot = root.GetShadowRoot();

        // cf-chl-widget-c835k
        //var iframe = shadowRoot.FindElement(By.Id("cf-chl-widget-6wdxu"));
        // invalid argument: invalid locator
        var iframe = shadowRoot.FindElement(By.TagName("iframe"));

        driver.SwitchTo().Frame(iframe);

        var checkArea = shadowRoot.FindElement(By.Id("DPxlC8"));
        var input = checkArea.FindElement(By.TagName("input"));
        input.Click();

        driver.SwitchTo().DefaultContent();
        #endregion
    }
}