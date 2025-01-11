using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForoWebApp.Services;

public class UserService(IConfiguration configuration, UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;
	private readonly byte[] _key = Encoding.ASCII.GetBytes(configuration.GetSection("Secret").ToString());

	public async Task<(string, User)> RegisterUser(UserRegistrationModel model)
	{
		User newUser = new()
		{
			Name = model.Name,
			Email = model.Email,
			Password = model.Password,
			RegisteredAt = model.RegisteredAt,
		};

		bool success = await _unitOfWork.UsersRepository.TryRegister(newUser);

		if (!success)
		{
			return (null, null);
		}

		string userToken = GetUserToken(newUser);

		return (userToken, newUser);
    }

	public async Task<string> LogUser(UserLoginModel model)
	{
		User? userData = await _unitOfWork.UsersRepository.FindUserByLogin(model);

		if(userData == null)
		{
			return null;
		}
		
		return GetUserToken(userData);
	}

	private string GetUserToken(User userData)
	{
        JwtSecurityTokenHandler tokenHandler = new();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new(ClaimTypes.Email, userData.Email)
            ]),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256)
        };

        SecurityToken userToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(userToken);
    }
}
