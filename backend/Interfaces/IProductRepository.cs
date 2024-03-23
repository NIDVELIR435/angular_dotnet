using backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductAsync();
    Task<Product> CreateProductAsync(Product productDto);
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> UpdateProductAsync(int id, Product productDto);
    Task DeleteProductByIdAsync(int id);
}