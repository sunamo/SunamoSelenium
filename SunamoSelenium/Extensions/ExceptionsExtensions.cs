namespace SunamoSelenium.Extensions;

public static class ExceptionsExtensions
{
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
