using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Models.DTOs.BasicUsers;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/basic-users")]
    [ApiController]
    public class BasicUsersController : ControllerBase
    {
        private readonly IBasicUsersBusinessService basicUsersBusinessService;

        public BasicUsersController(IBasicUsersBusinessService basicUsersBusinessService)
        {
            this.basicUsersBusinessService = basicUsersBusinessService;
        }

        // GET: api/basicUsers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BasicUserDto>>> GetBasicUsers()
        {
            var basicUsers = await basicUsersBusinessService.GetBasicUsers();
            return Ok(basicUsers);

        }

        // GET: api/basicUsers/4
        [HttpGet("{basicUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BasicUserDto>> GetBasicUser(string basicUserId)
        {
            var basicUser = await basicUsersBusinessService.GetBasicUser(basicUserId);
            return Ok(basicUser);
        }

        // DELETE: api/basicUsers/4
        [HttpDelete("{basicUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBasicUser(string basicUserId)
        {
            await basicUsersBusinessService.DeleteBasicUser(basicUserId);
            return Ok();
        }
    }
}