using SocialNetworkApi.DTO.GET.GetUser;
using SocialNetworkApi.DTO.POST.Authentication;
using ApplicationUser = SocialNetworkApi.DTO.POST.Registration.ApplicationUser;

namespace SocialNetworkApi.Services;

public interface IApplicationUserService
{
    AuthenticationResponse Authenticate(AuthenticationRequest request);
    void Register(ApplicationUser user);
    void Update(int userId, DTO.PUT.UpdateApplicationUser.ApplicationUser user);
    IEnumerable<DTO.GET.GetUsers.ApplicationUser> GetAll(string? gender, string? rolName);
    void Delete(int id);
    DTO.GET.GetUser.ApplicationUser? GetById(int id);
    UserIDs GetIDs(int userId);
}