using Autoparts.Api.Features.Functionary.Infraestructure;
using Autoparts.Api.Features.Product.Infraestructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName)
    )
);

builder.Services.AddDbContext<FunctionaryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly(typeof(FunctionaryDbContext).Assembly.FullName)
    )
);


builder.Services.AddScoped<FunctionaryRepository>();

builder.Services.AddScoped<ProductRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Autoparts.Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = "api-docs/{documentName}/swagger.json");
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Autoparts.Api v1"));
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

using var scope = app.Services.CreateScope();

var productDb = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
productDb.Database.Migrate();

var saleDb = scope.ServiceProvider.GetRequiredService<FunctionaryDbContext>();
saleDb.Database.Migrate();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
