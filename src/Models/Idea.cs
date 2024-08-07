using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaTfg.Models;

[Table("idea")]
public class Idea
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int IdeaId { get; set; }

	[Column(TypeName = "VARCHAR(100)")]
	public required string Title { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public required string ShortDescription { get; set; }

	[Column(TypeName = "MEDIUMTEXT")]
	public required string Description { get; set; }

	[Column(TypeName = "DATETIME")]
	public required DateTime CreatedAt { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	[DefaultValue(TStatus.Available)]
	public required TStatus Status { get; set; }

	public int? UserIdRequested { get; set; }

	[ForeignKey("UserIdRequested")]
	public User? UserRequestered { get; set; }

	public Idea()
	{
		CreatedAt = DateTime.Now;
	}
}
