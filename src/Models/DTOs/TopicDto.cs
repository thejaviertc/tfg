namespace ConectaTfg.Models.DTOs;

public class TopicDto
{
	public required int TopicId { get; set; }

	public required string Title { get; set; }

	public required string ShortDescription { get; set; }

	public required string Description { get; set; }

	public required DateTime CreatedAt { get; set; }

	public required TStatus Status { get; set; }

	public required UserDto User { get; set; }
}
