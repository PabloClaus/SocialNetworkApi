using AutoMapper;
using SocialNetworkApi.Authorization.Jwt;
using SocialNetworkApi.Common.DTO.GET.GetUser;
using SocialNetworkApi.Common.DTO.POST.Authentication;
using SocialNetworkApi.Core;
using ApplicationUser = SocialNetworkApi.Common.DTO.POST.Registration.ApplicationUser;

namespace SocialNetworkApi.Services;

public class ApplicationUserService : IApplicationUserService
{
    #region ApplicationRole enum

    public enum ApplicationRole
    {
        Admin = 1,
        User = 2
    }

    #endregion

    private readonly ICoreService _coreService;
    private readonly IJsonWebTokenService _jsonWebTokenService;
    private readonly IMapper _mapper;


    public ApplicationUserService(IJsonWebTokenService jsonWebTokenService, ICoreService coreService, IMapper mapper)
    {
        _jsonWebTokenService = jsonWebTokenService;
        _coreService = coreService;
        _mapper = mapper;
    }

    #region IApplicationUserService Members

    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
        var user = await _coreService.GetApplicationUserByEmailAsync(request.Email);
        if (IsApplicationUserNotOk(request, user)) throw new Exception("Username or password is incorrect");

        var response = _mapper.Map<AuthenticationResponse> (user!);
        response.Token = _jsonWebTokenService.GenerateToken(user!);
        return response;

        static bool IsApplicationUserNotOk(AuthenticationRequest request, Core.Entities.ApplicationUser? user)
        {
            return user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        }
    }

    public async Task RegisterAsync(ApplicationUser user)
    {
        if (!await _coreService.IsMailAvailableAsync(user.Email!))
            throw new Exception("Email '" + user.Email + "' is already taken");

        await _coreService.AddApplicationUser(user);
    }

    public async Task UpdateAsync(int userId, Common.DTO.PUT.UpdateApplicationUser.ApplicationUser user)
    {
        var applicationUser = ValidateAndCreateNewUser(await GetApplicationUserAsync(userId), user);

        await _coreService.UpdateApplicationUser(applicationUser!);

        static Core.Entities.ApplicationUser ValidateAndCreateNewUser(Core.Entities.ApplicationUser tempDbUser,
            Common.DTO.PUT.UpdateApplicationUser.ApplicationUser tempDtoUser)
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

    public async Task<IEnumerable<Common.DTO.GET.GetUsers.ApplicationUser>> GetAllAsync(string? gender, string? roleName)
    {
        return await _coreService.ApplicationUserGetAllAsync(gender, roleName);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetApplicationUserAsync(id);
        if (user!.RoleId == (int)ApplicationRole.Admin)
            throw new Exception("You can't delete the admin user");
        await _coreService.DeleteApplicationUser(user);
    }

    public async Task<Common.DTO.GET.GetUser.ApplicationUser> GetByIdAsync(int id)
    {
        var user = await GetApplicationUserAsync(id);

        return _mapper.Map<Common.DTO.GET.GetUser.ApplicationUser>(user);
    }

    public async Task <UserIDs> GetIDsAsync(int id)
    {
        var user = await GetApplicationUserAsync(id);
        return _mapper.Map<UserIDs>(user);
    }

    #endregion

    private async Task<Core.Entities.ApplicationUser> GetApplicationUserAsync(int id)
    {
        var user = await _coreService.GetApplicationUserAsync(id);
        if (user == null) throw new Exception("User does not exist");
        return user;
    }
}