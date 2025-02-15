namespace ForoWebApp.Models.ViewModels;

public class ErrorViewModel(string? requestId, string[]? errors)
{
    public string? RequestId { get; set; } = requestId;

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string[]? Errors { get; set; } = errors ?? [];
}
