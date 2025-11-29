using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockStatusOverTime",
                table: "Products",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                computedColumnSql: "CASE\r\n                                               WHEN Stock = 0 THEN 0\r\n                                               WHEN Stock BETWEEN 0 AND 3 THEN 1\r\n                                               WHEN Stock >= 3 THEN 2\r\n                                               ELSE 0\r\n                                             END;",
                oldClrType: typeof(int),
                oldType: "INT",
                oldComputedColumnSql: "SELECT \r\n                                             StockStatus = \r\n                                                 CASE \r\n                                                     WHEN Stock = 0 THEN 0\r\n                                                     WHEN BETWEEN 0 AND 3 THEN 1\r\n                                                     WHEN Stock >= 3 THEN 2\r\n                                                     ELSE 0\r\n                                                 END\r\n                                             FROM [dbo].[Products];");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockStatusOverTime",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StockStatus",
                table: "Products",
                type: "INT",
                nullable: false,
                computedColumnSql: "SELECT \r\n                                             StockStatus = \r\n                                                 CASE \r\n                                                     WHEN Stock = 0 THEN 0\r\n                                                     WHEN BETWEEN 0 AND 3 THEN 1\r\n                                                     WHEN Stock >= 3 THEN 2\r\n                                                     ELSE 0\r\n                                                 END\r\n                                             FROM [dbo].[Products];",
                oldClrType: typeof(int),
                oldType: "INT",
                oldComputedColumnSql: "CASE\r\n                                               WHEN Stock = 0 THEN 0\r\n                                               WHEN Stock BETWEEN 0 AND 3 THEN 1\r\n                                               WHEN Stock >= 3 THEN 2\r\n                                               ELSE 0\r\n                                             END;");
        }
    }
}
