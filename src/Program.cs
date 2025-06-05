using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.Infraestructure;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Middleware;
using Autoparts.Api.Shared.ValueObjects;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#region Initials Configurations

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

#endregion

#region Configure DbContext

builder.Services.AddDbContext<AutopartsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly(typeof(AutopartsDbContext).Assembly.FullName)
    )
);

#endregion

#region Configure Identity

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AutopartsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
});

#endregion

# region Register Repositories

builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ManufacturerRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<SkuGenerator>();
builder.Services.AddScoped<PurchaseRepository>();
builder.Services.AddScoped<ReturnRepository>();
builder.Services.AddScoped<SaleRepository>();
builder.Services.AddScoped<SupplierRepository>();
builder.Services.AddScoped<UserRepository>();

# endregion

#region Register MediatR

var myHandlers = AppDomain.CurrentDomain.Load("Autoparts.Api");
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(myHandlers);
    //cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

#endregion

#region Register Validators

builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<Supplier>, SupplierValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();

#endregion

#region Register Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Autoparts.Api", Version = "v1" });
});

#endregion

var app = builder.Build();

#region Configure Swagger

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

#endregion

#region Ensure the database is created and migrations are applied

using var scope = app.Services.CreateScope();
var productDb = scope.ServiceProvider.GetRequiredService<AutopartsDbContext>();
productDb.Database.Migrate();

#endregion

#region Configure Middleware

app.UseExceptionHandling();

#endregion

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
