using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAtUserId_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatedByUserId",
                table: "Product",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                table: "Product",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_User_CreatedByUserId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CreatedByUserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Product");
        }
    }
}
