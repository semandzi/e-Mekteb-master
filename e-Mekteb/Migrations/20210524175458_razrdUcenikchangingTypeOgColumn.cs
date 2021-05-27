using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class razrdUcenikchangingTypeOgColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivMedzlisa",
                table: "RazrediUcenik");

            migrationBuilder.DropColumn(
                name: "Razred",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<int>(
                name: "MedzlisId",
                table: "RazrediUcenik",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RazredId",
                table: "RazrediUcenik",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedzlisId",
                table: "RazrediUcenik");

            migrationBuilder.DropColumn(
                name: "RazredId",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<string>(
                name: "NazivMedzlisa",
                table: "RazrediUcenik",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Razred",
                table: "RazrediUcenik",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
