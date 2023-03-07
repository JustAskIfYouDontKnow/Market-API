using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.API.Migrations
{
    /// <inheritdoc />
    public partial class changeKeyNameCreatedByUserIdForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Product",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CreatedByUserId",
                table: "Product",
                newName: "IX_Product_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_UserId",
                table: "Product",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_UserId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Product",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserId",
                table: "Product",
                newName: "IX_Product_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                table: "Product",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
