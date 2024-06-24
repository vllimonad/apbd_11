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

    public void RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = GetHashedPasswordAndSalt(model.Password);
        var user = new ApplicationUser
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            
        };
        if (_context.Users.Any(u => u.Login.Equals(model.Login)))
        {
            throw new Exception("Such login already exists");
        }
        
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public LoginResponseModel Login(LoginRequestModel model)
    {
        if (! _context.Users.Any(u => u.Login.Equals(model.UserName)))
        {
            throw new Exception("No such user exists");
        }

        ApplicationUser applicationUser = _context.Users.First(u => u.Login.Equals(model.UserName));

        var dbpass = applicationUser.Password;
        var dbsalt = applicationUser.Salt;
        var userpass = model.Password;
        var resultpass = GetHashedPasswordWithSalt(userpass, dbsalt);
        if (! resultpass.Equals(dbpass))
        {
            throw new Exception("Wrong credentials");
        }
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, applicationUser.Login),
           
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds,
            claims: userclaim
            );
        
        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
        

      
        var reftoken = GenerateRefreshToken();
        applicationUser.RefreshToken = reftoken;
        applicationUser.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();
        return new LoginResponseModel
        {
            Token = stringToken,
            RefreshToken = reftoken
        };
    }
    
    public Tuple<string, string> GetHashedPasswordAndSalt(string password)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA1,
            10000,
            256 / 8));

        var saltBase64 = Convert.ToBase64String(salt);

        return new Tuple<string, string>(hashed, saltBase64);
    }

    public LoginResponseModel Refresh(string refreshToken)
    {
        ApplicationUser user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken)!;
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"] ?? string.Empty));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
           
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds,
            claims: userclaim
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
            256 / 8));

        return currentHashedPassword;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}