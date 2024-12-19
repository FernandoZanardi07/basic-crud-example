using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AuthApi.Application;

public class BaseApplicationService<T> where T : Domain.Entities.BaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    public readonly IMapper _mapper;

    public BaseApplicationService(DbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _mapper = mapper;
    }

    protected IQueryable<T> BaseQuery()
        => _dbSet.AsNoTracking().Where(entity => !entity.IsDeleted);

    public virtual T Add(T entity)
    {
        _dbSet.Add(entity);
        entity.PrepareToInsert();

        return entity;
    }

    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }

    protected virtual void Dispose()
    {
        _context.Dispose();
    }
}
