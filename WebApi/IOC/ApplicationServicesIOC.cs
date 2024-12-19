using ExampleCrud.WebApi.Application.ApplicationService;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;

namespace ExampleCrud.WebApi.IOC;

public static class ApplicationServicesIOC
{
    public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(BaseApplicationService<>));
        builder.Services.AddScoped<IPersonApplicationService, PersonApplicationService>();
        builder.Services.AddScoped<IContactApplicationService, ContactApplicationService>();
    }
}
