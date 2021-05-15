using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimCap.Migrations
{
    public partial class SecondCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Caps",
                newName: "InTime");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Caps",
                newName: "CapId");

            migrationBuilder.CreateTable(
                name: "Outs",
                columns: table => new
                {
                    OutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outs", x => x.OutId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outs");

            migrationBuilder.RenameColumn(
                name: "InTime",
                table: "Caps",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "CapId",
                table: "Caps",
                newName: "Id");
        }
    }
}
