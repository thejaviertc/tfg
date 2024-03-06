using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TfgTemporalName.Models;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly TfgTemporalNameContext _context;

	public AuthController(TfgTemporalNameContext context)
	{
		_context = context;

		// TODO: Remove
		_context.Database.EnsureCreated();
	}

	[HttpPost("register")]
	public ActionResult RegisterUser([FromForm] User user)
	{
		if (!ModelState.IsValid)
			return BadRequest();

		if (!Regex.IsMatch(user.Email, @"^.+@(alumnos\.)?upm\.es$"))
			return BadRequest(new { Message = "Esta dirección de correo electrónico no pertenece a la UPM" });

		if (_context.Users.Any(u => u.Email == user.Email))
			return Conflict(new { Message = "Este correo ya está siendo usado" });

		if (user.Password.Length < 6)
			return BadRequest(new { Message = "La contraseña tiene que tener un mínimo de 6 carácteres" });

		user.Password = new PasswordHasher<User>().HashPassword(user, user.Password);

		_context.Users.Add(user);
		_context.SaveChanges();

		return Ok();
	}
}
