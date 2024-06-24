using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using apbd_11.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apbd_11.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class MiddlewareController(IApplicationService service) : ControllerBase
{
    
    [AllowAnonymous]
    [HttpPost("/register")]
    public IActionResult Register(RegisterRequest model)
    {
        try { 
            service.Register(model);
            return Ok("Registered");
        } catch (Exception e) {
            return Unauthorized(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpPost("/login")]
    public IActionResult Login(LoginRequestModel model) 
    {
        try { 
            return Ok(service.Login(model));
        } catch (Exception e) {
            return Unauthorized(e.Message);
        }
    }
    
    [Authorize]
    [HttpPost("/refresh")]
    public IActionResult RefreshToken([Required] string token)
    {
        try {
            return Ok(service.Refresh(token));
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("/hash-password-without-salt/{password}")]
    public IActionResult HashPassword(string password)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            new byte[] {0},
            10,
            HashAlgorithmName.SHA512,
            128
        );

        return Ok(Convert.ToHexString(hash));
    }
    
    [Authorize]
    [HttpGet("/hash-password/{password}")]
    public IActionResult HashPasswordWithSalt(string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        return Ok(passwordHasher.HashPassword(new User(), password));
    }
    
    [Authorize]
    [HttpPost("verify-password")]
    public IActionResult VerifyPassword(VerifyPasswordRequestModel requestModel)
    {
        var passwordHasher = new PasswordHasher<User>();
        return Ok(passwordHasher.VerifyHashedPassword(new User(), requestModel.Hash, requestModel.Password) == PasswordVerificationResult.Success);
    }
}

public class User
{
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class VerifyPasswordRequestModel
{
    public string Password { get; set; } = null!;
    public string Hash { get; set; } = null!;
}

public class LoginRequestModel
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class LoginResponseModel
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenRequestModel
{
    public string RefreshToken { get; set; } = null!;
}

public class RegisterRequest
{
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

}