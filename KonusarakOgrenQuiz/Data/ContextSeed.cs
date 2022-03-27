using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenQuiz.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Models.Roles.Basic.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Models.Roles.SuperAdmin.ToString());

            }
                
                

            
        }

    }
}
