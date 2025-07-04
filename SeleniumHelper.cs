namespace SunamoSelenium;

public class SeleniumHelper
{
    /// <summary>
    /// Then add ServiceCollection.AddSingleton(typeof(IWebDriver), driver);
    /// Poté se přihlaš. Nemá smysl to tu předávat jako metodu, do logIn bych potřeboval seleniumNavigateService, které bych musel vytvořit ručně. BuildServiceProvider volám až po přidání IWebDriver do services
    /// </summary>
    /// <returns></returns>
    public static async Task<IWebDriver?> InitEdgeDriver(ILogger logger, string pathToBrowserDriver, EdgeOptions? options = null)
    {
        if (options == null)
        {
            options = new();
        }

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
        if (versionEdge.Major != versionEdgeDriver.Major)
        {
            logger.LogError($"Version EdgeDriver {versionEdgeDriver} is different than version Edge {versionEdge}. Download new on https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH");
            return null;
        }
        else if (versionEdge.Major != versionEdgeDriver.Major)
        {
            logger.LogWarning($"Major version EdgeDriver {versionEdgeDriver.Major} is different than version Edge {versionEdge.Major}. Download new on https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/?form=MA13LH, if you want.");
        }

        // toto tu je abych se vyhnul chybě disconnected: not connected to DevTools
        options.AddArguments(["--disable-dev-shm-usage", "--no-sandbox"]);
        var driver = new EdgeDriver(pathToBrowserDriver, options);
        driver.Manage().Window.Maximize();
        return driver;
    }
}