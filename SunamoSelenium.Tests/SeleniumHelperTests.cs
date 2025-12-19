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
        // EN: Use Selenium Manager for automatic EdgeDriver download and management
        // CZ: Použij Selenium Manager pro automatické stažení a správu EdgeDriveru
        var data = await SeleniumHelper.InitEdgeDriver(logger);

        Assert.NotNull(data);
    }
}