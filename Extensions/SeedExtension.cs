using CodeSimits.Enums;
using CodeSimits.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeSimits.Extensions;

public static class SeedExtension
    {
        public static void UseCustomUserData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                CreateRoles(_roleManager).Wait();
                CreateAdmin(_userManager).Wait();
            }
        }

        private async static Task CreateRoles(RoleManager<IdentityRole> _roleManager)
        {
            int res = await _roleManager.Roles.CountAsync();
            if (res == 0)
            {
                foreach (Roles role in Enum.GetValues(typeof(Roles)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
        }

        private async static Task CreateAdmin(UserManager<AppUser> _userManager)
        {
            if (!await _userManager.Users.AnyAsync(x => x.UserName == "admin"))
            {
                AppUser admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                };
                admin.EmailConfirmed = true;

                await _userManager.CreateAsync(admin, "Abc!@Abc141");
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
