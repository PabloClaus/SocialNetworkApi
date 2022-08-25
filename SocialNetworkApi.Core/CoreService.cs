using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetworkApi.Core.Data;
using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core
{
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

        public async Task<ApplicationUser?> GetApplicationUserByEmailAsync(string? requestEmail)
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
            _context.ApplicationUser!.Add(_mapper.Map<ApplicationUser>(user));
            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserAsync(int id)
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

        public async Task UpdateApplicationUser(ApplicationUser user)
        {
            _context.ApplicationUser!.Update(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("The same user is currently being modified. " + ex);
            }
        }

        public async Task<IEnumerable<Common.DTO.GET.GetUsers.ApplicationUser>> ApplicationUserGetAllAsync(
            string? gender, string? roleName)
        {
            var user = _context.ApplicationUser!
                .AsSplitQuery()
                .Include(x => x.Role!)
                .AsNoTracking()
                .ProjectTo<Common.DTO.GET.GetUsers.ApplicationUser>(_mapper.ConfigurationProvider);
            if (gender != null)
            {
                user = user.Where(x => x.Gender == gender);
            }

            if (roleName != null)
            {
                user = user.Where(x => x.RoleName == roleName);
            }

            return await user.ToListAsync();
        }

        #endregion
    }
}