using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Seeds
{
    public class DefaultRolesSeed
    {
        public static async Task AddDefaultRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole {Name = "User"});
            }
        }
    }
}