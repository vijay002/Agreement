using Agreement.Domain.Account;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agreement.Domain.SeedData
{
    public static class AppDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
            )
        {
        }

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
               // context.Database.EnsureCreated();

                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();




                if (!context.Users.Any(usr => usr.UserName == "admin@gmail.com"))
                {
                    var user = new ApplicationUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                    };

                    var userResult = _userManager.CreateAsync(user, "Pass#123").Result;
                }

                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = _roleManager.CreateAsync(new ApplicationRole { Name = "Admin" }).Result;
                }

                var adminUser = _userManager.FindByNameAsync("admin@gmail.com").Result;
                var adminRole = _userManager.AddToRolesAsync(adminUser, new string[] { "Admin" }).Result;

                var _adminRole = _roleManager.Roles.Where(x => x.Name == "Admin").FirstOrDefault();

                context.SaveChanges();
            }

        }

    }
}
