using System.Linq.Expressions;

namespace Repositories;

public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Get an entity by ID.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns></returns>
    T GetById(int id);

    /// <summary>
    /// Get all entities available.
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Find all entities that satisfy a condition.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <returns></returns>
    IEnumerable<T> Find(Expression<Func<T, bool>> condition);

    /// <summary>
    /// Add one entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    void Add(T entity);

    /// <summary>
    /// Add an amount of entities.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    void AddRange(IEnumerable<T> entities);

    /// <summary>
    /// Remove one entity.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(T entity);

    /// <summary>
    /// Remove an amount of entities.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    void RemoveRange(IEnumerable<T> entities);
}