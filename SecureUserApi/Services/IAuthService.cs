// Services/IAuthService.cs
using SecureUserApi.Dtos;
using System.Threading.Tasks;

namespace SecureUserApi.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto registerDto);
    Task<TokenDto?> LoginAsync(LoginDto loginDto);
    Task<TokenDto?> RefreshTokenAsync(string refreshToken);
}