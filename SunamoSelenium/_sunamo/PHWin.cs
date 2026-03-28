namespace SunamoSelenium._sunamo;

/// <summary>
/// Helper class for Windows process operations.
/// </summary>
internal class PHWin
{
    /// <summary>
    /// Opens the specified URL in the default browser on Windows.
    /// </summary>
    /// <param name="url">The URL to open.</param>
    internal static void OpenUrlInDefaultBrowser(string url)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}
