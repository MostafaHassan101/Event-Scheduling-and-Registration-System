//namespace BasicShop.Application.Common.Behaviours;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//public class TokenValidationMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TokenValidationMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        var tokenValidator = context.RequestServices.GetRequiredService<ITokenValidator>(); // Implement ITokenValidator
//        var token = context.Request.Cookies["access_token"]; // Get the token from the cookie

//        if (token != null && await tokenValidator.ValidateTokenAsync(token))
//        {
//            await _next(context);
//        }
//        else
//        {
//            context.Response.StatusCode = 401; // Unauthorized
//            await context.Response.WriteAsync("Invalid or missing token.");
//        }
//    }
//}