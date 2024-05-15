using ConectaTfg.Models;
using ConectaTfg.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaTfg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdeasController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	private readonly IAuthService _authService;

	public IdeasController(ApplicationDbContext dbContext, IAuthService authService)
	{
		_dbContext = dbContext;
		_authService = authService;

		// TODO: Remove
		_dbContext.Database.EnsureCreated();
	}

	/// <summary>
	/// Returns all the ideas stored
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[Authorize]
	public ActionResult<List<Idea>> GetIdeas()
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		return Ok(_dbContext.Ideas);
	}

	/// <summary>
	/// Returns the specified Idea
	/// </summary>
	/// <param name="ideaId">The ID of the Idea</param>
	/// <returns></returns>
	[HttpGet("{ideaId}")]
	[Authorize]
	public ActionResult<Idea> GetIdea(int ideaId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		Idea? idea = _dbContext.Ideas.Find(ideaId);

		if (idea is null)
			return NotFound();

		if (idea.UserId != user.UserId && user.Role != TUserRole.Profesor)
			return Forbid();

		return Ok(idea);
	}

	/// <summary>
	/// Returns all the ideas related to the current user
	/// </summary>
	/// <returns></returns>
	[HttpGet("me")]
	[Authorize]
	public ActionResult<List<Idea>> GetMyIdeas()
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		var ideas = _dbContext.Ideas.Where(t => t.UserId == user.UserId);

		return Ok(ideas);
	}

	/// <summary>
	/// Inserts a new valid Idea into the database
	/// </summary>
	/// <param name="idea">The Idea that is going to be added</param>
	/// <returns></returns>
	[HttpPost]
	[Authorize]
	public ActionResult AddIdea([FromForm] Idea idea)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		if (!ModelState.IsValid)
			return BadRequest();

		if (idea.Title.Length < 6 || idea.Title.Length > 50)
			return BadRequest(new { Message = "El título tiene que tener entre 6 y 50 carácteres" });

		if (idea.ShortDescription.Length < 20 || idea.ShortDescription.Length > 255)
			return BadRequest(new { Message = "La descripción corta tiene que tener entre 20 y 255 carácteres" });

		if (idea.Description.Length < 50)
			return BadRequest(new { Message = "La descripción tiene que tener al menos 50 carácteres" });

		idea.UserId = user.UserId;

		_dbContext.Ideas.Add(idea);
		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Updates the selected Idea with the provided data
	/// </summary>
	/// <param name="ideaRequest"></param>
	/// <returns></returns>
	[HttpPut("{ideaId}")]
	[Authorize]
	public ActionResult EditIdea(int ideaId, [FromForm] IdeaRequest ideaRequest)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		Idea? idea = _dbContext.Ideas.Find(ideaId);

		if (idea is null)
			return NotFound();

		if (idea.UserId != user.UserId)
			return Forbid();

		if (idea.Title.Length < 6 || idea.Title.Length > 50)
			return BadRequest(new { Message = "El título tiene que tener entre 6 y 50 carácteres" });

		if (idea.ShortDescription.Length < 20 || idea.ShortDescription.Length > 255)
			return BadRequest(new { Message = "La descripción corta tiene que tener entre 20 y 255 carácteres" });

		if (idea.Description.Length < 50)
			return BadRequest(new { Message = "La descripción tiene que tener al menos 50 carácteres" });

		idea.Title = ideaRequest.Title;
		idea.ShortDescription = ideaRequest.ShortDescription;
		idea.Description = ideaRequest.Description;

		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Deletes the specified Idea
	/// </summary>
	/// <param name="ideaId">The ID of the Idea</param>
	/// <returns></returns>
	[HttpDelete("{ideaId}")]
	[Authorize]
	public ActionResult DeleteIdea(int ideaId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		Idea? idea = _dbContext.Ideas.Find(ideaId);

		if (idea is null)
			return NotFound();

		if (idea.UserId != user.UserId)
			return Forbid();

		_dbContext.Ideas.Remove(idea);
		_dbContext.SaveChanges();

		return Ok();
	}
}
