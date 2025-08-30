using AutoMapper;
using MediatR;
using PersonRegistry.Application.FileStorage;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Exceptions;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

using Domain.Entities;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand>
{
    private readonly IPersonRepository personRepository;
    private readonly IFileStorage fileStorage;
    private readonly IMapper mapper;
    
    public CreatePersonCommandHandler(
        IPersonRepository personRepository,
        IFileStorage fileStorage,
        IMapper mapper)
    {
        this.personRepository = personRepository;
        this.fileStorage = fileStorage;
        this.mapper = mapper;
    }
    public async Task Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = this.mapper.Map<Person>(request);
        
        person.Image = await UploadAndGetImageUrlAsync(request, cancellationToken);
        person.PersonRelations = await BuildPersonRelationsAsync(person, request.RelatedPersonIds, cancellationToken);
        
        await this.personRepository.AddAsync(person, cancellationToken);
    }

    private async Task<string> UploadAndGetImageUrlAsync(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        if (request.Image == null)
        {
            return null;
        }
        
        await this.fileStorage.UploadAsync(request.Image, cancellationToken);
        string imageUrl = this.fileStorage.GetUrl(request.Image.Name);
        
        return imageUrl;
    }

    private async Task<List<PersonRelation>> BuildPersonRelationsAsync(
        Person person,
        List<int> relatedPersonIds,
        CancellationToken cancellationToken)
    {
        var relatedPersons = await this.personRepository.GetByIdsAsync(relatedPersonIds, cancellationToken);

        if (relatedPersons.Count != relatedPersonIds.Count)
        {
            throw new ApiException("Not all related persons were found.");
        }

        return relatedPersons
            .Select(p => new PersonRelation
            {
                Person = person,
                PersonId = person.Id,
                RelatedPerson = p,
                RelatedPersonId = p.Id
            }).ToList();
    }
}