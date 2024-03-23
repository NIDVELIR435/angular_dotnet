using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories;

public class ProductRepository(StoreContext storeContext) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetProductAsync()
    {
        return  await storeContext.Products.ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product productDto)
    {
        Product? product = await storeContext.Products
            .FirstOrDefaultAsync(p => p.Name == productDto.Name);
        if (product is not null)  throw new Exception("Product already exist");

        storeContext.Products.Add(productDto);
        await storeContext.SaveChangesAsync();

        return await storeContext.Products
            .Where(p => p.Name == productDto.Name)
            .FirstAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await storeContext.Products.FindAsync(id);
    }

    public async Task<Product?> UpdateProductAsync(int id, Product productDto)
    {
        Product? product = await storeContext.Products.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        product.Name = productDto.Name;
        await storeContext.SaveChangesAsync();
        return await GetProductByIdAsync(id);
    }

    public async Task DeleteProductByIdAsync(int id)
    {
        Product? product = await storeContext.Products.FindAsync(id);
        if (product is null) throw new Exception($"Product with id {id} does not exist.");

        storeContext.Products.Remove(product);
        await storeContext.SaveChangesAsync();
    }
}