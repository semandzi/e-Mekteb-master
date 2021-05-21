using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class addingNewColumnToSkola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UcenikId",
                table: "Skole",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UcenikId",
                table: "Skole");
        }
    }
}
