using ForoWebApp.Features.Common;

namespace ForoWebApp.Features.Threads.CreateThread;

public class CreateThreadResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public string? ThreadId { get; set; }
}
