using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_DataLayer.Migrations
{
    public partial class AlterCartAndCartDetailsTBLsAgainAndAgian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CartSum",
                table: "Cart",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CartSum",
                table: "Cart",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
