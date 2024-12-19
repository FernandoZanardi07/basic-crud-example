using AuthApi.Domain.Constants;
using ExampleCrud.WebApi.API.DTOs;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCrud.WebApi.API.Controllers;

[Route("person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonApplicationService _personApplicationService;

    public PersonController(IPersonApplicationService personApplicationService)
    {
        _personApplicationService = personApplicationService;
    }

    [HttpGet("{id}")]
    public ActionResult<object> Get([FromRoute] Guid id)
    {
        var person = _personApplicationService.ById(id);
        return Ok(person);
    }

    [HttpGet]
    public ActionResult<object> Get()
    {
        var persons = _personApplicationService.GetAll();
        return Ok(persons);
    }

    [HttpPost]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public ActionResult<object> Post([FromBody] Person person)
    {
        var result = _personApplicationService.Add(person);
        return Ok(result);
    }

    [HttpPut()]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public ActionResult<object> Put([FromBody] Person person)
    {
        var result = _personApplicationService.Update(person);
        return Ok(result);
    }

    /// <summary>
    /// Default value for AuthenticationScheme property in the <see cref="BearerTokenOptions"/>.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = AppConstants.RoleAdmin)]
    public ActionResult Delete(Guid id)
    {
        _personApplicationService.Delete(id);
        return Ok();
    }
}
