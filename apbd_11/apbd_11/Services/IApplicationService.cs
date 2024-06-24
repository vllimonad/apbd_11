using apbd_11.Controllers;
using Microsoft.AspNetCore.Identity.Data;
using RegisterRequest = apbd_11.Controllers.RegisterRequest;

namespace apbd_11.Services;

public interface IApplicationService
{
    void RegisterUser(RegisterRequest model);
    LoginResponseModel Login(LoginRequestModel model);
    string GenerateRefreshToken();
    string GetHashedPasswordWithSalt(string password, string salt);
    Tuple<string, string> GetHashedPasswordAndSalt(string password);

    LoginResponseModel Refresh(string refreshToken);
}