using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.Core.Entities;

public class ApplicationRole
{
    [Key] public int RoleId { get; set; }
    [Required] public string? Name { get; set; }
}