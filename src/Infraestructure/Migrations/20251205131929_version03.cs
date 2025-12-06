using System;
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
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "IdentityUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "IdentityUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "IdentityUser");
        }
    }
}
