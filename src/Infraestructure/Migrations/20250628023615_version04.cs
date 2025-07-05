using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "PurchaseProducts",
                newName: "AcquisitionCost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AcquisitionCost",
                table: "PurchaseProducts",
                newName: "SellingPrice");
        }
    }
}
