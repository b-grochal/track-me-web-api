using Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Locations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.Property(l => l.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(l => l.CapturedDate)
            .HasColumnName("captured_date")
            .IsRequired();

        builder.Property(l => l.Position)
            .HasColumnName("position")
            .HasColumnType("geography (point)")
            .IsRequired();

        builder.Property(l => l.TripId)
            .HasColumnName("trip_id")
            .IsRequired();

        builder.HasKey(l => l.Id)
            .HasName("pk_location");

        builder.HasOne(l => l.Trip)
            .WithMany(t => t.Locations)
            .HasForeignKey(l => l.TripId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_location_trip");

    }
}
