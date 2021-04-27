using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class addinglisttoBiljeska : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BiljeskaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BiljeskaId",
                table: "AspNetUsers",
                column: "BiljeskaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Biljeske_BiljeskaId",
                table: "AspNetUsers",
                column: "BiljeskaId",
                principalTable: "Biljeske",
                principalColumn: "BiljeskaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Biljeske_BiljeskaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BiljeskaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BiljeskaId",
                table: "AspNetUsers");
        }
    }
}
