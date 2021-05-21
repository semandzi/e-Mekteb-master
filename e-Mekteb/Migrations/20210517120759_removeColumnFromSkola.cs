using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class removeColumnFromSkola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UcenikId",
                table: "Skole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UcenikId",
                table: "Skole",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
