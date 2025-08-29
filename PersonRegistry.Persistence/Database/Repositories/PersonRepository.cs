using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(PersonRegistryDbContext context)
        : base(context)
    {
    }
}