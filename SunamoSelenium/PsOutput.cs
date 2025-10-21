// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoSelenium;

public class PsOutput
{
    public static async Task<List<string>> InvokeAsync(PowerShell ps)
    {
        var output = await ps.InvokeAsync();
        List<string> result;
        if (ps.HadErrors)
        {
            result = PsOutput.ProcessErrorRecords(ps.Streams.Error);
        }
        else
        {
            result = PsOutput.ProcessPSObjects(output);
        }
        return result;
    }
    public static List<string> ProcessErrorRecords(PSDataCollection<ErrorRecord> errors)
    {
        List<string> result = new List<string>();
        StringBuilder stringBuilder = new();
        foreach (var item in errors)
        {
            AddErrorRecord(stringBuilder, item);
            result.Add(stringBuilder.ToString());
        }
        return result;
    }

    private static void AddErrorRecord(StringBuilder stringBuilder, ErrorRecord e)
    {
        stringBuilder.Clear();
        if (e == null) return;
        if (e.ErrorDetails != null) stringBuilder.AppendLine(e.ErrorDetails.Message);
        stringBuilder.AppendLine(e.Exception.GetAllMessages());
    }
    public static List<string> ProcessPSObjects(ICollection<PSObject> pso)
    {
        var output = new List<string>();
        foreach (var item in pso)
            if (item != null)
                output.Add(item.ToString().ToUnixLineEnding());
        return output;
    }
}