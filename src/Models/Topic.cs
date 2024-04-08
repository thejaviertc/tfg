using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TfgTemporalName.Models;

[Table("topic")]
public class Topic
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TopicId { get; set; }

	[Column(TypeName = "VARCHAR(100)")]
	public required string Title { get; set; }

	[Column(TypeName = "MEDIUMTEXT")]
	public required string Description { get; set; }

	[Column(TypeName = "DATETIME")]
	public required DateTime CreatedAt { get; set; }

	public int UserId { get; set; }
}
