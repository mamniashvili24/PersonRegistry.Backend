using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.Property(o => o.Number)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(o => o.PhoneNumberType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(pn => pn.PersonId)
            .IsRequired();

        builder.HasOne(o => o.Person)
            .WithMany(o => o.PhoneNumbers)
            .HasForeignKey(o => o.PersonId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}