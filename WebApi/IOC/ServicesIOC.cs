using ExampleCrud.WebApi.Application.Interfaces;
using WebApi.Domain.Services;

namespace ExampleCrud.WebApi.IOC;

public static class ServicesIOC
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPersonService, PersonService>();
    }
}