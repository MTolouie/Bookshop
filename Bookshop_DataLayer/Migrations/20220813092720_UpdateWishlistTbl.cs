using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_DataLayer.Migrations
{
    public partial class UpdateWishlistTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Books_BookId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_BookId",
                table: "Wishlists");

            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "Wishlists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "DiscountedPrice",
                table: "Wishlists",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Wishlists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscounted",
                table: "Wishlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Wishlists",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "IsDiscounted",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Wishlists");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_BookId",
                table: "Wishlists",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Books_BookId",
                table: "Wishlists",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
