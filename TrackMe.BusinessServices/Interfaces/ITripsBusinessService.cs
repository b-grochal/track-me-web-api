using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackMe.Models.DTOs.Trips;

namespace TrackMe.BusinessServices.Interfaces
{
    public interface ITripsBusinessService
    {
        Task<IEnumerable<TripDto>> GetTrips();
        Task<IEnumerable<TripDto>> GetTrips(string basicUserId);
        Task CreateTrip(NewTripDto newTrip);
        Task CreateTripSensorData(int tripId, NewSensorDataDto newSensorData);
        Task<TripDto> GetTrip(int tripId);
        Task<TripSensorDataDto> GetTripSensorDataDto(int tripId);
        Task DeleteTrip(int tripId);
    }
}
