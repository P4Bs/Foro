namespace ForoWebApp.Validators;

public class FieldValidation(string Field, string Message)
{
    public string FieldName { get; set; } = Field;
    public string ValidationMessage { get; set; } = Message;
}
