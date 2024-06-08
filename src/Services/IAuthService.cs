using System.Security.Claims;
using ConectaTfg.Models;

namespace ConectaTfg.Services;

public interface IAuthService
{
	public bool IsEmailAlreadyUsed(string email);

	public string GenerateHashedPassword(User user, string password);

	public bool IsValidPassword(User user, string password);

	public string GenerateJwt(User user);

	public User? GetAuthenticatedUser(ClaimsPrincipal userClaims);

	public int? GetUserIdFromJwt(ClaimsPrincipal userClaims);
}
