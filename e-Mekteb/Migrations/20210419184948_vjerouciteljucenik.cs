using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class vjerouciteljucenik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VjerouciteljUcenik",
                columns: table => new
                {
                    VjerouciteljUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UcenikId = table.Column<string>(nullable: true),
                    VjerouciteljId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VjerouciteljUcenik", x => x.VjerouciteljUcenikId);
                    table.ForeignKey(
                        name: "FK_VjerouciteljUcenik_AspNetUsers_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VjerouciteljUcenik_AspNetUsers_VjerouciteljId",
                        column: x => x.VjerouciteljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VjerouciteljUcenik_UcenikId",
                table: "VjerouciteljUcenik",
                column: "UcenikId");

            migrationBuilder.CreateIndex(
                name: "IX_VjerouciteljUcenik_VjerouciteljId",
                table: "VjerouciteljUcenik",
                column: "VjerouciteljId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VjerouciteljUcenik");
        }
    }
}
