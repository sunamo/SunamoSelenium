namespace SunamoSelenium;

using SunamoSelenium._sunamo;

public class SeleniumHelper
{
    // CZ: URL pro stažení EdgeDriver
    private const string EdgeDriverDownloadUrl = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH";

    // CZ: URL pro stažení ChromeDriver
    private const string ChromeDriverDownloadUrl = "https://googlechromelabs.github.io/chrome-for-testing/";

    /// <summary>
    /// Opens the EdgeDriver download page in the default browser
    /// CZ: Otevře stránku pro stažení EdgeDriveru ve výchozím prohlížeči
    /// </summary>
    public static void OpenEdgeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(EdgeDriverDownloadUrl);
    }

    /// <summary>
    /// Opens the ChromeDriver download page in the default browser
    /// CZ: Otevře stránku pro stažení ChromeDriveru ve výchozím prohlížeči
    /// </summary>
    public static void OpenChromeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(ChromeDriverDownloadUrl);
    }

    /// <summary>
    /// Alias for InitEdgeDriver for backward compatibility
    /// EN: Initializes Edge WebDriver using built-in Selenium Manager (automatic driver download)
    /// CZ: Alias pro InitEdgeDriver kvůli zpětné kompatibilitě. Používá vestavěný Selenium Manager (automatické stažení driveru)
    /// </summary>
    public static async Task<IWebDriver?> InitDriver(ILogger logger, EdgeOptions? options = null, bool throwEx = false)
    {
        return await InitEdgeDriver(logger, options, throwEx);
    }

    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// Poté se přihlaš. Nemá smysl to tu předávat jako metodu, do logIn bych potřeboval seleniumNavigateService, které bych musel vytvořit ručně. BuildServiceProvider volám až po přidání IWebDriver do services
    /// EN: Uses built-in Selenium Manager (Selenium 4.6+) to automatically download and manage EdgeDriver
    /// CZ: Používá vestavěný Selenium Manager (Selenium 4.6+) pro automatické stažení a správu EdgeDriveru
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver?> InitEdgeDriver(ILogger logger, EdgeOptions? options = null, bool throwEx = false)
    {
        if (options == null)
        {
            options = new();
        }

        // EN: Add arguments to avoid disconnected: not connected to DevTools error
        // CZ: Přidat argumenty pro vyvarování se chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        // EN: Use built-in Selenium Manager for automatic EdgeDriver download and management
        // CZ: Použít vestavěný Selenium Manager pro automatické stažení a správu EdgeDriveru
        logger.LogInformation("Using built-in Selenium Manager for automatic EdgeDriver management");
        try
        {
            // EN: Enable verbose logging for debugging
            // CZ: Povolit podrobné logování pro debugging
            var service = EdgeDriverService.CreateDefaultService();
            service.LogPath = "edgedriver.log";
            service.EnableVerboseLogging = true;

            logger.LogInformation("EdgeDriver service created with verbose logging enabled at: edgedriver.log");

            // EN: Selenium Manager (built into Selenium 4.6+) will automatically download the correct driver version
            // CZ: Selenium Manager (vestavěný v Selenium 4.6+) automaticky stáhne správnou verzi driveru
            var driver = new EdgeDriver(service, options);
            driver.Manage().Window.Maximize();
            logger.LogInformation("Successfully initialized EdgeDriver using built-in Selenium Manager");
            return driver;
        }
        catch (Exception ex)
        {
            var detailedMessage = $"EdgeDriver initialization failed. " +
                $"Error: {ex.Message}. " +
                $"Inner exception: {ex.InnerException?.Message ?? "none"}. " +
                $"Stack trace: {ex.StackTrace}. " +
                $"Please ensure Edge browser is installed and EdgeDriver is compatible with your Edge version. " +
                $"Download from: {EdgeDriverDownloadUrl}";

            logger.LogError(ex, detailedMessage);

            if (throwEx)
            {
                throw new InvalidOperationException(detailedMessage, ex);
            }

            return null;
        }
    }

    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// Poté se přihlaš. Nemá smysl to tu předávat jako metodu, do logIn bych potřeboval seleniumNavigateService, které bych musel vytvořit ručně. BuildServiceProvider volám až po přidání IWebDriver do services
    /// EN: Uses built-in Selenium Manager (Selenium 4.6+) to automatically download and manage ChromeDriver
    /// CZ: Používá vestavěný Selenium Manager (Selenium 4.6+) pro automatické stažení a správu ChromeDriveru
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver?> InitChromeDriver(ILogger logger, ChromeOptions? options = null, bool throwEx = false)
    {
        await Task.Delay(0); // EN: Make method async / CZ: Udělat metodu asynchronní

        if (options == null)
        {
            options = new();
        }

        // EN: Add arguments to avoid disconnected: not connected to DevTools error
        // CZ: Přidat argumenty pro vyvarování se chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        // EN: Use built-in Selenium Manager for automatic ChromeDriver download and management
        // CZ: Použít vestavěný Selenium Manager pro automatické stažení a správu ChromeDriveru
        logger.LogInformation("Using built-in Selenium Manager for automatic ChromeDriver management");
        try
        {
            // EN: Selenium Manager (built into Selenium 4.6+) will automatically download the correct driver version
            // CZ: Selenium Manager (vestavěný v Selenium 4.6+) automaticky stáhne správnou verzi driveru
            var driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            logger.LogInformation("Successfully initialized ChromeDriver using built-in Selenium Manager");
            return driver;
        }
        catch (Exception ex)
        {
            var detailedMessage = $"ChromeDriver initialization failed. " +
                $"Error: {ex.Message}. " +
                $"Inner exception: {ex.InnerException?.Message ?? "none"}. " +
                $"Stack trace: {ex.StackTrace}. " +
                $"Please ensure Chrome browser is installed and ChromeDriver is compatible with your Chrome version. " +
                $"Download from: {ChromeDriverDownloadUrl}";

            logger.LogError(ex, detailedMessage);

            if (throwEx)
            {
                throw new InvalidOperationException(detailedMessage, ex);
            }

            return null;
        }
    }
}