using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}