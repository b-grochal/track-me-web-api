using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.DAL;
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
    }
}
