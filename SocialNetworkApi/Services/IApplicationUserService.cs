using SocialNetworkApi.Common.DTO.GET.GetUser;
using SocialNetworkApi.Common.DTO.POST.Authentication;

namespace SocialNetworkApi.Services;

public interface IApplicationUserService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    Task RegisterAsync(Common.DTO.POST.Registration.ApplicationUser user);
    Task UpdateAsync(int userId, Common.DTO.PUT.UpdateApplicationUser.ApplicationUser user);
    Task<IEnumerable<Common.DTO.GET.GetUsers.ApplicationUser>> GetAllAsync(string? gender, string? rolName);
    Task DeleteAsync(int id);
    Task<Common.DTO.GET.GetUser.ApplicationUser> GetByIdAsync(int id);
    Task<UserIDs> GetIDsAsync(int userId);
}