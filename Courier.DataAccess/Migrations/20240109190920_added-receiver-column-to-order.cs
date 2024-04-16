using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedreceivercolumntoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Subject_SenderId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Subject_ReceiverId",
                table: "Orders",
                column: "ReceiverId",
                principalTable: "Subject",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Subject_SenderId",
                table: "Orders",
                column: "SenderId",
                principalTable: "Subject",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Subject_ReceiverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Subject_SenderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReceiverId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Subject_SenderId",
                table: "Orders",
                column: "SenderId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
