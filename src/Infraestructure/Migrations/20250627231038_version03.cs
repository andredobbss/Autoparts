using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPurchase",
                table: "Purchases",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Purchases",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Purchases");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPurchase",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");
        }
    }
}
