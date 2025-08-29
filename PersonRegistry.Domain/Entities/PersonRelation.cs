namespace PersonRegistry.Domain.Entities;

public class PersonRelation
{
    public int Id { get; set; }
    public required int PersonId { get; set; }
    public required Person Person { get; set; }
    public required int RelatedPersonId { get; set; }
    public required Person RelatedPerson { get; set; }
}