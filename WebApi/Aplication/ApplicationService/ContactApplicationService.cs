using AutoMapper;
using ExampleCrud.WebApi.API.DTOs;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;
using ExampleCrud.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using DE = ExampleCrud.WebApi.Domain.Entities;

namespace ExampleCrud.WebApi.Application.ApplicationService;

public class ContactApplicationService : BaseApplicationService<DE.Contact>, IContactApplicationService
{
    public ContactApplicationService
    (
        ContextDb context,
        IMapper mapper
    ) : base(context, mapper)
    { }

    public Contact Add(Contact contact, Guid personId)
    {
        var entity = _mapper.Map<DE.Contact>(contact);
        
        entity.PersonId = personId;
        base.Add(entity);
        _context.SaveChanges();

        return contact;
    }

    public async Task<IEnumerable<Contact>> AddContacts(IEnumerable<Contact> contacts, Guid personId)
    {
        var entities = _mapper.Map<IEnumerable<DE.Contact>>(contacts);

        entities.ToList().ForEach(entity =>
        {
            entity.PersonId = personId;
            entity.PrepareToInsert();
        });

        _dbSet.AddRange(entities);
        await _context.SaveChangesAsync();

        return _mapper.Map<IEnumerable<Contact>>(entities);
    }

    public async Task<IEnumerable<Contact>> GetContactsByPerson(Guid personId)
    {
        var query = BaseQuery().Where(entity => entity.PersonId == personId);
        var result = await query.ToListAsync();
        return _mapper.Map<IEnumerable<Contact>>(result);
    }

    public async Task RemoveContact(Guid contactId)
    {
        var entity = await _dbSet.FindAsync(contactId);
        if (entity.IsNullOrDefault())
            throw new Exception("Contact not found");

        entity.PrepareToDelete();
        await _context.SaveChangesAsync();
    }
}
