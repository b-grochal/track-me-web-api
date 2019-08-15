using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DatabaseContext databaseContext;

        public AccountController(UserManager<ApplicationUser> userManager, DatabaseContext databaseContext)
        {
            this.userManager = userManager;
            this.databaseContext = databaseContext;
        }

        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
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

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    role = applicationUserRole
                });
            }
            return Unauthorized();
            
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var duplicatedUser = await userManager.FindByEmailAsync(registerViewModel.Email);
            if(duplicatedUser == null)
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
                return Ok();
            }

            return Conflict(new
            {
                message = "User with this email already exist."
            });

        }
        
    }
}