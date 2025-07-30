namespace SunamoSelenium.Tests;

using Microsoft.Extensions.Logging;
using SunamoTest;

public class SeleniumHelperTests
{
    ILogger logger = TestLogger.Instance;

    [Fact]
    public async Task InitDriverTest()
    {
        var d = await SeleniumHelper.InitEdgeDriver(logger, @"D:\pa\_dev\edgedriver_win64\msedgedriver.exe");


    }
}