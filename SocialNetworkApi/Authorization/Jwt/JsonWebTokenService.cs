using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialNetworkApi.Core.Entities;
using SocialNetworkApi.Model;

namespace SocialNetworkApi.Authorization.Jwt;

public class JsonWebTokenService : IJsonWebTokenService
{
    private readonly AppSettings _appSettings;

    public JsonWebTokenService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    #region IJsonWebTokenService Members

    public string GenerateToken(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings!.Secret!);
        var tokenDescriptor = GetSecurityTokenDescriptor(user, key);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

        static SecurityTokenDescriptor GetSecurityTokenDescriptor(ApplicationUser applicationUser, byte[] secret)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", applicationUser.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }

    public int? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings!.Secret!);
        try
        {
            return GetValidatedUserId(token, tokenHandler, key);
        }
        catch
        {
            return null;
        }

        static int? GetValidatedUserId(string _token, ISecurityTokenValidator jwtSecurityTokenHandler, byte[] secret)
        {
            jwtSecurityTokenHandler.ValidateToken(_token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secret),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken) validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return userId;
        }
    }

    #endregion
}