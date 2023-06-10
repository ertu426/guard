using AuthService.WebAPI.Models;
using AuthService.WebAPI.Models.Auth;
using AuthService.WebAPI.Utils.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.WebAPI.Controllers.Auth;

[Route("[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly JwtHelper _jwtHelper;
    private readonly List<User> _userList; 

    public AuthController(IConfiguration configuration)
    {
        string? secretKey = configuration.GetSection("JwtSettings:SecretKey").Value;
        _jwtHelper = new JwtHelper(secretKey);
        _userList = UserModels.Users;
    }

    [HttpPost, Route("[action]")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // if user not exist in database
        User? user = _userList.Find(x => x.Username == request.Username && x.Password == request.Password);
        
        if (user is null)
            return BadRequest(new { code = "400", message = "username or password is incorrect" });

        string token = _jwtHelper.GenerateToken(user);
        return Ok(new { code = "200", data = new { token }, message = "success" });
    }
    
    [HttpPost, Route("[action]")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        // find user in _userList
        if(_userList.Exists(x => x.Username == request.Username))
            return BadRequest(new {code = "400", message = "user already exists"});

        if (!String.Equals(request.Password, request.ConfirmPassword))
            return BadRequest(new {code = "400", message = "password not match"});
        
        string role = _userList.Count == 0 ? "admin" : "user";

        User user = new User
        {
            Username = request.Username,
            Password = request.Password,
            Role = role
        };
        _userList.Add(user);
        
        string token = _jwtHelper.GenerateToken(user);
        return Ok(new { code = "200", data = new { token }, message = "success" });
    }
}