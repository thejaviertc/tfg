using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TfgTemporalName.Models;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	public UserController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;

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
		var subClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);

		if (subClaim is null)
			return BadRequest();

		User? user = _dbContext.Users.Find(int.Parse(subClaim.Value));

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
}
