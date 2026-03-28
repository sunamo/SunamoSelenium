namespace SunamoSelenium.Tests;

using Microsoft.Extensions.Logging;
using SunamoTest;

/// <summary>
/// Tests for <see cref="SeleniumHelper"/> initialization and driver management.
/// </summary>
public class SeleniumHelperTests
{
    private readonly ILogger logger = TestLogger.Instance;

    /// <summary>
    /// Verifies that InitEdgeDriver
    /// successfully initializes an EdgeDriver instance using Selenium Manager.
    /// </summary>
    [Fact]
    public async Task InitDriverTest()
    {
        var driver = await SeleniumHelper.InitEdgeDriver(logger);

        Assert.NotNull(driver);

        driver.Quit();
    }
}
