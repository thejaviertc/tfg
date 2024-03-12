using TfgTemporalName.Models;

namespace TfgTemporalName.Services;

public interface IAuthService
{
	public bool IsEmailAlreadyUsed(string email);

	public string GetHashedPassword(User user);

	public User? GetAuthenticatedUser(LoginRequest loginRequest);

	public string GenerateJwt();
}
