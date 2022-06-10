using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetworkApi.Core.Entities;

namespace SocialNetworkApi.Core.Data;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<ApplicationUser>? ApplicationUser { get; set; }
    public DbSet<ApplicationRol>? ApplicationRol { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region RolSeed

        modelBuilder.Entity<ApplicationRol>().HasData(
            new {Name = "Admin", RolId = 1},
            new {Name = "User", RolId = 2}
        );

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@admin.com",
                Birthday = null,
                Gender = null,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("@dmin"),
                RolId = 1
            });

        #endregion
    }
}