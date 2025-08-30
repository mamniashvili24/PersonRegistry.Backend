using AutoMapper;
using MediatR;
using PersonRegistry.Application.FileStorage;
using PersonRegistry.Application.Repositories;

namespace PersonRegistry.Application.Features.Person.Commands.DeletePerson;

using Domain.Entities;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository personRepository;
    private readonly IFileStorage fileStorage;
    
    public DeletePersonCommandHandler(
        IPersonRepository personRepository,
        IFileStorage fileStorage)
    {
        this.personRepository = personRepository;
        this.fileStorage = fileStorage;
    }
    
    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = await this.personRepository.FirstAsync(request.Id, cancellationToken);

        if (person.Image != null)
        {
            await this.fileStorage.DeleteAsync(person.Image, cancellationToken);
        }
        
        await this.personRepository.DeleteAsync(person, cancellationToken);
    }
}