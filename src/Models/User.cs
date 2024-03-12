using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace TfgTemporalName.Models;

[Table("user")]
public partial class User
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int UserId { get; set; }

	[Column(TypeName = "VARCHAR(30)")]
	public required string Name { get; set; }

	[Column(TypeName = "VARCHAR(60)")]
	public required string Surname { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public required string Email { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public required string Password { get; set; }

	[GeneratedRegex(@"^.+@(alumnos\.)?upm\.es$")]
	private static partial Regex UpmEmailRegex();

	/// <summary>
	/// Checks if the Email is from the UPM domain
	/// </summary>
	/// <returns></returns>
	public bool IsEmailFromUpm()
	{
		return UpmEmailRegex().IsMatch(Email);
	}
}
