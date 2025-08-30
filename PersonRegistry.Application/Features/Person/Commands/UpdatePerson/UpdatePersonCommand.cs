using AutoMapper;
using MediatR;
using PersonRegistry.Application.Commons.Mappings;
using PersonRegistry.Application.Features.Person.Commands.CreatePerson;
using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePerson;

public class UpdatePersonCommand : IRequest, IMap
{
    public int Id { get; set; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PersonalNumber { get; init; }
    public required Gender Gender { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required int CityId { get; init; }
    public required List<PhoneNumberDto> PhoneNumbers { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePersonCommand, Domain.Entities.Person>();
    }
}