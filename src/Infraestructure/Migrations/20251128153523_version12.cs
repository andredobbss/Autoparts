using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                computedColumnSql: "CASE\r\n                                               WHEN Stock = 0 THEN 0\r\n                                               WHEN Stock BETWEEN 0 AND 3 THEN 1\r\n                                               WHEN Stock >= 3 THEN 2\r\n                                               ELSE 0\r\n                                             END;");
        }
    }
}
