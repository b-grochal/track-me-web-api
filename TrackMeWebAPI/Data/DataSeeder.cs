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
                var context = serviceProvider.GetRequiredService<DatabaseContext>();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin.ToString())).Wait();
                roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin.ToString())).Wait();
                context.SaveChanges();
            }
        }

        public static void CreateAdmins(IServiceProvider serviceProvider)
        {

        }

        public static void CreateUsers(IServiceProvider serviceProvider)
        {

        }
    }
}
