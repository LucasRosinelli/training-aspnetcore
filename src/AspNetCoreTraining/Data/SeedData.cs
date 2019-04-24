using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists)
            {
                return;
            }

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(u => u.UserName == Constants.AdministratorTestUser)
                .SingleOrDefaultAsync();

            if (testAdmin != null)
            {
                return;
            }

            testAdmin = new IdentityUser()
            {
                UserName = Constants.AdministratorTestUser,
                Email = Constants.AdministratorTestUser
            };
            await userManager.CreateAsync(testAdmin, Constants.AdministratorTestUserPassword);
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }
}