using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityService.Models;
using System;

namespace SecurityService
{
    /// <summary>
    /// Seucurity Context Class
    /// </summary>
    /// <seealso cref="IdentityDbContext{ApplicationUser, ApplicationRole, Guid}" />
    public class SecurityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SecurityContext(DbContextOptions<SecurityContext> options): base(options)
        {
        }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Seed Data
            //Seed teh data for Testing
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasData(new ApplicationUser() { 
                    Id = userId,
                    UserName = "vecheverria",
                    NormalizedUserName = "VECHEVERRIA",
                    Email = "viktoralfa19@hotmail.com",
                    NormalizedEmail = "VIKTORALFA19@HOTMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEK0P3wqVX+wVwCkh+IGz75osuM+O/Ze3pzy6kWH4tzJOYT5d9YEUvd4cJaSjlB8Ykw==",
                    SecurityStamp = "E3CQR4AEZMMCB5EIBJGLUCDGVF6O3PNS",
                    ConcurrencyStamp = "a5c8b60a-ef7c-4400-922f-623e226e1431",
                    PhoneNumber = "0995005863",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                });
            });
            builder.Entity<ApplicationRole>(entity=> {
                entity.HasData(new ApplicationRole() {
                    Id = roleId,
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    ConcurrencyStamp = "E3CQR4AEZMMCB5EIBJGLUCDGVF6O3PNS"
                });
            });
            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                RoleId = roleId,
                UserId = userId
            });
            #endregion
        }
    }
}
