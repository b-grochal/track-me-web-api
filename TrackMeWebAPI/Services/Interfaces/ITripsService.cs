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
    }
}
