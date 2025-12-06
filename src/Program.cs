#region Using Directives

using Autoparts.Api.Features.Categories.Apis;
using Autoparts.Api.Features.Categories.Domain;
using Autoparts.Api.Features.Clients.Apis;
using Autoparts.Api.Features.Clients.Domain;
using Autoparts.Api.Features.Manufacturers.Apis;
using Autoparts.Api.Features.Manufacturers.Domain;
using Autoparts.Api.Features.Products.Apis;
using Autoparts.Api.Features.Products.Domain;
using Autoparts.Api.Features.Purchases.Apis;
using Autoparts.Api.Features.Purchases.Domain;
using Autoparts.Api.Features.Returns.Apis;
using Autoparts.Api.Features.Returns.Domain;
using Autoparts.Api.Features.Sales.Apis;
using Autoparts.Api.Features.Sales.Domain;
using Autoparts.Api.Features.Suppliers.Apis;
using Autoparts.Api.Features.Suppliers.Domain;
using Autoparts.Api.Features.Users.Apis;
using Autoparts.Api.Features.Users.Domain;
using Autoparts.Api.Features.Users.Services;
using Autoparts.Api.Infraestructure.Persistence;
using Autoparts.Api.Shared.Email;
using Autoparts.Api.Shared.Middleware;
using Autoparts.Api.Shared.Products.Repository;
using Autoparts.Api.Shared.Resources;
using Autoparts.Api.Shared.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

# region Configure Authentication and Authorization

var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException(Resource.INVALID_SECRET_KEY);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization();

# endregion

# region Register Services and Repositories

builder.Services.AddScoped<ISkuGenerator, SkuGenerator>();
builder.Services.AddScoped<ITokenService, TokenService>();
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

#region Register Validators

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

//app.UseAuthentication();

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
