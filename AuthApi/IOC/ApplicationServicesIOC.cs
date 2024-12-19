using AuthApi.Application;
using AuthApi.Application.InterfaceApplicationService;

namespace AuthApi.IOC;

public static class ApplicationServicesIOC
{
    public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(BaseApplicationService<>));
        builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
    }
}
