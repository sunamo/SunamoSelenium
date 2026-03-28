namespace SunamoSelenium.Extensions;

/// <summary>
/// Extension methods for <see cref="Exception"/> objects.
/// </summary>
public static class ExceptionsExtensions
{
    /// <summary>
    /// Recursively collects all messages from the exception and its inner exceptions.
    /// </summary>
    /// <param name="exception">The exception to extract messages from.</param>
    /// <returns>A concatenated string of all exception messages including inner exceptions.</returns>
    public static string GetAllMessages(this Exception exception)
    {
        if (exception == null)
        {
            return "";
        }

        string message = exception.Message;

        if (exception.InnerException != null)
        {
            message += Environment.NewLine + "Inner Exception: " + exception.InnerException.GetAllMessages();
        }

        return message;
    }
}
