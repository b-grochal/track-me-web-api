using Domain.Admins;
using Domain.ApplicationUsers;
using Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ApplicationUsers;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(u => u.Id)
            .HasName("id");
        
        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();
        
        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .IsRequired();

        builder.Property(u => u.Role)
            .HasColumnName("role")
            .IsRequired()
            .HasConversion<string>();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasDiscriminator(u => u.Role)
            .HasValue<Admin>(ApplicationUserRole.Admin)
            .HasValue<Member>(ApplicationUserRole.Member);
    }
}
