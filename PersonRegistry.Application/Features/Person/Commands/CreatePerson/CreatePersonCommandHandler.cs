using AutoMapper;
using MediatR;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Exceptions;

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
        List<Person> personRelations = await this.personRepository.GetByIdsAsync(request.RelatedPersonIds, cancellationToken);
        if (personRelations.Count != request.RelatedPersonIds.Count)
        {
            throw new ApiException("didn't find all related persons");
        }

        person.PersonRelations = personRelations
            .Select(o =>
                new PersonRelation
                {
                    PersonId = person.Id,
                    Person = person,
                    RelatedPersonId = o.Id,
                    RelatedPerson = o
                }).ToList();
        
        await this.personRepository.AddAsync(person, cancellationToken);
    }
}