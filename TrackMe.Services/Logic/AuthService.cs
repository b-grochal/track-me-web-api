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
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        
        public AuthService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> Authenticate(string email, string password)
        {
            var applicationUser = await userManager.FindByEmailAsync(email);
            var isPasswordValid = await userManager.CheckPasswordAsync(applicationUser, password);

            if (applicationUser != null && isPasswordValid)
            {
                return applicationUser;
            }

            throw new AuthenticationException("Incorrect email or password.");
        }

        public async Task Register(BasicUser newBasicUser, string password)
        {
            var isBasicUserDuplicated = await userManager.FindByEmailAsync(newBasicUser.Email) == null;

            if (isBasicUserDuplicated)
            {
                throw new RegistrationException("User with passed email already exists.");
            }

            await userManager.CreateAsync(newBasicUser, password);
            await userManager.AddToRoleAsync(newBasicUser, ApplicationUserRoles.BasicUser);
        }
    }
}
