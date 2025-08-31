using System.Reflection.Metadata;

namespace com.split.backend.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity>
{
    /// <summary>
    /// Add an entity to the repository
    /// </summary>
    /// <param name="entity">
    /// The entity to add
    /// </param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);

    
    /// <summary>
    /// FInd an entity by its ID
    /// </summary>
    /// <param name="id">
    /// The id of the entity to find
    /// </param>
    /// <returns>
    ///  The entity if found, otherwise null
    /// </returns>
    Task<TEntity?> FindByIdAsync(int id);
    
    
    /// <summary>
    ///  Update an entity by Its Id
    /// </summary>
    /// <param name="entity">
    /// To entity that is meant to be updated
    /// </param>
    void Update(TEntity entity);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    void Remove(TEntity entity);
    
    
    
    Task<IEnumerable<TEntity>> ListAsync();
}