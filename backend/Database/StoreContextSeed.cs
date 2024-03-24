using System.Text.Json;
using backend.Entities;

namespace backend.Data;

public class StoreContextSeed
{
    private const string SeedFolderPath = "Database/SeedData";

    public static async Task SeedAsync(StoreContext context)
    {
        await SeedBrands(context);
        await SeedTypes(context);
        await SeedProducts(context);
    }

    private static async Task SeedBrands(StoreContext context)
    {
        if (!context.ProductsBrands.Any())
        {
            string brandsData = await File.ReadAllTextAsync($"{SeedFolderPath}/brands.json");
            List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData)!;

            foreach (var brand in brands)
            {
                context.ProductsBrands.Add(brand);
            }

            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedTypes(StoreContext context)
    {
        if (!context.ProductsTypes.Any())
        {
            string typeData = await File.ReadAllTextAsync($"{SeedFolderPath}/types.json");
            List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typeData)!;

            foreach (var type in types)
            {
                context.ProductsTypes.Add(type);
            }

            await context.SaveChangesAsync();
        }
    }
    private static async Task SeedProducts(StoreContext context)
    {
        if (!context.Products.Any())
        {
            string productsData = await File.ReadAllTextAsync($"{SeedFolderPath}/products.json");
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData)!;

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            await context.SaveChangesAsync();
        }
    }
}