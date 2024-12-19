using System.Reflection;
using AuthApi.IOC;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
})
.AddBearerToken(options =>
{
    options.BearerTokenExpiration = TimeSpan.FromDays(1);
});

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthAPiConnection"),
        b => b.MigrationsAssembly("AuthApi")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.ConfigureApplicationServices();
builder.ConfigureServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UsePathBase("/api");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();