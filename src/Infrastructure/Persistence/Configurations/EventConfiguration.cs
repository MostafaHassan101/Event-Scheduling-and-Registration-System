using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description);
        builder.OwnsOne(e => e.Date, d =>
        {
            d.Property(p => p.Value).HasColumnName("Date");
        });
        builder.OwnsOne(e => e.Time, t =>
        {
            t.Property(p => p.Value).HasColumnName("Time");
        });
        builder.OwnsOne(e => e.Location, l =>
        {
            l.Property(p => p.Value).HasColumnName("Location");
        });

        builder.HasMany(e => e.Participants)
               .WithMany(u => u.Events)
               .UsingEntity(j => j.ToTable("EventParticipants"));
    }
}