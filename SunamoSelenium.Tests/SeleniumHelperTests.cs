// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoSelenium.Tests;

using Microsoft.Extensions.Logging;
using SunamoTest;

public class SeleniumHelperTests
{
    ILogger logger = TestLogger.Instance;

    [Fact]
    public async Task InitDriverTest()
    {
        var data = await SeleniumHelper.InitEdgeDriver(logger, @"D:\pa\_dev\edgedriver_win64\msedgedriver.exe");


    }
}