using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.DTO.PUT.UpdateApplicationUser;

public class ApplicationUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [PasswordPropertyText] public string? Password { get; set; }

    [DataType(DataType.DateTime)] public DateTime? Birthday { get; set; } = default;

    [RegularExpression("MASC|FEM", ErrorMessage = "Only MASC or FEM are allowed.")]
    public string? Gender { get; set; } = default;
}