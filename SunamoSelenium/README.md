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