namespace SocialNetworkApi.Common.DTO.GET.GetUser;

public class ApplicationUser
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Gender { get; set; }
    public string? RoleName { get; set; }

}