using FluentValidation;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
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

        When(o => o.Image != null, () =>
        {
            RuleFor(o => o.Image!.Name).NotNull().NotEmpty();
            RuleFor(o => o.Image!.Stream).NotNull().NotEmpty();
            RuleFor(o => o.Image!.Extension).NotNull().NotEmpty();
            RuleFor(o => o.Image!.ContentType).NotNull().NotEmpty();
        });
        
        RuleForEach(o => o.PhoneNumbers)
            .NotNull()
            .NotEmpty()
            .SetValidator(new PhoneNumberDtoValidator());
    }
}

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(o => o.Number)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);

        RuleFor(o => o.Type)
            .IsInEnum();
    }
}