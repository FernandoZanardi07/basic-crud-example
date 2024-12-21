using ExampleCrud.WebApi.API.DTOs;

namespace ExampleCrud.WebApi.Application.InterfaceApplicationService;

public interface IPersonApplicationService
{
    Task<IEnumerable<Person>> GetAll();
    Person ById(Guid id);
    Person Add(Person person);
    Task<Person> Update(Person person);
    bool Delete(Guid id);
}
