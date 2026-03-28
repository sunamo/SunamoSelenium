using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SunamoCl.SunamoCmd;
using SunamoDependencyInjection.Exceptions;
using SunamoSelenium;

namespace RunnerSelenium;

/// <summary>
/// Entry point for the Selenium runner application.
/// </summary>
partial class Program
{
    const string appName = "ToNugets.Cmd.Roslyn";

    static ServiceCollection services = new();
    static ServiceProvider provider;
    static ILogger logger;

    static Program()
    {
        CmdBootStrap.AddILogger(services, true, null, appName);

        provider = services.BuildServiceProvider();
        logger = provider.GetService<ILogger>() ?? throw new ServiceNotFoundException(nameof(ILogger));
    }

    static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        await CmdBootStrap.RunWithRunArgs(new SunamoCl.SunamoCmd.Args.RunArgs
        {
            AddGroupOfActions = AddGroupOfActions,
            AskUserIfRelease = true,
            Args = args,
            RunInDebugAsync = RunInDebugAsync,
            ServiceCollection = services,
            IsDebug =
#if DEBUG
          true
#else
false
#endif
        });

        Console.ReadLine();
    }

    static async Task RunInDebugAsync()
    {
        await Task.Delay(1);

        EdgeOptions options = new();
        options.AddArgument("--disable-blink-features=AutomationControlled");
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalEdgeOption("useAutomationExtension", false);
        options.AddArgument("--auto-open-devtools-for-tabs");

        options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.2572.88");

        var driver = await SeleniumHelper.InitEdgeDriver(logger, options, isThrowingException: true);
        if (driver == null)
        {
            logger.LogError("Failed to initialize Edge driver");
            return;
        }

        IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
        javaScriptExecutor.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
    }

}
