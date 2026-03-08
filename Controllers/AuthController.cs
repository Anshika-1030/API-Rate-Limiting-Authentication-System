using Microsoft.AspNetCore.Mvc;
using SecureRateLimitAPI.Services;
using SecureRateLimitAPI.DTOs;

namespace SecureRateLimitAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
private readonly IAuthService _service;

public AuthController(IAuthService service)
{
_service = service;
}

[HttpPost("register")]
public async Task<IActionResult> Register(RegisterDTO dto)
{
await _service.Register(dto);

return Ok("User created");
}

[HttpPost("login")]
public async Task<IActionResult> Login(LoginDTO dto)
{
var token = await _service.Login(dto);

return Ok(token);
}
}
