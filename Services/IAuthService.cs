using SecureRateLimitAPI.DTOs;

namespace SecureRateLimitAPI.Services;

public interface IAuthService
{
Task<TokenResponseDTO> Login(LoginDTO dto);

Task Register(RegisterDTO dto);
}
