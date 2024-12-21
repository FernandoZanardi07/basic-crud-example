using ExampleCrud.WebApi.Domain.Enums;

namespace ExampleCrud.WebApi.API.DTOs;

public record Contact(
    Guid? Id,
    string Number,
    PhoneType Type,
    Guid PersonId
);
