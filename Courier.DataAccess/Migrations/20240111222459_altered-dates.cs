using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class altereddates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InquiryDate",
                table: "Inquiries");

            migrationBuilder.RenameColumn(
                name: "Posted",
                table: "Orders",
                newName: "PostedDate");

            migrationBuilder.RenameColumn(
                name: "Posted",
                table: "Offers",
                newName: "PostedDate");

            migrationBuilder.RenameColumn(
                name: "Posted",
                table: "Inquiries",
                newName: "PostedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DecisionDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "Orders",
                newName: "Posted");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "Offers",
                newName: "Posted");

            migrationBuilder.RenameColumn(
                name: "PostedDate",
                table: "Inquiries",
                newName: "Posted");

            migrationBuilder.AddColumn<DateTime>(
                name: "InquiryDate",
                table: "Inquiries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
