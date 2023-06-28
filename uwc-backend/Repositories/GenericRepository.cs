using System.Linq.Expressions;
using Models;
using Utilities;

namespace Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : IndexedEntity
{
    protected readonly UwcDbContext _context;

    public GenericRepository(UwcDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public bool ContainSubstring(string parent, string child)
    {
        return parent.Contains(child);
    }

    public bool DoesIdExist(int id)
    {
        return _context.Set<T>().Any(entity => entity.Id == id);
    }

    public bool HasAny()
    {
        return _context.Set<T>().Any();
    }

    public int GetNextId()
    {
        if (!HasAny()) return 1;
        return _context.Set<T>().Max(entity => entity.Id) + 1;
    }

    public void RemoveById(int id)
    {
        _context.Set<T>().Remove(_context.Set<T>().Single(x => x.Id == id));
    }

    public bool CompareDouble(double a, double b)
    {
        return Math.Abs(a - b) < Constants.EPSILON_COMPARE_DOUBLE;
    }

    public int Count(Func<T, bool> predicate)
    {
        var count = 0;
        foreach (var item in _context.Set<T>())
            if (predicate(item))
                count++;
        return count;
    }
}