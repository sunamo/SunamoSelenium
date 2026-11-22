using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SunamoSelenium;

/// <summary>
/// Placeholder web driver for dependency injection in hosts that never open a browser; every member throws NotImplementedException.
/// </summary>
public class WebDriverDummy : IWebDriver
{
    /// <inheritdoc/>
    public string Url { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <inheritdoc/>
    public string Title => throw new NotImplementedException();

    /// <inheritdoc/>
    public string PageSource => throw new NotImplementedException();

    /// <inheritdoc/>
    public string CurrentWindowHandle => throw new NotImplementedException();

    /// <inheritdoc/>
    public ReadOnlyCollection<string> WindowHandles => throw new NotImplementedException();

    /// <inheritdoc/>
    public void Close()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IWebElement FindElement(By by)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IOptions Manage()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public INavigation Navigate()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Quit()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public ITargetLocator SwitchTo()
    {
        throw new NotImplementedException();
    }
}
