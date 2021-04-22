using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class Prisutnost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prisutnosti_AspNetUsers_AplicationUserId1",
                table: "Prisutnosti");

            migrationBuilder.DropIndex(
                name: "IX_Prisutnosti_AplicationUserId1",
                table: "Prisutnosti");

            migrationBuilder.DropColumn(
                name: "AplicationUserId1",
                table: "Prisutnosti");

            migrationBuilder.AlterColumn<string>(
                name: "AplicationUserId",
                table: "Prisutnosti",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Prisutnosti_AplicationUserId",
                table: "Prisutnosti",
                column: "AplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prisutnosti_AspNetUsers_AplicationUserId",
                table: "Prisutnosti",
                column: "AplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prisutnosti_AspNetUsers_AplicationUserId",
                table: "Prisutnosti");

            migrationBuilder.DropIndex(
                name: "IX_Prisutnosti_AplicationUserId",
                table: "Prisutnosti");

            migrationBuilder.AlterColumn<int>(
                name: "AplicationUserId",
                table: "Prisutnosti",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AplicationUserId1",
                table: "Prisutnosti",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prisutnosti_AplicationUserId1",
                table: "Prisutnosti",
                column: "AplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Prisutnosti_AspNetUsers_AplicationUserId1",
                table: "Prisutnosti",
                column: "AplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
