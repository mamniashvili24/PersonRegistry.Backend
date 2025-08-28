using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Application.Repositories;

public interface IPersonRepository
{
    Task AddAsync(Person person, CancellationToken cancellationToken);
}