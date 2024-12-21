namespace ExampleCrud.WebApi.Domain.Entities;

public abstract class BaseEntity : IDisposable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void PrepareToInsert()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsDeleted = false;
        DeletedAt = null;
    }

    public void PrepareToUpdate()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void PrepareToDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}