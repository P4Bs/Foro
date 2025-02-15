using MediatR;

namespace ForoWebApp.Features.Users.LogUser;

public class LogInRequest : IRequest<LogInResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
