using Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Locations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(l => l.Id)
            .HasName("id");

        builder.Property(l => l.CapturedDate)
            .HasColumnName("latitude")
            .IsRequired();

        builder.Property(l => l.Position)
            .HasColumnName("position")
            .HasColumnType("geography (point)")
            .IsRequired();
    }
}
