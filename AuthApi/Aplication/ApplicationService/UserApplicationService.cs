using AuthApi.API.DTOs;
using AuthApi.Application.InterfaceApplicationService;
using DE = AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StackExchange.Redis;
using AuthApi.Domain.Constants;

namespace AuthApi.Application;

public class UserApplicationService : BaseApplicationService<DE.User>, IUserApplicationService
{
    private readonly IUserService _userService;
    private readonly IConnectionMultiplexer _redis;

    public UserApplicationService
    (
        IUserService userService,
        ContextDb context,
        IMapper mapper,
        IConnectionMultiplexer redis
    ) : base(context, mapper)
    {
        _userService = userService;
        _redis = redis;
    }
    public async Task<string> ValidateAndGetTolken(UserLogin userLogin)
    {
        var db = _redis.GetDatabase();
        var tokenString = string.Empty;

        var cachedToken = await db.StringGetAsync(userLogin.Username);
        if (!string.IsNullOrEmpty(cachedToken))
            return cachedToken;

        var user = await BaseQuery().FirstOrDefaultAsync(user => user.Username == userLogin.Username);
        if (user != null && BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
        {
            tokenString = await _userService.GenerateToken(user);
            await db.StringSetAsync(userLogin.Username, tokenString, TimeSpan.FromHours(1));
        }

        return tokenString;
    }

    public async Task CreateUser(UserLogin userDto)
    {
        var user = _mapper.Map<DE.User>(userDto);

        var userExists = await BaseQuery().AnyAsync(user => user.Username == userDto.Username);
        if (userExists)
            throw new Exception("User already exists");

        user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        user.Role = AppConstants.RoleAdmin;

        await _dbSet.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
