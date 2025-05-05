using Domain.Locations;
using Domain.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Database.Context;
using TrackMe.Services.Exceptions;
using TrackMe.Services.Interfaces;

namespace TrackMe.Services.Logic
{
    public class TripsService : ITripsService
    {
        private readonly DatabaseContext databaseContext;

        public TripsService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task CreateTrip(Trip newTrip)
        {
            await databaseContext.Trips.AddAsync(newTrip);
            await databaseContext.SaveChangesAsync();
        }

        public async Task CreateTripSensorData(Location newSensorData)
        {
            await databaseContext.SensorData.AddAsync(newSensorData);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteTrip(int tripId)
        {
            var trip = await databaseContext.Trips.FindAsync(tripId);
            if(trip == null)
            {
                throw new TripNotFoundException("Cannot find trip with passed Id.");
            }
            databaseContext.Trips.Remove(trip);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Trip>> GetTrips()
        {
            return await databaseContext.Trips
                .ToListAsync();
        }

        public async Task<Trip> GetTrip(int tripId)
        {
            return await databaseContext.Trips
                .FindAsync(tripId);
        }

        public async Task<IEnumerable<Trip>> GetTrips(string basicUserId)
        {
            return await databaseContext.Trips
                .Where(t => t.MemberId == basicUserId)
                .ToListAsync();
        }
    }
}
