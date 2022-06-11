using SocialNetworkApi.Authorization.Jwt;
using SocialNetworkApi.Core;
using SocialNetworkApi.DTO.GET.GetUser;
using SocialNetworkApi.DTO.POST.Authentication;
using SocialNetworkApi.Model;
using ApplicationUser = SocialNetworkApi.DTO.POST.Registration.ApplicationUser;

namespace SocialNetworkApi.Services;

public class ApplicationUserService : IApplicationUserService
{
    #region ApplicationRole enum

    public enum ApplicationRol
    {
        Admin = 1,
        User = 2
    }

    #endregion

    private readonly ICoreService _coreService;
    private readonly IJsonWebTokenService _jsonWebTokenService;


    public ApplicationUserService(IJsonWebTokenService jsonWebTokenService, ICoreService coreService)
    {
        _jsonWebTokenService = jsonWebTokenService;
        _coreService = coreService;
    }

    #region IApplicationUserService Members

    public AuthenticationResponse Authenticate(AuthenticationRequest request)
    {
        var user = _coreService.GetApplicationUserByEmail(request.Email);
        if (IsApplicationUserNotOk(request, user)) throw new Exception("Username or password is incorrect");

        var response = (AuthenticationResponse) user!;
        response.Token = _jsonWebTokenService.GenerateToken(user!);
        return response;

        static bool IsApplicationUserNotOk(AuthenticationRequest request, Core.Entities.ApplicationUser? user)
        {
            return user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        }
    }

    public void Register(ApplicationUser user)
    {
        if (!_coreService.IsMailAvailable(user.Email!))
            throw new Exception("Email '" + user.Email + "' is already taken");

        var entityUser = GetEntityUser(user);
        _coreService.AddApplicationUser(entityUser);

        static Core.Entities.ApplicationUser GetEntityUser(ApplicationUser userTemp)
        {
            return new Core.Entities.ApplicationUser
            {
                FirstName = userTemp.FirstName,
                LastName = userTemp.LastName,
                Email = userTemp.Email,
                Birthday = userTemp.Birthday,
                Gender = userTemp.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userTemp.Password),
                RoleId = (int) ApplicationRol.User
            };
        }
    }

    public void Update(int userId, DTO.PUT.UpdateApplicationUser.ApplicationUser user)
    {
        var applicationUser = ValidateAndCreateNewUser(GetApplicationUser(userId)!, user);

        _coreService.UpdateApplicationUser(applicationUser!);

        static Core.Entities.ApplicationUser ValidateAndCreateNewUser(Core.Entities.ApplicationUser tempDbUser,
            DTO.PUT.UpdateApplicationUser.ApplicationUser tempDtoUser)
        {
            if (!string.IsNullOrEmpty(tempDtoUser.FirstName)) tempDbUser!.FirstName = tempDtoUser.FirstName;
            if (!string.IsNullOrEmpty(tempDtoUser.LastName)) tempDbUser!.LastName = tempDtoUser.LastName;
            if (DateTime.TryParse(tempDtoUser.Birthday.ToString(), out var tempDate))
                tempDbUser!.Birthday = tempDate;
            if (!string.IsNullOrEmpty(tempDtoUser.Gender)) tempDbUser!.Gender = tempDtoUser.Gender;

            if (!string.IsNullOrEmpty(tempDtoUser.Password))
                tempDbUser!.PasswordHash = BCrypt.Net.BCrypt.HashPassword(tempDtoUser.Password);

            return tempDbUser;
        }
    }

    public IEnumerable<DTO.GET.GetUsers.ApplicationUser> GetAll(string? gender, string? roleName)
    {
        return _coreService.ApplicationUserGetAll(gender, roleName).Select(x => (DTO.GET.GetUsers.ApplicationUser) x);
    }

    public void Delete(int id)
    {
        var user = GetApplicationUser(id);
        _coreService.DeleteApplicationUser(user!);
    }

    public DTO.GET.GetUser.ApplicationUser GetById(int id)
    {
        var user = GetApplicationUser(id);

        return (DTO.GET.GetUser.ApplicationUser) user;
    }

    public UserIDs GetIDs(int id)
    {
        var user = GetApplicationUser(id);

        return (UserIDs) user;
    }

    #endregion

    private Core.Entities.ApplicationUser? GetApplicationUser(int id)
    {
        var user = _coreService.GetApplicationUser(id);
        if (user == null) throw new Exception("User does not exist");
        return user;
    }
}