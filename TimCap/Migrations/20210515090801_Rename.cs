using Microsoft.EntityFrameworkCore.Migrations;

namespace TimCap.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutTime",
                table: "CapDigs",
                newName: "DigTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DigTime",
                table: "CapDigs",
                newName: "OutTime");
        }
    }
}
