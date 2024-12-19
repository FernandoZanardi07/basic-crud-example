using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Interfaces;

public interface IUserService
{
    Task<string> GenerateToken(User user);
}
