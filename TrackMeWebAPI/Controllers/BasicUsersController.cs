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
    public class BasicUsersController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<ApplicationUser> userManager;

        public BasicUsersController(DatabaseContext databaseContext, UserManager<ApplicationUser> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;
        }

        // GET api/basicUsers/all
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<BasicUserViewModel>>> GetAllBasicUsers()
        {
            return await this.databaseContext.BasicUsers
                .Select(x => new BasicUserViewModel
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync();

        }

        // GET api/basicUsers/4
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BasicUserViewModel>> GetBasicUserDetails(int id)
        {
            var basicUser = await this.databaseContext.BasicUsers.FindAsync(id);

            return new BasicUserViewModel
            {
                ID = basicUser.ID,
                FirstName = basicUser.FirstName,
                LastName = basicUser.LastName,
                Email = basicUser.Email,
                PhoneNumber = basicUser.PhoneNumber
            };
        }

        // GET api/basicUsers
        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult<BasicUserViewModel>> GetBasicUserAccountDetails()
        {
            var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
            var basicUser = await this.databaseContext.BasicUsers.SingleOrDefaultAsync(x => x.ApplicationUserID.Equals(applicationUserID));
            
            return new BasicUserViewModel
            {
                ID = basicUser.ID,
                FirstName = basicUser.FirstName,
                LastName = basicUser.LastName,
                Email = basicUser.Email,
                PhoneNumber = basicUser.PhoneNumber
            };
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> DeleteBasicUser(int id)
        {
            var basicUser = await databaseContext.BasicUsers.FindAsync(id);
            var applicationUser = await userManager.FindByEmailAsync(basicUser.Email);

            if (basicUser != null && applicationUser != null)
            {
                databaseContext.BasicUsers.Remove(basicUser);
                await userManager.DeleteAsync(applicationUser);
                databaseContext.SaveChanges();
                return Ok();
            }

            return Conflict(new
            {
                message = "Error during deleting basic user."
            });
        }

        [HttpPut]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult> UpdateBasicUser([FromBody] UpdatedBasicUserViewModel updatedBasicUser)
        {
            
            var oldBasicUser = await databaseContext.BasicUsers.FindAsync(updatedBasicUser.ID);
            var applicationUser = await userManager.FindByEmailAsync(oldBasicUser.Email);
            applicationUser.Email = updatedBasicUser.Email;
            applicationUser.UserName = updatedBasicUser.Email;
            oldBasicUser.Email = updatedBasicUser.Email;
            oldBasicUser.FirstName = updatedBasicUser.FirstName;
            oldBasicUser.LastName = updatedBasicUser.LastName;
            oldBasicUser.PhoneNumber = updatedBasicUser.PhoneNumber;
            await userManager.UpdateAsync(applicationUser);
            databaseContext.BasicUsers.Update(oldBasicUser);
            databaseContext.SaveChanges();
            return Ok();
        }


    }
}