using Microsoft.EntityFrameworkCore;

public class ContextDb : BaseDbContext
{
    public ContextDb(DbContextOptions<ContextDb> options)
    : base(options)
    { }
}