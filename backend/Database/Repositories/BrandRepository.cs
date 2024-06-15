using backend.Data;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Database.Repositories;

public class BrandRepository(StoreContext storeContext) : IRepository<Brand>
{
    private readonly DbSet<Brand> _dbSet = storeContext.Brands;
    
    public async Task<IReadOnlyList<Brand>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Brand> CreateAsync(Brand dto)
    {
        Brand? brand = await _dbSet.FirstOrDefaultAsync(p => p.Name == dto.Name);
        if (brand is not null) throw new BadHttpRequestException("Brand already exist");

        _dbSet.Add(dto);
        await storeContext.SaveChangesAsync();

        return await storeContext.Brands
            .Where(p => p.Name == dto.Name)
            .FirstAsync();
    }

    public async Task<Brand?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Brand?> UpdateByIdAsync(int id, Brand dto)
    {
        Brand? brand = await _dbSet.FindAsync(id);
        if (brand is null) throw new Exception($"Brand with id {id} does not exist.");

        brand.Name = dto.Name;
        await storeContext.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        Brand? brand = await _dbSet.FindAsync(id);
        if (brand is null) throw new Exception($"Brand with id {id} does not exist.");

        _dbSet.Remove(brand);
        await storeContext.SaveChangesAsync();
    }
}