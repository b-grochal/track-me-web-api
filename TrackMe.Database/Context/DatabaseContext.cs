using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMe.Domain.Entities;


namespace TrackMe.Database.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BasicUser> BasicUsers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
            .ToTable("ApplicationUsers")
            .HasDiscriminator<string>("ApplicationUserType")
            .HasValue<Admin>(ApplicationUserRoles.Admin.ToString())
            .HasValue<BasicUser>(ApplicationUserRoles.BasicUser.ToString());

            builder.Entity<Trip>().HasOne(o => o.BassicUser)
                .WithMany(a => a.Trips)
                .HasForeignKey(o => o.BasicUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
