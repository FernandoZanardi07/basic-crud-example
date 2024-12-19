using AuthApi.Domain.Constants;
using ExampleCrud.WebApi.API.DTOs;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCrud.WebApi.API.Controllers;

[Route("contact")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IContactApplicationService _contactAplicationService;

    public ContactController(IContactApplicationService contactAplicationService)
    {
        _contactAplicationService = contactAplicationService;
    }

    [HttpPost("{personId}")]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public ActionResult<object> Post([FromBody] Contact contact, [FromRoute] Guid personId)
    {
        var result = _contactAplicationService.Add(contact, personId);
        return Ok(result);
    }

    [HttpPost("bulk/{personId}")]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public async Task<ActionResult<object>> Post([FromBody] IEnumerable<Contact> contacts, [FromRoute] Guid personId)
    {
        var result = await _contactAplicationService.AddContacts(contacts, personId);
        return Ok(result);
    }

    [HttpGet("person/{personId}")]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public async Task<ActionResult<IEnumerable<Contact>>> GetByPerson([FromRoute] Guid personId)
    {
        var result = await _contactAplicationService.GetContactsByPerson(personId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _contactAplicationService.RemoveContact(id);
        return Ok();
    }
}
