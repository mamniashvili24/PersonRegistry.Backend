using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(250);
    }
}