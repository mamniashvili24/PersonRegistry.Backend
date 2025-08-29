using AutoMapper;
using MediatR;
using PersonRegistry.Application.Commons.Mappings;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

public class CreatePersonCommand : IRequest, IMap
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PersonalNumber { get; set; }
    public required Gender Gender { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required int CityId { get; set; }
    public required List<PhoneNumber> PhoneNumbers { get; set; }
    // public IFormFile Image { get; set; }
    public List<int> RelatedPersonIds { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePersonCommand, Domain.Entities.Person>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => string.Empty));
        
        profile.CreateMap<PhoneNumber, Domain.Entities.PhoneNumber>();
    }
}

public class PhoneNumber
{
    public required string Number { get; set; }
    public required PhoneNumberType PhoneNumberType { get; set; }
}