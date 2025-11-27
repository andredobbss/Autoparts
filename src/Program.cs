#region Using Directives

using Autoparts.Api.Features.Categories.Apis;
using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Categories.Infraestructure;
using Autoparts.Api.Features.Clients.Apis;
using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Clients.Infraestructure;
using Autoparts.Api.Features.Manufacturers.Apis;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Manufacturers.Infraestructure;
using Autoparts.Api.Features.Products.Apis;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Products.Infraestructure;
using Autoparts.Api.Features.Purchases.Apis;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Purchases.Infraestructure;
using Autoparts.Api.Features.Returns.Apis;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Returns.Infraestructure;
using Autoparts.Api.Features.Sales.Apis;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Sales.Infraestructure;
using Autoparts.Api.Features.Suppliers.Apis;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Suppliers.Infraestructure;
using Autoparts.Api.Features.Users.Apis;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.Infraestructure;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Email;
using Autoparts.Api.Shared.Middleware;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Products.Stock;
using Autoparts.Api.Shared.ValueObejct;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

#endregion

#region Initials Configurations

var builder = WebApplication.CreateBuilder(args);

// minimal API
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddApplicationCookie();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

#endregion

#region Register Swagger

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Autoparts.Api", Version = "v1" });
});

#endregion

#region Configure DbContext

string connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION_AUTOPARTS") ??
                          builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AutopartsDbContext>(options =>
    options.UseSqlServer(connectionString,
        sql => sql.MigrationsAssembly(typeof(AutopartsDbContext).Assembly.FullName)
    )
);

#endregion

#region Configure Identity

builder.Services
    .AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AutopartsDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager();

#endregion

# region Register Repositories

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISkuGenerator, SkuGenerator>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStockCalculator, StockCalculator>();

builder.Services.AddScoped<IProductList, ProductList>();

builder.Services.AddSingleton<IEmailSender<User>, NoOpEmailSender>();

# endregion

#region Register MediatR

var myHandlers = AppDomain.CurrentDomain.Load("Autoparts.Api");
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(myHandlers);
});

#endregion

#region Register AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region Register Validators

builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();
builder.Services.AddScoped<IValidator<Client>, ClientValidator>();
builder.Services.AddScoped<IValidator<Manufacturer>, ManufacturerValidator>();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();
builder.Services.AddScoped<IValidator<Purchase>, PurchaseValidator>();
builder.Services.AddScoped<IValidator<Return>, ReturnValidator>();
builder.Services.AddScoped<IValidator<Sale>, SaleValidator>();
builder.Services.AddScoped<IValidator<Supplier>, SupplierValidator>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();

#endregion

# region Build the application

var app = builder.Build();

#endregion

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

#region Configure HTTP request pipeline

app.UseHttpsRedirection();

#endregion

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

#region Configure Minimal APIs

app.MapCategoryApi();
app.MapClientApi();
app.MapManufactureApi();
app.MapProductApi();
app.MapPurchaseApi();
app.MapReturnApi();
app.MapSaleApi();
app.MapSupplierApi();
app.MapUserApi();

#endregion

app.Run();
