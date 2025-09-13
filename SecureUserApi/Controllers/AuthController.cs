// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using SecureUserApi.Dtos;
using SecureUserApi.Services;

namespace SecureUserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            var isSuccess = await _authService.RegisterAsync(registerDto);
            if (!isSuccess)
            {
                return BadRequest("Username or email already exists.");
            }
            return Ok(new { Message = "User registered successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during registration.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var tokens = await _authService.LoginAsync(loginDto);
            if (tokens == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(tokens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenDto tokenDto)
    {
        try
        {
            var tokens = await _authService.RefreshTokenAsync(tokenDto.RefreshToken);
            if (tokens == null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }
            return Ok(tokens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during token refresh.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
        }
    }
}