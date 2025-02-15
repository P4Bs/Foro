namespace ForoWebApp.Features.Common;

public abstract class BaseResponse(bool success, string[]? errors = null)
{
    public bool Success { get; set; } = success;
    public string[]? Errors { get; set; } = errors ?? [];
}
