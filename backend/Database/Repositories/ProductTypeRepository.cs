using backend.Data;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories;

public class ProductTypeRepository(StoreContext storeContext) : IRepository<ProductType>
{
    private readonly DbSet<ProductType> _dbSet = storeContext.ProductsTypes;


    public async Task<IReadOnlyList<ProductType>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<ProductType> CreateAsync(ProductType dto)
    {
        ProductType? productType = await _dbSet.FirstOrDefaultAsync(p => p.Name == dto.Name);

        if (productType is not null) throw new Exception("Product type already exist");

        _dbSet.Add(dto);
        await storeContext.SaveChangesAsync();

        return await storeContext.ProductsTypes
            .Where(p => p.Name == dto.Name)
            .FirstAsync();
    }

    public async Task<ProductType?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<ProductType?> UpdateByIdAsync(int id, ProductType dto)
    {
        ProductType? productType = await _dbSet.FindAsync(id);
        if (productType is null) throw new Exception($"Product type with id {id} does not exist.");

        productType.Name = dto.Name;
        await storeContext.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        ProductType? productType = await _dbSet.FindAsync(id);
        if (productType is null) throw new Exception($"Product type with id {id} does not exist.");

        _dbSet.Remove(productType);
        await storeContext.SaveChangesAsync();
    }
}