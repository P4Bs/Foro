namespace ForoWebApp.Helpers.Message;

public static class TransformMessageHelper
{
    public static string TransformMessage(string messageContent)
    {
        return messageContent.Replace("\n", "<br>");
    }
}
