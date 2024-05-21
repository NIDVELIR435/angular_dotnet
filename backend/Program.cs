using backend.Data;
using backend.Database.Repositories;
using backend.Entities;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using NSwag;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>((options) => options.UseNpgsql(connectionStrings));
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<ProductType>, ProductTypeRepository>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (IServiceScope serviceScope = app.Services.CreateScope())
    {
        StoreContext storeContext = serviceScope.ServiceProvider.GetRequiredService<StoreContext>();
        await storeContext.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(storeContext);
    }

    // app.UseExceptionHandler();
    // app.UseDeveloperExceptionPage();
    
    // Add OpenAPI 3.0 document serving middleware
    // Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();
    // Add web UIs to interact with the document
    // Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("health");

app.Run();