using ForoWebApp.Features.Common;
using ForoWebApp.Models.ViewModels;

namespace ForoWebApp.Features.Threads.GetThread;

public class GetThreadResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public ThreadViewModel ThreadViewModel { get; set; }
}
