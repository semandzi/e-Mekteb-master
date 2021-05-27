using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class razrdUcenikchangingTypeOgColumnRazred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazredId",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<string>(
                name: "Razred",
                table: "RazrediUcenik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Razred",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<int>(
                name: "RazredId",
                table: "RazrediUcenik",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
