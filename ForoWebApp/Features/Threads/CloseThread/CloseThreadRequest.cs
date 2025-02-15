using MediatR;

namespace ForoWebApp.Features.Threads.CloseThread;

public class CloseThreadRequest : IRequest<CloseThreadResponse>
{
    public string ThreadId { get; set; }
}
