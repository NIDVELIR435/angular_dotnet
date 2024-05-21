using backend.Data;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories;

public class ProductRepository(StoreContext storeContext) : IRepository<Product>
{
    private DbSet<Product> dbSet = storeContext.Products;
    
    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }


    public async Task<Product> CreateAsync(Product dto)
    {
        Product? product = await dbSet.FirstOrDefaultAsync(p => p.Name == dto.Name);
        if (product is not null) throw new Exception("Product already exist");

        dbSet.Add(dto);
        await storeContext.SaveChangesAsync();

        return await dbSet.Where(p => p.Name == dto.Name)
            .FirstAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<Product?> UpdateByIdAsync(int id, Product productDto)
    {
        Product? product = await dbSet.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        product.Name = productDto.Name;
        await storeContext.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        Product? product = await dbSet.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        dbSet.Remove(product);
        await storeContext.SaveChangesAsync();
    }
}