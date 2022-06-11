namespace SocialNetworkApi.DTO.GET.GetUser;

public class UserIDs
{
    public int Id { get; set; }
    public int RoleId { get; set; }

    public static explicit operator UserIDs(Core.Entities.ApplicationUser? v)
    {
        return new UserIDs
        {
            Id = v!.Id,
            RoleId = v.RoleId
        };
    }
}