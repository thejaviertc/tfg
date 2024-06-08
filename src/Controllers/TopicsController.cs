using ConectaTfg.Models;
using ConectaTfg.Models.DTOs;
using ConectaTfg.Models.Requests;
using ConectaTfg.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaTfg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	private readonly IAuthService _authService;

	public TopicsController(ApplicationDbContext dbContext, IAuthService authService)
	{
		_dbContext = dbContext;
		_authService = authService;

		// TODO: Remove
		_dbContext.Database.EnsureCreated();
	}

	/// <summary>
	/// Returns all the topics stored
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Authorize]
	public ActionResult<List<TopicDto>> GetTopics()
	{
		return Ok(
			_dbContext
				.Topics.Include(t => t.User)
				.Select(t => new TopicDto
				{
					TopicId = t.TopicId,
					Title = t.Title,
					ShortDescription = t.ShortDescription,
					Description = t.Description,
					CreatedAt = t.CreatedAt,
					Status = t.Status,
					User = new UserDto
					{
						UserId = t.User.UserId,
						Name = t.User.Name,
						Surname = t.User.Surname,
						Email = t.User.Email,
						Role = t.User.Role
					}
				})
				.ToList()
		);
	}

	/// <summary>
	/// Returns the specified Topic
	/// </summary>
	/// <param name="topicId">The ID of the Topic</param>
	/// <returns></returns>
	[HttpGet("{topicId}")]
	[Authorize]
	public ActionResult<TopicDto> GetTopic(int topicId)
	{
		Topic? topic = _dbContext.Topics.Include(t => t.User).FirstOrDefault(t => t.TopicId == topicId);

		if (topic is null)
			return NotFound();

		return Ok(
			new TopicDto
			{
				TopicId = topic.TopicId,
				Title = topic.Title,
				ShortDescription = topic.ShortDescription,
				Description = topic.Description,
				CreatedAt = topic.CreatedAt,
				Status = topic.Status,
				User = new UserDto
				{
					UserId = topic.User.UserId,
					Name = topic.User.Name,
					Surname = topic.User.Surname,
					Email = topic.User.Email,
					Role = topic.User.Role
				}
			}
		);
	}

	/// <summary>
	/// Returns all the topics related to the current user
	/// </summary>
	/// <returns></returns>
	[HttpGet("me")]
	[Authorize]
	public ActionResult<List<TopicDto>> GetMyTopics()
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		return Ok(
			_dbContext
				.Topics.Where(t => t.UserId == user.UserId)
				.Select(t => new TopicDto
				{
					TopicId = t.TopicId,
					Title = t.Title,
					ShortDescription = t.ShortDescription,
					Description = t.Description,
					CreatedAt = t.CreatedAt,
					Status = t.Status,
					User = new UserDto
					{
						UserId = t.User.UserId,
						Name = t.User.Name,
						Surname = t.User.Surname,
						Email = t.User.Email,
						Role = t.User.Role
					}
				})
				.ToList()
		);
	}

	/// <summary>
	/// Inserts a new valid Topic into the database
	/// </summary>
	/// <param name="topic">The Topic that is going to be added</param>
	/// <returns></returns>
	[HttpPost]
	[Authorize]
	public ActionResult AddTopic([FromForm] Topic topic)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		if (!ModelState.IsValid)
			return BadRequest();

		if (topic.Title.Length < 6 || topic.Title.Length > 50)
			return BadRequest(new { Message = "El título tiene que tener entre 6 y 50 carácteres" });

		if (topic.ShortDescription.Length < 20 || topic.ShortDescription.Length > 255)
			return BadRequest(new { Message = "La descripción corta tiene que tener entre 20 y 255 carácteres" });

		if (topic.Description.Length < 50)
			return BadRequest(new { Message = "La descripción tiene que tener al menos 50 carácteres" });

		topic.UserId = user.UserId;

		_dbContext.Topics.Add(topic);
		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Updates the selected Topic with the provided data
	/// </summary>
	/// <param name="topicRequest"></param>
	/// <returns></returns>
	[HttpPut("{topicId}")]
	[Authorize]
	public ActionResult EditTopic(int topicId, [FromForm] TopicRequest topicRequest)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		Topic? topic = _dbContext.Topics.Find(topicId);

		if (topic is null)
			return NotFound();

		if (topic.UserId != user.UserId)
			return Forbid();

		if (topic.Title.Length < 6 || topic.Title.Length > 50)
			return BadRequest(new { Message = "El título tiene que tener entre 6 y 50 carácteres" });

		if (topic.ShortDescription.Length < 20 || topic.ShortDescription.Length > 255)
			return BadRequest(new { Message = "La descripción corta tiene que tener entre 20 y 255 carácteres" });

		if (topic.Description.Length < 50)
			return BadRequest(new { Message = "La descripción tiene que tener al menos 50 carácteres" });

		topic.Title = topicRequest.Title;
		topic.ShortDescription = topicRequest.ShortDescription;
		topic.Description = topicRequest.Description;

		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Deletes the specified Topic
	/// </summary>
	/// <param name="topicId">The ID of the Topic</param>
	/// <returns></returns>
	[HttpDelete("{topicId}")]
	[Authorize]
	public ActionResult DeleteTopic(int topicId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		Topic? topic = _dbContext.Topics.Find(topicId);

		if (topic is null)
			return NotFound();

		if (topic.UserId != user.UserId)
			return Forbid();

		_dbContext.Topics.Remove(topic);
		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Requests the specified Topic
	/// </summary>
	/// <param name="topicId">The ID of the Topic</param>
	/// <returns></returns>
	[HttpPost("{topicId}/request")]
	[Authorize]
	public ActionResult RequestTopic(int topicId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		Topic? topic = _dbContext.Topics.Find(topicId);

		if (topic is null)
			return NotFound();

		if (topic.Status != TStatus.Available)
			return BadRequest(new { Message = "Este tema no está disponible!" });

		topic.UserIdRequested = user.UserId;
		topic.UserRequestered = user;

		topic.Status = TStatus.WaitingResponse;

		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Gets the Yser that made the petition of the Topic
	/// </summary>
	/// <param name="topicId">The ID of the Topic</param>
	/// <returns></returns>
	[HttpGet("{topicId}/petition")]
	[Authorize]
	public ActionResult GetPetition(int topicId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		Topic? topic = _dbContext.Topics.Include(t => t.UserRequestered).FirstOrDefault(t => t.TopicId == topicId);

		if (topic is null)
			return NotFound();

		if (topic.Status != TStatus.WaitingResponse)
			return BadRequest(new { Message = "Este tema no tiene ninguna petición!" });

		return Ok(
			new UserDto
			{
				UserId = topic.UserRequestered!.UserId,
				Name = topic.UserRequestered!.Name,
				Surname = topic.UserRequestered!.Surname,
				Email = topic.UserRequestered!.Email,
				Role = topic.UserRequestered!.Role
			}
		);
	}

	/// <summary>
	/// Changes the Status of the Topic
	/// </summary>
	/// <param name="topicId">The ID of the Topic</param>
	/// <returns></returns>
	[HttpPut("{topicId}/status/{isAccepted}")]
	[Authorize]
	public ActionResult UpdateStatus(int topicId, bool isAccepted)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		Topic? topic = _dbContext.Topics.Find(topicId);

		if (topic is null)
			return NotFound();

		if (topic.Status != TStatus.WaitingResponse)
			return BadRequest(new { Message = "Este tema no tiene ninguna petición!" });

		topic.Status = isAccepted ? TStatus.Accepted : TStatus.Available;

		if (!isAccepted)
		{
			topic.UserIdRequested = null;
			topic.UserRequestered = null;
		}

		_dbContext.SaveChanges();

		return Ok();
	}
}
