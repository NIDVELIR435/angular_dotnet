using backend.Data;
using backend.Data.Repositories;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreContext>((options) => options.UseNpgsql(connectionStrings));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope serviceScope = app.Services.CreateScope())
    {
        StoreContext storeContext = serviceScope.ServiceProvider.GetRequiredService<StoreContext>();
        await StoreContextSeed.SeedAsync(storeContext);
    }
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("health");

app.Run();