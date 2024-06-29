using ConectaTfg.Models;
using ConectaTfg.Models.Requests;
using ConectaTfg.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConectaTfg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	private readonly IAuthService _authService;

	public AuthController(ApplicationDbContext dbContext, IAuthService authService)
	{
		_dbContext = dbContext;
		_authService = authService;

		// TODO: Remove
		_dbContext.Database.EnsureCreated();
	}

	/// <summary>
	/// Inserts a new valid User into the database
	/// </summary>
	/// <param name="user">User data from the form in the Frontend</param>
	/// <returns></returns>
	[HttpPost("register")]
	public ActionResult RegisterUser([FromForm] User user)
	{
		if (!ModelState.IsValid)
			return BadRequest();

		if (!user.IsEmailFromUpm())
			return BadRequest(new { Message = "Esta dirección de correo electrónico no pertenece a la UPM" });

		if (_authService.IsEmailAlreadyUsed(user.Email))
			return Conflict(new { Message = "Este correo ya está siendo usado" });

		if (user.Password.Length < 6)
			return BadRequest(new { Message = "La contraseña tiene que tener un mínimo de 6 carácteres" });

		user.Password = _authService.GenerateHashedPassword(user, user.Password);

		if (user.Email.Contains("@upm.es"))
			user.Role = TUserRole.Profesor;

		_dbContext.Users.Add(user);
		_dbContext.SaveChanges();

		return Ok();
	}

	/// <summary>
	/// Authenticates a valid User and returns a new JWT
	/// </summary>
	/// <param name="loginRequest">Login Request from the form in the Frontend</param>
	/// <returns></returns>
	[HttpPost("login")]
	public ActionResult Login([FromForm] UserRequest loginRequest)
	{
		if (string.IsNullOrEmpty(loginRequest.Password))
			return BadRequest();

		User? user = _dbContext.Users.FirstOrDefault(u => u.Email == loginRequest.Email);

		if (user is null || !_authService.IsValidPassword(user, loginRequest.Password))
			return Unauthorized(new { Message = "El Email o la Contraseña es incorrecto" });

		return Ok(new { sessionId = _authService.GenerateJwt(user) });
	}
}
