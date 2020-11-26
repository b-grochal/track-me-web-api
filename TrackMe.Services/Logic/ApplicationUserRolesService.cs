using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;
using TrackMe.Services.Interfaces;

namespace TrackMe.Services.Logic
{
    public class ApplicationUserRolesService : IApplicationUserRolesService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserRolesService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetApplicationUserRole(string applicationUserId)
        {
            var applicationUser = await userManager.FindByIdAsync(applicationUserId);
            var applicationUsersRoles = await userManager.GetRolesAsync(applicationUser);
            return applicationUsersRoles.First();
        }
    }
}
