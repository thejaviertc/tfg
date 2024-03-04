using Microsoft.AspNetCore.Mvc;

namespace TfgTemporalName.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost("register")]
	public ActionResult<Test> RegisterUser([FromForm] Test model)
	{
		if (ModelState.IsValid)
			return model;
		else
			return BadRequest(ModelState);
	}
}
