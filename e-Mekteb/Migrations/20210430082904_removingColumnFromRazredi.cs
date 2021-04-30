using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class removingColumnFromRazredi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Razredi_AspNetUsers_AplicationUserId",
                table: "Razredi");

            migrationBuilder.DropIndex(
                name: "IX_Razredi_AplicationUserId",
                table: "Razredi");

            migrationBuilder.DropColumn(
                name: "AplicationUserId",
                table: "Razredi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AplicationUserId",
                table: "Razredi",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
