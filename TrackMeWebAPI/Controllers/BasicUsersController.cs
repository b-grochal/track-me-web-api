using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicUsersController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public BasicUsersController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        // GET api/basicUsers/all
        [HttpGet]
        [Authorize(Roles="Admin")]
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
            var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;
            var basicUser = await this.databaseContext.BasicUsers.FindAsync(basicUserID);

            return new BasicUserViewModel
            {
                ID = basicUser.ID,
                FirstName = basicUser.FirstName,
                LastName = basicUser.LastName,
                Email = basicUser.Email,
                PhoneNumber = basicUser.PhoneNumber
            };
        }
    }
}