using FluentValidation;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(o => o.FirstName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(o => o.LastName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(o => o.PersonalNumber)
            .NotNull()
            .NotEmpty();
        
        RuleFor(o => o.Gender)
            .NotNull()
            .NotEmpty();
        
        RuleFor(o => o.DateOfBirth)
            .NotNull()
            .NotEmpty();
        
        RuleFor(o => o.CityId)
            .NotNull()
            .NotEmpty();

        RuleFor(o => o.PhoneNumbers)
            .NotNull()
            .NotEmpty();
    }
}