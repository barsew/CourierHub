using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedaddresstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DestinationAddress",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "SourceAddress",
                table: "Inquiries");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAddressId",
                table: "Inquiries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceAddressId",
                table: "Inquiries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_AddressId",
                table: "Subject",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Address_DestinationAddressId",
                table: "Inquiries",
                column: "DestinationAddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiries_Address_SourceAddressId",
                table: "Inquiries",
                column: "SourceAddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Address_AddressId",
                table: "Subject",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Address_DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiries_Address_SourceAddressId",
                table: "Inquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Address_AddressId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Subject_AddressId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropIndex(
                name: "IX_Inquiries_SourceAddressId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "DestinationAddressId",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "SourceAddressId",
                table: "Inquiries");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Subject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceAddress",
                table: "Inquiries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
