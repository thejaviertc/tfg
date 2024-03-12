using Microsoft.AspNetCore.Mvc;
using TfgTemporalName.Models;
using TfgTemporalName.Services;

namespace TfgTemporalName.Controllers;

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

		user.Password = _authService.GetHashedPassword(user);

		_dbContext.Users.Add(user);
		_dbContext.SaveChanges();

		return Ok();
	}

	[HttpPost("login")]
	public ActionResult Login([FromForm] LoginRequest loginData)
	{
		User? user = _authService.GetAuthenticatedUser(loginData);

		if (user is null)
			return BadRequest(new { Message = "El Email o la Contraseña es incorrecto" });

		return Ok(new { sessionId = _authService.GenerateJwt() });
	}
}
