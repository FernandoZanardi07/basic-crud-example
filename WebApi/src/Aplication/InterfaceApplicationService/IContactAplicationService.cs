using ExampleCrud.WebApi.API.DTOs;

namespace ExampleCrud.WebApi.Application.InterfaceApplicationService;

public interface IContactApplicationService
{
    Contact Add(Contact contact, Guid personId);
    Task<IEnumerable<Contact>> AddContacts(IEnumerable<Contact> contacts, Guid personId);
    Task<IEnumerable<Contact>> GetContactsByPerson(Guid personId);
    Task RemoveContact(Guid contactId);
}
