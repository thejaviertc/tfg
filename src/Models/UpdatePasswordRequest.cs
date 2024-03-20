namespace TfgTemporalName.Models;

public class UpdatePasswordRequest
{
	public required string Password { get; set; }

	public required string NewPassword { get; set; }
}
