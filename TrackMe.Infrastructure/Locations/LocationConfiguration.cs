using Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Locations
{
    internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(l => l.Id)
                .HasName("id");

            builder.Property(l => l.Latitude)
                .HasColumnName("latitude")
                .IsRequired();
            builder.Property(l => l.Longitude)
                .HasColumnName("longitude")
                .IsRequired();
            builder.Property(l => l.Timestamp)
                .HasColumnName("timestamp")
                .IsRequired();
        }
    }
}
