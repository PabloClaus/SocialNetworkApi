﻿using Microsoft.EntityFrameworkCore;
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
    public DbSet<ApplicationRole>? ApplicationRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ApplicationUser>(entity => {
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<ApplicationRole>(entity => {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        #region RolSeed

        modelBuilder.Entity<ApplicationRole>().HasData(
            new {Name = "Admin", RoleId = 1},
            new {Name = "User", RoleId = 2}
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
                RoleId = 1
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
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
                RoleId = 2
            }
        );

        #endregion
    }
}