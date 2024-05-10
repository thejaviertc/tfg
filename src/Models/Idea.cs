using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TfgTemporalName.Models;

[Table("idea")]
public class Idea
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int IdeaId { get; set; }

	[Column(TypeName = "VARCHAR(50)")]
	public required string Title { get; set; }

	[Column(TypeName = "VARCHAR(255)")]
	public required string ShortDescription { get; set; }

	[Column(TypeName = "MEDIUMTEXT")]
	public required string Description { get; set; }

	[Column(TypeName = "DATETIME")]
	public required DateTime CreatedAt { get; set; }

	public int UserId { get; set; }

	public Idea()
	{
		CreatedAt = DateTime.Now;
	}
}