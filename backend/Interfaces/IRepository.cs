using backend.Entities;

namespace backend.Interfaces;

public interface IRepository<TEntity> where TEntity : IdEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity dto);
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> UpdateByIdAsync(int id, TEntity dto);
    Task DeleteByIdAsync(int id);
}