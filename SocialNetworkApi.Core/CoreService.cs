using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Core.Data;
using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core;

public class CoreService : ICoreService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CoreService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region ICoreService Members

    public async Task<Core.Entities.ApplicationUser?> GetApplicationUserByEmailAsync(string? requestEmail)
    {
        return (await _context.ApplicationUser!
            .SingleOrDefaultAsync(x => x.Email == requestEmail))!;
    }

    public async Task<bool> IsMailAvailableAsync(string email)
    {
        return await _context.ApplicationUser!.AnyAsync(x => x.Email == email);
    }

    public async Task AddApplicationUser(Common.DTO.POST.Registration.ApplicationUser user)
    {
        _context.ApplicationUser!.Add(_mapper.Map<Entities.ApplicationUser>(user));
        await _context.SaveChangesAsync();
    }

    public async Task<Core.Entities.ApplicationUser?> GetApplicationUserAsync(int id)
    {
        return (await _context.ApplicationUser!
            .Include(x => x.Role!)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task DeleteApplicationUser(ApplicationUser applicationUser)
    {
        _context.ApplicationUser!.Remove(applicationUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateApplicationUser (Core.Entities.ApplicationUser user)
    {
        _context.ApplicationUser!.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Common.DTO.GET.GetUsers.ApplicationUser>> ApplicationUserGetAllAsync(string? gender, string? roleName)
    {

        if (AreThereFiltersToApply( gender, roleName))
            return await _context.ApplicationUser!
                .AsSplitQuery()
                .Include(x => x.Role!)
                .AsNoTracking()
                .ProjectTo<Common.DTO.GET.GetUsers.ApplicationUser>(_mapper.ConfigurationProvider)
                .Where(GetQueryExpression(gender, roleName))
                .ToListAsync();

        return await _context.ApplicationUser!
            .Include(x => x.Role!)
            .AsSplitQuery()
            .ProjectTo<Common.DTO.GET.GetUsers.ApplicationUser>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();


        bool AreThereFiltersToApply(string? aGender, string? aRoleName)
        {
            return !string.IsNullOrEmpty(aGender) || !string.IsNullOrEmpty(aRoleName);
        }
        
        Expression<Func<Common.DTO.GET.GetUsers.ApplicationUser, bool>> GetQueryExpression(string? aGender, string? aRoleName)
        {
            if(string.IsNullOrEmpty(aGender))
                return x => x.RoleName == aRoleName;
            if(string.IsNullOrEmpty(aRoleName))
                return x => x.Gender == aGender;
            return x => x.RoleName == aRoleName && x.Gender == aGender;
        }

    }

    #endregion
}