using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TfgTemporalName.Models;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class AuthController : ControllerBase
{
	private readonly TfgTemporalNameContext _context;

	private readonly IConfiguration _configuration;

	[GeneratedRegex(@"^.+@(alumnos\.)?upm\.es$")]
	private static partial Regex UpmRegex();

	public AuthController(TfgTemporalNameContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;

		// TODO: Remove
		_context.Database.EnsureCreated();
	}

	[HttpPost("register")]
	public ActionResult RegisterUser([FromForm] User user)
	{
		if (!ModelState.IsValid)
			return BadRequest();

		if (!UpmRegex().IsMatch(user.Email))
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

	[HttpPost("login")]
	public ActionResult Login([FromForm] LoginRequest loginData)
	{
		User? user = _context.Users.FirstOrDefault(u => u.Email == loginData.Email);

		if (
			user is null
			|| new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, loginData.Password)
				== PasswordVerificationResult.Failed
		)
			return BadRequest(new { Message = "El Email o la Contraseña es incorrecto" });

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

		var securityToken = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
		);

		var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

		return Ok(new { sessionId = token });
	}
}
