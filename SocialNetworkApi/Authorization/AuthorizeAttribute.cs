using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetworkApi.DTO.GET.GetUser;

namespace SocialNetworkApi.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IList<int> _roles;

    public AuthorizeAttribute(params int[]? roles)
    {
        _roles = roles ?? Array.Empty<int>();
    }

    #region IAuthorizationFilter Members

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var userIDs = (UserIDs) context!.HttpContext!.Items["UserIDs"]!;
        if (userIDs == null || (_roles.Any() && !_roles.Contains(userIDs.RoleId)))
            context.Result = new JsonResult(new {message = "Unauthorized"})
                {StatusCode = StatusCodes.Status401Unauthorized};
    }

    #endregion
}