using PersonRegistry.Domain.Enums;

namespace PersonRegistry.Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    public required string Number { get; set; }
    public PhoneNumberType Type { get; set; }
    public int PersonId { get; set; }
    public required Person Person { get; set; }
}