using ExampleCrud.WebApi.Domain.Entities;

namespace ExampleCrud.WebApi.Application.Interfaces;

public interface IPersonService : IDisposable
{
    Task<bool> HasValidDiferencesToUpdate(Person oldPerson, Person newPerson);
}