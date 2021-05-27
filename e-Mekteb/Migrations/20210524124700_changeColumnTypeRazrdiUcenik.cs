using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class changeColumnTypeRazrdiUcenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GodinaId",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<int>(
                name: "SkolskaGodinaId",
                table: "RazrediUcenik",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RazrediUcenik_SkolskaGodinaId",
                table: "RazrediUcenik",
                column: "SkolskaGodinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RazrediUcenik_SkolskeGodine_SkolskaGodinaId",
                table: "RazrediUcenik",
                column: "SkolskaGodinaId",
                principalTable: "SkolskeGodine",
                principalColumn: "SkolskaGodinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RazrediUcenik_SkolskeGodine_SkolskaGodinaId",
                table: "RazrediUcenik");

            migrationBuilder.DropIndex(
                name: "IX_RazrediUcenik_SkolskaGodinaId",
                table: "RazrediUcenik");

            migrationBuilder.DropColumn(
                name: "SkolskaGodinaId",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<string>(
                name: "GodinaId",
                table: "RazrediUcenik",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
