using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class newColumnInSkoleUcenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkolaId",
                table: "SkoleUcenika",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SkoleUcenika_SkolaId",
                table: "SkoleUcenika",
                column: "SkolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_SkoleUcenika_Skole_SkolaId",
                table: "SkoleUcenika",
                column: "SkolaId",
                principalTable: "Skole",
                principalColumn: "SkolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkoleUcenika_Skole_SkolaId",
                table: "SkoleUcenika");

            migrationBuilder.DropIndex(
                name: "IX_SkoleUcenika_SkolaId",
                table: "SkoleUcenika");

            migrationBuilder.DropColumn(
                name: "SkolaId",
                table: "SkoleUcenika");
        }
    }
}
