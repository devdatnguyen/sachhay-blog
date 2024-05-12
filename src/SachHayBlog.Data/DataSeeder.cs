using Microsoft.AspNetCore.Identity;
using SachHayBlog.Core.Domain.Identity;

namespace SachHayBlog.Data
{
    public class DataSeeder
    {
        public async Task SeedAsync (SachHayBlogContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var rootAdminRole = Guid.NewGuid();
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole() 
                { 
                    Id = rootAdminRole,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Quản trị viên",
                } );
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                var userId = Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    FirstName = "Dat",
                    LastName = "Nguyen",
                    Email = "n.quocdat.006@gmail.com",
                    NormalizedEmail = "N.QUOCDAT.006@GMAIL.COM",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    DateCreated = DateTime.Now
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123$");
                await context.Users.AddAsync(user);
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRole,
                    UserId = userId
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
