using AutoMapper;
using ExampleCrud.WebApi.API.DTOs;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;
using ExampleCrud.WebApi.Application.Interfaces;
using ExampleCrud.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DE = ExampleCrud.WebApi.Domain.Entities;

namespace ExampleCrud.WebApi.Application.ApplicationService;

public class PersonApplicationService : BaseApplicationService<DE.Person>, IPersonApplicationService
{
    private readonly IPersonService _personService;
    private readonly IContactApplicationService _ContactApplicationService;

    public PersonApplicationService
    (
        IPersonService personService,
        IContactApplicationService contactService,
        ContextDb context,
        IMapper mapper
    ) : base(context, mapper)
    {
        _personService = personService;
        _ContactApplicationService = contactService;
    }

    public DE.Person? GetById(Guid id, bool include = false)
    {
        var query = BaseQuery().Where(entity => entity.Id == id);

        if (include)
            query = query.Include(entity => entity.Contacts);

        return query.FirstOrDefault();
    }

    public Person Add(Person person)
    {
        if (BaseQuery().Any(entity => entity.CpfNumber == person.CpfNumber))
            throw new Exception("Person already exists");

        var entity = _mapper.Map<DE.Person>(person);

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            base.Add(entity);
            _context.SaveChanges();

            if (!entity.Contacts.IsNullOrEmpty())
                _ContactApplicationService.AddContacts(person.Contacts.ToList(), entity.Id);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Log.Error($"Error adding person: {ex}");
            throw;
        }

        return _mapper.Map<Person>(base.Add(entity));
    }

    public bool Delete(Guid id)
    {
        var person = GetById(id);

        if (person.IsNullOrDefault())
            throw new Exception("Person not found");

        person.PrepareToDelete();
        base.Update(person);

        return true;
    }

    public Person ById(Guid id)
    {
        var person = GetById(id, true);

        if (person.IsNullOrDefault())
            throw new Exception("Person not found");

        return _mapper.Map<Person>(person);
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        var entities = BaseQuery().ToList();

        return _mapper.Map<IEnumerable<Person>>(entities);
    }

    public async Task<Person> Update(Person person)
    {
        if (person.Id.IsNullOrDefault() || person.IsNullOrDefault())
            throw new Exception("Invalid person data");

        var originalEntity = GetById(person.Id.GetValueOrDefault());
        if (originalEntity.IsNullOrDefault())
            throw new Exception("Person not found");

        var newEntity = _mapper.Map<DE.Person>(person);

        if (await _personService.HasValidDiferencesToUpdate(originalEntity, newEntity))
        {
            base.Update(newEntity);

            if (!newEntity.Contacts.IsNullOrDefault())
                await _ContactApplicationService.AddContacts(person.Contacts.ToList(), newEntity.Id);

            _context.SaveChanges();
        }

        return person;
    }

    protected override void Dispose()
    {
        base.Dispose();

        _personService?.Dispose();

        GC.SuppressFinalize(this);
    }
}