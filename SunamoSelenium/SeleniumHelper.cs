namespace SunamoSelenium;

using SunamoSelenium._sunamo;

public class SeleniumHelper
{
    private const string edgeDriverDownloadUrl = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH";

    private const string chromeDriverDownloadUrl = "https://googlechromelabs.github.io/chrome-for-testing/";

    public static void OpenEdgeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(edgeDriverDownloadUrl);
    }

    public static void OpenChromeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(chromeDriverDownloadUrl);
    }

    public static async Task<IWebDriver?> InitDriver(ILogger logger, EdgeOptions? options = null, bool isThrowingException = false)
    {
        return await InitEdgeDriver(logger, options, isThrowingException);
    }

    [Obsolete("Do NOT use hardcoded driver paths! Use InitEdgeDriver(logger, options) without driverPath parameter. Selenium Manager will automatically download the correct driver version.", true)]
    public static Task<IWebDriver?> InitEdgeDriver(ILogger logger, string driverPath, EdgeOptions? options = null, bool isThrowingException = false)
    {
        throw new InvalidOperationException(
            "CRITICAL: Do NOT use hardcoded EdgeDriver paths! " +
            "Selenium Manager (built into Selenium 4.6+) automatically downloads and manages the correct driver version. " +
            "Usage: await SeleniumHelper.InitEdgeDriver(logger, options); " +
            "Remove the 'driverPath' parameter from your code. " +
            "Selenium Manager will automatically detect your Edge browser version and download the matching EdgeDriver. " +
            "This ensures compatibility and eliminates manual driver management.");
    }

    public static async Task<IWebDriver?> InitEdgeDriver(ILogger logger, EdgeOptions? options = null, bool isThrowingException = false)
    {
        options ??= new();

        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        logger.LogInformation("Using built-in Selenium Manager for automatic EdgeDriver management");
        try
        {
            var service = EdgeDriverService.CreateDefaultService();
            service.LogPath = "edgedriver.log";
            service.EnableVerboseLogging = true;

            logger.LogInformation("EdgeDriver service created with verbose logging enabled at: edgedriver.log");

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
                $"Download from: {edgeDriverDownloadUrl}";

            logger.LogError(ex, detailedMessage);

            if (isThrowingException)
            {
                throw new InvalidOperationException(detailedMessage, ex);
            }

            return null;
        }
    }

    public static async Task<IWebDriver?> InitChromeDriver(ILogger logger, ChromeOptions? options = null, bool isThrowingException = false)
    {
        await Task.Delay(0);

        options ??= new();

        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        logger.LogInformation("Using built-in Selenium Manager for automatic ChromeDriver management");
        try
        {
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
                $"Download from: {chromeDriverDownloadUrl}";

            logger.LogError(ex, detailedMessage);

            if (isThrowingException)
            {
                throw new InvalidOperationException(detailedMessage, ex);
            }

            return null;
        }
    }
}
