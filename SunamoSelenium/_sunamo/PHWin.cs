namespace SunamoSelenium._sunamo;

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class PHWin
{
    internal static void OpenUrlInDefaultBrowser(string url)
    {
        // Otevře URL ve výchozím prohlížeči na Windows
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}