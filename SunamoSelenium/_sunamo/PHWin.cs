namespace SunamoSelenium._sunamo;

internal class PHWin
{
    internal static void OpenUrlInDefaultBrowser(string url)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}
