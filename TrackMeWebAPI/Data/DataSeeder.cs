using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;

namespace TrackMeWebAPI.Data
{
    public class DataSeeder
    {
        public static void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any())
            {
                var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin.ToString())).Wait();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin.ToString())).Wait();
                dbContext.SaveChanges();
            }
        }

        public static void CreateAdmins(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<DatabaseContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            if(!dbContext.Admins.Any())
            {
                ApplicationUser applicationAdmin = new ApplicationUser
                {
                    Email = "adam@gmail.com",
                    UserName = "adam@gmail.com"
                };

                string hashedPassword = userManager.PasswordHasher.HashPassword(applicationAdmin, "adam");
                applicationAdmin.PasswordHash = hashedPassword;
                userManager.CreateAsync(applicationAdmin).Wait();
                userManager.AddToRoleAsync(applicationAdmin, ApplicationRoles.Admin.ToString()).Wait();

                var admin = new Admin
                {
                    FirstName = "Adam",
                    LastName = "Małysz",
                    ApplicationUserID = applicationAdmin.Id

                };

                dbContext.Admins.Add(admin as Admin);
                dbContext.SaveChanges();


            }


        }

        public static void CreateUsers(IServiceProvider serviceProvider)
        {

        }
    }
}
