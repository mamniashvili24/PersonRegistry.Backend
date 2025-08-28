using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PersonalNumber { get; set; }
    public required string Image { get; set; }
    public required Gender Gender { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required int CityId { get; set; }
    public required City City { get; set; }
    public ICollection<Person> RelatedPersons { get; set; } = new List<Person>();
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
}