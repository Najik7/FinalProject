using System.Threading.Tasks;
using FinalProject.Context.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Seeds
{
    public class DefaultAdminSeed
    {
        public static async Task AddDefaultAdminAsync(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "Admin@gmail.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(user, "@admin123");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}