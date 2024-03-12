using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TfgTemporalName.Models;

namespace TfgTemporalName.Services;

public class AuthService : IAuthService
{
	private readonly SigningCredentials _signingCredentials;

	private readonly IConfiguration _configuration;

	private readonly ApplicationDbContext _dbContext;

	private readonly PasswordHasher<User> _passwordHasher;

	public AuthService(IConfiguration configuration, ApplicationDbContext dbContext)
	{
		_configuration = configuration;

		_signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
			SecurityAlgorithms.HmacSha256
		);

		_dbContext = dbContext;
		_passwordHasher = new PasswordHasher<User>();
	}

	public bool IsEmailAlreadyUsed(string email)
	{
		return _dbContext.Users.Any(u => u.Email == email);
	}

	public string GetHashedPassword(User user)
	{
		return _passwordHasher.HashPassword(user, user.Password);
	}

	public User? GetAuthenticatedUser(LoginRequest loginRequest)
	{
		User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

		if (user is null || !IsValidPassword(user, loginRequest.Password))
			return null;

		return user;
	}

	private bool IsValidPassword(User user, string password)
	{
		return _passwordHasher.VerifyHashedPassword(user, user.Password, password)
			== PasswordVerificationResult.Success;
	}

	public string GenerateJwt()
	{
		var securityToken = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: _signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(securityToken);
	}
}
