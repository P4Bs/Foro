using ForoWebApp.Helpers.Message;
using System.ComponentModel.DataAnnotations;

namespace ForoWebApp.Models.ViewModels;

public class NewThreadViewModel
{
    [Required(ErrorMessage = "El campo de título es obligatorio")]
    public string Title { get; set; }

    [Required(ErrorMessage = "El mensaje no puede estar vacío")]
    private string messageContent;
    public string MessageContent
    {
        get => messageContent;
        set => messageContent = TransformMessageHelper.TransformMessage(value);
    }
}
