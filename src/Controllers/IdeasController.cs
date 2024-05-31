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
	public ActionResult<List<IdeaDto>> GetIdeas()
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		return Ok(
			_dbContext
				.Ideas.Include(i => i.User)
				.Select(i => new IdeaDto
				{
					IdeaId = i.IdeaId,
					Title = i.Title,
					ShortDescription = i.ShortDescription,
					Description = i.Description,
					CreatedAt = i.CreatedAt,
					Status = i.Status,
					User = new UserDto
					{
						UserId = i.User.UserId,
						Name = i.User.Name,
						Surname = i.User.Surname,
						Email = i.User.Email,
						Role = i.User.Role
					}
				})
				.ToList()
		);
	}

	/// <summary>
	/// Returns the specified Idea
	/// </summary>
	/// <param name="ideaId">The ID of the Idea</param>
	/// <returns></returns>
	[HttpGet("{ideaId}")]
	[Authorize]
	public ActionResult<IdeaDto> GetIdea(int ideaId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		Idea? idea = _dbContext.Ideas.Include(i => i.User).FirstOrDefault(i => i.IdeaId == ideaId);

		if (idea is null)
			return NotFound();

		if (idea.UserId != user.UserId && user.Role != TUserRole.Profesor)
			return Forbid();

		return Ok(
			new IdeaDto
			{
				IdeaId = idea.IdeaId,
				Title = idea.Title,
				ShortDescription = idea.ShortDescription,
				Description = idea.Description,
				CreatedAt = idea.CreatedAt,
				Status = idea.Status,
				User = new UserDto
				{
					UserId = idea.User.UserId,
					Name = idea.User.Name,
					Surname = idea.User.Surname,
					Email = idea.User.Email,
					Role = idea.User.Role
				}
			}
		);
	}

	/// <summary>
	/// Returns all the ideas related to the current user
	/// </summary>
	/// <returns></returns>
	[HttpGet("me")]
	[Authorize]
	public ActionResult<List<IdeaDto>> GetMyIdeas()
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Alumno)
			return Forbid();

		return Ok(
			_dbContext
				.Ideas.Where(i => i.UserId == user.UserId)
				.Select(i => new IdeaDto
				{
					IdeaId = i.IdeaId,
					Title = i.Title,
					ShortDescription = i.ShortDescription,
					Description = i.Description,
					CreatedAt = i.CreatedAt,
					Status = i.Status,
					User = new UserDto
					{
						UserId = i.User.UserId,
						Name = i.User.Name,
						Surname = i.User.Surname,
						Email = i.User.Email,
						Role = i.User.Role
					}
				})
				.ToList()
		);
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

	/// <summary>
	/// Requests the specified Idea
	/// </summary>
	/// <param name="ideaId">The ID of the Idea</param>
	/// <returns></returns>
	[HttpPost("{ideaId}/request")]
	[Authorize]
	public ActionResult RequestIdea(int ideaId)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

		if (user.Role != TUserRole.Profesor)
			return Forbid();

		Idea? idea = _dbContext.Ideas.Find(ideaId);

		if (idea is null)
			return NotFound();

		if (idea.Status != TStatus.Available)
			return BadRequest(new { Message = "Esta idea no está disponible!" });

		idea.UserIdRequested = user.UserId;
		idea.UserRequestered = user;

		idea.Status = TStatus.WaitingResponse;

		_dbContext.SaveChanges();

		return Ok();
	}
}
