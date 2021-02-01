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
using TrackMe.Models.DTOs.Admins;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminsBusinessService adminsBusinessService;

        public AdminsController(IAdminsBusinessService adminsBusinessService)
        {
            this.adminsBusinessService = adminsBusinessService;
        }

        // GET: api/admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAdmins()
        {
            var admins = await adminsBusinessService.GetAdmins();
            return Ok(admins);
        }

        // GET: api/admins/4
        [HttpGet("{adminId}")]
        public async Task<ActionResult<AdminDto>> GetAdmin(string adminId)
        {
            var admin = await adminsBusinessService.GetAdmin(adminId);
            return Ok(admin);
        }

        // POST: api/admins
        [HttpPost]
        public async Task<ActionResult> CreateAdmin([FromBody] NewAdminDto newAdminDto)
        {
            await adminsBusinessService.CreateAdmin(newAdminDto);
            return Ok();
        }

        // DELETE: api/admins/4
        [HttpDelete("{adminId}")]
        public async Task<ActionResult> DeleteAdmin(string adminId)
        {
            await adminsBusinessService.DeleteAdmin(adminId);
            return Ok();
        }

    }
}