using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Migrations
{
    /// <inheritdoc />
    public partial class version06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_DeletedAt",
                table: "Suppliers",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DeletedAt",
                table: "Sales",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_DeletedAt",
                table: "Returns",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DeletedAt",
                table: "Purchases",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedAt",
                table: "Products",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_DeletedAt",
                table: "Manufacturers",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DeletedAt",
                table: "Clients",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedAt",
                table: "Categories",
                column: "DeletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_DeletedAt",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Sales_DeletedAt",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Returns_DeletedAt",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DeletedAt",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedAt",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_DeletedAt",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_DeletedAt",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedAt",
                table: "Categories");
        }
    }
}
