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

        // GET api/basicusers
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<IEnumerable<BasicUserViewModel>>> GetBasicUsers()
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
    }
}