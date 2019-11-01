using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Exceptions;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.Services.Logic;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService tripsService;

        public TripsController(DatabaseContext databaseContext, ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        // GET api/trips
        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult<IEnumerable<TripViewModel>>> GetTrips()
        {
            try
            {
                var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
                var trips = await this.tripsService.GetTrips(applicationUserID);
                return Ok(trips);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }

        // GET api/trips/all
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<TripViewModel>>> GetAllTrips()
        {
            var trips = await tripsService.GetAllTrips();
            return Ok(trips);
        }

        // POST api/trips
        [HttpPost]
        [Authorize(Roles ="BasicUser")]
        public async Task<ActionResult> CreateTrip([FromBody] NewTripViewModel newTripViewModel)
        {
            try
            {
                var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
                await this.tripsService.CreateTrip(applicationUserID, newTripViewModel);
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

        // GET api/trips/4/details
        [HttpGet("{id}/details")]
        [Authorize(Roles = "BasicUser,Admin")]
        public async Task<ActionResult<IEnumerable<SensorsValuesViewModel>>> GetTripDetails(int id)
        {
            var tripDetails = await this.tripsService.GetTripDetails(id);
            return Ok(tripDetails);
        }

        // DELETE api/trips/4
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,BasicUser")]
        public async Task<ActionResult> DeleteTrip(int id)
        {
            try
            {
                await this.tripsService.DeleteTrip(id);
                return Ok();
            }
            catch (TripNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            
        }

        //[HttpPost]
        //[Authorize(Roles = "BasicUser")]
        //public async Task<ActionResult> CreateTripDetails([FromBody] NewTripViewModel newTripViewModel)
        //{

        //    var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
        //    var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;

        //    Trip trip = new Trip
        //    {
        //        Name = newTripViewModel.Name
        //    };

        //    trip.BasicUserID = basicUserID;
        //    await this.databaseContext.Trips.AddAsync(trip);
        //    this.databaseContext.SaveChanges();
        //    return Ok();
        //}

    }




}