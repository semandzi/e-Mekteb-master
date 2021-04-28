using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class obavijesti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Biljeske_BiljeskaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BiljeskaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BiljeskaId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    ObavijestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.ObavijestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.AddColumn<int>(
                name: "BiljeskaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BiljeskaId",
                table: "AspNetUsers",
                column: "BiljeskaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Biljeske_BiljeskaId",
                table: "AspNetUsers",
                column: "BiljeskaId",
                principalTable: "Biljeske",
                principalColumn: "BiljeskaId");
        }
    }
}
