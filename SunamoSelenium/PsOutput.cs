namespace SunamoSelenium;

/// <summary>
/// Represents the ps output type.
/// </summary>
public class PsOutput
{
    /// <summary>
    /// Invoke async.
    /// </summary>
    /// <param name="powerShell">The power shell.</param>
    /// <returns>The invoke async result.</returns>
    public static async Task<List<string>> InvokeAsync(PowerShell powerShell)
    {
        var output = await powerShell.InvokeAsync();
        List<string> result;
        if (powerShell.HadErrors)
        {
            result = ProcessErrorRecords(powerShell.Streams.Error);
        }
        else
        {
            result = ProcessPSObjects(output);
        }
        return result;
    }

    /// <summary>
    /// Process error records.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns>The process error records result.</returns>
    public static List<string> ProcessErrorRecords(PSDataCollection<ErrorRecord> errors)
    {
        var result = new List<string>();
        StringBuilder stringBuilder = new();
        foreach (var item in errors)
        {
            AddErrorRecord(stringBuilder, item);
            result.Add(stringBuilder.ToString());
        }
        return result;
    }

    private static void AddErrorRecord(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        stringBuilder.Clear();
        if (errorRecord is null) return;
        if (errorRecord.ErrorDetails is not null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);
        stringBuilder.AppendLine(errorRecord.Exception.GetAllMessages());
    }

    /// <summary>
    /// Process PS objects.
    /// </summary>
    /// <param name="collection">The collection.</param>
    /// <returns>The process PS objects result.</returns>
    public static List<string> ProcessPSObjects(ICollection<PSObject> collection)
    {
        var result = new List<string>();
        foreach (var item in collection)
            if (item is not null)
                result.Add(item.ToString().ToUnixLineEnding());
        return result;
    }
}
