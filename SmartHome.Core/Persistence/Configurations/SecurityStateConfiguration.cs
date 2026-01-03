using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Persistence.Configurations;

public class SecurityStateConfiguration : IEntityTypeConfiguration<SecurityState>
{
    public void Configure(EntityTypeBuilder<SecurityState> builder)
    {
        builder.ToTable("SecurityState");

        builder.HasKey(x => x.SecurityStateId);

        builder.HasIndex(x => x.UserId).IsUnique(); // 1:1

        builder.HasOne(x => x.User)
            .WithOne(x => x.SecurityState)
            .HasForeignKey<SecurityState>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Mode)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(SecurityMode.DISARMED);

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}
