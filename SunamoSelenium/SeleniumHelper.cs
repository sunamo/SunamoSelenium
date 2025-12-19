namespace SunamoSelenium;

using SunamoSelenium._sunamo;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

public class SeleniumHelper
{
    // CZ: URL pro stažení EdgeDriver
    private const string EdgeDriverDownloadUrl = "https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH";

    /// <summary>
    /// Opens the EdgeDriver download page in the default browser
    /// CZ: Otevře stránku pro stažení EdgeDriveru ve výchozím prohlížeči
    /// </summary>
    public static void OpenEdgeDriverDownloadPage()
    {
        PHWin.OpenUrlInDefaultBrowser(EdgeDriverDownloadUrl);
    }

    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// Poté se přihlaš. Nemá smysl to tu předávat jako metodu, do logIn bych potřeboval seleniumNavigateService, které bych musel vytvořit ručně. BuildServiceProvider volám až po přidání IWebDriver do services
    /// EN: If pathToBrowserDriver is null, WebDriverManager will automatically download the correct driver version
    /// CZ: Pokud je pathToBrowserDriver null, WebDriverManager automaticky stáhne správnou verzi driveru
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver?> InitEdgeDriver(ILogger logger, string? pathToBrowserDriver = null, EdgeOptions? options = null, bool throwEx = false)
    {
        if (options == null)
        {
            options = new();
        }

        // EN: Add arguments to avoid disconnected: not connected to DevTools error
        // CZ: Přidat argumenty pro vyvarování se chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);

        IWebDriver driver;

        // EN: If pathToBrowserDriver is null, use WebDriverManager for automatic driver download
        // CZ: Pokud je pathToBrowserDriver null, použij WebDriverManager pro automatické stažení driveru
        if (string.IsNullOrEmpty(pathToBrowserDriver))
        {
            logger.LogInformation("Using WebDriverManager for automatic EdgeDriver management");
            try
            {
                // EN: WebDriverManager will automatically download the correct driver version
                // CZ: WebDriverManager automaticky stáhne správnou verzi driveru
                new DriverManager().SetUpDriver(new EdgeConfig());
                driver = new EdgeDriver(options);
                driver.Manage().Window.Maximize();
                return driver;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to setup EdgeDriver using WebDriverManager");
                if (throwEx)
                {
                    throw;
                }
                return null;
            }
        }

        // EN: Legacy path with manual driver path and version checking
        // CZ: Starší způsob s manuální cestou k driveru a kontrolou verzí
        if (!File.Exists(pathToBrowserDriver))
        {
            logger.LogError($"File {pathToBrowserDriver} not found!");
            return null;
        }

        var ps = PowerShell.Create();
        var wd = Path.GetDirectoryName(pathToBrowserDriver);
        ps.AddScript($"Set-Location -Path '{wd}'");
        ps.AddScript(".\\" + $"{Path.GetFileName(pathToBrowserDriver)} -v");
        PSDataCollection<PSObject> output = await ps.InvokeAsync();
        List<string> result;
        Version versionEdgeDriver;
        if (ps.HadErrors)
        {
            logger.LogError("Cannot get version of " + pathToBrowserDriver);
            result = PsOutput.ProcessErrorRecords(ps.Streams.Error);
            return null;
        }
        else
        {
            result = PsOutput.ProcessPSObjects(output);
            // Microsoft Edge WebDriver 131.0.2903.48 (eb872b980f9ea5184cec7f71c2e6df8ac30265cc)
            var parts = result[0].Split(' ');
            var version = parts[3];
            versionEdgeDriver = Version.Parse(version);
        }
        ps = PowerShell.Create();
        ps.AddScript("where.exe msedge");
        var whereOutput = await PsOutput.InvokeAsync(ps);
        if (!File.Exists(whereOutput[0]))
        {
            logger.LogError("where.exe msedge failed: " + whereOutput[0]);
            return null;
        }
        ps = PowerShell.Create();
        ps.AddScript($"(Get-Item \"{whereOutput[0]}\").VersionInfo.FileVersion");
        var edgeVersionOutput = await PsOutput.InvokeAsync(ps);
        var versionEdge = Version.Parse(edgeVersionOutput[0]);
        var versionDiff = Math.Abs(versionEdge.Major - versionEdgeDriver.Major);

        if (versionDiff > 1)
        {
            // EN: Major version difference is too large (more than 1), this will likely not work
            // CZ: Rozdíl major verzí je příliš velký (více než 1), pravděpodobně nebude fungovat
            var message = $"Version EdgeDriver {versionEdgeDriver} is very different than version Edge {versionEdge}. Download new on {EdgeDriverDownloadUrl}. Use OpenEdgeDriverDownloadPage() method to open download page.";

            if (throwEx)
            {
                throw new Exception(message);
            }

            logger.LogError(message);
            return null;
        }
        else if (versionDiff == 1)
        {
            // EN: Minor version difference (1), may still work - just warn
            // CZ: Malý rozdíl verzí (1), může ještě fungovat - jen varování
            logger.LogWarning($"EdgeDriver version {versionEdgeDriver.Major} differs by 1 from Edge version {versionEdge.Major}. It should still work, but consider updating. Download new on {EdgeDriverDownloadUrl}");
        }
        else if (versionEdge.Major != versionEdgeDriver.Major)
        {
            logger.LogWarning($"Major version EdgeDriver {versionEdgeDriver.Major} is different than version Edge {versionEdge.Major}. Download new on {EdgeDriverDownloadUrl}, if you want. Use OpenEdgeDriverDownloadPage() method to open download page.");
        }


        driver = new EdgeDriver(pathToBrowserDriver, options);
        driver.Manage().Window.Maximize();
        return driver;
    }
}