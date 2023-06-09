using AuthService.WebAPI.Utils.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebAPI.Controllers.Auth;

[Route("[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly JwtHelper _jwtHelper;

    public AuthController(IConfiguration configuration)
    {
        string? secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
        _jwtHelper = new JwtHelper(secretKey);
    }

    [HttpPost, Route("[action]")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        string token = _jwtHelper.GenerateToken(request.Username);
        return Ok(new { code = "200", data = new { token }, message = "success" });
    }
}