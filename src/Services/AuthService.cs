using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
	public string GenerateHashedPassword(User user, string password)
	{
		return _passwordHasher.HashPassword(user, password);
	}

	/// <summary>
	/// Checks if the password provided is the password of the User
	/// </summary>
	/// <param name="user">The User who tries to login</param>
	/// <param name="password">The password that is going to be checked</param>
	/// <returns></returns>
	public bool IsValidPassword(User user, string password)
	{
		return _passwordHasher.VerifyHashedPassword(user, user.Password, password)
			== PasswordVerificationResult.Success;
	}

	/// <summary>
	/// Obtains a new JWT for the User
	/// </summary>
	/// <param name="user">The User who is going to receive the JWT</param>
	/// <returns></returns>
	public string GenerateJwt(User user)
	{
		var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()) };

		var securityToken = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			claims,
			expires: DateTime.Now.AddMinutes(60 * 24 * 7 * 4 * 12), // TODO: Change
			signingCredentials: _signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(securityToken);
	}

	/// <summary>
	/// Returns the User from the Database if the authentication is valid
	/// </summary>
	/// <param name="userClaims">The claims of the JWT</param>
	/// <returns></returns>
	public User? GetAuthenticatedUser(ClaimsPrincipal userClaims)
	{
		int? userId = GetUserIdFromJwt(userClaims);

		if (userId is null)
			return null;

		return _dbContext.Users.Find(userId);
	}

	/// <summary>
	/// Obtains the UserId inside the JWT
	/// </summary>
	/// <param name="user">Claims of the User</param>
	/// <returns></returns>
	public int? GetUserIdFromJwt(ClaimsPrincipal userClaims)
	{
		Claim? subClaim = userClaims.FindFirst(JwtRegisteredClaimNames.Sub);

		return subClaim is not null ? int.Parse(subClaim.Value) : null;
	}
}
