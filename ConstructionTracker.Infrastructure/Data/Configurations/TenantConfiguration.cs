using ConstructionTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructionTracker.Infrastructure.Data.Configurations;

/// <summary>
/// Entity configuration for the Tenant entity.
/// </summary>
public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .ValueGeneratedNever();
        
        builder.Property(t => t.CompanyName)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(t => t.Subdomain)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(t => t.Subdomain)
            .IsUnique();
        
        builder.Property(t => t.ContactEmail)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(t => t.ContactPhone)
            .HasMaxLength(20);
        
        builder.Property(t => t.Address)
            .HasMaxLength(500);
        
        builder.Property(t => t.SubscriptionPlan)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("free");
        
        builder.Property(t => t.IsActive)
            .HasDefaultValue(true);
        
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        
        // Relationship: Tenant has many Users
        builder.HasMany(t => t.Users)
            .WithOne(u => u.Tenant)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
