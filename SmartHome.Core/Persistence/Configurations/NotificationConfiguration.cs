using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notification");

        builder.HasKey(x => x.NotificationId);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(NotificationType.INCIDENT);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Incident)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.IncidentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
