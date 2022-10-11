using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NahlasKitchen.Migrations
{
    public partial class AddingSaleBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OnSale",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnSale",
                table: "Products");
        }
    }
}
