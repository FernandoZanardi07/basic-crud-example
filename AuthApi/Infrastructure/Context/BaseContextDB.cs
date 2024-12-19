using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entityMethod = typeof(ModelBuilder).GetMethods()
            .First(m => m.Name == "Entity" && m.IsGenericMethod && m.GetParameters().Length == 0);

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.IsClass && !type.IsAbstract && type.IsPublic && type.Namespace == "AuthApi.Domain.Entities")
            {
                entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, null);
            }
        }
    }
}