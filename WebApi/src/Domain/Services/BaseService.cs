namespace ExampleCrud.WebApi.Domain.Services;

public abstract class BaseService : IDisposable
{
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
