using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgTemporalName.Models;
using TfgTemporalName.Services;

namespace TfgTemporalName.Controllers;

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
	public ActionResult<List<Topic>> GetTopics()
	{
		return Ok(_dbContext.Topics);
	}

	/// <summary>
	/// Returns the specified Topic
	/// </summary>
	/// <param name="id">The ID of the Topic</param>
	/// <returns></returns>
	[HttpGet("{id}")]
	[Authorize]
	public ActionResult<Topic> GetTopic(int id)
	{
		Topic? topic = _dbContext.Topics.Find(id);

		if (topic is null)
			return NotFound();

		return Ok(topic);
	}

	/// <summary>
	/// Returns all the topics related to the current user
	/// </summary>
	/// <returns></returns>
	[HttpGet("me")]
	[Authorize]
	public ActionResult<List<Topic>> GetMyTopics()
	{
		int? userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			return BadRequest();

		var topics = _dbContext.Topics.Where(t => t.UserId == userId);

		return Ok(topics);
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
		int? userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			return BadRequest();

		if (!ModelState.IsValid)
			return BadRequest();

		if (topic.Title.Length < 6 || topic.Title.Length > 50)
			return BadRequest(new { Message = "El título tiene que tener entre 6 y 50 carácteres" });

		if (topic.ShortDescription.Length < 20 || topic.ShortDescription.Length > 255)
			return BadRequest(new { Message = "La descripción corta tiene que tener entre 20 y 255 carácteres" });

		if (topic.Description.Length < 50)
			return BadRequest(new { Message = "La descripción tiene que tener al menos 50 carácteres" });

		topic.UserId = (int)userId;

		_dbContext.Topics.Add(topic);
		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Deletes the specified Topic
	/// </summary>
	/// <param name="id">The ID of the Topic</param>
	/// <returns></returns>
	[HttpDelete("{id}")]
	[Authorize]
	public ActionResult DeleteTopic(int id)
	{
		int? userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			return BadRequest();

		Topic? topic = _dbContext.Topics.Find(id);

		if (topic is null)
			return NotFound();

		if (topic.UserId != userId)
			return Forbid();

		_dbContext.Topics.Remove(topic);
		_dbContext.SaveChanges();

		return Ok();
	}
}
