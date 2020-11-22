using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;
using TrackMe.Services.Exceptions;
using TrackMe.Services.Interfaces;
using TrackMeWebAPI.Models;

namespace TrackMe.Services.Logic
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<ApplicationUserIdentity> applicationUserIdentityManager;

        public AccountService(UserManager<ApplicationUserIdentity> userManager)
        {
            this.applicationUserIdentityManager = userManager;
        }

        public async Task<ApplicationUserIdentity> Authenticate(string email, string password)
        {
            var applicationUserIdentity = await applicationUserIdentityManager.FindByNameAsync(email);
            var isPasswordValid = await applicationUserIdentityManager.CheckPasswordAsync(applicationUserIdentity, password);

            if (applicationUserIdentity != null && isPasswordValid)
            {
                return applicationUserIdentity;
            }

            throw new AuthorizationException("Incorrect email or password.");
        }
    }
}
