using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Model;

public class AuthenticationResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }

    public static explicit operator AuthenticationResponse(ApplicationUser v)
    {
        return new AuthenticationResponse
        {
            Id = v.Id,
            FirstName = v.FirstName,
            LastName = v.LastName,
            Email = v.Email
        };
    }
}