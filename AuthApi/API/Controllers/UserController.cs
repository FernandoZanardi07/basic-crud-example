using AuthApi.API.DTOs;
using AuthApi.Application.InterfaceApplicationService;
using AuthApi.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Controllers;

[ApiController]
[Route("user")]
public class AuthController : ControllerBase
{
    private readonly IUserApplicationService _userApplicationService;

    public AuthController(IUserApplicationService userService)
    {
        _userApplicationService = userService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromBody] UserLogin userLogin)
    {
        var tolken = await _userApplicationService.ValidateAndGetTolken(userLogin);

        return Ok(
            new
            {
                Token = tolken,
                Auth = tolken != string.Empty
            }
        );
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] UserLogin userLogin)
    {
        await _userApplicationService.CreateUser(userLogin);
        return Ok();
    }
}
