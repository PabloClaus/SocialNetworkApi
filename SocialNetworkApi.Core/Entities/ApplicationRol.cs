using System.ComponentModel.DataAnnotations;

namespace SocialNetworkApi.Core.Entities;

public class ApplicationRol
{
    [Key] public int RolId { get; set; }
    [Required] public string? Name { get; set; }
}