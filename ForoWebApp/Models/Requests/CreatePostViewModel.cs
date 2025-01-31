using ForoWebApp.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models.Requests;

public class CreatePostViewModel
{
    [Required(ErrorMessage = "El mensaje no puede estar vacío")]
    private string messageContent;
    public string MessageContent
    {
        get => messageContent;
        set => messageContent = TransformMessageHelper.TransformMessage(value);
    }
}
