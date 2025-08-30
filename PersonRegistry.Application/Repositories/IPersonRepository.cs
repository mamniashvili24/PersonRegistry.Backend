using System.Security.AccessControl;
using PersonRegistry.Domain.Entities;

namespace PersonRegistry.Application.Repositories;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<List<Person>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task<Person> FirstAsync(int id, CancellationToken cancellationToken);
}