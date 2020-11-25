using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;
using TrackMe.Services.Exceptions;
using TrackMe.Services.Interfaces;

namespace TrackMe.Services.Logic
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ChangePassword(string applicationUserId, string oldPassword, string newPassword)
        {
            var applicationUser = await userManager.FindByIdAsync(applicationUserId);
            await userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
        }

        public async Task UdpateAccountData(ApplicationUser applicationUser)
        {
            await userManager.UpdateAsync(applicationUser);
        }
    }
}
