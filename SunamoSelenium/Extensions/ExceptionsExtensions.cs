namespace SunamoSelenium.Extensions;

/// <summary>
/// Represents the exceptions extensions type.
/// </summary>
public static class ExceptionsExtensions
{
    /// <summary>
    /// Get all messages.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>The get all messages result.</returns>
    public static string GetAllMessages(this Exception exception)
    {
        if (exception is null)
        {
            return "";
        }

        string message = exception.Message;

        if (exception.InnerException is not null)
        {
            message += Environment.NewLine + "Inner Exception: " + exception.InnerException.GetAllMessages();
        }

        return message;
    }
}
