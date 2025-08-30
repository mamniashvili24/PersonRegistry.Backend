using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using PersonRegistry.Application.Commons.Mappings;
using PersonRegistry.Domain.Entities;
using PersonRegistry.Domain.Enums;
using PersonRegistry.Domain.Models;

namespace PersonRegistry.Application.Features.Person.Commands.CreatePerson;

public class CreatePersonCommand : IRequest, IMap
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PersonalNumber { get; init; }
    public required Gender Gender { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required int CityId { get; init; }
    public required List<PhoneNumberDto> PhoneNumbers { get; init; }
    [JsonIgnore]
    public FileContainer? Image { get; set; }

    public List<int> RelatedPersonIds { get; init; } = new();
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePersonCommand, Domain.Entities.Person>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
    }
}

public class PhoneNumberDto : IMap
{
    public required string Number { get; set; }
    public required PhoneNumberType Type { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PhoneNumberDto, PhoneNumber>();
    }
}