using apbd_11.Services;

namespace apbd_11.Middlewares;

using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string Realm = "My Realm";

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

            if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                authHeader.Parameter != null)
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                // Replace with your own user validation logic
                if (await IsAuthorized(username, password))
                {
                    await _next(context);
                    return;
                }
            }
        }

        // Return 401 Unauthorized if authentication fails
        context.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private async Task<bool> IsAuthorized(string username, string password)
    {
        // Replace this with your own logic
        return true;
    }
}
