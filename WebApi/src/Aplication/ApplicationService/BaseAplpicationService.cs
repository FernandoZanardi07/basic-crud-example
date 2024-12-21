using System.Linq.Expressions;
using AutoMapper;
using ExampleCrud.WebApi.Application.InterfaceApplicationService;
using Microsoft.EntityFrameworkCore;

namespace ExampleCrud.WebApi.Application.ApplicationService;

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
    {
        return _dbSet.AsNoTracking().Where(entity => !entity.IsDeleted);
    }

    public virtual T Add(T entity)
    {
        _dbSet.Add(entity);
        entity.PrepareToInsert();

        return entity;
    }

    public virtual T? GetById(Guid id)
    {
        return BaseQuery().Where(entity => entity.Id == id).FirstOrDefault();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }

    public virtual void Update(T entity)
    {
        entity.PrepareToUpdate();
        _dbSet.Update(entity);
        _context.SaveChanges();
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
