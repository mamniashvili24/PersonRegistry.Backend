using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Entities;
using PersonRegistry.Persistence.Database.Configurations;

namespace PersonRegistry.Persistence.Database;

public class PersonRegistryDbContext : DbContext
{
    public PersonRegistryDbContext(DbContextOptions<PersonRegistryDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<City> Cities { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
    }
}