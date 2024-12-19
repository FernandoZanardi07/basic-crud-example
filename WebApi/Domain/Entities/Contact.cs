using ExampleCrud.WebApi.Domain.Enums;

namespace ExampleCrud.WebApi.Domain.Entities;

public class Contact : BaseEntity
{
    public required string Number { get; set; }
    public PhoneType Type { get; set; } 

    public Guid PersonId { get; set; } 
}
