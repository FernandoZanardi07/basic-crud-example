using AuthApi.Domain.Interfaces;
using AuthApi.Domain.Services;

namespace AuthApi.IOC;

public static class ServicesIOC
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
    }
}