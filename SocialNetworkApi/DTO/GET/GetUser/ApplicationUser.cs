namespace SocialNetworkApi.DTO.GET.GetUser;

public class ApplicationUser
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Gender { get; set; }
    public string? RoleName { get; set; }

    public static explicit operator ApplicationUser(Core.Entities.ApplicationUser? v)
    {
        return new ApplicationUser
        {
            Id = v!.Id,
            FirstName = v.FirstName,
            LastName = v.LastName,
            Email = v.Email,
            Birthday = v.Birthday,
            Gender = v.Gender,
            RoleName = v.Role!.Name
        };
    }
}