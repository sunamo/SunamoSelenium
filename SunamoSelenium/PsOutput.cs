namespace SunamoSelenium;

/// <summary>
/// Provides methods for invoking PowerShell commands and processing their output.
/// </summary>
public class PsOutput
{
    /// <summary>
    /// Asynchronously invokes a PowerShell pipeline and returns the output as a list of strings.
    /// If errors occurred, returns formatted error messages instead.
    /// </summary>
    /// <param name="powerShell">The PowerShell instance to invoke.</param>
    /// <returns>A list of strings containing either the output or error messages.</returns>
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
    /// Processes a collection of PowerShell error records into formatted string messages.
    /// </summary>
    /// <param name="errors">The collection of error records to process.</param>
    /// <returns>A list of formatted error messages.</returns>
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

    /// <summary>
    /// Appends error details from an <see cref="ErrorRecord"/> to the provided <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="stringBuilder">The string builder to append error details to.</param>
    /// <param name="errorRecord">The error record containing the error details.</param>
    private static void AddErrorRecord(StringBuilder stringBuilder, ErrorRecord errorRecord)
    {
        stringBuilder.Clear();
        if (errorRecord == null) return;
        if (errorRecord.ErrorDetails != null) stringBuilder.AppendLine(errorRecord.ErrorDetails.Message);
        stringBuilder.AppendLine(errorRecord.Exception.GetAllMessages());
    }

    /// <summary>
    /// Converts a collection of PowerShell objects to a list of strings with Unix line endings.
    /// </summary>
    /// <param name="collection">The collection of PowerShell objects to process.</param>
    /// <returns>A list of string representations with Unix line endings.</returns>
    public static List<string> ProcessPSObjects(ICollection<PSObject> collection)
    {
        var result = new List<string>();
        foreach (var item in collection)
            if (item != null)
                result.Add(item.ToString().ToUnixLineEnding());
        return result;
    }
}
