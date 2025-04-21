using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Locations;
using Domain.Trips;

namespace TrackMe.Services.Interfaces
{
    public interface ITripsService
    {
        Task<IEnumerable<Trip>> GetTrips();
        Task<IEnumerable<Trip>> GetTrips(string basicUserId);
        Task CreateTrip(Trip newTrip);
        Task CreateTripSensorData(Location newSensorData);
        Task<Trip> GetTrip(int tripId);
        Task DeleteTrip(int tripId);
    }
}
