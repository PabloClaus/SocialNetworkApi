using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.DTO.POST.Authentication;

public class AuthenticationRequest
{
    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] [PasswordPropertyText] public string? Password { get; set; }
}
/*
{
"Email": "claus@test.com",
"password": "1234567890"
}
*/