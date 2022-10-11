using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NahlasKitchen.Migrations
{
    public partial class thirdCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_userId",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carts_userId",
                table: "Carts");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                column: "userId",
                unique: true);
        }
    }
}
