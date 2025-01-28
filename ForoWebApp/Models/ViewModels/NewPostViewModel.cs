using ForoWebApp.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models.ViewModels;

public class NewPostViewModel
{
    [Required(ErrorMessage = "El mensaje no puede estar vacío")]
    public string messageContent;

    public string MessageContent
    {
        get => messageContent;
        set => messageContent = TransformMessageHelper.TransformMessage(value);
    }
}
