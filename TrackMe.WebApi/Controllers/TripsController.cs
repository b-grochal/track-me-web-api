using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Models.DTOs.Trips;

namespace TrackMeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsBusinessService tripsBusinessService;

        public TripsController(ITripsBusinessService tripsBusinessService)
        {
            this.tripsBusinessService = tripsBusinessService;
        }

        // GET: api/trips
        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetTrips()
        {
            var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserId").Value;
            var trips = await this.tripsBusinessService.GetTrips(applicationUserID);
            return Ok(trips);
        }

        // GET: api/trips/all
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetAllTrips()
        {
            var trips = await tripsBusinessService.GetTrips();
            return Ok(trips);
        }

        // POST: api/trips
        [HttpPost]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult> CreateTrip([FromBody] NewTripDto newTripDto)
        {
            var applicationUserId = User.Claims.First(x => x.Type == "ApplicationUserId").Value;
            await this.tripsBusinessService.CreateTrip(applicationUserId, newTripDto);
            return Ok();
        }

        // GET: api/trips/4/sensor-data
        [HttpGet("{tripId}/sensor-data")]
        [Authorize(Roles = "BasicUser,Admin")]
        public async Task<ActionResult<TripSensorDataDto>> GetTripSensorData(int tripId)
        {
            var tripSensorData = await this.tripsBusinessService.GetTripSensorDataDto(tripId);
            return Ok(tripSensorData);
        }

        // DELETE: api/trips/4
        [HttpDelete("{tripId}")]
        [Authorize(Roles = "Admin,BasicUser")]
        public async Task<ActionResult> DeleteTrip(int tripId)
        {
            await this.tripsBusinessService.DeleteTrip(tripId);
            return Ok();
        }

        // POST: api/trips/4/details
        [HttpPost("{tripId}/sensor-data")]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult> CreateTripSensorData(int tripId, [FromBody] NewSensorDataDto newSensorDataDto)
        {
            await this.tripsBusinessService.CreateTripSensorData(tripId, newSensorDataDto);
            return Ok();
        }
    }
}