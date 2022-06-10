using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.Core.Entities;

public class ApplicationUser
{
    [Key] public int Id { get; set; }

    [Required] public string? FirstName { get; set; }

    [Required] public string? LastName { get; set; }

    [Required] public string? Email { get; set; }

    public DateTime? Birthday { get; set; }
    public string? Gender { get; set; }

    [Required] public string? PasswordHash { get; set; }

    public ApplicationRol? Rol { get; set; }

    [Required] public int RolId { get; set; }
}