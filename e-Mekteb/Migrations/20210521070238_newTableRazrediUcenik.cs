using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class newTableRazrediUcenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RazrediUcenik",
                columns: table => new
                {
                    RazredUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    UcenikId = table.Column<string>(nullable: true),
                    MedzlisId = table.Column<int>(nullable: false),
                    SkolaId = table.Column<int>(nullable: false),
                    GodinaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazrediUcenik", x => x.RazredUcenikId);
                    table.ForeignKey(
                        name: "FK_RazrediUcenik_Skole_SkolaId",
                        column: x => x.SkolaId,
                        principalTable: "Skole",
                        principalColumn: "SkolaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RazrediUcenik_SkolaId",
                table: "RazrediUcenik",
                column: "SkolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RazrediUcenik");
        }
    }
}
