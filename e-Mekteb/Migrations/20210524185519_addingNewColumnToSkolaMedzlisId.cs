using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class addingNewColumnToSkolaMedzlisId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedzlisId",
                table: "Skole",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Skole_MedzlisId",
                table: "Skole",
                column: "MedzlisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skole_Medzlisi_MedzlisId",
                table: "Skole",
                column: "MedzlisId",
                principalTable: "Medzlisi",
                principalColumn: "MedzlisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skole_Medzlisi_MedzlisId",
                table: "Skole");

            migrationBuilder.DropIndex(
                name: "IX_Skole_MedzlisId",
                table: "Skole");

            migrationBuilder.DropColumn(
                name: "MedzlisId",
                table: "Skole");
        }
    }
}
