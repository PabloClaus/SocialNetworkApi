using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Authorization.Jwt;

public interface IJsonWebTokenService
{
    public string GenerateToken(ApplicationUser user);
    public int? ValidateToken(string token);
}