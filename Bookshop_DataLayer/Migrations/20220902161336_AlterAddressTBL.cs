using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookshop_DataLayer.Migrations
{
    public partial class AlterAddressTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PROVINCE",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zipcode",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROVINCE",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Addresses");
        }
    }
}
