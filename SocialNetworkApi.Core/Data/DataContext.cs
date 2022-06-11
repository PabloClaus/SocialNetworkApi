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
                Email = "admin@mail.com",
                Birthday = null,
                Gender = null,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                RolId = 1
            },
            new ApplicationUser
            {
                Id = 2,
                FirstName = "Barrett",
                LastName = "Lowe",
                Email = "blowe@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("blowe"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 3,
                FirstName = "Curtis",
                LastName = "Disney",
                Email = "cdisney@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("cdisney"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 4,
                FirstName = "Stafford",
                LastName = "Owston",
                Email = "sowston@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("sowston"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 5,
                FirstName = "Kira",
                LastName = "Lowe",
                Email = "klowe@mail.com",
                Birthday = null,
                Gender = "FEM",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("sowston"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 6,
                FirstName = "Kira",
                LastName = "Haward",
                Email = "khaward@mail.com",
                Birthday = null,
                Gender = "FEM",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("khaward"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 7,
                FirstName = "Ronda",
                LastName = "Lowe",
                Email = "rlowe@mail.com",
                Birthday = null,
                Gender = "FEM",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("rlowe"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 8,
                FirstName = "Everett",
                LastName = "Sweet",
                Email = "esweet@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("esweet"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 9,
                FirstName = "Doyle",
                LastName = "Nicolson",
                Email = "dnicolson@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("dnicolson"),
                RolId = 2
            },
            new ApplicationUser
            {
                Id = 10,
                FirstName = "Pablo",
                LastName = "Claus",
                Email = "pclaus@mail.com",
                Birthday = null,
                Gender = "MASC",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("pclaus"),
                RolId = 2
            }
        );

        #endregion
    }
}