using MediatR;

namespace ForoWebApp.Features.Threads.CreateThread;

public class CreateThreadRequest : IRequest<CreateThreadResponse>
{
    public string ThemeId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string PostContent { get; set; }
}
