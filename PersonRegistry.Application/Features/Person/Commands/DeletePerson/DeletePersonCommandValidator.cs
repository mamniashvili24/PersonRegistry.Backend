using FluentValidation;

namespace PersonRegistry.Application.Features.Person.Commands.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0);
    }
}