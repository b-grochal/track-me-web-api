using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
using TrackMeWebAPI.Models;
using TrackMeWebAPI.Services.Interfaces;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Logic
{
    public class TripsService : ITripsService
    {
        private readonly DatabaseContext databaseContext;
        public TripsService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<TripViewModel>> GetAllTrips()
        {
            return await this.databaseContext.Trips
                .Select(x => new TripViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    BasicUserEmail = this.databaseContext.BasicUsers.SingleOrDefault(y => y.ID == x.BasicUserID).Email
                }).ToListAsync();
        }

        public async Task<IEnumerable<TripViewModel>> GetTrips(string applicationUserID)
        {
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

        public async Task CreateTrip(string applicationUserID, NewTripViewModel newTripViewModel)
        {
            var basicUserID = this.databaseContext.BasicUsers.SingleOrDefault(x => x.ApplicationUserID.Equals(applicationUserID)).ID;

            Trip trip = new Trip
            {
                Name = newTripViewModel.Name
            };

            trip.BasicUserID = basicUserID;
            await this.databaseContext.Trips.AddAsync(trip);
            this.databaseContext.SaveChanges();
        }

        public async Task<IEnumerable<SensorsValuesViewModel>> GetTripDetails(int tripId)
        {
            return await this.databaseContext.SensorsValues
                .Where(x => x.TripID == tripId)
                .Select(x => new SensorsValuesViewModel
                {
                    UploadDate = x.UploadDate.ToShortDateString() + " " + x.UploadDate.ToShortTimeString(),
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

        public async Task DeleteTrip(int tripID)
        {
            var trip = await databaseContext.Trips.FindAsync(tripID);
            databaseContext.Trips.Remove(trip);
            databaseContext.SaveChanges();
        }
    }
}
