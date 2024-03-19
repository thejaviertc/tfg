using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgTemporalName.Models;
using TfgTemporalName.Services;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	private readonly IAuthService _authService;

	public UserController(ApplicationDbContext dbContext, IAuthService authService)
	{
		_dbContext = dbContext;
		_authService = authService;

		// TODO: Remove
		_dbContext.Database.EnsureCreated();
	}

	/// <summary>
	/// Returns the Name, Surname and Email of the current User
	/// </summary>
	/// <returns></returns>
	[HttpGet("me")]
	[Authorize]
	public ActionResult GetCurrentUser()
	{
		int? userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			return BadRequest();

		User? user = _dbContext.Users.Find(userId);

		if (user is null)
			return BadRequest();

		return Ok(
			new
			{
				user.Name,
				user.Surname,
				user.Email
			}
		);
	}

	/// <summary>
	/// Updates the current User with the provided Name and Surname
	/// </summary>
	/// <param name="updateRequest">The update request with the new Name and Surname</param>
	/// <returns></returns>
	[HttpPut("me")]
	[Authorize]
	public ActionResult UpdateUser([FromForm] UpdateRequest updateRequest)
	{
		int? userId = _authService.GetUserIdFromJwt(User);

		if (userId is null)
			// TODO: Check
			return BadRequest(new { Message = "El SessionId es inválido, por favor inicie sesión de nuevo" });

		User? user = _dbContext.Users.Find(userId);

		if (user is null)
			// TODO: Check
			return BadRequest(new { Message = "El SessionId es inválido, por favor inicie sesión de nuevo" });

		user.Name = updateRequest.Name;
		user.Surname = updateRequest.Surname;

		_dbContext.SaveChanges();

		return Ok();
	}
}
