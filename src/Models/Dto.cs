using ConectaTfg.Models;

public class TopicDto
{
	public int TopicId { get; set; }

	public string Title { get; set; }

	public string ShortDescription { get; set; }

	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }

	public TStatus Status { get; set; }

	public UserDto User { get; set; }
}

public class IdeaDto
{
	public int IdeaId { get; set; }

	public string Title { get; set; }

	public string ShortDescription { get; set; }

	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }

	public TStatus Status { get; set; }

	public UserDto User { get; set; }
}

public class UserDto
{
	public int UserId { get; set; }

	public string Name { get; set; }

	public string Surname { get; set; }

	public string Email { get; set; }

	public TUserRole Role { get; set; }
}
