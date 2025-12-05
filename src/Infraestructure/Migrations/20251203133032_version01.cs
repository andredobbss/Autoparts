using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoparts.Api.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class version01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Products")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ProductsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "SysEndTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:TemporalIsPeriodEndColumn", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SysStartTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("SqlServer:TemporalIsPeriodStartColumn", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SysEndTime",
                table: "Products")
                .Annotation("SqlServer:TemporalIsPeriodEndColumn", true);

            migrationBuilder.DropColumn(
                name: "SysStartTime",
                table: "Products")
                .Annotation("SqlServer:TemporalIsPeriodStartColumn", true);

            migrationBuilder.AlterTable(
                name: "Products")
                .OldAnnotation("SqlServer:IsTemporal", true)
                .OldAnnotation("SqlServer:TemporalHistoryTableName", "ProductsHistory")
                .OldAnnotation("SqlServer:TemporalHistoryTableSchema", null)
                .OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "SysEndTime")
                .OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "SysStartTime");
        }
    }
}
