using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TfgTemporalName.Models;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	public TopicsController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;

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
}
