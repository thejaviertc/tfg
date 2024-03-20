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
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized();

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
	public ActionResult UpdateCurrentUser([FromForm] UserRequest updateRequest)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized(
				new { Message = "No se ha podido encontrar el usuario en el servidor. Intente de nuevo" }
			);

		if (string.IsNullOrEmpty(updateRequest.Name) || string.IsNullOrEmpty(updateRequest.Surname))
			return BadRequest();

		user.Name = updateRequest.Name;
		user.Surname = updateRequest.Surname;

		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Updates the current User with the provided new password
	/// </summary>
	/// <param name="updatePasswordRequest">The update password request with the current Password and NewPassword</param>
	/// <returns></returns>
	[HttpPut("me/password")]
	[Authorize]
	public ActionResult UpdateCurrentUserPassword([FromForm] UserRequest updatePasswordRequest)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized(
				new { Message = "No se ha podido encontrar el usuario en el servidor. Intente de nuevo" }
			);

		if (
			string.IsNullOrEmpty(updatePasswordRequest.Password)
			|| string.IsNullOrEmpty(updatePasswordRequest.NewPassword)
		)
			return BadRequest();

		if (!_authService.IsValidPassword(user, updatePasswordRequest.Password))
			return BadRequest(new { Message = "La contraseña actual introducida es incorrecta" });

		if (updatePasswordRequest.NewPassword.Length < 6)
			return BadRequest(new { Message = "La nueva contraseña tiene que tener un mínimo de 6 carácteres" });

		user.Password = _authService.GenerateHashedPassword(user, updatePasswordRequest.NewPassword);

		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Deletes the current user from the system
	/// </summary>
	/// <param name="deleteUserRequest">The delete user request with the current Password</param>
	/// <returns></returns>
	[HttpDelete("me")]
	[Authorize]
	public ActionResult DeleteCurrentUser([FromForm] UserRequest deleteUserRequest)
	{
		User? user = _authService.GetAuthenticatedUser(User);

		if (user is null)
			return Unauthorized(
				new { Message = "No se ha podido encontrar el usuario en el servidor. Intente de nuevo" }
			);

		if (string.IsNullOrEmpty(deleteUserRequest.Password))
			return BadRequest();

		if (!_authService.IsValidPassword(user, deleteUserRequest.Password))
			return BadRequest(new { Message = "La contraseña actual introducida es incorrecta" });

		_dbContext.Users.Remove(user);
		_dbContext.SaveChanges();

		return Ok();
	}
}
