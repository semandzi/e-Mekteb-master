using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class AplicationUserChangingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImeOca",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Starost",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BrojMobitela",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImeiPrezimeRoditelja",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojMobitela",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImeiPrezimeRoditelja",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImeOca",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Starost",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
