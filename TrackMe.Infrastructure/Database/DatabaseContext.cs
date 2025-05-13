using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace TrackMe.Database.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Member> BasicUsers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Location> SensorData { get; set; }

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
            .HasValue<Member>(ApplicationUserRoles.BasicUser.ToString());

            builder.Entity<Trip>().HasOne(o => o.Member)
                .WithMany(a => a.Trips)
                .HasForeignKey(o => o.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
