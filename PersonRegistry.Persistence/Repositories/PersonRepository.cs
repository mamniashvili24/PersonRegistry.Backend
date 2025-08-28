using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}