using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class skola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skole_AspNetUsers_AplicationUserId1",
                table: "Skole");

            migrationBuilder.DropIndex(
                name: "IX_Skole_AplicationUserId1",
                table: "Skole");

            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "Skole");

            migrationBuilder.DropColumn(
                name: "AplicationUserId1",
                table: "Skole");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Skole",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Skole",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostanskiBroj",
                table: "Skole",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Skole");

            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Skole");

            migrationBuilder.DropColumn(
                name: "PostanskiBroj",
                table: "Skole");

            migrationBuilder.AddColumn<int>(
                name: "AplicationUserId",
                table: "Skole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AplicationUserId1",
                table: "Skole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skole_AplicationUserId1",
                table: "Skole",
                column: "AplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Skole_AspNetUsers_AplicationUserId1",
                table: "Skole",
                column: "AplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
