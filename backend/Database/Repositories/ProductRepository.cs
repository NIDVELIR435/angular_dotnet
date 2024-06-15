using backend.Data;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories;

public class ProductRepository(StoreContext storeContext) : IRepository<Product>
{
    private readonly DbSet<Product> _dbSet = storeContext.Products;

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await _dbSet
            // includes relations so in our requests join will be attached  
            .Include(product => product.ProductType)
            .Include(product => product.Brand)
            .ToListAsync();
    }


    public async Task<Product> CreateAsync(Product dto)
    {
        Product? product = await _dbSet.FirstOrDefaultAsync(p => p.Name == dto.Name);
        if (product is not null) throw new Exception("Product already exist");

        _dbSet.Add(dto);
        await storeContext.SaveChangesAsync();

        return await _dbSet.Where(p => p.Name == dto.Name)
            .FirstAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Product?> UpdateByIdAsync(int id, Product productDto)
    {
        Product? product = await _dbSet.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        product.Name = productDto.Name;
        await storeContext.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        Product? product = await _dbSet.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        _dbSet.Remove(product);
        await storeContext.SaveChangesAsync();
    }
}