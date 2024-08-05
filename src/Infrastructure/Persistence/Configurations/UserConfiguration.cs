using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired();

        builder.OwnsOne(e => e.ContactInformation, d =>
        {
            d.Property(p => p.Value).HasColumnName("ContactInformation");
            d.WithOwner();
        });
    }
}
