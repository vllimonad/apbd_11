using System.Net;

namespace apbd_11.Middlewares;

public class CustomExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandler> _logger;
    public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        try 
        {
            await _next(context);
        } 
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = new
            {
                error = new
                {
                    message = "An error occurred while processing request",
                    detail = ex.Message
                }
            };
            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}