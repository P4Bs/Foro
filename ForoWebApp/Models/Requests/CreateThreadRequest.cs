using ForoWebApp.Helpers.Message;

namespace ForoWebApp.Models.Requests;

public class CreateThreadRequest
{
    public string Title { get; set; }
    private string messageContent;
    public string MessageContent
    {
        get => messageContent;
        set => messageContent = TransformMessageHelper.TransformMessage(value);
    }
}
