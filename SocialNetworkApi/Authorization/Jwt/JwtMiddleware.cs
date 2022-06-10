using SocialNetworkApi.Services;

namespace SocialNetworkApi.Authorization.Jwt;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IApplicationUserService applicationUserService,
        IJsonWebTokenService jsonWebTokenService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jsonWebTokenService.ValidateToken(token!);
        if (userId != null) context.Items["UserIDs"] = applicationUserService.GetIDs(userId.Value);

        await _next(context);
    }
}