using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SunamoCl.SunamoCmd;
using SunamoDependencyInjection.Exceptions;
using SunamoSelenium;
using SunamoSelenium.Services;
using SunamoSelenium.Tests;

namespace RunnerSelenium;
partial class Program
{

    const string appName = "ToNugets.Cmd.Roslyn";

    static ServiceCollection Services = new();
    static ServiceProvider Provider;
    static ILogger logger;

    static Program()
    {
        CmdBootStrap.AddILogger(Services, true, null, appName);

        Provider = Services.BuildServiceProvider();
        logger = Provider.GetService<ILogger>() ?? throw new ServiceNotFoundException(nameof(ILogger));
    }

    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        var runnedAction = await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs
        {
            AddGroupOfActions = AddGroupOfActions,
            AskUserIfRelease = true,
            Args = args,
            RunInDebugAsync = RunInDebugAsync,
            ServiceCollection = Services,
            IsDebug =
#if DEBUG
          true
#else
false
#endif
        });

        //Console.WriteLine("Finished");
        Console.ReadLine();
    }

    static async Task RunInDebugAsync()
    {
        await Task.Delay(1);

        SeleniumHelperTests t = new SeleniumHelperTests();
        //await t.InitDriverTest();

        //await LoginSeznamkaCz();

        //await LoginSeznamCz();

        EdgeOptions options = new();
        options.AddArgument("--disable-blink-features=AutomationControlled");
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalEdgeOption("useAutomationExtension", false);
        options.AddArgument("--auto-open-devtools-for-tabs");

        // Volitelně zkuste přidat user-agenta běžného prohlížeče
        options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.2572.88");

        var driver = await SeleniumHelper.InitEdgeDriver(logger, options, throwEx: true);
        if (driver == null)
        {
            logger.LogError("Failed to initialize Edge driver");
            return;
        }

        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
    }


    private static async Task LoginSeznamkaCz()
    {
        // EN: Use built-in Selenium Manager for automatic EdgeDriver download
        // CZ: Použít vestavěný Selenium Manager pro automatické stažení EdgeDriveru
        var driver = await SeleniumHelper.InitEdgeDriver(logger);

        if (driver == null)
        {
            logger.LogError("Failed to initialize Edge driver");
            return;
        }

        SeleniumService seleniumService = new SeleniumService(driver, logger);

        SeleniumNavigateService seleniumNavigateService = new(logger, driver, seleniumService);

        await seleniumNavigateService.Go("https://www.seznamka.cz/");

        var acceptCookies = driver.FindElement(By.CssSelector(".fc-button.fc-cta-consent.fc-primary-button"));
        acceptCookies?.Click();
    }
}
