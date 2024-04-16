using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedpostedtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Posted",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Posted",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Posted",
                table: "Inquiries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Posted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Posted",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Posted",
                table: "Inquiries");
        }
    }
}
