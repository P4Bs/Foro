using ForoWebApp.Features.Common;

namespace ForoWebApp.Features.Posts.PublishPost;

public class PublishPostResponse(bool success, string[]? errors = null) : BaseResponse(success, errors)
{
    public string? ThreadId { get; set; }
}
