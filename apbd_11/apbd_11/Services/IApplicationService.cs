using apbd_11.Controllers;
using Microsoft.AspNetCore.Identity.Data;
using RegisterRequest = apbd_11.Controllers.RegisterRequest;

namespace apbd_11.Services;

public interface IApplicationService
{
    Task Register(RegisterRequest model);
    Task<LoginResponseModel> Refresh(string refreshToken);
    Task<LoginResponseModel> Login(LoginRequestModel model);
    string GenerateRefreshToken();
    string[] GetHashedPasswordAndSalt(string password);
    string GetHashedPasswordWithSalt(string password, string salt);
    Task<bool> IsAuthorized(string username, string password);
}