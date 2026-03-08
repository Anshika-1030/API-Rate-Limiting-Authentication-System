using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureRateLimitAPI.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{

[Authorize]
[HttpGet("user")]
public IActionResult UserEndpoint()
{
return Ok("User Access Granted");
}

[Authorize(Roles = "Admin")]
[HttpGet("admin")]
public IActionResult AdminEndpoint()
{
return Ok("Admin Access Granted");
}

}
