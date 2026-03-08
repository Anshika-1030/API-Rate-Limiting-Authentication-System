using SecureRateLimitAPI.Data;
using SecureRateLimitAPI.DTOs;
using SecureRateLimitAPI.Models;
using BCrypt.Net;

namespace SecureRateLimitAPI.Services;

public class AuthService : IAuthService
{
private readonly AppDbContext _context;
private readonly TokenService _tokenService;

public AuthService(AppDbContext context, TokenService tokenService)
{
_context = context;
_tokenService = tokenService;
}

public async Task Register(RegisterDTO dto)
{
var user = new User
{
Username = dto.Username,
PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
Role = dto.Role
};

_context.Users.Add(user);
await _context.SaveChangesAsync();
}

public async Task<TokenResponseDTO> Login(LoginDTO dto)
{
var user = _context.Users.FirstOrDefault(x => x.Username == dto.Username);

if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
throw new Exception("Invalid credentials");

var accessToken = _tokenService.CreateToken(user);
var refreshToken = _tokenService.GenerateRefreshToken();

return new TokenResponseDTO
{
AccessToken = accessToken,
RefreshToken = refreshToken
};
}
}
