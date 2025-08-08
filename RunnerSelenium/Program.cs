using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using SunamoCl.SunamoCmd;
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

    static void Main()
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
    }



    private static async Task LoginSeznamkaCz()
    {
        var driver = await SeleniumHelper.InitEdgeDriver(logger, @"D:\pa\_dev\edgedriver_win64\msedgedriver.exe");

        SeleniumService seleniumService = new SeleniumService(driver, logger);

        SeleniumNavigateService seleniumNavigateService = new(logger, driver, seleniumService);

        await seleniumNavigateService.Go("https://www.seznamka.cz/");

        var acceptCookies = driver.FindElement(By.CssSelector(".fc-button.fc-cta-consent.fc-primary-button"));
        acceptCookies?.Click();
    }
}
