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

    public ApplicationUser? GetApplicationUserByEmail(string? requestEmail)
    {
        return _context.ApplicationUser!.SingleOrDefault(x => x.Email == requestEmail);
    }

    public bool IsMailAvailable(string email)
    {
        return !_context.ApplicationUser!.Any(x => x.Email == email);
    }

    public void AddApplicationUser(ApplicationUser entityUser)
    {
        _context.ApplicationUser!.Add(entityUser);
        _context.SaveChanges();
    }

    public ApplicationUser? GetApplicationUser(int id)
    {
        return _context.ApplicationUser!
            .Include(x => x.Rol!)
            .FirstOrDefault(x => x.Id == id);
    }

    public void DeleteApplicationUser(ApplicationUser applicationUser)
    {
        _context.ApplicationUser!.Remove(applicationUser);
        _context.SaveChanges();
    }

    public void UpdateApplicationUser(ApplicationUser applicationUser)
    {
        _context.ApplicationUser!.Update(applicationUser);
        _context.SaveChanges();
    }

    public IEnumerable<ApplicationUser> ApplicationUserGetAll()
    {
        return _context.ApplicationUser!
            .Include(x => x.Rol!)
            .ToList();
    }

    #endregion
}