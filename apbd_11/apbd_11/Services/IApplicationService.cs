using apbd_11.Controllers;
using Microsoft.AspNetCore.Identity.Data;
using RegisterRequest = apbd_11.Controllers.RegisterRequest;

namespace apbd_11.Services;

public interface IApplicationService
{
    void Register(RegisterRequest model);
    LoginResponseModel Refresh(string refreshToken);
    LoginResponseModel Login(LoginRequestModel model);
    string GenerateRefreshToken();
    string[] GetHashedPasswordAndSalt(string password);
    string GetHashedPasswordWithSalt(string password, string salt);
}