using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/Authendication/Login")
        {
             await _next(context);
            return;
        }
        // Check if Authorization header is present
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Authorization token is missing or invalid.");
            return;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        // Validate token (example with JwtSecurityTokenHandler)
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Validate the token (example: check the role claim for "user")
            var role = jwtToken.Claims.FirstOrDefault(x => x.Type.Contains("role"))?.Value;
            if (role != "user")
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("User does not have the necessary permissions.");
                return;
            }
        }
        catch
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid token.");
            return;
        }

        // If authorization is successful, call the next middleware in the pipeline
        await _next(context);
    }
}
