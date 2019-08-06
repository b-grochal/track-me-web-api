using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicUserController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public BasicUserController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        [Route("GetTrips")]
        public async Task<ActionResult<IEnumerable<TripViewModel>>> GetTrips()
        {
            var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
            var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;

            return await this.databaseContext.Trips
                .Where(x => x.BasicUserID == basicUserID)
                .Select(x => new TripViewModel
                {
                    ID = x.ID,
                    Name = x.Name
                })
                .ToListAsync();

        }
    }
}