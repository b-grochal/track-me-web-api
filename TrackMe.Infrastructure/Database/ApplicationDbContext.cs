using Domain.Admins;
using Domain.ApplicationUsers;
using Domain.Locations;
using Domain.Members;
using Domain.Trips;
using Microsoft.EntityFrameworkCore;

namespace TrackMe.Database.Context;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Admin> Admins { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<Trip> Trips { get; set; }

    public DbSet<Location> SensorData { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //modelBuilder.HasDefaultSchema(Schemas.Default);

        //base.OnModelCreating(modelBuilder);
    }
}
