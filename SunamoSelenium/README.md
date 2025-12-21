Jak toto použít v nové aplikaci:

Přidáte WebDriver - potřebujete edge / chrome web driver na vašem disku:

    internal static async Task<ServiceProvider> AddWebDriverService(ServiceCollection Services)
    {
        var driver = await SeleniumHelper.InitDriver(logger, @"D:\pa\_dev\edgedriver_win64\msedgedriver.exe");
    
        if (driver == null)
        {
            throw new ServiceNotFoundException(nameof(IWebDriver));
        }
    
        return AddSingletonService<IWebDriver>(Services, driver);
    }

nject the services via DI (Dependency Injection):

SeleniumNavigateService
SeleniumService

and then you can just use it.
## Target Frameworks

**TargetFrameworks:** `net10.0;net9.0`

**Reason:** Dependencies require .NET 9.0+:
- PowerShell SDK 7.5.0+ requires net9.0
- System.Management.Automation 7.5.0 requires net9.0
- Lock type (System.Threading.Lock) available from net9.0
