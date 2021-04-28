using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class obavijestiForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VjerouciteljId",
                table: "Obavijesti",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti_VjerouciteljId",
                table: "Obavijesti",
                column: "VjerouciteljId");

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijesti_AspNetUsers_VjerouciteljId",
                table: "Obavijesti",
                column: "VjerouciteljId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obavijesti_AspNetUsers_VjerouciteljId",
                table: "Obavijesti");

            migrationBuilder.DropIndex(
                name: "IX_Obavijesti_VjerouciteljId",
                table: "Obavijesti");

            migrationBuilder.DropColumn(
                name: "VjerouciteljId",
                table: "Obavijesti");
        }
    }
}
