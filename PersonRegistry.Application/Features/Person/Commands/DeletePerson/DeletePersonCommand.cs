using MediatR;

namespace PersonRegistry.Application.Features.Person.Commands.DeletePerson;

public class DeletePersonCommand : IRequest
{
    public required int Id { get; set; }
}