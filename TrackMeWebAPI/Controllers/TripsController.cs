﻿using System;
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
    public class TripsController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public TripsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        // GET api/trips
        [HttpGet]
        [Authorize(Roles = "BasicUser")]
        public async Task<ActionResult<IEnumerable<TripViewModel>>> GetTrips()
        {
            var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
            var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;

            return await this.databaseContext.Trips
                .Where(x => x.BasicUserID == basicUserID)
                .Select(x => new TripViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    BasicUserEmail = this.databaseContext.BasicUsers.SingleOrDefault(y => y.ApplicationUserID == applicationUserID).Email

                })
                .ToListAsync();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<TripViewModel>>> GetAllTrips()
        {
            return await this.databaseContext.Trips
                .Select(x => new TripViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    BasicUserEmail = this.databaseContext.BasicUsers.SingleOrDefault(y => y.ID == x.BasicUserID).Email
                }).ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles ="BasicUser")]
        public async Task<ActionResult> CreateTrip([FromBody] NewTripViewModel newTripViewModel)
        {

            var applicationUserID = User.Claims.First(x => x.Type == "ApplicationUserID").Value;
            var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;

            Trip trip = new Trip
            {
                Name = newTripViewModel.Name
            };

            trip.BasicUserID = basicUserID;
            await this.databaseContext.Trips.AddAsync(trip);
            this.databaseContext.SaveChanges();
            return Ok();
        }

        // GET api/trips/4/details
        [HttpGet("{id}/details")]
        [Authorize(Roles = "BasicUser,Admin")]
        public async Task<ActionResult<IEnumerable<SensorsValuesViewModel>>> GetTripDetails(int id)
        {
            return await this.databaseContext.SensorsValues
                .Where(x => x.TripID == id)
                .Select(x => new SensorsValuesViewModel
                {
                    UploadDate = x.UploadDate.ToLongDateString(),
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    AccelerometerX = x.AccelerometerX,
                    AccelerometerY = x.AccelerometerY,
                    AccelerometerZ = x.AccelerometerZ,
                    GyroscopeX = x.GyroscopeX,
                    GyroscopeY = x.GyroscopeY,
                    GyroscopeZ = x.GyroscopeZ,
                    MagneticFieldX = x.MagneticFieldX,
                    MagneticFieldY = x.MagneticFieldY,
                    MagneticFieldZ = x.MagneticFieldZ

                })
                .ToListAsync();
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