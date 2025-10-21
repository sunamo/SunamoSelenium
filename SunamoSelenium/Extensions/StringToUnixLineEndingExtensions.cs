// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
﻿namespace SunamoSelenium.Extensions;

public static class StringToUnixLineEndingExtensions
{
    public static string ToUnixLineEnding(this string s)
    {
        return s.ReplaceLineEndings("\n");
    }
}
