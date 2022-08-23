using SocialNetworkApi.DTO.GET.GetUser;
using SocialNetworkApi.DTO.POST.Authentication;
using ApplicationUser = SocialNetworkApi.DTO.POST.Registration.ApplicationUser;

namespace SocialNetworkApi.Services;

public interface IApplicationUserService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task RegisterAsync(ApplicationUser user);
    Task UpdateAsync(int userId, DTO.PUT.UpdateApplicationUser.ApplicationUser user);
    Task<IEnumerable<DTO.GET.GetUsers.ApplicationUser>> GetAllAsync(string? gender, string? rolName);
    Task DeleteAsync(int id);
    Task<DTO.GET.GetUser.ApplicationUser> GetByIdAsync(int id);
    Task<UserIDs> GetIDsAsync(int userId);
}