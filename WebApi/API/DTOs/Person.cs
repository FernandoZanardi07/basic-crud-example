namespace ExampleCrud.WebApi.API.DTOs;

public record Person(
    Guid? Id,
    string Name,
    long CpfNumber,
    DateTime DateOfBirth,
    bool IsActive,
    IEnumerable<Contact>? Contacts
);