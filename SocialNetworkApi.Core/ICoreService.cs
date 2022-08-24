using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core;

public interface ICoreService
{
    public Task<Entities.ApplicationUser?> GetApplicationUserByEmailAsync(string? requestEmail);

    public Task<bool> IsMailAvailableAsync(string email);

    public Task AddApplicationUser(Common.DTO.POST.Registration.ApplicationUser user);

    public Task<Core.Entities.ApplicationUser?> GetApplicationUserAsync(int id);

    public Task DeleteApplicationUser(ApplicationUser applicationUser);

    public Task UpdateApplicationUser(Entities.ApplicationUser user);

    public Task<IEnumerable<Common.DTO.GET.GetUsers.ApplicationUser>> ApplicationUserGetAllAsync(string? gender, string? roleName);
}