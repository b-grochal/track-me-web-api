using Domain.Admins;
using Domain.ApplicationUsers;
using Domain.Locations;
using Domain.Members;
using Domain.Trips;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Data;

public interface IApplicationDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<Trip> Trips { get; set; }

    public DbSet<Location> SensorData { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
