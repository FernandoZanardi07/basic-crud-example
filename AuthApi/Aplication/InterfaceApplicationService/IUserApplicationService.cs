using AuthApi.API.DTOs;

namespace AuthApi.Application.InterfaceApplicationService;

public interface IUserApplicationService
{
    Task<string> ValidateAndGetTolken(UserLogin userLogin);
}
