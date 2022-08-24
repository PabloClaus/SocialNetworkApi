using SocialNetworkApi.Core.Entities;
using ApplicationUser = SocialNetworkApi.Common.DTO.GET.GetUser.ApplicationUser;

namespace SocialNetworkApi.Authorization.Jwt;

public interface IJsonWebTokenService
{
    public string GenerateToken(Core.Entities.ApplicationUser user);
    public int? ValidateToken(string token);
}