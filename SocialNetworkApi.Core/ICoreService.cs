using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core;

public interface ICoreService
{
    public ApplicationUser? GetApplicationUserByEmail(string? requestEmail);

    public bool IsMailAvailable(string email);

    public void AddApplicationUser(ApplicationUser entityUser);

    public ApplicationUser? GetApplicationUser(int id);

    public void DeleteApplicationUser(ApplicationUser applicationUser);

    public void UpdateApplicationUser(ApplicationUser applicationUser);

    public IEnumerable<ApplicationUser> ApplicationUserGetAll(string? gender, string? rolName);
}