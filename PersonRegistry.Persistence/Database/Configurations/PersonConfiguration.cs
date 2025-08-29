using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.PersonalNumber)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();
        
        builder.Property(p => p.Image)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(p => p.Gender)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(p => p.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");
        
        builder.Property(p => p.CityId)
            .IsRequired();

        builder.HasOne(p => p.City)
            .WithMany()
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(p => p.PhoneNumbers)
            .WithOne(pn => pn.Person)
            .HasForeignKey(pn => pn.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(p => p.PersonRelations)
            .WithOne(pr => pr.Person)
            .HasForeignKey(pr => pr.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(p => p.RelatedToPersons)
            .WithOne(pr => pr.RelatedPerson)
            .HasForeignKey(pr => pr.RelatedPersonId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Ignore(p => p.AllRelations);
    }
}