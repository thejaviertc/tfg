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
	public ActionResult<Topic> GetMyTopics()
	{
		var userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			return BadRequest();

		var topics = _dbContext.Topics.Where(t => t.UserId == userId);

		return Ok(topics);
	}

	/// <summary>
	/// Deletes the specified Topic
	/// </summary>
	/// <param name="id">The ID of the Topic</param>
	/// <returns></returns>
	[HttpDelete("{id}")]
	[Authorize]
	public ActionResult<Topic> DeleteTopic(int id)
	{
		var userId = _authService.GetUserIdFromJwt(User);

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
