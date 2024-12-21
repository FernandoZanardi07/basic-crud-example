using ExampleCrud.WebApi.Domain.Entities;
using ExampleCrud.WebApi.Application.Interfaces;
using ExampleCrud.WebApi.Domain.Services;

namespace WebApi.Domain.Services;

public class PersonService : BaseService, IPersonService
{
    public Task<bool> HasValidDiferencesToUpdate(Person oldPerson, Person newPerson)
    {
        if (oldPerson.CpfNumber != newPerson.CpfNumber)
            throw new Exception("CpfNumber can't be changed");

        var result = true switch
        {
            _ when oldPerson.Name != newPerson.Name => true,
            _ when oldPerson.DateOfBirth != newPerson.DateOfBirth => true,
            _ when oldPerson.IsActive != newPerson.IsActive => true,
            _ => false
        };

        return Task.FromResult(result);
    }
}
