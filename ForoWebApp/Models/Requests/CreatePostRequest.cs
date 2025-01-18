using ForoWebApp.Helpers.Message;

namespace ForoWebApp.Models.Requests;

public class CreatePostRequest
{
    public string ThreadId { get; set; }
    public string UserId { get; set; }
    private string messageContent;
    public string MessageContent
    {
        get => messageContent;
        set => messageContent = TransformMessageHelper.TransformMessage(value);
    }
}
