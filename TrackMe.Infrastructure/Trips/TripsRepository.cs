using Application.Common.Data;
using Domain.Trips;
using Infrastructure.Common;
using TrackMe.Database.Context;

namespace Infrastructure.Trips;

internal class TripsRepository : Repository<Trip>, ITripsRepository
{
    public TripsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
