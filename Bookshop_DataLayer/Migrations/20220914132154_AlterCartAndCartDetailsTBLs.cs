using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_DataLayer.Migrations
{
    public partial class AlterCartAndCartDetailsTBLs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Cart",
                newName: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Cart",
                newName: "OrderId");
        }
    }
}
