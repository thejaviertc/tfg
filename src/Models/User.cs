using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TfgTemporalName.Models;

[Table("user")]
public class User
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
}
