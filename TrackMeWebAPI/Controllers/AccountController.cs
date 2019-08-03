using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
                    new Claim("UserID", applicationUser.Id),
                    new Claim(ClaimTypes.Email, applicationUser.Email),
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
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();

            
        }
    }
}