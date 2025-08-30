using AutoMapper;
using MediatR;
using PersonRegistry.Application.Repositories;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePerson;

using Domain.Entities;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository personRepository;
    private readonly IMapper mapper;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IMapper mapper)
    {
        this.personRepository = personRepository;
        this.mapper = mapper;
    }
    
    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = await this.personRepository.FirstAsync(request.Id, cancellationToken);
        
        this.mapper.Map(request, person);

        await this.personRepository.UpdateAsync(person, false, cancellationToken);
    }
}