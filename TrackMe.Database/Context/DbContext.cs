using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackMeWebAPI.Models;

namespace TrackMeWebAPI.DAL
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BasicUser> BasicUsers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<SensorsValues> SensorsValues { get; set; }
        


        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
