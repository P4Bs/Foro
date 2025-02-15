using ForoWebApp.Features.Common;

namespace ForoWebApp.Features.Threads.CloseThread;

public class CloseThreadResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
}
