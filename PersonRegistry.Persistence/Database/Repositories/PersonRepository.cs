using Microsoft.EntityFrameworkCore;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Database.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(PersonRegistryDbContext context)
        : base(context)
    {
    }

    public async Task<List<Person>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken)
    {
        return await this.dbSet
            .Where(person => ids.Contains(person.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<Person> FirstAsync(int id, CancellationToken cancellationToken)
    {
        return await this.dbSet
            .FirstAsync(person => person.Id == id, cancellationToken);
    }
}