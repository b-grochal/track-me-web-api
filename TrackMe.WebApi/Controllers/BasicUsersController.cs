using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicUsersController : ControllerBase
    {
        private readonly IBasicUsersService basicUsersService;

        public BasicUsersController(IBasicUsersService basicUsersService)
        {
            this.basicUsersService = basicUsersService;
        }

        // GET api/basicUsers/all
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<BasicUserViewModel>>> GetAllBasicUsers()
        {
            var basicUsers = await basicUsersService.GetAllBasicUsers();
            return Ok(basicUsers);

        }

        // GET api/basicUsers/4
        [HttpGet("{basicUserId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BasicUserViewModel>> GetBasicUserDetails(int basicUserId)
        {
            try
            {
                var basicUser = await basicUsersService.GetBasicUserDetails(basicUserId);
                return Ok(basicUser);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        // GET api/basicUsers
        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult<BasicUserViewModel>> GetBasicUserAccountDetails()
        {
            try
            {
                var applicationUserId = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
                var basicUser = await basicUsersService.GetBasicUserAccountDetails(applicationUserId);
                return Ok(basicUser);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }

        // DELETE api/basicUsers/4
        [HttpDelete("{basicUserId}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> DeleteBasicUser(int basicUserId)
        {
            try
            {
                await basicUsersService.DeleteBasicUser(basicUserId);
                return Ok();
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }

        // PUT api/basicUsers
        [HttpPut]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult> UpdateBasicUser([FromBody] UpdatedBasicUserViewModel updatedBasicUser)
        {
            try
            {
                await basicUsersService.UpdateBasicUser(updatedBasicUser);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }


    }
}