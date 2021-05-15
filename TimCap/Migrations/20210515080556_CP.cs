using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimCap.Migrations
{
    public partial class CP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outs");

            migrationBuilder.CreateTable(
                name: "CapDigs",
                columns: table => new
                {
                    UserDig = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CapId = table.Column<int>(type: "int", nullable: false),
                    CapOwnId = table.Column<int>(type: "int", nullable: true),
                    OutTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapDigs", x => new { x.CapId, x.UserDig });
                    table.ForeignKey(
                        name: "FK_CapDigs_Caps_CapOwnId",
                        column: x => x.CapOwnId,
                        principalTable: "Caps",
                        principalColumn: "CapId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CapDigs_CapOwnId",
                table: "CapDigs",
                column: "CapOwnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapDigs");

            migrationBuilder.CreateTable(
                name: "Outs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outs_Caps_OutId",
                        column: x => x.OutId,
                        principalTable: "Caps",
                        principalColumn: "CapId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Outs_OutId",
                table: "Outs",
                column: "OutId");
        }
    }
}
