using Microsoft.EntityFrameworkCore.Migrations;

namespace TimCap.Migrations
{
    public partial class FixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapDigs_Caps_CapOwnId",
                table: "CapDigs");

            migrationBuilder.DropIndex(
                name: "IX_CapDigs_CapOwnId",
                table: "CapDigs");

            migrationBuilder.DropColumn(
                name: "CapOwnId",
                table: "CapDigs");

            migrationBuilder.AddForeignKey(
                name: "FK_CapDigs_Caps_CapId",
                table: "CapDigs",
                column: "CapId",
                principalTable: "Caps",
                principalColumn: "CapId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapDigs_Caps_CapId",
                table: "CapDigs");

            migrationBuilder.AddColumn<int>(
                name: "CapOwnId",
                table: "CapDigs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CapDigs_CapOwnId",
                table: "CapDigs",
                column: "CapOwnId");

            migrationBuilder.AddForeignKey(
                name: "FK_CapDigs_Caps_CapOwnId",
                table: "CapDigs",
                column: "CapOwnId",
                principalTable: "Caps",
                principalColumn: "CapId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
