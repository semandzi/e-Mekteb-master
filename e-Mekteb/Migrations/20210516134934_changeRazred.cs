using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class changeRazred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "SkolskaGodinaId",
                table: "Razredi",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MedzlisId",
                table: "Razredi",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SkolskaGodinaId",
                table: "Razredi",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedzlisId",
                table: "Razredi",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VjerouciteljId",
                table: "Razredi",
                type: "nvarchar(450)",
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
    }
}
