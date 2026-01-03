using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHome.Core.Domain.Entities;
using SmartHome.Core.Domain.Enums;

namespace SmartHome.Core.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("Incident");

        builder.HasKey(x => x.IncidentId);

        builder.HasIndex(x => x.EventId).IsUnique(); // 0..1

        builder.HasOne(x => x.Event)
            .WithOne(x => x.Incident)
            .HasForeignKey<Incident>(x => x.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(IncidentStatus.OPEN);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}
