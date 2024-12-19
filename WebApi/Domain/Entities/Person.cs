namespace ExampleCrud.WebApi.Domain.Entities;

public class Person : BaseEntity
{
    public string? Name { get; set; }
    public long CpfNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }

    public IEnumerable<Contact>? Contacts { get; set; }
}
