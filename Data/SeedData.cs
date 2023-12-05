using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public static class SeedData
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager,
                                            RoleManager<IdentityRole> roleManager)
        {
            String roleName = "Admin" ;

            IdentityResult roleResult;

                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
          

            ApplicationUser user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                };
                await userManager.CreateAsync(user, "Admin@123");
            }
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
