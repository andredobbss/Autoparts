using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClientName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Complement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_CellPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address_TaxId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Complement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_CellPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address_TaxId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserName = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "NVARCHAR(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "VARCHAR(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    CompanyName = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Neighborhood = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Complement = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_CellPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address_TaxId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRole_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClaimType = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    ClaimValue = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IdentityUserToken_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    ReturnId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Justification = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    InvoiceNumber = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    Loss = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClientId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.ReturnId);
                    table.ForeignKey(
                        name: "FK_Returns_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Returns_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClientId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    TechnicalDescription = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    SKU = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Compatibility = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    AcquisitionCost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false, defaultValue: 0.0m),
                    SellingPrice = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false, defaultValue: 0.0m),
                    Stock = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    CategoryId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SupplierId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchases_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim_IdentityUser_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnProducts",
                columns: table => new
                {
                    ProductsProductId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ReturnsReturnId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnProducts", x => new { x.ProductsProductId, x.ReturnsReturnId });
                    table.ForeignKey(
                        name: "FK_ReturnProducts_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnProducts_Returns_ReturnsReturnId",
                        column: x => x.ReturnsReturnId,
                        principalTable: "Returns",
                        principalColumn: "ReturnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProducts",
                columns: table => new
                {
                    ProductsProductId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    SalesSaleId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProducts", x => new { x.ProductsProductId, x.SalesSaleId });
                    table.ForeignKey(
                        name: "FK_SaleProducts_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProducts_Sales_SalesSaleId",
                        column: x => x.SalesSaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseProducts",
                columns: table => new
                {
                    ProductsProductId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    PurchasesPurchaseId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseProducts", x => new { x.ProductsProductId, x.PurchasesPurchaseId });
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseProducts_Purchases_PurchasesPurchaseId",
                        column: x => x.PurchasesPurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedAt",
                table: "Categories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientName",
                table: "Clients",
                column: "ClientName");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedAt",
                table: "Clients",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRole_UserId",
                table: "IdentityRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IdentityRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleClaim_RoleId",
                table: "IdentityRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_NormalizedEmail",
                table: "IdentityUser",
                column: "NormalizedEmail",
                unique: true,
                filter: "[NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_NormalizedUserName",
                table: "IdentityUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim_UserId",
                table: "IdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin_UserId",
                table: "IdentityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole_UserId",
                table: "IdentityUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CreatedAt",
                table: "Manufacturers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_ManufacturerId",
                table: "Manufacturers",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedAt",
                table: "Products",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseProducts_PurchasesPurchaseId",
                table: "PurchaseProducts",
                column: "PurchasesPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CreatedAt",
                table: "Purchases",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_InvoiceNumber",
                table: "Purchases",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseId",
                table: "Purchases",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProducts_ReturnsReturnId",
                table: "ReturnProducts",
                column: "ReturnsReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_ClientId",
                table: "Returns",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_CreatedAt",
                table: "Returns",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_ReturnId",
                table: "Returns",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_UserId",
                table: "Returns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_SalesSaleId",
                table: "SaleProducts",
                column: "SalesSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ClientId",
                table: "Sales",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CreatedAt",
                table: "Sales",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleId",
                table: "Sales",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CreatedAt",
                table: "Suppliers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierId",
                table: "Suppliers",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin");

            migrationBuilder.DropTable(
                name: "IdentityUserRole");

            migrationBuilder.DropTable(
                name: "IdentityUserToken");

            migrationBuilder.DropTable(
                name: "PurchaseProducts");

            migrationBuilder.DropTable(
                name: "ReturnProducts");

            migrationBuilder.DropTable(
                name: "SaleProducts");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
