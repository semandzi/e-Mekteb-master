using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class vjerouciteljAktivnostUklanjanjeKolone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivPredmeta",
                table: "Predaje");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NazivPredmeta",
                table: "Predaje",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
