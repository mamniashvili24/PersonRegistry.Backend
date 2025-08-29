using AutoMapper;
using MediatR;
using PersonRegistry.Application.Repositories;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

using Domain.Entities;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand>
{
    private readonly IPersonRepository personRepository;
    private readonly IMapper mapper;
    
    public CreatePersonCommandHandler(
        IPersonRepository personRepository,
        IMapper mapper)
    {
        this.personRepository = personRepository;
        this.mapper = mapper;
    }
    public async Task Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = this.mapper.Map<Person>(request);
        await this.personRepository.AddAsync(person, cancellationToken);
    }
}