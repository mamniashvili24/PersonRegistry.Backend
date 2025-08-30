using AutoMapper;
using MediatR;
using PersonRegistry.Application.FileStorage;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Domain.Models;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePerson;

using Domain.Entities;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository personRepository;
    private readonly IFileStorage fileStorage;
    private readonly IMapper mapper;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IFileStorage fileStorage,
        IMapper mapper)
    {
        this.personRepository = personRepository;
        this.fileStorage = fileStorage;
        this.mapper = mapper;
    }
    
    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = await this.personRepository.FirstAsync(request.Id, cancellationToken);
        
        await MapRequestToPersonAsync(request, person, cancellationToken);

        await this.personRepository.UpdateAsync(person, false, cancellationToken);
    }

    private async Task MapRequestToPersonAsync(UpdatePersonCommand request, Person person, CancellationToken cancellationToken)
    {
        person.PhoneNumbers.Clear();
        this.mapper.Map(request, person);
        
        if (request.Image != null)
        {
            person.Image = await UploadAndGetImageUrlAsync(request.Image, cancellationToken);
        }
    }

    private async Task<string> UploadAndGetImageUrlAsync(FileContainer image, CancellationToken cancellationToken)
    {
        await this.fileStorage.UploadAsync(image, cancellationToken);
        string imageUrl = this.fileStorage.GetUrl(image.Name);
        
        return imageUrl;
    }
}