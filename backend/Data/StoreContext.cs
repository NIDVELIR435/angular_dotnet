using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductsTypes { get; set; }
    public DbSet<ProductBrand> ProductsBrands { get; set; }
}