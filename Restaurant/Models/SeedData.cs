using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class SeedData
    {
        private const string ADMIN_ROLE = "admin";
        private const string MANAGER_ROLE = "manager";
        private const string CHEF_ROLE = "chef";

        public static void Populate(RestaurantDbContext db)
        {

        }

        public static async Task PopulateUsersAsync(UserManager<IdentityUser> userManager)
        {
            const string ADMIN_USERNAME = "admin@ipg.pt";
            const string ADMIN_PASSWORD = "Secret123$";

            const string MANAGER_USERNAME = "peter@ipg.pt";
            const string MANAGER_PASSWORD = "Secret123$";


            IdentityUser user = await userManager.FindByNameAsync(ADMIN_USERNAME);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = ADMIN_USERNAME,
                    Email = ADMIN_USERNAME
                };
                await userManager.CreateAsync(user, ADMIN_PASSWORD);
            }

            if(!await userManager.IsInRoleAsync(user, ADMIN_ROLE))
            {
                await userManager.AddToRoleAsync(user, ADMIN_ROLE);
            }

            user = await userManager.FindByNameAsync(MANAGER_USERNAME);

            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = MANAGER_USERNAME,
                    Email = MANAGER_USERNAME
                };
                await userManager.CreateAsync(user, MANAGER_PASSWORD);
            }

            user = await userManager.FindByNameAsync("test@ipg.pt");

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "test@ipg.pt",
                    Email = "test@ipg.pt"
                };
                await userManager.CreateAsync(user, ADMIN_PASSWORD);
            }

            //create other user accounts...

        }

        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            const string CAN_ADD_MENUS = "can_add_menus";

            if (!await roleManager.RoleExistsAsync(ADMIN_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));
            }
            if (!await roleManager.RoleExistsAsync(MANAGER_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(MANAGER_ROLE));
            }
            if (!await roleManager.RoleExistsAsync(CHEF_ROLE))
            {
                await roleManager.CreateAsync(new IdentityRole(CHEF_ROLE));
            }
        }
    }
}
