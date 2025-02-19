using MediatR;

namespace ForoWebApp.Features.Threads.GetThread;

public class GetThreadRequest : IRequest<GetThreadResponse>
{
    public string ThreadId { get; set; }
    public int? Page { get; set; }
}
