using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class addColumnToRazredi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VjerouciteljId",
                table: "Razredi",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Razredi_VjerouciteljId",
                table: "Razredi",
                column: "VjerouciteljId");

            migrationBuilder.AddForeignKey(
                name: "FK_Razredi_AspNetUsers_VjerouciteljId",
                table: "Razredi",
                column: "VjerouciteljId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razredi_AspNetUsers_VjerouciteljId",
                table: "Razredi");

            migrationBuilder.DropIndex(
                name: "IX_Razredi_VjerouciteljId",
                table: "Razredi");

            migrationBuilder.DropColumn(
                name: "VjerouciteljId",
                table: "Razredi");
        }
    }
}
