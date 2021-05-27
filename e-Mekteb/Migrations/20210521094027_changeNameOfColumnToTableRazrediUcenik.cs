using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class changeNameOfColumnToTableRazrediUcenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedzlisId",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<string>(
                name: "NazivMedzlisa",
                table: "RazrediUcenik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivMedzlisa",
                table: "RazrediUcenik");

            migrationBuilder.AddColumn<int>(
                name: "MedzlisId",
                table: "RazrediUcenik",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
