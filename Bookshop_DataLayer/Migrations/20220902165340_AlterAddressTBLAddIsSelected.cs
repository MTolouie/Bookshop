using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_DataLayer.Migrations
{
    public partial class AlterAddressTBLAddIsSelected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "Addresses");
        }
    }
}
