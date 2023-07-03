using System.Linq.Expressions;
using Models;

namespace Repositories;

public class GenericRepository<T> where T : IndexedEntity
{
    protected readonly UwcDbContext _context;

    public GenericRepository(UwcDbContext context)
    {
        _context = context;
    }

    /// <summary>
    ///     Add one entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    /// <summary>
    ///     Add an amount of entities.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    /// <summary>
    ///     Find all entities that satisfy a condition.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <returns></returns>
    public IEnumerable<T> Find(Expression<Func<T, bool>> condition)
    {
        return _context.Set<T>().Where(condition);
    }

    /// <summary>
    ///     Check if the table has exactly one entity that satisfies the condition.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="resultMessage">Contains the result message, indicates the status of the query.</param>
    /// <returns>True if there exists a single entity that satisfies the condition, else false.</returns>
    public bool ContainsUnique(Expression<Func<T, bool>> condition, out string resultMessage)
    {
        resultMessage = "Success.";

        if (!_context.Set<T>().Any())
        {
            resultMessage = "The table is empty.";
            return false;
        }

        var possibleEntities = _context.Set<T>().Where(condition);
        if (!possibleEntities.Any())
        {
            resultMessage = "Entity does not exist.";
            return false;
        }
        if (possibleEntities.Count() > 1)
        {
            resultMessage = "There are multiple entities that satisfy the condition.";
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Get the unique entity that satisfies the condition.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <returns>The entity if there exists exactly one that satisfies the condition, else null.</returns>
    public T GetUnique(Expression<Func<T, bool>> condition)
    {
        var possibleEntities = _context.Set<T>().Where(condition);
        if (!possibleEntities.Any()) return null!;
        if (possibleEntities.Count() > 1) return null!;

        return possibleEntities.First();
    }

    /// <summary>
    ///     Get all entities available.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    /// <summary>
    ///     Get an entity by ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns></returns>
    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    /// <summary>
    ///     Remove one entity.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    /// <summary>
    ///     Remove an amount of entities.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public bool DoesIdExist(int id)
    {
        return _context.Set<T>().Any(entity => entity.Id == id);
    }

    public bool HasAny()
    {
        return _context.Set<T>().Any();
    }

    public void RemoveById(int id)
    {
        _context.Set<T>().Remove(_context.Set<T>().Single(x => x.Id == id));
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