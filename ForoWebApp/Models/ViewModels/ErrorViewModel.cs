namespace ForoWebApp.Models.ViewModels;

public class ErrorViewModel(string? requestId, string? errorMessage)
{
    public string? RequestId { get; set; } = requestId;

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? ErrorMessage { get; set; } = errorMessage;
}
