using AuthApi.API.DTOs;
using AuthApi.Application.InterfaceApplicationService;
using DE = AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AuthApi.Application;

public class UserApplicationService : BaseApplicationService<DE.User>, IUserApplicationService
{
    private readonly IUserService _userService;

    public UserApplicationService
    (
        IUserService userService,
        ContextDb context,
        IMapper mapper
    ) : base(context, mapper)
    {
        _userService = userService;
    }
    public async Task<string> ValidateAndGetTolken(UserLogin userLogin)
    {
        var tokenString = string.Empty;

        var user = await BaseQuery().FirstOrDefaultAsync(user => user.Username == userLogin.Username && user.Password == userLogin.Password);
        if (user != null)
            tokenString = await _userService.GenerateToken(user);

        return tokenString;
    }
}
