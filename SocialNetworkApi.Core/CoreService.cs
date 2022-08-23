using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Core.Data;
using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core;

public class CoreService : ICoreService
{
    private readonly DataContext _context;

    public CoreService(DataContext context)
    {
        _context = context;
    }

    #region ICoreService Members

    public async Task<ApplicationUser?> GetApplicationUserByEmailAsync(string? requestEmail)
    {
        return await _context.ApplicationUser!.SingleOrDefaultAsync(x => x.Email == requestEmail);
    }

    public async Task<bool> IsMailAvailableAsync(string email)
    {
        return await _context.ApplicationUser!.AnyAsync(x => x.Email == email);
    }

    public async Task AddApplicationUser(ApplicationUser entityUser)
    {
        _context.ApplicationUser!.Add(entityUser);
        await _context.SaveChangesAsync();
    }

    public async Task<ApplicationUser?> GetApplicationUserAsync(int id)
    {
        return await _context.ApplicationUser!
            .Include(x => x.Role!)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task DeleteApplicationUser(ApplicationUser applicationUser)
    {
        _context.ApplicationUser!.Remove(applicationUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateApplicationUser (ApplicationUser applicationUser)
    {
        _context.ApplicationUser!.Update(applicationUser);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> ApplicationUserGetAllAsync(string? gender, string? roleName)
    {

        if (AreThereFiltersToApply( gender, roleName))
            return await _context.ApplicationUser!
                .Include(x => x.Role!)
                .AsNoTracking()
                .Where(GetQueryExpression(gender, roleName))
                .ToListAsync();

        return await _context.ApplicationUser!
            .Include(x => x.Role!)
            .AsNoTracking()
            .ToListAsync();


        bool AreThereFiltersToApply(string? aGender, string? aRoleName)
        {
            return !string.IsNullOrEmpty(aGender) || !string.IsNullOrEmpty(aRoleName);
        }
        
        Expression<Func<ApplicationUser, bool>> GetQueryExpression(string? aGender, string? aRoleName)
        {
            if(string.IsNullOrEmpty(aGender))
                return x => x.Role!.Name == aRoleName;
            if(string.IsNullOrEmpty(aRoleName))
                return x => x.Gender == aGender;
            return x => x.Role!.Name == aRoleName && x.Gender == aGender;
        }

    }

    #endregion
}