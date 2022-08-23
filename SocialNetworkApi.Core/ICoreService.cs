using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core;

public interface ICoreService
{
    public Task<ApplicationUser?> GetApplicationUserByEmailAsync(string? requestEmail);

    public Task<bool> IsMailAvailableAsync(string email);

    public Task AddApplicationUser(ApplicationUser entityUser);

    public Task<ApplicationUser?> GetApplicationUserAsync(int id);

    public Task DeleteApplicationUser(ApplicationUser applicationUser);

    public Task UpdateApplicationUser(ApplicationUser applicationUser);

    public Task<IEnumerable<ApplicationUser>> ApplicationUserGetAllAsync(string? gender, string? roleName);
}