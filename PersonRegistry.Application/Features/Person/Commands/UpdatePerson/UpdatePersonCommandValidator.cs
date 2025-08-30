using FluentValidation;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        
        RuleFor(o => o.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        
        RuleFor(o => o.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(o => o.PersonalNumber)
            .NotNull()
            .NotEmpty()
            .Length(11);

        RuleFor(o => o.Gender)
            .IsInEnum();
        
        RuleFor(o => o.DateOfBirth)
            .NotNull()
            .NotEmpty()
            .Must(dob => dob <= DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18))
            .WithMessage("Person must be at least 18 years old.");


        RuleFor(o => o.CityId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(o => o.PhoneNumbers)
            .NotNull()
            .NotEmpty();
    }
}