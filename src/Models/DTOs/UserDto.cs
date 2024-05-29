namespace ConectaTfg.Models.DTOs;

public class UserDto
{
	public required int UserId { get; set; }

	public required string Name { get; set; }

	public required string Surname { get; set; }

	public required string Email { get; set; }

	public required TUserRole Role { get; set; }
}
