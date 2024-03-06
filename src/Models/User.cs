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
	public string Name { get; set; }

	[Column(TypeName = "VARCHAR(60)")]
	public string Surname { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public string Email { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public string Password { get; set; }
}
