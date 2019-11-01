using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Logic
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DatabaseContext databaseContext;

        public AccountService(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext)
        {
            this.userManager = userManager;
            this.databaseContext = databaseContext;
        }

        public async Task<LoggedUserViewModel> Login(LoginViewModel loginViewModel)
        {
            var applicationUser = await userManager.FindByNameAsync(loginViewModel.Email);
            var isPasswordValid = await userManager.CheckPasswordAsync(applicationUser, loginViewModel.Password);
            var identityOptions = new IdentityOptions();

            if (applicationUser != null && isPasswordValid)
            {
                var applicationUserRole = userManager.GetRolesAsync(applicationUser).Result.First();

                var authClaims = new[]
                {
                    new Claim("ApplicationUserID", applicationUser.Id),
                    new Claim("Email", applicationUser.Email),
                    new Claim("Role", applicationUserRole),
                    new Claim(identityOptions.ClaimsIdentity.RoleClaimType, applicationUserRole)
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my security key hell yeach"));

                var token = new JwtSecurityToken(
                    issuer: "hello",
                    audience: "hello",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new LoggedUserViewModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Role = applicationUserRole
                };
            }
            throw new UserNotFoundException("Cannot find user with passed data.");
        }

        public async Task Register(RegisterViewModel registerViewModel)
        {
            var duplicatedUser = await userManager.FindByEmailAsync(registerViewModel.Email);
            if (duplicatedUser == null)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email
                };

                BasicUser basicUser = new BasicUser
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    PhoneNumber = registerViewModel.PhoneNumber
                };

                string hashedPassword = userManager.PasswordHasher.HashPassword(applicationUser, registerViewModel.Password);
                applicationUser.PasswordHash = hashedPassword;
                userManager.CreateAsync(applicationUser).Wait();
                userManager.AddToRoleAsync(applicationUser, ApplicationRoles.BasicUser.ToString()).Wait();
                basicUser.ApplicationUserID = applicationUser.Id;
                basicUser.Email = applicationUser.Email;
                databaseContext.BasicUsers.Add(basicUser as BasicUser);
                databaseContext.SaveChanges();
            }
            throw new DuplicatedUserException("User with passed email already exists.");
            
        }
    }
}
