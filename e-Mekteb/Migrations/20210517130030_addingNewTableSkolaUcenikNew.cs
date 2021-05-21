using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class addingNewTableSkolaUcenikNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkoleUcenika",
                columns: table => new
                {
                    SkolaUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UcenikId = table.Column<string>(nullable: true),
                    NazivSkole = table.Column<string>(nullable: true),
                    VjerouciteljId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkoleUcenika", x => x.SkolaUcenikId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkoleUcenika");
        }
    }
}
