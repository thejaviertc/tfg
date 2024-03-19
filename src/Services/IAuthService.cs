using System.Security.Claims;
using TfgTemporalName.Models;

namespace TfgTemporalName.Services;

public interface IAuthService
{
	public bool IsEmailAlreadyUsed(string email);

	public string GenerateHashedPassword(User user);

	public User? GetAuthenticatedUser(LoginRequest loginRequest);

	public string GenerateJwt(User user);

	public int? GetUserIdFromJwt(ClaimsPrincipal user);
}
