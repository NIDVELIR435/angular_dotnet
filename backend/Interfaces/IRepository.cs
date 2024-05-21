using Microsoft.EntityFrameworkCore;

namespace backend.Interfaces;

public interface IRepository<TEntity>
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity> CreateAsync(TEntity dto);
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> UpdateByIdAsync(int id, TEntity dto);
    Task DeleteByIdAsync(int id);
}