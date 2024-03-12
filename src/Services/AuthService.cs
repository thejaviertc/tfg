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

	/// <summary>
	/// Checks if the provided email is already used by other User
	/// </summary>
	/// <param name="email">The email to check</param>
	/// <returns></returns>
	public bool IsEmailAlreadyUsed(string email)
	{
		return _dbContext.Users.Any(u => u.Email == email);
	}

	/// <summary>
	/// Obtains the Hash of the password depending of the User
	/// </summary>
	/// <param name="user"></param>
	/// <returns></returns>
	public string GenerateHashedPassword(User user)
	{
		return _passwordHasher.HashPassword(user, user.Password);
	}

	/// <summary>
	/// Returns the User from the Database if the authentication is valid
	/// </summary>
	/// <param name="loginRequest">User data from the form in the Frontend</param>
	/// <returns></returns>
	public User? GetAuthenticatedUser(LoginRequest loginRequest)
	{
		User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

		if (user is null || !IsValidPassword(user, loginRequest.Password))
			return null;

		return user;
	}

	/// <summary>
	/// Checks if the password provided is the password of the User
	/// </summary>
	/// <param name="user">The User which tries to login</param>
	/// <param name="password">The password that is going to be checked</param>
	/// <returns></returns>
	private bool IsValidPassword(User user, string password)
	{
		return _passwordHasher.VerifyHashedPassword(user, user.Password, password)
			== PasswordVerificationResult.Success;
	}

	/// <summary>
	/// Obtains a new JWT for the User
	/// </summary>
	/// <returns></returns>
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
