using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class changetypeinttostringtablerazredi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razredi_AspNetUsers_AplicationUserId1",
                table: "Razredi");

            migrationBuilder.DropIndex(
                name: "IX_Razredi_AplicationUserId1",
                table: "Razredi");

            migrationBuilder.DropColumn(
                name: "AplicationUserId1",
                table: "Razredi");

            migrationBuilder.AlterColumn<string>(
                name: "AplicationUserId",
                table: "Razredi",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Razredi_AplicationUserId",
                table: "Razredi",
                column: "AplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Razredi_AspNetUsers_AplicationUserId",
                table: "Razredi",
                column: "AplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razredi_AspNetUsers_AplicationUserId",
                table: "Razredi");

            migrationBuilder.DropIndex(
                name: "IX_Razredi_AplicationUserId",
                table: "Razredi");

            migrationBuilder.AlterColumn<int>(
                name: "AplicationUserId",
                table: "Razredi",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AplicationUserId1",
                table: "Razredi",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Razredi_AplicationUserId1",
                table: "Razredi",
                column: "AplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Razredi_AspNetUsers_AplicationUserId1",
                table: "Razredi",
                column: "AplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
