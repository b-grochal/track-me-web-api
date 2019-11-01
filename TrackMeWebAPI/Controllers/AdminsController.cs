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
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsService adminsService;

        public AdminsController(IAdminsService adminsService)
        {
            this.adminsService = adminsService;
        }

        // GET api/admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminViewModel>>> GetAdmins()
        {
            var admins = await adminsService.GetAdmins();
            return Ok(admins);
        }

        // GET api/admins/4
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminViewModel>> GetAdminDetails(int id)
        {
            try
            {
                var admin = await adminsService.GetAdminDetails(id);
                return Ok(admin);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }

        // POST api/admins
        [HttpPost]
        public async Task<ActionResult> CreateAdmin([FromBody] NewAdminViewModel newAdminViewModel)
        {
            try
            {
                await adminsService.CreateAdmin(newAdminViewModel);
                return Ok();
            }
            catch (DuplicatedUserException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            try
            {
                await adminsService.DeleteAdmin(id);
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