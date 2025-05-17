using Domain.Trips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Trips;

internal sealed class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("trips");

        builder.Property(l => l.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(l => l.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(l => l.MemberId)
            .HasColumnName("member_id")
            .IsRequired();

        builder.HasKey(t => t.Id)
            .HasName("pk_trip");

        builder.HasOne(t => t.Member)
            .WithMany(t => t.Trips)
            .HasForeignKey(t => t.MemberId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_trip_member");
    }
}
