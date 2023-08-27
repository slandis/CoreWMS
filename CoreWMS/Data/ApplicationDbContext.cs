using CoreWMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;

namespace CoreWMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.IsEnabled).IsRequired();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
            });


            builder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(250);

            builder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(250);

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationRole>().HasData(new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = "1",
                    Name = "CORE_ADMIN",
                    NormalizedName = "CORE_ADMIN",
                    Description = "System Administrators",
                    IsEnabled = true
                  },
                new ApplicationRole
                {
                    Id = "2",
                    Name = "CORE_USER",
                    NormalizedName = "CORE_USER",
                    Description = "System Users",
                    IsEnabled = true
                },
                new ApplicationRole
                {
                    Id = "3",
                    Name = "CORE_INVENTORY",
                    NormalizedName = "CORE_INVENTORY",
                    Description = "Inventory Users",
                    IsEnabled = true
                },
                new ApplicationRole
                {
                    Id = "4",
                    Name = "CORE_RECEIVING",
                    NormalizedName = "CORE_RECEIVING",
                    Description = "Receiving Users",
                    IsEnabled = true
                },
                new ApplicationRole
                {
                    Id = "5",
                    Name = "CORE_SHIPPING",
                    NormalizedName = "CORE_SHIPPING",
                    Description = "Shipping Users",
                    IsEnabled = true
                },
                new ApplicationRole
                {
                    Id = "6",
                    Name = "CORE_PLANNING",
                    NormalizedName = "CORE_PLANNING",
                    Description = "Planning Users",
                    IsEnabled = true
                }
            });

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1", // primary key
                    UserName = "administrator",
                    NormalizedUserName = "ADMINISTRATOR",
                    FirstName = "",
                    LastName = "",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1", // for admin username
                    UserId = "1"  // for admin role
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = "1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4",
                    UserId = "1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "5",
                    UserId = "1"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "6",
                    UserId = "1"
                }
            );

            base.OnModelCreating(builder);
        }
    }
}