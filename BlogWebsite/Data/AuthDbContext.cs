﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BlogWebsite.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected AuthDbContext()
        {
        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var AdminId = "43e56eed-6303-4310-a8ee-a8a49f48f568";
            var superRoldAdminId = "847782ad-52b1-462c-9cbd-8180c3e3c8c6";
            var userRoleId = "374cfae7-9e47-4a0c-aa04-1be9e5a1ce8d";
            var Roles = new List<IdentityRole>
            {
                new IdentityRole{
                Name = "admin",
                NormalizedName = "admin",
                Id= AdminId,
                ConcurrencyStamp=AdminId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id= superRoldAdminId,
                    ConcurrencyStamp = superRoldAdminId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id= userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(Roles);
            var superAdminId = "6fb79341-d865-49cd-a8fa-08cd397dd408";
            var superAdminUser = new IdentityUser
            {
               UserName="superAdmin@bloggie.com",
               Email= "superAdmin@bloggie.com",
               NormalizedEmail = "superAdmin@bloggie.com".ToUpper(),
               Id= superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                                        .HashPassword(superAdminUser, "superAdmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);




            var newAdminId = "51ae3cfb-5e28-4e9b-b7aa-8a655b449756"; // Generate a new GUID for the new admin user
            var newAdminUser = new IdentityUser
            {
                UserName = "newadmin@bloggie.com",
                Email = "newadmin@bloggie.com",
                NormalizedEmail = "newadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "newadmin@bloggie.com".ToUpper(),
                Id = newAdminId,
            };
            newAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(newAdminUser, "newAdmin@123");

            builder.Entity<IdentityUser>().HasData(newAdminUser);

            var superAdminRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId=AdminId,
                    UserId=superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId=superRoldAdminId,
                    UserId=superAdminId
                }
            };
              builder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
            var newAdminUserRoles = new List<IdentityUserRole<string>>
{
    new IdentityUserRole<string>
    {
        RoleId = AdminId,
        UserId = newAdminId
    },
    new IdentityUserRole<string>
    {
        RoleId = userRoleId,
        UserId = newAdminId
    },
    new IdentityUserRole<string>
    {
        RoleId = superRoldAdminId, // Use the existing SuperAdmin role ID
        UserId = newAdminId
    }
};

            builder.Entity<IdentityUserRole<string>>().HasData(newAdminUserRoles);
        }
    }
}
