using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoSelenium._sunamo;

internal class PHWin
{
    public static void OpenUrlInDefaultBrowser(string url)
    {
        // Otevře URL ve výchozím prohlížeči na Windows
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}
