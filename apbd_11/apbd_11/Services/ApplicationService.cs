using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using apbd_11.Context;
using apbd_11.Controllers;
using apbd_11.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using RegisterRequest = apbd_11.Controllers.RegisterRequest;

namespace apbd_11.Services;

public class ApplicationService : IApplicationService
{
    private readonly ApplicationContext _context;
    private readonly IConfiguration _config;

    public ApplicationService(ApplicationContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public void Register(RegisterRequest model)
    {
        var hashedPasswordAndSalt = GetHashedPasswordAndSalt(model.Password);
        var user = new ApplicationUser
        {
            Login = model.Login,
            Email = model.Email,
            Password = hashedPasswordAndSalt[0],
            Salt = hashedPasswordAndSalt[1],
            
        };
        if (_context.Users.Any(u => u.Login.Equals(model.Login))) throw new Exception("This login already exist");
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public LoginResponseModel Login(LoginRequestModel model)
    {
        if (! _context.Users.Any(u => u.Login.Equals(model.UserName))) throw new Exception("This user does not exist");
        
        ApplicationUser applicationUser = _context.Users.First(u => u.Login.Equals(model.UserName));
        var hashedPasswordWithSalt = GetHashedPasswordWithSalt(model.Password, applicationUser.Salt); 
        
        if (!hashedPasswordWithSalt.Equals(applicationUser.Password)) throw new Exception("Wrong credentials");
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        Claim[] claims =
        {
            new Claim(ClaimTypes.Name, applicationUser.Login),
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: signingCredentials,
            claims: claims
            );
        
        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
        
        var refreshToken = GenerateRefreshToken();
        applicationUser.RefreshToken = refreshToken;
        applicationUser.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();
        return new LoginResponseModel
        {
            Token = stringToken,
            RefreshToken = refreshToken
        };
    }
    
    public string[] GetHashedPasswordAndSalt(string password)
    {
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            32));

        var saltBase64 = Convert.ToBase64String(salt);

        return new[]{hashed, saltBase64};
    }

    public LoginResponseModel Refresh(string refreshToken)
    {
        ApplicationUser user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken)!;
        if (user is null) throw new SecurityTokenException("Invalid refresh token");
        if (user.RefreshTokenExp < DateTime.Now) throw new SecurityTokenException("Refresh token expired");

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? string.Empty));
        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        Claim[] claims =
        {
            new Claim(ClaimTypes.Name, user.Login),
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: signingCredentials,
            claims: claims
        );

        var newRefreshToken = GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return new LoginResponseModel()
        {
            RefreshToken = newRefreshToken,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    public string GetHashedPasswordWithSalt(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            saltBytes,
            KeyDerivationPrf.HMACSHA1,
            10000,
            32));
        return currentHashedPassword;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}