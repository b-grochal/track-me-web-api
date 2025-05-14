using Domain.ApplicationUsers;
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

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasDiscriminator<string>("role")
            .HasValue<RegularUser>("regular_user")
            .HasValue<AdminUser>("admin_user");
    }
}
