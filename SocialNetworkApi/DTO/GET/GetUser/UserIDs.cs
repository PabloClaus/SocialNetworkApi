namespace SocialNetworkApi.DTO.GET.GetUser;

public class UserIDs
{
    public int Id { get; set; }
    public int RolId { get; set; }

    public static explicit operator UserIDs(Core.Entities.ApplicationUser? v)
    {
        return new UserIDs
        {
            Id = v!.Id,
            RolId = v.RolId
        };
    }
}