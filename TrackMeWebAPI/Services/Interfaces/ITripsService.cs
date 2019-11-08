using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.ViewModels;

namespace TrackMeWebAPI.Services.Interfaces
{
    public interface ITripsService
    {
        Task<IEnumerable<TripViewModel>> GetAllTrips();

        Task<IEnumerable<TripViewModel>> GetTrips(string applicationUserID);

        Task CreateTrip(string applicationUserID, NewTripViewModel newTripViewModel);

        Task CreateTripDetails(int tripId, SensorsValuesViewModel sensorsValuesViewModel);

        Task<IEnumerable<SensorsValuesViewModel>> GetTripDetails(int tripID);

        Task DeleteTrip(int tripID);
    }
}
