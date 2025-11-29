using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                computedColumnSql: "CASE WHEN Stock = 0 THEN 0 WHEN Stock BETWEEN 0 AND 3 THEN 1 WHEN Stock >= 3 THEN 2 ELSE 0 END;",
                oldClrType: typeof(int),
                oldType: "INT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldComputedColumnSql: "CASE WHEN Stock = 0 THEN 0 WHEN Stock BETWEEN 0 AND 3 THEN 1 WHEN Stock >= 3 THEN 2 ELSE 0 END;");
        }
    }
}
