using MediatR;

namespace ForoWebApp.Features.Posts.PublishPost;

public class PublishPostRequest : IRequest<PublishPostResponse>
{
    public string ThreadId { get; set; }
    public string UserId { get; set; }
    public string PostContent { get; set; }
}
