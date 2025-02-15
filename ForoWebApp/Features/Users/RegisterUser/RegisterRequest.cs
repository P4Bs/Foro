using MediatR;

namespace ForoWebApp.Features.Users.RegisterUser;

public class RegisterRequest : IRequest<RegisterResponse>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
