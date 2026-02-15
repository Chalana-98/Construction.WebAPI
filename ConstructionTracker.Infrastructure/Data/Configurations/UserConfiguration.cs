using ConstructionTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for the User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .ValueGeneratedNever();
        
        builder.Property(u => u.TenantId)
            .IsRequired();
        
        builder.HasIndex(u => u.TenantId);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        // Unique constraint: Email must be unique within a tenant
        builder.HasIndex(u => new { u.TenantId, u.Email })
            .IsUnique();
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);
        
        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Viewer");
        
        builder.Property(u => u.JobTitle)
            .HasMaxLength(100);
        
        builder.Property(u => u.IsActive)
            .HasDefaultValue(true);
        
        builder.Property(u => u.EmailVerified)
            .HasDefaultValue(false);
        
        builder.Property(u => u.RefreshToken)
            .HasMaxLength(500);
        
        builder.Property(u => u.CreatedAt)
            .IsRequired();
        
        // Ignore computed property
        builder.Ignore(u => u.FullName);
    }
}
