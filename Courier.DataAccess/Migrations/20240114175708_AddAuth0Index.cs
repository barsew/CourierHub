using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAuth0Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Auth0Id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Auth0Id",
                table: "Users",
                column: "Auth0Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Auth0Id",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Auth0Id",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
