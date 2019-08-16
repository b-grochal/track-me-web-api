using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminsController(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        // GET api/admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminViewModel>>> GetAdmins()
        {
            return await this.databaseContext.Admins
                .Select(x => new AdminViewModel
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                }).ToListAsync();

        }

        // GET api/admins/4
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminViewModel>> GetAdminDetails(int id)
        {
            var admin = await this.databaseContext.Admins.FindAsync(id);

            return new AdminViewModel
            {
                ID = admin.ID,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email
            };
        }

        // POST api/admins
        [HttpPost]
        public async Task<ActionResult> CreateAdmin([FromBody] NewAdminViewModel newAdminViewModel)
        {
            var duplicatedAdmin = await userManager.FindByEmailAsync(newAdminViewModel.Email);
            if (duplicatedAdmin == null)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    Email = newAdminViewModel.Email,
                    UserName = newAdminViewModel.Email
                };

                Admin admin = new Admin
                {
                    FirstName = newAdminViewModel.FirstName,
                    LastName = newAdminViewModel.LastName,
                    Email = newAdminViewModel.Email
                };

                
                string hashedPassword = userManager.PasswordHasher.HashPassword(applicationUser, newAdminViewModel.Password);
                applicationUser.PasswordHash = hashedPassword;
                userManager.CreateAsync(applicationUser).Wait();
                userManager.AddToRoleAsync(applicationUser, ApplicationRoles.Admin.ToString()).Wait();
                admin.ApplicationUserID = applicationUser.Id;
                admin.Email = applicationUser.Email;
                databaseContext.Admins.Add(admin as Admin);
                databaseContext.SaveChanges();
                return Ok();
            }

            return Conflict(new
            {
                message = "Admin with this email already exist."
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            var admin = await databaseContext.Admins.FindAsync(id);
            var applicationUser = await userManager.FindByEmailAsync(admin.Email);

            if(admin != null && applicationUser != null)
            {
                databaseContext.Admins.Remove(admin);
                await userManager.DeleteAsync(applicationUser);
                return Ok();
            }

            return Conflict(new
            {
                message = "Error during deleting admin."
            });
        }

    }
}