using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Products_ProductsProductId",
                table: "PurchaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Purchases_PurchasesPurchaseId",
                table: "PurchaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProducts_Products_ProductsProductId",
                table: "ReturnProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProducts_Returns_ReturnsReturnId",
                table: "ReturnProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Products_ProductsProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sales_SalesSaleId",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "SalesSaleId",
                table: "SaleProducts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "SaleProducts",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProducts_SalesSaleId",
                table: "SaleProducts",
                newName: "IX_SaleProducts_ProductId");

            migrationBuilder.RenameColumn(
                name: "ReturnsReturnId",
                table: "ReturnProducts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "ReturnProducts",
                newName: "ReturnId");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnProducts_ReturnsReturnId",
                table: "ReturnProducts",
                newName: "IX_ReturnProducts_ProductId");

            migrationBuilder.RenameColumn(
                name: "PurchasesPurchaseId",
                table: "PurchaseProducts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "PurchaseProducts",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProducts_PurchasesPurchaseId",
                table: "PurchaseProducts",
                newName: "IX_PurchaseProducts_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SaleProducts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalItem",
                table: "SaleProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ReturnProducts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "ReturnProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalItem",
                table: "ReturnProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchase",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchaseProducts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SellingPrice",
                table: "PurchaseProducts",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalItem",
                table: "PurchaseProducts",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Products_ProductId",
                table: "PurchaseProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Purchases_PurchaseId",
                table: "PurchaseProducts",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProducts_Products_ProductId",
                table: "ReturnProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProducts_Returns_ReturnId",
                table: "ReturnProducts",
                column: "ReturnId",
                principalTable: "Returns",
                principalColumn: "ReturnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Products_ProductId",
                table: "SaleProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Products_ProductId",
                table: "PurchaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseProducts_Purchases_PurchaseId",
                table: "PurchaseProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProducts_Products_ProductId",
                table: "ReturnProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProducts_Returns_ReturnId",
                table: "ReturnProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Products_ProductId",
                table: "SaleProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Sales_SaleId",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "SaleProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ReturnProducts");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ReturnProducts");

            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "ReturnProducts");

            migrationBuilder.DropColumn(
                name: "TotalPurchase",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "TotalItem",
                table: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleProducts",
                newName: "SalesSaleId");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "SaleProducts",
                newName: "ProductsProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProducts_ProductId",
                table: "SaleProducts",
                newName: "IX_SaleProducts_SalesSaleId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ReturnProducts",
                newName: "ReturnsReturnId");

            migrationBuilder.RenameColumn(
                name: "ReturnId",
                table: "ReturnProducts",
                newName: "ProductsProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnProducts_ProductId",
                table: "ReturnProducts",
                newName: "IX_ReturnProducts_ReturnsReturnId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PurchaseProducts",
                newName: "PurchasesPurchaseId");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "PurchaseProducts",
                newName: "ProductsProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseProducts_ProductId",
                table: "PurchaseProducts",
                newName: "IX_PurchaseProducts_PurchasesPurchaseId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Sales",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Returns",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchases",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Products_ProductsProductId",
                table: "PurchaseProducts",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseProducts_Purchases_PurchasesPurchaseId",
                table: "PurchaseProducts",
                column: "PurchasesPurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProducts_Products_ProductsProductId",
                table: "ReturnProducts",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProducts_Returns_ReturnsReturnId",
                table: "ReturnProducts",
                column: "ReturnsReturnId",
                principalTable: "Returns",
                principalColumn: "ReturnId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Products_ProductsProductId",
                table: "SaleProducts",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Sales_SalesSaleId",
                table: "SaleProducts",
                column: "SalesSaleId",
                principalTable: "Sales",
                principalColumn: "SaleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
