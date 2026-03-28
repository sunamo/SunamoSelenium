namespace SunamoSelenium;

using SunamoSelenium._sunamo;

/// <summary>
/// Helper class for initializing and managing Selenium WebDriver instances.
/// </summary>
public class SeleniumHelper
{
    private const string edgeDriverDownloadUrl = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH";

    private const string chromeDriverDownloadUrl = "https://googlechromelabs.github.io/chrome-for-testing/";

    /// <summary>
    /// Opens the EdgeDriver download page in the default browser.
    /// </summary>
    public static void OpenEdgeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(edgeDriverDownloadUrl);
    }

    /// <summary>
    /// Opens the ChromeDriver download page in the default browser.
    /// </summary>
    public static void OpenChromeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(chromeDriverDownloadUrl);
    }

    /// <summary>
    /// Alias for <see cref="InitEdgeDriver(ILogger, EdgeOptions?, bool)"/> for backward compatibility.
    /// Initializes Edge WebDriver using built-in Selenium Manager (automatic driver download).
    /// </summary>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="options">Optional Edge browser options.</param>
    /// <param name="isThrowingException">When true, throws an exception on failure instead of returning null.</param>
    /// <returns>An initialized <see cref="IWebDriver"/> instance or null on failure.</returns>
    public static async Task<IWebDriver?> InitDriver(ILogger logger, EdgeOptions? options = null, bool isThrowingException = false)
    {
        return await InitEdgeDriver(logger, options, isThrowingException);
    }

    /// <summary>
    /// OBSOLETE: Do NOT use hardcoded driver paths. This method throws an exception.
    /// Always use Selenium Manager for automatic driver management by calling
    /// <see cref="InitEdgeDriver(ILogger, EdgeOptions?, bool)"/> without path parameter.
    /// </summary>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="driverPath">The hardcoded driver path (not supported).</param>
    /// <param name="options">Optional Edge browser options.</param>
    /// <param name="isThrowingException">When true, throws an exception on failure instead of returning null.</param>
    /// <returns>Never returns; always throws <see cref="InvalidOperationException"/>.</returns>
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

    /// <summary>
    /// Initializes an Edge WebDriver instance using built-in Selenium Manager (Selenium 4.6+)
    /// for automatic driver download and management.
    /// After initialization, register the driver via ServiceCollection.AddSingleton(typeof(IWebDriver), driver).
    /// </summary>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="options">Optional Edge browser options. If null, default options are created.</param>
    /// <param name="isThrowingException">When true, throws an exception on failure instead of returning null.</param>
    /// <returns>An initialized <see cref="IWebDriver"/> instance or null on failure.</returns>
    public static async Task<IWebDriver?> InitEdgeDriver(ILogger logger, EdgeOptions? options = null, bool isThrowingException = false)
    {
        if (options == null)
        {
            options = new();
        }

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

    /// <summary>
    /// Initializes a Chrome WebDriver instance using built-in Selenium Manager (Selenium 4.6+)
    /// for automatic driver download and management.
    /// After initialization, register the driver via ServiceCollection.AddSingleton(typeof(IWebDriver), driver).
    /// </summary>
    /// <param name="logger">The logger instance for diagnostic output.</param>
    /// <param name="options">Optional Chrome browser options. If null, default options are created.</param>
    /// <param name="isThrowingException">When true, throws an exception on failure instead of returning null.</param>
    /// <returns>An initialized <see cref="IWebDriver"/> instance or null on failure.</returns>
    public static async Task<IWebDriver?> InitChromeDriver(ILogger logger, ChromeOptions? options = null, bool isThrowingException = false)
    {
        await Task.Delay(0);

        if (options == null)
        {
            options = new();
        }

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
